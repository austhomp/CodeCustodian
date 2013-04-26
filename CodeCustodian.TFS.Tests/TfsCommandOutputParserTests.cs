using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCustodian.TFS.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [TestClass]
    public class TfsCommandOutputParserTests
    {
        private TfsCommandOutputParser parser;

        [TestInitialize]
        public void TestInitialize()
        {
            this.parser = new TfsCommandOutputParser();
        }

        [TestMethod]
        public void ParseWorkspaceOutput_Should_ReturnTheWorkspace_When_TheValidInputHasOneWorkspace()
        {
            var output = this.CreateListWorkspacesOutput("workspace1");

            var workspaces = this.parser.ParseWorkspacesOutput(output).ToList();

            Assert.IsTrue(workspaces.Count == 1);
            Assert.IsTrue(workspaces.First() == "workspace1");
        }

        [TestMethod]
        public void ParseWorkspaceOutput_Should_ReturnAllWorkspaces_When_TheValidInputHasMultipleWorkspaces()
        {
            var output = this.CreateListWorkspacesOutput("workspace1", "workspace 2");

            var workspaces = this.parser.ParseWorkspacesOutput(output).ToList();

            Assert.IsTrue(workspaces.Count == 2);
            Assert.IsTrue(workspaces.First() == "workspace1");
            Assert.IsTrue(workspaces.Skip(1).First() == "workspace 2");
        }

        [TestMethod]
        public void ParseWorkspaceOutput_Should_ReturnNoWorkspaces_When_TheValidInputHasNoWorkspaces()
        {
            var output = this.CreateListWorkspacesOutput();

            var workspaces = this.parser.ParseWorkspacesOutput(output).ToList();

            Assert.IsFalse(workspaces.Any());
        }

        [TestMethod]
        public void ParseWorkspaceOutput_Should_ReturnNoWorkspaces_When_TheInputIsNotValid()
        {
            var output = "workspace1";

            var workspaces = this.parser.ParseWorkspacesOutput(output).ToList();

            Assert.IsFalse(workspaces.Any());
        }

        [TestMethod]
        public void ParseMappedFoldersOutput_Should_ReturnNoFolders_When_TheInputIsNotValid()
        {
            var output = @"C:\folder";

            var workspaces = this.parser.ParseMappedFoldersOutput(output).ToList();

            Assert.IsFalse(workspaces.Any());
        }

        [TestMethod]
        public void ParseMappedFoldersOutput_Should_ReturnOneFolder_When_TheValidInputHasOneFolder()
        {
            const string Folder1 = @"C:\Folder";
            var folders = new Dictionary<string, string>()
                              {
                                  { "Workspace1", Folder1 }
                              };
            var output = this.CreateMappedFoldersOutput(folders);

            var workspaces = this.parser.ParseMappedFoldersOutput(output).ToList();

            Assert.IsTrue(workspaces.Single() == Folder1);
        }

        [TestMethod]
        public void ParseMappedFoldersOutput_Should_ReturnAllFolders_When_TheValidInputHasMultipleFolders()
        {
            const string Folder1 = @"C:\Folder";
            const string Folder2 = @"D:\Folder Name\";
            var folders = new Dictionary<string, string>()
                              {
                                  { "Workspace1", Folder1 } ,
                                  { "Workspace1", Folder2}
                              };
            var output = this.CreateMappedFoldersOutput(folders);

            var workspaces = this.parser.ParseMappedFoldersOutput(output).ToList();

            Assert.IsTrue(workspaces[0] == Folder1);
            Assert.IsTrue(workspaces[1] == Folder2);
        }

        private string CreateListWorkspacesOutput(params string[] workspaces)
        {
            var b = new StringBuilder();
            b.AppendLine("Collection: http://tfs.company.com:8080/tfs/collection")
             .AppendLine("Workspace               Owner            Computer   Comment")
             .AppendLine("----------------------- ---------------- ---------- ---------------------------");
            foreach (var workspace in workspaces)
            {
                b.Append(workspace)
                 .AppendLine(@"               DOM\user         HOSTNAME");
            }

            return b.ToString();
        }

        private string CreateMappedFoldersOutput(Dictionary<string, string> entries)
        {
            var b = new StringBuilder();
            b.AppendLine(@"Workspace : MyWorkspace (DOM\user)")
             .AppendLine("Collection: http://tfs.company.com:8080/tfs/collection");
            foreach (var entry in entries)
            {
                b.AppendFormat("$/XX/{0}: {1}", entry.Key, entry.Value)
                 .AppendLine();
            }

            return b.ToString();
        }

    }
}
