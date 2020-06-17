Imports System.Data.Odbc
Public Class formjabatan
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dataakun.Show()
        Me.Hide()
    End Sub
    Sub reset()
        btninput.Enabled = True
        btnedit.Enabled = False
        txtnama.Text = ""
        'txtguru1.Text.Trim = ""
        txtpokok.Text = "0"
        txtjamngajar.Text = "0"
        txtjabatan.Text = "0"
        txttunguru.Text = "0"
        txtid.Text = ""
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Golongan.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub formjabatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”) ' hari
    End Sub

    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        If txtid.Text = "" Or txtnama.Text = "" Or txtguru.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Form Jabatan", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()
            Try
                Dim Sql As String = "Insert into tjabatan (kode_jabatan,nama_jabatan,guru,walikelas,gaji_pokok,gaji_jam_ngajar,tunjangan_jabatan,tunjangan_guru) values (?,?,?,?,?,?,?,?)"
                Dim mycomm As OdbcCommand = New OdbcCommand(Sql, CONN)
                With mycomm.Parameters
                    .Add("?", OdbcType.VarChar, 10).Value = txtid.Text.Trim
                    .Add("?", OdbcType.VarChar, 50).Value = txtnama.Text
                    .Add("?", OdbcType.VarChar, 30).Value = txtguru1.Text.Trim
                    .Add("?", OdbcType.VarChar, 25).Value = "0"
                    .Add("?", OdbcType.VarChar, 25).Value = txtpokok.Text
                    .Add("?", OdbcType.VarChar, 25).Value = txtjamngajar.Text
                    .Add("?", OdbcType.VarChar, 25).Value = txtjabatan.Text
                    .Add("?", OdbcType.VarChar, 25).Value = txttunguru.Text
                End With
                mycomm.ExecuteNonQuery()
                mycomm = Nothing
                MsgBox("Simpan Data jabatan ke database berhasil", MsgBoxStyle.MsgBoxSetForeground, "Simpan")
                dataakun.Show()
                Me.Hide()
                Call dataakun.TampilGrid()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        If txtid.Text = "" Or txtnama.Text = "" Or txtguru.Text = "" Or txtpokok.Text = "" Or txtjamngajar.Text = "" Then
            MessageBox.Show("Silahkan Isi Semua From", "Pendataan Konsumen", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            Call koneksi()
            Dim edit As String = "update tjabatan set nama_jabatan='" & txtnama.Text & "',guru='" & txtguru1.Text & "',gaji_pokok ='" & txtpokok.Text & "',gaji_jam_ngajar='" & txtjamngajar.Text & "',tunjangan_jabatan='" & txtjabatan.Text & "',tunjangan_guru='" & txttunguru.Text & "'where kode_jabatan='" & txtid.Text & "'"
            CMD = New OdbcCommand(edit, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Data Berhasil Di UPDATE")
            jabatan.Show()
            Me.Hide()
            'Call kosongkandata()
            'Call refreshDatagrid1()
        End If
    End Sub

    Private Sub txtguru_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtguru.SelectedIndexChanged
        If txtguru.Text = "Ya" Then
            txtjamngajar.ReadOnly = False
            txttunguru.ReadOnly = False
            txtguru1.Text = "1"
        Else
            txtjamngajar.ReadOnly = True
            txtjamngajar.Text = "0"
            txttunguru.ReadOnly = True
            txttunguru.Text = "0"
            txtguru1.Text = "0"
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call reset()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call reset()
        jabatan.Show()
        Me.Hide()
    End Sub
End Class