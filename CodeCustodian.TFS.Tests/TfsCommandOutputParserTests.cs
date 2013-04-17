using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCustodian.TFS.Tests
{
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
        public void ParseWorkspaceOutput_Should_NoWorkspaces_When_TheValidInputHasNoWorkspaces()
        {
            var output = this.CreateListWorkspacesOutput();

            var workspaces = this.parser.ParseWorkspacesOutput(output).ToList();

            Assert.IsTrue(!workspaces.Any());
        }

        [TestMethod]
        public void ParseWorkspaceOutput_Should_NoWorkspaces_When_TheInputIsNotValid()
        {
            var output = "workspace1";

            var workspaces = this.parser.ParseWorkspacesOutput(output).ToList();

            Assert.IsTrue(!workspaces.Any());
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

    }
}
