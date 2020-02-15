using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDAL
{
    public partial interface  IDbSession
    {
        //IDAL.IRoleDal RoleDal { get; }

        //IDAL.IUserInfoDal UserInfoDal { get;  }

        int SaveChanges();
    }
}
