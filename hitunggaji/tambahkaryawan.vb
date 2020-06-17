Imports System.Data.Odbc
Public Class tambahkaryawan
    Dim CONN As OdbcConnection
    Dim CMD As OdbcCommand
    Dim DS As New DataSet
    Dim DA As OdbcDataAdapter
    Dim RD As OdbcDataReader
    Dim LokasiData As String
    'membuat koneksi ke database "db_hitgaji"
    Sub koneksi()
        LokasiData = "Driver={MySQL ODBC 3.51 Driver}; Database=db_hitgaji;server=localhost; uid=root"
        CONN = New OdbcConnection(LokasiData)
        If CONN.State = ConnectionState.Closed Then
            CONN.Open()
        End If
    End Sub
    'tanggal
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”) ' hari
    End Sub
    'reset
    Sub kosongkandata()
        btninput.Enabled = True
        btnedit.Enabled = False
        txtnpk.Text = ""
        txtnama.Text = ""
        txtalamat.Text = ""
        txttelp.Text = ""
        txtjk.Text = ""
        txtanak.Text = ""
        txtemail.Text = ""
        txtgolongan.Text = ""
        txtjabatan.Text = ""
    End Sub

    Private Sub tambahkaryawan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'txttanggal.Value = Format(Now, "dd/MM/yyyy")
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
        Me.Hide()
    End Sub
    'keluar
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If MessageBox.Show("Apakah Anda Ingin Sign Out?", "Konfirmasi Keluar Aplikasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
            login.txtuser.Text = "Username"
            login.txtpw.Text = "Password"
            login.txtuser.Focus()
            login.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        tabelgolongan.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        tabeljabatan.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call kosongkandata()
    End Sub
    'tambah data
    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        Call koneksi()
        Dim Simpan As String = "insert into tkaryawan values ('" & txtnpk.Text & "', '" & txtnama.Text & "', '" & txtalamat.Text & "','" & txttelp.Text & "', '" & txtjk.Text & "', '" & txtstatus.Text & "','" & txtanak.Text & "','" & txtemail.Text & "', '" & txtgolongan.Text & "' ,'" & txtjabatan.Text & "')"
        CMD = New OdbcCommand(Simpan, CONN)
        CMD.ExecuteNonQuery()
        MsgBox("Input Data BERHASIL")
        Call kosongkandata()
        Call kosongkandata()
        Call data_guru.refreshDatagrid()
        data_guru.Show()
        Me.Hide()
    End Sub
    'edit
    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Call koneksi()
        Dim edit As String = "update tkaryawan set Nama='" & txtnama.Text & "',Alamat='" & txtalamat.Text & "',No_telp ='" & txttelp.Text & "',Jenis_kelamin='" & txtjk.Text & "',Status='" & txtstatus.Text & "',Jumlah_anak='" & txtanak.Text & "',Email='" & txtemail.Text & "',Kode_golongan='" & txtgolongan.Text & "',Kode_jabatan='" & txtjabatan.Text & "'where NPK='" & txtnpk.Text & "'"
        CMD = New OdbcCommand(edit, CONN)
        CMD.ExecuteNonQuery()
        MsgBox("Data Berhasil Di UPDATE")
        Call kosongkandata()
        Call data_guru.refreshDatagrid()
        data_guru.Show()
        Me.Hide()
    End Sub
    Private Sub txtstatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtstatus.SelectedIndexChanged
        If (txtstatus.Text = "Lajang") Then
            txtanak.Text = "0"
            txtanak.Enabled = False
        ElseIf (txtstatus.Text = "Menikah")
            txtanak.Enabled = True
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        data_guru.Show()
        Me.Hide()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Call kosongkandata()
        data_guru.txtnpk.Text = ""
        data_guru.Show()
        Me.Hide()
    End Sub
    'angka doang
    Private Sub txttelp_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txttelp.KeyPress
        If (e.KeyChar < "0" OrElse e.KeyChar > "9") _
            AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        data_gaji.Show()
        Me.Hide()
    End Sub
End Class