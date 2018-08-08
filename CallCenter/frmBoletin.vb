Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System
Imports System.Linq
Imports System.Text

Public Class frmBoletin
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private _AñoPed As Short
    Private _Celula As Byte
    Private _Pedido As Integer
    Private _Ruta As Short
    Private _Rutaboletin As Short = 0
    Private _PedidoReferencia As String = ""
    Private _Nombre As String = ""
    Private _DatosCargados As Boolean
    Private _Titulo As String = "Boletines"
    Private _Columna As Integer
    Private _LlevaLaNota As String
    Private DtCel As New DataTable()
    Private _FCompromiso As Date    
    Private _Autotanque As Integer = 0
    Private _FAlta As Date
    Public _URLGateway As String
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnSep2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnRefrescar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnSep1 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCierreRuta As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCarga As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnConfirmaSuministro As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnEnviarMensaje As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnConsultaCliente As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCambioCompromiso As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnConfirmacionCliente As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLlamadaOperador As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnActualizaStatusMG As System.Windows.Forms.ToolBarButton
    Friend WithEvents DsTipoFactura1 As Sigamet.dsTipoFactura
    Friend WithEvents btnReasignar As System.Windows.Forms.ToolBarButton
    Friend WithEvents tbBarra As System.Windows.Forms.ToolBar





