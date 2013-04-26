namespace CodeCustodian.TFS
{
    using System;
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
            int lineCount = lines.Count();
            if (lineCount > 3)
            {
                var dashedLineIndex = Array.FindIndex(lines, 0, x => x.StartsWith("-"));
                if (dashedLineIndex >= 0)
                {
                    var dashedLine = lines[dashedLineIndex];
                    var firstSpace = dashedLine.IndexOf(' ');
                    var lengthOfFirstSetOfDashes = firstSpace;
                    for (int i = dashedLineIndex + 1; i < lineCount; i++)
                    {
                        var line = lines[i];
                        if (line.Length > lengthOfFirstSetOfDashes)
                        {
                            var workspace = line.Substring(0, lengthOfFirstSetOfDashes).Trim();
                            workspaces.Add(workspace);
                        }
                    }
                }
            }

            return workspaces;
        }
    }
}