Public Class LecturaLiquidacion
    Inherits System.Windows.Forms.Form

    Public _Fecha As DateTime
    Public _Ruta As Integer
    Public _Folio As Integer
    Public _Anioatt As Integer
    Public _Acepto As Boolean
    Public _Descarga As Boolean

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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbRuta As System.Windows.Forms.ComboBox
    Friend WithEvents cmdRuta As System.Data.SqlClient.SqlCommand
    Friend WithEvents daRuta As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsRutasLiquidaciones As Sigamet.dsRutasLiquidaciones
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbAutotanque As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.cmbRuta = New System.Windows.Forms.ComboBox()
        Me.DsRutasLiquidaciones = New Sigamet.dsRutasLiquidaciones()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.DateTimePicker()
        Me.cmdRuta = New System.Data.SqlClient.SqlCommand()
        Me.daRuta = New System.Data.SqlClient.SqlDataAdapter()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbAutotanque = New System.Windows.Forms.Label()
        CType(Me.DsRutasLiquidaciones, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(77, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Ruta :"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(256, 14)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 1
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(256, 46)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 2
        Me.btnCancelar.Text = "Cancelar"
        '
        'cmbRuta
        '
        Me.cmbRuta.DataSource = Me.DsRutasLiquidaciones.Rutas
        Me.cmbRuta.DisplayMember = "Descripcion"
        Me.cmbRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRuta.Location = New System.Drawing.Point(112, 48)
        Me.cmbRuta.Name = "cmbRuta"
        Me.cmbRuta.Size = New System.Drawing.Size(136, 21)
        Me.cmbRuta.TabIndex = 0
        Me.cmbRuta.ValueMember = "Ruta"
        '
        'DsRutasLiquidaciones
        '
        Me.DsRutasLiquidaciones.DataSetName = "dsRutasLiquidaciones"
        Me.DsRutasLiquidaciones.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsRutasLiquidaciones.Namespace = "http://www.tempuri.org/dsRutasLiquidaciones.xsd"
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "Data Source=Desarrollo; Initial Catalog=Sigamet;User ID =sa;Password =DEVELOPMENT" & _
        ""
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 14)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Fecha liquidación :"
        '
        'txtFecha
        '
        Me.txtFecha.CustomFormat = "dd/MM/yyyy"
        Me.txtFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFecha.Location = New System.Drawing.Point(112, 16)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Size = New System.Drawing.Size(136, 21)
        Me.txtFecha.TabIndex = 4
        '
        'cmdRuta
        '
        Me.cmdRuta.CommandText = "SELECT ATT.Ruta, R.Descripcion, ATT.Folio, ATT.AñoAtt AS AnioAtt, ATT.Autotanque " & _
        "FROM AutotanqueTurno ATT INNER JOIN Ruta R ON ATT.Ruta = R.Ruta WHERE (ATT.FAsig" & _
        "nacion BETWEEN @Fecha1 AND @Fecha2) AND (ATT.Celula = @Celula) AND (ATT.StatusLo" & _
        "gistica = 'LIQUIDADO') ORDER BY ATT.Ruta"
        Me.cmdRuta.Connection = Me.SqlConnection
        Me.cmdRuta.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Fecha1", System.Data.SqlDbType.DateTime, 8, "FAsignacion"))
        Me.cmdRuta.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Fecha2", System.Data.SqlDbType.DateTime, 8, "FAsignacion"))
        Me.cmdRuta.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Celula", System.Data.SqlDbType.TinyInt, 1, "Celula"))
        '
        'daRuta
        '
        Me.daRuta.SelectCommand = Me.cmdRuta
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(43, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Autotanque :"
        '
        'lbAutotanque
        '
        Me.lbAutotanque.BackColor = System.Drawing.Color.White
        Me.lbAutotanque.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbAutotanque.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsRutasLiquidaciones, "Rutas.Autotanque"))
        Me.lbAutotanque.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAutotanque.ForeColor = System.Drawing.Color.Blue
        Me.lbAutotanque.Location = New System.Drawing.Point(112, 77)
        Me.lbAutotanque.Name = "lbAutotanque"
        Me.lbAutotanque.Size = New System.Drawing.Size(72, 21)
        Me.lbAutotanque.TabIndex = 6
        Me.lbAutotanque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LecturaLiquidacion
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(338, 112)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbAutotanque, Me.txtFecha, Me.cmbRuta, Me.btnCancelar, Me.btnAceptar, Me.Label3, Me.Label1, Me.Label2})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "LecturaLiquidacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ruta a conciliar"
        CType(Me.DsRutasLiquidaciones, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub LecturaLiquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Fecha As DateTime

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        cmdInsert.Connection = SqlConnection
        cmdInsert.CommandTimeout = 30
        cmdInsert.CommandText = "Select Convert(Datetime,Convert(VarChar(2),Day(getdate()))+'/'+Convert(VarChar(2),Month(getdate()))+'/'+Convert(VarChar(4),Year(getdate()))) as Fecha "
        rdrInsert = cmdInsert.ExecuteReader
        rdrInsert.Read()
        Fecha = CType(rdrInsert("Fecha"), Date)
        rdrInsert.Close()
        cmdInsert.Dispose()

        txtFecha.Value = Fecha

        _Acepto = False

    End Sub

    Private Sub txtFecha_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFecha.ValueChanged
        DsRutasLiquidaciones.Rutas.Clear()
        cmdRuta.Parameters("@Fecha1").Value = CType(CType(txtFecha.Value.Date, String) + " 00:00:00", DateTime)
        cmdRuta.Parameters("@Fecha2").Value = CType(CType(txtFecha.Value.Date, String) + " 23:23:59", DateTime)
        cmdRuta.Parameters("@Celula").Value = GLOBAL_Celula
        daRuta.Fill(DsRutasLiquidaciones, "Rutas")
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing
        Dim Registro As Integer = Nothing

        If DsRutasLiquidaciones.Rutas.Count > 0 Then
            _Fecha = txtFecha.Value.Date
            _Ruta = CType(cmbRuta.SelectedValue, Integer)
            _Folio = CType(DsRutasLiquidaciones.Rutas(cmbRuta.SelectedIndex).Folio, Integer)
            _Anioatt = DsRutasLiquidaciones.Rutas(cmbRuta.SelectedIndex).AnioAtt
            _Acepto = True
            'cmdInsert.Connection = SqlConnection
            'cmdInsert.CommandType = CommandType.Text
            'cmdInsert.CommandText = "Select Count(*) as Registro from Rampac Where AñoAtt=@AñoAtt and Folio=@Folio "
            'cmdInsert.Parameters.Clear()
            'cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _Anioatt
            'cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
            'rdrInsert = cmdInsert.ExecuteReader
            'rdrInsert.Read()
            'Registro = rdrInsert("Registro")
            'rdrInsert.Close()
            'cmdInsert.Dispose()
            'If Registro = 0 Then
            '    MsgBox("La tarjeta rampac no a sido descargada, por lo tanto la liquidacion sera de forma manual", MsgBoxStyle.Information, "Mensaje del sistema")
            '    _Descarga = False
            'Else
            '    _Descarga = True
            'End If
            _Descarga = True
            Me.Close()
        Else
            MsgBox("En este dia no existen rutas a conciliar", MsgBoxStyle.Exclamation, "Mensaje del sistema")
        End If
    End Sub


    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub LecturaLiquidacion_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Sub cmbRuta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRuta.SelectedIndexChanged
        Me.BindingContext(DsRutasLiquidaciones, "Rutas").Position = cmbRuta.SelectedIndex
    End Sub
End Class
