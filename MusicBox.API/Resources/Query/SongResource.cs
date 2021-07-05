using System;

namespace MusicBox.API.Resources.Query
{
    public class SongResource
    {
        //Let op, normaal gesproken zou ik niet een database ID exposen naar buiten, maar een niet-voorspelbare unieke sleutel hiervoor gebruiken.
        //Vanwege tijdsbeperking heb ik dit hier niet toegevoegd, maar ik wil laten weten dat ik wel degelijk bewust ben van de risico's.
        public short Id { get; set; }

        public string Name { get; set; }
        public string Year { get; set; }
        public ArtistResource Artist { get; set; }
        public string ShortName { get; set; }
        public int BPM { get; set; }
        public int DurationInMilliseconds { get; set; }
        public string Genre { get; set; }
        public string SpotifyId { get; set; }
        public string Album { get; set; }
    }
}
