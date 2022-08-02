using APIBrasileirao.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIBrasileirao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Brasileirao : ControllerBase
    {
        MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=sys;port=3306;password=Consultant123");
        MySqlCommand cmd = new MySqlCommand();
        
        // GET: api/<Brasileirao>/partidas
        [HttpGet("partidas")]
        public IEnumerable<Partida> GetPartidas([FromQuery(Name = "rodada")] string rodada, [FromQuery(Name = "temporada")] string temporada)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"CALL `sys`.`uspGetPartidas`({rodada},'{temporada}');";
            MySqlDataReader myReader;
            List<Partida>  retorno = new List<Partida>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    var partida = new Partida();
                    partida.IDPartida = myReader.GetInt32(0);
                    partida.DataPartida = myReader.GetString(1);
                    partida.Rodada = myReader.GetInt32(2);
                    partida.EhMandante = myReader.GetBoolean(3);
                    partida.QuantidadeGols = myReader.GetInt32(4);
                    partida.Escudo = myReader.GetString(5);
                    partida.Sigla = myReader.GetString(6);
                    retorno.Add(partida);
                    Console.WriteLine(myReader.GetString(6));
                }
            }
            finally
            {
                myReader.Close();
                conn.Close();
            }
            return retorno;
        }

        // GET api/<Brasileirao>/5
        [HttpGet("partidas/{id}")]
        public IEnumerable<PartidaDetalhe> GetPartida(int id)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"CALL `sys`.`uspGetPartidaDetalhe`({id});";
            MySqlDataReader myReader;
            List<PartidaDetalhe>  retorno = new List<PartidaDetalhe>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    var partida = new PartidaDetalhe();
                    partida.IDPartida = myReader.GetInt32(0);
                    partida.DataPartida = myReader.GetString(1);
                    partida.Rodada = myReader.GetInt32(2);
                    partida.Estadio = myReader.GetString(3);
                    partida.Clube = myReader.GetString(4);
                    partida.Clube = myReader.GetString(5);
                    partida.EhMandante = myReader.GetBoolean(6);
                    partida.QuantidadeGols = myReader.GetInt32(7);
                    partida.Tecnico = myReader.GetString(8);
                    partida.Formacao = myReader.GetString(9);
                    retorno.Add(partida);
                }
            }
            finally
            {
                myReader.Close();
                conn.Close();
            }
            return retorno;
        }

        // POST api/<Brasileirao>
        [HttpPost("partida")]
        public string Post([FromBody] PartidaPost partida)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"CALL `sys`.`uspInsertPartida`('{partida.DataPartidaIN}', {partida.RodadaIN}, '{partida.EstadioIN}', '{partida.ClubeVencedorIN}', '{partida.Mandante}', '{partida.Visitante}', {partida.PlacarMandante}, {partida.PlacarVisitante}, '{partida.TecnicoMandante}', '{partida.TecnicoVisitante}', '{partida.FormacaoMandante}', '{partida.FormacaoVisitante}');";
            try
            {
                cmd.ExecuteNonQuery();
                return ("Partida Adicionada");
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpGet("times")]
        public IEnumerable<Time> GetTimes()
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "CALL `sys`.`uspGetTimes`();";
            MySqlDataReader myReader;
            List<Time> retorno = new List<Time>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    var time = new Time();
                    time.DataCriacao = myReader.GetDateTime(0).Date.ToString().Split(' ')[0];
                    time.Escudo = myReader.GetString(1);
                    time.Sigla = myReader.GetString(2);
                    time.Clube = myReader.GetString(3);
                    retorno.Add(time);
                }
            }
            finally
            {
                myReader.Close();
                conn.Close();
            }
            return retorno;
        }

        [HttpGet("gols/{id}")]
        public IEnumerable<Gol> GetGols(int id)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"CALL `sys`.`uspGetGols`({id});";
            MySqlDataReader myReader;
            List<Gol> retorno = new List<Gol>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    var gol = new Gol();
                    gol.IDPartida = myReader.GetInt32(0);
                    gol.Jogador = myReader.GetString(1);
                    gol.Minutagem = myReader.GetInt32(2);
                    gol.Clube = myReader.GetString(3);
                    retorno.Add(gol);
                }
            }
            finally
            {
                myReader.Close();
                conn.Close();
            }
            return retorno;
        }

        [HttpGet("cartoes/{id}")]
        public IEnumerable<Cartao> GetCartoes(int id, [FromQuery(Name = "clube")] string clube)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"CALL `sys`.`uspGetCartoes`({id}, '{clube}');";
            MySqlDataReader myReader;
            List<Cartao> retorno = new List<Cartao>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    var cartao = new Cartao();
                    cartao.IDPartida = myReader.GetInt32(0);
                    cartao.Cor = myReader.GetString(1);
                    cartao.Jogador = myReader.GetString(2);
                    cartao.Tempo = myReader.GetInt32(3);
                    retorno.Add(cartao);
                }
            }
            finally
            {
                myReader.Close();
                conn.Close();
            }
            return retorno;
        }

        [HttpGet("jogadores/{clube}")]
        public IEnumerable<Jogador> GetJogadores(string clube)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = $"CALL `sys`.`uspGetJogadores`('{clube}');";
            MySqlDataReader myReader;
            List<Jogador> retorno = new List<Jogador>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    var jogador = new Jogador();
                    jogador.IDJogador = myReader.GetInt32(0);
                    jogador.Nome = myReader.GetString(1);
                    jogador.Clube = myReader.GetString(2);
                    jogador.NumeroCamisa = myReader.GetInt32(3);
                    retorno.Add(jogador);
                }
            }
            finally
            {
                myReader.Close();
                conn.Close();
            }
            return retorno;
        }

        [HttpGet("gols")]
        public IEnumerable<QuantGols> GetJogadores([FromQuery(Name = "temporada")] string temporada, [FromQuery(Name = "clube")] string clube)
        {
            conn.Open();
            cmd.Connection = conn;
            if(temporada == null || clube == null)
            {
                throw new Exception("Temporada ou clube null");
                return new List<QuantGols>();
            }
            cmd.CommandText = $"CALL `sys`.`uspGetGolsTimeTemporada`('{temporada}','{clube}');";
            MySqlDataReader myReader;
            List<QuantGols> retorno = new List<QuantGols>();
            myReader = cmd.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    var quantidade = new QuantGols();
                    quantidade.QuantidadeGols = myReader.GetInt32(0);
                    quantidade.Clube = myReader.GetString(1);
                    quantidade.Jogador = myReader.GetString(2);
                    retorno.Add(quantidade);
                }
            }
            finally
            {
                myReader.Close();
                conn.Close();
            }
            return retorno;
        }

        // PUT api/<Brasileirao>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Brasileirao>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
