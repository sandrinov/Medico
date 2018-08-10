namespace Me_dico.it.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFlatOnAnswer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropIndex("dbo.Answer", new[] { "UserId" });
            AddColumn("dbo.Answer", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answer", "UserName");
            CreateIndex("dbo.Answer", "UserId");
            AddForeignKey("dbo.Answer", "UserId", "dbo.User", "UserId");
        }
    }
}
