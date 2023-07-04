using EmaPosAdminProject.Models.Entity;
using EmaPosAdminProject.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmaPosAdminProject.Controllers.Admin
{
    public class SektorlerController : Controller
    {
        // GET: Sektorler
        EmaDbEntities db = new EmaDbEntities();
        [Authorize]
        public ActionResult SektorlerListesi()
        {
            SectorCategories model = new SectorCategories();
            model.SectortList = db.sector.ToList();
            model.SectorsCategoryList = db.sectorsCategory.ToList();
            return View(model);
        }
        [Authorize]
        [HttpGet]
        public ActionResult YeniSektor()
        {
            SectorCategories model = new SectorCategories();
            model.CategoryList = db.category.ToList();
            if (db.sector.Count() > 0)
            {
                ViewBag.orderNum = db.sector.Max(x => x.orderNumber).Value;
                ViewBag.orderNum++;
            }
            else
            {
                ViewBag.orderNum = 1;
            }
            ViewBag.category = db.category.ToList();
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniSektor(SectorCategories model)
        {
            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Sector/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                model.Sector.image = filename + extension;
            }
            //model.ProductImage.productId = model.Product.id;
            //model.Product.categoryId = id;
            db.sector.Add(model.Sector);
            //db.productImage.Add(model.ProductImage);
            db.SaveChanges();
            return RedirectToAction("SektorlerListesi");
        }
        [Authorize]
        public ActionResult SektorSil(int id)
        {
            var data = db.sector.Find(id);
            List<sectorsCategory> categoryList = db.sectorsCategory.Where(x => x.sectorId == id).ToList();
            foreach (var item in categoryList)
            {
                db.sectorsCategory.Remove(item);
            }
            db.sector.Remove(data);
            db.SaveChanges();
            return RedirectToAction("SektorlerListesi");
        }
        [Authorize]
        public ActionResult SektorGetir(int id)
        {
            SectorCategories model = new SectorCategories();
            model.Sector = db.sector.Find(id);
            return View("SektorGetir", model);
        }
        [Authorize]
        public ActionResult SektorGuncelle(SectorCategories sc)
        {
            if (Request.Files.Count > 0 && sc.Sector.image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(Request.Files[0].FileName);
                string extension = Path.GetExtension(Request.Files[0].FileName);
                string path = "~/Images/Sector/" + filename + extension + "";
                Request.Files[0].SaveAs(Server.MapPath(path));
                sc.Sector.image = filename + extension;
            }
            var data = db.sector.Find(sc.Sector.id);
            data.orderNumber = sc.Sector.orderNumber;
            data.title = sc.Sector.title;
            data.shortText = sc.Sector.shortText;
            data.text = sc.Sector.text;
            if (sc.Sector.image != null)
            {
                data.image = sc.Sector.image;
            }
            db.SaveChanges();
            return RedirectToAction("SektorlerListesi");
        }
        [Authorize]
        public ActionResult KategoriListesi(int id)
        {
            SectorCategories model = new SectorCategories();
            model.SectortList = db.sector.Where(x => x.id == id).ToList();
            model.SectorsCategoryList = db.sectorsCategory.Where(x => x.sectorId == id).ToList();
            model.Id = id;
            ViewBag.category = db.category.ToList();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult YeniKategori(int sectorId,int categoryId,sectorsCategory sc)
        {
            
            //model.SectorsCategory.sectorId = 3;
            //model.SectorsCategory.categoryId = categoryId;
            //db.sectorsCategory.Add(model.SectorsCategory);
            
            sc.sectorId = sectorId;
            sc.categoryId = categoryId;
            db.sectorsCategory.Add(sc);
            db.SaveChanges();
            return RedirectPermanent("/Sektorler/KategoriListesi/" + sectorId);
        }

        public ActionResult KategoriSil(int id)
        {
            var data = db.sectorsCategory.Find(id);
            int sectorID = (int)data.sectorId; 
            db.sectorsCategory.Remove(data);
            db.SaveChanges();
            return RedirectPermanent("/Sektorler/KategoriListesi/" + sectorID);
        }


    }
}