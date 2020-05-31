using DevexpressTreeListExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevexpressTreeListExample
{
    internal class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            #region Create Default UserType

            if (!context.UserTypes.Any())
            {
                UserType userType = new UserType
                {
                    UserTypeName = "System Admin",
                };
                context.UserTypes.Add(userType);
                context.SaveChanges();

                UserType userType1 = new UserType
                {
                    UserTypeName = "Admin",
                };
                context.UserTypes.Add(userType1);
                context.SaveChanges();

                UserType userType2 = new UserType
                {
                    UserTypeName = "User",
                };
                context.UserTypes.Add(userType2);
                context.SaveChanges();
            }

            #endregion Create Default UserType

            #region Create Default User

            if (!context.Users.Any()) //Look for any users.
            {
                User user = new User
                {
                    UserName = "systemadmin",
                    FirstName = "Ali Yasin",
                    LastName = "DOĞAN",
                    UserTypeID = 1,
                };
                context.Users.Add(user);
                context.SaveChanges();

                User user1 = new User
                {
                    UserName = "admin",
                    FirstName = "Ali",
                    LastName = "DOĞAN",
                    UserTypeID = 2,
                };
                context.Users.Add(user1);
                context.SaveChanges();

                User user2 = new User
                {
                    UserName = "user",
                    FirstName = "Yasin",
                    LastName = "DOĞAN",
                    UserTypeID = 3,
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            #endregion Create Default User

            #region Create Default Category

            if (!context.Categories.Any())
            {
                Category category1 = new Category
                {
                    CategoryName = "ANA SAYFA",
                    SubCategoryID = 0,
                };
                context.Categories.Add(category1);
                context.SaveChanges();

                Category category2 = new Category
                {
                    CategoryName = "ÜRÜNLER",
                    SubCategoryID = 0,
                };
                context.Categories.Add(category2);
                context.SaveChanges();

                Category category3 = new Category
                {
                    CategoryName = "HAKKIMIZDA",
                    SubCategoryID = 0,
                };
                context.Categories.Add(category3);
                context.SaveChanges();

                Category category4 = new Category
                {
                    CategoryName = "İLETİŞİM",
                    SubCategoryID = 0,
                };
                context.Categories.Add(category4);
                context.SaveChanges();

                #region Ürünler Alt Kategori

                Category category5 = new Category
                {
                    CategoryName = "Beyaz Eşya",
                    SubCategoryID = 2,
                };
                context.Categories.Add(category5);
                context.SaveChanges();

                Category category6 = new Category
                {
                    CategoryName = "Elektronik Eşya",
                    SubCategoryID = 2,
                };
                context.Categories.Add(category6);
                context.SaveChanges();

                Category category7 = new Category
                {
                    CategoryName = "Buzdolabı",
                    SubCategoryID = 5,
                };
                context.Categories.Add(category7);
                context.SaveChanges();

                Category category8 = new Category
                {
                    CategoryName = "Çamaşır Makinesi",
                    SubCategoryID = 5,
                };
                context.Categories.Add(category8);
                context.SaveChanges();

                Category category9 = new Category
                {
                    CategoryName = "Bulaşık Makinesi",
                    SubCategoryID = 5,
                };
                context.Categories.Add(category9);
                context.SaveChanges();

                Category category10 = new Category
                {
                    CategoryName = "Televizyon",
                    SubCategoryID = 5,
                };
                context.Categories.Add(category10);
                context.SaveChanges();

                #endregion Ürünler Alt Kategori

                #region Hakkımızda Alt Kategori

                Category category11 = new Category
                {
                    CategoryName = "Vizyonumuz",
                    SubCategoryID = 3,
                };
                context.Categories.Add(category11);
                context.SaveChanges();

                Category category12 = new Category
                {
                    CategoryName = "Misyonumuz",
                    SubCategoryID = 3,
                };
                context.Categories.Add(category12);
                context.SaveChanges();

                #endregion Hakkımızda Alt Kategori
            }

            #endregion Create Default Category
        }
    }
}