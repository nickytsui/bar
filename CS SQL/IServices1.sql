 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace IBLL
{
   
	
	public  partial interface IFoodOrderService :IBaseService<FoodOrder>
	{
    }
	
	public  partial interface IMenuService :IBaseService<Menu>
	{
    }
	
	public  partial interface IUserService :IBaseService<User>
	{
    }
	
}