namespace CodeCustodian.TFS
{
    public interface ITfsCommandPathLocator
    {
        string GetTfExeLocation();

        string GetTfptExeLocation();
    }
}