#Region " Windows Form Designer generated code "

    Public Sub New(Optional ByVal URLGateway As String = Nothing)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        _URLGateway = URLGateway

        Dim objPedidoGateway As New RTGMGateway.RTGMPedidoGateway
        Dim _CelulaCarga As Byte
        Dim FechaDtp As Date
        Dim SolicitudPedidoGateway As RTGMGateway.SolicitudPedidoGateway

        FechaDtp = dtpFecha.Value.Date

        If CType(cboUsuarioCelula.SelectedValue, Byte) <> 0 Then
            _CelulaCarga = CType(cboUsuarioCelula.SelectedValue, Byte)
        Else
            _CelulaCarga = Main.GLOBAL_Celula
        End If

        Dim ListaPedidos As List(Of RTGMCore.Pedido)
        objPedidoGateway.URLServicio = _URLGateway
        ListaPedidos = objPedidoGateway.buscarPedidos(SolicitudPedidoGateway)

        'Add any initialization after the InitializeComponent() call
        chkPortatil.Visible = GLOBAL_ManejarClientesPortatil
        habilitaCambioCompromiso()
        habilitaReasignacionPedidos()
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
    Friend WithEvents lvwBoletin As System.Windows.Forms.ListView
    Friend WithEvents colRuta As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRutaBoletin As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRutaBoletinDescripcion As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRAF As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPedidoReferencia As System.Windows.Forms.ColumnHeader
    Friend WithEvents colHora As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCliente As System.Windows.Forms.ColumnHeader
    Friend WithEvents colNombre As System.Windows.Forms.ColumnHeader
    Friend WithEvents colAñoPed As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCelula As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPedido As System.Windows.Forms.ColumnHeader
    Friend WithEvents colPrioridad As System.Windows.Forms.ColumnHeader
    Friend WithEvents imgLista As System.Windows.Forms.ImageList
    Friend WithEvents lblBoletines As System.Windows.Forms.Label
    Friend WithEvents colDireccion As System.Windows.Forms.ColumnHeader
    Friend WithEvents colUsuario As System.Windows.Forms.ColumnHeader
    Friend WithEvents grdLlamada As System.Windows.Forms.DataGrid
    Friend WithEvents txtLlamadaObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents styLlamada As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents grdcolFLlamada As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdcolMotivo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents grdcolObservaciones As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents SeleccionCalleColonia As SigaMetClasses.SeleccionCalleColonia
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdcolUsuario As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents colInsistente As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTelCasa As System.Windows.Forms.ColumnHeader
    Friend WithEvents colObservaciones As System.Windows.Forms.ColumnHeader
    Friend WithEvents lblTelCasa As System.Windows.Forms.Label
    Friend WithEvents grpDatos As System.Windows.Forms.GroupBox
    Friend WithEvents lblObservacionesPedido As System.Windows.Forms.Label
    Friend WithEvents cboStatusBoletin As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents colRutaDescripcion As System.Windows.Forms.ColumnHeader
    Friend WithEvents cboRuta As SigaMetClasses.Combos.ComboRutaBoletin
    'Friend WithEvents cboRuta As SigaMetClasses.Combos.ComboRuta
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkTodasLasRutas As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkPortatil As System.Windows.Forms.CheckBox
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents colFCompromiso As System.Windows.Forms.ColumnHeader
    Friend WithEvents cboUsuarioCelula As System.Windows.Forms.ComboBox
    Friend WithEvents LabelNombreEmpresa1 As NombreEmpresa.LabelNombreEmpresa
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblChofer As System.Windows.Forms.Label

    Friend WithEvents colEstadoSGC As ColumnHeader
    Friend WithEvents colEstadoMG As ColumnHeader
    Friend WithEvents colAutotanqueMG As ColumnHeader
    Friend WithEvents colPedidoMG As ColumnHeader
    Friend WithEvents colFStatusMG As ColumnHeader

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBoletin))
        Me.imgLista = New System.Windows.Forms.ImageList(Me.components)
        Me.lvwBoletin = New System.Windows.Forms.ListView()
        Me.colPedidoReferencia = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAñoPed = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCelula = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPedido = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRuta = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRutaDescripcion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRutaBoletin = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRutaBoletinDescripcion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colHora = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFCompromiso = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCliente = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colNombre = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colDireccion = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPrioridad = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colUsuario = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colStatus = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colInsistente = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colTelCasa = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colObservaciones = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colRAF = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colEstadoSGC = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colEstadoMG = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAutotanqueMG = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colPedidoMG = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colFStatusMG = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.lblBoletines = New System.Windows.Forms.Label()
        Me.grdLlamada = New System.Windows.Forms.DataGrid()
        Me.styLlamada = New System.Windows.Forms.DataGridTableStyle()
        Me.grdcolFLlamada = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grdcolMotivo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grdcolUsuario = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.grdcolObservaciones = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.txtLlamadaObservaciones = New System.Windows.Forms.TextBox()
        Me.SeleccionCalleColonia = New SigaMetClasses.SeleccionCalleColonia((_URLGateway))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpDatos = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.lblObservacionesPedido = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTelCasa = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.cboStatusBoletin = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboRuta = New SigaMetClasses.Combos.ComboRutaBoletin()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkTodasLasRutas = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.chkPortatil = New System.Windows.Forms.CheckBox()
        Me.lblFecha = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.cboUsuarioCelula = New System.Windows.Forms.ComboBox()
        Me.LabelNombreEmpresa1 = New NombreEmpresa.LabelNombreEmpresa()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblChofer = New System.Windows.Forms.Label()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.btnSep2 = New System.Windows.Forms.ToolBarButton()
        Me.btnRefrescar = New System.Windows.Forms.ToolBarButton()
        Me.btnSep1 = New System.Windows.Forms.ToolBarButton()
        Me.btnCierreRuta = New System.Windows.Forms.ToolBarButton()
        Me.btnCarga = New System.Windows.Forms.ToolBarButton()
        Me.btnConfirmaSuministro = New System.Windows.Forms.ToolBarButton()
        Me.btnEnviarMensaje = New System.Windows.Forms.ToolBarButton()
        Me.btnConsultaCliente = New System.Windows.Forms.ToolBarButton()
        Me.btnCambioCompromiso = New System.Windows.Forms.ToolBarButton()
        Me.btnConfirmacionCliente = New System.Windows.Forms.ToolBarButton()
        Me.btnLlamadaOperador = New System.Windows.Forms.ToolBarButton()
        Me.btnActualizaStatusMG = New System.Windows.Forms.ToolBarButton()
        Me.tbBarra = New System.Windows.Forms.ToolBar()
        Me.DsTipoFactura1 = New Sigamet.dsTipoFactura()
        Me.btnReasignar = New System.Windows.Forms.ToolBarButton()
        CType(Me.grdLlamada, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.grpDatos.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.DsTipoFactura1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.imgLista.Images.SetKeyName(12, "Icono para el proceso de actualización.ico")
        Me.imgLista.Images.SetKeyName(13, "wmdm.ico")
        Me.imgLista.Images.SetKeyName(14, "ipad.png")
        '
        'lvwBoletin
        '
        Me.lvwBoletin.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvwBoletin.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvwBoletin.BackColor = System.Drawing.Color.Honeydew
        Me.lvwBoletin.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPedidoReferencia, Me.colAñoPed, Me.colCelula, Me.colPedido, Me.colRuta, Me.colRutaDescripcion, Me.colRutaBoletin, Me.colRutaBoletinDescripcion, Me.colHora, Me.colFCompromiso, Me.colCliente, Me.colNombre, Me.colDireccion, Me.colPrioridad, Me.colUsuario, Me.colStatus, Me.colInsistente, Me.colTelCasa, Me.colObservaciones, Me.colRAF})
        Me.lvwBoletin.FullRowSelect = True
        Me.lvwBoletin.GridLines = True
        Me.lvwBoletin.HideSelection = False
        Me.lvwBoletin.Location = New System.Drawing.Point(0, 88)
        Me.lvwBoletin.Name = "lvwBoletin"
        Me.lvwBoletin.Size = New System.Drawing.Size(1008, 264)
        Me.lvwBoletin.SmallImageList = Me.imgLista
        Me.lvwBoletin.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lvwBoletin, "Dé doble-clic para abrir la ventana de CallCenter de este cliente")
        Me.lvwBoletin.UseCompatibleStateImageBehavior = False
        Me.lvwBoletin.View = System.Windows.Forms.View.Details
        '
        'colPedidoReferencia
        '
        Me.colPedidoReferencia.Text = "Pedido"
        Me.colPedidoReferencia.Width = 100
        '
        'colAñoPed
        '
        Me.colAñoPed.Text = "AñoPed"
        Me.colAñoPed.Width = 0
        '
        'colCelula
        '
        Me.colCelula.Text = "Celula"
        Me.colCelula.Width = 0
        '
        'colPedido
        '
        Me.colPedido.Text = "Pedido"
        Me.colPedido.Width = 0
        '
        'colRuta
        '
        Me.colRuta.Text = "Ruta"
        Me.colRuta.Width = 0
        '
        'colRutaDescripcion
        '
        Me.colRutaDescripcion.Text = "Ruta"
        '
        'colRutaBoletin
        '
        Me.colRutaBoletin.Text = "RutaBoletin"
        Me.colRutaBoletin.Width = 0
        '
        'colRutaBoletinDescripcion
        '
        Me.colRutaBoletinDescripcion.Text = "RutaBoletin"
        '
        'colHora
        '
        Me.colHora.Text = "F.Alta"
        Me.colHora.Width = 70
        '
        'colFCompromiso
        '
        Me.colFCompromiso.Text = "F. Compromiso"
        Me.colFCompromiso.Width = 70
        '
        'colCliente
        '
        Me.colCliente.Text = "Cliente"
        Me.colCliente.Width = 100
        '
        'colNombre
        '
        Me.colNombre.Text = "Nombre"
        Me.colNombre.Width = 180
        '
        'colDireccion
        '
        Me.colDireccion.Text = "Dirección"
        Me.colDireccion.Width = 150
        '
        'colPrioridad
        '
        Me.colPrioridad.Text = "Prioridad"
        Me.colPrioridad.Width = 70
        '
        'colUsuario
        '
        Me.colUsuario.Text = "Atendió"
        Me.colUsuario.Width = 65
        '
        'colStatus
        '
        Me.colStatus.Text = "Estatus"
        Me.colStatus.Width = 80
        '
        'colInsistente
        '
        Me.colInsistente.Text = "Nota"
        Me.colInsistente.Width = 40
        '
        'colTelCasa
        '
        Me.colTelCasa.Text = "TelCasa"
        Me.colTelCasa.Width = 0
        '
        'colObservaciones
        '
        Me.colObservaciones.Text = "Observaciones"
        Me.colObservaciones.Width = 0
        '
        'colRAF
        '
        Me.colRAF.Text = "Reporte RAF"
        '
        'colEstadoSGC
        '
        Me.colEstadoSGC.Text = "Estado SGC"
        Me.colEstadoSGC.Width = 85
        '
        'colEstadoMG
        '
        Me.colEstadoMG.Text = "Estado MG"
        Me.colEstadoMG.Width = 85
        '
        'colAutotanqueMG
        '
        Me.colAutotanqueMG.Text = "AutotanqueMG"
        Me.colAutotanqueMG.Width = 0
        '
        'colPedidoMG
        '
        Me.colPedidoMG.Text = "PedidoMG"
        Me.colPedidoMG.Width = 0
        '
        'colFStatusMG
        '
        Me.colFStatusMG.Text = "Fecha Status MG"
        Me.colFStatusMG.Width = 85
        '
        'lblBoletines
        '
        Me.lblBoletines.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBoletines.AutoSize = True
        Me.lblBoletines.BackColor = System.Drawing.Color.LightGray
        Me.lblBoletines.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBoletines.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblBoletines.ImageList = Me.imgLista
        Me.lblBoletines.Location = New System.Drawing.Point(4, 4)
        Me.lblBoletines.Name = "lblBoletines"
        Me.lblBoletines.Size = New System.Drawing.Size(119, 23)
        Me.lblBoletines.TabIndex = 2
        Me.lblBoletines.Text = "Boletines:"
        Me.lblBoletines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdLlamada
        '
        Me.grdLlamada.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLlamada.BackgroundColor = System.Drawing.Color.Gainsboro
        Me.grdLlamada.CaptionBackColor = System.Drawing.Color.Khaki
        Me.grdLlamada.CaptionForeColor = System.Drawing.Color.Black
        Me.grdLlamada.CaptionText = "Llamadas del cliente"
        Me.grdLlamada.DataMember = ""
        Me.grdLlamada.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdLlamada.Location = New System.Drawing.Point(592, 16)
        Me.grdLlamada.Name = "grdLlamada"
        Me.grdLlamada.ReadOnly = True
        Me.grdLlamada.Size = New System.Drawing.Size(416, 168)
        Me.grdLlamada.TabIndex = 3
        Me.grdLlamada.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.styLlamada})
        '
        'styLlamada
        '
        Me.styLlamada.DataGrid = Me.grdLlamada
        Me.styLlamada.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.grdcolFLlamada, Me.grdcolMotivo, Me.grdcolUsuario, Me.grdcolObservaciones})
        Me.styLlamada.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.styLlamada.MappingName = "Llamada"
        Me.styLlamada.ReadOnly = True
        Me.styLlamada.RowHeadersVisible = False
        '
        'grdcolFLlamada
        '
        Me.grdcolFLlamada.Format = ""
        Me.grdcolFLlamada.FormatInfo = Nothing
        Me.grdcolFLlamada.HeaderText = "F.Llamada"
        Me.grdcolFLlamada.MappingName = "FLlamada"
        Me.grdcolFLlamada.Width = 120
        '
        'grdcolMotivo
        '
        Me.grdcolMotivo.Format = ""
        Me.grdcolMotivo.FormatInfo = Nothing
        Me.grdcolMotivo.HeaderText = "Motivo"
        Me.grdcolMotivo.MappingName = "DesMotivo"
        Me.grdcolMotivo.Width = 200
        '
        'grdcolUsuario
        '
        Me.grdcolUsuario.Format = ""
        Me.grdcolUsuario.FormatInfo = Nothing
        Me.grdcolUsuario.HeaderText = "Usuario"
        Me.grdcolUsuario.MappingName = "Usuario"
        Me.grdcolUsuario.Width = 90
        '
        'grdcolObservaciones
        '
        Me.grdcolObservaciones.Format = ""
        Me.grdcolObservaciones.FormatInfo = Nothing
        Me.grdcolObservaciones.HeaderText = "Observaciones"
        Me.grdcolObservaciones.MappingName = "Observaciones"
        Me.grdcolObservaciones.NullText = ""
        Me.grdcolObservaciones.Width = 0
        '
        'txtLlamadaObservaciones
        '
        Me.txtLlamadaObservaciones.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLlamadaObservaciones.Location = New System.Drawing.Point(592, 216)
        Me.txtLlamadaObservaciones.Multiline = True
        Me.txtLlamadaObservaciones.Name = "txtLlamadaObservaciones"
        Me.txtLlamadaObservaciones.ReadOnly = True
        Me.txtLlamadaObservaciones.Size = New System.Drawing.Size(416, 72)
        Me.txtLlamadaObservaciones.TabIndex = 5
        '
        'SeleccionCalleColonia
        '
        Me.SeleccionCalleColonia.AltaCalleColonia = True
        Me.SeleccionCalleColonia.Calle = 0
        Me.SeleccionCalleColonia.Colonia = 0
        Me.SeleccionCalleColonia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SeleccionCalleColonia.Location = New System.Drawing.Point(8, 72)
        Me.SeleccionCalleColonia.Name = "SeleccionCalleColonia"
        Me.SeleccionCalleColonia.Size = New System.Drawing.Size(536, 144)
        Me.SeleccionCalleColonia.TabIndex = 0
        Me.SeleccionCalleColonia._URLGateway = _URLGateway
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Panel1.Controls.Add(Me.grpDatos)
        Me.Panel1.Controls.Add(Me.grdLlamada)
        Me.Panel1.Controls.Add(Me.txtLlamadaObservaciones)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(0, 352)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1024, 312)
        Me.Panel1.TabIndex = 6
        '
        'grpDatos
        '
        Me.grpDatos.Controls.Add(Me.Label7)
        Me.grpDatos.Controls.Add(Me.lblNombre)
        Me.grpDatos.Controls.Add(Me.lblObservacionesPedido)
        Me.grpDatos.Controls.Add(Me.Label4)
        Me.grpDatos.Controls.Add(Me.lblTelCasa)
        Me.grpDatos.Controls.Add(Me.Label2)
        Me.grpDatos.Controls.Add(Me.Label3)
        Me.grpDatos.Controls.Add(Me.lblCliente)
        Me.grpDatos.Controls.Add(Me.SeleccionCalleColonia)
        Me.grpDatos.Location = New System.Drawing.Point(16, 8)
        Me.grpDatos.Name = "grpDatos"
        Me.grpDatos.Size = New System.Drawing.Size(568, 280)
        Me.grpDatos.TabIndex = 7
        Me.grpDatos.TabStop = False
        Me.grpDatos.Text = "Datos del pedido"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Nombre:"
        '
        'lblNombre
        '
        Me.lblNombre.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNombre.Location = New System.Drawing.Point(96, 48)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(448, 21)
        Me.lblNombre.TabIndex = 11
        Me.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblObservacionesPedido
        '
        Me.lblObservacionesPedido.BackColor = System.Drawing.Color.White
        Me.lblObservacionesPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblObservacionesPedido.Location = New System.Drawing.Point(96, 240)
        Me.lblObservacionesPedido.Name = "lblObservacionesPedido"
        Me.lblObservacionesPedido.Size = New System.Drawing.Size(448, 32)
        Me.lblObservacionesPedido.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(352, 211)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Tel.Casa:"
        '
        'lblTelCasa
        '
        Me.lblTelCasa.BackColor = System.Drawing.Color.White
        Me.lblTelCasa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTelCasa.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTelCasa.Location = New System.Drawing.Point(416, 208)
        Me.lblTelCasa.Name = "lblTelCasa"
        Me.lblTelCasa.Size = New System.Drawing.Size(128, 21)
        Me.lblTelCasa.TabIndex = 7
        Me.lblTelCasa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 240)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 32)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Observaciones del pedido:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Cliente:"
        '
        'lblCliente
        '
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.Location = New System.Drawing.Point(96, 24)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(448, 21)
        Me.lblCliente.TabIndex = 2
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.Color.Khaki
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(592, 192)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(416, 21)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Observaciones de la llamada"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(968, 8)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'cboStatusBoletin
        '
        Me.cboStatusBoletin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboStatusBoletin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatusBoletin.Items.AddRange(New Object() {"BOLETIN", "BOLETINADO"})
        Me.cboStatusBoletin.Location = New System.Drawing.Point(918, 8)
        Me.cboStatusBoletin.Name = "cboStatusBoletin"
        Me.cboStatusBoletin.Size = New System.Drawing.Size(88, 21)
        Me.cboStatusBoletin.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(873, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "Estatus:"
        '
        'cboRuta
        '
        Me.cboRuta.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRuta.Enabled = False
        Me.cboRuta.Location = New System.Drawing.Point(848, 21)
        Me.cboRuta.Name = "cboRuta"
        Me.cboRuta.Size = New System.Drawing.Size(96, 21)
        Me.cboRuta.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(808, 24)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Ruta:"
        '
        'chkTodasLasRutas
        '
        Me.chkTodasLasRutas.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkTodasLasRutas.Checked = True
        Me.chkTodasLasRutas.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkTodasLasRutas.Location = New System.Drawing.Point(696, 25)
        Me.chkTodasLasRutas.Name = "chkTodasLasRutas"
        Me.chkTodasLasRutas.Size = New System.Drawing.Size(104, 12)
        Me.chkTodasLasRutas.TabIndex = 14
        Me.chkTodasLasRutas.Text = "&Todas las rutas"
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(729, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 13)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "Célula:"
        '
        'chkPortatil
        '
        Me.chkPortatil.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkPortatil.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkPortatil.Location = New System.Drawing.Point(547, 6)
        Me.chkPortatil.Name = "chkPortatil"
        Me.chkPortatil.Size = New System.Drawing.Size(56, 24)
        Me.chkPortatil.TabIndex = 17
        Me.chkPortatil.Text = "&Portatil"
        '
        'lblFecha
        '
        Me.lblFecha.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFecha.AutoSize = True
        Me.lblFecha.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.Location = New System.Drawing.Point(606, 11)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(40, 13)
        Me.lblFecha.TabIndex = 18
        Me.lblFecha.Text = "Fecha:"
        '
        'dtpFecha
        '
        Me.dtpFecha.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFecha.Location = New System.Drawing.Point(645, 8)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(80, 21)
        Me.dtpFecha.TabIndex = 19
        '
        'cboUsuarioCelula
        '
        Me.cboUsuarioCelula.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboUsuarioCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsuarioCelula.Location = New System.Drawing.Point(768, 8)
        Me.cboUsuarioCelula.Name = "cboUsuarioCelula"
        Me.cboUsuarioCelula.Size = New System.Drawing.Size(96, 21)
        Me.cboUsuarioCelula.TabIndex = 7
        '
        'LabelNombreEmpresa1
        '
        Me.LabelNombreEmpresa1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelNombreEmpresa1.AutoSize = True
        Me.LabelNombreEmpresa1.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelNombreEmpresa1.Location = New System.Drawing.Point(696, 44)
        Me.LabelNombreEmpresa1.Name = "LabelNombreEmpresa1"
        Me.LabelNombreEmpresa1.Size = New System.Drawing.Size(0, 16)
        Me.LabelNombreEmpresa1.TabIndex = 20
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.lblChofer)
        Me.Panel2.Controls.Add(Me.chkTodasLasRutas)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.cboRuta)
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.lblBoletines)
        Me.Panel2.Location = New System.Drawing.Point(0, 40)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1008, 48)
        Me.Panel2.TabIndex = 21
        '
        'lblChofer
        '
        Me.lblChofer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblChofer.BackColor = System.Drawing.Color.LightGray
        Me.lblChofer.Font = New System.Drawing.Font("Verdana", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChofer.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblChofer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblChofer.ImageList = Me.imgLista
        Me.lblChofer.Location = New System.Drawing.Point(8, 28)
        Me.lblChofer.Name = "lblChofer"
        Me.lblChofer.Size = New System.Drawing.Size(680, 16)
        Me.lblChofer.TabIndex = 15
        Me.lblChofer.Text = "Chofer"
        Me.lblChofer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 2
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Tag = "Cerrar"
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.ToolTipText = "Cerrar"
        '
        'btnSep2
        '
        Me.btnSep2.Name = "btnSep2"
        Me.btnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnRefrescar
        '
        Me.btnRefrescar.ImageIndex = 1
        Me.btnRefrescar.Name = "btnRefrescar"
        Me.btnRefrescar.Tag = "Refrescar"
        Me.btnRefrescar.Text = "Refrescar"
        Me.btnRefrescar.ToolTipText = "Refrescar información"
        '
        'btnSep1
        '
        Me.btnSep1.Name = "btnSep1"
        Me.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnCierreRuta
        '
        Me.btnCierreRuta.ImageIndex = 9
        Me.btnCierreRuta.Name = "btnCierreRuta"
        Me.btnCierreRuta.Tag = "CierreRuta"
        Me.btnCierreRuta.Text = "Fin de ruta"
        '
        'btnCarga
        '
        Me.btnCarga.ImageIndex = 10
        Me.btnCarga.Name = "btnCarga"
        Me.btnCarga.Tag = "Carga"
        Me.btnCarga.Text = "Realiza carga"
        Me.btnCarga.ToolTipText = "Realizar carga"
        '
        'btnConfirmaSuministro
        '
        Me.btnConfirmaSuministro.ImageIndex = 7
        Me.btnConfirmaSuministro.Name = "btnConfirmaSuministro"
        Me.btnConfirmaSuministro.Tag = "ConfirmacionSuministro"
        Me.btnConfirmaSuministro.Text = "Atención cliente"
        Me.btnConfirmaSuministro.ToolTipText = "Cambio de status a atendido"
        Me.btnConfirmaSuministro.Visible = False
        '
        'btnEnviarMensaje
        '
        Me.btnEnviarMensaje.ImageIndex = 11
        Me.btnEnviarMensaje.Name = "btnEnviarMensaje"
        Me.btnEnviarMensaje.Tag = "EnviarMensaje"
        Me.btnEnviarMensaje.Text = "Enviar Mensaje"
        Me.btnEnviarMensaje.ToolTipText = "Enviar Mensaje"
        '
        'btnConsultaCliente
        '
        Me.btnConsultaCliente.Enabled = False
        Me.btnConsultaCliente.ImageIndex = 0
        Me.btnConsultaCliente.Name = "btnConsultaCliente"
        Me.btnConsultaCliente.Tag = "ConsultaCliente"
        Me.btnConsultaCliente.Text = "Consulta cliente"
        Me.btnConsultaCliente.ToolTipText = "Consultar cliente seleccionado"
        '
        'btnCambioCompromiso
        '
        Me.btnCambioCompromiso.Enabled = False
        Me.btnCambioCompromiso.ImageIndex = 5
        Me.btnCambioCompromiso.Name = "btnCambioCompromiso"
        Me.btnCambioCompromiso.Tag = "CambioCompromiso"
        Me.btnCambioCompromiso.Text = "Cambio de compromiso"
        Me.btnCambioCompromiso.ToolTipText = "Cambio de fecha de compromiso"
        '
        'btnConfirmacionCliente
        '
        Me.btnConfirmacionCliente.Enabled = False
        Me.btnConfirmacionCliente.ImageIndex = 3
        Me.btnConfirmacionCliente.Name = "btnConfirmacionCliente"
        Me.btnConfirmacionCliente.Tag = "ConfirmacionCliente"
        Me.btnConfirmacionCliente.Text = "Confirmación cliente"
        Me.btnConfirmacionCliente.ToolTipText = "Confirmación del cliente"
        '
        'btnLlamadaOperador
        '
        Me.btnLlamadaOperador.Enabled = False
        Me.btnLlamadaOperador.ImageIndex = 4
        Me.btnLlamadaOperador.Name = "btnLlamadaOperador"
        Me.btnLlamadaOperador.Tag = "LlamadaOperador"
        Me.btnLlamadaOperador.Text = "Llamada operador"
        Me.btnLlamadaOperador.ToolTipText = "Llamada operador"
        '
        'btnActualizaStatusMG
        '
        Me.btnActualizaStatusMG.Enabled = False
        Me.btnActualizaStatusMG.ImageIndex = 13
        Me.btnActualizaStatusMG.Name = "btnActualizaStatusMG"
        Me.btnActualizaStatusMG.Tag = "StatusMG"
        Me.btnActualizaStatusMG.Text = "Actualizar MG"
        Me.btnActualizaStatusMG.ToolTipText = "Actualizar estatus pedidos Móvil Gas"
        Me.btnActualizaStatusMG.Visible = False
        '
        'tbBarra
        '
        Me.tbBarra.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.tbBarra.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnLlamadaOperador, Me.btnConfirmacionCliente, Me.btnCambioCompromiso, Me.btnConsultaCliente, Me.btnEnviarMensaje, Me.btnConfirmaSuministro, Me.btnCarga, Me.btnCierreRuta, Me.btnReasignar, Me.btnSep1, Me.btnRefrescar, Me.btnActualizaStatusMG, Me.btnSep2, Me.btnCerrar})
        Me.tbBarra.DropDownArrows = True
        Me.tbBarra.ImageList = Me.imgLista
        Me.tbBarra.Location = New System.Drawing.Point(0, 0)
        Me.tbBarra.Name = "tbBarra"
        Me.tbBarra.ShowToolTips = True
        Me.tbBarra.Size = New System.Drawing.Size(1008, 42)
        Me.tbBarra.TabIndex = 0
        '
        'DsTipoFactura1
        '
        Me.DsTipoFactura1.DataSetName = "dsTipoFactura"
        Me.DsTipoFactura1.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsTipoFactura1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btnReasignar
        '
        Me.btnReasignar.ImageIndex = 14
        Me.btnReasignar.Name = "btnReasignar"
        Me.btnReasignar.Tag = "ReasignarPedidos"
        Me.btnReasignar.Text = "Reasignar pedidos"
        Me.btnReasignar.ToolTipText = "Reasignar pedidos boletinados"
        '
        'frmBoletin
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(1008, 645)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.LabelNombreEmpresa1)
        Me.Controls.Add(Me.cboUsuarioCelula)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpFecha)
        Me.Controls.Add(Me.chkPortatil)
        Me.Controls.Add(Me.cboStatusBoletin)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lvwBoletin)
        Me.Controls.Add(Me.tbBarra)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBoletin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Boletines"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.grdLlamada, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpDatos.ResumeLayout(False)
        Me.grpDatos.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.DsTipoFactura1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public autotanque As String

    Private Sub LimpiaVariables()
        _AñoPed = 0
        _Celula = 0
        _Pedido = 0
        _Ruta = 0
        _PedidoReferencia = ""
        _Nombre = ""
        _Cliente = 0
        _LlevaLaNota = ""
        _FCompromiso = Nothing
        _Autotanque = 0
        _FAlta = Nothing
    End Sub


