using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using WebApplication2.Filters;

namespace WebApplication2.Controllers
{
    [Culture]
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public ActionResult CreateMenu(string userName, int id)
        {

            Site site;
            using (MyDbContext db = new MyDbContext())
            {
                site = db.Sites
                    .Include(s => s.Pages)
                    .Include(s => s.Menu)
                    .Include(s => s.Menu.Items)
                    .SingleOrDefault(p => p.SiteId == id);
            }
            if (site == null)
                site = new Site();
            return View("CreateMenu", site);
        }

        [HttpPost]
        public void SaveMenu(Menu saveMenu)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Menu menu = db.Menus.Include(c => c.Items).SingleOrDefault(p => p.Id == saveMenu.Id);
                menu.Items.Clear();
                saveMenu.Items.CopyItemsTo(menu.Items);
                db.SaveChanges();
            }
        }
    }
}
