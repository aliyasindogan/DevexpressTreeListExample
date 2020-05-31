using System.ComponentModel.DataAnnotations.Schema;

namespace DevexpressTreeListExample.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserTypeID { get; set; }

        [ForeignKey("UserTypeID")]
        public UserType UserType { get; set; }
    }
}