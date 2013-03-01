namespace CodeCustodian.TFS
{
    public interface ITfsCommandFactory
    {
        ITfsCommand Create(TfsCommandType commandType, string parameter);
    }
}