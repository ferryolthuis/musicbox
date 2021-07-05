namespace MusicBox.Model
{
    public class Artist
    {
        public short Id { get; set; }
        public string Name { get; set; }

        public void UpdateProperties(Artist fromArtist)
        {
            Name = fromArtist.Name;
        }
    }
}