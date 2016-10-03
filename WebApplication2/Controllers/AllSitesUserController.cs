using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AllSitesUserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /AllSiteUser/
        public ActionResult Index()
        {
            return View(db.Sites);
        }
	}
}