namespace CodeCustodian.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CodeRepositoryStore : ICodeRepositoryStore
    {
        private readonly IConfiguration configuration;


        public CodeRepositoryStore(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<CodeRepositoryItem> RetrieveConfiguredCodeRepositories()
        {
            return this.configuration.CodeRepositoryItems.ToList();
        }
    }
}