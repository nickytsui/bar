//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class FoodOrder
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int FoodID { get; set; }
        public int FoodNumber { get; set; }
        public decimal Money { get; set; }
        public int ISPlay { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Img { get; set; }
        public string MenuName { get; set; }
        public string address { get; set; }
    }
}