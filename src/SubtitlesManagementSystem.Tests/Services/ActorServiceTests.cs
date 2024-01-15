using Data.DataAccess.Repositories.Interfaces;
using Data.DataModels.Entities;
using Microsoft.AspNetCore.Http;
using Moq;
using SubtitlesManagementSystem.Business.Services.Actors;
using SubtitlesManagementSystem.Web.Models.Actors.BindingModels;
using System.Security.Claims;
using System.Security.Principal;

namespace SubtitlesManagementSystem.Tests.Services
{
    public class ActorServiceTests
    {
        private readonly Mock<IActorRepository> _actorRepositoryMock;

        private readonly Mock<IFilmProductionRepository> _filmProductionRepositoryMock;

        private readonly Mock<IFilmProductionActorRepository> _filmProductionActorRepositoryMock;

        private readonly IActorService _actorService;

        public ActorServiceTests()
        {
            _actorRepositoryMock = new Mock<IActorRepository>();
            _filmProductionRepositoryMock = new Mock<IFilmProductionRepository>();
            _filmProductionActorRepositoryMock = new Mock<IFilmProductionActorRepository>();

            _actorService = new ActorService(
                _actorRepositoryMock.Object,
                _filmProductionRepositoryMock.Object,
                _filmProductionActorRepositoryMock.Object
            );
        }

        [Fact]
        public void Test_AddActorEntity_IsVerifiable()
        {
            _actorRepositoryMock.Setup(arm => arm.Add(It.IsAny<Actor>())).Verifiable();
        }

        [Fact]
        public void Test_AddFilmProductionEntity_IsVerifiable()
        {
            _filmProductionRepositoryMock.Setup(fprm => fprm.Add(It.IsAny<FilmProduction>())).Verifiable();
        }

        [Fact]
        public void Test_AddFilmProductionActorEntity_IsVerifiable()
        {
            _filmProductionActorRepositoryMock.Setup(fparp => fparp.Add(It.IsAny<FilmProductionActor>())).Verifiable();
        }

        [Fact]
        public void Test_ActorRepositoryGetById_ShouldReturnActor()
        {
            _actorRepositoryMock.Setup(arm => arm.GetById(It.IsAny<string>())).Returns(new Actor());
        }

        [Theory]
        [InlineData("Christian", "Bale", new string[] { "1aab45d", "45d27d5", "45d27d5" })]
        [InlineData("Elliot", "Page", new string[] { "45d27d5", "45d27d5" })]
        [InlineData("Joseph", "Gordon-Levitt", new string[] { "1aab45d" })]
        [InlineData("Anne", "Hathaway", new string[] { "587bcfe", "1aab45d" })]
        [InlineData("Franka", "Potente", null)]
        [InlineData("Matthew", "McConaughey", null)]
        [InlineData("Jessica", "Chastain", null)]
        [InlineData("Matt", "Damon", null)]
        [InlineData("Leonardo", "Di Caprio", null)]
        [InlineData("Cillian", "Murphy", null)]
        [InlineData("Natalie", "Portman", null)]
        [InlineData("Hugo", "Weaving", null)]
        public void Test_CreateActor_ShouldReturnTrue(string firstName, string lastName, string[] selectedFilmProductions)
        {
            // Arrange
            var testActorToCreate = new CreateActorBindingModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            var genericIdentity = new GenericIdentity("admin");

            var claimsPrincipal = new ClaimsPrincipal(genericIdentity);

            var defaultHttpContext = new DefaultHttpContext
            {
                User = claimsPrincipal,
            };

            // Act
            bool actorCreationActualResult = _actorService.CreateActor(testActorToCreate, selectedFilmProductions, defaultHttpContext.User.Identity.Name);

            // Assert
            Assert.True(actorCreationActualResult);
        }
    }
}
