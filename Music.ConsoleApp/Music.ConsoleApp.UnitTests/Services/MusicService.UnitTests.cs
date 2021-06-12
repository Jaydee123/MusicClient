using Moq;
using Music.ConsoleApp.Interfaces;
using Music.ConsoleApp.Entities;
using Music.ConsoleApp.Services;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music.ConsoleApp.UnitTests.Repositories
{
    public class MusicServiceTests
    {
        private Mock<IArtistService> _mockArtistService;
        private Mock<ISongService> _mockSongService;
        private MusicService _musicService;

        [SetUp]
        public void Setup()
        {
            _mockArtistService = new Mock<IArtistService>();
            _mockSongService = new Mock<ISongService>();
            _musicService = new MusicService(_mockArtistService.Object, _mockSongService.Object);
        }

        [Test]
        public async Task WhenThereIsAnExistingArtistAndSongList_ThenReturnWordCount()
        {
            // arrange
            var artistName = "Queen";
            var songName = "I want to break free";
            var lyrics = "I want to break free, I want to break free, I want to break free from your lies";

            _mockArtistService.Setup(_ => _.GetArtist(It.IsAny<string>())).Returns(Task.FromResult(new Artist { Recordings = new List<Recording> { new Recording { Title = songName } } }));
            _mockSongService.Setup(_ => _.GetSong(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(new Song { Lyrics = lyrics }));

            // act
            var wordCount = await _musicService.GetAverageSongCountForArtist(artistName);

            // assert
            wordCount.ShouldBe(18);
        }

        [Test]
        public async Task WhenThereIsAnExistingArtistAndMultipleSongList_ThenReturnWordCount()
        {
            // arrange
            var artistName = "Queen";
            var songName1 = "I want to break free";
            var lyrics1 = "I want to break free, I want to break free, I want to break free from your lies";
            var songName2 = "Bohemian Rhapsody";
            var lyrics2 = "I see a little silhouetto of a man, Scaramouch, Scaramouch, will you do the Fandango!";

            _mockArtistService.Setup(_ => _.GetArtist(It.IsAny<string>())).Returns(Task.FromResult(new Artist { Recordings = new List<Recording> { new Recording { Title = songName1 }, new Recording { Title = songName2 } } }));
            _mockSongService.Setup(_ => _.GetSong(artistName, songName1)).Returns(Task.FromResult(new Song { Lyrics = lyrics1 }));
            _mockSongService.Setup(_ => _.GetSong(artistName, songName2)).Returns(Task.FromResult(new Song { Lyrics = lyrics2 }));

            // act
            var wordCount = await _musicService.GetAverageSongCountForArtist(artistName);

            // assert
            wordCount.ShouldBe(16);
        }

        [Test]
        public async Task WhenThereIsAnExistingArtistButNoSongList_ThenReturnZero()
        {
            // arrange
            var artistName = "Queen";
            var songName = "I want to break free";
            _mockArtistService.Setup(_ => _.GetArtist(It.IsAny<string>())).Returns(Task.FromResult(new Artist { Recordings = new List<Recording> { new Recording { Title = songName } } }));
            _mockSongService.Setup(_ => _.GetSong(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult((Song)null));

            // act
            var wordCount = await _musicService.GetAverageSongCountForArtist(artistName);

            // assert
            wordCount.ShouldBe(0);
        }

        [Test]
        public async Task WhenThereIsntAnExistingArtistAndNullSongList_ThenReturnZero()
        {
            // arrange
            var artistName = "Queen";
            _mockArtistService.Setup(_ => _.GetArtist(It.IsAny<string>())).Returns(Task.FromResult((Artist)null));
            _mockSongService.Setup(_ => _.GetSong(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult((Song)null));

            // act
            var wordCount = await _musicService.GetAverageSongCountForArtist(artistName);

            // assert
            wordCount.ShouldBe(0);
        }

        [Test]
        public async Task WhenThereIsAnExistingArtistButEmptyRecordingsList_ThenReturnZero()
        {
            // arrange
            var artistName = "Queen";
            _mockArtistService.Setup(_ => _.GetArtist(It.IsAny<string>())).Returns(Task.FromResult(new Artist { Recordings = new List<Recording>() }));
            _mockSongService.Setup(_ => _.GetSong(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult((Song)null));

            // act
            var wordCount = await _musicService.GetAverageSongCountForArtist(artistName);

            // assert
            wordCount.ShouldBe(0);
        }
    }
}
