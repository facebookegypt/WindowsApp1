Public Class Form1
    Private CountItms(1) As Integer 
    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ThisTree As TreeNode = New TreeNode("My Google Drive", 0, 0) With {
            .ToolTipText = "My Google Drive",
            .Name = "GDriveFolders"
        }

        Dim MyFolders As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Try
            MyFolders = Await Class2.GetFolders
            For Each I As KeyValuePair(Of String, String) In MyFolders
                ThisTree.Nodes.Add(I.Value, I.Key, 1, 2).ToolTipText =
                    ("Folder ID : " & I.Value)
            Next
            CountItms(0) = ThisTree.Nodes.Count
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        With MyGDrive
            .BeginUpdate()
            .Nodes.Clear()
            .ImageList = ImageList1
            .ShowNodeToolTips = True
            .Nodes.Add(ThisTree)
            .EndUpdate()
        End With
    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then Close()
    End Sub
    Private Sub BrowseBtn_Click(sender As Object, e As EventArgs) Handles BrowseBtn.Click
        Dim FilNm As String() = Class1.BrowseFile()
        If Not IsNothing(FilNm) Then
            FileLocTxt.Text = FilNm(0)
            STRFileNm.Text = FilNm(1)
            STRSize.Text = FormatNumber(FilNm(2).ToString, 2,,, TriState.True) & " B" &
                " | " & FormatNumber(FilNm(3).ToString, 2,,, TriState.True) & " KB"
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
    Private Sub UploadBtn_Click(sender As Object, e As EventArgs) Handles UploadBtn.Click
        Try
            Class2.UploadFileToFolder(FileLocTxt.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub MyGDrive_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles MyGDrive.AfterSelect
        Dim MsgT As String = Nothing

        'If Google Drive Folder, then Upload
        If Not IsNothing(e.Node.TreeView.SelectedNode) And
            Not String.IsNullOrEmpty(FileLocTxt.Text) Then
            MsgT = " Will be uploaded to " & e.Node.FullPath _
                & " ( " & e.Node.ToolTipText & " )"

            UploadBtn.Enabled = True
            DownloadBtn.Enabled = False
            'If Google Drive File, then Download
        ElseIf Not IsNothing(e.Node.TreeView.SelectedNode) Then
            MsgT = ("Choose a File to Upload.")
            UploadBtn.Enabled = False
            DownloadBtn.Enabled = True
        End If
        STRFileNm.Text = MsgT
    End Sub

    Private Sub MyGDrive_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles MyGDrive.BeforeExpand


    End Sub

    Private Sub MyGDrive_AfterExpand(sender As Object, e As TreeViewEventArgs) Handles MyGDrive.AfterExpand
        Dim MyFiles As Dictionary(Of String, String) = New Dictionary(Of String, String)
        MyFiles = Class2.ListFiles
        For Each I As KeyValuePair(Of String, String) In MyFiles
            e.Node.Nodes.Add(I.Value, I.Key, 3, 2).ToolTipText =
              ("File ID : " & I.Value)
            CountItms(1) += 1
        Next
        e.Node.ToolTipText += (" : " & CountItms(0) & " Folders and " & CountItms(1) & " Files.")
    End Sub
End Class
