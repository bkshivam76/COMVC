using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class NoteBook_Grid
    {
        public string ITEM_NAME { get; set; }
        public string ITEM_LED_ID { get; set; }
        public string LED_NAME { get; set; }
        public string ITEM_TRANS_TYPE { get; set; }
        public string REC_ID { get; set; }
        public decimal? AMT_DT_01 { get; set; }
        public decimal? AMT_DT_02 { get; set; }
        public decimal? AMT_DT_03 { get; set; }
        public decimal? AMT_DT_04 { get; set; }
        public decimal? AMT_DT_05 { get; set; }
        public decimal? AMT_DT_06 { get; set; }
        public decimal? AMT_DT_07 { get; set; }
        public decimal? AMT_DT_08 { get; set; }
        public decimal? AMT_DT_09 { get; set; }
        public decimal? AMT_DT_10 { get; set; }
        public decimal? AMT_DT_11 { get; set; }
        public decimal? AMT_DT_12 { get; set; }
        public decimal? AMT_DT_13 { get; set; }
        public decimal? AMT_DT_14 { get; set; }
        public decimal? AMT_DT_15 { get; set; }
        public decimal? AMT_DT_16 { get; set; }
        public decimal? AMT_DT_17 { get; set; }
        public decimal? AMT_DT_18 { get; set; }
        public decimal? AMT_DT_19 { get; set; }
        public decimal? AMT_DT_20 { get; set; }
        public decimal? AMT_DT_21 { get; set; }
        public decimal? AMT_DT_22 { get; set; }
        public decimal? AMT_DT_23 { get; set; }
        public decimal? AMT_DT_24 { get; set; }
        public decimal? AMT_DT_25 { get; set; }
        public decimal? AMT_DT_26 { get; set; }
        public decimal? AMT_DT_27 { get; set; }
        public decimal? AMT_DT_28 { get; set; }
        public decimal? AMT_DT_29 { get; set; }
        public decimal? AMT_DT_30 { get; set; }
        public decimal? AMT_DT_31 { get; set; }
        public decimal? AMT_TOTAL { get; set; }

    }
    [Serializable]
    public class NoteBook_Info
    {     
        public int? Cmb_View_SelectedIndex { get; set; }
        public string ActionMethod { get; set; }      
        public bool Lock_unlockRight { get; set; }   
        public DateTime? xEntryDate { get; set; }
        public List<NoteBook_Period> PeriodData { get; set; }
        public List<NoteBook_Grid> NoteBookData { get; set; }
        public int TotalDays { get; set; }
        public string Periodtext { get; set; }
        public string GridBand2_Caption { get; set; }
        public string fr_date { get; set; }
    }
    [Serializable]
    public class NoteBook_Period
    {
        public int Index { get; set; }
        public string Period { get; set; }
    }
    [Serializable]
    public class NoteBook_ChangedValue 
    {
        public int? Value { get; set; }
        public string Key { get; set; }
        public string Field { get; set; }
    }
}