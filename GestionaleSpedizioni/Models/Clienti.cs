using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionaleSpedizioni.Models
{
    public class Clienti
    {
        public int IdClienti { get; set; }


        [Required(ErrorMessage ="Il Nominativo è obbligatorio")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Codice Fiscale o Partita Iva è obbligatorio")]
        public string CodiceFiscaleOPartitaIva { get; set; }

        [Required(ErrorMessage = "L'Indirizzo è obbligatorio")]
        public string Indirizzo { get; set; } 
        
        [Required(ErrorMessage = "La Città è obbligatoria")]
        public string Citta { get; set; }
        public string Tipologia { get; set; }







        public static void AddClienti(Clienti p)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Clienti values (@Nome,@CodiceFiscaleOPartitaIva,@Indirizzo,@Citta,@Tipologia)", conn);

                cmd.Parameters.AddWithValue("Nome", p.Nome);
                cmd.Parameters.AddWithValue("CodiceFiscaleOPartitaIva", p.CodiceFiscaleOPartitaIva);
                cmd.Parameters.AddWithValue("Indirizzo", p.Indirizzo);
                cmd.Parameters.AddWithValue("Citta", p.Citta);
                cmd.Parameters.AddWithValue("Tipologia", p.Tipologia);



                cmd.ExecuteNonQuery();



            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }


        }



        public static List<Clienti> ListClienti()
        {
            List<Clienti> clienti = new List<Clienti>();
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Clienti", conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Clienti p = new Clienti();
                    p.IdClienti = Convert.ToInt32(reader["IdClienti"].ToString());
                    p.Nome = reader["Nome"].ToString();
                    p.CodiceFiscaleOPartitaIva = reader["CodiceFiscaleOPartitaIva"].ToString();
                    p.Indirizzo = reader["Indirizzo"].ToString();
                    p.Citta = reader["Citta"].ToString();
                    p.Tipologia = reader["Tipologia"].ToString();
                   


                    clienti.Add(p);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return clienti;
        }




        public static Clienti DettaglioClienti(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            Clienti p = new Clienti();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"select * from Clienti where IdClienti={id}", conn);

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    p.IdClienti = Convert.ToInt32(reader["IdClienti"].ToString());
                    p.Nome = reader["Nome"].ToString();
                    p.CodiceFiscaleOPartitaIva = reader["CodiceFiscaleOPartitaIva"].ToString();
                    p.Indirizzo = reader["Indirizzo"].ToString();
                    p.Citta = reader["Citta"].ToString();
                    p.Tipologia = reader["Tipologia"].ToString();



                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return p;
        }


        public static void ModificaCliente(Clienti p)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"update Clienti set Nome=@Nome,CodiceFiscaleOPartitaIva=@CodiceFiscaleOPartitaIva,Indirizzo=@Indirizzo,Citta=@Citta,Tipologia=@Tipologia where IdClienti={p.IdClienti}", conn);


                cmd.Parameters.AddWithValue("Nome", p.Nome);
             cmd.Parameters.AddWithValue("CodiceFiscaleOPartitaIva", p.CodiceFiscaleOPartitaIva);
                cmd.Parameters.AddWithValue("Indirizzo", p.Indirizzo);
                cmd.Parameters.AddWithValue("Citta", p.Citta);
                cmd.Parameters.AddWithValue("Tipologia", p.Tipologia);
            



                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
        }


        public static void Delete(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"delete from Clienti where IdClienti={id}", conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }
}