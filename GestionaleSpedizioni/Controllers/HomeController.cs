using GestionaleSpedizioni.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestionaleSpedizioni.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Exclude ="IdUser")]Admin a)
        {
                if (ModelState.IsValid)
                {
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connection);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("Select * from Users WHERE Username=@Username And Password=@Password", conn);
                cmd.Parameters.AddWithValue("Username", a.Username);
                cmd.Parameters.AddWithValue("Password", a.Password);


                SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {

                        FormsAuthentication.SetAuthCookie(a.Username, false);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ViewBag.messaggio = "Utente non abilitato";
                        return View();

                    }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
                }

            return View();


        }

        public ActionResult CercaSpedizione()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CercaSpedizione([Bind(Exclude ="IdSpedizione")]CercaSp c, string CodiceFiscaleOPartitaIva, int IdSpedizione)
        {
            if (ModelState.IsValid)
            { 
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Spedizioni inner JOIN Clienti  ON Spedizioni.IdClienti = Clienti.IdClienti  WHERE Clienti.CodiceFiscaleOPartitaIva = @CodiceFiscaleOPartitaIva  AND Spedizioni.IdSpedizione = @IdSpedizione", conn);
                cmd.Parameters.AddWithValue("CodiceFiscaleOPartitaIva", CodiceFiscaleOPartitaIva);
                cmd.Parameters.AddWithValue("IdSpedizione", IdSpedizione);


                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    c.CodiceFiscaleOPartitaIva = reader["CodiceFiscaleOPartitaIva"].ToString();
                    c.IdSpedizione = Convert.ToInt32(reader["IdSpedizione"]);

                   

                    if (c.CodiceFiscaleOPartitaIva == CodiceFiscaleOPartitaIva && c.IdSpedizione == IdSpedizione)
                    {
                        return RedirectToAction("RisultatoSped", new { id = c.IdSpedizione });
                    }
                    
                    else
                    {
                        return View();
                    }

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            }

            return View();
        }

        public ActionResult RisultatoSped(int id)
        {

            Spedizioni s = new Spedizioni();
            s = Spedizioni.DettaglioSpedizioni(id);
            return View(s);
        }


        public ActionResult GetPartialHome(int id)
        {
           
                List<AggiornamentoSpedizione> aggiornamento = new List<AggiornamentoSpedizione>();
                string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connection);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand($"select * from AggiornamentiSpedizione where IdSpedizione={id} order by AggiornamentiSpedizione.IdAggiornamento desc", conn);
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
               
            return PartialView("GetPartialHome", aggiornamento);
        }
    

    }
}