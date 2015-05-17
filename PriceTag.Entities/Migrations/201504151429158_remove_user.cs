namespace PriceTag.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_user : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tag", newName: "Tags");
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserProfile_Id", "dbo.UserProfile");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "UserProfile_Id" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropTable("dbo.Role");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Users");
            DropTable("dbo.UserClaim");
            DropTable("dbo.UserLogin");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId });
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        UserProfile_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PictureUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId });
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserLogin", "UserId");
            CreateIndex("dbo.UserClaim", "UserId");
            CreateIndex("dbo.Users", "UserProfile_Id");
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.UserRole", "UserId");
            CreateIndex("dbo.UserRole", "RoleId");
            CreateIndex("dbo.Role", "Name", unique: true, name: "RoleNameIndex");
            AddForeignKey("dbo.Users", "UserProfile_Id", "dbo.UserProfile", "Id");
            AddForeignKey("dbo.UserRole", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserLogin", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserClaim", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserRole", "RoleId", "dbo.Role", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Tags", newName: "Tag");
        }
    }
}
