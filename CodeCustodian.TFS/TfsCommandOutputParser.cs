namespace CodeCustodian.TFS
{
    using System.Collections.Generic;
    using System.Linq;

    public class TfsCommandOutputParser : ITfsCommandOutputParser
    {
        public IEnumerable<string> ParseMappedFoldersOutput(string output)
        {
            var mappedFolders = new List<string>();

            if (!output.StartsWith("todo: find the no mapped folders text"))
            {
                // todo: parse mapped folders
            }

            return mappedFolders;
        }

        public IEnumerable<string> ParseWorkspacesOutput(string output)
        {
            var workspaces = new List<string>();

            var lines = output.SplitLines();
            if (lines.Count() > 3)
            {

            }

            return workspaces;
        }
    }
}