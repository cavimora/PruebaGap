namespace SuperZapatos.DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 28, fixedLength: true),
                        Description = c.String(nullable: false, maxLength: 100, fixedLength: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 4, storeType: "numeric"),
                        Total_in_shelf = c.Int(nullable: false),
                        Total_in_vault = c.Int(nullable: false),
                        Store_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Store", t => t.Store_Id)
                .Index(t => t.Store_Id);
            
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 28, fixedLength: true),
                        Address = c.String(nullable: false, maxLength: 200, fixedLength: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Store_Id", "dbo.Store");
            DropIndex("dbo.Articles", new[] { "Store_Id" });
            DropTable("dbo.Store");
            DropTable("dbo.Articles");
        }
    }
}
