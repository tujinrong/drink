namespace WebEvaluation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.S_Code",
                c => new
                    {
                        Kind = c.String(nullable: false, maxLength: 10, unicode: false),
                        CD = c.String(nullable: false, maxLength: 4, unicode: false),
                        Name = c.String(maxLength: 10),
                        Memo = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => new { t.Kind, t.CD });
            
            CreateTable(
                "dbo.M_Division",
                c => new
                    {
                        DivCD = c.String(nullable: false, maxLength: 1, unicode: false),
                        DivName = c.String(nullable: false, maxLength: 10),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.DivCD);
            
            CreateTable(
                "dbo.T_EvaByLeader",
                c => new
                    {
                        PartyID = c.Int(nullable: false),
                        LeaderEva = c.String(maxLength: 2, unicode: false),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartyID);
            
            CreateTable(
                "dbo.T_EvaByStaff",
                c => new
                    {
                        PartyID = c.Int(nullable: false),
                        Eva1Date = c.DateTime(),
                        Eva1Result = c.String(maxLength: 1),
                        Eva1Time = c.Int(),
                        Eva1StaffCD = c.String(maxLength: 8),
                        Eva2Date = c.DateTime(),
                        Eva2Result = c.String(maxLength: 1),
                        Eva2Time = c.Int(),
                        Eva2StaffCD = c.String(maxLength: 8),
                        Eva3Date = c.DateTime(),
                        Eva3Result = c.String(maxLength: 1),
                        Eva3Time = c.Int(),
                        Eva3StaffCD = c.String(maxLength: 8),
                        StatffEva = c.String(maxLength: 2),
                        CareFlg = c.String(maxLength: 1),
                        Record = c.String(),
                        EvaResultFlag = c.String(maxLength: 1),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartyID);
            
            CreateTable(
                "dbo.M_Group",
                c => new
                    {
                        GroupCD = c.String(nullable: false, maxLength: 2, unicode: false),
                        GroupName = c.String(maxLength: 10),
                        DivCD = c.String(maxLength: 1, unicode: false),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.GroupCD);
            
            CreateTable(
                "dbo.M_Message",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.MessageID);
            
            CreateTable(
                "dbo.T_Party",
                c => new
                    {
                        PartyID = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        PartyNo = c.String(nullable: false, maxLength: 10, unicode: false),
                        ShopCD = c.String(maxLength: 5, unicode: false),
                        HallType = c.String(maxLength: 10),
                        PartyDate = c.DateTime(nullable: false),
                        StartTime = c.String(maxLength: 5),
                        TantoCD = c.String(nullable: false, maxLength: 8, unicode: false),
                        BrideName = c.String(maxLength: 30),
                        BrideKana = c.String(maxLength: 30),
                        BrideHomeTel = c.String(maxLength: 13),
                        BrideMobile = c.String(maxLength: 13),
                        GroomName = c.String(maxLength: 30),
                        GroomKana = c.String(maxLength: 30),
                        GroomHomeTel = c.String(maxLength: 13),
                        GroomMobile = c.String(maxLength: 13),
                        FinishFlag = c.Boolean(nullable: false),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartyID);
            
            CreateTable(
                "dbo.T_Report",
                c => new
                    {
                        PartyID = c.Int(nullable: false),
                        Memo = c.String(),
                        TelFlg = c.String(maxLength: 1),
                        Remark = c.String(maxLength: 200),
                        TelWho = c.String(maxLength: 2),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.PartyID);
            
            CreateTable(
                "dbo.M_Shop",
                c => new
                    {
                        ShopCD = c.String(nullable: false, maxLength: 5, unicode: false),
                        ShopName = c.String(nullable: false, maxLength: 100),
                        GroupCD = c.String(maxLength: 2, unicode: false),
                        ShopType = c.String(maxLength: 1, unicode: false),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ShopCD);
            
            CreateTable(
                "dbo.M_Staff",
                c => new
                    {
                        StaffCD = c.String(nullable: false, maxLength: 8, unicode: false),
                        StaffName = c.String(nullable: false, maxLength: 10),
                        StaffKana = c.String(maxLength: 20),
                        Sex = c.String(nullable: false, maxLength: 1, unicode: false),
                        EnrollmentDate = c.DateTime(),
                        Yakusyoku = c.String(maxLength: 30),
                        Duty = c.String(maxLength: 30),
                        UnitCD = c.String(nullable: false, maxLength: 11),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.StaffCD);
            
            CreateTable(
                "dbo.M_System",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CusCenterUnitCD = c.String(maxLength: 11, unicode: false),
                        AccessIP = c.String(maxLength: 15, unicode: false),
                        AdminIP = c.String(maxLength: 15, unicode: false),
                        PageRowCount = c.Int(nullable: false),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.S_Unit",
                c => new
                    {
                        UnitCD = c.String(nullable: false, maxLength: 11, unicode: false),
                        UnitName = c.String(maxLength: 20),
                        ShopCD = c.String(maxLength: 5, unicode: false),
                        IsCusCenter = c.Boolean(nullable: false),
                        IsShop = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UnitCD);
            
            CreateTable(
                "dbo.M_User",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 8),
                        Password = c.String(maxLength: 20),
                        StaffCD = c.String(maxLength: 8),
                        RoleCD = c.String(maxLength: 2, unicode: false),
                        UpdateUserID = c.String(maxLength: 8, unicode: false),
                        UpdateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.M_User");
            DropTable("dbo.S_Unit");
            DropTable("dbo.M_System");
            DropTable("dbo.M_Staff");
            DropTable("dbo.M_Shop");
            DropTable("dbo.T_Report");
            DropTable("dbo.T_Party");
            DropTable("dbo.M_Message");
            DropTable("dbo.M_Group");
            DropTable("dbo.T_EvaByStaff");
            DropTable("dbo.T_EvaByLeader");
            DropTable("dbo.M_Division");
            DropTable("dbo.S_Code");
        }
    }
}
