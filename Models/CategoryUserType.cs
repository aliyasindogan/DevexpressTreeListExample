using System.ComponentModel.DataAnnotations.Schema;

namespace DevexpressTreeListExample.Models
{
    public class CategoryUserType
    {
        public int Id { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int UserTypeID { get; set; }
        public bool IsChecked { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [ForeignKey("UserTypeID")]
        public virtual UserType UserType { get; set; }
    }
}