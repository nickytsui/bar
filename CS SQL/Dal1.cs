 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDAL;
using Model;

namespace DAL
{
   
	 public partial class FoodOrderDal : BaseDal<FoodOrder>,IFoodOrderDal
    {
       
    }	
	 public partial class MenuDal : BaseDal<Menu>,IMenuDal
    {
       
    }	
	 public partial class UserDal : BaseDal<User>,IUserDal
    {
       
    }	
	
}