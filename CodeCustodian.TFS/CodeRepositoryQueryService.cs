namespace CodeCustodian.TFS
{
    using System;

    using CodeCustodian.Core;

    public class CodeRepositoryQueryService : ICodeRepositoryQueryService
    {
        public CodeRepositoryQueryService()
        {
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

        private string QueryAllWorkspaces()
        {
            return "todo";
        }

        private string QueryWorkspace(CodeRepositoryItem codeRepositoryItem)
        {
            throw new NotImplementedException();
        }

        private string QueryFolder(CodeRepositoryItem codeRepositoryItem)
        {
            throw new NotImplementedException();
        }

        public bool CanHandle(CodeRepositoryItem codeRepositoryItem)
        {
            return codeRepositoryItem.CanBeHandledBy<TFSHandledType>("TFS");
        }
    }
}