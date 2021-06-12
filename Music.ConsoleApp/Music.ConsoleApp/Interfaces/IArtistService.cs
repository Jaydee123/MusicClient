using Music.ConsoleApp.Entities;
using System.Threading.Tasks;

namespace Music.ConsoleApp.Interfaces
{
    public interface IArtistService
    {
        Task<Artist> GetArtist(string name);
    }
}
