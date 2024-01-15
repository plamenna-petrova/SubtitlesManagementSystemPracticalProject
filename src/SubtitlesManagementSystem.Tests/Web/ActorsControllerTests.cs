using Data.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SubtitlesManagementSystem.Business.Services.Actors;
using SubtitlesManagementSystem.Business.Transactions.Interfaces;
using SubtitlesManagementSystem.Common.Helpers;
using SubtitlesManagementSystem.Web.Controllers;
using SubtitlesManagementSystem.Web.Models.Actors.ViewModels;
using System.Security.Claims;
using System.Security.Principal;

namespace SubtitlesManagementSystem.Tests.Web
{
    public class ActorsControllerTests
    {
        private readonly Mock<IActorService> _actorServiceMock;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly ActorsController _actorsController;

        public ActorsControllerTests()
        {
            _actorServiceMock = new Mock<IActorService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var genericIdentity = new GenericIdentity("admin");

            var claimsPrincipal = new ClaimsPrincipal(genericIdentity);

            var defaultHttpContext = new DefaultHttpContext
            {
                User = claimsPrincipal,
            };

            var controllerContext = new ControllerContext
            {
                HttpContext = defaultHttpContext
            };

            _actorsController = new ActorsController(_actorServiceMock.Object, _unitOfWorkMock.Object)
            {
                ControllerContext = controllerContext
            };
        }

        [Fact]
        public void Test_ActorsController_ShouldHaveValidType()
        {
            Assert.IsType<ActorsController>(_actorsController);
        }

        [Fact]  
        public void Test_ActorsControllerIndexAction_ShouldReturnValidSortingFilteringAndPaginationData()
        {
            // Arrange
            int expectedPagesCount = 1;
            int expectedCurrentPage = 1;
            int expectedActorsCount = 1;

            var testActors = new List<AllActorsViewModel>
            {
                new AllActorsViewModel
                {
                    FirstName = "Christian",
                    LastName = "Bale"
                },
                new AllActorsViewModel
                {
                    FirstName = "Elliot",
                    LastName = "Page"
                },
                new AllActorsViewModel
                {
                    FirstName = "Joseph",
                    LastName = "Gordon-Levitt"
                }
            };

            // Act
            _actorServiceMock.Setup(asm => asm.GetAllActors()).Returns(testActors);

            var sortedFilteredAndPaginatedActorsActionResult = _actorsController.Index("actor_first_name_descending", "Jo", "Jo", 3, null);
            var sortedFilteredAndPaginatedActionViewResult = Assert.IsType<ViewResult>(sortedFilteredAndPaginatedActorsActionResult);
            var allActorsViewModelPaginatedList = Assert.IsAssignableFrom<PaginatedList<AllActorsViewModel>>(
                sortedFilteredAndPaginatedActionViewResult.ViewData.Model
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(expected: "Joseph", actual: allActorsViewModelPaginatedList[0].FirstName);
                Assert.Equal(expected: "Gordon-Levitt", actual: allActorsViewModelPaginatedList[0].LastName);
                Assert.Equal(expectedActorsCount, allActorsViewModelPaginatedList.Count);
                Assert.Equal(expectedCurrentPage, allActorsViewModelPaginatedList.PageIndex);
                Assert.Equal(expectedPagesCount, allActorsViewModelPaginatedList.TotalPages);
            });
        }
    }
}
