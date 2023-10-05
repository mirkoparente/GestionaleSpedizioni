using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GestionaleSpedizioni.Models
{
    public class Admin
    {
        public int IdUser { get; set; }


        [Required(ErrorMessage ="Inserisci Username")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Inserisci Password")]
        public string Password { get; set; }
        public string Ruolo { get; set; }


    }
}