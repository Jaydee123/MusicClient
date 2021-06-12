using Moq;
using Music.ConsoleApp.Interfaces;
using Music.ConsoleApp.Entities;
using Music.ConsoleApp.Services;
using NUnit.Framework;
using Shouldly;
using System.Threading.Tasks;

namespace Music.ConsoleApp.UnitTests.Services
{
    public class SongServiceTests
    {
        private Mock<IHttpClientService<Song>> _mockHttpClient;
        private SongService _songService;

        [SetUp]
        public void Setup()
        {
            _mockHttpClient = new Mock<IHttpClientService<Song>>();
            _songService = new SongService(_mockHttpClient.Object);
        }

        [Test]
        public async Task WhenThereIsAnExistingSong_ThenReturnSong()
        {
            // arrange
            var lyrics = "I want to break free, I want to break free, I want to break free from your lies";
            var artist = "Queen";
            var songName = "I want to break free";
            _mockHttpClient.Setup(_ => _.Get(It.IsAny<string>())).Returns(Task.FromResult(new Song { Lyrics = lyrics }));

            // act
            var song = await _songService.GetSong(artist, songName);

            // assert
            song.Name.ShouldBe(songName);
            song.ArtistName.ShouldBe(artist);
            song.Lyrics.ShouldBe(lyrics);
        }

        [Test]
        public async Task WhenThereIsntAnExistingSong_ThenReturnNull()
        {
            // arrange
            var artist = "Queen";
            var songName = "I want to break free";
            _mockHttpClient.Setup(_ => _.Get(It.IsAny<string>())).Returns(Task.FromResult((Song)null));

            // act
            var song = await _songService.GetSong(artist, songName);

            // assert
            song.ShouldBe(null);
        }
    }
}
