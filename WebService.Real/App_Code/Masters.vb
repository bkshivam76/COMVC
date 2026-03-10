Imports System.Data
Imports Microsoft.VisualBasic
Namespace Real
    <Serializable>
    Public Class Personnels
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetPersonnelCharges
            Public Period_From As DateTime
            Public Period_To As DateTime
            Public PersonnelID As Integer
        End Class
        <Serializable>
        Public Class Param_Get_PersonnelCharges_UsageCount
            Public EffectiveFromDate As DateTime
            Public PersonnelID As Integer
        End Class
        <Serializable>
        Public Class Param_Mark_Personnel_asLeft
            Public LeavingDate As DateTime
            Public PersonnelID As Integer
        End Class
        <Serializable>
        Public Class Param_InsertPersonnel
            Public AB_ID As String
            Public Type As String
            ''' <summary>
            ''' Contains Comma separated list of Skill Types for selected Personnel
            ''' </summary>
            Public Skill_Type_IDs As String
            Public Payment_Mode As String
            Public Dept_ID As Int32?
            Public Contractor_ID As String
            Public PF_No As String
            Public Joining_Date As DateTime?
            Public Designation_ID As String
            Public Other_Details As String
        End Class
        <Serializable>
        Public Class Param_UpdatePersonnel
            Public AB_ID As String
            Public Type As String
            ''' <summary>
            ''' Contains Comma separated list of Skill Types for selected Personnel
            ''' </summary>
            Public Skill_Type_IDs As String
            Public Payment_Mode As String
            Public Dept_ID As Int32?
            Public Contractor_ID As String
            Public PF_No As String
            Public Joining_Date As DateTime?
            Public Designation_ID As String
            Public Other_Details As String
            Public PersonnelID As Int32
        End Class
        <Serializable>
        Public Class Param_InsertPersonnelCharges
            Public Pers_ID As Int32
            Public Eff_From As DateTime
            Public Rate_Charges As Decimal
            Public Rate_Unit_ID As String
            Public Eff_Remarks As String
        End Class
        <Serializable>
        Public Class Param_UpdatePersonnelCharges
            Public Pers_ChargeID As Int32
            Public Pers_ID As Int32
            Public Eff_From As DateTime
            Public Rate_Charges As Decimal
            Public Rate_Unit_ID As String
            Public Eff_Remarks As String
        End Class
        <Serializable>
        Public Class Param_Get_Personnel_Count
            Public AB_ID As String
            Public DeptID As Int32
            Public PersType As String
            Public PersonnelID As Int32 = 0
        End Class
        <Serializable>
        Public Class Param_Get_PFNo_Count
            Public PFNo As String
            Public PersonnelID As Int32 = 0
        End Class
