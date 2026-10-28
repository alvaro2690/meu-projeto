using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelgest
{
    class Metodos
    {
        private readonly string conexao =
         @"Data Source=Sandra-Ventura;Initial Catalog=travelGest;Integrated Security=True;";

        private string CriarHash(string senha)
        {
            byte[] salt = new byte[16];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                senha, salt, 10000))
            {
                byte[] hash = pbkdf2.GetBytes(32);

                return Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }
        }

        private bool ValidarSenha(string senha, string armazenada)
        {
            string[] partes = armazenada.Split(':');

            if (partes.Length != 2)
                return false;

            byte[] salt = Convert.FromBase64String(partes[0]);
            byte[] hashEsperado = Convert.FromBase64String(partes[1]);

            using (var pbkdf2 = new Rfc2898DeriveBytes(
                senha, salt, 10000))
            {
                byte[] hashAtual = pbkdf2.GetBytes(32);

                if (hashAtual.Length != hashEsperado.Length)
                    return false;

                int diferenca = 0;

                for (int i = 0; i < hashAtual.Length; i++)
                    diferenca |= hashAtual[i] ^ hashEsperado[i];

                return diferenca == 0;
            }
        }

        public bool CadastrarUtilizador(
            string nome, string email, string contacto, string senha)
        {
            using (SqlConnection cn = new SqlConnection(conexao))
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Utilizadores
                  (Nome, Email, Contacto, SenhaHash)
                  VALUES (@Nome, @Email, @Contacto, @SenhaHash)", cn))
            {
                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = nome.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email.Trim();
                cmd.Parameters.Add("@Contacto", SqlDbType.VarChar, 30).Value = contacto.Trim();
                cmd.Parameters.Add("@SenhaHash", SqlDbType.VarChar, 255).Value = CriarHash(senha);

                cn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Login(string email, string senha)
        {
            using (SqlConnection cn = new SqlConnection(conexao))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT SenhaHash FROM Utilizadores WHERE Email = @Email", cn))
            {
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email.Trim();

                cn.Open();
                object resultado = cmd.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                    return false;

                return ValidarSenha(senha, resultado.ToString());
            }
        }

        public DataTable ListarClientes()
        {
            DataTable tabela = new DataTable();

            using (SqlConnection cn = new SqlConnection(conexao))
            using (SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT Id, Nome, Telefone, Email, Morada
                  FROM Clientes ORDER BY Id DESC", cn))
            {
                da.Fill(tabela);
            }

            return tabela;
        }

        public bool GuardarCliente(
            int id, string nome, string telefone,
            string email, string morada)
        {
            string sql = id == 0
                ? @"INSERT INTO Clientes (Nome, Telefone, Email, Morada)
                    VALUES (@Nome, @Telefone, @Email, @Morada)"
                : @"UPDATE Clientes SET Nome=@Nome, Telefone=@Telefone,
                    Email=@Email, Morada=@Morada WHERE Id=@Id";

            using (SqlConnection cn = new SqlConnection(conexao))
            using (SqlCommand cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = nome.Trim();
                cmd.Parameters.Add("@Telefone", SqlDbType.VarChar, 30).Value = telefone.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value =
                    string.IsNullOrWhiteSpace(email) ? (object)DBNull.Value : email.Trim();
                cmd.Parameters.Add("@Morada", SqlDbType.VarChar, 200).Value =
                    string.IsNullOrWhiteSpace(morada) ? (object)DBNull.Value : morada.Trim();

                if (id != 0)
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                cn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool EliminarCliente(int id)
        {
            using (SqlConnection cn = new SqlConnection(conexao))
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM Clientes WHERE Id=@Id", cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                cn.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public DataTable ListarReservas()
        {
            DataTable tabela = new DataTable();

            string sql = @"
                SELECT R.Id,
                       C.Nome AS Cliente,
                       V.Origem + ' - ' + V.Destino AS Viagem,
                       R.DataReserva,
                       ISNULL(VD.ValorTotal, V.Preco) AS Valor,
                       R.Estado,
                       VD.MetodoPagamento
                FROM Reservas R
                INNER JOIN Clientes C ON C.Id = R.ClienteId
                INNER JOIN Viagens V ON V.Id = R.ViagemId
                LEFT JOIN Vendas VD ON VD.ReservaId = R.Id
                ORDER BY R.Id DESC";

            using (SqlConnection cn = new SqlConnection(conexao))
            using (SqlDataAdapter da = new SqlDataAdapter(sql, cn))
            {
                da.Fill(tabela);
            }

            return tabela;
        }

        public bool AdicionarReserva(int clienteId, int viagemId)
        {
            using (SqlConnection cn = new SqlConnection(conexao))
            {
                cn.Open();

                using (SqlTransaction transacao = cn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                            IF NOT EXISTS (
                                SELECT 1 FROM Clientes WHERE Id=@ClienteId
                            )
                                THROW 50001, 'Cliente inexistente.', 1;

                            IF NOT EXISTS (
                                SELECT 1 FROM Viagens
                                WHERE Id=@ViagemId
                                  AND LugaresDisponiveis > 0
                            )
                                THROW 50002, 'Viagem inexistente ou sem lugares.', 1;

                            INSERT INTO Reservas
                                (ClienteId, ViagemId, Estado)
                            VALUES
                                (@ClienteId, @ViagemId, 'Pendente');",
                            cn, transacao))
                        {
                            cmd.Parameters.Add("@ClienteId", SqlDbType.Int).Value = clienteId;
                            cmd.Parameters.Add("@ViagemId", SqlDbType.Int).Value = viagemId;
                            cmd.ExecuteNonQuery();
                        }

                        transacao.Commit();
                        return true;
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }
        public DataTable ListarViagens()
        {
            DataTable tabela = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT Id,
                   Origem,
                   Destino,
                   DataPartida,
                   Preco,
                   LugaresDisponiveis
            FROM Viagens
            ORDER BY Origem, Destino";

                using (SqlDataAdapter adaptador =
                    new SqlDataAdapter(sql, con))
                {
                    adaptador.Fill(tabela);
                }
            }

            return tabela;
        }

        public string connectionString { get; set; }

        public bool EliminarReserva(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Reservas WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();

                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    return linhasAfetadas > 0;
                }
            }
        }

        public bool AdicionarReserva(int clienteId, int viagemId, string estado)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sql = @"
            INSERT INTO Reservas
                (ClienteId, ViagemId, DataReserva, Estado)
            VALUES
                (@ClienteId, @ViagemId, GETDATE(), @Estado)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ClienteId", clienteId);
                    cmd.Parameters.AddWithValue("@ViagemId", viagemId);
                    cmd.Parameters.AddWithValue("@Estado", estado);

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
    
}
