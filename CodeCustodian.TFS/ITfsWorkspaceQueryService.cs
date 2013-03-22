namespace CodeCustodian.TFS
{
    using System.Collections;
    using System.Collections.Generic;

    public interface ITfsWorkspaceQueryService
    {
        IEnumerable<TfsWorkspaceResult> RetrieveAll();
    }
}