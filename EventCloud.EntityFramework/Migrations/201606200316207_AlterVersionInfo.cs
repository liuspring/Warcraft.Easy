namespace EventCloud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterVersionInfo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.qrtz_version_info", "zip_filename", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.qrtz_version_info", "zip_filepath", c => c.String(maxLength: 200, storeType: "nvarchar"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.qrtz_version_info", "zip_filepath", c => c.String(maxLength: 50, storeType: "nvarchar"));
            AlterColumn("dbo.qrtz_version_info", "zip_filename", c => c.String(maxLength: 50, storeType: "nvarchar"));
        }
    }
}
