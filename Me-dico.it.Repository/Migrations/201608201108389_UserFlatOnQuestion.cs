namespace Me_dico.it.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFlatOnQuestion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropIndex("dbo.Question", new[] { "UserId" });
            AddColumn("dbo.Question", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Question", "UserName");
            CreateIndex("dbo.Question", "UserId");
            AddForeignKey("dbo.Question", "UserId", "dbo.User", "UserId");
        }
    }
}
