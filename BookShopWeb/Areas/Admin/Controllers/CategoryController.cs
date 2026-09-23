using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        // Inject qua constructor để sử dụng các phương thức của IUnitOfWork
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //var objCategoryList = _db.Categories.ToList();
            //return View();

            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisPlayOrder.ToString())
            {
                ModelState.AddModelError("name", "the name must not same displayorder");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category đã thêm thành công";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _db.Categories.Find(id);
            var categryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);

            if (categryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categryFromDbFirst);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisPlayOrder.ToString())
            {
                ModelState.AddModelError("name", "tên và thứ tự hiển thị không được giống nhau");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["warning"] = "Category đã cập nhật thành công";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categryFromDbFirst = _db.Categories.FirstOrDefault(u => u.id == id);
            //var categryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            //var categryFromDbFirst = _db.Categories.FirstOrDefault(u => u.id == id);
            //var categryFromDbSingle = _db.Categories.SingleOrDefault(u => u.id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Category.Remove(categoryFromDb);
                _unitOfWork.Save();
                TempData["error"] = "Category đã xoá thành công";
                return RedirectToAction("index");
            }
            return View(categoryFromDb);
        }

    }
}
