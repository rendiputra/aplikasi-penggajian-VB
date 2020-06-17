Imports System.Data.Odbc
Public Class data_guru
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    Private Sub data_guru_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call TampilGrid()
        Call aturDGV()
        Call refreshDatagrid()
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
        DA = New OdbcDataAdapter("select * from tkaryawan", CONN)
        DS = New DataSet
        DA.Fill(DS, "tkaryawan")
        DataGridView1.DataSource = DS.Tables("tkaryawan")
        DataGridView1.ReadOnly = True
    End Sub

    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.SteelBlue
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.SteelBlue
    End Sub
    'proses refresh dg1
    Sub refreshDatagrid()
        Try
            Call koneksi()
            DS = New DataSet
            DA = New OdbcDataAdapter("SELECT * FROM tkaryawan", CONN)
            DA.Fill(DS, "tkaryawan")
            DataGridView1.Columns(0).HeaderText = "NPK"
            DataGridView1.Columns(1).HeaderText = "Nama"
            DataGridView1.Columns(2).HeaderText = "Alamat"
            DataGridView1.Columns(3).HeaderText = "No Telp"
            DataGridView1.Columns(4).HeaderText = "Jenis Kelamin"
            DataGridView1.Columns(5).HeaderText = "Status"
            DataGridView1.Columns(6).HeaderText = "Jumlah Anak"
            DataGridView1.Columns(7).HeaderText = "E-Mail"
            DataGridView1.Columns(8).HeaderText = "Kode Golongan"
            DataGridView1.Columns(9).HeaderText = "Kode Jabatan"
            txtnpk.Text = ""
            Dim GridView As New DataView(DS.Tables("tkaryawan"))
            DataGridView1.DataSource = GridView
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Hide()
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call tambahkaryawan.kosongkandata()
        tambahkaryawan.txtnpk.Enabled = True
        tambahkaryawan.Show()
        Me.Hide()
    End Sub
    'proses auto complete pada Textbox apabila id sama dengan txtid"
    Sub tampiltextbox()
        CONN.Close()
        Call koneksi()
        CMD = New OdbcCommand("SELECT * FROM tkaryawan WHERE NPK='" & txtnpk.Text & "'", CONN)
        RD = CMD.ExecuteReader()
        RD.Read()
        If RD.HasRows Then
            tambahkaryawan.txtnama.Text = RD.Item("Nama")
            tambahkaryawan.txtalamat.Text = RD.Item("Alamat")
            tambahkaryawan.txttelp.Text = RD.Item("No_telp")
            tambahkaryawan.txtjk.Text = RD.Item("Jenis_kelamin")
            tambahkaryawan.txtstatus.Text = RD.Item("Status")
            tambahkaryawan.txtanak.Text = RD.Item("Jumlah_anak")
            tambahkaryawan.txtemail.Text = RD.Item("Email")
            tambahkaryawan.txtgolongan.Text = RD.Item("kode_golongan")
            tambahkaryawan.txtjabatan.Text = RD.Item("kode_jabatan")
        End If
    End Sub
    'proses pengiriman data
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If tambahkaryawan.txtnpk.Text = txtnpk.Text Or txtnpk.Text = tambahkaryawan.txtnpk.Text Then
            tambahkaryawan.btninput.Enabled = True
            tambahkaryawan.btnedit.Enabled = False
        Else
            tambahkaryawan.btnedit.Enabled = True
            tambahkaryawan.btninput.Enabled = False
        End If
        If txtnpk.Text = "" Then
            MsgBox("Silahkan Masukan NPK Yang Akan DIEDIT!")
        Else
            tambahkaryawan.txtnpk.Enabled = False
            tambahkaryawan.txtnpk.Text = txtnpk.Text
            Call tampiltextbox()
            tambahkaryawan.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If txtnpk.Text = "" Then
            MsgBox("Silahkan Pilih Data Yang Akan DIHAPUS dengan masukkan NPK")
        Else
            If MessageBox.Show("Apakah Ingin Menghapus Data?", "Konfirmasi Perubahan Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Call koneksi()
                Dim hapus As String = "delete from tkaryawan where NPK='" & txtnpk.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                Call TampilGrid()
                txtnpk.Text = ""
            End If
        End If
    End Sub
    Private Sub txtnip_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtnpk.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
            AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtnpk.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnuser.Click
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        data_gaji.Show()
        Me.Hide()
    End Sub

    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click
        Call cari()
    End Sub
    'proses cari data berdasarkan id, NPK, Nama, alamat
    Sub cari()
        Call koneksi()
        DA = New OdbcDataAdapter("Select  * from tkaryawan Where NPK like '%" & txtcari.Text & "%' OR nama like '%" & txtcari.Text & "%' OR alamat like '%" & txtcari.Text & "%' OR No_telp like '%" & txtcari.Text & "%' OR jenis_kelamin like '%" & txtcari.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tkaryawan")
        DataGridView1.DataSource = (DS.Tables("tkaryawan"))
        Dim tampil1 As String
        tampil1 = "select * from tkaryawan where NPK ='" & txtcari.Text & "'"
        CMD = New OdbcCommand(tampil1, CONN)
        RD = CMD.ExecuteReader
    End Sub

    Private Sub txtcari_KeyDown(sender As Object, e As KeyEventArgs) Handles txtcari.KeyDown
        If e.KeyCode = Keys.Enter Then
            btncari.PerformClick()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        crformkaryawan.Show()
        Me.Hide()
    End Sub

End Class