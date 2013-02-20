namespace CodeCustodian.Core
{
    using System.Collections.Generic;

    public interface IConfiguration
    {
        IList<CodeRepositoryItem> CodeRepositoryItems { get; } 
    }
}