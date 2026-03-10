Imports System.Data
Imports Microsoft.VisualBasic
Namespace Real
    <Serializable>
    Public Class Projects
        <Serializable>
        Public Enum Project_Status
            _New
            Cancelled
            Rejected
            Changes_Recommended
            Requested
            Assigned_for_Estimation_Creation
            Submitted_for_Estimate_Approval
            Approved
            Assigned
            In_Progress
            Completed
        End Enum

#Region "Param Classes"
        <Serializable>
        Public Class Param_GetProjectRegister
            Public From_Date As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_Insert_Project_Txn
            Inherits Param_Insert_Project
            Public InAttachment As Attachments.Parameter_Insert_Attachment()
            Public InProjectEstimation As Param_Insert_Project_Estimation()
        End Class
        <Serializable>
        Public Class Param_Insert_Project
            Public Project_Name As String
            Public Project_Request_Date As DateTime
            Public Project_Complex_ID As String
            Public Project_Type_ID As String
            Public Santion_No As String
            Public Sanction_Date As DateTime?
            Public Project_Requestor_Main_Dept_ID As Integer?
            Public Project_Requestor_Sub_Dept_ID As Integer?
            'Public Project_Status As String
            Public Project_Req_Start_Date As DateTime?
            Public Project_Req_Finish_Date As DateTime?
            Public Project_Estimation_Date As DateTime?
            Public Project_Summary As String
            Public Project_Assignee_Main_Dept_ID As Integer?()
            Public Project_Assignee_Sub_Dept_ID As Integer?()
            Public Project_Engineer_ID As Integer?
            Public Project_Estimator_ID As Integer?
            Public Project_Actual_Start_Date As DateTime?
            Public Project_Actual_Finish_Date As DateTime?
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_Project_Estimation
            'Public Project_ID As Int32
            Public Description As String
            Public UnitID As String
            Public Estimated_Quantity As Decimal
            Public Estimated_Rate As Decimal
            Public Estimated_Amount As Decimal
        End Class
        <Serializable>
        Public Class Param_Update_Project_Status
            Public UpdatedStatus As Project_Status
            ''' <summary>
            ''' Filled when project is marked as Completed 
            ''' </summary>
            Public ProjectCompletionDate As DateTime?
            Public ProjectID As Int32
            Public SanctionNo As String
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Update_Project
            Public Project_Name As String
            Public Project_Request_Date As DateTime
            Public Project_Complex_ID As String
            Public Project_Type_ID As String
            Public Santion_No As String
            Public Sanction_Date As DateTime?
            Public Project_Requestor_Main_Dept_ID As Integer?
            Public Project_Requestor_Sub_Dept_ID As Integer?
            'Public Project_Status As String
            Public Project_Req_Start_Date As DateTime?
            Public Project_Req_Finish_Date As DateTime?
            Public Project_Estimation_Date As DateTime?
            Public Project_Summary As String
            Public Project_Assignee_Main_Dept_ID As Integer?()
            Public Project_Assignee_Sub_Dept_ID As Integer?()
            Public Project_Engineer_ID As Integer?
            Public Project_Estimator_ID As Integer?
            Public Project_Actual_Start_Date As DateTime?
            Public Project_Actual_Finish_Date As DateTime?
            ' Public Remarks As String
            Public ProjectID As Int32
        End Class
        <Serializable>
        Public Class PKs_Int
            ''' <summary>
            ''' Primary Key of Referred Record
            ''' </summary>
            Public Rec_ID As Int32
        End Class
        <Serializable>
        Public Class PKs_String
            ''' <summary>
            ''' Primary Key of Referred Record
            ''' </summary>
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Deleted_Attachments
            Inherits PKs_String
        End Class
        <Serializable>
        Public Class Param_Unlinked_Attachments
            Inherits PKs_String
        End Class
        <Serializable>
        Public Class Param_Deleted_Estimations
            Inherits PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_Remarks
            Inherits PKs_Int
        End Class
        <Serializable>
        Public Class Param_Update_Project_Estimation
            Inherits Param_Insert_Project_Estimation
            ''' <summary>
            ''' PKey of the Edited Estimation Row
            ''' </summary>
            Public Estimation_Rec_ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Project_Txn
            Inherits Param_Update_Project
            Public Remarks As String
            '==Additions
            ''' <summary>
            ''' Array of Attachments added during Update of Project
            ''' </summary>
            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            ''' <summary>
            ''' Array of Estimations added during Update of Project
            ''' </summary>
            Public Added_ProjectEstimations As Param_Insert_Project_Estimation()

            '==Updates
            ''' <summary>
            ''' Array of Documents updated during Update of Project
            ''' </summary>
            Public Updated_Attachments As Attachments.Parameter_Update_Attachment()
            ''' <summary>
            ''' Array of Estimations updated during Update of Project
            ''' </summary>
            Public Updated_ProjectEstimations As Param_Update_Project_Estimation()

            '==Deletions
            ''' <summary>
            ''' Array of Remarks Deleted during Update of Project
            ''' </summary>
            Public Deleted_ProjectRemarks As Param_Deleted_Remarks()
            ''' <summary>
            ''' Array of Documents Deleted during Update of Project
            ''' </summary>
            Public Deleted_ProjectAttachments As Param_Deleted_Attachments()
            ''' <summary>
            ''' Array of Documents unlinked from Project during Update of Project
            ''' </summary>
            Public Unlinked_ProjectAttachments As Param_Unlinked_Attachments()
            ''' <summary>
            ''' Array of Estimations Deleted during Update of Project
            ''' </summary>
            Public Deleted_ProjectEstimates As Param_Deleted_Estimations()
        End Class
        <Serializable>
        Public Class Param_GetStockDocuments
            Public RefID As Int32
            Public Screen_Type As String
        End Class
        <Serializable>
        Public Class Param_GetStockRemarks
            Inherits Param_GetStockDocuments
        End Class
        <Serializable>
        Public Class Param_GetStockPersonnels
            Public Skill_Type As String = Nothing
            Public Skill_Type_ID As String = Nothing
            Public Store_Dept_ID As Int32? = Nothing
            Public PersonnelType As String = Nothing
        End Class
        <Serializable>
        Public Class Param_InsertProjectRemarks
            Public Remarks As String
            Public Project_ID As Int32
        End Class
        <Serializable>
        Public Class Param_GetProjCnt_ForGivenSanctionNo_CurrInstt
            Public Sanction_No As String
            Public Project_ID As Int32?
        End Class
