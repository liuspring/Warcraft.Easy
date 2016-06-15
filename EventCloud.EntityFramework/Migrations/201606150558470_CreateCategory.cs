namespace EventCloud.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.qrtz_category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        IsDeleted = c.Boolean(nullable: false),
                        DeDefault1leterUserId = c.Long(),
                        DeletionTime = c.DateTime(precision: 0),
                        LastModificationTime = c.DateTime(precision: 0),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false, precision: 0),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.qrtz_category",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
