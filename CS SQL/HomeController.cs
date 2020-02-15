using CZBK.NewsManagement.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        IBLL.IMenuService MenuService { get; set; }
        IBLL.IFoodOrderService FoodOrderService { get; set; }
        public ActionResult Index()
        {
            int State = (int)UI.Models.StateEnum.Normal;


           List<Model.Menu> list=  MenuService.LoadEntities(m => m.State==State).ToList();

            ViewData["list"] = list;


            return View();
        }
        public ActionResult Single(int id)
        {
            Model.Menu MenuModel = MenuService.LoadEntities(m => m.Id == id).ToList()[0];

            ViewData.Model = MenuModel;


            return View();
        }


        public ActionResult AddMenu()
        {
            if (Session["LoginUser"] == null)
            {

                return Redirect("/Login/CheckUser");
            }

            Model.User User = Session["LoginUser"] as Model.User;

            if (User.State != 1)
            {
                return Redirect("/Home/Error");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddMenu(string MenuName,string MenuPrice,string DetailedInformation,int Inventory)
        {
            Model.Menu Model = new Model.Menu();
            Model.Name = MenuName;
            Model.Price =Decimal.Parse( MenuPrice);
            Model.Number = Inventory;
            Model.DetailedInformation = DetailedInformation;
            Model.State = 0;
            if (Request.Files["MenuImg"] != null)
            {
                var requestFile = Request.Files["MenuImg"];
                string imagePath = "/Upload/Images/";
                string fileName = imagePath + Guid.NewGuid().ToString() + requestFile.FileName;

                requestFile.SaveAs(Server.MapPath(fileName));

                /// return Content(fileName);
                /// 
                Model.Img = fileName;


            }

            MenuService.Add(Model);

            return Content("<script type='text/javascript'>alert('Add OK!');window.location.href='/Home/GetMenu';</script>");

            //if (MenuService>0)
            //{
                
            //}
            //else
            //{
            //    return Content("<script type='text/javascript'>alert('Add NO!');</script>");
            //}



        }
        public ActionResult Error()
        {

            return View();

        }

        public ActionResult GetMenu()
        {
            if (Session["LoginUser"] ==null)
            {
                
                return Redirect("/Login/CheckUser");
            }

            Model.User User = Session["LoginUser"] as Model.User;

            if (User.State!=1)
            {
                return Redirect("/Home/Error");
            }



            List<Model.Menu> list = MenuService.LoadEntities(m => true).ToList();

            ViewData["list"] = list;
            
            return View();
        }

        public ActionResult LoadMenuList()
        {
            int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);
            int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);

            int total;
            List<Model.Menu> list = MenuService.LoadPageEntities(pageSize,pageIndex,  out total, m => true, m => m.Id, true).ToList();
            
            
            string navStrHmtl = LaomaPager.ShowPageNavigate(pageSize, pageIndex, MenuService.LoadEntities(m=>true).ToList().Count());

            
            System.Web.Script.Serialization.JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var data = new { NavStr = navStrHmtl, DataRows = list };
         
            string temp = javaScriptSerializer.Serialize(data);

            return Content(temp);
        }

        public ActionResult MenuEdit(int id)
        {
            Model.Menu MenuModel = MenuService.LoadEntities(m => m.Id == id).ToList()[0];

            ViewData.Model = MenuModel;
            ///MenuService.Savechanges();
            
            return View();


        }
        [HttpPost]
        public ActionResult MenuEdit(string MenuName,string MenuPrice,int Menuid,string DetailedInformation,int State,int Inventory)
        {
           
            Model.Menu Model = new Model.Menu();
            Model.Name = MenuName;
            Model.Price = Decimal.Parse(MenuPrice);
            Model.Number = Inventory;
            Model.Id = Menuid;
            Model.DetailedInformation = DetailedInformation;
            Model.State = State;

            if (Request.Files["MenuImg"] != null)
            {
                var requestFile = Request.Files["MenuImg"];
                string imagePath = "/Upload/Images/";
                string fileName = imagePath + Guid.NewGuid().ToString() + requestFile.FileName;

                requestFile.SaveAs(Server.MapPath(fileName));

                /// return Content(fileName);
                /// 
                Model.Img = fileName;


            }
            else
            {
                ///Model.Img = "";
            }
            MenuService.Update(Model);

            MenuService.Savechanges();
           
                return Content("<script type='text/javascript'>alert('Edit OK!');afterEditSuccess();$('#editDiv').dialog('close');initTableList();</script>");
            //}
            //else
            //{
            //    return Content("<script type='text/javascript'>alert('Edit NO!');</script>");
            //}

          

        }

        
        public ActionResult DeleteMenu(int id)
        {
            if(MenuService.Delete(id)>0)
            {
                return Content("ok");
            }
            else
            {
                return Content("<script type='text/javascript'>alert('Delete No!');</script>");
            }



        }

        public ActionResult GetUserOrder()
        {
            if (Session["LoginUser"] ==null)
            {
                return Redirect("/Login/CheckUser");
            }
            else
            {
                Model.User User = Session["LoginUser"] as Model.User;

               List<Model.FoodOrder> list= FoodOrderService.LoadEntities(F => F.UserID == User.Id && F.ISPlay == 0).ToList();

                ViewData["list"] = list;


            }




            return View();
        }
        //menuId: menuId, Count: value
        public ActionResult AddUserOrder(int menuId,int Count)
        {
            if (Count<0)
            {
                return Content("The number of menus cannot be 0 !");
            }


            if (Session["LoginUser"]==null)
            {
                return Content("Please login!");
            }
            else
            {
                Model.User User = Session["LoginUser"] as Model.User;

                //FoodOrderService

              List<Model.FoodOrder>  list= FoodOrderService.LoadEntities(F => F.UserID == User.Id && F.FoodID == menuId && F.ISPlay != 1).ToList();

                Model.Menu menuModel= MenuService.LoadEntities(M => M.Id == menuId).FirstOrDefault();

                if (menuModel.Number< Count)
                {
                    return Content("Insufficient inventory,"+menuModel.Number+ " piece left");
                }
                

                Model.FoodOrder foodOrderModel = new Model.FoodOrder();
                if (list.Count==0)
                {
                    foodOrderModel.CreateTime = DateTime.Now;
                    foodOrderModel.FoodID = menuId;
                    foodOrderModel.FoodNumber = Count;
                    foodOrderModel.Money =(decimal)menuModel.Price;
                    foodOrderModel.Img = menuModel.Img;
                    foodOrderModel.MenuName = menuModel.Name;
                    foodOrderModel.UserID = User.Id;
                    foodOrderModel.ISPlay = 0;

                    FoodOrderService.Add(foodOrderModel);

                   
                    return Content("ok");
                    
                }
                else if (list.Count==1)
                {
                    foodOrderModel = list[0];

                    foodOrderModel.FoodNumber = foodOrderModel.FoodNumber + Count;
                    foodOrderModel.Money = (decimal)(foodOrderModel.Money + menuModel.Price * Count);
                 
                    FoodOrderService.Update(foodOrderModel);
                    FoodOrderService.Savechanges();
                    return Content("ok");

                }
                

            }

            return  Content("NO");
        }

        public ActionResult Demo()
        {
            return View();
        }
        //"action": "change", "FoodID": FoodID, "FId": FId, "count": count
        public ActionResult ChangeCart(string action,int FoodID,int FId,int count)
        {
            if (action=="change")
            {
                Model.FoodOrder foodOrderModel= FoodOrderService.LoadEntities(F => F.Id == FId).FirstOrDefault();

                Model.Menu MenuModel= MenuService.LoadEntities(M => M.Id == foodOrderModel.FoodID).FirstOrDefault();

                if (MenuModel.Number<count)
                {
                    return Content("Insufficient inventory," + MenuModel.Number + " piece left");
                }


                foodOrderModel.FoodNumber = count;
                foodOrderModel.Money = (decimal)MenuModel.Price * count;


                FoodOrderService.Update(foodOrderModel);
                
                    return Content("ok");


            }
            else if(action== "del")
            {
                if(FoodOrderService.Delete(FId)>0)
                {

                    return Content("ok");

                }
                else
                {
                    return Content("no");
                }
            }






            return View();
        }


        [HttpPost]
        public ActionResult Play()
        {
            Model.User User = Session["LoginUser"] as Model.User;

            if (User!=null)
            {
                List<Model.FoodOrder> list = FoodOrderService.LoadEntities(F => F.UserID == User.Id &&F.ISPlay==0).ToList();
                
                
                foreach (var item in list)
                {
                    Model.FoodOrder FoodOrderModel = new Model.FoodOrder();

                    FoodOrderModel = item;
                    FoodOrderModel.ISPlay = 1;

                   Model.Menu menuModel= MenuService.LoadEntities(M => M.Id == FoodOrderModel.FoodID).FirstOrDefault();

                    
                    if (menuModel!=null)
                    {
                        if (menuModel.Number < item.FoodNumber)
                        {
                            //库存不足
                            return Content("Insufficient inventory!");
                        }

                        Model.Menu menuModel2 = new Model.Menu();

                    menuModel2 = menuModel;    
                    menuModel2.Number = menuModel.Number - item.FoodNumber;
                    

                    MenuService.Update(menuModel2);
                    }

                    FoodOrderService.Update(FoodOrderModel);

                }
                

                return Content("ok");
            }
            else
            {
                return Content("Please log in!");
            }
            

            
        }

        public ActionResult GetUserOldOrder()
        {
            return View();


        }

        public ActionResult GetOrder()
        {
            int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);
            int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);

            Model.User User = Session["LoginUser"] as Model.User;

            if (User==null)
            {
                return Content("Please log in"); //未登入
            }

            int total;
            List<Model.FoodOrder> list = FoodOrderService.LoadPageEntities(pageSize, pageIndex, out total, F =>F.UserID==User.Id&&F.ISPlay==1, m => m.CreateTime, false).ToList();

            List<UI.Models.ViewModels> ToList = new List<Models.ViewModels>();


            foreach (var item in list)
            {
                if (item.ISPlay == 0)
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
                else if (item.ISPlay == 1)
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



        public ActionResult Play2()
        {
            return View();
        }

        public IBLL.IUserService UserService { get; set; }
        public string GetUserName(int userid)
        {
            return UserService.LoadEntities(U => U.Id == userid).FirstOrDefault().UserName;


        }

        ///public IBLL.IMenuService MenuService { get; set; }
        public string GetMenuName(int id)
        {
            return MenuService.LoadEntities(U => U.Id == id).FirstOrDefault().Name;


        }
    }
}