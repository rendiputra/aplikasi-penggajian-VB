Imports System.Data.Odbc
Public Class Golongan
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=db_hitgaji;server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”)
    End Sub
    Sub tampilgrid()
        Call koneksi()
        DA = New OdbcDataAdapter("select * from tgolongan", CONN)
        DS = New DataSet
        DA.Fill(DS, "tgolongan")
        DataGridView1.DataSource = DS.Tables("tgolongan")
        DataGridView1.ReadOnly = True
    End Sub
    Sub cari()
        Call koneksi()
        DA = New OdbcDataAdapter("Select * from tgolongan Where golongan like '%" & txtcari.Text & "%'", CONN)
        DS = New DataSet
        DS.Clear()
        DA.Fill(DS, "tgolongan")
        DataGridView1.DataSource = (DS.Tables("tgolongan"))
        'Dim tampil1 As String
        'tampil1 = "select * from tgolongan where golongan ='" & txtcari.Text & "'"
        'CMD = New OdbcCommand(tampil1, CONN)
        'RD = CMD.ExecuteReader
    End Sub
    Sub tampiltextbox()
        CONN.Close()
        Call koneksi()
        CMD = New OdbcCommand("SELECT * FROM tgolongan WHERE golongan='" & txtid.Text & "'", CONN)
        RD = CMD.ExecuteReader()
        RD.Read()
        If RD.HasRows Then
            formgolongan.txtid.Text = RD.Item("Golongan")
            formgolongan.txtkeluarga.Text = RD.Item("Tunjangan_keluarga")
            formgolongan.txtasuransi.Text = RD.Item("Tunjangan_asuransi")
            formgolongan.txttransport.Text = RD.Item("Tunjangan_transport")
            'formgolongan.txtliburtrans.Text = RD.Item("Transport_libur")
            ' formgolongan.txtdanakolektif.Text = RD.Item("pot_dana_kolektif")
            formgolongan.txtsimpananwajib.Text = RD.Item("pot_simpanan_wajib")
        End If
    End Sub
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.DarkCyan
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkCyan
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If formgolongan.txtid.Text = txtid.Text Or txtid.Text = formgolongan.txtid.Text Then
            formgolongan.btninput.Enabled = True
            formgolongan.btnedit.Enabled = False
        Else
            formgolongan.btnedit.Enabled = True
            formgolongan.btninput.Enabled = False
        End If
        If txtid.Text = "" Then
            MsgBox("Silahkan Masukan ID Yang Akan DIEDIT!")
        Else
            formgolongan.txtid.Enabled = False
            formgolongan.txtid.Text = txtid.Text
            Call tampiltextbox()
            formgolongan.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call formgolongan.NomorOtomatis()
        formgolongan.txtid.Enabled = False
        formgolongan.btnedit.Enabled = False
        formgolongan.btninput.Enabled = True
        formgolongan.Show()
        Me.Hide()
    End Sub

    Private Sub Golongan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilgrid()
        Call aturDGV()
    End Sub

    Private Sub txtcari_TextChanged(sender As Object, e As EventArgs) Handles txtcari.TextChanged
        Call cari()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Call kosongkandata()
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If txtid.Text = "" Then
            MsgBox("Silahkan Pilih Data Yang Akan DIHAPUS dengan masukkan ID")
        Else
            If MessageBox.Show("Apakah Ingin Menghapus Data?", "Konfirmasi Perubahan Data", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Call koneksi()
                Dim hapus As String = "delete from tgolongan where Golongan='" & txtid.Text & "'"
                CMD = New OdbcCommand(hapus, CONN)
                CMD.ExecuteNonQuery()
                Call tampilgrid()
                txtid.Text = ""
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        jabatan.Show()
        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtid.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
    End Sub
End Class