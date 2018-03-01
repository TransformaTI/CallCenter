Public Class ImprimeNotas
    Inherits System.Windows.Forms.Form

    Private _Fecha As DateTime
    Private _Ruta As Int16
    Private _Celula As Int16

    Private Sub GenerarNotas()
        Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        cmdInsert.Connection = SqlConnection
        cmdInsert.CommandTimeout = 200
        Transaccion = SqlConnection.BeginTransaction
        cmdInsert.Transaction = Transaccion
        Try
            
            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "sp_GeneraPedidosDeProgramacion"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
            cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
            cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
            cmdInsert.ExecuteNonQuery()

            Application.DoEvents()
            lbComentario.Text = "GENERANDO NOTAS DE REMISION..."
            lbComentario.ForeColor = Color.Blue

            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "sp_GeneraNotasRemision"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
            cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
            cmdInsert.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
            cmdInsert.Parameters.Add("@UltimoDia", SqlDbType.Int).Value = 0
            cmdInsert.ExecuteNonQuery()

            Transaccion.Commit()

        Catch er As Exception
            Transaccion.Rollback()
            Me.Cursor = System.Windows.Forms.Cursors.Default
            lbComentario.Text = "NO SE COMPLETO EL PROCESO..."
            lbComentario.ForeColor = Color.Red
            MsgBox(er.Message, MsgBoxStyle.Critical)
        Finally
            Me.Cursor = System.Windows.Forms.Cursors.Default
            lbComentario.Text = "EL PROCESO HA FINALIZADO CON EXITO..."
            lbComentario.ForeColor = Color.Green
            btnAceptar.Enabled = True
        End Try


    End Sub

    Public Sub Entrada(ByVal Fecha As DateTime, ByVal Ruta As Integer, ByVal Celula As Integer)
        _Fecha = Fecha
        _Ruta = CType(Ruta, Short)
        _Celula = CType(Celula, Short)

        Try
            SqlConnection.Close()
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            lbComentario.Text = "NO SE GENERAN REMISIONES Y PEDIDOS"
            lbComentario.ForeColor = Color.Red
        End Try

        Me.Text = "Impresión de remisiones para el " + Format(Fecha, "D")

        Timer1.Enabled = True
        Me.ShowDialog()



    End Sub

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lbComentario As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lbComentario = New System.Windows.Forms.Label()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.SuspendLayout()
        '
        'lbComentario
        '
        Me.lbComentario.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbComentario.Location = New System.Drawing.Point(8, 32)
        Me.lbComentario.Name = "lbComentario"
        Me.lbComentario.Size = New System.Drawing.Size(416, 23)
        Me.lbComentario.TabIndex = 0
        Me.lbComentario.Text = "GENERANDO PEDIDOS DE PROGRAMACION ..."
        Me.lbComentario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnAceptar
        '
        Me.btnAceptar.Enabled = False
        Me.btnAceptar.Location = New System.Drawing.Point(352, 88)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 1
        Me.btnAceptar.Text = "Aceptar"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=Digital5000;initial catalog=Sigamet;persist security info=False;user " & _
        "id=sa;workstation id=FHURTADO;packet size=4096"
        '
        'ImprimeNotas
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(434, 118)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnAceptar, Me.lbComentario})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "ImprimeNotas"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Impresión de notas de remisón"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        GenerarNotas()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.Close()
    End Sub

    Private Sub ImprimeNotas_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub
End Class
