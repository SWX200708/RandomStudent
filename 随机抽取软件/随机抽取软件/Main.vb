Public Class Main

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        If MsgBox("确定退出吗？", vbYesNoCancel + vbQuestion) = vbYes Then
            End
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        NameList.Show()
        Me.Enabled = False
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        NameList.Show()
        NameList.NameList1.Focus()
        Me.Enabled = False
    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            MkDir("D:/QZEZ Software/随机抽取软件")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        names.Clear()
        MsgBox("当前名单已被清空！", vbInformation)
        NL.Text = ""
    End Sub

    Private Sub ToolStripStatusLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel2.Click
        Dim res As String = ""
        For i = 0 To names.Count() - 1
            res += Str(i) + ":   " + names(i) + vbCrLf
        Next
        MsgBox(res)
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Label1.Text = names(Int(New Random().Next(0, range)))
    End Sub

    Private Sub BTN_BEGIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_BEGIN.Click
        range = names.Count()
        Debug.Print(range)
        If range = 0 Or range = 1 Then
            MsgBox("名单异常！", vbCritical)
            range = 0
            Exit Sub
        End If
        Timer1.Enabled = True
    End Sub

    Private Sub BTN_END_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTN_END.Click
        Timer1.Enabled = False
    End Sub
End Class
