using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ProyectoCRUD.Models;
using System.Data.SqlClient;
using System.Data;
namespace ProyectoCRUD.Controllers
{
    public class ContactController : Controller
    {
        private static string conexion = ConfigurationManager.ConnectionStrings["cadena"].ToString();

        private static List<Contact> olista = new List<Contact>();

        // GET: Contact
        public ActionResult Inicio()
        {
            olista = new List<Contact>();

            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("Select * from CONTACT", oconexion);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact nuevoContacto = new Contact();

                        nuevoContacto.IdContact = Convert.ToInt32(dr["IdContact"]);
                        nuevoContacto.Names = dr["Names"].ToString();
                        nuevoContacto.surnames = dr["surnames"].ToString();
                        nuevoContacto.Phone = dr["Phone"].ToString();
                        nuevoContacto.Mail = dr["Mail"].ToString();

                        olista.Add(nuevoContacto);
                    }
                }
            }

            return View(olista);
        }




        public ActionResult Registrar()
        {
            return View();
        }


        public ActionResult Editar(int? idcontacto)
        {
            if (idcontacto == null)
                return RedirectToAction("Inicio","Contact");

            Contact ocontacto = olista.Where(c => c.IdContact == idcontacto).FirstOrDefault();

            return View(ocontacto);
        }




        [HttpPost]
        public ActionResult Registrar(Contact ocontacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Registrar", oconexion);
                cmd.Parameters.AddWithValue("Names", ocontacto.Names);
                cmd.Parameters.AddWithValue("surnames", ocontacto.surnames);
                cmd.Parameters.AddWithValue("Phone", ocontacto.Phone);
                cmd.Parameters.AddWithValue("Mail", ocontacto.Mail);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contact");
        }



        [HttpPost]
        public ActionResult Editar(Contact ocontacto)
        {
            using (SqlConnection oconexion = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("sp_editar", oconexion);
                cmd.Parameters.AddWithValue("IdContact", ocontacto.IdContact);
                cmd.Parameters.AddWithValue("Names", ocontacto.Names);
                cmd.Parameters.AddWithValue("surnames", ocontacto.surnames);
                cmd.Parameters.AddWithValue("Phone", ocontacto.Phone);
                cmd.Parameters.AddWithValue("Mail", ocontacto.Mail);
                cmd.CommandType = CommandType.StoredProcedure;
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Inicio", "Contact");
        }
    }
}