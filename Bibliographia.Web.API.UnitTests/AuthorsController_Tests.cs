using AutoMapper;
using Bibliographia.Web.API.Configurations;
using Bibliographia.Web.API.Controllers;
using Bibliographia.Web.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blazor.SankoreAPI.Tests
{
    public class AuthorsController_Tests
    {
        private static IMapper? _mapper;

        public AuthorsController_Tests()
        {
            if (_mapper == null)
            {
                MapperConfiguration? mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MapperConfig());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task GetAuthors_Test()
        {
            Mock<ILogger<AuthorsController>>? _mockLogger = new Mock<ILogger<AuthorsController>>();
            //Arrange
            // todo: define the required assets
            DbContextOptions<BiblioContext>? _options = new DbContextOptionsBuilder<BiblioContext>().UseInMemoryDatabase(databaseName: "Bibliographia").Options;
            using BiblioContext? context = new BiblioContext(_options);
            _ = context.Add(new Author()
            {
                Id = 1,
                FirstName = "Brenda",
                LastName = "Williams"
            });
            _ = context.Add(new Author()
            {
                Id = 2,
                FirstName = "Johnny",
                LastName = "Joe"
            });
            _ = context.SaveChanges();
            List<Author>? authors = await context.Authors.ToListAsync();
            //var authorsController = new AuthorsController(context, new NullLogger<AuthorsController>(), new IMapper<AuthorsController>());
            AuthorsController? authorsController = new AuthorsController(context, _mockLogger.Object, _mapper);
            // Act
            // todo: invoke the test
            Microsoft.AspNetCore.Mvc.ActionResult<IEnumerable<Bibliographia.Web.API.Models.DataTransfer.Author.AuthorReadOnlyDto>>? authorsList = await authorsController.GetAuthors();

            // Assert
            // todo: verify that conditions are met
            Assert.NotNull(authorsList);

        }
    }
}
