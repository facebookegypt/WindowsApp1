Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ThisTree As TreeNode = New TreeNode("GDriveFolders", 0, 0) With {
            .ToolTipText = "Hello Google Drive",
            .Text = "My Google Drive"
        }
        For Each I As KeyValuePair(Of String, String) In Class2.GetFolders
            ThisTree.Nodes.Add(I.Value, I.Key, 1, 2).ToolTipText =
                ("Folder ID : " & I.Value)
        Next
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
    Private Sub FileLocTxt_TextChanged(sender As Object, e As EventArgs) Handles FileLocTxt.TextChanged
        UploadBtn.Enabled = True
    End Sub
    Private Sub FileLocTxt_GotFocus(sender As Object, e As EventArgs) Handles FileLocTxt.GotFocus
        If Not String.IsNullOrEmpty(FileLocTxt.Text) Then
            UploadBtn.Enabled = True
            DownloadBtn.Enabled = False
        End If
    End Sub
    Private Sub UploadBtn_Click(sender As Object, e As EventArgs) Handles UploadBtn.Click
        Try
            Class2.UploadFileToFolder(FileLocTxt.Text)
        Catch ex As Exception

        End Try
    End Sub
End Class
