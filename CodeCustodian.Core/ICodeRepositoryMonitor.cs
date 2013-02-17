namespace CodeCustodian.Core
{
    using System.Collections.Generic;

    public interface ICodeRepositoryMonitor
    {
        void Refresh(IList<CodeRepositoryItem> codeRepositoryItems);
    }
}