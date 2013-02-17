namespace CodeCustodian.Core
{
    using System;
    using System.Collections.Generic;

    public class CodeRepositoryStore : ICodeRepositoryStore
    {
        private IList<CodeRepositoryItem> list;

        public IList<CodeRepositoryItem> RetrieveConfiguredCodeRepositories()
        {
            if (list == null)
            {
                this.list = new List<CodeRepositoryItem>();
                this.list.Add(new CodeRepositoryItem("Test", "N/A", "TEST"));
            }

            return list;
        }

        public void Add(CodeRepositoryItem codeRepositoryItem)
        {
            if (codeRepositoryItem == null)
            {
                throw new ArgumentNullException("codeRepositoryItem");
            }

            list.Add(codeRepositoryItem);
        }

        public void Remove(CodeRepositoryItem codeRepositoryItem)
        {
            if (codeRepositoryItem == null)
            {
                throw new ArgumentNullException("codeRepositoryItem");
            }

            if (this.list.Contains(codeRepositoryItem))
            {
                list.Remove(codeRepositoryItem);
            }
        }
    }
}