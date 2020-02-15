using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UI.Models
{
    /// <summary>
    /// DbContext的扩展类
    /// </summary>
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