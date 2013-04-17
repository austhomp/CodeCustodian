namespace CodeCustodian.TFS
{
    using System;
    using System.Collections.Generic;

    using System.Linq;

    public class TfsWorkspaceQueryService : ITfsWorkspaceQueryService
    {
        private readonly ITfsCommandFactory commandFactory;

        private readonly ITfsCommandOutputParser tfsCommandOutputParser;

        public TfsWorkspaceQueryService(ITfsCommandFactory commandFactory, ITfsCommandOutputParser tfsCommandOutputParser)
        {
            if (commandFactory == null)
            {
                throw new ArgumentNullException("commandFactory");
            }

            if (tfsCommandOutputParser == null)
            {
                throw new ArgumentNullException("tfsCommandOutputParser");
            }

            this.commandFactory = commandFactory;
            this.tfsCommandOutputParser = tfsCommandOutputParser;
        }

        public IEnumerable<TfsWorkspaceResult> RetrieveAll()
        {
            var listWorkspacesCommand = this.commandFactory.Create(TfsCommandType.ListWorkspaces, string.Empty);
            var listWorkspacesResult = listWorkspacesCommand.Run();
            System.Diagnostics.Debug.WriteLine("output" + listWorkspacesResult.Output);

            var workspaces = this.tfsCommandOutputParser.ParseWorkspacesOutput(listWorkspacesResult.Output);
            var results = new List<TfsWorkspaceResult>();

            foreach (var workspace in workspaces)
            {
                var mappedFoldersCommand = this.commandFactory.Create(TfsCommandType.ListWorkingFoldersForWorkspace, workspace);
                var mappedFoldersResult = mappedFoldersCommand.Run();
                var mappedFolders = this.tfsCommandOutputParser.ParseMappedFoldersOutput(mappedFoldersResult.Output);
                results.Add(new TfsWorkspaceResult(workspace, mappedFolders));
            }

            return results;
        }
    }
}