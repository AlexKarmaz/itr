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
    public class PageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        public ActionResult PageView(int id, int? pageId)
        {
            Page page;
            using (MyDbContext db = new MyDbContext())
            {
                page = db.Sites
                    .Include(s => s.Pages)
                    .SingleOrDefault(p => p.SiteId == id).Pages.FirstOrDefault(p => p.PageId == pageId);
            }
            return View("PageView", page);
        }

        [HttpPost]
        public void SavePage(Page savePage)
        {
            using (MyDbContext db = new MyDbContext())
            {
                db.Pages.SingleOrDefault(p => p.PageId == savePage.PageId).HtmlCode = savePage.HtmlCode;
                db.SaveChanges();
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return null;
        }

        [HttpPost, ActionName("DeletePage")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Page page = db.Pages.Find(id);
            page.PageId = 0;
          //  db.Pages.RemoveRange(site.Pages);
            db.Pages.Remove(page);
            db.SaveChanges();
            return null;
        }
    }
}
