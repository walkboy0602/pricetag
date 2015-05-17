namespace PriceTag.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateTag_userId_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tag", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tag", "UserId", c => c.Int(nullable: false));
        }
    }
}
