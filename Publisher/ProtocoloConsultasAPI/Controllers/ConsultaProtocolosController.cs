using Dapper;
using ProtocoloConsultasAPI.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Configuration;

namespace ProtocoloConsultasAPI.Controllers
{
    [RoutePrefix("api/consultaProtocolos")]
    public class ConsultaProtocolosController : ApiController
    {
        private readonly string _connectionString;

        public ConsultaProtocolosController()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Método para autenticação e geração do token
        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate([FromBody] LoginModel model)
        {
            if (model.Username == "admin" && model.Password == "senha")
            {
                var token = JwtAuthentication.GenerateToken(model.Username);
                return Ok(new { token });
            }

            Logger.Log($"Falha na autenticação para o usuário {model.Username}");
            return Unauthorized();
        }

        // GET: api/consultaProtocolos/porNumeroProtocolo/{numeroProtocolo}
        [HttpGet]
        [JwtAuthorize]
        [Route("porNumeroProtocolo/{numeroProtocolo}")]
        public IHttpActionResult PorNumeroProtocolo(string numeroProtocolo)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Protocolos WHERE NumeroProtocolo = @NumeroProtocolo";
                var protocolo = connection.QueryFirstOrDefault<Protocolo>(sql, new { NumeroProtocolo = numeroProtocolo });

                if (protocolo == null)
                {
                    Logger.Log($"Protocolo não encontrado: {numeroProtocolo}");
                    return NotFound();
                }

                return Ok(protocolo);
            }
        }

        // GET: api/consultaProtocolos/porCpf/{cpf}
        [HttpGet]
        [JwtAuthorize]
        [Route("porCpf/{cpf}")]
        public IHttpActionResult PorCpf(string cpf)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Protocolos WHERE Cpf = @Cpf";
                var protocolos = connection.Query<Protocolo>(sql, new { Cpf = cpf });

                if (!protocolos.Any())
                {
                    Logger.Log($"Nenhum protocolo encontrado para o CPF: {cpf}");
                    return NotFound();
                }

                return Ok(protocolos);
            }
        }

        // GET: api/consultaProtocolos/porRg/{rg}
        [HttpGet]
        [JwtAuthorize]
        [Route("porRg/{rg}")]
        public IHttpActionResult PorRg(string rg)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Protocolos WHERE Rg = @Rg";
                var protocolos = connection.Query<Protocolo>(sql, new { Rg = rg });

                if (!protocolos.Any())
                {
                    Logger.Log($"Nenhum protocolo encontrado para o RG: {rg}");
                    return NotFound();
                }

                return Ok(protocolos);
            }
        }
    }
}
