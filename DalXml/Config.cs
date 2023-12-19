using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal static class Config
{
    static string s_data_config_xml = "data-config";
    internal static int NextDependencyId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextDependencyId"); }
    internal static int NextTaskId { get => XMLTools.GetAndIncreaseNextId(s_data_config_xml, "NextTaskId"); }


    internal static DateTime? dateBeginProject = XMLTools.LoadListFromXMLElement(@"data-config").ToDateTimeNullable("dateBeginProject");

    internal static DateTime? dateEndProject = XMLTools.LoadListFromXMLElement(@"data-config").ToDateTimeNullable("dateEndProject");

}

