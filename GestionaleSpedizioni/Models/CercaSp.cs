using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GestionaleSpedizioni.Models
{
    public class CercaSp
    {
        
        [Display(Name = "Inserisci Codice Fiscale o Partita Iva")]
        [Required(ErrorMessage ="Inserisci Campo")]
        public string CodiceFiscaleOPartitaIva { get; set; }



        [Display(Name = "Numero spedizione")]
        [Required(ErrorMessage ="Inserisci Campo")]
        public int IdSpedizione { get; set; }

        public Spedizioni Spedizione { get; set; }

        public Clienti Clienti { get; set; }


        public static List<CercaSp> Cerca()
        {

            List<CercaSp> cercasp = new List<CercaSp>();
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Spedizioni inner JOIN Clienti  ON Spedizioni.IdClienti = Clienti.IdClienti  WHERE Clienti.CodiceFiscaleOPartitaIva = @CodiceFiscaleOPartitaIva  AND Spedizioni.IdSpedizione = @IdSpedizione", conn);
                CercaSp p= new CercaSp();
                cmd.Parameters.AddWithValue("CodiceFiscaleOPartitaIva", p.CodiceFiscaleOPartitaIva);
                cmd.Parameters.AddWithValue("IdSpedizione",p.IdSpedizione);
                

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    p.CodiceFiscaleOPartitaIva = reader["CodiceFiscaleOPartitaIva"].ToString();
                    p.IdSpedizione = Convert.ToInt32(reader["IdSpedizione"]);
                    p.Spedizione.DataConsegna = Convert.ToDateTime(reader["DataConsegna"]);
                    p.Spedizione.Peso = Convert.ToDouble(reader["Peso"]);
                    p.Spedizione.CostoSpedizione = Convert.ToDouble(reader["CostoSpedizione"]);
                    p.Spedizione.NomeDestinatario = reader["NomeDestinatario"].ToString();
                    p.Spedizione.Destinazione = reader["Destinazione"].ToString();
                    p.Spedizione.IndirizzoDest = reader["IndirizzoDest"].ToString();
                    p.Spedizione.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                    p.Clienti.Nome = reader["Nome"].ToString();
                   



                    cercasp.Add(p);
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return cercasp;
        }

    }
}
