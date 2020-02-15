using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using DAL;
using IDAL;

namespace DalFactory
{
    public class DALSimpleFactory
    {
        //public static IDAL.IUserInfoDal GetUserInfoDal()
        //{

        //    //如果直接new 的话那么必须  改代码的才能切换不同的数据库访问层。

        //    //非常希望能做到：只改配置就能够创建出实例出来。也就是改变数据库访问层的实现。

        //    //return GetInstences("CZBK.OADemo.DAL", "CZBK.OADemo.DAL." + "UserInfoDal") as IUserInfoDal;



        //    return   new UserInfoAdoNetDal();
        //}


        //public static IDAL.IRoleDal GetRoleDal()
        //{
        //    return  new RoleDal();
        //}


        public static Object GetInstences(string assemblyName, string typeName)
        {
            return Assembly.Load(assemblyName).CreateInstance(typeName);

            //return null;
        }
    }
}
