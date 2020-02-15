using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Model;

namespace IDAL
{
    /// <summary>
    /// 抽象里所有的数据库访问层Dal实例的公共的方法约束。
    /// </summary>
    public interface IBaseDal<T> where T:class ,new ()
    {
        T Add(T entity);

        bool Update(T entity);

        bool Delete(T entity);

        int Delete(params int[] ids);

        //u=>true
        IQueryable<T> LoadEntities(Func<T, bool> whereLambda);//规约设计模式。 where a>10

        IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex, out int total,
                                                  Func<T, bool> whereLambda
                                                  , Func<T, S> orderbyLambda, bool isAsc);
    }
}
