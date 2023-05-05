using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList);
        }
        public IActionResult Upsert(int? id)
        {   
            ProductViewModel productViewModel = new()
            {
                CategoryList = _unitOfWork.Category
               .GetAll().Select((u => new SelectListItem
               {
                   Text = u.Name,
                   Value = u.Id.ToString()
               })),
                Product = new Product()
            };
            if(id==null || id == 0)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = _unitOfWork.Product.Get(u => u.Id==id);
                return View(productViewModel);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModel productViewModel, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productViewModel.Product.ImageURL))
                    {
                        var OldImagePath = Path.Combine(wwwRootPath, productViewModel.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(OldImagePath))
                        {
                            System.IO.File.Delete(OldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    } 
                    productViewModel.Product.ImageURL = @"\images\product\" + fileName;
                }
                if (productViewModel.Product.Id == 0)       
                {
                    _unitOfWork.Product.Add(productViewModel.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productViewModel.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product created suceessfully";
                return RedirectToAction("Index");
            }
            else
            {
                productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()

                });
            }
            return View();
        }    
               
        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if(productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while Deleteing" });
            }
            var OldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(OldImagePath))
            {
                System.IO.File.Delete(OldImagePath);
            }
            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();
           
            return Json(new { success = true, message = "Delete Successfull" });
        }
        #endregion
    }
}
