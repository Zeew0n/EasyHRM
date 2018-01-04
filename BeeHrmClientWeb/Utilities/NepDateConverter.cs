using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BeeHrmClientWeb.Utilities.Date
{
    public class NepDateConverter
    {
        private static IDictionary<int, int[]> bs;

        private static IDictionary<int, string> months;
        public NepDateConverter()
        {

        }
        private static int[] GetYears()
        {
            if (bs == null) InitializeData();
            return (int[])bs.Keys;
        }
        public static IDictionary<int, string> GetMonths()
        {
            if (bs == null) InitializeData();
            return months;
        }

        private static void InitializeData()
        {
            bs = new Dictionary<int, int[]>
                     {
                         {0, new int[] {2000, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {1, new int[] {2001, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {2, new int[] {2002, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {3, new int[] {2003, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {4, new int[] {2004, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {5, new int[] {2005, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {6, new int[] {2006, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {7, new int[] {2007, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {8, new int[] {2008, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31}},
                         {9, new int[] {2009, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {10, new int[] {2010, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {11, new int[] {2011, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {12, new int[] {2012, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30}},
                         {13, new int[] {2013, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {14, new int[] {2014, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {15, new int[] {2015, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {16, new int[] {2016, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30}},
                         {17, new int[] {2017, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {18, new int[] {2018, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {19, new int[] {2019, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {20, new int[] {2020, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {21, new int[] {2021, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {22, new int[] {2022, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30}},
                         {23, new int[] {2023, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {24, new int[] {2024, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {25, new int[] {2025, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {26, new int[] {2026, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {27, new int[] {2027, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {28, new int[] {2028, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {29, new int[] {2029, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30}},
                         {30, new int[] {2030, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {31, new int[] {2031, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {32, new int[] {2032, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {33, new int[] {2033, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {34, new int[] {2034, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {35, new int[] {2035, 30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31}},
                         {36, new int[] {2036, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {37, new int[] {2037, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {38, new int[] {2038, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {39, new int[] {2039, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30}},
                         {40, new int[] {2040, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {41, new int[] {2041, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {42, new int[] {2042, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {43, new int[] {2043, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30}},
                         {44, new int[] {2044, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {45, new int[] {2045, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {46, new int[] {2046, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {47, new int[] {2047, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {48, new int[] {2048, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {49, new int[] {2049, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30}},
                         {50, new int[] {2050, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {51, new int[] {2051, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {52, new int[] {2052, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {53, new int[] {2053, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30}},
                         {54, new int[] {2054, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {55, new int[] {2055, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {56, new int[] {2056, 31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30}},
                         {57, new int[] {2057, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {58, new int[] {2058, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {59, new int[] {2059, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {60, new int[] {2060, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {61, new int[] {2061, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {62, new int[] {2062, 30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31}},
                         {63, new int[] {2063, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {64, new int[] {2064, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {65, new int[] {2065, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {66, new int[] {2066, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31}},
                         {67, new int[] {2067, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {68, new int[] {2068, 31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {69, new int[] {2069, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {70, new int[] {2070, 31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30}},
                         {71, new int[] {2071, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {72, new int[] {2072, 31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30}},
                         {73, new int[] {2073, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31}},
                         {74, new int[] {2074, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {75, new int[] {2075, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {76, new int[] {2076, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30}},
                         {77, new int[] {2077, 31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31}},
                         {78, new int[] {2078, 31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {79, new int[] {2079, 31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30}},
                         {80, new int[] {2080, 31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30}},
                         {81, new int[] {2081, 31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30}},
                         {82, new int[] {2082, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30}},
                         {83, new int[] {2083, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30}},
                         {84, new int[] {2084, 31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30}},
                         {85, new int[] {2085, 31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30}},
                         {86, new int[] {2086, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30}},
                         {87, new int[] {2087, 31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30}},
                         {88, new int[] {2088, 30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30}},
                         {89, new int[] {2089, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30}},
                         {90, new int[] {2090, 30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30}}
                     };

            months = new Dictionary<int, string>();
            months.Add(1, "Baishak");
            months.Add(2, "Jestha");
            months.Add(3, "Ashad");
            months.Add(4, "Shrawn");
            months.Add(5, "Bhadra");
            months.Add(6, "Ashwin");
            months.Add(7, "kartik");
            months.Add(8, "Mangshir");
            months.Add(9, "Poush");
            months.Add(10, "Magh");
            months.Add(11, "Falgun");
            months.Add(12, "Chaitra");

        }

        public static bool IsLeapYear(int year)
        {
            int a = year;
            if (a == 0)
            {
                if (a % 400 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                if (a % 4 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static string GetNepaliMonth(int m)
        {
            return months[m];
        }



        private static string GetDayOfWeek(int d)
        {
            string day = "";
            switch (d)
            {
                case 1:
                    day = "Sunday";
                    break;

                case 2:
                    day = "Monday";
                    break;

                case 3:
                    day = "Tuesday";
                    break;

                case 4:
                    day = "Wednesday";
                    break;

                case 5:
                    day = "Thursday";
                    break;

                case 6:
                    day = "Friday";
                    break;

                case 7:
                    day = "Saturday";
                    break;
            }
            return day;
        }


        public static bool IsRangeEng(int yy, int mm, int dd)
        {
            if (yy < 1944 || yy > 2033)
            {
                Debug.WriteLine("Supported only between 1944-2022");
                return false;
            }

            if (mm < 1 || mm > 12)
            {
                Debug.WriteLine("Error! value 1-12 only");
                return false;
            }

            if (dd < 1 || dd > 31)
            {
                Debug.WriteLine("Error! value 1-31 only");
                return false;
            }

            return true;
        }

        private static bool IsRangeNep(int yy, int mm, int dd)
        {
            if (!IsYearRangeNep(yy))
            {
                return false;
            }
            if (mm < 1 || mm > 12)
            {
                Debug.WriteLine("Error! value 1-12 only");
                return false;
            }

            if (dd < 1 || dd > 32)
            {
                Debug.WriteLine("Error! value 1-31 only");
                return false;
            }
            return true;
        }
        public static bool IsYearRangeNep(int yy)
        {
            if (yy < 2000 || yy > 2089)
            {
                Debug.WriteLine("Supported only between 2000-2089");
                return false;
            }
            return true;
        }

        public static NepDate EngToNep(DateTime dateTime)
        {
            return EngToNep(dateTime.Year, dateTime.Month, dateTime.Day);
        }


        public static NepDate EngToNep(int yy, int mm, int dd)
        {
            if (bs == null) InitializeData();
            if (IsRangeEng(yy, mm, dd) == false)
            {
                return null;
            }
            else
            {

                // english month data.
                int[] month = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int[] lmonth = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

                int def_eyy = 1944;									//spear head english date...
                int def_nyy = 2000;
                int def_nmm = 9;
                int def_ndd = 17 - 1;		//spear head nepali date...
                int total_eDays = 0;
                int total_nDays = 0; int a = 0; int day = 7 - 1;		//all the initializations...
                int m = 0; int y = 0; int i = 0; int j = 0;
                int numDay = 0;

                // count total no. of days in-terms of year
                for (i = 0; i < (yy - def_eyy); i++)
                {	//total days for month calculation...(english)
                    if (IsLeapYear(def_eyy + i))
                        for (j = 0; j < 12; j++)
                            total_eDays += lmonth[j];
                    else
                        for (j = 0; j < 12; j++)
                            total_eDays += month[j];
                }

                // count total no. of days in-terms of month					
                for (i = 0; i < (mm - 1); i++)
                {
                    if (IsLeapYear(yy))
                        total_eDays += lmonth[i];
                    else
                        total_eDays += month[i];
                }

                // count total no. of days in-terms of date
                total_eDays += dd;


                i = 0; j = def_nmm;
                total_nDays = def_ndd;
                m = def_nmm;
                y = def_nyy;

                // count nepali date from array
                while (total_eDays != 0)
                {
                    a = bs[i][j];
                    total_nDays++;						//count the days
                    day++;								//count the days interms of 7 days
                    if (total_nDays > a)
                    {
                        m++;
                        total_nDays = 1;
                        j++;
                    }
                    if (day > 7)
                        day = 1;
                    if (m > 12)
                    {
                        y++;
                        m = 1;
                    }
                    if (j > 12)
                    {
                        j = 1; i++;
                    }
                    total_eDays--;
                }

                numDay = day;
                var nepDate = new NepDate();
                nepDate.Year = y;
                nepDate.Month = m;
                nepDate.Day = total_nDays;
                nepDate.WeekDayName = GetDayOfWeek(day);
                nepDate.MonthName = GetNepaliMonth(m);
                nepDate.WeekDay = numDay;
                return nepDate;
            }
        }


        public static DateTime NepToEng(NepDate dateTime)
        {
            return NepToEng(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static DateTime NepToEng(int yy, int mm, int dd)
        {


            if (bs == null) InitializeData();
            int def_eyy = 1943;
            int def_emm = 4;
            int def_edd = 14 - 1;		// init english date.
            int def_nyy = 2000; int def_nmm = 1; int def_ndd = 1;		// equivalent nepali date.
            int total_eDays = 0; int total_nDays = 0; int a = 0; int day = 4 - 1;		// initializations...
            int m = 0; int y = 0; int i = 0;
            int j = 0;
            int k = 0; int numDay = 0;

            int[] month = new int[] { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int[] lmonth = new int[] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (IsRangeNep(yy, mm, dd) == false)
            {
                return new DateTime();

            }
            else
            {

                // count total days in-terms of year
                for (i = 0; i < (yy - def_nyy); i++)
                {
                    for (j = 1; j <= 12; j++)
                    {
                        total_nDays += bs[k][j];
                    }
                    k++;
                }

                // count total days in-terms of month			
                for (j = 1; j < mm; j++)
                {
                    total_nDays += bs[k][j];
                }

                // count total days in-terms of dat
                total_nDays += dd;

                //calculation of equivalent english date...
                total_eDays = def_edd;
                m = def_emm;
                y = def_eyy;
                while (total_nDays != 0)
                {
                    if (IsLeapYear(y))
                    {
                        a = lmonth[m];
                    }
                    else
                    {
                        a = month[m];
                    }
                    total_eDays++;
                    day++;
                    if (total_eDays > a)
                    {
                        m++;
                        total_eDays = 1;
                        if (m > 12)
                        {
                            y++;
                            m = 1;
                        }
                    }
                    if (day > 7)
                        day = 1;
                    total_nDays--;
                }
                numDay = day;

                var date = new DateTime(y, m, total_eDays);
                return date;

            }
        }

    };
}