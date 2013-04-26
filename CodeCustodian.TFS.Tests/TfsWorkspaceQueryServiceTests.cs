namespace CodeCustodian.TFS.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TfsWorkspaceQueryServiceTests
    {
        private Mock<ITfsCommandFactory> commandFactory;

        [TestInitialize]
        public void TestInitialize()
        {
            var tfsCommand = new Mock<ITfsCommand>();
            tfsCommand.Setup(x => x.Run()).Returns(new TfsCommandResult(1, string.Empty));
            this.commandFactory = new Mock<ITfsCommandFactory>(); 
            this.commandFactory.Setup(x => x.Create(It.IsAny<TfsCommandType>(), It.IsAny<string>())).Returns(tfsCommand.Object);
        }

        [TestMethod]
        public void RetrieveAll_Should_ReturnTheWorkspace_When_ThereIsOneWorkspace()
        {
            const string Workspace1 = "workspace1";
            var tfsCommandOutputParser = new Mock<ITfsCommandOutputParser>();
            tfsCommandOutputParser.Setup(x => x.ParseWorkspacesOutput(It.IsAny<string>())).Returns(new string[] { Workspace1 });
            tfsCommandOutputParser.Setup(x => x.ParseMappedFoldersOutput(It.IsAny<string>())).Returns(new string[0]);
            var queryService = new TfsWorkspaceQueryService(this.commandFactory.Object, tfsCommandOutputParser.Object);

            var workspaces = queryService.RetrieveAll().ToList();

            Assert.IsTrue(workspaces.Count == 1);
            Assert.IsTrue(workspaces[0].WorkSpaceName == Workspace1);
        }

        [TestMethod]
        public void RetrieveAll_Should_ReturnAllWorkspaces_When_ThereAreMultipleWorkspaces()
        {
            const string Workspace1 = "workspace1";
            const string Workspace2 = "workspace 2";
            var tfsCommandOutputParser = new Mock<ITfsCommandOutputParser>();
            tfsCommandOutputParser.Setup(x => x.ParseWorkspacesOutput(It.IsAny<string>())).Returns(new string[] { Workspace1, Workspace2 });
            tfsCommandOutputParser.Setup(x => x.ParseMappedFoldersOutput(It.IsAny<string>())).Returns(new string[0]);
            var queryService = new TfsWorkspaceQueryService(this.commandFactory.Object, tfsCommandOutputParser.Object);

            var workspaces = queryService.RetrieveAll().ToList();

            Assert.IsTrue(workspaces.Count == 2);
            Assert.IsTrue(workspaces[0].WorkSpaceName == Workspace1);
            Assert.IsTrue(workspaces[1].WorkSpaceName == Workspace2);
        }

        [TestMethod]
        public void RetrieveAll_Should_ReturnNoMappedFoldersForTheWorkspace_When_ThereAreNoMappedFoldersForAWorkspace()
        {
            const string Workspace1 = "workspace1";
            var tfsCommandOutputParser = new Mock<ITfsCommandOutputParser>();
            tfsCommandOutputParser.Setup(x => x.ParseWorkspacesOutput(It.IsAny<string>())).Returns(new string[] { Workspace1 });
            tfsCommandOutputParser.Setup(x => x.ParseMappedFoldersOutput(It.IsAny<string>())).Returns(new string[0]);
            var queryService = new TfsWorkspaceQueryService(this.commandFactory.Object, tfsCommandOutputParser.Object);

            var workspaces = queryService.RetrieveAll().ToList();

            Assert.IsFalse(workspaces[0].MappedPaths.Any());
        }

        [TestMethod]
        public void RetrieveAll_Should_ReturnOneMappedFolderForTheWorkspace_When_ThereIsOneMappedFolderForAWorkspace()
        {
            const string Workspace1 = "workspace1";
            const string MappedFolder1 = "mapped folder 1";
            var tfsCommandOutputParser = new Mock<ITfsCommandOutputParser>();
            tfsCommandOutputParser.Setup(x => x.ParseWorkspacesOutput(It.IsAny<string>())).Returns(new string[] { Workspace1 });
            tfsCommandOutputParser.Setup(x => x.ParseMappedFoldersOutput(It.IsAny<string>())).Returns(new string[] { MappedFolder1 });
            var queryService = new TfsWorkspaceQueryService(this.commandFactory.Object, tfsCommandOutputParser.Object);

            var workspaces = queryService.RetrieveAll().ToList();

            Assert.IsTrue(workspaces[0].MappedPaths.Single() == MappedFolder1);
        }
    }
}
