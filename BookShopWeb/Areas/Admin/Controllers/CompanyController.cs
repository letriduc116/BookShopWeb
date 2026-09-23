using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using BanSach.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Company> objCompanyList = _unitOfWork.Company.GetAll();
            return View(objCompanyList);
        }



        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null || id == 0)
            {
                // Create company
                return View(company);
            }
            else
            {
                // update company : lấy dữ liệu từ database theo id rồi gán vào company
                // để hiển thị lên view khi click vào nút edit
                company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);

            }

            return View(company);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                }
                else
                {
                    _unitOfWork.Company.Update(obj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Đã tạo hoặc cập nhật công ty thành công";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        

        #region API_CALLS
        // chuyển dữ liệu từ controller sang view bằng json
        [HttpGet]
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }


        [HttpDelete, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                 _unitOfWork.Company.Remove(obj);
                _unitOfWork.Save();
                return Json(new {sucess = true, message = "Đã công ty xoá thành công"});
            }
            return View(obj);
        }
        #endregion
    }
}
