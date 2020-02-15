using CZBK.NewsManagement.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace UI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Model.User User = Session["LoginUser"] as Model.User;

            if (User == null)
            {

                return Redirect("/Login/CheckUser");
            }

            
            if (User.State != 1)
            {
                return Redirect("/Home/Error");
            }
            

            return View();
        }
        public IBLL.IFoodOrderService FoodOrderService { get; set; }
        public ActionResult GetOrder()
        {
            int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);
            int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);

            int total;
            List<Model.FoodOrder> list = FoodOrderService.LoadPageEntities(pageSize, pageIndex, out total, m => true, m => m.CreateTime, false).ToList();


            List<UI.Models.ViewModels> ToList = new List<Models.ViewModels>();


            foreach (var item in list)
            {
                if (item.ISPlay==0)
                {
                    ToList.Add(new Models.ViewModels()
                    {
                        Id = item.Id,
                        CreateTime = (DateTime)item.CreateTime,
                        Address = item.address,
                        FoodID = item.FoodID,
                        FoodNumber = item.FoodNumber,
                        Img = item.Img,
                        ISPlay = "Paid",
                        MenuName = GetMenuName(item.FoodID),
                        Money = item.Money,
                        UserID = item.UserID,
                        UserName = GetUserName(item.UserID)


                    });
                }
                else if (item.ISPlay==1)
                {
                    ToList.Add(new Models.ViewModels()
                    {
                        Id = item.Id,
                        CreateTime = (DateTime)item.CreateTime,
                        Address = item.address,
                        FoodID = item.FoodID,
                        FoodNumber = item.FoodNumber,
                        Img = item.Img,
                        ISPlay = "Unpaid",
                        MenuName = GetMenuName(item.FoodID),
                        Money = item.Money,
                        UserID = item.UserID,
                        UserName = GetUserName(item.UserID)


                    });
                }
            }




            string navStrHmtl = LaomaPager.ShowPageNavigate(pageSize, pageIndex, FoodOrderService.LoadEntities(m => true).ToList().Count());


            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var data = new { NavStr = navStrHmtl, DataRows = ToList };

            string temp = javaScriptSerializer.Serialize(data);

            return Content(temp);



        }
        public IBLL.IUserService UserService { get; set; }
        public string GetUserName(int userid)
        {
            return UserService.LoadEntities(U => U.Id == userid).FirstOrDefault().UserName;


        }

        public IBLL.IMenuService MenuService { get; set; }
        public string GetMenuName(int id)
        {
            return MenuService.LoadEntities(U => U.Id == id).FirstOrDefault().Name;


        }

        public ActionResult GetUserData()
        {
            return View();

        }

        public ActionResult LoadUserList()
        {
            int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);
            int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);

            int total;
            List<Model.User> list = UserService.LoadPageEntities(pageSize, pageIndex, out total, m => true, m => m.Id, true).ToList();


            string navStrHmtl = LaomaPager.ShowPageNavigate(pageSize, pageIndex, UserService.LoadEntities(m => true).ToList().Count());


            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var data = new { NavStr = navStrHmtl, DataRows = list };

            string temp = javaScriptSerializer.Serialize(data);

            return Content(temp);



        }

        [HttpGet]
        public ActionResult UserEdit(int id)
        {
            Model.User UserModel = UserService.LoadEntities(m => m.Id == id).ToList()[0];

            ViewData.Model = UserModel;
            ///MenuService.Savechanges();

            return View();


        }
        [HttpPost]
        public ActionResult UserEdit(Model.User UserInfo)
        {
            if (string.IsNullOrEmpty(UserInfo.UserName))
            {
                return Content("<script type='text/javascript'>alert('Account cannot be modified!');</script>");
            }

            UserService.Update(UserInfo);

            
            return Content("ok");


        }

        public ActionResult DeleteUser(int id)
        {

            UserService.Delete(id);

            UserService.Savechanges();

            
            return Content("ok");
            
            

        }



    }
}