using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using Microsoft.AspNetCore.Mvc;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.Covertype.GetAll();
            return View(objCoverTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            

            if (ModelState.IsValid)
            {
                _unitOfWork.Covertype.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Covertype đã thêm thành công";
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
            var coverTypeFromDbFirst = _unitOfWork.Covertype.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.Covertype.Update(obj);
                _unitOfWork.Save();
                TempData["warning"] = "Covertype đã cập nhật thành công";
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

            var coverTypeFromDb = _unitOfWork.Covertype.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var coverTypeFromDb = _unitOfWork.Covertype.GetFirstOrDefault(u => u.Id == id);

            if (coverTypeFromDb == null)
            {
                return NotFound();
            }
            else
            {
                _unitOfWork.Covertype.Remove(coverTypeFromDb);
                _unitOfWork.Save();
                TempData["error"] = "Covertype đã xoá thành công";
                return RedirectToAction("index");
            }
            return View(coverTypeFromDb);
        }

    }
}
