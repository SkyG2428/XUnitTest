using Implementation.IRepository;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace UnitTestControllerWebApp.Controllers
{
    public class CategoryController : Controller
    {
        ICategoryRepository _catRepo;
        IRepository<Category> _categoryRepository;

        public CategoryController(ICategoryRepository catRepo , IRepository<Category> categoryRepository)
        {
            _catRepo = catRepo;
            _categoryRepository = categoryRepository;
            //_categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            var model = _catRepo.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryList = _catRepo.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _catRepo.Add(model);
                _catRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _catRepo.GetAll().ToList();
            return View();
        }

        public IActionResult Edit(int Id)
        {
            Category model = _catRepo.Find(Id);
            ViewBag.CategoryList = _catRepo.GetAll().ToList();
            return View("Create", model);
        }

        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Update(model);
                _catRepo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = _catRepo.GetAll().ToList();
            return View("Create", model);
        }

        public IActionResult Delete(int Id)
        {
            _catRepo.Delete(Id);
            _catRepo.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            var model = _catRepo.Find(Id);
            return View("Details",model);
        }
    }
}
