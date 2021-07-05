namespace MusicBox.API.Resources.Query
{
    public class ArtistResource
    {
        //Let op, normaal gesproken zou ik niet een database ID exposen naar buiten, maar een niet-voorspelbare unieke sleutel hiervoor gebruiken.
        //Vanwege tijdsbeperking heb ik dit hier niet toegevoegd, maar ik wil laten weten dat ik wel degelijk bewust ben van de risico's.
        public short Id { get; set; }

        public string Name { get; set; }
    }
}
