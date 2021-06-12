using System.Collections.Generic;

namespace Music.ConsoleApp.Entities
{

    public class Artist
    {
        public string Name { get; set; }
        public List<Recording> Recordings { get; set; }
    }

    public class Recording
    {
        public string Title { get; set; }
    }
}
