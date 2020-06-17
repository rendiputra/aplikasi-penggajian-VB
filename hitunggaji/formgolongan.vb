Imports System.Data.Odbc
Public Class formgolongan
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
    Sub kosongkandata()
        txtid.Text = ""
        txtkeluarga.Text = ""
        txtasuransi.Text = ""
        txttransport.Text = ""


        txtsimpananwajib.Text = ""
    End Sub
    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        If txtid.Text = "" Or txtkeluarga.Text = "" Or txtasuransi.Text = "" Or txttransport.Text = "" Or txtsimpananwajib.Text = "" Then
            MsgBox("Silahkan Isi Semua Form")
        Else
            Call koneksi()
            Dim simpan As String = "insert into tgolongan values('" & txtid.Text & "','" & txtkeluarga.Text & "', '" & txtasuransi.Text & "','" & txttransport.Text & "','" & txtsimpananwajib.Text & "')"
            CMD = New OdbcCommand(simpan, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Input Da ta Telah Berhasil")
            Call kosongkandata()
            Golongan.Show()
            Me.Hide()
            Call Golongan.tampilgrid()
        End If
    End Sub
    Sub NomorOtomatis()
        Call koneksi()
        CMD = New OdbcCommand("select golongan from tgolongan order by golongan desc", CONN)
        RD = CMD.ExecuteReader
        RD.Read()
        If Not RD.HasRows Then
            txtid.Text = "001"
        Else
            txtid.Text = Format(Microsoft.VisualBasic.Right(RD.Item("golongan"), 3) + 1, "000")
        End If
    End Sub

    Private Sub formgolongan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Call NomorOtomatis()
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call kosongkandata()
        txtid.Enabled = False
        btnedit.Enabled = False
        btninput.Enabled = False
        Golongan.Show()
        Me.Hide()
    End Sub

    Private Sub btnedit_Click(sender As Object, e As EventArgs) Handles btnedit.Click
        Call koneksi()
        If txtid.Text = "" Or txtkeluarga.Text = "" Or txtasuransi.Text = "" Or txttransport.Text = "" Or txtsimpananwajib.Text = "" Then
            MsgBox("Silahkan Isi Semua Form")
        Else
            Dim edit As String = "update tgolongan set Tunjangan_keluarga='" & txtkeluarga.Text & "',Tunjangan_asuransi='" & txtasuransi.Text & "',Tunjangan_transport='" & txttransport.Text & "',pot_simpanan_wajib='" & txtsimpananwajib.Text & "' where Golongan='" & txtid.Text & "'"
            CMD = New OdbcCommand(edit, CONN)
            CMD.ExecuteNonQuery()
            MsgBox("Edit Data Berhasil")
            Call kosongkandata()
            Golongan.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call kosongkandata()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Call kosongkandata()
    End Sub
End Class