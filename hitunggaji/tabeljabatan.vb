Imports System.Data.Odbc
Public Class tabeljabatan
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
        DA = New OdbcDataAdapter("select * from tjabatan", CONN)
        DS = New DataSet
        DA.Fill(DS, "tjabatan")
        DataGridView1.DataSource = DS.Tables("tjabatan")
        DataGridView1.ReadOnly = True
    End Sub

    Sub aturDGV()
        DataGridView1.RowsDefaultCellStyle.BackColor = Color.SteelBlue
        DataGridView1.RowsDefaultCellStyle.ForeColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White
        DataGridView1.AlternatingRowsDefaultCellStyle.ForeColor = Color.SteelBlue
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        On Error Resume Next
        tambahkaryawan.txtjabatan.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Me.Hide()
    End Sub

    Private Sub tabeljabatan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampilgrid()
        Call aturDGV()
    End Sub

    Private Sub btninput_Click(sender As Object, e As EventArgs) Handles btninput.Click
        Me.Hide()
    End Sub
End Class