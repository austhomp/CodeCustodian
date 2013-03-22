namespace CodeCustodian.TFS
{
    using System;

    public class TfsCommandFactory : ITfsCommandFactory
    {
        private readonly ITfsCommandPathLocator commandPathLocator;

        private readonly string tfExePath;

        private readonly string tfptExePath;

        public TfsCommandFactory(ITfsCommandPathLocator commandPathLocator)
        {
            this.commandPathLocator = commandPathLocator;

            this.tfExePath = commandPathLocator.GetTfExeLocation();
            this.tfptExePath = commandPathLocator.GetTfptExeLocation();
        }

        public ITfsCommand Create(TfsCommandType commandType, string parameter)
        {
            switch (commandType)
            {
                case TfsCommandType.ListWorkspaces:
                    return new TfsCommand(this.tfExePath, "workspaces");
                case TfsCommandType.ListWorkingFoldersForWorkspace:
                    return new TfsCommand(this.tfExePath, string.Format("workfold /workspace:{0}", parameter));
                case TfsCommandType.GetLatest:
                    return new TfsCommand(this.tfExePath, "get", parameter);
                case TfsCommandType.QueryLatest:
                    return new TfsCommand(this.tfExePath, "get /preview /noprompt", parameter);
                default:
                    throw new ArgumentOutOfRangeException("commandType");
            }
        }
    }
}