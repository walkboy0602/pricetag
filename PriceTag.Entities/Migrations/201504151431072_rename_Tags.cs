namespace PriceTag.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rename_Tags : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Tags", newName: "Tag");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Tag", newName: "Tags");
        }
    }
}
