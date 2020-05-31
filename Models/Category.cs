namespace DevexpressTreeListExample.Models
{
    using System.Collections.Generic;

    public partial class Category
    {
        public Category()
        {
            CategoryUserTypes = new HashSet<CategoryUserType>();
        }

        public int Id { get; set; }

        public string CategoryName { get; set; }

        public int? SubCategoryID { get; set; }

        public virtual ICollection<CategoryUserType> CategoryUserTypes { get; set; }
    }
}