#Region "Barra de botones"

    Private Sub CambioCompromiso()
        If chkPortatil.Checked Then
            CambioCompromisoPortatil()
            Exit Sub
        End If
        Cursor = Cursors.WaitCursor
        Dim frmPedido As New Pedido()
        frmPedido.Entrada(_Cliente, _Nombre, Me.SeleccionCalleColonia.CP, _Ruta, 0, _Celula, _Ruta.ToString, 1)
        'frmPedido.Entrada(_Cliente, _Nombre, Me.SeleccionCalleColonia.CP, _Ruta, "", _Celula, _Ruta.ToString, 1)
        frmPedido.Dispose()
        Cursor = Cursors.Default
    End Sub

    Private Function ConsultaClientesBoletinCRM(ByVal Pedidos As List(Of RTGMCore.Pedido)) As Integer

        Dim objPedido As New RTGMCore.Pedido
        Dim cliente As New Integer
        Dim oSolicitud As RTGMGateway.SolicitudGateway
        Dim oDireccionEntrega As RTGMCore.DireccionEntrega
        Dim oGateway As New RTGMGateway.RTGMGateway(SigaMetClasses.GLOBAL_Modulo, GLOBAL_ConString)

        lvwBoletin.Items.Clear()

        For Each objPedido In Pedidos
            cliente = objPedido.IDDireccionEntrega
            oSolicitud = New RTGMGateway.SolicitudGateway

            'oSolicitud.Fuente = RTGMCore.Fuente.Sigamet
            oSolicitud.IDEmpresa = SigaMetClasses.GLOBAL_Empresa

            oSolicitud.IDCliente = cliente
            oGateway.URLServicio = _URLGateway
            oDireccionEntrega = oGateway.buscarDireccionEntrega(oSolicitud)
            objPedido.DireccionEntrega.Nombre = oDireccionEntrega.Nombre

            Dim oItem As ListViewItem
            Dim tipo As Integer = 0

            oItem = New ListViewItem(CType(objPedido.PedidoReferencia, String).Trim, tipo) '0
            oItem.SubItems.Add(CType(objPedido.AnioPed, Short).ToString) '1
            oItem.SubItems.Add(CType(objPedido.IDZona, Byte).ToString) '2
            oItem.SubItems.Add(CType(objPedido.IDPedido, Integer).ToString) '3
            oItem.SubItems.Add(CType(objPedido.RutaOrigen.NumeroRuta, Short).ToString) '4
            oItem.SubItems.Add(CType(objPedido.RutaOrigen.Descripcion, String).Trim) '5
            oItem.SubItems.Add(CType(objPedido.RutaOrigen.NumeroRuta, Short).ToString) '6
            oItem.SubItems.Add(CType(objPedido.RutaBoletin.Descripcion, String).Trim) '7
            oItem.SubItems.Add(CType(objPedido.FAlta, Date).ToString) '8
            oItem.SubItems.Add(CType(objPedido.FCompromiso, Date).ToShortDateString) '9
            oItem.SubItems.Add(CType(objPedido.IDDireccionEntrega, String).Trim) '10
            oItem.SubItems.Add(CType(oDireccionEntrega.Nombre, String).Trim) '11
            oItem.SubItems.Add(CType(oDireccionEntrega.DireccionCompleta, String).Trim) '12
            oItem.SubItems.Add(CType(objPedido.PrioridadPedido, String).Trim) '13
            oItem.SubItems.Add(CType(objPedido.IDUsuarioAlta, String).Trim) '14
            oItem.SubItems.Add(CType(objPedido.EstatusBoletin, String).Trim) '15
            oItem.SubItems.Add(CType(objPedido.LlamadaInsistente, String).Trim) '16
            oItem.SubItems.Add(CType(oDireccionEntrega.Telefono1, String).Trim) '17
            oItem.SubItems.Add(CType(oDireccionEntrega.Observaciones, String).Trim) '18


            lvwBoletin.Items.Add(oItem)
        Next

        Return 0


    End Function


    Private Sub CargaBoletin(Optional ByVal URLGateway As String = "")
        ' chkPortatil.Checked = True

        If chkPortatil.Checked Then
            CargaBoletinPortatil()
            Exit Sub
        End If
        Cursor = Cursors.WaitCursor

        Dim oGateway As RTGMGateway.RTGMGateway

        Dim objPedidoGateway As New RTGMGateway.RTGMPedidoGateway
        Dim _CelulaCarga As Byte
        Dim FechaDtp As Date
        Dim SolicitudPedidoGateway As RTGMGateway.SolicitudPedidoGateway

        FechaDtp = dtpFecha.Value.Date

        If CType(cboUsuarioCelula.SelectedValue, Byte) <> 0 Then
            _CelulaCarga = CType(cboUsuarioCelula.SelectedValue, Byte)
        Else
            _CelulaCarga = Main.GLOBAL_Celula
        End If

        If Not (_URLGateway Is String.Empty Or _URLGateway Is Nothing) Then
            oGateway = New RTGMGateway.RTGMGateway(SigaMetClasses.GLOBAL_Modulo, GLOBAL_ConString)
            oGateway.URLServicio = _URLGateway

            SolicitudPedidoGateway.FechaCompromisoInicio = FechaDtp
            SolicitudPedidoGateway.IDZona = _CelulaCarga
            SolicitudPedidoGateway.EstatusBoletin = "BOLETIN"
            'SolicitudPedidoGateway.FuenteDatos = RTGMCore.Fuente.Sigamet
            SolicitudPedidoGateway.IDEmpresa = SigaMetClasses.GLOBAL_Empresa

            Dim ListaPedidos As List(Of RTGMCore.Pedido)
            objPedidoGateway.URLServicio = _URLGateway
            ListaPedidos = objPedidoGateway.buscarPedidos(SolicitudPedidoGateway)
            'Consulta los pedidos que vienen como respuesta del Web Service 
            ConsultaClientesBoletinCRM(ListaPedidos)

            Cursor = Cursors.Default


        Else

            If lvwBoletin.Columns.Contains(colEstadoMG) Then
                lvwBoletin.Columns.Remove(colPedidoMG)
                lvwBoletin.Columns.Remove(colAutotanqueMG)
                lvwBoletin.Columns.Remove(colEstadoMG)
                lvwBoletin.Columns.Remove(colFStatusMG)
            End If

            btnLlamadaOperador.Enabled = False
            btnConfirmacionCliente.Enabled = False
            btnCambioCompromiso.Enabled = False
            btnConsultaCliente.Enabled = False

            LimpiaVariables()

            grpDatos.Visible = False

            lvwBoletin.Items.Clear()
            grdLlamada.DataSource = Nothing
            txtLlamadaObservaciones.Text = ""
            lblChofer.Text = String.Empty


            If CType(cboUsuarioCelula.SelectedValue, Byte) <> 0 Then
                _CelulaCarga = CType(cboUsuarioCelula.SelectedValue, Byte)
            Else
                _CelulaCarga = Main.GLOBAL_Celula
            End If

            Dim cmd As New SqlCommand("spCCConsultaBoletinDia", CnnSigamet)
            With cmd
                .CommandTimeout = 90
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@Celula", SqlDbType.TinyInt).Value = _CelulaCarga
                .Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFecha.Value.Date
                .Parameters.Add("@StatusBoletin", SqlDbType.VarChar).Value = cboStatusBoletin.Text
                If Not chkTodasLasRutas.Checked Then
                    .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = cboRuta.Ruta
                End If
            End With

            Dim dr As SqlDataReader

            Try
                AbreConexion()

                Try
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                Catch ex As Exception
                    MessageBox.Show("La base de datos está ocupada.  Por favor intente de nuevo.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CierraConexion()
                    Cursor = Cursors.Default
                    Exit Sub
                End Try

                Dim ixc As Integer
                Dim showSGCStatus As Boolean

                If Not lvwBoletin.Columns.Contains(colEstadoSGC) Then
                    For ixc = 0 To dr.FieldCount - 1
                        If dr.GetName(ixc).Equals("StatusSGC") Then
                            lvwBoletin.Columns.Add(colEstadoSGC)
                            showSGCStatus = True
                            Exit For
                        End If
                    Next
                Else
                    showSGCStatus = True
                End If

                Dim oItem As ListViewItem

                While dr.Read
                    Dim tipo As Integer = 0

                    If CType(dr("Tipo"), String).Trim.ToUpper = "I" Then
                        tipo = 8
                    Else
                        tipo = 6
                    End If

                    oItem = New ListViewItem(CType(dr("PedidoReferencia"), String).Trim, tipo) '0
                    oItem.SubItems.Add(CType(dr("AñoPed"), Short).ToString) '1
                    oItem.SubItems.Add(CType(dr("Celula"), Byte).ToString) '2
                    oItem.SubItems.Add(CType(dr("Pedido"), Integer).ToString) '3
                    oItem.SubItems.Add(CType(dr("Ruta"), Short).ToString) '4
                    oItem.SubItems.Add(CType(dr("RutaDescripcion"), String).Trim) '5
                    oItem.SubItems.Add(CType(dr("RutaBoletin"), Short).ToString) '6
                    oItem.SubItems.Add(CType(dr("RutaDescripcionBoletin"), String).Trim) '7
                    oItem.SubItems.Add(CType(dr("FAlta"), Date).ToString) '8
                    oItem.SubItems.Add(CType(dr("FCompromiso"), Date).ToShortDateString) '9
                    oItem.SubItems.Add(CType(dr("Cliente"), String)) '10
                    oItem.SubItems.Add(CType(dr("Nombre"), String).Trim) '11
                    oItem.SubItems.Add(CType(dr("DireccionCompleta"), String).Trim) '12
                    oItem.SubItems.Add(CType(dr("PrioridadPedidoDescripcion"), String).Trim) '13
                    oItem.SubItems.Add(CType(dr("Usuario"), String).Trim) '14
                    oItem.SubItems.Add(CType(dr("StatusBoletin"), String).Trim) '15
                    oItem.SubItems.Add(CType(dr("Insistente"), String).Trim) '16
                    oItem.SubItems.Add(CType(dr("TelCasa"), String).Trim) '17
                    oItem.SubItems.Add(CType(dr("Observaciones"), String).Trim) '18

                    If CType(dr("RutaBoletin"), Short) = 0 Then '19
                        oItem.SubItems.Add(CType(dr("ReporteRAF"), String).ToString)
                    Else
                        oItem.SubItems.Add(CType(dr("ReporteRAFBoletin"), String).ToString)
                    End If

                    If showSGCStatus Then
                        oItem.SubItems.Add(CType(IIf(dr("StatusSGC") Is DBNull.Value, String.Empty, dr("StatusSGC")), String).Trim.ToUpper) '20
                    End If

                    lvwBoletin.Items.Add(oItem)
                End While

                If Not Global_MuestraRutaBoletin Then
                    lvwBoletin.Columns(6).Width = 0
                    lvwBoletin.Columns(7).Width = 0
                End If
                'Desactivado 16/11/2004
                'lblBoletines.Text = "Boletines de: "
                lblBoletines.Text = ""
                lblChofer.Text = String.Empty

                If chkTodasLasRutas.Checked Then
                    lblBoletines.Text &= cboUsuarioCelula.Text.Trim
                Else
                    lblBoletines.Text &= cboRuta.Descripcion
                End If
                'lblBoletines.Text &= " con el estatus " & Me.cboStatusBoletin.Text.Trim & " (" & lvwBoletin.Items.Count.ToString & " en total)"
                lblBoletines.Text &= " - Status " & Me.cboStatusBoletin.Text.Trim & " (" & lvwBoletin.Items.Count.ToString & " en total)"

            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
                cmd.Dispose()
                Cursor = Cursors.Default

            End Try

            If Not chkTodasLasRutas.Checked Then
                ConsultaChoferes()
            End If

        End If






    End Sub

    Private Sub ConsultaChoferes()
        lblChofer.Text = String.Empty

        Dim cmd As New SqlCommand("spCCConsultaTripulacionBoletin", CnnSigamet)
        With cmd
            .CommandTimeout = 90
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = cboRuta.Ruta
            .Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dtpFecha.Value.Date
        End With

        Dim dr As SqlDataReader

        Try
            AbreConexion()
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            While dr.Read
                lblChofer.Text = "AT: " & CType(dr("Autotanque"), String) &
                    " OP: " & CType(dr("Operador"), String) &
                    "/" & CType(dr("Ayudante"), String) & " "
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            cmd.Dispose()
            Cursor = Cursors.Default
        End Try
    End Sub

    'Integración de funcionalidades de portatil desarrolladas por transforma 25012012
    ' Visualiza la carga a realizar del pedido portatil CAGP20111122
    Private Sub CargaPedido()
        Dim oCargar As New ClienteZonaEconomica.frmMovimientoAlmacenPedidos(dtpFecha.Value.Date, GLOBAL_Servidor, GLOBAL_Database, GLOBAL_Usuario, GLOBAL_Password,
            GLOBAL_RutaReportes, CnnSigamet)
        oCargar.ShowDialog()
    End Sub

    Private Enum enumTipoLlamada
        Operador = 1
        Cliente = 2
        Suministro = 3
    End Enum

    Private Sub Llamada(ByVal TipoLlamada As enumTipoLlamada)
        If chkPortatil.Checked Then
            LlamadaPortatil(TipoLlamada)
            Exit Sub
        End If
        Cursor = Cursors.WaitCursor
        'si tipo llamada = 2 , inicializar con el constuctor

        If (TipoLlamada = enumTipoLlamada.Cliente) Then
            Dim oItem As ListViewItem
            Dim pedidoReferencia As String = Nothing
            Dim rutaPedido As Int16
            For Each oItem In lvwBoletin.Items
                If oItem.Selected Then
                    pedidoReferencia = oItem.SubItems(0).Text.Trim
                    rutaPedido = Convert.ToInt16(oItem.SubItems(4).Text.Trim())
                End If
            Next

            Dim frmLlamada As New Llamada(pedidoReferencia, rutaPedido, dtpFecha.Value.Date, DtCel, _URLGateway)
            frmLlamada.Entrada(_Cliente, _Nombre, _Celula, _Pedido, lblTelCasa.Text, _Ruta, _AñoPed, TipoLlamada, _FCompromiso, False, _FAlta)
            'Dim frmLlamada As New Llamada("20148118689", rutaPedido, dtpFecha.Value.Date, DtCel)
            'frmLlamada.Entrada(_Cliente, _Nombre, 8, 118689, lblTelCasa.Text, _Ruta, 2014, TipoLlamada)

            If frmLlamada.Tag Is Nothing Then
            Else
                If (CType(frmLlamada.Tag, Boolean) = True) Then
                    MessageBox.Show("El pedido ha sido boletinado exitosamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("El pedido No ha sido boletinado exitosamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If


        Else
            Dim frmLlamada As New Llamada()
            frmLlamada.Entrada(_Cliente, _Nombre, _Celula, _Pedido, lblTelCasa.Text, _Ruta, _AñoPed, TipoLlamada, _FCompromiso, False, _FAlta)
            frmLlamada.Dispose()
        End If
        'TODO Debería validar si sí se dió de alta la llamada o no
        CargaBoletin()

        Cursor = Cursors.Default
    End Sub

    Private Sub tbBarra_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tbBarra.ButtonClick
        Select Case e.Button.Tag.ToString()
            Case "LlamadaOperador"
                Llamada(enumTipoLlamada.Operador)
            Case "ConfirmacionCliente"
                ConfirmacionCliente(enumTipoLlamada.Cliente)
            Case "ConfirmacionSuministro"
                ConfirmacionCliente(enumTipoLlamada.Suministro)
            Case "Refrescar"
                CargaBoletin()
            Case "CambioCompromiso"
                CambioCompromiso()
            Case "CierreRuta"
                FinRuta()
            Case "ConsultaCliente"
                If Not chkPortatil.Checked Then
                    ConsultaCliente()
                End If
                'Integración de funcionalidades de portatil desarrolladas por transforma 25012012
            Case "Carga"
                If chkPortatil.Checked Then
                    CargaPedido()
                End If
            Case "EnviarMensaje"
                EnviarMensaje()
            Case "Cerrar"
                Me.Close()
            Case "StatusMG"
                ActualizarPedidosMG()
            Case "ReasignarPedidos"
                ReasignarPedidosBoletinados()

        End Select
    End Sub

    Private Sub ConsultaCliente()
        Cursor = Cursors.WaitCursor
        Dim oConsultaCliente As New SigaMetClasses.frmConsultaCliente(_Cliente, PermiteCapturarNotas:=False)
        oConsultaCliente.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub EnviarMensaje()
        Dim mensaje As New frmEnviarMensaje()
        mensaje.ShowDialog()
    End Sub

    Private Sub ConfirmacionCliente(ByVal Tipo As enumTipoLlamada)
        If chkPortatil.Checked Then
            ConfirmacionClientePortatil(Tipo)
            Exit Sub
        End If
        Dim TotalSeleccionados As Integer
        Dim oItem As ListViewItem
        Dim strMensaje As String = Nothing

        'texis
        ''Llamar aquí la función de boletín en línea.
        ''si el resultado es verdadero ExitSub
        'If (BoletinarPedido()) Then
        '    CargaBoletin()
        '    Exit Sub
        'Else


        For Each oItem In lvwBoletin.Items
            If oItem.Selected Then
                'strMensaje &= "Ruta: " & oItem.SubItems(4).Text & " Cliente: " & oItem.SubItems(6).Text & " " & oItem.SubItems(7).Text & " Dirección: " & oItem.SubItems(10).Text & Chr(13)
                strMensaje &= "Ruta: " & oItem.SubItems(4).Text & " Cliente: " & oItem.SubItems(8).Text & " " & oItem.SubItems(9).Text &
                    " Dirección: " & oItem.SubItems(10).Text & Chr(13)
                TotalSeleccionados += 1
            End If
        Next

        If TotalSeleccionados <= 1 Then
            Llamada(Tipo)
        Else

            If MessageBox.Show("¿Desea realizar la confirmación en grupo de los siguientes pedidos? " & Chr(13) & Chr(13) & strMensaje, _Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor

                Me.Refresh()

                Dim oEspere As New frmBoletinEspere()
                Me.AddOwnedForm(oEspere)

                oEspere.Show()
                oEspere.Refresh()

                Dim Transaccion As SqlTransaction
                Dim cmdInsert As New SqlCommand("spCCBoletinaPedido")
                cmdInsert.Connection = CnnSigamet
                cmdInsert.CommandTimeout = 100
                cmdInsert.CommandType = CommandType.StoredProcedure

                Try
                    AbreConexion()
                Catch
                    MessageBox.Show(SigaMetClasses.M_NO_CONEXION, _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End Try


                Transaccion = CnnSigamet.BeginTransaction
                cmdInsert.Transaction = Transaccion

                Try

                    For Each oItem In lvwBoletin.Items
                        If oItem.Selected Then

                            Dim AñoPed As Short = CType(oItem.SubItems(1).Text, Short)
                            Dim Celula As Byte = CType(oItem.SubItems(2).Text, Byte)
                            Dim Pedido As Integer = CType(oItem.SubItems(3).Text, Integer)
                            Dim Cliente As Integer = CType(oItem.SubItems(8).Text, Integer)

                            cmdInsert.Parameters.Clear()
                            cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
                            cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = AñoPed
                            cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
                            cmdInsert.Parameters.Add("@Usuario", SqlDbType.Text).Value = GLOBAL_Usuario

                            cmdInsert.ExecuteNonQuery()

                        End If
                    Next

                    Transaccion.Commit()

                Catch ex As Exception
                    Transaccion.Rollback()
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Finally
                    CierraConexion()
                    Cursor = Cursors.Default
                    Me.RemoveOwnedForm(oEspere)
                    oEspere.Close()
                    cmdInsert.Dispose()
                    CargaBoletin()

                End Try
            End If
            'End If
        End If



    End Sub
#End Region


#Region "ListView"

    Private Sub lvwBoletin_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwBoletin.ColumnClick
        Try
            If e.Column <> _Columna Then
                _Columna = e.Column
                lvwBoletin.Sorting = System.Windows.Forms.SortOrder.Ascending
            Else
                If lvwBoletin.Sorting = System.Windows.Forms.SortOrder.Ascending Then
                    lvwBoletin.Sorting = System.Windows.Forms.SortOrder.Descending
                Else
                    lvwBoletin.Sorting = System.Windows.Forms.SortOrder.Ascending
                End If
            End If
            lvwBoletin.Sort()

            Select Case e.Column
                'Case 8
                Case 10
                    lvwBoletin.ListViewItemSorter = New SigaMetClasses.ListViewComparador(e.Column, lvwBoletin.Sorting, SigaMetClasses.ListViewComparador.enumTipoDatoComparacion.Numerico)
                    'Case 6
                Case 8
                    lvwBoletin.ListViewItemSorter = New SigaMetClasses.ListViewComparador(e.Column, lvwBoletin.Sorting, SigaMetClasses.ListViewComparador.enumTipoDatoComparacion.Fecha)
                Case Else
                    lvwBoletin.ListViewItemSorter = New SigaMetClasses.ListViewComparador(e.Column, lvwBoletin.Sorting)
            End Select

        Catch
            lvwBoletin.Refresh()
        End Try
    End Sub

    Private Function ConsultarDetallePedidoCRM(ByVal objPedidoParam As RTGMCore.Pedido) As RTGMCore.Pedido

        Dim cliente As New Integer
        Dim oGateway As New RTGMGateway.RTGMGateway(SigaMetClasses.GLOBAL_Modulo, GLOBAL_ConString)
        Dim objPedidoGateway As New RTGMGateway.RTGMPedidoGateway
        Dim SolicitudPedidoGateway As RTGMGateway.SolicitudPedidoGateway
        SolicitudPedidoGateway.PedidoReferencia = objPedidoParam.PedidoReferencia
        SolicitudPedidoGateway.IDEmpresa = objPedidoParam.IDEmpresa
        SolicitudPedidoGateway.IDZona = objPedidoParam.IDZona
        SolicitudPedidoGateway.EstatusBoletin = "BOLETIN"
        SolicitudPedidoGateway.FechaCompromisoInicio = objPedidoParam.FCompromiso
        ' SolicitudPedidoGateway.FuenteDatos = RTGMCore.Fuente.Sigamet
        Dim ListaPedidos As List(Of RTGMCore.Pedido)
        objPedidoGateway.URLServicio = _URLGateway
        ListaPedidos = objPedidoGateway.buscarPedidos(SolicitudPedidoGateway)

        If ListaPedidos.Count > 0 Then
            Return ListaPedidos(0)
        Else
            Return objPedidoParam
        End If


    End Function

    Private Sub lvwBoletin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwBoletin.SelectedIndexChanged
        Dim dt As DataTable
        Try
            If Not IsNothing(lvwBoletin.FocusedItem) Then

                _PedidoReferencia = lvwBoletin.FocusedItem.Text.Trim
                _AñoPed = CType(lvwBoletin.FocusedItem.SubItems(1).Text, Short)
                _Celula = CType(lvwBoletin.FocusedItem.SubItems(2).Text, Byte)
                _Pedido = CType(lvwBoletin.FocusedItem.SubItems(3).Text, Integer)
                _Ruta = CType(lvwBoletin.FocusedItem.SubItems(4).Text, Short)
                _Rutaboletin = CType(lvwBoletin.FocusedItem.SubItems(6).Text, Short)
                _Cliente = CType(lvwBoletin.FocusedItem.SubItems(10).Text, Integer)
                _Nombre = CType(lvwBoletin.FocusedItem.SubItems(11).Text, String)
                _LlevaLaNota = CType(lvwBoletin.FocusedItem.SubItems(16).Text, String).Trim
                _FCompromiso = CType(lvwBoletin.FocusedItem.SubItems(9).Text, Date)
                _FAlta = CType(lvwBoletin.FocusedItem.SubItems(8).Text, Date)
                If chkPortatil.Checked Then
                    _Autotanque = CType(lvwBoletin.FocusedItem.SubItems(21).Text, Integer)
                End If

                lblCliente.Text = _Cliente.ToString
                lblNombre.Text = _Nombre
                lblTelCasa.Text = SigaMetClasses.FormatoTelefono(CType(lvwBoletin.FocusedItem.SubItems(17).Text, String).Trim)

                'Se agrega funcionalidad para ir al Web Service a consultar el detalle del pedido o del cliente . 

                Dim objPedido As New RTGMCore.Pedido
                objPedido.IDEmpresa = GLOBAL_Corporativo
                objPedido.FCompromiso = _FCompromiso
                objPedido.IDZona = _Celula
                objPedido.EstatusBoletin = "BOLETIN"
                objPedido.PedidoReferencia = _PedidoReferencia
                objPedido = ConsultarDetallePedidoCRM(objPedido)
                lblCliente.Text = objPedido.DireccionEntrega.IDDireccionEntrega
                lblNombre.Text = objPedido.DireccionEntrega.Nombre
                lblTelCasa.Text = objPedido.DireccionEntrega.Telefono1

                lblObservacionesPedido.Text = CType(lvwBoletin.FocusedItem.SubItems(18).Text, String).Trim
                If chkPortatil.Checked Then
                    SeleccionCalleColonia.CargaDatosClientePortatilSoloLectura(_Cliente)
                Else
                    SeleccionCalleColonia.CargaDatosClienteSoloLectura(_Cliente)
                End If
                If Not grpDatos.Visible Then grpDatos.Visible = True

                txtLlamadaObservaciones.Text = ""
                If chkPortatil.Checked Then
                    dt = Main.LlamadasClientePortatil(_Cliente)
                Else
                    dt = Main.LlamadasCliente(_Cliente)
                End If
                If Not IsNothing(dt) Then
                    grdLlamada.DataSource = dt
                    If dt.Rows.Count > 0 Then
                        grdLlamada.Select(0)
                        grdLlamada.CurrentRowIndex = 0
                        MuestraObservacionesLLamada()
                    End If
                Else
                    grdLlamada.DataSource = Nothing
                End If

                btnLlamadaOperador.Enabled = True
                If chkPortatil.Checked Then
                    Select Case cboStatusBoletin.SelectedIndex
                        Case 0
                            btnConfirmacionCliente.Enabled = True
                            btnCambioCompromiso.Enabled = True
                            btnConfirmaSuministro.Enabled = True
                            btnLlamadaOperador.Enabled = True
                            If GLOBAL_VersionMovilGas = 1 Then
                                If CType(lvwBoletin.FocusedItem.SubItems(20).Text, Integer) <> 0 Then
                                    btnConfirmaSuministro.Enabled = False
                                    'btnConfirmacionCliente.Enabled = False
                                End If
                            End If
                        Case 1
                            btnCambioCompromiso.Enabled = False
                            btnLlamadaOperador.Enabled = True
                            btnConfirmaSuministro.Enabled = True
                            btnConfirmacionCliente.Enabled = False
                            If GLOBAL_VersionMovilGas = 1 Then
                                If CType(lvwBoletin.FocusedItem.SubItems(20).Text, Integer) <> 0 Then
                                    btnConfirmaSuministro.Enabled = False
                                End If
                            End If
                        Case 2
                            btnConfirmacionCliente.Enabled = False
                            btnCambioCompromiso.Enabled = False
                            btnLlamadaOperador.Enabled = False
                            btnConfirmaSuministro.Enabled = False
                        Case 3
                            btnConfirmacionCliente.Enabled = False
                            btnCambioCompromiso.Enabled = False
                            btnLlamadaOperador.Enabled = False
                            btnConfirmaSuministro.Enabled = False
                    End Select
                    ''LUSATE Si el pedido se envió a Movíl Gas no permite darlo por atendido desde Sigamet
                    If GLOBAL_VersionMovilGas = 1 Then
                        btnActualizaStatusMG.Enabled = True
                        btnActualizaStatusMG.Visible = True

                    End If
                    ''
                Else
                    If cboStatusBoletin.SelectedIndex = 0 Then
                        btnConfirmacionCliente.Enabled = True
                        btnCambioCompromiso.Enabled = True
                    Else
                        btnConfirmacionCliente.Enabled = False
                        btnCambioCompromiso.Enabled = False
                    End If
                    btnActualizaStatusMG.Enabled = False
                End If

                btnConsultaCliente.Enabled = True

                If Not chkPortatil.Checked Then
                    If _LlevaLaNota = "SI" Then
                        Me.btnCambioCompromiso.Enabled = False
                    Else
                        Me.btnCambioCompromiso.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            LimpiaVariables()
            btnLlamadaOperador.Enabled = False
            btnConfirmacionCliente.Enabled = False
            btnCambioCompromiso.Enabled = False
            btnConsultaCliente.Enabled = False
            btnActualizaStatusMG.Enabled = False
            btnConfirmaSuministro.Enabled = True
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

#End Region

    Private Sub MuestraObservacionesLLamada()
        If Not IsDBNull(grdLlamada.Item(grdLlamada.CurrentRowIndex, 3)) Then
            txtLlamadaObservaciones.Text = CType(grdLlamada.Item(grdLlamada.CurrentRowIndex, 3), String).Trim
        Else
            txtLlamadaObservaciones.Text = ""
        End If

    End Sub
    Private Sub grdLlamada_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLlamada.CurrentCellChanged
        grdLlamada.Select(grdLlamada.CurrentRowIndex)
        MuestraObservacionesLLamada()

    End Sub
    Private Sub frmBoletin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '***CARGA DEL NOMBRE DE LA EMPRESA***
        Try
            LabelNombreEmpresa1.CargarNombreEmpresa()
        Catch
        End Try
        '************************************

        If GLOBAL_ConfirmaBoletinGrupo Then
            Me.lvwBoletin.MultiSelect = False
        End If


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
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error al cargar parametros de boletin " & ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        CierraConexion()

        cboUsuarioCelula.DisplayMember = "Descripcion"
        cboUsuarioCelula.ValueMember = "Celula"
        cboUsuarioCelula.DataSource = dtCelula
        'texis
        DtCel = dtCelula
        'fin solo la linea de arriba texis

        If cboUsuarioCelula.Items.Count > 0 Then
            cboUsuarioCelula.SelectedIndex = 0
        End If

        'cboRuta.CargaDatos(Celula:=Main.GLOBAL_Celula)

        'TODO: Filtro de ruta por portatil y estaconario
        cboRuta.CargaDatos(Celula:=GLOBAL_Celula, ActivarFiltro:=GLOBAL_ManejarClientesPortatil,
                MostrarPortatil:=chkPortatil.Checked)
        If cboRuta.Items.Count > 0 Then
            cboRuta.SelectedIndex = 0
        End If

        cboStatusBoletin.SelectedIndex = 0

        CargaBoletin()
        _DatosCargados = True

        'Integración de funcionalidades de portatil desarrolladas por transforma 25012012
        btnCarga.Enabled = oSeguridad.TieneAcceso("RealizarCargarPedidos")
        btnEnviarMensaje.Enabled = oSeguridad.TieneAcceso("EnviarMensajeAtt")
    End Sub
    Private Sub cboStatusBoletin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboStatusBoletin.SelectedIndexChanged
        If _DatosCargados Then
            CargaBoletin()
        End If
    End Sub
    Private Sub cboRuta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRuta.SelectedIndexChanged
        If _DatosCargados Then
            CargaBoletin()
        End If
    End Sub
    Private Sub chkTodasLasRutas_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTodasLasRutas.CheckedChanged
        'lblChofer.Text = String.Empty
        If _DatosCargados Then
            cboRuta.Enabled = Not chkTodasLasRutas.Checked
            CargaBoletin()
        End If
    End Sub


    Private Sub lvwBoletin_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwBoletin.DoubleClick
        Cursor = Cursors.WaitCursor
        Dim oCallCenter As New frmCallCenter(_Cliente, chkPortatil.Checked)
        oCallCenter.Show()
        Cursor = Cursors.Default

    End Sub

    Private Sub habilitaCambioCompromiso()
        If Not GLOBAL_AplicaCambioFechaCompromiso Then
            Me.tbBarra.Buttons.Remove(btnCambioCompromiso)
        End If
    End Sub

    Private Sub habilitaReasignacionPedidos()
        If Not Global_MuestraReasignacionPedidos Then
            Me.tbBarra.Buttons.Remove(btnReasignar)
        End If
    End Sub

#Region "Funcionalidad de portatil"

#Region "Barra de botones"

    Private Sub CambioCompromisoPortatil()
        Cursor = Cursors.WaitCursor
        Dim frmPedido As New frmPedidoPortatil(_Cliente, _Pedido, True)
        frmPedido.ShowDialog()
        frmPedido.Dispose()
        CargaBoletinPortatil()
        Cursor = Cursors.Default
    End Sub
    Private Sub CargaBoletinPortatil(Optional ByVal URLGateway As String = Nothing)

        Dim objPedido As New RTGMCore.Pedido
        Dim cliente As New Integer
        Dim oGateway As New RTGMGateway.RTGMGateway(SigaMetClasses.GLOBAL_Modulo, GLOBAL_ConString)
        Dim SolicitudPedidoGateway As RTGMGateway.SolicitudPedidoGateway
        Dim objPedidoGateway As New RTGMGateway.RTGMPedidoGateway
        Dim FechaDtp As Date

        Cursor = Cursors.WaitCursor
        btnLlamadaOperador.Enabled = False
        btnConfirmacionCliente.Enabled = False
        btnCambioCompromiso.Enabled = False
        btnConsultaCliente.Enabled = False
        btnConfirmaSuministro.Enabled = False
        btnActualizaStatusMG.Enabled = False

        LimpiaVariables()

        grpDatos.Visible = False

        lvwBoletin.Items.Clear()
        grdLlamada.DataSource = Nothing
        txtLlamadaObservaciones.Text = ""

        FechaDtp = dtpFecha.Value.Date
        Dim _CelulaCarga As Byte
        If CType(cboUsuarioCelula.SelectedValue, Byte) <> 0 Then
            _CelulaCarga = CType(cboUsuarioCelula.SelectedValue, Byte)
        Else
            _CelulaCarga = Main.GLOBAL_Celula
        End If

        If Not (_URLGateway Is String.Empty Or _URLGateway Is Nothing) Then
            oGateway = New RTGMGateway.RTGMGateway(SigaMetClasses.GLOBAL_Modulo, GLOBAL_ConString)
            oGateway.URLServicio = _URLGateway

            SolicitudPedidoGateway.FechaCompromisoInicio = FechaDtp
            SolicitudPedidoGateway.IDZona = _CelulaCarga
            SolicitudPedidoGateway.EstatusBoletin = "BOLETIN"
            'SolicitudPedidoGateway.FuenteDatos = RTGMCore.Fuente.Sigamet
            Dim ListaPedidos As List(Of RTGMCore.Pedido)
            objPedidoGateway.URLServicio = _URLGateway
            ListaPedidos = objPedidoGateway.buscarPedidos(SolicitudPedidoGateway)
            'Consulta los pedidos que vienen como respuesta del Web Service 
            ConsultaClientesBoletinCRM(ListaPedidos)

            Cursor = Cursors.Default

        Else
            Dim cmd As New SqlCommand("spCCConsultaBoletinPortatil", CnnSigamet)

            With cmd
                .CommandTimeout = 90
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@Celula", SqlDbType.TinyInt).Value = _CelulaCarga
                .Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFecha.Value.Date
                .Parameters.Add("@Status", SqlDbType.VarChar).Value = cboStatusBoletin.Text
                If Not chkTodasLasRutas.Checked Then
                    .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = cboRuta.Ruta
                End If
            End With

            Dim dr As SqlDataReader

            Try
                AbreConexion()

                Try
                    dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                Catch ex As Exception
                    MessageBox.Show("La base de datos está ocupada.  Por favor intente de nuevo.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CierraConexion()
                    Cursor = Cursors.Default
                    Exit Sub
                End Try

                Dim ixc As Integer
                Dim showMGStatus As Boolean

                'If GLOBAL_UsarMobileGas Then
                If lvwBoletin.Columns.Contains(colEstadoSGC) Then
                    lvwBoletin.Columns.Remove(colEstadoSGC)
                End If

                If Not lvwBoletin.Columns.Contains(colEstadoMG) Then
                    For ixc = 0 To dr.FieldCount - 1
                        If dr.GetName(ixc).Equals("StatusMG") Then
                            lvwBoletin.Columns.Add(colPedidoMG)
                            lvwBoletin.Columns.Add(colAutotanqueMG)
                            lvwBoletin.Columns.Add(colEstadoMG)
                            lvwBoletin.Columns.Add(colFStatusMG)
                            showMGStatus = True
                            Exit For
                        End If
                    Next
                Else
                    showMGStatus = True
                End If
                'End If

                Dim oItem As ListViewItem

                While dr.Read
                    oItem = New ListViewItem(CType(dr("PedidoReferencia"), String).Trim, 6) '0
                    oItem.SubItems.Add(CType(dr("AñoPed"), Short).ToString) '1
                    oItem.SubItems.Add(CType(dr("Celula"), Byte).ToString) '2
                    oItem.SubItems.Add(CType(dr("Pedido"), Integer).ToString) '3
                    oItem.SubItems.Add(CType(dr("Ruta"), Short).ToString) '4
                    oItem.SubItems.Add(CType(dr("RutaDescripcion"), String).Trim) '5
                    oItem.SubItems.Add(CType(dr("RutaBoletin"), Short).ToString) '6
                    oItem.SubItems.Add(CType(dr("RutaDescripcionBoletin"), String).Trim) '7
                    oItem.SubItems.Add(CType(dr("FAlta"), Date).ToString) '8
                    oItem.SubItems.Add(CType(dr("FCompromiso"), Date).ToShortDateString) '9
                    oItem.SubItems.Add(CType(dr("Cliente"), String)) '10
                    oItem.SubItems.Add(CType(dr("Nombre"), String).Trim) '11
                    oItem.SubItems.Add(CType(dr("DireccionCompleta"), String).Trim) '12
                    oItem.SubItems.Add(CType(dr("PrioridadPedidoDescripcion"), String).Trim) '13
                    oItem.SubItems.Add(CType(dr("Usuario"), String).Trim) '14
                    oItem.SubItems.Add(CType(dr("StatusBoletin"), String).Trim) '15
                    oItem.SubItems.Add(CType(dr("Insistente"), String).Trim) '16
                    oItem.SubItems.Add(CType(dr("TelCasa"), String).Trim) '17
                    oItem.SubItems.Add(CType(dr("Observaciones"), String).Trim) '18

                    If CType(dr("RutaBoletin"), Short) = 0 Then '19
                        oItem.SubItems.Add(CType(dr("ReporteRAF"), String))
                    Else
                        oItem.SubItems.Add(CType(dr("ReporteRAFBoletin"), String))
                    End If

                    oItem.SubItems.Add(CType(dr("PedidoMG"), Integer).ToString) '20
                    oItem.SubItems.Add(CType(dr("AutotanqueMG"), Integer).ToString) '21

                    If showMGStatus Then
                        oItem.SubItems.Add(CType(IIf(dr("StatusMG") Is DBNull.Value, String.Empty, dr("StatusMG")), String).Trim.ToUpper) '22
                        oItem.SubItems.Add(CType(IIf(dr("FStatusMG") Is DBNull.Value, String.Empty, dr("FStatusMG")), String).Trim.ToUpper) '23
                    End If

                    lvwBoletin.Items.Add(oItem)
                End While

                'If Not Global_MuestraRutaBoletin Then
                '    lvwBoletin.Columns(6).Width = 0
                '    lvwBoletin.Columns(7).Width = 0
                'End If

                lblBoletines.Text = "Boletines de: "
                If chkTodasLasRutas.Checked Then
                    lblBoletines.Text &= cboUsuarioCelula.Text.Trim
                Else
                    lblBoletines.Text &= cboRuta.Descripcion
                End If
                lblBoletines.Text &= " con el estatus " & Me.cboStatusBoletin.Text.Trim & " (" & lvwBoletin.Items.Count.ToString & " en total)"

            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
                cmd.Dispose()
                Cursor = Cursors.Default
            End Try

            If GLOBAL_VersionMovilGas = 1 And cboStatusBoletin.Text = "BOLETIN" Then
                ConsultaPedidosPortatilCanceladosSigamet()
            End If


        End If






    End Sub
    Private Sub LlamadaPortatil(ByVal TipoLlamada As enumTipoLlamada)
        Cursor = Cursors.WaitCursor
        Dim frmLlamada As New Llamada()
        frmLlamada.Entrada(_Cliente, _Nombre, _Celula, _Pedido, lblTelCasa.Text, _Ruta, _AñoPed, TipoLlamada, _FCompromiso, True, _FAlta)

        'LUSATE se traslada esta parte del código al formulario Llamada para registro de campos adicionales
        'If frmLlamada.DialogResult = DialogResult.OK AndAlso (TipoLlamada = enumTipoLlamada.Cliente Or TipoLlamada = enumTipoLlamada.Suministro) Then
        '    Dim cmdBoletin As New SqlCommand("exec spCCPedidoPortatilStatus @Pedido, @Status", CnnSigamet)
        '    cmdBoletin.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
        '    If TipoLlamada = enumTipoLlamada.Cliente Then
        '        cmdBoletin.Parameters.Add("@Status", SqlDbType.Char).Value = "RADIADO"
        '    Else
        '        cmdBoletin.Parameters.Add("@Status", SqlDbType.Char).Value = "ATENDIDO"
        '    End If
        '    Try
        '        AbreConexion()
        '        cmdBoletin.ExecuteNonQuery()
        '    Catch ex As Exception
        '        MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Finally
        '        CierraConexion()
        '    End Try
        'End If

        If frmLlamada.DialogResult = DialogResult.OK AndAlso (TipoLlamada = enumTipoLlamada.Suministro) Then
            Dim cmdBoletin As New SqlCommand("exec spCCPedidoPortatilStatus @Pedido, @Status", CnnSigamet)
            cmdBoletin.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
            cmdBoletin.Parameters.Add("@Status", SqlDbType.Char).Value = "ATENDIDO"
            Try
                AbreConexion()
                cmdBoletin.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
            End Try
        End If

        frmLlamada.Dispose()



        'TODO Debería validar si sí se dió de alta la llamada o no
        CargaBoletinPortatil()

        Cursor = Cursors.Default
    End Sub

    Private Sub ConfirmacionClientePortatil(ByVal Tipo As enumTipoLlamada)
        Dim TotalSeleccionados As Integer
        Dim oItem As ListViewItem
        Dim strMensaje As String = Nothing

        For Each oItem In lvwBoletin.Items
            If oItem.Selected Then
                strMensaje &= "Ruta: " & oItem.SubItems(4).Text & " Cliente: " & oItem.SubItems(6).Text & " " & oItem.SubItems(7).Text & " Dirección: " & oItem.SubItems(8).Text & Chr(13)
                TotalSeleccionados += 1
            End If
        Next

        If TotalSeleccionados <= 1 Then
            Llamada(Tipo)
        Else

            If MessageBox.Show("¿Desea realizar la confirmación en grupo de los siguientes pedidos? " & Chr(13) & Chr(13) & strMensaje, _Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
                Me.Cursor = Cursors.WaitCursor

                Me.Refresh()

                Dim oEspere As New frmBoletinEspere()
                Me.AddOwnedForm(oEspere)

                oEspere.Show()
                oEspere.Refresh()

                Dim Transaccion As SqlTransaction
                Dim cmdInsert As New SqlCommand("spCCPedidoPortatilStatus")
                cmdInsert.Connection = CnnSigamet
                cmdInsert.CommandTimeout = 100
                cmdInsert.CommandType = CommandType.StoredProcedure

                Try
                    AbreConexion()
                Catch
                    MessageBox.Show(SigaMetClasses.M_NO_CONEXION, _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub

                End Try


                Transaccion = CnnSigamet.BeginTransaction
                cmdInsert.Transaction = Transaccion

                Try

                    For Each oItem In lvwBoletin.Items
                        If oItem.Selected Then

                            Dim Pedido As Integer = CType(oItem.SubItems(3).Text, Integer)

                            cmdInsert.Parameters.Clear()
                            cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                            If Tipo = enumTipoLlamada.Cliente Then
                                cmdInsert.Parameters.Add("@Status", SqlDbType.Text).Value = "RADIADO"
                            Else
                                cmdInsert.Parameters.Add("@Status", SqlDbType.Text).Value = "ATENDIDO"
                            End If
                            cmdInsert.ExecuteNonQuery()

                        End If
                    Next

                    Transaccion.Commit()

                Catch ex As Exception
                    Transaccion.Rollback()
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

                Finally
                    CierraConexion()
                    Cursor = Cursors.Default
                    Me.RemoveOwnedForm(oEspere)
                    oEspere.Close()
                    cmdInsert.Dispose()
                    CargaBoletin()

                End Try
            End If

        End If

    End Sub
#End Region
    Private Sub chkPortatil_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPortatil.CheckedChanged
        LimpiaVariables()
        'lblFecha.Visible = chkPortatil.Checked
        'dtpFecha.Visible = chkPortatil.Checked
        cboStatusBoletin.Items.Clear()
        btnConfirmaSuministro.Visible = chkPortatil.Checked
        btnConsultaCliente.Visible = Not chkPortatil.Checked
        'TODO: Filtro de rutas estacionario y portátil
        cboRuta.CargaDatos(Celula:=GLOBAL_Celula, aCTIVARFiltrO:=GLOBAL_ManejarClientesPortatil, _
                                    mOSTRARPORTATIL:=chkPortatil.Checked)
        If chkPortatil.Checked Then
            CargaBoletinPortatil()
            cboStatusBoletin.Items.Add("BOLETIN")
            cboStatusBoletin.Items.Add("BOLETINADO")
            cboStatusBoletin.Items.Add("SURTIDO")
            cboStatusBoletin.Items.Add("TODOS")
            'TODO: Cambio de status de boletines portatil para homologación estacionario
            '20/11/2004 JAG
            'cboStatusBoletin.Items.Add("ACTIVO")
            'cboStatusBoletin.Items.Add("RADIADO")
            'cboStatusBoletin.Items.Add("ATENDIDO")
            'cboStatusBoletin.Items.Add("TODOS")
        Else
            LimpiaVariables()
            cboStatusBoletin.Items.Add("BOLETIN")
            cboStatusBoletin.Items.Add("BOLETINADO")
            btnActualizaStatusMG.Enabled = False
            btnActualizaStatusMG.Visible = False
        End If
        cboStatusBoletin.SelectedIndex = 0
    End Sub
    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        CargaBoletin()
    End Sub

    Dim InfoPedidoPortatil As InfoPedidoPortatil

    Private Sub lvwBoletin_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
    Handles lvwBoletin.MouseDown
        If e.Button = MouseButtons.Right Then
            If lvwBoletin.SelectedItems.Count > 0 AndAlso chkPortatil.Checked Then
                InfoPedidoPortatil = New InfoPedidoPortatil(_Cliente, _
                    CInt(lvwBoletin.SelectedItems(0).SubItems(0).Text))
                InfoPedidoPortatil.Location = Cursor.Position
                InfoPedidoPortatil.Show()
            End If
        End If
    End Sub

    Private Sub lvwPedido_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvwBoletin.MouseMove
        If Not InfoPedidoPortatil Is Nothing Then
            InfoPedidoPortatil.Dispose()
        End If
    End Sub

#End Region


    Private Sub cboUsuarioCelula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUsuarioCelula.SelectedIndexChanged
        If _DatosCargados Then
            cboRuta.CargaDatos(Celula:=CType(cboUsuarioCelula.SelectedValue, Byte))
            CargaBoletin()
        End If
    End Sub

    Private Sub lblBoletines_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblBoletines.Click

    End Sub

    Private Sub FinRuta()
        'Llamada para cierre de ruta
        Dim frmLlamada As New Llamadas.frmLlamada(40, _
            Convert.ToInt16(cboUsuarioCelula.SelectedValue), _
            Convert.ToInt16(IIf(chkTodasLasRutas.Checked = True, 0, cboRuta.Ruta)))        
        frmLlamada.ShowInTaskbar = False
        frmLlamada.StartPosition = FormStartPosition.CenterScreen        
        frmLlamada.ShowDialog()        
    End Sub

    'Function BoletinarPedido() As Boolean
    '    Dim resultado As Boolean = False
    '    Dim oItem As ListViewItem
    '    Dim strMensaje As String
    '    Dim seleccion As String = String.Empty

    '    Dim pedidoReferencia As String
    '    Dim rutaPedido As Short

    '    For Each oItem In lvwBoletin.Items
    '        If oItem.Selected Then
    '            'strMensaje &= "Ruta: " & oItem.SubItems(4).Text & " Cliente: " & oItem.SubItems(6).Text & " " & oItem.SubItems(7).Text & " Dirección: " & oItem.SubItems(10).Text & Chr(13)
    '            pedidoReferencia = oItem.SubItems(0).Text.Trim()
    '            rutaPedido = Convert.ToInt16(oItem.SubItems(4).Text.Trim())

    '            strMensaje &= "Ruta: " & rutaPedido.ToString() & " Pedido: " & pedidoReferencia & Chr(13) & _
    '                " Cliente: " & oItem.SubItems(8).Text & " " & oItem.SubItems(9).Text & _
    '                " Dirección: " & oItem.SubItems(10).Text & Chr(13)
    '        End If
    '    Next

    '    MessageBox.Show(strMensaje)

    '    Dim servicioPedido As New desarrollogm.Pedido()

    '    'TODO: Revisar de cuándo debe ser la fecha de la asignación de los autotanques, del día de la consulta o del día de salida del
    '    'camión
    '    Me.Cursor = Cursors.WaitCursor
    '    Dim dtAutotanquesDia As DataTable = servicioPedido.ConsultaAutotanquesPorRutaYDia(Main.GLOBAL_Estacion, _
    '        rutaPedido, dtpFecha.Value.Date, DateTime.Now).Tables(0)
    '    Me.Cursor = Cursors.Default

    '    If dtAutotanquesDia.Rows.Count > 0 Then
    '        If dtAutotanquesDia.Rows.Count = 1 Then
    '            Dim valor As String = CType(dtAutotanquesDia.Rows(0).Item(0), String)
    '            Dim frmLlamada As New Llamada(pedidoReferencia, CType(dtAutotanquesDia.Rows(0).Item("Autotanque"), Int32), CType(dtAutotanquesDia.Rows(0).Item("NombrePlantaSGC"), String), True)
    '            frmLlamada.Entrada(_Cliente, _Nombre, _Celula, _Pedido, lblTelCasa.Text, _Ruta, _AñoPed, 2)

    '            If (frmLlamada.ShowDialog() = DialogResult.OK) Then
    '                MessageBox.Show("El pedido " + seleccion + " ha sido boletinado exitosamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                resultado = True
    '            Else
    '                MessageBox.Show("El pedido " + seleccion + " No ha sido boletinado exitosamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If
    '        Else
    '            Dim formSeleccionAutotanque As New frmSeleccionAutotanque(dtAutotanquesDia)
    '            formSeleccionAutotanque.ShowDialog()
    '            seleccion = CType(formSeleccionAutotanque.Tag(), String)
    '            Dim frmLlamada As New Llamada(pedidoReferencia, CType(dtAutotanquesDia.Rows(0).Item("Autotanque"), Int32), CType(dtAutotanquesDia.Rows(0).Item("NombrePlantaSGC"), String), True)
    '            frmLlamada.Entrada(_Cliente, _Nombre, _Celula, _Pedido, lblTelCasa.Text, _Ruta, _AñoPed, 2)
    '            If (frmLlamada.ShowDialog() = DialogResult.OK) Then
    '                MessageBox.Show("El pedido " + seleccion + " ha sido boletinado exitosamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                resultado = True
    '            Else
    '                MessageBox.Show("El pedido " + seleccion + " No ha sido boletinado exitosamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If
    '        End If
    '    Else
    '        MessageBox.Show("No hay autotanques asignados en la ruta seleccionada.")
    '    End If
    '    Return resultado
    'End Function
    Private Sub ActualizarPedidosMG()
        ''LUSATE

        'If GLOBAL_UsarMobileGas And _Autotanque Then
        If GLOBAL_VersionMovilGas = 1 And _Rutaboletin Then
            Try
                Cursor = Cursors.WaitCursor
                Dim RutinasMG As New MobileGas.MobileGas(GLOBAL_ConString, GLOBAL_MGConnectionString)
                'If RutinasMG.UsaBoletinAutotanque(_Autotanque) Then
                Dim Actualizacion As String
                Actualizacion = RutinasMG.ActualizaEstatusPedidosMG(dtpFecha.Value, 0, _Ruta)
                MessageBox.Show(Actualizacion, "Actualización de pedidos Móvil Gas", MessageBoxButtons.OK, MessageBoxIcon.Information)
                'End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Cursor = Cursors.Default
            End Try
        Else
            MessageBox.Show("Para actualizar los pedidos de Móvil Gas en necesario que el pedido esté boletinado ó surtido", "Móvil Gas", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        CargaBoletin()
    End Sub

    Private Sub ConsultaPedidosPortatilCanceladosSigamet()
        Dim _CelulaCarga As Byte
        If CType(cboUsuarioCelula.SelectedValue, Byte) <> 0 Then
            _CelulaCarga = CType(cboUsuarioCelula.SelectedValue, Byte)
        Else
            _CelulaCarga = Main.GLOBAL_Celula
        End If

        Dim cmd As New SqlCommand("spCCConsultaBoletinPortatil", CnnSigamet)

        With cmd
            .CommandTimeout = 90
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Celula", SqlDbType.TinyInt).Value = _CelulaCarga
            .Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFecha.Value.Date
            .Parameters.Add("@Status", SqlDbType.VarChar).Value = "CANCELADO"
            .Parameters.Add("@CanceladosSigamet", SqlDbType.Bit).Value = 1
            If Not chkTodasLasRutas.Checked Then
                .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = cboRuta.Ruta
            End If
        End With

        Dim dr As SqlDataReader

        Try
            AbreConexion()

            Try
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            Catch ex As Exception
                MessageBox.Show("La base de datos está ocupada.  Por favor intente de nuevo.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CierraConexion()
                Cursor = Cursors.Default
                Exit Sub
            End Try


            Dim oItem As ListViewItem

            While dr.Read
                oItem = New ListViewItem(CType(dr("PedidoReferencia"), String).Trim, 6)
                oItem.SubItems.Add(CType(dr("AñoPed"), Short).ToString)
                oItem.SubItems.Add(CType(dr("Celula"), Byte).ToString)
                oItem.SubItems.Add(CType(dr("Pedido"), Integer).ToString)
                oItem.SubItems.Add(CType(dr("Ruta"), Short).ToString)
                oItem.SubItems.Add(CType(dr("RutaDescripcion"), String).Trim)
                oItem.SubItems.Add(CType(dr("RutaBoletin"), Short).ToString) '
                oItem.SubItems.Add(CType(dr("RutaDescripcionBoletin"), String).Trim) '
                oItem.SubItems.Add(CType(dr("FAlta"), Date).ToString)
                oItem.SubItems.Add(CType(dr("FCompromiso"), Date).ToShortDateString)
                oItem.SubItems.Add(CType(dr("Cliente"), String))
                oItem.SubItems.Add(CType(dr("Nombre"), String).Trim)
                oItem.SubItems.Add(CType(dr("DireccionCompleta"), String).Trim)
                oItem.SubItems.Add(CType(dr("PrioridadPedidoDescripcion"), String).Trim)
                oItem.SubItems.Add(CType(dr("Usuario"), String).Trim)
                oItem.SubItems.Add(CType(dr("StatusBoletin"), String).Trim)
                oItem.SubItems.Add(CType(dr("Insistente"), String).Trim)
                oItem.SubItems.Add(CType(dr("TelCasa"), String).Trim)
                oItem.SubItems.Add(CType(dr("Observaciones"), String).Trim)


                If CType(dr("RutaBoletin"), Short) = 0 Then
                    oItem.SubItems.Add(CType(dr("ReporteRAF"), String))
                Else
                    oItem.SubItems.Add(CType(dr("ReporteRAFBoletin"), String))
                End If


                oItem.SubItems.Add(CType(dr("PedidoMG"), Integer).ToString)
                oItem.SubItems.Add(CType(dr("AutotanqueMG"), Integer).ToString)
                oItem.SubItems.Add(CType(IIf(dr("StatusMG") Is DBNull.Value, String.Empty, dr("StatusMG")), String).Trim.ToUpper) '17
                oItem.SubItems.Add(CType(IIf(dr("FStatusMG") Is DBNull.Value, String.Empty, dr("FStatusMG")), String).Trim.ToUpper)
                lvwBoletin.Items.Add(oItem)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            cmd.Dispose()
            Cursor = Cursors.Default

        End Try
    End Sub

    Private Sub ReasignarPedidosBoletinados()
        Try           
            Me.Cursor = Cursors.WaitCursor            
            Dim frmReasignacion As New ReasignacionPedidos.frmReasignacion(GLOBAL_Estacion, GLOBAL_URLWebserviceBoletin, Nothing, Nothing, Nothing)
            Me.Cursor = Cursors.Default
            frmReasignacion.ShowDialog()
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error:" & vbCrLf & ex.Message & vbCrLf & _
               ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

End Class
