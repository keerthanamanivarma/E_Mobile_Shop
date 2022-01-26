using e_mobile_shopping.Models;
using e_mobile_shopping.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_mobile_shopping.Controllers
{
    public class ItemController : Controller
    {
        private EshopDBEntities objEShopDBEntities;
        public ItemController()
        {
            objEShopDBEntities = new EshopDBEntities();
        }
        // GET: Item
        public ActionResult Index()
        {
            ItemViewModel objItemViewModel = new ItemViewModel();
            objItemViewModel.CategorySelectListItem = (from objCat in objEShopDBEntities.Categories
                 select new SelectListItem()
                 {
                     Text = objCat.CategoryName,
                     Value = objCat.CategoryId.ToString(),
                     Selected = true

                 });
            return View(objItemViewModel);
        }

        [HttpPost]
        public JsonResult Index(ItemViewModel objItemViewModel)
        {
            string NewImage = Guid.NewGuid() + Path.GetExtension(objItemViewModel.ImagePath.FileName); //Guid will give Image name path ... is a extension
            objItemViewModel.ImagePath.SaveAs(Server.MapPath("~/Images/" + NewImage));

            Item objItem = new Item();
            objItem.ImagePath = "~/Images/" + NewImage;
            objItem.CategoryId = objItemViewModel.CategoryId;
            objItem.Description = objItemViewModel.Description;
            objItem.ItemCode = objItemViewModel.ItemCode;
            objItem.ItemId = Guid.NewGuid();
            objItem.ItemName = objItemViewModel.ItemName;
            objItem.ItemPrice = objItemViewModel.ItemPrice;
            objEShopDBEntities.Items.Add(objItem);
            objEShopDBEntities.SaveChanges();



            return Json(new { Success = true, Message = "Item is added Successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}