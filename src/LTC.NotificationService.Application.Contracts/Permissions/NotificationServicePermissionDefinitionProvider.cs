using LTC.NotificationService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace LTC.NotificationService.Permissions;

public class NotificationServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(NotificationServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(NotificationServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<NotificationServiceResource>(name);
    }
}
