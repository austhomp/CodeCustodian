namespace CodeCustodian.TFS
{
    using System;

    public class TfsCommandFactory : ITfsCommandFactory
    {
        public ITfsCommand Create(TfsCommandType commandType, string parameter)
        {
            switch (commandType)
            {
                case TfsCommandType.ListWorkspaces:
                    return new TfsCommand("workspaces");
                case TfsCommandType.ListWorkingFoldersForWorkspace:
                    return new TfsCommand(string.Format("workfold /workspace:{0}", parameter));
                case TfsCommandType.GetLatest:
                    return new TfsCommand("get", parameter);
                case TfsCommandType.QueryLatest:
                    return new TfsCommand("get /preview /noprompt", parameter);
                default:
                    throw new ArgumentOutOfRangeException("commandType");
            }
        }
    }
}