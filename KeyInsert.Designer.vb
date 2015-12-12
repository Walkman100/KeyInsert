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
        Me.components = New System.ComponentModel.Container()
        Me.lstKeyStrokes = New System.Windows.Forms.ListView()
        Me.colheadKeyStroke = New System.Windows.Forms.ColumnHeader()
        Me.colheadTime = New System.Windows.Forms.ColumnHeader()
        Me.contextCommands = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.contextCommandsResizePathHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsResizePathContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsSeperator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.contextCommandsResizeArgsHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsResizeArgsContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsSeperator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.contextCommandsResizeAllHeader = New System.Windows.Forms.ToolStripMenuItem()
        Me.contextCommandsResizeAllContent = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.bwKeyInserter = New System.ComponentModel.BackgroundWorker()
        Me.lnkInfo = New System.Windows.Forms.LinkLabel()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.grpStopKey = New System.Windows.Forms.GroupBox()
        Me.optKeyShift = New System.Windows.Forms.RadioButton()
        Me.optKeyAlt = New System.Windows.Forms.RadioButton()
        Me.optKeyCtrl = New System.Windows.Forms.RadioButton()
        Me.grpStart = New System.Windows.Forms.GroupBox()
        Me.chkStartHide = New System.Windows.Forms.CheckBox()
        Me.chkStartBackground = New System.Windows.Forms.CheckBox()
        Me.chkStartMinimise = New System.Windows.Forms.CheckBox()
        Me.grpEnd = New System.Windows.Forms.GroupBox()
        Me.chkEndShow = New System.Windows.Forms.CheckBox()
        Me.chkEndForeground = New System.Windows.Forms.CheckBox()
        Me.chkEndRestore = New System.Windows.Forms.CheckBox()
        Me.btnScriptSave = New System.Windows.Forms.Button()
        Me.btnScriptLoad = New System.Windows.Forms.Button()
        Me.ofdConfig = New System.Windows.Forms.OpenFileDialog()
        Me.sfdConfig = New System.Windows.Forms.SaveFileDialog()
        Me.contextCommands.SuspendLayout
        Me.grpStopKey.SuspendLayout
        Me.grpStart.SuspendLayout
        Me.grpEnd.SuspendLayout
        Me.SuspendLayout
        '
        'lstKeyStrokes
        '
        Me.lstKeyStrokes.AllowColumnReorder = true
        Me.lstKeyStrokes.AllowDrop = true
        Me.lstKeyStrokes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
                        Or System.Windows.Forms.AnchorStyles.Left)  _
                        Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lstKeyStrokes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colheadKeyStroke, Me.colheadTime})
        Me.lstKeyStrokes.ContextMenuStrip = Me.contextCommands
        Me.lstKeyStrokes.FullRowSelect = true
        Me.lstKeyStrokes.GridLines = true
        Me.lstKeyStrokes.HideSelection = false
        Me.lstKeyStrokes.LabelEdit = true
        Me.lstKeyStrokes.Location = New System.Drawing.Point(12, 12)
        Me.lstKeyStrokes.Name = "lstKeyStrokes"
        Me.lstKeyStrokes.Size = New System.Drawing.Size(402, 226)
        Me.lstKeyStrokes.TabIndex = 22
        Me.lstKeyStrokes.UseCompatibleStateImageBehavior = false
        Me.lstKeyStrokes.View = System.Windows.Forms.View.Details
        '
        'colheadKeyStroke
        '
        Me.colheadKeyStroke.Text = "Keystroke"
        Me.colheadKeyStroke.Width = 269
        '
        'colheadTime
        '
        Me.colheadTime.Text = "Wait Time (ms)"
        Me.colheadTime.Width = 112
        '
        'contextCommands
        '
        Me.contextCommands.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.contextCommandsResizePathHeader, Me.contextCommandsResizePathContent, Me.contextCommandsSeperator1, Me.contextCommandsResizeArgsHeader, Me.contextCommandsResizeArgsContent, Me.contextCommandsSeperator2, Me.contextCommandsResizeAllHeader, Me.contextCommandsResizeAllContent})
        Me.contextCommands.Name = "contextMenuStripCommands"
        Me.contextCommands.Size = New System.Drawing.Size(267, 148)
        '
        'contextCommandsResizePathHeader
        '
        Me.contextCommandsResizePathHeader.AutoToolTip = true
        Me.contextCommandsResizePathHeader.Name = "contextCommandsResizePathHeader"
        Me.contextCommandsResizePathHeader.Size = New System.Drawing.Size(266, 22)
        Me.contextCommandsResizePathHeader.Tag = "0"
        Me.contextCommandsResizePathHeader.Text = "Resize Keystroke column by Header"
        '
        'contextCommandsResizePathContent
        '
        Me.contextCommandsResizePathContent.AutoToolTip = true
        Me.contextCommandsResizePathContent.Name = "contextCommandsResizePathContent"
        Me.contextCommandsResizePathContent.Size = New System.Drawing.Size(266, 22)
        Me.contextCommandsResizePathContent.Tag = "0"
        Me.contextCommandsResizePathContent.Text = "Resize Keystroke column by Content"
        '
        'contextCommandsSeperator1
        '
        Me.contextCommandsSeperator1.Name = "contextCommandsSeperator1"
        Me.contextCommandsSeperator1.Size = New System.Drawing.Size(263, 6)
        '
        'contextCommandsResizeArgsHeader
        '
        Me.contextCommandsResizeArgsHeader.AutoToolTip = true
        Me.contextCommandsResizeArgsHeader.Name = "contextCommandsResizeArgsHeader"
        Me.contextCommandsResizeArgsHeader.Size = New System.Drawing.Size(266, 22)
        Me.contextCommandsResizeArgsHeader.Tag = "1"
        Me.contextCommandsResizeArgsHeader.Text = "Resize Time column by Header"
        '
        'contextCommandsResizeArgsContent
        '
        Me.contextCommandsResizeArgsContent.AutoToolTip = true
        Me.contextCommandsResizeArgsContent.Name = "contextCommandsResizeArgsContent"
        Me.contextCommandsResizeArgsContent.Size = New System.Drawing.Size(266, 22)
        Me.contextCommandsResizeArgsContent.Tag = "1"
        Me.contextCommandsResizeArgsContent.Text = "Resize Time column by Content"
        '
        'contextCommandsSeperator2
        '
        Me.contextCommandsSeperator2.Name = "contextCommandsSeperator2"
        Me.contextCommandsSeperator2.Size = New System.Drawing.Size(263, 6)
        '
        'contextCommandsResizeAllHeader
        '
        Me.contextCommandsResizeAllHeader.AutoToolTip = true
        Me.contextCommandsResizeAllHeader.Name = "contextCommandsResizeAllHeader"
        Me.contextCommandsResizeAllHeader.Size = New System.Drawing.Size(266, 22)
        Me.contextCommandsResizeAllHeader.Text = "Resize all by Column Header"
        '
        'contextCommandsResizeAllContent
        '
        Me.contextCommandsResizeAllContent.AutoToolTip = true
        Me.contextCommandsResizeAllContent.Name = "contextCommandsResizeAllContent"
        Me.contextCommandsResizeAllContent.Size = New System.Drawing.Size(266, 22)
        Me.contextCommandsResizeAllContent.Text = "Resize all by Column Content"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(420, 12)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(132, 23)
        Me.btnAdd.TabIndex = 23
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = true
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnRemove.Location = New System.Drawing.Point(420, 41)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(132, 23)
        Me.btnRemove.TabIndex = 24
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = true
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = true
        Me.lblStatus.Location = New System.Drawing.Point(504, 228)
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
        Me.lnkInfo.Location = New System.Drawing.Point(420, 228)
        Me.lnkInfo.Name = "lnkInfo"
        Me.lnkInfo.Size = New System.Drawing.Size(78, 13)
        Me.lnkInfo.TabIndex = 38
        Me.lnkInfo.TabStop = true
        Me.lnkInfo.Text = "Key strings info"
        '
        'btnStart
        '
        Me.btnStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnStart.Location = New System.Drawing.Point(420, 70)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(132, 23)
        Me.btnStart.TabIndex = 39
        Me.btnStart.Text = "Start"
        Me.btnStart.UseVisualStyleBackColor = true
        '
        'grpStopKey
        '
        Me.grpStopKey.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpStopKey.Controls.Add(Me.optKeyShift)
        Me.grpStopKey.Controls.Add(Me.optKeyAlt)
        Me.grpStopKey.Controls.Add(Me.optKeyCtrl)
        Me.grpStopKey.Location = New System.Drawing.Point(558, 12)
        Me.grpStopKey.Name = "grpStopKey"
        Me.grpStopKey.Size = New System.Drawing.Size(132, 88)
        Me.grpStopKey.TabIndex = 40
        Me.grpStopKey.TabStop = false
        Me.grpStopKey.Text = "Key to press to stop:"
        '
        'optKeyShift
        '
        Me.optKeyShift.AutoSize = true
        Me.optKeyShift.Location = New System.Drawing.Point(6, 65)
        Me.optKeyShift.Name = "optKeyShift"
        Me.optKeyShift.Size = New System.Drawing.Size(46, 17)
        Me.optKeyShift.TabIndex = 2
        Me.optKeyShift.Text = "Shift"
        Me.optKeyShift.UseVisualStyleBackColor = true
        '
        'optKeyAlt
        '
        Me.optKeyAlt.AutoSize = true
        Me.optKeyAlt.Location = New System.Drawing.Point(6, 42)
        Me.optKeyAlt.Name = "optKeyAlt"
        Me.optKeyAlt.Size = New System.Drawing.Size(37, 17)
        Me.optKeyAlt.TabIndex = 1
        Me.optKeyAlt.Text = "Alt"
        Me.optKeyAlt.UseVisualStyleBackColor = true
        '
        'optKeyCtrl
        '
        Me.optKeyCtrl.AutoSize = true
        Me.optKeyCtrl.Checked = true
        Me.optKeyCtrl.Location = New System.Drawing.Point(6, 19)
        Me.optKeyCtrl.Name = "optKeyCtrl"
        Me.optKeyCtrl.Size = New System.Drawing.Size(82, 17)
        Me.optKeyCtrl.TabIndex = 0
        Me.optKeyCtrl.TabStop = true
        Me.optKeyCtrl.Text = "Ctrl (Control)"
        Me.optKeyCtrl.UseVisualStyleBackColor = true
        '
        'grpStart
        '
        Me.grpStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpStart.Controls.Add(Me.chkStartHide)
        Me.grpStart.Controls.Add(Me.chkStartBackground)
        Me.grpStart.Controls.Add(Me.chkStartMinimise)
        Me.grpStart.Location = New System.Drawing.Point(420, 106)
        Me.grpStart.Name = "grpStart"
        Me.grpStart.Size = New System.Drawing.Size(132, 88)
        Me.grpStart.TabIndex = 41
        Me.grpStart.TabStop = false
        Me.grpStart.Text = "Action on script start:"
        '
        'chkStartHide
        '
        Me.chkStartHide.AutoSize = true
        Me.chkStartHide.Location = New System.Drawing.Point(6, 65)
        Me.chkStartHide.Name = "chkStartHide"
        Me.chkStartHide.Size = New System.Drawing.Size(87, 17)
        Me.chkStartHide.TabIndex = 2
        Me.chkStartHide.Text = "Hide window"
        Me.chkStartHide.UseVisualStyleBackColor = true
        '
        'chkStartBackground
        '
        Me.chkStartBackground.AutoSize = true
        Me.chkStartBackground.Location = New System.Drawing.Point(6, 42)
        Me.chkStartBackground.Name = "chkStartBackground"
        Me.chkStartBackground.Size = New System.Drawing.Size(123, 17)
        Me.chkStartBackground.TabIndex = 1
        Me.chkStartBackground.Text = "Send to background"
        Me.chkStartBackground.UseVisualStyleBackColor = true
        '
        'chkStartMinimise
        '
        Me.chkStartMinimise.AutoSize = true
        Me.chkStartMinimise.Checked = true
        Me.chkStartMinimise.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStartMinimise.Location = New System.Drawing.Point(6, 19)
        Me.chkStartMinimise.Name = "chkStartMinimise"
        Me.chkStartMinimise.Size = New System.Drawing.Size(66, 17)
        Me.chkStartMinimise.TabIndex = 0
        Me.chkStartMinimise.Text = "Minimise"
        Me.chkStartMinimise.UseVisualStyleBackColor = true
        '
        'grpEnd
        '
        Me.grpEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpEnd.Controls.Add(Me.chkEndShow)
        Me.grpEnd.Controls.Add(Me.chkEndForeground)
        Me.grpEnd.Controls.Add(Me.chkEndRestore)
        Me.grpEnd.Location = New System.Drawing.Point(558, 106)
        Me.grpEnd.Name = "grpEnd"
        Me.grpEnd.Size = New System.Drawing.Size(132, 88)
        Me.grpEnd.TabIndex = 42
        Me.grpEnd.TabStop = false
        Me.grpEnd.Text = "Action on script end:"
        '
        'chkEndShow
        '
        Me.chkEndShow.AutoSize = true
        Me.chkEndShow.Location = New System.Drawing.Point(6, 65)
        Me.chkEndShow.Name = "chkEndShow"
        Me.chkEndShow.Size = New System.Drawing.Size(92, 17)
        Me.chkEndShow.TabIndex = 2
        Me.chkEndShow.Text = "Show window"
        Me.chkEndShow.UseVisualStyleBackColor = true
        '
        'chkEndForeground
        '
        Me.chkEndForeground.AutoSize = true
        Me.chkEndForeground.Location = New System.Drawing.Point(6, 42)
        Me.chkEndForeground.Name = "chkEndForeground"
        Me.chkEndForeground.Size = New System.Drawing.Size(116, 17)
        Me.chkEndForeground.TabIndex = 1
        Me.chkEndForeground.Text = "Bring to foreground"
        Me.chkEndForeground.UseVisualStyleBackColor = true
        '
        'chkEndRestore
        '
        Me.chkEndRestore.AutoSize = true
        Me.chkEndRestore.Location = New System.Drawing.Point(6, 19)
        Me.chkEndRestore.Name = "chkEndRestore"
        Me.chkEndRestore.Size = New System.Drawing.Size(63, 17)
        Me.chkEndRestore.TabIndex = 0
        Me.chkEndRestore.Text = "Restore"
        Me.chkEndRestore.UseVisualStyleBackColor = true
        '
        'btnScriptSave
        '
        Me.btnScriptSave.AutoSize = true
        Me.btnScriptSave.Location = New System.Drawing.Point(420, 200)
        Me.btnScriptSave.Name = "btnScriptSave"
        Me.btnScriptSave.Size = New System.Drawing.Size(132, 23)
        Me.btnScriptSave.TabIndex = 43
        Me.btnScriptSave.Text = "Save script..."
        Me.btnScriptSave.UseVisualStyleBackColor = true
        '
        'btnScriptLoad
        '
        Me.btnScriptLoad.AutoSize = true
        Me.btnScriptLoad.Location = New System.Drawing.Point(558, 200)
        Me.btnScriptLoad.Name = "btnScriptLoad"
        Me.btnScriptLoad.Size = New System.Drawing.Size(132, 23)
        Me.btnScriptLoad.TabIndex = 44
        Me.btnScriptLoad.Text = "Load script..."
        Me.btnScriptLoad.UseVisualStyleBackColor = true
        '
        'ofdConfig
        '
        Me.ofdConfig.DefaultExt = "xml.KeyInsert"
        Me.ofdConfig.Filter = "KeyInsert configs|*.xml.KeyInsert|All files|*.*"
        Me.ofdConfig.SupportMultiDottedExtensions = true
        '
        'sfdConfig
        '
        Me.sfdConfig.DefaultExt = "xml.KeyInsert"
        Me.sfdConfig.Filter = "KeyInsert configs|*.xml.KeyInsert|All files|*.*"
        Me.sfdConfig.SupportMultiDottedExtensions = true
        '
        'KeyInsert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(702, 250)
        Me.Controls.Add(Me.btnScriptLoad)
        Me.Controls.Add(Me.btnScriptSave)
        Me.Controls.Add(Me.grpEnd)
        Me.Controls.Add(Me.grpStart)
        Me.Controls.Add(Me.grpStopKey)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.lnkInfo)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lstKeyStrokes)
        Me.Icon = Global.KeyInsert.Resources.key_presser_5
        Me.Name = "KeyInsert"
        Me.Text = "KeyInsert"
        Me.contextCommands.ResumeLayout(false)
        Me.grpStopKey.ResumeLayout(false)
        Me.grpStopKey.PerformLayout
        Me.grpStart.ResumeLayout(false)
        Me.grpStart.PerformLayout
        Me.grpEnd.ResumeLayout(false)
        Me.grpEnd.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout
    End Sub
    Private WithEvents btnScriptSave As System.Windows.Forms.Button
    Private WithEvents btnScriptLoad As System.Windows.Forms.Button
    Private sfdConfig As System.Windows.Forms.SaveFileDialog
    Private ofdConfig As System.Windows.Forms.OpenFileDialog
    Private grpStart As System.Windows.Forms.GroupBox
    Private WithEvents chkStartHide As System.Windows.Forms.CheckBox
    Private WithEvents chkStartBackground As System.Windows.Forms.CheckBox
    Private WithEvents chkStartMinimise As System.Windows.Forms.CheckBox
    Private grpEnd As System.Windows.Forms.GroupBox
    Private chkEndShow As System.Windows.Forms.CheckBox
    Private chkEndForeground As System.Windows.Forms.CheckBox
    Private chkEndRestore As System.Windows.Forms.CheckBox
    Private optKeyCtrl As System.Windows.Forms.RadioButton
    Private optKeyAlt As System.Windows.Forms.RadioButton
    Private optKeyShift As System.Windows.Forms.RadioButton
    Private grpStopKey As System.Windows.Forms.GroupBox
    Private WithEvents contextCommandsResizeAllContent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizeAllHeader As System.Windows.Forms.ToolStripMenuItem
    Private contextCommandsSeperator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents contextCommandsResizeArgsContent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizeArgsHeader As System.Windows.Forms.ToolStripMenuItem
    Private contextCommandsSeperator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents contextCommandsResizePathContent As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents contextCommandsResizePathHeader As System.Windows.Forms.ToolStripMenuItem
    Private contextCommands As System.Windows.Forms.ContextMenuStrip
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
