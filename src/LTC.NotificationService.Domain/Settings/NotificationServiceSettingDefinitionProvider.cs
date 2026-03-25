using Volo.Abp.Settings;

namespace LTC.NotificationService.Settings;

public class NotificationServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(NotificationServiceSettings.MySetting1));
    }
}
