Imports System.Data.Odbc
Public Class data_gaji
    Dim CONN As OdbcConnection
    Dim cmd As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=db_hitgaji; server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub

    'proses auto complete pada Textbox apabila id sama dengan txtid"
    Sub tampiltextbox()
        CONN.Close()
        Call koneksi()
        cmd = New OdbcCommand("SELECT * FROM tpenggajian WHERE id_gaji='" & txtid.Text & "'", CONN)
        RD = cmd.ExecuteReader()
        RD.Read()
        If RD.HasRows Then
            form_gaji.txtnomor.Text = RD.Item("id_gaji")
            form_gaji.txtnpk.Text = RD.Item("NPK")
            form_gaji.txttgl.Text = RD.Item("tanggal")
            form_gaji.txteditperiode.Text = RD.Item("periode")
            form_gaji.txtnama.Text = RD.Item("nama")
            form_gaji.txtjabatan2.Text = RD.Item("jabatan")
            form_gaji.txtpokok.Text = RD.Item("gaji_pokok")
            form_gaji.txtperjam.Text = RD.Item("gaji_jam")
            form_gaji.txttransport.Text = RD.Item("tunjangan_transport")
            form_gaji.txtjabatan3.Text = RD.Item("tunjangan_jabatan")
            form_gaji.txtguru.Text = RD.Item("tunjangan_guru")
            form_gaji.txtkeluarga.Text = RD.Item("tunjangan_keluarga")
            form_gaji.txtasuransi.Text = RD.Item("tunjangan_asuransi")
            form_gaji.txtpotppn.Text = RD.Item("pot_PPN")
            form_gaji.txtpotdansos.Text = RD.Item("pot_dansos")
            form_gaji.txtpotinfaq.Text = RD.Item("pot_infaq")
            form_gaji.txtpotwajib.Text = RD.Item("pot_simpanan_wajib")
            form_gaji.txtjmlpendapatan.Text = RD.Item("pendapatan")
            form_gaji.txtjmlpot.Text = RD.Item("potongan")
            form_gaji.txtgajibersih.Text = RD.Item("gaji_bersih")
        End If
    End Sub
    Sub TampilGrid()
        Call koneksi()
        DA = New OdbcDataAdapter("select id_gaji,NPK,tanggal,Periode,nama,jabatan,gaji_pokok,gaji_jam,Tunjangan_Transport,Tunjangan_jabatan,Tunjangan_guru,Tunjangan_keluarga,Tunjangan_asuransi,Pot_PPN,Pot_Dansos, pot_infaq,  pendapatan, potongan, gaji_bersih from tpenggajian", CONN)
        DS = New DataSet
        DA.Fill(DS, "tpenggajian")
        DataGridView1.DataSource = DS.Tables("tpenggajian")
        DataGridView1.ReadOnly = True
    End Sub
    'Desain DGV
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.SteelBlue
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.SteelBlue
        'DataGridView1.Columns(0).Width = 60
        'DataGridView1.Columns(1).Width = 175
        'DataGridView1.Columns(2).Width = 165
        'DataGridView1.Columns(3).Width = 160
        'DataGridView1.Columns(4).Width = 140
        'DataGridView1.Columns(5).Width = 105
        'DataGridView1.Columns(6).Width = 100
        'DataGridView1.Columns(7).Width = 100
        'DataGridView1.Columns(8).Width = 100
        'DataGridView1.Columns(9).Width = 100
        'DataGridView1.Columns(10).Width = 100
    End Sub
    'proses cari data berdasarkan id, NPK, Nama, alamat, Tanggal
    Sub cari()
        Call koneksi()
        DA = New OdbcDataAdapter("Select * from tpenggajian Where id_gaji like '%" & txtcari.Text & "%' OR nama like '%" & txtcari.Text & "%' OR NPK like '%" & txtcari.Text & "%' OR tanggal like '%" & txtcari.Text & "%' OR jabatan like '%" & txtcari.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tpenggajian")
        DataGridView1.DataSource = (DS.Tables("tpenggajian"))
        Dim tampil1 As String
        tampil1 = "select * from tpenggajian where id_gaji ='" & txtcari.Text & "'"
        cmd = New OdbcCommand(tampil1, CONN)
        RD = cmd.ExecuteReader
    End Sub
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtid.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        'DataGridView1.Rows(e.RowIndex).Cells(0).Value = ""
    End Sub
    Private Sub btnuser_Click(sender As Object, e As EventArgs) Handles btnuser.Click
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        form_gaji.Show()
        Me.Hide()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, "dddd, dd MMMMM yyyy")
    End Sub

    Private Sub data_gaji_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call TampilGrid()
        Call aturDGV()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        data_guru.Show()
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

    Private Sub txtcari_TextChanged(sender As Object, e As EventArgs) Handles txtcari.TextChanged
        Call cari()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If form_gaji.txtnomor.Text = txtid.Text Or txtid.Text = form_gaji.txtnomor.Text Then
            form_gaji.btninput.Enabled = True
            form_gaji.btnedit.Enabled = False
        Else
            form_gaji.btnedit.Enabled = True
            form_gaji.btninput.Enabled = False
        End If
        If txtid.Text = "" Then
            MsgBox("Silahkan Masukan ID Yang Akan DIEDIT!")
        Else
            'form_gaji.txtnomor.Enabled = False
            form_gaji.txtnomor.Text = txtid.Text
            Call tampiltextbox()
            form_gaji.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub btncari_Click(sender As Object, e As EventArgs) Handles btncari.Click

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        crformdatagaji.Show()
        Me.Hide()
    End Sub
End Class