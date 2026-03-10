Imports Common_Lib.RealTimeService
Imports System
Partial Public Class DbOperations

#Region "--Masters--"
    <Serializable>
    Public Class Personnels
        Inherits SharedVariables

#Region "Param/Return Classes"
        ''' <summary>
        ''' Return class for GetRequestorList()
        ''' </summary>
        <Serializable>
        Public Class Return_GetRequestorList
            Public Requestor_Name As String
            Public Requestor_Dept As String
            Public Requestor_Contact_No As String
            Public Requestor_ID As Integer
        End Class
        <Serializable>
        Public Class Return_GetSkillTypes
            Public Skill_Type As String
            Public Skill_Type_ID As String
        End Class
        <Serializable>
        Public Class Return_GetDesignations
            Public Designation As String
            Public Designation_ID As String
        End Class
        <Serializable>
        Public Class Return_GetPersonnelCharges
            Public Rate_per_Unit As Decimal
            Public UnitID As String
            Public Unit As String
            Public REC_ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetRegister
            Public Property main_Register As List(Of Return_GetRegister_MainGrid)
            Public Property nested_Register As List(Of Return_GetRegister_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetRegister_MainGrid
            Public Property Name As String
            Public Property Gender As String
            Public Property Personnel_Type As String
            Public Property Skill_Type As String
            Public Property Aadhaar_No As String
            Public Property PAN_No As String
            Public Property DOB As Date?
            Public Property PF_No As String
            Public Property Dept_Name As String
            Public Property Contractor_Name As String
            Public Property Payment_Mode As String
            Public Property Joining_Date As Date?
            Public Property Leaving_Date As Date?
            Public Property Contact_No As String
            Public Property Other_Details As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_EDIT_ON As DateTime
            Public Property REC_EDIT_BY As String
            Public Property REC_ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetRegister_NestedGrid
            Public Property Effective_Date As Date
            Public Property Charges As Decimal?
            Public Property Unit As String
            Public Property Remarks As String
            Public Property Effective_Till As Date?
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_EDIT_ON As DateTime
            Public Property REC_EDIT_BY As String
            Public Property ID As Int32
            Public Property Personnel_ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPersons
            Public Property Name As String
            Public Property Gender As String
            Public Property DoB As DateTime?
            Public Property Pan_No As String
            Public Property Mobile_No As String
            Public Property Aadhar_No As String
            Public Property ID As String
        End Class
        <Serializable>
        Public Class Return_GetContractors
            Public Contractor_Name As String
            Public ID As Integer
            Public Contact_No As String
        End Class
        <Serializable>
        Public Class Return_GetPersonnelRecord
            Public AB_ID As String
            Public Type As String
            Public Skill_Type_ID As String
            Public Payment_Mode As String
            Public PF_NO As String
            Public Joining_Date As DateTime?
            Public Leaving_Date As DateTime?
            Public Other_Details As String
            Public MainDept_ID As Int32?
            Public SubDept_ID As Int32?
            Public Designation_ID As String
            Public Contractor_ID As Int32?
            Public REC_ADD_ON As DateTime
            Public REC_EDIT_ON As DateTime
            Public REC_ADD_BY As String
            Public REC_EDIT_BY As String
            Public REC_ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPersonnelChargesRecord
            Public EffDate As DateTime
            Public Charges As Decimal
            Public UnitID As String
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Return_Get_Personnel_Usage_Period
            Public Property MinUsageFromDate As DateTime?
            Public Property MaxUsageToDate As DateTime?
        End Class
        <Serializable>
        Public Class Return_GetUnits
            Public Unit As String
            Public Unit_ID As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetRegister() As Return_GetRegister
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.Personnels_GetRegister, ClientScreen.Stock_Personnel_Master, Nothing)

            Dim _main_data As New Return_GetRegister
            Dim _uo_main_data As List(Of Return_GetRegister_MainGrid) = New List(Of Return_GetRegister_MainGrid)
            Dim _uo_nested_data As List(Of Return_GetRegister_NestedGrid) = New List(Of Return_GetRegister_NestedGrid)
            _main_data.main_Register = _uo_main_data
            _main_data.nested_Register = _uo_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetRegister_MainGrid
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Gender = row.Field(Of String)("Gender")
                    newdata.Personnel_Type = row.Field(Of String)("Personnel_Type")
                    newdata.Skill_Type = row.Field(Of String)("Skill_Type")
                    newdata.Aadhaar_No = row.Field(Of String)("Aadhaar_No")
                    newdata.PAN_No = row.Field(Of String)("PAN_No")
                    newdata.DOB = row.Field(Of Date?)("DOB")
                    newdata.PF_No = row.Field(Of String)("PF_No")
                    newdata.Dept_Name = row.Field(Of String)("Dept_Name")
                    newdata.Contractor_Name = row.Field(Of String)("Contractor_Name")
                    newdata.Payment_Mode = row.Field(Of String)("Payment_Mode")
                    newdata.Joining_Date = row.Field(Of Date?)("Joining_Date")
                    newdata.Leaving_Date = row.Field(Of Date?)("Leaving_Date")
                    newdata.Contact_No = row.Field(Of String)("Contact_No")
                    newdata.Other_Details = row.Field(Of String)("Other_Details")

                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    newdata.REC_ID = row.Field(Of Integer)("REC_ID")
                    _uo_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetRegister_NestedGrid
                    subdata.Effective_Date = row.Field(Of Date)("Effective_Date")
                    subdata.Charges = row.Field(Of Decimal?)("Charges")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.Remarks = row.Field(Of String)("Remarks")
                    subdata.Effective_Till = row.Field(Of Date?)("Effective_Till")
                    subdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    subdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    subdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    subdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.Personnel_ID = row.Field(Of Int32)("Personnel_ID")
                    _uo_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function

        Public Function GetPersons(Optional Party_Rec_ID As String = Nothing) As List(Of Return_GetPersons)
            Dim _Add As Addresses = New Addresses(cBase)
            Dim param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            param.Party_Rec_ID = Party_Rec_ID
            Dim _retTable As DataTable = _Add.GetList(ClientScreen.Stock_Personnel_Master, param)

            Dim main_data As List(Of Return_GetPersons) = New List(Of Return_GetPersons)
            For Each row As DataRow In _retTable.Rows
                Dim newdata = New Return_GetPersons
                newdata.Name = row.Field(Of String)("Name")
                newdata.Gender = row.Field(Of String)("Gender")
                newdata.DoB = row.Field(Of DateTime?)("DOB")
                newdata.Aadhar_No = row.Field(Of String)("Aadhar_No")
                newdata.Pan_No = row.Field(Of String)("Pan_No")
                newdata.Mobile_No = row.Field(Of String)("Mobile_No")
                newdata.ID = row.Field(Of String)("ID")
                main_data.Add(newdata)
            Next
            Return main_data
        End Function
        ''' <summary>
        ''' GetRequestorList - Load data in Requestor Dropdown
        ''' </summary>
        ''' <returns></returns>
        ''' //this function no used in any screen
        Public Function GetRequestorList() As List(Of Return_GetRequestorList)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Personnels_GetRequestorList, ClientScreen.Profile_Stock)
            Dim _Personnels_data As List(Of Return_GetRequestorList) = New List(Of Return_GetRequestorList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetRequestorList
                    newdata.Requestor_Name = row.Field(Of String)("Requestor Name")
                    newdata.Requestor_Dept = row.Field(Of String)("Requestor Dept")
                    newdata.Requestor_Contact_No = row.Field(Of String)("Requestor Contact No")
                    newdata.Requestor_ID = row.Field(Of Integer)("Requestor ID")

                    _Personnels_data.Add(newdata)
                Next
            End If
            Return _Personnels_data
        End Function
        Public Function GetSkillTypes() As List(Of Return_GetSkillTypes)
            Dim retTable As DataTable = GetMisc("PERSONNEL SKILLS", ClientScreen.Stock_Personnel_Master, "Skill_Type", "Skill_Type_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _Types_data As List(Of Return_GetSkillTypes) = New List(Of Return_GetSkillTypes)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetSkillTypes
                    newdata.Skill_Type = row.Field(Of String)("Skill_Type")
                    newdata.Skill_Type_ID = row.Field(Of String)("Skill_Type_ID")

                    _Types_data.Add(newdata)
                Next
            End If
            Return _Types_data
        End Function
        Public Function GetDesignations() As List(Of Return_GetDesignations)
            Dim retTable As DataTable = GetMisc("DESGINATIONS", ClientScreen.Stock_Personnel_Master, "Designation", "Designation_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _Types_data As List(Of Return_GetDesignations) = New List(Of Return_GetDesignations)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDesignations
                    newdata.Designation = row.Field(Of String)("Designation")
                    newdata.Designation_ID = row.Field(Of String)("Designation_ID")

                    _Types_data.Add(newdata)
                Next
            End If
            Return _Types_data
        End Function
        Public Function GetContractors() As List(Of Return_GetContractors)
            Dim _param As New Param_GetStockPersonnels()
            _param.Skill_Type = "Contractor"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Project, _param)
            Dim _Engg_data As List(Of Return_GetContractors) = New List(Of Return_GetContractors)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetContractors
                    newdata.Contractor_Name = row.Field(Of String)("Name")
                    newdata.Contact_No = row.Field(Of String)("mob_no")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _Engg_data.Add(newdata)
                Next
            End If
            Return _Engg_data
        End Function
        Public Function GetPersonnelRecord(ByVal Rec_ID As Int32) As Return_GetPersonnelRecord
            Dim _Table As DataTable = GetDataListOfRecords(RealServiceFunctions.Personnels_GetPersonnelRecord, ClientScreen.Stock_Personnel_Master, Rec_ID)
            'GetRecordByID(Rec_ID, ClientScreen.Stock_Personnel_Master, RealTimeService.Tables.STOCK_PERSONNEL_INFO, Common.ClientDBFolderCode.DATA)
            ' Dim _data As List(Of Return_GetPersonnelRecord) = New List(Of Return_GetPersonnelRecord)
            Dim newdata = New Return_GetPersonnelRecord
            If (Not (_Table) Is Nothing) Then
                If _Table.Rows.Count > 0 Then
                    Dim row As DataRow = _Table.Rows(0)
                    newdata.AB_ID = row.Field(Of String)("Pers_AB_ID")
                    newdata.Type = row.Field(Of String)("Pers_Type")
                    newdata.Skill_Type_ID = row.Field(Of String)("Pers_Skill_Type_ID")
                    newdata.Payment_Mode = row.Field(Of String)("Pers_Payment_Mode")
                    newdata.PF_NO = row.Field(Of String)("Pers_PF_NO")
                    newdata.Joining_Date = row.Field(Of DateTime?)("Pers_Joining_Date")
                    newdata.Leaving_Date = row.Field(Of DateTime?)("Pers_Leaving_Date")
                    newdata.Other_Details = row.Field(Of String)("Pers_Other_Details")
                    newdata.MainDept_ID = row.Field(Of Int32?)("Pers_Main_Dept_ID")
                    newdata.SubDept_ID = row.Field(Of Int32?)("Pers_sub_Dept_ID")
                    newdata.Designation_ID = row.Field(Of String)("Pers_Designation_ID")
                    newdata.Contractor_ID = row.Field(Of Int32?)("Pers_Contractor_ID")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    newdata.REC_ID = row.Field(Of Integer)("REC_ID")
                    '  _data.Add(newdata)
                End If
            End If
            Return newdata
        End Function
        Public Function GetPersonnelChargesRecord(ByVal Rec_ID As Int32) As Return_GetPersonnelChargesRecord
            Dim _Table As DataTable = GetDataListOfRecords(RealServiceFunctions.Personnels_GetPersonnelChargesRecord, ClientScreen.Stock_Personnel_Master, Rec_ID)
            'GetRecordByID(Rec_ID, ClientScreen.Stock_Personnel_Master, RealTimeService.Tables.STOCK_PERSONNEL_INFO, Common.ClientDBFolderCode.DATA)
            ' Dim _data As List(Of Return_GetPersonnelRecord) = New List(Of Return_GetPersonnelRecord)
            Dim newdata = New Return_GetPersonnelChargesRecord
            If (Not (_Table) Is Nothing) Then
                If _Table.Rows.Count > 0 Then
                    Dim row As DataRow = _Table.Rows(0)
                    newdata.EffDate = row.Field(Of DateTime)("PC_Eff_From")
                    newdata.Charges = row.Field(Of Decimal)("PC_Rate_Charges")
                    newdata.UnitID = row.Field(Of String)("PC_Rate_Unit_ID")
                    newdata.Remarks = row.Field(Of String)("PC_Eff_Remarks")
                End If
            End If
            Return newdata
        End Function
        Public Function Get_Personnel_Usage_Period(ByVal Rec_ID As Int32) As Return_Get_Personnel_Usage_Period
            Dim _Table As DataTable = GetDataListOfRecords(RealServiceFunctions.Personnels_Get_Personnel_Usage_Period, ClientScreen.Stock_Personnel_Master, Rec_ID)
            'GetRecordByID(Rec_ID, ClientScreen.Stock_Personnel_Master, RealTimeService.Tables.STOCK_PERSONNEL_INFO, Common.ClientDBFolderCode.DATA)
            ' Dim _data As List(Of Return_GetPersonnelRecord) = New List(Of Return_GetPersonnelRecord)
            Dim newdata = New Return_Get_Personnel_Usage_Period()
            If (Not (_Table) Is Nothing) Then
                If _Table.Rows.Count > 0 Then
                    For Each row As DataRow In _Table.Rows
                        newdata.MinUsageFromDate = row.Field(Of DateTime?)("from_date")
                        newdata.MaxUsageToDate = row.Field(Of DateTime?)("to_date")
                    Next

                End If
            End If
            Return newdata
        End Function 'Mantis bug 1045 fixed
        Public Function GetPersonnelCharges(PersonnelID As Integer, PeriodFrom As DateTime, PeriodTo As DateTime) As List(Of Return_GetPersonnelCharges)
            Dim _Chrg_Param As New Common_Lib.RealTimeService.Param_GetPersonnelCharges
            _Chrg_Param.PersonnelID = PersonnelID
            _Chrg_Param.Period_From = PeriodFrom
            _Chrg_Param.Period_To = PeriodTo
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Personnels_GetPersonnelCharges, ClientScreen.Profile_Stock, _Chrg_Param)
            Dim _Personnels_data As List(Of Return_GetPersonnelCharges) = New List(Of Return_GetPersonnelCharges)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPersonnelCharges
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Rate_per_Unit = row.Field(Of Decimal)("Rate_per_Unit")
                    newdata.REC_ID = row.Field(Of Integer)("REC_ID") 'Mantis bug 0000362 fixed
                    _Personnels_data.Add(newdata)
                Next
            End If
            Return _Personnels_data
        End Function
        Public Function Get_StockPersonnel_Usage_Count(ByVal PersonnelID As Int32) As Integer
            Return CInt(GetScalarBySP(RealTimeService.RealServiceFunctions.Personnels_Get_StockPersonnel_Usage_Count, ClientScreen.Stock_Personnel_Master, PersonnelID))
        End Function
        Public Function Get_PersonnelCharges_UsageCount(PersonnelID As Integer, effectiveFromDate As DateTime) As Integer
            Dim inparam As New Param_Get_PersonnelCharges_UsageCount
            inparam.PersonnelID = PersonnelID
            inparam.EffectiveFromDate = effectiveFromDate
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Personnels_Get_PersonnelCharges_UsageCount, ClientScreen.Stock_Personnel_Master, inparam))
        End Function
        Public Function Get_Personnel_Count(AB_ID As String, DeptID As Int32, PersType As String, Optional PersonnelID As Int32 = 0) As Integer
            Dim inparam As New Param_Get_Personnel_Count
            inparam.DeptID = DeptID
            inparam.AB_ID = AB_ID
            inparam.PersType = PersType
            inparam.PersonnelID = PersonnelID
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Personnels_Get_Personnel_Count, ClientScreen.Stock_Personnel_Master, inparam))
        End Function
        Public Function Get_PFNo_Count(PFNo As String, Optional PersonnelID As Int32 = 0) As Integer
            Dim inparam As New Param_Get_PFNo_Count
            inparam.PersonnelID = PersonnelID
            inparam.PFNo = PFNo
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Personnels_Get_PFNo_Count, ClientScreen.Stock_Personnel_Master, inparam))
        End Function
        Public Function GetUnits() As List(Of Return_GetUnits)
            Dim retTable As DataTable = GetMisc("UNITS", ClientScreen.Profile_Stock, "Unit", "Unit_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _Stock_Profile_GetUnits_data As List(Of Return_GetUnits) = New List(Of Return_GetUnits)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetUnits
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Unit_ID = row.Field(Of String)("Unit_ID")

                    _Stock_Profile_GetUnits_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_GetUnits_data
        End Function
        Public Function Mark_Personnel_asReopen(PersonnelID As Int32) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Personnels_Mark_Personnel_asReopen, ClientScreen.Stock_Personnel_Master, PersonnelID)
        End Function
        Public Function Mark_Personnel_asLeft(LeavingDate As DateTime, PersonnelID As Int32) As Boolean
            Dim UpParam As New Param_Mark_Personnel_asLeft
            UpParam.LeavingDate = LeavingDate
            UpParam.PersonnelID = PersonnelID
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Personnels_Mark_Personnel_asLeft, ClientScreen.Stock_Personnel_Master, UpParam)
        End Function
        Public Function InsertPersonnelCharges(ByVal InParam As Param_InsertPersonnelCharges) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Personnels_InsertPersonnelCharges, ClientScreen.Stock_Personnel_Master, InParam)
        End Function
        Public Function updatePersonnelCharges(inparam As Param_UpdatePersonnelCharges) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Personnels_UpdatePersonnelCharges, ClientScreen.Stock_Personnel_Master, inparam)
        End Function
        Public Function InsertPersonnel(ByVal InParam As Param_InsertPersonnel) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Personnels_InsertPersonnel, ClientScreen.Stock_Personnel_Master, InParam)
        End Function
        Public Function UpdatePersonnel(inparam As Param_UpdatePersonnel) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Personnels_UpdatePersonnel, ClientScreen.Stock_Personnel_Master, inparam)
        End Function
        Public Function DeletePersonnel(PersID As Int32) As Boolean
            DeleteByCondition("SPS_PERS_ID = " + PersID.ToString(), Tables.STOCK_PERS_SKILLS_MAPPING, ClientScreen.Stock_Personnel_Master)
            DeleteByCondition("PC_PERS_ID = " + PersID.ToString(), Tables.STOCK_PERSONNEL_CHARGES, ClientScreen.Stock_Personnel_Master)
            Return DeleteByCondition("REC_ID = " + PersID.ToString(), Tables.STOCK_PERSONNEL_INFO, ClientScreen.Stock_Personnel_Master)
        End Function
        Public Function DeletePersonnelCharges(ChargesID As Int32) As Boolean
            Return DeleteByCondition("REC_ID = " + ChargesID.ToString(), Tables.STOCK_PERSONNEL_CHARGES, ClientScreen.Stock_Personnel_Master)
        End Function
    End Class
    <Serializable>
    Public Class StockDeptStores
        Inherits SharedVariables

