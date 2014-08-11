namespace CodeCustodian.TFS
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CodeCustodian.Core;

    public class CodeRepositoryQueryService : ICodeRepositoryQueryService
    {
        private readonly ITfsCommandFactory commandFactory;
        private readonly ITfsWorkspaceQueryService workspaceQueryService;

        public CodeRepositoryQueryService(ITfsCommandFactory commandFactory, ITfsWorkspaceQueryService workspaceQueryService)
        {
            this.commandFactory = commandFactory;
            this.workspaceQueryService = workspaceQueryService;
        }

        public string QueryStatus(CodeRepositoryItem codeRepositoryItem)
        {
            TFSHandledType? typeToHandle = codeRepositoryItem.ParseAs<TFSHandledType>("TFS");
            if (!typeToHandle.HasValue)
            {
                return "Not Supported";
            }

            string result = "Not Implemented";
            switch (typeToHandle.Value)
            {
                case TFSHandledType.AllWorkspaces:
                    result = this.QueryAllWorkspaces();
                    break;
                case TFSHandledType.Workspace:
                    result = this.QueryWorkspace(codeRepositoryItem);
                    break;
                case TFSHandledType.Folder:
                    result = this.QueryFolder(codeRepositoryItem);
                    break;
            }

            return result;
        }

        public bool CanHandle(CodeRepositoryItem codeRepositoryItem)
        {
            return codeRepositoryItem.CanBeHandledBy<TFSHandledType>("TFS");
        }

        private string QueryAllWorkspaces()
        {
            var output = new StringBuilder();
            var workspaceResults = this.workspaceQueryService.RetrieveAll();
            foreach (var workspaceResult in workspaceResults)
            {
                if (workspaceResult.MappedPaths.Any())
                {
                    var queryLatestCommand = this.commandFactory.Create(TfsCommandType.QueryLatest, workspaceResult.MappedPaths.First());
                    var queryLatestResult = queryLatestCommand.Run();
                    output.AppendLine(queryLatestResult.Output);
                }
            }

            return output.ToString();
        }

        private string QueryWorkspace(CodeRepositoryItem codeRepositoryItem)
        {
            throw new NotImplementedException();
        }

        private string QueryFolder(CodeRepositoryItem codeRepositoryItem)
        {
            throw new NotImplementedException();
        }
    }
}