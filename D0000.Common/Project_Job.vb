Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Job/Project--"
    <Serializable>
    Public Class Projects
        Inherits SharedVariables

#Region "Return Classes"
        ''' <summary>
        ''' Return class for GetList()
        ''' </summary>
        <Serializable>
        Public Class Return_GetList
            Public Project_Type As String
            Public Project_Name As String
            Public Sanction_no As String
            Public Sanction_Date As DateTime?
            Public Complex_Name As String
            Public Project_Id As Integer
            Public Complex_Id As String
        End Class
        <Serializable>
        Public Class Return_GetOpenProjectsList
            Public Project_Type As String
            Public Project_Name As String
            Public Sanction_no As String
            Public Sanction_Date As DateTime?
            Public Complex_Name As String
            Public Project_Id As Integer
            Public Complex_Id As String
            Public Project_Status As String
        End Class
        ''' <summary>
        ''' Return Class for GetRegister
        ''' </summary>
        <Serializable>
        Public Class Return_GetRegister
            Public Property Project_Name As String
            Public Property Complex_Name As String
            Public Property Sanction_No As String
            Public Property Project_Type As String
            Public Property Request_Date As DateTime
            Public Property Req_Main_Dept As String
            Public Property Req_Sub_Dept As String
            Public Property Assignee_Main_Dept As String
            Public Property Assignee_Sub_Dept As String
            Public Property Project_Status As String
            Public Property Estimated_Cost As Decimal
            Public Property Actual_Cost As Decimal
            Public Property Estimation_Date As DateTime?
            Public Property Sanction_Date As DateTime?
            Public Property Start_Date As DateTime?
            Public Property Finish_Date As DateTime?
            Public Property Actual_Start_Date As DateTime?
            Public Property Actual_Finish_Date As DateTime?
            Public Property Proj_Summary As String
            Public Property Rec_Add_On As DateTime
            Public Property Rec_Add_By As String
            Public Property Rec_Edit_On As DateTime
            Public Property Rec_Edit_By As String
            Public Property ID As Integer
            Public Property CurrUserRole As String
            Public Property Proj_Engineer As String
            Public Property Proj_Estimator As String
            Public Property Proj_Approver As String
        End Class
        <Serializable>
        Public Class Return_GetProjTypes
            Public Project_Type As String
            Public Project_Type_ID As String
        End Class
        'Public Class Return_GetStockMainDept
        '    Inherits StockDeptStores.Return_GetStoreDept
        'End Class
        'Public Class Return_GetStockSubDept
        '    Inherits StockDeptStores.Return_GetStoreDept
        'End Class
        <Serializable>
        Public Class Return_GetProjectEnginners
            Public Name As String
            Public Type As String
            Public Main_Dept As String
            Public Sub_Dept As String
            Public ID As Integer
            Public Mobile_No As String

            Public Property Skill_Type As String
            Public Property Joining_Date As DateTime? 'Mantis bug 0001045 resolved
            Public Property Leaving_Date As DateTime? 'Mantis bug 0001045 resolved
        End Class
        <Serializable>
        Public Class Return_GetProjectEstimators
            Inherits Return_GetProjectEnginners
        End Class
        <Serializable>
        Public Class Return_GetProjectRemarks
            Public Property Sr_No As Int64
            Public Property Remarks As String
            Public Property Remarks_By As String
            Public Property Remarks_By_Designation As String
            Public Property ID As Integer
            Public Property Added_On As DateTime
        End Class
        <Serializable>
        Public Class Return_GetProjectEstimation
            Public Property Sr_No As Int64
            Public Property Description As String
            Public Property Estimated_Qty As Decimal
            Public Property Unit As String
            Public Property Rate As Decimal
            Public Property Total_Amount As Decimal
            Public Property ID As Integer
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property UnitID As String
        End Class
        <Serializable>
        Public Class Return_GetRecord_Project
            Public Proj_Name As String
            Public Proj_Request_Date As Date
            Public Proj_Sanction_No As String
            Public Proj_Sanction_Date As Date?
            Public Proj_Type_ID As String
            Public Proj_Complex_Id As String
            Public Proj_Requestor_Main_Dept As Integer?
            Public Proj_Requestor_Sub_Dept As Integer?
            Public Proj_Status As String
            Public Proj_Start_Date As Date?
            Public Proj_Finish_Date As Date?
            Public Proj_Estimation_Date As Date?
            Public Proj_Summary As String
            Public REC_ADD_ON As Date
            Public REC_ADD_BY As String
            Public REC_EDIT_BY As String
            Public REC_EDIT_ON As Date
            Public REC_ID As Integer
            Public Proj_Assignee_Main_Dept_Id As String '//Mantis bug 0000314 fixed
            Public Proj_Assignee_Sub_Dept_Id As String '//Mantis bug 0000314 fixed
            Public Proj_Engineer_ID As Integer?
            Public Proj_Actual_Start As Date?
            Public Proj_Actual_Finish As Date?
            Public Proj_Estimator_ID As Integer?
            Public Proj_Approver_ID As Integer?
            Public AssigneeMainDept As String '//Mantis bug 0000314 fixed
            Public AssigneeSubDept As String '//Mantis bug 0000314 fixed

        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' GetList without param - Load data in Project Dropdown
        ''' </summary>
        ''' <returns></returns>
        Public Function GetRegister(fromDate As DateTime, toDate As DateTime) As List(Of Return_GetRegister)
            Dim InParam As New Param_GetProjectRegister()
            InParam.From_Date = fromDate
            InParam.ToDate = toDate
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetRegister, ClientScreen.Stock_Project, InParam)
            Dim _Projects_data As List(Of Return_GetRegister) = New List(Of Return_GetRegister)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetRegister
                    newdata.Project_Name = row.Field(Of String)("Prj_Name")
                    newdata.Sanction_No = row.Field(Of String)("Sanction_No")
                    newdata.Complex_Name = row.Field(Of String)("Complex_Name")
                    newdata.Project_Type = row.Field(Of String)("Prj_Type")
                    newdata.Request_Date = row.Field(Of DateTime)("Request_Date")
                    newdata.Req_Main_Dept = row.Field(Of String)("Req_Main_Dept")
                    newdata.Req_Sub_Dept = row.Field(Of String)("Req_Sub_Dept")
                    newdata.Assignee_Main_Dept = row.Field(Of String)("Assignee_Main_Dept")
                    newdata.Assignee_Sub_Dept = row.Field(Of String)("Assignee_Sub_Dept")
                    newdata.Project_Status = row.Field(Of String)("Prj_Status")
                    newdata.Estimated_Cost = row.Field(Of Decimal)("Est_Cost")
                    newdata.Estimation_Date = row.Field(Of DateTime?)("Est_Date")
                    newdata.Sanction_Date = row.Field(Of DateTime?)("Sanction_Date")
                    newdata.Start_Date = row.Field(Of DateTime?)("Start_Date")
                    newdata.Finish_Date = row.Field(Of DateTime?)("Finish_Date")
                    newdata.Actual_Start_Date = row.Field(Of DateTime?)("Actual_Start_Dt")
                    newdata.Actual_Finish_Date = row.Field(Of DateTime?)("Actual_Finish_Dt")

                    newdata.Proj_Summary = row.Field(Of String)("Proj_Summary")
                    newdata.Rec_Add_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Rec_Add_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Rec_Edit_On = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.Rec_Edit_By = row.Field(Of String)("REC_EDIT_BY")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.CurrUserRole = row.Field(Of String)("CurrUserRole")
                    newdata.Actual_Cost = row.Field(Of Decimal)("Act_Cost")
                    newdata.Proj_Engineer = row.Field(Of String)("Proj_Engineer")
                    newdata.Proj_Estimator = row.Field(Of String)("Proj_Estimator")
                    newdata.Proj_Approver = row.Field(Of String)("Proj_Approver")

                    _Projects_data.Add(newdata)
                Next
            End If
            Return _Projects_data
        End Function
        Public Function GetList() As List(Of Return_GetList)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetList, ClientScreen.Profile_Stock)
            Dim _Projects_data As List(Of Return_GetList) = New List(Of Return_GetList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetList
                    newdata.Project_Type = row.Field(Of String)("Project Type")
                    newdata.Project_Name = row.Field(Of String)("Project Name")
                    newdata.Sanction_no = row.Field(Of String)("Sanction no")
                    newdata.Sanction_Date = row.Field(Of DateTime?)("Sanction Date")
                    newdata.Complex_Name = row.Field(Of String)("Complex Name")
                    newdata.Project_Id = row.Field(Of Integer)("Project Id")
                    newdata.Complex_Id = row.Field(Of String)("Complex Id")
                    _Projects_data.Add(newdata)
                Next
            End If
            Return _Projects_data
        End Function

        Public Function GetOpenProjectsList() As List(Of Return_GetOpenProjectsList)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetOpenProjectList, ClientScreen.Profile_Stock)
            Dim _Projects_data As List(Of Return_GetOpenProjectsList) = New List(Of Return_GetOpenProjectsList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetOpenProjectsList
                    newdata.Project_Type = row.Field(Of String)("Project Type")
                    newdata.Project_Name = row.Field(Of String)("Project Name")
                    newdata.Sanction_no = row.Field(Of String)("Sanction no")
                    newdata.Sanction_Date = row.Field(Of DateTime?)("Sanction Date")
                    newdata.Complex_Name = row.Field(Of String)("Complex Name")
                    newdata.Project_Id = row.Field(Of Integer)("Project Id")
                    newdata.Complex_Id = row.Field(Of String)("Complex Id")
                    newdata.Project_Status = row.Field(Of String)("Project Status")
                    _Projects_data.Add(newdata)
                Next
            End If
            Return _Projects_data
        End Function
        Public Function GetProject_Open_Jobs_Count(ProjID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Projects_GetProject_Open_Jobs_Count, ClientScreen.Stock_Project, ProjID))
        End Function
        Public Function GetProject_Jobs_Count(ProjID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Projects_GetProject_Jobs_Count, ClientScreen.Stock_Project, ProjID))
        End Function
        Public Function GetProjCnt_ForGivenSanctionNo_CurrInstt(InParam As Param_GetProjCnt_ForGivenSanctionNo_CurrInstt) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Projects_GetProjCnt_ForGivenSanctionNo_CurrInstt, ClientScreen.Stock_Project, InParam))
        End Function
        Public Function GetProjTypes() As List(Of Return_GetProjTypes)
            Dim retTable As DataTable = GetMisc("PROJECT TYPES", ClientScreen.Stock_Project, "Project_Type", "Project_Type_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _Proj_GetProjTypes_data As List(Of Return_GetProjTypes) = New List(Of Return_GetProjTypes)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProjTypes
                    newdata.Project_Type = row.Field(Of String)("Project_Type")
                    newdata.Project_Type_ID = row.Field(Of String)("Project_Type_ID")

                    _Proj_GetProjTypes_data.Add(newdata)
                Next
            End If
            Return _Proj_GetProjTypes_data
        End Function
        'Public Function GetStockMainDept() As List(Of Return_GetStockMainDept)

        '    Dim _deptStore As New StockDeptStores(cBase)
        '    _deptStore.GetStoredept(GetStoreDept_ReturnType.MAIN_DEPT, ClientScreen.Stock_Project)

        '    Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockMainDept, ClientScreen.Stock_Project)
        '    Dim _Dept_data As List(Of Return_GetStockMainDept) = New List(Of Return_GetStockMainDept)
        '    If (Not (retTable) Is Nothing) Then
        '        For Each row As DataRow In retTable.Rows
        '            Dim newdata = New Return_GetStockMainDept
        '            newdata.Name = row.Field(Of String)("Name")
        '            newdata.InCharge_Name = row.Field(Of String)("InCharge_Name")
        '            newdata.Center = row.Field(Of String)("Center")
        '            newdata.ID = row.Field(Of Integer)("ID")
        '            _Dept_data.Add(newdata)
        '        Next
        '    End If
        '    Return _Dept_data
        'End Function
        'Public Function GetStockSubDept(MainDeptID As Integer) As List(Of Return_GetStockSubDept)
        '    Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockSubDept, ClientScreen.Stock_Project, MainDeptID)
        '    Dim _Dept_data As List(Of Return_GetStockSubDept) = New List(Of Return_GetStockSubDept)
        '    If (Not (retTable) Is Nothing) Then
        '        For Each row As DataRow In retTable.Rows
        '            Dim newdata = New Return_GetStockSubDept
        '            newdata.Name = row.Field(Of String)("Name")
        '            newdata.InCharge_Name = row.Field(Of String)("InCharge_Name")
        '            newdata.Center = row.Field(Of String)("Center")
        '            newdata.ID = row.Field(Of Integer)("ID")
        '            _Dept_data.Add(newdata)
        '        Next
        '    End If
        '    Return _Dept_data
        'End Function
        Public Function GetRecord(ByVal ProjectID As Int32) As List(Of Return_GetRecord_Project)
            Dim ret_table As DataTable = GetDataListOfRecords(RealServiceFunctions.Projects_GetRecord, ClientScreen.Stock_Project, ProjectID)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim projectdata = New List(Of Return_GetRecord_Project)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_GetRecord_Project()
                    newdata.Proj_Name = row.Field(Of String)("Proj_Name")
                    newdata.Proj_Request_Date = row.Field(Of Date)("Proj_Request_Date")
                    newdata.Proj_Sanction_No = row.Field(Of String)("Proj_Sanction_No")
                    newdata.Proj_Sanction_Date = row.Field(Of Date?)("Proj_Sanction_Date")
                    newdata.Proj_Type_ID = row.Field(Of String)("Proj_Type_ID")
                    newdata.Proj_Complex_Id = row.Field(Of String)("Proj_Complex_Id")
                    newdata.Proj_Requestor_Main_Dept = row.Field(Of Integer?)("Proj_Requestor_Main_Dept_Id")
                    newdata.Proj_Requestor_Sub_Dept = row.Field(Of Integer?)("Proj_Requestor_Sub_Dept_Id")
                    newdata.Proj_Status = row.Field(Of String)("Proj_Status")
                    newdata.Proj_Start_Date = row.Field(Of Date?)("Proj_Start_Date")
                    newdata.Proj_Finish_Date = row.Field(Of Date?)("Proj_Finish_Date")
                    newdata.Proj_Estimation_Date = row.Field(Of Date?)("Proj_Estimation_Date")
                    newdata.Proj_Summary = row.Field(Of String)("Proj_Summary")
                    newdata.REC_ADD_ON = row.Field(Of Date)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    newdata.REC_EDIT_ON = row.Field(Of Date)("REC_EDIT_ON")
                    newdata.REC_ID = row.Field(Of Integer)("REC_ID")
                    newdata.AssigneeMainDept = row.Field(Of String)("AssigneeMainDept") 'Mantis bug 0000314 fixed
                    newdata.AssigneeSubDept = row.Field(Of String)("AssigneeSubDept") 'Mantis bug 0000314 fixed
                    newdata.Proj_Engineer_ID = row.Field(Of Integer?)("Proj_Engineer_ID")
                    newdata.Proj_Actual_Start = row.Field(Of Date?)("Proj_Actual_Start")
                    newdata.Proj_Actual_Finish = row.Field(Of Date?)("Proj_Actual_Finish")
                    newdata.Proj_Estimator_ID = row.Field(Of Integer?)("Proj_Estimator_ID")
                    newdata.Proj_Approver_ID = row.Field(Of Integer?)("Proj_Approver_ID")
                    projectdata.Add(newdata)
                Next
                Return projectdata
            End If
        End Function
        Public Function GetProjectEnginners() As List(Of Return_GetProjectEnginners)
            Dim _param As New Param_GetStockPersonnels()
            _param.Skill_Type = "Project Engineer"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Project, _param)
            Dim _Engg_data As List(Of Return_GetProjectEnginners) = New List(Of Return_GetProjectEnginners)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProjectEnginners
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _Engg_data.Add(newdata)
                Next
            End If
            Return _Engg_data
        End Function
        Public Function GetProjectEstimators() As List(Of Return_GetProjectEstimators)
            Dim _param As New Param_GetStockPersonnels()
            _param.Skill_Type = "Project Estimator"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Project, _param)
            Dim _Estimator_data As List(Of Return_GetProjectEstimators) = New List(Of Return_GetProjectEstimators)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProjectEstimators
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _Estimator_data.Add(newdata)
                Next
            End If
            Return _Estimator_data
        End Function
        Public Function GetProjectDocuments(ProjectID As Integer) As List(Of Return_GetDocumentsGridData)
            Dim _Docs_Param As New Common_Lib.RealTimeService.Param_GetStockDocuments
            _Docs_Param.RefID = ProjectID
            _Docs_Param.Screen_Type = "Projects"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockDocuments, ClientScreen.Stock_Project, _Docs_Param)
            Dim _Docs_data As List(Of Return_GetDocumentsGridData) = New List(Of Return_GetDocumentsGridData)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocumentsGridData
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Document_Name = row.Field(Of String)("DOC_NAME")
                    newdata.Document_Type = row.Field(Of String)("DOC_TYPE")
                    newdata.File_Name = row.Field(Of String)("File_Name")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Added_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Applicable_From = row.Field(Of DateTime?)("Applicable_From")
                    newdata.Applicable_To = row.Field(Of DateTime?)("Applicable_To")
                    newdata.Document_Name_ID = row.Field(Of String)("Document_Name_ID")
                    _Docs_data.Add(newdata)
                Next
            End If
            Return _Docs_data
        End Function
        Public Function GetProjectRemarks(ProjectID As Integer) As List(Of Return_GetProjectRemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = ProjectID
            _Remarks_Param.Screen_Type = "Projects"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_Project, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetProjectRemarks) = New List(Of Return_GetProjectRemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProjectRemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function
        Public Function GetProjectEstimation(ProjectID As Integer) As List(Of Return_GetProjectEstimation)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetProjectEstimates, ClientScreen.Stock_Project, ProjectID)
            Dim _Estimate_data As List(Of Return_GetProjectEstimation) = New List(Of Return_GetProjectEstimation)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProjectEstimation
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Description = row.Field(Of String)("PE_Description")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Estimated_Qty = row.Field(Of Decimal)("Estimated_Qty")
                    newdata.Rate = row.Field(Of Decimal)("Rate")
                    newdata.Total_Amount = row.Field(Of Decimal)("Total_Amount")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Added_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.UnitID = row.Field(Of String)("UnitID")


                    _Estimate_data.Add(newdata)
                Next
            End If
            Return _Estimate_data
        End Function
        Public Function GetYrAuditedPeriod() As DataTable
            Return GetAuditedPeriod(ClientScreen.Stock_Project)
        End Function
        Public Function GetYrAccountsSubmittedPeriod() As DataTable
            Return GetAccountsSubmittedPeriod(ClientScreen.Stock_Project)
        End Function
        Public Function GetAttachmentLinkScreen(ProjectID As Int32, AttachmentID As String) As String
            Dim inparam As New Parameter_GetAttachmentLinkCount()
            inparam.RefRecordID = ProjectID
            inparam.RefScreen = "Project"
            inparam.AttachmentID = AttachmentID
            Return cBase._Attachments_DBOps.GetAttachmentLinkScreen(inparam)
        End Function
        Public Function InsertProject(InParam As Param_Insert_Project_Txn) As Int32
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Projects_Insert, ClientScreen.Stock_Project, InParam)
        End Function
        Public Function InsertProjectRemarks(InParam As Param_InsertProjectRemarks) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Projects_InsertProjectRemarks, ClientScreen.Stock_Project, InParam)
        End Function
        Public Function UpdateProjectStatus(UpParam As Param_Update_Project_Status) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Projects_UpdateProjectStatus, ClientScreen.Stock_Project, UpParam)
        End Function
        Public Function UpdateProject(UpParam As Param_Update_Project_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Projects_Update, ClientScreen.Stock_Project, UpParam)
        End Function
        Public Function DeleteProject(ProjectID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Projects_Delete, ClientScreen.Stock_Project, ProjectID)
        End Function
    End Class
    <Serializable>
    Public Class Jobs
        Inherits SharedVariables
