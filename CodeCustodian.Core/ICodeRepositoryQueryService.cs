﻿namespace CodeCustodian.Core
{
    public interface ICodeRepositoryQueryService
    {
        string QueryStatus(CodeRepositoryItem codeRepositoryItem);

        bool HandlesType(string type);
    }
}