Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.ControlChars
Imports SigaMetClasses.Enumeradores

Public Class frmCallCenter
    Inherits System.Windows.Forms.Form
    Private _Titulo As String = "CallCenter"
    Private _Cliente As Integer
    Private _CelulaCliente As Byte
    Private _RutaCliente As Short
    Private _Empresa As Integer
    Private _StatusCalidad As String
    Public _URLGateway As String

    Private isCargando As Integer = 0

    Private _TelCasa As String

    Private _EsClienteNuevo As Boolean = False 'Indica si es un cliente nuevo
    Private _ClienteCargado As Boolean = False 'Indica si se ha cargado la información de un cliente de la BD
    Private _DatosModificados As Boolean = False  'Indica si los datos han sido modificados

    'Variables para el pedido seleccionado actualmente en el grid
    Private _AñoPed As Short
    Private _Celula As Byte
    Private _Pedido As Integer
    Private _PedidoReferencia As String
    Private _Status As String
    Private _ClientePreCarga As Integer
    Private _PreCarga As Boolean
    Private _SecuenciaEquipo As Integer
    Private _TipoPedido As String
    Private _FAlta As Date

    Private _Producto As String

    Private _Colonia As Integer
    Private _ColoniaNombre As String
    Private _ClientePadreEdificio As Integer

    Private _ClientePadre As Integer

    Private _Referencia As String
    Friend WithEvents btnModificarEquipo As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblCtePadreEdif As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkAlertaRAF As SVCC.BlinkingClickLabel
    Friend WithEvents btnFugas As System.Windows.Forms.ToolBarButton
    Friend WithEvents lnkNoSuministrar As SVCC.BlinkingClickLabel
    Friend WithEvents grpGeoReferencia As System.Windows.Forms.GroupBox
    Friend WithEvents lblValorY As System.Windows.Forms.Label
    Friend WithEvents lblValorX As System.Windows.Forms.Label
    Friend WithEvents lblY As System.Windows.Forms.Label
    Friend WithEvents lblX As System.Windows.Forms.Label
    Friend WithEvents btnGeoreferenciar As System.Windows.Forms.Button

    'Información de pedidos portatil 
    Dim InfoPedidoPortatil As InfoPedidoPortatil