#Region "Return Classes"
        <Serializable>
        Public Class Return_GetRecord
            Public Property Job_Name As String
            Public Property Job_RequestDate As Date
            Public Property Job_No As Integer
            Public Property Job_RequestorId As Integer
            Public Property Job_RequestorSubDeptId As Integer?
            Public Property Job_RequestorMainDeptId As Integer
            Public Property Job_Description As String
            Public Property Job_EndDate As Date?
            Public Property Job_StartDate As Date?
            Public Property Job_RequestedFinishDate As Date?
            Public Property Job_RequestedStartDate As Date?
            Public Property Job_AssigneeId As Integer?
            Public Property Job_AssigneeSubDeptID As String 'Mantis bug 0000374 fixed
            Public Property Job_AssigneeMainDeptID As Integer
            Public Property Job_ComplexId As String
            Public Property Job_ProjectId As Integer?
            Public Property Job_Type As String
            Public Property Job_Status As String
            Public Property Job_BudgetLimit As Decimal?
            Public Property Job_EstRequired As Boolean?

        End Class
        <Serializable>
        Public Class Return_GetRegister
            Public Property Job_Name As String
            Public Property Job_No As Integer
            Public Property Job_Type As String
            Public Property Requestor As String
            Public Property Requestor_Main_Department As String
            Public Property Requestor_Sub_Department As String
            Public Property Job_Status As String
            Public Property Project As String
            Public Property Complex As String
            Public Property Sanction_No As String
            Public Property Assignee_Main_Dept As String
            Public Property Assignee_Sub_Dept As String
            Public Property Job_Request_Date As DateTime
            Public Property Job_Start_Date As DateTime?
            Public Property Job_End_Date As DateTime?
            Public Property Estimated_Cost As Decimal?
            Public Property Actual_Cost As Decimal?
            Public Property Estimation_Required As Boolean?
            Public Property Job_Description As String
            Public Property Sanction_Date As DateTime?
            Public Property Job_Requested_Start_Date As DateTime?
            Public Property Job_Requested_finish_Date As DateTime?
            Public Property Rec_Add_On As DateTime
            Public Property Rec_Add_By As String
            Public Property Rec_Edit_On As DateTime
            Public Property Rec_Edit_By As String
            Public Property ID As Integer
            Public Property CurrUserRole As String
        End Class
        <Serializable>
        Public Class Return_GetJobTypes
            Public Job_Type As String
            Public Job_Type_ID As String
        End Class
        <Serializable>
        Public Class Return_GetList
            Public Property Job_Name As String
            Public Property Job_No As Integer
            Public Property Job_Type As String
            Public Property Proj_Name As String
            Public Property Complex As String
            Public Property ID As Integer
            Public Property Sanction_No As String
        End Class
        <Serializable>
        Public Class Return_GetStockPersonnels
            Public Property Name As String
            Public Property Type As String
            Public Property Main_Dept As String
            Public Property Sub_Dept As String
            Public Property ID As Integer
            Public Property Mobile_No As String
            Public Property Email As String
            Public Property Skill_Type As String
            Public Property Designation As String
            Public Property Contractor As String
            Public Property Main_DeptID As Integer?
            Public Property Sub_DeptID As Integer?
            Public Property Leaving_Date As DateTime? 'Mantis bug 0001045 resolved
            Public Property Joining_Date As DateTime? 'Mantis bug 0001045 resolved
        End Class
        'Public Class Return_GetJobDocuments
        '    Inherits Projects.Return_GetProjectDocuments
        'End Class
        <Serializable>
        Public Class Return_GetJobRemarks
            Inherits Projects.Return_GetProjectRemarks
        End Class
        <Serializable>
        Public Class Return_GetJobItemEstimates
            Public Property Item As String
            Public Property ItemType As String
            Public Property Item_Code As String
            Public Property Quantity As Decimal
            Public Property Unit As String
            Public Property Est_Rate As Decimal
            Public Property Est_Amount As Decimal
            Public Property Tolerance As Decimal?
            Public Property Remarks As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_ID As Int32
            Public Property UnitID As String
            Public Property Item_ID As Int32
            Public Property Sr As Int64
        End Class
        <Serializable>
        Public Class Return_GetJobManpowerEstimates
            Public Property Manpower_Type As String
            Public Property Unit As String
            Public Property Estimated_Consumption As Decimal
            Public Property Estimated_Rate_per_Unit As Decimal
            Public Property Est_Cost As Decimal
            Public Property Remarks As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_ID As Int32
            Public Property UnitID As String
            Public Property SkillID As String
            Public Property Sr As Int64
        End Class
        <Serializable>
        Public Class Return_GetJobManpowerUsage
            Public Property Person_Name As String
            Public Property Work_Period_From As DateTime
            Public Property Work_Period_To As DateTime
            Public Property UnitID As String
            Public Property Unit As String
            Public Property Units_Worked As Decimal
            Public Property Rate_per_Unit As Decimal
            Public Property Total_Cost As Decimal
            Public Property Remarks As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_ID As Int32
            Public Property Manpower_ID As Int32
            Public Property Sr As Int64
            Public Property Job_Manpower_ChargeID As Integer? 'Mantis bug 0000362 fixed
        End Class
        <Serializable>
        Public Class Return_GetJobMachineUsage
            Public Property Machine_Name As String
            Public Property Machine_No As String
            Public Property Mch_Count As Int32
            Public Property Usage_in_Hrs As Decimal
            Public Property Remarks As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_ID As Int32
            Public Property Machine_ID As Int32
            Public Property Sr As Int64
        End Class
        <Serializable>
        Public Class Return_GetJobExpensesIncurred
            Public Property _Date As DateTime
            Public Property ItemName As String
            Public Property Head As String
            Public Property Party As String
            Public Property Amount As Decimal
            Public Property Narration As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_ID As Int32
            Public Property Sr As Int64
            Public Property Txn_ID As String '//Mantis bug 0000393 fixed
            Public Property Txn_Sr_No As Int32? '//Mantis bug 0000393 fixed
        End Class
        <Serializable>
        Public Class Return_Get_Job_Expenses_For_Mapping
            Public Property _Date As DateTime
            Public Property ItemName As String
            Public Property Head As String
            Public Property Party As String
            Public Property Amount As Decimal
            Public Property Mapped_On As DateTime?
            Public Property Mapped_By As String
            Public Property ID As Int32?
            Public Property Sr As Int64
            Public Property Txn_ID As String
            Public Property Txn_Sr_No As Int32?
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetRegister(fromDate As DateTime, toDate As DateTime) As List(Of Return_GetRegister)
            Dim InParam As New Param_GetJobRegister()
            InParam.From_Date = fromDate
            InParam.ToDate = toDate
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Jobs_GetRegister, ClientScreen.Stock_Job, InParam)
            Dim _Jobs_data As List(Of Return_GetRegister) = New List(Of Return_GetRegister)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetRegister
                    newdata.Job_Name = row.Field(Of String)("Job_Name")
                    newdata.Job_No = row.Field(Of Integer)("Job_No")
                    newdata.Job_Type = row.Field(Of String)("Job_Type")
                    newdata.Requestor = row.Field(Of String)("Requestor")
                    newdata.Requestor_Main_Department = row.Field(Of String)("Requestor_Main_Department")
                    newdata.Requestor_Sub_Department = row.Field(Of String)("Requestor_Sub_Department")
                    newdata.Job_Status = row.Field(Of String)("Job_Status")
                    newdata.Project = row.Field(Of String)("Project")
                    newdata.Complex = row.Field(Of String)("Complex")
                    newdata.Sanction_No = row.Field(Of String)("Sanction_No")
                    newdata.Assignee_Main_Dept = row.Field(Of String)("Assignee_Main_Dept")
                    newdata.Assignee_Sub_Dept = row.Field(Of String)("Assignee_Sub_Dept")
                    newdata.Job_Start_Date = row.Field(Of DateTime?)("Job_Start_Date")
                    newdata.Job_End_Date = row.Field(Of DateTime?)("Job_End_Date")
                    newdata.Estimated_Cost = row.Field(Of Decimal?)("Estimated_Cost")
                    newdata.Actual_Cost = row.Field(Of Decimal?)("Actual_Cost")
                    newdata.Estimation_Required = row.Field(Of Boolean?)("Estimation_Required")
                    newdata.Job_Description = row.Field(Of String)("Job_Description")
                    newdata.Sanction_Date = row.Field(Of DateTime?)("Sanction_Date")
                    newdata.Job_Requested_Start_Date = row.Field(Of DateTime?)("Job_Requested_Start_Date")
                    newdata.Job_Requested_finish_Date = row.Field(Of DateTime?)("Job_Requested_finish_Date")

                    newdata.Rec_Add_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Rec_Add_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Rec_Edit_On = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.Rec_Edit_By = row.Field(Of String)("REC_EDIT_BY")
                    newdata.ID = row.Field(Of Integer)("REC_ID")
                    newdata.CurrUserRole = row.Field(Of String)("CurrUserRole")
                    newdata.Job_Request_Date = row.Field(Of DateTime)("Job_Request_Date")

                    _Jobs_data.Add(newdata)
                Next
            End If
            Return _Jobs_data
        End Function
        Public Function GetOpenJobs() As List(Of Return_GetList)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Jobs_GetList, ClientScreen.Stock_Job, True)
            Dim _Jobs_data As List(Of Return_GetList) = New List(Of Return_GetList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetList
                    newdata.Job_Name = row.Field(Of String)("Job_Name")
                    newdata.Job_No = row.Field(Of Integer)("Job_No")
                    newdata.Job_Type = row.Field(Of String)("Job_Type")
                    newdata.Proj_Name = row.Field(Of String)("Proj_Name")
                    newdata.Complex = row.Field(Of String)("Complex")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Sanction_No = row.Field(Of String)("Sanction_No")
                    _Jobs_data.Add(newdata)
                Next
            End If
            Return _Jobs_data
        End Function
        ''' <summary>
        ''' Gets total count of User Orders posted against given Job
        ''' </summary>
        ''' <param name="JobID"></param>
        ''' <returns></returns>
        Public Function GetJob_UO_Count(JobID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Jobs_GetJob_UO_Count, ClientScreen.Stock_Job, JobID))
        End Function
        ''' <summary>
        ''' Gets total count of non-completed User Orders posted against given Job
        ''' </summary>
        ''' <param name="JobID"></param>
        ''' <returns></returns>
        Public Function GetJob_UO_Pending_Count(JobID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Jobs_GetJob_UO_Pending_Count, ClientScreen.Stock_Job, JobID))
        End Function
        ''' <summary>
        ''' Gets total count of Requisition Requests posted against given Job
        ''' </summary>
        ''' <param name="JobID"></param>
        ''' <returns></returns>
        Public Function GetJob_RR_Count(JobID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Jobs_GetJob_RR_Count, ClientScreen.Stock_Job, JobID))
        End Function
        ''' <summary>
        ''' Gets total count of non-completed Requisition Requests posted against given Job
        ''' </summary>
        ''' <param name="JobID"></param>
        ''' <returns></returns>
        Public Function GetJob_RR_Pending_Count(JobID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Jobs_GetJob_RR_Pending_Count, ClientScreen.Stock_Job, JobID))
        End Function
        Public Function GetRecord(ByVal JobID As Int32) As Return_GetRecord
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Jobs_GetRecord, ClientScreen.Stock_Job, JobID)
            Dim newdata As Return_GetRecord = New Return_GetRecord
            If (Not (retTable) Is Nothing) Then
                Dim Data As DataRow = retTable.Rows(0)

                newdata.Job_Name = Data.Field(Of String)("Job_Name")
                newdata.Job_RequestDate = Data.Field(Of Date)("Job_Request_Date")
                newdata.Job_No = Data.Field(Of Integer)("Job_No")
                newdata.Job_RequestorId = Data.Field(Of Integer)("Job_Requestor_Id")
                newdata.Job_RequestorSubDeptId = Data.Field(Of Integer?)("Job_Requestor_Sub_Dept_Id")
                newdata.Job_RequestorMainDeptId = Data.Field(Of Integer)("Job_Requestor_Main_Dept_Id")
                newdata.Job_Description = Data.Field(Of String)("Job_Description")
                newdata.Job_EndDate = Data.Field(Of Date?)("Job_Completion_Date")
                newdata.Job_StartDate = Data.Field(Of Date?)("Job_Start_Date")
                newdata.Job_RequestedFinishDate = Data.Field(Of Date?)("Job_Requested_Completion_Date")
                newdata.Job_RequestedStartDate = Data.Field(Of Date?)("Job_Requested_Start_Date")
                newdata.Job_AssigneeId = Data.Field(Of Integer?)("Job_Assignee_Id")
                'newdata.Job_AssigneeSubDeptID = Data.Field(Of Integer?)("Job_Assignee_Sub_Dept_ID")
                newdata.Job_AssigneeSubDeptID = Data.Field(Of String)("AssigneeSubDept")
                newdata.Job_AssigneeMainDeptID = Data.Field(Of Integer)("Job_Assignee_Main_Dept_ID")
                newdata.Job_ComplexId = Data.Field(Of String)("Job_Complex_Id")
                newdata.Job_ProjectId = Data.Field(Of Integer?)("Job_Project_Id")
                newdata.Job_Type = Data.Field(Of String)("Job_Type")
                newdata.Job_Status = Data.Field(Of String)("Job_Status")
                newdata.Job_BudgetLimit = Data.Field(Of Decimal?)("Job_Budget_Limit")
                newdata.Job_EstRequired = Data.Field(Of Boolean?)("Job_Estimate_Required")
            End If
            Return newdata
        End Function
        ''' <summary>
        ''' Give total count of UO/Machine Usage/Expenses Posted/Manpower usage against mentioned job
        ''' </summary>
        ''' <param name="JobID"></param>
        ''' <returns></returns>
        Public Function GetJob_Total_Usage_Count(JobID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Jobs_GetJob_Total_Usage_Count, ClientScreen.Stock_Job, JobID))
        End Function
        Public Function GetJob_Project_Main_Assignee(JobID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Jobs_GetJob_Project_Main_Assignee, ClientScreen.Stock_Job, JobID))
        End Function
        Public Function GetStockPersonnels() As List(Of Return_GetStockPersonnels)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Project)
            Dim _Personnel_data As List(Of Return_GetStockPersonnels) = New List(Of Return_GetStockPersonnels)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStockPersonnels
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Mobile_No = row.Field(Of String)("mob_no")
                    newdata.Email = row.Field(Of String)("Email")
                    newdata.Skill_Type = row.Field(Of String)("Skill_Type")
                    newdata.Designation = row.Field(Of String)("Designation")
                    newdata.Contractor = row.Field(Of String)("Contractor")
                    newdata.Main_DeptID = row.Field(Of Integer?)("MAIN_DEPT_ID")
                    newdata.Sub_DeptID = row.Field(Of Integer?)("SUB_DEPT_ID")


                    _Personnel_data.Add(newdata)
                Next
            End If
            Return _Personnel_data
        End Function
        Public Function GetPaidPersonnels() As List(Of Return_GetStockPersonnels)
            Dim Inparam As New Param_GetStockPersonnels
            Inparam.PersonnelType = "Paid"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Project, Inparam)
            Dim _Personnel_data As List(Of Return_GetStockPersonnels) = New List(Of Return_GetStockPersonnels)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStockPersonnels
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Mobile_No = row.Field(Of String)("mob_no")
                    newdata.Email = row.Field(Of String)("Email")
                    newdata.Skill_Type = row.Field(Of String)("Skill_Type")
                    newdata.Designation = row.Field(Of String)("Designation")
                    newdata.Contractor = row.Field(Of String)("Contractor")
                    newdata.Main_DeptID = row.Field(Of Integer?)("MAIN_DEPT_ID")
                    newdata.Sub_DeptID = row.Field(Of Integer?)("SUB_DEPT_ID")
                    newdata.Leaving_Date = row.Field(Of DateTime?)("Leaving_Date") 'Mantis bug 0001045 resolved
                    newdata.Joining_Date = row.Field(Of DateTime?)("Joining_Date") 'Mantis bug 0001045 resolved
                    _Personnel_data.Add(newdata)
                Next
            End If
            Return _Personnel_data
        End Function
        Public Function GetJobDocuments(JobID As Integer) As List(Of Return_GetDocumentsGridData)
            Dim _Docs_Param As New Common_Lib.RealTimeService.Param_GetStockDocuments
            _Docs_Param.RefID = JobID
            _Docs_Param.Screen_Type = "Jobs"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockDocuments, ClientScreen.Stock_Project, _Docs_Param)
            Dim _Docs_data As List(Of Return_GetDocumentsGridData) = New List(Of Return_GetDocumentsGridData)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocumentsGridData
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Document_Name = row.Field(Of String)("DOC_NAME")
                    newdata.Document_Type = row.Field(Of String)("DOC_TYPE")
                    newdata.File_Name = row.Field(Of String)("File_Name")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Added_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Applicable_From = row.Field(Of DateTime?)("Applicable_From")
                    newdata.Applicable_To = row.Field(Of DateTime?)("Applicable_To")
                    newdata.Document_Name_ID = row.Field(Of String)("Document_Name_ID")
                    _Docs_data.Add(newdata)
                Next
            End If
            Return _Docs_data
        End Function
        Public Function GetJobRemarks(JobID As Integer) As List(Of Return_GetJobRemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = JobID
            _Remarks_Param.Screen_Type = "Jobs"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_Project, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetJobRemarks) = New List(Of Return_GetJobRemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetJobRemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function
        Public Function GetJobItemEstimates(JobID As Integer) As List(Of Return_GetJobItemEstimates)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Jobs_GetJobItemEstimates, ClientScreen.Stock_Job, JobID)
            Dim _Item_Est_data As List(Of Return_GetJobItemEstimates) = New List(Of Return_GetJobItemEstimates)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetJobItemEstimates
                    newdata.Item = row.Field(Of String)("Item")
                    newdata.ItemType = row.Field(Of String)("ItemType")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Quantity = row.Field(Of Decimal)("Quantity")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Est_Rate = row.Field(Of Decimal)("Est_Rate")
                    newdata.Est_Amount = row.Field(Of Decimal)("Est_Amount")
                    newdata.Tolerance = row.Field(Of Decimal?)("Tolerance")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_ID = row.Field(Of Int32)("REC_ID")
                    newdata.Item_ID = row.Field(Of Int32)("ItemID")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    _Item_Est_data.Add(newdata)
                Next
            End If
            Return _Item_Est_data
        End Function
        Public Function GetJobManpowerEstimates(JobID As Integer) As List(Of Return_GetJobManpowerEstimates)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Jobs_GetJobManpowerEstimates, ClientScreen.Stock_Job, JobID)
            Dim _Manpower_Est_data As List(Of Return_GetJobManpowerEstimates) = New List(Of Return_GetJobManpowerEstimates)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetJobManpowerEstimates
                    newdata.Manpower_Type = row.Field(Of String)("Manpower_Type")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Estimated_Consumption = row.Field(Of Decimal)("Estimated_Consumption")
                    newdata.Estimated_Rate_per_Unit = row.Field(Of Decimal)("Estimated_Rate_per_Unit")
                    newdata.Est_Cost = row.Field(Of Decimal)("Est_Cost")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_ID = row.Field(Of Int32)("REC_ID")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.SkillID = row.Field(Of String)("SkillID")
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    _Manpower_Est_data.Add(newdata)
                Next
            End If
            Return _Manpower_Est_data
        End Function
        Public Function GetJobManpowerUsage(JobID As Integer) As List(Of Return_GetJobManpowerUsage)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Jobs_GetJobManpowerUsage, ClientScreen.Stock_Job, JobID)
            Dim _Manpower_Est_data As List(Of Return_GetJobManpowerUsage) = New List(Of Return_GetJobManpowerUsage)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetJobManpowerUsage
                    newdata.Person_Name = row.Field(Of String)("Person_Name")
                    newdata.Work_Period_From = row.Field(Of DateTime)("Work_Period_From")
                    newdata.Work_Period_To = row.Field(Of DateTime)("Work_Period_To")
                    newdata.UnitID = row.Field(Of String)("Units")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Units_Worked = row.Field(Of Decimal)("Units_Worked")
                    newdata.Rate_per_Unit = row.Field(Of Decimal)("Rate_per_Unit")
                    newdata.Total_Cost = row.Field(Of Decimal)("Total_Cost")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_ID = row.Field(Of Int32)("REC_ID")
                    newdata.Manpower_ID = row.Field(Of Int32)("Manpower_ID")
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    _Manpower_Est_data.Add(newdata)
                Next
            End If
            Return _Manpower_Est_data
        End Function
        Public Function GetJobMachineUsage(JobID As Integer) As List(Of Return_GetJobMachineUsage)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Jobs_GetJobMachineUsage, ClientScreen.Stock_Job, JobID)
            Dim _Manpower_Est_data As List(Of Return_GetJobMachineUsage) = New List(Of Return_GetJobMachineUsage)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetJobMachineUsage
                    newdata.Machine_Name = row.Field(Of String)("Machine_Name")
                    newdata.Machine_No = row.Field(Of String)("Machine_No")
                    newdata.Mch_Count = row.Field(Of Int32)("Mch_Count")
                    newdata.Usage_in_Hrs = row.Field(Of Decimal)("Usage_in_Hrs")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_ID = row.Field(Of Int32)("REC_ID")
                    newdata.Machine_ID = row.Field(Of Int32)("Machine_ID")
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    _Manpower_Est_data.Add(newdata)
                Next
            End If
            Return _Manpower_Est_data
        End Function
        Public Function GetJobExpensesIncurred(JobID As Integer) As List(Of Return_GetJobExpensesIncurred)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Jobs_GetJobExpensesIncurred, ClientScreen.Stock_Job, JobID)
            Dim _Exp_data As List(Of Return_GetJobExpensesIncurred) = New List(Of Return_GetJobExpensesIncurred)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetJobExpensesIncurred
                    newdata._Date = row.Field(Of DateTime)("Date")
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Party = row.Field(Of String)("Party")
                    newdata.Amount = row.Field(Of Decimal)("Amount")
                    newdata.Narration = row.Field(Of String)("Narration")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_ID = row.Field(Of Int32)("REC_ID")
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Txn_ID = row.Field(Of String)("Txn_ID") '//Mantis bug 0000393 fixed
                    newdata.Txn_Sr_No = row.Field(Of Int32?)("Txn_Sr_No") '//Mantis bug 0000393 fixed
                    _Exp_data.Add(newdata)
                Next
            End If
            Return _Exp_data
        End Function
        Public Function Get_Job_Expenses_For_Mapping(JobID As Integer) As List(Of Return_Get_Job_Expenses_For_Mapping)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Jobs_Get_Job_Expenses_For_Mapping, ClientScreen.Stock_Job, JobID)
            Dim _Exp_data As List(Of Return_Get_Job_Expenses_For_Mapping) = New List(Of Return_Get_Job_Expenses_For_Mapping)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Get_Job_Expenses_For_Mapping
                    newdata._Date = row.Field(Of DateTime)("Date")
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Party = row.Field(Of String)("Party")
                    newdata.Amount = row.Field(Of Decimal)("Amount")
                    newdata.Txn_ID = row.Field(Of String)("Txn_ID")
                    newdata.Txn_Sr_No = row.Field(Of Integer?)("Txn_Sr_No")
                    newdata.Mapped_On = row.Field(Of DateTime?)("Mapped_On")
                    newdata.Mapped_By = row.Field(Of String)("Mapped_By")
                    newdata.ID = row.Field(Of Int32?)("ID")
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    _Exp_data.Add(newdata)
                Next
            End If
            Return _Exp_data
        End Function
        Public Function Get_Machines() As List(Of DbOperations.StockProfile.Return_Get_Stocks_Listing)
            Dim _Param As New Common_Lib.RealTimeService.Param_Get_Stocks_Listing
            _Param.StockType = "Machines"
            Dim _Profile As New StockProfile(cBase)
            Return _Profile.Get_Stocks_Listing(Common_Lib.RealTimeService.ClientScreen.Stock_Job, _Param)
        End Function
        Public Function GetJobTypes() As List(Of Return_GetJobTypes)
            Dim retTable As DataTable = GetMisc("JOB TYPES", ClientScreen.Stock_Project, "Job_Type", "Job_Type_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _Job_GetJobTypes_data As List(Of Return_GetJobTypes) = New List(Of Return_GetJobTypes)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetJobTypes
                    newdata.Job_Type = row.Field(Of String)("Job_Type")
                    newdata.Job_Type_ID = row.Field(Of String)("Job_Type_ID")

                    _Job_GetJobTypes_data.Add(newdata)
                Next
            End If
            Return _Job_GetJobTypes_data
        End Function
        Public Function GetAttachmentLinkScreen(JobID As Int32, AttachmentID As String) As String
            Dim inparam As New Parameter_GetAttachmentLinkCount()
            inparam.RefRecordID = JobID
            inparam.RefScreen = "Job"
            inparam.AttachmentID = AttachmentID
            Return cBase._Attachments_DBOps.GetAttachmentLinkScreen(inparam)
        End Function
        Public Function InsertJob(InParam As Param_Insert_Job_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Jobs_Insert_Txn, ClientScreen.Stock_Job, InParam)
        End Function
        Public Function InsertJobManpowerUsage(Inparam As Param_Insert_Job_ManpowerUsage) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Jobs_InsertJobManpowerUsage, ClientScreen.Stock_Job, Inparam)
        End Function
        Public Function InsertJobRemarks(Inparam As Param_InsertJobRemarks) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Jobs_InsertJobRemarks, ClientScreen.Stock_Job, Inparam)
        End Function
        Public Function InsertJobExpensesIncurred(Inparam As Param_Insert_Job_ExpensesIncurred) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Jobs_InsertJobExpensesIncurred, ClientScreen.Stock_Job, Inparam)
        End Function
        Public Function InsertJobMachineUsage(Inparam As Param_Insert_Job_MachineUsage) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Jobs_InsertJobMachineUsage, ClientScreen.Stock_Job, Inparam)
        End Function
        Public Function UpdateJobStatus(UpParam As Param_Update_Job_Status) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Jobs_UpdateJobStatus, ClientScreen.Stock_Job, UpParam)
        End Function
        Public Function UpdateJob(UpParam As Param_Update_Job_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Jobs_Update, ClientScreen.Stock_Job, UpParam)
        End Function
        Public Function DeleteJob(JobID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Jobs_Delete_Txn, ClientScreen.Stock_Job, JobID)
        End Function
    End Class
#End Region
End Class