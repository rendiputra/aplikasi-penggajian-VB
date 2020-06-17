Imports System.Data.Odbc
Public Class Form1
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    'membuat koneksi ke database "dbpendataan"
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=db_hitgaji;server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub

    'nampilin datagrid
    Sub tampilgrid()
        Call koneksi()
        DA = New OdbcDataAdapter("select NPK,tanggal,Periode,nama,jabatan, pendapatan, potongan, gaji_bersih from tpenggajian", CONN)
        DS = New DataSet
        DA.Fill(DS, "tpenggajian")
        DataGridView1.DataSource = DS.Tables("tpenggajian")
        DataGridView1.ReadOnly = True
    End Sub
    'proses cari data berdasarkan id, NPK, Nama, alamat, Tanggal
    Sub cari()
        Call koneksi()
        DA = New OdbcDataAdapter("Select  NPK,tanggal,Periode,nama,jabatan, pendapatan, potongan, gaji_bersih from tpenggajian Where npk like '%" & txtcari.Text & "%' OR nama like '%" & txtcari.Text & "%' OR tanggal like '%" & txtcari.Text & "%' OR periode like '%" & txtcari.Text & "%' OR jabatan like '%" & txtcari.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tpenggajian")
        DataGridView1.DataSource = (DS.Tables("tpenggajian"))
        Dim tampil1 As String
        tampil1 = "select * from tpenggajian where id_gaji ='" & txtcari.Text & "'"
        CMD = New OdbcCommand(tampil1, CONN)
        RD = CMD.ExecuteReader
    End Sub
    'mentata datagrid
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.SteelBlue
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.SteelBlue
        'DataGridView1.Columns(0).Visible = False
        'DataGridView1.Columns(1).Width = 41
        'DataGridView1.Columns(2).Width = 50
        'DataGridView1.Columns(3).Width = 50
        'DataGridView1.Columns(4).Visible = False
        'DataGridView1.Columns(5).Visible = False
        'DataGridView1.Columns(6).Visible = False
        'DataGridView1.Columns(7).Visible = False
        'DataGridView1.Columns(8).Visible = False
        'DataGridView1.Columns(9).Visible = False
        'DataGridView1.Columns(10).Visible = False
        'DataGridView1.Columns(11).Visible = False
        'DataGridView1.Columns(12).Visible = False
        'DataGridView1.Columns(13).Visible = False
        'DataGridView1.Columns(14).Visible = False
        'DataGridView1.Columns(15).Visible = False
        'DataGridView1.Columns(16).Visible = False
        'DataGridView1.Columns(17).Visible = False
        'DataGridView1.Columns(18).Visible = False
        'DataGridView1.Columns(19).Visible = False
        'DataGridView1.Columns(20).Visible = False
        'DataGridView1.Columns(21).Width = 100
        'DataGridView1.Columns(22).Width = 120
        'DataGridView1.Columns(23).Width = 95
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilgrid()

        'Me.WindowState = FormWindowState.Maximized
        'Call TampilGrid1()
        Call aturDGV()
        'inputdata.txttanggal.Text = ""
        Timer1.Enabled = True
        'Label1.Text = "Selamat datang " & login.txtuser.Text
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        data_guru.Show()
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”) ' hari
        'Label4.Text = Format(Now, “HH:mm:ss”) ' jam
    End Sub

    Private Sub btnuser_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtcari_MouseClick(sender As Object, e As MouseEventArgs) Handles txtcari.MouseClick
        If txtcari.Text = "Search" Then
            txtcari.Text = ""
            txtcari.TextAlign = HorizontalAlignment.Left
        End If
    End Sub

    Private Sub txtcari_Leave(sender As Object, e As EventArgs) Handles txtcari.Leave
        If txtcari.Text = "" Then
            txtcari.Text = "Search"
            txtcari.TextAlign = HorizontalAlignment.Center
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MessageBox.Show("Apakah Anda Ingin Sign Out?", "Konfirmasi Keluar Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            login.txtuser.Text = "Username"
            login.txtpw.Text = "Password"
            login.txtuser.Focus()
            login.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub btnuser_Click_1(sender As Object, e As EventArgs) Handles btnuser.Click
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        data_gaji.Show()
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

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub
End Class