namespace CodeCustodian.Core
{
    public interface ICodeRepositoryUpdateService
    {
        void GetLatest(CodeRepositoryItem codeRepositoryItem);
    }
}