using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace GestionaleSpedizioni.Models
{
    public class Spedizioni
    {
        [Display(Name ="Numero Spedizione")]
        public int IdSpedizione { get; set; }


        [Display(Name ="Data Spedizione")]
        [Required(ErrorMessage ="Inserisci la data")]
        public DateTime DataSpedizione { get; set; }
        public double Peso { get; set; }

        [Required(ErrorMessage = "Inserisci destinazione")]
        public string Destinazione { get; set; }


        [Display(Name ="Indirizzo Destinatario")]
        [Required(ErrorMessage ="Inserisci indirizzo")]
        public string IndirizzoDest { get; set; }

        [Display(Name ="Costo Spedizione")]
        public double CostoSpedizione { get; set; }

        [Display(Name ="Data Consegna Prevista")]
        public DateTime DataConsegna { get; set; }

        [Display(Name ="Mittente")]
        public int IdClienti { get; set; }



        [Display(Name ="Destinatario")]
        [Required(ErrorMessage ="Inserisci destinatario")]
        public string NomeDestinatario { get; set; }

        public int Tot {  get; set; } 


        public string Nome {  get; set; }


        public static void AddSpedizione(Spedizioni p)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("insert into Spedizioni values (@DataSpedizione, @Peso, @Destinazione, @IndirizzoDest, @CostoSpedizione, @DataConsegna, @IdClienti,@NomeDestinatario)", conn);

                cmd.Parameters.AddWithValue("DataSpedizione", p.DataSpedizione);
                cmd.Parameters.AddWithValue("Peso", p.Peso);
                cmd.Parameters.AddWithValue("Destinazione", p.Destinazione);
                cmd.Parameters.AddWithValue("IndirizzoDest", p.IndirizzoDest);
                cmd.Parameters.AddWithValue("CostoSpedizione", p.CostoSpedizione);
                cmd.Parameters.AddWithValue("DataConsegna", p.DataConsegna);
                cmd.Parameters.AddWithValue("IdClienti", p.IdClienti);
                cmd.Parameters.AddWithValue("NomeDestinatario", p.NomeDestinatario);



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



        public static List<Spedizioni> ListSpedizioni()
        {
            List<Spedizioni> spedizioni = new List<Spedizioni>();
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Clienti.Nome , Spedizioni.* from Spedizioni inner join Clienti on Spedizioni.IdClienti=Clienti.IdClienti ", conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    Spedizioni p = new Spedizioni();
                    p.IdSpedizione = Convert.ToInt32(reader["IdSpedizione"]);
                    p.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                    p.Peso = Convert.ToDouble(reader["Peso"]);
                    p.Destinazione = reader["Destinazione"].ToString();
                    p.IndirizzoDest = reader["IndirizzoDest"].ToString();
                    p.CostoSpedizione = Convert.ToDouble(reader["CostoSpedizione"]);
                    p.DataConsegna = Convert.ToDateTime(reader["DataConsegna"]);
                    p.IdClienti = Convert.ToInt32(reader["IdClienti"]);
                    p.NomeDestinatario = reader["NomeDestinatario"].ToString();
                    p.Nome = reader["Nome"].ToString();



                    spedizioni.Add(p);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return spedizioni;
        }




        public static Spedizioni DettaglioSpedizioni(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            Spedizioni p = new Spedizioni();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"select Clienti.Nome,Spedizioni.* from Spedizioni inner join Clienti on Spedizioni.IdClienti=Clienti.IdClienti  where IdSpedizione={id}", conn);

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    p.IdSpedizione = Convert.ToInt32(reader["IdSpedizione"]);
                    p.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                    p.Peso = Convert.ToDouble(reader["Peso"]);
                    p.Destinazione = reader["Destinazione"].ToString();
                    p.IndirizzoDest = reader["IndirizzoDest"].ToString();
                    p.CostoSpedizione = Convert.ToDouble(reader["CostoSpedizione"]);
                    p.DataConsegna = Convert.ToDateTime(reader["DataConsegna"]);
                    p.IdClienti = Convert.ToInt32(reader["IdClienti"]);
                    p.NomeDestinatario = reader["NomeDestinatario"].ToString();
                    p.Nome = reader["Nome"].ToString();



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


        public static void ModificaSpedizione(Spedizioni p)
        {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"update Spedizioni set DataSpedizione = @DataSpedizione,Peso = @Peso,Destinazione = @Destinazione,IndirizzoDest = @IndirizzoDest,CostoSpedizione = @CostoSpedizione,DataConsegna = @DataConsegna, NomeDestinatario = @NomeDestinatario WHERE IdSpedizione = {p.IdSpedizione}", conn);


                cmd.Parameters.AddWithValue("DataSpedizione", p.DataSpedizione);
                cmd.Parameters.AddWithValue("Peso", p.Peso);
                cmd.Parameters.AddWithValue("Destinazione", p.Destinazione);
                cmd.Parameters.AddWithValue("IndirizzoDest", p.IndirizzoDest);
                cmd.Parameters.AddWithValue("CostoSpedizione", p.CostoSpedizione);
                cmd.Parameters.AddWithValue("DataConsegna", p.DataConsegna);
                cmd.Parameters.AddWithValue("NomeDestinatario", p.NomeDestinatario);



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
                SqlCommand cmd = new SqlCommand($"delete from Spedizioni where IdSpedizione={id}", conn);
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