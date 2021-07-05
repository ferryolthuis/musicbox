using System.ComponentModel.DataAnnotations;

namespace MusicBox.API.Resources.Command
{
    public class SaveArtistResource
    {
        //Ik heb er hier bewust voor gekozen de gebruiker niet zelf de ID's te laten kiezen.
        //Dat voegt imo niks toe, vereist allerlei extra code terwijl nu EF het voor mij oplost.
        //Daarnaast vind ik database ID's in een publieke API laag een beveiligingsrisico.
        [Required]
        public string Name { get; set; }
    }
}
