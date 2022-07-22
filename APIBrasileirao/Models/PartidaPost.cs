namespace APIBrasileirao.Models
{
    public class PartidaPost
    {
        public string DataPartidaIN { get; set; }
        public int RodadaIN { get; set; }
        public string EstadioIN { get; set; }
        public string ClubeVencedorIN { get; set; }
        public string Mandante { get; set; }
        public string Visitante { get; set; }
        public int PlacarMandante { get; set; }
        public int PlacarVisitante { get; set; }
        public string TecnicoMandante { get; set; }
        public string TecnicoVisitante { get; set; }
        public string FormacaoMandante { get; set; }
        public string FormacaoVisitante { get; set; }
    }
}
