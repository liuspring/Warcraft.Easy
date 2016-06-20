namespace TaskManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.qrtz_version_info", "zip_filepath", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.qrtz_version_info", "zip_filepath");
        }
    }
}
