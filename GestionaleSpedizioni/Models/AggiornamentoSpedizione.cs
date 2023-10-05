using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Cache;
using System.Net.Http;

namespace GestionaleSpedizioni.Models
{
    public class AggiornamentoSpedizione
    {
        public int IDAggiornamento { get; set; }

        [Required(ErrorMessage ="Inserisci lo stato della consegna")]
        public string Stato { get; set; }
        public string Luogo { get; set; }
        public string Descrizione { get; set; }

        [Display(Name ="Aggiornamento")]
        public DateTime DataOra { get; set; }

        [Display(Name ="Numero Spedizione")]
        public int IdSpedizione { get; set; }




        public static void AddSpedizione(AggiornamentoSpedizione p,int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into AggiornamentiSpedizione values (@Stato, @Luogo, @Descrizione, @DataOra,@IdSpedizione)", conn);

                cmd.Parameters.AddWithValue("@Stato", p.Stato);
                cmd.Parameters.AddWithValue("@Luogo", p.Luogo);
                cmd.Parameters.AddWithValue("@Descrizione", p.Descrizione);
                cmd.Parameters.AddWithValue("@DataOra", DateTime.Now);
                cmd.Parameters.AddWithValue("@IdSpedizione", id);



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



        public static List<AggiornamentoSpedizione> ListAggiornamento()
        {
            List<AggiornamentoSpedizione> aggiornamento = new List<AggiornamentoSpedizione>();
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from AggiornamentiSpedizione", conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    AggiornamentoSpedizione p = new AggiornamentoSpedizione();
                    p.IDAggiornamento = Convert.ToInt32(reader["IDAggiornamento"]);
                    p.Stato = reader["Stato"].ToString();
                    p.Luogo = reader["Luogo"].ToString();
                    p.Descrizione = reader["Descrizione"].ToString();
                    p.DataOra = Convert.ToDateTime(reader["DataOra"]);
                    p.IdSpedizione = Convert.ToInt32(reader["IdSpedizione"]);



                    aggiornamento.Add(p);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return aggiornamento;
        }




        public static AggiornamentoSpedizione DettaglioAggiornamento(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            AggiornamentoSpedizione p = new AggiornamentoSpedizione();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"select * from AggiornamentiSpedizione where IDSpedizione={id}", conn);

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    p.IDAggiornamento = Convert.ToInt32(reader["IDAggiornamento"]);
                    p.Stato = reader["Stato"].ToString();
                    p.Luogo = reader["Luogo"].ToString();
                    p.Descrizione = reader["Descrizione"].ToString();
                    p.DataOra = Convert.ToDateTime(reader["DataOra"]);
                    p.IdSpedizione = Convert.ToInt32(reader["IdSpedizione"]);




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


        public static void ModificaAggiornamento(AggiornamentoSpedizione p)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"update AggiornamentiSpedizione set Stato = @Stato,Luogo = @Luogo,Descrizione = @Descrizione, DataOra = @DataOra  WHERE IDSpedizione = {p.IdSpedizione}", conn);


                cmd.Parameters.AddWithValue("@Stato", p.Stato);
                cmd.Parameters.AddWithValue("@Luogo", p.Luogo);
                cmd.Parameters.AddWithValue("@Descrizione", p.Descrizione);
                cmd.Parameters.AddWithValue("@DataOra",DateTime.Now);
                cmd.Parameters.AddWithValue("@IdSpedizione", p.IdSpedizione);




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
                SqlCommand cmd = new SqlCommand($"delete from AggiornamentiSpedizioni where IDSpedizione={id}", conn);
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