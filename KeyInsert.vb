﻿Public Partial Class KeyInsert
    Public Sub New()
        Me.InitializeComponent()
    End Sub
    
    Sub lstKeyStrokes_ColumnClick() Handles lstKeyStrokes.ColumnClick
        lstKeyStrokes.Sorting = IIf(lstKeyStrokes.Sorting = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
        lstKeyStrokes.Sort
    End Sub
    
'    Sub ResizeByHeader(sender As Object, e As EventArgs) Handles contextCommandsResizePathHeader.Click, _
'            contextCommandsResizeArgsHeader.Click, contextCommandsResizeArgHeader.Click
'        lstKeyStrokes.AutoResizeColumn(sender.Tag, ColumnHeaderAutoResizeStyle.HeaderSize)
'    End Sub
'    
'    Sub ResizeByContent(sender As Object, e As EventArgs) Handles contextCommandsResizePathContent.Click, _
'            contextCommandsResizeArgsContent.Click, contextCommandsResizeArgContent.Click
'        lstKeyStrokes.AutoResizeColumn(sender.Tag, ColumnHeaderAutoResizeStyle.ColumnContent)
'    End Sub
'    
'    Sub ResizeAllByHeader() Handles contextCommandsResizeAllHeader.Click
'        lstKeyStrokes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
'    End Sub
'    
'    Sub ResizeAllByContent() Handles contextCommandsResizeAllContent.Click
'        lstKeyStrokes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
'    End Sub
    
    Sub lstKeyStrokes_DragEnter(sender As Object, e As DragEventArgs) Handles lstKeyStrokes.DragEnter
        If e.Data.GetDataPresent(DataFormats.Text) Then 'Or e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    
    Sub lstKeyStrokes_DragDrop(sender As Object, e As DragEventArgs) Handles lstKeyStrokes.DragDrop
        If e.Data.GetDataPresent(DataFormats.Text) Then
            Dim tmpListViewItem As New ListViewItem(New String() {e.Data.GetData(DataFormats.Text).ToString, " ", "draggedFile"})
            lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop)
            For i = 0 To Integer.MaxValue
                If (e.Data.GetData(DataFormats.FileDrop)(i) <> Nothing) Then
                    Dim tmpListViewItem As New ListViewItem(New String() {e.Data.GetData(DataFormats.FileDrop)(i), " ", "draggedFile"})
                    lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
                Else
                    Exit For
                End If
            Next
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
    End Sub
    
    Sub btnAdd_Click() Handles btnAdd.Click
        Dim tmpListViewItem As New ListViewItem(New String() {"{ENTER}", "1"})
        lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
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
            Process.Start("http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx?cs-lang=vb#remarksToggle")
        Catch ex As Exception
            If MsgBox("Unable to launch URL, copy to clipboard instead?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then Clipboard.SetText("http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx?cs-lang=vb#remarksToggle")
        End Try
    End Sub
    
    Sub btnHotkey_Click() Handles btnHotkey.Click
        If btnHotkey.Text = "Enable Hotkey (Ctrl)" Then
            btnHotkey.Text = "Hotkey Enabled!"
            timerKeyChecker.Interval = 1000
            timerKeyChecker.Start()
        ElseIf btnHotkey.Text = "Disable Hotkey (Ctrl)" Then
            btnHotkey.Text = "Hotkey Disabled!"
            timerKeyChecker.Interval = 1000
        End If
    End Sub
    
    Private Sub timerKeyChecker_Tick() Handles timerKeyChecker.Tick
        If btnHotkey.Text = "Hotkey Enabled!" Then
            btnHotkey.Text = "Disable Hotkey (Ctrl)"
            timerKeyChecker.Interval = 100
        ElseIf btnHotkey.Text = "Hotkey Disabled!" Then
            btnHotkey.Text = "Enable Hotkey (Ctrl)"
            timerKeyChecker.Stop()
        End If
        If My.Computer.Keyboard.CtrlKeyDown = True Then
            If lblStatus.Text = "Not Running" Then
                bwKeyInserter.RunWorkerAsync
            Else
                bwKeyInserter.CancelAsync
            End If
            MsgBox("Started")
        End If
    End Sub
    
    Sub bwKeyInserter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwKeyInserter.DoWork
        If e.Cancel Then
            lblStatus.Text = "Canceling..."
        Else
            lblStatus.Text = "Running..."
            For Each item As ListViewItem In lstKeyStrokes.Items
                If lblStatus.Text <> "Canceling..." Then
                    lblStatus.Text = "Running: " & item.Index
                    MsgBox(item.SubItems.Item(0).Text)
                    SendKeys.Send(item.SubItems.Item(0).Text)
                    MsgBox(item.SubItems.Item(1).Text)
                    Threading.Thread.Sleep(item.SubItems.Item(1).Text)
                End If
            Next
            lblStatus.Text = "Not Running"
        End If
    End Sub
End Class
