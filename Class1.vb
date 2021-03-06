Public Class Class1
    Private Shared FileNm() As String = Nothing
    Private Shared FolderNm As String = Nothing
    Public Shared Function BrowseFile() As String()
        Dim OFD As New OpenFileDialog With {
        .CheckFileExists = True,
        .Filter = ("Image File PNG *.png|*.png|MS Office Word DOCX|*.docx|PDF File PDF|*.pdf" &
        "|Music Files MP3|*.mp3|Text File TXT|*.txt"),
        .Multiselect = False,
        .RestoreDirectory = True,
        .SupportMultiDottedExtensions = False,
        .ValidateNames = True
        }
        Try
            If OFD.ShowDialog = DialogResult.OK Then
                FileNm = {OFD.FileName, OFD.SafeFileName, FileLen(OFD.FileName), FileLen(OFD.FileName) / 1024}
            Else
                Return Nothing
                Exit Function
            End If
        Catch ex As IO.IOException
            MsgBox(ex.Message)
        End Try
        Return FileNm
    End Function
    Public Shared Function BrowseFolder() As String
        Dim FBD As New FolderBrowserDialog With {
        .RootFolder = Environment.SpecialFolder.Desktop,
        .ShowNewFolderButton = True
        }
        If FBD.ShowDialog = DialogResult.OK Then
            FolderNm = FBD.SelectedPath
        Else
            Return Nothing
            Exit Function
        End If
        Return FolderNm
    End Function
End Class
