'
' Created by SharpDevelop.
' User: Walkman
' Date: 2015/11/28
' Time: 04:26 PM
'
Partial Class KeyInsert
    Inherits System.Windows.Forms.Form
    
    ''' <summary>
    ''' Designer variable used to keep track of non-visual components.
    ''' </summary>
    Private components As System.ComponentModel.IContainer
    
    ''' <summary>
    ''' Disposes resources used by the form.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    
    ''' <summary>
    ''' This method is required for Windows Forms designer support.
    ''' Do not change the method contents inside the source code editor. The Forms designer might
    ''' not be able to load this method if it was changed manually.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.lstKeyStrokes = New System.Windows.Forms.ListView()
        Me.colheadKeyStroke = New System.Windows.Forms.ColumnHeader()
        Me.colheadTime = New System.Windows.Forms.ColumnHeader()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.bwKeyInserter = New System.ComponentModel.BackgroundWorker()
        Me.lnkInfo = New System.Windows.Forms.LinkLabel()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.SuspendLayout
        '
        'lstKeyStrokes
        '
        Me.lstKeyStrokes.AllowDrop = true
        Me.lstKeyStrokes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
                        Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstKeyStrokes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colheadKeyStroke, Me.colheadTime})
        Me.lstKeyStrokes.FullRowSelect = true
        Me.lstKeyStrokes.GridLines = true
        Me.lstKeyStrokes.HideSelection = false
        Me.lstKeyStrokes.LabelEdit = true
        Me.lstKeyStrokes.Location = New System.Drawing.Point(12, 12)
        Me.lstKeyStrokes.MultiSelect = false
        Me.lstKeyStrokes.Name = "lstKeyStrokes"
        Me.lstKeyStrokes.Size = New System.Drawing.Size(608, 204)
        Me.lstKeyStrokes.TabIndex = 22
        Me.lstKeyStrokes.UseCompatibleStateImageBehavior = false
        Me.lstKeyStrokes.View = System.Windows.Forms.View.Details
        '
        'colheadKeyStroke
        '
        Me.colheadKeyStroke.Text = "Keystroke"
        Me.colheadKeyStroke.Width = 474
        '
        'colheadTime
        '
        Me.colheadTime.Text = "Wait Time (ms)"
        Me.colheadTime.Width = 110
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(626, 12)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(114, 23)
        Me.btnAdd.TabIndex = 23
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = true
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnRemove.Location = New System.Drawing.Point(626, 41)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(114, 23)
        Me.btnRemove.TabIndex = 24
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = true
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = true
        Me.lblStatus.Location = New System.Drawing.Point(626, 96)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(67, 13)
        Me.lblStatus.TabIndex = 37
        Me.lblStatus.Text = "Not Running"
        '
        'bwKeyInserter
        '
        Me.bwKeyInserter.WorkerReportsProgress = true
        Me.bwKeyInserter.WorkerSupportsCancellation = true
        '
        'lnkInfo
        '
        Me.lnkInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lnkInfo.AutoSize = true
        Me.lnkInfo.Location = New System.Drawing.Point(626, 206)
        Me.lnkInfo.Name = "lnkInfo"
        Me.lnkInfo.Size = New System.Drawing.Size(78, 13)
        Me.lnkInfo.TabIndex = 38
        Me.lnkInfo.TabStop = true
        Me.lnkInfo.Text = "Key strings info"
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnStart.Location = New System.Drawing.Point(626, 70)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(114, 23)
        Me.btnStart.TabIndex = 39
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = true
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblInfo.AutoSize = true
        Me.lblInfo.Location = New System.Drawing.Point(626, 109)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(82, 13)
        Me.lblInfo.TabIndex = 40
        Me.lblInfo.Text = "Hold Ctrl to stop"
        '
        'KeyInsert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(752, 228)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.lnkInfo)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lstKeyStrokes)
        Me.Name = "KeyInsert"
        Me.Text = "KeyInsert"
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private lblInfo As System.Windows.Forms.Label
    Private WithEvents btnStart As System.Windows.Forms.Button
    Private WithEvents lnkInfo As System.Windows.Forms.LinkLabel
    Private WithEvents bwKeyInserter As System.ComponentModel.BackgroundWorker
    Private lblStatus As System.Windows.Forms.Label
    Private colheadTime As System.Windows.Forms.ColumnHeader
    Private WithEvents btnRemove As System.Windows.Forms.Button
    Private WithEvents btnAdd As System.Windows.Forms.Button
    Private colheadKeyStroke As System.Windows.Forms.ColumnHeader
    Private WithEvents lstKeyStrokes As System.Windows.Forms.ListView
End Class
