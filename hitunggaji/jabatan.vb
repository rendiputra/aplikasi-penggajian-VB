Imports System.Data.Odbc
Public Class jabatan
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

    Private Sub jabatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilgrid()
        Call aturDGV()
        Label3.Text = Format(Now, “dddd, dd MMMMM yyyy”)
    End Sub
    Sub tampiltextbox()
        CONN.Close()
        Call koneksi()
        CMD = New OdbcCommand("SELECT * FROM tjabatan WHERE kode_jabatan='" & txtid.Text & "'", CONN)
        RD = CMD.ExecuteReader()
        RD.Read()
        If RD.HasRows Then
            formjabatan.txtid.Text = RD.Item("kode_jabatan")
            formjabatan.txtnama.Text = RD.Item("nama_jabatan")
            formjabatan.txtguru.Text = RD.Item("guru")
            formjabatan.txtpokok.Text = RD.Item("gaji_pokok")
            formjabatan.txtjamngajar.Text = RD.Item("gaji_jam_ngajar")
            formjabatan.txtjabatan.Text = RD.Item("tunjangan_jabatan")
            formjabatan.txttunguru.Text = RD.Item("tunjangan_guru")
        End If
    End Sub
    Sub tampilgrid()
        Call koneksi()
        DA = New OdbcDataAdapter("select * from tjabatan", CONN)
        DS = New DataSet
        DA.Fill(DS, "tjabatan")
        DataGridView1.DataSource = DS.Tables("tjabatan")
        DataGridView1.ReadOnly = True
    End Sub
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.DarkCyan
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.DarkCyan
        DataGridView1.Columns(3).Visible = False
        DataGridView1.Columns(2).Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dataakun.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Golongan.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form1.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        formjabatan.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If formjabatan.txtid.Text = txtid.Text Or txtid.Text = formjabatan.txtid.Text Then
            formjabatan.btninput.Enabled = True
            formjabatan.btnedit.Enabled = False
        Else
            formjabatan.btnedit.Enabled = True
            formjabatan.btninput.Enabled = False
        End If
        If txtid.Text = "" Then
            MsgBox("Silahkan Masukan ID Yang Akan DIEDIT!")
        Else
            formjabatan.txtid.Enabled = False
            formjabatan.txtid.Text = txtid.Text
            Call tampiltextbox()
            formjabatan.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        txtid.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
    End Sub
End Class