namespace CodeCustodian.TFS.Tests
{
    using System;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using System.Linq;

    [TestClass]
    public class TfsWorkspaceQueryServiceTests
    {
        private Mock<ITfsCommandFactory> commandFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            this.commandFactory = new Mock<ITfsCommandFactory>();
        }

        [TestMethod]
        public void RetrieveAll_Should_ReturnTheWorkspace_When_TheValidInputHasOneWorkspace()
        {
            var listWorkspacesCommand = new Mock<ITfsCommand>();
            string listWorkspacesOutput = CreateListWorkspacesOutput("workspace1");
            listWorkspacesCommand.Setup(x => x.Run()).Returns(new TfsCommandResult(1, listWorkspacesOutput));
            this.commandFactory.Setup(x => x.Create(TfsCommandType.ListWorkspaces, It.IsAny<string>()))
                .Returns(listWorkspacesCommand.Object);
            var queryService = new TfsWorkspaceQueryService(this.commandFactory.Object, new TfsCommandOutputParser());

            var workspaces = queryService.RetrieveAll().ToList();

            Assert.IsTrue(workspaces.Count == 1);
            Assert.IsTrue(workspaces[0].WorkSpaceName == "workspace1");
            Assert.IsTrue(!workspaces[0].MappedPaths.Any());

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
