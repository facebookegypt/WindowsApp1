Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Threading
Imports System.Threading.Tasks
Class Class2
    'If modifying these scopes, delete your previously saved credentials
    ' at ~/.credentials/drive-dotnet-quickstart.json
    Private Shared ReadOnly Scopes() As String = {DriveService.Scope.Drive}
    Private Shared ReadOnly ApplicationName As String = ("My First Project")
    Dim I As List(Of IList) = New List(Of IList)
    Private Shared MyList As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private Shared Async Function GetService() As Task(Of DriveService)
        Dim credential As UserCredential = Nothing
        Using Stream As FileStream = New FileStream("Credentials.json", FileMode.Open, FileAccess.Read)
            'The file token.json stores the user's access and refresh tokens, and is created
            'automatically when the authorization flow completes for the first time.
            Dim CredPath As String = Path.Combine(Application.StartupPath, "token.json")

            credential =
                Await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.FromStream(Stream).Secrets,
                                                                  Scopes, "user", CancellationToken.None,
                                                                  New FileDataStore(CredPath, True))
            'Debug.WriteLine("Credential file saved to " + CredPath)
            'Create Drive API service.
            Dim service = New DriveService(New BaseClientService.Initializer() With {
                                           .HttpClientInitializer = credential,
                                           .ApplicationName = ApplicationName
                                           }
                                           )
            Return service
        End Using
    End Function
    Public Shared Function GetFolders(Optional ByVal QueryI As String = "application/vnd.google-apps.folder") _
        As Dictionary(Of String, String)
        Using Service As DriveService = GetService().Result

            Dim listRequest As FilesResource.ListRequest = Service.Files.List()
            With listRequest
                .Fields = "*"
                .Q = "'root' in parents"
                .Spaces = "Drive"
            End With
            Dim listFolder As FileList = listRequest.Execute
            For Each item As Data.File In listFolder.Files
                If item.MimeType = QueryI Then
                    MyList.Add(item.Name, item.Parents(0))
                End If
            Next
        End Using
        Return MyList
    End Function
    Public Shared Function ListFiles() As Dictionary(Of String, String)
        Using Service = GetService().Result
            'Define parameters of request.
            Dim listRequest As FilesResource.ListRequest = Service.Files.List()

            listRequest.PageSize = 20
            listRequest.Fields = "nextPageToken, files(id, name)"
            'List files.
            Dim files As List(Of Google.Apis.Drive.v3.Data.File) = listRequest.Execute().Files
            'Debug.WriteLine("Files:")
            If Not IsNothing(files) AndAlso files.Count > 0 Then
                For Each file In files
                    'Debug.WriteLine("{0} ({1})", file.Name, file.Id)
                    '  MyList.Add(file.Name, file.Id)
                Next
            Else
                'Debug.WriteLine("No files found.")
                '  MyList.Add("Empty Drive", 0.ToString)
            End If
        End Using
        'Return MyList
    End Function
    Public Shared Function UploadFileToFolder(FullPathLocFil As String) As _
        Task(Of Google.Apis.Upload.IUploadProgress)
        Dim Service = GetService.Result
        Dim TaskI As Task(Of Google.Apis.Upload.IUploadProgress) = Nothing
        Dim contType As String = GetContentType(FullPathLocFil)
        Dim Scopes() As String =
            {DriveService.Scope.DriveFile, DriveService.Scope.Drive}
        Dim ThisFile As Data.File = New Data.File() With
            {
            .Name = FullPathLocFil,
            .MimeType = contType
        }
        Using UpStream As FileStream =
            New FileStream(FullPathLocFil, FileMode.Open, FileAccess.Read)
            Dim insert = Service.Files.Create(ThisFile, UpStream, contType)
            insert.ChunkSize = Google.Apis.Upload.ResumableUpload.MinimumChunkSize * 2
            TaskI = insert.UploadAsync().Result
        End Using
        Return TaskI
        Upload_ProgressChanged(TaskI)
    End Function
    Private Shared Sub Upload_ProgressChanged(progress As Google.Apis.Upload.IUploadProgress)
        Debug.WriteLine(progress.Status + " " + progress.BytesSent)
    End Sub
    Private Shared Function GetContentType(File As String) As String
        Dim mimeType As String = "application/unknown"
        Dim ext As String = Path.GetExtension(File).ToLower()
        Dim regKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext)
        If Not IsNothing(regKey) AndAlso Not IsNothing(regKey.GetValue("Content Type")) Then
            mimeType = regKey.GetValue("Content Type").ToString()
        End If
        Return mimeType
    End Function
End Class