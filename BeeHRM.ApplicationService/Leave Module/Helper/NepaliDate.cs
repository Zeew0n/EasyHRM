using System;
using System.Collections.Generic;
using System.Text;

namespace English2NepaliDateConversion
{
    /// <summary>
    /// NepaliDate - data object class
    /// </summary>
    public class NepaliDate
    {
        private string _nepaliDate;

        /// <summary>
        /// String representation of Nepali Date. Format yyyy/m/d
        /// </summary>
        public string npDate
        {
            get { return _nepaliDate; }
            set { _nepaliDate = value; }
        }

        private int _npDaysInMonth;

        /// <summary>
        /// DaysInMonth of Nepali date
        /// </summary>
        public int npDaysInMonth
        {
            get { return _npDaysInMonth; }
            set { _npDaysInMonth = value; }
        }

        private int _npYear;

        /// <summary>
        /// Numeric Year of Nepali date
        /// </summary>
        public int npYear
        {
            get { return _npYear; }
            set { _npYear = value; }
        }

        private int _npMonth;

        /// <summary>
        /// Numeric Month of Nepali date
        /// </summary>
        public int npMonth
        {
            get { return _npMonth; }
            set { _npMonth = value; }
        }

        private int _npDay;

        /// <summary>
        /// Numeric Day of Nepali date
        /// </summary>
        public int npDay
        {
            get { return _npDay; }
            set { _npDay = value; }
        }
    }
}
