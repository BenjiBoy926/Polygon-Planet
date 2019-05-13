using UnityEngine;
using System.Collections;

public static class DebugExt
{
    public static void DebugSpecifiedReport(string report, DebugInfoSpecification info)
    {
        if(info == DebugInfoSpecification.Report ||
            info == DebugInfoSpecification.ReportAndBreak)
        {
            Debug.Log(report);

            if(info == DebugInfoSpecification.ReportAndBreak)
            {
                Debug.Break();
            }
        }
    }
}
