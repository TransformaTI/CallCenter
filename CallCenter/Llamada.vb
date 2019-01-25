Imports System.Data.SqlClient
Imports System
Imports System.Linq
Imports System.Text
Imports System.Collections.Generic

Public Class Llamada
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private _Celula As Integer
    Private _Ruta As Integer
    Private _Anio As Integer
    Private _Pedido As Integer
    Private _Boletin As Integer
    Private _numeroCelda As Integer
    Private _Portatil As Boolean
    Private _Colonia As Integer
    Private _FCompromiso As Date
    Private _UsuarioMovil As Integer
    Private _AutoTanque As Integer

    ' Variable para enviar el pedido a la plataforma SGCWeb     - RM 03/10/2018
    Private _SGCWebHabilitado As Boolean
    Private _FuenteGateway As String = ""
    Private pedidoReferencia As String
    Private fechaPedido As Date
    Private boletinEnLinea As Boolean = False
    Private idPlantaSGC As String
    Private daCel As New DataTable
    Private rutaPed As Int32
    Friend WithEvents lnkAlertaRAF As SVCC.BlinkingClickLabel
    Private LlenaHorarios As Boolean = False
    Private RAFPuedeContinuar As Boolean = True
    Friend WithEvents lnkAlertaFinDeDia As SVCC.BlinkingClickLabel
    Private RAF As String = ""
    Friend WithEvents chkNoValidarGPS As System.Windows.Forms.CheckBox
    Private _FAlta As Date
    Dim _RutaBoletin As Short
    Private _URLGateway As String
    Private _Modulo As Byte = 1
	Private _CadenaConexion As String


	Public Property URLGateway() As String
        Get
            Return _URLGateway
        End Get
        Set(ByVal value As String)
            _URLGateway = value
        End Set
    End Property


    Public Property CadenaConexion() As String
        Get
            Return _CadenaConexion
        End Get
        Set(ByVal value As String)
            _CadenaConexion = value
        End Set
    End Property

    Public Property FuenteGateway As String
        Get
            Return _FuenteGateway
        End Get
        Set(value As String)
            _FuenteGateway = value
        End Set
    End Property

	Public Sub ConsultaAutotanquesPorDia(ByVal ruta As Int32, ByVal inicio As Boolean)
		Me.Cursor = Cursors.WaitCursor

		Dim servicioPedido As New desarrollogm.Pedido()

		servicioPedido.Url = GLOBAL_URLWebserviceBoletin

		Dim dtAutotanquesDia As New DataTable()

		Try
			If GLOBAL_UsarSigametServices Then
				dtAutotanquesDia = servicioPedido.ConsultaAutotanquesPorRutaYDia(Main.GLOBAL_Estacion,
					ruta, DateTime.Now.Date, DateTime.Now.Date).Tables(0)
				Me.Cursor = Cursors.Default

			End If
		Catch ex As Exception
			MessageBox.Show("Se produjo un error consultando los autotanques:" & vbCrLf & ex.Message,
							Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
		End Try

		If dtAutotanquesDia.Rows.Count > 0 Then
			RemoveHandler cmbOperador.SelectedIndexChanged, AddressOf cmbAutoTanque_SelectedIndexChanged

			cmbAutoTanque.DataSource = dtAutotanquesDia
			cmbAutoTanque.DisplayMember = "Autotanque"
			cmbAutoTanque.ValueMember = "Autotanque"
			boletinEnLinea = True
			idPlantaSGC = CType(dtAutotanquesDia.Rows(0).Item("NombrePlantaSGC"), String)

			cmbOperador.DataSource = dtAutotanquesDia
			cmbOperador.DisplayMember = "OperadorNombre"
			cmbOperador.ValueMember = "Operador"

			lblMensaje.Text = ""
		Else
			If inicio Then
				cmbAutoTanque.DataSource = Nothing
				cmbAutoTanque.Items.Clear()
				Me.cmbAutoTanque.DataSource = Me.DsLlamada.Autotanque
				Me.cmbAutoTanque.DisplayMember = "Autotanque"
				Me.cmbAutoTanque.ValueMember = "Autotanque"
				boletinEnLinea = False

				AddHandler cmbOperador.SelectedIndexChanged, AddressOf cmbAutoTanque_SelectedIndexChanged

				lblMensaje.Text = "No hay autotanques asignados en la ruta seleccionada"
			Else
				cmbAutoTanque.DataSource = Nothing
				cmbAutoTanque.Items.Clear()


				cmbOperador.DataSource = Nothing
				cmbOperador.Items.Clear()

				boletinEnLinea = False

				AddHandler cmbOperador.SelectedIndexChanged, AddressOf cmbAutoTanque_SelectedIndexChanged

				lblMensaje.Text = "No hay autotanques asignados en la ruta seleccionada"
			End If

		End If

		Me.Cursor = Cursors.Default
	End Sub

	Public Sub New(ByVal PedidoReferencia As String,
                   ByVal RutaPedido As Int32,
                   ByVal FechaPedido As Date,
                   ByVal DaCelula As DataTable,
                   ByVal URLGateway As String,
          Optional ByVal Modulo As Byte = 0,
          Optional ByVal CadenaConexion As String = "",
          Optional ByVal SGCWebHabilitado As Boolean = False,
          Optional ByVal FuenteGateway As String = "")
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        _URLGateway = URLGateway
        _Modulo = Modulo
        _CadenaConexion = CadenaConexion
        _SGCWebHabilitado = SGCWebHabilitado
        _FuenteGateway = FuenteGateway

        Me.pedidoReferencia = PedidoReferencia
        Me.fechaPedido = FechaPedido
        daCel = DaCelula
        rutaPed = RutaPedido
        ConsultaAutotanquesPorDia(RutaPedido, True)

    End Sub

    Private Sub chkBoletinarOtraRuta_CheckedChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBoletinarOtraRuta.CheckedChanged
        cmbAutoTanque.DataSource = Nothing
		cmbOperador.DataSource = Nothing

		If GLOBAL_UsarSigametServices Then
			If daCel.Rows.Count > 0 Then
				cmbCelula.DisplayMember = "Celula"
				cmbCelula.ValueMember = "Celula"
				cmbCelula.DataSource = daCel

				Dim celula As Byte
				If cmbCelula.Items.Count > 0 Then
					cmbCelula.SelectedIndex = 0
					celula = Convert.ToByte(cmbCelula.SelectedValue)
				End If
				cmbRuta.CargaDatos(Celula:=celula, ActivarFiltro:=True, MostrarPortatil:=False)

				If cmbRuta.Items.Count > 0 Then
					LlenaHorarios = True
					cmbRuta.SelectedIndex = 0
				End If

				If (chkBoletinarOtraRuta.Checked) Then
					cmbCelula.Visible = True
					cmbRuta.Visible = True
					cmbRuta.Enabled = True
					lblCelulaRuta.Visible = True
				Else
					cmbCelula.Visible = False
					cmbRuta.Visible = False
					cmbRuta.Enabled = False
					lblCelulaRuta.Visible = False
					boletinEnLinea = False
					lblMensaje.Text = ""
					ConsultaAutotanquesPorDia(rutaPed, True)
				End If
			End If
		Else
			If (chkBoletinarOtraRuta.Checked) Then
				cmbCelula.Visible = True
				cmbRuta.Visible = True
				cmbRuta.Enabled = True
				lblCelulaRuta.Visible = True

				ConsultaCelulas()
				If daCel.Rows.Count > 0 Then
					cmbCelula.DisplayMember = "Celula"
					cmbCelula.ValueMember = "Celula"
					cmbCelula.DataSource = daCel

					Dim celula As Byte
					If cmbCelula.Items.Count > 0 Then
						cmbCelula.SelectedIndex = 0
						celula = Convert.ToByte(cmbCelula.SelectedValue)
					End If
				End If
			Else
				cmbCelula.Visible = False
				cmbRuta.Visible = False
				cmbRuta.Enabled = False
				lblCelulaRuta.Visible = False
				boletinEnLinea = False
				lblMensaje.Text = ""

				LlenaListaAutotanques(_Ruta, False)
			End If
		End If

	End Sub

    Private Sub cmbRuta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRuta.SelectedIndexChanged
        If Not cmbRuta.SelectedIndex = -1 Then
			If CInt(cmbRuta.Ruta) <> 0 Then
				LlenaListaAutotanques(cmbRuta.Ruta, False)
			End If
		End If
        'LUSATE
        If Me.LlenaHorarios Then
            grdHorario.DataSource = fncHorario(cmbRuta.Ruta, _Colonia)
        End If
        '' 
    End Sub

    Private Sub cmbCelula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCelula.SelectedIndexChanged
        If GLOBAL_VersionMovilGas <> 0 Then
            If _Portatil Then
                cmbRuta.CargaDatos(Celula:=CType(cmbCelula.SelectedValue, Byte), ActivarFiltro:=True, MostrarPortatil:=True)
            Else
                cmbRuta.CargaDatos(Celula:=CType(cmbCelula.SelectedValue, Byte), ActivarFiltro:=True, MostrarPortatil:=False)
            End If

        Else
            cmbRuta.CargaDatos(Celula:=CType(cmbCelula.SelectedValue, Byte))
        End If

		If cmbRuta.Items.Count > 0 Then
			cmbRuta.SelectedIndex = 0
			grdHorario.DataSource = fncHorario(cmbRuta.Ruta, _Colonia)
		Else
			Try
				cmbOperador.DataSource = Nothing
				cmbOperador.Items.Clear()

			Catch ex As Exception

			End Try

			Try
				cmbAutoTanque.DataSource = Nothing
				cmbAutoTanque.Items.Clear()
			Catch ex As Exception

			End Try
		End If
    End Sub

    Public Function ConsultarDatosCliente(ByVal Cliente As Integer) As RTGMCore.DireccionEntrega

        Dim oGateway = New RTGMGateway.RTGMGateway(_Modulo, _CadenaConexion)
        Dim oSolicitud As RTGMGateway.SolicitudGateway
        Dim oDireccionEntrega As New RTGMCore.DireccionEntrega

        oSolicitud.IDCliente = Cliente
        oGateway.URLServicio = _URLGateway
        Try
            oDireccionEntrega = oGateway.buscarDireccionEntrega(oSolicitud)
        Catch ex As Exception
            Throw ex
        End Try

        Return oDireccionEntrega
    End Function


    Public Sub Entrada(ByVal Cliente As Integer, ByVal Nombre As String, ByVal Celula As Integer, ByVal Pedido As Integer, ByVal Origen As String, ByVal Ruta As Integer, ByVal Anio As Integer, ByVal Boletin As Integer, ByVal FCompromiso As Date, ByVal Portatil As Boolean, ByVal FAlta As Date)
        Dim Dias As Integer = Nothing

        Me.Text = "Llamada - [" + Nombre + "]"
        _Cliente = Cliente
        _Celula = Celula
        _Pedido = Pedido
        _Anio = Anio
        _Boletin = Boletin
        _Portatil = Portatil
        _FCompromiso = FCompromiso
        _Ruta = Ruta
        _FAlta = FAlta

        Try
            SqlConnection.Close()
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OkOnly, "Mensaje de sistema")
        End Try

        If Pedido = 0 Then
            lbPedido.Text = ""
        Else
            lbPedido.Text = CType(Pedido, String)
        End If

        txtDemandante.Text = Nombre
        lbOrigen.Text = Origen
        Me.Text += "|Fecha llamada: " + CType(Now.Date, String)

        'Se va al web service a traer el nombre  en ambos casos,   Portatil y Normal .
        'If Not (_URLGateway Is String.Empty Or _URLGateway Is Nothing) Then
        '    Dim oDireccionEntrega As New RTGMCore.DireccionEntrega
        '    oDireccionEntrega = ConsultarDatosCliente(Cliente)

        '    Me.Text = "Llamada - [" + oDireccionEntrega.Nombre + "]"
        '    _Cliente = Cliente
        '    _Celula = Celula
        '    _Pedido = Pedido
        '    _Anio = Anio
        '    _Boletin = Boletin
        '    _Portatil = Portatil
        '    _FCompromiso = FCompromiso
        '    _Ruta = Ruta
        '    _FAlta = FAlta

        '    Me.txtDemandante.Text = oDireccionEntrega.Nombre
        'End If

        LlenaListaAutotanques(_Ruta, True)

        Select Case Boletin
            Case 0
                cmbMotivo.Enabled = True
                lbMotivo.Visible = False
            Case 1
                cmbMotivo.SelectedValue = GLOBAL_MotivoBoletinOperador
                cmbMotivo.Visible = False
                lbMotivo.Visible = False
                lbMotivo.Text = "Boletin al operador"

                cboMotivo2.Visible = True
            Case 2
                cmbMotivo.SelectedValue = GLOBAL_MotivoConfirmacionBoletin
                cmbMotivo.Visible = False
                lbMotivo.Visible = True
                lbMotivo.Text = "Confirmación de boletin"
        End Select

        ConsultacoloniasPorcliente()

        If Global_MuestraNoValidarGPS Then
            chkNoValidarGPS.Visible = True
        End If

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
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents lbTipoLlamada As System.Windows.Forms.Label
    Friend WithEvents lbSentidoLlamada As System.Windows.Forms.Label
    Friend WithEvents lbPedido As System.Windows.Forms.Label
    Friend WithEvents cmbAutoTanque As System.Windows.Forms.ComboBox
    Friend WithEvents cmbOperador As System.Windows.Forms.ComboBox
    Friend WithEvents lbOrigen As System.Windows.Forms.Label
    Friend WithEvents lbDestino As System.Windows.Forms.Label
    Friend WithEvents lbExtDestino As System.Windows.Forms.Label
    Friend WithEvents txtDemandante As System.Windows.Forms.TextBox
    Friend WithEvents cmdCMotivo As System.Data.SqlClient.SqlCommand
    Friend WithEvents daMotivo As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCAutoTanque As System.Data.SqlClient.SqlCommand
    Friend WithEvents daAutotanque As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCOperador As System.Data.SqlClient.SqlCommand
    Friend WithEvents daOperador As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLlamada1 As Sigamet.dsLlamada
    Friend WithEvents DsLlamada As Sigamet.dsLlamada
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents cmbMotivo As System.Windows.Forms.ComboBox
    Friend WithEvents lbMotivo As System.Windows.Forms.Label
    Friend WithEvents cboMotivo2 As SigaMetClasses.Combos.ComboMotivoLlamada
    Friend WithEvents chkFSiguienteLlamada As System.Windows.Forms.CheckBox
    Friend WithEvents lblSigFecha As SVCC.BlinkingClickLabel
    Friend WithEvents dtpFechaProximaLlamada As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkBoletinarOtraRuta As System.Windows.Forms.CheckBox
    Friend WithEvents lblMensaje As System.Windows.Forms.Label
    Friend WithEvents lblCelulaRuta As System.Windows.Forms.Label
    Friend WithEvents cmbCelula As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRuta As SigaMetClasses.Combos.ComboRutaBoletin
    Friend WithEvents grdHorario As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Llamada))
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.DsLlamada = New Sigamet.dsLlamada()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbTipoLlamada = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lbSentidoLlamada = New System.Windows.Forms.Label()
        Me.cmbAutoTanque = New System.Windows.Forms.ComboBox()
        Me.cmbOperador = New System.Windows.Forms.ComboBox()
        Me.lbOrigen = New System.Windows.Forms.Label()
        Me.lbDestino = New System.Windows.Forms.Label()
        Me.lbExtDestino = New System.Windows.Forms.Label()
        Me.txtDemandante = New System.Windows.Forms.TextBox()
        Me.lbPedido = New System.Windows.Forms.Label()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.cmdCMotivo = New System.Data.SqlClient.SqlCommand()
        Me.daMotivo = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCAutoTanque = New System.Data.SqlClient.SqlCommand()
        Me.daAutotanque = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCOperador = New System.Data.SqlClient.SqlCommand()
        Me.daOperador = New System.Data.SqlClient.SqlDataAdapter()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.cmbMotivo = New System.Windows.Forms.ComboBox()
        Me.lbMotivo = New System.Windows.Forms.Label()
        Me.cboMotivo2 = New SigaMetClasses.Combos.ComboMotivoLlamada()
        Me.dtpFechaProximaLlamada = New System.Windows.Forms.DateTimePicker()
        Me.chkFSiguienteLlamada = New System.Windows.Forms.CheckBox()
        Me.lblSigFecha = New SVCC.BlinkingClickLabel()
        Me.chkBoletinarOtraRuta = New System.Windows.Forms.CheckBox()
        Me.lblMensaje = New System.Windows.Forms.Label()
        Me.lblCelulaRuta = New System.Windows.Forms.Label()
        Me.cmbCelula = New System.Windows.Forms.ComboBox()
        Me.cmbRuta = New SigaMetClasses.Combos.ComboRutaBoletin()
        Me.grdHorario = New System.Windows.Forms.DataGrid()
        Me.lnkAlertaRAF = New SVCC.BlinkingClickLabel()
        Me.lnkAlertaFinDeDia = New SVCC.BlinkingClickLabel()
        Me.chkNoValidarGPS = New System.Windows.Forms.CheckBox()
        CType(Me.DsLlamada, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdHorario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(8, 36)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Observaciones :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(8, 358)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Origen :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(8, 386)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Destino :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(8, 12)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(52, 13)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Motivo :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(8, 152)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(73, 13)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Tipo llamada :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 303)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(60, 13)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "Operador :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(8, 275)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(74, 13)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Auto tanque :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(312, 386)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(34, 13)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "Ext. :"
        '
        'DsLlamada
        '
        Me.DsLlamada.DataSetName = "dsLlamada"
        Me.DsLlamada.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsLlamada.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnAceptar
        '
        Me.btnAceptar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(199, 528)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(112, 23)
        Me.btnAceptar.TabIndex = 12
        Me.btnAceptar.Text = "&Aceptar"
        '
        'btnCancelar
        '
        Me.btnCancelar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(319, 528)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(112, 23)
        Me.btnCancelar.TabIndex = 13
        Me.btnCancelar.Text = "&Cancelar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 200)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Pedido :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 330)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "Demandante :"
        '
        'lbTipoLlamada
        '
        Me.lbTipoLlamada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbTipoLlamada.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLlamada, "Motivo.DesTipoLlamada", True))
        Me.lbTipoLlamada.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTipoLlamada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbTipoLlamada.Location = New System.Drawing.Point(164, 148)
        Me.lbTipoLlamada.Name = "lbTipoLlamada"
        Me.lbTipoLlamada.Size = New System.Drawing.Size(264, 20)
        Me.lbTipoLlamada.TabIndex = 3
        Me.lbTipoLlamada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(8, 176)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 26
        Me.Label5.Text = "Sentido :"
        '
        'lbSentidoLlamada
        '
        Me.lbSentidoLlamada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbSentidoLlamada.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsLlamada, "Motivo.DesSentidoLlamada", True))
        Me.lbSentidoLlamada.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbSentidoLlamada.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lbSentidoLlamada.Location = New System.Drawing.Point(164, 172)
        Me.lbSentidoLlamada.Name = "lbSentidoLlamada"
        Me.lbSentidoLlamada.Size = New System.Drawing.Size(264, 20)
        Me.lbSentidoLlamada.TabIndex = 4
        Me.lbSentidoLlamada.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbAutoTanque
        '
        Me.cmbAutoTanque.DataSource = Me.DsLlamada.Autotanque
        Me.cmbAutoTanque.DisplayMember = "Autotanque"
        Me.cmbAutoTanque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAutoTanque.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbAutoTanque.Location = New System.Drawing.Point(164, 272)
        Me.cmbAutoTanque.Name = "cmbAutoTanque"
        Me.cmbAutoTanque.Size = New System.Drawing.Size(70, 21)
        Me.cmbAutoTanque.TabIndex = 6
        Me.cmbAutoTanque.ValueMember = "Autotanque"
        '
        'cmbOperador
        '
        Me.cmbOperador.DataSource = Me.DsLlamada.Operador
        Me.cmbOperador.DisplayMember = "Nombre"
        Me.cmbOperador.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOperador.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbOperador.Location = New System.Drawing.Point(164, 299)
        Me.cmbOperador.Name = "cmbOperador"
        Me.cmbOperador.Size = New System.Drawing.Size(264, 21)
        Me.cmbOperador.TabIndex = 7
        Me.cmbOperador.ValueMember = "Operador"
        '
        'lbOrigen
        '
        Me.lbOrigen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbOrigen.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbOrigen.Location = New System.Drawing.Point(164, 354)
        Me.lbOrigen.Name = "lbOrigen"
        Me.lbOrigen.Size = New System.Drawing.Size(264, 20)
        Me.lbOrigen.TabIndex = 9
        Me.lbOrigen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbDestino
        '
        Me.lbDestino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbDestino.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDestino.Location = New System.Drawing.Point(164, 382)
        Me.lbDestino.Name = "lbDestino"
        Me.lbDestino.Size = New System.Drawing.Size(138, 20)
        Me.lbDestino.TabIndex = 10
        Me.lbDestino.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbExtDestino
        '
        Me.lbExtDestino.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbExtDestino.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbExtDestino.Location = New System.Drawing.Point(372, 382)
        Me.lbExtDestino.Name = "lbExtDestino"
        Me.lbExtDestino.Size = New System.Drawing.Size(56, 20)
        Me.lbExtDestino.TabIndex = 11
        Me.lbExtDestino.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDemandante
        '
        Me.txtDemandante.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDemandante.Location = New System.Drawing.Point(164, 326)
        Me.txtDemandante.Name = "txtDemandante"
        Me.txtDemandante.Size = New System.Drawing.Size(264, 21)
        Me.txtDemandante.TabIndex = 8
        '
        'lbPedido
        '
        Me.lbPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPedido.Location = New System.Drawing.Point(164, 196)
        Me.lbPedido.Name = "lbPedido"
        Me.lbPedido.Size = New System.Drawing.Size(264, 20)
        Me.lbPedido.TabIndex = 5
        Me.lbPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=Digital5000;initial catalog=Sigamet;persist security info=False;user " &
    "id=sa;workstation id=FHURTADO;packet size=4096"
        Me.SqlConnection.FireInfoMessageEventOnUserErrors = False
        '
        'cmdCMotivo
        '
        Me.cmdCMotivo.CommandText = resources.GetString("cmdCMotivo.CommandText")
        Me.cmdCMotivo.Connection = Me.SqlConnection
        '
        'daMotivo
        '
        Me.daMotivo.SelectCommand = Me.cmdCMotivo
        '
        'cmdCAutoTanque
        '
        Me.cmdCAutoTanque.CommandText = "SELECT Autotanque FROM Autotanque WHERE (Ruta = @Ruta)"
        Me.cmdCAutoTanque.Connection = Me.SqlConnection
        Me.cmdCAutoTanque.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Ruta", System.Data.SqlDbType.SmallInt, 2, "Ruta")})
        '
        'daAutotanque
        '
        Me.daAutotanque.SelectCommand = Me.cmdCAutoTanque
        '
        'cmdCOperador
        '
        Me.cmdCOperador.CommandText = resources.GetString("cmdCOperador.CommandText")
        Me.cmdCOperador.Connection = Me.SqlConnection
        Me.cmdCOperador.Parameters.AddRange(New System.Data.SqlClient.SqlParameter() {New System.Data.SqlClient.SqlParameter("@Autotanque", System.Data.SqlDbType.SmallInt, 2, "Autotanque")})
        '
        'daOperador
        '
        Me.daOperador.SelectCommand = Me.cmdCOperador
        '
        'txtObservaciones
        '
        Me.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservaciones.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservaciones.Location = New System.Drawing.Point(164, 32)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservaciones.Size = New System.Drawing.Size(264, 112)
        Me.txtObservaciones.TabIndex = 2
        '
        'cmbMotivo
        '
        Me.cmbMotivo.DataSource = Me.DsLlamada.Motivo
        Me.cmbMotivo.DisplayMember = "Descripcion"
        Me.cmbMotivo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMotivo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMotivo.Location = New System.Drawing.Point(164, 8)
        Me.cmbMotivo.Name = "cmbMotivo"
        Me.cmbMotivo.Size = New System.Drawing.Size(264, 21)
        Me.cmbMotivo.TabIndex = 1
        Me.cmbMotivo.ValueMember = "MotivoLlamada"
        '
        'lbMotivo
        '
        Me.lbMotivo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbMotivo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMotivo.Location = New System.Drawing.Point(164, 8)
        Me.lbMotivo.Name = "lbMotivo"
        Me.lbMotivo.Size = New System.Drawing.Size(264, 21)
        Me.lbMotivo.TabIndex = 27
        Me.lbMotivo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboMotivo2
        '
        Me.cboMotivo2.Location = New System.Drawing.Point(164, 8)
        Me.cboMotivo2.Name = "cboMotivo2"
        Me.cboMotivo2.Size = New System.Drawing.Size(264, 21)
        Me.cboMotivo2.TabIndex = 28
        Me.cboMotivo2.Text = "ComboMotivoLlamada1"
        Me.cboMotivo2.Visible = False
        '
        'dtpFechaProximaLlamada
        '
        Me.dtpFechaProximaLlamada.Enabled = False
        Me.dtpFechaProximaLlamada.Location = New System.Drawing.Point(164, 406)
        Me.dtpFechaProximaLlamada.Name = "dtpFechaProximaLlamada"
        Me.dtpFechaProximaLlamada.Size = New System.Drawing.Size(264, 21)
        Me.dtpFechaProximaLlamada.TabIndex = 29
        '
        'chkFSiguienteLlamada
        '
        Me.chkFSiguienteLlamada.Location = New System.Drawing.Point(11, 407)
        Me.chkFSiguienteLlamada.Name = "chkFSiguienteLlamada"
        Me.chkFSiguienteLlamada.Size = New System.Drawing.Size(16, 20)
        Me.chkFSiguienteLlamada.TabIndex = 30
        '
        'lblSigFecha
        '
        Me.lblSigFecha.AlternatingColor2 = System.Drawing.Color.Red
        Me.lblSigFecha.AutoSize = True
        Me.lblSigFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSigFecha.LinkColor = System.Drawing.Color.Red
        Me.lblSigFecha.Location = New System.Drawing.Point(24, 410)
        Me.lblSigFecha.Name = "lblSigFecha"
        Me.lblSigFecha.Size = New System.Drawing.Size(140, 13)
        Me.lblSigFecha.TabIndex = 31
        Me.lblSigFecha.TabStop = True
        Me.lblSigFecha.Text = "Fecha próxima llamada:"
        Me.lblSigFecha.TimerInterval = 500
        Me.lblSigFecha.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'chkBoletinarOtraRuta
        '
        Me.chkBoletinarOtraRuta.Location = New System.Drawing.Point(324, 220)
        Me.chkBoletinarOtraRuta.Name = "chkBoletinarOtraRuta"
        Me.chkBoletinarOtraRuta.Size = New System.Drawing.Size(112, 24)
        Me.chkBoletinarOtraRuta.TabIndex = 32
        Me.chkBoletinarOtraRuta.Text = "Boletina otra ruta"
        '
        'lblMensaje
        '
        Me.lblMensaje.Location = New System.Drawing.Point(8, 438)
        Me.lblMensaje.Name = "lblMensaje"
        Me.lblMensaje.Size = New System.Drawing.Size(420, 23)
        Me.lblMensaje.TabIndex = 33
        '
        'lblCelulaRuta
        '
        Me.lblCelulaRuta.Location = New System.Drawing.Point(8, 249)
        Me.lblCelulaRuta.Name = "lblCelulaRuta"
        Me.lblCelulaRuta.Size = New System.Drawing.Size(72, 23)
        Me.lblCelulaRuta.TabIndex = 34
        Me.lblCelulaRuta.Text = "Celula/Ruta:"
        Me.lblCelulaRuta.Visible = False
        '
        'cmbCelula
        '
        Me.cmbCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCelula.Location = New System.Drawing.Point(164, 245)
        Me.cmbCelula.Name = "cmbCelula"
        Me.cmbCelula.Size = New System.Drawing.Size(70, 21)
        Me.cmbCelula.TabIndex = 35
        Me.cmbCelula.Visible = False
        '
        'cmbRuta
        '
        Me.cmbRuta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRuta.Enabled = False
        Me.cmbRuta.Location = New System.Drawing.Point(240, 245)
        Me.cmbRuta.Name = "cmbRuta"
        Me.cmbRuta.Size = New System.Drawing.Size(188, 21)
        Me.cmbRuta.TabIndex = 36
        Me.cmbRuta.Visible = False
        '
        'grdHorario
        '
        Me.grdHorario.CaptionVisible = False
        Me.grdHorario.DataMember = ""
        Me.grdHorario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdHorario.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdHorario.Location = New System.Drawing.Point(11, 438)
        Me.grdHorario.Name = "grdHorario"
        Me.grdHorario.PreferredColumnWidth = 82
        Me.grdHorario.ReadOnly = True
        Me.grdHorario.Size = New System.Drawing.Size(420, 84)
        Me.grdHorario.TabIndex = 107
        '
        'lnkAlertaRAF
        '
        Me.lnkAlertaRAF.AlternatingColor2 = System.Drawing.Color.Red
        Me.lnkAlertaRAF.AutoSize = True
        Me.lnkAlertaRAF.BackColor = System.Drawing.Color.Gainsboro
        Me.lnkAlertaRAF.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lnkAlertaRAF.LinkColor = System.Drawing.Color.Red
        Me.lnkAlertaRAF.Location = New System.Drawing.Point(162, 220)
        Me.lnkAlertaRAF.Name = "lnkAlertaRAF"
        Me.lnkAlertaRAF.Size = New System.Drawing.Size(84, 13)
        Me.lnkAlertaRAF.TabIndex = 108
        Me.lnkAlertaRAF.TabStop = True
        Me.lnkAlertaRAF.Text = "Ruta con RAF"
        Me.lnkAlertaRAF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lnkAlertaRAF.TimerInterval = 500
        Me.lnkAlertaRAF.Visible = False
        '
        'lnkAlertaFinDeDia
        '
        Me.lnkAlertaFinDeDia.AlternatingColor2 = System.Drawing.Color.Red
        Me.lnkAlertaFinDeDia.AutoSize = True
        Me.lnkAlertaFinDeDia.BackColor = System.Drawing.Color.Gainsboro
        Me.lnkAlertaFinDeDia.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold)
        Me.lnkAlertaFinDeDia.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lnkAlertaFinDeDia.Location = New System.Drawing.Point(12, 528)
        Me.lnkAlertaFinDeDia.Name = "lnkAlertaFinDeDia"
        Me.lnkAlertaFinDeDia.Size = New System.Drawing.Size(122, 13)
        Me.lnkAlertaFinDeDia.TabIndex = 109
        Me.lnkAlertaFinDeDia.TabStop = True
        Me.lnkAlertaFinDeDia.Text = "Fin de día realizado."
        Me.lnkAlertaFinDeDia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lnkAlertaFinDeDia.TimerInterval = 500
        Me.lnkAlertaFinDeDia.Visible = False
        '
        'chkNoValidarGPS
        '
        Me.chkNoValidarGPS.Location = New System.Drawing.Point(240, 270)
        Me.chkNoValidarGPS.Name = "chkNoValidarGPS"
        Me.chkNoValidarGPS.Size = New System.Drawing.Size(196, 24)
        Me.chkNoValidarGPS.TabIndex = 110
        Me.chkNoValidarGPS.Text = "No validar posición GPS de cliente"
        Me.chkNoValidarGPS.Visible = False
        '
        'Llamada
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(438, 562)
        Me.Controls.Add(Me.chkNoValidarGPS)
        Me.Controls.Add(Me.lnkAlertaFinDeDia)
        Me.Controls.Add(Me.lnkAlertaRAF)
        Me.Controls.Add(Me.grdHorario)
        Me.Controls.Add(Me.cmbRuta)
        Me.Controls.Add(Me.cmbCelula)
        Me.Controls.Add(Me.lblCelulaRuta)
        Me.Controls.Add(Me.lblMensaje)
        Me.Controls.Add(Me.chkBoletinarOtraRuta)
        Me.Controls.Add(Me.lblSigFecha)
        Me.Controls.Add(Me.chkFSiguienteLlamada)
        Me.Controls.Add(Me.dtpFechaProximaLlamada)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me.lbPedido)
        Me.Controls.Add(Me.txtDemandante)
        Me.Controls.Add(Me.lbExtDestino)
        Me.Controls.Add(Me.lbDestino)
        Me.Controls.Add(Me.lbOrigen)
        Me.Controls.Add(Me.cmbOperador)
        Me.Controls.Add(Me.cmbAutoTanque)
        Me.Controls.Add(Me.lbSentidoLlamada)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lbTipoLlamada)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.cmbMotivo)
        Me.Controls.Add(Me.lbMotivo)
        Me.Controls.Add(Me.cboMotivo2)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Llamada"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Llamada - [Cliente]"
        CType(Me.DsLlamada, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdHorario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub cmbMotivo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.BindingContext(DsLlamada, "Motivo").Position = cmbMotivo.SelectedIndex
    End Sub

    Private Sub cmbAutoTanque_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles cmbAutoTanque.SelectedIndexChanged     
        'cmdCOperador.Parameters("@Autotanque").Value = cmbAutoTanque.SelectedValue
        'DsLlamada.Operador.Clear()        
        'daOperador.Fill(DsLlamada, "Operador")
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If CType(cmbAutoTanque.SelectedValue, Int32) <> Nothing OrElse _Boletin <> 2 Then
            Me.Cursor = Cursors.WaitCursor

            'LUSATE Registrar RutaBoletin            
            If chkBoletinarOtraRuta.Checked Then
                _RutaBoletin = cmbRuta.Ruta
            Else
                _RutaBoletin = _Ruta
            End If


            'LUSATE Restringe el boletinado en caso de que la unidad tenga reporte de RAF y no pueda operar.
            If Not RAFPuedeContinuar Then
                MessageBox.Show("La ruta tiene reporte de RAF y no puede seguir operando, no es posible boletinarle pedidos.", "Reporte de RAF", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            '04/09/2014: Se separa esta sección del código para evitar ingresar a la transacción si falla el proceso de pegasus
            '03/10/2018: Se agrega validación de la variable _SGCWebHabilitado  - RM
            If (boletinEnLinea AndAlso _SGCWebHabilitado) Then
                Try
                    Dim servicioPedido As New desarrollogm.Pedido()
                    servicioPedido.Url = GLOBAL_URLWebserviceBoletin

                    If chkNoValidarGPS.Checked Then
                        servicioPedido.AltaPedidoConfiguracionAnulada(Main.GLOBAL_Estacion, "GPS", _Celula, _Anio, _Pedido, GLOBAL_Usuario, String.Empty)
                    End If

                    servicioPedido.BoletinarPedido(Main.GLOBAL_Estacion, GLOBAL_Usuario, pedidoReferencia,
                        False, Convert.ToInt32(cmbAutoTanque.SelectedValue), idPlantaSGC, txtObservaciones.Text)
                Catch ex As Exception
                    MessageBox.Show("No fué posible enviar el pedido al sistema SGCWEB," & vbCrLf &
                        "no se registrará el boletín en SIGAMET." & vbCrLf &
                        ex.Message,
                        "Error enviando pedido", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try

            ElseIf _FuenteGateway.Equals("CRM") AndAlso _Boletin = frmBoletin.enumTipoLlamada.Cliente Then
                Try
                    BoletinarPedidoEnCRM(_Pedido, _Celula)
                Catch ex As Exception
                    MessageBox.Show("No fue posible boletinar el pedido en CRM," & vbCrLf &
                        "no se registrará el boletín en SIGAMET." & vbCrLf &
                        ex.Message,
                        "Error enviando pedido", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try
            End If

            ''LUSATE Rutinas MobilGas
            If _Boletin = 2 And _Portatil = True Then
                BoletinarPedidoPortatil()
            End If

            Dim Transaccion As SqlClient.SqlTransaction
            Dim cmdInsert As New SqlClient.SqlCommand()
            Dim rdrInsert As SqlClient.SqlDataReader = Nothing
            Dim SiGrabo As Boolean

            cmdInsert.Connection = SqlConnection
            SiGrabo = False
            Transaccion = SqlConnection.BeginTransaction
            cmdInsert.Transaction = Transaccion
            Try
                cmdInsert.CommandType = CommandType.StoredProcedure
                cmdInsert.CommandTimeout = 100
                cmdInsert.CommandText = "sp_InsertaLlamada"
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                cmdInsert.Parameters.Add("@TelefonoOrigen", SqlDbType.Char).Value = lbOrigen.Text
                If _Portatil Then
                    cmdInsert.CommandText = "spCCInsertaLlamadaPortatil"
                End If
                If _Boletin = 2 Then
                    cmbMotivo.SelectedValue = GLOBAL_MotivoConfirmacionBoletin
                Else
                    If _Boletin = 1 Then
                        'cmbMotivo.SelectedValue = GLOBAL_MotivoBoletinOperador
                        cmbMotivo.SelectedValue = cboMotivo2.SelectedValue
                    End If
                End If

                cmdInsert.Parameters.Add("@MotivoLlamada", SqlDbType.Int).Value = cmbMotivo.SelectedValue

                If _Pedido = 0 Then
                    cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = 0
                    cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = 0
                    cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = 0
                Else
                    cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                    cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = _Anio
                    cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                End If

                If cmbOperador.Text = "" Then
                    cmdInsert.Parameters.Add("@Operador", SqlDbType.Int).Value = 0
                Else
                    cmdInsert.Parameters.Add("@Operador", SqlDbType.Int).Value = cmbOperador.SelectedValue
                End If

                If cmbAutoTanque.Text = "" Then
                    cmdInsert.Parameters.Add("@Autotanque", SqlDbType.Int).Value = 0
                Else
                    cmdInsert.Parameters.Add("@Autotanque", SqlDbType.Int).Value = cmbAutoTanque.SelectedValue
                End If

                cmdInsert.Parameters.Add("@Demandante", SqlDbType.Char).Value = txtDemandante.Text

                If (chkFSiguienteLlamada.Checked) Then
                    cmdInsert.Parameters.Add("@FProximaLlamada", SqlDbType.DateTime).Value = dtpFechaProximaLlamada.Value.Date
                End If

                cmdInsert.ExecuteNonQuery()

                Transaccion.Commit()

                'texis()
                'If (boletinEnLinea) Then
                '    Dim servicioPedido As New desarrollogm.Pedido()

                '    servicioPedido.Url = GLOBAL_URLWebserviceBoletin

                '    servicioPedido.BoletinarPedido(Main.GLOBAL_Estacion, GLOBAL_Usuario, pedidoReferencia, False, Convert.ToInt32(cmbAutoTanque.SelectedValue), idPlantaSGC, txtObservaciones.Text)
                'End If
                '
                Me.Cursor = Cursors.Default
                SiGrabo = True
                Me.DialogResult = DialogResult.OK
                Me.Tag = True
            Catch er As Exception
                Transaccion.Rollback()
                MsgBox(er.Message, MsgBoxStyle.Critical)
                SiGrabo = False
                Me.DialogResult = DialogResult.Cancel
                Me.Tag = False
            Finally
                If SiGrabo Then
                    boletinEnLinea = False
                    If (boletinEnLinea = False) Then
                        If _Boletin = 2 And _Portatil = False Then

                            'Se actualiza pedido, tanto en la base como en el WS .
                            ' 03/10/2018: Se actualiza el pedido solo en la base de datos   - RM
                            Llamada_ActualizaPedido(_Anio, _Celula, _Pedido, _RutaBoletin)

                        End If
                    End If

                    If _Boletin = 0 And _Portatil And cmbMotivo.SelectedValue = 4 And _Pedido <> 0 Then
                        cmdInsert.CommandText = "spCCPedidoPortatilStatus"
                        cmdInsert.Parameters.Clear()
                        cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                        cmdInsert.Parameters.Add("@Status", SqlDbType.Char).Value = "ACTIVO"
                        cmdInsert.Parameters.Add("@Rutaboletin", SqlDbType.SmallInt).Value = DBNull.Value
                        cmdInsert.Parameters.Add("@StatusGM", SqlDbType.VarChar).Value = DBNull.Value
                        Try
                            cmdInsert.ExecuteNonQuery()
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                        End Try
                    End If
                End If
            End Try

            Me.Close()
        Else
            MsgBox("No hay autotanque seleccionado, Favor de verificar", MsgBoxStyle.Information)
        End If

    End Sub


    Public Function Llamada_ActualizaPedido(ByVal Anio As Integer, ByVal Celula As String, ByVal Pedido As Integer, RutaBoletin As String) As Boolean

        Dim cmdInsert As New SqlClient.SqlCommand()

        Dim rdrInsert As SqlClient.SqlDataReader = Nothing
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.CommandTimeout = 100
        cmdInsert.Connection = SqlConnection

        cmdInsert.CommandText = "spCCActualizaBoletinEnPedido"
        cmdInsert.Parameters.Clear()
        cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = Anio
        cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
        cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
        cmdInsert.Parameters.Add("@Rutaboletin", SqlDbType.SmallInt).Value = RutaBoletin

        Try
            cmdInsert.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'Try
        '    If Not (_URLGateway Is String.Empty Or _URLGateway Is Nothing) Then
        '        Dim objGateway As RTGMGateway.RTGMActualizarPedido = New RTGMGateway.RTGMActualizarPedido(_Modulo, _CadenaConexion)
        '        objGateway.URLServicio = _URLGateway

        '        'Se arma el pedido con los datos que llegan a la funciòn.

        '        Dim objPedido As New RTGMCore.PedidoCRMDatos
        '        objPedido.IDEmpresa = GLOBAL_Corporativo
        '        objPedido.IDZona = Celula
        '        'objPedido.AnioPed = Anio
        '        objPedido.EstatusBoletin = "BOLETINADO"
        '        'objPedido.PedidoReferencia = Pedido
        '        objPedido.IDPedido = Pedido
        '        Dim ListaPedidos As List(Of RTGMCore.Pedido) = New List(Of RTGMCore.Pedido)()

        '        ListaPedidos.Add(objPedido)

        '        Dim oSolicitudActualizarPedido As RTGMGateway.SolicitudActualizarPedido = New RTGMGateway.SolicitudActualizarPedido With {
        '            .Pedidos = ListaPedidos,
        '            .Portatil = False,
        '            .TipoActualizacion = RTGMCore.TipoActualizacion.Boletin
        '        }

        '        Dim ListaRespuesta As List(Of RTGMCore.Pedido) = objGateway.ActualizarPedido(oSolicitudActualizarPedido)
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show("Error actualizando el pedido en CRM." & vbCrLf & ex.Message,
        '                    ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End Try
    End Function

    ''' <summary>
    ''' Boletina el pedido en el CRM
    ''' </summary>
    Private Sub BoletinarPedidoEnCRM(ByVal pedido As Integer, ByVal celula As Integer)

        Try
            If Not (_URLGateway Is String.Empty Or _URLGateway Is Nothing) Then
                Dim objGateway As RTGMGateway.RTGMActualizarPedido = New RTGMGateway.RTGMActualizarPedido(_Modulo, _CadenaConexion)
                objGateway.URLServicio = _URLGateway

                Dim objPedido As New RTGMCore.PedidoCRMDatos
                objPedido.IDEmpresa = GLOBAL_Corporativo
                objPedido.EstatusBoletin = "BOLETINADO"
                objPedido.IDPedido = pedido
                objPedido.IDZona = celula

                Dim ListaPedidos As List(Of RTGMCore.Pedido) = New List(Of RTGMCore.Pedido)()

                ListaPedidos.Add(objPedido)

                Dim oSolicitudActualizarPedido As RTGMGateway.SolicitudActualizarPedido = New RTGMGateway.SolicitudActualizarPedido With {
                    .Pedidos = ListaPedidos,
                    .Portatil = False,
                    .TipoActualizacion = RTGMCore.TipoActualizacion.Boletin
                }

                Dim ListaRespuesta As List(Of RTGMCore.Pedido) = objGateway.ActualizarPedido(oSolicitudActualizarPedido)

                If (Not ListaRespuesta(0).Success AndAlso
                        Not IsNothing(ListaRespuesta(0).Message)) Then

                    Throw New Exception(ListaRespuesta(0).Message)
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Funcion para encontrar la referencia de la celda del grid de pedidos
    Public Function getCelda() As Integer
        Return _numeroCelda
    End Function

    'Funcion para referenciar la celda del grid de pedidos
    Public Function setCelda(ByVal numVal As Integer) As Integer
        _numeroCelda = numVal
    End Function

    Private Sub Llamada_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Sub Llamada_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboMotivo2.CargaDatos()
		'If GLOBAL_UsarMobileGas And _Portatil = True Then
		'    'ConsultaOperadorAutotanque()
		'End If

		'LUSATE Consulta RAF por autotanque
		If cmbAutoTanque.SelectedIndex <> -1 Then
			ConsultaRAFPorRutaAutotanque(cmbAutoTanque.SelectedValue)
			If RAF <> "" Then
				lnkAlertaRAF.Text = RAF
				lnkAlertaRAF.Visible = True
			Else
				lnkAlertaRAF.Visible = False
			End If
			ConsultaOperadorAutotanque()
			'LUSATE Consulta Fin de día por autotanque
			ConsultaFinDeDia(cmbAutoTanque.SelectedValue)
		Else
			lnkAlertaRAF.Visible = False
			lnkAlertaFinDeDia.Visible = False
		End If

	End Sub

    Private Sub chkFSiguienteLlamada_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkFSiguienteLlamada.CheckedChanged
        dtpFechaProximaLlamada.Enabled = chkFSiguienteLlamada.Checked
    End Sub
    'LUSATE Rutina para llenar Horarios por Ruta por colonia
    Private Function fncHorario(ByVal Ruta As Integer, ByVal Colonia As Integer) As DataTable
        Dim retTable As New DataTable("Horarios")
        Dim cmdSelect As New SqlClient.SqlCommand()
        cmdSelect.CommandText = "sp_HorarioRutaGridV3"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Connection = CnnSigamet
        cmdSelect.Parameters.Add("@Ruta", SqlDbType.Int).Value = Ruta
        cmdSelect.Parameters.Add("@Colonia", SqlDbType.Int).Value = Colonia
        Dim daHorario As New SqlClient.SqlDataAdapter(cmdSelect)
        Try
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If
            daHorario.Fill(retTable)
        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error no." & CStr(ex.Number) & Chr(13) &
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error." & Chr(13) &
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()
            daHorario.Dispose()
        End Try
        Return retTable
    End Function
    Private Sub ConsultaAutotanquesPorRutaPortatil(ByVal RutaConsulta As Integer)
        If GLOBAL_VersionMovilGas = 1 Then
            cmdCAutoTanque.Parameters.Clear()
            cmdCAutoTanque.CommandText = "spCCConsultaAttOperadorPorRuta"
            cmdCAutoTanque.CommandType = CommandType.StoredProcedure

            cmdCAutoTanque.Parameters.Add("@Ruta", System.Data.SqlDbType.Int).Value = RutaConsulta
            If _Boletin = 2 Then
                cmdCAutoTanque.Parameters.Add("@BoletinEnLinea", System.Data.SqlDbType.Bit).Value = 1
            ElseIf _Boletin = 3 Then
                cmdCAutoTanque.Parameters.Add("@BoletinEnLinea", System.Data.SqlDbType.Bit).Value = 0
            End If
        ElseIf GLOBAL_VersionMovilGas = 2 Or GLOBAL_VersionMovilGas = 3 Then
            cmdCAutoTanque.Parameters.Clear()
            cmdCAutoTanque.CommandText = "spCCConsultaAutotanquesPorRutaPortatil"
            cmdCAutoTanque.CommandType = CommandType.StoredProcedure

            cmdCAutoTanque.Parameters.Add("@Ruta", System.Data.SqlDbType.Int).Value = RutaConsulta
            If _FCompromiso = Nothing Then
                cmdCAutoTanque.Parameters.Add("@FCompromiso", System.Data.SqlDbType.DateTime).Value = DBNull.Value
            Else
                cmdCAutoTanque.Parameters.Add("@FCompromiso", System.Data.SqlDbType.DateTime).Value = _FCompromiso
            End If
        End If
    End Sub
    Private Sub ConsultaOperadorAutotanque()
        ''LUSATE                   
        If GLOBAL_VersionMovilGas <> 3 And Not _Portatil Then
            DsLlamada.Operador.Clear()
            If (DsLlamada.Operador.Rows.Count > 0) Then
                daAutotanque.Fill(DsLlamada, "Operador")
            End If
            If IsNothing(cmbOperador.DataSource) Then
                cmbOperador.DataSource = DsLlamada.Operador
                cmbOperador.DisplayMember = "Nombre"
                cmbOperador.ValueMember = "Operador"
            End If

        End If
    End Sub

    Private Sub cmbAutoTanque_SelectedIndexChanged_1(sender As System.Object, e As System.EventArgs) Handles cmbAutoTanque.SelectedIndexChanged
        If GLOBAL_VersionMovilGas = 1 And cmbAutoTanque.SelectedIndex <> -1 And _Portatil Then
            _UsuarioMovil = 0
            cmbOperador.SelectedValue = cmbAutoTanque.SelectedValue
        ElseIf GLOBAL_VersionMovilGas = 3 And cmbAutoTanque.SelectedIndex <> -1 And _Portatil Then
            cmbOperador.SelectedIndex = cmbAutoTanque.SelectedIndex
        End If

        'LUSATE Consulta RAF por autotanque
        If cmbAutoTanque.SelectedIndex <> -1 Then
            ConsultaRAFPorRutaAutotanque(cmbAutoTanque.SelectedValue)
            If RAF <> "" Then
                lnkAlertaRAF.Text = RAF
                lnkAlertaRAF.Visible = True
            Else
                lnkAlertaRAF.Visible = False
            End If

            'LUSATE Consulta Fin de día por autotanque
            ConsultaFinDeDia(cmbAutoTanque.SelectedValue)
        Else
            lnkAlertaRAF.Visible = False
            lnkAlertaFinDeDia.Visible = False
        End If

    End Sub
    Private Sub ConsultaRAFPorRutaAutotanque(ByVal AutotanqueRAF As Integer)
        Cursor = Cursors.WaitCursor
        Dim ExisteRAF As String = ""
        Dim cmdRAF As New SqlCommand("spCCConsultaRAFPorRuta", CnnSigamet)
        Dim drRAF As SqlDataReader

        RAF = ""
        RAFPuedeContinuar = True

        cmdRAF.CommandType = CommandType.StoredProcedure
        cmdRAF.Parameters.Add("@Autotanque", SqlDbType.Int).Value = AutotanqueRAF
        Try
            AbreConexion()
            drRAF = cmdRAF.ExecuteReader()
            If drRAF.HasRows Then
                drRAF.Read()
                RAF = CType(drRAF("PuedeOperar"), String)
                RAFPuedeContinuar = CType(drRAF("EnCondicionesParaOperar"), Boolean)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            cmdRAF.Dispose()
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ConsultaFinDeDia(AutotanqueFinDeDia)
        Dim cmdSelect As New SqlClient.SqlCommand()
        cmdSelect.CommandText = "spConsultaFinDeDia"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Connection = CnnSigamet
        cmdSelect.Parameters.Add("@Autotanque", SqlDbType.SmallInt).Value = AutotanqueFinDeDia

        Dim drFinDia As SqlClient.SqlDataReader

        Try
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If

            drFinDia = cmdSelect.ExecuteReader()
            drFinDia.Read()
            If CType(drFinDia("Llamada"), Integer) <> 0 Then
                lnkAlertaFinDeDia.Visible = True
            End If


        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error no." & CStr(ex.Number) & Chr(13) &
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error." & Chr(13) &
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()

        End Try
    End Sub

    Public Function EsBoletinadoPedidoPortatil(PedidoPtl As Integer) As Boolean
        Dim EsBoletinado As Boolean = True
        Dim cmdSelect As New SqlClient.SqlCommand()
        cmdSelect.CommandText = "spCCConsultaEstatusPedidoPortatil"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Connection = CnnSigamet
        cmdSelect.Parameters.Add("@PedidoPortatil", SqlDbType.Int).Value = PedidoPtl

        Dim drBoletinado As SqlClient.SqlDataReader

        Try
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If

            drBoletinado = cmdSelect.ExecuteReader()
            drBoletinado.Read()
            If CType(drBoletinado("StatusMG"), String) = "" Then
                EsBoletinado = False
            End If

        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error no." & CStr(ex.Number) & Chr(13) &
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error." & Chr(13) &
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()

        End Try

        Return EsBoletinado
    End Function

    Private Sub BoletinarPedidoPortatil()
        If Not EsBoletinadoPedidoPortatil(_Pedido) Then
            If GLOBAL_VersionMovilGas = 1 Then
                _AutoTanque = CInt(cmbAutoTanque.SelectedValue)
                _UsuarioMovil = cmbAutoTanque.SelectedItem("UsuarioMovil")

                Try
                    Dim MGRutinas As New MobileGas.MobileGas(GLOBAL_ConString, GLOBAL_MGConnectionString)
                    Dim PedRegistrado As Boolean = MGRutinas.InsertaPedidoMobileGas(_Pedido, _Cliente, _FCompromiso, _UsuarioMovil, _AutoTanque, _RutaBoletin, _FAlta)
                    If PedRegistrado = False Then
                        MessageBox.Show("No fué posible enviar el pedido al sistema Móvil Gas," & vbCrLf &
                        "no se registrará el boletín en SIGAMET." & vbCrLf &
                        "No es posible boletinar pedidos de otros días.",
                        "Error enviando pedido", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Me.Cursor = Cursors.Default
                        Return
                    End If
                Catch ex As Exception
                    MessageBox.Show("No fué posible enviar el pedido al sistema Móvil Gas," & vbCrLf &
                        "no se registrará el boletín en SIGAMET." & vbCrLf &
                        ex.Message,
                        "Error enviando pedido", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try

                'EVH .   2018/07/23 Se agrega la funcionalidad para boletinar un pedido de Portàtil . 
                If Not (_URLGateway Is String.Empty Or _URLGateway Is Nothing) Then

                    Try
                        Dim objGateway As RTGMGateway.RTGMActualizarPedido = New RTGMGateway.RTGMActualizarPedido(_Modulo, _CadenaConexion)
                        objGateway.URLServicio = _URLGateway
                        Dim lstPedido As List(Of RTGMCore.Pedido) = New List(Of RTGMCore.Pedido)()
                        lstPedido.Add(New RTGMCore.PedidoCRMSaldo With {
                                    .IDPedido = _Pedido,
                                    .IDZona = _Celula,
                                    .AnioPed = _Anio,
                                    .PedidoReferencia = _Pedido
            })
                        Dim Solicitud As RTGMGateway.SolicitudActualizarPedido = New RTGMGateway.SolicitudActualizarPedido With {
                                    .Pedidos = lstPedido,
                                    .Portatil = True,
                                    .TipoActualizacion = RTGMCore.TipoActualizacion.Boletin,
                                    .Usuario = "ROPIMA"
            }

                        Dim ListaRespuesta As List(Of RTGMCore.Pedido) = objGateway.ActualizarPedido(Solicitud)

                    Catch ex As Exception


                    End Try


                End If



            Else
                _AutoTanque = CInt(cmbAutoTanque.SelectedValue)
                Dim cmd As New SqlCommand("spCCPedidoPortatilStatus", CnnSigamet)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                cmd.Parameters.Add("@Status", SqlDbType.Char).Value = "RADIADO"
                cmd.Parameters.Add("@Rutaboletin", SqlDbType.SmallInt).Value = _RutaBoletin
                cmd.Parameters.Add("@StatusGM", SqlDbType.VarChar).Value = "NUEVO"
                cmd.Parameters.Add("@AutotanqueMG", SqlDbType.Int).Value = _AutoTanque
                AbreConexion()
                Try
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    CierraConexion()
                    cmd.Dispose()
                End Try
            End If
        Else
            Dim cmd As New SqlCommand("spCCPedidoPortatilStatus", CnnSigamet)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
            cmd.Parameters.Add("@Status", SqlDbType.Char).Value = "RADIADO"
            cmd.Parameters.Add("@Rutaboletin", SqlDbType.SmallInt).Value = DBNull.Value
            cmd.Parameters.Add("@StatusGM", SqlDbType.VarChar).Value = DBNull.Value
            AbreConexion()
            Try
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If
    End Sub
    Private Sub ConsultacoloniasPorcliente()
        'LUSATE
        Dim cmd As New SqlCommand("SELECT Colonia FROM Cliente WHERE Cliente = @Cliente", Me.SqlConnection)
        cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente

        If SqlConnection.State = ConnectionState.Closed Then
            SqlConnection.Open()
        End If

        Try
            _Colonia = CType(cmd.ExecuteScalar, Integer)
        Catch ex As Exception
            Throw ex
        Finally
            cmd.Dispose()
        End Try

        grdHorario.DataSource = fncHorario(rutaPed, _Colonia)
        ''
    End Sub

    Private Sub LlenaListaAutotanques(ByVal RutaConsulta As Integer, ByVal Inicio As Boolean)
        ''LUSATE       
        If GLOBAL_UsarSigametServices Then
            If Inicio Then
                cmdCAutoTanque.Parameters("@Ruta").Value = RutaConsulta
                daAutotanque.Fill(DsLlamada, "Autotanque")
            Else
                ConsultaAutotanquesPorDia(RutaConsulta, False)
            End If
        Else
            DsLlamada.Autotanque.Clear()
            cmbAutoTanque.DataSource = Nothing
            cmbAutoTanque.Items.Clear()

            DsLlamada.Operador.Clear()
            cmbOperador.DataSource = Nothing
            cmbOperador.Items.Clear()
            If _Portatil Then
                ConsultaAutotanquesPorRutaPortatil(RutaConsulta)
                daAutotanque.Fill(Me.DsLlamada.Operador)
                Me.cmbOperador.DataSource = Me.DsLlamada.Operador
                Me.cmbOperador.DisplayMember = "Nombre"
                Me.cmbOperador.ValueMember = "Operador"
            Else
                cmdCAutoTanque.Parameters("@Ruta").Value = RutaConsulta
            End If
            daAutotanque.Fill(Me.DsLlamada.Autotanque)
        End If

		'If Me.cmbAutoTanque.Items.Count > 0 Then
		'	Me.cmbAutoTanque.DataSource = Me.DsLlamada.Autotanque
		'	Me.cmbAutoTanque.DisplayMember = "Autotanque"
		'	Me.cmbAutoTanque.ValueMember = "Autotanque"
		'End If

		' 17/10/2018 RM - Siempre cargar la tabla Motivo
		daMotivo.Fill(DsLlamada, "Motivo")
    End Sub

    Private Sub ConsultaCelulas()
        Dim cmdCelula As New SqlCommand
        cmdCelula.Connection = CnnSigamet
        If GLOBAL_CelulasUsuario Then
            cmdCelula.CommandText = "spSEGUsuarioCelulaConsulta"
            cmdCelula.CommandType = CommandType.StoredProcedure
            cmdCelula.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = GLOBAL_Usuario
        Else
            cmdCelula.CommandText = "Select Celula,Descripcion from Celula where Comercial=1 and Celula<>0 order by Celula "
        End If

        Dim daCelula As New SqlDataAdapter(cmdCelula)
        Dim dtCelula As New DataTable()

        AbreConexion()
        Try
            daCelula.Fill(dtCelula)
            daCel = dtCelula
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error al cargar parametros de boletin " & ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub cmbOperador_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cmbOperador.SelectedIndexChanged
        If GLOBAL_VersionMovilGas = 3 And cmbOperador.SelectedIndex <> -1 And _Portatil Then
            cmbAutoTanque.SelectedIndex = cmbOperador.SelectedIndex
        End If
    End Sub
End Class
