Public Class Bitacora
    Inherits System.Windows.Forms.Form
    Private _Usuario As String
    Private _numeroCelda As Integer

    Public Sub Entrada(ByVal Usuario As String, ByVal Nombre As String)
        Me.Text = "Bitacora diaria"
        _Usuario = Usuario

        lbGrid.Text = "para el usuario " + Nombre

        Try
            SqlConnection.Close()
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        dtDiallamada.Value = Now
        dtDiallamada.Select()

        DsBitacora.Bitacora.Clear()
        cmdCBitacora.Parameters("@Usuario").Value = _Usuario
        cmdCBitacora.Parameters("@Dia").Value = dtDiallamada.Value.Day
        cmdCBitacora.Parameters("@Mes").Value = dtDiallamada.Value.Month
        cmdCBitacora.Parameters("@Anio").Value = dtDiallamada.Value.Year
        Try
            daBitacora.Fill(DsBitacora, "Bitacora")
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)


        End Try


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
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents gdBitacora As System.Windows.Forms.DataGrid
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents cmdCBitacora As System.Data.SqlClient.SqlCommand
    Friend WithEvents daBitacora As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsBitacora As Sigamet.dsBitacora
    Friend WithEvents dgstBitacora As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents dgcFlamada As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dgcPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dgcOrigen As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dgcMotivo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TipoLlamada As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dgcSentido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dgcUsuario As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbGrid As System.Windows.Forms.Label
    Friend WithEvents dtDiallamada As System.Windows.Forms.DateTimePicker
    Friend WithEvents dgcRuta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents dgcDireccion As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.gdBitacora = New System.Windows.Forms.DataGrid()
        Me.DsBitacora = New Sigamet.dsBitacora()
        Me.dgstBitacora = New System.Windows.Forms.DataGridTableStyle()
        Me.dgcFlamada = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgcRuta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgcPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgcDireccion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgcOrigen = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgcMotivo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TipoLlamada = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgcSentido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.dgcUsuario = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.cmdCBitacora = New System.Data.SqlClient.SqlCommand()
        Me.daBitacora = New System.Data.SqlClient.SqlDataAdapter()
        Me.dtDiallamada = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbGrid = New System.Windows.Forms.Label()
        CType(Me.gdBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(384, 512)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "&Cancelar"
        '
        'gdBitacora
        '
        Me.gdBitacora.BackgroundColor = System.Drawing.SystemColors.Control
        Me.gdBitacora.CaptionBackColor = System.Drawing.Color.Khaki
        Me.gdBitacora.CaptionFont = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gdBitacora.CaptionForeColor = System.Drawing.Color.Black
        Me.gdBitacora.DataMember = ""
        Me.gdBitacora.DataSource = Me.DsBitacora.Bitacora
        Me.gdBitacora.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gdBitacora.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gdBitacora.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.gdBitacora.Name = "gdBitacora"
        Me.gdBitacora.ReadOnly = True
        Me.gdBitacora.Size = New System.Drawing.Size(872, 470)
        Me.gdBitacora.TabIndex = 0
        Me.gdBitacora.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.dgstBitacora})
        '
        'DsBitacora
        '
        Me.DsBitacora.DataSetName = "dsBitacora"
        Me.DsBitacora.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsBitacora.Namespace = "http://www.tempuri.org/dsBitacora.xsd"
        '
        'dgstBitacora
        '
        Me.dgstBitacora.AlternatingBackColor = System.Drawing.Color.Khaki
        Me.dgstBitacora.DataGrid = Me.gdBitacora
        Me.dgstBitacora.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.dgcFlamada, Me.dgcRuta, Me.dgcPedido, Me.dgcDireccion, Me.dgcOrigen, Me.dgcMotivo, Me.TipoLlamada, Me.dgcSentido, Me.dgcUsuario})
        Me.dgstBitacora.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgstBitacora.MappingName = "Bitacora"
        Me.dgstBitacora.ReadOnly = True
        '
        'dgcFlamada
        '
        Me.dgcFlamada.Format = "HH:mm:ss"
        Me.dgcFlamada.FormatInfo = Nothing
        Me.dgcFlamada.HeaderText = "Hora"
        Me.dgcFlamada.MappingName = "FLlamada"
        Me.dgcFlamada.NullText = ""
        Me.dgcFlamada.Width = 57
        '
        'dgcRuta
        '
        Me.dgcRuta.Format = ""
        Me.dgcRuta.FormatInfo = Nothing
        Me.dgcRuta.HeaderText = "Ruta"
        Me.dgcRuta.MappingName = "Ruta"
        Me.dgcRuta.NullText = ""
        Me.dgcRuta.Width = 45
        '
        'dgcPedido
        '
        Me.dgcPedido.Format = ""
        Me.dgcPedido.FormatInfo = Nothing
        Me.dgcPedido.HeaderText = "Cliente"
        Me.dgcPedido.MappingName = "Cliente"
        Me.dgcPedido.NullText = ""
        Me.dgcPedido.Width = 60
        '
        'dgcDireccion
        '
        Me.dgcDireccion.Format = ""
        Me.dgcDireccion.FormatInfo = Nothing
        Me.dgcDireccion.HeaderText = "Domicilio"
        Me.dgcDireccion.MappingName = "Domicilio"
        Me.dgcDireccion.NullText = ""
        Me.dgcDireccion.Width = 300
        '
        'dgcOrigen
        '
        Me.dgcOrigen.Format = ""
        Me.dgcOrigen.FormatInfo = Nothing
        Me.dgcOrigen.HeaderText = "Origen"
        Me.dgcOrigen.MappingName = "TelefonoOrigen"
        Me.dgcOrigen.NullText = ""
        Me.dgcOrigen.Width = 75
        '
        'dgcMotivo
        '
        Me.dgcMotivo.Format = ""
        Me.dgcMotivo.FormatInfo = Nothing
        Me.dgcMotivo.HeaderText = "Motivo"
        Me.dgcMotivo.MappingName = "DesMotivo"
        Me.dgcMotivo.NullText = ""
        Me.dgcMotivo.Width = 75
        '
        'TipoLlamada
        '
        Me.TipoLlamada.Format = ""
        Me.TipoLlamada.FormatInfo = Nothing
        Me.TipoLlamada.HeaderText = "Tipo"
        Me.TipoLlamada.MappingName = "DesTipoLlamada"
        Me.TipoLlamada.NullText = ""
        Me.TipoLlamada.Width = 65
        '
        'dgcSentido
        '
        Me.dgcSentido.Format = ""
        Me.dgcSentido.FormatInfo = Nothing
        Me.dgcSentido.HeaderText = "Sentido"
        Me.dgcSentido.MappingName = "DesSentido"
        Me.dgcSentido.NullText = ""
        Me.dgcSentido.Width = 65
        '
        'dgcUsuario
        '
        Me.dgcUsuario.Format = ""
        Me.dgcUsuario.FormatInfo = Nothing
        Me.dgcUsuario.HeaderText = "Usuario"
        Me.dgcUsuario.MappingName = "Usuario"
        Me.dgcUsuario.NullText = ""
        Me.dgcUsuario.Width = 75
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'cmdCBitacora
        '
        Me.cmdCBitacora.CommandText = "SELECT L.AñoLla, L.Llamada, L.FLlamada, L.MotivoLlamada, L.Pedido, L.Cliente, L.O" & _
        "perador, L.TelefonoOrigen, L.TelefonoDestino, E.Nombre AS Empleado, C.Nombre, ML" & _
        ".Descripcion AS DesMotivo, TL.Descripcion AS DesTipoLlamada, SL.Descripcion AS D" & _
        "esSentido, L.Usuario, C.Ruta, CONVERT(VarChar(60), CA.Nombre) + ' # ' + CONVERT(" & _
        "VarChar(9), C.NumExterior) + ' ' + CONVERT(VarChar(50), ISNULL(C.NumInterior, ''" & _
        ")) + ' Col. ' + CONVERT(VarChar(80), ISNULL(CO.Nombre, '')) AS Domicilio FROM Ll" & _
        "amada L INNER JOIN MotivoLlamada ML ON L.MotivoLlamada = ML.MotivoLlamada INNER " & _
        "JOIN TipoLlamada TL ON ML.TipoLlamada = TL.TipoLlamada INNER JOIN SentidoLlamada" & _
        " SL ON ML.SentidoLlamada = SL.SentidoLlamada INNER JOIN Cliente C ON L.Cliente =" & _
        " C.Cliente LEFT OUTER JOIN Operador O ON L.Operador = O.Operador LEFT OUTER JOIN" & _
        " Empleado E ON E.Empleado = O.Empleado INNER JOIN Calle CA ON C.Calle = CA.Calle" & _
        " INNER JOIN Colonia CO ON C.Colonia = CO.Colonia WHERE (DATEPART(dd, L.FLlamada)" & _
        " = @Dia) AND (DATEPART(mm, L.FLlamada) = @Mes) AND (DATEPART(yyyy, L.FLlamada) =" & _
        " @Anio) AND (L.Usuario = @Usuario) ORDER BY L.FLlamada"
        Me.cmdCBitacora.CommandTimeout = 100
        Me.cmdCBitacora.Connection = Me.SqlConnection
        Me.cmdCBitacora.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Dia", System.Data.SqlDbType.Decimal))
        Me.cmdCBitacora.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Mes", System.Data.SqlDbType.Decimal))
        Me.cmdCBitacora.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Anio", System.Data.SqlDbType.Decimal))
        Me.cmdCBitacora.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Usuario", System.Data.SqlDbType.VarChar, 15, "Usuario"))
        '
        'daBitacora
        '
        Me.daBitacora.SelectCommand = Me.cmdCBitacora
        '
        'dtDiallamada
        '
        Me.dtDiallamada.CalendarTitleForeColor = System.Drawing.SystemColors.Window
        Me.dtDiallamada.Location = New System.Drawing.Point(37, 4)
        Me.dtDiallamada.Name = "dtDiallamada"
        Me.dtDiallamada.Size = New System.Drawing.Size(208, 21)
        Me.dtDiallamada.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Khaki
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 14)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Del "
        '
        'lbGrid
        '
        Me.lbGrid.AutoSize = True
        Me.lbGrid.BackColor = System.Drawing.Color.Khaki
        Me.lbGrid.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbGrid.Location = New System.Drawing.Point(250, 8)
        Me.lbGrid.Name = "lbGrid"
        Me.lbGrid.TabIndex = 4
        Me.lbGrid.Text = "para el usuario X"
        '
        'Bitacora
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(872, 470)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbGrid, Me.Label1, Me.dtDiallamada, Me.gdBitacora, Me.btnCancelar})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Bitacora"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bitacora diaria - [Usuario]"
        CType(Me.gdBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub


    Private Sub dtDiallamada_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtDiallamada.ValueChanged

        DsBitacora.Bitacora.Clear()
        cmdCBitacora.Parameters("@Usuario").Value = _Usuario
        cmdCBitacora.Parameters("@Dia").Value = dtDiallamada.Value.Day
        cmdCBitacora.Parameters("@Mes").Value = dtDiallamada.Value.Month
        cmdCBitacora.Parameters("@Anio").Value = dtDiallamada.Value.Year
        daBitacora.Fill(DsBitacora, "Bitacora")

    End Sub

    'Funcion para encontrar la referencia de la celda del grid de pedidos
    Public Function getCelda() As Integer
        Return _numeroCelda
    End Function

    'Funcion para referenciar la celda del grid de pedidos
    Public Function setCelda(ByVal numVal As Integer) As Integer
        _numeroCelda = numVal
    End Function

    Private Sub Bitacora_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Sub gdBitacora_Navigate(ByVal sender As System.Object, ByVal ne As System.Windows.Forms.NavigateEventArgs) Handles gdBitacora.Navigate

    End Sub
End Class
