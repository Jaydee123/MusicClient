using Microsoft.Extensions.Configuration;
using Music.ConsoleApp.Interfaces;
using Music.ConsoleApp.Entities;
using Music.ConsoleApp.Utils;
using System.Threading.Tasks;

namespace Music.ConsoleApp.Services
{
    public class SongService : ISongService
    {
        private readonly IHttpClientService<Song> _httpClientService;

        public Artist _artist { get; set; }

        public SongService(IHttpClientService<Song> httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<Song> GetSong(string artist, string songName)
        {
            var song = await _httpClientService.Get(Consts.SONG_API_URL + "/" + artist + "/" + songName);
            
            if (song != null)
            {
                song.Name = songName;
                song.ArtistName = artist;
            }

            return song;
        }
    }
}
