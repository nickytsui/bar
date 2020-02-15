using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Model;

namespace DAL
{
    /// <summary>
    /// 把数据库访问层公共的方法抽出来进行实现。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDal<T> where T : class , new()
    {
        //上下文直击New。那么这样不行。我们要保证db实例是线程内唯一。

        //问题：把保证Ef上下文实例唯一的代码写在这个地方合适吗？
        //考虑思路：第一当前类的职责是什么？当前的需求跟当前类的职责是否是一致的？
        //Model.DataModelContainer db =new DataModelContainer();

        private DbContext db = EFDbContextFactory.GetCurrentDbContext();
        
        public virtual T Add(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();

            return entity;
        }

        public virtual bool Update(T entity)
        {
            
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            //db.Entry(entity).State = EntityState.Detached;
            return db.SaveChanges() > 0;

            ///return true;
        }

        public virtual bool Delete(T entity)
        {
            
            
            db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;

            return db.SaveChanges() > 0;

            ///return true;

        }

        public virtual int Delete(params int[] ids)
        {
            //T entity =new T();
            //entity.ID
            //首先可以通过  泛型的基类的约束来实现对id字段赋值。
            //也可也使用反射的方式。
            foreach (var item in ids)
            {
                var entity = db.Set<T>().Find(item);//如果实体已经在内存中，那么就直接从内存拿，如果内存中跟踪实体没有，那么才查询数据库。
                db.Set<T>().Remove(entity); 
            }

            db.SaveChanges();

            //return 

            return ids.Count();

        }

        public IQueryable<T> LoadEntities(Func<T, bool> whereLambda)
        {
            

            var data= db.Set<T>().AsNoTracking().Where(whereLambda).AsQueryable();
            
            db.DetachAll();

            return data;
            

        }
        //// AsNoTracking()
        public IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex, out int total, Func<T, bool> whereLambda, Func<T, S> orderbyLambda, bool isAsc)
        {
            total = db.Set<T>().AsNoTracking().Where(whereLambda).Count();

            if (isAsc)
            {
                return 
                db.Set<T>().AsNoTracking()
                  .Where(whereLambda)
                  .OrderBy(orderbyLambda)
                  .Skip(pageSize*(pageIndex - 1))
                  .Take(pageSize)
                  .AsQueryable();
            }
            else
            {
                return
               db.Set<T>().AsNoTracking()
                 .Where(whereLambda)
                 .OrderByDescending(orderbyLambda)
                 .Skip(pageSize * (pageIndex - 1))
                 .Take(pageSize)
                 .AsQueryable();
            }
        }


        #region MyRegion
        //public IQueryable<T> LoadEntities(Expression<Func<T, bool>> whereLambda)
        //{

        //    return db.Set<T>().AsNoTracking().Where(whereLambda).AsQueryable();



        //}
        ////// AsNoTracking()
        //public IQueryable<T> LoadPageEntities<S>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderbyLambda, bool isAsc)
        //{
        //    total = db.Set<T>().AsNoTracking().Where(whereLambda).Count();

        //    if (isAsc)
        //    {
        //        return
        //        db.Set<T>().AsNoTracking()
        //          .Where(whereLambda)
        //          .OrderBy(orderbyLambda)
        //          .Skip(pageSize * (pageIndex - 1))
        //          .Take(pageSize)
        //          .AsQueryable();
        //    }
        //    else
        //    {
        //        return
        //       db.Set<T>().AsNoTracking()
        //         .Where(whereLambda)
        //         .OrderByDescending(orderbyLambda)
        //         .Skip(pageSize * (pageIndex - 1))
        //         .Take(pageSize)
        //         .AsQueryable();
        //    }
        //}
        #endregion

        





    }
    public static class DbContextDetachAllExtension
    {
        /// <summary>
        /// 取消跟踪DbContext中所有被跟踪的实体
        /// </summary>
        public static void DetachAll(this DbContext dbContext)
        {
            //循环遍历DbContext中所有被跟踪的实体
            while (true)
            {
                //每次循环获取DbContext中一个被跟踪的实体
                var currentEntry = dbContext.ChangeTracker.Entries().FirstOrDefault();

                //currentEntry不为null，就将其State设置为EntityState.Detached，即取消跟踪该实体
                if (currentEntry != null)
                {
                    //设置实体State为EntityState.Detached，取消跟踪该实体，之后dbContext.ChangeTracker.Entries().Count()的值会减1
                    currentEntry.State = System.Data.Entity.EntityState.Detached;
                }
                //currentEntry为null，表示DbContext中已经没有被跟踪的实体了，则跳出循环
                else
                {
                    break;
                }
            }
        }

    }
    }
