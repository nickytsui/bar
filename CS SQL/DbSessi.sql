 

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
    public partial class DbSession : IDbSession
    {  
	
		//private IDepartmentDal _DepartmentDal;
		//public IDepartmentDal DepartmentDal {
  //          get {
  //              if (_DepartmentDal == null)
  //              {
  //                  _DepartmentDal =new DepartmentDal();
  //              }
  //              return _DepartmentDal;
  //          }
  //      }
	
		//private IRoleDal _RoleDal;
		//public IRoleDal RoleDal {
  //          get {
  //              if (_RoleDal == null)
  //              {
  //                  _RoleDal =new RoleDal();
  //              }
  //              return _RoleDal;
  //          }
  //      }
	
		//private IUserInfoDal _UserInfoDal;
		//public IUserInfoDal UserInfoDal {
  //          get {
  //              if (_UserInfoDal == null)
  //              {
  //                  _UserInfoDal =new UserInfoDal();
  //              }
  //              return _UserInfoDal;
  //          }
  //      }
	}	
}