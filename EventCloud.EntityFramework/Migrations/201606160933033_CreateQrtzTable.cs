namespace EventCloud.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class CreateQrtzTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.qrtz_category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        category_name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.qrtz_command",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cmd = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        cmd_name = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        cmd_state = c.Byte(nullable: false),
                        node_id = c.Int(nullable: false),
                        task_id = c.Int(nullable: false),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Command_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.qrtz_node", t => t.node_id, cascadeDelete: true)
                .ForeignKey("dbo.qrtz_task", t => t.task_id, cascadeDelete: true)
                .Index(t => t.node_id)
                .Index(t => t.task_id);
            
            CreateTable(
                "dbo.qrtz_node",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        node_name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        node_ip = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        if_check_state = c.Boolean(nullable: false),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Node_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.qrtz_error",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        node_id = c.Int(nullable: false),
                        task_id = c.Int(nullable: false),
                        mgs = c.String(nullable: false, maxLength: 4000, storeType: "nvarchar"),
                        error_type = c.Byte(nullable: false),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Error_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.qrtz_node", t => t.node_id, cascadeDelete: true)
                .ForeignKey("dbo.qrtz_task", t => t.task_id, cascadeDelete: true)
                .Index(t => t.node_id)
                .Index(t => t.task_id);
            
            CreateTable(
                "dbo.qrtz_task",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        task_name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        category_id = c.Int(nullable: false),
                        cmd_state = c.Int(nullable: false),
                        node_id = c.Int(nullable: false),
                        last_start_time = c.DateTime(nullable: false, precision: 0),
                        last_end_time = c.DateTime(nullable: false, precision: 0),
                        last_error_time = c.DateTime(nullable: false, precision: 0),
                        error_count = c.Int(nullable: false),
                        run_count = c.Int(nullable: false),
                        state = c.Byte(nullable: false),
                        version = c.Int(nullable: false),
                        app_config_json = c.String(nullable: false, maxLength: 1000, storeType: "nvarchar"),
                        cron = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        mainclass_dll_filename = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        mainclass_namespace = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        remark = c.String(nullable: false, maxLength: 2000, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Task_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.qrtz_category", t => t.category_id, cascadeDelete: true)
                .ForeignKey("dbo.qrtz_node", t => t.node_id, cascadeDelete: true)
                .Index(t => t.category_id)
                .Index(t => t.node_id);
            
            CreateTable(
                "dbo.qrtz_log",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        node_id = c.Int(nullable: false),
                        task_id = c.Int(nullable: false),
                        mgs = c.String(nullable: false, maxLength: 4000, storeType: "nvarchar"),
                        log_type = c.Byte(nullable: false),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Log_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.qrtz_node", t => t.node_id, cascadeDelete: true)
                .ForeignKey("dbo.qrtz_task", t => t.task_id, cascadeDelete: true)
                .Index(t => t.node_id)
                .Index(t => t.task_id);
            
            CreateTable(
                "dbo.qrtz_performance",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        node_id = c.Int(nullable: false),
                        task_id = c.Int(nullable: false),
                        cpu = c.Single(nullable: false),
                        memory = c.Single(nullable: false),
                        install_dir_size = c.Single(nullable: false),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Performance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.qrtz_node", t => t.node_id, cascadeDelete: true)
                .ForeignKey("dbo.qrtz_task", t => t.task_id, cascadeDelete: true)
                .Index(t => t.node_id)
                .Index(t => t.task_id);
            
            CreateTable(
                "dbo.qrtz_temp_data",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        task_id = c.Int(nullable: false),
                        data_json = c.String(maxLength: 50, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TempData_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.qrtz_task", t => t.task_id, cascadeDelete: true)
                .Index(t => t.task_id);
            
            CreateTable(
                "dbo.qrtz_version_info",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        task_id = c.Int(nullable: false),
                        version_type = c.Int(nullable: false),
                        zip_file = c.Binary(),
                        zip_filename = c.String(maxLength: 50, storeType: "nvarchar"),
                        is_deleted = c.Boolean(nullable: false),
                        deleter_user_id = c.Long(),
                        deletion_time = c.DateTime(precision: 0),
                        last_modification_time = c.DateTime(precision: 0),
                        last_modifier_user_id = c.Long(),
                        creation_time = c.DateTime(nullable: false, precision: 0),
                        creator_user_id = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VersionInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.qrtz_task", t => t.task_id, cascadeDelete: true)
                .Index(t => t.task_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.qrtz_version_info", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_temp_data", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_performance", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_performance", "node_id", "dbo.qrtz_node");
            DropForeignKey("dbo.qrtz_task", "node_id", "dbo.qrtz_node");
            DropForeignKey("dbo.qrtz_log", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_log", "node_id", "dbo.qrtz_node");
            DropForeignKey("dbo.qrtz_error", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_command", "task_id", "dbo.qrtz_task");
            DropForeignKey("dbo.qrtz_task", "category_id", "dbo.qrtz_category");
            DropForeignKey("dbo.qrtz_error", "node_id", "dbo.qrtz_node");
            DropForeignKey("dbo.qrtz_command", "node_id", "dbo.qrtz_node");
            DropIndex("dbo.qrtz_version_info", new[] { "task_id" });
            DropIndex("dbo.qrtz_temp_data", new[] { "task_id" });
            DropIndex("dbo.qrtz_performance", new[] { "task_id" });
            DropIndex("dbo.qrtz_performance", new[] { "node_id" });
            DropIndex("dbo.qrtz_log", new[] { "task_id" });
            DropIndex("dbo.qrtz_log", new[] { "node_id" });
            DropIndex("dbo.qrtz_task", new[] { "node_id" });
            DropIndex("dbo.qrtz_task", new[] { "category_id" });
            DropIndex("dbo.qrtz_error", new[] { "task_id" });
            DropIndex("dbo.qrtz_error", new[] { "node_id" });
            DropIndex("dbo.qrtz_command", new[] { "task_id" });
            DropIndex("dbo.qrtz_command", new[] { "node_id" });
            DropTable("dbo.qrtz_version_info",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VersionInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_temp_data",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TempData_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_performance",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Performance_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_log",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Log_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_task",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Task_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_error",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Error_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_node",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Node_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_command",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Command_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.qrtz_category",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Category_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
