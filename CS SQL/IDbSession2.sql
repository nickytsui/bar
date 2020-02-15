 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
	public partial  interface  IDbSession
    {  
		
		IFoodOrderDal FoodOrderDal { get; }
		
		IMenuDal MenuDal { get; }
		
		IUserDal UserDal { get; }
	}	
}