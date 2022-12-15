using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.BusinessLayer.Abstract;

namespace RealEstate.PresentationLayer.Areas.Default.Controllers
{
    [AllowAnonymous]
    [Area("Default")]
    public class AreasProductController : Controller
    {
        private readonly IProductService _productService;
        public AreasProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
