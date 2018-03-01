Public Class Tanque
    Inherits System.Windows.Forms.Form
    Private _TipoOperacion As Integer
    Private _Cliente As Integer
    Private _Secuencia As Integer
    Private _numeroCelda As Integer

    Public Sub Entrada(ByVal Cliente As Integer, _
                       ByVal Nombre As String, _
                       ByVal Tipo As Integer, _
                       ByVal Secuencia As Integer)

        Me.Text = "Equipos - [" + Nombre + "]"
        _TipoOperacion = Tipo
        _Cliente = Cliente
        _Secuencia = Secuencia

        Try
            SqlConnection.Close()
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try


        daEquipo.Fill(DsEquipos, "Equipo")
        daTipoPropiedad.Fill(DsEquipos, "TipoPropiedad")

        cmdCClienteEquipo.Parameters("@Cliente").Value = Cliente
        daClienteEquipo.Fill(DsEquipos, "ClienteEquipo")

        If _TipoOperacion = 0 Then
            DsEquipos.ClienteEquipo.AddClienteEquipoRow(1, "", "  /  /    ", "  /  /    ", "  /  /    ", "  /  /    ", Cliente, 1, 1, "", "")
            btnNuevo.Enabled = False
        Else
            cmbSecuencia.SelectedValue = _Secuencia
        End If


        Me.ShowDialog()

    End Sub


    Private Sub LimpiarAskMaskEdBox()
        txtFabricacion.PromptChar = "_"
        txtFabricacion.Mask = "????????????"
        txtFabricacion.Text = "____________"
        txtFabricacion.PromptChar = " "
        txtFabricacion.Mask = "##/##/####"

        txtUltraSonido.PromptChar = "_"
        txtUltraSonido.Mask = "????????????"
        txtUltraSonido.Text = "____________"
        txtUltraSonido.PromptChar = " "
        txtUltraSonido.Mask = "##/##/####"

        txtCaducidad.PromptChar = "_"
        txtCaducidad.Mask = "????????????"
        txtCaducidad.Text = "____________"
        txtCaducidad.PromptChar = " "
        txtCaducidad.Mask = "##/##/####"

        txtCambioValvula.PromptChar = "_"
        txtCambioValvula.Mask = "????????????"
        txtCambioValvula.Text = "____________"
        txtCambioValvula.PromptChar = " "
        txtCambioValvula.Mask = "##/##/####"
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbSecuencia As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTanque As System.Windows.Forms.ComboBox
    Friend WithEvents txtSerie As System.Windows.Forms.TextBox
    Friend WithEvents bntAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnNuevo As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lbContrato As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbTipoPropiedad As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents cmdCEquipo As System.Data.SqlClient.SqlCommand
    Friend WithEvents daEquipo As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsEquipos As Sigamet.dsEquipos
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtFabricacion As AxMSMask.AxMaskEdBox
    Friend WithEvents txtUltraSonido As AxMSMask.AxMaskEdBox
    Friend WithEvents txtCaducidad As AxMSMask.AxMaskEdBox
    Friend WithEvents txtCambioValvula As AxMSMask.AxMaskEdBox
    Friend WithEvents daTipoPropiedad As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCTipoPropiedad As System.Data.SqlClient.SqlCommand
    Friend WithEvents cmdCClienteEquipo As System.Data.SqlClient.SqlCommand
    Friend WithEvents daClienteEquipo As System.Data.SqlClient.SqlDataAdapter
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Tanque))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbSecuencia = New System.Windows.Forms.ComboBox()
        Me.DsEquipos = New Sigamet.dsEquipos()
        Me.cmbTanque = New System.Windows.Forms.ComboBox()
        Me.txtSerie = New System.Windows.Forms.TextBox()
        Me.bntAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnNuevo = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbTipoPropiedad = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lbContrato = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtFabricacion = New AxMSMask.AxMaskEdBox()
        Me.txtUltraSonido = New AxMSMask.AxMaskEdBox()
        Me.txtCaducidad = New AxMSMask.AxMaskEdBox()
        Me.txtCambioValvula = New AxMSMask.AxMaskEdBox()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.cmdCEquipo = New System.Data.SqlClient.SqlCommand()
        Me.daEquipo = New System.Data.SqlClient.SqlDataAdapter()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmdCTipoPropiedad = New System.Data.SqlClient.SqlCommand()
        Me.daTipoPropiedad = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCClienteEquipo = New System.Data.SqlClient.SqlCommand()
        Me.daClienteEquipo = New System.Data.SqlClient.SqlDataAdapter()
        CType(Me.DsEquipos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFabricacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtUltraSonido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCaducidad, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCambioValvula, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(116, 14)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Numero de equipo :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 152)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 14)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Serie :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 214)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(114, 14)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Fecha de fabricacion :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 240)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 14)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Fecha de ultrasonido :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 296)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Cambio de valvulas :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 270)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 14)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Fecha de Caducidad :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(24, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 14)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Marca del equipo :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(24, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(70, 14)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Capacidad :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(24, 48)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(51, 14)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Equipo :"
        '
        'cmbSecuencia
        '
        Me.cmbSecuencia.DataSource = Me.DsEquipos.ClienteEquipo
        Me.cmbSecuencia.DisplayMember = "Secuencia"
        Me.cmbSecuencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSecuencia.Location = New System.Drawing.Point(136, 13)
        Me.cmbSecuencia.Name = "cmbSecuencia"
        Me.cmbSecuencia.Size = New System.Drawing.Size(82, 21)
        Me.cmbSecuencia.TabIndex = 0
        Me.cmbSecuencia.ValueMember = "Secuencia"
        '
        'DsEquipos
        '
        Me.DsEquipos.DataSetName = "dsEquipos"
        Me.DsEquipos.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsEquipos.Namespace = "http://www.tempuri.org/dsEquipos.xsd"
        '
        'cmbTanque
        '
        Me.cmbTanque.DataSource = Me.DsEquipos.Equipo
        Me.cmbTanque.DisplayMember = "DesEquipo"
        Me.cmbTanque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTanque.Location = New System.Drawing.Point(136, 46)
        Me.cmbTanque.Name = "cmbTanque"
        Me.cmbTanque.Size = New System.Drawing.Size(301, 21)
        Me.cmbTanque.TabIndex = 1
        Me.cmbTanque.ValueMember = "Equipo"
        '
        'txtSerie
        '
        Me.txtSerie.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtSerie.Location = New System.Drawing.Point(136, 150)
        Me.txtSerie.MaxLength = 30
        Me.txtSerie.Name = "txtSerie"
        Me.txtSerie.Size = New System.Drawing.Size(304, 21)
        Me.txtSerie.TabIndex = 5
        Me.txtSerie.Text = ""
        '
        'bntAceptar
        '
        Me.bntAceptar.Image = CType(resources.GetObject("bntAceptar.Image"), System.Drawing.Bitmap)
        Me.bntAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.bntAceptar.Location = New System.Drawing.Point(464, 13)
        Me.bntAceptar.Name = "bntAceptar"
        Me.bntAceptar.TabIndex = 11
        Me.bntAceptar.Text = "&Guardar"
        Me.bntAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(464, 45)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 12
        Me.btnCancelar.Text = "&Cerrar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnNuevo
        '
        Me.btnNuevo.Image = CType(resources.GetObject("btnNuevo.Image"), System.Drawing.Bitmap)
        Me.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnNuevo.Location = New System.Drawing.Point(464, 91)
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.TabIndex = 13
        Me.btnNuevo.Text = "&Nuevo"
        Me.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(24, 99)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 14)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "Tipo de equipo :"
        '
        'cmbTipoPropiedad
        '
        Me.cmbTipoPropiedad.DataSource = Me.DsEquipos.TipoPropiedad
        Me.cmbTipoPropiedad.DisplayMember = "Descripcion"
        Me.cmbTipoPropiedad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoPropiedad.Location = New System.Drawing.Point(136, 176)
        Me.cmbTipoPropiedad.Name = "cmbTipoPropiedad"
        Me.cmbTipoPropiedad.Size = New System.Drawing.Size(304, 21)
        Me.cmbTipoPropiedad.TabIndex = 6
        Me.cmbTipoPropiedad.ValueMember = "TipoPropiedad"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(24, 180)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(97, 14)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Tipo propiedad :"
        '
        'lbContrato
        '
        Me.lbContrato.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbContrato.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEquipos, "Equipo.DesMarcaEquipo"))
        Me.lbContrato.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbContrato.Location = New System.Drawing.Point(136, 72)
        Me.lbContrato.Name = "lbContrato"
        Me.lbContrato.Size = New System.Drawing.Size(302, 23)
        Me.lbContrato.TabIndex = 2
        Me.lbContrato.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label12.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEquipos, "Equipo.DesTipoEquipo"))
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(136, 97)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(302, 23)
        Me.Label12.TabIndex = 3
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label13
        '
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsEquipos, "Equipo.Capacidad"))
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(136, 122)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(104, 23)
        Me.Label13.TabIndex = 4
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFabricacion
        '
        Me.txtFabricacion.Location = New System.Drawing.Point(136, 211)
        Me.txtFabricacion.Name = "txtFabricacion"
        Me.txtFabricacion.OcxState = CType(resources.GetObject("txtFabricacion.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtFabricacion.Size = New System.Drawing.Size(104, 20)
        Me.txtFabricacion.TabIndex = 7
        '
        'txtUltraSonido
        '
        Me.txtUltraSonido.Location = New System.Drawing.Point(136, 240)
        Me.txtUltraSonido.Name = "txtUltraSonido"
        Me.txtUltraSonido.OcxState = CType(resources.GetObject("txtUltraSonido.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtUltraSonido.Size = New System.Drawing.Size(104, 20)
        Me.txtUltraSonido.TabIndex = 8
        '
        'txtCaducidad
        '
        Me.txtCaducidad.Location = New System.Drawing.Point(136, 268)
        Me.txtCaducidad.Name = "txtCaducidad"
        Me.txtCaducidad.OcxState = CType(resources.GetObject("txtCaducidad.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtCaducidad.Size = New System.Drawing.Size(104, 20)
        Me.txtCaducidad.TabIndex = 9
        '
        'txtCambioValvula
        '
        Me.txtCambioValvula.Location = New System.Drawing.Point(136, 296)
        Me.txtCambioValvula.Name = "txtCambioValvula"
        Me.txtCambioValvula.OcxState = CType(resources.GetObject("txtCambioValvula.OcxState"), System.Windows.Forms.AxHost.State)
        Me.txtCambioValvula.Size = New System.Drawing.Size(104, 20)
        Me.txtCambioValvula.TabIndex = 10
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=Digital5000;initial catalog=Sigamet;persist security info=False;user " & _
        "id=sa;workstation id=FHURTADO;packet size=4096"
        '
        'cmdCEquipo
        '
        Me.cmdCEquipo.CommandText = "SELECT E.Equipo, E.Descripcion AS DesEquipo, E.Capacidad, E.TipoEquipo, E.MarcaEq" & _
        "uipo, TE.Descripcion AS DesTipoEquipo, ME.Descripcion AS DesMarcaEquipo FROM Equ" & _
        "ipo E INNER JOIN TipoEquipo TE ON E.TipoEquipo = TE.TipoEquipo INNER JOIN MarcaE" & _
        "quipo ME ON E.MarcaEquipo = ME.MarcaEquipo ORDER BY E.Descripcion"
        Me.cmdCEquipo.Connection = Me.SqlConnection
        '
        'daEquipo
        '
        Me.daEquipo.SelectCommand = Me.cmdCEquipo
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(248, 124)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(31, 14)
        Me.Label14.TabIndex = 20
        Me.Label14.Text = "Litros"
        '
        'cmdCTipoPropiedad
        '
        Me.cmdCTipoPropiedad.CommandText = "SELECT TipoPropiedad, Descripcion FROM TipoPropiedad"
        Me.cmdCTipoPropiedad.Connection = Me.SqlConnection
        '
        'daTipoPropiedad
        '
        Me.daTipoPropiedad.SelectCommand = Me.cmdCTipoPropiedad
        '
        'cmdCClienteEquipo
        '
        Me.cmdCClienteEquipo.CommandText = "SELECT CE.Secuencia, CE.Serie, ISNULL(CONVERT(Char(2), REPLICATE('0', 2 - DATALEN" & _
        "GTH(CONVERT(varChar(2), DATEPART(day, CE.FFabricacion)))) + CONVERT(varChar(2), " & _
        "DATEPART(day, CE.FFabricacion))) + '/' + CONVERT(Char(2), REPLICATE('0', 2 - DAT" & _
        "ALENGTH(CONVERT(varChar(2), DATEPART(month, CE.FFabricacion)))) + CONVERT(varCha" & _
        "r(2), DATEPART(month, CE.FFabricacion))) + '/' + CONVERT(Char(4), DATEPART(year," & _
        " CE.FFabricacion)), ' / / ') AS FFabricacion, ISNULL(CONVERT(Char(2), REPLICATE(" & _
        "'0', 2 - DATALENGTH(CONVERT(varChar(2), DATEPART(day, CE.FUltrasonido)))) + CONV" & _
        "ERT(varChar(2), DATEPART(day, CE.FUltrasonido))) + '/' + CONVERT(Char(2), REPLIC" & _
        "ATE('0', 2 - DATALENGTH(CONVERT(varChar(2), DATEPART(month, CE.FUltrasonido)))) " & _
        "+ CONVERT(varChar(2), DATEPART(month, CE.FUltrasonido))) + '/' + CONVERT(Char(4)" & _
        ", DATEPART(year, CE.FUltrasonido)), ' / / ') AS FUltraSonido, ISNULL(CONVERT(Cha" & _
        "r(2), REPLICATE('0', 2 - DATALENGTH(CONVERT(varChar(2), DATEPART(day, CE.FCambio" & _
        "Valvulas)))) + CONVERT(varChar(2), DATEPART(day, CE.FCambioValvulas))) + '/' + C" & _
        "ONVERT(Char(2), REPLICATE('0', 2 - DATALENGTH(CONVERT(varChar(2), DATEPART(month" & _
        ", CE.FCambioValvulas)))) + CONVERT(varChar(2), DATEPART(month, CE.FCambioValvula" & _
        "s))) + '/' + CONVERT(Char(4), DATEPART(year, CE.FCambioValvulas)), ' / / ') AS F" & _
        "CambioValvulas, ISNULL(CONVERT(Char(2), REPLICATE('0', 2 - DATALENGTH(CONVERT(va" & _
        "rChar(2), DATEPART(day, CE.FCaducidad)))) + CONVERT(varChar(2), DATEPART(day, CE" & _
        ".FCaducidad))) + '/' + CONVERT(Char(2), REPLICATE('0', 2 - DATALENGTH(CONVERT(va" & _
        "rChar(2), DATEPART(month, CE.FCaducidad)))) + CONVERT(varChar(2), DATEPART(month" & _
        ", CE.FCaducidad))) + '/' + CONVERT(Char(4), DATEPART(year, CE.FCaducidad)), ' / " & _
        "/ ') AS FCaducidad, CE.Cliente, CE.Equipo, CE.TipoPropiedad, E.Descripcion AS De" & _
        "sEquipo, TP.Descripcion AS DesTipoPropiedad FROM ClienteEquipo CE INNER JOIN Equ" & _
        "ipo E ON CE.Equipo = E.Equipo INNER JOIN TipoPropiedad TP ON CE.TipoPropiedad = " & _
        "TP.TipoPropiedad WHERE (CE.Cliente = @Cliente) ORDER BY CE.Secuencia"
        Me.cmdCClienteEquipo.Connection = Me.SqlConnection
        Me.cmdCClienteEquipo.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Cliente", System.Data.SqlDbType.Int, 4, "Cliente"))
        '
        'daClienteEquipo
        '
        Me.daClienteEquipo.SelectCommand = Me.cmdCClienteEquipo
        '
        'Tanque
        '
        Me.AcceptButton = Me.bntAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(552, 326)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label14, Me.txtCambioValvula, Me.txtCaducidad, Me.txtUltraSonido, Me.txtFabricacion, Me.Label13, Me.Label12, Me.lbContrato, Me.cmbTipoPropiedad, Me.btnNuevo, Me.btnCancelar, Me.bntAceptar, Me.txtSerie, Me.cmbTanque, Me.cmbSecuencia, Me.Label11, Me.Label10, Me.Label9, Me.Label8, Me.Label7, Me.Label6, Me.Label5, Me.Label4, Me.Label3, Me.Label2, Me.Label1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Tanque"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Equipos - [Cliente]"
        CType(Me.DsEquipos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFabricacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtUltraSonido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCaducidad, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCambioValvula, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub cmbTipoPropiedad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoPropiedad.SelectedIndexChanged
        Me.BindingContext(DsEquipos, "TipoPropiedad").Position = cmbTipoPropiedad.SelectedIndex
    End Sub

    Private Sub cmbTanque_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTanque.SelectedIndexChanged
        Me.BindingContext(DsEquipos, "Equipo").Position = cmbTanque.SelectedIndex
    End Sub

    Private Sub cmbSecuencia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSecuencia.SelectedIndexChanged
        Me.BindingContext(DsEquipos, "ClienteEquipo").Position = cmbSecuencia.SelectedIndex
        cmbTipoPropiedad.SelectedIndex = cmbTipoPropiedad.FindString(DsEquipos.ClienteEquipo(cmbSecuencia.SelectedIndex).DesTipoPropiedad)
        cmbTanque.SelectedIndex = cmbTanque.FindString(DsEquipos.ClienteEquipo(cmbSecuencia.SelectedIndex).DesEquipo)
        txtSerie.Text = DsEquipos.ClienteEquipo(cmbSecuencia.SelectedIndex).Serie

        LimpiarAskMaskEdBox()
        txtFabricacion.SelText = DsEquipos.ClienteEquipo(cmbSecuencia.SelectedIndex).FFabricacion
        txtUltraSonido.SelText = DsEquipos.ClienteEquipo(cmbSecuencia.SelectedIndex).FUltrasonido
        txtCaducidad.SelText = DsEquipos.ClienteEquipo(cmbSecuencia.SelectedIndex).FCaducidad
        txtCambioValvula.SelText = DsEquipos.ClienteEquipo(cmbSecuencia.SelectedIndex).FCambioValvulas

    End Sub


    Private Sub txtFabricacion_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFabricacion.Validated
        If txtFabricacion.FormattedText <> "  /  /    " Then
            If Len(RTrim(LTrim(txtFabricacion.FormattedText.Replace(" ", "")))) = 10 Then
                If Not IsDate(txtFabricacion.FormattedText) Then
                    MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    txtFabricacion.Select()
                End If
            Else
                MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                txtFabricacion.Select()
            End If
        End If

    End Sub

    Private Sub txtUltraSonido_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUltraSonido.Validated
        If txtUltraSonido.FormattedText <> "  /  /    " Then
            If Len(RTrim(LTrim(txtUltraSonido.FormattedText.Replace(" ", "")))) = 10 Then

                If Not IsDate(txtUltraSonido.FormattedText) Then
                    MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    txtUltraSonido.Select()
                End If
            Else
                MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                txtUltraSonido.Select()
            End If

        End If

    End Sub

    Private Sub txtCaducidad_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCaducidad.Validated
        If txtCaducidad.FormattedText <> "  /  /    " Then
            If Len(RTrim(LTrim(txtCaducidad.FormattedText.Replace(" ", "")))) = 10 Then

                If Not IsDate(txtCaducidad.FormattedText) Then
                    MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    txtCaducidad.Select()
                End If
            Else
                MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                txtCaducidad.Select()
            End If
        End If


    End Sub

    Private Sub txtCambioValvula_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCambioValvula.Validated
        If txtCambioValvula.FormattedText <> "  /  /    " Then
            If Len(RTrim(LTrim(txtCambioValvula.FormattedText.Replace(" ", "")))) = 10 Then

                If Not IsDate(txtCambioValvula.FormattedText) Then
                    MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    txtCambioValvula.Select()
                End If
            Else
                MsgBox("No es un valor de fecha valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                txtCambioValvula.Select()
            End If
        End If


    End Sub


    Private Sub bntAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAceptar.Click
        Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Existe As Integer
        Dim SiGrabo As Boolean

        cmdInsert.Connection = SqlConnection
        cmdInsert.CommandTimeout = 30
        cmdInsert.CommandText = "Select Count(*) as Registro from ClienteEquipo where Cliente=@Cliente and Secuencia=@Secuencia"
        cmdInsert.Parameters.Clear()
        cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        cmdInsert.Parameters.Add("@Secuencia", SqlDbType.Int).Value = CType(cmbSecuencia.Text, Integer)
        rdrInsert = cmdInsert.ExecuteReader()
        rdrInsert.Read()
        Existe = CType(rdrInsert("Registro"), Integer)
        rdrInsert.Close()

        SiGrabo = False

        Transaccion = SqlConnection.BeginTransaction
        cmdInsert.Connection = SqlConnection
        cmdInsert.Transaction = Transaccion
        cmdInsert.CommandType = CommandType.StoredProcedure
        Try
            cmdInsert.CommandText = "sp_ActualizaClienteEquipo"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
            cmdInsert.Parameters.Add("@Secuencia", SqlDbType.Int).Value = cmbSecuencia.SelectedValue
            cmdInsert.Parameters.Add("@Serie", SqlDbType.Char).Value = txtSerie.Text
            If txtFabricacion.FormattedText <> "  /  /    " Then
                cmdInsert.Parameters.Add("@FFabricacion", SqlDbType.Char).Value = txtFabricacion.FormattedText
            Else
                cmdInsert.Parameters.Add("@FFabricacion", SqlDbType.Char).Value = ""
            End If

            If txtUltraSonido.FormattedText <> "  /  /    " Then
                cmdInsert.Parameters.Add("@FUltraSonido", SqlDbType.Char).Value = txtUltraSonido.FormattedText
            Else
                cmdInsert.Parameters.Add("@FUltraSonido", SqlDbType.Char).Value = ""
            End If

            If txtCambioValvula.FormattedText <> "  /  /    " Then
                cmdInsert.Parameters.Add("@FCambioValvulas", SqlDbType.Char).Value = txtCambioValvula.FormattedText
            Else
                cmdInsert.Parameters.Add("@FCambioValvulas", SqlDbType.Char).Value = ""
            End If

            If txtCaducidad.FormattedText <> "  /  /    " Then
                cmdInsert.Parameters.Add("@FCaducidad", SqlDbType.Char).Value = txtCaducidad.FormattedText
            Else
                cmdInsert.Parameters.Add("@FCaducidad", SqlDbType.Char).Value = ""
            End If

            cmdInsert.Parameters.Add("@Equipo", SqlDbType.Int).Value = cmbTanque.SelectedValue
            cmdInsert.Parameters.Add("@TipoPropiedad", SqlDbType.Int).Value = cmbTipoPropiedad.SelectedValue
            If Existe = 0 Then
                cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 0
            Else
                cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 1
            End If

            cmdInsert.ExecuteNonQuery()
            Transaccion.Commit()
            SiGrabo = True
        Catch er As Exception
            Transaccion.Rollback()
            MsgBox(er.Message, MsgBoxStyle.Critical)
            SiGrabo = False
        Finally
            If SiGrabo Then
                btnNuevo.Enabled = True
                Me.Close()
            End If

        End Try


    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Dim Indice As Integer
        Indice = DsEquipos.ClienteEquipo.Rows.Count + 1
        LimpiarAskMaskEdBox()
        DsEquipos.ClienteEquipo.AddClienteEquipoRow(CType(Indice, Short), "", "  /  /    ", "  /  /    ", "  /  /    ", "  /  /    ", _Cliente, 1, 1, "", "")
        cmbSecuencia.SelectedIndex = Indice - 1
        btnNuevo.Enabled = False
        txtSerie.Focus()
    End Sub

    'Funcion para encontrar la referencia de la celda del grid de pedidos
    Public Function getCelda() As Integer
        Return _numeroCelda
    End Function

    'Funcion para referenciar la celda del grid de pedidos
    Public Function setCelda(ByVal numVal As Integer) As Integer
        _numeroCelda = numVal
    End Function

    Private Sub Tanque_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Sub Tanque_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
