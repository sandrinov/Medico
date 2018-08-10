namespace Me_dico.it.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialù : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivationCode",
                c => new
                    {
                        ActivationCodeId = c.Int(nullable: false, identity: true),
                        MedicoCode = c.String(nullable: false),
                        ActivactionCode = c.String(nullable: false),
                        Mobile = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ExpiredDate = c.DateTime(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.ActivationCodeId);
            
            CreateTable(
                "dbo.AnswerComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(),
                        AnswerSource_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answer", t => t.AnswerSource_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.AnswerSource_Id);
            
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        VoteCount = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(),
                        QuestionSource_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Question", t => t.QuestionSource_Id, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionSource_Id);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        VoteCount = c.Int(nullable: false),
                        AnswersCount = c.Int(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(),
                        QuestionSource_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Question", t => t.QuestionSource_Id, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.QuestionSource_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Guid(nullable: false, identity: true),
                        NickName = c.String(),
                        Email = c.String(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        Password = c.String(),
                        ActivationCode = c.String(),
                        IdMedicalCard = c.String(),
                        CreateDate = c.DateTime(),
                        ActivationDate = c.DateTime(),
                        LastLoginDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Title = c.String(),
                        Photo = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Mobile = c.String(),
                        DateOfBirth = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(),
                        User_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UpdateDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionTags",
                c => new
                    {
                        Question_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Question_Id, t.Tag_Id })
                .ForeignKey("dbo.Question", t => t.Question_Id)
                .ForeignKey("dbo.Tag", t => t.Tag_Id)
                .Index(t => t.Question_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerComment", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropForeignKey("dbo.QuestionTags", "Tag_Id", "dbo.Tag");
            DropForeignKey("dbo.QuestionTags", "Question_Id", "dbo.Question");
            DropForeignKey("dbo.QuestionComment", "QuestionSource_Id", "dbo.Question");
            DropForeignKey("dbo.QuestionComment", "UserId", "dbo.User");
            DropForeignKey("dbo.Profile", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "QuestionSource_Id", "dbo.Question");
            DropForeignKey("dbo.AnswerComment", "AnswerSource_Id", "dbo.Answer");
            DropIndex("dbo.QuestionTags", new[] { "Tag_Id" });
            DropIndex("dbo.QuestionTags", new[] { "Question_Id" });
            DropIndex("dbo.Profile", new[] { "User_UserId" });
            DropIndex("dbo.QuestionComment", new[] { "QuestionSource_Id" });
            DropIndex("dbo.QuestionComment", new[] { "UserId" });
            DropIndex("dbo.Question", new[] { "UserId" });
            DropIndex("dbo.Answer", new[] { "QuestionSource_Id" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropIndex("dbo.AnswerComment", new[] { "AnswerSource_Id" });
            DropIndex("dbo.AnswerComment", new[] { "UserId" });
            DropTable("dbo.QuestionTags");
            DropTable("dbo.Tag");
            DropTable("dbo.Profile");
            DropTable("dbo.User");
            DropTable("dbo.QuestionComment");
            DropTable("dbo.Question");
            DropTable("dbo.Answer");
            DropTable("dbo.AnswerComment");
            DropTable("dbo.ActivationCode");
        }
    }
}
