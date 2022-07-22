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
        public IEnumerable<Partida> Get()
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "CALL `sys`.`uspGetPartidas`();";
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
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Brasileirao>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
