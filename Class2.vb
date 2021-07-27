Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Drive.v3
Imports Google.Apis.Drive.v3.Data
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports System.IO
Imports System.Threading
Class Class2
    'If modifying these scopes, delete your previously saved credentials
    ' at ~/.credentials/drive-dotnet-quickstart.json
    Private Shared ReadOnly ClientId = "372026692886-6ksvf5v3m7g5kq5jk60ncs3nv0jt1alj.apps.googleusercontent.com"
    Private Shared ReadOnly ClientSecret = "u7ZwM7SJw6NeJ7nY4F8UPbKt"
    Private Shared ReadOnly Scopes() As String = {DriveService.Scope.Drive, DriveService.Scope.DriveFile}
    Private Shared ReadOnly ApplicationName As String = ("evry1falls")
    Private Shared ReadOnly UserName As String = ("User")
    Private Shared ReadOnly FileTypeDrive() As String = {"Folder", "File"}
    Private Shared Async Function GetService() As Task(Of DriveService)
        Dim credential As UserCredential =
                Await GoogleWebAuthorizationBroker.AuthorizeAsync(
                New ClientSecrets() With
                {.ClientId = ClientId, .ClientSecret = ClientSecret},
                Scopes, UserName, CancellationToken.None)
        'Create Drive API service.
        Dim service = New DriveService(New BaseClientService.Initializer() With
            {.HttpClientInitializer = credential, .ApplicationName = ApplicationName})
        Return service
        'End Using
    End Function
    Public Shared Async Function GetFolders(Optional ByVal Fields As String = "files(id,parents,name,mimeType)",
                                            Optional Q As String = "mimeType = 'application/vnd.google-apps.folder' and 'root' in parents and trashed=false") As Task(Of List(Of String()))
        Dim MyList As List(Of String()) = New List(Of String())
        Using Service As DriveService = Await GetService()
            Dim listRequest As FilesResource.ListRequest = Service.Files.List()
            With listRequest
                .PageSize = 100
                .Fields = Fields
                .Q = Q
                .Spaces = "Drive"
            End With
            Try
                Dim listFolder As FileList = listRequest.Execute
                If Not IsNothing(listFolder) AndAlso listFolder.Files.Count > 0 Then
                    For Each item As Data.File In listFolder.Files
                        MyList.Add({item.Name, item.Id, FileTypeDrive(0)})
                        listRequest.PageToken = String.Empty
                    Next
                Else
                    Debug.WriteLine("No Folders found.")
                    'MyList.Add({"No Folders found", 0.ToString, "File"})
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Using
        Return MyList
    End Function
    Public Shared Async Function ListFiles(Optional Fields As String = "files(id,parents,name,mimeType)",
                                           Optional Q As String = "mimeType != 'application/vnd.google-apps.folder' and 'root' in parents and trashed=false") _
                                           As Task(Of List(Of String()))
        Dim MyList As List(Of String()) = New List(Of String())
        Using Service = Await GetService()
            'Define parameters of request.
            Dim listRequest1 As FilesResource.ListRequest = Service.Files.List()
            With listRequest1
                .PageSize = 1000
                .Fields = Fields
                .Q = Q
                .Spaces = "Drive"
            End With
            'List files.
            Try
                Dim files As FileList = listRequest1.Execute()
                If Not IsNothing(files) AndAlso files.Files.Count > 0 Then
                    For Each file In files.Files
                        'Debug.WriteLine("{0} ({1})", file.Name, file.Id)
                        MyList.Add({file.Name, file.Id, FileTypeDrive(1)})
                        listRequest1.PageToken = String.Empty
                    Next
                Else
                    Debug.WriteLine("No files found.")
                    'MyList.Add({"No files found", 0.ToString, "File"})
                End If
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
            End Try
        End Using
        Return MyList
    End Function
    Public Shared Async Function UploadFileToFolder(FullPathLocFil As String, FileParents As String) As Task(Of Google.Apis.Upload.IUploadProgress)
        Dim TaskI As Google.Apis.Upload.IUploadProgress = Nothing
        Dim contType As String = GetContentType(FullPathLocFil)
        Dim ThisFile As Data.File = New Data.File() With
            {
            .Name = FullPathLocFil,
            .MimeType = contType,
            .Parents = {FileParents}
        }
        Dim ChunkSize As Integer = 0
        Using Service = Await GetService()
            Using UpStream As FileStream = New FileStream(FullPathLocFil, FileMode.Open, FileAccess.Read)
                Dim insert = Service.Files.Create(ThisFile, UpStream, contType)
                ChunkSize = Google.Apis.Upload.ResumableUpload.MinimumChunkSize * 2
                insert.ChunkSize = ChunkSize
                TaskI = Await insert.UploadAsync(Nothing)
            End Using
        End Using
        Return TaskI
    End Function
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