using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            else
            {
                context.Insert(productCategory);
                context.commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string ID)
        {
            ProductCategory productCategory = context.Find(ID);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string ID)
        {
            ProductCategory ProductCategoryToEdit = context.Find(ID);

            if (ProductCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategory);
                }

                ProductCategoryToEdit.Category = productCategory.Category;
                

                context.commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string ID)
        {
            ProductCategory ProductCategoryToDelete = context.Find(ID);

            if (ProductCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {
            ProductCategory ProductCategoryToDelete = context.Find(ID);

            if (ProductCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(ID);
                context.commit();
                return RedirectToAction("Index");
            }
        }
    }
}