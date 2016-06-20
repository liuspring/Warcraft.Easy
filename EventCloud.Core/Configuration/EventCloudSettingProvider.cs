using System.Collections.Generic;
using Abp.Configuration;

namespace TaskManager.Configuration
{
    public class TaskManagerSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    TaskManagerSettingNames.MaxAllowedEventRegistrationCountInLast30DaysPerUser,
                    defaultValue: "10",
                    scopes: SettingScopes.Tenant),
            };
        }
    }
}
