using MusicBox.API.Resources.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MusicBox.API.Resources.Command
{
    public class SaveSongResource
    {
        //Ik heb er hier bewust voor gekozen de gebruiker niet zelf de ID's te laten kiezen.
        //Dat voegt imo niks toe, vereist allerlei extra code terwijl nu EF het voor mij oplost.
        //Daarnaast vind ik database ID's in een publieke API laag een beveiligingsrisico.
        [Required]
        public string Name { get; set; }

        [PastYear]
        public int Year { get; set; }

        [Required]
        public string Artist { get; set; }

        [Required]
        public string ShortName { get; set; }

        [Range(10, 10000, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double BPM { get; set; }

        [Required] 
        public int DurationInMilliseconds { get; set; }

        [Required]
        public string Genre { get; set; }

        public string SpotifyId { get; set; }

        public string Album { get; set; }
    }
}
