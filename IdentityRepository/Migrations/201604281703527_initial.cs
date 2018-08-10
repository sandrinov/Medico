namespace IdentityServerRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomUsers",
                c => new
                    {
                        Subject = c.Guid(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Subject);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomUsers");
        }
    }
}