#End Region

        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_projects"
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetOpenProjectsList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_open_projects"
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetRegister(inParam As Param_GetProjectRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "get_project_Register"
            Dim params() As String = {"FromDate", "ToDate", "Logged_User_ID", "Cen_ID"}
            Dim values() As Object = {inParam.From_Date, inParam.ToDate, inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.PROJECT_INFO, SPName, ConnectOneWS.Tables.PROJECT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetProject_Open_Jobs_Count(ProjID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CASE WHEN (Select Count(REC_ID) from Job_info Where Job_Project_Id = " + ProjID.ToString() + ") > 0 THEN COUNT(JOB.REC_ID) Else 1 END FROM Job_info JOB INNER JOIN Project_Info PROJECT ON JOB.Job_Project_Id = PROJECT.REC_ID WHERE PROJECT.REC_ID = " + ProjID.ToString() + " AND JOB.Job_Status NOT IN ('COMPLETED','REJECTED','CANCELLED') "
            Return dbService.GetScalar(ConnectOneWS.Tables.PROJECT_INFO, Query, ConnectOneWS.Tables.PROJECT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetProjCnt_ForGivenSanctionNo_CurrInstt(inParam As Param_GetProjCnt_ForGivenSanctionNo_CurrInstt, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim Query As String = "SELECT COUNT(1) FROM Project_Info Prj INNER JOIN centre_info as CI on Prj.Proj_Cen_ID = CEN_ID WHERE Proj_Sanction_No = '" & inParam.Sanction_No & "' AND CEN_INS_ID = (SELECT CEN_INS_ID FROM centre_info WHERE CEN_ID = " & inBasicParam.openCenID & ")"
            'Dim Query As String = "Select COALESCE((Select COUNT(Prj.Proj_Sanction_No) PrjSancNo FROM Project_Info Prj INNER JOIN centre_info as CI on Prj.Proj_Cen_ID = CEN_ID Where Proj_Sanction_No = " + inParam.Sanction_No + " AND CEN_INS_ID = (SELECT CEN_INS_ID FROM centre_info WHERE CEN_ID = " & inBasicParam.openCenID & ") AND Prj.REC_ID <> ISNULL(" + inParam.Project_ID + ",0) ),0)"
            Dim Query As String = "Select COUNT(Prj.Proj_Sanction_No) PrjSancNo FROM Project_Info Prj INNER JOIN centre_info as CI on Prj.Proj_Cen_ID = CEN_ID Where Proj_Sanction_No = '" + inParam.Sanction_No + "' AND CEN_INS_ID = (SELECT CEN_INS_ID FROM centre_info WHERE CEN_ID = " & inBasicParam.openCenID.ToString() & ") AND Prj.REC_ID <> ISNULL(" + inParam.Project_ID.ToString() + ",0)"
            Return dbService.GetScalar(ConnectOneWS.Tables.PROJECT_INFO, Query, ConnectOneWS.Tables.PROJECT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetRecord(ProjID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT *    ,CONVERT(Varchar(255),STUFF((Select ', ' + Convert(Varchar(20),T.PA_Dept_ID) from Project_Assignee_Info T Where T.PA_Proj_ID = PROJECT.REC_ID and T.PA_Dept_Type = 'Main' For XML PATH('')),1,1,'')) AssigneeMainDept,Convert(Varchar(255),STUFF((Select ', ' + Convert(Varchar(20),T.PA_Dept_ID) from Project_Assignee_Info T Where T.PA_Proj_ID = PROJECT.REC_ID and T.PA_Dept_Type = 'Sub' For XML PATH('')),1,1,'')) AssigneeSubDept from Project_Info PROJECT WHERE PROJECT.REC_ID = " + ProjID.ToString() + ""
            Return dbService.GetSingleRecord(ConnectOneWS.Tables.PROJECT_INFO, Query, ConnectOneWS.Tables.PROJECT_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetProject_Jobs_Count(ProjID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "get_project_job_count"
            Dim params() As String = {"@Prj_ID"}
            Dim values() As Object = {ProjID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.JOB_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function


        ''commented as being replaced by storemaster.getdeptstore
        'Public Shared Function GetStockMainDept(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim SPName As String = "sp_get_Stock_Main_Dept"
        '    Dim params() As String = {}
        '    Dim values() As Object = {}
        '    Dim dbTypes() As System.Data.DbType = {}
        '    Dim lengths() As Integer = {}
        '    Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        'End Function

        'Public Shared Function GetStockSubDept(MainDeptID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim SPName As String = "sp_get_Stock_Sub_Dept"
        '    Dim params() As String = {"Main_Dept_ID"}
        '    Dim values() As Object = {MainDeptID}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
        '    Dim lengths() As Integer = {4}
        '    Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        'End Function

        Public Shared Function GetStockPersonnels(inparam As Param_GetStockPersonnels, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If inparam.Store_Dept_ID = 0 Then inparam.Store_Dept_ID = Nothing
            Dim SPName As String = "sp_get_Personnel_List"
            Dim params() As String = {"Skill_Type", "Skill_Type_ID", "STORE_DEPT_ID", "PersType", "@CEN_ID"}
            Dim values() As Object = {inparam.Skill_Type, inparam.Skill_Type_ID, inparam.Store_Dept_ID, inparam.PersonnelType, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 36, 4, 50, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, SPName, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetStockPersonnels(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Personnel_List"
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, SPName, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetStockDocuments(inParam As Param_GetStockDocuments, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Stock_Documents"
            Dim params() As String = {"Screen_Type", "Ref_ID"}
            Dim values() As Object = {inParam.Screen_Type, inParam.RefID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {50, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_DOCUMENT_INFO, SPName, ConnectOneWS.Tables.STOCK_DOCUMENT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetStockRemarks(inParam As Param_GetStockRemarks, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Stock_Remarks"
            Dim params() As String = {"Screen_Type", "Ref_ID"}
            Dim values() As Object = {inParam.Screen_Type, inParam.RefID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {50, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, ConnectOneWS.Tables.STOCK_REMARKS_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetProjectEstimates(ProjectID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Project_Estimation"
            Dim params() As String = {"@ProjectID"}
            Dim values() As Object = {ProjectID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.PROJECT_ESTIMATION, SPName, ConnectOneWS.Tables.PROJECT_ESTIMATION.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function InsertProjectTxn(inparam_Txn As Param_Insert_Project_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim inparam As New Param_Insert_Project
            inparam.Project_Actual_Finish_Date = inparam_Txn.Project_Actual_Finish_Date
            inparam.Project_Request_Date = inparam_Txn.Project_Request_Date
            inparam.Project_Complex_ID = inparam_Txn.Project_Complex_ID
            inparam.Project_Type_ID = inparam_Txn.Project_Type_ID
            inparam.Project_Name = inparam_Txn.Project_Name
            inparam.Santion_No = inparam_Txn.Santion_No
            inparam.Sanction_Date = inparam_Txn.Sanction_Date
            inparam.Project_Requestor_Main_Dept_ID = inparam_Txn.Project_Requestor_Main_Dept_ID
            inparam.Project_Requestor_Sub_Dept_ID = inparam_Txn.Project_Requestor_Sub_Dept_ID
            inparam.Project_Req_Start_Date = inparam_Txn.Project_Req_Start_Date
            inparam.Project_Req_Finish_Date = inparam_Txn.Project_Req_Finish_Date
            inparam.Project_Estimation_Date = inparam_Txn.Project_Estimation_Date
            inparam.Project_Summary = inparam_Txn.Project_Summary
            inparam.Project_Assignee_Main_Dept_ID = inparam_Txn.Project_Assignee_Main_Dept_ID
            inparam.Project_Assignee_Sub_Dept_ID = inparam_Txn.Project_Assignee_Sub_Dept_ID
            inparam.Project_Engineer_ID = inparam_Txn.Project_Engineer_ID
            inparam.Project_Actual_Start_Date = inparam_Txn.Project_Actual_Start_Date
            inparam.Project_Actual_Finish_Date = inparam_Txn.Project_Actual_Finish_Date
            inparam.Project_Estimator_ID = inparam_Txn.Project_Estimator_ID

            Dim ProjectID As String = InsertProject(inparam, inBasicParam)
            If Not inparam_Txn.Remarks = Nothing Then
                Dim inRemarks As New Param_InsertProjectRemarks
                inRemarks.Remarks = inparam_Txn.Remarks
                inRemarks.Project_ID = ProjectID
                InsertProjectRemarks(inRemarks, inBasicParam)
            End If

            If Not inparam_Txn.InAttachment Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In inparam_Txn.InAttachment
                    InAttachment.Ref_Rec_ID = ProjectID
                    InAttachment.Ref_Screen = "Projects"
                    Attachments.Insert(InAttachment, inBasicParam)
                Next
            End If

            If Not inparam_Txn.InProjectEstimation Is Nothing Then
                For Each InEstimation As Param_Insert_Project_Estimation In inparam_Txn.InProjectEstimation
                    InsertProjectEstimation(InEstimation, ProjectID, inBasicParam)
                Next
            End If

            Return ProjectID
        End Function

        Private Shared Function InsertProject(inparam As Param_Insert_Project, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Project_details"
            Dim params() As String = {"@UserID", "@Prj_CEN_ID", "@Prj_Yr_ID", "@Prj_Name", "@Prj_Req_Date", "@Complex_ID", "@Prj_Type_ID", "@Sanction_No", "@Sanction_Date", "@Req_Main_Dept_ID", "@Req_Sub_Dept_ID", "@Est_Date", "@Prj_Start_Date", "@Prj_Finish_Date", "@Prj_Engineer_ID", "@Prj_Summary", "@Prj_Act_Start", "@Prj_Act_Finish", "@Prj_Estimator_ID"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openCenID, inBasicParam.openYearID, inparam.Project_Name, inparam.Project_Request_Date, inparam.Project_Complex_ID, inparam.Project_Type_ID, inparam.Santion_No, inparam.Sanction_Date, inparam.Project_Requestor_Main_Dept_ID, inparam.Project_Requestor_Sub_Dept_ID, inparam.Project_Estimation_Date, inparam.Project_Req_Start_Date, inparam.Project_Req_Finish_Date, inparam.Project_Engineer_ID, inparam.Project_Summary, inparam.Project_Actual_Start_Date, inparam.Project_Actual_Finish_Date, inparam.Project_Estimator_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4, 255, 12, 36, 36, 50, 12, 4, 4, 12, 12, 12, 4, -1, 12, 12, 4}
            Dim ProjectID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.PROJECT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            InsertProjectAssignees(ProjectID, "Main", inparam.Project_Assignee_Main_Dept_ID, inBasicParam)
            InsertProjectAssignees(ProjectID, "Sub", inparam.Project_Assignee_Sub_Dept_ID, inBasicParam)

            Return ProjectID
        End Function

        Private Shared Function InsertProjectAssignees(ProjID As Int32, AssigneeType As String, DeptID As Integer?(), inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If AssigneeType = "Sub" And DeptID Is Nothing Then Return True 'Mantis bug 0000323 fixed

            For Each AssigneeMainDept As String In DeptID
                Dim SPName As String = "Insert_Project_Assignee_Info"
                Dim params() As String = {"@UserID", "@Assignee_Type", "@Assignee_Dept_Id", "@Proj_ID"}
                Dim values() As Object = {inBasicParam.openUserID, AssigneeType, AssigneeMainDept, ProjID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
                Dim lengths() As Integer = {255, 5, 4, 4}
                dbService.InsertBySP(ConnectOneWS.Tables.PROJECT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Next
            Return True
        End Function


        Public Shared Function InsertProjectRemarks(inparam As Param_InsertProjectRemarks, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, inparam.Project_ID, inparam.Remarks, "Projects"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Private Shared Function InsertProjectEstimation(inparam As Param_Insert_Project_Estimation, Project_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Project_Estimation"
            Dim params() As String = {"@UserID", "@Prj_ID", "@PE_Desc", "@PE_Est_Qty", "@PE_Est_Unit_ID", "@PE_Est_Rate", "@PE_Est_Amt"}
            Dim values() As Object = {inBasicParam.openUserID, Project_ID, inparam.Description, inparam.Estimated_Quantity, inparam.UnitID, inparam.Estimated_Rate, inparam.Estimated_Amount}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal}
            Dim lengths() As Integer = {255, 4, 8000, 19, 36, 19, 19}
            dbService.InsertBySP(ConnectOneWS.Tables.PROJECT_ESTIMATION, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function UpdateProjectStatus(inparam As Param_Update_Project_Status, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _Updated_Project_Status As String = ""
            Select Case inparam.UpdatedStatus
                Case Project_Status._New
                    _Updated_Project_Status = "New"
                Case Project_Status.Cancelled
                    _Updated_Project_Status = "Cancelled"
                Case Project_Status.Rejected
                    _Updated_Project_Status = "Rejected"
                Case Project_Status.Changes_Recommended
                    _Updated_Project_Status = "Changes Recommended"
                Case Project_Status.Requested
                    _Updated_Project_Status = "Requested"
                Case Project_Status.Assigned_for_Estimation_Creation
                    _Updated_Project_Status = "Assigned for Estimation Creation"
                Case Project_Status.Submitted_for_Estimate_Approval
                    _Updated_Project_Status = "Submitted for Estimate Approval"
                Case Project_Status.Approved
                    _Updated_Project_Status = "Approved"
                Case Project_Status.Assigned
                    _Updated_Project_Status = "Assigned"
                Case Project_Status.In_Progress
                    _Updated_Project_Status = "In-Progress"
                Case Project_Status.Completed
                    _Updated_Project_Status = "Completed"
            End Select
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_update_Project_Status"
            Dim params() As String = {"@ProjectID", "@UpdatedStatus", "@Proj_CompletionDate", "@Sanction_No", "@Remarks", "@Logged_In_User"}
            Dim values() As Object = {inparam.ProjectID, _Updated_Project_Status, inparam.ProjectCompletionDate, inparam.SanctionNo, inparam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 100, 12, 50, 8000, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.PROJECT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function UpdateProjectTxn(inparam_Txn As Param_Update_Project_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim inparam As New Param_Update_Project
            inparam.Project_Actual_Finish_Date = inparam_Txn.Project_Actual_Finish_Date
            inparam.Project_Request_Date = inparam_Txn.Project_Request_Date
            inparam.Project_Complex_ID = inparam_Txn.Project_Complex_ID
            inparam.Project_Type_ID = inparam_Txn.Project_Type_ID
            inparam.Project_Name = inparam_Txn.Project_Name
            inparam.Santion_No = inparam_Txn.Santion_No
            inparam.Sanction_Date = inparam_Txn.Sanction_Date
            inparam.Project_Requestor_Main_Dept_ID = inparam_Txn.Project_Requestor_Main_Dept_ID
            inparam.Project_Requestor_Sub_Dept_ID = inparam_Txn.Project_Requestor_Sub_Dept_ID
            inparam.Project_Req_Start_Date = inparam_Txn.Project_Req_Start_Date
            inparam.Project_Req_Finish_Date = inparam_Txn.Project_Req_Finish_Date
            inparam.Project_Estimation_Date = inparam_Txn.Project_Estimation_Date
            inparam.Project_Summary = inparam_Txn.Project_Summary
            inparam.Project_Assignee_Main_Dept_ID = inparam_Txn.Project_Assignee_Main_Dept_ID
            inparam.Project_Assignee_Sub_Dept_ID = inparam_Txn.Project_Assignee_Sub_Dept_ID
            inparam.Project_Engineer_ID = inparam_Txn.Project_Engineer_ID
            inparam.Project_Actual_Start_Date = inparam_Txn.Project_Actual_Start_Date
            inparam.Project_Actual_Finish_Date = inparam_Txn.Project_Actual_Finish_Date
            inparam.ProjectID = inparam_Txn.ProjectID
            inparam.Project_Estimator_ID = inparam_Txn.Project_Estimator_ID
            UpdateProject(inparam, inBasicParam)

            '==Additions in Child Records
            'Add Remarks 
            If Not inparam_Txn.Remarks = Nothing Then
                Dim inRemarks As New Param_InsertProjectRemarks
                inRemarks.Remarks = inparam_Txn.Remarks
                inRemarks.Project_ID = inparam_Txn.ProjectID
                InsertProjectRemarks(inRemarks, inBasicParam)
            End If
            'Add Attachment
            If Not inparam_Txn.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In inparam_Txn.Added_Attachments
                    InAttachment.Ref_Rec_ID = inparam_Txn.ProjectID
                    InAttachment.Ref_Screen = "Projects"
                    Attachments.Insert(InAttachment, inBasicParam)
                Next
            End If
            'Add Estimation
            If Not inparam_Txn.Added_ProjectEstimations Is Nothing Then
                For Each InEstimation As Param_Insert_Project_Estimation In inparam_Txn.Added_ProjectEstimations
                    InsertProjectEstimation(InEstimation, inparam_Txn.ProjectID, inBasicParam)
                Next
            End If
            '==Deletions in Child Records
            'Delete Remarks
            If Not inparam_Txn.Deleted_ProjectRemarks Is Nothing Then
                For Each delRemarks As Param_Deleted_Remarks In inparam_Txn.Deleted_ProjectRemarks
                    DeleteProjectRemarks(delRemarks.Rec_ID, inBasicParam)
                Next
            End If
            'Delete Attachment
            If Not inparam_Txn.Deleted_ProjectAttachments Is Nothing Then
                For Each delAttachment As Param_Deleted_Attachments In inparam_Txn.Deleted_ProjectAttachments
                    Attachments.Delete_Attachment_ByID(delAttachment.Rec_ID, inBasicParam)
                Next
            End If
            'Unlink Attachment 
            If Not inparam_Txn.Unlinked_ProjectAttachments Is Nothing Then
                For Each unlinkAttachment As Param_Unlinked_Attachments In inparam_Txn.Unlinked_ProjectAttachments
                    Dim unlinkParam As New Attachments.Parameter_Attachment_Unlink
                    unlinkParam.AttachmentID = unlinkAttachment.Rec_ID
                    unlinkParam.Ref_Rec_ID = inparam.ProjectID
                    Attachments.Unlink_Attachment(unlinkParam, inBasicParam)
                Next
            End If
            'Delete Estimation
            If Not inparam_Txn.Deleted_ProjectEstimates Is Nothing Then
                For Each delEstimates As Param_Deleted_Estimations In inparam_Txn.Deleted_ProjectEstimates
                    DeleteProjectEstimation(delEstimates.Rec_ID, inBasicParam)
                Next
            End If
            '==Updations in Child Records
            'Update Estimation
            If Not inparam_Txn.Updated_ProjectEstimations Is Nothing Then
                For Each updEstimates As Param_Update_Project_Estimation In inparam_Txn.Updated_ProjectEstimations
                    UpdateProjectEstimation(updEstimates, inBasicParam)
                Next
            End If
            'Update Estimation
            If Not inparam_Txn.Updated_Attachments Is Nothing Then
                For Each updAttachment As Attachments.Parameter_Update_Attachment In inparam_Txn.Updated_Attachments
                    Attachments.Update(updAttachment, inBasicParam)
                Next
            End If
            Return True
        End Function

        Private Shared Function UpdateProject(inparam As Param_Update_Project, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Project_details"
            Dim params() As String = {"@UserID", "@Prj_Name", "@Prj_Req_Date", "@Complex_ID", "@Prj_Type_ID", "@Sanction_No", "@Sanction_Date", "@Req_Main_Dept_ID", "@Req_Sub_Dept_ID", "@Est_Date", "@Prj_Start_Date", "@Prj_Finish_Date", "@Prj_Engineer_ID", "@Prj_Summary", "@Prj_Estimator_ID", "@Prj_ID"}
            Dim values() As Object = {inBasicParam.openUserID, inparam.Project_Name, inparam.Project_Request_Date, inparam.Project_Complex_ID, inparam.Project_Type_ID, inparam.Santion_No, inparam.Sanction_Date, inparam.Project_Requestor_Main_Dept_ID, inparam.Project_Requestor_Sub_Dept_ID, inparam.Project_Estimation_Date, inparam.Project_Req_Start_Date, inparam.Project_Req_Finish_Date, inparam.Project_Engineer_ID, inparam.Project_Summary, inparam.Project_Estimator_ID, inparam.ProjectID} 'Mantis bug 0000454 fixed.
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 255, 12, 36, 36, 50, 12, 4, 4, 12, 12, 12, 4, -1, 4, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.PROJECT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            dbService.DeleteByCondition(ConnectOneWS.Tables.PROJECT_ASSIGNEE_INFO, " PA_Proj_ID = " + inparam.ProjectID.ToString(), inBasicParam)

            InsertProjectAssignees(inparam.ProjectID, "Main", inparam.Project_Assignee_Main_Dept_ID, inBasicParam)
            InsertProjectAssignees(inparam.ProjectID, "Sub", inparam.Project_Assignee_Sub_Dept_ID, inBasicParam)
            Return True
        End Function

        Private Shared Function UpdateProjectEstimation(updParam As Param_Update_Project_Estimation, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_Project_Estimation"
            Dim params() As String = {"@Est_Rec_ID", "@PE_Desc", "@PE_Est_Qty", "@PE_Est_Unit_ID", "@PE_Est_Rate", "@PE_Est_Amt"}
            Dim values() As Object = {updParam.Estimation_Rec_ID, updParam.Description, updParam.Estimated_Quantity, updParam.UnitID, updParam.Estimated_Rate, updParam.Estimated_Amount}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal}
            Dim lengths() As Integer = {4, 8000, 19, 36, 19, 19}
            dbService.UpdateBySP(ConnectOneWS.Tables.PROJECT_ESTIMATION, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function DeleteProject(Project_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_Ref_ID = " + Project_ID.ToString() + " AND SR_Screen_Type = 'Projects'", inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.PROJECT_ESTIMATION, "PE_Proj_ID = " + Project_ID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.PROJECT_ASSIGNEE_INFO, " PA_Proj_ID = " + Project_ID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.PROJECT_INFO, Project_ID.ToString(), inBasicParam)

            'Unlink those Documents which are attached somewhere else, and delete those which are not linked elsewhere
            Dim inDocs As New Projects.Param_GetStockDocuments()
            inDocs.RefID = Project_ID
            inDocs.Screen_Type = "Projects"

            Dim _Docs_in_PO As DataTable = Projects.GetStockDocuments(inDocs, inBasicParam)
            For Each cRow As DataRow In _Docs_in_PO.Rows
                Dim inparam As New Attachments.Parameter_GetAttachmentLinkCount()
                inparam.RefRecordID = Project_ID
                inparam.RefScreen = "Projects"
                inparam.AttachmentID = cRow("ID")
                Dim LinkedScreenName As String = Attachments.GetAttachmentLinkCount(inparam, inBasicParam)
                If String.IsNullOrEmpty(LinkedScreenName) Then
                    Attachments.Delete_Attachment_ByReference(Project_ID, "Projects", inBasicParam)
                Else
                    Attachments.Unlink_Attachment_ByReference(Project_ID, "Projects", inBasicParam)
                End If
            Next

            Return True
        End Function

        Private Shared Function DeleteProjectEstimation(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.PROJECT_ESTIMATION, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProjectRemarks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_REMARKS_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class Jobs
#Region "Param Classes"
        <Serializable>
        Public Class Param_Update_Job_Status
            Public UpdatedStatus As Job_Status
            ''' <summary>
            ''' Filled when Job is marked as Completed 
            ''' </summary>
            Public JobCompletionDate As DateTime
            Public JobID As Int32
        End Class
        <Serializable>
        Public Class Param_GetJobRegister
            Public From_Date As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_Insert_Job_Txn
            Inherits Param_Insert_Job
            Public InAttachment As Attachments.Parameter_Insert_Attachment()
            Public InJobItemEstimation As Param_Insert_Job_ItemEstimation()
            Public InJobManpowerEstimation As Param_Insert_Job_ManpowerEstimation()
            Public InJobManpowerUsage As Param_Insert_Job_ManpowerUsage()
            Public InJobExpensesIncurred As Param_Insert_Job_ExpensesIncurred()
            Public InJobMachineUsage As Param_Insert_Job_MachineUsage()
        End Class
        <Serializable>
        Public Class Param_Insert_Job
            Public Job_Name As String
            Public Job_Request_Date As DateTime
            Public Job_Type As String
            Public Job_Project_Id As Int32?
            Public Job_Complex_Id As String
            'Public Job_Status As String
            Public Job_Requestor_Id As Int32
            Public Job_Assignee_Main_Dept_ID As Int32
            Public Job_Assignee_Sub_Dept_ID As Int32?()
            Public Job_Assignee_Id As Int32?
            Public Job_Requested_Start_Date As DateTime?
            Public Job_Requested_finish_Date As DateTime?
            Public Job_Start_Date As DateTime?
            Public Job_Completion_Date As DateTime?
            Public Job_Description As String
            Public Job_Estimate_Required As Boolean?
            Public Job_Budget_Limit As Decimal?
            Public Job_Requestor_Main_Dept_Id As Int32
            Public Job_Requestor_Sub_Dept_Id As Int32?
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_Job_ItemEstimation
            'Public Job_id As Int32
            Public sub_Item_ID As Int32
            Public Est_Qty As Decimal
            Public Est_Rate As Decimal
            Public Est_Amount As Decimal
            Public Est_Tolerance As Decimal
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_Job_ManpowerEstimation
            ' Public Job_id As Int32
            Public Manpower_Skill_ID As String
            Public Unit_ID As String
            Public Estimated_Qty As Decimal
            Public Est_Amount As Decimal
            Public Est_Rate As Decimal
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_Job_ManpowerUsage
            Public Job_ID As Int32
            Public Person_ID As Int32
            Public Period_From As DateTime
            Public Period_To As DateTime
            Public Units_Worked As Decimal
            Public Total_Amount As Decimal
            Public Charge_ID As Int32
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_InsertJobRemarks
            Public Remarks As String
            Public JobID As Int32
        End Class
        <Serializable>
        Public Class Param_Insert_Job_ExpensesIncurred
            Public Job_ID As Int32
            Public Exp_Tr_ID As String
            Public Exp_Tr_Sr_No As Int32
        End Class
        <Serializable>
        Public Class Param_Insert_Job_MachineUsage
            Public Job_ID As Int32
            Public Machine_ID As Int32
            Public Machine_Count As Int32
            Public Machine_Usage As Decimal
            Public Machine_Remarks As String
        End Class
        <Serializable>
        Public Class Param_Update_Job
            'Public JobID As Integer
            Public Job_Name As String
            Public Job_Request_Date As DateTime?
            Public Job_Type As String
            Public Job_Project_Id As Int32?
            Public Job_Complex_Id As String
            Public Job_Status As String
            Public Job_Requestor_Id As Int32?
            Public Job_Assignee_Main_Dept_ID As Int32?
            Public Job_Assignee_Sub_Dept_ID As Int32?()
            Public Job_Assignee_Id As Int32?
            Public Job_Requested_Start_Date As DateTime?
            Public Job_Requested_finish_Date As DateTime?
            Public Job_Start_Date As DateTime?
            Public Job_Completion_Date As DateTime?
            Public Job_Description As String
            Public Job_Estimate_Required As Boolean?
            Public Job_Budget_Limit As Decimal?
            Public Job_Requestor_Main_Dept_Id As Int32?
            Public Job_Requestor_Sub_Dept_Id As Int32?
        End Class
        <Serializable>
        Public Class Param_Deleted_Job_ItemEstimation
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_Job_ManpowerEstimation
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_Job_ManpowerUsage
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_Job_ExpensesIncurred
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_Job_MachineUsage
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Update_Job_Txn
            Inherits Param_Update_Job
            Public Remarks As String
            Public Job_ID As Integer

            '==Additions

            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            Public Added_JobItemEstimations As Param_Insert_Job_ItemEstimation()
            Public Added_JobManpowerEstimations As Param_Insert_Job_ManpowerEstimation()
            Public Added_JobManpowerUsage As Param_Insert_Job_ManpowerUsage()
            Public Added_JobExpensesIncurred As Param_Insert_Job_ExpensesIncurred()
            Public Added_JobMachineUsage As Param_Insert_Job_MachineUsage()

            '==Updates

            Public Updated_Attachments As Attachments.Parameter_Update_Attachment()
            Public Updated_JobItemEstimations As Param_Update_Job_ItemEstimation()
            Public Updated_JobManpowerEstimations As Param_Update_Job_ManpowerEstimation()
            Public Updated_JobManpowerUsage As Param_Update_Job_ManpowerUsage()
            Public Updated_JobMachineUsage As Param_Update_Job_MachineUsage()

            '==Deletions


            Public Deleted_Remarks As Projects.Param_Deleted_Remarks()

            Public Deleted_Attachments As Projects.Param_Deleted_Attachments()

            Public Unlinked_Attachments As Projects.Param_Unlinked_Attachments()

            Public Deleted_JobItemEstimations As Param_Deleted_Job_ItemEstimation()

            Public Deleted_JobManpowerEstimations As Param_Deleted_Job_ManpowerEstimation()
            Public Deleted_JobManpowerUsage As Param_Deleted_Job_ManpowerUsage()
            Public Deleted_JobExpensesIncurred As Param_Deleted_Job_ExpensesIncurred()
            Public Deleted_JobMachineUsage As Param_Deleted_Job_MachineUsage()
        End Class
        <Serializable>
        Public Class Param_Update_Job_ItemEstimation
            Inherits Param_Insert_Job_ItemEstimation
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Job_ManpowerEstimation
            Inherits Param_Insert_Job_ManpowerEstimation
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Job_ManpowerUsage
            Inherits Param_Insert_Job_ManpowerUsage
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Job_MachineUsage
            Inherits Param_Insert_Job_MachineUsage
            Public ID As Int32
        End Class

#End Region
        <Serializable>
        Public Enum Job_Status
            _New
            Cancelled
            Rejected
            Changes_Recommended
            Requested
            Assigned_for_Estimation_Creation
            Submitted_for_Estimate_Approval
            Approved
            Assigned
            In_Progress
            Completed
        End Enum

        Public Shared Function GetJob_UO_Count(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Count(UO_Job_ID) From User_Order Where UO_Job_ID = " + JobID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.USER_ORDER, Query, ConnectOneWS.Tables.USER_ORDER.ToString(), inBasicParam)
        End Function
        Public Shared Function GetJob_UO_Pending_Count(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Count(UO_Job_ID) From User_Order Where UO_Status != 'Completed' AND UO_Job_ID = " + JobID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.USER_ORDER, Query, ConnectOneWS.Tables.USER_ORDER.ToString(), inBasicParam)
        End Function
        Public Shared Function GetJob_Total_Usage_Count(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select  (Select COALESCE(Count(UO_Job_ID),0) From User_Order Where UO_Job_ID =  " + JobID.ToString() + " )  + (Select COALESCE(Count(JMch_Job_id),0) From Job_Machine_Usage Where JMch_Job_id =  " + JobID.ToString() + " ) + (Select COALESCE(Count(JM_Job_id),0) From Job_Manpower_Usage Where JM_Job_id =  " + JobID.ToString() + " ) + (Select COALESCE(Count(JE_Job_id),0) From Job_Expenses_Incurred Where JE_Job_id = " + JobID.ToString() + " ) AS NoPost_LinkedWithJob"
            Return dbService.GetScalar(ConnectOneWS.Tables.USER_ORDER, Query, ConnectOneWS.Tables.USER_ORDER.ToString(), inBasicParam)
        End Function
        Public Shared Function GetJob_RR_Count(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Count(RR_Job_ID) From Requisition_Request_Info Where RR_Job_ID = " + JobID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, Query, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetJob_RR_Pending_Count(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Count(RR_Job_ID) From Requisition_Request_Info Where RR_Status != 'Completed' AND  RR_Job_ID = " + JobID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, Query, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetJob_Project_Main_Assignee(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT PROJ.Proj_Assignee_Main_Dept_Id FROM Job_info JOB INNER JOIN Project_Info AS PROJ ON JOB.Job_Project_Id = PROJ.REC_ID WHERE JOB.REC_ID = " + JobID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.JOB_INFO, Query, ConnectOneWS.Tables.JOB_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetRecord(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT *,Convert(Varchar(255),STUFF((Select ', ' + Convert(Varchar(20),T.JA_Dept_ID) from Job_Assignee_Info T Where T.JA_Job_ID = JOB.REC_ID and T.JA_Dept_Type = 'Sub' For XML PATH('')),1,1,'')) AssigneeSubDept from Job_Info JOB WHERE JOB.REC_ID = " + JobID.ToString() + ""
            Return dbService.GetSingleRecord(ConnectOneWS.Tables.JOB_INFO, Query, ConnectOneWS.Tables.JOB_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetRegister(inParam As Param_GetJobRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Job_Register"
            Dim params() As String = {"FromDate", "ToDate", "Logged_User_ID", "Cen_ID"}
            Dim values() As Object = {inParam.From_Date, inParam.ToDate, inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.JOB_INFO, SPName, ConnectOneWS.Tables.JOB_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList(OpenJobsOnly As Boolean, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Jobs_List"
            Dim params() As String = {"@CEN_ID", "@Open_Job_Only"}
            Dim values() As Object = {inBasicParam.openCenID, OpenJobsOnly}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Boolean}
            Dim lengths() As Integer = {4, 2}
            Return dbService.ListFromSP(ConnectOneWS.Tables.JOB_INFO, SPName, ConnectOneWS.Tables.JOB_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetJobItemEstimates(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Job_Item_Est"
            Dim params() As String = {"@JobID"}
            Dim values() As Object = {JobID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.JOB_ITEM_ESTIMATION, SPName, ConnectOneWS.Tables.PROJECT_ESTIMATION.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetJobManpowerEstimates(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Job_ManpowerEst"
            Dim params() As String = {"@JobID"}
            Dim values() As Object = {JobID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.JOB_MANPOWER_ESTIMATION, SPName, ConnectOneWS.Tables.PROJECT_ESTIMATION.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetJobManpowerUsage(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Job_ManpowerUsage"
            Dim params() As String = {"@JobID"}
            Dim values() As Object = {JobID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.JOB_MANPOWER_USAGE, SPName, ConnectOneWS.Tables.PROJECT_ESTIMATION.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetJobMachineUsage(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Job_MachineUsage"
            Dim params() As String = {"@JobID"}
            Dim values() As Object = {JobID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.JOB_MACHINE_USAGE, SPName, ConnectOneWS.Tables.PROJECT_ESTIMATION.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetJobExpensesIncurred(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Job_ExpensesIncurred"
            Dim params() As String = {"@JobID"}
            Dim values() As Object = {JobID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.JOB_INFO, SPName, ConnectOneWS.Tables.JOB_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_Job_Expenses_For_Mapping(JobID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Expenses_For_Mapping"
            Dim params() As String = {"@RefID", "@CEN_ID", "@YEAR_ID", "@RefScreenType"}
            Dim values() As Object = {JobID, inBasicParam.openCenID, inBasicParam.openYearID, "Job"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 4, 50}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function InsertJobTxn(inparam_Txn As Param_Insert_Job_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim inparam As New Param_Insert_Job
            inparam.Job_Name = inparam_Txn.Job_Name
            inparam.Job_Request_Date = inparam_Txn.Job_Request_Date
            inparam.Job_Type = inparam_Txn.Job_Type
            inparam.Job_Project_Id = inparam_Txn.Job_Project_Id
            inparam.Job_Complex_Id = inparam_Txn.Job_Complex_Id
            'inparam.Job_Status = inparam_Txn.Job_Status
            inparam.Job_Requestor_Id = inparam_Txn.Job_Requestor_Id
            inparam.Job_Assignee_Main_Dept_ID = inparam_Txn.Job_Assignee_Main_Dept_ID
            inparam.Job_Assignee_Sub_Dept_ID = inparam_Txn.Job_Assignee_Sub_Dept_ID
            inparam.Job_Assignee_Id = inparam_Txn.Job_Assignee_Id
            inparam.Job_Requested_Start_Date = inparam_Txn.Job_Requested_Start_Date
            inparam.Job_Requested_finish_Date = inparam_Txn.Job_Requested_finish_Date
            inparam.Job_Start_Date = inparam_Txn.Job_Start_Date
            inparam.Job_Completion_Date = inparam_Txn.Job_Completion_Date
            inparam.Job_Description = inparam_Txn.Job_Description
            inparam.Job_Estimate_Required = inparam_Txn.Job_Estimate_Required
            inparam.Job_Budget_Limit = inparam_Txn.Job_Budget_Limit
            inparam.Job_Requestor_Main_Dept_Id = inparam_Txn.Job_Requestor_Main_Dept_Id
            inparam.Job_Requestor_Sub_Dept_Id = inparam_Txn.Job_Requestor_Sub_Dept_Id

            Dim JobID As String = InsertJob(inparam, inBasicParam)
            If Not inparam_Txn.Remarks = Nothing Then
                InsertJobRemarks(inparam_Txn.Remarks, JobID, inBasicParam)
            End If

            If Not inparam_Txn.InAttachment Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In inparam_Txn.InAttachment
                    InAttachment.Ref_Rec_ID = JobID
                    InAttachment.Ref_Screen = "Jobs"
                    Attachments.Insert(InAttachment, inBasicParam)
                Next
            End If

            If Not inparam_Txn.InJobItemEstimation Is Nothing Then
                For Each InEstimation As Param_Insert_Job_ItemEstimation In inparam_Txn.InJobItemEstimation
                    InsertJobItemEstimation(InEstimation, JobID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.InJobManpowerEstimation Is Nothing Then
                For Each InManEstimation As Param_Insert_Job_ManpowerEstimation In inparam_Txn.InJobManpowerEstimation
                    InsertJobManpowerEstimation(InManEstimation, JobID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.InJobManpowerUsage Is Nothing Then
                For Each InManUsage As Param_Insert_Job_ManpowerUsage In inparam_Txn.InJobManpowerUsage
                    InManUsage.Job_ID = JobID
                    InsertJobManpowerUsage(InManUsage, inBasicParam)
                Next
            End If

            If Not inparam_Txn.InJobExpensesIncurred Is Nothing Then
                For Each InExpIncurred As Param_Insert_Job_ExpensesIncurred In inparam_Txn.InJobExpensesIncurred
                    InExpIncurred.Job_ID = JobID
                    InsertJobExpensesIncurred(InExpIncurred, inBasicParam)
                Next
            End If

            If Not inparam_Txn.InJobMachineUsage Is Nothing Then
                For Each InMachineUsage As Param_Insert_Job_MachineUsage In inparam_Txn.InJobMachineUsage
                    InMachineUsage.Job_ID = JobID
                    InsertJobMachineUsage(InMachineUsage, inBasicParam)
                Next
            End If

            Return True
        End Function
        Private Shared Function InsertJob(inparam As Param_Insert_Job, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Job_Details"
            Dim params() As String = {"@Job_Cen_ID", "@Job_Year_ID", "@Job_Name", "@Job_Request_Date", "@Job_Type", "@Job_Project_Id", "@Job_Complex_Id", "@Job_Requestor_Id", "@Job_Assignee_Main_Dept_ID", "@Job_Assignee_Id", "@Job_Requested_Start_Date", "@Job_Requested_finish_Date", "@Job_Start_Date", "@Job_Completion_Date", "@Job_Description", "@Job_Estimate_Required", "@Job_Budget_Limit", "@Job_Requestor_Main_Dept_Id", "@Job_Requestor_Sub_Dept_Id", "@User_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inparam.Job_Name, inparam.Job_Request_Date, inparam.Job_Type, inparam.Job_Project_Id, inparam.Job_Complex_Id, inparam.Job_Requestor_Id, inparam.Job_Assignee_Main_Dept_ID, inparam.Job_Assignee_Id, inparam.Job_Requested_Start_Date, inparam.Job_Requested_finish_Date, inparam.Job_Start_Date, inparam.Job_Completion_Date, inparam.Job_Description, inparam.Job_Estimate_Required, inparam.Job_Budget_Limit, inparam.Job_Requestor_Main_Dept_Id, inparam.Job_Requestor_Sub_Dept_Id, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Decimal, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 12, 50, 4, 36, 4, 4, 4, 12, 12, 12, 12, 8000, 2, 19, 4, 4, 255}

            Dim JobID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.JOB_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            If Not inparam.Job_Assignee_Sub_Dept_ID Is Nothing Then
                InsertJobAssignees(JobID, "Sub", inparam.Job_Assignee_Sub_Dept_ID, inBasicParam)
            End If 'Mantis bug 0000438 fixed

            Return JobID
        End Function

        Private Shared Function InsertJobAssignees(JA_Job_ID As Int32, JA_Dept_Type As String, JA_Dept_ID As Integer?(), inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            For Each AssigneeSubDept As Integer In JA_Dept_ID
                Dim SPName As String = "Insert_Job_Assignee_Info"
                Dim params() As String = {"@UserID", "@JA_Dept_Type", "@JA_Dept_ID", "@JA_Job_ID"}
                Dim values() As Object = {inBasicParam.openUserID, JA_Dept_Type, AssigneeSubDept, JA_Job_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
                Dim lengths() As Integer = {255, 30, 4, 4}
                dbService.InsertBySP(ConnectOneWS.Tables.JOB_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Next

            Return True
        End Function 'Mantis bug 0000374 fixed
        Private Shared Function InsertJobItemEstimation(inparam As Param_Insert_Job_ItemEstimation, JobID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Job_Item_Est"
            Dim params() As String = {"@JIE_Job_id", "@JIE_sub_Item_ID", "@JIE_Est_Qty", "@JIE_Est_Rate", "@JIE_Est_Amount", "@JIE_Est_Tolerance", "@JIE_Remarks", "@UserID"}
            Dim values() As Object = {JobID, inparam.sub_Item_ID, inparam.Est_Qty, inparam.Est_Rate, inparam.Est_Amount, inparam.Est_Tolerance, inparam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 19, 19, 8000, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.JOB_ITEM_ESTIMATION, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function InsertJobManpowerEstimation(inparam As Param_Insert_Job_ManpowerEstimation, JobID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Job_Manpower_Est"
            Dim params() As String = {"@JME_Job_id", "@JME_Manpower_Skill", "@JME_Unit_ID", "@JME_Estimated_Qty", "@JME_Est_Amount", "@JME_Est_Rate", "@JME_Remarks", "@User_ID"}
            Dim values() As Object = {JobID, inparam.Manpower_Skill_ID, inparam.Unit_ID, inparam.Estimated_Qty, inparam.Est_Amount, inparam.Est_Rate, inparam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 36, 19, 19, 19, 8000, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.JOB_MANPOWER_ESTIMATION, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertJobRemarks(inParam As Param_InsertJobRemarks, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Return InsertJobRemarks(inParam.Remarks, inParam.JobID, inBasicParam)
        End Function
        Private Shared Function InsertJobRemarks(Remarks As String, JobID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, JobID, Remarks, "Jobs"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertJobManpowerUsage(inparam As Param_Insert_Job_ManpowerUsage, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Job_Manpower_Detail"
            Dim params() As String = {"@JM_Job_id", "@JM_Person_ID", "@JM_Period_From", "@JM_Period_To", "@JM_Units_Worked", "@JM_Total_Amount", "@JM_Remarks", "@User_ID", "@Pers_Charge_ID"}
            Dim values() As Object = {inparam.Job_ID, inparam.Person_ID, inparam.Period_From, inparam.Period_To, inparam.Units_Worked, inparam.Total_Amount, inparam.Remarks, inBasicParam.openUserID, inparam.Charge_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 12, 12, 19, 19, 8000, 255, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.JOB_MANPOWER_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertJobExpensesIncurred(inparam As Param_Insert_Job_ExpensesIncurred, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Job_Expenses"
            Dim params() As String = {"@JE_Job_id", "@JE_Exp_Tr_ID", "@JE_Exp_Tr_Sr_No", "@User_ID"}
            Dim values() As Object = {inparam.Job_ID, inparam.Exp_Tr_ID, inparam.Exp_Tr_Sr_No, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 4, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.JOB_EXPENSES_INCURRED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertJobMachineUsage(inparam As Param_Insert_Job_MachineUsage, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Job_Machine_Usage"
            Dim params() As String = {"@JMch_Job_id", "@JMch_Machine_ID", "@JMch_Machine_Count", "@JMch_Machine_Usage", "@JMch_Machine_Remarks", "@User_ID"}
            Dim values() As Object = {inparam.Job_ID, inparam.Machine_ID, inparam.Machine_Count, inparam.Machine_Usage, inparam.Machine_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 4, 19, 8000, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.JOB_MACHINE_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function UpdateJobStatus(inparam As Param_Update_Job_Status, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _Updated_Job_Status As String = ""
            Select Case inparam.UpdatedStatus
                Case Job_Status._New
                    _Updated_Job_Status = "New"
                Case Job_Status.Cancelled
                    _Updated_Job_Status = "Cancelled"
                Case Job_Status.Rejected
                    _Updated_Job_Status = "Rejected"
                Case Job_Status.Changes_Recommended
                    _Updated_Job_Status = "Changes Recommended"
                Case Job_Status.Requested
                    _Updated_Job_Status = "Requested"
                Case Job_Status.Assigned_for_Estimation_Creation
                    _Updated_Job_Status = "Assigned for Estimation Creation"
                Case Job_Status.Submitted_for_Estimate_Approval
                    _Updated_Job_Status = "Submitted for Estimate Approval"
                Case Job_Status.Approved
                    _Updated_Job_Status = "Approved"
                Case Job_Status.Assigned
                    _Updated_Job_Status = "Assigned"
                Case Job_Status.In_Progress
                    _Updated_Job_Status = "In-Progress"
                Case Job_Status.Completed
                    _Updated_Job_Status = "Completed"
            End Select

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Job_Status"
            Dim params() As String = {"@JobId", "@JobStatus", "@Logged_In_User"}
            Dim values() As Object = {inparam.JobID, _Updated_Job_Status, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 100, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.JOB_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            If inparam.UpdatedStatus = Job_Status.Completed And inparam.JobCompletionDate <> DateTime.MinValue Then
                Dim UpdateDateQuery As String = "UPDATE Job_info SET Job_Completion_Date ='" + inparam.JobCompletionDate.ToString(Common.Server_Date_Format_Long) + "' WHERE REC_ID = " + inparam.JobID.ToString()
                dbService.Update(ConnectOneWS.Tables.JOB_INFO, UpdateDateQuery, inBasicParam)
            End If
            Return True
        End Function
        Public Shared Function UpdateJobTxn(inparam_Txn As Param_Update_Job_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim inparam As New Param_Update_Job
            inparam.Job_Name = inparam_Txn.Job_Name
            inparam.Job_Request_Date = inparam_Txn.Job_Request_Date
            inparam.Job_Type = inparam_Txn.Job_Type
            inparam.Job_Project_Id = inparam_Txn.Job_Project_Id
            inparam.Job_Complex_Id = inparam_Txn.Job_Complex_Id
            inparam.Job_Status = inparam_Txn.Job_Status
            inparam.Job_Requestor_Id = inparam_Txn.Job_Requestor_Id
            inparam.Job_Assignee_Main_Dept_ID = inparam_Txn.Job_Assignee_Main_Dept_ID
            inparam.Job_Assignee_Sub_Dept_ID = inparam_Txn.Job_Assignee_Sub_Dept_ID
            inparam.Job_Assignee_Id = inparam_Txn.Job_Assignee_Id
            inparam.Job_Requested_Start_Date = inparam_Txn.Job_Requested_Start_Date
            inparam.Job_Requested_finish_Date = inparam_Txn.Job_Requested_finish_Date
            inparam.Job_Start_Date = inparam_Txn.Job_Start_Date
            inparam.Job_Completion_Date = inparam_Txn.Job_Completion_Date
            inparam.Job_Description = inparam_Txn.Job_Description
            inparam.Job_Estimate_Required = inparam_Txn.Job_Estimate_Required
            inparam.Job_Budget_Limit = inparam_Txn.Job_Budget_Limit
            inparam.Job_Requestor_Main_Dept_Id = inparam_Txn.Job_Requestor_Main_Dept_Id
            inparam.Job_Requestor_Sub_Dept_Id = inparam_Txn.Job_Requestor_Sub_Dept_Id

            Jobs.UpdateJob(inparam, inparam_Txn.Job_ID, inBasicParam)

            '==Additions in Child Records
            If Not inparam_Txn.Remarks = Nothing Then
                InsertJobRemarks(inparam_Txn.Remarks, inparam_Txn.Job_ID, inBasicParam)
            End If

            If Not inparam_Txn.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In inparam_Txn.Added_Attachments
                    InAttachment.Ref_Rec_ID = inparam_Txn.Job_ID
                    InAttachment.Ref_Screen = "Jobs"
                    Attachments.Insert(InAttachment, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Added_JobItemEstimations Is Nothing Then
                For Each InEstimation As Param_Insert_Job_ItemEstimation In inparam_Txn.Added_JobItemEstimations
                    InsertJobItemEstimation(InEstimation, inparam_Txn.Job_ID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Added_JobManpowerEstimations Is Nothing Then
                For Each InManEstimation As Param_Insert_Job_ManpowerEstimation In inparam_Txn.Added_JobManpowerEstimations
                    InsertJobManpowerEstimation(InManEstimation, inparam_Txn.Job_ID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Added_JobManpowerUsage Is Nothing Then
                For Each InManUsage As Param_Insert_Job_ManpowerUsage In inparam_Txn.Added_JobManpowerUsage
                    InManUsage.Job_ID = inparam_Txn.Job_ID
                    InsertJobManpowerUsage(InManUsage, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Added_JobExpensesIncurred Is Nothing Then
                For Each InExpIncurred As Param_Insert_Job_ExpensesIncurred In inparam_Txn.Added_JobExpensesIncurred
                    InExpIncurred.Job_ID = inparam_Txn.Job_ID
                    InsertJobExpensesIncurred(InExpIncurred, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Added_JobMachineUsage Is Nothing Then
                For Each InMachineUsage As Param_Insert_Job_MachineUsage In inparam_Txn.Added_JobMachineUsage
                    InMachineUsage.Job_ID = inparam_Txn.Job_ID
                    InsertJobMachineUsage(InMachineUsage, inBasicParam)
                Next
            End If

            '==Deletions in Child Records

            'Delete Remarks
            If Not inparam_Txn.Deleted_Remarks Is Nothing Then
                For Each delRemarks As Projects.Param_Deleted_Remarks In inparam_Txn.Deleted_Remarks
                    DeleteRemarks(delRemarks.Rec_ID, inBasicParam)
                Next
            End If
            'Delete Attachment
            If Not inparam_Txn.Deleted_Attachments Is Nothing Then
                For Each delAttachment As Projects.Param_Deleted_Attachments In inparam_Txn.Deleted_Attachments
                    Attachments.Delete_Attachment_ByID(delAttachment.Rec_ID, inBasicParam)
                Next
            End If
            'Unlink Attachment 
            If Not inparam_Txn.Unlinked_Attachments Is Nothing Then
                For Each unlinkAttachment As Projects.Param_Unlinked_Attachments In inparam_Txn.Unlinked_Attachments
                    Dim unlinkParam As New Attachments.Parameter_Attachment_Unlink
                    unlinkParam.AttachmentID = unlinkAttachment.Rec_ID
                    unlinkParam.Ref_Rec_ID = inparam_Txn.Job_ID
                    Attachments.Unlink_Attachment(unlinkParam, inBasicParam)
                Next
            End If
            'Delete Estimation


            If Not inparam_Txn.Deleted_JobItemEstimations Is Nothing Then
                For Each delEstimation As Param_Deleted_Job_ItemEstimation In inparam_Txn.Deleted_JobItemEstimations
                    DeleteJobItemEstimation(delEstimation.Rec_ID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Deleted_JobManpowerEstimations Is Nothing Then
                For Each delManEstimation As Param_Deleted_Job_ManpowerEstimation In inparam_Txn.Deleted_JobManpowerEstimations
                    DeleteJobManpowerEstimation(delManEstimation.Rec_ID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Deleted_JobManpowerUsage Is Nothing Then
                For Each delManUsage As Param_Deleted_Job_ManpowerUsage In inparam_Txn.Deleted_JobManpowerUsage
                    DeleteJobManpowerUsage(delManUsage.Rec_ID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Deleted_JobExpensesIncurred Is Nothing Then
                For Each delExpIncurred As Param_Deleted_Job_ExpensesIncurred In inparam_Txn.Deleted_JobExpensesIncurred
                    DeleteJobExpensesIncurred(delExpIncurred.Rec_ID, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Deleted_JobMachineUsage Is Nothing Then
                For Each delMachineUsage As Param_Deleted_Job_MachineUsage In inparam_Txn.Deleted_JobMachineUsage
                    DeleteJobMachineUsage(delMachineUsage.Rec_ID, inBasicParam)
                Next
            End If


            '==Updations in Child Records


            If Not inparam_Txn.Updated_Attachments Is Nothing Then
                For Each UpdAttachment As Attachments.Parameter_Update_Attachment In inparam_Txn.Updated_Attachments
                    Attachments.Update(UpdAttachment, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Updated_JobItemEstimations Is Nothing Then
                For Each UpdEstimation As Param_Update_Job_ItemEstimation In inparam_Txn.Updated_JobItemEstimations
                    UpdateJobItemEstimation(UpdEstimation, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Updated_JobManpowerEstimations Is Nothing Then
                For Each UpdManEstimation As Param_Update_Job_ManpowerEstimation In inparam_Txn.Updated_JobManpowerEstimations
                    UpdateJobManpowerEstimation(UpdManEstimation, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Updated_JobManpowerUsage Is Nothing Then
                For Each UpdManUsage As Param_Update_Job_ManpowerUsage In inparam_Txn.Updated_JobManpowerUsage
                    UpdateJobManpowerUsage(UpdManUsage, inBasicParam)
                Next
            End If

            If Not inparam_Txn.Updated_JobMachineUsage Is Nothing Then
                For Each UpdMachineUsage As Param_Update_Job_MachineUsage In inparam_Txn.Updated_JobMachineUsage
                    UpdateJobMachineUsage(UpdMachineUsage, inBasicParam)
                Next
            End If

            Return True
        End Function
        Private Shared Function UpdateJob(inparam As Param_Update_Job, JobID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Job_details"
            Dim params() As String = {"@JobID", "@Job_Name", "@Job_Request_Date", "@Job_Type", "@Job_Project_Id", "@Job_Complex_Id", "@Job_Requestor_Id", "@Job_Assignee_Main_Dept_ID", "@Job_Assignee_Id", "@Job_Requested_Start_Date", "@Job_Requested_finish_Date", "@Job_Start_Date", "@Job_Completion_Date", "@Job_Description", "@Job_Estimate_Required", "@Job_Budget_Limit", "@Job_Requestor_Main_Dept_Id", "@Job_Requestor_Sub_Dept_Id", "@User_ID"}
            Dim values() As Object = {JobID, inparam.Job_Name, inparam.Job_Request_Date, inparam.Job_Type, inparam.Job_Project_Id, inparam.Job_Complex_Id, inparam.Job_Requestor_Id, inparam.Job_Assignee_Main_Dept_ID, inparam.Job_Assignee_Id, inparam.Job_Requested_Start_Date, inparam.Job_Requested_finish_Date, inparam.Job_Start_Date, inparam.Job_Completion_Date, inparam.Job_Description, inparam.Job_Estimate_Required, inparam.Job_Budget_Limit, inparam.Job_Requestor_Main_Dept_Id, inparam.Job_Requestor_Sub_Dept_Id, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Decimal, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 12, 50, 4, 36, 4, 4, 4, 12, 12, 12, 12, 8000, 2, 19, 4, 4, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.JOB_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            dbService.DeleteByCondition(ConnectOneWS.Tables.JOB_ASSIGNEE_INFO, " JA_Job_ID  = " + JobID.ToString(), inBasicParam)
            If Not inparam.Job_Assignee_Sub_Dept_ID Is Nothing Then
                InsertJobAssignees(JobID, "Sub", inparam.Job_Assignee_Sub_Dept_ID, inBasicParam)
            End If

            Return True
        End Function 'Mantis bug 0000374 fixed
        Private Shared Function UpdateJobItemEstimation(inparam As Param_Update_Job_ItemEstimation, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Job_Item_Est"
            Dim params() As String = {"@JIE_ID", "@JIE_sub_Item_ID", "@JIE_Est_Qty", "@JIE_Est_Rate", "@JIE_Est_Amount", "@JIE_Est_Tolerance", "@JIE_Remarks"}
            Dim values() As Object = {inparam.ID, inparam.sub_Item_ID, inparam.Est_Qty, inparam.Est_Rate, inparam.Est_Amount, inparam.Est_Tolerance, inparam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 19, 19, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.JOB_ITEM_ESTIMATION, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateJobManpowerEstimation(inparam As Param_Update_Job_ManpowerEstimation, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Job_Manpower_Est"
            Dim params() As String = {"@JME_ID", "@JME_Manpower_Skill", "@JME_Unit_ID", "@JME_Estimated_Qty", "@JME_Est_Amount", "@JME_Est_Rate", "@JME_Remarks"}
            Dim values() As Object = {inparam.ID, inparam.Manpower_Skill_ID, inparam.Unit_ID, inparam.Estimated_Qty, inparam.Est_Amount, inparam.Est_Rate, inparam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 36, 19, 19, 19, 8000, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.JOB_MANPOWER_ESTIMATION, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateJobManpowerUsage(inparam As Param_Update_Job_ManpowerUsage, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Job_Manpower_Detail"
            Dim params() As String = {"@JM_ID", "@JM_Person_ID", "@JM_Period_From", "@JM_Period_To", "@JM_Units_Worked", "@JM_Total_Amount", "@JM_Remarks"}
            Dim values() As Object = {inparam.ID, inparam.Person_ID, inparam.Period_From, inparam.Period_To, inparam.Units_Worked, inparam.Total_Amount, inparam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 12, 12, 19, 19, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.JOB_MANPOWER_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateJobMachineUsage(inparam As Param_Update_Job_MachineUsage, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Job_Machine_Usage"
            Dim params() As String = {"@JMch_ID", "@JMch_Machine_ID", "@JMch_Machine_Count", "@JMch_Machine_Usage", "@JMch_Machine_Remarks"}
            Dim values() As Object = {inparam.ID, inparam.Machine_ID, inparam.Machine_Count, inparam.Machine_Usage, inparam.Machine_Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 4, 19, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.JOB_MACHINE_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function DeleteJob(Job_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_Ref_ID = " + Job_ID.ToString() + " AND SR_Screen_Type = 'Jobs'", inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.JOB_ITEM_ESTIMATION, "JIE_Job_id = " + Job_ID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.JOB_MANPOWER_ESTIMATION, "JME_Job_id = " + Job_ID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.JOB_MANPOWER_USAGE, "JM_Job_id = " + Job_ID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.JOB_EXPENSES_INCURRED, "JE_Job_id = " + Job_ID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.JOB_MACHINE_USAGE, "JMCH_Job_id = " + Job_ID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.JOB_ASSIGNEE_INFO, "JA_Job_ID  = " + Job_ID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.JOB_INFO, Job_ID.ToString(), inBasicParam)

            'Unlink those Documents which are attached somewhere else, and delete those which are not linked elsewhere
            Dim inDocs As New Projects.Param_GetStockDocuments()
            inDocs.RefID = Job_ID
            inDocs.Screen_Type = "Jobs"

            Dim _Docs_in_PO As DataTable = Projects.GetStockDocuments(inDocs, inBasicParam)
            For Each cRow As DataRow In _Docs_in_PO.Rows
                Dim inparam As New Attachments.Parameter_GetAttachmentLinkCount()
                inparam.RefRecordID = Job_ID
                inparam.RefScreen = "Jobs"
                inparam.AttachmentID = cRow("ID")
                Dim LinkedScreenName As String = Attachments.GetAttachmentLinkCount(inparam, inBasicParam)
                If String.IsNullOrEmpty(LinkedScreenName) Then
                    Attachments.Delete_Attachment_ByReference(Job_ID, "Jobs", inBasicParam)
                Else
                    Attachments.Unlink_Attachment_ByReference(Job_ID, "Jobs", inBasicParam)
                End If
            Next

            Return True
        End Function
        Private Shared Function DeleteJobItemEstimation(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.JOB_ITEM_ESTIMATION, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteJobManpowerEstimation(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.JOB_MANPOWER_ESTIMATION, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteJobManpowerUsage(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.JOB_MANPOWER_USAGE, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteJobExpensesIncurred(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.JOB_EXPENSES_INCURRED, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteJobMachineUsage(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.JOB_MACHINE_USAGE, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteRemarks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_REMARKS_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
    End Class
End Namespace