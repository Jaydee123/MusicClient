using Moq;
using Music.ConsoleApp.Interfaces;
using Music.ConsoleApp.Entities;
using Music.ConsoleApp.Services;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.ConsoleApp.UnitTests.Services
{
    public class ArtistServiceTests
    {
        private Mock<IHttpClientService<Artist>> _mockHttpClient;
        private ArtistService _artistService;

        [SetUp]
        public void Setup()
        {
            _mockHttpClient = new Mock<IHttpClientService<Artist>>();
            _artistService = new ArtistService(_mockHttpClient.Object);
        }

        [Test]
        public async Task WhenThereIsAnExistingArtist_ThenReturnArtist()
        {
            // arrange
            var artistName = "Queen";
            var songName = "I want to break free";
            _mockHttpClient.Setup(_ => _.Get(It.IsAny<string>())).Returns(Task.FromResult(new Artist { Recordings = new List<Recording> { new Recording { Title = "I want to break free" } } }));

            // act
            var artist = await _artistService.GetArtist(artistName);

            // assert
            artist.Name.ShouldBe(artistName);
            artist.Recordings.FirstOrDefault().Title.ShouldBe(songName);
        }

        [Test]
        public async Task WhenThereIsntAnExistingArtist_ThenReturnNull()
        {
            // arrange
            var artistName = "Queen";
            _mockHttpClient.Setup(_ => _.Get(It.IsAny<string>())).Returns(Task.FromResult((Artist)null));
            
            // act
            var artist = await _artistService.GetArtist(artistName);

            // assert
            artist.ShouldBe(null);
        }
    }
}
