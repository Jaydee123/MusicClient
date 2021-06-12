using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Music.ConsoleApp.Interfaces;
using Music.ConsoleApp.Entities;
using Music.ConsoleApp.Services;
using Music.ConsoleApp.Utils;
using System;

namespace Music.ConsoleApp
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

            Consts.ARTIST_API_URL = config.GetSection("ArtistUrl").Value;
            Consts.SONG_API_URL = config.GetSection("SongUrl").Value;

            ConfigureServices();

            var musicService= serviceProvider.GetService<IMusicService>();
            Console.WriteLine($"Artist: Queen, Average Song Count:" + musicService.GetAverageSongCountForArtist("Queen").Result);
        }

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddTransient<IArtistService, ArtistService>();
            services.AddTransient<ISongService, SongService>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IHttpClientService<Artist>, HttpClientService<Artist>>();
            services.AddTransient<IHttpClientService<Song>, HttpClientService<Song>>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}