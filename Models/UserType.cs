using System.Collections.Generic;

namespace DevexpressTreeListExample.Models
{
    public class UserType
    {
        public UserType()
        {
            CategoryUserTypes = new HashSet<CategoryUserType>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string UserTypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<CategoryUserType> CategoryUserTypes { get; set; }
    }
}