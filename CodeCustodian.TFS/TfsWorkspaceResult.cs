namespace CodeCustodian.TFS
{
    using System.Collections.Generic;
    using System.Linq;

    public class TfsWorkspaceResult
    {
        private readonly IList<string> mappedPaths;

        private readonly string workSpaceName;

        public TfsWorkspaceResult(string workSpaceName, IEnumerable<string> mappedPaths)
        {
            this.workSpaceName = workSpaceName;
            this.mappedPaths = mappedPaths.ToList();
        }

        public string WorkSpaceName
        {
            get
            {
                return this.workSpaceName;
            }
        }

        public IEnumerable<string> MappedPaths
        {
            get
            {
                return this.mappedPaths;
            }
        }
    }
}