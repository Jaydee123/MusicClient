using Music.ConsoleApp.Interfaces;
using Music.ConsoleApp.Entities;
using Music.ConsoleApp.Utils;
using System.Threading.Tasks;

namespace Music.ConsoleApp.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IHttpClientService<Artist> _httpClientService;

        public Artist _artist { get; set; }

        public ArtistService(IHttpClientService<Artist> httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<Artist> GetArtist(string name)
        {
            var artist = await _httpClientService.Get(Consts.ARTIST_API_URL + "recording/?query=artist:" + name + "&fmt=json");

            if(artist != null)
            {
                artist.Name = name;
            }

            return artist;
        }
    }
}
