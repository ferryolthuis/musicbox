using System;

namespace MusicBox.Model
{
    public class Song
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Artist Artist { get; set; }
        public string ShortName { get; set; }
        public int BPM { get; set; }
        public TimeSpan Duration { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }
        public string Album { get; set; }

        public void UpdateProperties(Song song)
        {
            Name = song.Name;
            Year = song.Year;
            Artist = song.Artist;
            ShortName = song.ShortName;
            BPM = song.BPM;
            Duration = song.Duration;
            Genre = song.Genre;
            SpotifyId = song.SpotifyId;
            Album = song.Album;
        }
    }
}
