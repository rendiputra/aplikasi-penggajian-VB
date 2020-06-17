Imports System.Data.Odbc
Imports System.Windows.Forms
Public Class form_gaji
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
    Sub NomorOtomatis()
        Call koneksi()
        cmd = New OdbcCommand("select * from tpenggajian order by id_gaji desc", CONN)
        RD = cmd.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            txtnomor.Text = "0000000001"
        Else
            txtnomor.Text = Format(Microsoft.VisualBasic.Right(RD.Item("id_gaji"), 10) + 1, "0000000000")
        End If
    End Sub
    Sub reset()
        btninput.Enabled = True
        btnedit.Enabled = False
        Call NomorOtomatis()
        txtnpk.Text = ""
        txtgolongan.Text = ""
        txtjabatan.Text = ""
        txttgl.Text = ""
        txtbulan.Text = ""
        txttahun.Text = ""
        txtnama.Text = ""
        txtjabatan2.Text = ""
        txtpokok.Text = "0"
        txtperjam.Text = "0"
        txttransport.Text = "0"
        txtjabatan3.Text = "0"
        txtguru.Text = "0"
        txtkeluarga.Text = "0"
        txtasuransi.Text = "0"
        txtpotppn.Text = "0"
        txtpotdansos.Text = "0"
        txtpotinfaq.Text = "0"
        'txtpotwajib.Text = "0"
        txtjmlpendapatan.Text = "0"
        txtjmlpot.Text = "0"
        txtgajibersih.Text = "0"
        txteditperiode.Text = ""
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MessageBox.Show("Apakah Anda Ingin Sign Out?", "Konfirmasi Keluar Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            Call reset()
            login.txtuser.Text = "Username"
            login.txtpw.Text = "Password"
            login.txtuser.Focus()
            login.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”) ' hari
        txttgl.Text = Format(Now, "dd MMMMM yyyy") ' txt
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        tabeljabatan.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        tabelgolongan.Show()
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txtjabatan3.TextChanged

    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles Label15.Click

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        tabelkaryawan.Show()
        'Me.Hide()
    End Sub

    Private Sub txtnpk_TextChanged(sender As Object, e As EventArgs) Handles txtnpk.TextChanged
        Call koneksi()
        txtnpk.Text = UCase(txtnpk.Text)
        cmd = New OdbcCommand("select * from tkaryawan where NPK='" & txtnpk.Text & "'", CONN)
        RD = cmd.ExecuteReader
        RD.Read()
        If RD.HasRows Then
            txtnama.Text = RD.Item(1)
            txtgolongan.Text = RD.Item(8)
            txtjabatan.Text = RD.Item(9)

            'jabatan
            cmd = New OdbcCommand("select * from tjabatan where kode_jabatan='" & txtjabatan.Text & "'", CONN)
            RD = cmd.ExecuteReader
            RD.Read()
            If RD.HasRows Then
                txtjabatan2.Text = RD.Item("Nama_jabatan")
                txtguru.Text = RD.Item("Tunjangan_guru")
                lblperjam.Text = RD.Item(5)
                txtpokok.Text = RD.Item(4)

                'golongan
                cmd = New OdbcCommand("select * from tgolongan where golongan='" & txtgolongan.Text & "'", CONN)
                RD = cmd.ExecuteReader
                RD.Read()
                If RD.HasRows Then
                    'txtgolongan.Text = RD.Item(0)
                    txtkeluarga.Text = RD.Item(1)
                    'txttunjangananak.Text = RD.Item(2)
                    txttransport.Text = RD.Item(3)
                    'txtuangmakan.Text = RD.Item(4)
                    'txtuanglembur.Text = RD.Item(5)
                    'txtasuransikesehatan.Text = RD.Item(6)
                End If

                Call hitung()
            End If
        End If

    End Sub

    Private Sub txtnpk_LostFocus(sender As Object, e As EventArgs) Handles txtnpk.LostFocus

    End Sub

    Private Sub form_gaji_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call NomorOtomatis()
    End Sub
    Sub hitung()
        txtjmlpendapatan.Text = Val(txtpokok.Text) + Val(txtperjam.Text) + Val(txttransport.Text) + Val(txtjabatan3.Text) + Val(txtguru.Text) + Val(txtguru.Text) + Val(txtasuransi.Text)
        txtpotppn.Text = txtjmlpendapatan.Text * 0.1
        txtjmlpot.Text = Val(txtpotdansos.Text) + Val(txtpotinfaq.Text) + Val(txtpotppn.Text) + Val(txtpotwajib.Text)
        txtgajibersih.Text = Val(txtjmlpendapatan.Text) - Val(txtjmlpot.Text)
    End Sub
    Private Sub txtjam_TextChanged(sender As Object, e As EventArgs) Handles txtjam.TextChanged
        txtperjam.Text = Val(lblperjam.Text) * Val(txtjam.Text)
        txtjmlpendapatan.Text = Val(txtperjam.Text) + Val(txttransport.Text) + Val(txtjabatan3.Text) + Val(txtguru.Text) + Val(txtkeluarga.Text) + Val(txtasuransi.Text)
        Call hitung()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call reset()
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call reset()
        data_guru.Show()
        Me.Hide()
    End Sub

    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        If txtnpk.Text = "" Or txtnomor.Text = "" Or txtnama.Text = "" Or txtgolongan.Text = "" Or txtjabatan.Text = "" Or txteditperiode.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()

            Try
                Call koneksi()
                Dim da As New OdbcDataAdapter
                Dim cekperiode = "select * from tpenggajian where periode='" + txteditperiode.Text + "' and NPK='" & txtnpk.Text & "'"
                Dim cmd As New OdbcCommand
                cmd.Connection = CONN
                cmd.CommandText = cekperiode
                da.SelectCommand = cmd
                Dim rd As OdbcDataReader
                rd = cmd.ExecuteReader()
                If rd.HasRows = 0 Then
                    Dim Sql As String = "Insert into tpenggajian (id_gaji,NPK,tanggal,periode,nama,jabatan,gaji_pokok,gaji_jam,Tunjangan_Transport,Tunjangan_jabatan,Tunjangan_guru,Tunjangan_keluarga,Tunjangan_asuransi,Pot_PPN,Pot_Dansos, pot_infaq, pot_simpanan_wajib, pendapatan, potongan, gaji_bersih) values (? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? ,? )"
                    Dim mycomm As OdbcCommand = New OdbcCommand(Sql, CONN)
                    With mycomm.Parameters
                        .Add("?", OdbcType.VarChar, 10).Value = txtnomor.Text.Trim
                        .Add("?", OdbcType.VarChar, 20).Value = txtnpk.Text.Trim
                        .Add("?", OdbcType.VarChar, 30).Value = txttgl.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txteditperiode.Text
                        .Add("?", OdbcType.VarChar, 1000).Value = txtnama.Text
                        .Add("?", OdbcType.VarChar, 1000).Value = txtjabatan2.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpokok.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtperjam.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txttransport.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtjabatan3.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtguru.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtkeluarga.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtasuransi.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotppn.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotdansos.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotinfaq.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotwajib.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtjmlpendapatan.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtjmlpot.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtgajibersih.Text
                    End With

                    mycomm.ExecuteNonQuery()
                    mycomm = Nothing
                    MsgBox("Input data gaji berhasil", MsgBoxStyle.MsgBoxSetForeground, "Input Sukses")
                    data_gaji.Show()
                    Me.Hide()
                    Call data_gaji.TampilGrid()
                Else
                    MsgBox("Anda telah menginput ('" + txtnama.Text + "') pada periode : (" + txteditperiode.Text + ")",
                    MsgBoxStyle.Exclamation, "Invalid Input")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If txtnpk.Text = "" Or txtnomor.Text = "" Or txtnama.Text = "" Or txtgolongan.Text = "" Or txtjabatan.Text = "" Or txteditperiode.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()
            Try
                Call koneksi()
                Dim da As New OdbcDataAdapter
                Dim cekperiode = "select * from tpenggajian where periode='" + txteditperiode.Text + "' and NPK='" & txtnpk.Text & "'"
                Dim cmd As New OdbcCommand
                cmd.Connection = CONN
                cmd.CommandText = cekperiode
                da.SelectCommand = cmd
                Dim rd As OdbcDataReader
                rd = cmd.ExecuteReader()
                If rd.HasRows = 0 Then
                    Dim Sql As String = "update tpenggajian set NPK = ?, tanggal = ?, periode = ?, nama = ?,jabatan = ?, gaji_pokok = ?, gaji_jam = ?, Tunjangan_Transport = ? , Tunjangan_jabatan = ?, Tunjangan_guru = ?, Tunjangan_keluarga = ?, Tunjangan_asuransi = ?, Pot_PPN = ?,Pot_Dansos = ?, pot_infaq = ?, pot_simpanan_wajib = ?, pendapatan = ?, potongan = ?, gaji_bersih = ? where id_gaji = ?"
                    Dim mycomm As OdbcCommand = New OdbcCommand(Sql, CONN)
                    With mycomm.Parameters
                        .Add("?", OdbcType.VarChar, 20).Value = txtnpk.Text.Trim
                        .Add("?", OdbcType.VarChar, 30).Value = txttgl.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txteditperiode.Text
                        .Add("?", OdbcType.VarChar, 1000).Value = txtnama.Text
                        .Add("?", OdbcType.VarChar, 1000).Value = txtjabatan2.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpokok.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtperjam.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txttransport.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtjabatan3.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtguru.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtkeluarga.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtasuransi.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotppn.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotdansos.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotinfaq.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtpotwajib.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtjmlpendapatan.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtjmlpot.Text
                        .Add("?", OdbcType.VarChar, 30).Value = txtgajibersih.Text
                        .Add("?", OdbcType.VarChar, 10).Value = txtnomor.Text.Trim
                    End With
                    mycomm.ExecuteNonQuery()
                    mycomm = Nothing
                    MsgBox("Update data gaji berhasil", MsgBoxStyle.MsgBoxSetForeground, "Edit Sukses")
                    data_gaji.Show()
                    Me.Hide()
                    Call data_gaji.TampilGrid()
                Else
                    MsgBox("Anda telah menginput ('" + txtnama.Text + "') pada periode : (" + txteditperiode.Text + ")",
                    MsgBoxStyle.Exclamation, "Invalid Input")
                End If
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Call reset()
        data_gaji.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call reset()
    End Sub

    Private Sub txtpotinfaq_TextChanged(sender As Object, e As EventArgs) Handles txtpotinfaq.TextChanged
        Call hitung()
    End Sub

    Private Sub txtbulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtbulan.SelectedIndexChanged
        txteditperiode.Text = txtbulan.Text + " " + txttahun.Text
        If txtbulan.Text = "Januari" Then
            txtcekbulan.Text = "1"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
            'formatcek.Text = Format(txtcekperiode, "MM yyyy")
        ElseIf txtbulan.Text = "Februari" Then
            txtcekbulan.Text = "2"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "Maret" Then
            txtcekbulan.Text = "3"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "April" Then
            txtcekbulan.Text = "4"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "Mei" Then
            txtcekbulan.Text = "5"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "Juni" Then
            txtcekbulan.Text = "6"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "Juli" Then
            txtcekbulan.Text = "7"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "Agustus" Then
            txtcekbulan.Text = "8"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "September" Then
            txtcekbulan.Text = "9"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "Oktober" Then
            txtcekbulan.Text = "10"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "November" Then
            txtcekbulan.Text = "11"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txtbulan.Text = "Desember" Then
            txtcekbulan.Text = "12"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        End If
    End Sub

    Private Sub txttahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txttahun.SelectedIndexChanged
        txteditperiode.Text = txtbulan.Text + " " + txttahun.Text
        If txttahun.Text = "2019" Then
            txtcektahun.Text = "2019"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txttahun.Text = "2020" Then
            txtcektahun.Text = "2020"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text
        ElseIf txttahun.Text = "2021" Then
            txtcektahun.Text = "2021"
            txtcekperiode.Text = txtcektahun.Text + txtcekbulan.Text


        End If
    End Sub

    Private Sub btnuser_Click(sender As Object, e As EventArgs) Handles btnuser.Click
        Call reset()
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub formatcek_Click(sender As Object, e As EventArgs) Handles formatcek.Click

    End Sub

    Private Sub txtcekperiode_Click(sender As Object, e As EventArgs) Handles txtcekperiode.Click

    End Sub
End Class