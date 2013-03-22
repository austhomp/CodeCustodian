namespace CodeCustodian.TFS
{
    using System;
    using System.Linq;

    using CodeCustodian.Core;

    public class CodeRepositoryUpdateService : ICodeRepositoryUpdateService
    {
        private readonly ITfsCommandFactory commandFactory;
        private readonly ITfsWorkspaceQueryService workspaceQueryService;

        public CodeRepositoryUpdateService(ITfsCommandFactory commandFactory, ITfsWorkspaceQueryService workspaceQueryService)
        {
            this.commandFactory = commandFactory;
            this.workspaceQueryService = workspaceQueryService;
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
                case TFSHandledType.Folder:
                    throw new NotImplementedException(string.Format("Repository type {0} has not been implemented for GetLatest yet", repositoryType.Value));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void GetLatestForAllWorkspaces(CodeRepositoryItem codeRepositoryItem)
        {
            var workspaceResults = this.workspaceQueryService.RetrieveAll();
            foreach (var workspaceResult in workspaceResults)
            {
                var getLatestCommand = this.commandFactory.Create(TfsCommandType.GetLatest, workspaceResult.MappedPaths.First());
                var getLatestResult = getLatestCommand.Run();
            }
        }
    }
}