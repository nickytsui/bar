using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class LoginController : Controller
    {
        public IBLL.IUserService UserService { get; set; }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string UserName,string UserPwd, string UserPwd2,string Email)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return Content("<script type='text/javascript'>alert('Account number cannot be empty！');</script>");
            }

            if (string.IsNullOrEmpty(UserPwd)|| string.IsNullOrEmpty(UserPwd2))
            {
                return Content("<script type='text/javascript'>alert('Password cannot be empty！');</script>");
            }

            Model.User ModelUser = new Model.User();

            if (UserPwd!=UserPwd2)
            {
                return Content("<script type='text/javascript'>alert('Incorrect password！');</script>");
            }

            ModelUser.UserName = UserName;
            ModelUser.UserPwd = UserPwd;

            ModelUser.money = 0;
            ModelUser.State = 0;
            ModelUser.mailbox = Email;


           Model.User user= UserService.Add(ModelUser);

            if (user.Id!=0)
            {
                return Content("<script type='text/javascript'>alert('login was successful！');window.location.href='/Home/Index';</script>");
            }
            else
            {
                return Content("<script type='text/javascript'>alert('login has failed！');</script>");
            }




            
        }


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckUser(string LoginPwd, string LoginCode, string vCode)
        {
            //第一教研验证码
            string sessionCode = Session["ValidateCode"] == null ? string.Empty : Session["ValidateCode"].ToString();

            Session["ValidateCode"] = null;
            if (vCode != sessionCode)
            {
                //ViewData["error"] = "<script> alert('验证码错误！')</script>";
                //return View();
                return Content("<script type='text/javascript'>alert('Verification code error！');window.location.href='/Home/Index';</script>");
            }

            Model.User user =
                UserService.LoadEntities(u => u.UserName == LoginCode && u.UserPwd == LoginPwd).FirstOrDefault();

            if (user == null)
            {
                //ViewData["error"] = "<script> alert('用户名密码错误！')</script>";
                //return View();
                return Content("<script type='text/javascript'>alert('Wrong user name and password');window.location.href='/Home/Index';</script>");
            }


            Session["LoginUser"] = user;

            //return RedirectToAction("Index", "UserInfo");
            return Content("<script type='text/javascript'>alert('Login successfully');window.location.href='/Home/Index';</script>");
        }

       
        public ActionResult ClearUser()
        {
            Session["LoginUser"] = null;

            
            return Content("Login successfully");
        }




        public ActionResult VCode()
        {
            //引用的都是程序集。需要生成后才会有效果的。
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
            //Response.WriteFile();
            //return File()

            //Response.BinaryWrite();

        }

    }
}