using System;
using System.Collections.Generic;
using System.Text;
using static Core.Enums.Enumarations;

namespace Core.Extensions
{
    public static class StatusExtensions
    {
        public static string ToTaskStatus(this int val)
        {
            string returnVal = string.Empty;

            if (val == (int)TaskStatus.Open)
                returnVal = "Açık";
            else if (val == (int)TaskStatus.Assigned)
                returnVal = "Atandı";
            else if (val == (int)TaskStatus.Closed)
                returnVal = "Kapalı";
            else if (val == (int)TaskStatus.Waiting)
                returnVal = "Bekliyor";

            return returnVal;
        }

        public static string ToAssignStatus(this int val)
        {
            string returnVal = string.Empty;

            if (val == (int)AssignTaskStatus.Open)
                returnVal = "Açık";
            else if (val == (int)AssignTaskStatus.Assigned)
                returnVal = "Atandı";
            else if (val == (int)AssignTaskStatus.Closed)
                returnVal = "Kapalı";
            else if (val == (int)AssignTaskStatus.Waiting)
                returnVal = "Bekliyor";

            return returnVal;
        }
    }
}
