using Music.ConsoleApp.Interfaces;
using Music.ConsoleApp.Entities;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Music.ConsoleApp.Services
{
    public class MusicService : IMusicService
    {
        private readonly IArtistService _artistService;
        private readonly ISongService _songService;

        public MusicService(IArtistService artistService, ISongService songService)
        {
            _artistService = artistService;
            _songService = songService;
        }

        public async Task<int> GetAverageSongCountForArtist(string name)
        {
            var artist = await _artistService.GetArtist(name);
            var songs = new List<Song>();

            if (artist != null && artist.Recordings != null && artist.Recordings.Count > 0)
            {
                artist.Recordings.ToList().ForEach(async _ =>
                {
                    var song = await _songService.GetSong(name, _.Title);
                    if (song != null) songs.Add(song);
                });

                if (songs != null && songs.Count > 0)
                {
                    return (int)(songs.ToList().Average(_ => _.WordCount));
                }
            }

            return 0;
        }
    }
}
