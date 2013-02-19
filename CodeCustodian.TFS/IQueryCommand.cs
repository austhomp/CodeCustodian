namespace CodeCustodian.TFS
{
    using CodeCustodian.Core;

    public interface IQueryCommand
    {
        QueryCommandResponse Execute(CodeRepositoryItem codeRepositoryItem);
    }
}