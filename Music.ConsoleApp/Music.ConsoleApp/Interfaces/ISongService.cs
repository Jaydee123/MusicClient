using Music.ConsoleApp.Entities;
using System.Threading.Tasks;

namespace Music.ConsoleApp.Interfaces
{
    public interface ISongService
    {
        Task<Song> GetSong(string artist, string song);
    }
}
