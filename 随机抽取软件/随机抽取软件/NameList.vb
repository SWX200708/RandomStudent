Imports System.IO
Imports System.Text

Public Class NameList
    Private Sub NameList_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        For Each file In New DirectoryInfo("D:/QZEZ Software/随机抽取软件").GetFiles()
            NameList1.Items.Add(file.Name)
        Next

    End Sub

    Private Sub Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Edit.Click
        If NameList1.SelectedItem = Nothing Then
            MsgBox("未选择修改的名单！", vbExclamation)
            Statu.Text = "警告：未选择修改的名单！"
            Exit Sub
        Else
            NameList1.Enabled = False
            Statu.Text = "正在修改学生名单"
        End If
        TextBox1.Enabled = True
        Choose.Enabled = False
        Delete.Enabled = False
        Newone.Enabled = False
        Changeok.Enabled = True
        Giveup.Enabled = True
    End Sub

    Private Sub Changeok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Changeok.Click
        Dim NAmess As String = TextBox1.Text
        Dim path As String = "D:/QZEZ Software/随机抽取软件/" + NameList1.SelectedItem
        Using writer As New StreamWriter(path, False, Encoding.UTF8)
            writer.WriteLine(NAmess)
        End Using
        Statu.Text = "修改已经存盘！"
        NameList1.Enabled = True
        TextBox1.Enabled = False
        Choose.Enabled = True
        Delete.Enabled = True
        Newone.Enabled = True
        Changeok.Enabled = False
        Giveup.Enabled = False
    End Sub

    Private Sub NameList1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NameList1.SelectedIndexChanged
        Try
            Dim path As String = "D:/QZEZ Software/随机抽取软件/" + NameList1.SelectedItem

            Dim content As String = File.ReadAllText(path, System.Text.Encoding.UTF8)
            TextBox1.Text = content '加载
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical + vbYes)
        End Try

    End Sub

    Private Sub Giveup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Giveup.Click
        Statu.Text = "警告：修改已被放弃！"
        TextBox1.Text = "<需要重新加载>"
        NameList1.Enabled = True
        TextBox1.Enabled = False
        Choose.Enabled = True
        Delete.Enabled = True
        Newone.Enabled = True
        Changeok.Enabled = False
        Giveup.Enabled = False
    End Sub

    Private Sub Newone_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Newone.Click
        Dim NameName As String
        NameName = InputBox("请输入新建名单的名称：", "新建名单")
        If Len(NameName) <> 0 Then
            For i = 0 To NameList1.Items.Count - 1 Step 1
                'Debug.Print(Replace(NameList1.Items(i), ".txt", ""))
                If NameName = Replace(NameList1.Items(i), ".txt", "") Then
                    MsgBox("名单名称重名！", vbCritical)
                    Statu.Text = "名单名称重名！"
                    Exit Sub
                End If
            Next
        Else
            MsgBox("名单名称不合法！", vbCritical)
            Statu.Text = "名单名称不合法！"
            Exit Sub
        End If

        Dim filePath As String = "D:/QZEZ Software/随机抽取软件/" + NameName + ".txt"

        ' 创建一个新的 UTF-8 编码的 StreamWriter 对象，以创建新文件
        Using writer As New StreamWriter(filePath, False, Encoding.UTF8)
            ' 不需要写入内容，仅用于创建一个新的 TXT 文件
        End Using
        NameList1.Items.Clear()

        MsgBox("名单添加成功！", vbInformation)
        Statu.Text = "名单添加成功！"

        For Each file In New DirectoryInfo("D:/QZEZ Software/随机抽取软件").GetFiles()
            NameList1.Items.Add(file.Name)
        Next

    End Sub

    Private Sub NameList_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosed
        Main.Enabled = True
    End Sub

    Private Sub NameList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete.Click
        If NameList1.SelectedItem = Nothing Then
            MsgBox("未选择删除的名单！", vbExclamation)
            Statu.Text = "警告：未选择删除的名单！"
            Exit Sub
        Else
            Statu.Text = "正在删除学生名单"
        End If

        If MsgBox("您真的要删除学生名单吗？", vbYesNo + vbQuestion) = vbYes Then
            If MsgBox("删除学生名单后，无法恢复，是否确定？", vbYesNo + vbExclamation) = vbYes Then
                Dim filePath As String = "D:/QZEZ Software/随机抽取软件/" + NameList1.SelectedItem
                If File.Exists(filePath) Then
                    Try
                        File.Delete(filePath)
                    Catch ex As Exception
                        MsgBox(ex.Message, vbCritical)
                        Statu.Text = "错误：" + ex.Message
                        Exit Sub
                    End Try
                Else
                    MsgBox("找不到该文件！", vbCritical)
                    Statu.Text = "错误：删除学生名单失败！"
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        MsgBox("删除成功！", vbExclamation)
        Statu.Text = "删除成功！"

        NameList1.Items.Clear()
        For Each file In New DirectoryInfo("D:/QZEZ Software/随机抽取软件").GetFiles()
            NameList1.Items.Add(file.Name)
        Next
    End Sub

    Private Sub Choose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Choose.Click
        names.Clear()
        Try
            For Each line As String In TextBox1.Lines
                If Len(line) <> 0 Then
                    names.Add(line)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
            Statu.Text = "错误：" + ex.Message
        End Try
        MsgBox("存取成功", vbInformation)
        Statu.Text = "存取成功！"
        Main.NL.Text = NameList1.SelectedItem
    End Sub
End Class