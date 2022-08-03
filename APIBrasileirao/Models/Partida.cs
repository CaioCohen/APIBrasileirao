namespace APIBrasileirao.Models
{
    public class Partida
    {
        public int IDPartida { get; set; }
        public string DataPartida { get; set; }
        public int Rodada { get; set; }
        public bool EhMandante { get; set; }
        public int QuantidadeGols { get; set; }
        public string Escudo { get; set; }
        public string Sigla { get; set; }
        public string Estadio { get; set; }
        public string Clube { get; set; }
    }
}
