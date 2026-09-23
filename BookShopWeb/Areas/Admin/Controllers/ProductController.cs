using BanSach.DataAcess.Data;
using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using BanSach.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll();
            return View(objProductList);
        }



        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM();
            productVM.product = new Product();

            // Kiểm tra xem dữ liệu có được lấy ra không
            var categories = _unitOfWork.Category.GetAll();
            var coverTypes = _unitOfWork.Covertype.GetAll();

            // Đổ dữ liệu vào SelectList
            productVM.CategoryList = categories.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });

            productVM.CoverTypeList = coverTypes.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            
            if (id == null || id == 0)
            {
                // Create product
                return View(productVM);
            }
            else
            {
                // update product : lấy dữ liệu từ database theo id rồi gán vào productVM
                // để hiển thị lên view khi click vào nút edit
                productVM.product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            }


            return View(productVM);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj, IFormFile file)
        {

            // Loại bỏ xác thực ImageUrl nếu file mới chưa được upload
            ModelState.Remove("product.ImageUrl");

            if (ModelState.IsValid)
            {
                //upload images
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null && file.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    
                    if (obj.product.ImageUrl != null)
                    {
                        // lấy đường dẫn ảnh cũ để xóa đi và thay thế bằng ảnh mới
                        var oldImagePath = Path.Combine(wwwRootPath, obj.product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    // lưu ảnh mới vào thư mục images/products 
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    // lưu đường dẫn ảnh vào database
                    obj.product.ImageUrl = @"\images\products\" + fileName + extension;
                }

                if (obj.product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Đã tạo hoặc cập nhật sản phẩm thành công";
                return RedirectToAction("index");
            }
            return View(obj);
        }

        

        #region API_CALLS
        // chuyển dữ liệu từ controller sang view bằng json
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,coverType");
            return Json(new { data = productList });
        }


        [HttpDelete, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                if (obj.ImageUrl != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    var oldImagePath = Path.Combine(wwwRootPath, obj.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                _unitOfWork.Product.Remove(obj);
                _unitOfWork.Save();
                return Json(new {sucess = true, message = "Đã sản phẩm xoá thành công"});
            }
            return View(obj);
        }
        #endregion
    }
}
