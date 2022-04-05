using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class GuidExtensions
    {
        public static string ToStringFromGuid(this Guid g)
        {
            return g.ToString().Replace("{", "").Replace("}", "").ToUpper();
        }
    }
}
