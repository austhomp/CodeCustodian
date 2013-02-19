namespace CodeCustodian.TFS
{
    public class QueryCommandFactory : IQueryCommandFactory
    {
        public IQueryCommand CreateFor(TFSHandledType tfsHandledType)
        {
            IQueryCommand command = null;
            switch (tfsHandledType)
            {
                case TFSHandledType.AllWorkspaces:
                    break;
                case TFSHandledType.Workspace:
                    break;
                case TFSHandledType.Folder:
                    break;
            }
            
            return command;
        }
    }
}