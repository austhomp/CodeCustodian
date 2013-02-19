namespace CodeCustodian.Core
{
    public interface ICodeRepositoryQueryService
    {
        string QueryStatus(CodeRepositoryItem codeRepositoryItem);

        bool CanHandle(CodeRepositoryItem codeRepositoryItem);
    }
}