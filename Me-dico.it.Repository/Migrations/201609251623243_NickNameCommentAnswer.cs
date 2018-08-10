namespace Me_dico.it.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NickNameCommentAnswer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AnswerComment", "UserId", "dbo.User");
            DropIndex("dbo.AnswerComment", new[] { "UserId" });
            AddColumn("dbo.AnswerComment", "NickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnswerComment", "NickName");
            CreateIndex("dbo.AnswerComment", "UserId");
            AddForeignKey("dbo.AnswerComment", "UserId", "dbo.User", "UserId");
        }
    }
}
