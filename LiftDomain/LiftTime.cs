using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using LiftCommon;

namespace LiftDomain
{
    public class LiftTime : BaseLiftDomain
    {
        public static DateTime CurrentTime
        {
            get
            {
                TimeZoneInfo tzi = null;

                DateTime result = DateTime.Now.ToUniversalTime();

                User u = User.Current;

                if (u != null)
                {
                    if (u.id > 0)
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById(u.time_zone);
                    }
                }

                if (tzi == null)
                {
                    Organization org = Organization.Current;

                    if (org != null && tzi == null)
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById(org.time_zone);
                    }
                }

                if (tzi != null)
                {
                    TimeSpan utcOffset = tzi.BaseUtcOffset;

                    if (tzi.IsDaylightSavingTime(result))
                    {
                        utcOffset = new TimeSpan(utcOffset.Hours + 1, 0, 0);
                    }

                    result = result.Add(utcOffset);
                }

                return result;
            }
        }

        public static TimeZoneInfo CurrentTimeZone
        {
            get
            {
                bool found = false;
                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time");

                User u = User.Current;

                if (u != null)
                {
                    if (u.id.Value > 0)
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById(u.time_zone);
                        found = true;
                    }
                }
                
                if (!found)
                {
                    Organization org = Organization.Current;

                    if (org != null)
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById(org.time_zone);
                    }
                }
                return tzi;
            }
        }
               
        public static DateTime UTC
        {
            get
            {
                DateTime result = DateTime.Now.ToUniversalTime();
                return result;
            }
        }

        public DataSet get_timezones()
        {
            DataSet result = new DataSet();
            DataTable tztable = result.Tables.Add("tz");
            tztable.Columns.Add("name", typeof(string));
            tztable.Columns.Add("id", typeof(string));

            foreach( TimeZoneInfo tzi in TimeZoneInfo.GetSystemTimeZones())
            {
                DataRow tz = tztable.NewRow();
                tz["name"] = tzi.DisplayName;
                tz["id"] = tzi.Id;
                tztable.Rows.Add(tz);
            }

            return result;
            
        }

        public static string aboutTime(DateTime userTime)
        {
            string result = string.Empty;
            string unit = string.Empty;
            double n = 0;
            DateTime current = LiftTime.CurrentTime;
            TimeSpan diff = current.Subtract(userTime);

            if (isBetween(diff.TotalDays, 365, double.MaxValue))
            {
                unit = "datetime.about_year";
                n = diff.TotalDays / 365.0;
            }
            else if (isBetween(diff.TotalDays, 31, 365))
            {
                unit = "datetime.about_month";
                n = diff.TotalDays / 31;
            }
            else if (isBetween(diff.TotalDays, 7, 31))
            {
                unit = "datetime.about_week";
                n = diff.TotalDays / 7;
            }
            else if (isBetween(diff.TotalDays, 1, 7))
            {
                unit = "datetime.about_day";
                n = diff.TotalDays;
            }
            else if (isBetween(diff.TotalHours, 1, 24))
            {
                unit = "datetime.about_hour";
                n = diff.TotalHours;
            }
            else if (isBetween(diff.TotalMinutes, 0, 60))
            {
                unit = "datetime.about_minute";
                n = diff.TotalMinutes;
            }

            int N = (int)Math.Round(n);

            if (N == 0) 
            {
                unit = "datetime.just_now";
            }
            else if (N > 1)
            {
                unit += "s";
            }

            result = Language.Current.phrase(unit);

            result = result.Replace("${N}", N.ToString() );

            return result;
        }

        protected static bool isBetween(double n, double lower, double upper)
        {
            return ((n >= lower) && (n < upper));
        }
        
        public static DateTime toUserTime(DateTime utcTime)
        {
            DateTime result = utcTime;
            TimeZoneInfo tzi = LiftTime.CurrentTimeZone;

            TimeSpan utcOffset = tzi.BaseUtcOffset;


            result = result.Add(utcOffset);
            return result;
        }
        
        
        public static DateTime fromUserTime(DateTime userTime)
        {
            DateTime result = userTime;
            TimeZoneInfo tzi = LiftTime.CurrentTimeZone;

            TimeSpan utcOffset = tzi.BaseUtcOffset;


            utcOffset = new TimeSpan(utcOffset.Hours * -1, 0, 0);

            result = result.Add(utcOffset);
            return result;
        }


        public static int UserTzOffset
        {
            get
            {
                TimeZoneInfo tzi = LiftTime.CurrentTimeZone;

                TimeSpan utcOffset = tzi.BaseUtcOffset;


                return utcOffset.Hours;
            }
        }
    }

    
}
