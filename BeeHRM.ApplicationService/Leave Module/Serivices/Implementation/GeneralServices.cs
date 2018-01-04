using English2NepaliDateConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Leave_Module.Serivices.Implementation
{
    public class GeneralServices
    {
        public static NepaliDate GetNepaliDate(DateTime enDate)
        {
            #region Core Algorithm for Nepali date conversion
            //Getting Nepali date data for Nepali date calculation
            int[] npDateData = NepaliDateDataArray.GetNepaliDateDataArray(enDate.Year);

            //Getting English day of the year
            int enDayOfYear = enDate.DayOfYear;

            //Initializing Nepali Year from the data
            int npYear = npDateData[0];

            //Initializing Nepali month to Poush (9)
            //This is because English date Jan 1 always fall in Poush month of Nepali Calendar, which is 9th month of Nepali calendar
            int npMonth = 9;

            //Initializing Nepali DaysInMonth with total days in the month of Poush
            int npDaysInMonth = npDateData[2];

            //Initializing temp nepali days
            //This is sum of total days in each Nepali month starting Jan 1 in Nepali month Poush
            //Note: for the month Poush, only counting days after Jan 1
            //***** This is the key field to calculate Nepali date *****
            int npTempDays = npDateData[2] - npDateData[1] + 1;

            //Looping through Nepali date data array to get exact Nepali month, Nepali year & Nepali daysInMonth information
            for (int i = 3; enDayOfYear > npTempDays; i++)
            {
                npTempDays += npDateData[i];
                npDaysInMonth = npDateData[i];
                npMonth++;

                if (npMonth > 12)
                {
                    npMonth -= 12;
                    npYear++;
                }
            }

            //Calculating Nepali day
            int npDay = npDaysInMonth - (npTempDays - enDayOfYear);

            #endregion

            #region Constructing and returning NepaliDate object
            //Returning back NepaliDate object with all the date details
            NepaliDate npDate = new NepaliDate();
            npDate.npDate = String.Format("{0}/{1}/{2}", npYear, npMonth, npDay);
            npDate.npYear = npYear;
            npDate.npMonth = npMonth;
            npDate.npDay = npDay;
            npDate.npDaysInMonth = npDaysInMonth;

            return npDate;
            #endregion
        }


    }
}
