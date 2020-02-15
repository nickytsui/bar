 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace IDAL
{
   
	
	public partial interface IFoodOrderDal :IBaseDal<FoodOrder>
    { 
	}	
	
	public partial interface IMenuDal :IBaseDal<Menu>
    { 
	}	
	
	public partial interface IUserDal :IBaseDal<User>
    { 
	}	
	
}