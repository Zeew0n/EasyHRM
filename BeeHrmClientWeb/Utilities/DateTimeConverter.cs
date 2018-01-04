using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeHrmClientWeb.Utilities
{
    public class DateTimeConverter
    {
        static int[] numbers = new int[]
        { 2000,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30,
                30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30
            };
        public static DateTime BStoAD(DateTime dtBS)
        {

            long totalNepDaysCount = 0;
            int refNepYear = 2000;

            int refEngYear = 1943;
            int refEngMonth = 04;
            int refEngDay = 14;
            int nepYear = Convert.ToInt32(dtBS.Year);
            int nepMonth = Convert.ToInt32(dtBS.Month);
            int nepDay = Convert.ToInt32(dtBS.Day);

            int diffYear = nepYear - refNepYear;
            int diffMonths = diffYear * 12 + (nepMonth - 1);
            int diffDays = nepDay - 1;
            int daystoAdd = 0;

            for (int i = 1; i <= diffMonths; i++)
            {
                daystoAdd += numbers[i];
            }
            totalNepDaysCount = daystoAdd + diffDays;

            DateTime refEngDate = new DateTime(refEngYear, refEngMonth, refEngDay, 0, 0, 0);
            DateTime AddDate = new DateTime(refEngDate.Year, refEngDate.Month, refEngDate.Day);
            AddDate = AddDate.AddDays(totalNepDaysCount);
            return AddDate;
        }

        public static string DateInBS(DateTime dtAD)
        {
            int[] numbers = new int[] { 2000,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31,
                31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30,
                31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30,
                31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30,
                30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30,
                30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30

            };
            //DateTime dtAD = new DateTime();
            //dtAD = DateTime.Now;
            //string weekday = dtAD.DayOfWeek.ToString();

            //string nepaliWeekday;
            //if (weekday == "Sunday")
            //    nepaliWeekday = "आइतबार";
            //else if (weekday == "Monday")
            //    nepaliWeekday = "सोमबार";
            //else if (weekday == "Tuesday")
            //    nepaliWeekday = "मंगलबार";
            //else if (weekday == "Wednesday")
            //    nepaliWeekday = "बुधबार";
            //else if (weekday == "Thursday")
            //    nepaliWeekday = "बिहीबार";
            //else if (weekday == "Friday")
            //    nepaliWeekday = "शुक्रबार";
            //else
            //    nepaliWeekday = "शनिबार";


            int totalNepDaysCount = 0;
            int refNepYear = 2000;

            int refEngYear = 1943;
            int refEngMonth = 04;
            int refEngDay = 14;

            DateTime refEngDate = new DateTime(refEngYear, refEngMonth, refEngDay, 0, 0, 0);
            TimeSpan t = (dtAD - refEngDate);
            int totalEnglishDaysCount = t.Days;

            int count = 1;

            do
            {
                totalNepDaysCount = totalNepDaysCount + (numbers[count]);
                if (totalNepDaysCount >= totalEnglishDaysCount) break;
                count++;

            } while (true);

            int nepYearCount = (count - 1) / 12;
            int nepaliYear = refNepYear + nepYearCount;
            //string nepaliYearFinal;
            //if (nepaliYear == 2069)
            //    nepaliYearFinal = "२०६९";
            //else if (nepaliYear == 2070)
            //    nepaliYearFinal = "२०७०";
            //else if (nepaliYear == 2071)
            //    nepaliYearFinal = "२०७१";
            //else if (nepaliYear == 2072)
            //    nepaliYearFinal = "२०७२";
            //else if (nepaliYear == 2073)
            //    nepaliYearFinal = "२०७३";
            //else if (nepaliYear == 2074)
            //    nepaliYearFinal = "२०७४";
            //else if (nepaliYear == 2075)
            //    nepaliYearFinal = "२०७५";
            //else if (nepaliYear == 2076)
            //    nepaliYearFinal = "२०७६";
            //else if (nepaliYear == 2077)
            //    nepaliYearFinal = "२०७७";
            //else if (nepaliYear == 2078)
            //    nepaliYearFinal = "२०७८";
            //else if (nepaliYear == 2079)
            //    nepaliYearFinal = "२०७९";
            //else if (nepaliYear == 2080)
            //    nepaliYearFinal = "२०८०";
            //else if (nepaliYear == 2081)
            //    nepaliYearFinal = "२०८१";
            //else if (nepaliYear == 2082)
            //    nepaliYearFinal = "२०८२";
            //else if (nepaliYear == 2083)
            //    nepaliYearFinal = "२०८३";
            //else if (nepaliYear == 2084)
            //    nepaliYearFinal = "२०८४";
            //else if (nepaliYear == 2085)
            //    nepaliYearFinal = "२०८५";
            //else if (nepaliYear == 2086)
            //    nepaliYearFinal = "२०८६";
            //else
            //    nepaliYearFinal = "ॐ";

            int nepMonth = count % 12;
            if (nepMonth == 0)
                nepMonth = 12;
            int nepDays = 0;
            for (int j = 1; j < count; j++)
            {
                nepDays += numbers[j];
            }
            double remNepDays = (totalEnglishDaysCount + 1) - nepDays;

            //string nepaliDayFinal;
            //if (remNepDays == 1)
            //    nepaliDayFinal = "०१";
            //else if (remNepDays == 2)
            //    nepaliDayFinal = "०२";
            //else if (remNepDays == 3)
            //    nepaliDayFinal = "०३";
            //else if (remNepDays == 4)
            //    nepaliDayFinal = "०४";
            //else if (remNepDays == 5)
            //    nepaliDayFinal = "०५";
            //else if (remNepDays == 6)
            //    nepaliDayFinal = "०६";
            //else if (remNepDays == 7)
            //    nepaliDayFinal = "०७";
            //else if (remNepDays == 8)
            //    nepaliDayFinal = "०८";
            //else if (remNepDays == 9)
            //    nepaliDayFinal = "०९";
            //else if (remNepDays == 10)
            //    nepaliDayFinal = "१०";
            //else if (remNepDays == 11)
            //    nepaliDayFinal = "११";
            //else if (remNepDays == 12)
            //    nepaliDayFinal = "१२";
            //else if (remNepDays == 13)
            //    nepaliDayFinal = "१३";
            //else if (remNepDays == 14)
            //    nepaliDayFinal = "१४";
            //else if (remNepDays == 15)
            //    nepaliDayFinal = "१५";
            //else if (remNepDays == 16)
            //    nepaliDayFinal = "१६";
            //else if (remNepDays == 17)
            //    nepaliDayFinal = "१७";
            //else if (remNepDays == 18)
            //    nepaliDayFinal = "१८";
            //else if (remNepDays == 19)
            //    nepaliDayFinal = "१९";
            //else if (remNepDays == 20)
            //    nepaliDayFinal = "२०";
            //else if (remNepDays == 21)
            //    nepaliDayFinal = "२१";
            //else if (remNepDays == 22)
            //    nepaliDayFinal = "२२";
            //else if (remNepDays == 23)
            //    nepaliDayFinal = "२३";
            //else if (remNepDays == 24)
            //    nepaliDayFinal = "२४";
            //else if (remNepDays == 25)
            //    nepaliDayFinal = "२५";
            //else if (remNepDays == 26)
            //    nepaliDayFinal = "२६";
            //else if (remNepDays == 27)
            //    nepaliDayFinal = "२७";
            //else if (remNepDays == 28)
            //    nepaliDayFinal = "२८";
            //else if (remNepDays == 29)
            //    nepaliDayFinal = "२९";
            //else if (remNepDays == 30)
            //    nepaliDayFinal = "३०";
            //else if (remNepDays == 31)
            //    nepaliDayFinal = "३१";
            //else if (remNepDays == 32)
            //    nepaliDayFinal = "३२";
            //else
            //    nepaliDayFinal = "ॐ";


            //string monthinbs;
            //if (nepMonth == 1)
            //    monthinbs = "बैशाख";
            //else if (nepMonth == 2)
            //    monthinbs = "जेष्ठ";
            //else if (nepMonth == 3)
            //    monthinbs = "आषाढ";
            //else if (nepMonth == 4)
            //    monthinbs = "श्रावन";
            //else if (nepMonth == 5)
            //    monthinbs = "भाद्र";
            //else if (nepMonth == 6)
            //    monthinbs = "असोज";
            //else if (nepMonth == 7)
            //    monthinbs = "कार्तिक";
            //else if (nepMonth == 8)
            //    monthinbs = "मंसिर";
            //else if (nepMonth == 9)
            //    monthinbs = "पौष";
            //else if (nepMonth == 10)
            //    monthinbs = "माघ";
            //else if (nepMonth == 11)
            //    monthinbs = "फागुन";
            //else
            //    monthinbs = "चैत्र";


            string dateinbs = nepaliYear.ToString() + "/" + nepMonth.ToString("00") + "/" + remNepDays.ToString("00");
            //DateTime returnDate = new DateTime(Convert.ToInt32(nepaliYear), Convert.ToInt32(nepMonth), Convert.ToInt32(remNepDays));
            return dateinbs;
        }

        public static DateTime ADtoBS(DateTime dtAD)
        {

            double totalNepDaysCount = 0;
            int refNepYear = 2000;

            int refEngYear = 1943;
            int refEngMonth = 04;
            int refEngDay = 14;
            DateTime refEngDate = new DateTime(refEngYear, refEngMonth, refEngDay, 0, 0, 0);

            double totalEnglishDaysCount = (dtAD - refEngDate).TotalDays;

            int count = 1;
            do
            {
                totalNepDaysCount = totalNepDaysCount + (numbers[count]);
                count++;

            } while (totalNepDaysCount >= totalEnglishDaysCount);

            int nepYearCount = count / 12;
            int nepaliYear = refNepYear + nepYearCount;
            int nepMonth = count % 12;
            int nepDays = 0;
            for (int j = 1; j == (count - 2); j++)
            {
                nepDays += numbers[j];
            }
            double remNepDays = totalNepDaysCount - nepDays;

            int nepDay = Convert.ToInt32(numbers[count - 1] - remNepDays);

            DateTime nepdate = new DateTime(nepaliYear, nepMonth, nepDay, 10, 10, 10);
            return nepdate;

        }
    }
}