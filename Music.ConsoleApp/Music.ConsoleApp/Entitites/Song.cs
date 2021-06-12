namespace Music.ConsoleApp.Entities
{
    public class Song
    {
        public string Name { get; set; }

        public string ArtistName { get; set; }
        public string Lyrics { get; set; }
        public int WordCount
        {
            get
            {
                if (Lyrics != null)
                {
                    return Lyrics.Split(' ').Length;
                }
                return 0;
            }
        }
    }
}
