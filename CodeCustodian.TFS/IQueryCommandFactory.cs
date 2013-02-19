namespace CodeCustodian.TFS
{
    public interface IQueryCommandFactory
    {
        IQueryCommand CreateFor(TFSHandledType tfsHandledType);
    }
}