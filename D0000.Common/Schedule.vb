'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Schedule"
    <Serializable>
    Public Class Schedule
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        <Serializable>
        Public Class Param_Insert_Schedule
            Public Sch_Name As String
            Public Sch_Type As String
            Public From_Time As String
            Public To_Time As String
            Public Freq_Type As String
            Public Daily_Recurrence As Integer
            Public Weekly_Recurrence As Integer
            Public Weekly_Monday As Boolean
            Public Weekly_Tuesday As Boolean
            Public Weekly_Wednesday As Boolean
            Public Weekly_Thursday As Boolean
            Public Weekly_Friday As Boolean
            Public Weekly_Saturday As Boolean
            Public Weekly_Sunday As Boolean
            Public Monthly_Type As String
            Public Monthly_Date As Integer
            Public Monthly_Day_No As Integer
            Public Monthly_Day As String
            Public Monthly_Recurrence As Integer
            Public Day_Freq_Type As String
            Public Day_Recurrence As Integer
            Public Random_Recurrence As Integer
            Public Summary As String
            Public Total_Timebands As Integer
            Public Day_Recurrence_2 As Integer
            Public Random_Recurrence_2 As Integer
            Public From_Time_2 As String
            Public To_Time_2 As String
            Public Day_Recurrence_3 As Integer
            Public Random_Recurrence_3 As Integer
            Public From_Time_3 As String
            Public To_Time_3 As String
            Public Day_Recurrence_4 As Integer
            Public Random_Recurrence_4 As Integer
            Public From_Time_4 As String
            Public To_Time_4 As String
            Public Day_Recurrence_5 As Integer
            Public Random_Recurrence_5 As Integer
            Public From_Time_5 As String
            Public To_Time_5 As String
            Public Day_Recurrence_6 As Integer
            Public Random_Recurrence_6 As Integer
            Public From_Time_6 As String
            Public To_Time_6 As String
            Public Day_Recurrence_7 As Integer
            Public Random_Recurrence_7 As Integer
            Public From_Time_7 As String
            Public To_Time_7 As String
            Public Day_Recurrence_8 As Integer
            Public Random_Recurrence_8 As Integer
            Public From_Time_8 As String
            Public To_Time_8 As String
            Public Day_Recurrence_9 As Integer
            Public Random_Recurrence_9 As Integer
            Public From_Time_9 As String
            Public To_Time_9 As String
            Public Day_Recurrence_10 As Integer
            Public Random_Recurrence_10 As Integer
            Public From_Time_10 As String
            Public To_Time_10 As String
            Public Is_Universal As Boolean
        End Class
        <Serializable>
        Public Class Param_Update_Schedule
            Public Sch_Name As String
            Public Sch_Type As String
            Public From_Time As String
            Public To_Time As String
            Public Freq_Type As String
            Public Daily_Recurrence As Integer
            Public Weekly_Recurrence As Integer
            Public Weekly_Monday As Boolean
            Public Weekly_Tuesday As Boolean
            Public Weekly_Wednesday As Boolean
            Public Weekly_Thursday As Boolean
            Public Weekly_Friday As Boolean
            Public Weekly_Saturday As Boolean
            Public Weekly_Sunday As Boolean
            Public Monthly_Type As String
            Public Monthly_Date As Integer
            Public Monthly_Day_No As Integer
            Public Monthly_Day As String
            Public Monthly_Recurrence As Integer
            Public Day_Freq_Type As String
            Public Day_Recurrence As Integer
            Public Random_Recurrence As Integer
            Public Summary As String
            Public Total_Timebands As Integer
            Public Day_Recurrence_2 As Integer
            Public Random_Recurrence_2 As Integer
            Public From_Time_2 As String
            Public To_Time_2 As String
            Public Day_Recurrence_3 As Integer
            Public Random_Recurrence_3 As Integer
            Public From_Time_3 As String
            Public To_Time_3 As String
            Public Day_Recurrence_4 As Integer
            Public Random_Recurrence_4 As Integer
            Public From_Time_4 As String
            Public To_Time_4 As String
            Public Day_Recurrence_5 As Integer
            Public Random_Recurrence_5 As Integer
            Public From_Time_5 As String
            Public To_Time_5 As String
            Public Day_Recurrence_6 As Integer
            Public Random_Recurrence_6 As Integer
            Public From_Time_6 As String
            Public To_Time_6 As String
            Public Day_Recurrence_7 As Integer
            Public Random_Recurrence_7 As Integer
            Public From_Time_7 As String
            Public To_Time_7 As String
            Public Day_Recurrence_8 As Integer
            Public Random_Recurrence_8 As Integer
            Public From_Time_8 As String
            Public To_Time_8 As String
            Public Day_Recurrence_9 As Integer
            Public Random_Recurrence_9 As Integer
            Public From_Time_9 As String
            Public To_Time_9 As String
            Public Day_Recurrence_10 As Integer
            Public Random_Recurrence_10 As Integer
            Public From_Time_10 As String
            Public To_Time_10 As String
            Public Schedule_ID As Integer
            Public Is_Universal As Boolean
        End Class
        Public Function Get_ScheduleListing() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_schedule_listing]"
            Dim params() As String = {"@CEN_ID"}
            Dim values() As Object = {cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {4}
            Return _RealService.ListFromSP(Tables.SCHEDULER_INFO, SPName, Tables.SCHEDULER_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
        End Function
        Public Function Get_ScheduleInstanceListing(ByVal Schedule_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_schedule_instance_listing]"
            Dim params() As String = {"@SCHEDULE_ID", "@CEN_ID"}
            Dim values() As Object = {Schedule_ID, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return _RealService.ListFromSP(Tables.SCHEDULER_INSTANCE_INFO, SPName, Tables.SCHEDULER_INSTANCE_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
        End Function
        Public Function Get_ScheduleQueueListing() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_queueOfScheduledJobs]"
            Dim params() As String = {"@CEN_ID"}
            Dim values() As Object = {cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {4}
            Return _RealService.ListFromSP(Tables.SCHEDULER_INSTANCE_QUEUE, SPName, Tables.SCHEDULER_INSTANCE_QUEUE.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
        End Function
        Public Function Get_ScheduleLogListing(ByVal Schedule_Instance_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_logOfScheduledJob]"
            Dim params() As String = {"@SCHEDULE_INSTANCE_ID"}
            Dim values() As Object = {Schedule_Instance_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {4}
            Return _RealService.ListFromSP(Tables.SCHEDULER_INSTANCE_LOG, SPName, Tables.SCHEDULER_INSTANCE_LOG.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
        End Function
        Public Function GetRecord_Schedule(ByVal Schedule_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Help_Scheduler)
            Dim query As String = "SELECT * FROM SCHEDULER_INFO WHERE REC_ID = " & Schedule_ID
            Return _RealService.List(Tables.SCHEDULER_INFO, query, Tables.SCHEDULER_INFO.ToString, inbasicparam)
        End Function
        Public Function GetRecord_ScheduleTimeBand(ByVal Schedule_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Help_Scheduler)
            Dim query As String = "SELECT * FROM SCHEDULER_TIMEBAND_INFO WHERE TB_SCHEDULE_ID = " & Schedule_ID
            Return _RealService.List(Tables.SCHEDULER_TIMEBAND_INFO, query, Tables.SCHEDULER_TIMEBAND_INFO.ToString, inbasicparam)
        End Function
        Public Function InsertSchedule(Inparam As Param_Insert_Schedule) As Integer
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_Insert_Schedule]"
            Dim params() As String = {"@SI_CEN_ID", "@SI_NAME", "@SI_TYPE", "@SI_FR_TIME", "@SI_TO_TIME", "@SI_FREQ_TYPE", "@SI_DAILY_RECURRENCE", "@SI_WEEKLY_RECURRENCE", "@SI_WEEKLY_MON", "@SI_WEEKLY_TUE", "@SI_WEEKLY_WED", "@SI_WEEKLY_THURS", "@SI_WEEKLY_FRI", "@SI_WEEKLY_SAT", "@SI_WEEKLY_SUN", "@SI_MONTHLY_TYPE", "@SI_MONTHLY_DATE", "@SI_MONTHLY_DAY_NO", "@SI_MONTHLY_DAY", "@SI_MONTHLY_RECURRENCE", "@SI_DAY_FREQ_TYPE", "@SI_DAY_RECURRENCE", "@SI_RANDOM_RECURRENCE", "@SI_SUMMARY", "@REC_ADD_BY", "@REC_EDIT_BY", "@SI_TOTAL_TIMEBANDS", "@SI_IS_UNIVERSAL", "@TB_DAY_RECURRENCE_2", "@TB_RANDOM_RECURRENCE_2", "@TB_FR_TIME_2", "@TB_TO_TIME_2", "@TB_DAY_RECURRENCE_3", "@TB_RANDOM_RECURRENCE_3", "@TB_FR_TIME_3", "@TB_TO_TIME_3", "@TB_DAY_RECURRENCE_4", "@TB_RANDOM_RECURRENCE_4", "@TB_FR_TIME_4", "@TB_TO_TIME_4", "@TB_DAY_RECURRENCE_5", "@TB_RANDOM_RECURRENCE_5", "@TB_FR_TIME_5", "@TB_TO_TIME_5", "@TB_DAY_RECURRENCE_6", "@TB_RANDOM_RECURRENCE_6", "@TB_FR_TIME_6", "@TB_TO_TIME_6", "@TB_DAY_RECURRENCE_7", "@TB_RANDOM_RECURRENCE_7", "@TB_FR_TIME_7", "@TB_TO_TIME_7", "@TB_DAY_RECURRENCE_8", "@TB_RANDOM_RECURRENCE_8", "@TB_FR_TIME_8", "@TB_TO_TIME_8", "@TB_DAY_RECURRENCE_9", "@TB_RANDOM_RECURRENCE_9", "@TB_FR_TIME_9", "@TB_TO_TIME_9", "@TB_DAY_RECURRENCE_10", "@TB_RANDOM_RECURRENCE_10", "@TB_FR_TIME_10", "@TB_TO_TIME_10"}
            Dim values() As Object = {cBase._open_Cen_ID, Inparam.Sch_Name, Inparam.Sch_Type, Inparam.From_Time, Inparam.To_Time, Inparam.Freq_Type, Inparam.Daily_Recurrence, Inparam.Weekly_Recurrence, Inparam.Weekly_Monday, Inparam.Weekly_Tuesday, Inparam.Weekly_Wednesday, Inparam.Weekly_Thursday, Inparam.Weekly_Friday, Inparam.Weekly_Saturday, Inparam.Weekly_Sunday, Inparam.Monthly_Type, Inparam.Monthly_Date, Inparam.Monthly_Day_No, Inparam.Monthly_Day, Inparam.Monthly_Recurrence, Inparam.Day_Freq_Type, Inparam.Day_Recurrence, Inparam.Random_Recurrence, Inparam.Summary, cBase._open_User_ID, cBase._open_User_ID, Inparam.Total_Timebands, Inparam.Is_Universal, Inparam.Day_Recurrence_2, Inparam.Random_Recurrence_2, Inparam.From_Time_2, Inparam.To_Time_2, Inparam.Day_Recurrence_3, Inparam.Random_Recurrence_3, Inparam.From_Time_3, Inparam.To_Time_3, Inparam.Day_Recurrence_4, Inparam.Random_Recurrence_4, Inparam.From_Time_4, Inparam.To_Time_4, Inparam.Day_Recurrence_5, Inparam.Random_Recurrence_5, Inparam.From_Time_5, Inparam.To_Time_5, Inparam.Day_Recurrence_6, Inparam.Random_Recurrence_6, Inparam.From_Time_6, Inparam.To_Time_6, Inparam.Day_Recurrence_7, Inparam.Random_Recurrence_7, Inparam.From_Time_7, Inparam.To_Time_7, Inparam.Day_Recurrence_8, Inparam.Random_Recurrence_8, Inparam.From_Time_8, Inparam.To_Time_8, Inparam.Day_Recurrence_9, Inparam.Random_Recurrence_9, Inparam.From_Time_9, Inparam.To_Time_9, Inparam.Day_Recurrence_10, Inparam.Random_Recurrence_10, Inparam.From_Time_10, Inparam.To_Time_10}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.Time, DbType.Time, DbType.String, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.String, DbType.Int32, DbType.Int32, DbType.String, DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time}
            Dim lengths() As Integer = {4, -1, 255, 5, 5, 255, 4, 4, 1, 1, 1, 1, 1, 1, 1, 255, 4, 4, 255, 4, 255, 4, 4, 8000, 255, 255, 4, 1, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5}
            Dim Schedule_Rec_ID As Integer = _RealService.InsertBySPPublic(Tables.SCHEDULER_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
            Return Schedule_Rec_ID
        End Function
        Public Function UpdateSchedule(UpParam As Param_Update_Schedule) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_UpdateScheduler]"
            Dim params() As String = {"@SI_CEN_ID", "@SI_NAME", "@SI_TYPE", "@SI_FR_TIME", "@SI_TO_TIME", "@SI_FREQ_TYPE", "@SI_DAILY_RECURRENCE", "@SI_WEEKLY_RECURRENCE", "@SI_WEEKLY_MON", "@SI_WEEKLY_TUE", "@SI_WEEKLY_WED", "@SI_WEEKLY_THURS", "@SI_WEEKLY_FRI", "@SI_WEEKLY_SAT", "@SI_WEEKLY_SUN", "@SI_MONTHLY_TYPE", "@SI_MONTHLY_DATE", "@SI_MONTHLY_DAY_NO", "@SI_MONTHLY_DAY", "@SI_MONTHLY_RECURRENCE", "@SI_DAY_FREQ_TYPE", "@SI_DAY_RECURRENCE", "@SI_RANDOM_RECURRENCE", "@SI_SUMMARY", "@REC_EDIT_BY", "@SI_TOTAL_TIMEBANDS", "@SI_IS_UNIVERSAL", "@TB_DAY_RECURRENCE_2", "@TB_RANDOM_RECURRENCE_2", "@TB_FR_TIME_2", "@TB_TO_TIME_2", "@TB_DAY_RECURRENCE_3", "@TB_RANDOM_RECURRENCE_3", "@TB_FR_TIME_3", "@TB_TO_TIME_3", "@TB_DAY_RECURRENCE_4", "@TB_RANDOM_RECURRENCE_4", "@TB_FR_TIME_4", "@TB_TO_TIME_4", "@TB_DAY_RECURRENCE_5", "@TB_RANDOM_RECURRENCE_5", "@TB_FR_TIME_5", "@TB_TO_TIME_5", "@TB_DAY_RECURRENCE_6", "@TB_RANDOM_RECURRENCE_6", "@TB_FR_TIME_6", "@TB_TO_TIME_6", "@TB_DAY_RECURRENCE_7", "@TB_RANDOM_RECURRENCE_7", "@TB_FR_TIME_7", "@TB_TO_TIME_7", "@TB_DAY_RECURRENCE_8", "@TB_RANDOM_RECURRENCE_8", "@TB_FR_TIME_8", "@TB_TO_TIME_8", "@TB_DAY_RECURRENCE_9", "@TB_RANDOM_RECURRENCE_9", "@TB_FR_TIME_9", "@TB_TO_TIME_9", "@TB_DAY_RECURRENCE_10", "@TB_RANDOM_RECURRENCE_10", "@TB_FR_TIME_10", "@TB_TO_TIME_10", "@SCHEDULE_ID"}
            Dim values() As Object = {cBase._open_Cen_ID, UpParam.Sch_Name, UpParam.Sch_Type, UpParam.From_Time, UpParam.To_Time, UpParam.Freq_Type, UpParam.Daily_Recurrence, UpParam.Weekly_Recurrence, UpParam.Weekly_Monday, UpParam.Weekly_Tuesday, UpParam.Weekly_Wednesday, UpParam.Weekly_Thursday, UpParam.Weekly_Friday, UpParam.Weekly_Saturday, UpParam.Weekly_Sunday, UpParam.Monthly_Type, UpParam.Monthly_Date, UpParam.Monthly_Day_No, UpParam.Monthly_Day, UpParam.Monthly_Recurrence, UpParam.Day_Freq_Type, UpParam.Day_Recurrence, UpParam.Random_Recurrence, UpParam.Summary, cBase._open_User_ID, UpParam.Total_Timebands, UpParam.Is_Universal, UpParam.Day_Recurrence_2, UpParam.Random_Recurrence_2, UpParam.From_Time_2, UpParam.To_Time_2, UpParam.Day_Recurrence_3, UpParam.Random_Recurrence_3, UpParam.From_Time_3, UpParam.To_Time_3, UpParam.Day_Recurrence_4, UpParam.Random_Recurrence_4, UpParam.From_Time_4, UpParam.To_Time_4, UpParam.Day_Recurrence_5, UpParam.Random_Recurrence_5, UpParam.From_Time_5, UpParam.To_Time_5, UpParam.Day_Recurrence_6, UpParam.Random_Recurrence_6, UpParam.From_Time_6, UpParam.To_Time_6, UpParam.Day_Recurrence_7, UpParam.Random_Recurrence_7, UpParam.From_Time_7, UpParam.To_Time_7, UpParam.Day_Recurrence_8, UpParam.Random_Recurrence_8, UpParam.From_Time_8, UpParam.To_Time_8, UpParam.Day_Recurrence_9, UpParam.Random_Recurrence_9, UpParam.From_Time_9, UpParam.To_Time_9, UpParam.Day_Recurrence_10, UpParam.Random_Recurrence_10, UpParam.From_Time_10, UpParam.To_Time_10, UpParam.Schedule_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String, DbType.Time, DbType.Time, DbType.String, DbType.Int32, DbType.Int32, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.Boolean, DbType.String, DbType.Int32, DbType.Int32, DbType.String, DbType.Int32, DbType.String, DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.Int32, DbType.Boolean, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32, DbType.Int32, DbType.Time, DbType.Time, DbType.Int32}
            Dim lengths() As Integer = {4, -1, 255, 5, 5, 255, 4, 4, 1, 1, 1, 1, 1, 1, 1, 255, 4, 4, 255, 4, 255, 4, 4, 8000, 255, 4, 1, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4, 4, 5, 5, 4}
            _RealService.UpdateBySPPublic(Tables.SCHEDULER_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
            Return True
        End Function
        Public Function DeleteSchedule(ByVal Schedule_ID As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Help_Scheduler)
            _RealService.DeleteByCondition(Tables.SCHEDULER_TIMEBAND_INFO, "TB_SCHEDULE_ID='" & Schedule_ID & "'", inbasicparam)
            _RealService.DeleteByCondition(Tables.SCHEDULER_INFO, "REC_ID ='" & Schedule_ID & "'", inbasicparam)
            Return True
        End Function
        Public Function Get_Schedules(Optional showOnlyRecurringSchedules As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_schedules]"
            Dim params() As String = {"@CEN_ID", "@SHOW_ONLY_RECURRING_SCHEDULES"}
            Dim values() As Object = {cBase._open_Cen_ID, showOnlyRecurringSchedules}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Boolean}
            Dim lengths() As Integer = {4, 1}
            Return _RealService.ListFromSP(Tables.SCHEDULER_INFO, SPName, Tables.SCHEDULER_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
        End Function
        Public Function InsertScheduleInstance(ByVal schedule_ID As Integer, Optional From_Date As String = Nothing, Optional To_Date As String = Nothing) As Integer
            Dim Schedule_Instance_ID As Integer
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_insert_schedule_instance]"
            Dim params() As String = {"@SII_SCHEDULE_ID", "@SII_CEN_ID", "@SII_FROM", "@SII_TO", "@REC_ADD_BY"}
            Dim values() As Object = {schedule_ID, cBase._open_Cen_ID, From_Date, To_Date, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.DateTime2, DbType.DateTime2, DbType.String}
            Dim lengths() As Integer = {4, 4, 7, 7, 255}
            Schedule_Instance_ID = _RealService.InsertBySPPublic(Tables.SCHEDULER_INSTANCE_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
            Return Schedule_Instance_ID
        End Function
        Public Function InsertMappingToScheduleInstance(ByVal scheduleInstanceID As Integer, ByVal jobID As Integer, ByVal jobType As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_insert_schedule_instance_mapping]"
            Dim params() As String = {"@SM_SCH_INSTANCE_ID", "@SM_JOB_ID", "@SM_JOB_TYPE", "@REC_ADD_BY"}
            Dim values() As Object = {scheduleInstanceID, jobID, jobType, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 255}
            _RealService.InsertBySPPublic(Tables.SCHEDULER_INSTANCE_MAPPING, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
            Return True
        End Function
        Public Function InsertScheduleInstanceLog(ByVal scheduleInstanceID As String, ByVal jobID As String, ByVal jobType As String, ByVal chartInstanceID As String, ByVal recAddBy As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_insert_schedule_instance_log]"
            Dim params() As String = {"@SL_SCH_INSTANCE_ID", "@SL_JOB_ID", "@SL_JOB_TYPE", "@SL_CHART_INSTANCE_ID", "REC_ADD_BY"}
            Dim values() As Object = {scheduleInstanceID, jobID, jobType, chartInstanceID, recAddBy}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {-1, -1, -1, -1, -1}
            _RealService.InsertBySPPublic(Tables.SCHEDULER_INSTANCE_LOG, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
            Return True
        End Function
        Public Function ExecuteScheduleInstanceQueue() As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_exec_schedule_instance_queue]"
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            'Return _RealService.ListFromSP(Tables.SCHEDULER_INSTANCE_QUEUE, SPName, Tables.SCHEDULER_INSTANCE_QUEUE.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
            Return _RealService.ListDatasetFromSP(Tables.SCHEDULER_INSTANCE_QUEUE, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
        End Function
        Public Function checkScheduleNameUniqueness(ByVal Schedule_Name As String) As Integer
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Help_Scheduler)
            Dim Query As String = "SELECT COUNT(*) FROM SCHEDULER_INFO WHERE (SI_CEN_ID = " & cBase._open_Cen_ID & " OR SI_IS_UNIVERSAL = 1) AND RTRIM(LTRIM(SI_NAME)) = '" & Schedule_Name & "'"
            Dim Count As Integer = _RealService.GetScalar(Tables.SCHEDULER_INFO, Query, Tables.SCHEDULER_INFO.ToString, inbasicparam)
            Return Count
        End Function
        Public Function checkJobsMappedToSchedule(ByVal Schedule_ID As String) As Integer
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Help_Scheduler)
            Dim Query As String = "SELECT COUNT(*) FROM SCHEDULER_INFO AS INFO INNER JOIN SCHEDULER_INSTANCE_INFO AS INST ON INFO.REC_ID = INST.SII_SCHEDULE_ID INNER JOIN SCHEDULER_INSTANCE_MAPPING AS MAP ON MAP.SM_SCH_INSTANCE_ID = INST.REC_ID WHERE INFO.REC_ID = " & Schedule_ID
            Dim Count As Integer = _RealService.GetScalar(Tables.SCHEDULER_INFO, Query, Tables.SCHEDULER_INFO.ToString, inbasicparam)
            Return Count
        End Function
        Public Function GetChartMappedToSchedule(ByVal Schedule_ID As String) As Integer
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Help_Scheduler)
            Dim Query As String = "SELECT COUNT(*) FROM SCHEDULER_INFO AS INFO INNER JOIN SCHEDULER_INSTANCE_INFO AS INST ON INFO.REC_ID = INST.SII_SCHEDULE_ID INNER JOIN SCHEDULER_INSTANCE_MAPPING AS MAP ON MAP.SM_SCH_INSTANCE_ID = INST.REC_ID WHERE SM_JOB_TYPE='CHART' AND INFO.REC_ID = " & Schedule_ID
            Dim Count As Integer = _RealService.GetScalar(Tables.SCHEDULER_INFO, Query, Tables.SCHEDULER_INFO.ToString, inbasicparam)
            Return Count
        End Function
        'The priority can be given to direct usage of the sp in the deletion sp of the mapped job. 
        Public Function DeleteScheduleInstanceAndMapping(ByVal Job_ID As String, ByVal Job_Type As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_delete_schedule_instance"
            Dim params() As String = {"@JOB_ID", "@JOB_TYPE"}
            Dim values() As Object = {Convert.ToInt32(Job_ID), Job_Type}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 255}
            _RealService.UpdateBySPPublic(Tables.SCHEDULER_INSTANCE_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Help_Scheduler))
            Return True
        End Function
    End Class
#End Region
End Class
