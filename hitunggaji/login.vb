Imports System.Data.Odbc
Public Class login
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String

    'script koneksi dbpendataan
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=db_hitgaji;server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub


    Private Sub txtpw_MouseClick(sender As Object, e As MouseEventArgs) Handles txtpw.MouseClick
        If txtpw.Text = "Password" Then
            txtpw.Text = ""
            txtpw.PasswordChar = "*"
            txtpw.MaxLength = 30
        End If
    End Sub

    Private Sub txtpw_Leave(sender As Object, e As EventArgs) Handles txtpw.Leave
        If txtpw.Text = "" Then
            txtpw.Text = "Password"
            txtpw.PasswordChar = ""
        End If
    End Sub
    Private Sub txtpw_KeyDown(sender As Object, e As KeyEventArgs) Handles txtpw.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1.PerformClick()
        End If
    End Sub

    Private Sub txtuser_MouseClick(sender As Object, e As MouseEventArgs) Handles txtuser.MouseClick
        If txtuser.Text = "Username" Then
            txtuser.Text = ""
            txtuser.MaxLength = 20
        End If
    End Sub

    Private Sub txtuser_Leave(sender As Object, e As EventArgs) Handles txtuser.Leave
        If txtuser.Text = "" Then
            txtuser.Text = "Username"
        End If
    End Sub
    Private Sub txtpw_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtpw.KeyPress
        txtpw.PasswordChar = "*"
    End Sub

    'script login dan hak akses
    Private Sub button1_click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi()
        Dim da As New OdbcDataAdapter
        Dim login = "select * from tlogin where username='" + txtuser.Text + "' and password='" + txtpw.Text + "'"
        Dim cmd As New OdbcCommand
        cmd.Connection = CONN
        cmd.CommandText = login
        da.SelectCommand = cmd
        Dim rd As OdbcDataReader
        rd = cmd.ExecuteReader()
        If rd.HasRows = 0 Then
            MsgBox("Username dan Password salah!! ",
                   MsgBoxStyle.Exclamation, "error login")
        Else
            CONN.Close()
            Call koneksi()
            cmd = New OdbcCommand("select * from tlogin where username='" + txtuser.Text + "' and password='" + txtpw.Text + "'", CONN)
            MsgBox("Login berhasil, Selamat datang " & txtuser.Text & "!", MsgBoxStyle.Information, "successfull login")
            rd = cmd.ExecuteReader()
            rd.Read()
            'Label2.Text = rd.Item("gambar") 'buat dipanggil lagi jadi image location
            Label3.Text = rd.Item("level") 'buat meriksa admin
            'Form1.txtadmin.ImageLocation = rd.Item("gambar") 'untuk menentukan lokasi gambar pada txtadmin
            'inputdata.txtadmin.imagelocation = rd.Item("gambar")
            'data_konsumen.txtadmin.imagelocation = rd.Item("gambar")
            'Form1.Label1.Text = "selamat datang " & txtuser.Text
            Form1.show()
            Me.Hide()
            If Label3.Text = 1 Then 'meriksa admin apa bukan
            Else ' klo bukan admin
                Form1.PictureBox1.Visible = False
                'data_konsumen.picturebox1.visible = False
                'inputdata.picturebox1.visible = False
                Form1.btnuser.Visible = False
                data_guru.btnuser.Visible = False
                data_gaji.btnuser.Visible = False
                'inputdata.btnuser.visible = False
            End If
        End If
    End Sub
End Class
