 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Linq.Expressions;
using IBLL;
namespace BLL
{
   
	
	public partial class FoodOrderService:BaseService<FoodOrder>,IBLL.IFoodOrderService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.FoodOrderDal;
        }
    }
	
	public partial class MenuService:BaseService<Menu>,IBLL.IMenuService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.MenuDal;
        }
    }
	
	public partial class UserService:BaseService<User>,IBLL.IUserService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserDal;
        }
    }
	
}