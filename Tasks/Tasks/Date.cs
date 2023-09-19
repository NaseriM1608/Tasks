using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Tasks
{
    static class Time
    {
        public static void PrintDate()
        {
            Console.Clear();

            GregorianCalendar gc = new GregorianCalendar();
            if (gc.GetMinute(DateTime.Now) < 10)
            {
                Console.WriteLine(gc.GetMonth(DateTime.Now) + "/" + gc.GetDayOfMonth(DateTime.Now) + "/" + gc.GetYear(DateTime.Now) + " " + gc.GetHour(DateTime.Now) + ":0" + gc.GetMinute(DateTime.Now) + "\n");
            }

            else
            {
                Console.WriteLine(gc.GetMonth(DateTime.Now) + "/" + gc.GetDayOfMonth(DateTime.Now) + "/" + gc.GetYear(DateTime.Now) + " " + gc.GetHour(DateTime.Now) + ":" + gc.GetMinute(DateTime.Now) + "\n");
            }
        }
    }
}
