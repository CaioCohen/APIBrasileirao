namespace APIBrasileirao.Models
{
    public class PartidaDetalhe
    {
        public int IDPartida { get; set; }
        public string DataPartida { get; set; }
        public int Rodada { get; set; }
        public string Estadio { get; set; }
        public string ClubeVencedor { get; set; }
        public string Clube { get; set; }
        public bool EhMandante { get; set; }
        public int QuantidadeGols { get; set; }
        public string Tecnico { get; set; }
        public string Formacao { get; set; }
    }
}