#Region " Windows Form Designer generated code "

    Public Sub New(Optional ByVal URLGateway As String = Nothing)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        _URLGateway = URLGateway

        'Add any initialization after the InitializeComponent() call
        chkPortatil.Visible = GLOBAL_ManejarClientesPortatil
        lblReferencia.Visible = Not GLOBAL_ManejarClientesPortatil
        'TODO: Para el cambio de zona económica de cliente '17-06-2005'
        If Not GLOBAL_ManejarClientesPortatil Then
            activaCambioZona(oSeguridad.TieneAcceso("CambioZonaEconomica"))
        End If
        'se agregó para desctivar el botón at más cercano 29/09/2004
        activaATMasCercano(GLOBAL_AplicaATMasCercano)
        activaClientesHijos(GLOBAL_AplicaAdministracionEdificios)
        activaAvanzarProgramacion(GLOBAL_AvanzaProgramacion)
        SeleccionCalleColonia.AltaCalleColonia = GLOBAL_AltaCalleColonia

        btnTarjeta.Visible = oSeguridad.TieneAcceso("TarjetaCredito")

        'Captura de contrato firmado para sigamet corporativo
        'chkContrato.Visible = GLOBAL_CapturaContratoFirmado 'CONTROLARÁ AHORA EL ESTATUS DE PROSPECTO DE UN CLIENTE.

        lblProspecto.Visible = oSeguridad.TieneAcceso("AltaProspectos")
        chkContrato.Visible = oSeguridad.TieneAcceso("AltaProspectos")

        'lblContratoText.Visible = GLOBAL_CapturaContratoFirmado

        'Captura de factor de comisión por cliente
        lblComision.Visible = GLOBAL_CapturaFactorComision
        lblComisionText.Visible = GLOBAL_CapturaFactorComision

        'Control de Ventas Multinivel
        btnVentasMultinivel.Visible = GLOBAL_VentasMultinivel

        'Modulo de quejas Activo
        btnQueja.Visible = GLOBAL_ModuloQuejas


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
    Friend WithEvents tbBarra As System.Windows.Forms.ToolBar
    Friend WithEvents ToolBarButton7 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton13 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton14 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents imgLista As System.Windows.Forms.ImageList
    Friend WithEvents btnBuscar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents separador2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents separador1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnNuevo As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnGuardar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnModificar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnProgramacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnTanques As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnServicios As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnPedido As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLlamadas As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnHistorico As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnConsultaCliente As System.Windows.Forms.ToolBarButton
    Friend WithEvents imgPedido As System.Windows.Forms.ImageList
    Friend WithEvents ttMensaje As System.Windows.Forms.ToolTip
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuBuscar As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPedido As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCerrar As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRefrescar As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNuevo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGuardar As System.Windows.Forms.MenuItem
    Friend WithEvents mnuModificar As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuServicioTecnico As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCancelacion As System.Windows.Forms.MenuItem
    Friend WithEvents mnuConfiguracion As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents pnlCallCenter As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtObservacionesLlamada As System.Windows.Forms.TextBox
    Friend WithEvents lblListaPedido As System.Windows.Forms.Label
    Friend WithEvents lvwPedido As System.Windows.Forms.ListView
    Friend WithEvents lvwcolPedidoReferencia As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolTipo As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolFPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolFCompromiso As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolFSuministro As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolLitros As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolEstatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolRuta As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolAutotanque As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolUsuario As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolAñoPed As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolCelula As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolFactura As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwcolObservaciones As System.Windows.Forms.ColumnHeader
    Friend WithEvents SeleccionCalleColonia As SigaMetClasses.SeleccionCalleColonia
    Friend WithEvents txtObservacionesCliente As System.Windows.Forms.TextBox
    Friend WithEvents cboOrigenCliente As SigaMetClasses.Combos.ComboOrigenCliente
    Friend WithEvents grdClasificacion As System.Windows.Forms.GroupBox
    Friend WithEvents cboRamoCliente As SigaMetClasses.Combos.ComboRamoCliente
    Friend WithEvents lblTipoFactura As System.Windows.Forms.Label
    Friend WithEvents lblClasificacionCliente As System.Windows.Forms.Label
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents lblFAlta As System.Windows.Forms.Label
    Friend WithEvents btnConsultaEmpresa As System.Windows.Forms.Button
    Friend WithEvents btnSeleccionaEmpresa As System.Windows.Forms.Button
    Friend WithEvents grdLlamada As System.Windows.Forms.DataGrid
    Friend WithEvents grpTelefono As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtTelAlterno2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTelCasa As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtTelAlterno1 As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lblRazonSocial As System.Windows.Forms.Label
    Friend WithEvents lblCelula As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents txtNombre As System.Windows.Forms.TextBox
    Friend WithEvents lblOrigenCliente As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents styLlamada As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents colLLFLlamada As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colLLMotivo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colLLPedido As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colLLUsuario As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colLLOperador As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colLLDemandante As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colLLObservaciones As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnProgramacionOK As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblStatus As SigaMetClasses.Controles.LabelStatus
    Friend WithEvents lblStatusCalidad As SigaMetClasses.Controles.LabelStatus
    Friend WithEvents lvwLlamada As System.Windows.Forms.ListView
    Friend WithEvents lvwCallFLlamada As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwCallDesMotivo As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwCallPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwCallUsuario As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwCallOperador As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwCallObservaciones As System.Windows.Forms.ColumnHeader
    Friend WithEvents lvwCallDemandante As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnAvanzaProgramacion As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnPedidoBitacora As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnNotaAgregar As System.Windows.Forms.Button
    Friend WithEvents btnNotaCerrar As System.Windows.Forms.Button
    Friend WithEvents btnNotaTablero As System.Windows.Forms.Button
    Friend WithEvents btnClientesHijos As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnATMasCercano As System.Windows.Forms.ToolBarButton
    Friend WithEvents EliminarST1 As Botones.EliminarST
    Friend WithEvents btnEliminarEquipo As System.Windows.Forms.Button
    Friend WithEvents chkVIP As System.Windows.Forms.CheckBox
    Friend WithEvents lblVip As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtEmail As System.Windows.Forms.TextBox
    Friend WithEvents chkPortatil As System.Windows.Forms.CheckBox
    Friend WithEvents cboRuta As SigaMetClasses.Combos.ComboRuta2Filtro
    Friend WithEvents lblCreditoExcedido As SVCC.BlinkingClickLabel
    Friend WithEvents btnCambioZona As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnTarjeta As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblCartera As System.Windows.Forms.Label
    Friend WithEvents lblComision As System.Windows.Forms.Label
    Friend WithEvents lblComisionText As System.Windows.Forms.Label
    Friend WithEvents lblContratoText As System.Windows.Forms.Label
    Friend WithEvents chkContrato As System.Windows.Forms.CheckBox
    Friend WithEvents btnVentasMultinivel As System.Windows.Forms.ToolBarButton
    Friend WithEvents grpTanques As System.Windows.Forms.GroupBox
    Friend WithEvents grdClienteEquipo As System.Windows.Forms.DataGrid
    Friend WithEvents colCESecuencia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colCEEquipo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colCETipoPropiedad As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colCESerie As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents styClienteEquipo As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents grpProgramaCliente As System.Windows.Forms.GroupBox
    Friend WithEvents btnCalendario As System.Windows.Forms.Button
    Friend WithEvents lblObservacionesProgramacion As System.Windows.Forms.Label
    Friend WithEvents lblProgramacion As SigaMetClasses.Controles.LabelStatus
    Friend WithEvents grdProgramaCliente As System.Windows.Forms.DataGrid
    Friend WithEvents colProgTexto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents styProgramacion As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents grpVentasMultinivel As System.Windows.Forms.GroupBox
    Friend WithEvents lblSaldo As System.Windows.Forms.Label
    Friend WithEvents lblHijos As System.Windows.Forms.Label
    Friend WithEvents lblClienteOrigen As System.Windows.Forms.Label
    Friend WithEvents lblPromotor As System.Windows.Forms.Label
    Friend WithEvents tbBarra2 As System.Windows.Forms.ToolBar
    Friend WithEvents btnBarra2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnAgregarEquipo As System.Windows.Forms.ToolBarButton    
    Friend WithEvents btnImagenes As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnProgST As System.Windows.Forms.ToolBarButton
    Friend WithEvents lnkQueja As SVCC.BlinkingClickLabel
    Friend WithEvents btnQueja As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnContactos As System.Windows.Forms.Button
    Friend WithEvents lblGiroCliente As System.Windows.Forms.Label
    Friend WithEvents txtLada As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents lblReferencia As System.Windows.Forms.Label
    Friend WithEvents lvwcolProducto As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnSolicitudCredito As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnInfoProspectos As System.Windows.Forms.ToolBarButton
    Friend WithEvents lblNombreEmpresa As NombreEmpresa.LabelNombreEmpresa
    Friend WithEvents lblProspecto As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCallCenter))
        Me.tbBarra = New System.Windows.Forms.ToolBar()
        Me.btnBarra2 = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.separador1 = New System.Windows.Forms.ToolBarButton()
        Me.btnBuscar = New System.Windows.Forms.ToolBarButton()
        Me.btnRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.separador2 = New System.Windows.Forms.ToolBarButton()
        Me.btnNuevo = New System.Windows.Forms.ToolBarButton()
        Me.btnGuardar = New System.Windows.Forms.ToolBarButton()
        Me.btnModificar = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton7 = New System.Windows.Forms.ToolBarButton()
        Me.btnProgramacion = New System.Windows.Forms.ToolBarButton()
        Me.btnProgramacionOK = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton13 = New System.Windows.Forms.ToolBarButton()
        Me.btnTanques = New System.Windows.Forms.ToolBarButton()
        Me.btnServicios = New System.Windows.Forms.ToolBarButton()
        Me.btnProgST = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton14 = New System.Windows.Forms.ToolBarButton()
        Me.btnPedido = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelacion = New System.Windows.Forms.ToolBarButton()
        Me.btnPedidoBitacora = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
        Me.btnLlamadas = New System.Windows.Forms.ToolBarButton()
        Me.btnHistorico = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton1 = New System.Windows.Forms.ToolBarButton()
        Me.btnConsultaCliente = New System.Windows.Forms.ToolBarButton()
        Me.btnQueja = New System.Windows.Forms.ToolBarButton()
        Me.imgLista = New System.Windows.Forms.ImageList(Me.components)
        Me.btnClientesHijos = New System.Windows.Forms.ToolBarButton()
        Me.btnAvanzaProgramacion = New System.Windows.Forms.ToolBarButton()
        Me.btnATMasCercano = New System.Windows.Forms.ToolBarButton()
        Me.btnCambioZona = New System.Windows.Forms.ToolBarButton()
        Me.btnVentasMultinivel = New System.Windows.Forms.ToolBarButton()
        Me.btnTarjeta = New System.Windows.Forms.ToolBarButton()
        Me.imgPedido = New System.Windows.Forms.ImageList(Me.components)
        Me.ttMensaje = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnConsultaEmpresa = New System.Windows.Forms.Button()
        Me.btnSeleccionaEmpresa = New System.Windows.Forms.Button()
        Me.lblCelula = New System.Windows.Forms.Label()
        Me.btnNotaAgregar = New System.Windows.Forms.Button()
        Me.btnNotaCerrar = New System.Windows.Forms.Button()
        Me.btnNotaTablero = New System.Windows.Forms.Button()
        Me.btnEliminarEquipo = New System.Windows.Forms.Button()
        Me.lblCreditoExcedido = New SVCC.BlinkingClickLabel()
        Me.btnContactos = New System.Windows.Forms.Button()
        Me.lblReferencia = New System.Windows.Forms.Label()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mnuBuscar = New System.Windows.Forms.MenuItem()
        Me.mnuRefrescar = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuNuevo = New System.Windows.Forms.MenuItem()
        Me.mnuGuardar = New System.Windows.Forms.MenuItem()
        Me.mnuModificar = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.mnuServicioTecnico = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.mnuPedido = New System.Windows.Forms.MenuItem()
        Me.mnuCancelacion = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuConfiguracion = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mnuCerrar = New System.Windows.Forms.MenuItem()
        Me.pnlCallCenter = New System.Windows.Forms.Panel()
        Me.grpGeoReferencia = New System.Windows.Forms.GroupBox()
        Me.lblValorY = New System.Windows.Forms.Label()
        Me.lblValorX = New System.Windows.Forms.Label()
        Me.lblY = New System.Windows.Forms.Label()
        Me.lblX = New System.Windows.Forms.Label()
        Me.btnGeoreferenciar = New System.Windows.Forms.Button()
        Me.lnkNoSuministrar = New SVCC.BlinkingClickLabel()
        Me.lnkAlertaRAF = New SVCC.BlinkingClickLabel()
        Me.tbBarra2 = New System.Windows.Forms.ToolBar()
        Me.btnAgregarEquipo = New System.Windows.Forms.ToolBarButton()
        Me.btnModificarEquipo = New System.Windows.Forms.ToolBarButton()
        Me.btnImagenes = New System.Windows.Forms.ToolBarButton()
        Me.btnSolicitudCredito = New System.Windows.Forms.ToolBarButton()
        Me.btnInfoProspectos = New System.Windows.Forms.ToolBarButton()
        Me.btnFugas = New System.Windows.Forms.ToolBarButton()
        Me.lblPromotor = New System.Windows.Forms.Label()
        Me.grpVentasMultinivel = New System.Windows.Forms.GroupBox()
        Me.lblClienteOrigen = New System.Windows.Forms.Label()
        Me.lblHijos = New System.Windows.Forms.Label()
        Me.lblSaldo = New System.Windows.Forms.Label()
        Me.chkPortatil = New System.Windows.Forms.CheckBox()
        Me.EliminarST1 = New Botones.EliminarST()
        Me.lvwLlamada = New System.Windows.Forms.ListView()
        Me.lvwCallFLlamada = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwCallDesMotivo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwCallPedido = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwCallUsuario = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwCallOperador = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwCallDemandante = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwCallObservaciones = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.grdLlamada = New System.Windows.Forms.DataGrid()
        Me.styLlamada = New System.Windows.Forms.DataGridTableStyle()
        Me.colLLFLlamada = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colLLMotivo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colLLPedido = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colLLUsuario = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colLLOperador = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colLLDemandante = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colLLObservaciones = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtObservacionesLlamada = New System.Windows.Forms.TextBox()
        Me.lblListaPedido = New System.Windows.Forms.Label()
        Me.lvwPedido = New System.Windows.Forms.ListView()
        Me.lvwcolPedidoReferencia = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolTipo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolFPedido = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolFCompromiso = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolFSuministro = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolLitros = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolEstatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolRuta = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolAutotanque = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolUsuario = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolAñoPed = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolCelula = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolPedido = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolFactura = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolObservaciones = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lvwcolProducto = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtObservacionesCliente = New System.Windows.Forms.TextBox()
        Me.grdClasificacion = New System.Windows.Forms.GroupBox()
        Me.lblGiroCliente = New System.Windows.Forms.Label()
        Me.lblComisionText = New System.Windows.Forms.Label()
        Me.lblComision = New System.Windows.Forms.Label()
        Me.lblVip = New System.Windows.Forms.Label()
        Me.chkVIP = New System.Windows.Forms.CheckBox()
        Me.lblTipoFactura = New System.Windows.Forms.Label()
        Me.lblClasificacionCliente = New System.Windows.Forms.Label()
        Me.lblCartera = New System.Windows.Forms.Label()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.lblFAlta = New System.Windows.Forms.Label()
        Me.grpTelefono = New System.Windows.Forms.GroupBox()
        Me.lblContratoText = New System.Windows.Forms.Label()
        Me.lblProspecto = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtTelAlterno2 = New System.Windows.Forms.TextBox()
        Me.txtTelAlterno1 = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.chkContrato = New System.Windows.Forms.CheckBox()
        Me.lnkQueja = New SVCC.BlinkingClickLabel()
        Me.lblRazonSocial = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.lblOrigenCliente = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.grpTanques = New System.Windows.Forms.GroupBox()
        Me.grdClienteEquipo = New System.Windows.Forms.DataGrid()
        Me.styClienteEquipo = New System.Windows.Forms.DataGridTableStyle()
        Me.colCESecuencia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colCEEquipo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colCETipoPropiedad = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colCESerie = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grpProgramaCliente = New System.Windows.Forms.GroupBox()
        Me.grdProgramaCliente = New System.Windows.Forms.DataGrid()
        Me.styProgramacion = New System.Windows.Forms.DataGridTableStyle()
        Me.colProgTexto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lblObservacionesProgramacion = New System.Windows.Forms.Label()
        Me.btnCalendario = New System.Windows.Forms.Button()
        Me.lblCtePadreEdif = New System.Windows.Forms.LinkLabel()
        Me.lblNombreEmpresa = New NombreEmpresa.LabelNombreEmpresa()
        Me.cboOrigenCliente = New SigaMetClasses.Combos.ComboOrigenCliente()
        Me.lblStatusCalidad = New SigaMetClasses.Controles.LabelStatus()
        Me.lblStatus = New SigaMetClasses.Controles.LabelStatus()
        Me.SeleccionCalleColonia = New SigaMetClasses.SeleccionCalleColonia()
        Me.cboRamoCliente = New SigaMetClasses.Combos.ComboRamoCliente()
        Me.txtLada = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtTelCasa = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.cboRuta = New SigaMetClasses.Combos.ComboRuta2Filtro()
        Me.lblProgramacion = New SigaMetClasses.Controles.LabelStatus()
        Me.pnlCallCenter.SuspendLayout()
        Me.grpGeoReferencia.SuspendLayout()
        Me.grpVentasMultinivel.SuspendLayout()
        CType(Me.grdLlamada, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grdClasificacion.SuspendLayout()
        Me.grpTelefono.SuspendLayout()
        Me.grpTanques.SuspendLayout()
        CType(Me.grdClienteEquipo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpProgramaCliente.SuspendLayout()
        CType(Me.grdProgramaCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tbBarra
        '
        Me.tbBarra.AutoSize = False
        Me.tbBarra.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnBarra2, Me.btnCerrar, Me.separador1, Me.btnBuscar, Me.btnRefrescar, Me.separador2, Me.btnNuevo, Me.btnGuardar, Me.btnModificar, Me.ToolBarButton7, Me.btnProgramacion, Me.btnProgramacionOK, Me.ToolBarButton13, Me.btnTanques, Me.btnServicios, Me.btnProgST, Me.ToolBarButton14, Me.btnPedido, Me.btnCancelacion, Me.btnPedidoBitacora, Me.ToolBarButton4, Me.btnLlamadas, Me.btnHistorico, Me.ToolBarButton1, Me.btnConsultaCliente, Me.btnQueja})
        Me.tbBarra.ButtonSize = New System.Drawing.Size(74, 34)
        Me.tbBarra.Dock = System.Windows.Forms.DockStyle.Right
        Me.tbBarra.DropDownArrows = True
        Me.tbBarra.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbBarra.ImageList = Me.imgLista
        Me.tbBarra.Location = New System.Drawing.Point(935, 0)
        Me.tbBarra.Name = "tbBarra"
        Me.tbBarra.ShowToolTips = True
        Me.tbBarra.Size = New System.Drawing.Size(73, 541)
        Me.tbBarra.TabIndex = 12
        '
        'btnBarra2
        '
        Me.btnBarra2.ImageIndex = 26
        Me.btnBarra2.Name = "btnBarra2"
        Me.btnBarra2.Tag = "Barra2"
        Me.btnBarra2.Text = "Herramientas"
        Me.btnBarra2.ToolTipText = "Herramientas adicionales"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 0
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Tag = "Cerrar"
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.ToolTipText = "Cerrar"
        '
        'separador1
        '
        Me.separador1.Name = "separador1"
        Me.separador1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnBuscar
        '
        Me.btnBuscar.ImageIndex = 1
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Tag = "Buscar"
        Me.btnBuscar.Text = "Buscar"
        Me.btnBuscar.ToolTipText = "Realizar una búsqueda de cliente"
        '
        'btnRefrescar
        '
        Me.btnRefrescar.ImageIndex = 2
        Me.btnRefrescar.Name = "btnRefrescar"
        Me.btnRefrescar.Tag = "Refrescar"
        Me.btnRefrescar.Text = "Refrescar"
        Me.btnRefrescar.ToolTipText = "Refrescar la información"
        '
        'separador2
        '
        Me.separador2.Name = "separador2"
        Me.separador2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnNuevo
        '
        Me.btnNuevo.ImageIndex = 3
        Me.btnNuevo.Name = "btnNuevo"
        Me.btnNuevo.Tag = "Nuevo"
        Me.btnNuevo.Text = "Nuevo"
        Me.btnNuevo.ToolTipText = "Nuevo"
        '
        'btnGuardar
        '
        Me.btnGuardar.ImageIndex = 4
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Tag = "Guardar"
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.ToolTipText = "Guardar"
        '
        'btnModificar
        '
        Me.btnModificar.ImageIndex = 5
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.Tag = "Modificar"
        Me.btnModificar.Text = "Modificar"
        Me.btnModificar.ToolTipText = "Modificar los datos del cliente"
        '
        'ToolBarButton7
        '
        Me.ToolBarButton7.Name = "ToolBarButton7"
        Me.ToolBarButton7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnProgramacion
        '
        Me.btnProgramacion.ImageIndex = 6
        Me.btnProgramacion.Name = "btnProgramacion"
        Me.btnProgramacion.Tag = "Programacion"
        Me.btnProgramacion.Text = "Programación"
        Me.btnProgramacion.ToolTipText = "Programación"
        Me.btnProgramacion.Visible = False
        '
        'btnProgramacionOK
        '
        Me.btnProgramacionOK.ImageIndex = 6
        Me.btnProgramacionOK.Name = "btnProgramacionOK"
        Me.btnProgramacionOK.Tag = "ProgramacionOK"
        Me.btnProgramacionOK.Text = "Programación"
        Me.btnProgramacionOK.ToolTipText = "Programación"
        '
        'ToolBarButton13
        '
        Me.ToolBarButton13.Name = "ToolBarButton13"
        Me.ToolBarButton13.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnTanques
        '
        Me.btnTanques.ImageIndex = 9
        Me.btnTanques.Name = "btnTanques"
        Me.btnTanques.Tag = "Tanques"
        Me.btnTanques.Text = "Tanques"
        Me.btnTanques.ToolTipText = "Tanques"
        Me.btnTanques.Visible = False
        '
        'btnServicios
        '
        Me.btnServicios.ImageIndex = 10
        Me.btnServicios.Name = "btnServicios"
        Me.btnServicios.Tag = "Servicios"
        Me.btnServicios.Text = "Servicios"
        Me.btnServicios.ToolTipText = "Servicios"
        '
        'btnProgST
        '
        Me.btnProgST.ImageIndex = 27
        Me.btnProgST.Name = "btnProgST"
        Me.btnProgST.Tag = "ProgST"
        Me.btnProgST.Text = "Prog. Serv."
        '
        'ToolBarButton14
        '
        Me.ToolBarButton14.Name = "ToolBarButton14"
        Me.ToolBarButton14.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnPedido
        '
        Me.btnPedido.ImageIndex = 11
        Me.btnPedido.Name = "btnPedido"
        Me.btnPedido.Tag = "Pedido"
        Me.btnPedido.Text = "Pedido"
        Me.btnPedido.ToolTipText = "Pedido"
        '
        'btnCancelacion
        '
        Me.btnCancelacion.Enabled = False
        Me.btnCancelacion.ImageIndex = 12
        Me.btnCancelacion.Name = "btnCancelacion"
        Me.btnCancelacion.Tag = "Cancelacion"
        Me.btnCancelacion.Text = "Cancelación"
        Me.btnCancelacion.ToolTipText = "Cancelación"
        '
        'btnPedidoBitacora
        '
        Me.btnPedidoBitacora.Enabled = False
        Me.btnPedidoBitacora.ImageIndex = 18
        Me.btnPedidoBitacora.Name = "btnPedidoBitacora"
        Me.btnPedidoBitacora.Tag = "PedidoBitacora"
        Me.btnPedidoBitacora.Text = "Histórico mod."
        Me.btnPedidoBitacora.ToolTipText = "Histórico de modificaciones del pedido"
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.Name = "ToolBarButton4"
        Me.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnLlamadas
        '
        Me.btnLlamadas.ImageIndex = 13
        Me.btnLlamadas.Name = "btnLlamadas"
        Me.btnLlamadas.Tag = "Llamadas"
        Me.btnLlamadas.Text = "Llamadas"
        Me.btnLlamadas.ToolTipText = "Llamadas"
        '
        'btnHistorico
        '
        Me.btnHistorico.ImageIndex = 15
        Me.btnHistorico.Name = "btnHistorico"
        Me.btnHistorico.Tag = "Historico"
        Me.btnHistorico.Text = "Histórico"
        Me.btnHistorico.ToolTipText = "Histórico"
        '
        'ToolBarButton1
        '
        Me.ToolBarButton1.Name = "ToolBarButton1"
        Me.ToolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnConsultaCliente
        '
        Me.btnConsultaCliente.ImageIndex = 16
        Me.btnConsultaCliente.Name = "btnConsultaCliente"
        Me.btnConsultaCliente.Tag = "ConsultaCliente"
        Me.btnConsultaCliente.Text = "Consultar"
        Me.btnConsultaCliente.ToolTipText = "Consultar datos del cliente"
        '
        'btnQueja
        '
        Me.btnQueja.ImageIndex = 28
        Me.btnQueja.Name = "btnQueja"
        Me.btnQueja.Tag = "Queja"
        Me.btnQueja.Text = "Queja"
        Me.btnQueja.ToolTipText = "Registro de quejas"
        '
        'imgLista
        '
        Me.imgLista.ImageStream = CType(resources.GetObject("imgLista.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgLista.TransparentColor = System.Drawing.Color.Transparent
        Me.imgLista.Images.SetKeyName(0, "")
        Me.imgLista.Images.SetKeyName(1, "")
        Me.imgLista.Images.SetKeyName(2, "")
        Me.imgLista.Images.SetKeyName(3, "")
        Me.imgLista.Images.SetKeyName(4, "")
        Me.imgLista.Images.SetKeyName(5, "")
        Me.imgLista.Images.SetKeyName(6, "")
        Me.imgLista.Images.SetKeyName(7, "")
        Me.imgLista.Images.SetKeyName(8, "")
        Me.imgLista.Images.SetKeyName(9, "")
        Me.imgLista.Images.SetKeyName(10, "")
        Me.imgLista.Images.SetKeyName(11, "")
        Me.imgLista.Images.SetKeyName(12, "")
        Me.imgLista.Images.SetKeyName(13, "")
        Me.imgLista.Images.SetKeyName(14, "")
        Me.imgLista.Images.SetKeyName(15, "")
        Me.imgLista.Images.SetKeyName(16, "")
        Me.imgLista.Images.SetKeyName(17, "")
        Me.imgLista.Images.SetKeyName(18, "")
        Me.imgLista.Images.SetKeyName(19, "")
        Me.imgLista.Images.SetKeyName(20, "")
        Me.imgLista.Images.SetKeyName(21, "")
        Me.imgLista.Images.SetKeyName(22, "")
        Me.imgLista.Images.SetKeyName(23, "")
        Me.imgLista.Images.SetKeyName(24, "")
        Me.imgLista.Images.SetKeyName(25, "")
        Me.imgLista.Images.SetKeyName(26, "")
        Me.imgLista.Images.SetKeyName(27, "")
        Me.imgLista.Images.SetKeyName(28, "")
        Me.imgLista.Images.SetKeyName(29, "")
        Me.imgLista.Images.SetKeyName(30, "")
        Me.imgLista.Images.SetKeyName(31, "ic_cilindros.png")
        '
        'btnClientesHijos
        '
        Me.btnClientesHijos.ImageIndex = 19
        Me.btnClientesHijos.Name = "btnClientesHijos"
        Me.btnClientesHijos.Tag = "ClientesHijos"
        Me.btnClientesHijos.Text = "Clientes hijos"
        Me.btnClientesHijos.ToolTipText = "Consulta los clientes hijos asignados a este cliente"
        '
        'btnAvanzaProgramacion
        '
        Me.btnAvanzaProgramacion.ImageIndex = 7
        Me.btnAvanzaProgramacion.Name = "btnAvanzaProgramacion"
        Me.btnAvanzaProgramacion.Tag = "AvanzaProgramacion"
        Me.btnAvanzaProgramacion.Text = "Avanzar"
        Me.btnAvanzaProgramacion.ToolTipText = "Avanzar al siguiente ciclo de programación"
        '
        'btnATMasCercano
        '
        Me.btnATMasCercano.ImageIndex = 20
        Me.btnATMasCercano.Name = "btnATMasCercano"
        Me.btnATMasCercano.Tag = "AT mas cercano"
        Me.btnATMasCercano.Text = "AT Cercano"
        Me.btnATMasCercano.ToolTipText = "AutoTanque más cercano"
        Me.btnATMasCercano.Visible = False
        '
        'btnCambioZona
        '
        Me.btnCambioZona.ImageIndex = 21
        Me.btnCambioZona.Name = "btnCambioZona"
        Me.btnCambioZona.Tag = "CambioZona"
        Me.btnCambioZona.Text = "Cambio de zona"
        Me.btnCambioZona.ToolTipText = "Cambio de zona economica"
        '
        'btnVentasMultinivel
        '
        Me.btnVentasMultinivel.ImageIndex = 23
        Me.btnVentasMultinivel.Name = "btnVentasMultinivel"
        Me.btnVentasMultinivel.Tag = "VentasMultinivel"
        Me.btnVentasMultinivel.Text = "Ventas multinivel"
        Me.btnVentasMultinivel.ToolTipText = "Administrar clientes recomendados"
        '
        'btnTarjeta
        '
        Me.btnTarjeta.ImageIndex = 22
        Me.btnTarjeta.Name = "btnTarjeta"
        Me.btnTarjeta.Tag = "Tarjeta"
        Me.btnTarjeta.Text = "Tarjeta"
        Me.btnTarjeta.ToolTipText = "Alta de tarjetas"
        '
        'imgPedido
        '
        Me.imgPedido.ImageStream = CType(resources.GetObject("imgPedido.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgPedido.TransparentColor = System.Drawing.Color.Transparent
        Me.imgPedido.Images.SetKeyName(0, "")
        Me.imgPedido.Images.SetKeyName(1, "")
        Me.imgPedido.Images.SetKeyName(2, "")
        Me.imgPedido.Images.SetKeyName(3, "")
        Me.imgPedido.Images.SetKeyName(4, "")
        Me.imgPedido.Images.SetKeyName(5, "")
        '
        'btnConsultaEmpresa
        '
        Me.btnConsultaEmpresa.BackColor = System.Drawing.SystemColors.Control
        Me.btnConsultaEmpresa.Enabled = False
        Me.btnConsultaEmpresa.Image = CType(resources.GetObject("btnConsultaEmpresa.Image"), System.Drawing.Image)
        Me.btnConsultaEmpresa.Location = New System.Drawing.Point(496, 65)
        Me.btnConsultaEmpresa.Name = "btnConsultaEmpresa"
        Me.btnConsultaEmpresa.Size = New System.Drawing.Size(24, 21)
        Me.btnConsultaEmpresa.TabIndex = 4
        Me.ttMensaje.SetToolTip(Me.btnConsultaEmpresa, "Consultar la empresa del cliente seleccionado")
        Me.btnConsultaEmpresa.UseVisualStyleBackColor = False
        '
        'btnSeleccionaEmpresa
        '
        Me.btnSeleccionaEmpresa.BackColor = System.Drawing.SystemColors.Control
        Me.btnSeleccionaEmpresa.Enabled = False
        Me.btnSeleccionaEmpresa.Location = New System.Drawing.Point(471, 65)
        Me.btnSeleccionaEmpresa.Name = "btnSeleccionaEmpresa"
        Me.btnSeleccionaEmpresa.Size = New System.Drawing.Size(24, 21)
        Me.btnSeleccionaEmpresa.TabIndex = 3
        Me.btnSeleccionaEmpresa.Text = "..."
        Me.ttMensaje.SetToolTip(Me.btnSeleccionaEmpresa, "Seleccionar la empresa relacionada con este cliente")
        Me.btnSeleccionaEmpresa.UseVisualStyleBackColor = False
        '
        'lblCelula
        '
        Me.lblCelula.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCelula.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblCelula.Location = New System.Drawing.Point(400, 9)
        Me.lblCelula.Name = "lblCelula"
        Me.lblCelula.Size = New System.Drawing.Size(144, 21)
        Me.lblCelula.TabIndex = 60
        Me.lblCelula.Text = "Servicios Técnicos"
        Me.lblCelula.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ttMensaje.SetToolTip(Me.lblCelula, "Célula del cliente")
        '
        'btnNotaAgregar
        '
        Me.btnNotaAgregar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNotaAgregar.BackColor = System.Drawing.SystemColors.Control
        Me.btnNotaAgregar.Image = CType(resources.GetObject("btnNotaAgregar.Image"), System.Drawing.Image)
        Me.btnNotaAgregar.Location = New System.Drawing.Point(798, 112)
        Me.btnNotaAgregar.Name = "btnNotaAgregar"
        Me.btnNotaAgregar.Size = New System.Drawing.Size(40, 24)
        Me.btnNotaAgregar.TabIndex = 57
        Me.ttMensaje.SetToolTip(Me.btnNotaAgregar, "Agrega una nota al cliente seleccionado")
        Me.btnNotaAgregar.UseVisualStyleBackColor = False
        '
        'btnNotaCerrar
        '
        Me.btnNotaCerrar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNotaCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnNotaCerrar.Image = CType(resources.GetObject("btnNotaCerrar.Image"), System.Drawing.Image)
        Me.btnNotaCerrar.Location = New System.Drawing.Point(838, 112)
        Me.btnNotaCerrar.Name = "btnNotaCerrar"
        Me.btnNotaCerrar.Size = New System.Drawing.Size(40, 24)
        Me.btnNotaCerrar.TabIndex = 58
        Me.ttMensaje.SetToolTip(Me.btnNotaCerrar, "Cierra todas las notas abiertas")
        Me.btnNotaCerrar.UseVisualStyleBackColor = False
        '
        'btnNotaTablero
        '
        Me.btnNotaTablero.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNotaTablero.BackColor = System.Drawing.SystemColors.Control
        Me.btnNotaTablero.Image = CType(resources.GetObject("btnNotaTablero.Image"), System.Drawing.Image)
        Me.btnNotaTablero.Location = New System.Drawing.Point(878, 112)
        Me.btnNotaTablero.Name = "btnNotaTablero"
        Me.btnNotaTablero.Size = New System.Drawing.Size(40, 24)
        Me.btnNotaTablero.TabIndex = 59
        Me.ttMensaje.SetToolTip(Me.btnNotaTablero, "Tablero de notas")
        Me.btnNotaTablero.UseVisualStyleBackColor = False
        '
        'btnEliminarEquipo
        '
        Me.btnEliminarEquipo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEliminarEquipo.BackColor = System.Drawing.SystemColors.Control
        Me.btnEliminarEquipo.Image = CType(resources.GetObject("btnEliminarEquipo.Image"), System.Drawing.Image)
        Me.btnEliminarEquipo.Location = New System.Drawing.Point(740, 108)
        Me.btnEliminarEquipo.Name = "btnEliminarEquipo"
        Me.btnEliminarEquipo.Size = New System.Drawing.Size(4, 4)
        Me.btnEliminarEquipo.TabIndex = 96
        Me.ttMensaje.SetToolTip(Me.btnEliminarEquipo, "Elimina el equipo seleccionado del cliente")
        Me.btnEliminarEquipo.UseVisualStyleBackColor = False
        Me.btnEliminarEquipo.Visible = False
        '
        'lblCreditoExcedido
        '
        Me.lblCreditoExcedido.ActiveLinkColor = System.Drawing.Color.Maroon
        Me.lblCreditoExcedido.AlternatingColor2 = System.Drawing.Color.Red
        Me.lblCreditoExcedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditoExcedido.ForeColor = System.Drawing.Color.Maroon
        Me.lblCreditoExcedido.LinkColor = System.Drawing.Color.Red
        Me.lblCreditoExcedido.Location = New System.Drawing.Point(780, 140)
        Me.lblCreditoExcedido.Name = "lblCreditoExcedido"
        Me.lblCreditoExcedido.Size = New System.Drawing.Size(156, 20)
        Me.lblCreditoExcedido.TabIndex = 99
        Me.lblCreditoExcedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblCreditoExcedido.TimerInterval = 500
        Me.ttMensaje.SetToolTip(Me.lblCreditoExcedido, "Célula del cliente")
        '
        'btnContactos
        '
        Me.btnContactos.BackColor = System.Drawing.SystemColors.Control
        Me.btnContactos.Image = CType(resources.GetObject("btnContactos.Image"), System.Drawing.Image)
        Me.btnContactos.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnContactos.Location = New System.Drawing.Point(521, 65)
        Me.btnContactos.Name = "btnContactos"
        Me.btnContactos.Size = New System.Drawing.Size(24, 21)
        Me.btnContactos.TabIndex = 103
        Me.ttMensaje.SetToolTip(Me.btnContactos, "Consulta ")
        Me.btnContactos.UseVisualStyleBackColor = False
        '
        'lblReferencia
        '
        Me.lblReferencia.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReferencia.ForeColor = System.Drawing.Color.Black
        Me.lblReferencia.Location = New System.Drawing.Point(292, 9)
        Me.lblReferencia.Name = "lblReferencia"
        Me.lblReferencia.Size = New System.Drawing.Size(108, 20)
        Me.lblReferencia.TabIndex = 104
        Me.lblReferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ttMensaje.SetToolTip(Me.lblReferencia, "Célula del cliente")
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuBuscar, Me.mnuRefrescar, Me.MenuItem3, Me.mnuNuevo, Me.mnuGuardar, Me.mnuModificar, Me.MenuItem6, Me.mnuServicioTecnico, Me.MenuItem7, Me.mnuPedido, Me.mnuCancelacion, Me.MenuItem2, Me.mnuConfiguracion, Me.MenuItem5, Me.mnuCerrar})
        Me.MenuItem1.MergeOrder = 100
        Me.MenuItem1.Text = "CallCenter"
        '
        'mnuBuscar
        '
        Me.mnuBuscar.Index = 0
        Me.mnuBuscar.Shortcut = System.Windows.Forms.Shortcut.CtrlB
        Me.mnuBuscar.Text = "&Buscar"
        '
        'mnuRefrescar
        '
        Me.mnuRefrescar.Index = 1
        Me.mnuRefrescar.Shortcut = System.Windows.Forms.Shortcut.CtrlR
        Me.mnuRefrescar.Text = "&Refrescar"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "-"
        '
        'mnuNuevo
        '
        Me.mnuNuevo.Index = 3
        Me.mnuNuevo.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.mnuNuevo.Text = "&Nuevo"
        '
        'mnuGuardar
        '
        Me.mnuGuardar.Index = 4
        Me.mnuGuardar.Shortcut = System.Windows.Forms.Shortcut.CtrlG
        Me.mnuGuardar.Text = "&Guardar"
        '
        'mnuModificar
        '
        Me.mnuModificar.Index = 5
        Me.mnuModificar.Shortcut = System.Windows.Forms.Shortcut.CtrlM
        Me.mnuModificar.Text = "&Modificar"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 6
        Me.MenuItem6.Text = "-"
        '
        'mnuServicioTecnico
        '
        Me.mnuServicioTecnico.Index = 7
        Me.mnuServicioTecnico.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuServicioTecnico.Text = "&Servicios técnicos"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 8
        Me.MenuItem7.Text = "-"
        '
        'mnuPedido
        '
        Me.mnuPedido.Index = 9
        Me.mnuPedido.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.mnuPedido.Text = "&Pedido"
        '
        'mnuCancelacion
        '
        Me.mnuCancelacion.Index = 10
        Me.mnuCancelacion.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.mnuCancelacion.Text = "&Cancelación"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 11
        Me.MenuItem2.Text = "-"
        '
        'mnuConfiguracion
        '
        Me.mnuConfiguracion.Index = 12
        Me.mnuConfiguracion.Text = "Configuración..."
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 13
        Me.MenuItem5.Text = "-"
        '
        'mnuCerrar
        '
        Me.mnuCerrar.Index = 14
        Me.mnuCerrar.Text = "Cerrar"
        '
        'pnlCallCenter
        '
        Me.pnlCallCenter.Controls.Add(Me.grpGeoReferencia)
        Me.pnlCallCenter.Controls.Add(Me.lnkNoSuministrar)
        Me.pnlCallCenter.Controls.Add(Me.lnkAlertaRAF)
        Me.pnlCallCenter.Controls.Add(Me.lblReferencia)
        Me.pnlCallCenter.Controls.Add(Me.btnContactos)
        Me.pnlCallCenter.Controls.Add(Me.tbBarra2)
        Me.pnlCallCenter.Controls.Add(Me.cboOrigenCliente)
        Me.pnlCallCenter.Controls.Add(Me.lblPromotor)
        Me.pnlCallCenter.Controls.Add(Me.grpVentasMultinivel)
        Me.pnlCallCenter.Controls.Add(Me.lblCreditoExcedido)
        Me.pnlCallCenter.Controls.Add(Me.chkPortatil)
        Me.pnlCallCenter.Controls.Add(Me.EliminarST1)
        Me.pnlCallCenter.Controls.Add(Me.btnEliminarEquipo)
        Me.pnlCallCenter.Controls.Add(Me.btnNotaTablero)
        Me.pnlCallCenter.Controls.Add(Me.btnNotaAgregar)
        Me.pnlCallCenter.Controls.Add(Me.btnNotaCerrar)
        Me.pnlCallCenter.Controls.Add(Me.lvwLlamada)
        Me.pnlCallCenter.Controls.Add(Me.grdLlamada)
        Me.pnlCallCenter.Controls.Add(Me.lblStatusCalidad)
        Me.pnlCallCenter.Controls.Add(Me.lblStatus)
        Me.pnlCallCenter.Controls.Add(Me.Label1)
        Me.pnlCallCenter.Controls.Add(Me.Label44)
        Me.pnlCallCenter.Controls.Add(Me.Label43)
        Me.pnlCallCenter.Controls.Add(Me.Label42)
        Me.pnlCallCenter.Controls.Add(Me.Label33)
        Me.pnlCallCenter.Controls.Add(Me.Label32)
        Me.pnlCallCenter.Controls.Add(Me.Label27)
        Me.pnlCallCenter.Controls.Add(Me.Label7)
        Me.pnlCallCenter.Controls.Add(Me.Label6)
        Me.pnlCallCenter.Controls.Add(Me.Label2)
        Me.pnlCallCenter.Controls.Add(Me.txtObservacionesLlamada)
        Me.pnlCallCenter.Controls.Add(Me.lblListaPedido)
        Me.pnlCallCenter.Controls.Add(Me.lvwPedido)
        Me.pnlCallCenter.Controls.Add(Me.SeleccionCalleColonia)
        Me.pnlCallCenter.Controls.Add(Me.txtObservacionesCliente)
        Me.pnlCallCenter.Controls.Add(Me.grdClasificacion)
        Me.pnlCallCenter.Controls.Add(Me.lblFAlta)
        Me.pnlCallCenter.Controls.Add(Me.btnConsultaEmpresa)
        Me.pnlCallCenter.Controls.Add(Me.btnSeleccionaEmpresa)
        Me.pnlCallCenter.Controls.Add(Me.grpTelefono)
        Me.pnlCallCenter.Controls.Add(Me.lblRazonSocial)
        Me.pnlCallCenter.Controls.Add(Me.lblCelula)
        Me.pnlCallCenter.Controls.Add(Me.lblCliente)
        Me.pnlCallCenter.Controls.Add(Me.txtNombre)
        Me.pnlCallCenter.Controls.Add(Me.cboRuta)
        Me.pnlCallCenter.Controls.Add(Me.lblOrigenCliente)
        Me.pnlCallCenter.Controls.Add(Me.lblRuta)
        Me.pnlCallCenter.Controls.Add(Me.grpTanques)
        Me.pnlCallCenter.Controls.Add(Me.grpProgramaCliente)
        Me.pnlCallCenter.Controls.Add(Me.lblCtePadreEdif)
        Me.pnlCallCenter.Controls.Add(Me.lblNombreEmpresa)
        Me.pnlCallCenter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlCallCenter.Location = New System.Drawing.Point(0, 0)
        Me.pnlCallCenter.Name = "pnlCallCenter"
        Me.pnlCallCenter.Size = New System.Drawing.Size(935, 541)
        Me.pnlCallCenter.TabIndex = 56
        '
        'grpGeoReferencia
        '
        Me.grpGeoReferencia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpGeoReferencia.Controls.Add(Me.lblValorY)
        Me.grpGeoReferencia.Controls.Add(Me.lblValorX)
        Me.grpGeoReferencia.Controls.Add(Me.lblY)
        Me.grpGeoReferencia.Controls.Add(Me.lblX)
        Me.grpGeoReferencia.Controls.Add(Me.btnGeoreferenciar)
        Me.grpGeoReferencia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpGeoReferencia.Location = New System.Drawing.Point(748, 360)
        Me.grpGeoReferencia.Name = "grpGeoReferencia"
        Me.grpGeoReferencia.Size = New System.Drawing.Size(172, 65)
        Me.grpGeoReferencia.TabIndex = 107
        Me.grpGeoReferencia.TabStop = False
        Me.grpGeoReferencia.Text = "Georeferencia"
        '
        'lblValorY
        '
        Me.lblValorY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblValorY.AutoSize = True
        Me.lblValorY.Location = New System.Drawing.Point(75, 37)
        Me.lblValorY.Name = "lblValorY"
        Me.lblValorY.Size = New System.Drawing.Size(0, 13)
        Me.lblValorY.TabIndex = 4
        '
        'lblValorX
        '
        Me.lblValorX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblValorX.AutoSize = True
        Me.lblValorX.Location = New System.Drawing.Point(75, 21)
        Me.lblValorX.Name = "lblValorX"
        Me.lblValorX.Size = New System.Drawing.Size(0, 13)
        Me.lblValorX.TabIndex = 3
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(53, 37)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(17, 13)
        Me.lblY.TabIndex = 2
        Me.lblY.Text = "Y:"
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(53, 21)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(17, 13)
        Me.lblX.TabIndex = 1
        Me.lblX.Text = "X:"
        '
        'btnGeoreferenciar
        '
        Me.btnGeoreferenciar.Enabled = False
        Me.btnGeoreferenciar.Image = CType(resources.GetObject("btnGeoreferenciar.Image"), System.Drawing.Image)
        Me.btnGeoreferenciar.Location = New System.Drawing.Point(6, 21)
        Me.btnGeoreferenciar.Name = "btnGeoreferenciar"
        Me.btnGeoreferenciar.Size = New System.Drawing.Size(40, 34)
        Me.btnGeoreferenciar.TabIndex = 0
        Me.btnGeoreferenciar.UseVisualStyleBackColor = True
        '
        'lnkNoSuministrar
        '
        Me.lnkNoSuministrar.AlternatingColor2 = System.Drawing.Color.Red
        Me.lnkNoSuministrar.AutoSize = True
        Me.lnkNoSuministrar.BackColor = System.Drawing.Color.Gainsboro
        Me.lnkNoSuministrar.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold)
        Me.lnkNoSuministrar.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lnkNoSuministrar.Location = New System.Drawing.Point(239, 12)
        Me.lnkNoSuministrar.Name = "lnkNoSuministrar"
        Me.lnkNoSuministrar.Size = New System.Drawing.Size(49, 11)
        Me.lnkNoSuministrar.TabIndex = 106
        Me.lnkNoSuministrar.TabStop = True
        Me.lnkNoSuministrar.Text = "No surtir"
        Me.lnkNoSuministrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lnkNoSuministrar.TimerInterval = 500
        Me.lnkNoSuministrar.Visible = False
        '
        'lnkAlertaRAF
        '
        Me.lnkAlertaRAF.AlternatingColor2 = System.Drawing.Color.Red
        Me.lnkAlertaRAF.AutoSize = True
        Me.lnkAlertaRAF.BackColor = System.Drawing.Color.Gainsboro
        Me.lnkAlertaRAF.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold)
        Me.lnkAlertaRAF.LinkColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.lnkAlertaRAF.Location = New System.Drawing.Point(726, 118)
        Me.lnkAlertaRAF.Name = "lnkAlertaRAF"
        Me.lnkAlertaRAF.Size = New System.Drawing.Size(72, 11)
        Me.lnkAlertaRAF.TabIndex = 56
        Me.lnkAlertaRAF.TabStop = True
        Me.lnkAlertaRAF.Text = "Ruta con RAF"
        Me.lnkAlertaRAF.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lnkAlertaRAF.TimerInterval = 500
        Me.lnkAlertaRAF.Visible = False
        '
        'tbBarra2
        '
        Me.tbBarra2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.tbBarra2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tbBarra2.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnAvanzaProgramacion, Me.btnATMasCercano, Me.btnCambioZona, Me.btnVentasMultinivel, Me.btnTarjeta, Me.btnAgregarEquipo, Me.btnModificarEquipo, Me.btnImagenes, Me.btnClientesHijos, Me.btnSolicitudCredito, Me.btnInfoProspectos, Me.btnFugas})
        Me.tbBarra2.ButtonSize = New System.Drawing.Size(74, 34)
        Me.tbBarra2.Dock = System.Windows.Forms.DockStyle.None
        Me.tbBarra2.DropDownArrows = True
        Me.tbBarra2.ImageList = Me.imgLista
        Me.tbBarra2.Location = New System.Drawing.Point(936, 0)
        Me.tbBarra2.Name = "tbBarra2"
        Me.tbBarra2.ShowToolTips = True
        Me.tbBarra2.Size = New System.Drawing.Size(8, 367)
        Me.tbBarra2.TabIndex = 102
        Me.tbBarra2.Visible = False
        '
        'btnAgregarEquipo
        '
        Me.btnAgregarEquipo.ImageIndex = 24
        Me.btnAgregarEquipo.Name = "btnAgregarEquipo"
        Me.btnAgregarEquipo.Tag = "AgregarEquipo"
        Me.btnAgregarEquipo.Text = "Agregar Equipo"
        Me.btnAgregarEquipo.ToolTipText = "Agregar equipo"
        '
        'btnModificarEquipo
        '
        Me.btnModificarEquipo.ImageIndex = 5
        Me.btnModificarEquipo.Name = "btnModificarEquipo"
        Me.btnModificarEquipo.Tag = "ModificarEquipo"
        Me.btnModificarEquipo.Text = "Modificar Equipo"
        Me.btnModificarEquipo.ToolTipText = "Modificar equipo"
        '
        'btnImagenes
        '
        Me.btnImagenes.ImageIndex = 25
        Me.btnImagenes.Name = "btnImagenes"
        Me.btnImagenes.Tag = "Imagenes"
        Me.btnImagenes.Text = "Imágenes"
        Me.btnImagenes.ToolTipText = "Administrar documentación del cliente"
        '
        'btnSolicitudCredito
        '
        Me.btnSolicitudCredito.ImageIndex = 29
        Me.btnSolicitudCredito.Name = "btnSolicitudCredito"
        Me.btnSolicitudCredito.Tag = "SolicitudCredito"
        Me.btnSolicitudCredito.Text = "Solicitud Crédito"
        Me.btnSolicitudCredito.ToolTipText = "Solicitar crédito para este cliente"
        '
        'btnInfoProspectos
        '
        Me.btnInfoProspectos.ImageIndex = 30
        Me.btnInfoProspectos.Name = "btnInfoProspectos"
        Me.btnInfoProspectos.Tag = "Prospectos"
        Me.btnInfoProspectos.Text = "Prospectos"
        Me.btnInfoProspectos.Visible = False
        '
        'btnFugas
        '
        Me.btnFugas.ImageIndex = 31
        Me.btnFugas.Name = "btnFugas"
        Me.btnFugas.Tag = "Fugas"
        Me.btnFugas.Text = "Fugas portatil"
        Me.btnFugas.ToolTipText = "Registro de fugas portatil"
        '
        'lblPromotor
        '
        Me.lblPromotor.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPromotor.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPromotor.Location = New System.Drawing.Point(766, 68)
        Me.lblPromotor.Name = "lblPromotor"
        Me.lblPromotor.Size = New System.Drawing.Size(150, 18)
        Me.lblPromotor.TabIndex = 101
        Me.lblPromotor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpVentasMultinivel
        '
        Me.grpVentasMultinivel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpVentasMultinivel.Controls.Add(Me.lblClienteOrigen)
        Me.grpVentasMultinivel.Controls.Add(Me.lblHijos)
        Me.grpVentasMultinivel.Controls.Add(Me.lblSaldo)
        Me.grpVentasMultinivel.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpVentasMultinivel.Location = New System.Drawing.Point(568, 360)
        Me.grpVentasMultinivel.Name = "grpVentasMultinivel"
        Me.grpVentasMultinivel.Size = New System.Drawing.Size(176, 65)
        Me.grpVentasMultinivel.TabIndex = 100
        Me.grpVentasMultinivel.TabStop = False
        Me.grpVentasMultinivel.Text = "Ventas multinivel"
        '
        'lblClienteOrigen
        '
        Me.lblClienteOrigen.AutoSize = True
        Me.lblClienteOrigen.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClienteOrigen.Location = New System.Drawing.Point(16, 48)
        Me.lblClienteOrigen.Name = "lblClienteOrigen"
        Me.lblClienteOrigen.Size = New System.Drawing.Size(77, 13)
        Me.lblClienteOrigen.TabIndex = 2
        Me.lblClienteOrigen.Text = "Cliente origen:"
        '
        'lblHijos
        '
        Me.lblHijos.AutoSize = True
        Me.lblHijos.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHijos.Location = New System.Drawing.Point(16, 32)
        Me.lblHijos.Name = "lblHijos"
        Me.lblHijos.Size = New System.Drawing.Size(34, 13)
        Me.lblHijos.TabIndex = 1
        Me.lblHijos.Text = "Hijos:"
        '
        'lblSaldo
        '
        Me.lblSaldo.AutoSize = True
        Me.lblSaldo.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblSaldo.Location = New System.Drawing.Point(16, 16)
        Me.lblSaldo.Name = "lblSaldo"
        Me.lblSaldo.Size = New System.Drawing.Size(41, 13)
        Me.lblSaldo.TabIndex = 0
        Me.lblSaldo.Text = "Saldo:"
        '
        'chkPortatil
        '
        Me.chkPortatil.Location = New System.Drawing.Point(299, 9)
        Me.chkPortatil.Name = "chkPortatil"
        Me.chkPortatil.Size = New System.Drawing.Size(64, 20)
        Me.chkPortatil.TabIndex = 98
        Me.chkPortatil.Text = "Portátil"
        '
        'EliminarST1
        '
        Me.EliminarST1.Cliente = 0
        Me.EliminarST1.Location = New System.Drawing.Point(936, -26)
        Me.EliminarST1.Name = "EliminarST1"
        Me.EliminarST1.Secuencia = 0
        Me.EliminarST1.Size = New System.Drawing.Size(8, 26)
        Me.EliminarST1.TabIndex = 97
        '
        'lvwLlamada
        '
        Me.lvwLlamada.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwLlamada.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwCallFLlamada, Me.lvwCallDesMotivo, Me.lvwCallPedido, Me.lvwCallUsuario, Me.lvwCallOperador, Me.lvwCallDemandante, Me.lvwCallObservaciones})
        Me.lvwLlamada.FullRowSelect = True
        Me.lvwLlamada.Location = New System.Drawing.Point(14, 574)
        Me.lvwLlamada.Name = "lvwLlamada"
        Me.lvwLlamada.Size = New System.Drawing.Size(738, 0)
        Me.lvwLlamada.TabIndex = 92
        Me.lvwLlamada.UseCompatibleStateImageBehavior = False
        Me.lvwLlamada.View = System.Windows.Forms.View.Details
        '
        'lvwCallFLlamada
        '
        Me.lvwCallFLlamada.Text = "F.Llamada"
        Me.lvwCallFLlamada.Width = 170
        '
        'lvwCallDesMotivo
        '
        Me.lvwCallDesMotivo.Text = "Motivo de la llamada"
        Me.lvwCallDesMotivo.Width = 200
        '
        'lvwCallPedido
        '
        Me.lvwCallPedido.Text = "Pedido"
        Me.lvwCallPedido.Width = 80
        '
        'lvwCallUsuario
        '
        Me.lvwCallUsuario.Text = "Usuario"
        Me.lvwCallUsuario.Width = 75
        '
        'lvwCallOperador
        '
        Me.lvwCallOperador.Text = "Operador"
        '
        'lvwCallDemandante
        '
        Me.lvwCallDemandante.Text = "Demandante"
        Me.lvwCallDemandante.Width = 140
        '
        'lvwCallObservaciones
        '
        Me.lvwCallObservaciones.Text = "Observaciones"
        Me.lvwCallObservaciones.Width = 0
        '
        'grdLlamada
        '
        Me.grdLlamada.AlternatingBackColor = System.Drawing.Color.LightGray
        Me.grdLlamada.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLlamada.BackColor = System.Drawing.Color.DarkGray
        Me.grdLlamada.CaptionBackColor = System.Drawing.Color.LightGray
        Me.grdLlamada.CaptionFont = New System.Drawing.Font("Verdana", 10.0!)
        Me.grdLlamada.CaptionForeColor = System.Drawing.Color.Navy
        Me.grdLlamada.CaptionText = "Llamadas y quejas"
        Me.grdLlamada.DataMember = ""
        Me.grdLlamada.ForeColor = System.Drawing.Color.Black
        Me.grdLlamada.GridLineColor = System.Drawing.Color.Black
        Me.grdLlamada.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdLlamada.HeaderBackColor = System.Drawing.Color.Silver
        Me.grdLlamada.HeaderForeColor = System.Drawing.Color.Black
        Me.grdLlamada.LinkColor = System.Drawing.Color.Navy
        Me.grdLlamada.Location = New System.Drawing.Point(14, 571)
        Me.grdLlamada.Name = "grdLlamada"
        Me.grdLlamada.ParentRowsBackColor = System.Drawing.Color.White
        Me.grdLlamada.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdLlamada.ReadOnly = True
        Me.grdLlamada.SelectionBackColor = System.Drawing.Color.Navy
        Me.grdLlamada.SelectionForeColor = System.Drawing.Color.White
        Me.grdLlamada.Size = New System.Drawing.Size(738, 377)
        Me.grdLlamada.TabIndex = 65
        Me.grdLlamada.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.styLlamada})
        '
        'styLlamada
        '
        Me.styLlamada.DataGrid = Me.grdLlamada
        Me.styLlamada.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colLLFLlamada, Me.colLLMotivo, Me.colLLPedido, Me.colLLUsuario, Me.colLLOperador, Me.colLLDemandante, Me.colLLObservaciones})
        Me.styLlamada.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.styLlamada.MappingName = "Llamada"
        Me.styLlamada.ReadOnly = True
        Me.styLlamada.RowHeadersVisible = False
        '
        'colLLFLlamada
        '
        Me.colLLFLlamada.Format = ""
        Me.colLLFLlamada.FormatInfo = Nothing
        Me.colLLFLlamada.HeaderText = "F.Llamada"
        Me.colLLFLlamada.MappingName = "FLlamada"
        Me.colLLFLlamada.NullText = ""
        Me.colLLFLlamada.Width = 150
        '
        'colLLMotivo
        '
        Me.colLLMotivo.Format = ""
        Me.colLLMotivo.FormatInfo = Nothing
        Me.colLLMotivo.HeaderText = "Motivo"
        Me.colLLMotivo.MappingName = "DesMotivo"
        Me.colLLMotivo.NullText = ""
        Me.colLLMotivo.Width = 150
        '
        'colLLPedido
        '
        Me.colLLPedido.Format = ""
        Me.colLLPedido.FormatInfo = Nothing
        Me.colLLPedido.HeaderText = "Pedido"
        Me.colLLPedido.MappingName = "Pedido"
        Me.colLLPedido.NullText = ""
        Me.colLLPedido.Width = 75
        '
        'colLLUsuario
        '
        Me.colLLUsuario.Format = ""
        Me.colLLUsuario.FormatInfo = Nothing
        Me.colLLUsuario.HeaderText = "Atendió"
        Me.colLLUsuario.MappingName = "Usuario"
        Me.colLLUsuario.NullText = ""
        Me.colLLUsuario.Width = 75
        '
        'colLLOperador
        '
        Me.colLLOperador.Format = ""
        Me.colLLOperador.FormatInfo = Nothing
        Me.colLLOperador.HeaderText = "Operador"
        Me.colLLOperador.MappingName = "Operador"
        Me.colLLOperador.NullText = ""
        Me.colLLOperador.Width = 75
        '
        'colLLDemandante
        '
        Me.colLLDemandante.Format = ""
        Me.colLLDemandante.FormatInfo = Nothing
        Me.colLLDemandante.HeaderText = "Demandante"
        Me.colLLDemandante.MappingName = "Demandante"
        Me.colLLDemandante.NullText = ""
        Me.colLLDemandante.Width = 160
        '
        'colLLObservaciones
        '
        Me.colLLObservaciones.Format = ""
        Me.colLLObservaciones.FormatInfo = Nothing
        Me.colLLObservaciones.MappingName = "Observaciones"
        Me.colLLObservaciones.NullText = ""
        Me.colLLObservaciones.Width = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(758, 574)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 13)
        Me.Label1.TabIndex = 87
        Me.Label1.Text = "Observaciones de la llamada:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(17, 233)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(82, 13)
        Me.Label44.TabIndex = 81
        Me.Label44.Text = "Observaciones:"
        Me.Label44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(566, 43)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(34, 13)
        Me.Label43.TabIndex = 80
        Me.Label43.Text = "Ruta:"
        Me.Label43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(566, 68)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(43, 13)
        Me.Label42.TabIndex = 79
        Me.Label42.Text = "Orígen:"
        Me.Label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(566, 116)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(56, 13)
        Me.Label33.TabIndex = 77
        Me.Label33.Text = "E.Calidad:"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(566, 92)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(47, 13)
        Me.Label32.TabIndex = 76
        Me.Label32.Text = "Estatus:"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(742, 92)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(40, 13)
        Me.Label27.TabIndex = 73
        Me.Label27.Text = "F.Alta:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 69
        Me.Label7.Text = "Empresa:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 44)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 68
        Me.Label6.Text = "Nombre:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(17, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 57
        Me.Label2.Text = "Cliente:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtObservacionesLlamada
        '
        Me.txtObservacionesLlamada.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtObservacionesLlamada.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtObservacionesLlamada.Location = New System.Drawing.Point(758, 591)
        Me.txtObservacionesLlamada.Multiline = True
        Me.txtObservacionesLlamada.Name = "txtObservacionesLlamada"
        Me.txtObservacionesLlamada.ReadOnly = True
        Me.txtObservacionesLlamada.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservacionesLlamada.Size = New System.Drawing.Size(160, 377)
        Me.txtObservacionesLlamada.TabIndex = 86
        '
        'lblListaPedido
        '
        Me.lblListaPedido.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblListaPedido.BackColor = System.Drawing.Color.White
        Me.lblListaPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblListaPedido.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblListaPedido.ForeColor = System.Drawing.Color.Navy
        Me.lblListaPedido.Location = New System.Drawing.Point(14, 433)
        Me.lblListaPedido.Name = "lblListaPedido"
        Me.lblListaPedido.Size = New System.Drawing.Size(904, 23)
        Me.lblListaPedido.TabIndex = 83
        Me.lblListaPedido.Text = "Pedidos"
        Me.lblListaPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lvwPedido
        '
        Me.lvwPedido.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwPedido.BackColor = System.Drawing.SystemColors.Window
        Me.lvwPedido.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.lvwcolPedidoReferencia, Me.lvwcolTipo, Me.lvwcolFPedido, Me.lvwcolFCompromiso, Me.lvwcolFSuministro, Me.lvwcolLitros, Me.lvwcolEstatus, Me.lvwcolRuta, Me.lvwcolAutotanque, Me.lvwcolUsuario, Me.lvwcolAñoPed, Me.lvwcolCelula, Me.lvwcolPedido, Me.lvwcolFactura, Me.lvwcolObservaciones, Me.lvwcolProducto})
        Me.lvwPedido.FullRowSelect = True
        Me.lvwPedido.Location = New System.Drawing.Point(14, 457)
        Me.lvwPedido.Name = "lvwPedido"
        Me.lvwPedido.Size = New System.Drawing.Size(904, 110)
        Me.lvwPedido.SmallImageList = Me.imgPedido
        Me.lvwPedido.TabIndex = 82
        Me.lvwPedido.UseCompatibleStateImageBehavior = False
        Me.lvwPedido.View = System.Windows.Forms.View.Details
        '
        'lvwcolPedidoReferencia
        '
        Me.lvwcolPedidoReferencia.Text = "Pedido"
        Me.lvwcolPedidoReferencia.Width = 110
        '
        'lvwcolTipo
        '
        Me.lvwcolTipo.Text = "Tipo"
        Me.lvwcolTipo.Width = 35
        '
        'lvwcolFPedido
        '
        Me.lvwcolFPedido.Text = "F.Pedido"
        Me.lvwcolFPedido.Width = 140
        '
        'lvwcolFCompromiso
        '
        Me.lvwcolFCompromiso.Text = "F.Compromiso"
        Me.lvwcolFCompromiso.Width = 85
        '
        'lvwcolFSuministro
        '
        Me.lvwcolFSuministro.Text = "F.Suministro"
        Me.lvwcolFSuministro.Width = 85
        '
        'lvwcolLitros
        '
        Me.lvwcolLitros.Text = "Litros"
        Me.lvwcolLitros.Width = 55
        '
        'lvwcolEstatus
        '
        Me.lvwcolEstatus.Text = "Estatus"
        Me.lvwcolEstatus.Width = 75
        '
        'lvwcolRuta
        '
        Me.lvwcolRuta.Text = "Ruta"
        '
        'lvwcolAutotanque
        '
        Me.lvwcolAutotanque.Text = "A.T."
        Me.lvwcolAutotanque.Width = 35
        '
        'lvwcolUsuario
        '
        Me.lvwcolUsuario.Text = "Atendió"
        '
        'lvwcolAñoPed
        '
        Me.lvwcolAñoPed.Text = "AñoPed"
        Me.lvwcolAñoPed.Width = 0
        '
        'lvwcolCelula
        '
        Me.lvwcolCelula.Text = "Celula"
        Me.lvwcolCelula.Width = 0
        '
        'lvwcolPedido
        '
        Me.lvwcolPedido.Text = "Pedido"
        Me.lvwcolPedido.Width = 0
        '
        'lvwcolFactura
        '
        Me.lvwcolFactura.Text = "Factura"
        Me.lvwcolFactura.Width = 65
        '
        'lvwcolObservaciones
        '
        Me.lvwcolObservaciones.Text = "Observaciones"
        Me.lvwcolObservaciones.Width = 80
        '
        'lvwcolProducto
        '
        Me.lvwcolProducto.Width = 0
        '
        'txtObservacionesCliente
        '
        Me.txtObservacionesCliente.Location = New System.Drawing.Point(97, 233)
        Me.txtObservacionesCliente.Name = "txtObservacionesCliente"
        Me.txtObservacionesCliente.Size = New System.Drawing.Size(448, 21)
        Me.txtObservacionesCliente.TabIndex = 6
        '
        'grdClasificacion
        '
        Me.grdClasificacion.Controls.Add(Me.lblGiroCliente)
        Me.grdClasificacion.Controls.Add(Me.lblComisionText)
        Me.grdClasificacion.Controls.Add(Me.lblComision)
        Me.grdClasificacion.Controls.Add(Me.lblVip)
        Me.grdClasificacion.Controls.Add(Me.chkVIP)
        Me.grdClasificacion.Controls.Add(Me.cboRamoCliente)
        Me.grdClasificacion.Controls.Add(Me.lblTipoFactura)
        Me.grdClasificacion.Controls.Add(Me.lblClasificacionCliente)
        Me.grdClasificacion.Controls.Add(Me.lblCartera)
        Me.grdClasificacion.Controls.Add(Me.Label40)
        Me.grdClasificacion.Controls.Add(Me.Label39)
        Me.grdClasificacion.Controls.Add(Me.Label38)
        Me.grdClasificacion.Controls.Add(Me.Label41)
        Me.grdClasificacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdClasificacion.Location = New System.Drawing.Point(288, 273)
        Me.grdClasificacion.Name = "grdClasificacion"
        Me.grdClasificacion.Size = New System.Drawing.Size(260, 152)
        Me.grdClasificacion.TabIndex = 8
        Me.grdClasificacion.TabStop = False
        Me.grdClasificacion.Text = "Clasificación"
        '
        'lblGiroCliente
        '
        Me.lblGiroCliente.AutoSize = True
        Me.lblGiroCliente.Location = New System.Drawing.Point(100, 0)
        Me.lblGiroCliente.Name = "lblGiroCliente"
        Me.lblGiroCliente.Size = New System.Drawing.Size(0, 13)
        Me.lblGiroCliente.TabIndex = 53
        '
        'lblComisionText
        '
        Me.lblComisionText.AutoSize = True
        Me.lblComisionText.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComisionText.Location = New System.Drawing.Point(136, 123)
        Me.lblComisionText.Name = "lblComisionText"
        Me.lblComisionText.Size = New System.Drawing.Size(53, 13)
        Me.lblComisionText.TabIndex = 52
        Me.lblComisionText.Text = "Comisión:"
        Me.lblComisionText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblComision
        '
        Me.lblComision.BackColor = System.Drawing.Color.Gainsboro
        Me.lblComision.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblComision.Enabled = False
        Me.lblComision.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblComision.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblComision.Location = New System.Drawing.Point(196, 120)
        Me.lblComision.Name = "lblComision"
        Me.lblComision.Size = New System.Drawing.Size(52, 21)
        Me.lblComision.TabIndex = 51
        Me.lblComision.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblVip
        '
        Me.lblVip.AutoSize = True
        Me.lblVip.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVip.Location = New System.Drawing.Point(8, 124)
        Me.lblVip.Name = "lblVip"
        Me.lblVip.Size = New System.Drawing.Size(63, 13)
        Me.lblVip.TabIndex = 50
        Me.lblVip.Text = "Cliente VIP:"
        Me.lblVip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkVIP
        '
        Me.chkVIP.Location = New System.Drawing.Point(100, 121)
        Me.chkVIP.Name = "chkVIP"
        Me.chkVIP.Size = New System.Drawing.Size(16, 21)
        Me.chkVIP.TabIndex = 4
        Me.chkVIP.TabStop = False
        '
        'lblTipoFactura
        '
        Me.lblTipoFactura.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTipoFactura.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTipoFactura.Location = New System.Drawing.Point(100, 72)
        Me.lblTipoFactura.Name = "lblTipoFactura"
        Me.lblTipoFactura.Size = New System.Drawing.Size(148, 21)
        Me.lblTipoFactura.TabIndex = 2
        Me.lblTipoFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblClasificacionCliente
        '
        Me.lblClasificacionCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblClasificacionCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClasificacionCliente.Location = New System.Drawing.Point(100, 24)
        Me.lblClasificacionCliente.Name = "lblClasificacionCliente"
        Me.lblClasificacionCliente.Size = New System.Drawing.Size(148, 21)
        Me.lblClasificacionCliente.TabIndex = 0
        Me.lblClasificacionCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCartera
        '
        Me.lblCartera.BackColor = System.Drawing.Color.Gainsboro
        Me.lblCartera.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCartera.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCartera.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblCartera.Location = New System.Drawing.Point(100, 96)
        Me.lblCartera.Name = "lblCartera"
        Me.lblCartera.Size = New System.Drawing.Size(148, 21)
        Me.lblCartera.TabIndex = 3
        Me.lblCartera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label40.Location = New System.Drawing.Point(8, 75)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(67, 13)
        Me.Label40.TabIndex = 46
        Me.Label40.Text = "Facturación:"
        Me.Label40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label39.Location = New System.Drawing.Point(8, 51)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(38, 13)
        Me.Label39.TabIndex = 45
        Me.Label39.Text = "Ramo:"
        Me.Label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(8, 27)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(69, 13)
        Me.Label38.TabIndex = 44
        Me.Label38.Text = "Clasificación:"
        Me.Label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label41.Location = New System.Drawing.Point(8, 99)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(48, 13)
        Me.Label41.TabIndex = 47
        Me.Label41.Text = "Cartera:"
        Me.Label41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFAlta
        '
        Me.lblFAlta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFAlta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFAlta.Location = New System.Drawing.Point(782, 88)
        Me.lblFAlta.Name = "lblFAlta"
        Me.lblFAlta.Size = New System.Drawing.Size(133, 21)
        Me.lblFAlta.TabIndex = 72
        Me.lblFAlta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpTelefono
        '
        Me.grpTelefono.Controls.Add(Me.lblContratoText)
        Me.grpTelefono.Controls.Add(Me.lblProspecto)
        Me.grpTelefono.Controls.Add(Me.txtLada)
        Me.grpTelefono.Controls.Add(Me.txtEmail)
        Me.grpTelefono.Controls.Add(Me.Label3)
        Me.grpTelefono.Controls.Add(Me.Label18)
        Me.grpTelefono.Controls.Add(Me.Label17)
        Me.grpTelefono.Controls.Add(Me.txtTelAlterno2)
        Me.grpTelefono.Controls.Add(Me.txtTelCasa)
        Me.grpTelefono.Controls.Add(Me.txtTelAlterno1)
        Me.grpTelefono.Controls.Add(Me.Label16)
        Me.grpTelefono.Controls.Add(Me.chkContrato)
        Me.grpTelefono.Controls.Add(Me.lnkQueja)
        Me.grpTelefono.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTelefono.Location = New System.Drawing.Point(14, 273)
        Me.grpTelefono.Name = "grpTelefono"
        Me.grpTelefono.Size = New System.Drawing.Size(266, 152)
        Me.grpTelefono.TabIndex = 7
        Me.grpTelefono.TabStop = False
        Me.grpTelefono.Text = "Teléfonos"
        '
        'lblContratoText
        '
        Me.lblContratoText.AutoSize = True
        Me.lblContratoText.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContratoText.Location = New System.Drawing.Point(128, 136)
        Me.lblContratoText.Name = "lblContratoText"
        Me.lblContratoText.Size = New System.Drawing.Size(103, 13)
        Me.lblContratoText.TabIndex = 54
        Me.lblContratoText.Text = "Contrato aprobado:"
        Me.lblContratoText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblContratoText.Visible = False
        '
        'lblProspecto
        '
        Me.lblProspecto.AutoSize = True
        Me.lblProspecto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProspecto.Location = New System.Drawing.Point(172, 124)
        Me.lblProspecto.Name = "lblProspecto"
        Me.lblProspecto.Size = New System.Drawing.Size(59, 13)
        Me.lblProspecto.TabIndex = 55
        Me.lblProspecto.Text = "Prospecto:"
        Me.lblProspecto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEmail
        '
        Me.txtEmail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmail.Location = New System.Drawing.Point(92, 96)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(164, 21)
        Me.txtEmail.TabIndex = 4
        Me.txtEmail.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 13)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Email:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(8, 75)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(65, 13)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "Tel. Celular:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(8, 51)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 13)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Tel. Alterno:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtTelAlterno2
        '
        Me.txtTelAlterno2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelAlterno2.Location = New System.Drawing.Point(92, 72)
        Me.txtTelAlterno2.Name = "txtTelAlterno2"
        Me.txtTelAlterno2.Size = New System.Drawing.Size(164, 21)
        Me.txtTelAlterno2.TabIndex = 3
        Me.txtTelAlterno2.TabStop = False
        '
        'txtTelAlterno1
        '
        Me.txtTelAlterno1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelAlterno1.Location = New System.Drawing.Point(92, 48)
        Me.txtTelAlterno1.Name = "txtTelAlterno1"
        Me.txtTelAlterno1.Size = New System.Drawing.Size(164, 21)
        Me.txtTelAlterno1.TabIndex = 2
        Me.txtTelAlterno1.TabStop = False
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(8, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(35, 13)
        Me.Label16.TabIndex = 31
        Me.Label16.Text = "Casa:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkContrato
        '
        Me.chkContrato.Location = New System.Drawing.Point(240, 121)
        Me.chkContrato.Name = "chkContrato"
        Me.chkContrato.Size = New System.Drawing.Size(16, 21)
        Me.chkContrato.TabIndex = 53
        Me.chkContrato.TabStop = False
        '
        'lnkQueja
        '
        Me.lnkQueja.AlternatingColor2 = System.Drawing.Color.Red
        Me.lnkQueja.AutoSize = True
        Me.lnkQueja.BackColor = System.Drawing.Color.Gainsboro
        Me.lnkQueja.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lnkQueja.LinkColor = System.Drawing.Color.Red
        Me.lnkQueja.Location = New System.Drawing.Point(8, 124)
        Me.lnkQueja.Name = "lnkQueja"
        Me.lnkQueja.Size = New System.Drawing.Size(79, 13)
        Me.lnkQueja.TabIndex = 5
        Me.lnkQueja.TabStop = True
        Me.lnkQueja.Text = "Queja Activa"
        Me.lnkQueja.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lnkQueja.TimerInterval = 500
        '
        'lblRazonSocial
        '
        Me.lblRazonSocial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRazonSocial.Location = New System.Drawing.Point(97, 65)
        Me.lblRazonSocial.Name = "lblRazonSocial"
        Me.lblRazonSocial.Size = New System.Drawing.Size(372, 21)
        Me.lblRazonSocial.TabIndex = 2
        Me.lblRazonSocial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCliente
        '
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.Color.Navy
        Me.lblCliente.Location = New System.Drawing.Point(97, 9)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(192, 21)
        Me.lblCliente.TabIndex = 0
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNombre
        '
        Me.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNombre.Location = New System.Drawing.Point(97, 41)
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.Size = New System.Drawing.Size(448, 21)
        Me.txtNombre.TabIndex = 1
        '
        'lblOrigenCliente
        '
        Me.lblOrigenCliente.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOrigenCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblOrigenCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOrigenCliente.Location = New System.Drawing.Point(622, 64)
        Me.lblOrigenCliente.Name = "lblOrigenCliente"
        Me.lblOrigenCliente.Size = New System.Drawing.Size(296, 21)
        Me.lblOrigenCliente.TabIndex = 85
        Me.lblOrigenCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRuta
        '
        Me.lblRuta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(622, 40)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(296, 21)
        Me.lblRuta.TabIndex = 1
        Me.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grpTanques
        '
        Me.grpTanques.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTanques.BackColor = System.Drawing.Color.Gainsboro
        Me.grpTanques.Controls.Add(Me.grdClienteEquipo)
        Me.grpTanques.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpTanques.Location = New System.Drawing.Point(568, 164)
        Me.grpTanques.Name = "grpTanques"
        Me.grpTanques.Size = New System.Drawing.Size(352, 80)
        Me.grpTanques.TabIndex = 71
        Me.grpTanques.TabStop = False
        Me.grpTanques.Text = "Equipos del cliente"
        '
        'grdClienteEquipo
        '
        Me.grdClienteEquipo.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.grdClienteEquipo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grdClienteEquipo.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grdClienteEquipo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdClienteEquipo.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdClienteEquipo.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.grdClienteEquipo.DataMember = ""
        Me.grdClienteEquipo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdClienteEquipo.FlatMode = True
        Me.grdClienteEquipo.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdClienteEquipo.ForeColor = System.Drawing.Color.MidnightBlue
        Me.grdClienteEquipo.GridLineColor = System.Drawing.Color.Gainsboro
        Me.grdClienteEquipo.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdClienteEquipo.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.grdClienteEquipo.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdClienteEquipo.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdClienteEquipo.LinkColor = System.Drawing.Color.Teal
        Me.grdClienteEquipo.Location = New System.Drawing.Point(3, 17)
        Me.grdClienteEquipo.Name = "grdClienteEquipo"
        Me.grdClienteEquipo.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.grdClienteEquipo.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.grdClienteEquipo.ReadOnly = True
        Me.grdClienteEquipo.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.grdClienteEquipo.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdClienteEquipo.Size = New System.Drawing.Size(346, 60)
        Me.grdClienteEquipo.TabIndex = 33
        Me.grdClienteEquipo.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.styClienteEquipo})
        '
        'styClienteEquipo
        '
        Me.styClienteEquipo.DataGrid = Me.grdClienteEquipo
        Me.styClienteEquipo.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colCESecuencia, Me.colCEEquipo, Me.colCETipoPropiedad, Me.colCESerie})
        Me.styClienteEquipo.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.styClienteEquipo.MappingName = "ClienteEquipo"
        Me.styClienteEquipo.ReadOnly = True
        Me.styClienteEquipo.RowHeadersVisible = False
        '
        'colCESecuencia
        '
        Me.colCESecuencia.Format = ""
        Me.colCESecuencia.FormatInfo = Nothing
        Me.colCESecuencia.HeaderText = "No."
        Me.colCESecuencia.MappingName = "Secuencia"
        Me.colCESecuencia.NullText = ""
        Me.colCESecuencia.Width = 30
        '
        'colCEEquipo
        '
        Me.colCEEquipo.Format = ""
        Me.colCEEquipo.FormatInfo = Nothing
        Me.colCEEquipo.HeaderText = "Equipo"
        Me.colCEEquipo.MappingName = "Equipo"
        Me.colCEEquipo.Width = 105
        '
        'colCETipoPropiedad
        '
        Me.colCETipoPropiedad.Format = ""
        Me.colCETipoPropiedad.FormatInfo = Nothing
        Me.colCETipoPropiedad.HeaderText = "Tipo"
        Me.colCETipoPropiedad.MappingName = "TipoPropiedad"
        Me.colCETipoPropiedad.Width = 140
        '
        'colCESerie
        '
        Me.colCESerie.Format = ""
        Me.colCESerie.FormatInfo = Nothing
        Me.colCESerie.HeaderText = "Serie"
        Me.colCESerie.MappingName = "Serie"
        Me.colCESerie.Width = 80
        '
        'grpProgramaCliente
        '
        Me.grpProgramaCliente.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpProgramaCliente.BackColor = System.Drawing.Color.Gainsboro
        Me.grpProgramaCliente.Controls.Add(Me.grdProgramaCliente)
        Me.grpProgramaCliente.Controls.Add(Me.lblObservacionesProgramacion)
        Me.grpProgramaCliente.Controls.Add(Me.lblProgramacion)
        Me.grpProgramaCliente.Controls.Add(Me.btnCalendario)
        Me.grpProgramaCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpProgramaCliente.Location = New System.Drawing.Point(568, 248)
        Me.grpProgramaCliente.Name = "grpProgramaCliente"
        Me.grpProgramaCliente.Size = New System.Drawing.Size(352, 112)
        Me.grpProgramaCliente.TabIndex = 88
        Me.grpProgramaCliente.TabStop = False
        Me.grpProgramaCliente.Text = "Programación del cliente"
        '
        'grdProgramaCliente
        '
        Me.grdProgramaCliente.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.grdProgramaCliente.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grdProgramaCliente.BackgroundColor = System.Drawing.Color.WhiteSmoke
        Me.grdProgramaCliente.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdProgramaCliente.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdProgramaCliente.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.grdProgramaCliente.DataMember = ""
        Me.grdProgramaCliente.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProgramaCliente.FlatMode = True
        Me.grdProgramaCliente.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdProgramaCliente.ForeColor = System.Drawing.Color.MidnightBlue
        Me.grdProgramaCliente.GridLineColor = System.Drawing.Color.Gainsboro
        Me.grdProgramaCliente.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdProgramaCliente.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.grdProgramaCliente.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdProgramaCliente.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdProgramaCliente.LinkColor = System.Drawing.Color.Teal
        Me.grdProgramaCliente.Location = New System.Drawing.Point(3, 17)
        Me.grdProgramaCliente.Name = "grdProgramaCliente"
        Me.grdProgramaCliente.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.grdProgramaCliente.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.grdProgramaCliente.ReadOnly = True
        Me.grdProgramaCliente.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.grdProgramaCliente.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdProgramaCliente.Size = New System.Drawing.Size(346, 72)
        Me.grdProgramaCliente.TabIndex = 0
        Me.grdProgramaCliente.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.styProgramacion})
        '
        'styProgramacion
        '
        Me.styProgramacion.DataGrid = Me.grdProgramaCliente
        Me.styProgramacion.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colProgTexto})
        Me.styProgramacion.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.styProgramacion.MappingName = "ProgramaCliente"
        Me.styProgramacion.ReadOnly = True
        Me.styProgramacion.RowHeadersVisible = False
        '
        'colProgTexto
        '
        Me.colProgTexto.Format = ""
        Me.colProgTexto.FormatInfo = Nothing
        Me.colProgTexto.HeaderText = "Programación"
        Me.colProgTexto.MappingName = "Texto"
        Me.colProgTexto.Width = 350
        '
        'lblObservacionesProgramacion
        '
        Me.lblObservacionesProgramacion.BackColor = System.Drawing.SystemColors.Control
        Me.lblObservacionesProgramacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblObservacionesProgramacion.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lblObservacionesProgramacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObservacionesProgramacion.Location = New System.Drawing.Point(3, 89)
        Me.lblObservacionesProgramacion.Name = "lblObservacionesProgramacion"
        Me.lblObservacionesProgramacion.Size = New System.Drawing.Size(346, 20)
        Me.lblObservacionesProgramacion.TabIndex = 1
        '
        'btnCalendario
        '
        Me.btnCalendario.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCalendario.BackColor = System.Drawing.SystemColors.Control
        Me.btnCalendario.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalendario.Location = New System.Drawing.Point(163, 0)
        Me.btnCalendario.Name = "btnCalendario"
        Me.btnCalendario.Size = New System.Drawing.Size(104, 17)
        Me.btnCalendario.TabIndex = 100
        Me.btnCalendario.Text = "Ver calendario!"
        Me.btnCalendario.UseVisualStyleBackColor = False
        Me.btnCalendario.Visible = False
        '
        'lblCtePadreEdif
        '
        Me.lblCtePadreEdif.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCtePadreEdif.Location = New System.Drawing.Point(568, 140)
        Me.lblCtePadreEdif.Name = "lblCtePadreEdif"
        Me.lblCtePadreEdif.Size = New System.Drawing.Size(152, 20)
        Me.lblCtePadreEdif.TabIndex = 37
        '
        'lblNombreEmpresa
        '
        Me.lblNombreEmpresa.AutoSize = True
        Me.lblNombreEmpresa.Font = New System.Drawing.Font("Verdana", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombreEmpresa.Location = New System.Drawing.Point(566, 6)
        Me.lblNombreEmpresa.Name = "lblNombreEmpresa"
        Me.lblNombreEmpresa.Size = New System.Drawing.Size(0, 26)
        Me.lblNombreEmpresa.TabIndex = 105
        '
        'cboOrigenCliente
        '
        Me.cboOrigenCliente.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboOrigenCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboOrigenCliente.Location = New System.Drawing.Point(622, 64)
        Me.cboOrigenCliente.Name = "cboOrigenCliente"
        Me.cboOrigenCliente.Size = New System.Drawing.Size(296, 21)
        Me.cboOrigenCliente.TabIndex = 10
        '
        'lblStatusCalidad
        '
        Me.lblStatusCalidad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatusCalidad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatusCalidad.Location = New System.Drawing.Point(622, 112)
        Me.lblStatusCalidad.Name = "lblStatusCalidad"
        Me.lblStatusCalidad.Size = New System.Drawing.Size(98, 21)
        Me.lblStatusCalidad.TabIndex = 91
        Me.lblStatusCalidad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblStatus
        '
        Me.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(622, 88)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(98, 21)
        Me.lblStatus.TabIndex = 90
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SeleccionCalleColonia
        '
        Me.SeleccionCalleColonia.AltaCalleColonia = True
        Me.SeleccionCalleColonia.Calle = 0
        Me.SeleccionCalleColonia.Colonia = 0
        Me.SeleccionCalleColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeleccionCalleColonia.Location = New System.Drawing.Point(9, 89)
        Me.SeleccionCalleColonia.Name = "SeleccionCalleColonia"
        Me.SeleccionCalleColonia.Size = New System.Drawing.Size(536, 144)
        Me.SeleccionCalleColonia.TabIndex = 5
        '
        'cboRamoCliente
        '
        Me.cboRamoCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRamoCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboRamoCliente.Location = New System.Drawing.Point(100, 48)
        Me.cboRamoCliente.Name = "cboRamoCliente"
        Me.cboRamoCliente.Size = New System.Drawing.Size(148, 21)
        Me.cboRamoCliente.TabIndex = 1
        '
        'txtLada
        '
        Me.txtLada.Location = New System.Drawing.Point(92, 24)
        Me.txtLada.Name = "txtLada"
        Me.txtLada.Size = New System.Drawing.Size(44, 21)
        Me.txtLada.TabIndex = 0
        Me.ttMensaje.SetToolTip(Me.txtLada, "Clave lada para los teléfonos del cliente")
        '
        'txtTelCasa
        '
        Me.txtTelCasa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelCasa.Location = New System.Drawing.Point(136, 24)
        Me.txtTelCasa.Name = "txtTelCasa"
        Me.txtTelCasa.Size = New System.Drawing.Size(120, 21)
        Me.txtTelCasa.TabIndex = 1
        '
        'cboRuta
        '
        Me.cboRuta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRuta.Location = New System.Drawing.Point(622, 40)
        Me.cboRuta.Name = "cboRuta"
        Me.cboRuta.Size = New System.Drawing.Size(296, 21)
        Me.cboRuta.TabIndex = 9
        '
        'lblProgramacion
        '
        Me.lblProgramacion.AutoSize = True
        Me.lblProgramacion.BackColor = System.Drawing.SystemColors.Control
        Me.lblProgramacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgramacion.Location = New System.Drawing.Point(288, 0)
        Me.lblProgramacion.Name = "lblProgramacion"
        Me.lblProgramacion.Size = New System.Drawing.Size(0, 13)
        Me.lblProgramacion.TabIndex = 93
        Me.lblProgramacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmCallCenter
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(1008, 541)
        Me.Controls.Add(Me.pnlCallCenter)
        Me.Controls.Add(Me.tbBarra)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "frmCallCenter"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CallCenter"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnlCallCenter.ResumeLayout(False)
        Me.pnlCallCenter.PerformLayout()
        Me.grpGeoReferencia.ResumeLayout(False)
        Me.grpGeoReferencia.PerformLayout()
        Me.grpVentasMultinivel.ResumeLayout(False)
        Me.grpVentasMultinivel.PerformLayout()
        CType(Me.grdLlamada, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grdClasificacion.ResumeLayout(False)
        Me.grdClasificacion.PerformLayout()
        Me.grpTelefono.ResumeLayout(False)
        Me.grpTelefono.PerformLayout()
        Me.grpTanques.ResumeLayout(False)
        CType(Me.grdClienteEquipo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpProgramaCliente.ResumeLayout(False)
        Me.grpProgramaCliente.PerformLayout()
        CType(Me.grdProgramaCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Barra de botones"


    Private Sub tbBarra_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbBarra.ButtonClick
        Select Case e.Button.Tag.ToString()
            Case "Cerrar"
                If _altaClienteWeb Then
                    If _Cliente <> 0 Then Me.DialogResult = DialogResult.OK
                    Me.Close()
                End If
                Me.Close()
            Case "Buscar"
                BuscarCliente()

            Case "Refrescar"
                Refrescar()

            Case "Nuevo"
                Nuevo()

            Case "Guardar"
                Guardar()

            Case "Modificar"
                Modificar()

            Case "Tanques"
                If _Cliente > 0 Then
                    ClienteEquipo()
                End If

            Case "Servicios"
                If _Cliente > 0 Then
                    Servicios()
                End If

            Case "ProgST"
                If _Cliente > 0 Then
                    ProgramacionServiciosTecnicos()
                End If

            Case "Pedido"
                'se agregó el pedido portátil 28/09/2004
                If _Cliente > 0 Then
                    If chkPortatil.Checked Then
                        PedidoPortatil()
                    Else
                        Pedido()
                    End If
                End If

            Case "Cancelacion"
                'se agregó el pedido portátil 28/09/2004
                If _Cliente > 0 Then
                    If chkPortatil.Checked Then
                        CancelaPedidoPortatil()
                    ElseIf _PedidoReferencia <> "" Then
                        CancelaPedido()
                    End If
                End If


            Case "Llamadas"
                If _Cliente > 0 Then
                    Llamada()
                End If

            Case ("Historico")
                If _Cliente > 0 Then
                    Historico()
                End If

            Case "ConsultaCliente"
                If _Cliente > 0 Then
                    ConsultaCliente()
                End If

            Case "ProgramacionOK"
                If _Cliente > 0 Then
                    Programacion()
                End If

            Case "PedidoBitacora"
                ConsultaPedidoBitacora()

            Case "Barra2"
                hideBar()

            Case "Queja"
                altaQueja()

            Case "Prospectos"
                Prospectos()
        End Select
    End Sub

    Private Sub ClientesHijos()
        Cursor = Cursors.WaitCursor
        'Dim oHijos As New AdmEdificios.ConsultaClientesHijos(_Cliente, GLOBAL_ConString)
        Dim oHijos As New AdmEdificios.ConsultaClientesHijos(_Cliente, GLOBAL_ConString, GLOBAL_Corporativo, GLOBAL_Sucursal, GLOBAL_Usuario)
        oHijos.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub ConsultaPedidoBitacora()
        If _PedidoReferencia <> "" Then
            Cursor = Cursors.WaitCursor
            Dim oPedidoBitacora As New SigaMetClasses.ConsultaPedidoBitacora(_PedidoReferencia)
            oPedidoBitacora.ShowDialog()
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Refrescar()
        'Se agregó pedido portátil
        If _Cliente > 0 And _ClienteCargado Then
            Me.CerrarNotas()
            If chkPortatil.Checked Then
                CargaClientePortatil(_Cliente)
            Else
                CargaCliente(_Cliente)
            End If
        Else
            Nuevo()
        End If
    End Sub

    Private Sub Programacion()
        ' If _CelulaCliente = GLOBAL_Celula Or Main.GLOBAL_CelulaAdmin = True Then
        Cursor = Cursors.WaitCursor
        Dim oProgramacion As Programacion.frmProgramacion
        oProgramacion = New Programacion.frmProgramacion(_Cliente, Main.GLOBAL_Usuario, True, SigaMetClasses.Enumeradores.enumTipoOperacionProgramacion.Alta, Main.GLOBAL_UserInfo, _
            GLOBAL_CapturaObservacionesInactivacionProg)
        If oProgramacion.ShowDialog() = DialogResult.OK Then
            Me.CargaProgramaCliente()
            Me.CargaPedido()
            Me.CargaLlamada()
        End If
        Cursor = Cursors.Default
        'Else
        'MessageBox.Show("El cliente no puede ser modificado porque no pertenece a su célula.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End If
    End Sub

    '    Private Sub Programacion()
    'TODO Mejorar
    'Cursor = Cursors.WaitCursor
    'Dim frmProgramacion As New CCProgramacion()

    'Try

    '    If lblProgramaTexto.Text <> "" Then
    '        frmProgramacion.Entrada(_Cliente, txtNombre.Text, 1, CType(lblPrograma.Text, Integer), SeleccionCalleColonia.CP, _RutaCliente)
    '        frmProgramacion.Dispose()

    '        CargaPrograma()
    '        CargaPedido()
    '        CargaLlamada()
    '    Else
    '        frmProgramacion.Entrada(_Cliente, txtNombre.Text, 0, 0, SeleccionCalleColonia.CP, _RutaCliente)
    '        frmProgramacion.Dispose()

    '        CargaPrograma()
    '        CargaPedido()
    '        CargaLlamada()
    '    End If
    'Catch ex As Exception
    '    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

    'Finally
    '    Cursor = Cursors.Default

    'End Try

    '   End Sub

    Private Sub AvanzaProgramacion()
        Cursor = Cursors.WaitCursor
        If lblProgramacion.Text.Trim <> "" Then
            Dim cmd As New SqlCommand("Select count(*) as Total from ProgramacionBeta Where Cliente = @Cliente", Main.CnnSigamet)
            With cmd
                .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
            End With
            Main.AbreConexion()
            Dim _Total As Integer = CType(cmd.ExecuteScalar, Integer)
            Main.CierraConexion()
            If _Total > 0 Then
                Dim oAvanzaProgramacion As New frmAvanzaProgramacion(_Cliente)
                If oAvanzaProgramacion.ShowDialog = DialogResult.OK Then
                    Me.CargaPedido()
                    Me.CargaLlamada()
                End If
                Cursor = Cursors.Default
            Else
                MessageBox.Show("El cliente no tiene una fecha de programación.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cursor = Cursors.Default
                Exit Sub
            End If
        Else
            MessageBox.Show("El cliente no tiene una programación asignada.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cursor = Cursors.Default
            Exit Sub
        End If

    End Sub

    Private Sub Guardar()
        'Agregado el 14/09/2004 para evitar que personal no autorizado maneje clientes de administración de edificios
        '14/09/2004 Jorge A. Guerrero
        'La validación del parametro contenido en GLOBAL_AplicaAdministracionEdificios se agregó el 29/09/2004

        If Not _EsClienteNuevo Then
            Exit Sub
        End If

        If GLOBAL_AplicaAdministracionEdificios Then
            If Not (oSeguridad.TieneAcceso("Administración de edificios")) And Trim(CType(cboRamoCliente.Text, String)) _
                     = GLOBAL_RamoClienteAdmEdificios Then
                MessageBox.Show("Solo el encargado de administración" & Chr(13) & _
                        "de edificios puede manejar clientes" & Chr(13) & "con el ramo 'Edificio administrado'", _
                        Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If

        If txtNombre.Text.Trim = "" Then
            MessageBox.Show("Debe teclear el nombre del cliente.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNombre.Focus()
            Exit Sub
        End If

        If cboRuta.SelectedIndex < 0 Then
            MessageBox.Show("Debe seleccionar la ruta.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboRuta.Focus()
            Exit Sub
        End If

        If cboOrigenCliente.SelectedIndex < 0 Then
            MessageBox.Show("Debe seleccionar el orígen del cliente.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboOrigenCliente.Focus()
            Exit Sub
        End If

        If cboRamoCliente.SelectedIndex < 0 Then
            MessageBox.Show("Debe seleccionar el ramo del cliente.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboRamoCliente.Focus()
            Exit Sub
        End If

        Cursor = Cursors.WaitCursor

        'If Not SeleccionCalleColonia.ExisteCalle Or _
        '    Not SeleccionCalleColonia.ExisteColonia Then
        '    SeleccionCalleColonia.GuardaDatos()
        'End If

        SeleccionCalleColonia.GuardaDatos()

        Dim oCliente As New SigaMetClasses.cCliente()
        Dim _NuevoCliente As Integer
        Try
            'se agregó pedido portátil
            '10/11/2004 Se agregó el parámetro Usuario, requerido por el spCCAltaModificaClientePortátil
            If chkPortatil.Checked Then

                Dim cteExistente As Integer = oCliente.validaDireccionPortatil(SeleccionCalleColonia.Calle, SeleccionCalleColonia.NumExterior, _
                    SeleccionCalleColonia.Colonia, SeleccionCalleColonia.NumInterior, CnnSigamet)
                If cteExistente <> -1 AndAlso cteExistente <> 0 Then
                    If MessageBox.Show("El cliente no. " & CStr(cteExistente) & " tiene la misma dirección" & Chr(13) & _
                        "del cliente que va a dar de alta. ¿Desea continuar?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) _
                            = DialogResult.No Then
                        Throw New Exception("No se guardó el cliente")
                    End If
                End If

                _NuevoCliente = oCliente.AltaModificaPortatil(GLOBAL_Celula, _
                                    txtNombre.Text.Trim, _
                                    SeleccionCalleColonia.Calle, _
                                    SeleccionCalleColonia.NumExterior, _
                                    SeleccionCalleColonia.Colonia, _
                                    CType(cboRuta.SelectedValue, Short), _
                                    CType(cboRamoCliente.SelectedValue, Short), _
                                    CType(cboOrigenCliente.SelectedValue, Byte), _
                                    txtTelCasa.Text.Trim, _
                                    txtTelAlterno1.Text.Trim, _
                                    SeleccionCalleColonia.NumInterior, _
                                    SeleccionCalleColonia.EntreCalle1, _
                                    SeleccionCalleColonia.EntreCalle2, _
                                    txtObservacionesCliente.Text.Trim, _
                                    GLOBAL_Usuario, _
                                    UserInfo:=Main.GLOBAL_UserInfo)
            Else
                Dim oSeguridad As New SigaMetClasses.cSeguridad(GLOBAL_Usuario, 1)
                Dim cteExistente As Integer = oCliente.validaDireccionEstacionario(CInt(Val(lblCliente.Text)), SeleccionCalleColonia.Calle, SeleccionCalleColonia.NumExterior, _
                    SeleccionCalleColonia.Colonia, SeleccionCalleColonia.NumInterior, CnnSigamet)
                If GLOBAL_VentasMultinivel AndAlso cteExistente > 0 AndAlso Not oSeguridad.TieneAcceso("AltaClienteExistente") Then
                    MessageBox.Show("Existe un cliente con la misma dirección del cliente que va a dar de alta." & Chr(13) & "No se guardará el nuevo cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
                _NuevoCliente = oCliente.AltaModifica(GLOBAL_Celula, _
                    txtNombre.Text.Trim, _
                    Main.PRED_TipoCliente, _
                    SeleccionCalleColonia.Calle, _
                    SeleccionCalleColonia.NumExterior, _
                    SeleccionCalleColonia.Colonia, _
                    CType(cboRuta.SelectedValue, Short), _
                    CType(cboRamoCliente.SelectedValue, Short), _
                    CType(cboOrigenCliente.SelectedValue, Byte), _
                    Main.PRED_ClasificacionCliente, _
                    txtTelCasa.Text.Trim, _
                    txtTelAlterno1.Text.Trim, _
                    txtTelAlterno2.Text.Trim, _
                     , , _
                    SeleccionCalleColonia.NumInterior, _
                    _Empresa, _
                    SeleccionCalleColonia.EntreCalle1, _
                    SeleccionCalleColonia.EntreCalle2, _
                    txtObservacionesCliente.Text.Trim, _
                    GLOBAL_Usuario, _
                    UserInfo:=Main.GLOBAL_UserInfo, _
                    Email:=txtEmail.Text.Trim, _
                    VIP:=chkVIP.Checked, _
                    ContratoAprobado:=chkContrato.Checked, _
                    Promotor:=_promotor, _
                    Lada:=txtLada.Text.Trim)
            End If
            '28/09/2004
            'Se agregaron los parametros Email y VIP
            _EsClienteNuevo = False

            MessageBox.Show(SigaMetClasses.M_DATOS_OK, _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)

            lblCliente.Text = _NuevoCliente.ToString
            'lblCelula.Text = GLOBAL_CelulaDescripcion

            txtNombre.Focus()
            btnGuardar.Enabled = False

            'Vuelve a cargar el cliente
            _Cliente = _NuevoCliente

            'If _altaClienteWeb Then
            '    Me.DialogResult = DialogResult.OK
            '    Me.Close()
            'End If

            'se agregó pedido portátil
            chkPortatil.Enabled = False
            If chkPortatil.Checked Then
                CargaClientePortatil(_Cliente)
            Else
                CargaCliente(_Cliente)
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            oCliente.Dispose()
        End Try

    End Sub

    Private Sub ClienteEquipo()
        Cursor = Cursors.WaitCursor
        Dim oTanque As New Tanque()

        If Not IsNothing(grdClienteEquipo.DataSource) Then
            If CType(grdClienteEquipo.DataSource, DataTable).Rows.Count = 0 Then
                oTanque.Entrada(_Cliente, txtNombre.Text, 0, 0)
            Else
                oTanque.Entrada(_Cliente, txtNombre.Text, 1, CType(grdClienteEquipo.Item(grdClienteEquipo.CurrentRowIndex, 0), Integer))
            End If
        Else
            oTanque.Entrada(_Cliente, txtNombre.Text, 0, 0)
        End If


        oTanque.Dispose()

        CargaClienteEquipo()

        Cursor = Cursors.Default
    End Sub

    Private Sub CancelaPedido()
        If _Cliente <= 0 Then
            Exit Sub
        End If

        'Para evitar la cancelación de servicios técnicos en esta pantalla
        If _Producto.Trim = "4" Then
            MessageBox.Show("No puede cancelar servicios técnicos," & vbCrLf & _
                            "contacte al área encargada.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If
        'TODO: Para permitir cancelación de pedidos de cualquier cel.
        'If ((_CelulaCliente = GLOBAL_Celula) Or Main.GLOBAL_CelulaAdmin = True) Or oSeguridad.TieneAcceso("Calidad") Then
        '    Cursor = Cursors.WaitCursor
        'Dim frmMotivoCancelacion As New MotivoCancelacion()
        'frmMotivoCancelacion.Entrada(_Cliente, _AñoPed, _Celula, _Pedido, txtNombre.Text)
        'frmMotivoCancelacion.Dispose()
        'CargaCliente(_Cliente)

        Dim oCancelacionPedido As New SigaMetClasses.frmCancelacionPedido(Me._AñoPed, Me._Celula, Me._Pedido, Main.GLOBAL_UserInfo, GLOBAL_ConString)

        Dim oPedido As New SigaMetClasses.cPedido(Me._AñoPed, Me._Celula, Me._Pedido)

        If oCancelacionPedido.ShowDialog = DialogResult.OK Then
            Me.CargaCliente(_Cliente)

            If GLOBAL_UsarSigametServices Then
                Dim servicioPedido As New desarrollogm.Pedido()

                servicioPedido.Url = GLOBAL_URLWebserviceBoletin

                Try
                    servicioPedido.CancelaPedidoCallCenter(Main.GLOBAL_Estacion, oPedido.PedidoReferencia)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If

        Cursor = Cursors.Default
        'Else
        '    MessageBox.Show("Sólo puede cancelar pedidos de clientes de su célula.", Me._Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

    End Sub

    'Private Sub Pedido()
    '    Dim frmPedidoAlta As frmPedido
    '    Dim oPedido As SigaMetClasses.cPedido

    '    oPedido = New SigaMetClasses.cPedido()
    '    oPedido.ConsultaPedidoActivo(_Cliente)

    '    If oPedido.Pedido <> 0 Then
    '        frmPedidoAlta = New frmPedido(_Cliente, oPedido.PedidoReferencia)
    '    Else
    '        frmPedidoAlta = New frmPedido(_Cliente)
    '    End If

    '    frmPedidoAlta.ShowDialog()

    'End Sub


    Private Sub Pedido()

        If _Cliente <= 0 Then
            Exit Sub
        End If

        'Dim Nombre As String
        Dim cmd As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Ruta As Short = Nothing

        Dim Anio As Short
        Dim Celula As Byte
        Dim Pedido As Integer

        Dim Existe As Integer
        Dim FCompromiso As Date
        Dim FActual As Date
        Dim Boletin As Integer

        Dim DescRuta As String = ""


        cmd.Connection = CnnSigamet
        cmd.CommandTimeout = 30
        cmd.CommandType = CommandType.Text

        cmd.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                " Select Count(*) as Registro from Pedido INNER JOIN Producto ON Pedido.Producto=Producto.Producto and Producto.TipoProducto=1 where Cliente=@Cliente and Status='ACTIVO' and TipoCargo=1 and TipoPedido in (1,2)  " & _
                                " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
        cmd.Parameters.Clear()
        cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente

        AbreConexion()

        rdrInsert = cmd.ExecuteReader()
        rdrInsert.Read()
        Existe = CType(rdrInsert("Registro"), Integer)
        rdrInsert.Close()
        cmd.Dispose()

        If cboRuta.Text = "" Then
            DescRuta = lblRuta.Text
        Else
            DescRuta = cboRuta.Text
        End If
        If lvwPedido.Items.Count = 0 Then
            Dim frmPedido As New Pedido()
            frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, _Colonia, _CelulaCliente, DescRuta, 0, _
            ColoniaNombre:=_ColoniaNombre)
            'frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, SeleccionCalleColonia.Colonia.ToString, _CelulaCliente, cboRuta.Text, 0)
            frmPedido.Dispose()

            CargaPedido()

            CargaLlamada()

        Else
            If Existe = 0 Then
                Dim frmPedido As New Pedido()
                frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, _Colonia, _
                    _CelulaCliente, DescRuta, 0, mensajeCreditoExcedido:=GLOBAL_MensajeCreditoExcedido, ColoniaNombre:=_ColoniaNombre)
                'frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, SeleccionCalleColonia.Colonia.ToString, _
                '    _CelulaCliente, cboRuta.Text, 0, GLOBAL_MensajeCreditoExcedido)
                frmPedido.Dispose()

                CargaPedido()

                CargaLlamada()

            Else

                cmd.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " Select FCompromiso, getdate() as FActual, Pedido, AñoPed, Celula from Pedido INNER JOIN Producto ON Pedido.Producto=Producto.Producto and Producto.TipoProducto=1 where Cliente=@Cliente and Status='ACTIVO' and TipoCargo=1 and TipoPedido in (1,2) " & _
                                        " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                rdrInsert = cmd.ExecuteReader()
                rdrInsert.Read()
                FCompromiso = CType(rdrInsert("FCompromiso"), Date)
                FActual = CType(rdrInsert("FActual"), Date)
                Pedido = CType(rdrInsert("Pedido"), Integer)
                Anio = CType(rdrInsert("AñoPed"), Short)
                Celula = CType(rdrInsert("Celula"), Byte)
                rdrInsert.Close()
                cmd.Dispose()

                cmd.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " Select Count(*) as Nota from Nota where Celula=@Celula and Pedido=@Pedido and AñoPed=@Anio " & _
                                        " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                cmd.Parameters.Clear()
                cmd.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = Anio
                rdrInsert = cmd.ExecuteReader()
                rdrInsert.Read()
                Boletin = CType(rdrInsert("Nota"), Integer)
                rdrInsert.Close()
                cmd.Dispose()


                If Boletin > 0 Then
                    If FActual.Date < FCompromiso.Date Then

                        Dim frmPedido As New Pedido()
                        frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, _Colonia, _CelulaCliente, DescRuta, 1, _
                        ColoniaNombre:=_ColoniaNombre)
                        'frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, SeleccionCalleColonia.Colonia.ToString, _CelulaCliente, cboRuta.Text, 1)
                        frmPedido.Dispose()

                        CargaPedido()

                        CargaLlamada()

                    Else
                        If MsgBox("El cliente tiene pedidos activos." + Chr(13) + "No se puede modificar el pedido, por que la fecha compromiso ya no es menor al dia de hoy." + Chr(13) + Chr(13) + "EL OPERADOR YA LLEVA LA NOTA DEL PEDIDO ACTIVO." + Chr(13) + Chr(13) + "¿Desea registrar este pedido como un boletin con llamada del cliente?.", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then

                            cmd.CommandType = CommandType.Text
                            'cmd.CommandText = " Update Pedido set StatusBoletin='BOLETIN', LlamadaInsistente=1 where Celula=@Celula and AñoPed=@Anio and Pedido=@Pedido "
                            cmd.CommandText = "spCCActualizaBoletinAPedido"
                            cmd.CommandType = CommandType.StoredProcedure

                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                            cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                            cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = Anio
                            cmd.ExecuteNonQuery()


                            cmd.CommandText = "sp_InsertaLlamada"
                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                            cmd.Parameters.Add("@Observaciones", SqlDbType.Text).Value = ""
                            cmd.Parameters.Add("@TelefonoOrigen", SqlDbType.Char).Value = ""
                            cmd.Parameters.Add("@MotivoLlamada", SqlDbType.Int).Value = 13
                            cmd.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                            cmd.Parameters.Add("@AñoPed", SqlDbType.Int).Value = Anio
                            cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                            cmd.Parameters.Add("@Operador", SqlDbType.Int).Value = 0
                            cmd.Parameters.Add("@Autotanque", SqlDbType.Int).Value = 0
                            cmd.Parameters.Add("@Demandante", SqlDbType.Char).Value = ""
                            cmd.ExecuteNonQuery()
                        End If

                        Exit Sub

                    End If
                Else

                    If FActual.Date <= FCompromiso.Date Then

                        Dim frmPedido As Pedido = New Pedido()
                        frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, _Colonia, _CelulaCliente, DescRuta, 1, _
                        ColoniaNombre:=_ColoniaNombre)
                        'frmPedido.Entrada(_Cliente, txtNombre.Text, SeleccionCalleColonia.CP, _RutaCliente, SeleccionCalleColonia.Colonia.ToString, _CelulaCliente, cboRuta.Text, 1)
                        frmPedido.Dispose()

                        CargaPedido()
                        CargaLlamada()

                    Else
                        If MsgBox("El cliente tiene pedidos activos." & Chr(13) & "No se puede modificar el pedido, por que la fecha compromiso ya no es menor al dia de hoy." & Chr(13) & Chr(13) + "EL OPERADOR YA LLEVA LA NOTA DEL PEDIDO ACTIVO." + Chr(13) + Chr(13) + "¿Desea registrar este pedido como un boletin con llamada del cliente?.", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then

                            cmd.CommandType = CommandType.Text
                            cmd.CommandText = "spCCActualizaBoletinAPedido"
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                            cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                            cmd.Parameters.Add("@Anio", SqlDbType.Int).Value = Anio
                            cmd.ExecuteNonQuery()

                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.CommandText = "sp_InsertaLlamada"
                            cmd.Parameters.Clear()
                            cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                            cmd.Parameters.Add("@Observaciones", SqlDbType.Text).Value = ""
                            cmd.Parameters.Add("@TelefonoOrigen", SqlDbType.Char).Value = ""
                            cmd.Parameters.Add("@MotivoLlamada", SqlDbType.Int).Value = 13
                            cmd.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                            cmd.Parameters.Add("@AñoPed", SqlDbType.Int).Value = Anio
                            cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                            cmd.Parameters.Add("@Operador", SqlDbType.Int).Value = 0
                            cmd.Parameters.Add("@Autotanque", SqlDbType.Int).Value = 0
                            cmd.Parameters.Add("@Demandante", SqlDbType.Char).Value = ""
                            cmd.ExecuteNonQuery()
                        End If
                        Exit Sub
                    End If

                End If

            End If
        End If

        CierraConexion()

        'CargaCliente(_Cliente)

    End Sub

    Private Sub Modificar()
        Dim accesoCalidad As Boolean = oSeguridad.TieneAcceso("Calidad")
        Dim modificarStatusCalidad As Boolean = oSeguridad.TieneAcceso("ModificarStatusCalidad")
        If _Cliente > 0 Then
            'Se agregó la funcionalidad del cambio de datos del modulo de calidad 5-11-2004
            'Jorge A. Guerrero
            'MHV. Se va a mover para lo de todos ven todo. 26/11/2004
            If _StatusCalidad <> "VERIFICADO" Or accesoCalidad Then
                'If (_CelulaCliente = GLOBAL_Celula Or Main.GLOBAL_CelulaAdmin = True) Or accesoCalidad Then
                'If _StatusCalidad <> "VERIFICADO" Then
                '    If _CelulaCliente = GLOBAL_Celula Or Main.GLOBAL_CelulaAdmin = True Then

                Cursor = Cursors.WaitCursor

                Dim _CambiarClientePadre As Boolean
                If Me.cboRamoCliente.Text.Trim = GLOBAL_RamoClienteAdmEdificios Then
                    '_CambiarClientePadre = True
                    _CambiarClientePadre = False
                End If
                'se agregó cliente portátil
                Dim oCliente As SigaMetClasses.ModificaCliente

                Try
                    oCliente = New SigaMetClasses.ModificaCliente(_Cliente, _
                                                            GLOBAL_Usuario, _
                                                            PermiteModificarStatus:=accesoCalidad, _
                                                            PermiteModificarStatusCalidad:=modificarStatusCalidad, _
                                                            PermiteModificarCelula:=accesoCalidad, _
                                                            PermiteCambiarClientePadre:=_CambiarClientePadre, _
                                                            UserInfo:=Main.GLOBAL_UserInfo, _
                                                            Portatil:=chkPortatil.Checked, _
                                                            aplicarAdmEdificios:=GLOBAL_AplicaAdministracionEdificios, _
                                                            ramoadmedificios:=GLOBAL_RamoClienteAdmEdificios, _
                                                            ManejarClientesPortatil:=GLOBAL_ManejarClientesPortatil, _
                                                            habilitarcambiodatosfiscales:=oSeguridad.TieneAcceso("Cambio de datos fiscales"), _
                                                            AltaCalleColonia:=GLOBAL_AltaCalleColonia, _
                                                            CambioPorcentajeComision:=oSeguridad.TieneAcceso("FactorComisionCliente"), _
                                                            CambioStatusContratoFirmado:=oSeguridad.TieneAcceso("CapturaContratoFirmado"), _
                                                            CambioDigitoVerificador:=oSeguridad.TieneAcceso("CAMBIO_DIGITOVERIFICADOR"))
                Catch ex As Exception
                    MessageBox.Show(ex.Message)
                    Return
                End Try
                'Dim oCliente As New SigaMetClasses.ModificaCliente(_Cliente, _
                '                                            GLOBAL_Usuario, _
                '                                            PermiteModificarStatus:=False, _
                '                                            PermiteModificarStatusCalidad:=False, _
                '                                            PermiteCambiarClientePadre:=_CambiarClientePadre, _
                '                                            UserInfo:=Main.GLOBAL_UserInfo, _
                '                                            Portatil:=chkPortatil.Checked, _
                '                                            aplicarAdmEdificios:=GLOBAL_AplicaAdministracionEdificios, _
                '                                            ramoadmedificios:=GLOBAL_RamoClienteAdmEdificios, _
                '                                            ManejarClientesPortatil:=GLOBAL_ManejarClientesPortatil, _
                '                                            habilitarcambiodatosfiscales:=oSeguridad.TieneAcceso("Cambio de datos fiscales"))

                'Control de alta de calles y colonias
                oCliente.AltaCalleColonia = GLOBAL_AltaCalleColonia


                ''oCliente.TopLevel = False
                ''Me.SuspendLayout()
                ''Me.Controls.Add(oCliente)
                ''oCliente.BringToFront()
                oCliente.ShowDialog()
                'se agregó cliente portátil
                If chkPortatil.Checked Then
                    CargaClientePortatil(_Cliente)
                Else
                    CargaCliente(_Cliente)
                End If
                Cursor = Cursors.Default
                'Else
                '    MessageBox.Show("El cliente no puede ser modificado porque no pertenece a su célula.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
                'End If

            Else
                MessageBox.Show("El cliente no puede ser modificado porque tiene estatus VERIFICADO.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

    End Sub


    Private Sub Llamada()
        Cursor = Cursors.WaitCursor

        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Pedido As Integer
        Dim Año As Short

        'cmdInsert.CommandText = "Select AñoPed, Celula, Pedido from Pedido where Cliente=@Cliente and Status='ACTIVO' "
        'cmdInsert.Connection = CnnSigamet
        'cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente

        ''Pedido de cliente portatil
        'If chkPortatil.Checked Then
        '    cmdInsert.CommandText = "Select datepart(y,FCompromiso) as AñoPed, PedidoPortatil as Pedido from PedidoPortatil where ClientePortatil= @Cliente and Status In('ACTIVO','RADIADO') "
        'End If

        cmdInsert.CommandText = "spCCConsultaPedidosActivosPorCliente"
        cmdInsert.Connection = CnnSigamet
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente

        If chkPortatil.Checked Then
            cmdInsert.Parameters.Add("@Portatil", SqlDbType.Bit).Value = 1
        Else
            cmdInsert.Parameters.Add("@Portatil", SqlDbType.Bit).Value = 0
        End If


        AbreConexion()

        rdrInsert = cmdInsert.ExecuteReader(CommandBehavior.CloseConnection)
        If rdrInsert.Read = False Then
            Pedido = 0
            Año = 0
        Else
            Pedido = CType(rdrInsert("Pedido"), Integer)
            Año = CType(rdrInsert("AñoPed"), Short)
        End If

        CierraConexion()

        cmdInsert.Dispose()
        'se agregó cliente portátil
        Dim frmLlamada As New Llamada()
        frmLlamada.Entrada(_Cliente, txtNombre.Text, _CelulaCliente, Pedido, txtTelCasa.Text.Trim, _RutaCliente, Año, 0, _FAlta, chkPortatil.Checked, _FAlta)
        'frmLlamada.setCelda(_numeroCelda)
        '_numeroCelda = frmLlamada.getCelda()
        frmLlamada.Dispose()
        'dsCallCenter.Llamada.Clear()
        'cmdCLlamada.Parameters("@Cliente").Value = CType(lbContrato.Text, Integer)
        'daLlamadas.Fill(dsCallCenter, "Llamada")
        Cursor = Cursors.Default
        If chkPortatil.Checked Then
            CargaLlamadaPortatil()
        Else
            CargaLlamada()
        End If

    End Sub

    Private Sub Servicios()
        If _Cliente > 0 Then
            Cursor = Cursors.WaitCursor
            Dim oST As SigametST.frmServicios
            Try
                oST = New SigametST.frmServicios(_Cliente, Now.Date, GLOBAL_Usuario, GLOBAL_Corporativo, GLOBAL_Sucursal)
                oST.ShowDialog()
                CargaPedido()
                CargaLlamada()
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Cursor = Cursors.Default
            End Try
        End If

    End Sub

    Private Sub BuscarCliente()
        Cursor = Cursors.WaitCursor
        Dim frmBuscar As New SigaMetClasses.BusquedaCliente(Celula:=Main.GLOBAL_Celula, Remoto:=Main.GLOBAL_Remoto, PriodidadPortatil:=GLOBAL_PrioridadPortatil)
        'agregado el 28/09/2004
        frmBuscar.ClientesPortatil = GLOBAL_ManejarClientesPortatil
        If GLOBAL_ManejarClientesPortatil Then

        End If
        'LUSATE GeoReferencia
        btnGeoreferenciar.Enabled = False
        lblValorX.Text = ""
        lblValorY.Text = ""

        If frmBuscar.ShowDialog = DialogResult.OK Then
            _Cliente = frmBuscar.Cliente            
            'agregado el 28/09/2004
            If _Cliente <> 0 Then
                chkPortatil.Checked = frmBuscar.ClientesPortatil
                chkPortatil.Enabled = False
                If Not frmBuscar.ClientesPortatil Then
                    CargaCliente(_Cliente)                    
                Else
                    CargaClientePortatil(_Cliente)
                End If
                'LUSATE Alerta RAF
                lnkAlertaRAF.Visible = ConsultaRAFPorRuta(_RutaCliente)
            End If

        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub Nuevo()
        LimpiaTodo()
        SeleccionCalleColonia.CausesValidation = True
        SeleccionCalleColonia.LimpiaTodo()
        DatosIniciales()

        ActivaBarraBotones(False)
        _EsClienteNuevo = False
        btnGuardar.Enabled = False

        Me.BackColor = CONFIG_ColorFondo

        cboRuta.SelectedIndex = -1
        cboRamoCliente.SelectedIndex = -1
        btnSeleccionaEmpresa.Visible = True
        btnSeleccionaEmpresa.Enabled = False

        btnModificar.Enabled = True
        btnProgramacionOK.Enabled = True
        btnServicios.Enabled = True
        btnProgST.Enabled = True
        btnPedido.Enabled = True
        btnCancelacion.Enabled = True
        btnPedidoBitacora.Enabled = True
        btnLlamadas.Enabled = True
        btnHistorico.Enabled = True
        btnConsultaCliente.Enabled = True

        chkContrato.Checked = False
        chkContrato.Enabled = True

        txtNombre.Focus()
        Me.Text = "CallCenter"

        If GLOBAL_GruposPromocionales Then
            AddHandler cboOrigenCliente.SelectedIndexChanged, AddressOf cboOrigenCliente_SelectedIndexChanged
        End If
    End Sub

    Private Sub ConsultaCliente()

        Dim oConfig As New SigaMetClasses.cConfig(GLOBAL_Modulo, GLOBAL_Corporativo, GLOBAL_Sucursal)

        Try
            _URLGateway = oConfig.Parametros("URLGateway")

        Catch ex As Exception
            'MessageBox.Show("El parámetro _URLGateway no está configurado.  " + ex.Message)
        End Try


        Dim oSigamet As New SigaMetClasses.frmConsultaCliente(_Cliente)
        Dim oConsultaCliente As SigaMetClasses.frmConsultaCliente
        oSigamet.Modulo = GLOBAL_Modulo  'SigaMetClasses.GLOBAL_Modulo

        If (_URLGateway Is String.Empty Or _URLGateway Is Nothing) Then
            oConsultaCliente = New SigaMetClasses.frmConsultaCliente(_Cliente)
        Else
            oConsultaCliente = New SigaMetClasses.frmConsultaCliente(_Cliente, _URLGateway, GLOBAL_ConString, GLOBAL_Usuario, GLOBAL_Modulo)
            'oConsultaCliente = New SigaMetClasses.frmConsultaCliente(_Cliente, _URLGateway)
        End If
        Cursor = Cursors.WaitCursor
        oConsultaCliente.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub Historico()
        Cursor = Cursors.WaitCursor
        Dim oBitacora As New SigaMetClasses.frmClienteBitacora(_Cliente)
        oBitacora.ShowDialog()
        Cursor = Cursors.Default

    End Sub

#End Region

    Public Sub New(ByVal Cliente As Integer, Optional ByVal Portatil As Boolean = False)
        MyBase.New()
        InitializeComponent()
        'se agregó cliente portatil 28/09/2004
        chkPortatil.Visible = GLOBAL_ManejarClientesPortatil
        activaATMasCercano(GLOBAL_AplicaATMasCercano)
        _ClientePreCarga = Cliente
        If GLOBAL_ManejarClientesPortatil Then
            chkPortatil.Checked = Portatil
            lblReferencia.Visible = Not Portatil
        End If
        _PreCarga = True
    End Sub

#Region "Carga de datos"

    Private Sub CargaCliente(ByVal Cliente As Integer)
        Dim _CargaDatosCliente As Boolean = False 'Indica si se cargaron correctamente los datos del cliente 

        Cursor = Cursors.WaitCursor

        'Control de Ventas Multinivel
        btnVentasMultinivel.Enabled = True

        Dim strQuery As String = "spCCConsultaVwDatosCliente"
        Dim cmd As New SqlCommand(strQuery, CnnSigamet)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
        Dim dr As SqlDataReader
        Try
            AbreConexion()

            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read Then

                '02/03/2012 #CASALA - En el caso de que se valide que se carguen las rutas solo del usuario
                'Y cuando se busque un cliente si el cliente pertenece a una celula que no tiene asignada el usuario
                'manda la notificación
                If VerificaCelulasUsuarioIgualCelulaCliente(CType(dr("Celula"), Byte)) = False Then
                    MessageBox.Show("Este cliente pertenece a la célula: '" + CType(dr("CelulaDescripcion"), String).Trim + "'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                'Restablecer barra principal
                RestablecerBarraPrincipal()

                'Dim dr As DataRow = dt.Rows(0)
                lblCtePadreEdif.Text = ""
                'Botones
                btnSeleccionaEmpresa.Enabled = False
                btnGuardar.Enabled = False

                lblCliente.Text = CType(dr("Cliente"), Integer).ToString
                _CelulaCliente = CType(dr("Celula"), Byte)
                _RutaCliente = CType(dr("Ruta"), Short)

                'Cambio de color de la célula si pertenece a otra célula
                If _CelulaCliente = GLOBAL_Celula Then
                    lblCelula.ForeColor = Color.MediumBlue
                    Me.BackColor = CONFIG_ColorFondo
                Else
                    lblCelula.ForeColor = Color.Firebrick
                    Me.BackColor = CONFIG_ColorFondoAlterno
                End If

                lblCelula.Text = CType(dr("CelulaDescripcion"), String).Trim

                txtNombre.Text = CType(dr("Nombre"), String).Trim
                txtNombre.ReadOnly = True

                If Not IsDBNull(dr("Empresa")) Then
                    _Empresa = CType(dr("Empresa"), Integer)
                    lblRazonSocial.Text = CType(dr("RazonSocial"), String).Trim
                    btnConsultaEmpresa.Enabled = True
                    btnSeleccionaEmpresa.Visible = False
                Else
                    _Empresa = 0
                    lblRazonSocial.Text = ""
                    btnConsultaEmpresa.Enabled = False
                    btnSeleccionaEmpresa.Visible = False
                End If

                txtObservacionesCliente.Text = CType(dr("Observaciones"), String).Trim
                txtObservacionesCliente.ReadOnly = True

                'Validación de la ruta si el cliente es de otra célula diferente a la mía

                cboRuta.Visible = False
                lblRuta.Visible = True
                lblRuta.Text = CType(dr("RutaDescripcion"), String).Trim

                If Not IsDBNull(dr("OrigenClienteDescripcion")) Then
                    lblOrigenCliente.Text = CType(dr("OrigenClienteDescripcion"), String).Trim
                End If

                lblOrigenCliente.Visible = True
                cboOrigenCliente.Visible = False

                _StatusCalidad = CType(dr("StatusCalidad"), String).Trim
                lblStatus.Text = CType(dr("Status"), String).Trim
                lblStatusCalidad.Text = _StatusCalidad
                If _StatusCalidad = "VERIFICADO" Then
                    lblStatusCalidad.ForeColor = Color.Green
                Else
                    lblStatusCalidad.ForeColor = Color.Black
                End If

                lblFAlta.Text = CType(dr("FAlta"), Date).ToString

                'Teléfonos
                txtLada.Text = CType(dr("Lada"), String).Trim

                _TelCasa = CType(dr("TelCasa"), String).Trim
                txtTelCasa.Text = SigaMetClasses.FormatoTelefono(CType(dr("TelCasa"), String).Trim)
                txtTelAlterno1.Text = CType(dr("TelAlterno1"), String).Trim
                txtTelAlterno2.Text = CType(dr("TelAlterno2"), String).Trim
                txtEmail.Text = CType(dr("Email"), String).Trim

                txtLada.ReadOnly = True
                txtTelCasa.ReadOnly = True
                txtTelAlterno1.ReadOnly = True
                txtTelAlterno2.ReadOnly = True
                txtEmail.ReadOnly = True

                'Clasificación
                'lblTipoCliente.Text = CType(dr("TipoClienteDescripcion"), String).Trim
                lblClasificacionCliente.Text = CType(dr("ClasificacionClienteDescripcion"), String).Trim
                cboRamoCliente.SelectedValue = CType(dr("RamoCliente"), Short)
                cboRamoCliente.Enabled = False
                lblTipoFactura.Text = CType(dr("TipoFacturaDescripcion"), String).Trim
                lblCartera.Text = CType(dr("CarteraDescripcion"), String).Trim
                'VIP
                chkVIP.Checked = CType(dr("VIP"), Boolean)
                chkVIP.Enabled = False

                'TODO: Validación de crédito excedido JAGD 27/12/2004
                lblCreditoExcedido.Text = ""
                If Not IsDBNull(dr("Saldo")) And Not IsDBNull(dr("maxImporteCredito")) Then
                    If CType(dr("Saldo"), Decimal) > CType(dr("maxImporteCredito"), Decimal) Then
                        lblCreditoExcedido.Text = GLOBAL_MensajeCreditoExcedido
                    End If
                End If

                If Not IsDBNull(dr("MsgCobranza")) Then
                    lblCreditoExcedido.Text = CType(dr("MsgCobranza"), String)
                End If

                If Not IsDBNull(dr("Colonia")) Then
                    _Colonia = CInt(dr("Colonia"))
                    _ColoniaNombre = CStr(dr("ColoniaNombre"))
                Else
                    _Colonia = 0
                    _ColoniaNombre = ""
                End If

                If Not IsDBNull(dr("ClientePadreEdificio")) Then
                    _ClientePadreEdificio = CInt(dr("ClientePadreEdificio"))
                Else
                    _ClientePadreEdificio = Nothing
                End If

                If Not IsDBNull(dr("Referencia")) Then
                    lblReferencia.Text = CType(dr("Referencia"), String)
                Else
                    lblReferencia.Text = String.Empty
                End If

                'Consulta de contrato padre
                If Not IsDBNull(dr("ClientePadre")) Then
                    _ClientePadre = CType(dr("ClientePadre"), Integer)
                    If _ClientePadre > 0 AndAlso _ClientePadre <> _Cliente Then
                        lblCtePadreEdif.Text = "Cte. Padre " & CType(_ClientePadre, Integer)
                        lblCtePadreEdif.Tag = _ClientePadre.ToString()
                        AddHandler lblCtePadreEdif.Click, AddressOf lnkDetalleClientePadre
                    End If
                End If

                If Not IsDBNull(dr("GiroClienteDescripcion")) Then
                    lblGiroCliente.Text = CType(dr("GiroClienteDescripcion"), String)
                End If

                'Consulta de contrato firmado sigamet corporativo
                'If Not IsDBNull(dr("ContratoAprobado")) Then
                '    chkContrato.Checked = CBool(dr("ContratoAprobado"))
                'Else

                'Para mostrar cuando el cliente está en estado de PROSPECTO 23-09-2013 jagd
                chkContrato.Checked = Convert.ToBoolean(lblStatus.Text.Trim().ToUpper = "PROSPECTO")
                chkContrato.Enabled = False

                'Consulta de factor de comisión por cliente
                If Not IsDBNull(dr("PorcentajeComisionVenta")) Then
                    lblComision.Text = CStr(dr("PorcentajeComisionVenta"))
                    If lblComision.Text.Trim.Length > 0 Then
                        lblComision.Text = lblComision.Text & " %"
                    End If
                End If

                'Consulta de quejas activas
                If Not IsDBNull(dr("QuejaActiva")) Then
                    lnkQueja.Enabled = True
                    lnkQueja.Visible = True

                    If lnkQueja.Enabled Then
                        lnkQueja.Text = CType(dr("QuejaActiva"), String)
                    End If
                Else
                    lnkQueja.Enabled = False
                    lnkQueja.Visible = False
                End If

                If GLOBAL_GruposPromocionales Then
                    _promotor = CType(dr("Promotor"), Integer)
                    If _promotor <> Nothing Then
                        lblPromotor.Text = CType(dr("NombrePromotor"), String)
                    End If
                End If

                'LUSATE
                lnkNoSuministrar.Visible = CType(dr("NoSuministrar"), Boolean)

                'LUSATE GeoReferencia
                lblValorX.Text = CType(dr("X"), String)
                lblValorY.Text = CType(dr("Y"), String)

                dr.Close()

                SeleccionCalleColonia.CargaDatosClienteSoloLectura(Cliente)

                _CargaDatosCliente = True

                'LUSATE GeoReferencia
                btnGeoreferenciar.Enabled = Global_HabilitaBtnGeoReferencia

            End If

            If _CargaDatosCliente Then


                CargaClienteEquipo()
                CargaPedido()
                CargaLlamada()
                CargaPrograma()
                CargaProgramaCliente()
                CargaResumenVMN()

                'ActivaBarraBotones(True)
                'Para deshabilitar los controles para clientes inactivos
                'If Not GLOBAL_ManejoClientesInactivos Then
                '    ActivaBarraBotones(Not (lblStatus.Text.Trim = "INACTIVO"))
                '    If lblStatus.Text.Trim = "INACTIVO" Then
                '        btnModificar.Enabled = oSeguridad.TieneAcceso("Calidad")
                '    End If
                'Else
                '    ActivaBarraBotones(True)
                'End If

                'btnCancelacion.Enabled = False
                'Se mueve de lugar, después de la asignación de valor al campo clientecargado
                LimpiaVariablesPedido()

                txtNombre.CausesValidation = False

                SeleccionCalleColonia.CausesValidation = False

                _ClienteCargado = True

                'Deshabilitar controles para clientes inactivos
                If Not GLOBAL_ManejoClientesInactivos Then
                    ActivaBarraBotones(Not (lblStatus.Text.Trim = "INACTIVO"))
                    If lblStatus.Text.Trim = "INACTIVO" Then
                        btnModificar.Enabled = oSeguridad.TieneAcceso("Calidad")
                    End If
                Else
                    ActivaBarraBotones(True)
                End If

                btnCancelacion.Enabled = False

                If Main.CONFIG_AbreNotasClienteAuto Then
                    Cursor = Cursors.WaitCursor
                    SigaMetClasses.AbrePostitCliente(_Cliente, Me)
                    Cursor = Cursors.Default
                    Me.Refresh()
                End If

            End If

        Catch ex As Exception
            _CargaDatosCliente = False
            _ClienteCargado = False
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            Cursor = Cursors.Default()
            cmd.Dispose()

        End Try
        'TODO: Parametrizar 
        'If cboRamoCliente.Text.Trim = "EDIFICIO ADMINISTRADO" Then
        If GLOBAL_AplicaAdministracionEdificios Then
            If cboRamoCliente.Text.Trim = GLOBAL_RamoClienteAdmEdificios Then
                Me.btnClientesHijos.Enabled = True
                desactivaBotonesClientesHijos()
                If Not (oSeguridad.TieneAcceso("Administración de edificios")) Then
                    '    desactivaBotonesClientesHijos()
                    'Else
                    MessageBox.Show("Solo el encargado de administración" & Chr(13) & _
                   "de edificios puede manejar clientes" & Chr(13) & "del ramo 'Edificio administrado'", _
                   Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    'desactivaBotonesAdmEdificios(oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS"))
                    btnModificar.Enabled = False
                    Me.btnClientesHijos.Enabled = False
                End If
            Else
                Me.btnClientesHijos.Enabled = False
            End If
        End If
        'AGREGADO EL 27/09/2004
        EliminarST1.Cliente = _Cliente

        'Informar de clientes en rutas inactivas
        Dim warning As New SigaMetClasses.frmWarning(_RutaCliente)
        If warning.DialogResult = DialogResult.OK Then

            btnPedido.Enabled = False

            'Else

            '    btnPedido.Enabled = True

        End If

        'Validación si es Prospecto: Deshabilitar opciones RFA 010709
        ValidaProspecto(lblStatus.Text.Trim)

        Me.Text = SeleccionCalleColonia.CalleNombre & " : " & txtNombre.Text & " -" & lblCliente.Text & "-"

        'Validación de domicilios duplicados
        If oSeguridad.TieneAcceso("ValidaDirDuplicada") Then
            Try
                Dim validacionDireccion As New SigaMetClasses.frmValidacionDireccion(_Cliente, txtNombre.Text, _TelCasa)
            Catch ex As Exception
                MessageBox.Show("Error validando la dirección del cliente")
            End Try
        End If        
    End Sub

    Private Sub CargaPedido()
        Cursor = Cursors.WaitCursor
        Dim cmd As New SqlCommand("sp_PedidoCliente", CnnSigamet)
        Dim dr As SqlDataReader
        Dim _ColorTexto, _ColorBack As System.Drawing.Color

        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        End With

        Try
            AbreConexion()
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            lvwPedido.Items.Clear()

            Do While dr.Read
                Dim oItem As New ListViewItem()
                Dim _Status As String
                _ColorBack = lvwPedido.BackColor

                If Not IsDBNull(dr("Pedido")) Then
                    oItem.Text = CType(dr("PedidoReferencia"), String).Trim
                Else
                    oItem.Text = ""
                End If

                If Not IsDBNull(dr("Tipo")) Then
                    If CType(dr("Tipo"), String).Trim = "P" Then oItem.ImageIndex = 0
                    If CType(dr("Tipo"), String).Trim = "T" Then oItem.ImageIndex = 1
                    If CType(dr("Tipo"), String).Trim = "N" Then oItem.ImageIndex = 3
                    If CType(dr("Tipo"), String).Trim = "L" Then oItem.ImageIndex = 4
                    If CType(dr("Tipo"), String).Trim = "I" Then oItem.ImageIndex = 5
                    oItem.SubItems.Add(CType(dr("Tipo"), String).Trim)
                Else
                    oItem.ImageIndex = 2
                    oItem.SubItems.Add("")
                End If

                oItem.SubItems.Add(CType(dr("FPedido"), Date).ToString)
                oItem.SubItems.Add(CType(dr("FCompromiso"), Date).ToShortDateString)

                If Not IsDBNull(dr("FSuministro")) Then
                    oItem.SubItems.Add(CType(dr("FSuministro"), Date).ToShortDateString)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Litros")) Then
                    oItem.SubItems.Add(CType(dr("Litros"), Decimal).ToString)
                Else
                    oItem.SubItems.Add("")
                End If

                'Status del Pedido
                _Status = CType(dr("Status"), String).Trim

                Select Case _Status
                    Case "ACTIVO"
                        _ColorTexto = Color.Green
                    Case "CANCELADO"
                        _ColorTexto = Color.Red
                    Case "SURTIDO"
                        _ColorTexto = Color.Black
                    Case Else
                        _ColorTexto = Color.Black
                        _ColorBack = Color.Khaki
                End Select

                oItem.SubItems.Add(_Status)

                oItem.ForeColor = _ColorTexto
                oItem.BackColor = _ColorBack

                If Not IsDBNull(dr("RutaDes")) Then
                    oItem.SubItems.Add(CType(dr("RutaDes"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Autotanque")) Then
                    oItem.SubItems.Add(CType(dr("Autotanque"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Usuario")) Then
                    oItem.SubItems.Add(CType(dr("Usuario"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("AñoPed")) Then
                    oItem.SubItems.Add(CType(dr("AñoPed"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Celula")) Then
                    oItem.SubItems.Add(CType(dr("Celula"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Pedido")) Then
                    oItem.SubItems.Add(CType(dr("Pedido"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Factura")) Then
                    oItem.SubItems.Add(CType(dr("Factura"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Observaciones")) Then
                    oItem.SubItems.Add(CType(dr("Observaciones"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                'Consulta del tipo de producto para validar la cancelación de servicios técnicos
                If Not IsDBNull(dr("Producto")) Then
                    oItem.SubItems.Add(CType(dr("Producto"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                lvwPedido.Items.Add(oItem)

                RestablecerBarraPrincipal()
            Loop

            lblListaPedido.Text = "Lista de pedidos del cliente (" & lvwPedido.Items.Count.ToString & " en total)"

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            CierraConexion()
            cmd.Dispose()
            Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub CargaLlamada()
        Cursor = Cursors.WaitCursor
        Dim dt As DataTable
        'Dim cmd As New SqlCommand("spCCConsultaLlamadasCliente", CnnSigamet)
        'Dim da As New SqlDataAdapter(cmd)
        'Dim dt As New DataTable("Llamada")

        'With cmd
        '.CommandType = CommandType.StoredProcedure
        '.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        'End With

        'Borrar la lista de llamadas 7-11-2006
        lvwLlamada.Items.Clear()
        Try
            dt = Main.LlamadasCliente(_Cliente)
            'da.Fill(dt)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    lvwLlamada.Items.Clear()
                    Dim dr As DataRow, item As ListViewItem
                    For Each dr In dt.Rows
                        item = New ListViewItem(CType(dr("FLlamada"), Date).ToString) '0
                        Try
                            item.ForeColor = System.Drawing.Color.FromName(CType(dr("Color"), String).Trim)
                        Catch
                            item.ForeColor = Color.Black
                        End Try

                        If Not IsDBNull(dr("DesMotivo")) Then
                            item.SubItems.Add(CType(dr("DesMotivo"), String).Trim) '1
                        Else
                            item.SubItems.Add("")
                        End If


                        If Not IsDBNull(dr("Pedido")) Then
                            item.SubItems.Add(CType(dr("Pedido"), String).Trim) '2
                        Else
                            item.SubItems.Add("")
                        End If

                        If Not IsDBNull(dr("Usuario")) Then
                            item.SubItems.Add(CType(dr("Usuario"), String).Trim) '3
                        Else
                            item.SubItems.Add("")
                        End If

                        If Not IsDBNull(dr("Operador")) Then
                            item.SubItems.Add(CType(dr("Operador"), String).Trim) '4
                        Else
                            item.SubItems.Add("")
                        End If

                        If Not IsDBNull(dr("Demandante")) Then
                            item.SubItems.Add(CType(dr("Demandante"), String).Trim) '5
                        Else
                            item.SubItems.Add("")
                        End If

                        If Not IsDBNull(dr("Observaciones")) Then
                            item.SubItems.Add(CType(dr("Observaciones"), String).Trim) '6
                        Else
                            item.SubItems.Add("")
                        End If

                        lvwLlamada.Items.Add(item)
                    Next

                    grdLlamada.DataSource = dt
                    grdLlamada.CaptionText = "Lista de llamadas del cliente (" & dt.Rows.Count.ToString & " en total)"
                Else
                    lvwLlamada.Items.Clear()
                    grdLlamada.DataSource = Nothing
                    grdLlamada.CaptionText = "El cliente no tiene llamadas"
                End If
            Else
                lvwLlamada.Items.Clear()

                grdLlamada.DataSource = Nothing
                grdLlamada.CaptionText = "El cliente no tiene llamadas"
            End If

            txtObservacionesLlamada.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Cursor = Cursors.Default

        End Try
    End Sub

    Public Function VerificaCelulasUsuarioIgualCelulaCliente(ByVal Celula As Byte) As Boolean
        If (GLOBAL_CelulasUsuario) Then
            Dim rowCelula As DataRow
            Dim encuentraCelula As Boolean
            Dim descripcionCelula As String
            encuentraCelula = False
            For Each rowCelula In GLOBAL_dtCelulasUsuario.Rows
                If CType(rowCelula("Celula"), Byte) = Celula Then
                    encuentraCelula = True
                    descripcionCelula = CType(rowCelula("CelulaDescripcion"), String).Trim()
                End If
            Next
            'If encuentraCelula = False Then
            '    MessageBox.Show("Este cliente pertenece a la célula " + descripcionCelula + ".", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            'End If
            Return encuentraCelula
        Else
            Return True
        End If

    End Function

    Private Sub CargaClienteEquipo()
        If Not chkPortatil.Checked Then
            Cursor = Cursors.WaitCursor
            Dim strQuery As String = _
            "SELECT Cliente, Secuencia, Equipo, TipoPropiedad, Serie FROM vwClienteYEquipo WHERE Cliente = @Cliente"
            Dim cmd As New SqlCommand(strQuery, CnnSigamet)
            cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable("ClienteEquipo")
            Try
                da.Fill(dt)
                If dt.Rows.Count > 0 Then
                    grdClienteEquipo.DataSource = dt
                    grdClienteEquipo.CaptionText = "Lista de equipos del cliente (" & dt.Rows.Count.ToString & " en total)"
                Else
                    grdClienteEquipo.DataSource = Nothing
                    grdClienteEquipo.CaptionText = "No hay equipos relacionados"
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Cursor = Cursors.Default
                cmd.Dispose()
                da.Dispose()
            End Try
        End If
    End Sub

    Private Sub CargaPrograma()
        'Cursor = Cursors.WaitCursor
        'Dim cmd As New SqlCommand("sp_ProgramaTexto", CnnSigamet)
        'Dim dr As SqlDataReader

        'With cmd
        '    .CommandType = CommandType.StoredProcedure
        '    .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        'End With

        'Try
        '    AbreConexion()

        '    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        '    If dr.Read Then
        '        lblPrograma.Text = CType(dr("Programa"), String)
        '        lblProgramaTexto.Text = CType(dr("ProgramaTexto"), String).Trim
        '        lblHorario.Text = CType(dr("HorarioTexto"), String).Trim
        '        lblDiaFestivo.Text = CType(dr("DiaFestivo"), String).Trim
        '        lblRestricciones.Text = CType(dr("Restricciones"), String).Trim
        '        lblStatusPrograma.Text = CType(dr("Status"), String).Trim
        '        lblObservacionesPrograma.Text = CType(dr("Observaciones"), String).Trim
        '    End If


        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        'Finally
        '    CierraConexion()
        '    cmd.Dispose()
        '    Cursor = Cursors.Default

        'End Try
    End Sub

    Private Sub CargaProgramaCliente()

        Dim cmd As SqlCommand
        Dim oCliente As SigaMetClasses.cCliente = Nothing
        Try
            oCliente = New SigaMetClasses.cCliente(_Cliente)
            If oCliente.Programacion Then lblProgramacion.Text = "ACTIVA" Else lblProgramacion.Text = "INACTIVA"
            lblObservacionesProgramacion.Text = oCliente.ObservacionesProgramacion
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido el siguiente error al cargar los datos del cliente: " & ex.Message, Me._Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oCliente.Dispose()

        End Try

        cmd = New SqlCommand("spCCProgramaClienteConsulta", CnnSigamet)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        End With

        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable("ProgramaCliente")

        Try

            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                grdProgramaCliente.DataSource = dt
                grdProgramaCliente.CaptionText = "Lista de programaciones del cliente"
            Else
                LimpiaProgramaCliente()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            da.Dispose()
            cmd.Dispose()

        End Try
    End Sub

    Private Sub ValidaProspecto(ByVal status As String)
        'Restablecer la barra de herramientas
        RestablecerBarraPrincipal()

        If (status.Trim.ToUpper = "PROSPECTO") Then

            'btnBarra2.Text = "Prospectos"
            'btnBarra2.Tag = "Prospectos"
            'btnBarra2.ImageIndex = 30
            'btnBarra2.ToolTipText = "Prospectos"

            btnBarra2.Enabled = False

            btnModificar.Enabled = False
            btnProgramacionOK.Enabled = False
            btnServicios.Enabled = False
            btnProgST.Enabled = False
            btnPedido.Enabled = False
            btnCancelacion.Enabled = False
            btnPedidoBitacora.Enabled = False
            btnLlamadas.Enabled = False
            btnHistorico.Enabled = False
            btnConsultaCliente.Enabled = False
        End If
    End Sub

#End Region

#Region "Carga de datos portatil"
    Private Sub CargaClientePortatil(ByVal Cliente As Integer)
        Dim _CargaDatosCliente As Boolean = False 'Indica si se cargaron correctamente los datos del cliente 

        Cursor = Cursors.WaitCursor

        lnkNoSuministrar.Visible = False

        'Control de Ventas Multinivel
        btnVentasMultinivel.Enabled = False


        Dim strQuery As String = "SELECT * FROM vwDatosClientePortatil WHERE Cliente = @Cliente"
        Dim cmd As New SqlCommand(strQuery, CnnSigamet)
        cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
        Dim dr As SqlDataReader
        ActivaBarraBotones(True)
        btnAvanzaProgramacion.Enabled = False
        btnProgramacion.Enabled = False
        btnProgramacionOK.Enabled = False
        btnCalendario.Enabled = False
        btnServicios.Enabled = False
        btnConsultaCliente.Enabled = False
        btnATMasCercano.Enabled = False
        btnHistorico.Enabled = False
        Try
            AbreConexion()

            'Restablecer la barra principal
            RestablecerBarraPrincipal()

            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read Then

                '02/03/2012 #CASALA - En el caso de que se valide que se carguen las rutas solo del usuario
                'Y cuando se busque un cliente si el cliente pertenece a una celula que no tiene asignada el usuario
                'manda la notificación
                If VerificaCelulasUsuarioIgualCelulaCliente(CType(dr("Celula"), Byte)) = False Then
                    MessageBox.Show("Este cliente pertenece a la célula: '" + CType(dr("CelulaDescripcion"), String).Trim + "'.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                'Dim dr As DataRow = dt.Rows(0)

                'Botones
                btnSeleccionaEmpresa.Enabled = False
                btnGuardar.Enabled = False

                lblCliente.Text = CType(dr("Cliente"), Integer).ToString
                _CelulaCliente = CType(dr("Celula"), Byte)
                _RutaCliente = CType(dr("Ruta"), Short)

                'Cambio de color de la célula si pertenece a otra célula
                If _CelulaCliente = GLOBAL_Celula Then
                    lblCelula.ForeColor = Color.MediumBlue
                    Me.BackColor = CONFIG_ColorFondo
                Else
                    lblCelula.ForeColor = Color.Firebrick
                    Me.BackColor = CONFIG_ColorFondoAlterno
                End If

                lblCelula.Text = CType(dr("CelulaDescripcion"), String).Trim

                txtNombre.Text = CType(dr("Nombre"), String).Trim
                txtNombre.ReadOnly = True


                _Empresa = 0
                lblRazonSocial.Text = ""
                btnConsultaEmpresa.Enabled = False
                btnSeleccionaEmpresa.Visible = False


                txtObservacionesCliente.Text = CType(dr("Observaciones"), String).Trim
                txtObservacionesCliente.ReadOnly = True

                'Validación de la ruta si el cliente es de otra célula diferente a la mía

                cboRuta.Visible = False
                lblRuta.Visible = True
                lblRuta.Text = CType(dr("RutaDescripcion"), String).Trim

                If Not IsDBNull(dr("OrigenClienteDescripcion")) Then
                    lblOrigenCliente.Text = CType(dr("OrigenClienteDescripcion"), String).Trim
                End If

                lblOrigenCliente.Visible = True
                cboOrigenCliente.Visible = False

                _StatusCalidad = ""
                lblStatus.Text = ""
                lblStatusCalidad.Text = ""
                lblStatusCalidad.ForeColor = Color.Black

                lblFAlta.Text = CType(dr("FAlta"), Date).ToString

                'Teléfonos
                txtTelCasa.Text = SigaMetClasses.FormatoTelefono(CType(dr("TelCasa"), String).Trim)
                txtTelAlterno1.Text = CType(dr("TelAlterno"), String).Trim
                txtTelAlterno2.Text = ""

                txtLada.ReadOnly = True
                txtTelCasa.ReadOnly = True
                txtTelAlterno1.ReadOnly = True
                txtTelAlterno2.ReadOnly = True

                'Clasificación
                'lblTipoCliente.Text = ""
                lblClasificacionCliente.Text = ""
                cboRamoCliente.SelectedValue = CType(dr("RamoCliente"), Short)
                cboRamoCliente.Enabled = False
                lblTipoFactura.Text = ""
                lblCartera.Text = ""

                'Consulta de quejas activas
                If Not IsDBNull(dr("QuejaActiva")) Then
                    lnkQueja.Enabled = True
                    lnkQueja.Visible = True

                    If lnkQueja.Enabled Then
                        lnkQueja.Text = CType(dr("QuejaActiva"), String)
                    End If
                Else
                    lnkQueja.Enabled = False
                    lnkQueja.Visible = False
                End If


                dr.Close()


                SeleccionCalleColonia.CargaDatosClientePortatilSoloLectura(Cliente)
                _CargaDatosCliente = True

            End If

            If _CargaDatosCliente Then

                'Revisar
                CargaClienteEquipo()
                '
                LimpiaProgramaCliente()
                CargaPedidoPortatil()
                CargaLlamadaPortatil()


                btnCancelacion.Enabled = False
                LimpiaVariablesPedido()

                txtNombre.CausesValidation = False

                SeleccionCalleColonia.CausesValidation = False

                _ClienteCargado = True

            End If

        Catch ex As Exception
            _CargaDatosCliente = False
            _ClienteCargado = False
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            Cursor = Cursors.Default()
            cmd.Dispose()

        End Try

        'Validacion de adminsitracion de edificios
        If GLOBAL_AplicaAdministracionEdificios Then
            If cboRamoCliente.Text.Trim = GLOBAL_RamoClienteAdmEdificios Then
                Me.btnClientesHijos.Enabled = True
                If (oSeguridad.TieneAcceso("Administración de edificios")) Then
                    desactivaBotonesClientesHijos()
                Else
                    MessageBox.Show("Solo el encargado de administración" & Chr(13) & _
                   "de edificios puede manejar clientes" & Chr(13) & "del ramo 'Edificio administrado'", _
                   Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    desactivaBotonesAdmEdificios(oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS"))
                    btnModificar.Enabled = False
                End If
            Else
                Me.btnClientesHijos.Enabled = False
            End If
        End If
        Me.Text = SeleccionCalleColonia.CalleNombre & " : " & txtNombre.Text & " -" & lblCliente.Text & "-"
    End Sub

    Private Sub CargaPedidoPortatil()
        Cursor = Cursors.WaitCursor
        Dim cmd As New SqlCommand("spCCPedidoClientePortatil", CnnSigamet)
        Dim dr As SqlDataReader
        Dim _ColorTexto, _ColorBack As System.Drawing.Color

        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        End With

        Try
            AbreConexion()
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            lvwPedido.Items.Clear()

            Do While dr.Read
                Dim oItem As New ListViewItem(CType(dr("PedidoPortatil"), String).Trim) '0
                Dim _Status As String
                _ColorBack = lvwPedido.BackColor
                oItem.ImageIndex = 1
                oItem.SubItems.Add(CType(dr("Tipo"), String).Trim) '1
                oItem.SubItems.Add(CType(dr("FPedido"), Date).ToString) '2
                oItem.SubItems.Add(CType(dr("FCompromiso"), Date).ToShortDateString) '3

                If Not IsDBNull(dr("FSuministro")) Then
                    oItem.SubItems.Add(CType(dr("FSuministro"), Date).ToShortDateString) '4
                Else
                    oItem.SubItems.Add("")
                End If
                oItem.SubItems.Add("-") '5

                'Status del Pedido
                _Status = CType(dr("Status"), String).Trim

                Select Case _Status
                    Case "ACTIVO"
                        _ColorTexto = Color.Green
                    Case "CANCELADO"
                        _ColorTexto = Color.Red
                    Case "SURTIDO"
                        _ColorTexto = Color.Black
                    Case Else
                        _ColorTexto = Color.Black
                        _ColorBack = Color.Khaki
                End Select

                oItem.SubItems.Add(_Status) '6

                oItem.ForeColor = _ColorTexto
                oItem.BackColor = _ColorBack

                If Not IsDBNull(dr("RutaDes")) Then
                    oItem.SubItems.Add(CType(dr("RutaDes"), String).Trim) '7
                Else
                    oItem.SubItems.Add("")
                End If
                oItem.SubItems.Add("-") '8
                If Not IsDBNull(dr("Usuario")) Then '9
                    oItem.SubItems.Add(CType(dr("Usuario"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                oItem.SubItems.Add(CType(dr("FCompromiso"), Date).Year.ToString) '10

                If Not IsDBNull(dr("Celula")) Then '11
                    oItem.SubItems.Add(CType(dr("Celula"), String).Trim)
                Else
                    oItem.SubItems.Add("")
                End If

                If Not IsDBNull(dr("PedidoPortatil")) Then '12
                    oItem.SubItems.Add(CType(dr("PedidoPortatil"), String).Trim)
                Else
                    oItem.SubItems.Add("-")
                End If

                oItem.SubItems.Add("-") '13

                If Not IsDBNull(dr("Observaciones")) Then
                    oItem.SubItems.Add(CType(dr("Observaciones"), String).Trim) '14
                    Debug.WriteLine("Escribiendo observaciones")
                Else
                    oItem.SubItems.Add("-")
                End If

                lvwPedido.Items.Add(oItem)

            Loop

            lblListaPedido.Text = "Lista de pedidos del cliente (" & lvwPedido.Items.Count.ToString & " en total)"

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            CierraConexion()
            cmd.Dispose()
            Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub CargaLlamadaPortatil()
        Cursor = Cursors.WaitCursor
        Dim dt As DataTable
        'Dim cmd As New SqlCommand("spCCConsultaLlamadasCliente", CnnSigamet)
        'Dim da As New SqlDataAdapter(cmd)
        'Dim dt As New DataTable("Llamada")

        'With cmd
        '.CommandType = CommandType.StoredProcedure
        '.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        'End With

        Try
            dt = Main.LlamadasClientePortatil(_Cliente)
            'da.Fill(dt)
            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    lvwLlamada.Items.Clear()
                    Dim dr As DataRow, item As ListViewItem
                    For Each dr In dt.Rows
                        item = New ListViewItem(CType(dr("FLlamada"), Date).ToString)
                        Try
                            item.ForeColor = System.Drawing.Color.FromName(CType(dr("Color"), String).Trim)
                        Catch
                            item.ForeColor = Color.Black
                        End Try

                        item.SubItems.Add(CType(dr("DesMotivo"), String).Trim)

                        If Not IsDBNull(dr("Pedido")) Then
                            item.SubItems.Add(CType(dr("Pedido"), String).Trim)
                        Else
                            item.SubItems.Add("")
                        End If

                        item.SubItems.Add(CType(dr("Usuario"), String).Trim)

                        If Not IsDBNull(dr("Operador")) Then
                            item.SubItems.Add(CType(dr("Operador"), String).Trim)
                        Else
                            item.SubItems.Add("")
                        End If

                        If Not IsDBNull(dr("Demandante")) Then
                            item.SubItems.Add(CType(dr("Demandante"), String).Trim)
                        Else
                            item.SubItems.Add("")
                        End If

                        If Not IsDBNull(dr("Observaciones")) Then
                            item.SubItems.Add(CType(dr("Observaciones"), String).Trim)
                        Else
                            item.SubItems.Add("")
                        End If

                        lvwLlamada.Items.Add(item)
                    Next

                    grdLlamada.DataSource = dt
                    grdLlamada.CaptionText = "Lista de llamadas del cliente (" & dt.Rows.Count.ToString & " en total)"
                Else
                    lvwLlamada.Items.Clear()
                    grdLlamada.DataSource = Nothing
                    grdLlamada.CaptionText = "El cliente no tiene llamadas"
                End If
            Else
                lvwLlamada.Items.Clear()

                grdLlamada.DataSource = Nothing
                grdLlamada.CaptionText = "El cliente no tiene llamadas"
            End If

            txtObservacionesLlamada.Text = ""

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Cursor = Cursors.Default

        End Try
    End Sub
    Private Sub PedidoPortatil()
        Dim frmPedidoPortatil As New frmPedidoPortatil(_Cliente, PermitirCambios:=GLOBAL_AplicaCambioFechaCompromiso)
        frmPedidoPortatil.ShowDialog()
        CargaPedidoPortatil()
        CargaLlamadaPortatil()
    End Sub
    Private Sub CancelaPedidoPortatil()
        If _Cliente <= 0 Then
            Exit Sub
        End If
        'If (_CelulaCliente = GLOBAL_Celula) Or Main.GLOBAL_CelulaAdmin = True Then
        Cursor = Cursors.WaitCursor
        'Dim frmMotivoCancelacion As New MotivoCancelacion()
        'frmMotivoCancelacion.Entrada(_Cliente, _AñoPed, _Celula, _Pedido, txtNombre.Text)
        'frmMotivoCancelacion.Dispose()
        'CargaCliente(_Cliente)

        Dim oCancelacionPedido As New SigaMetClasses.frmCancelacionPedido(txtNombre.Text.Trim, Me._Pedido, GLOBAL_ConString)
        If oCancelacionPedido.ShowDialog = DialogResult.OK Then
            Me.CargaClientePortatil(_Cliente)
        End If

        Cursor = Cursors.Default
        'Else
        '    MessageBox.Show("Sólo puede cancelar pedidos de clientes de su célula.", Me._Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

    End Sub

#End Region

#Region "Procedimientos de limpieza y datos iniciales"


    Private Sub DatosIniciales()
        lblStatus.Text = PRED_Status
        lblStatusCalidad.Text = PRED_StatusCalidad
        'lblTipoCliente.Text = PRED_TipoClienteDescripcion
        lblClasificacionCliente.Text = PRED_ClasificacionClienteDescripcion
        lblTipoFactura.Text = PRED_TipoFacturaDescripcion
        lblCartera.Text = PRED_CarteraDescripcion

        cboOrigenCliente.SelectedValue = PRED_OrigenCliente

        'Para cargar el origen del cliente por default cliente 18-05-2006
        If _altaClienteWeb Then
            cboOrigenCliente.SelectedValue = GLOBAL_OrigenClientePortal
            cboOrigenCliente.Enabled = False
        End If
    End Sub

    Private Sub ActivaBarraBotones(ByVal Activar As Boolean)
        btnRefrescar.Enabled = _ClienteCargado
        btnModificar.Enabled = Activar
        btnProgramacion.Enabled = Activar
        btnProgramacionOK.Enabled = Activar
        btnAvanzaProgramacion.Enabled = Activar
        'btnTanques.Enabled = Activar
        btnServicios.Enabled = Activar

        '***
        If Activar Then
            btnProgST.Enabled = oSeguridad.TieneAcceso("PROGRAMACIONST")
        Else
            btnProgST.Enabled = Activar
        End If

        btnPedido.Enabled = Activar
        btnCancelacion.Enabled = Activar

        btnLlamadas.Enabled = Activar
        'Habilitar consultas e histórico 
        'btnHistorico.Enabled = Activar
        btnHistorico.Enabled = _ClienteCargado
        'btnConsultaCliente.Enabled = Activar
        btnConsultaCliente.Enabled = _ClienteCargado
        '...
        btnNotaAgregar.Enabled = Activar
        btnNotaCerrar.Enabled = Activar
        btnNotaTablero.Enabled = Activar
        If GLOBAL_AplicaAdministracionEdificios Then
            btnClientesHijos.Enabled = Activar
        End If
        'btnAgregarEquipo.Enabled = Activar
        btnEliminarEquipo.Enabled = Activar
        If GLOBAL_AplicaATMasCercano Then
            btnATMasCercano.Enabled = Activar
        End If

        'inactivar menús para no levantar pedidos a clientes inactivos
        '***
        mnuModificar.Enabled = Activar
        mnuServicioTecnico.Enabled = Activar
        mnuPedido.Enabled = Activar
        mnuCancelacion.Enabled = Activar
        '***
    End Sub

    Private Sub desactivaBotonesClientesHijos()
        'Desactiva los botones para que los clientes hijos
        'no tengan programación, 
        '14/09/2004 JAGD
        'Dim oHijos As New SigaMetClasses.ConsultaClientesHijos(_Cliente)
        Dim ohijos As New AdmEdificios.ConsultaClientesHijos(_Cliente, GLOBAL_ConString, GLOBAL_Corporativo, GLOBAL_Sucursal)
        If Not (ohijos.verificaSiEsPadre(_Cliente)) Then
            desactivaBotonesAdmEdificios(False)
            lblCtePadreEdif.Text = "Edificio Administrado: Cliente hijo " & CrLf & "(Cte. Padre " & CStr(Val(_ClientePadreEdificio)) & ")"
            lblCtePadreEdif.Tag = _ClientePadreEdificio.ToString()
            AddHandler lblCtePadreEdif.Click, AddressOf lnkCtePadreEdifClick
        Else
            lblCtePadreEdif.Text = "Edificio Administrado: Cliente padre"
            lblCtePadreEdif.Tag = _Cliente.ToString()

            btnProgramacionOK.Enabled = oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS")
            btnPedido.Enabled = oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS")
            btnCancelacion.Enabled = oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS")
            btnAgregarEquipo.Enabled = oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS")
            btnModificarEquipo.Enabled = oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS")
            btnEliminarEquipo.Enabled = oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS")
            mnuPedido.Enabled = Not oSeguridad.TieneAcceso("PEDIDOS_EDIFICIOS")
        End If
    End Sub

    Private Sub lnkCtePadreEdifClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim ctePadre As New AsignacionMultiple.AsignacionClientePadre(SigaMetClasses.DataLayer.Conexion, CType(lblCtePadreEdif.Tag, Integer), True)
        ctePadre.ShowDialog()
    End Sub

    Private Sub desactivaBotonesAdmEdificios(ByVal LevantarPedidos As Boolean)
        btnProgramacion.Enabled = False
        btnProgramacionOK.Enabled = LevantarPedidos
        btnAvanzaProgramacion.Enabled = False
        btnPedido.Enabled = LevantarPedidos
        btnCancelacion.Enabled = LevantarPedidos
        btnClientesHijos.Enabled = False
        btnAgregarEquipo.Enabled = LevantarPedidos
        btnModificarEquipo.Enabled = LevantarPedidos
        btnEliminarEquipo.Enabled = LevantarPedidos
        btnATMasCercano.Enabled = False
        mnuPedido.Enabled = False
    End Sub

    Private Sub LimpiaTodo()
        txtNombre.CausesValidation = True

        'Se agregó el 28/09/2004
        chkPortatil.Checked = False
        chkPortatil.Enabled = True
        LimpiaClienteEquipo()
        LimpiaVariables()
        LimpiaDatosPrincipales()
        LimpiaTelefonos()
        LimpiaClasificacion()
        LimpiaPrograma()
        LimpiaProgramaCliente()
        LimpiaPedido()
        LimpiaLlamada()

        'Limpiado de VentasMultinivel
        btnVentasMultinivel.Enabled = False

        'Quejas
        lnkQueja.Enabled = False
        lnkQueja.Visible = False

        'Barra de herramientas de prospectos
        RestablecerBarraPrincipal()

        'LUSATE
        btnGeoreferenciar.Enabled = False
    End Sub

    Private Sub LimpiaPedido()
        lvwPedido.Items.Clear()
        lblListaPedido.Text = "Pedidos"
    End Sub

    Private Sub LimpiaLlamada()
        'Borrar la lista de llamadas 7-11-2006
        lvwLlamada.Items.Clear()
        grdLlamada.DataSource = Nothing
        grdLlamada.CaptionText = "Llamadas y quejas"
        txtObservacionesLlamada.Text = ""
    End Sub

    Private Sub LimpiaClienteEquipo()
        grdClienteEquipo.DataSource = Nothing
        grdClienteEquipo.CaptionText = ""
    End Sub


    Private Sub LimpiaVariables()
        _Cliente = 0
        _CelulaCliente = 0
        _RutaCliente = 0
        _Empresa = 0
    End Sub

    Private Sub LimpiaVariablesPedido()
        _AñoPed = 0
        _Celula = 0
        _Pedido = 0
        _PedidoReferencia = ""
        _Status = ""
    End Sub

    Private Sub LimpiaDatosPrincipales()
        lblCliente.Text = "<Nuevo>"
        lblCelula.Text = GLOBAL_CelulaDescripcion
        lblCelula.ForeColor = Color.MediumBlue
        txtNombre.Text = ""
        lblRazonSocial.Text = ""
        txtObservacionesCliente.Text = ""
        lblStatus.Text = ""
        lblStatusCalidad.Text = ""
        lblFAlta.Text = ""
        lblRuta.Text = ""
        lblOrigenCliente.Text = ""

        lblCtePadreEdif.Text = ""

        txtNombre.ReadOnly = False
        txtObservacionesCliente.ReadOnly = False
        cboRuta.Visible = True
        lblRuta.Visible = False
        cboOrigenCliente.Visible = True
        lblOrigenCliente.Visible = False
        cboRamoCliente.Enabled = True

        lblGiroCliente.Text = String.Empty
        lblReferencia.Text = String.Empty
    End Sub

    Private Sub LimpiaTelefonos()
        txtLada.Text = ""
        txtTelCasa.Text = ""
        txtTelAlterno1.Text = ""
        txtTelAlterno2.Text = ""
        txtEmail.Text = ""

        txtLada.ReadOnly = False
        txtTelCasa.ReadOnly = False
        txtTelAlterno1.ReadOnly = False
        txtTelAlterno2.ReadOnly = False
        txtEmail.ReadOnly = False
    End Sub

    Private Sub LimpiaClasificacion()
        'Se desactivó el día 28/09/2004 por Jorge A. Guerrero
        'lblTipoCliente.Text = ""
        lblClasificacionCliente.Text = ""
        cboRamoCliente.Text = ""
        cboRamoCliente.SelectedValue = -1
        lblTipoFactura.Text = ""
        lblCartera.Text = ""
        chkVIP.Checked = False
        chkVIP.Enabled = True
    End Sub

    Private Sub LimpiaPrograma()
        'lblPrograma.Text = ""
        'lblProgramaTexto.Text = ""
        'lblStatusPrograma.Text = ""
        'lblRestricciones.Text = ""
        'lblHorario.Text = ""
        'lblDiaFestivo.Text = ""
        'lblObservacionesPrograma.Text = ""
    End Sub

    Private Sub LimpiaProgramaCliente()
        grdProgramaCliente.DataSource = Nothing
        grdProgramaCliente.CaptionText = "El cliente no tiene programación"
        lblObservacionesProgramacion.Text = ""
        lblProgramacion.Text = ""
    End Sub

    Private Sub RestablecerBarraPrincipal()
        btnBarra2.Text = "Herramientas"
        btnBarra2.Tag = "Barra2"
        btnBarra2.ImageIndex = 26
        btnBarra2.ToolTipText = "Herramientas"
        btnBarra2.Enabled = True
    End Sub

#End Region

#Region "Validación de controles"
    Private Sub txtNombre_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNombre.Validated

        If txtNombre.Text.Trim <> "" Then
            Dim oCliente As New SigaMetClasses.cCliente()
            Dim _ClienteParecido As Integer = oCliente.Consulta(txtNombre.Text.Trim)
            If _ClienteParecido <> 0 Then
                If MessageBox.Show("Ya existe un cliente con ese nombre. ¿Desea ver los datos?", _Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                    Dim oConsultaDatosCliente As New SigaMetClasses.ModificaCliente(_ClienteParecido, GLOBAL_Usuario, SePermiteModificar:=False)
                    If oConsultaDatosCliente.ShowDialog() = DialogResult.OK Then
                        _EsClienteNuevo = False
                        _Cliente = _ClienteParecido
                        CargaCliente(_Cliente)
                    End If
                Else
                    _EsClienteNuevo = True
                    btnGuardar.Enabled = True
                    btnSeleccionaEmpresa.Enabled = True
                End If
            Else
                _EsClienteNuevo = True
                btnGuardar.Enabled = True
                btnSeleccionaEmpresa.Enabled = True
            End If
        Else
            _EsClienteNuevo = False
            btnGuardar.Enabled = False
            btnSeleccionaEmpresa.Enabled = False
        End If
    End Sub


    Private Sub SeleccionCalleColonia_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles SeleccionCalleColonia.Validated
        Dim oCliente As SigaMetClasses.cCliente
        oCliente = New SigaMetClasses.cCliente()

        Dim _ClienteParecido As Integer = oCliente.Consulta(SeleccionCalleColonia.CalleNombre, _
                                                            SeleccionCalleColonia.NumExterior, _
                                                            SeleccionCalleColonia.NumInterior, _
                                                            SeleccionCalleColonia.ColoniaNombre)
        If _ClienteParecido <> 0 Then
            If MessageBox.Show("Ya existe un cliente con esa dirección. ¿Desea ver los datos?", _Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Dim oConsultaDatosCliente As New SigaMetClasses.ModificaCliente(_ClienteParecido, GLOBAL_Usuario, SePermiteModificar:=False)
                If oConsultaDatosCliente.ShowDialog() = DialogResult.OK Then
                    _EsClienteNuevo = False
                    _Cliente = _ClienteParecido
                    CargaCliente(_Cliente)
                End If
            Else
                _EsClienteNuevo = True
                btnGuardar.Enabled = True
                btnSeleccionaEmpresa.Enabled = True
            End If
        Else
            _EsClienteNuevo = True
            btnGuardar.Enabled = True
            btnSeleccionaEmpresa.Enabled = True
        End If

    End Sub

#End Region

    Private Sub frmCallCenter_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Cursor = Cursors.WaitCursor
    End Sub

    Private Sub frmCallCenter_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        Cursor = Cursors.Default
    End Sub

    Private Sub btnSeleccionaEmpresa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionaEmpresa.Click
        Cursor = Cursors.WaitCursor
        Dim oCatEmpresa As New SigaMetClasses.CatalogoEmpresa(permiteseleccionar:=True)
        With oCatEmpresa
            .WindowState = FormWindowState.Normal
            .FormBorderStyle = FormBorderStyle.FixedDialog
            .MaximizeBox = False
            .MinimizeBox = False
            .StartPosition = FormStartPosition.CenterScreen
        End With

        If oCatEmpresa.ShowDialog() = DialogResult.OK Then
            _Empresa = oCatEmpresa.Empresa
            Dim oEmpresa As SigaMetClasses.cEmpresa = Nothing
            Try
                oEmpresa = New SigaMetClasses.cEmpresa(_Empresa)
                lblRazonSocial.Text = oEmpresa.RazonSocial
                btnConsultaEmpresa.Enabled = True
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                _Empresa = 0
                lblRazonSocial.Text = ""
                btnConsultaEmpresa.Enabled = False
            Finally
                oEmpresa.Dispose()

            End Try
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub btnConsultaEmpresa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultaEmpresa.Click
        If _Empresa <> 0 Then
            ConsultaEmpresa()
        End If
    End Sub

    Private Sub ConsultaEmpresa()
        Cursor = Cursors.WaitCursor
        Dim oEmpresa As New SigaMetClasses.ConsultaEmpresa(_Empresa)
        oEmpresa.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub ConsultaPedido()
        Cursor = Cursors.WaitCursor
        Dim oConsultaPedido As SigaMetClasses.ConsultaCargo = Nothing
        Cursor = Cursors.WaitCursor
        Try
            If String.IsNullOrEmpty(_URLGateway) Then
                oConsultaPedido = New SigaMetClasses.ConsultaCargo(_PedidoReferencia)
                oConsultaPedido.ShowDialog()
            Else
                oConsultaPedido = New SigaMetClasses.ConsultaCargo(_PedidoReferencia,
                                                                   strURLGateway:=_URLGateway,
                                                                   Modulo:=GLOBAL_Modulo,
                                                                   CadenaConexion:=GLOBAL_ConString,
                                                                   Celula:=_Celula)
                oConsultaPedido.ShowDialog()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            oConsultaPedido.Dispose()
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub lvwPedido_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwPedido.SelectedIndexChanged
        Try
            'TODO: clientes portátiles
            If chkPortatil.Checked Then
                _PedidoReferencia = CType(lvwPedido.FocusedItem.Text, String).Trim
                _Status = CType(lvwPedido.FocusedItem.SubItems(6).Text, String).Trim
                _AñoPed = CType(lvwPedido.FocusedItem.SubItems(10).Text, Short)
                _Celula = CType(lvwPedido.FocusedItem.SubItems(11).Text, Byte)
                _Pedido = CType(lvwPedido.FocusedItem.SubItems(0).Text, Integer)
                _TipoPedido = CType(lvwPedido.FocusedItem.SubItems(2).Text, String)
                'Consulta del producto para validar la cancelación de servicios técnicos
                '_Producto = CType(lvwPedido.FocusedItem.SubItems(15).Text, String)
            Else
                _PedidoReferencia = CType(lvwPedido.FocusedItem.Text, String).Trim
                _Status = CType(lvwPedido.FocusedItem.SubItems(6).Text, String).Trim
                _AñoPed = CType(lvwPedido.FocusedItem.SubItems(10).Text, Short)
                _Celula = CType(lvwPedido.FocusedItem.SubItems(11).Text, Byte)
                _Pedido = CType(lvwPedido.FocusedItem.SubItems(12).Text, Integer)
                _TipoPedido = CType(lvwPedido.FocusedItem.SubItems(2).Text, String)
                'Consulta del producto para validar la cancelación de servicios técnicos
                _Producto = CType(lvwPedido.FocusedItem.SubItems(15).Text, String)
            End If

            If _Status = "ACTIVO" Or _Status = "CANCELADO" Or _Status = "RADIADO" Then
                btnCancelacion.Enabled = True
            Else
                btnCancelacion.Enabled = False
            End If

            If _PedidoReferencia <> "" Then
                Me.btnPedidoBitacora.Enabled = True
            Else
                Me.btnPedidoBitacora.Enabled = False
            End If

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            _PedidoReferencia = ""
            btnCancelacion.Enabled = False
            btnPedidoBitacora.Enabled = False
        End Try
    End Sub

    Private Sub lvwPedido_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwPedido.DoubleClick
        If chkPortatil.Checked Then
            Exit Sub
        End If
        If _PedidoReferencia <> "" Then
            ConsultaPedido()
        Else
            CambioObservacionesPedidoProgramado()
        End If
    End Sub

    Private Sub CambioObservacionesPedidoProgramado()
        Cursor = Cursors.WaitCursor
        Dim oProg As Programacion.frmObservaciones
        oProg = New Programacion.frmObservaciones(_Cliente)
        If oProg.ShowDialog() = DialogResult.OK Then
            Me.CargaPedido()
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub grdLlamada_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLlamada.CurrentCellChanged
        grdLlamada.Select(grdLlamada.CurrentRowIndex)

        If Not IsDBNull(grdLlamada.Item(grdLlamada.CurrentRowIndex, 6)) Then
            txtObservacionesLlamada.Text = CType(grdLlamada.Item(grdLlamada.CurrentRowIndex, 6), String).Trim
        Else
            txtObservacionesLlamada.Text = ""
        End If

    End Sub

#Region "Menús"
    Private Sub mnuCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCerrar.Click
        Me.Close()
    End Sub

    Private Sub mnuBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBuscar.Click
        BuscarCliente()
    End Sub

    Private Sub mnuRefrescar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRefrescar.Click
        Refrescar()
    End Sub

    Private Sub mnuNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNuevo.Click
        Nuevo()
    End Sub

    Private Sub mnuGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGuardar.Click
        Guardar()
    End Sub

    Private Sub mnuModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModificar.Click
        Modificar()
    End Sub

    Private Sub mnuServicioTecnico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServicioTecnico.Click
        Servicios()
    End Sub

    Private Sub mnuPedido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPedido.Click
        If _Cliente > 0 Then
            If chkPortatil.Checked Then
                PedidoPortatil()
            Else
                Pedido()
            End If
        End If
    End Sub

    Private Sub mnuCancelacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCancelacion.Click
        CancelaPedido()

    End Sub

#End Region

#Region "Configuración"
    Private Sub mnuConfiguracion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguracion.Click
        Configuracion()
    End Sub


    Private Sub Configuracion()
        Cursor = Cursors.WaitCursor
        Dim oConfiguracion As New frmCallCenterConfig()
        If oConfiguracion.ShowDialog() = DialogResult.OK Then
            Main.CargaConfiguracion()
            If Not _ClienteCargado Then
                Me.BackColor = CONFIG_ColorFondo
            Else
                If GLOBAL_Celula = _CelulaCliente Then
                    Me.BackColor = CONFIG_ColorFondo
                Else
                    Me.BackColor = CONFIG_ColorFondoAlterno
                End If
            End If

            ToolBarSize(CONFIG_BotonesGrandes)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub ToolBarSize(ByVal BotonesGrandes As Boolean)
        If BotonesGrandes Then
            tbBarra.ButtonSize = New Size(74, 36)
            tbBarra.Width = 73
        Else
            tbBarra.ButtonSize = New Size(20, 20)
            tbBarra.Width = 22
            tbBarra.Height = 22
        End If
    End Sub

#End Region

    Private Sub btnCalendario_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalendario.Click
        If _Cliente <> 0 Then
            Cursor = Cursors.WaitCursor
            Dim oCalendario As New frmCalendarioCliente(_Cliente)
            oCalendario.ShowDialog()
            Cursor = Cursors.Default
        End If
    End Sub

#Region "garbage"
    'Private Sub btnCalculaSiguientePedido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalculaSiguientePedido.Click
    '    Cursor = Cursors.WaitCursor
    '    Dim _SiguientePedido As Date
    '    Dim cmd As New SqlCommand("SELECT dbo.CalculaFechaProximoPedidoProgramado(@Cliente,@Año,dbo.CalculaFechaOrigenProgramaCliente(@Cliente),1)", Main.CnnSigamet)
    '    With cmd
    '        .CommandType = CommandType.Text
    '        .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
    '        .Parameters.Add("@Año", SqlDbType.SmallInt).Value = Now.Year
    '    End With

    '    Main.AbreConexion()
    '    Try
    '        _SiguientePedido = CType(cmd.ExecuteScalar(), Date)
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

    '    End Try

    '    Main.CierraConexion()

    '    MessageBox.Show("El siguiente pedido será el " & _SiguientePedido.ToLongDateString, Me._Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '    Cursor = Cursors.Default

    'End Sub
#End Region

    Private Sub lvwLlamada_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwLlamada.SelectedIndexChanged
        txtObservacionesLlamada.Text = lvwLlamada.FocusedItem.SubItems(6).Text.Trim
    End Sub

    Private Sub frmCallCenter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Configuración inicial
        Dim Portatil As Boolean
        Me.BackColor = CONFIG_ColorFondo

        Me.tbBarra2.Visible = False
        ToolBarSize(CONFIG_BotonesGrandes)

        '***************
        'Carga de datos
        '***************

        'SeleccionCalleColonia
        SeleccionCalleColonia.CargaDatos()

        'ComboOrigenCliente
        cboOrigenCliente.CargaDatos()

        'Carga las rutas según su célula
        'If Not GLOBAL_CelulaAdmin Then
        isCargando = 0
        If GLOBAL_ManejarClientesPortatil Then
            'cboRuta.CargaDatos(Celula:=GLOBAL_Celula)
            'filtro por ruta al incio de la carga de datos
            'cboRuta.CargaDatos(Celula:=GLOBAL_Celula, aCTIVARFiltrO:=GLOBAL_ManejarClientesPortatil, _
            '    mOSTRARPORTATIL:=chkPortatil.Checked)
            If GLOBAL_CelulasUsuario Then
                cboRuta.CargaDatos(ActivarFiltro:=GLOBAL_ManejarClientesPortatil, _
                                   MostrarPortatil:=chkPortatil.Checked, Usuario:=GLOBAL_Usuario)
            Else
                cboRuta.CargaDatos(ActivarFiltro:=GLOBAL_ManejarClientesPortatil, _
                                   MostrarPortatil:=chkPortatil.Checked)
            End If

        Else            
            If GLOBAL_CelulasUsuario Then
                cboRuta.CargaDatos(Usuario:=GLOBAL_Usuario)
            Else
                cboRuta.CargaDatos()
            End If            
        End If
        isCargando = 1

        'ComboRamoCliente
        cboRamoCliente.CargaDatos()

        'Evita que no se puedan dar de alta datos, si el usuario no pertenece a una célula comercial o a una célula administradora
        btnGuardar.Visible = GLOBAL_CelulaComercial Or GLOBAL_CelulaAdmin

        Portatil = chkPortatil.Checked
        Nuevo()
        chkPortatil.Checked = Portatil
        'Indica si se pre-carga un cliente.
        'Este flag es usado por la Bitácora del usuario
        '10 de agosto del 2004
        If _PreCarga Then
            If _ClientePreCarga <> 0 Then
                chkPortatil.Enabled = False
                _Cliente = _ClientePreCarga
                If chkPortatil.Checked Then
                    Me.CargaClientePortatil(_Cliente)
                Else
                    Me.CargaCliente(_Cliente)
                End If
            End If
        Else
            If _nombre.Trim.Length > 0 Then txtNombre.Text = _nombre.Trim
            If _direccion.Trim.Length > 0 Then lblCtePadreEdif.Text = _direccion.Trim
            If _lada.Trim.Length > 0 Then txtLada.Text = _lada.Trim
            If _telefono.Trim.Length > 0 Then txtTelCasa.Text = _telefono.Trim
            If _correoElectronico.Trim.Length > 0 Then txtEmail.Text = _correoElectronico.Trim
        End If

    End Sub

#Region "Notas"
    Private Sub btnNotaAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotaAgregar.Click
        If _Cliente > 0 Then
            AgregarNota()
        End If
    End Sub

    Private Sub btnNotaCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotaCerrar.Click
        If _Cliente > 0 Then
            CerrarNotas()
        End If
    End Sub

    Private Sub btnNotaTablero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotaTablero.Click
        If _Cliente > 0 Then
            Notas()
        End If
    End Sub

    Private Sub AgregarNota()
        Dim oPostit As New SigaMetClasses.Postit(SigaMetClasses.Postit.enumTipoPostit.Cliente, _
                                                Main.GLOBAL_Usuario, _
                                                Cliente:=_Cliente, _
                                                Contenedor:=Me)
        oPostit.Show()
    End Sub

    Private Sub CerrarNotas()
        Cursor = Cursors.WaitCursor
        Dim p As SigaMetClasses.Postit = Nothing
        For Each p In Me.OwnedForms
            p.Close()
        Next

        If Not IsNothing(p) Then
            p.Dispose()
        End If

        Cursor = Cursors.Default
    End Sub

    Private Sub Notas()
        Cursor = Cursors.WaitCursor
        Dim oTablero As New SigaMetClasses.PostitLista(SigaMetClasses.Postit.enumTipoPostit.Cliente, GLOBAL_Usuario, True, True, Cliente:=_Cliente)
        oTablero.ShowDialog()
        Cursor = Cursors.Default
    End Sub
#End Region

#Region "Funcinalidad activada por parámetro (AT Más cercano, Clientes Hijos ADMEdif, Avance de programaciones)"

    Private Sub ATMasCercano()
        'Muestra la consulta de AT mas cercano por GPS
        Dim oATMasCercano As New AutotanqueMasCercano.frmAutotanqueMasCercano(_Cliente)
        oATMasCercano.ShowDialog()
        oATMasCercano.Dispose()
    End Sub

    Private Sub activaATMasCercano(ByVal Aplicar As Boolean)
        If Not (Aplicar) Then
            tbBarra2.Buttons.Remove(btnATMasCercano)
        End If
    End Sub

    Private Sub activaClientesHijos(ByVal Aplicar As Boolean)
        If Not (Aplicar) Then
            tbBarra.Buttons.Remove(btnClientesHijos)
        End If
    End Sub

    'Hablita el acceso a avance de programaciones 15/01/2004
    Private Sub activaAvanzarProgramacion(ByVal Aplicar As Boolean)
        If Not (Aplicar) Then
            tbBarra2.Buttons.Remove(btnAvanzaProgramacion)
        End If
    End Sub

    'Hablita el acceso a avance de programaciones 15/01/2004
    Private Sub activaCambioZona(ByVal Aplicar As Boolean)
        If Not (Aplicar) Then
            tbBarra2.Buttons.Remove(btnCambioZona)
        End If
    End Sub

#End Region

#Region "Equipo del cliente"
    'Private Sub btnAgregarEquipo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    agregarEquipo()
    'End Sub

    Private Sub agregarEquipo()
        If Not (_Cliente = 0) Then


            '20150722 CASALA
            'Dim frmEquipo As New SigametST.frmComodato(_Cliente, 1, GLOBAL_Usuario, 0) ', 0, 0, Nothing)
            'frmEquipo.MinimizeBox = False
            'frmEquipo.MaximizeBox = False
            'frmEquipo.ShowDialog()
            'CargaClienteEquipo()


            Dim frmEquipo As New LiquidacionSTN.frmEquipoST(_Cliente, GLOBAL_Usuario, True)
            frmEquipo.MinimizeBox = False
            frmEquipo.MaximizeBox = False
            frmEquipo.ShowDialog()
            CargaClienteEquipo()


        End If
    End Sub



    Private Sub modificarEquipo()
        If Not (_Cliente = 0) Then
            Dim frmEquipo As New LiquidacionSTN.frmEquipoST(_Cliente, GLOBAL_Usuario, False)
            frmEquipo.MinimizeBox = False
            frmEquipo.MaximizeBox = False
            frmEquipo.ShowDialog()
            CargaClienteEquipo()
        End If
    End Sub

    Private Sub btnEliminarEquipo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminarEquipo.Click
        eliminarEquipo()
    End Sub

    Private Sub eliminarEquipo()
        If Not (_Cliente = 0) AndAlso grdClienteEquipo.VisibleRowCount > 0 Then
            If Not (grdClienteEquipo.Item(grdClienteEquipo.CurrentRowIndex, 0) Is DBNull.Value) Then
                If MessageBox.Show("¿Desea eliminar el equipo seleccionado?", _
                    Me._Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    If Not (CType(grdClienteEquipo.Item(grdClienteEquipo.CurrentRowIndex, 2), String) = "COMODATO") Then
                        EliminarST1.Secuencia = CType(grdClienteEquipo.Item(grdClienteEquipo.CurrentRowIndex, 0), Integer)
                        EliminarST1.EliminarEquipoST()
                        CargaClienteEquipo()
                    Else
                        MessageBox.Show("No puede eliminar equipos en comodato" & CrLf & "Contacte a la célula de servicios técnicos", _
                            "Equipo del cliente", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                End If
            End If
        End If
    End Sub
#End Region

#Region "Display info pedido portatil"
    'Private Sub lvwPedido_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwPedido.MouseHover
    '    If lvwPedido.SelectedItems.Count > 0 AndAlso chkPortatil.Checked Then
    '        Dim Location As Point
    '        InfoPedidoPortatil = New InfoPedidoPortatil(_Cliente, CInt(lvwPedido.SelectedItems(0).SubItems(0).Text))
    '        InfoPedidoPortatil.Location = Cursor.Position
    '        InfoPedidoPortatil.Show()
    '    End If
    'End Sub

    Private Sub lvwPedido_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwPedido.MouseDown
        If e.Button = MouseButtons.Right Then
            If lvwPedido.SelectedItems.Count > 0 AndAlso chkPortatil.Checked Then
                InfoPedidoPortatil = New InfoPedidoPortatil(_Cliente, CInt(lvwPedido.SelectedItems(0).SubItems(0).Text))
                InfoPedidoPortatil.Location = Cursor.Position
                InfoPedidoPortatil.Show()
            End If
        End If
    End Sub

    Private Sub lvwPedido_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwPedido.MouseMove
        If Not InfoPedidoPortatil Is Nothing Then
            InfoPedidoPortatil.Dispose()
        End If
    End Sub

#End Region

#Region "Inactivación de funciones para estacionario en portátil"
    Private Sub chkPortatil_CheckStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPortatil.CheckStateChanged
        txtEmail.Enabled = chkPortatil.CheckState = CheckState.Unchecked
        chkVIP.Enabled = chkPortatil.CheckState = CheckState.Unchecked
        btnAgregarEquipo.Visible = chkPortatil.CheckState = CheckState.Unchecked
        btnModificarEquipo.Visible = chkPortatil.CheckState = CheckState.Unchecked
        btnEliminarEquipo.Visible = chkPortatil.CheckState = CheckState.Unchecked
    End Sub
#End Region

#Region "Filtro de rutas de portátil"
    Private Sub chkPortatil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
    Handles chkPortatil.CheckedChanged
        If GLOBAL_ManejarClientesPortatil Then
            'cboRuta.CargaDatos(Celula:=GLOBAL_Celula, aCTIVARFiltrO:=GLOBAL_ManejarClientesPortatil, _
            'mOSTRARPORTATIL:=chkPortatil.Checked)
            If GLOBAL_CelulasUsuario Then
                cboRuta.CargaDatos(ActivarFiltro:=GLOBAL_ManejarClientesPortatil, _
                                   MostrarPortatil:=chkPortatil.Checked, Usuario:=GLOBAL_Usuario)
            Else
                cboRuta.CargaDatos(ActivarFiltro:=GLOBAL_ManejarClientesPortatil, _
                                   MostrarPortatil:=chkPortatil.Checked)
            End If
        End If
        btnVentasMultinivel.Visible = GLOBAL_VentasMultinivel AndAlso Not chkPortatil.Checked
    End Sub
#End Region

#Region "Cambio de zona económica"
    Private Sub cambioZonaEconomica()

        If _Cliente <> 0 Then
            Dim cambioZona As New ClienteZonaEconomica.frmClienteZonaEconomica(_Cliente, _
                GLOBAL_Usuario, GLOBAL_Password, CnnSigamet)
            If oSeguridad.TieneAcceso("CambioZonaEconomica") Then
                cambioZona.StartPosition = FormStartPosition.CenterScreen
                cambioZona.ShowDialog()
            End If
        End If

    End Sub
#End Region

#Region "Captura de tarjetas"
    Private Sub CapturaTarjeta()
        If _Cliente > 0 Then
            Dim frmCapTarjetaCredito As New SigaMetClasses.frmConTarjetaCredito(_Cliente, GLOBAL_Usuario, _URLGateway, 1, GLOBAL_ConString)
            frmCapTarjetaCredito.ShowDialog()
        End If
    End Sub
#End Region

#Region "VentasMultinivel"
    Private Sub MustraClientesRecomendados()
        Dim frmClientesRecomendados As New VentasMultinivel.UILayer.frmClientesRecomendados(CInt(lblCliente.Text))
        frmClientesRecomendados.ShowDialog()
    End Sub
    Private Sub CargaResumenVMN()
        If GLOBAL_VentasMultinivel Then
            Dim cmd As New SqlCommand("spVMNResumenCliente", CnnSigamet)
            Dim rdr As SqlDataReader
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = CInt(lblCliente.Text)
            Try
                AbreConexion()
                rdr = cmd.ExecuteReader(CommandBehavior.SingleRow)
                If rdr.Read() AndAlso GLOBAL_VentasMultinivel Then
                    lblSaldo.Text = "Saldo:  $" & rdr("Saldo").ToString()
                    lblHijos.Text = "Hijos:  " & rdr("Hijos").ToString()
                    lblClienteOrigen.Text = "Cliente origen:  " & rdr("ClienteOrigen").ToString()
                Else
                    lblSaldo.Text = "Saldo:  $0.0"
                    lblHijos.Text = "Hijos:  0"
                    lblClienteOrigen.Text = "Cliente origen:  0"
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
            End Try
        End If
    End Sub

    Private Sub ResumenVMVVisible()
        If GLOBAL_VentasMultinivel Then
            grpVentasMultinivel.Visible = True
        Else
            grpVentasMultinivel.Visible = False
        End If

    End Sub

#End Region

#Region "Captura de promotores"

    Private _promotor As Integer

    Private Sub cboOrigenCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        AsignacionPromotor()
    End Sub

    Private Sub AsignacionPromotor()
        If GLOBAL_GruposPromocionales Then
            Dim promotor As AsignacionPromotor.AsignacionPromotor
            Dim origenCliente As New AsignacionPromotor.ClienteOrigenPromocion(CnnSigamet, CType(cboOrigenCliente.SelectedValue, Integer))
            _promotor = 0
            lblPromotor.Text = String.Empty
            cboOrigenCliente.Width = cboRuta.Width
            Label1.Text = String.Empty
            If origenCliente.PromocionValida Then
                If Not origenCliente.Vigente Then
                    MessageBox.Show(origenCliente.MensajePromocion & Chr(13) & _
                                    "Esta promoción ya no está vigente", origenCliente.MensajePromocion, _
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                promotor = New AsignacionPromotor.AsignacionPromotor(CnnSigamet, CType(cboOrigenCliente.SelectedValue, Integer), _
                    cboOrigenCliente.Text)
                promotor.StartPosition = FormStartPosition.CenterScreen
                If promotor.ShowDialog() = DialogResult.OK Then
                    _promotor = promotor.Promotor
                    cboOrigenCliente.Width = cboOrigenCliente.Width - lblPromotor.Width
                    lblPromotor.Text = promotor.Nombre
                Else
                    cboOrigenCliente.Width = cboRuta.Width
                    cboOrigenCliente.SelectedIndex = 0
                    cboOrigenCliente.Focus()
                End If
                promotor.Dispose()
            End If
        End If
    End Sub

#End Region

#Region "Programación de servicios técnicos"

    Private Sub ProgramacionServiciosTecnicos()
        If oSeguridad.TieneAcceso("ProgramacionST") Then
            Dim frmServicios As New ProgramacionesST.frmServicios(_Cliente, GLOBAL_Usuario, CnnSigamet)
            frmServicios.ShowDialog()
        Else
            MessageBox.Show("No tiene privilegios para esta operación", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
#End Region

#Region "Solicitud de autorización de crédito"

    Private Sub SolicitudCredito()
        If _Cliente <> 0 Then
            Dim frmAutorizacion As New AutorizacionCredito.SolicitudCredito(_Cliente, GLOBAL_Usuario, CnnSigamet)
            frmAutorizacion.ShowDialog()
        End If
    End Sub

#End Region

    Private Sub CapturaImagenes()
        If _Cliente <> 0 Then
            Dim imgViewer As New ImgCallCenter.frmImgCCMain(CnnSigamet, _Cliente, GLOBAL_Usuario)
            imgViewer.StartPosition = FormStartPosition.CenterScreen
            imgViewer.ShowDialog()
        End If
    End Sub

#Region "Barra auxiliar de botones"

    Private Sub hideBar()
        tbBarra2.Width = (tbBarra2.Buttons.Count * tbBarra2.ButtonSize.Width)
        tbBarra2.Height = tbBarra2.ButtonSize.Height
        tbBarra2.Left = tbBarra.Left - tbBarra2.Width
        tbBarra2.Visible = Not (tbBarra2.Visible)
    End Sub

    Private Sub tbBarra2_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbBarra2.ButtonClick
        Select Case e.Button.Tag.ToString()
            Case "AvanzaProgramacion"
                If _Cliente > 0 Then
                    AvanzaProgramacion()
                End If
            Case "AgregarEquipo"
                agregarEquipo()
            Case "ModificarEquipo"
                modificarEquipo()
            Case "AT mas cercano"
                ATMasCercano()
            Case "CambioZona"
                cambioZonaEconomica()
            Case "VentasMultinivel"
                Me.MustraClientesRecomendados()
            Case "Tarjeta"
                CapturaTarjeta()
            Case "Imagenes"
                CapturaImagenes()
            Case "ClientesHijos"
                ClientesHijos()
            Case "SolicitudCredito"
                SolicitudCredito()
            Case "Prospectos"
                Prospectos()
            Case "Fugas"
                RegistroFugas()
        End Select
    End Sub

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

#Region "Clientes y pedidos vía WEB"
    'Nombre del cliente en la página WEB
    Private _nombre As String = String.Empty
    Private _direccion As String = String.Empty
    Private _telefono As String = String.Empty

    Private _lada As String = String.Empty

    Private _correoElectronico As String = String.Empty

    Private _fechaCompromiso As Date
    Private _observaciones As String = String.Empty

    Private _altaClienteWeb As Boolean = False

    Public Sub New(ByVal Nombre As String, _
    ByVal Direccion As String, _
    ByVal Lada As String, _
    ByVal Telefono As String, _
    ByVal CorreoElectronico As String, _
    ByVal FechaCompromiso As DateTime, _
    ByVal Observaciones As String)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        chkPortatil.Visible = GLOBAL_ManejarClientesPortatil

        lblReferencia.Visible = Not GLOBAL_ManejarClientesPortatil
        'TODO: Para el cambio de zona económica de cliente '17-06-2005'
        If Not GLOBAL_ManejarClientesPortatil Then
            activaCambioZona(oSeguridad.TieneAcceso("CambioZonaEconomica"))
        End If
        'se agregó para desctivar el botón at más cercano 29/09/2004
        activaATMasCercano(GLOBAL_AplicaATMasCercano)
        activaClientesHijos(GLOBAL_AplicaAdministracionEdificios)
        activaAvanzarProgramacion(GLOBAL_AvanzaProgramacion)
        SeleccionCalleColonia.AltaCalleColonia = GLOBAL_AltaCalleColonia

        btnTarjeta.Visible = oSeguridad.TieneAcceso("TarjetaCredito")

        'Captura de contrato firmado para sigamet corporativo
        'chkContrato.Visible = GLOBAL_CapturaContratoFirmado
        'lblContratoText.Visible = GLOBAL_CapturaContratoFirmado

        'Captura de factor de comisión por cliente
        lblComision.Visible = GLOBAL_CapturaFactorComision
        lblComisionText.Visible = GLOBAL_CapturaFactorComision

        'Control de Ventas Multinivel
        btnVentasMultinivel.Visible = GLOBAL_VentasMultinivel

        'Alta de clientes captados en la página WEB
        _nombre = Nombre
        _direccion = Direccion
        _lada = Lada
        _telefono = Telefono
        _correoElectronico = CorreoElectronico

        _observaciones = Observaciones

        'cboOrigenCliente.SelectedValue = GLOBAL_OrigenClientePortal
        'cboOrigenCliente.Enabled = False

        _altaClienteWeb = True
    End Sub

    Public ReadOnly Property Cliente() As Integer
        Get
            Return _Cliente
        End Get
    End Property

    Public ReadOnly Property Celula() As Byte
        Get
            Return _CelulaCliente
        End Get
    End Property

    Public ReadOnly Property Ruta() As Short
        Get
            Return _RutaCliente
        End Get
    End Property


#End Region

#Region "Quejas"
    Private Sub lnkQueja_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkQueja.LinkClicked
        QuejasLibrary.Public.[Global].ConfiguraLibrary(SigametSeguridad.Seguridad.Conexion.ConnectionString, SigametSeguridad.Seguridad.Conexion, GLOBAL_Usuario, 1)
        Dim SeguimientoQueja As QuejasLibrary.frmSeguimientoQueja = New QuejasLibrary.frmSeguimientoQueja(_Cliente)
        SeguimientoQueja.WindowState = FormWindowState.Maximized
        SeguimientoQueja.ShowDialog()
        Refrescar()
    End Sub

    Private Sub altaQueja()
        QuejasLibrary.Public.[Global].ConfiguraLibrary(SigametSeguridad.Seguridad.Conexion.ConnectionString, SigametSeguridad.Seguridad.Conexion, GLOBAL_Usuario, 1)
        Dim frmQueja As QuejasLibrary.frmAltaQueja = New QuejasLibrary.frmAltaQueja(False, _Cliente, chkPortatil.Checked)
        Try
            If frmQueja.ShowDialog = DialogResult.OK Then
                Refrescar()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub grpVentasMultinivel_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpVentasMultinivel.Enter

    End Sub

    Private Sub btnContactos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContactos.Click
        If _Cliente <> 0 Then
            Dim consultaContacto As CRMContactos.ListaContactos = New CRMContactos.ListaContactos(CnnSigamet, _Cliente, GLOBAL_ConString, _URLGateway)
            consultaContacto.Width = Me.Width - 100
            consultaContacto.Height = Me.Height - 100
            consultaContacto.WindowState = FormWindowState.Normal
            consultaContacto.StartPosition = FormStartPosition.CenterScreen
            consultaContacto.ShowDialog()
        End If
    End Sub

#Region "Detalles de contrato padre"
    Private Sub lnkDetalleClientePadre(ByVal sender As Object, ByVal e As EventArgs)
        Dim ctePadre As New AsignacionMultiple.AsignacionClientePadre(SigaMetClasses.DataLayer.Conexion, CType(lblCtePadreEdif.Tag, Integer))
        ctePadre.ShowDialog()
    End Sub
#End Region



#Region "Prospectos"
    Private Sub Prospectos()
        If _Cliente > 0 Then
            Dim _frmProspectos As New ProspectosMetro.frmProspectos(_Cliente)
            _frmProspectos.StartPosition = FormStartPosition.CenterScreen
            _frmProspectos.ShowDialog()
        End If
    End Sub
#End Region

    Private Sub pnlCallCenter_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlCallCenter.Paint
        Try
            lblNombreEmpresa.CargarNombreEmpresa()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtEmail_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmail.Leave
        If txtEmail.Text.Trim().Length > 0 Then
            If Not RegularValidations.RegularValidations.Instance.ValidarCorreoElectronico(txtEmail.Text) Then
                MessageBox.Show(RegularValidations.RegularValidations.Instance.MensajeValidacion, _
                "Formato de email no válido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtEmail.Focus()
            End If
        End If
    End Sub

    'Requerimiento:   10
    'Folio:           2
    'Proyecto:        1001
    'Descripción:     SALDO MÍNIMO PARA REPORTE DE CLIENTES NUEVOS – PERSONAS FÍSICAS    
    '                 No se reportan clientes nuevos de personas físicas con saldo menor a 5000, estos registros se    
    '                 eliminan de la base de datos al concluir el proceso de registro de la información.    
    'Autor:           Jorge A. Guerrero    
    'Fecha:           13/08/10
    Private Sub txtTelAlterno2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTelAlterno2.Leave
        If txtTelAlterno2.Text.Trim().Length > 0 Then
            If Not RegularValidations.RegularValidations.Instance.ValidarNumeroCelular(txtTelAlterno2.Text) Then
                MessageBox.Show(RegularValidations.RegularValidations.Instance.MensajeValidacion, _
                "Formato de número celular no válido", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtTelAlterno2.Focus()
            End If
        End If
    End Sub

    Private Function ConsultaRAFPorRuta(ByVal RutaRAF As Integer) As Boolean
        Cursor = Cursors.WaitCursor
        Dim ExisteRAF As Boolean = False
        Dim cmdRAF As New SqlCommand("spCCConsultaRAFPorRuta", CnnSigamet)
        Dim drRAF As SqlDataReader


        cmdRAF.CommandType = CommandType.StoredProcedure
        cmdRAF.Parameters.Add("@Ruta", SqlDbType.Int).Value = RutaRAF
        Try
            AbreConexion()
            drRAF = cmdRAF.ExecuteReader(CommandBehavior.CloseConnection)
            If drRAF.HasRows Then
                ExisteRAF = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            cmdRAF.Dispose()
            Cursor = Cursors.Default
        End Try
        Return ExisteRAF
    End Function

    Private Sub cboRuta_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboRuta.SelectedIndexChanged
        'LUSATE Alerta RAF
        If cboRuta.SelectedIndex <> -1 And isCargando = 1 Then
            lnkAlertaRAF.Visible = ConsultaRAFPorRuta(cboRuta.SelectedValue)
        End If
    End Sub

    Private Sub RegistroFugas()
        If _Cliente <> 0 And chkPortatil.Checked Then
            Try
                Dim frm As Form = Nothing
                Me.Cursor = Cursors.WaitCursor
                frm = New ControlFugasPortatilClasses.frmFugaPortatil(_Cliente)
                Me.Cursor = Cursors.Default
                'frm.WindowState = FormWindowState.Normal
                'frm.MdiParent = Me
                frm.Show()
            Catch ex As Exception
                MessageBox.Show("Ha ocurrido un error:" & vbCrLf & ex.Message & vbCrLf & _
                    ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnGeoreferenciar_Click(sender As System.Object, e As System.EventArgs) Handles btnGeoreferenciar.Click
        Cursor = Cursors.WaitCursor

        Dim CadenaConexionGPS As String = _
                        CType(SigametSeguridad.Seguridad.Parametros(6, CType(GLOBAL_Corporativo, Byte), _
                        CType(GLOBAL_Sucursal, Byte)).ValorParametro("CadenaConexionGPS"), String)


        Dim frmGeoReferencia As New MapaSoft.Runtime.Formularios.frmGeoReferenciaManual(GLOBAL_ConString, CadenaConexionGPS, _Cliente)
       
        If frmGeoReferencia.ShowDialog = DialogResult.OK Then
            CargaCliente(_Cliente)
        End If

        Cursor = Cursors.Default

    End Sub
End Class