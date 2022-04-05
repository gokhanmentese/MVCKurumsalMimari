using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Common
{
    public class CommonFunction
    {
        public static string RandomPasswordString()
        {
            Random rnd = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                int a = rnd.Next(2);
                switch (a)
                {
                    case 0:
                        char c = Convert.ToChar(65 + rnd.Next(26));
                        s += Convert.ToString(c);
                        break;
                    default:
                        s += rnd.Next(10).ToString();
                        break;
                }
            }
            return s;
        }
    }
}
