
Public Class Class1
    Private Shared FileNm() As String = Nothing
    Public Shared Function BrowseFile() As String()
        Dim OFD As New OpenFileDialog With {
        .CheckFileExists = True,
        .Filter = ("Image File PNG *.png|*.png|MS Office Word DOCX|*.docx|PDF File PDF|*.pdf|Music Files MP3|*.mp3"),
        .InitialDirectory = Application.StartupPath,
        .Multiselect = False,
        .RestoreDirectory = True,
        .SupportMultiDottedExtensions = False,
        .ValidateNames = True
        }
        Try
            If OFD.ShowDialog = DialogResult.OK Then
                FileNm = {OFD.FileName, OFD.SafeFileName}
            End If
        Catch ex As IO.IOException
            MsgBox(ex.Message)
        End Try
        Return FileNm
    End Function
End Class
