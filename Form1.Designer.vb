<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.UploadBtn = New System.Windows.Forms.Button()
        Me.FileLocTxt = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BrowseBtn = New System.Windows.Forms.Button()
        Me.DownloadBtn = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.STRFileNm = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.STRSize = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.STRFileID = New System.Windows.Forms.ToolStripLabel()
        Me.MyGDrive = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UploadBtn
        '
        Me.UploadBtn.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UploadBtn.Enabled = False
        Me.UploadBtn.Location = New System.Drawing.Point(12, 327)
        Me.UploadBtn.Name = "UploadBtn"
        Me.UploadBtn.Size = New System.Drawing.Size(75, 23)
        Me.UploadBtn.TabIndex = 0
        Me.UploadBtn.Text = "Upload"
        Me.UploadBtn.UseVisualStyleBackColor = True
        '
        'FileLocTxt
        '
        Me.FileLocTxt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FileLocTxt.Location = New System.Drawing.Point(12, 301)
        Me.FileLocTxt.Name = "FileLocTxt"
        Me.FileLocTxt.ReadOnly = True
        Me.FileLocTxt.Size = New System.Drawing.Size(439, 20)
        Me.FileLocTxt.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 285)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "File location"
        '
        'BrowseBtn
        '
        Me.BrowseBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BrowseBtn.Location = New System.Drawing.Point(460, 299)
        Me.BrowseBtn.Name = "BrowseBtn"
        Me.BrowseBtn.Size = New System.Drawing.Size(75, 23)
        Me.BrowseBtn.TabIndex = 3
        Me.BrowseBtn.Tag = ""
        Me.BrowseBtn.Text = "Browse"
        Me.BrowseBtn.UseVisualStyleBackColor = True
        '
        'DownloadBtn
        '
        Me.DownloadBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DownloadBtn.Enabled = False
        Me.DownloadBtn.Location = New System.Drawing.Point(376, 327)
        Me.DownloadBtn.Name = "DownloadBtn"
        Me.DownloadBtn.Size = New System.Drawing.Size(75, 23)
        Me.DownloadBtn.TabIndex = 4
        Me.DownloadBtn.Text = "Download"
        Me.DownloadBtn.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.STRFileNm, Me.ToolStripSeparator1, Me.STRSize, Me.ToolStripSeparator2, Me.STRFileID})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 363)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(550, 25)
        Me.ToolStrip1.TabIndex = 5
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'STRFileNm
        '
        Me.STRFileNm.Name = "STRFileNm"
        Me.STRFileNm.Size = New System.Drawing.Size(0, 22)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'STRSize
        '
        Me.STRSize.Name = "STRSize"
        Me.STRSize.Size = New System.Drawing.Size(0, 22)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'STRFileID
        '
        Me.STRFileID.Name = "STRFileID"
        Me.STRFileID.Size = New System.Drawing.Size(0, 22)
        '
        'MyGDrive
        '
        Me.MyGDrive.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MyGDrive.FullRowSelect = True
        Me.MyGDrive.HideSelection = False
        Me.MyGDrive.HotTracking = True
        Me.MyGDrive.ImageIndex = 0
        Me.MyGDrive.ImageList = Me.ImageList1
        Me.MyGDrive.Location = New System.Drawing.Point(12, 12)
        Me.MyGDrive.Name = "MyGDrive"
        Me.MyGDrive.SelectedImageIndex = 0
        Me.MyGDrive.ShowNodeToolTips = True
        Me.MyGDrive.Size = New System.Drawing.Size(439, 270)
        Me.MyGDrive.StateImageList = Me.ImageList1
        Me.MyGDrive.TabIndex = 6
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Group_close.png")
        Me.ImageList1.Images.SetKeyName(1, "folder_stand.png")
        Me.ImageList1.Images.SetKeyName(2, "open_folder.png")
        Me.ImageList1.Images.SetKeyName(3, "blank_report.png")
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(550, 388)
        Me.ControlBox = False
        Me.Controls.Add(Me.MyGDrive)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.DownloadBtn)
        Me.Controls.Add(Me.BrowseBtn)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.FileLocTxt)
        Me.Controls.Add(Me.UploadBtn)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Google Drive Api V3 Upload & Download Files"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents UploadBtn As Button
    Friend WithEvents FileLocTxt As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents BrowseBtn As Button
    Friend WithEvents DownloadBtn As Button
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents STRFileNm As ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents STRSize As ToolStripLabel
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents STRFileID As ToolStripLabel
    Friend WithEvents MyGDrive As TreeView
    Friend WithEvents ImageList1 As ImageList
End Class
