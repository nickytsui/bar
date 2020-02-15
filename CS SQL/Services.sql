 

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Model;

//namespace BLL
//{
   
	
//	public partial class DepartmentService:BaseService<Department>,IBLL.IDepartmentService
//    {
//        public override void SetCurrentDal()
//        {
//            CurrentDal = DbSession.DepartmentDal;
//        }
//    }
	
//	public partial class RoleService:BaseService<Role>,IBLL.IRoleService
//    {
//        public override void SetCurrentDal()
//        {
//            CurrentDal = DbSession.RoleDal;
//        }
//    }
	
//	public partial class UserInfoService:BaseService<UserInfo>,IBLL.IUserInfoService
//    {
//        public override void SetCurrentDal()
//        {
//            CurrentDal = DbSession.UserInfoDal;
//        }
//    }
	
//}