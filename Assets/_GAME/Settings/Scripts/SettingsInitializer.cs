using Core.Common.Initializers;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = SettingsAssetsPaths.Assets + "Addressable Initializer")]
    public class SettingsInitializer : Initializer
    {
        public override void Initialize()
        {
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 1;

            OnInitializationComplete();
        }
    }
}