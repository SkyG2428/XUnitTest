using Implementation.IRepository;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestControllerWebApp.Controllers;



namespace TestCategoryController
{
    public class CategoryTestUnit
    {
        [Fact]
        public void IndexTestMethod()
        {
            // A: Arrange

            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>();
            
            //Mock<IRepository<Category>> catRepo = new Mock<IRepository<Category>>();

            CategoryController catCtrl = new CategoryController( categoryRepo.Object);

            Category c1 = new Category()
            {
                Id = 1,
                Name = "Casual Shirts"
            };
            Category c2 = new Category()
            {
                Id = 2,
                Name = "Grossery"
            };

            List<Category> categories = new List<Category>() { c1, c2};

            categoryRepo.Setup(repo => repo.GetCategories()).Returns(categories);

            // A : Action
            var result = catCtrl.Index() as ViewResult;

            // A : Assert
           Assert.Equal("Index", result.ViewName);

            var model = result.Model as List<Category>;
           
            CollectionAttribute.Equals(model, c1);
            CollectionAttribute.Equals(model, c2);

        }
    }
}
