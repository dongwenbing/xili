//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MB.Services.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class area
    {
        public int area_id { get; set; }
        public string area_name { get; set; }
        public int is_site { get; set; }
        public string pinyin { get; set; }
        public string pinyin_initials { get; set; }
        public int father_area_id { get; set; }
        public string father_area_info { get; set; }
        public int area_type { get; set; }
        public int grade { get; set; }
        public int status { get; set; }
        public string remark { get; set; }
        public int sort { get; set; }
        public int is_add_circle { get; set; }
    }
}