#End Region
        Public Shared Function GetRegister(inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PersonnelMaster_Register"
            Dim params() As String = {"@UserID", "@CENID"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetRequestorList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Requestor_List"
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, SPName, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPersonnelCharges(inParam As Param_GetPersonnelCharges, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select T0.PC_Rate_Unit_ID UnitID, MISC_NAME AS Unit,  T0.PC_Rate_Charges Rate_per_Unit, T0.REC_ID AS REC_ID From Stock_Personnel_Charges T0 INNER JOIN MISC_INFO UNIT ON PC_Rate_Unit_ID = UNIT.REC_ID Where T0.PC_Pers_ID = " + inParam.PersonnelID.ToString() + " AND (T0.PC_Eff_From <= '" + inParam.Period_From.ToString(Common.Server_Date_Format_Long) + "'  AND '" + inParam.Period_From.ToString(Common.Server_Date_Format_Long) + "' <= COALESCE(T0.PC_Eff_To,'" + inParam.Period_From.ToString(Common.Server_Date_Format_Long) + "') ) AND (T0.PC_Eff_From <=  '" + inParam.Period_To.ToString(Common.Server_Date_Format_Long) + "' AND '" + inParam.Period_To.ToString(Common.Server_Date_Format_Long) + "'  <= COALESCE(T0.PC_Eff_To,'" + inParam.Period_To.ToString(Common.Server_Date_Format_Long) + "'))" 'Mantis bug 0000362 fixed
            Return dbService.List(ConnectOneWS.Tables.STOCK_PERSONNEL_CHARGES, Query, ConnectOneWS.Tables.JOB_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetPersonnelRecord(PersID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Dim Query As String = "SELECT [Pers_Cen_ID],[Pers_AB_ID],[Pers_Type],[Pers_Payment_Mode],[Pers_PF_No],[Pers_Joining_Date],[Pers_Leaving_Date],[Pers_Other_Details],pers.[REC_ADD_ON] ,pers.[REC_ADD_BY],pers.[REC_EDIT_ON],pers.[REC_EDIT_BY],pers.[REC_ID],[Pers_Designation_ID],[Pers_Contractor_ID] ,COALESCE(pers_Dept.SD_Dept_ID,pers_dept_id) Pers_Main_Dept_ID ,CASE WHEN pers_dept.SD_Dept_ID is null then null else pers_dept_id end as Pers_sub_Dept_ID ,STUFF((Select ',' + skill.SPS_Skill_ID from Stock_Pers_Skills_Mapping skill where skill.SPS_Pers_ID = pers.REC_ID for XML PATH('')),1,1, '')  as Pers_SKILL_TYPE_ID from Stock_Personnel_Info pers LEFT JOIN Store_Dept_Info AS pers_Dept on Pers_Dept_ID = pers_Dept.REC_ID WHERE pers.REC_ID = " + PersID.ToString() + ""


            Dim Query As String = "SELECT [Pers_Cen_ID],(Select A1.REC_ID from address_book A Inner Join address_book A1 on A.C_ORG_REC_ID = A1.C_ORG_REC_ID Where A.REC_ID = [Pers_AB_ID] AND A1.C_COD_YEAR_ID =" + inBasicParam.openYearID.ToString() + ") AS Pers_AB_ID,[Pers_Type],[Pers_Payment_Mode],[Pers_PF_No],[Pers_Joining_Date],[Pers_Leaving_Date],[Pers_Other_Details],pers.[REC_ADD_ON] ,pers.[REC_ADD_BY],pers.[REC_EDIT_ON],pers.[REC_EDIT_BY],pers.[REC_ID],[Pers_Designation_ID],[Pers_Contractor_ID] ,COALESCE(pers_Dept.SD_Dept_ID,pers_dept_id) Pers_Main_Dept_ID ,CASE WHEN pers_dept.SD_Dept_ID is null then null else pers_dept_id end as Pers_sub_Dept_ID ,STUFF((Select ',' + skill.SPS_Skill_ID from Stock_Pers_Skills_Mapping skill where skill.SPS_Pers_ID = pers.REC_ID for XML PATH('')),1,1, '') as Pers_SKILL_TYPE_ID from Stock_Personnel_Info pers LEFT JOIN Store_Dept_Info AS pers_Dept on Pers_Dept_ID = pers_Dept.REC_ID WHERE pers.REC_ID = " + PersID.ToString() + ""

            'mantis #69'



            Return dbService.GetSingleRecord(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetPersonnelChargesRecord(PersChargesID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT * from Stock_Personnel_Charges WHERE REC_ID = " + PersChargesID.ToString() + ""
            Return dbService.GetSingleRecord(ConnectOneWS.Tables.STOCK_PERSONNEL_CHARGES, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_CHARGES.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_StockPersonnel_Usage_Count(ByVal PersonnelID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_StockPersonnel_Usage"
            Dim params() As String = {"@PersonnelID"}
            Dim values() As Object = {PersonnelID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_PersonnelCharges_UsageCount(inparam As Param_Get_PersonnelCharges_UsageCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE(SUM(A.MP_Usage),0) MP_Usage  From (Select COUNT(T0.REC_ID) MP_Usage From Job_Manpower_Usage T0 Where T0.JM_Person_ID =  " + inparam.PersonnelID.ToString() + "  and T0.JM_Period_From >=  '" + inparam.EffectiveFromDate.ToString(Common.DateFormatLong) + "'  Union All Select COUNT(T0.SPMU_Prod_id) MP_Usage From Stock_Production_Manpower_Usage T0 Where T0.SPMU_Person_ID = " + inparam.PersonnelID.ToString() + " and T0.SPMU_Period_From >= '" + inparam.EffectiveFromDate.ToString(Common.DateFormatLong) + "' ) A"
            Return dbService.GetScalar(ConnectOneWS.Tables.JOB_MANPOWER_USAGE, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_Personnel_Count(inparam As Param_Get_Personnel_Count, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE((Select sum(1) From Stock_Personnel_Info T0 Where T0.Pers_AB_ID = '" + inparam.AB_ID.ToString() + "' and T0.pers_dept_id = COALESCE(" + inparam.DeptID.ToString() + ",T0.pers_dept_id) and T0.Pers_Type = '" + inparam.PersType.ToString() + "' AND REC_ID <> " + inparam.PersonnelID.ToString() + "),0) Person_Exists" '//Mantis bug 0000044 fixed
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_PFNo_Count(inparam As Param_Get_PFNo_Count, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE((Select T0.REC_ID from Stock_Personnel_Info T0 Where T0.Pers_PF_No = '" + inparam.PFNo.ToString() + "' AND REC_ID <> " + inparam.PersonnelID.ToString() + "),0) PFNo_Exists"
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_Personnel_Usage_Period(PersonnelID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select min(from_date) from_date, max(to_date) to_date From (Select min(t0.JM_Period_From) from_date,  max(t0.JM_Period_To) to_date from Job_Manpower_Usage T0 Where T0.JM_Person_ID = " + PersonnelID.ToString() + " Union All Select min(t0.SPMU_Period_From) from_date,  max(t0.SPMU_Period_To) to_date From Stock_Production_Manpower_Usage T0 Where T0.SPMU_Person_ID = " + PersonnelID.ToString() + " ) A"
            Return dbService.List(ConnectOneWS.Tables.JOB_MANPOWER_USAGE, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Mark_Personnel_asLeft(inparam As Param_Mark_Personnel_asLeft, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "UPDATE Stock_Personnel_Info SET Pers_Leaving_Date =  '" + inparam.LeavingDate.ToString(Common.Server_Date_Format_Short) + "' Where REC_ID = " + inparam.PersonnelID.ToString() + " "
            dbService.Update(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, inBasicParam)
            Return True
        End Function
        Public Shared Function Mark_Personnel_asReopen(PersonnelID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "UPDATE Stock_Personnel_Info SET Pers_Leaving_Date =  NULL Where REC_ID = " + PersonnelID.ToString() + " "
            dbService.Update(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertPersonnel(ByVal InParam As Param_InsertPersonnel, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Stock_Personnel"
            Dim params() As String = {"@Cen_ID", "@User_ID", "@Pers_AB_ID", "@Pers_Type", "@Pers_Payment_Mode", "@Pers_Dept_ID", "@Pers_Contractor_ID", "@Pers_PF_No", "@Pers_Joining_Date", "@Pers_Designation_ID", "@Pers_Other_Details"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openUserID, InParam.AB_ID, InParam.Type, InParam.Payment_Mode, InParam.Dept_ID, InParam.Contractor_ID, InParam.PF_No, InParam.Joining_Date, InParam.Designation_ID, InParam.Other_Details}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 36, 50, 50, 4, 36, 50, 12, 36, 8000}
            Dim PersonnelID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            If InParam.Skill_Type_IDs <> Nothing Then
                Dim Skills As String() = InParam.Skill_Type_IDs.Split(",")
                For Each Skill As String In Skills
                    If Skill.Trim().Length > 0 Then InsertPersonnelSkills(PersonnelID, Skill.Trim(), inBasicParam)
                Next
            End If
            Return PersonnelID
        End Function
        Private Shared Function InsertPersonnelSkills(ByVal PersonnelID As Int32, SkillID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Stock_PersSkills_Mapping"
            Dim params() As String = {"@SPS_Pers_ID", "@SPS_Skill_ID", "@UserID"}
            Dim values() As Object = {PersonnelID, SkillID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PERS_SKILLS_MAPPING, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Private Shared Function DeleteAllPersonnelSkills(ByVal PersonnelID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_PERS_SKILLS_MAPPING, "SPS_pers_ID=" + PersonnelID.ToString(), inBasicParam)
            Return True
        End Function
        Public Shared Function UpdatePersonnel(ByVal InParam As Param_UpdatePersonnel, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_Stock_Personnel"
            Dim params() As String = {"@PersonnelID", "@User_ID", "@Pers_AB_ID", "@Pers_Type", "@Pers_Payment_Mode", "@Pers_Dept_ID", "@Pers_Contractor_ID", "@Pers_PF_No", "@Pers_Joining_Date", "@Pers_Designation_ID", "@Pers_Other_Details"}
            Dim values() As Object = {InParam.PersonnelID, inBasicParam.openUserID, InParam.AB_ID, InParam.Type, InParam.Payment_Mode, InParam.Dept_ID, InParam.Contractor_ID, InParam.PF_No, InParam.Joining_Date, InParam.Designation_ID, InParam.Other_Details}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 36, 50, 50, 4, 36, 50, 12, 36, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            DeleteAllPersonnelSkills(InParam.PersonnelID, inBasicParam)
            If InParam.Skill_Type_IDs <> Nothing Then
                Dim Skills As String() = InParam.Skill_Type_IDs.Split(",")
                For Each Skill As String In Skills
                    If Skill.Length > 0 Then InsertPersonnelSkills(InParam.PersonnelID, Skill, inBasicParam)
                Next
            End If
            Return True
        End Function
        Public Shared Function InsertPersonnelCharges(ByVal InParam As Param_InsertPersonnelCharges, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_StockPersonnel_Charges"
            Dim params() As String = {"@PC_Pers_ID", "@PC_Eff_From", "@PC_Rate_Charges", "@PC_Rate_Unit_ID", "@PC_Eff_Remarks", "@UserID"}
            Dim values() As Object = {InParam.Pers_ID, InParam.Eff_From, InParam.Rate_Charges, InParam.Rate_Unit_ID, InParam.Eff_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 19, 36, 255, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PERSONNEL_CHARGES, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function updatePersonnelCharges(ByVal InParam As Param_UpdatePersonnelCharges, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_StockPers_Charges"
            Dim params() As String = {"@PC_Eff_From", "@PC_Rate_Charges", "@PC_Rate_Unit_ID", "@PC_Eff_Remarks", "@UserID", "@Pers_ChargeID"}
            Dim values() As Object = {InParam.Eff_From, InParam.Rate_Charges, InParam.Rate_Unit_ID, InParam.Eff_Remarks, inBasicParam.openUserID, InParam.Pers_ChargeID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime2, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 19, 36, 255, 255, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PERSONNEL_CHARGES, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

    End Class
    <Serializable>
    Public Class StockDeptStores
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetStoreList
            Public DeptID As Int32?
            Public GetAllStores As Boolean = False
        End Class
        <Serializable>
        Public Class Param_InsertStoreDept
            Public SD_Name As String
            Public Dept_Type As String
            Public Sub_Dept_ID As Int32?
            Public Incharge_ID As Int32?
            Public Reg_No As String
            Public Contact_Person_ID As Int32?
            Public Remarks As String
            Public Dept_ID As Int32?
            Public Is_Central_Store As Boolean
            Public Premesis_Type As Premesis_Type
            Public Premesis_ID As String
            Public mapped_Locations As List(Of Param_InsertStoreDept_Mapping)
        End Class
        <Serializable>
        Public Class Param_UpdateStoreDept
            Public SD_Name As String
            Public Dept_Type As String
            Public Sub_Dept_ID As Int32?
            Public Incharge_ID As Int32?
            Public Reg_No As String
            Public Contact_Person_ID As Int32?
            Public Remarks As String
            Public Dept_ID As Int32?
            Public Is_Central_Store As Boolean
            Public Premesis_Type As Premesis_Type
            Public Premesis_ID As String
            Public ID As Int32
            Public mapped_Locations As List(Of Param_InsertStoreDept_Mapping)
        End Class
        <Serializable>
        Public Class Param_InsertStoreDept_Mapping
            ''' <summary>
            ''' Leave it blank please
            ''' </summary>
            Public Dept_ID As Int32
            Public Location_ID As String
        End Class
        <Serializable>
        Public Class Param_CloseStoreDept
            Public Store_Dept_ID As Int32
            Public Close_Date As DateTime
            Public Close_Remarks As String
        End Class
        <Serializable>
        Public Enum Premesis_Type
            Service_Place
            Service_Property
        End Enum
        <Serializable>
        Public Enum GetStoreDept_ReturnType
            ''' <summary>
            ''' Return all Stores and Dept
            ''' </summary>
            ALL
            ''' <summary>
            ''' Return Stores onlu
            ''' </summary>
            STORE
            ''' <summary>
            ''' Return Departments only
            ''' </summary>
            DEPT
            ''' <summary>
            ''' Return Main Dept only
            ''' </summary>
            MAIN_DEPT
        End Enum
        <Serializable>
        Public Class Param_GetStoreDept
            Public MainDeptID As Int32? = Nothing
            Public PersonnelID As Int32? = Nothing
            Public Return_Type As GetStoreDept_ReturnType = GetStoreDept_ReturnType.ALL
            Public For_Curr_UID_Only As Boolean = False
        End Class
        <Serializable>
        Public Class Param_GetStoreNoUsageCountInstt
            Public StoreNo As String
            Public StoreRecID As Int32
        End Class
#End Region

        Public Shared Function GetStoreDept(inparam As Param_GetStoreDept, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Store_Dept"
            Dim params() As String = {"@Main_Dept_ID", "@RETURN_TYPE", "@PERSONNEL_ID", "@CENID"}
            Dim values() As Object = {inparam.MainDeptID, inparam.Return_Type.ToString(), inparam.PersonnelID, IIf(inparam.For_Curr_UID_Only, inBasicParam.openCenID, Nothing)}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 20, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        ''' <summary>
        ''' Get  Store list for whole open instt mapped to logged in USER . If dept param is mentioned then Stores are returned for that dept only 
        ''' </summary>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' 
        Public Shared Function GetStoreList(inparam As Param_GetStoreList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Store_List"
            Dim params() As String = {"user_id", "CEN_ID", "@DeptID", "@SHOW_ALL_STORES"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openCenID, inparam.DeptID, inparam.GetAllStores}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Boolean}
            Dim lengths() As Integer = {255, 4, 4, 2}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function


        Public Shared Function Get_MainSubDept_Personnels(StoreDeptID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_MainSubDept_Pers"
            Dim params() As String = {"@STORE_DEPT_ID", "CEN_ID"}
            Dim values() As Object = {StoreDeptID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Shows Stores for Logged in User 
        ''' </summary>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        Public Shared Function GetStoreList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Store_List"
            Dim params() As String = {"user_id", "CEN_ID"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetRegister(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_StoreDept_Master_Register"
            Dim params() As String = {"CENID"}
            Dim values() As Object = {inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStoreDeptPremesis(PremesisType As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Premesis_ByType"
            Dim params() As String = {"@PremesisType", "@CENID", "@year_ID"}
            Dim values() As Object = {PremesisType, inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {20, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, SPName, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_StoreDept_Detail(StoreDeptID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_StoreDept_Detail"
            Dim params() As String = {"@Store_ID"}
            Dim values() As Object = {StoreDeptID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {20}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStoreDeptUsageCount(StoreDeptID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim Query As String = "Select COALESCE(SUM(Store_Count),0) Store_Count From (Select 1 Store_Count From Stock_Info A Where A.Stock_Store_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Sub_item_Store_Mapping A Where A.Map_Store_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from User_Order A Where A.UO_store_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Requisition_Request_Info A Where A.RR_Dept_Store_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Stock_Personnel_Info A Where A.Pers_Dept_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Stock_Transfer_Order A Where (A.STO_Main_Dept_To_ID =  " + StoreDeptID.ToString() + " Or A.STO_Sub_Dept_To_ID = " + StoreDeptID.ToString() + ") ) A"
            ' Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
            Dim SPName As String = "Get_StoreDept_Usage"
            Dim params() As String = {"@StoreDeptID"}
            Dim values() As Object = {StoreDeptID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {20}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStoreNoUsageCountInstt(inparam As Param_GetStoreNoUsageCountInstt, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE(SUM(1),0) Store_Count From Store_Dept_Info SD INNER JOIN CENTRE_INFO AS CI ON SD_Cen_ID = CI.CEN_ID  Where CEN_INS_ID=  (SELECT CEN_INS_ID FROM centre_info WHERE CEN_ID = " + inBasicParam.openCenID.ToString() + ") AND SD_Reg_No = '" + inparam.StoreNo.ToString() + "' AND SD.REC_ID <> " + inparam.StoreRecID.ToString() + ""
            Return dbService.GetScalar(ConnectOneWS.Tables.STORE_DEPT_INFO, Query, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetStockCountForLocation(LocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COUNT(T2.REC_ID) Loc_Count From Stock_Info T2 Where T2.Stock_Location_ID = '" + LocationID.ToString() + "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetStoresForPersonnel(PersonnelID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select StoreID as  From (Select T.Pers_Dept_ID StoreID from Stock_Personnel_Info T Inner Join Store_Dept_Info T0 on T.Pers_Dept_ID = T0.REC_ID and UPPER(T0.SD_Type) = 'STORE' Where T.REC_ID = " + PersonnelID.ToString() + " Union All Select T.REC_ID StoreID from Store_Dept_Info T Where T.SD_Incharge_ID = " + PersonnelID.ToString() + " and UPPER(T.SD_Type) = 'STORE') T Where T.Store_Dept is Not Null"
            Return dbService.List(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_CentralStorespecific_usage_count(ByVal StoreID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "get_CentralStorespecific_usage_count"
            Dim params() As String = {"@SD_ID"}
            Dim values() As Object = {StoreID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function InsertStoreDept(ByVal InParam As Param_InsertStoreDept, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Dept_Store"
            Dim params() As String = {"@SD_Cen_ID", "@SD_Name", "@SD_Type", "@SD_Sub_Dept_ID", "@SD_Incharge_ID", "@SD_Reg_No", "@SD_Contact_Person_ID", "@SD_Remarks", "@user_id", "@SD_Dept_ID", "@SD_Is_Central_Store", "@SD_Premesis_Type", "@SD_Premesis_ID"}
            Dim values() As Object = {inBasicParam.openCenID, InParam.SD_Name, InParam.Dept_Type, InParam.Sub_Dept_ID, InParam.Incharge_ID, InParam.Reg_No, InParam.Contact_Person_ID, InParam.Remarks, inBasicParam.openUserID, InParam.Dept_ID, InParam.Is_Central_Store, IIf(InParam.Premesis_Type = Premesis_Type.Service_Place, "Service Place", "Property"), InParam.Premesis_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 20, 4, 4, 50, 4, 8000, 255, 4, 1, 50, 36}
            Dim DeptID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            'Add location mapping
            For Each dep_loc_map As Param_InsertStoreDept_Mapping In InParam.mapped_Locations
                dep_loc_map.Dept_ID = DeptID
                InsertStoreDeptMapping(dep_loc_map, inBasicParam)
            Next

            Return DeptID
        End Function
        Private Shared Function InsertStoreDeptMapping(ByVal InParam As Param_InsertStoreDept_Mapping, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_DeptStore_Location_Mapping"
            Dim params() As String = {"@SL_SD_ID", "@SL_LOC_ID"}
            Dim values() As Object = {InParam.Dept_ID, InParam.Location_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 36}
            dbService.InsertBySP(ConnectOneWS.Tables.STORE_DEPT_LOCATION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdateStoreDept(ByVal InParam As Param_UpdateStoreDept, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Dept_Store"
            Dim params() As String = {"@SD_Name", "@SD_Type", "@SD_Sub_Dept_ID", "@SD_Incharge_ID", "@SD_Reg_No", "@SD_Contact_Person_ID", "@SD_Remarks", "@user_id", "@SD_Dept_ID", "@SD_Is_Central_Store", "@SD_Premesis_Type", "@SD_Premesis_ID", "@SD_ID"}
            Dim values() As Object = {InParam.SD_Name, InParam.Dept_Type, InParam.Sub_Dept_ID, InParam.Incharge_ID, InParam.Reg_No, InParam.Contact_Person_ID, InParam.Remarks, inBasicParam.openUserID, InParam.Dept_ID, InParam.Is_Central_Store, IIf(InParam.Premesis_Type = Premesis_Type.Service_Place, "Service Place", "Property"), InParam.Premesis_ID, InParam.ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 20, 4, 4, 50, 4, 8000, 255, 4, 1, 50, 36, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            'Delete existing Location Mapping
            Remove_All_Store_Dept_Location_Mappings(InParam.ID, inBasicParam)
            'Add location mapping
            For Each dep_loc_map As Param_InsertStoreDept_Mapping In InParam.mapped_Locations
                dep_loc_map.Dept_ID = InParam.ID
                InsertStoreDeptMapping(dep_loc_map, inBasicParam)
            Next
            Return True
        End Function
        Public Shared Function CloseStoreDept(ByVal InParam As Param_CloseStoreDept, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "UPDATE Store_Dept_Info SET SD_Close_Date = '" + InParam.Close_Date.ToString(Common.DateFormatLong) + "', SD_Close_Remarks='" + InParam.Close_Remarks + "' WHERE REC_ID = " + InParam.Store_Dept_ID.ToString()
            dbService.Update(ConnectOneWS.Tables.STORE_DEPT_INFO, Query, inBasicParam)
            Return True
        End Function
        Public Shared Function ReopenStoreDept(ByVal Store_Dept_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "UPDATE Store_Dept_Info SET SD_Close_Date = NULL, SD_Close_Remarks=NULL WHERE REC_ID = " + Store_Dept_ID.ToString()
            dbService.Update(ConnectOneWS.Tables.STORE_DEPT_INFO, Query, inBasicParam)
            Return True
        End Function
        'Public Shared Function Delete_Store_Dept(DeptStoreID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    dbService.Delete(ConnectOneWS.Tables.STORE_DEPT_INFO, DeptStoreID.ToString(), inBasicParam)
        '    Return True
        'End Function
        Private Shared Function Remove_All_Store_Dept_Location_Mappings(DeptStoreID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.STORE_DEPT_LOCATION_INFO, "SL_SD_ID = " + DeptStoreID.ToString(), inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class StockApprovalRequired
#Region "Param Classes"
        <Serializable>
        Public Enum Entity_Screens
            Job
            Project
            Production_Order
            Purchase_Order
            Requisition_Request
            Transfer_Order
            User_Order
        End Enum
        <Serializable>
        Public Class Param_Get_Approval_Required
            ''' <summary>
            ''' ID of Store for which entity is raised
            ''' </summary>
            Public Store_ID As Int32?
            ''' <summary>
            ''' ID of Stock Item used (if single)
            ''' </summary>
            Public Stock_Item_ID As Int32?
            ''' <summary>
            ''' Total Amount for Entity
            ''' </summary>
            Public Amount As Decimal?
            ''' <summary>
            ''' Date of Transaction of Entity
            ''' </summary>
            Public TxnDate As DateTime?
            ''' <summary>
            ''' Qty of Item 
            ''' </summary>
            Public Qty As Decimal?
            ''' <summary>
            ''' Entity Screen name for which approval is checked
            ''' </summary>
            Public RefScreen As Entity_Screens
            ''' <summary>
            ''' PK ID of Entity for which Approval Reqd is checked
            ''' </summary>
            Public RefID As Int32
        End Class
#End Region

        Public Shared Function Get_Approval_Required(inparam As Param_Get_Approval_Required, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Approval_Required"
            Dim params() As String = {"@Store_ID", "@User_ID", "@Stock_Item_ID", "@Amt", "@TxnDate", "@Qty", "@RefScreen", "@RefID"}
            Dim values() As Object = {inparam.Store_ID, inBasicParam.openUserID, inparam.Stock_Item_ID, inparam.Amount, inparam.TxnDate, inparam.Qty, inparam.RefScreen, inparam.RefID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 255, 4, 19, 12, 11, 255, 4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
    End Class
    <Serializable>
    Public Class Suppliers
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetItemSupplierMapping
            Public ItemCategory As String
            Public ItemID As Int32?
            Public SupplierID As Int32?
        End Class
        <Serializable>
        Public Class Param_InsertItemSupplierMapping
            Public Supplier_ID As Int32
            Public Sub_Item_ID As String
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_InsertSupplier_Txn
            Inherits Param_InsertSupplier
            ''' <summary>
            ''' params containing details of added banks during insert transaction
            ''' </summary>
            Public Added_Banks As List(Of Param_InsertsupplierBank)
        End Class
        <Serializable>
        Public Class Param_InsertSupplier
            Public AB_ID As String
            Public Company_Code As String
            Public Reg_No As String
            Public Contact_Person As String
            Public Other_Details As String
        End Class
        <Serializable>
        Public Class Param_UpdateSupplier_Txn
            Inherits Param_UpdateSupplier
            ''' <summary>
            ''' params containing details of added banks during update transaction
            ''' </summary>
            Public Added_Banks As List(Of Param_InsertsupplierBank)
            ''' <summary>
            ''' params containing details of updated banks during update transaction
            ''' </summary>
            Public Updated_Banks As List(Of Param_UpdatesupplierBank)
            ''' <summary>
            ''' params containing IDs of deleted banks during update transaction
            ''' </summary>
            Public Deleted_Banks_IDs As List(Of Int32)
        End Class
        <Serializable>
        Public Class Param_UpdateSupplier
            Inherits Param_InsertSupplier
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_InsertsupplierBank
            Public Supplier_ID As Int32
            Public BANK_ID As String
            Public Branch_Name As String
            Public Acc_No As String
            Public IFSC_Code As String
        End Class
        <Serializable>
        Public Class Param_UpdatesupplierBank
            Public ID As Int32
            Public BANK_ID As String
            Public Branch_Name As String
            Public Acc_No As String
            Public IFSC_Code As String
        End Class
        <Serializable>
        Public Class Param_UpdateItemSupplierMapping
            Inherits Param_InsertItemSupplierMapping
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_GetSupplierBankAccUsageCount
            Public BankID As String
            Public AccountNo As String
            Public SupplierBankID As Int32 = 0
        End Class
        <Serializable>
        Public Class Param_GetSupplier_Party_Duplication_Check
            Public Address_Book_RecID As String
            Public SupplierID As Int32 = 0
        End Class
#End Region
        Public Shared Function GetRegister(inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "get_supplier_master_register"
            Dim params() As String = {"CENID"}
            Dim values() As Object = {inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetSupplierBanks(SupplierID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "get_Supplier_Banks"
            Dim params() As String = {"@SupplierID"}
            Dim values() As Object = {SupplierID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_SUPPLIER_BANK, SPName, ConnectOneWS.Tables.STOCK_SUPPLIER_BANK.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetAllSuppliers(SubItemID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim sitemid As String = "NULL"
            If (Not SubItemID Is Nothing) Then
                sitemid = SubItemID.ToString()
            End If
            Dim Query As String = "Select T1.C_NAME Supplier,T0.Supp_Contact_Person ContactPerson,T1.C_PAN_NO PAN ,T0.REC_ID SuppId From Stock_Supplier_Info T0 Inner join address_book T1 on T0.Supp_AB_ID = T1.REC_ID Where T0.Supp_Cen_ID = '" + inBasicParam.openCenID.ToString() + "' "
            Return dbService.List(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, Query, ConnectOneWS.Tables.STOCK_SUPPLIER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetItemMappedSuppliers(SubItemID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim sitemid As String = "NULL"
            If (Not SubItemID Is Nothing) Then
                sitemid = SubItemID.ToString()
            End If
            Dim Query As String = "Select T1.C_NAME Supplier, t0.Supp_Company_Code As CompanyCode, Case When LEN(COALESCE(T1.C_TEL_NO_R_1,'')) > 0 THEN COALESCE(T1.C_TEL_NO_R_1+',','') ELSE '' END +COALESCE(T1.C_MOB_NO_1,'') Contact_No, T0.Supp_Contact_Person ContactPerson,T1.C_PAN_NO PAN, t0.REC_ID ID From Stock_Supplier_Info T0 Inner Join Supplier_Item_Mapping T0a On T0.REC_ID = T0a.SIM_Supp_ID And T0a.SIM_Sub_Item_ID = COALESCE(" + sitemid + ", T0a.SIM_Sub_Item_ID) Inner join address_book T1 On T0.Supp_AB_ID = T1.REC_ID"
            Return dbService.List(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, Query, ConnectOneWS.Tables.STOCK_SUPPLIER_INFO.ToString(), inBasicParam)
        End Function


        Public Shared Function GetItemSupplierMapping(inparam As Param_GetItemSupplierMapping, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Item_Supplier_Mapping"
            Dim params() As String = {"@ItemCategory", "@ItemID", "@SupplierID"}
            Dim values() As Object = {inparam.ItemCategory, inparam.ItemID, inparam.SupplierID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, SPName, ConnectOneWS.Tables.STOCK_SUPPLIER_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetSupplierRecord(SupplierID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Supp_Cen_ID, AB_CURR.REC_ID Supp_AB_ID, Supp_Company_Code, Supp_Reg_No, Supp_Contact_Person, SUPP.REC_ADD_ON, SUPP.REC_ADD_BY, SUPP.REC_EDIT_ON, SUPP.REC_EDIT_BY, SUPP.REC_ID, SUPP.Supp_Other_Details FROM Stock_Supplier_Info SUPP INNER JOIN ADDRESS_BOOK AB On Supp_AB_ID = AB.REC_ID LEFT JOIN address_book As AB_CURR On AB.C_ORG_REC_ID = AB_CURR.C_ORG_REC_ID And AB_CURR.C_COD_YEAR_ID = " + inBasicParam.openYearID.ToString() + " WHERE SUPP.REC_ID = " + SupplierID.ToString() + ""
            Return dbService.GetSingleRecord(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetSupplierUsage(SupplierID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select 'RR' as screen,RR_Date as Date from Requisition_Request_Item RRI INNER JOIN Requisition_Request_Info RR ON RR.REC_ID = RRI.RRI_RR_ID	Where RRI.RRI_supplier_ID = " + SupplierID.ToString() + " UNION ALL Select 'PO',PO_Date  from Purchase_Order_Info RRI Where PO_Supplier_ID = " + SupplierID.ToString()
            Return dbService.GetSingleRecord(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetSupplierBankAccUsageCount(inparam As Param_GetSupplierBankAccUsageCount, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM Stock_Supplier_Bank WHERE SB_Acc_No = '" + inparam.AccountNo + "' AND SB_BANK_ID='" + inparam.BankID + "' and REC_ID <> " + inparam.SupplierBankID
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetSupplier_Party_Duplication_Check(inparam As Param_GetSupplier_Party_Duplication_Check, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT count(supp.REC_ID) Cnt FROM Stock_Supplier_Info supp INNER JOIN address_book AS AB ON SUPP.Supp_AB_ID = AB.REC_ID LEFT OUTER JOIN address_book AB_ALL ON AB.C_ORG_REC_ID = AB_ALL.C_ORG_REC_ID where ab.C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = '" + inparam.Address_Book_RecID + "' ) and supp.rec_id <> " + inparam.SupplierID.ToString
            Dim DuplicatonCount As Int32 = dbService.GetScalar(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, Query, ConnectOneWS.Tables.STOCK_SUPPLIER_INFO.ToString(), inBasicParam)
            If DuplicatonCount > 0 Then Return True
            Return False
        End Function
        Public Shared Function InsertItemSupplierMapping(ByVal InParam As Param_InsertItemSupplierMapping, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Supplier_Item_Mapping"
            Dim params() As String = {"@SIM_Supp_ID", "@SIM_Sub_Item_ID", "@SIM_Remarks", "@UserID"}
            Dim values() As Object = {InParam.Supplier_ID, InParam.Sub_Item_ID, InParam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, -1, 255, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.SUPPLIER_ITEM_MAPPING, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertSupplier_Txn(inparam As Param_InsertSupplier_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim Supplier_ID As Int32 = InsertSupplier(inparam.AB_ID, inparam.Company_Code, inparam.Reg_No, inparam.Contact_Person, inparam.Other_Details, inBasicParam)
            For Each AddedBank As Param_InsertsupplierBank In inparam.Added_Banks
                If Not AddedBank Is Nothing Then
                    AddedBank.Supplier_ID = Supplier_ID
                    InsertSupplierBank(AddedBank, inBasicParam)
                End If
            Next
            Return True
        End Function
        Private Shared Function InsertSupplier(AB_ID As String, Company_Code As String, Reg_No As String, Contact_Person As String, Other_Details As String, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Supplier"
            Dim params() As String = {"@Supp_Cen_ID", "@Supp_AB_ID", "@Supp_Company_Code", "@Supp_Reg_No", "@Supp_Contact_Person", "@UserID", "@Supp_Other_Details"}
            Dim values() As Object = {inBasicParam.openCenID, AB_ID, Company_Code, Reg_No, Contact_Person, inBasicParam.openUserID, Other_Details}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 50, 50, 255, 255, 8000}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function UpdateSupplier_Txn(inparam As Param_UpdateSupplier_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            UpdateSupplier(inparam.ID, inparam.AB_ID, inparam.Company_Code, inparam.Reg_No, inparam.Contact_Person, inparam.Other_Details, inBasicParam)
            For Each AddedBank As Param_InsertsupplierBank In inparam.Added_Banks
                If Not AddedBank Is Nothing Then
                    InsertSupplierBank(AddedBank, inBasicParam)
                End If
            Next
            For Each UpdatedBank As Param_UpdatesupplierBank In inparam.Updated_Banks
                If Not UpdatedBank Is Nothing Then
                    UpdateSupplierBank(UpdatedBank, inBasicParam)
                End If
            Next
            For Each DeletedBankID As Int32 In inparam.Deleted_Banks_IDs
                If Not DeletedBankID = Nothing Then
                    DeleteSupplierBank(DeletedBankID, inBasicParam)
                End If
            Next
            Return True
        End Function
        Public Shared Function InsertSupplierBank(ByVal InParam As Param_InsertsupplierBank, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Supplier_Bank"
            Dim params() As String = {"@SB_Supp_ID", "@SB_BANK_ID", "@SB_Branch_Name", "@SB_Acc_No", "@SB_IFSC_Code", "@UserID"}
            Dim values() As Object = {InParam.Supplier_ID, InParam.BANK_ID, InParam.Branch_Name, InParam.Acc_No, InParam.IFSC_Code, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 255, 255, 11, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_SUPPLIER_BANK, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdateItemSupplierMapping(inparam As Param_UpdateItemSupplierMapping, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim remarks As String = "NULL"
            If (Not inparam.Remarks Is Nothing) Then
                remarks = inparam.Remarks
            End If
            Dim Query As String = "UPDATE [dbo].[Supplier_Item_Mapping] SET [SIM_Supp_ID] = " + inparam.Supplier_ID.ToString() + ",[SIM_Sub_Item_ID] = " + inparam.Sub_Item_ID.ToString() + ",[SIM_Remarks] = " + remarks + " WHERE rec_id = " + inparam.ID.ToString() + ""
            dbService.Update(ConnectOneWS.Tables.SUPPLIER_ITEM_MAPPING, Query, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateSupplier(ID As Int32, AB_ID As String, Company_Code As String, Reg_No As String, Contact_Person As String, Other_Details As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Stock_Supplier"
            Dim params() As String = {"@Supp_ID ", "@Supp_AB_ID", "@Supp_Company_Code", "@Supp_Reg_No", "@Supp_Contact_Person", "@UserID", "@Supp_Other_Details"}
            Dim values() As Object = {ID, AB_ID, Company_Code, Reg_No, Contact_Person, inBasicParam.openUserID, Other_Details}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 50, 50, 255, 255, 8000}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateSupplierBank(ByVal InParam As Param_UpdatesupplierBank, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_Supplier_Bank"
            Dim params() As String = {"@SB_ID", "@SB_BANK_ID", "@SB_Branch_Name", "@SB_Acc_No", "@SB_IFSC_Code", "@UserID"}
            Dim values() As Object = {InParam.ID, InParam.BANK_ID, InParam.Branch_Name, InParam.Acc_No, InParam.IFSC_Code, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 255, 255, 11, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_SUPPLIER_BANK, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function DeleteSupplier_Txn(ByVal SupplierID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.SUPPLIER_ITEM_MAPPING, "SIM_Supp_ID = " + SupplierID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_SUPPLIER_BANK, "SB_SUPP_ID = " + SupplierID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, SupplierID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteSupplierBank(ByVal SupplierBankID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_SUPPLIER_BANK, SupplierBankID, inBasicParam)
            Return True
        End Function
    End Class

End Namespace