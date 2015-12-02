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
        If e.Data.GetDataPresent(DataFormats.Text) Then
            e.Effect = DragDropEffects.All
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub
    
    Sub lstKeyStrokes_DragDrop(sender As Object, e As DragEventArgs) Handles lstKeyStrokes.DragDrop
        If e.Data.GetDataPresent(DataFormats.Text) Then
            Dim tmpListViewItem As New ListViewItem(New String() {e.Data.GetData(DataFormats.Text).ToString, "100"})
            lstKeyStrokes.FocusedItem = lstKeyStrokes.Items.Add(tmpListViewItem)
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
        Dim tmpListViewItem As New ListViewItem(New String() {"{ENTER}", "100"})
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
    
    Sub btnStart_Click() Handles btnStart.Click
        bwKeyInserter.RunWorkerAsync
    End Sub
    
    Sub bwKeyInserter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwKeyInserter.DoWork
        If e.Cancel Then
            lblStatus.Text = "Cancelling..."
        Else
            lblStatus.Text = "Running..."
            Do Until My.Computer.Keyboard.CtrlKeyDown
                For Each item As ListViewItem In lstKeyStrokes.Items
                    If Not My.Computer.Keyboard.CtrlKeyDown Then
                        lblStatus.Text = "Running: " & item.Index
                        bwKeyInserter.ReportProgress(item.Index) ' workaround for background workers not being able to interact with the UI
                        lblStatus.Text = "Waiting: " & item.SubItems.Item(1).Text
                        Threading.Thread.Sleep(item.SubItems.Item(1).Text)
                    End If
                Next
            Loop
            lblStatus.Text = "Not Running"
        End If
    End Sub
    
    Sub BwKeyInserter_ProgressChanged(sender As Object, e As System.ComponentModel.ProgressChangedEventArgs) Handles bwKeyInserter.ProgressChanged
        Dim keys As String
        keys = lstKeyStrokes.Items.Item(e.ProgressPercentage).Text
        SendKeys.Send(keys)
    End Sub
End Class
