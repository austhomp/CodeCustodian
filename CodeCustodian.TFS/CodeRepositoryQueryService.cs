namespace CodeCustodian.TFS
{
    using CodeCustodian.Core;

    public class CodeRepositoryQueryService : ICodeRepositoryQueryService
    {
        public string QueryStatus(CodeRepositoryItem codeRepositoryItem)
        {
            return "Not Implemented";
        }

        public bool HandlesType(string type)
        {
            return type == "TFS";
        }
    }
}