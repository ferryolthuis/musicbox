using Moq;
using MusicBox.Business.Services;
using MusicBox.Model;
using MusicBox.Persistence.Interfaces;
using NUnit.Framework;
using System.Threading.Tasks;

namespace MusicBox.UnitTests.Business.Services
{
    [TestFixture]
    public class ArtistServiceTest
    {
        private Mock<IArtistRepository> _artistRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private ArtistService _artistService;

        [SetUp]
        public void Setup()
        {
            _artistRepository = new Mock<IArtistRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _artistService = new ArtistService(_artistRepository.Object, _unitOfWork.Object);
        }

        [Test]
        public void Get_ItemPresent_ReturnsItem()
        {
            //Arrange
            var searchString = "SomeName";
            var artist = new Artist { Name = searchString };
            _artistRepository.Setup(x => x.FindByNameAsync(searchString)).Returns(Task.FromResult(artist));

            //Act
            var result =  _artistService.Get(searchString).Result;

            //Assert
            Assert.That(result.Item, Is.EqualTo(artist));
        }

        [Test]
        public void Get_ItemNotPresent_ReturnsErrorMessage()
        {
            //Arrange
            var searchString = "SomeSearchString";
            var artist = new Artist { Name = searchString };
            _artistRepository.Setup(x => x.FindByNameAsync(searchString)).Returns(Task.FromResult<Artist>(null));

            //Act
            var result = _artistService.Get(searchString).Result;

            //Assert
            Assert.That(result.Message, Is.EqualTo($"An artist with name {searchString} could not be found."));
        }
    }
}