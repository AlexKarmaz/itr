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
    public class SitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        
        public ActionResult Index()
        {
            var UserId = User.Identity.GetUserId();
            return View(db.Sites.Where(s=>s.UserId==UserId).ToList());
        }
                
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Site site)
        {
            site.CreationTime = DateTime.Now;
            site.UserId = User.Identity.GetUserId();
            site.Menu = new Menu() { IsTopBar = site.MenuId == 1 };
            site.MenuId = 0;            
            db.Sites.Add(site);
            db.SaveChanges();
            return Json(new { result = "Redirect", url = Url.Action("Details", "Sites", new { id = site.SiteId }) });

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Site site)
        {
            if (ModelState.IsValid)
            {
                db.Entry(site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = db.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        { 
            Site site = db.Sites.Find(id);
            site.Menu = null;
            db.Pages.RemoveRange(site.Pages);
            db.Sites.Remove(site);            
            db.SaveChanges();
            return RedirectToAction("Index");
       }           
        
        [HttpPost]
        public ActionResult LoadTemplate(string id)
        {
            return PartialView("template/" + id);
        }

        [HttpPost]
        public ActionResult LoadMenuEditor()
        {
            return PartialView("template/MenuEditor");
        }

        [AllowAnonymous]
        public ActionResult Site(int id, int? pageId)
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
            Page page = pageId != null ? site.Pages.FirstOrDefault(s => s.PageId == pageId) : null;
            if (page == null)
                page = site.Pages.Count > 0 ? site.Pages.FirstOrDefault() : new Page();
            page.Site = site; 
            if(page.Site.Menu == null)
            {
                page.Site.Menu = new Menu();
            }

            if (page.Site.Menu.Items == null)
            {
                page.Site.Menu.Items = new List<MenuItem>();
            }
            var UserId = User.Identity.GetUserId();
            ViewBag.IsAutor = site.UserId == UserId && UserId != null;
            return View("Site", page);
      }

        [AllowAnonymous]
        public ActionResult ChangeCulture(string lang)
        {
            List<string> cultures = new List<string>() { "ru", "en" };
            lang = !cultures.Contains(lang) ? "ru" : lang;
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {
                cookie = new HttpCookie("lang", lang);
                cookie.HttpOnly = false;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
