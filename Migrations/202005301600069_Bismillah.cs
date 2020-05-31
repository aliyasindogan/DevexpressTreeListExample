namespace DevexpressTreeListExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bismillah : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        SubCategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryUserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        SubCategoryID = c.Int(nullable: false),
                        UserTypeID = c.Int(nullable: false),
                        IsChecked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.UserTypeID);
            
            CreateTable(
                "dbo.UserTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserTypeName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTypes", t => t.UserTypeID, cascadeDelete: true)
                .Index(t => t.UserTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserTypeID", "dbo.UserTypes");
            DropForeignKey("dbo.CategoryUserTypes", "UserTypeID", "dbo.UserTypes");
            DropForeignKey("dbo.CategoryUserTypes", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Users", new[] { "UserTypeID" });
            DropIndex("dbo.CategoryUserTypes", new[] { "UserTypeID" });
            DropIndex("dbo.CategoryUserTypes", new[] { "CategoryID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserTypes");
            DropTable("dbo.CategoryUserTypes");
            DropTable("dbo.Categories");
        }
    }
}
