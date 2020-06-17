Imports System.Data.Odbc
Public Class tabelkaryawan
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

    Sub tampilgrid()
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
    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        'form_gaji.Show()
        Me.Hide()
    End Sub


    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        form_gaji.txtnpk.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        form_gaji.txtnama.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        form_gaji.txtgolongan.Text = DataGridView1.Rows(e.RowIndex).Cells(8).Value
        form_gaji.txtjabatan.Text = DataGridView1.Rows(e.RowIndex).Cells(9).Value


        Me.Hide()
    End Sub

    Private Sub tabelkaryawan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilgrid()
    End Sub
End Class