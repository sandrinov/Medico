namespace Me_dico.it.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class titlequestion : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Answer", "UserId");
            CreateIndex("dbo.Question", "UserId");
            AddForeignKey("dbo.Question", "UserId", "dbo.User", "UserId");
            AddForeignKey("dbo.Answer", "UserId", "dbo.User", "UserId");
            DropColumn("dbo.Answer", "UserName");
            DropColumn("dbo.Question", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Question", "UserName", c => c.String());
            AddColumn("dbo.Answer", "UserName", c => c.String());
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropIndex("dbo.Question", new[] { "UserId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
        }
    }
}
