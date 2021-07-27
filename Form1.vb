Public Class Form1
    Private CountItms(1) As Integer
    Private TempFolderDownload As String = ("C:\")
    Private IsFile As Boolean = True
    Private ThisFolderID As String = String.Empty
    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ThisTree As TreeNode = New TreeNode("My Google Drive", 0, 0) With {
            .ToolTipText = "My Google Drive",
            .Name = "GDriveFolders"
        }
        Dim MyFolders As List(Of String()) = New List(Of String())
        Try
            'List All Folders in main Drive
            MyFolders = Await Class2.GetFolders
            For Each I As String() In MyFolders
                ThisTree.Nodes.Add(I(1), I(0), 1, 2).ToolTipText =
                    I(2) & " ID : " & I(1)
            Next
            CountItms(0) = ThisTree.Nodes.Count
            'List All Files in main Drive Folder
            Dim MyFiles As List(Of String()) = New List(Of String())
            MyFiles = Await Class2.ListFiles
            For Each I1 As String() In MyFiles
                ThisTree.Nodes.Add(I1(1), I1(0), 3, 2).ToolTipText =
                   I1(2) & " ID : " & I1(1)
                CountItms(1) += 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        ThisTree.ToolTipText += (" : " & CountItms(0) & " Folders and " & CountItms(1) & " Files.")
        'Populate TreeView with TreeNodes
        With MyGDrive
            .BeginUpdate()
            .Nodes.Clear()
            .Nodes.Add(ThisTree)
            .ImageList = ImageList1
            .ShowNodeToolTips = True
            .EndUpdate()
        End With
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
    Private Sub BrowseBtn_Click(sender As Object, e As EventArgs) Handles BrowseBtn.Click
        If IsFile Then  'Pick a Folder
            TempFolderDownload = Class1.BrowseFolder
            FileLocTxt.Text = TempFolderDownload
        Else
            Dim FilNm As String() = Class1.BrowseFile()
            If Not IsNothing(FilNm) Then
                FileLocTxt.Text = FilNm(0)
                STRFileNm.Text = FilNm(1)
                STRSize.Text = FormatNumber(FilNm(2).ToString, 2,,, TriState.True) & " B" &
                    " | " & FormatNumber(FilNm(3).ToString, 2,,, TriState.True) & " KB"
            End If
        End If
    End Sub
    Private Sub DownloadBtn_Click(sender As Object, e As EventArgs) Handles DownloadBtn.Click
        'Download a File from ComboBox
        If String.IsNullOrEmpty(MyGDrive.Text) Or
            String.IsNullOrEmpty(STRFileID.Text) Then
            MsgBox("Choose a File to Download.")
            Exit Sub
        End If
    End Sub
    Private Async Sub UploadBtn_Click(sender As Object, e As EventArgs) Handles UploadBtn.Click
        Try
            Await Class2.UploadFileToFolder(FileLocTxt.Text, ThisFolderID)
            MsgBox("Completed.")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub MyGDrive_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles MyGDrive.AfterSelect
        FileLocTxt.Text = String.Empty
        'If Google Drive Folder, then Upload
        If Not IsNothing(e.Node) And
             e.Node.TreeView.SelectedNode.ToolTipText.StartsWith("File") Then
            IsFile = True
            UploadBtn.Enabled = False
            DownloadBtn.Enabled = True
            'If Google Drive File, then Download
        ElseIf e.Node.TreeView.SelectedNode.ToolTipText.StartsWith("Folder") Then
            IsFile = False
            ThisFolderID = e.Node.Name
            UploadBtn.Enabled = True
            DownloadBtn.Enabled = False
        End If
    End Sub
    Private Sub MyGDrive_BeforeSelect(sender As Object, e As TreeViewCancelEventArgs) Handles MyGDrive.BeforeSelect
        If Not IsNothing(e.Node) Then
            Dim ValidTxt As String = e.Node.ToolTipText
            If Not ValidTxt.StartsWith("Folder") AndAlso Not ValidTxt.StartsWith("File") Then
                BrowseBtn.Enabled = False
                Exit Sub
            End If
            BrowseBtn.Enabled = True
            If ValidTxt.StartsWith("File") Then 'File with no Extra Nodes
                IsFile = True
                Exit Sub
            End If
            Dim FolderID As String =
                "mimeType != 'application/vnd.google-apps.folder' and " + "trashed=false and '" + e.Node.Name + "' in parents"
            Dim FolderID1 As String =
                "mimeType = 'application/vnd.google-apps.folder' and trashed=false and '" + e.Node.Name + "' in parents"
            Dim ExtraFiles As List(Of String()) = Class2.ListFiles(, FolderID).Result
            Dim ExtraFolders As List(Of String()) = Class2.GetFolders(, FolderID1).Result
            e.Node.Nodes.Clear()
            For Each I1 As String() In ExtraFolders
                e.Node.Nodes.Add(I1(1), I1(0), 1, 2).ToolTipText =
                    I1(2) & " ID : " & I1(1)
            Next
            Application.DoEvents()
            For Each I As String() In ExtraFiles
                e.Node.Nodes.Add(I(1), I(0), 3, 2).ToolTipText =
                    I(2) & " ID : " & I(1)
            Next
        End If
    End Sub
End Class
