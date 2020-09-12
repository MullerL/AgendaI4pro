using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaI4pro.Models
{
    public class ContatoDAL : IContatoDAL
    {
        string connectionString = @"Data Source=DESKTOP-HP943ST\SQLEXPRESS;Initial Catalog=BancoI4pro;Integrated Security=True;";

        public IEnumerable<Contato> GetAllContato()
        {
            List<Contato> lstContato = new List<Contato>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT Id, Nome from Contato", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Contato Contato = new Contato();
                    Contato.Id = Convert.ToInt32(rdr["Id"]);
                    Contato.Nome = rdr["Nome"].ToString();
                    lstContato.Add(Contato);
                }
                con.Close();
            }
            return lstContato;
        }
        public void AddContato(Contato contato)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = @"Insert into Contato (Nome) Values(@Nome)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Nome", contato.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                comandoSQL = "SELECT IDENT_CURRENT('contato') Id";
                cmd = new SqlCommand(comandoSQL, con);
                con.Open();
                var rdr = cmd.ExecuteScalar();
                decimal idContato = (decimal)rdr;
                con.Close();

                foreach (var item in contato.Email)
                {
                    if (String.IsNullOrEmpty(item))
                        continue;

                    comandoSQL = @"Insert into Email (Email, IdContato) Values(@Email, @IdContato)";
                    cmd = new SqlCommand(comandoSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Email", item);
                    cmd.Parameters.AddWithValue("@IdContato", idContato);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();                    
                }

                foreach (var item in contato.Telefone)
                {
                    if (String.IsNullOrEmpty(item))
                        continue;

                    comandoSQL = @"Insert into Telefone (Telefone, IdContato) Values(@Telefone, @IdContato)";
                    cmd = new SqlCommand(comandoSQL, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Telefone", item);
                    cmd.Parameters.AddWithValue("@IdContato", idContato);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public void UpdateContato(Contato contato)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = @"Update Contato set Nome = @Nome where Id = @Id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", contato.Id);
                cmd.Parameters.AddWithValue("@Nome", contato.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (contato.IdEmail != null)
                {
                    for (int i = 0; i < contato.IdEmail.Count; i++)
                    {
                        if (i == contato.Email.Count || String.IsNullOrEmpty(contato.Email[i]))
                        {
                            comandoSQL = @"Delete from Email where Id = @IdEmail and IdContato = @Id";
                            cmd = new SqlCommand(comandoSQL, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@IdEmail", contato.IdEmail[i]);
                            cmd.Parameters.AddWithValue("@Id", contato.Id);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            comandoSQL = @"Update Email set Email = @Email where Id = @IdEmail and IdContato = @Id";
                            cmd = new SqlCommand(comandoSQL, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Email", contato.Email[i]);
                            cmd.Parameters.AddWithValue("@IdEmail", contato.IdEmail[i]);
                            cmd.Parameters.AddWithValue("@Id", contato.Id);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    for (int i = contato.IdEmail.Count; i < contato.Email.Count; i++)
                    {
                        if (String.IsNullOrEmpty(contato.Email[i]))
                            continue;

                        comandoSQL = @"Insert into Email (Email, IdContato) Values(@Email, @IdContato)";
                        cmd = new SqlCommand(comandoSQL, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", contato.Email[i]);
                        cmd.Parameters.AddWithValue("@IdContato", contato.Id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    } 
                }
                else if (contato.Email != null)
                {
                    foreach (var item in contato.Email)
                    {
                        if (String.IsNullOrEmpty(item))
                            continue;

                        comandoSQL = @"Insert into Email (Email, IdContato) Values(@Email, @IdContato)";
                        cmd = new SqlCommand(comandoSQL, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Email", item);
                        cmd.Parameters.AddWithValue("@IdContato", contato.Id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                if (contato.IdTelefone != null)
                {
                    for (int i = 0; i < contato.IdTelefone.Count; i++)
                    {
                        if (i == contato.Telefone.Count || String.IsNullOrEmpty(contato.Telefone[i]))
                        {
                            comandoSQL = @"Delete from Telefone where Id = @IdTelefone and IdContato = @Id";
                            cmd = new SqlCommand(comandoSQL, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@IdTelefone", contato.IdTelefone[i]);
                            cmd.Parameters.AddWithValue("@Id", contato.Id);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            comandoSQL = @"Update Telefone set Telefone = @Telefone where Id = @IdTelefone and IdContato = @Id";
                            cmd = new SqlCommand(comandoSQL, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Telefone", contato.Telefone[i]);
                            cmd.Parameters.AddWithValue("@IdTelefone", contato.IdTelefone[i]);
                            cmd.Parameters.AddWithValue("@Id", contato.Id);
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    for (int i = contato.IdTelefone.Count; i < contato.Telefone.Count; i++)
                    {
                        if (String.IsNullOrEmpty(contato.Telefone[i]))
                            continue;

                        comandoSQL = @"Insert into Telefone (Telefone, IdContato) Values(@Telefone, @IdContato)";
                        cmd = new SqlCommand(comandoSQL, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Telefone", contato.Telefone[i]);
                        cmd.Parameters.AddWithValue("@IdContato", contato.Id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                else if (contato.Telefone != null)
                {
                    foreach (var item in contato.Telefone)
                    {
                        if (String.IsNullOrEmpty(item))
                            continue;

                        comandoSQL = @"Insert into Telefone (Telefone, IdContato) Values(@Telefone, @IdContato)";
                        cmd = new SqlCommand(comandoSQL, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Telefone", item);
                        cmd.Parameters.AddWithValue("@IdContato", contato.Id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
        }
        public Contato GetContato(int? id)
        {
            Contato contato = new Contato();
            contato.IdEmail = new List<int>();
            contato.Email = new List<string>();
            contato.IdTelefone = new List<int>();
            contato.Telefone = new List<string>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Contato WHERE Id= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    contato.Id = Convert.ToInt32(rdr["Id"]);
                    contato.Nome = rdr["Nome"].ToString();
                }
                con.Close();
                con.Open();
                sqlQuery = "SELECT * FROM Email WHERE IdContato= " + id;
                cmd = new SqlCommand(sqlQuery, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    contato.IdEmail.Add((int)rdr["Id"]);
                    contato.Email.Add(rdr["Email"].ToString());
                }
                con.Close();
                con.Open();
                sqlQuery = "SELECT * FROM Telefone WHERE IdContato= " + id;
                cmd = new SqlCommand(sqlQuery, con);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    contato.IdTelefone.Add((int)rdr["Id"]);
                    contato.Telefone.Add(rdr["Telefone"].ToString());
                }
            }
            return contato;
        }
        public void DeleteContato(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = @"Delete from Contato where Id = @Id
                                      Delete from Email where IdContato = @Id
                                      Delete from Telefone where IdContato = @Id";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
