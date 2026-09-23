using BanSach.DataAcess.Repository.IRepository;
using BanSach.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookShopWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            // chú ý kĩ mấy tên sau khi includeProperties phải giống với tên của các bảng trong database 
            // nếu không sẽ báo lỗi VD : includeProperties:"Category,coverType" => Category,coverType phải giống với model Product
            // VD : public CoverType coverType { get; set; } => coverType phải giống với tên bảng trong database
            IEnumerable<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category,coverType");
            return View(objProductList);
        }

        // Detail Action Method
        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,coverType")
            };
            return View(cartObj);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
