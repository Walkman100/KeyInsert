Imports System.Xml

Public Partial Class KeyInsert
    Public Sub New()
        Me.InitializeComponent()
        Dim concatPath As String = ""
        For Each s As String In My.Application.CommandLineArgs
            If IO.File.Exists(s) Then
                ReadConfig(s)
            Else
                If concatPath = "" Then
                    MsgBox("""" & s & """ doesn't exist! checking for non-enclosed path...", MsgBoxStyle.Exclamation)
                    concatPath = s
                Else
                    concatPath &= " " & s
                    If IO.File.Exists(concatPath) Then
                        MsgBox("Found """ & concatPath & """!", MsgBoxStyle.Information)
                        ReadConfig(concatPath)
                    Else
                        MsgBox("""" & concatPath & """ doesn't exist!", MsgBoxStyle.Exclamation)
                    End If
                End If
            End If
        Next
    End Sub
    
    Sub lstKeyStrokes_ColumnClick() Handles lstKeyStrokes.ColumnClick
        lstKeyStrokes.Sorting = IIf(lstKeyStrokes.Sorting = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
        lstKeyStrokes.Sort
    End Sub
    
    Sub ResizeByHeader(sender As Object, e As EventArgs) Handles contextCommandsResizePathHeader.Click, contextCommandsResizeArgsHeader.Click
        lstKeyStrokes.AutoResizeColumn(sender.Tag, ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub
    
    Sub ResizeByContent(sender As Object, e As EventArgs) Handles contextCommandsResizePathContent.Click, contextCommandsResizeArgsContent.Click
        lstKeyStrokes.AutoResizeColumn(sender.Tag, ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub
    
    Sub ResizeAllByHeader() Handles contextCommandsResizeAllHeader.Click
        lstKeyStrokes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub
    
    Sub ResizeAllByContent() Handles contextCommandsResizeAllContent.Click
        lstKeyStrokes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
    End Sub
    
    Sub lstKeyStrokes_DragEnter(sender As Object, e As DragEventArgs) Handles lstKeyStrokes.DragEnter
        If e.Data.GetDataPresent(DataFormats.Text) Or e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    
    Sub lstKeyStrokes_DragDrop(sender As Object, e As DragEventArgs) Handles lstKeyStrokes.DragDrop
        If e.Data.GetDataPresent(DataFormats.Text) Then
            Dim data = e.Data.GetData(DataFormats.Text).ToString
            If data.Contains(vbNewLine) Then
                For Each line As String In data.Split(vbNewLine)
                    Dim tmpListViewItem As New ListViewItem(New String() {line, "100"})
                    lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
                Next
            Else
                Dim tmpListViewItem As New ListViewItem(New String() {data, "100"})
                lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
            End If
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim DroppedPath = e.Data.GetData(DataFormats.FileDrop)(0)
            
            Dim reader As XmlReader = XmlReader.Create(DroppedPath)
            Try
                reader.Read()
                ' File can be read as XML
                ReadConfig(DroppedPath)
            Catch ex As XmlException
                reader.Close()
                ' File can't be read as XML
                For Each line In System.IO.File.ReadAllLines(DroppedPath)
                    Dim tmpListViewItem As New ListViewItem(New String() {line, "100"})
                    lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
                Next
            End Try
        End If
    End Sub
    
    Sub EditSelectedEntry() Handles lstKeyStrokes.DoubleClick
        Dim inputBoxText As String
        If lstKeyStrokes.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstKeyStrokes.SelectedItems
                inputBoxText = InputBox("Enter Keystroke:", "", item.SubItems.Item(0).Text)
                If inputBoxText <> "" Then item.SubItems.Item(0).Text = inputBoxText
                inputBoxText = InputBox("Enter the time to wait after """ & item.Text & """ has been inserted:", "", item.SubItems.Item(1).Text)
                If inputBoxText <> "" Then item.SubItems.Item(1).Text = inputBoxText
            Next
        Else
            inputBoxText = InputBox("Enter Keystroke:", "", lstKeyStrokes.FocusedItem.SubItems.Item(0).Text)
            If inputBoxText <> "" Then lstKeyStrokes.FocusedItem.SubItems.Item(0).Text = inputBoxText
            inputBoxText = InputBox("Enter the time to wait after """ & lstKeyStrokes.FocusedItem.Text & """ has been inserted:", "", lstKeyStrokes.FocusedItem.SubItems.Item(1).Text)
            If inputBoxText <> "" Then lstKeyStrokes.FocusedItem.SubItems.Item(1).Text = inputBoxText
        End If
    End Sub
    
    Sub CheckButtons() Handles lstKeyStrokes.Click, lstKeyStrokes.SelectedIndexChanged, lstKeyStrokes.AfterLabelEdit, lstKeyStrokes.ColumnReordered
        If IsNothing(lstKeyStrokes.FocusedItem) Then
            btnRemove.Enabled = False
        Else
            btnRemove.Enabled = True
        End If
        If lstKeyStrokes.Items.Count = 0 Then
            btnStart.Enabled = False
        Else
            btnStart.Enabled = True
        End If
    End Sub
    
    Sub btnAdd_Click() Handles btnAdd.Click
        Dim inputBoxText = InputBox("Enter Keystroke to add:", "", "{ENTER}")
        If inputBoxText <> "" Then
            Dim tmpListViewItem As New ListViewItem(New String() {inputBoxText, "100"})
            lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
        End If
        CheckButtons
    End Sub
    
    Sub btnRemove_Click() Handles btnRemove.Click
        If lstKeyStrokes.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstKeyStrokes.SelectedItems
                item.Remove
            Next
        Else
            lstKeyStrokes.FocusedItem.Remove
        End If
        CheckButtons
    End Sub
    
    Sub lnkInfo_LinkClicked() Handles lnkInfo.LinkClicked
        Try
            Process.Start("https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx?cs-lang=vb#Anchor_2")
        Catch ex As Exception
            If MsgBox("Unable to launch URL, copy to clipboard instead?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then _
                Clipboard.SetText("https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx?cs-lang=vb#Anchor_2")
        End Try
    End Sub
    
    Sub chkStartMinimise_CheckedChanged() Handles chkStartMinimise.CheckedChanged
        chkEndRestore.Checked = chkStartMinimise.Checked
    End Sub
    
    Sub chkStartBackground_CheckedChanged() Handles chkStartBackground.CheckedChanged
        chkEndForeground.Checked = chkStartBackground.Checked
    End Sub
    
    Sub chkStartHide_CheckedChanged() Handles chkStartHide.CheckedChanged
        chkEndShow.Checked = chkStartHide.Checked
        chkEndShow.Enabled = Not chkStartHide.Checked
    End Sub
    
    Sub btnStart_Click() Handles btnStart.Click
        If chkStartMinimise.Checked Then WindowState = FormWindowState.Minimized
        If chkStartBackground.Checked Then Me.SendToBack
        If chkStartHide.Checked Then Me.Hide
        bwKeyInserter.RunWorkerAsync
    End Sub
    
    Function DisableKeyPressed As Boolean
        If optKeyCtrl.Checked Then
            Return My.Computer.Keyboard.CtrlKeyDown
        ElseIf optKeyAlt.Checked
            Return My.Computer.Keyboard.AltKeyDown
        ElseIf optKeyShift.Checked
            Return My.Computer.Keyboard.ShiftKeyDown
        Else
            Return "Wtf"
        End If
    End Function
    
    Sub bwKeyInserter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwKeyInserter.DoWork
        lblStatus.Text = "Running..."
        
        For i = 0 To numStartupDelay.Value Step 10
            lblStatus.Text = "Waiting (StartupDelay): " & numStartupDelay.Value - i
            
            Threading.Thread.Sleep(10)
            If DisableKeyPressed Then Exit For
        Next
        
        Dim runCount As Integer = 0
        Do Until DisableKeyPressed Or runCount = numRunCountLimit.Value
            For Each item As ListViewItem In lstKeyStrokes.Items
                lblStatus.Text = "Running: " & item.Index & "/" & lstKeyStrokes.Items.Count & ", Inserting."
                bwKeyInserter.ReportProgress(item.Index) ' workaround for background workers not being able to interact with the UI
                progressBar.Value = item.Index / lstKeyStrokes.Items.Count*100
                
                Dim i As Integer
                For i = 0 To item.SubItems.Item(1).Text Step 10
                    lblStatus.Text = "Running: " & item.Index + 1 & "/" & lstKeyStrokes.Items.Count & ", Waiting: " & item.SubItems.Item(1).Text - i
                    progressBar.Value = (item.Index + (i / item.SubItems.Item(1).Text) ) / lstKeyStrokes.Items.Count*100
                    
                    Threading.Thread.Sleep(10)
                    If DisableKeyPressed Then Exit Do
                Next
            Next
            runCount += 1
        Loop
        
        Threading.Thread.Sleep(100)
        
        If chkEndRestore.Checked Then WindowState = FormWindowState.Normal
        If chkEndForeground.Checked Then
            Me.BringToFront
            Me.Focus
            Me.Activate
        End If
        If chkEndShow.Checked Then Me.Show
        
        lblStatus.Text = "Not Running"
        progressBar.Value = 0
    End Sub
    
    Sub bwKeyInserter_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bwKeyInserter.ProgressChanged
        SendKeys.Send(lstKeyStrokes.Items.Item(e.ProgressPercentage).Text)
    End Sub
    
    Sub chkTaskbar_CheckedChanged() Handles chkTaskbar.CheckedChanged
        progressBar.ShowInTaskbar = chkTaskbar.Checked
    End Sub
    
    Sub ReadConfig(path As String)
        Dim reader As XmlReader = XmlReader.Create(path)
        Try
            reader.Read()
        Catch ex As XmlException
            reader.Close()
            MsgBox("Reading config failed! The error was: " & ex.ToString, MsgBoxStyle.Critical)
            Exit Sub
        End Try
        Dim attribute As String
        
        If reader.IsStartElement() AndAlso reader.Name = "KeyInsert" Then
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "KeyList" Then
                While reader.IsStartElement
                    If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "KeyString" Then
                        Dim tmpListViewItem As New ListViewItem(New String() {"{ENTER}", "100"})
                        
                        attribute = reader("keys")
                        If attribute IsNot Nothing Then
                            tmpListViewItem.Text = attribute
                        End If
                        
                        attribute = reader("time")
                        If attribute IsNot Nothing Then
                            tmpListViewItem.SubItems.Item(1).Text = attribute
                        End If
                        
                        lstKeyStrokes.Items.Add(tmpListViewItem)
                    End If
                End While
            End If
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "Settings" Then
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name = "ColumnSettings" Then
                    While reader.IsStartElement
                        If reader.Read AndAlso reader.IsStartElement() Then
                            If reader.Name = "KeyStrings" Then
                                attribute = reader("index")
                                If attribute IsNot Nothing Then
                                    colheadKeyStroke.DisplayIndex = attribute
                                End If
                                
                                attribute = reader("width")
                                If attribute IsNot Nothing Then
                                    colheadKeyStroke.Width = attribute
                                End If
                            ElseIf reader.Name = "WaitTime"
                                attribute = reader("index")
                                If attribute IsNot Nothing Then
                                    colheadTime.DisplayIndex = attribute
                                End If
                                
                                attribute = reader("width")
                                If attribute IsNot Nothing Then
                                    colheadTime.Width = attribute
                                End If
                            End If
                        End If
                    End While
                End If
                
                Do While reader.Read AndAlso reader.IsStartElement
                    Select Case reader.Name
                        Case "StopKey"
                            attribute = reader("key")
                            Select Case attribute
                                Case "ctrl"
                                    optKeyCtrl.Checked = True
                                Case "alt"
                                    optKeyAlt.Checked = True
                                Case "shift"
                                    optKeyShift.Checked = True
                            End Select
                        Case "StartActions"
                            chkStartMinimise.Checked = reader("minimise")
                            chkStartBackground.Checked = reader("background")
                            chkStartHide.Checked = reader("hide")
                        Case "EndActions"
                            chkEndRestore.Checked = reader("restore")
                            chkEndForeground.Checked = reader("foreground")
                            chkEndShow.Checked = reader("show")
                    End Select
                Loop
            End If
        End If
        
        reader.Close()
        CheckButtons
    End Sub
    
    Sub WriteConfig(path As String)
        Dim XMLwSettings As New XmlWriterSettings()
        XMLwSettings.Indent = True
        Dim writer As XmlWriter = XmlWriter.Create(path, XMLwSettings)
        
        writer.WriteStartDocument()
        writer.WriteStartElement("KeyInsert")
        
        writer.WriteStartElement("KeyList")
        For Each item In lstKeyStrokes.Items
            writer.WriteStartElement("KeyString")
                writer.WriteAttributeString("keys", item.Text)
                writer.WriteAttributeString("time", item.SubItems.Item(1).Text)
            writer.WriteEndElement()
        Next
        writer.WriteEndElement()
        
        writer.WriteStartElement("Settings")
            writer.WriteStartElement("ColumnSettings")
                writer.WriteStartElement("KeyStrings")
                    writer.WriteAttributeString("index", colheadKeyStroke.DisplayIndex)
                    writer.WriteAttributeString("width", colheadKeyStroke.Width)
                writer.WriteEndElement()
                writer.WriteStartElement("WaitTime")
                    writer.WriteAttributeString("index", colheadTime.DisplayIndex)
                    writer.WriteAttributeString("width", colheadTime.Width)
                writer.WriteEndElement()
            writer.WriteEndElement()
            
            writer.WriteStartElement("StopKey")
                If optKeyCtrl.Checked Then
                    writer.WriteAttributeString("key", "ctrl")
                ElseIf optKeyAlt.Checked
                    writer.WriteAttributeString("key", "alt")
                ElseIf optKeyShift.Checked
                    writer.WriteAttributeString("key", "shift")
                End If
            writer.WriteEndElement
            
            writer.WriteStartElement("StartActions")
                writer.WriteAttributeString("minimise", chkStartMinimise.Checked)
                writer.WriteAttributeString("background", chkStartBackground.Checked)
                writer.WriteAttributeString("hide", chkStartHide.Checked)
            writer.WriteEndElement
            
            writer.WriteStartElement("EndActions")
                writer.WriteAttributeString("restore", chkEndRestore.Checked)
                writer.WriteAttributeString("foreground", chkEndForeground.Checked)
                writer.WriteAttributeString("show", chkEndShow.Checked)
            writer.WriteEndElement
        writer.WriteEndElement()
        
        writer.WriteEndElement()
        writer.WriteEndDocument()
        
        writer.Close
    End Sub
    
    Sub btnScriptSave_Click() Handles btnScriptSave.Click
        If sfdConfig.ShowDialog = DialogResult.OK Then
            WriteConfig(sfdConfig.FileName)
        End If
    End Sub
    
    Sub btnScriptLoad_Click() Handles btnScriptLoad.Click
        If ofdConfig.ShowDialog = DialogResult.OK Then
            ReadConfig(ofdConfig.FileName)
        End If
    End Sub
End Class
