Imports System.Data.Odbc
Public Class tabelgolongan
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
        DA = New OdbcDataAdapter("select * from tgolongan", CONN)
        DS = New DataSet
        DA.Fill(DS, "tgolongan")
        DataGridView1.DataSource = DS.Tables("tgolongan")
        DataGridView1.ReadOnly = True
    End Sub
    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.SteelBlue
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.SteelBlue
        DataGridView1.Columns(0).Width = 79
        DataGridView1.Columns(1).Width = 130
        DataGridView1.Columns(2).Width = 130
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        tambahkaryawan.txtgolongan.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Me.Hide()
    End Sub

    Private Sub tabelgolongan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilgrid()
        Call aturDGV()
    End Sub

    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        Me.Hide()
    End Sub
End Class