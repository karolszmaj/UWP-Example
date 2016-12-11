using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;

namespace FilckrClient.Utils.DeviceType
{
    public class DeviceTypeResolver
    {
        public static DeviceType ResolveDeviceType()
        {
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                {
                    return DeviceType.Mobile;
                }
                case "Windows.Desktop":
                {
                    return DeviceType.Desktop;
                }
                default:
                {
                    return DeviceType.Other;
                }
            }
        }
    }
}
