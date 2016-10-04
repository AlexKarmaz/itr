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

namespace WebApplication2.Controllers
{
    public class SitesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Sites/
        public ActionResult Index()
        {
            return View(db.Sites.ToList());
        }

        // GET: /Sites/Details/5
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
        public ActionResult Edit([Bind(Include="SiteId,Title,Description,CreationTime,UserId")] Site site)
        {
            if (ModelState.IsValid)
            {
                db.Entry(site).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(site);
        }

        // GET: /Sites/Delete/5
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

        // POST: /Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Site site = db.Sites.Find(id);
            db.Sites.Remove(site);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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

        public ActionResult CreatePage(string userName, int id, int? pageId)
        {
            if (pageId == null)
                throw new HttpException(HttpStatusCode.BadRequest.ToString());
            Site site;
            using (MyDbContext db = new MyDbContext())
            {
                site = db.Sites.Include(s => s.Pages).SingleOrDefault(p => p.SiteId == id);
            }
            if (site == null)
                throw new HttpException(HttpStatusCode.NotFound.ToString());

                Session.Add("TemplateId", site.TemplateId);
                return View("CreatePage", site.Pages.SingleOrDefault(p => p.PageId == pageId));          
        }

        public ActionResult LoadPage(string userName, int id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Site site = db.Sites.SingleOrDefault(p => p.SiteId == id);
                if (site != null)
                {
                    Page page = new Page();
                    site.Pages.Add(page);
                    db.SaveChanges();
                    return RedirectToAction("CreatePage", new { id = id, pageId = page.PageId });
                }
            }
            throw new HttpException(HttpStatusCode.InternalServerError.ToString());
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
