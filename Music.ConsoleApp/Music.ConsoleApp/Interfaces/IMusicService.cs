using System.Threading.Tasks;

namespace Music.ConsoleApp.Interfaces
{
    interface IMusicService
    {
        Task<int> GetAverageSongCountForArtist(string name);
    }
}
