namespace Me_dico.it.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reallytitlequestion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropIndex("dbo.Question", new[] { "UserId" });
            AddColumn("dbo.Answer", "UserName", c => c.String());
            AddColumn("dbo.Question", "UserName", c => c.String());
            AddColumn("dbo.Question", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Question", "Title");
            DropColumn("dbo.Question", "UserName");
            DropColumn("dbo.Answer", "UserName");
            CreateIndex("dbo.Question", "UserId");
            CreateIndex("dbo.Answer", "UserId");
            AddForeignKey("dbo.Answer", "UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.Question", "UserId", "dbo.User", "UserId");
        }
    }
}
