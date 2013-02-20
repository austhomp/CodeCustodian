namespace CodeCustodian
{
    using System.Collections.Generic;

    using CodeCustodian.Core;

    public class AppConfiguration : IConfiguration
    {
        public AppConfiguration()
        {
            this.CodeRepositoryItems = new List<CodeRepositoryItem>();

            // todo implement storing and saving configuration and not use hardcoding
            this.CodeRepositoryItems.Add(new CodeRepositoryItem("All TFS Workspaces", "TFS.AllWorkspaces", string.Empty, "Unknown"));
        }

        public IList<CodeRepositoryItem> CodeRepositoryItems { get; private set; }
    }
}