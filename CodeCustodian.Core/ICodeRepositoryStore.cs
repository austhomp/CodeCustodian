namespace CodeCustodian.Core
{
    using System.Collections.Generic;

    public interface ICodeRepositoryStore
    {
        IList<CodeRepositoryItem> RetrieveConfiguredCodeRepositories();

        void Add(CodeRepositoryItem codeRepositoryItem);

        void Remove(CodeRepositoryItem codeRepositoryItem);
    }
}