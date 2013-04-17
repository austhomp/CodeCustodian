namespace CodeCustodian.TFS
{
    using System.Collections.Generic;

    public interface ITfsCommandOutputParser
    {
        IEnumerable<string> ParseMappedFoldersOutput(string output);

        IEnumerable<string> ParseWorkspacesOutput(string output);
    }
}