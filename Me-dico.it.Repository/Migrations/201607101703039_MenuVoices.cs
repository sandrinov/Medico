namespace Me_dico.it.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MenuVoices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IconName = c.String(),
                        Text = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Role = c.Int(nullable: false),
                        UserStatus = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Menu");
        }
    }
}
