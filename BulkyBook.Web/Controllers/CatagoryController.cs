using BulkyBook.Web.Data;
using BulkyBook.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBook.Web.Controllers
{
    public class CatagoryController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CatagoryController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Catagory> objCatagoryList = _db.Catagories.ToList();
            return View(objCatagoryList);
        }


        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Catagory obj) {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name","The DisplayOrder cannot exactly match the Name");
            
            }
            if (ModelState.IsValid)
            {
                _db.Catagories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
            
            
        }

        public IActionResult Edit(int? id)
            //show existing catagory whle loading page
        {
            if (id == null || id ==0)
            {
                return NotFound();
            }

            var catagoryFromDb = _db.Catagories.Find(id);

            if (catagoryFromDb == null) {
                return NotFound();
            }


            return View(catagoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Catagory obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name");

            }
            if (ModelState.IsValid)
            {
                _db.Catagories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);


        }

        public IActionResult Delete(int? id)
        //show existing catagory whle loading page
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var catagoryFromDb = _db.Catagories.Find(id);

            if (catagoryFromDb == null)
            {
                return NotFound();
            }


            return View(catagoryFromDb);
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
           var obj = _db.Catagories.Find(id);
            if (obj == null) {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Catagories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
            return View(obj);


        }
    }
}
