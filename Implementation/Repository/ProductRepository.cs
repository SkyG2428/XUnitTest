using DAL;
using Implementation.IRepository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Repository
{
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext db) : base(db)
        {

        }
        public IEnumerable<ProductModel> GetProductWithCategories()
        {
            #region NotUse
            //var data = (from prd in _db.Products
            //            join cat in _db.Categories
            //            on prd.CategoryId equals cat.Id
            //            select new
            //            {
            //                prd.Id,
            //                prd.Name,
            //                prd.Description,
            //                prd.UnitPrice,
            //                Category = cat.Name
            //            }).ToList();

            //IList<ProductModel> model = new List<ProductModel>();
            //foreach (var item in data)
            //{
            //    ProductModel prd = new ProductModel
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //        Description = item.Description,
            //        UnitPrice = item.UnitPrice,
            //        Category = item.Category
            //    };
            //    model.Add(prd);
            //}
            #endregion NotUse

            List<ProductModel> model = new List<ProductModel>();

            model = (from p in _db.Products
                     join c in _db.Categories
                     on p.CategoryId equals c.Id
                     select new ProductModel()
                     {
                         Id = p.Id,
                         Name = p.Name,
                         Description = p.Description,
                         UnitPrice = p.UnitPrice,
                         CategoryId = p.CategoryId,
                         Category = c.Name,
                         
                     }).ToList();

            return model;
        }
    }
}
