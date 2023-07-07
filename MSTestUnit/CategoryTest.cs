using Implementation.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using UnitTestControllerWebApp.Controllers;

namespace MSTestUnit
{
    [TestClass]
    public class CategoryTest
    {
        Category c1;
        Category c2;
        Mock<ICategoryRepository> categoryRepo;
        Mock<IRepository<Category>> CategoryIRepository;
        List<Category> categories;
        CategoryController catCtrl;
        Category cat;
        public CategoryTest()
        {
            c1 = new Category()
            {
                Id = 1,
                Name = "Casual Shirts"
            };
            c2 = new Category()
            {
                Id = 2,
                Name = "Grossery"
            };

            categories = new List<Category>() { c1, c2 };
            categoryRepo = new Mock<ICategoryRepository>();
            CategoryIRepository = new Mock<IRepository<Category>>();
            catCtrl = new CategoryController(categoryRepo.Object, CategoryIRepository.Object);
            cat = new Category { Id = 3, Name = "Test" };

        }

        [TestMethod]
        public void IndexTest()
        {
            //Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>();

            //Mock<IRepository<Category>> catRepo = new Mock<IRepository<Category>>(); 

            //CategoryController catCtrl = new CategoryController(categoryRepo.Object);



            //List<Category> categories = new List<Category>() { c1, c2 };

            //categoryRepo.Setup(d => d.ToList()).Returns(categories);

            categoryRepo.Setup(repo => repo.GetAll()).Returns(categories);

            // A : Action
            var result = catCtrl.Index() as ViewResult;
            var model = result.Model as List<Category>;
            // A : Assert
            //Assert.AreEqual("Index", result.ViewName);

            

            CollectionAssert.Contains(model, c1); 
            CollectionAssert.Contains(model, c2);
        }


        [TestMethod]
        public void GetCreateTest()
        {
            // Setup
            categoryRepo.Setup(categoryRepo => categoryRepo.GetAll()).Returns(categories);
            //Action
            var result = catCtrl.Create() as ViewResult;
            var viewData = result.ViewData["CategoryList"] as List<Category>;

            //Assert
            CollectionAssert.Contains(viewData, c1);
            CollectionAssert.Contains(viewData, c2);
        }

        [TestMethod]
        public void PostCreateErrorTest()
        {
            //Setup
            categoryRepo.Setup(categoryRepo => categoryRepo.GetAll()).Returns(categories);

            cat.Name = null;
            
                catCtrl.ModelState.AddModelError("Name", "Please Enter Name");
            
            //Action
            var result = catCtrl.Create(cat) as ViewResult;
            var viewData = result.ViewData["CategoryList"] as List<Category>;

            //Assert
            CollectionAssert.Contains(viewData, c1);
            CollectionAssert.Contains(viewData, c2);
        }

        [TestMethod]
        public void PostCreateSuccess()
        {
            CategoryIRepository.Setup(repo => repo.Add(cat));
            //Action
            var result = catCtrl.Create(cat) as RedirectToActionResult;
            //Assert
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void GetEditTest()
        {
            int id = 3;
            // setup
            categoryRepo.Setup(repo => repo.GetAll()).Returns(categories);
            categoryRepo.Setup(repo => repo.Find(id)).Returns(cat);

            var result = catCtrl.Edit(id) as ViewResult;
            var viewData = result.ViewData["CategoryList"] as List <Category>;
            var model = result.Model as Category;

            //Assert
            CollectionAssert.Contains(viewData, c1);
            CollectionAssert.Contains(viewData, c2);

            Assert.AreEqual("Create", result.ViewName);
            Assert.AreEqual(cat, model);
           
        
        }

        [TestMethod]
        public void PostTestSuccess()
        {
            cat.Name = "Sport Shoes";
            // Setup
            categoryRepo.Setup(categoryRepo => categoryRepo.Update(cat));
            //Action
            var result = catCtrl.Edit(cat) as RedirectToActionResult;
            //Assert
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void TestDelete()
        {
            int id = 1;
            //Setup
            categoryRepo.Setup(repo => repo.Delete(id));
            //Action
            var result = catCtrl.Delete(id) as RedirectToActionResult;
            //Assert
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void TestDetails()
        {
            int id = 1;
            //setup
            categoryRepo.Setup(repo => repo.Find(id));
            //Action
            var result = catCtrl.Details(id) as ViewResult;
            //Assert
            Assert.AreEqual("Details", result.ViewName);
        }

    }
}
