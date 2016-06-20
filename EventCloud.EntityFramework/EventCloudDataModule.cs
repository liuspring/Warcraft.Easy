using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using TaskManager.EntityFramework;

namespace TaskManager
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(TaskManagerCoreModule))]
    public class TaskManagerDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
