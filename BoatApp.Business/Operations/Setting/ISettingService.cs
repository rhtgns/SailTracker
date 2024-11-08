using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailTracker.Business.Operations.Setting
{
    public interface ISettingService
    {
        Task ToggleMaintenance(); // Yazım hatası düzeltildi
        bool GetMaintenanceState();
    }
}
