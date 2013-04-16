namespace CodeCustodian.TFS
{
    using System.Collections.Generic;

    public class TfsWorkspaceQueryService : ITfsWorkspaceQueryService
    {
        private readonly ITfsCommandFactory commandFactory;

        public TfsWorkspaceQueryService(ITfsCommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public IEnumerable<TfsWorkspaceResult> RetrieveAll()
        {
            var listWorkspacesCommand = this.commandFactory.Create(TfsCommandType.ListWorkspaces, string.Empty);
            var listWorkspacesResult = listWorkspacesCommand.Run();
            System.Diagnostics.Debug.WriteLine("output" + listWorkspacesResult.Output);

            var workspaces = this.ParseWorkspaceOutput(listWorkspacesResult.Output);
            var results = new List<TfsWorkspaceResult>();

            foreach (var workspace in workspaces)
            {
                var mappedFoldersCommand = this.commandFactory.Create(TfsCommandType.ListWorkingFoldersForWorkspace, workspace);
                var mappedFoldersResult = mappedFoldersCommand.Run();
                var mappedFolders = this.ParseMappedFoldersOutput(mappedFoldersResult.Output);
                results.Add(new TfsWorkspaceResult(workspace, mappedFolders));
            }

            return results;
        }

        private IEnumerable<string> ParseMappedFoldersOutput(string output)
        {
            var mappedFolders = new List<string>();

            if (!output.StartsWith("todo: find the no mapped folders text"))
            {
                // todo: parse mapped folders
            }

            return mappedFolders;
        }

        internal IEnumerable<string> ParseWorkspaceOutput(string output)
        {
            var workspaces = new List<string>();
            
            if (!output.StartsWith("No workspace matching"))
            {
                // todo: parse workspaces
            }

            return workspaces;
        }
    }
}