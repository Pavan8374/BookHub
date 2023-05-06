using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM _shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork, ShoppingCartVM shoppingCartVM)
        {
            _unitOfWork = unitOfWork;
            _shoppingCartVM = shoppingCartVM;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
