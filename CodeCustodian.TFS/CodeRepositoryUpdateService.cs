namespace CodeCustodian.TFS
{
    using System;

    using CodeCustodian.Core;

    public class CodeRepositoryUpdateService : ICodeRepositoryUpdateService
    {
        private readonly ITfsCommandFactory commandFactory;

        public CodeRepositoryUpdateService(ITfsCommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        public void GetLatest(CodeRepositoryItem codeRepositoryItem)
        {
            var repositoryType = codeRepositoryItem.ParseAs<TFSHandledType>("TFS");
            if (!repositoryType.HasValue)
            {
                throw new NotSupportedException(string.Format("Repository type {0} not supported by GetLatest", codeRepositoryItem.Type));
            }

            switch (repositoryType.Value)
            {
                case TFSHandledType.AllWorkspaces:
                    this.GetLatestForAllWorkspaces(codeRepositoryItem);
                    break;
                case TFSHandledType.Workspace:
                    throw new NotImplementedException(string.Format("Repository type {0} has not been implemented for GetLatest yet", repositoryType.Value));
                    break;
                case TFSHandledType.Folder:
                    throw new NotImplementedException(string.Format("Repository type {0} has not been implemented for GetLatest yet", repositoryType.Value));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GetLatestForAllWorkspaces(CodeRepositoryItem codeRepositoryItem)
        {

            var listWorkspacesCommand = this.commandFactory.Create(TfsCommandType.ListWorkspaces);
            var listWorkspacesResult = listWorkspacesCommand.Run();

            // parse workspaces from output

            // foreach workspace
            var mappedFoldersResult = this.commandFactory.Create(TfsCommandType.ListWorkingFoldersForWorkspace);
            // get a working folder for each workspace

            // foreach working folder
            var getLatestCommand = this.commandFactory.Create(TfsCommandType.GetLatest);
            var getLatestResult = getLatestCommand.Run();

        }
    }
}