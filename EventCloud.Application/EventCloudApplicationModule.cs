using System.Reflection;
using Abp.AutoMapper;
using Abp.Modules;

namespace TaskManager
{
    [DependsOn(typeof(TaskManagerCoreModule), typeof(AbpAutoMapperModule))]
    public class TaskManagerApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
