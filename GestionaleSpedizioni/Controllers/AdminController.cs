using GestionaleSpedizioni.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionaleSpedizioni.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie(".ASPXAUTH");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("Index","Home");
        }



        //Clienti
        public ActionResult CreaCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreaCliente([Bind(Exclude ="IdClienti")]Clienti c)
        {

            if(ModelState.IsValid)
            {
                TempData["mess"] = "Cliente Aggiunto";
            Clienti.AddClienti(c);

            return RedirectToAction("ListaClienti", "Admin");
            }
            ViewBag.messaggio = "Dati Incorretti";
            return View();
        }

        public ActionResult ListaClienti()
        {
            List<Clienti>c = new List<Clienti>();
            c=Clienti.ListClienti();
            return View(c);
        }

        public ActionResult ModificaCliente(int id)
        {
            Clienti c=new Clienti();
            c=Clienti.DettaglioClienti(id);
            return View(c);
        }


        [HttpPost]
        public ActionResult ModificaCliente(Clienti c)
        {
            if (ModelState.IsValid)
            {
                TempData["messaggio"] = "Modifica Cliente effettuata";

                Clienti.ModificaCliente(c);
                return RedirectToAction("ListaClienti");
            }
            ViewBag.messaggio = "Dati Incorretti";
            return View();
        }


        public ActionResult Elimina(int id)
        {
            Clienti.Delete(id);
            return RedirectToAction("ListaClienti");

        }


    }


}