Imports DevExpress.XtraPrinting
Imports DevExpress.XtraReports.UI
Imports System.Reflection
Imports System.Reflection.Emit

Public Class ReportHelper

#Region "Start --> List To Dataset convertor"
    Public Function GetBuildingDataSetHacked(ByVal _
        list As List(Of ReportDataObjects.BuildingExpenseData)) As DataSet
        Dim _resultDataSet As New DataSet()
        Dim _resultDataTable As New DataTable("results")
        Dim _resultDataRow As DataRow = Nothing
        Dim _itemProperties() As PropertyInfo = _
             list.Item(0).GetType().GetProperties()
        Dim _callPropertyValue(_itemProperties.Length()) _
             As _getPropertyDelegate(Of ReportDataObjects.BuildingExpenseData)

        '
        ' Each item property becomes a column in the table 
        ' Build an array of Property Getters, one for each Property 
        ' in the item class. Can pass anything as [item] it is just a 
        ' place holder parameter, later we will invoke it with the
        ' correct item. This code assumes the runtime does not change
        ' the ORDER in which the proprties are returned.
        '
        _itemProperties = list.Item(0).GetType().GetProperties()
        Dim i As Integer = 0

        For Each p As PropertyInfo In _itemProperties
            _callPropertyValue(i) = _
              CreateGetPropertyValueDelegate(CType(list.Item(0), ReportDataObjects.BuildingExpenseData), p.Name)
            _resultDataTable.Columns.Add(p.Name, _
                      p.GetGetMethod.ReturnType())
            i += 1
        Next
        '    
        ' Data    
        '
        For Each item As ReportDataObjects.BuildingExpenseData In list
            '
            ' Get the data from this item into a DataRow
            ' then add the DataRow to the DataTable.
            ' Eeach items property becomes a colunm.
            '
            _itemProperties = item.GetType().GetProperties()
            _resultDataRow = _resultDataTable.NewRow()
            i = 0
            For Each p As PropertyInfo In _itemProperties
                _resultDataRow(p.Name) = _
                   _callPropertyValue(i).Invoke(item)
                i += 1
            Next
            _resultDataTable.Rows.Add(_resultDataRow)
        Next
        '    
        ' Add the DataTable to the DataSet, We are DONE!
        '
        _resultDataSet.Tables.Add(_resultDataTable)
        Return _resultDataSet
    End Function


    Public Function GetDataSetHacked(ByVal _
        list As List(Of ReportDataObjects.TransactionData)) As DataSet
        Dim _resultDataSet As New DataSet()
        Dim _resultDataTable As New DataTable("results")
        Dim _resultDataRow As DataRow = Nothing
        Dim _itemProperties() As PropertyInfo = _
             list.Item(0).GetType().GetProperties()
        Dim _callPropertyValue(_itemProperties.Length()) _
             As _getPropertyDelegate(Of ReportDataObjects.TransactionData)

        '
        ' Each item property becomes a column in the table 
        ' Build an array of Property Getters, one for each Property 
        ' in the item class. Can pass anything as [item] it is just a 
        ' place holder parameter, later we will invoke it with the
        ' correct item. This code assumes the runtime does not change
        ' the ORDER in which the proprties are returned.
        '
        _itemProperties = list.Item(0).GetType().GetProperties()
        Dim i As Integer = 0

        For Each p As PropertyInfo In _itemProperties
            _callPropertyValue(i) = _
              CreateGetPropertyValueDelegate(CType(list.Item(0), ReportDataObjects.TransactionData), p.Name)
            _resultDataTable.Columns.Add(p.Name, _
                      p.GetGetMethod.ReturnType())
            i += 1
        Next
        '    
        ' Data    
        '
        For Each item As ReportDataObjects.TransactionData In list
            '
            ' Get the data from this item into a DataRow
            ' then add the DataRow to the DataTable.
            ' Eeach items property becomes a colunm.
            '
            _itemProperties = item.GetType().GetProperties()
            _resultDataRow = _resultDataTable.NewRow()
            i = 0
            For Each p As PropertyInfo In _itemProperties
                _resultDataRow(p.Name) = _
                   _callPropertyValue(i).Invoke(item)
                i += 1
            Next
            _resultDataTable.Rows.Add(_resultDataRow)
        Next
        '    
        ' Add the DataTable to the DataSet, We are DONE!
        '
        _resultDataSet.Tables.Add(_resultDataTable)
        Return _resultDataSet
    End Function

    Private Delegate Function _
           _getPropertyDelegate(Of T)(ByVal _
           item As T) As Object
   
    Private Function CreateGetPropertyValueDelegate(Of T)(ByVal _
            item As T, ByVal itemName As Object) As  _
            _getPropertyDelegate(Of T)

        Dim _arg() As Type = {GetType(T)}
        Dim _propertyInfo As PropertyInfo = _
            item.GetType().GetProperty(itemName, _
            BindingFlags.Public Or BindingFlags.Instance)
        Dim _getPropertyValue As DynamicMethod = Nothing
        Dim _ilGenerator As ILGenerator = Nothing

        '
        ' Create the funciton
        '
        _getPropertyValue = New DynamicMethod("_getPropertyValue", _
                            GetType(Object), _arg, _
                            GetType(Integer).Module)
        '
        ' Write the body of the function.
        '
        _ilGenerator = _getPropertyValue.GetILGenerator()
        _ilGenerator.Emit(OpCodes.Ldarg_0)
        _ilGenerator.Emit(OpCodes.Callvirt, _propertyInfo.GetGetMethod())
        ' Box value types.
        If Not _propertyInfo.PropertyType.IsClass Then
            _ilGenerator.Emit(OpCodes.Box, _
                         _propertyInfo.GetGetMethod.ReturnType())
        End If
        _ilGenerator.Emit(OpCodes.Ret)
        '
        ' Return the Delegate
        '
        Return CType(_getPropertyValue.CreateDelegate(GetType(_getPropertyDelegate(Of T))),  _
                 _getPropertyDelegate(Of T))
    End Function

    Public Function GetDataSetHacked_General(Of T)(ByVal _
       list As List(Of T)) As DataSet
        Dim _resultDataSet As New DataSet()
        Dim _resultDataTable As New DataTable("results")
        Dim _resultDataRow As DataRow = Nothing
        Dim _itemProperties() As PropertyInfo = _
             list.Item(0).GetType().GetProperties()
        Dim _callPropertyValue(_itemProperties.Length()) _
             As _getPropertyDelegate(Of T)

        '
        ' Each item property becomes a column in the table 
        ' Build an array of Property Getters, one for each Property 
        ' in the item class. Can pass anything as [item] it is just a 
        ' place holder parameter, later we will invoke it with the
        ' correct item. This code assumes the runtime does not change
        ' the ORDER in which the proprties are returned.
        '
        _itemProperties = list.Item(0).GetType().GetProperties()
        Dim i As Integer = 0

        For Each p As PropertyInfo In _itemProperties
            _callPropertyValue(i) = _
              CreateGetPropertyValueDelegate(CType(list.Item(0), T), p.Name)
            _resultDataTable.Columns.Add(p.Name, _
                      p.GetGetMethod.ReturnType())
            i += 1
        Next
        '    
        ' Data    
        '
        For Each item As T In list
            '
            ' Get the data from this item into a DataRow
            ' then add the DataRow to the DataTable.
            ' Eeach items property becomes a colunm.
            '
            _itemProperties = item.GetType().GetProperties()
            _resultDataRow = _resultDataTable.NewRow()
            i = 0
            For Each p As PropertyInfo In _itemProperties
                _resultDataRow(p.Name) = _
                   _callPropertyValue(i).Invoke(item)
                i += 1
            Next
            _resultDataTable.Rows.Add(_resultDataRow)
        Next
        '    
        ' Add the DataTable to the DataSet, We are DONE!
        '
        _resultDataSet.Tables.Add(_resultDataTable)
        Return _resultDataSet
    End Function


    Public Function GetDataTableHacked_General(Of T)(ByVal _
       list As List(Of T)) As DataTable
        Dim _resultDataTable As New DataTable("results")
        Dim _resultDataRow As DataRow = Nothing
        Dim _itemProperties() As PropertyInfo = _
             list.Item(0).GetType().GetProperties()
        Dim _callPropertyValue(_itemProperties.Length()) _
             As _getPropertyDelegate(Of T)

        '
        ' Each item property becomes a column in the table 
        ' Build an array of Property Getters, one for each Property 
        ' in the item class. Can pass anything as [item] it is just a 
        ' place holder parameter, later we will invoke it with the
        ' correct item. This code assumes the runtime does not change
        ' the ORDER in which the proprties are returned.
        '
        _itemProperties = list.Item(0).GetType().GetProperties()
        Dim i As Integer = 0

        For Each p As PropertyInfo In _itemProperties
            _callPropertyValue(i) = _
              CreateGetPropertyValueDelegate(CType(list.Item(0), T), p.Name)
            _resultDataTable.Columns.Add(p.Name, _
                      p.GetGetMethod.ReturnType())
            i += 1
        Next
        '    
        ' Data    
        '
        For Each item As T In list
            '
            ' Get the data from this item into a DataRow
            ' then add the DataRow to the DataTable.
            ' Eeach items property becomes a colunm.
            '
            _itemProperties = item.GetType().GetProperties()
            _resultDataRow = _resultDataTable.NewRow()
            i = 0
            For Each p As PropertyInfo In _itemProperties
                _resultDataRow(p.Name) = _
                   _callPropertyValue(i).Invoke(item)
                i += 1
            Next
            _resultDataTable.Rows.Add(_resultDataRow)
        Next
        '    
        ' Add the DataTable to the DataSet, We are DONE!
        '
        Return _resultDataTable
    End Function

#End Region

End Class
