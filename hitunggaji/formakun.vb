Imports System.Data.Odbc
Imports System.IO
Imports System.Drawing.Bitmap
Public Class formakun
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    Private PathFile As String = Nothing
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=db_hitgaji; server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    Sub kosongkandata()
        txtid.Text = ""
        txtpw.Text = ""
        txtuser.Text = ""
        txtlevel.Text = ""
        lbllevel.Text = ""
    End Sub
    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        If txtpw.Text = "" Or txtuser.Text = "" Or lbllevel.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Hitung Gaji", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()
            Try
                Dim Sql As String = "Insert into tlogin (id_user,username,password,gambar,level) values (?,?,?,?,?)"
                Dim mycomm As OdbcCommand = New OdbcCommand(Sql, CONN)
                With mycomm.Parameters
                    .Add("?", OdbcType.VarChar, 10).Value = ""
                    .Add("?", OdbcType.VarChar, 20).Value = txtuser.Text.Trim
                    .Add("?", OdbcType.VarChar, 30).Value = txtpw.Text.Trim
                    '.Add("?", OdbcType.VarChar, 1000).Value = txtlink.Text.Trim
                    .Add("?", OdbcType.VarChar, 1000).Value = "D:\GALERI\Dokumen 9.8\DSC_0344.JPG"
                    .Add("?", OdbcType.TinyInt, 10).Value = lbllevel.Text.Trim
                End With
                mycomm.ExecuteNonQuery()
                mycomm = Nothing
                MsgBox("Simpan Data User ke database berhasil", MsgBoxStyle.MsgBoxSetForeground, "Simpan")
                dataakun.Show()
                Me.Hide()
                Call dataakun.TampilGrid()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub txtlevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtlevel.SelectedIndexChanged
        If txtlevel.Text = "Admin" Then
            lbllevel.Text = 1
        ElseIf txtlevel.Text = "User"
            lbllevel.Text = 2
        Else
            lbllevel.Text = ""
        End If
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub formakun_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call kosongkandata()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call kosongkandata()
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtpw.Text = ""
        txtuser.Text = ""
        lbllevel.Text = ""
        Golongan.Show()
        Me.Hide()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            txtpw.PasswordChar = "*"
        Else
            txtpw.PasswordChar = ""
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call kosongkandata()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If txtid.Text = "" Or txtpw.Text = "" Or txtuser.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()
            Dim edit As String = "update tlogin set username='" & txtuser.Text & "',password='" & txtpw.Text & "',gambar ='D:\GALERI\Dokumen 9.8\DSC_0344.JPG', level='" & lbllevel.Text & "'where id_user='" & txtid.Text & "'"
            CMD = New OdbcCommand(edit, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data Berhasil Di UPDATE")
            Call kosongkandata()
            dataakun.Show()
            Me.Hide()
            dataakun.TampilGrid()
        End If
    End Sub
End Class