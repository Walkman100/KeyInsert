Imports System.Xml

Partial Public Class KeyInsert
    ReadOnly theme As WalkmanLib.Theme = WalkmanLib.Theme.Default
    Function MessageBox(text As String, Optional buttons As MessageBoxButtons = 0, Optional icon As MessageBoxIcon = 0, Optional title As String = Nothing) As DialogResult
        If title Is Nothing Then title = Application.ProductName
        Return WalkmanLib.CustomMsgBox(text, theme, title, buttons, icon, WinVersionStyle.Win10, Me)
    End Function
    Function GetInput(ByRef input As String, Optional header As String = Nothing, Optional windowTitle As String = Nothing, Optional content As String = Nothing) As DialogResult
        Return WalkmanLib.InputDialog(input, theme, header, windowTitle, content, ownerForm:=Me)
    End Function

    Public Sub New()
        Me.InitializeComponent()
        lstKeyStrokes.DoubleBuffered(True)

        If WalkmanLib.GetDarkThemeEnabled() Then
            theme = WalkmanLib.Theme.Dark
        End If
        AddHandler lstKeyStrokes.DrawItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultItem
        AddHandler lstKeyStrokes.DrawSubItem, AddressOf WalkmanLib.CustomPaint.ListView_DrawDefaultSubItem
        AddHandler lstKeyStrokes.DrawColumnHeader, AddressOf WalkmanLib.CustomPaint.ListView_DrawCustomColumnHeader
        lstKeyStrokes.Tag = theme.ListViewColumnColors
        WalkmanLib.ApplyTheme(theme, Me, True)
        WalkmanLib.ApplyTheme(theme, Me.components.Components, True)
        If theme = WalkmanLib.Theme.Dark Then ToolStripManager.Renderer = New WalkmanLib.CustomPaint.ToolStripSystemRendererWithDisabled(theme.ToolStripItemDisabledText)

        lblVersion.Text = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build
        If WalkmanLib.IsAdmin Then
            Me.Text = "[Admin] KeyInsert"
        End If
        For Each s As String In My.Application.CommandLineArgs
            If IO.File.Exists(s) Then
                ReadConfig(s)
            ElseIf s = "start" Then
                btnStart_Click()
            Else
                MessageBox("""" & s & """ doesn't exist!", icon:=MessageBoxIcon.Exclamation)
            End If
        Next
    End Sub


    ' ==================== lstKeyStrokes ====================
    Sub lstKeyStrokes_ColumnClick() Handles lstKeyStrokes.ColumnClick
        lstKeyStrokes.Sorting = If(lstKeyStrokes.Sorting = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
        lstKeyStrokes.Sort()
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
                inputBoxText = item.SubItems.Item(0).Text
                If GetInput(inputBoxText, "Enter Keystroke:") = DialogResult.OK Then item.SubItems.Item(0).Text = inputBoxText
                inputBoxText = item.SubItems.Item(1).Text
                If GetInput(inputBoxText, "Enter the time to wait after """ & item.Text & """ has been inserted:") = DialogResult.OK Then item.SubItems.Item(1).Text = inputBoxText
            Next
        Else
            inputBoxText = lstKeyStrokes.FocusedItem.SubItems.Item(0).Text
            If GetInput(inputBoxText, "Enter Keystroke:") = DialogResult.OK Then lstKeyStrokes.FocusedItem.SubItems.Item(0).Text = inputBoxText
            inputBoxText = lstKeyStrokes.FocusedItem.SubItems.Item(1).Text
            If GetInput(inputBoxText, "Wait Time", content:="Enter the time to wait after """ & lstKeyStrokes.FocusedItem.Text & """ has been inserted:") = DialogResult.OK Then lstKeyStrokes.FocusedItem.SubItems.Item(1).Text = inputBoxText
        End If
    End Sub

    Sub CheckButtons() Handles lstKeyStrokes.Click, lstKeyStrokes.SelectedIndexChanged, lstKeyStrokes.AfterLabelEdit, lstKeyStrokes.ColumnReordered
        If IsNothing(lstKeyStrokes.FocusedItem) Then
            btnRemove.Enabled = False
            btnGetMouse.Enabled = False
        Else
            btnRemove.Enabled = True
            btnGetMouse.Enabled = True
        End If
        If lstKeyStrokes.Items.Count = 0 Then
            btnStart.Enabled = False
        Else
            btnStart.Enabled = True
        End If
    End Sub


    ' ==================== Right Panel ====================

    Sub btnAdd_Click() Handles btnAdd.Click
        Dim inputBoxText = "{ENTER}"
        If GetInput(inputBoxText, "Enter Keystroke to add:") = DialogResult.OK Then
            Dim tmpListViewItem As New ListViewItem(New String() {inputBoxText, "100"})
            lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
        End If
        CheckButtons()
    End Sub

    Sub btnRemove_Click() Handles btnRemove.Click
        If lstKeyStrokes.SelectedItems.Count > 1 Then
            For Each item As ListViewItem In lstKeyStrokes.SelectedItems
                item.Remove()
            Next
        Else
            lstKeyStrokes.FocusedItem.Remove()
        End If
        CheckButtons()
    End Sub

    Sub btnStart_Click() Handles btnStart.Click
        If chkStartMinimise.Checked Then WindowState = FormWindowState.Minimized
        If chkStartBackground.Checked Then Me.SendToBack()
        If chkStartHide.Checked Then Me.Hide()
        bwKeyInserter.RunWorkerAsync()
    End Sub

    Sub btnGetMouse_Click() Handles btnGetMouse.Click
        Me.SendToBack()
        Threading.Thread.Sleep(2000)

        lstKeyStrokes.FocusedItem.SubItems.Item(0).Text = "$MOVETO(" & Cursor.Position.X & ", " & Cursor.Position.Y & ")"
        Me.BringToFront()
    End Sub

    Sub lnkInfo_LinkClicked() Handles lnkInfo.LinkClicked
        Try
            Process.Start("https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=netframework-4.5#remarks")
        Catch ex As Exception
            If MessageBox("Unable to launch URL, copy to clipboard instead?", MessageBoxButtons.YesNo) = DialogResult.Yes Then _
                Clipboard.SetText("https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.sendkeys.send?view=netframework-4.5#remarks")
        End Try
    End Sub

    Sub btnMouseInfo_Click() Handles btnMouseInfo.Click
        Dim tmpString As String = "To move the mouse and click:" & vbNewLine & vbNewLine
        tmpString &= "$MOVETO(x, y) to set mouse position" & vbNewLine
        tmpString &= "$MOVE(x, y) to move mouse relative to current position" & vbNewLine & vbNewLine
        tmpString &= "$CLICK(LeftClick) to click" & vbNewLine & vbNewLine
        tmpString &= "Available CLICK arguments:" & vbNewLine
        tmpString &= "LeftClick, LeftDown, LeftUp" & vbNewLine
        tmpString &= "MiddleClick, MiddleDown, MiddleUp" & vbNewLine
        tmpString &= "RightClick, RightDown, RightUp" & vbNewLine
        tmpString &= "XClick, XDown, XUp" & vbNewLine & vbNewLine
        tmpString &= "<Button>Down and <Button>Up are used to click-and-drag"

        MessageBox(tmpString, icon:=MessageBoxIcon.Information, title:="Mouse Info")
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
    Sub chkTaskbar_CheckedChanged() Handles chkTaskbar.CheckedChanged
        progressBar.ShowInTaskbar = chkTaskbar.Checked
    End Sub
    Sub chkKeepOnTop_CheckedChanged() Handles chkKeepOnTop.CheckedChanged
        Me.TopMost = chkKeepOnTop.Checked
    End Sub


    ' ==================== BackgroundWorker methods ====================

    Function DisableKeyPressed() As Boolean
        If optKeyCtrl.Checked Then
            Return My.Computer.Keyboard.CtrlKeyDown
        ElseIf optKeyAlt.Checked Then
            Return My.Computer.Keyboard.AltKeyDown
        ElseIf optKeyShift.Checked Then
            Return My.Computer.Keyboard.ShiftKeyDown
        ElseIf optKeyNumLock.Checked Then
            Return My.Computer.Keyboard.NumLock
        ElseIf optKeyCapsLock.Checked Then
            Return My.Computer.Keyboard.CapsLock
        ElseIf optKeyScrollLock.Checked Then
            Return My.Computer.Keyboard.ScrollLock
        Else
            Throw New ApplicationException("No key selected to check!")
        End If
    End Function

    Sub bwKeyInserter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwKeyInserter.DoWork
        lblStatus.Text = "Running..."

        For i = 0 To numStartupDelay.Value Step 10
            lblStatus.Text = "Waiting (StartupDelay): " & numStartupDelay.Value - i

            Threading.Thread.Sleep(10)
            If DisableKeyPressed() Then Exit For
        Next

        Dim runCount As Integer = 0
        Do Until DisableKeyPressed() Or runCount = numRunCountLimit.Value Or bwKeyInserter.CancellationPending Or e.Cancel
            For Each item As ListViewItem In lstKeyStrokes.Items
                lblStatus.Text = "Running: " & item.Index & "/" & lstKeyStrokes.Items.Count & ", Inserting."

                If bwKeyInserter.CancellationPending Or e.Cancel Then Exit For
                ' this does the actual interaction
                bwKeyInserter.ReportProgress(item.Index) ' workaround for background workers not being able to interact with the UI

                progressBar.Value = item.Index / lstKeyStrokes.Items.Count * 100

                Dim i As Integer
                For i = 0 To item.SubItems.Item(1).Text Step 10
                    lblStatus.Text = "Running: " & item.Index + 1 & "/" & lstKeyStrokes.Items.Count & ", Waiting: " & item.SubItems.Item(1).Text - i
                    progressBar.Value = (item.Index + (i / item.SubItems.Item(1).Text)) / lstKeyStrokes.Items.Count * 100

                    Threading.Thread.Sleep(10)
                    If DisableKeyPressed() Then Exit Do
                Next
            Next
            runCount += 1
        Loop

        Threading.Thread.Sleep(100)

        If chkEndRestore.Checked Then WindowState = FormWindowState.Normal
        If chkEndForeground.Checked Then
            Me.BringToFront()
            Me.Focus()
            Me.Activate()
        End If
        If chkEndShow.Checked Then Me.Show()

        lblStatus.Text = "Not Running"
        progressBar.Value = 0
    End Sub

    ' cursor moving thanks to https://stackoverflow.com/a/8050847/2999220
    ' mouse click credits in WalkmanLib
    Sub bwKeyInserter_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bwKeyInserter.ProgressChanged
        Dim itemText As String = lstKeyStrokes.Items.Item(e.ProgressPercentage).Text

        If itemText.StartsWith("$MOVETO(", True, Nothing) Then '$MOVETO(x, y) to set mouse position
            ' remove $MOVETO(
            itemText = itemText.Substring(8)
            ' remove the ) at the end
            itemText = itemText.Remove(itemText.Length - 1)

            If itemText.Contains(",") Then
                Dim pointX As Integer
                Try
                    pointX = Integer.Parse(itemText.Split(",")(0))
                Catch ex As System.FormatException
                    bwKeyInserter.CancelAsync()
                    MessageBox("Error parsing $MOVETO at index " & e.ProgressPercentage & vbNewLine & vbNewLine & "Invalid integer: " & itemText.Split(",")(0), icon:=MessageBoxIcon.Error, title:="Error")
                    Exit Sub
                End Try

                Dim pointY As Integer
                Try
                    pointY = Integer.Parse(itemText.Split(",")(1))
                Catch ex As System.FormatException
                    bwKeyInserter.CancelAsync()
                    MessageBox("Error parsing $MOVETO at index " & e.ProgressPercentage & vbNewLine & vbNewLine & "Invalid integer: " & itemText.Split(",")(1), icon:=MessageBoxIcon.Error, title:="Error")
                    Exit Sub
                End Try

                Cursor.Position = New Point(pointX, pointY)
            Else
                bwKeyInserter.CancelAsync()
                MessageBox("Error parsing $MOVETO at index " & e.ProgressPercentage & vbNewLine & vbNewLine & ""","" seperator not found in " & itemText, icon:=MessageBoxIcon.Error, title:="Error")
            End If

        ElseIf itemText.StartsWith("$MOVE(", True, Nothing) Then '$MOVE(x, y) to move mouse relative to current position
            ' remove $MOVE(
            itemText = itemText.Substring(6)
            ' remove the ) at the end
            itemText = itemText.Remove(itemText.Length - 1)

            If itemText.Contains(",") Then
                Dim pointX As Integer
                Try
                    pointX = Integer.Parse(itemText.Split(",")(0))
                Catch ex As System.FormatException
                    bwKeyInserter.CancelAsync()
                    MessageBox("Error parsing $MOVE at index " & e.ProgressPercentage & vbNewLine & vbNewLine & "Invalid integer: " & itemText.Split(",")(0), icon:=MessageBoxIcon.Error, title:="Error")
                    Exit Sub
                End Try

                Dim pointY As Integer
                Try
                    pointY = Integer.Parse(itemText.Split(",")(1))
                Catch ex As System.FormatException
                    bwKeyInserter.CancelAsync()
                    MessageBox("Error parsing $MOVE at index " & e.ProgressPercentage & vbNewLine & vbNewLine & "Invalid integer: " & itemText.Split(",")(1), icon:=MessageBoxIcon.Error, title:="Error")
                    Exit Sub
                End Try

                Cursor.Position = New Point(Cursor.Position.X + pointX, Cursor.Position.Y + pointY)
            Else
                bwKeyInserter.CancelAsync()
                MessageBox("Error parsing $MOVE at index " & e.ProgressPercentage & vbNewLine & vbNewLine & ""","" seperator not found in " & itemText, icon:=MessageBoxIcon.Error, title:="Error")
            End If

        ElseIf itemText.StartsWith("$CLICK(", True, Nothing) Then '$CLICK(LeftClick) to click
            ' remove $CLICK(
            itemText = itemText.Substring(7)
            ' remove the ) at the end
            itemText = itemText.Remove(itemText.Length - 1)

            Dim resultMouseButton As MouseButton
            If MouseButton.TryParse(itemText, True, resultMouseButton) Then
                WalkmanLib.MouseClick(resultMouseButton)
            Else
                bwKeyInserter.CancelAsync()
                MessageBox("Error parsing $CLICK at index " & e.ProgressPercentage & vbNewLine & vbNewLine & "Invalid text: " & itemText, icon:=MessageBoxIcon.Error, title:="Error")
            End If
        Else
            SendKeys.Send(itemText)
        End If
    End Sub


    ' ==================== Config reading & saving ====================

    Sub ReadConfig(path As String)
        Dim reader As XmlReader = XmlReader.Create(path)
        Try
            reader.Read()
        Catch ex As XmlException
            reader.Close()
            MessageBox("Reading config failed! The error was: " & ex.ToString(), icon:=MessageBoxIcon.Error)
            Exit Sub
        End Try
        Dim attribute As String

        If reader.IsStartElement() AndAlso reader.Name.ToLower = "keyinsert" Then
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name.ToLower = "keylist" Then
                While reader.IsStartElement
                    If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name.ToLower = "keystring" Then
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
            If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name.ToLower = "settings" Then
                If reader.Read AndAlso reader.IsStartElement() AndAlso reader.Name.ToLower = "columnsettings" Then
                    While reader.IsStartElement
                        If reader.Read AndAlso reader.IsStartElement() Then
                            If reader.Name.ToLower = "keystrings" Then
                                attribute = reader("index")
                                If attribute IsNot Nothing Then
                                    colheadKeyStroke.DisplayIndex = attribute
                                End If

                                attribute = reader("width")
                                If attribute IsNot Nothing Then
                                    colheadKeyStroke.Width = attribute
                                End If
                            ElseIf reader.Name.ToLower = "waittime" Then
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
                    Select Case reader.Name.ToLower
                        Case "stopkey"
                            attribute = reader("key")
                            Select Case attribute.ToLower
                                Case "ctrl"
                                    optKeyCtrl.Checked = True
                                Case "alt"
                                    optKeyAlt.Checked = True
                                Case "shift"
                                    optKeyShift.Checked = True
                                Case "numlock"
                                    optKeyNumLock.Checked = True
                                Case "capslock"
                                    optKeyCapsLock.Checked = True
                                Case "scrolllock"
                                    optKeyScrollLock.Checked = True
                            End Select
                        Case "startactions"
                            chkStartMinimise.Checked = reader("minimise")
                            chkStartBackground.Checked = reader("background")
                            chkStartHide.Checked = reader("hide")
                        Case "endactions"
                            chkEndRestore.Checked = reader("restore")
                            chkEndForeground.Checked = reader("foreground")
                            chkEndShow.Checked = reader("show")
                        Case "startdelay"
                            numStartupDelay.Value = reader("value")
                        Case "runcountlimit"
                            numRunCountLimit.Value = reader("value")
                        Case "taskbarprogress"
                            chkTaskbar.Checked = reader("value")
                    End Select
                Loop
            End If
        End If

        reader.Close()
        CheckButtons()
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
                ElseIf optKeyAlt.Checked Then
                    writer.WriteAttributeString("key", "alt")
                ElseIf optKeyShift.Checked Then
                    writer.WriteAttributeString("key", "shift")
                ElseIf optKeyNumLock.Checked Then
                    writer.WriteAttributeString("key", "numlock")
                ElseIf optKeyCapsLock.Checked Then
                    writer.WriteAttributeString("key", "capslock")
                ElseIf optKeyScrollLock.Checked Then
                    writer.WriteAttributeString("key", "scrolllock")
                End If
            writer.WriteEndElement()

            writer.WriteStartElement("StartActions")
                writer.WriteAttributeString("minimise", chkStartMinimise.Checked)
                writer.WriteAttributeString("background", chkStartBackground.Checked)
                writer.WriteAttributeString("hide", chkStartHide.Checked)
            writer.WriteEndElement()

            writer.WriteStartElement("EndActions")
                writer.WriteAttributeString("restore", chkEndRestore.Checked)
                writer.WriteAttributeString("foreground", chkEndForeground.Checked)
                writer.WriteAttributeString("show", chkEndShow.Checked)
            writer.WriteEndElement()

            writer.WriteStartElement("StartDelay")
                writer.WriteAttributeString("value", numStartupDelay.Value)
            writer.WriteEndElement()
            writer.WriteStartElement("RunCountLimit")
                writer.WriteAttributeString("value", numRunCountLimit.Value)
            writer.WriteEndElement()
            writer.WriteStartElement("TaskbarProgress")
                writer.WriteAttributeString("value", chkTaskbar.Checked)
            writer.WriteEndElement()
        writer.WriteEndElement()

        writer.WriteEndElement()
        writer.WriteEndDocument()

        writer.Close()
    End Sub
End Class

Module Extensions
    <Runtime.CompilerServices.Extension()>
    Public Sub DoubleBuffered(control As Control, enable As Boolean) ' thanks to https://stackoverflow.com/a/15268338/2999220
        Dim doubleBufferPropertyInfo = control.[GetType]().GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable, Nothing)
    End Sub
End Module
