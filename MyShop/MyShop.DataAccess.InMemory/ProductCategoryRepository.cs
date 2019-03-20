using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories = new List<ProductCategory>();

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }

        public void commit()
        {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p)
        {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory ProductCategoryToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (ProductCategoryToUpdate != null)
            {
                ProductCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }
        }

        public ProductCategory Find(string ID)
        {
            ProductCategory productCategory = productCategories.Find(p => p.Id == ID);
            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string ID)
        {
            ProductCategory ProductCategoryToDelete = productCategories.Find(p => p.Id == ID);
            if (ProductCategoryToDelete != null)
            {
                productCategories.Remove(ProductCategoryToDelete);
            }
            else
            {
                throw new Exception("ProductCategory not found");
            }
        }
    }
}
