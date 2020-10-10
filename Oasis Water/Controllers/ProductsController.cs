using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using BusinessLogic;
namespace Oasis_Water.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Products()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Products(Product modelClasses)
        {
            List<Product> products = new List<Product>();

            products.Add(modelClasses);

            genericProduct genericProduct = new genericProduct(modelClasses, "Insert");

            genericProduct.Insert();

            return RedirectToAction("DisplayProduct");
        }



        public ActionResult DisplayProduct()
        {
            genericProduct genericProduct = new genericProduct("Select");

            List<Product> productList = new List<Product>();


            productList = genericProduct.SelectAll();

            return View(productList);

        }

        public ActionResult Delete(int id)
        {
            genericProduct genericProduct = new genericProduct("Delete");


            genericProduct.Delete(id);

            return RedirectToAction("DisplayProduct");
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            List<Product> products = new List<Product>();

            products.Add(product);

            genericProduct genericProduct = new genericProduct(product, "Update");

            genericProduct.edit(productID);

            return RedirectToAction("DisplayProduct");

        }

        static int productID;
        static int curentQ;
        public ActionResult Edit(int id, int curntQ)
        {
            productID = id;
            return View();
        }

        static int prodid;
        static string productname;
        public ActionResult addSaleData(int id, int curntQ)
        {
            genericProduct genericProduct = new genericProduct("Select");
            prodid = id;
            curentQ = curntQ;

            List<Product> productList = new List<Product>();
            productList = genericProduct.SelectAll();
            Product product = new Product();

            foreach (var item in productList)
            {
                if(item.id==id)
                {
                    product = item;
                    productname = item.ProductName;
                }
            }


            return View(product);
        }

        [HttpPost]
        public ActionResult addSaleData(int Quantity)
        {
            genericProduct genericProduct = new genericProduct();

            List<Product> productList = new List<Product>();

            int q = curentQ - Quantity;
             genericProduct.UpdateQuantity(prodid, q);
            if(q<5)
            {

                NotificationsDisplay notifications = new NotificationsDisplay();
                notifications.CreateAnotification("Only " + q  + " products left for  " + productname, "Packeger");

            }
            return RedirectToAction("addSaleData", new { id = prodid, curntQ = curentQ });
        }

      static  int AProdID;
        static string name;
        static int intQ;

        public ActionResult addQuantity(int id, int curntQ)
        {
            genericProduct genericProduct = new genericProduct("Select");
            AProdID = id;
            intQ = curntQ;

            List<Product> productList = new List<Product>();
            productList = genericProduct.SelectAll();
            Product product = new Product();

            foreach (var item in productList)
            {
                if (item.id == id)
                {
                    product = item;
                    name = item.ProductName;
                }
            }


            return View(product);
        }
        [HttpPost]
        public ActionResult addQuantity(int Quantity)
        {
            genericProduct genericProduct = new genericProduct();

            List<Product> productList = new List<Product>();

            int q = intQ + Quantity;
            genericProduct.UpdateQuantity(prodid, q);
           
            return RedirectToAction("addSaleData", new { id = prodid, curntQ = curentQ });
        }
    }
}