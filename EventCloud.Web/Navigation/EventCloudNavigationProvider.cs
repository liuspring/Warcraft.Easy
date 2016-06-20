using Abp.Application.Navigation;
using Abp.Localization;

namespace EventCloud.Web.Navigation
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class EventCloudNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            //context.Manager.MainMenu
            //    .AddItem(
            //        new MenuItemDefinition(
            //            AppPageNames.Events,
            //            new LocalizableString("Events", EventCloudConsts.LocalizationSourceName),
            //            url: "#/",
            //            icon: "fa fa-calendar-check-o"
            //            )
            //    ).AddItem(
            //        new MenuItemDefinition(
            //            AppPageNames.About,
            //            new LocalizableString("About", EventCloudConsts.LocalizationSourceName),
            //            url: "#/about",
            //            icon: "fa fa-info"
            //            )
            //    );

            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "任务管理",
                        new LocalizableString("任务管理", EventCloudConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-calendar-check-o"
                        ).AddItem(new MenuItemDefinition(
                        "任务列表",
                        new LocalizableString("任务列表", EventCloudConsts.LocalizationSourceName),
                        url: "/task",
                        icon: "fa fa-calendar-check-o"
                        )).AddItem(new MenuItemDefinition(
                        "命令列表",
                        new LocalizableString("命令列表", EventCloudConsts.LocalizationSourceName),
                        url: "/command",
                        icon: "fa fa-calendar-check-o"
                        )).AddItem(new MenuItemDefinition(
                        "分类列表",
                        new LocalizableString("分类列表", EventCloudConsts.LocalizationSourceName),
                        url: "/category",
                        icon: "fa fa-calendar-check-o"
                        )).AddItem(new MenuItemDefinition(
                        "节点管理",
                        new LocalizableString("Events", EventCloudConsts.LocalizationSourceName),
                        url: "/node",
                        icon: "fa fa-calendar-check-o"
                        ))
                ).AddItem(
                    new MenuItemDefinition(
                        "用户管理",
                        new LocalizableString("用户管理", EventCloudConsts.LocalizationSourceName),
                        url: "",
                        icon: "fa fa-info"
                        ).AddItem(new MenuItemDefinition(
                        "用户列表",
                        new LocalizableString("用户列表", EventCloudConsts.LocalizationSourceName),
                        url: "/user/userlist",
                        icon: "fa fa-info"
                        ))
                );


        }
    }
}
