Imports System.Data.Odbc
Public Class dataakun
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    Private Sub dataakun_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call TampilGrid()
        Call aturDGV()
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”)
    End Sub

    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=db_hitgaji; server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    Sub TampilGrid()
        Call koneksi()
        DA = New OdbcDataAdapter("select * from tlogin", CONN)
        DS = New DataSet
        DA.Fill(DS, "tlogin")
        DataGridView1.DataSource = DS.Tables("tlogin")
        DataGridView1.ReadOnly = True
    End Sub
    Sub cari()
        Call koneksi()
        DA = New OdbcDataAdapter("Select  * from tlogin Where id_user like '%" & txtcari.Text & "%' OR username like '%" & txtcari.Text & "%' OR password like '%" & txtcari.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tpenggajian")
        DataGridView1.DataSource = (DS.Tables("tlogin"))
        Dim tampil1 As String
        tampil1 = "select * from tlogin where id_user ='" & txtcari.Text & "'"
        CMD = New OdbcCommand(tampil1, CONN)
        RD = CMD.ExecuteReader
    End Sub
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.DarkCyan
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkCyan
        DataGridView1.Columns(3).Visible = False
    End Sub
    Sub tampiltextbox()
        CONN.Close()
        Call koneksi()
        CMD = New OdbcCommand("SELECT * FROM tlogin WHERE id_user='" & txtid.Text & "'", CONN)
        RD = CMD.ExecuteReader()
        RD.Read()
        If RD.HasRows Then
            formakun.txtid.Text = RD.Item("id_user")
            formakun.txtuser.Text = RD.Item("username")
            formakun.txtpw.Text = RD.Item("password")
            formakun.lbllevel.Text = RD.Item("level")
            If formakun.lbllevel.Text = "1" Then
                formakun.txtlevel.Text = "Admin"
            ElseIf formakun.lbllevel.Text = "2"
                formakun.txtlevel.Text = "User"
            End If

        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        formakun.txtid.Enabled = False
        formakun.btnedit.Enabled = False
        formakun.btninput.Enabled = True
        formakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If formakun.txtid.Text = txtid.Text Or txtid.Text = formakun.txtid.Text Then
            formakun.btninput.Enabled = True
            formakun.btnedit.Enabled = False
        Else
            formakun.btnedit.Enabled = True
            formakun.btninput.Enabled = False
        End If
        If txtid.Text = "" Then
            MsgBox("Silahkan Masukan ID Yang Akan DIEDIT!")
        Else
            formakun.txtid.Enabled = False
            formakun.txtid.Text = txtid.Text
            Call tampiltextbox()
            formakun.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Golongan.Show()
        Me.Hide()
    End Sub
    'delete
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If txtid.Text = "" Then
            MsgBox("Silahkan Pilih Data Yang Akan DIHAPUS dengan masukkan ID")
        Else
            If MessageBox.Show("Apakah Ingin Menghapus Data?", "Konfirmasi Perubahan Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Call koneksi()
                Dim hapus As String = "delete from tlogin where id_user='" & txtid.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                Call TampilGrid()
                txtid.Text = ""
            End If
        End If
    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        jabatan.Show()
        Me.Hide()
    End Sub

    Private Sub txtcari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcari.KeyDown
        If e.KeyCode = Keys.Enter Then
            btncari.PerformClick()
        End If
    End Sub

    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        Call cari()
    End Sub
End Class