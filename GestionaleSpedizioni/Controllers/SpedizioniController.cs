using GestionaleSpedizioni.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace GestionaleSpedizioni.Controllers
{

    
    [Authorize(Roles ="Admin")]
    public class SpedizioniController : Controller
    {

        public List<SelectListItem> SelectList {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                SelectListItem item = new SelectListItem { Text = "Seleziona", Value = "0" };
                SelectListItem item2 = new SelectListItem { Text = "In Preparazione", Value = "In Preparazione" };
                SelectListItem item3 = new SelectListItem { Text = "In Transito", Value = "In Transito" };
                SelectListItem item4 = new SelectListItem { Text = "In Consegna", Value = "In Consegna" };
                SelectListItem item5 = new SelectListItem { Text = "Consegnato", Value = "Consegnato" };
                list.Add(item);
                list.Add(item2);
                list.Add(item3);
                list.Add(item4);
                list.Add(item5);
                return list;
            }
                }
        // GET: Spedizioni
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaSpedizioni()
        {
            List<Spedizioni> c = new List<Spedizioni>();
            c = Spedizioni.ListSpedizioni();
            return View(c);
        }

        public ActionResult CreaSpedizione()
        {
            //Clienti
            List<Clienti>listacli=Clienti.ListClienti();
            List<SelectListItem>listItem =new List<SelectListItem>();
            SelectListItem Item2 = new SelectListItem { Text = "Seleziona", Value = "0" };
            listItem.Add(Item2);
            foreach (Clienti c in listacli)
            {
                SelectListItem Item = new SelectListItem { Text =c.Nome, Value=c.IdClienti.ToString()};
                listItem.Add(Item);
            }
            ViewBag.clienti=listItem;

            return View();
        }

        [HttpPost]
        public ActionResult CreaSpedizione([Bind(Exclude = "IdSpedizione")] Spedizioni c ,int ListItem)
        {
          

            

            if (ModelState.IsValid)
            {

                c.IdClienti = ListItem;
                Spedizioni.AddSpedizione(c);
                return RedirectToAction("ListaSpedizioni", "Spedizioni");
            }
            return View();
        }


        public ActionResult DettagliSpe(int id)
        {
            Spedizioni spe = new Spedizioni();
            spe= Spedizioni.DettaglioSpedizioni(id);
            return View(spe);
        }


        public ActionResult ModificaSpe(int id)
        {
            Spedizioni s = new Spedizioni();
            s = Spedizioni.DettaglioSpedizioni(id);
            return View(s);
        }

        [HttpPost]
        public ActionResult ModificaSpe(Spedizioni s)
        {
            if(ModelState.IsValid)
            {
                Spedizioni.ModificaSpedizione(s);
                return RedirectToAction("ListaSpedizioni");
            }
            return View();
        }


        public ActionResult Delete(int id)
        {
            Spedizioni.Delete(id);
            return RedirectToAction("ListaSpedizioni");
        }



        public ActionResult GetPartial(int id)
        {
            AggiornamentoSpedizione a = new AggiornamentoSpedizione();
            a = AggiornamentoSpedizione.DettaglioAggiornamento(id);
            return PartialView("GetPartial", a);
        }

        public ActionResult AggiornaStato()
        {
            ViewBag.listaSp = SelectList;
            return View();
        }

        [HttpPost]
        public ActionResult AggiornaStato(AggiornamentoSpedizione a,string SelectList,int id)
        {
            a.Stato = SelectList;
            AggiornamentoSpedizione.AddSpedizione(a,id);
            return RedirectToAction("ListaSpedizioni", "Spedizioni");
        }



        //Query

        public JsonResult ConsegnaOggi()
        {
            
                List<Spedizioni> consegnaOggi = new List<Spedizioni>();
                string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connection);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Spedizioni where DataConsegna=@DataConsegna", conn);
                    
                cmd.Parameters.AddWithValue("DataConsegna", DateTime.Today);
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



                       consegnaOggi.Add(p);
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    conn.Close();
                }
                return Json(consegnaOggi,JsonRequestBehavior.AllowGet);
            }


        public JsonResult InConsegna()
        {

            List<Spedizioni> inConsegna = new List<Spedizioni>();
            int tot = 0;
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select count(distinct Spedizioni.IdSpedizione) as tot from Spedizioni inner join AggiornamentiSpedizione on Spedizioni.IdSpedizione=AggiornamentiSpedizione.IdSpedizione where AggiornamentiSpedizione.IdSpedizione not in (Select AggiornamentiSpedizione.IdSpedizione from AggiornamentiSpedizione where stato like 'Consegnato' )", conn);
                cmd.Parameters.AddWithValue("DataConsegna", DateTime.Today);

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                     tot = Convert.ToInt32(reader["tot"]);

                    
                   
                    //p.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                    //p.Peso = Convert.ToDouble(reader["Peso"]);
                    //p.Destinazione = reader["Destinazione"].ToString();
                    //p.IndirizzoDest = reader["IndirizzoDest"].ToString();
                    //p.CostoSpedizione = Convert.ToDouble(reader["CostoSpedizione"]);
                    //p.DataConsegna = Convert.ToDateTime(reader["DataConsegna"]);
                    //p.IdClienti = Convert.ToInt32(reader["IdClienti"]);
                    //p.NomeDestinatario = reader["NomeDestinatario"].ToString();



                   
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return Json(tot, JsonRequestBehavior.AllowGet);
        }


        public JsonResult Destinazione()
        {

            List<Spedizioni> destinazione = new List<Spedizioni>();
            string connection = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connection);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select Destinazione, COUNT(*) tot FROM Spedizioni GROUP BY Destinazione", conn);

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                   Spedizioni a = new Spedizioni();
                    
                    a.Destinazione = reader["Destinazione"].ToString();
                    a.Tot = Convert.ToInt32(reader["tot"]);

                    destinazione.Add(a);

                    //p.DataSpedizione = Convert.ToDateTime(reader["DataSpedizione"]);
                    //p.Peso = Convert.ToDouble(reader["Peso"]);
                    //p.IndirizzoDest = reader["IndirizzoDest"].ToString();
                    //p.CostoSpedizione = Convert.ToDouble(reader["CostoSpedizione"]);
                    //p.DataConsegna = Convert.ToDateTime(reader["DataConsegna"]);
                    //p.IdClienti = Convert.ToInt32(reader["IdClienti"]);
                    //p.NomeDestinatario = reader["NomeDestinatario"].ToString();




                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return Json(destinazione, JsonRequestBehavior.AllowGet);
        }
    }
}