#Region "Param Classes"

        ''' <summary>
        ''' Return class for GetStoreList()
        ''' </summary>
        <Serializable>
        Public Class Return_GetStoreList
            Public Store_Name As String
            Public Dept_Name As String
            Public Sub_Dept_Name As String
            Public Dept_Incharge_Name As String
            Public Store_Incharge_Name As String
            Public Store_Incharge_ID As Integer 'Mantis bug 0000503 fixed

            Public StoreID As Integer
        End Class
        <Serializable>
        Public Class Return_GetRegister
            Public Property Store_Dept_Name As String
            Public Property Category As String
            Public Property Connecting_Main_Dept As String
            Public Property Connecting_Sub_Dept As String
            Public Property Registration_No As String
            Public Property Dept_Incharge As String
            Public Property Contact_No As String
            Public Property Contact_Person As String
            Public Property Premesis_Type As String
            Public Property Premesis_Name As String
            Public Property Is_Central_Store As Boolean
            Public Property Remarks As String
            Public Property Mapped_Locations As String
            Public Property Store_Dept_Address As String
            Public Property Store_Dept_State As String
            Public Property Store_Dept_City As String
            Public Property Add_By As String
            Public Property Add_Date As DateTime
            Public Property Edit_By As String
            Public Property Edit_Date As DateTime
            Public Property ID As Integer
            Public Property Close_Date As DateTime?
            Public Property Closure_Remarks As String
        End Class
        <Serializable>
        Public Class Return_GetDeptPersonnels
            Public Property Name As String
            Public Property Type As String
            Public Property Main_Dept As String
            Public Property Sub_Dept As String
            Public Property ID As Integer
            Public Property Mobile_No As String
            Public Property Skill_Type As String
            Public Property Designation As String
        End Class


        <Serializable>
        Public Class Return_Get_MainSubDept_Personnels
            Public Property Name As String
            Public Property Type As String
            Public Property Main_Dept As String
            Public Property Sub_Dept As String
            Public Property ID As Integer
            Public Property Mobile_No As String
            Public Property Email As String
            Public Property Skill_Type As String
            Public Property Designation As String

            Public Contractor As String

            Public MainDept_ID As Int32?

            Public SubDept_ID As Int32?
        End Class
        <Serializable>
        Public Class Return_Get_StoreDept_Detail
            Public Property Store_Dept_Name As String
            Public Property Category As String
            Public Property Main_DeptID As Int32?
            Public Property Sub_DeptID As Int32?
            Public Property Registeration_No As String
            Public Property InchargeID As Int32?
            Public Property ContactPersonID As Int32?
            Public Property PremesisType As String
            Public Property PremesisID As String
            Public Property IsCentralStore As Boolean
            Public Property MappedLocations As String
        End Class
        <Serializable>
        Public Class Return_GetStoreDeptPremesis
            Public Property Name As String
            Public Property Premises_Type As String
            Public Property Premises_Owner As String
            Public Property Pre_UID As String
            Public Property Pre_Address As String
            Public Property Pre_City As String
            Public Property Pre_State As String
            Public Property ID As String
        End Class
        <Serializable>
        Public Class Return_GetLocations
            Public Location_Name As String
            Public Loc_Id As String
            Public Matched_Type As String
            Public Matched_Name As String
            Public Matched_Instt As String
        End Class
        <Serializable>
        Public Class Return_GetStoreDept
            Public Name As String
            Public InCharge_Name As String
            Public InCharge_ID As Int32?
            Public Type As String
            Public Center As String
            Public ID As Integer
            Public MAIN_DEPT_ID As Int32?
            Public CommonPers As Int32?
            Public MAIN_DEPT_Name As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetRegister() As List(Of Return_GetRegister)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetRegister, ClientScreen.Stock_Dept_Store_Master)
            Dim _StockDeptStores_data As List(Of Return_GetRegister) = New List(Of Return_GetRegister)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetRegister
                    newdata.Store_Dept_Name = row.Field(Of String)("Store_Dept_Name")
                    newdata.Category = row.Field(Of String)("Category")
                    newdata.Connecting_Main_Dept = row.Field(Of String)("Connecting_Main_Dept")
                    newdata.Connecting_Sub_Dept = row.Field(Of String)("Connecting_Sub_Dept")
                    newdata.Registration_No = row.Field(Of String)("Registration_No")
                    newdata.Dept_Incharge = row.Field(Of String)("Dept_Incharge")
                    newdata.Contact_No = row.Field(Of String)("Contact_No")
                    newdata.Contact_Person = row.Field(Of String)("Contact_Person")
                    newdata.Premesis_Type = row.Field(Of String)("Premesis_Type")
                    newdata.Premesis_Name = row.Field(Of String)("Premesis_Name")
                    newdata.Is_Central_Store = row.Field(Of Boolean)("Is_Central_Store")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.Mapped_Locations = row.Field(Of String)("Mapped_Locations")
                    newdata.Store_Dept_Address = row.Field(Of String)("Store_Dept_Address")
                    newdata.Store_Dept_State = row.Field(Of String)("Store_Dept_State")
                    newdata.Store_Dept_City = row.Field(Of String)("Store_Dept_City")
                    newdata.Add_By = row.Field(Of String)("Add_By")
                    newdata.Add_Date = row.Field(Of DateTime)("Add_Date")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit_Date")
                    newdata.Edit_By = row.Field(Of String)("Edit_By")
                    newdata.Close_Date = row.Field(Of DateTime?)("Close_Date")
                    newdata.Closure_Remarks = row.Field(Of String)("Closure_Remarks")
                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function

        ''' <summary>
        ''' GetStoreList-Load data in Store / Dept Dropdown, and returns stores mapped to current user only
        ''' </summary>
        ''' <returns></returns>
        Public Function GetStoreList() As List(Of Return_GetStoreList)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreList, ClientScreen.Profile_Stock)
            Dim _StockDeptStores_data As List(Of Return_GetStoreList) = New List(Of Return_GetStoreList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreList
                    newdata.Store_Name = row.Field(Of String)("Store Name")
                    newdata.Dept_Name = row.Field(Of String)("Dept Name")
                    newdata.Sub_Dept_Name = row.Field(Of String)("Sub Dept Name")
                    newdata.Dept_Incharge_Name = row.Field(Of String)("Dept Incharge Name")
                    newdata.Store_Incharge_Name = row.Field(Of String)("Store Incharge Name")
                    newdata.StoreID = row.Field(Of Integer)("StoreID")

                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        ''' <summary>
        ''' Gets Store under a Specific Dept
        ''' </summary>
        ''' <param name="DeptID"></param>
        ''' <returns></returns>
        Public Function GetStoreList(DeptID As Int32) As List(Of Return_GetStoreList)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreList, ClientScreen.Stock_Dept_Store_Master, DeptID)
            Dim _StockDeptStores_data As List(Of Return_GetStoreList) = New List(Of Return_GetStoreList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreList
                    newdata.Store_Name = row.Field(Of String)("Store Name")
                    newdata.Dept_Name = row.Field(Of String)("Dept Name")
                    newdata.Sub_Dept_Name = row.Field(Of String)("Sub Dept Name")
                    newdata.Dept_Incharge_Name = row.Field(Of String)("Dept Incharge Name")
                    newdata.Store_Incharge_Name = row.Field(Of String)("Store Incharge Name")
                    newdata.StoreID = row.Field(Of Integer)("StoreID")

                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetStoredept(ReturnType As GetStoreDept_ReturnType, screenName As ClientScreen, Optional MainDeptID As Int32 = Nothing, Optional PersonnelID As Int32 = Nothing) As List(Of Return_GetStoreDept)
            Dim Inparam As New Param_GetStoreDept
            Inparam.Return_Type = ReturnType
            Inparam.MainDeptID = MainDeptID
            Inparam.PersonnelID = PersonnelID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreDept, screenName, Inparam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreDept) = New List(Of Return_GetStoreDept)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreDept
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.InCharge_Name = row.Field(Of String)("InCharge_Name")
                    newdata.InCharge_ID = row.Field(Of Int32?)("InCharge_ID")
                    newdata.Center = row.Field(Of String)("Center")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetMainDeptList(screenName As ClientScreen, Optional For_Curr_UID_Only As Boolean = False) As List(Of Return_GetStoreDept)
            Dim Inparam As New Param_GetStoreDept
            Inparam.Return_Type = GetStoreDept_ReturnType.MAIN_DEPT
            Inparam.For_Curr_UID_Only = For_Curr_UID_Only
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreDept, screenName, Inparam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreDept) = New List(Of Return_GetStoreDept)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreDept
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.InCharge_Name = row.Field(Of String)("InCharge_Name")
                    newdata.InCharge_ID = If((IsDBNull(row.Field(Of Int32?)("InCharge_ID"))), System.DBNull.Value, row.Field(Of Int32?)("InCharge_ID"))
                    newdata.Center = row.Field(Of String)("Center")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetSubDeptList(screenName As ClientScreen, mainDeptID As Int32?, Optional PersonnelID As Int32 = Nothing) As List(Of Return_GetStoreDept)
            Dim Inparam As New Param_GetStoreDept
            Inparam.Return_Type = GetStoreDept_ReturnType.DEPT
            Inparam.MainDeptID = mainDeptID
            Inparam.PersonnelID = PersonnelID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreDept, screenName, Inparam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreDept) = New List(Of Return_GetStoreDept)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreDept
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.InCharge_Name = row.Field(Of String)("InCharge_Name")
                    newdata.InCharge_ID = row.Field(Of Int32?)("InCharge_ID")
                    newdata.Center = row.Field(Of String)("Center")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.CommonPers = row.Field(Of Int32?)("CommonPers")

                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetAllDeptList(screenName As ClientScreen) As List(Of Return_GetStoreDept)
            Dim Inparam As New Param_GetStoreDept
            Inparam.Return_Type = GetStoreDept_ReturnType.DEPT
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreDept, screenName, Inparam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreDept) = New List(Of Return_GetStoreDept)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreDept
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.InCharge_Name = row.Field(Of String)("InCharge_Name")
                    newdata.InCharge_ID = row.Field(Of Int32?)("InCharge_ID")
                    newdata.Center = row.Field(Of String)("Center")
                    newdata.Type = row.Field(Of String)("SD_Type")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetAllStoreDeptList(Optional MainDeptID As Int32 = 0) As List(Of Return_GetStoreDept)
            Dim Inparam As New Param_GetStoreDept
            Inparam.Return_Type = GetStoreDept_ReturnType.ALL
            If MainDeptID > 0 Then Inparam.MainDeptID = MainDeptID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreDept, ClientScreen.Stock_Dept_Store_Master, Inparam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreDept) = New List(Of Return_GetStoreDept)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreDept
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.InCharge_Name = row.Field(Of String)("InCharge_Name")
                    newdata.InCharge_ID = row.Field(Of Int32?)("InCharge_ID")
                    newdata.Center = row.Field(Of String)("Center")
                    newdata.Type = row.Field(Of String)("SD_Type")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.MAIN_DEPT_ID = row.Field(Of Int32?)("MAIN_DEPT_ID")
                    newdata.MAIN_DEPT_Name = row.Field(Of String)("MAIN_DEPT_Name")
                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetDeptStoreList(DeptID As Int32) As List(Of Return_GetStoreList)
            Dim InParam As New Param_GetStoreList
            InParam.DeptID = DeptID
            InParam.GetAllStores = False
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreList, ClientScreen.Stock_Dept_Store_Master, InParam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreList) = New List(Of Return_GetStoreList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreList
                    newdata.Store_Name = row.Field(Of String)("Store Name")
                    newdata.Dept_Name = row.Field(Of String)("Dept Name")
                    newdata.Sub_Dept_Name = row.Field(Of String)("Sub Dept Name")
                    newdata.Dept_Incharge_Name = row.Field(Of String)("Dept Incharge Name")
                    newdata.Store_Incharge_Name = row.Field(Of String)("Store Incharge Name")
                    newdata.StoreID = row.Field(Of Integer)("StoreID")

                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetAllStoreList() As List(Of Return_GetStoreList)
            Dim InParam As New Param_GetStoreList
            InParam.GetAllStores = True
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreList, ClientScreen.Stock_Dept_Store_Master, InParam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreList) = New List(Of Return_GetStoreList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreList
                    newdata.Store_Name = row.Field(Of String)("Store Name")
                    newdata.Dept_Name = row.Field(Of String)("Dept Name")
                    newdata.Sub_Dept_Name = row.Field(Of String)("Sub Dept Name")
                    newdata.Dept_Incharge_Name = row.Field(Of String)("Dept Incharge Name")
                    newdata.Store_Incharge_Name = row.Field(Of String)("Store Incharge Name")
                    newdata.Store_Incharge_ID = row.Field(Of Integer)("StoreIncID") 'Mantis bug 0000503 fixed
                    newdata.StoreID = row.Field(Of Integer)("StoreID")

                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetDeptPersonnels(MainDeptID As Int32) As List(Of Return_GetDeptPersonnels)
            Dim _param As New Param_GetStockPersonnels()
            _param.Store_Dept_ID = MainDeptID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Dept_Store_Master, _param)
            Dim _Engg_data As List(Of Return_GetDeptPersonnels) = New List(Of Return_GetDeptPersonnels)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDeptPersonnels
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.Skill_Type = row.Field(Of String)("Skill_Type")
                    newdata.Designation = row.Field(Of String)("Designation")
                    newdata.Mobile_No = row.Field(Of String)("mob_no")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _Engg_data.Add(newdata)
                Next
            End If
            Return _Engg_data
        End Function


        Public Function Get_MainSubDept_Personnels(MainDeptID As Int32?) As List(Of Return_Get_MainSubDept_Personnels)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_Get_MainSubDept_Personnels, ClientScreen.Stock_Dept_Store_Master, MainDeptID)
            Dim _StocksubDeptStores_data As List(Of Return_Get_MainSubDept_Personnels) = New List(Of Return_Get_MainSubDept_Personnels)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Get_MainSubDept_Personnels
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
                    newdata.MainDept_ID = row.Field(Of Int32?)("MAIN_DEPT_ID")
                    newdata.SubDept_ID = row.Field(Of Int32?)("SUB_DEPT_ID")
                    _StocksubDeptStores_data.Add(newdata)
                Next
            End If
            Return _StocksubDeptStores_data
        End Function

        Public Function GetMappedLocations(Store_Dept_ID As Int32) As List(Of Return_GetLocations)
            Dim retTable As DataTable = cBase._AssetLocDBOps.GetStockLocationList(ClientScreen.Profile_Stock, Nothing, Nothing, Store_Dept_ID)
            Dim _Locations_data As List(Of Return_GetLocations) = New List(Of Return_GetLocations)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetLocations
                    newdata.Location_Name = row.Field(Of String)("Location Name")
                    newdata.Loc_Id = row.Field(Of String)("AL_ID")
                    newdata.Matched_Type = row.Field(Of String)("Matched Type")
                    newdata.Matched_Name = row.Field(Of String)("Matched Name")
                    newdata.Matched_Instt = row.Field(Of String)("Matched Instt.")
                    _Locations_data.Add(newdata)
                Next
            End If
            Return _Locations_data
        End Function
        Public Function GetPropertyLocations(Property_ID As String) As List(Of Return_GetLocations)
            Dim retTable As DataTable = cBase._AssetLocDBOps.GetStockLocationList(ClientScreen.Profile_Stock, Nothing, Nothing, 0, Property_ID)
            Dim _Locations_data As List(Of Return_GetLocations) = New List(Of Return_GetLocations)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetLocations
                    newdata.Location_Name = row.Field(Of String)("Location Name")
                    newdata.Loc_Id = row.Field(Of String)("AL_ID")
                    newdata.Matched_Type = row.Field(Of String)("Matched Type")
                    newdata.Matched_Name = row.Field(Of String)("Matched Name")
                    newdata.Matched_Instt = row.Field(Of String)("Matched Instt.")
                    _Locations_data.Add(newdata)
                Next
            End If
            Return _Locations_data
        End Function
        Public Function GetServicePlaceLocations(Service_Place_ID As String) As List(Of Return_GetLocations)
            Dim retTable As DataTable = cBase._AssetLocDBOps.GetStockLocationList(ClientScreen.Profile_Stock, Nothing, Nothing, 0, "", Service_Place_ID)
            Dim _Locations_data As List(Of Return_GetLocations) = New List(Of Return_GetLocations)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetLocations
                    newdata.Location_Name = row.Field(Of String)("Location Name")
                    newdata.Loc_Id = row.Field(Of String)("AL_ID")
                    newdata.Matched_Type = row.Field(Of String)("Matched Type")
                    newdata.Matched_Name = row.Field(Of String)("Matched Name")
                    newdata.Matched_Instt = row.Field(Of String)("Matched Instt.")
                    _Locations_data.Add(newdata)
                Next
            End If
            Return _Locations_data
        End Function
        Public Function GetStoreDeptUsageCount(StoreDeptID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockDeptStores_GetStoreUsageCount, ClientScreen.Stock_Dept_Store_Master, StoreDeptID))
        End Function
        Public Function Get_CentralStorespecific_usage_count(ByVal StoreID As Int32) As Int32
            Return CInt(GetScalarBySP(RealTimeService.RealServiceFunctions.StockDeptStores_Get_CentralStorespecific_usage_count, ClientScreen.Stock_Dept_Store_Master, StoreID))
        End Function
        Public Function GetStoreDeptNumber_UsageCountInstt(Store_Reg_No As String, Optional ID As Int32 = 0) As Integer
            Dim inparam As New Param_GetStoreNoUsageCountInstt()
            inparam.StoreNo = Store_Reg_No
            inparam.StoreRecID = ID
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockDeptStores_GetStoreNoUsageCountInstt, ClientScreen.Stock_Dept_Store_Master, inparam))
        End Function
        Public Function GetStockCountForLocation(LocationID As String) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockDeptStores_GetStockCountForLocation, ClientScreen.Stock_Dept_Store_Master, LocationID))
        End Function
        Public Function GetStoreDeptPremesis(PremesisType As String) As List(Of Return_GetStoreDeptPremesis)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStorePremesis, ClientScreen.Stock_Dept_Store_Master, PremesisType)
            Dim _StockDeptStores_data As List(Of Return_GetStoreDeptPremesis) = New List(Of Return_GetStoreDeptPremesis)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreDeptPremesis
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Premises_Type = row.Field(Of String)("Premises_Type")
                    newdata.Premises_Owner = row.Field(Of String)("Premises_Owner")
                    newdata.Pre_UID = row.Field(Of String)("Pre_UID")
                    newdata.Pre_Address = row.Field(Of String)("Pre_Address")
                    newdata.Pre_City = row.Field(Of String)("Pre_City")
                    newdata.Pre_State = row.Field(Of String)("Pre_State")
                    newdata.ID = row.Field(Of String)("ID")
                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function

        ''' <summary>
        ''' Returns IDs of Stores which are linked to mentioned PersonnelID
        ''' </summary>
        ''' <param name="PersonnelID"></param>
        ''' <returns></returns>
        Public Function GetStoresForPersonnel(PersonnelID As String) As List(Of Int32)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoresForPersonnel, ClientScreen.Stock_Dept_Store_Master, PersonnelID)
            Dim _StockStores_data As List(Of Int32) = New List(Of Int32)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    _StockStores_data.Add(row(0))
                Next
            End If
            Return _StockStores_data
        End Function
        Public Function Get_StoreDept_Detail(DeptID As Int32) As Return_Get_StoreDept_Detail
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_Get_StoreDept_Detail, ClientScreen.Stock_Dept_Store_Master, DeptID)
            Dim newdata = New Return_Get_StoreDept_Detail
            If (Not (retTable) Is Nothing) Then
                If retTable.Rows.Count > 0 Then
                    Dim row As DataRow = retTable.Rows(0)
                    newdata.Store_Dept_Name = row.Field(Of String)("Store_Dept_Name")
                    newdata.Category = row.Field(Of String)("Category")
                    newdata.Main_DeptID = row.Field(Of Int32?)("Main_DeptID")
                    newdata.Sub_DeptID = row.Field(Of Int32?)("Sub_DeptID")
                    newdata.Registeration_No = row.Field(Of String)("Registeration_No")
                    newdata.InchargeID = row.Field(Of Int32?)("InchargeID")
                    newdata.ContactPersonID = row.Field(Of Int32?)("ContactPersonID")
                    newdata.PremesisType = row.Field(Of String)("PremesisType")
                    newdata.PremesisID = row.Field(Of String)("PremesisID")
                    newdata.IsCentralStore = row.Field(Of Boolean)("IsCentralStore")
                    newdata.MappedLocations = row.Field(Of String)("MappedLocations")
                End If
            End If
            Return newdata
        End Function
        Public Function InsertStoreDept(ByVal InParam As Param_InsertStoreDept) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockDeptStores_InsertStoreDept, ClientScreen.Stock_Dept_Store_Master, InParam)
        End Function
        Public Function UpdateStoreDept(UpParam As Param_UpdateStoreDept) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockDeptStores_updateStoreDept, ClientScreen.Stock_Dept_Store_Master, UpParam)
        End Function
        Public Function CloseStoreDept(UpParam As Param_CloseStoreDept) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockDeptStores_CloseStoreDept, ClientScreen.Stock_Dept_Store_Master, UpParam)
        End Function
        Public Function ReopenStoreDept(StoreDeptID As Int32) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockDeptStores_ReopenStoreDept, ClientScreen.Stock_Dept_Store_Master, StoreDeptID)
        End Function
        Public Function DeleteStoreDept(DeptStoreID As Int32) As Boolean
            DeleteByCondition("SL_SD_ID = " + DeptStoreID.ToString(), Tables.STORE_DEPT_LOCATION_INFO, ClientScreen.Stock_Dept_Store_Master)
            DeleteByCondition("REC_ID = " + DeptStoreID.ToString(), Tables.STORE_DEPT_INFO, ClientScreen.Stock_Dept_Store_Master)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class Suppliers
        Inherits SharedVariables

#Region "Param Classes"
        <Serializable>
        Public Class Return_GetRegister
            Public main_Register As List(Of Return_GetRegister_MainGrid)
            Public nested_Register As List(Of Return_GetRegister_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetRegister_MainGrid
            Public Property Sr_No As Int64
            Public Property Supplier As String
            Public Property Company_Code As String
            Public Property Registered_No As String
            Public Property Contact_Person As String
            Public Property PAN_No As String
            Public Property Supplier_Address As String
            Public Property Country As String
            Public Property Suppl_State As String
            Public Property City As String
            Public Property Email As String
            Public Property Other_Details As String
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
            Public Property ID As Integer
        End Class
        <Serializable>
        Public Class Return_GetRegister_NestedGrid
            Public Property Bank As String
            Public Property Branch As String
            Public Property Account_No As String
            Public Property IFSC As String
            Public Property SupplierID As Int32
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
            Public Property ID As Integer
        End Class
        <Serializable>
        Public Class Return_GetPersons
            Public Property Name As String
            Public Property Address As String
            Public Property City As String
            Public Property State As String
            Public Property Country As String
            Public Property Pan_No As String
            Public Property Email As String
            Public Property ContactNo As String
            Public Property ID As String
        End Class
        <Serializable>
        Public Class Return_GetSupplierBanks
            Public Property Sr_No As Int64
            Public Property Bank_Name_ID As String 'BANK_id is stored 'Mantis bug 0000986 fixed
            Public Property SM_BankName As String 'Bank Name will be stored
            Public Property Branch_Name As String
            Public Property Account_No As String
            Public Property IFSC_Code As String
            Public Property ID As Int32
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
        End Class
        <Serializable>
        Public Class Return_GetBanks
            Public Property Bank As String
            Public Property ShortName As String
            Public Property ID As String
        End Class
        <Serializable>
        Public Class Return_GetAllSuppliers
            Public Property Supplier As String
            Public Property CompanyCode As String
            Public Property Contact_No As String
            Public Property ContactPerson As String
            Public Property PAN As String
            Public Property ID As Int32
        End Class

        <Serializable>
        Public Class Return_GetItemMappedSuppliers
            Public Property Supplier As String
            Public Property CompanyCode As String
            Public Property Contact_No As String
            Public Property ContactPerson As String
            Public Property PAN As String
            Public Property ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetItemSupplierMapping
            Public Property Sr_No As Int64
            Public Property ItemName As String
            Public Property Supplier As String
            Public Property ItemID As Int32
            Public Property SupplierID As Int32
            Public Property Mapping_ID As Int32
            Public Property ItemCloseDate As DateTime?
            Public Property Remarks As String
        End Class
        <Serializable>
        Public Class Return_GetSupplierRecord
            Public AB_ID As String
            Public Company_Code As String
            Public Reg_No As String
            Public Contact_Person As String
            Public REC_ADD_ON As DateTime
            Public REC_ADD_BY As String
            Public REC_EDIT_ON As DateTime
            Public REC_EDIT_BY As String
            Public ID As Int32
            Public Other_Details As String
        End Class
        <Serializable>
        Public Class Return_GetSupplierUsage
            Public screen As String
            Public _Date As DateTime
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetRegister() As Return_GetRegister
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.Suppliers_GetRegister, ClientScreen.Stock_Supplier_Master, Nothing)

            Dim _main_data As New Return_GetRegister
            Dim _Supplier_data As List(Of Return_GetRegister_MainGrid) = New List(Of Return_GetRegister_MainGrid)
            Dim _bank_data As List(Of Return_GetRegister_NestedGrid) = New List(Of Return_GetRegister_NestedGrid)
            _main_data.main_Register = _Supplier_data
            _main_data.nested_Register = _bank_data

            If (Not (retDataset) Is Nothing) Then
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetRegister_MainGrid
                    newdata.Sr_No = row.Field(Of Int64)("Sr")
                    newdata.Supplier = row.Field(Of String)("Supplier")
                    newdata.Company_Code = row.Field(Of String)("Company_Code")
                    newdata.Registered_No = row.Field(Of String)("Registered_No")
                    newdata.Contact_Person = row.Field(Of String)("Contact_Person")
                    newdata.PAN_No = row.Field(Of String)("PAN_No")
                    newdata.Supplier_Address = row.Field(Of String)("Supplier_Address")
                    newdata.Country = row.Field(Of String)("Country")
                    newdata.Suppl_State = row.Field(Of String)("Suppl_State")
                    newdata.City = row.Field(Of String)("City")
                    newdata.Email = row.Field(Of String)("Email")
                    newdata.Other_Details = row.Field(Of String)("Other_Details")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    _Supplier_data.Add(newdata)
                Next
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim newdata = New Return_GetRegister_NestedGrid
                    newdata.Bank = row.Field(Of String)("Bank")
                    newdata.Branch = row.Field(Of String)("Branch")
                    newdata.Account_No = row.Field(Of String)("Account_No")
                    newdata.IFSC = row.Field(Of String)("IFSC")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.SupplierID = row.Field(Of Int32)("SupplierID")
                    _bank_data.Add(newdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetPersons(Optional Party_Rec_ID As String = Nothing) As List(Of Return_GetPersons)
            Dim _Add As Addresses = New Addresses(cBase)
            Dim param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            param.Party_Rec_ID = Party_Rec_ID
            Dim _retTable As DataTable = _Add.GetList(ClientScreen.Stock_Supplier_Master, param)

            Dim main_data As List(Of Return_GetPersons) = New List(Of Return_GetPersons)
            For Each row As DataRow In _retTable.Rows
                Dim newdata = New Return_GetPersons
                newdata.Name = row.Field(Of String)("Name")
                newdata.Address = row.Field(Of String)("Address")
                newdata.City = row.Field(Of String)("City")
                newdata.State = row.Field(Of String)("State")
                newdata.Country = row.Field(Of String)("Country")
                newdata.Pan_No = row.Field(Of String)("Pan_No")
                newdata.ContactNo = row.Field(Of String)("ContactNo")
                newdata.Email = row.Field(Of String)("Email")
                newdata.ID = row.Field(Of String)("ID")
                main_data.Add(newdata)
            Next
            Return main_data
        End Function
        Public Function GetSupplierBanks(SupplierID As Int32) As List(Of Return_GetSupplierBanks)
            Dim _retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Suppliers_GetSupplierBanks, ClientScreen.Stock_Supplier_Master, SupplierID)

            Dim main_data As List(Of Return_GetSupplierBanks) = New List(Of Return_GetSupplierBanks)
            For Each row As DataRow In _retTable.Rows
                Dim newdata = New Return_GetSupplierBanks
                newdata.Sr_No = row.Field(Of Int64)("Sr_No")
                newdata.Bank_Name_ID = row.ItemArray(8)  'row.Field(Of String)("BankID")'Mantis bug 0000986 fixed
                newdata.SM_BankName = row.Field(Of String)("Bank_Name") 'Mantis bug 0000986 fixed
                newdata.Branch_Name = row.Field(Of String)("Branch_Name")
                newdata.Account_No = row.Field(Of String)("Account_No")
                newdata.IFSC_Code = row.Field(Of String)("IFSC_Code")
                newdata.AddedBy = row.Field(Of String)("AddedBy")
                newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                newdata.ID = row.Field(Of Int32)("ID")
                main_data.Add(newdata)
            Next
            Return main_data
        End Function
        Public Function GetBanks() As List(Of Return_GetBanks)
            Dim Query As String = "SELECT BI_BANK_NAME Bank, BI_SHORT_NAME ShortName, REC_ID as ID From  BANK_INFO  Where  REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
            Dim _retTable As DataTable = GetBankInfo(Query, Query, ClientScreen.Stock_Supplier_Master)
            Dim main_data As List(Of Return_GetBanks) = New List(Of Return_GetBanks)
            For Each row As DataRow In _retTable.Rows
                Dim newdata = New Return_GetBanks
                newdata.Bank = row.Field(Of String)("Bank")
                newdata.ShortName = row.Field(Of String)("ShortName")
                newdata.ID = row.Field(Of String)("ID")
                main_data.Add(newdata)
            Next
            Return main_data
        End Function
        Public Function GetAllSuppliers(SubItemID As Int32?) As List(Of Return_GetAllSuppliers)
            Dim _retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Suppliers_GetAllSuppliers, ClientScreen.Stock_Supplier_Master, SubItemID)

            Dim main_data As List(Of Return_GetAllSuppliers) = New List(Of Return_GetAllSuppliers)
            If (Not (_retTable) Is Nothing) Then
                For Each row As DataRow In _retTable.Rows
                    Dim newdata = New Return_GetAllSuppliers
                    newdata.Supplier = row.Field(Of String)("Supplier")
                    newdata.ContactPerson = row.Field(Of String)("ContactPerson")
                    ' newdata.Contact_No = row.Field(Of String)("Contact_No")
                    'newdata.CompanyCode = row.Field(Of String)("CompanyCode")
                    newdata.PAN = row.Field(Of String)("PAN")
                    newdata.ID = row.Field(Of Int32)("SuppId")
                    main_data.Add(newdata)
                Next
            End If
            Return main_data
        End Function

        Public Function GetItemMappedSuppliers(SubItemID As Int32?) As List(Of Return_GetItemMappedSuppliers)
            Dim _retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Suppliers_GetItemMappedSuppliers, ClientScreen.Stock_Supplier_Master, SubItemID)

            Dim main_data As List(Of Return_GetItemMappedSuppliers) = New List(Of Return_GetItemMappedSuppliers)
            If (Not (_retTable) Is Nothing) Then
                For Each row As DataRow In _retTable.Rows
                    Dim newdata = New Return_GetItemMappedSuppliers
                    newdata.Supplier = row.Field(Of String)("Supplier")
                    newdata.ContactPerson = row.Field(Of String)("ContactPerson")
                    newdata.Contact_No = row.Field(Of String)("Contact_No")
                    newdata.CompanyCode = row.Field(Of String)("CompanyCode")
                    newdata.PAN = row.Field(Of String)("PAN")
                    newdata.ID = row.Field(Of Int32)("ID")
                    main_data.Add(newdata)
                Next
            End If
            Return main_data
        End Function
        Public Function GetSupplierRecord(SupplierID As Int32) As Return_GetSupplierRecord
            Dim _retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Suppliers_GetSupplierRecord, ClientScreen.Stock_Supplier_Master, SupplierID)

            ' Dim main_data As List(Of Return_GetSupplierRecord) = New List(Of Return_GetSupplierRecord)
            Dim newdata = New Return_GetSupplierRecord
            If _retTable.Rows.Count > 0 Then 'Mantis bug 0000984 fixed
                Dim row As DataRow = _retTable.Rows(0)
                newdata.AB_ID = row.Field(Of String)("Supp_AB_ID")
                newdata.Company_Code = row.Field(Of String)("Supp_Company_Code")
                newdata.Reg_No = row.Field(Of String)("Supp_Reg_No")
                newdata.Contact_Person = row.Field(Of String)("Supp_Contact_Person")
                newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                newdata.ID = row.Field(Of Int32)("REC_ID")
                newdata.Other_Details = row.Field(Of String)("Supp_Other_Details")
                'main_data.Add(newdata)
                '  Next
            End If 'Mantis bug 0000984 fixed
            Return newdata
        End Function
        Public Function GetSupplierUsage(SupplierID As Int32) As Return_GetSupplierUsage
            Dim _retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Suppliers_GetSupplierUsage, ClientScreen.Stock_Supplier_Master, SupplierID)

            ' Dim main_data As List(Of Return_GetSupplierRecord) = New List(Of Return_GetSupplierRecord)
            Dim newdata = New Return_GetSupplierUsage    'Mantis bug 0001295 fixed
            If (_retTable.Rows.Count > 0) Then
                Dim row As DataRow = _retTable.Rows(0)
                newdata.screen = row.Field(Of String)("screen")
                newdata._Date = row.Field(Of DateTime)("Date")
                'main_data.Add(newdata)
                '  Next
            End If         'Mantis bug 0001295 fixed
            Return newdata
        End Function
        Public Function GetItemSupplierMapping(Inparam As Param_GetItemSupplierMapping) As List(Of Return_GetItemSupplierMapping)
            'Dim inparam As New Param_GetItemSupplierMapping()
            'inparam.ItemCategory = ItemCategory
            'inparam.ItemID = ItemID
            'inparam.SupplierID = SupplierID
            Dim _retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Suppliers_GetItemSupplierMapping, ClientScreen.Stock_Supplier_Master, inparam)

            Dim main_data As List(Of Return_GetItemSupplierMapping) = New List(Of Return_GetItemSupplierMapping)
            For Each row As DataRow In _retTable.Rows
                Dim newdata = New Return_GetItemSupplierMapping
                newdata.Sr_No = row.Field(Of Int64)("Sr_No")
                newdata.ItemName = row.Field(Of String)("ItemName")
                newdata.Supplier = row.Field(Of String)("Supplier")
                newdata.ItemID = row.Field(Of Int32)("ItemID")
                newdata.SupplierID = row.Field(Of Int32)("SupplierID")
                newdata.Mapping_ID = row.Field(Of Int32)("Mapping_ID")
                newdata.ItemCloseDate = row.Field(Of DateTime?)("ItemCloseDate")
                newdata.Remarks = row.Field(Of String)("Remarks")
                main_data.Add(newdata)
            Next
            Return main_data
        End Function
        Public Function GetSupplierBankAccUsageCount(BankID As String, AccountNo As String, Optional SupplierBankID As Int32 = 0) As Integer
            Dim inparam As New Param_GetSupplierBankAccUsageCount
            inparam.BankID = BankID
            inparam.AccountNo = AccountNo
            inparam.SupplierBankID = SupplierBankID
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.Suppliers_GetSupplierBankAccUsageCount, ClientScreen.Stock_Supplier_Master, inparam))
        End Function
        Public Function GetSupplier_Party_Duplication_Check(Address_Book_RecID As String, Optional SupplierID As Int32 = 0) As Boolean
            Dim inparam As New Param_GetSupplier_Party_Duplication_Check
            inparam.Address_Book_RecID = Address_Book_RecID
            inparam.SupplierID = SupplierID
            Return CBool(GetScalarBySP(RealTimeService.RealServiceFunctions.Suppliers_GetSupplier_Party_Duplication_Check, ClientScreen.Stock_Supplier_Master, inparam))
        End Function
        Public Function InsertItemSupplierMapping(SupplierID As Int32, SubItemID As String, Remarks As String) As Boolean
            Dim Inparam As New Param_InsertItemSupplierMapping()
            Inparam.Supplier_ID = SupplierID
            Inparam.Sub_Item_ID = SubItemID
            Inparam.Remarks = Remarks
            Return InsertRecord(RealTimeService.RealServiceFunctions.Suppliers_InsertItemSupplierMapping, ClientScreen.Stock_Supplier_Master, Inparam)
        End Function
        Public Function InsertSupplierBank(SupplierID As Int32, BankID As String, BranchName As String, Acc_No As String, IFSC As String) As Boolean
            Dim Inparam As New Param_InsertsupplierBank()
            Inparam.Supplier_ID = SupplierID
            Inparam.BANK_ID = BankID
            Inparam.Branch_Name = BranchName
            Inparam.Acc_No = Acc_No
            Inparam.IFSC_Code = IFSC
            Return InsertRecord(RealTimeService.RealServiceFunctions.Suppliers_InsertSupplierBank, ClientScreen.Stock_Supplier_Master, Inparam)
        End Function
        Public Function UpdateItemSupplierMapping(SupplierID As Int32, SubItemID As Int32, Remarks As String, MappingID As Int32) As Boolean
            Dim Inparam As New Param_UpdateItemSupplierMapping()
            Inparam.Supplier_ID = SupplierID
            Inparam.Sub_Item_ID = SubItemID
            Inparam.Remarks = Remarks
            Inparam.ID = MappingID
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Suppliers_UpdateItemSupplierMapping, ClientScreen.Stock_Supplier_Master, Inparam)
        End Function
        Public Function DeleteItemSupplierMapping(MappingID As Int32) As Boolean
            Return DeleteRecord(MappingID, Tables.SUPPLIER_ITEM_MAPPING, ClientScreen.Stock_Supplier_Master)
        End Function
        Public Function InsertSupplier(inparam As Param_InsertSupplier_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Suppliers_InsertSupplier_Txn, ClientScreen.Stock_Supplier_Master, inparam)
        End Function
        Public Function UpdateSupplier(inparam As Param_UpdateSupplier_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Suppliers_UpdateSupplier_Txn, ClientScreen.Stock_Supplier_Master, inparam)
        End Function
        Public Function DeleteSupplier(SupplierID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Suppliers_DeleteSupplier_Txn, ClientScreen.Stock_Supplier_Master, SupplierID)
        End Function

    End Class
    <Serializable>
    Public Class StockApprovalRequired
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Gets Approval Required Status for passed entity and ID
        ''' </summary>
        ''' <param name="inparam">RefScreen, RefID are mandatory</param>
        ''' <param name="screen">Calling screen</param>
        ''' <returns></returns>
        Public Function Get_Approval_Required(inparam As Param_Get_Approval_Required, screen As ClientScreen) As Boolean
            Return CBool(GetScalarBySP(RealTimeService.RealServiceFunctions.StockApprovalRequired_Get_Approval_Required, screen, inparam))
        End Function
    End Class
#End Region
End Class