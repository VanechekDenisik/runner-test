using System;
using Core.Common.Entities;

namespace Core.UI.Info
{
    public abstract class InfoComponent : EntityComponentWithConfig<EntityConfig>
    {
        public event Action OnUpdateInfo;
        
        public abstract string Header { get; }
        public abstract string[] Descriptions { get; }

        protected void UpdateInfo()
        {
            OnUpdateInfo?.Invoke();
        }
    }
}