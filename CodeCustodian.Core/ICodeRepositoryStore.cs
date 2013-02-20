namespace CodeCustodian.Core
{
    using System.Collections.Generic;

    public interface ICodeRepositoryStore
    {
        IEnumerable<CodeRepositoryItem> RetrieveConfiguredCodeRepositories();
    }
}