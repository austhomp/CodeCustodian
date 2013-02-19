namespace CodeCustodian.Core
{
    using System;
    using System.Collections.Generic;

    using System.Linq;

    public class CodeRepositoryMonitor : ICodeRepositoryMonitor
    {
        private readonly IEnumerable<ICodeRepositoryQueryService> queryServices;

        private readonly IEnumerable<ICodeRepositoryUpdateService> updateServices;

        public CodeRepositoryMonitor(IEnumerable<ICodeRepositoryQueryService> queryServices, IEnumerable<ICodeRepositoryUpdateService> updateServices)
        {
            if (queryServices == null)
            {
                throw new ArgumentNullException("queryServices");
            }

            if (updateServices == null)
            {
                throw new ArgumentNullException("updateServices");
            }

            this.queryServices = queryServices;
            this.updateServices = updateServices;
        }

        public void Refresh(IList<CodeRepositoryItem> codeRepositoryItems)
        {
            foreach (var item in codeRepositoryItems)
            {
                var queryService = this.queryServices.FirstOrDefault(x => x.CanHandle(item));
                var newStatus = "Not Supported";
                if (queryService != null)
                {
                    newStatus = queryService.QueryStatus(item);
                }

                item.Status = newStatus;
            }
        }
    }
}