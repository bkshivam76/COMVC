Imports System.Windows.Forms.Form
Imports System.ComponentModel
Imports DevExpress.XtraGrid

Public Class FindComponent

    Private _Form2Process As Form
    Private _ObjectsFound As List(Of ObjectsFound)

    Public Class ObjectsFound
        Public Property ObjectName() As String
        Public Property ObjectType() As String
        Public ReadOnly Property NameIsEmpty() As String
            Get
                If _ObjectName.Length = 0 Then
                    Return "Name is empty!!!"
                Else
                    Return String.Empty
                End If
            End Get
        End Property
        Public Sub New(n As String, t As String)
            ObjectName = n
            ObjectType = t
        End Sub
    End Class

    Public Sub New(form2Process As Form)
        _Form2Process = form2Process
    End Sub

    Public Function FindInAllObjects(propertyName As String) As Object
        _ObjectsFound = New List(Of ObjectsFound)
        Return FindObject(_Form2Process, _ObjectsFound, propertyName)
    End Function

    Private Function FindObject(o As Object, objectsFound As List(Of ObjectsFound), propertyName As String) As Object
        Dim objectControls = TryCast(ExtractObjectProperty(o, "Controls"), ControlCollection)
        Dim list As ArrayList = FindInternal(propertyName, True, objectControls, New ArrayList())
        'Dim array As Control() = New Control(list.Count - 1) {}
        'list.CopyTo(array, 0)
        If (list.Count > 0) Then
            Return list(0)
        End If
        Return Nothing
    End Function

    Private Function ExtractObjectProperty(oObject As Object, propertyName As String) As Object
        Try
            Return oObject.GetType().GetProperty(propertyName).GetValue(oObject, Nothing)
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Shared Function FindInternal(key As String, searchAllChildren As Boolean, controlsToLookIn As Control.ControlCollection, foundControls As ArrayList) As ArrayList
        If (controlsToLookIn Is Nothing) OrElse (foundControls Is Nothing) Then
            Return Nothing
        End If
        Try
            For i As Integer = 0 To controlsToLookIn.Count - 1
                If (controlsToLookIn(i) IsNot Nothing) AndAlso String.Compare(controlsToLookIn(i).Name, key, True) = 0 Then
                    foundControls.Add(controlsToLookIn(i))
                End If
                'To Check if Grid view is in collection, find gridControl and assign its defaultview as gridview
                If (controlsToLookIn(i).GetType().Name = "GridControl") Then
                    If (DirectCast(controlsToLookIn(i), DevExpress.XtraGrid.GridControl).DefaultView.Name = key) Then
                        foundControls.Add(DirectCast(controlsToLookIn(i), DevExpress.XtraGrid.GridControl).DefaultView)
                    End If
                End If
            Next
            If Not searchAllChildren Then
                Return foundControls
            End If
            For j As Integer = 0 To controlsToLookIn.Count - 1
                If ((controlsToLookIn(j) IsNot Nothing) AndAlso (controlsToLookIn(j).Controls IsNot Nothing)) AndAlso (controlsToLookIn(j).Controls.Count > 0) Then
                    foundControls = FindInternal(key, searchAllChildren, controlsToLookIn(j).Controls, foundControls)
                End If
            Next
        Catch exception As Exception
            
        End Try
        Return foundControls
    End Function

End Class