using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using smileRed.Backend.Models;
using smileRed.Domain;
using smileRed.Backend.Helpers;

namespace smileRed.Backend.Controllers
{
    public class GroupsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        //[Authorize(Roles = "Admin")]
        [Authorize(Users = "filintomeireles@gmail.com, meireles596@hotmail.com")]

        // GET: Groups
        public async Task<ActionResult> Index()
        {
            return View(await db.Groups.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GroupsView view)
        {
            string description = Convert.ToString(Request["Description"]);
            var existC = db.Groups.Where(c => c.Description == description).FirstOrDefault();

            if (existC != null)
            {
                ViewBag.Error = "The category already exists!";
                return View();
            }

            try
            {
                if (ModelState.IsValid)
                {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }
             
                var group = ToGroup(view);
                group.Image = pic;               
                db.Groups.Add(group);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error:" + ex.Message;
                ViewBag.Message = "The category must be unique";
                return View();
            }

            return View(view);
        }

        private Group ToGroup(GroupsView view)
        {
            return new Group
            {
                CategoryId = view.CategoryId,
                Description = view.Description,
                Image = view.Image,
                //IsActive = view.IsActive,
            };
        }

        // GET: Groups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            var view = ToView(group);
            return View(view);
        }

        private GroupsView ToView(Group group)
        {
            return new GroupsView
            {
                CategoryId = group.CategoryId,
                Description = group.Description,
                Image = group.Image,
                //IsActive = group.IsActive,
            };
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GroupsView view)
        {
           using (var transaction = db.Database.BeginTransaction())
           {
                try
            {
                if (ModelState.IsValid)
                {
                var pic = string.Empty;
                var folder = "~/Content/Images";

                if (view.ImageFile != null)
                {
                    pic = FilesHelper.UploadPhoto(view.ImageFile, folder);
                    pic = string.Format("{0}/{1}", folder, pic);
                }

                var group = ToGroup(view);
                group.Image = pic;
                db.Entry(group).State = EntityState.Modified;
                await db.SaveChangesAsync();
                transaction.Commit();
                return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ViewBag.Error = "Error:" + ex.Message;
                ViewBag.Message = "The category must be unique";
                return View();
             }
            }
            return View(view);
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Group group = await db.Groups.FindAsync(id);
            db.Groups.Remove(group);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
