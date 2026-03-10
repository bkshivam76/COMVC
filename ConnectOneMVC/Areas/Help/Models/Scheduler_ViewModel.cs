using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    [Serializable]
    public class Scheduler_ViewModel
    {
        public string ActionMethod { get; set; }
        public string TitleX_Scheduler { get; set; }
        public string PopupName_Scheduler { get; set; }
        public string Scheduler_ID { get; set; }
        public string Cen_ID { get; set; }
        public string Scheduler_Instance_ID { get; set; }
        public string Universal_Scheduler_CenID { get; set; }
        public bool Universal_SchedulerWindow { get; set; }
        //This field is just for visibility in UI--It will bind with UI
        public bool Is_Universal_SchedulerWindow_Editable { get; set; }
        public DateTime? Info_LastEditedOn { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string Name_SchedulerWindow { get; set; }
        public string Prev_Name_SchedulerWindow { get; set; }
        public string ScheduleType_SchedulerWindow { get; set; }
        public int MappedChartCount_SchedulerWindow { get; set; }
        #region oneTimeOccurance
        public DateTime? Time_SchedulerWindow { get; set; }
        #endregion
        #region Recurring
        public string FrequencyType_SchedulerWindow { get; set; }
            #region DailyRecurrence
            public Int32? DailyRecurrenceEvery_SchedulerWindow { get; set; }
            #endregion
            #region WeeklyFrequency
            public Int32? WeeklyRecurrenceEvery_SchedulerWindow { get; set; }
            public Boolean Monday_SchedulerWindow { get; set; }
            public Boolean Tuesday_SchedulerWindow { get; set; }
            public Boolean Wednesday_SchedulerWindow { get; set; }
            public Boolean Thursday_SchedulerWindow { get; set; }
            public Boolean Friday_SchedulerWindow { get; set; }
            public Boolean Saturday_SchedulerWindow { get; set; }
            public Boolean Sunday_SchedulerWindow { get; set; }
            #endregion
            #region MonthlyFrequency
            public String MonthlyFrequencyType_SchedulerWindow { get; set; }
            //MonthlyFrequencyDateSpecific_SchedulerWindow>=1 and <=31 with interval of 1 mnth or it may be 2 month 
            public Int32? MonthlyFrequencyDateSpecific_SchedulerWindow { get; set; }
            //MonthlyFrequencyEveryMonthInterval_SchedulerWindow>=1. It should repeat after given interval in terms of Month
            //For e.g., Job must execute after every 2 month if entered value is 2 .
            //This field is common for both Day and DayNo as well as Dates of a Month 
            public Int32? MonthlyFrequencyEveryMonthInterval_SchedulerWindow { get; set; }
            //This will work alongside of WeekDay(field:MonthlyFrequencyEveryWeekdaySpecific_SchedulerWindow)
            //1st or 2nd or 3rd or 4rth or 5th of Every Monday(or any weekday) of month
            //public List<DayNo> MonthlyFrequencyMultipleDay_SchedulerWindow { get; set; }
            //every Monday(Includes all week days)
            public Int32? MonthlyFrequencyMultipleDay_SchedulerWindow { get; set; }
        public String MonthlyFrequencyEveryWeekdaySpecific_SchedulerWindow { get; set; }
            #endregion
            #region DayFrequency
            //Day Frequency
            public String DayFrequencyType_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyOccuranceAt_SchedulerWindow { get; set; }
                #region Once
                //It will be enabled for Random & Reccurring
                public Int32? DayFrequencyNoOfTimeBands_SchedulerWindow { get; set; }
                #endregion
            
                #region Recurring
                public Int32? DayFrequencyOccuranceEveryTB1_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB2_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB3_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB4_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB5_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB6_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB7_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB8_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB9_SchedulerWindow { get; set; }
                public Int32? DayFrequencyOccuranceEveryTB10_SchedulerWindow { get; set; }
                #endregion
        
                #region RandomOccurence
                public Int32? DayFrequencyRandomOccurenceNoTB1_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB2_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB3_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB4_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB5_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB6_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB7_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB8_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB9_SchedulerWindow { get; set; }
                public Int32? DayFrequencyRandomOccurenceNoTB10_SchedulerWindow { get; set; }

        #endregion
            public DateTime? DayFrequencyStartTimeTB1_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB1_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB2_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB2_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB3_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB3_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB4_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB4_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB5_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB5_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB6_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB6_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB7_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB7_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB8_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB8_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB9_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB9_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyStartTimeTB10_SchedulerWindow { get; set; }
            public DateTime? DayFrequencyEndTimeTB10_SchedulerWindow { get; set; }
        #endregion
        #endregion
        //Readonly Textarea field
        public String Summary_SchedulerWindow { get; set; }
    }
}