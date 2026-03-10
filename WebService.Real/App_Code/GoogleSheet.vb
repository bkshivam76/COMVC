Imports System.Data
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Services
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports System.IO
Imports Newtonsoft.Json.Linq
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Threading.Tasks
Imports System.Net

Namespace Real
    <Serializable>
    Public Class GoogleSheet
        'Private Shared keyFilePath As String = HostingEnvironment.MapPath("~/Scripts/quantum-engine-403705-0902b7bfe2ee.json")
        'Private Shared clientSecretFilePath As String = HostingEnvironment.MapPath("~/Scripts/client_secret_708591934964-tvahpmf4h378rbubolarko8rajhmocsr.apps.googleusercontent.com.json")


        Public Async Function CreateAndWriteToSheetAsync(ByVal dt As DataTable, ByVal keyFilePath As String, ByVal serviceAccountEmail As String) As Task(Of String)
            Dim Scopes As String() = {Google.Apis.Sheets.v4.SheetsService.Scope.Spreadsheets, Google.Apis.Drive.v3.DriveService.Scope.Drive}
            Dim ApplicationName As String = "Google Sheets API .NET Quickstart"

            ' Authenticate using your client_secret.json file
            Dim credential As ServiceAccountCredential
            Using streamReader = New StreamReader(keyFilePath)
                Dim json As JObject = JObject.Parse(streamReader.ReadToEnd())
                Dim privateKey As String = json("private_key").ToString()

                Dim initializer = New ServiceAccountCredential.Initializer(serviceAccountEmail) With {
                    .Scopes = Scopes
                }
                credential = New ServiceAccountCredential(initializer.FromPrivateKey(privateKey))
            End Using

            ' Create Google Drive API service.
            Dim driveService = New DriveService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = credential,
                .ApplicationName = ApplicationName
            })

            ' Create a new Google Sheet.
            Dim fileMetadata = New Google.Apis.Drive.v3.Data.File() With {
                .Name = "New Google Sheet",
                .MimeType = "application/vnd.google-apps.spreadsheet"
            }
            Dim request = driveService.Files.Create(fileMetadata)
            request.Fields = "id"
            Dim file = request.Execute()

            ' Change the permissions of the new Google Sheet to give anyone with the link read access.
            Dim permission = New Permission() With {
                .Type = "anyone",
                .Role = "reader"
            }
            driveService.Permissions.Create(permission, file.Id).Execute()

            ' Create Google Sheets API service.
            Dim sheetsService = New SheetsService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = credential,
                .ApplicationName = ApplicationName
            })

            ' Prepare the data from DataTable
            Dim objList As New List(Of IList(Of Object))()
            ' Define request parameters.
            Dim valueRange As New ValueRange()

            ' Add column headers
            Dim headerRow As New List(Of Object)()
            For Each column As DataColumn In dt.Columns
                headerRow.Add(column.ColumnName)
            Next
            objList.Add(headerRow)

            ' Add data rows
            For Each dr As DataRow In dt.Rows
                Dim objArr As New List(Of Object)()
                For Each column As DataColumn In dt.Columns
                    objArr.Add(dr(column))
                Next
                objList.Add(objArr)
            Next

            valueRange.Values = objList

            ' Write the data to the Google Sheet
            Dim update = sheetsService.Spreadsheets.Values.Update(valueRange, file.Id, "A1")
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            Dim response = update.Execute()

            ' Freeze the top row
            Dim gridProperties = New GridProperties() With {
                .FrozenRowCount = 1
            }
            Dim sheetProperties = New SheetProperties() With {
                .SheetId = 0,
                .GridProperties = gridProperties
            }
            Dim req = New Request() With {
                .UpdateSheetProperties = New UpdateSheetPropertiesRequest() With {
                    .Properties = sheetProperties,
                    .Fields = "gridProperties.frozenRowCount"
                }
            }
            Dim batchUpdateRequest = New BatchUpdateSpreadsheetRequest() With {
                .Requests = New List(Of Request)() From {
                    req
                }
            }
            sheetsService.Spreadsheets.BatchUpdate(batchUpdateRequest, file.Id).Execute()

            Dim columnCount As Integer = dt.Columns.Count
            Dim range As String = "A1:" & ChrW(65 + columnCount - 1) & "1" ' 65 is the ASCII value for 'A'
            ' Format the header row
            Await FormatHeaderRowAsync(file.Id, 0, range, keyFilePath, serviceAccountEmail)

            ' Return the URL of the Google Sheet
            Return "https://docs.google.com/spreadsheets/d/" + file.Id
        End Function

        Public Async Function FormatHeaderRowAsync(ByVal spreadsheetId As String, ByVal sheetId As Integer, ByVal range As String, ByVal keyFilePath As String, ByVal serviceAccountEmail As String) As Task
            ' Prepare the JSON payload for the request
            Dim payload As New Dictionary(Of String, Object) From {
        {"requests", New List(Of Dictionary(Of String, Object)) From {
            New Dictionary(Of String, Object) From {
                {"repeatCell", New Dictionary(Of String, Object) From {
                    {"range", New Dictionary(Of String, Object) From {
                        {"sheetId", sheetId},
                        {"startRowIndex", 0},
                        {"endRowIndex", 1},
                        {"startColumnIndex", 0},
                        {"endColumnIndex", range.Length}
                    }},
                    {"cell", New Dictionary(Of String, Object) From {
                        {"userEnteredFormat", New Dictionary(Of String, Object) From {
                            {"backgroundColor", New Dictionary(Of String, Double) From {
                                {"red", 1.0},
                                {"green", 1.0},
                                {"blue", 0.0}
                            }},
                            {"textFormat", New Dictionary(Of String, Boolean) From {
                                {"bold", True}
                            }}
                        }}
                    }},
                    {"fields", "userEnteredFormat(backgroundColor,textFormat)"}
                }}
            }
        }}
    }

            ' Convert the payload to JSON
            Dim jsonPayload As String = JsonConvert.SerializeObject(payload)

            ' Prepare the HTTP request
            Dim requestUri As String = "https://sheets.googleapis.com/v4/spreadsheets/" & spreadsheetId & ":batchUpdate"

            ' Create a WebRequest instance
            Dim webRequest As WebRequest = WebRequest.Create(requestUri)

            ' Set the method to POST
            webRequest.Method = "POST"

            ' Add the authorization header to the request
            Dim credential As ServiceAccountCredential
            Using streamReader = New StreamReader(keyFilePath)
                Dim json As JObject = JObject.Parse(streamReader.ReadToEnd())
                Dim privateKey As String = json("private_key").ToString()

                Dim initializer = New ServiceAccountCredential.Initializer(serviceAccountEmail) With {
            .Scopes = {SheetsService.Scope.Spreadsheets}
        }
                credential = New ServiceAccountCredential(initializer.FromPrivateKey(privateKey))
            End Using
            Dim accessToken As String = Await credential.GetAccessTokenForRequestAsync()
            webRequest.Headers.Add("Authorization", "Bearer " & accessToken)

            ' Prepare the data to be posted
            Dim postData As String = jsonPayload
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postData)

            ' Set the ContentType property of the WebRequest
            webRequest.ContentType = "application/json"

            ' Set the ContentLength property of the WebRequest
            webRequest.ContentLength = byteArray.Length

            ' Get the request stream
            Dim dataStream As Stream = webRequest.GetRequestStream()

            ' Write the data to the request stream
            dataStream.Write(byteArray, 0, byteArray.Length)

            ' Close the Stream object
            dataStream.Close()

            ' Get the response
            Dim response As Net.WebResponse = webRequest.GetResponse()

            ' Display the status
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)

            ' Clean up the streams
            dataStream.Close()
            response.Close()
        End Function
    End Class
End Namespace