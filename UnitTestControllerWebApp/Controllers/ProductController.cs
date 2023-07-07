using Implementation.IRepository;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace UnitTestControllerWebApp.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Category> _categoryRepo;
        IProductRepository _productRepo;

        public ProductController(IRepository<Category> categoryRepo,
            IProductRepository productRepo)
        {
            _categoryRepo = categoryRepo;
            _productRepo = productRepo;
        }

        public IActionResult Index()
        {
            var data = _productRepo.GetProductWithCategories();
            return View("Index", data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _productRepo.Add(model);
                _productRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View();
        }

        public IActionResult Edit(int Id)
        {
            ProductModel model = _productRepo.Find(Id);
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                _productRepo.Update(model);
                _productRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _categoryRepo.GetAll().ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int Id)
        {
            _productRepo.Delete(Id);
            _productRepo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
