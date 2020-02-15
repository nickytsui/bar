 

using System.Linq;
using System.Text;
using DAL;
using IDAL;

namespace DalFactory
{
    /// <summary>
    /// DbSession:本质就是一个简单工厂，就是这么一个简单工厂，但从抽象意义上来说，它就是整个数据库访问层的统一入口。
    /// 因为拿到了DbSession就能够拿到整个数据库访问层所有的dal。
    /// </summary>
    public partial class DbSession :IDbSession
    {  
	
		private IFoodOrderDal _FoodOrderDal;
		public IFoodOrderDal FoodOrderDal {
            get {
                if (_FoodOrderDal == null)
                {
                    _FoodOrderDal =new FoodOrderDal();
                }
                return _FoodOrderDal;
            }
        }
	
		private IMenuDal _MenuDal;
		public IMenuDal MenuDal {
            get {
                if (_MenuDal == null)
                {
                    _MenuDal =new MenuDal();
                }
                return _MenuDal;
            }
        }
	
		private IUserDal _UserDal;
		public IUserDal UserDal {
            get {
                if (_UserDal == null)
                {
                    _UserDal =new UserDal();
                }
                return _UserDal;
            }
        }
	}	
}