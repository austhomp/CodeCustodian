namespace CodeCustodian.TFS
{
    using System;

    using CodeCustodian.Core;

    public class CodeRepositoryQueryService : ICodeRepositoryQueryService
    {
        private readonly IQueryCommandFactory queryCommandFactory;

        public CodeRepositoryQueryService(IQueryCommandFactory queryCommandFactory)
        {
            this.queryCommandFactory = queryCommandFactory;
        }

        public string QueryStatus(CodeRepositoryItem codeRepositoryItem)
        {
            TFSHandledType? typeToHandle = codeRepositoryItem.ParseAs<TFSHandledType>("TFS");
            if (!typeToHandle.HasValue)
            {
                return "Not Supported";
            }

            var queryCommand = this.queryCommandFactory.CreateFor(typeToHandle.Value);
            if (queryCommand == null)
            {
                return "Not Implemented";
            }

            var response = queryCommand.Execute(codeRepositoryItem);
            return response.Result;
        }

        public bool CanHandle(CodeRepositoryItem codeRepositoryItem)
        {
            return codeRepositoryItem.CanBeHandledBy<TFSHandledType>("TFS");
        }
    }
}