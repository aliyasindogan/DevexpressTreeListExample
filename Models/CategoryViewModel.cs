using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevexpressTreeListExample.Models
{
    public class  CategoryViewModel
    {
        public int Id { get; set; }
        public int? SubCategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool? Checked { get; set; }
    }
}