'Desarrolladores de cambios para generar y procesar archivo de BESTRAC
'CFSL          Carlos Francisco Sánchez Lavariega   --MODULO DE GENERACION
'CAGP          Claudia Aurora García Patiño         --MODULO DE PROCESAR
'A Linea agregada
'C Linea cambiada
'D Linea borrada

'Variables de cambios
'$A1            CAGP    18/11/2004  Se agrego a la tabla de Rampac la columna DESCUENTOFIJO por lo
'                                   mismo se agregará el siguiente codigo para guardar esta información
'$B1            CFSL    16/11/2004  Se agrego la variable global de Precios
'$C1            CFSL    16/11/2004  Se agrego la variable global de Clientes a atender
'$D1            CFSL    16/11/2004  Se agrego la variable global para determinar el tipo de descuento

'Procedimientos
'#P1            CFSL    16/11/2004
'#P2            CFSL    16/11/2004

'Modificación del 26-NOVIEMBRE-2004

'*********************************
'La referencia a la clase comboruta  se cambió hacia la clase comboRutaBoletin, que permite desplegar solo las
'rutas de gas estacionario.
'JAGD

Imports System.Data.SqlClient
Imports System.IO

Public Class frmInterfaseCorporativo
    Inherits System.Windows.Forms.Form

    Private RutaArchivosCarga As String
    Private RutaArchivosDescarga As String
    Private dsDatos As DataSet
    Private Titulo As String = "Interfase Rampac"
    Private ListaPedidos As ArrayList
    Private _AnoAtt As Short
    Private _Folio As Integer
    Public _Ruta As Short

    'Modificación para liquidar varias células MHV 19-11-2004
    Public _Celula As Short

    Private _Autotanque As Short
    Private _FInicioRutaRampac As Date
    Private _FTerminoRutaRampac As Date
    Private _FPrimerSurtidoRampac As Date
    Private _FUltimoSurtidoRampac As Date
    Private _TotalizadorInicialRampac As Integer
    Private _TotalizadorFinalRampac As Integer
    Private EncabezadoCorrupto As Boolean

    Private _DiadeHoy As DateTime

    Public _Fecha As DateTime
    Public _FolioP As Integer
    Public _Anioatt As Integer
    Public _Acepto As Boolean
    Public _Descarga As Boolean
    Public _VistaForma As Integer

    Private dsPrecios As DataSet            '$B1-A
    Private dsClientes As DataSet           '$C1-A
    Private DescuentoDirecto As Boolean     '$D1-A


    Public Sub Entrada(ByVal Vista As Integer)
        _VistaForma = Vista

        If Vista = 0 Then
            tabRampac.TabPages(0).Visible = True
            tabRampac.TabPages(1).Visible = False
            tpDescarga.Enabled = False
            tpCarga.Visible = False
            tpCarga.Show()
            tpDescarga.Dispose()
        End If

        If Vista = 1 Then
            tabRampac.TabPages(0).Visible = False
            tabRampac.TabPages(1).Visible = True
            tpCarga.Enabled = False
            tpDescarga.Visible = True
            tpDescarga.Show()
            tpCarga.Dispose()
        End If

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
    Friend WithEvents File1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents stbEstatus As System.Windows.Forms.StatusBar
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnConsultarDescarga As System.Windows.Forms.Button
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents btnConsultarCarga As System.Windows.Forms.Button
    Friend WithEvents grdPedidos As System.Windows.Forms.DataGrid
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboRuta As SigaMetClasses.Combos.ComboRutaBoletin
    'Friend WithEvents ComboRuta As SigaMetClasses.Combos.ComboRuta
    Friend WithEvents ComboCelula As SigaMetClasses.Combos.ComboCelula
    Friend WithEvents lblImporteTotal As System.Windows.Forms.Label
    Friend WithEvents lblLitrosTotal As System.Windows.Forms.Label
    Friend WithEvents lblLitrosCredito As System.Windows.Forms.Label
    Friend WithEvents lblImporteCredito As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblLitrosContado As System.Windows.Forms.Label
    Friend WithEvents lblImporteContado As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents tabRampac As System.Windows.Forms.TabControl
    Friend WithEvents tpCarga As System.Windows.Forms.TabPage
    Friend WithEvents tpDescarga As System.Windows.Forms.TabPage
    Friend WithEvents lstDescarga As System.Windows.Forms.ListBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblAutotanque As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblTotalEntregas As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTitulos As System.Windows.Forms.Label
    Friend WithEvents lblArchivo As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblInicioDeRuta As System.Windows.Forms.Label
    Friend WithEvents lblFinDeRuta As System.Windows.Forms.Label
    Friend WithEvents lblTotInicial As System.Windows.Forms.Label
    Friend WithEvents lblTotFinal As System.Windows.Forms.Label
    Friend WithEvents mnuPrincipal As System.Windows.Forms.MainMenu
    Friend WithEvents mnuArchivo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAcercade As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSalir As System.Windows.Forms.MenuItem
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblTiempoTranscurrido As System.Windows.Forms.Label
    Friend WithEvents btnGenerarPedidos As System.Windows.Forms.Button
    Friend WithEvents ComboCelula1 As SigaMetClasses.Combos.ComboCelula
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ComboRuta1 As SigaMetClasses.Combos.ComboRutaBoletin
    'Friend WithEvents ComboRuta1 As SigaMetClasses.Combos.ComboRuta
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents grdAT As System.Windows.Forms.DataGrid
    Friend WithEvents lblFechaDescarga As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents EstiloAT As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents colAnoAtt As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colFolio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colAutotanque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colMarcaAutotanque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents EstiloCarga As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents colPedidoReferencia As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colNombre As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colStatusLogistica As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colDireccionCompleta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colTipoPedidoDescripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lblTotalProgramados As System.Windows.Forms.Label
    Friend WithEvents lblTotalTelefonicos As System.Windows.Forms.Label
    Friend WithEvents mnuConfiguracion As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAyuda As System.Windows.Forms.MenuItem
    Friend WithEvents lblTotalNB As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents lblTotalRemisiones As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblHoraPrimerSurtido As System.Windows.Forms.Label
    Friend WithEvents lblHoraUltimoSurtido As System.Windows.Forms.Label
    Friend WithEvents lblTiempoRealVenta As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents imgLista As System.Windows.Forms.ImageList
    Friend WithEvents lblTotalDuplicados As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmInterfaseCorporativo))
        Me.stbEstatus = New System.Windows.Forms.StatusBar()
        Me.File1 = New System.Windows.Forms.OpenFileDialog()
        Me.tabRampac = New System.Windows.Forms.TabControl()
        Me.tpCarga = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lblTotalProgramados = New System.Windows.Forms.Label()
        Me.lblTotalTelefonicos = New System.Windows.Forms.Label()
        Me.btnGenerar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnConsultarCarga = New System.Windows.Forms.Button()
        Me.grdPedidos = New System.Windows.Forms.DataGrid()
        Me.EstiloCarga = New System.Windows.Forms.DataGridTableStyle()
        Me.colTipoPedidoDescripcion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colPedidoReferencia = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colNombre = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colDireccionCompleta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboRuta = New SigaMetClasses.Combos.ComboRutaBoletin()
        Me.ComboCelula = New SigaMetClasses.Combos.ComboCelula()
        Me.tpDescarga = New System.Windows.Forms.TabPage()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.lblFechaDescarga = New System.Windows.Forms.Label()
        Me.grdAT = New System.Windows.Forms.DataGrid()
        Me.EstiloAT = New System.Windows.Forms.DataGridTableStyle()
        Me.colAnoAtt = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colFolio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colAutotanque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colMarcaAutotanque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colStatusLogistica = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.ComboRuta1 = New SigaMetClasses.Combos.ComboRutaBoletin()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ComboCelula1 = New SigaMetClasses.Combos.ComboCelula()
        Me.btnGenerarPedidos = New System.Windows.Forms.Button()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblTotalDuplicados = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lblTotalNB = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lblTotalRemisiones = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.lblTotalEntregas = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.lblTitulos = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblTiempoRealVenta = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.lblHoraUltimoSurtido = New System.Windows.Forms.Label()
        Me.lblHoraPrimerSurtido = New System.Windows.Forms.Label()
        Me.lblTiempoTranscurrido = New System.Windows.Forms.Label()
        Me.lblTotInicial = New System.Windows.Forms.Label()
        Me.lblTotFinal = New System.Windows.Forms.Label()
        Me.lblFinDeRuta = New System.Windows.Forms.Label()
        Me.lblArchivo = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.lblInicioDeRuta = New System.Windows.Forms.Label()
        Me.lblAutotanque = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblImporteTotal = New System.Windows.Forms.Label()
        Me.lblImporteCredito = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblImporteContado = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblLitrosTotal = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblLitrosCredito = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblLitrosContado = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lstDescarga = New System.Windows.Forms.ListBox()
        Me.btnConsultarDescarga = New System.Windows.Forms.Button()
        Me.mnuPrincipal = New System.Windows.Forms.MainMenu()
        Me.mnuArchivo = New System.Windows.Forms.MenuItem()
        Me.mnuConfiguracion = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuSalir = New System.Windows.Forms.MenuItem()
        Me.mnuAyuda = New System.Windows.Forms.MenuItem()
        Me.mnuAcercade = New System.Windows.Forms.MenuItem()
        Me.imgLista = New System.Windows.Forms.ImageList(Me.components)
        Me.tabRampac.SuspendLayout()
        Me.tpCarga.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDescarga.SuspendLayout()
        CType(Me.grdAT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'stbEstatus
        '
        Me.stbEstatus.Location = New System.Drawing.Point(0, 515)
        Me.stbEstatus.Name = "stbEstatus"
        Me.stbEstatus.Size = New System.Drawing.Size(704, 22)
        Me.stbEstatus.TabIndex = 10
        Me.stbEstatus.Text = "Especifique los datos para la generación de la interfase"
        '
        'tabRampac
        '
        Me.tabRampac.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.tabRampac.Controls.AddRange(New System.Windows.Forms.Control() {Me.tpCarga, Me.tpDescarga})
        Me.tabRampac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabRampac.HotTrack = True
        Me.tabRampac.Multiline = True
        Me.tabRampac.Name = "tabRampac"
        Me.tabRampac.SelectedIndex = 0
        Me.tabRampac.Size = New System.Drawing.Size(704, 515)
        Me.tabRampac.TabIndex = 15
        '
        'tpCarga
        '
        Me.tpCarga.BackColor = System.Drawing.Color.Gainsboro
        Me.tpCarga.Controls.AddRange(New System.Windows.Forms.Control() {Me.GroupBox1, Me.btnGenerar, Me.btnBuscar, Me.btnConsultarCarga, Me.grdPedidos, Me.Label3, Me.dtpFecha, Me.Label2, Me.Label1, Me.ComboRuta, Me.ComboCelula})
        Me.tpCarga.Location = New System.Drawing.Point(4, 4)
        Me.tpCarga.Name = "tpCarga"
        Me.tpCarga.Size = New System.Drawing.Size(696, 489)
        Me.tpCarga.TabIndex = 0
        Me.tpCarga.Text = "Carga"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label22, Me.Label23, Me.lblTotalProgramados, Me.lblTotalTelefonicos})
        Me.GroupBox1.Location = New System.Drawing.Point(80, 395)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(184, 80)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        '
        'Label22
        '
        Me.Label22.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(8, 41)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(120, 23)
        Me.Label22.TabIndex = 34
        Me.Label22.Text = "Total programados:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(8, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 23)
        Me.Label23.TabIndex = 33
        Me.Label23.Text = "Total telefónicos:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalProgramados
        '
        Me.lblTotalProgramados.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.lblTotalProgramados.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalProgramados.Location = New System.Drawing.Point(136, 40)
        Me.lblTotalProgramados.Name = "lblTotalProgramados"
        Me.lblTotalProgramados.Size = New System.Drawing.Size(32, 23)
        Me.lblTotalProgramados.TabIndex = 32
        Me.lblTotalProgramados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotalTelefonicos
        '
        Me.lblTotalTelefonicos.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.lblTotalTelefonicos.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTelefonicos.Location = New System.Drawing.Point(136, 16)
        Me.lblTotalTelefonicos.Name = "lblTotalTelefonicos"
        Me.lblTotalTelefonicos.Size = New System.Drawing.Size(32, 23)
        Me.lblTotalTelefonicos.TabIndex = 31
        Me.lblTotalTelefonicos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnGenerar
        '
        Me.btnGenerar.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.btnGenerar.BackColor = System.Drawing.SystemColors.Control
        Me.btnGenerar.Enabled = False
        Me.btnGenerar.Image = CType(resources.GetObject("btnGenerar.Image"), System.Drawing.Bitmap)
        Me.btnGenerar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerar.Location = New System.Drawing.Point(189, 352)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.TabIndex = 25
        Me.btnGenerar.Text = "&Generar"
        Me.btnGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBuscar
        '
        Me.btnBuscar.Anchor = (System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)
        Me.btnBuscar.BackColor = System.Drawing.SystemColors.Control
        Me.btnBuscar.Image = CType(resources.GetObject("btnBuscar.Image"), System.Drawing.Bitmap)
        Me.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBuscar.Location = New System.Drawing.Point(80, 352)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.TabIndex = 24
        Me.btnBuscar.Text = "&Buscar"
        Me.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnConsultarCarga
        '
        Me.btnConsultarCarga.BackColor = System.Drawing.SystemColors.Control
        Me.btnConsultarCarga.Image = CType(resources.GetObject("btnConsultarCarga.Image"), System.Drawing.Bitmap)
        Me.btnConsultarCarga.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConsultarCarga.Location = New System.Drawing.Point(80, 24)
        Me.btnConsultarCarga.Name = "btnConsultarCarga"
        Me.btnConsultarCarga.Size = New System.Drawing.Size(184, 23)
        Me.btnConsultarCarga.TabIndex = 23
        Me.btnConsultarCarga.Text = "Consultar carga..."
        '
        'grdPedidos
        '
        Me.grdPedidos.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.grdPedidos.BackgroundColor = System.Drawing.Color.Gainsboro
        Me.grdPedidos.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.grdPedidos.DataMember = ""
        Me.grdPedidos.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdPedidos.Location = New System.Drawing.Point(280, 24)
        Me.grdPedidos.Name = "grdPedidos"
        Me.grdPedidos.ReadOnly = True
        Me.grdPedidos.Size = New System.Drawing.Size(400, 451)
        Me.grdPedidos.TabIndex = 21
        Me.grdPedidos.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.EstiloCarga})
        '
        'EstiloCarga
        '
        Me.EstiloCarga.DataGrid = Me.grdPedidos
        Me.EstiloCarga.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colTipoPedidoDescripcion, Me.colPedidoReferencia, Me.colCliente, Me.colNombre, Me.colDireccionCompleta})
        Me.EstiloCarga.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.EstiloCarga.MappingName = "Carga"
        Me.EstiloCarga.ReadOnly = True
        '
        'colTipoPedidoDescripcion
        '
        Me.colTipoPedidoDescripcion.Format = ""
        Me.colTipoPedidoDescripcion.FormatInfo = Nothing
        Me.colTipoPedidoDescripcion.HeaderText = "Tipo pedido"
        Me.colTipoPedidoDescripcion.MappingName = "TipoPedidoDescripcion"
        Me.colTipoPedidoDescripcion.Width = 75
        '
        'colPedidoReferencia
        '
        Me.colPedidoReferencia.Format = ""
        Me.colPedidoReferencia.FormatInfo = Nothing
        Me.colPedidoReferencia.HeaderText = "Pedido"
        Me.colPedidoReferencia.MappingName = "PedidoReferencia"
        Me.colPedidoReferencia.Width = 0
        '
        'colCliente
        '
        Me.colCliente.Format = ""
        Me.colCliente.FormatInfo = Nothing
        Me.colCliente.HeaderText = "Cliente"
        Me.colCliente.MappingName = "Cliente"
        Me.colCliente.Width = 90
        '
        'colNombre
        '
        Me.colNombre.Format = ""
        Me.colNombre.FormatInfo = Nothing
        Me.colNombre.HeaderText = "Nombre"
        Me.colNombre.MappingName = "Nombre"
        Me.colNombre.Width = 200
        '
        'colDireccionCompleta
        '
        Me.colDireccionCompleta.Format = ""
        Me.colDireccionCompleta.FormatInfo = Nothing
        Me.colDireccionCompleta.HeaderText = "Dirección"
        Me.colDireccionCompleta.MappingName = "DireccionCompleta"
        Me.colDireccionCompleta.Width = 260
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(24, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 14)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Del día:"
        '
        'dtpFecha
        '
        Me.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFecha.Location = New System.Drawing.Point(80, 56)
        Me.dtpFecha.MaxDate = New Date(2010, 12, 31, 0, 0, 0, 0)
        Me.dtpFecha.MinDate = New Date(2002, 1, 1, 0, 0, 0, 0)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.Size = New System.Drawing.Size(184, 21)
        Me.dtpFecha.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 14)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Ruta:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 14)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Célula:"
        '
        'ComboRuta
        '
        Me.ComboRuta.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left)
        Me.ComboRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.ComboRuta.Location = New System.Drawing.Point(80, 104)
        Me.ComboRuta.Name = "ComboRuta"
        Me.ComboRuta.Size = New System.Drawing.Size(184, 208)
        Me.ComboRuta.TabIndex = 16
        '
        'ComboCelula
        '
        Me.ComboCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboCelula.ForeColor = System.Drawing.Color.MediumBlue
        Me.ComboCelula.Location = New System.Drawing.Point(80, 80)
        Me.ComboCelula.Name = "ComboCelula"
        Me.ComboCelula.Size = New System.Drawing.Size(184, 21)
        Me.ComboCelula.TabIndex = 15
        '
        'tpDescarga
        '
        Me.tpDescarga.BackColor = System.Drawing.Color.Gainsboro
        Me.tpDescarga.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label21, Me.lblFechaDescarga, Me.grdAT, Me.Label20, Me.ComboRuta1, Me.Label18, Me.ComboCelula1, Me.btnGenerarPedidos, Me.Panel4, Me.lblTitulos, Me.Panel3, Me.Panel2, Me.Panel1, Me.lstDescarga, Me.btnConsultarDescarga})
        Me.tpDescarga.Location = New System.Drawing.Point(4, 4)
        Me.tpDescarga.Name = "tpDescarga"
        Me.tpDescarga.Size = New System.Drawing.Size(696, 489)
        Me.tpDescarga.TabIndex = 1
        Me.tpDescarga.Text = "Lectura de la descarga"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(16, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(42, 14)
        Me.Label21.TabIndex = 45
        Me.Label21.Text = "Fecha:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFechaDescarga
        '
        Me.lblFechaDescarga.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFechaDescarga.Location = New System.Drawing.Point(72, 56)
        Me.lblFechaDescarga.Name = "lblFechaDescarga"
        Me.lblFechaDescarga.Size = New System.Drawing.Size(232, 20)
        Me.lblFechaDescarga.TabIndex = 44
        Me.lblFechaDescarga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'grdAT
        '
        Me.grdAT.AlternatingBackColor = System.Drawing.Color.DarkSeaGreen
        Me.grdAT.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.grdAT.BackgroundColor = System.Drawing.Color.Gainsboro
        Me.grdAT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdAT.CaptionBackColor = System.Drawing.Color.Olive
        Me.grdAT.DataMember = ""
        Me.grdAT.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdAT.Location = New System.Drawing.Point(312, 8)
        Me.grdAT.Name = "grdAT"
        Me.grdAT.ReadOnly = True
        Me.grdAT.Size = New System.Drawing.Size(376, 104)
        Me.grdAT.TabIndex = 43
        Me.grdAT.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.EstiloAT})
        '
        'EstiloAT
        '
        Me.EstiloAT.DataGrid = Me.grdAT
        Me.EstiloAT.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colAnoAtt, Me.colFolio, Me.colAutotanque, Me.colMarcaAutotanque, Me.colStatusLogistica})
        Me.EstiloAT.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.EstiloAT.MappingName = "ATRuta"
        '
        'colAnoAtt
        '
        Me.colAnoAtt.Format = ""
        Me.colAnoAtt.FormatInfo = Nothing
        Me.colAnoAtt.MappingName = "AñoAtt"
        Me.colAnoAtt.Width = 0
        '
        'colFolio
        '
        Me.colFolio.Format = ""
        Me.colFolio.FormatInfo = Nothing
        Me.colFolio.HeaderText = "Folio"
        Me.colFolio.MappingName = "Folio"
        Me.colFolio.Width = 75
        '
        'colAutotanque
        '
        Me.colAutotanque.Format = ""
        Me.colAutotanque.FormatInfo = Nothing
        Me.colAutotanque.HeaderText = "Autotanque"
        Me.colAutotanque.MappingName = "Autotanque"
        Me.colAutotanque.Width = 75
        '
        'colMarcaAutotanque
        '
        Me.colMarcaAutotanque.Format = ""
        Me.colMarcaAutotanque.FormatInfo = Nothing
        Me.colMarcaAutotanque.HeaderText = "Marca"
        Me.colMarcaAutotanque.MappingName = "MarcaAutotanque"
        Me.colMarcaAutotanque.Width = 95
        '
        'colStatusLogistica
        '
        Me.colStatusLogistica.Format = ""
        Me.colStatusLogistica.FormatInfo = Nothing
        Me.colStatusLogistica.HeaderText = "Estatus"
        Me.colStatusLogistica.MappingName = "StatusLogistica"
        Me.colStatusLogistica.Width = 75
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(16, 35)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(35, 14)
        Me.Label20.TabIndex = 42
        Me.Label20.Text = "Ruta:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ComboRuta1
        '
        Me.ComboRuta1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboRuta1.Location = New System.Drawing.Point(72, 32)
        Me.ComboRuta1.Name = "ComboRuta1"
        Me.ComboRuta1.Size = New System.Drawing.Size(232, 21)
        Me.ComboRuta1.TabIndex = 41
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(16, 11)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(43, 14)
        Me.Label18.TabIndex = 40
        Me.Label18.Text = "Célula:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ComboCelula1
        '
        Me.ComboCelula1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboCelula1.ForeColor = System.Drawing.Color.MediumBlue
        Me.ComboCelula1.Location = New System.Drawing.Point(72, 8)
        Me.ComboCelula1.Name = "ComboCelula1"
        Me.ComboCelula1.Size = New System.Drawing.Size(232, 21)
        Me.ComboCelula1.TabIndex = 39
        '
        'btnGenerarPedidos
        '
        Me.btnGenerarPedidos.BackColor = System.Drawing.SystemColors.Control
        Me.btnGenerarPedidos.Enabled = False
        Me.btnGenerarPedidos.Image = CType(resources.GetObject("btnGenerarPedidos.Image"), System.Drawing.Bitmap)
        Me.btnGenerarPedidos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenerarPedidos.Location = New System.Drawing.Point(176, 80)
        Me.btnGenerarPedidos.Name = "btnGenerarPedidos"
        Me.btnGenerarPedidos.Size = New System.Drawing.Size(128, 23)
        Me.btnGenerarPedidos.TabIndex = 37
        Me.btnGenerarPedidos.Text = "Generar pedidos"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.IndianRed
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblTotalDuplicados, Me.Label30, Me.lblTotalNB, Me.Label29, Me.lblTotalRemisiones, Me.Label27, Me.lblTotalEntregas, Me.Label16})
        Me.Panel4.Location = New System.Drawing.Point(8, 288)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(304, 112)
        Me.Panel4.TabIndex = 36
        '
        'lblTotalDuplicados
        '
        Me.lblTotalDuplicados.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTotalDuplicados.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTotalDuplicados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalDuplicados.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDuplicados.Location = New System.Drawing.Point(176, 56)
        Me.lblTotalDuplicados.Name = "lblTotalDuplicados"
        Me.lblTotalDuplicados.Size = New System.Drawing.Size(112, 20)
        Me.lblTotalDuplicados.TabIndex = 44
        Me.lblTotalDuplicados.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.White
        Me.Label30.Location = New System.Drawing.Point(16, 59)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(119, 14)
        Me.Label30.TabIndex = 45
        Me.Label30.Text = "Total de duplicados:"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalNB
        '
        Me.lblTotalNB.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTotalNB.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTotalNB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalNB.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalNB.Location = New System.Drawing.Point(176, 32)
        Me.lblTotalNB.Name = "lblTotalNB"
        Me.lblTotalNB.Size = New System.Drawing.Size(112, 20)
        Me.lblTotalNB.TabIndex = 42
        Me.lblTotalNB.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.ForeColor = System.Drawing.Color.White
        Me.Label29.Location = New System.Drawing.Point(16, 35)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(136, 14)
        Me.Label29.TabIndex = 43
        Me.Label29.Text = "Total de notas blancas:"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalRemisiones
        '
        Me.lblTotalRemisiones.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTotalRemisiones.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTotalRemisiones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalRemisiones.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalRemisiones.Location = New System.Drawing.Point(176, 8)
        Me.lblTotalRemisiones.Name = "lblTotalRemisiones"
        Me.lblTotalRemisiones.Size = New System.Drawing.Size(112, 20)
        Me.lblTotalRemisiones.TabIndex = 40
        Me.lblTotalRemisiones.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.White
        Me.Label27.Location = New System.Drawing.Point(16, 11)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(120, 14)
        Me.Label27.TabIndex = 41
        Me.Label27.Text = "Total de remisiones:"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalEntregas
        '
        Me.lblTotalEntregas.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTotalEntregas.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTotalEntregas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotalEntregas.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalEntregas.Location = New System.Drawing.Point(176, 80)
        Me.lblTotalEntregas.Name = "lblTotalEntregas"
        Me.lblTotalEntregas.Size = New System.Drawing.Size(112, 20)
        Me.lblTotalEntregas.TabIndex = 31
        Me.lblTotalEntregas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.White
        Me.Label16.Location = New System.Drawing.Point(16, 83)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(108, 14)
        Me.Label16.TabIndex = 35
        Me.Label16.Text = "Total de registros:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTitulos
        '
        Me.lblTitulos.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTitulos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitulos.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulos.ForeColor = System.Drawing.Color.Blue
        Me.lblTitulos.Location = New System.Drawing.Point(8, 400)
        Me.lblTitulos.Name = "lblTitulos"
        Me.lblTitulos.Size = New System.Drawing.Size(680, 16)
        Me.lblTitulos.TabIndex = 35
        '
        'Panel3
        '
        Me.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Panel3.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblTiempoRealVenta, Me.Label25, Me.lblHoraUltimoSurtido, Me.lblHoraPrimerSurtido, Me.lblTiempoTranscurrido, Me.lblTotInicial, Me.lblTotFinal, Me.lblFinDeRuta, Me.lblArchivo, Me.lblRuta, Me.lblInicioDeRuta, Me.lblAutotanque, Me.Label28, Me.Label10, Me.Label19, Me.Label11, Me.Label5, Me.Label4, Me.Label17, Me.Label15, Me.Label14, Me.Label26})
        Me.Panel3.Location = New System.Drawing.Point(312, 112)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(376, 288)
        Me.Panel3.TabIndex = 34
        '
        'lblTiempoRealVenta
        '
        Me.lblTiempoRealVenta.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTiempoRealVenta.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTiempoRealVenta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTiempoRealVenta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTiempoRealVenta.ForeColor = System.Drawing.Color.Red
        Me.lblTiempoRealVenta.Location = New System.Drawing.Point(160, 248)
        Me.lblTiempoRealVenta.Name = "lblTiempoRealVenta"
        Me.lblTiempoRealVenta.Size = New System.Drawing.Size(208, 20)
        Me.lblTiempoRealVenta.TabIndex = 51
        Me.lblTiempoRealVenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(16, 251)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(128, 14)
        Me.Label25.TabIndex = 52
        Me.Label25.Text = "Tiempo real de venta:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblHoraUltimoSurtido
        '
        Me.lblHoraUltimoSurtido.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblHoraUltimoSurtido.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblHoraUltimoSurtido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHoraUltimoSurtido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHoraUltimoSurtido.Location = New System.Drawing.Point(160, 224)
        Me.lblHoraUltimoSurtido.Name = "lblHoraUltimoSurtido"
        Me.lblHoraUltimoSurtido.Size = New System.Drawing.Size(208, 20)
        Me.lblHoraUltimoSurtido.TabIndex = 48
        Me.lblHoraUltimoSurtido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblHoraPrimerSurtido
        '
        Me.lblHoraPrimerSurtido.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblHoraPrimerSurtido.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblHoraPrimerSurtido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblHoraPrimerSurtido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHoraPrimerSurtido.Location = New System.Drawing.Point(160, 200)
        Me.lblHoraPrimerSurtido.Name = "lblHoraPrimerSurtido"
        Me.lblHoraPrimerSurtido.Size = New System.Drawing.Size(208, 20)
        Me.lblHoraPrimerSurtido.TabIndex = 47
        Me.lblHoraPrimerSurtido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTiempoTranscurrido
        '
        Me.lblTiempoTranscurrido.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTiempoTranscurrido.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTiempoTranscurrido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTiempoTranscurrido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTiempoTranscurrido.Location = New System.Drawing.Point(160, 128)
        Me.lblTiempoTranscurrido.Name = "lblTiempoTranscurrido"
        Me.lblTiempoTranscurrido.Size = New System.Drawing.Size(208, 20)
        Me.lblTiempoTranscurrido.TabIndex = 45
        Me.lblTiempoTranscurrido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotInicial
        '
        Me.lblTotInicial.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTotInicial.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTotInicial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotInicial.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotInicial.Location = New System.Drawing.Point(160, 152)
        Me.lblTotInicial.Name = "lblTotInicial"
        Me.lblTotInicial.Size = New System.Drawing.Size(208, 20)
        Me.lblTotInicial.TabIndex = 43
        Me.lblTotInicial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTotFinal
        '
        Me.lblTotFinal.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblTotFinal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblTotFinal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblTotFinal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotFinal.Location = New System.Drawing.Point(160, 176)
        Me.lblTotFinal.Name = "lblTotFinal"
        Me.lblTotFinal.Size = New System.Drawing.Size(208, 20)
        Me.lblTotFinal.TabIndex = 41
        Me.lblTotFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFinDeRuta
        '
        Me.lblFinDeRuta.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblFinDeRuta.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblFinDeRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFinDeRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinDeRuta.Location = New System.Drawing.Point(160, 104)
        Me.lblFinDeRuta.Name = "lblFinDeRuta"
        Me.lblFinDeRuta.Size = New System.Drawing.Size(208, 20)
        Me.lblFinDeRuta.TabIndex = 39
        Me.lblFinDeRuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblArchivo
        '
        Me.lblArchivo.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblArchivo.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblArchivo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblArchivo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArchivo.Location = New System.Drawing.Point(160, 8)
        Me.lblArchivo.Name = "lblArchivo"
        Me.lblArchivo.Size = New System.Drawing.Size(208, 20)
        Me.lblArchivo.TabIndex = 37
        Me.lblArchivo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRuta
        '
        Me.lblRuta.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblRuta.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRuta.Location = New System.Drawing.Point(160, 32)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(208, 20)
        Me.lblRuta.TabIndex = 32
        Me.lblRuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblInicioDeRuta
        '
        Me.lblInicioDeRuta.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblInicioDeRuta.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblInicioDeRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblInicioDeRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInicioDeRuta.Location = New System.Drawing.Point(160, 80)
        Me.lblInicioDeRuta.Name = "lblInicioDeRuta"
        Me.lblInicioDeRuta.Size = New System.Drawing.Size(208, 20)
        Me.lblInicioDeRuta.TabIndex = 30
        Me.lblInicioDeRuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAutotanque
        '
        Me.lblAutotanque.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblAutotanque.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblAutotanque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAutotanque.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutotanque.Location = New System.Drawing.Point(160, 56)
        Me.lblAutotanque.Name = "lblAutotanque"
        Me.lblAutotanque.Size = New System.Drawing.Size(208, 20)
        Me.lblAutotanque.TabIndex = 29
        Me.lblAutotanque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(16, 227)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(136, 14)
        Me.Label28.TabIndex = 50
        Me.Label28.Text = "Hora de último surtido:"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(16, 131)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(124, 14)
        Me.Label10.TabIndex = 46
        Me.Label10.Text = "Tiempo transcurrido:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(16, 155)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(109, 14)
        Me.Label19.TabIndex = 44
        Me.Label19.Text = "Totalizador inicial:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(16, 179)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 14)
        Me.Label11.TabIndex = 42
        Me.Label11.Text = "Totalizador final:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 107)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 14)
        Me.Label5.TabIndex = 40
        Me.Label5.Text = "Fin de ruta:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 14)
        Me.Label4.TabIndex = 38
        Me.Label4.Text = "Archivo de descarga:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(16, 35)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(35, 14)
        Me.Label17.TabIndex = 36
        Me.Label17.Text = "Ruta:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(16, 83)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(85, 14)
        Me.Label15.TabIndex = 34
        Me.Label15.Text = "Inicio de ruta:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(16, 59)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(75, 14)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "Autotanque:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(16, 203)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(138, 14)
        Me.Label26.TabIndex = 49
        Me.Label26.Text = "Hora de primer surtido:"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblImporteTotal, Me.lblImporteCredito, Me.Label6, Me.Label8, Me.Label12, Me.lblImporteContado})
        Me.Panel2.Location = New System.Drawing.Point(8, 200)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(304, 88)
        Me.Panel2.TabIndex = 33
        '
        'lblImporteTotal
        '
        Me.lblImporteTotal.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblImporteTotal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblImporteTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblImporteTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporteTotal.ForeColor = System.Drawing.Color.Red
        Me.lblImporteTotal.Location = New System.Drawing.Point(176, 56)
        Me.lblImporteTotal.Name = "lblImporteTotal"
        Me.lblImporteTotal.Size = New System.Drawing.Size(112, 20)
        Me.lblImporteTotal.TabIndex = 17
        Me.lblImporteTotal.Text = "0.00"
        Me.lblImporteTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblImporteCredito
        '
        Me.lblImporteCredito.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblImporteCredito.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblImporteCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblImporteCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporteCredito.Location = New System.Drawing.Point(176, 32)
        Me.lblImporteCredito.Name = "lblImporteCredito"
        Me.lblImporteCredito.Size = New System.Drawing.Size(112, 20)
        Me.lblImporteCredito.TabIndex = 25
        Me.lblImporteCredito.Text = "0.00"
        Me.lblImporteCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(16, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(85, 14)
        Me.Label6.TabIndex = 19
        Me.Label6.Text = "Total importe:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(16, 35)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(98, 14)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Importe crédito:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(16, 11)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(104, 14)
        Me.Label12.TabIndex = 31
        Me.Label12.Text = "Importe contado:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblImporteContado
        '
        Me.lblImporteContado.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblImporteContado.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblImporteContado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblImporteContado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporteContado.Location = New System.Drawing.Point(176, 8)
        Me.lblImporteContado.Name = "lblImporteContado"
        Me.lblImporteContado.Size = New System.Drawing.Size(112, 20)
        Me.lblImporteContado.TabIndex = 29
        Me.lblImporteContado.Text = "0.00"
        Me.lblImporteContado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Khaki
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblLitrosTotal, Me.Label9, Me.lblLitrosCredito, Me.Label13, Me.lblLitrosContado, Me.Label7})
        Me.Panel1.Location = New System.Drawing.Point(8, 112)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(304, 88)
        Me.Panel1.TabIndex = 32
        '
        'lblLitrosTotal
        '
        Me.lblLitrosTotal.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblLitrosTotal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblLitrosTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLitrosTotal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLitrosTotal.ForeColor = System.Drawing.Color.Blue
        Me.lblLitrosTotal.Location = New System.Drawing.Point(176, 56)
        Me.lblLitrosTotal.Name = "lblLitrosTotal"
        Me.lblLitrosTotal.Size = New System.Drawing.Size(112, 20)
        Me.lblLitrosTotal.TabIndex = 16
        Me.lblLitrosTotal.Text = "0.00"
        Me.lblLitrosTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 35)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 14)
        Me.Label9.TabIndex = 26
        Me.Label9.Text = "Litros crédito:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLitrosCredito
        '
        Me.lblLitrosCredito.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblLitrosCredito.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblLitrosCredito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLitrosCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLitrosCredito.Location = New System.Drawing.Point(176, 32)
        Me.lblLitrosCredito.Name = "lblLitrosCredito"
        Me.lblLitrosCredito.Size = New System.Drawing.Size(112, 20)
        Me.lblLitrosCredito.TabIndex = 24
        Me.lblLitrosCredito.Text = "0.00"
        Me.lblLitrosCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(16, 11)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 14)
        Me.Label13.TabIndex = 30
        Me.Label13.Text = "Litros contado:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLitrosContado
        '
        Me.lblLitrosContado.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblLitrosContado.BackColor = System.Drawing.Color.WhiteSmoke
        Me.lblLitrosContado.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLitrosContado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLitrosContado.Location = New System.Drawing.Point(176, 8)
        Me.lblLitrosContado.Name = "lblLitrosContado"
        Me.lblLitrosContado.Size = New System.Drawing.Size(112, 20)
        Me.lblLitrosContado.TabIndex = 28
        Me.lblLitrosContado.Text = "0.00"
        Me.lblLitrosContado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(16, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(70, 14)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Total litros:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lstDescarga
        '
        Me.lstDescarga.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lstDescarga.BackColor = System.Drawing.Color.Black
        Me.lstDescarga.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstDescarga.Font = New System.Drawing.Font("Courier New", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstDescarga.ForeColor = System.Drawing.Color.FromArgb(CType(224, Byte), CType(224, Byte), CType(224, Byte))
        Me.lstDescarga.ItemHeight = 14
        Me.lstDescarga.Location = New System.Drawing.Point(8, 416)
        Me.lstDescarga.Name = "lstDescarga"
        Me.lstDescarga.Size = New System.Drawing.Size(680, 72)
        Me.lstDescarga.TabIndex = 21
        '
        'btnConsultarDescarga
        '
        Me.btnConsultarDescarga.BackColor = System.Drawing.SystemColors.Control
        Me.btnConsultarDescarga.Image = CType(resources.GetObject("btnConsultarDescarga.Image"), System.Drawing.Bitmap)
        Me.btnConsultarDescarga.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConsultarDescarga.Location = New System.Drawing.Point(8, 80)
        Me.btnConsultarDescarga.Name = "btnConsultarDescarga"
        Me.btnConsultarDescarga.Size = New System.Drawing.Size(152, 23)
        Me.btnConsultarDescarga.TabIndex = 22
        Me.btnConsultarDescarga.Text = "Consultar descarga..."
        '
        'mnuPrincipal
        '
        Me.mnuPrincipal.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuArchivo, Me.mnuAyuda})
        '
        'mnuArchivo
        '
        Me.mnuArchivo.Index = 0
        Me.mnuArchivo.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuConfiguracion, Me.MenuItem2, Me.mnuSalir})
        Me.mnuArchivo.Text = "&Archivo"
        '
        'mnuConfiguracion
        '
        Me.mnuConfiguracion.Index = 0
        Me.mnuConfiguracion.Text = "&Configuración"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "-"
        '
        'mnuSalir
        '
        Me.mnuSalir.Index = 2
        Me.mnuSalir.Text = "&Salir"
        '
        'mnuAyuda
        '
        Me.mnuAyuda.Index = 1
        Me.mnuAyuda.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAcercade})
        Me.mnuAyuda.Text = "&?"
        '
        'mnuAcercade
        '
        Me.mnuAcercade.Index = 0
        Me.mnuAcercade.Text = "&Acerca de..."
        '
        'imgLista
        '
        Me.imgLista.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.imgLista.ImageSize = New System.Drawing.Size(32, 32)
        Me.imgLista.ImageStream = CType(resources.GetObject("imgLista.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgLista.TransparentColor = System.Drawing.Color.Transparent
        '
        'frmInterfaseCorporativo
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(704, 537)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.tabRampac, Me.stbEstatus})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mnuPrincipal
        Me.Name = "frmInterfaseCorporativo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Interfase con el dispositivo Rampac -SIGAMET-"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tabRampac.ResumeLayout(False)
        Me.tpCarga.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDescarga.ResumeLayout(False)
        CType(Me.grdAT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    'P#1    Procedimiento para consultar los precios quie seran almacenados a la tarjeta RAMCARD
    Private Sub ConsultarPrecio()
        Cursor = Cursors.WaitCursor
        Dim conn As New SqlConnection(GLOBAL_ConString)
        Dim cmd As New SqlCommand("spCCCargaArchivoRampacPrecios", conn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter(cmd)

        Try
            dsPrecios = New DataSet()
            Try
                da.Fill(dsPrecios, "CargaPrecio")
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    '#P2    Procedimiento para consultar los clientes que seran atendidos y seran almacenados en la
    '       tarjeta RAMCARD
    Private Sub ConsultarCliente()
        Cursor = Cursors.WaitCursor
        Dim conn As New SqlConnection(GLOBAL_ConString)
        Dim cmd As New SqlCommand("spCCCargaArchivoRampacClientes", conn)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dtpFecha.Value.Date
            .Parameters.Add("@Celula", SqlDbType.TinyInt).Value = CType(ComboCelula.Celula, Byte)
            .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = ComboRuta.Ruta
        End With

        Dim da As New SqlDataAdapter(cmd)

        Try
            dsClientes = New DataSet()
            Try
                da.Fill(dsClientes, "CargaCliente")
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub


    Private Sub frmInterfase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        'Dim cn As New SqlConnection(SigaMetClasses.LeeInfoConexion(False))
        Dim cn As New SqlConnection(GLOBAL_ConString)

        Dim cParametro As New SigaMetClasses.cConfig(1)
        DescuentoDirecto = CType(cParametro.Parametros("DescuentoDirecto"), Boolean)


        cn.Open()
        cmdInsert.Connection = cn
        cmdInsert.CommandTimeout = 30
        cmdInsert.CommandText = " Select Convert(Datetime,Convert(VarChar(2),Day(getdate()))+'/'+Convert(VarChar(2),Month(getdate()))+'/'+Convert(VarChar(4),Year(getdate()))) as Fecha "
        rdrInsert = cmdInsert.ExecuteReader()
        rdrInsert.Read()
        _DiadeHoy = CType(rdrInsert("Fecha"), Date)
        rdrInsert.Close()
        cmdInsert.Dispose()
        cn.Close()
        cn.Dispose()

        dtpFecha.Value = dtpFecha.Value.AddDays(1)

        'Modificaciones para lo de cualquiera liquida cualquier ruta. MHV. 19/11/2004.


        'If CType(_Nivel, Double) <> 1 Then
        'ComboCelula.CargaDatos(CType(GLOBAL_Celula, Short))
        'Else

        ComboCelula.CargaDatos()
        ComboCelula1.CargaDatos()

        'End If

        'ComboCelula1.CargaDatos(CType(GLOBAL_Celula, Short))

        lblFechaDescarga.Text = Now.Date.ToShortDateString
        ConsultaArchivoConfiguracion()
        _Acepto = False

    End Sub

    Private Sub ConsultaConfiguracion()
        Dim frmConfig As New Configuracion(RutaArchivosCarga, RutaArchivosDescarga)
        frmConfig.ShowDialog()
    End Sub

    Private Sub ConsultaArchivoConfiguracion()
        'Procedimiento para leer el archivo de configuración para saber
        'los directorios en donde va a ir a leer y a escribir las interfases.
        Dim sr As StreamReader = Nothing
        Try
            sr = New StreamReader(Application.StartupPath & "\Config.txt")
            Dim i As Integer
            While sr.Peek > -1
                i += 1
                If i = 1 Then
                    RutaArchivosCarga = sr.ReadLine()
                End If
                If i = 2 Then
                    RutaArchivosDescarga = sr.ReadLine()
                End If
            End While
            If i <= 0 Then
                'No tiene registros válidos el archivo y se usa la carpeta de la
                'aplicación.
                RutaArchivosCarga = Application.StartupPath
                RutaArchivosDescarga = Application.StartupPath
            End If
        Catch exIO As IOException
            RutaArchivosCarga = Application.StartupPath
            RutaArchivosDescarga = Application.StartupPath
        Catch ex As Exception
            MessageBox.Show(ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not sr Is Nothing Then sr.Close()
        End Try
    End Sub

    Private Sub ConsultaCarga()

        Dim fs As FileStream = Nothing
        Dim sr As StreamReader = Nothing
        Dim strArchivo As String, strEncabezado As String, strLinea As String
        Dim reg As Integer = 148
        Dim i As Integer
        Dim TotalReg As Double = 0
        Dim Enc As Integer = 285
        Dim Cliente As String
        Dim Producto As String = Nothing
        Dim ClienteNombre As String
        Dim CalleNombre As String

        File1.InitialDirectory = RutaArchivosCarga
        File1.Filter = "Archivos de carga Rampac|T*.*|Todos los archivos|*.*"
        If File1.ShowDialog() = DialogResult.OK Then
            Try
                fs = New FileStream(File1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)
                sr = New StreamReader(fs, System.Text.Encoding.Default)

                strArchivo = sr.ReadToEnd()

                TotalReg = CType((Len(strArchivo) - Enc) / reg, Integer)

                If TotalReg <= 0 Then
                    MessageBox.Show("El archivo no tiene registros que leer.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                stbEstatus.Text = TotalReg.ToString & " registro(s)"

                strEncabezado = Mid(strArchivo, 1, Enc)

                'lstCarga.Items.Clear()

                Dim salto As Integer = Enc + 1

                Dim frmConCarga As New ConsultaCarga()

                For i = 1 To CInt(TotalReg)
                    strLinea = Trim(Mid(strArchivo, salto, reg))
                    Cliente = Mid(strLinea, 1, 10)
                    ClienteNombre = Mid(strLinea, 16, 20)
                    CalleNombre = Mid(strLinea, 56, 20)
                    frmConCarga.lstCarga.Items.Add(Cliente & " " & ClienteNombre & " " & CalleNombre)

                    salto += reg
                Next

                frmConCarga.lblInformacion.Text = "Archivo: " & File1.FileName
                frmConCarga.stbEstatus.Text = frmConCarga.lstCarga.Items.Count.ToString & " registro(s)"
                frmConCarga.ShowDialog()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                If Not fs Is Nothing Then fs.Close()
                If Not sr Is Nothing Then sr.Close()
            End Try
        End If

    End Sub

    Private Sub ConsultaDescargaArchivoSecuencial()


        Dim fs As FileStream = Nothing
        Dim sr As StreamReader = Nothing
        Dim strArchivo As String, strEncabezado As String, strLinea As String
        Dim reg As Integer = 148
        Dim i As Integer
        Dim TotalReg As Double = 0
        Dim Enc As Integer = 285
        Dim Cliente As String
        Dim Producto As String = Nothing
        Dim ClienteNombre As String
        Dim TipoOperacion As String
        Dim TipoPedido As String = Nothing
        Dim CalleNombre As String
        Dim InfoOperacion As String
        Dim LitrosSurtidos As String
        Dim Importe As String
        Dim Con As String
        Dim HoraInicio As String
        Dim HoraFinal As String
        Dim DiaYMes As String
        Dim decLitros As Decimal
        Dim decImporte As Decimal
        Dim DescuentoFijo As String     '$A1-A

        If File1.ShowDialog() = DialogResult.OK Then
            Try
                fs = New FileStream(File1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)
                sr = New StreamReader(fs, System.Text.Encoding.Default)

                strArchivo = sr.ReadLine

                TotalReg = CType((Len(strArchivo) - Enc) / reg, Integer)

                If TotalReg <= 0 Then
                    MessageBox.Show("El archivo no tiene registros que leer.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                stbEstatus.Text = TotalReg.ToString & " registro(s)"

                strEncabezado = Mid(strArchivo, 1, Enc)

                'lstCarga.Items.Clear()

                Dim salto As Integer = Enc + 1

                For i = 1 To CInt(TotalReg)
                    strLinea = Trim(Mid(strArchivo, salto, reg))
                    Cliente = Mid(strLinea, 1, 10)
                    If Not IsNumeric(Cliente) Then
                        MessageBox.Show("Error en la lectura del contrato del cliente.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    TipoOperacion = Mid(strLinea, 11, 1)

                    Select Case TipoOperacion
                        Case Is = "a"   'No se surtió
                            'ClienteNombre = Mid(strLinea, 16, 20)
                            'ClienteNombre = "NO SURTIDO"
                            'CalleNombre = ""
                        Case Is = "b", "d", "g"
                            DescuentoFijo = Mid(strLinea, 12, 3)    '$A1-A
                            If TipoOperacion = "b" Then
                                TipoPedido = "Tarjeta"
                            End If
                            If TipoOperacion = "d" Then
                                TipoPedido = "Duplicado"
                            End If
                            If TipoOperacion = "g" Then
                                TipoPedido = "NotaBlanca"
                            End If

                            TipoPedido = LSet(TipoPedido, 10)

                            CalleNombre = Mid(strLinea, 16, 20)

                            InfoOperacion = Mid(strLinea, 56, 60)

                            If Len(InfoOperacion) <> 60 Then
                                MessageBox.Show("La información del pedido del cliente '" & Cliente & "'" & Chr(13) & _
                                                " tiene una longitud diferente de 60. La lectura no puede continuar.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            LitrosSurtidos = Mid(InfoOperacion, 10, 7)
                            Importe = Mid(InfoOperacion, 17, 7)
                            Con = Mid(InfoOperacion, 43, 6)
                            HoraInicio = Mid(InfoOperacion, 49, 4)
                            HoraFinal = Mid(InfoOperacion, 53, 4)
                            DiaYMes = Mid(InfoOperacion, 57, 4)

                            'Validaciones de los datos
                            If Not IsNumeric(DescuentoFijo) Then    '$A1-A
                                MessageBox.Show("Error en la lectura del producto suritdo.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)    '$A1-A
                                Exit Sub                            '$A1-A
                            End If                                  '$A1-A
                            If Not IsNumeric(LitrosSurtidos) Then
                                MessageBox.Show("Error en la lectura de litros surtidos.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                            If Not IsNumeric(Importe) Then
                                MessageBox.Show("Error en la lectura de importe del pedido.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If
                            If Not IsNumeric(Con) Then
                                MessageBox.Show("Error en la lectura del consecutivo del pedido.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End If

                            LitrosSurtidos = Mid(LitrosSurtidos, 1, 5) & "." & Mid(LitrosSurtidos, 6, 2)
                            If i < CInt(TotalReg) Then
                                decLitros += CType(LitrosSurtidos, Decimal)
                            End If

                            Importe = Mid(Importe, 1, 5) & "." & Mid(Importe, 6, 2)
                            If i < CInt(TotalReg) Then
                                decImporte += CType(Importe, Decimal)
                            End If

                            HoraInicio = Mid(HoraInicio, 1, 2) & ":" & Mid(HoraInicio, 3, 2)

                            HoraFinal = Mid(HoraFinal, 1, 2) & ":" & Mid(HoraFinal, 3, 2)
                        Case Else
                            MessageBox.Show("Error en la lectura.  El tipo de operación no es reconocido por esta versión de sistema.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub

                    End Select

                    If TipoOperacion <> "a" And i < CInt(TotalReg) Then
                        'lstCarga.Items.Add(TipoPedido & " " & Cliente & " " & ClienteNombre & " " & CalleNombre & " " & LitrosSurtidos & " " & Importe & " " & Con & " " & HoraInicio & " " & HoraFinal & " " & DiaYMes)
                    End If
                    'ListBox1.Items.Add(strLinea)

                    ClienteNombre = ""
                    CalleNombre = ""
                    InfoOperacion = ""
                    TipoOperacion = ""
                    TipoPedido = ""
                    LitrosSurtidos = ""
                    Importe = ""
                    Con = ""
                    HoraInicio = ""
                    HoraFinal = ""
                    DiaYMes = ""

                    salto += reg
                Next i
                lblLitrosTotal.Text = decLitros.ToString("N")
                lblImporteTotal.Text = decImporte.ToString("N")

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
                If Not fs Is Nothing Then fs.Close()
                If Not sr Is Nothing Then sr.Close()
            End Try
        End If

    End Sub

    Private Sub ConsultaDescarga()
        Dim fs As FileStream = Nothing
        Dim sr As StreamReader = Nothing
        Dim strArchivo As String = Nothing
        Dim strEncabezado As String = Nothing
        Dim strLinea As String
        Dim reg As Integer = 148
        Dim i As Integer = Nothing
        Dim Ruta As Short
        Dim Autotanque As Short
        Dim InicioDeRuta As Date
        Dim FinDeRuta As Date
        Dim TiempoTranscurrido As Double
        Dim TotInicial As Integer
        Dim TotFinal As Integer
        Dim TotalReg As Double = 0
        Dim Enc As Integer = 285
        Dim Cliente As String
        Dim Producto As String = Nothing
        Dim ClienteNombre As String = Nothing
        Dim TipoOperacion As String
        Dim TipoPedido As String
        Dim CalleNombre As String
        Dim InfoOperacion As String
        Dim LitrosSurtidos As String
        Dim Importe As String
        Dim Con As String
        Dim HoraInicio As String
        Dim HoraFinal As String
        Dim DiaYMes As String
        Dim FormaPago As Byte
        Dim FormaPagoDescripcion As String = Nothing
        Dim TotalRegistros As Integer
        Dim decLitrosContado, decLitrosCredito, decLitrosTotal As Decimal
        Dim decImporteContado, decImporteCredito, decImporteTotal As Decimal
        Dim TotalRemisiones As Integer, TotalNB As Integer, TotalDuplicados As Integer
        Dim DescuentoFijo As String     '$A1-A

        File1.InitialDirectory = RutaArchivosDescarga
        File1.Filter = "Archivos de descarga Rampac|S*.*|Todos los archivos|*.*"
        If File1.ShowDialog() = DialogResult.OK Then

            'Checa si existe ARMA.EXE
            If Not File.Exists(Application.StartupPath & "\ARMA.EXE") Then
                MessageBox.Show("No se encontró el archivo ARMA.EXE en la ruta de la aplicación." & Chr(13) & "Por favor verifique.", Me.Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'Checa si existe DESARM.EXE
            If Not File.Exists(Application.StartupPath & "\DESARM.EXE") Then
                MessageBox.Show("No se encontró el archivo DESARM.EXE en la ruta de la aplicación." & Chr(13) & "Por favor verifique.", Me.Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            btnGenerarPedidos.Enabled = True

            Try
                ListaPedidos = New ArrayList()
                'Código agregado 
                File.Copy(File1.FileName, Application.StartupPath & "\tempA.dat", True)
                Environment.CurrentDirectory = Application.StartupPath
                Shell(Application.StartupPath & "\DESARM <tempA.dat> tempB.dat", AppWinStyle.Hide, True)
                'Fin de agregado

                'Código modificado
                fs = New FileStream(Application.StartupPath & "\tempB.dat", FileMode.Open, FileAccess.Read, FileShare.Read)
                'Fin de modificación

                sr = New StreamReader(fs, System.Text.Encoding.Default)


                LimpiaResultadosConsultaDescarga()
                EncabezadoCorrupto = False
                ManejaColoresEncabezadoCorrupto(EncabezadoCorrupto)

                lblTitulos.Text = "Co " & "Inicio " & "Fin   " & LSet("TipoPedido", 10) & " " & LSet("Cliente", 10) & " " & LSet("Calle", 20) & " " & LSet(" Litros", 8) & " " & LSet(" Importe", 8) & " Forma de pago" 'Ult. mod: 12/Nov/2002

                While sr.Peek > -1
                    strLinea = sr.ReadLine
                    TotalReg += 1
                    If TotalReg = 1 Then    '1er linea
                        Try
                            Ruta = CType(Mid(strLinea, 1, 5), Short)
                        Catch ex As InvalidCastException
                            'La ruta está corrupta
                            EncabezadoCorrupto = True
                            Ruta = ComboRuta1.Ruta
                            lblRuta.Text = Ruta.ToString
                            ManejaColoresEncabezadoCorrupto(EncabezadoCorrupto)
                        End Try
                    End If
                    If TotalReg = 2 Then    '2da linea
                        Try
                            InicioDeRuta = CType(Mid(strLinea, 5, 2) & "/" & Mid(strLinea, 7, 2) & "/" & Replace(Mid(strLinea, 9, 2), " ", "0") & " " & Mid(strLinea, 1, 2) & ":" & Mid(strLinea, 3, 2), Date)
                            Autotanque = CType(Mid(strLinea, 11, 3), Short)
                            TotInicial = CType(Mid(strLinea, 26, 7), Integer)
                        Catch ex As InvalidCastException
                            'Los datos estan corruptos
                            EncabezadoCorrupto = True
                            InicioDeRuta = Now.Date
                            Autotanque = _Autotanque
                            TotInicial = 0
                            ManejaColoresEncabezadoCorrupto(EncabezadoCorrupto)
                        End Try
                    End If
                    If TotalReg = 3 Then    '3ra linea
                        Try
                            FinDeRuta = CType(Mid(strLinea, 5, 2) & "/" & Mid(strLinea, 7, 2) & "/" & Replace(Mid(strLinea, 9, 2), " ", "0") & " " & Mid(strLinea, 1, 2) & ":" & Mid(strLinea, 3, 2), Date)
                            TotFinal = CType(Mid(strLinea, 23, 7), Integer)

                            TiempoTranscurrido = ((DateDiff(DateInterval.Second, InicioDeRuta, FinDeRuta)) / 60) / 60

                            If TotInicial > TotFinal Then
                                MessageBox.Show("Los totalizadores son incorrectos.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit While
                            End If
                        Catch ex As InvalidCastException
                            'Los datos estan corruptos
                            EncabezadoCorrupto = True
                            FinDeRuta = Now.Date
                            TotFinal = 0
                            TiempoTranscurrido = 0
                            ManejaColoresEncabezadoCorrupto(EncabezadoCorrupto)
                        End Try
                    End If
                    If Len(strLinea) = 148 Then 'Solo leo las lineas que tengan el largo de 148 caracteres
                        TotalRegistros += 1
                        Cliente = Mid(strLinea, 1, 10)

                        If Not IsNumeric(Cliente) Then
                            MessageBox.Show("Error en la lectura del cliente: " & Cliente, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End If
                        TipoOperacion = Mid(strLinea, 11, 1)

                        'Checo que no sea el fin de ruta
                        If TipoOperacion <> "a" And TipoOperacion <> "0" And Cliente <> "9999999999" Then

                            DescuentoFijo = Mid(strLinea, 12, 3)    '$A1-A
                            'Checo el tipo de operación de cada registro
                            Select Case TipoOperacion
                                Case Is = "b"
                                    TipoPedido = LSet("Tarjeta", 10)
                                    TotalRemisiones += 1
                                Case Is = "g"
                                    TipoPedido = LSet("NotaBlanca", 10)
                                    TotalNB += 1
                                Case Is = "d"
                                    TipoPedido = LSet("Duplicado", 10)
                                    TotalDuplicados += 1
                                Case Else
                                    TipoPedido = LSet("", 10)
                            End Select

                            CalleNombre = Mid(strLinea, 16, 20)

                            'FormaPago = CType(Mid(strLinea, 116, 3), Byte)
                            'Cambio hecho el 2 de enero del 2003
                            FormaPago = CType(Mid(strLinea, 118, 1), Byte)

                            InfoOperacion = Mid(strLinea, 56, 60)

                            LitrosSurtidos = Mid(InfoOperacion, 10, 7)
                            Importe = Mid(InfoOperacion, 17, 7)
                            Con = Mid(InfoOperacion, 43, 6)
                            HoraInicio = Mid(InfoOperacion, 49, 4)
                            HoraFinal = Mid(InfoOperacion, 53, 4)
                            DiaYMes = Mid(InfoOperacion, 57, 4)

                            LitrosSurtidos = Mid(LitrosSurtidos, 1, 5) & "." & Mid(LitrosSurtidos, 6, 2)

                            'If FormaPago = 21 Then
                            '    decLitrosContado += CType(LitrosSurtidos, Decimal)
                            'End If
                            'If FormaPago = 22 Then
                            '    decLitrosCredito += CType(LitrosSurtidos, Decimal)
                            'End If
                            '*******************
                            '2 de enero del 2002
                            'NOTA: Antes FormaPago 21 ó 22
                            '*******************
                            If FormaPago = 1 Then
                                decLitrosContado += CType(LitrosSurtidos, Decimal)
                            End If
                            If FormaPago = 2 Then
                                decLitrosCredito += CType(LitrosSurtidos, Decimal)
                            End If
                            decLitrosTotal += CType(LitrosSurtidos, Decimal)

                            Importe = Mid(Importe, 1, 5) & "." & Mid(Importe, 6, 2)
                            If FormaPago = 1 Then
                                FormaPagoDescripcion = "Contado"
                                decImporteContado += CType(Importe, Decimal)
                            End If
                            If FormaPago = 2 Then
                                FormaPagoDescripcion = "Crédito"
                                decImporteCredito += CType(Importe, Decimal)
                            End If
                            decImporteTotal += CType(Importe, Decimal)

                            HoraInicio = Mid(HoraInicio, 1, 2) & ":" & Mid(HoraInicio, 3, 2)
                            HoraFinal = Mid(HoraFinal, 1, 2) & ":" & Mid(HoraFinal, 3, 2)

                            'Creación de la lista de objetos
                            Dim oRampac As sRampacCorporativo
                            With oRampac
                                .AnoAtt = 2002
                                .Folio = 2
                                .Consecutivo = TotalRegistros
                                .Cliente = CType(Cliente, Integer)
                                .Litros = CType(LitrosSurtidos, Decimal)
                                .Importe = CType(Importe, Decimal)
                                .HoraInicio = CType(FinDeRuta.ToShortDateString & " " & HoraInicio, Date)
                                .HoraFinal = CType(FinDeRuta.ToShortDateString & " " & HoraFinal, Date)
                                .FormaPago = FormaPagoDescripcion
                                .Fecha = CType(FinDeRuta.ToShortDateString, Date)
                                .TipoOperacion = TipoPedido
                                .ConsecutivoRampac = CType(Con, Short)
                                .Ruta = Ruta
                                .Autotanque = Autotanque
                                .DescuentoFijo = CType(DescuentoFijo, Short)      '$A1-A
                            End With

                            ListaPedidos.Add(oRampac)

                            'Se agrega el registro del pedido al listbox
                            lstDescarga.Items.Add(Mid(Con, 5, 2) & " " & HoraInicio & " " & HoraFinal & " " & TipoPedido & " " & Cliente.ToString & " " & CalleNombre & _
                                " " & LitrosSurtidos & " " & Importe & " " & _
                                FormaPago & " " & FormaPagoDescripcion) 'Ult.mod: 13/nov/2002
                            lstDescarga.Sorted = True
                        End If

                    End If

                End While

                'Total de registros
                lblTotalRemisiones.Text = TotalRemisiones.ToString
                lblTotalNB.Text = TotalNB.ToString
                lblTotalDuplicados.Text = TotalDuplicados.ToString
                lblTotalEntregas.Text = lstDescarga.Items.Count.ToString

                lblArchivo.Text = File1.FileName
                lblRuta.Text = Ruta.ToString
                lblAutotanque.Text = Autotanque.ToString
                lblInicioDeRuta.Text = InicioDeRuta.ToString
                lblFinDeRuta.Text = FinDeRuta.ToString
                lblTiempoTranscurrido.Text = TiempoTranscurrido.ToString("N") & " hrs."
                lblTotInicial.Text = TotInicial.ToString
                lblTotFinal.Text = TotFinal.ToString
                lblHoraPrimerSurtido.Text = CType(InicioDeRuta.ToShortDateString & " " & Mid(CType(lstDescarga.Items(0), String), 4, 5), Date).ToString
                lblHoraUltimoSurtido.Text = CType(InicioDeRuta.ToShortDateString & " " & Mid(CType(lstDescarga.Items(lstDescarga.Items.Count - 1), String), 4, 5), Date).ToString
                lblTiempoRealVenta.Text = ((DateDiff(DateInterval.Second, CType(lblHoraPrimerSurtido.Text, Date), CType(lblHoraUltimoSurtido.Text, Date)) / 60) / 60).ToString("N") & " hrs."

                lblLitrosContado.Text = decLitrosContado.ToString("N")
                lblLitrosCredito.Text = decLitrosCredito.ToString("N")
                lblLitrosTotal.Text = decLitrosTotal.ToString("N")
                lblImporteContado.Text = decImporteContado.ToString("C")
                lblImporteCredito.Text = decImporteCredito.ToString("C")
                lblImporteTotal.Text = decImporteTotal.ToString("C")

                If TotalRegistros <= 0 Then
                    MessageBox.Show("El archivo " & File1.FileName & " no tiene registros.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If

                If EncabezadoCorrupto Then
                    MessageBox.Show("El encabezado del archivo Rampac " & File1.FileName & " está corrupto." & Chr(13) & _
                                     "Por favor, seleccione manualmente los datos de ruta y autotanque.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            Catch ex As Exception
                MessageBox.Show(ex.ToString, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If Not fs Is Nothing Then fs.Close()
                If Not sr Is Nothing Then sr.Close()

            End Try
        End If
    End Sub

    Private Sub ManejaColoresEncabezadoCorrupto(ByVal EstaCorrupto As Boolean)
        If EstaCorrupto Then
            lblRuta.ForeColor = Color.Yellow
            lblRuta.BackColor = Color.Black
            lblAutotanque.ForeColor = Color.Yellow
            lblAutotanque.BackColor = Color.Black
        Else
            lblRuta.ForeColor = Color.Black
            lblRuta.BackColor = Color.WhiteSmoke
            lblAutotanque.ForeColor = Color.Black
            lblAutotanque.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub LimpiaResultadosConsultaDescarga()
        lstDescarga.Items.Clear()
        lblLitrosContado.Text = "0.00"
        lblLitrosCredito.Text = "0.00"
        lblLitrosTotal.Text = "0.00"
        lblImporteContado.Text = "0.00"
        lblImporteCredito.Text = "0.00"
        lblImporteTotal.Text = "0.00"
    End Sub

    Private Sub btnConsultarDescarga_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultarDescarga.Click
        ConsultaDescarga()
    End Sub


    Private Sub mnuSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSalir.Click
        Me.Close()
    End Sub

    Private Sub btnConsultarCarga_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultarCarga.Click
        ConsultaCarga()
    End Sub

    Private Sub btnGenerarPedidos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerarPedidos.Click
        GenerarPedidos()
    End Sub

    Private Sub GenerarPedidos()
        If _AnoAtt <= 0 Or _Folio <= 0 Or _Autotanque = 0 Then
            MessageBox.Show("Debe seleccionar el autotanque relacionado con la descarga.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If MessageBox.Show("¿Desea generar los pedidos de esta descarga?" & Chr(13) & _
                           ComboRuta1.Descripcion & Chr(13) & _
                           "Autotanque: " & _Autotanque.ToString & Chr(13) & _
                           "Fecha: " & Now.Date.ToShortDateString, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
            Exit Sub
        End If

        Cursor = Cursors.WaitCursor
        Dim objRampac As sRampacCorporativo
        Dim Tran As SqlTransaction
        Dim cn As New SqlConnection(SigaMetClasses.LeeInfoConexion(False))
        Dim cmdATT As New SqlCommand("spATTActualizaRampac")
        Dim cmdRampac As New SqlCommand("spRampacAlta")

        cn.Open()
        Tran = cn.BeginTransaction()

        Try

            'Actualización de los campos en la tabla AutotanqueTurno
            _FInicioRutaRampac = CType(lblInicioDeRuta.Text, Date)
            _FTerminoRutaRampac = CType(lblFinDeRuta.Text, Date)
            _FPrimerSurtidoRampac = CType(lblHoraPrimerSurtido.Text, Date)
            _FUltimoSurtidoRampac = CType(lblHoraUltimoSurtido.Text, Date)
            _TotalizadorInicialRampac = CType(lblTotInicial.Text, Integer)
            _TotalizadorFinalRampac = CType(lblTotFinal.Text, Integer)

            cmdATT.CommandType = CommandType.StoredProcedure
            cmdATT.Connection = cn
            cmdATT.Transaction = Tran

            With cmdATT
                .Parameters.Add(New SqlParameter("@AnoAtt", SqlDbType.SmallInt)).Value = _AnoAtt
                .Parameters.Add(New SqlParameter("@Folio", SqlDbType.Int)).Value = _Folio
                .Parameters.Add(New SqlParameter("@FInicioRutaRampac", SqlDbType.DateTime)).Value = _FInicioRutaRampac
                .Parameters.Add(New SqlParameter("@FTerminoRutaRampac", SqlDbType.DateTime)).Value = _FTerminoRutaRampac
                .Parameters.Add(New SqlParameter("@FPrimerSurtidoRampac", SqlDbType.DateTime)).Value = _FPrimerSurtidoRampac
                .Parameters.Add(New SqlParameter("@FUltimoSurtidoRampac", SqlDbType.DateTime)).Value = _FUltimoSurtidoRampac
                .Parameters.Add(New SqlParameter("@TotalizadorInicialRampac", SqlDbType.Int)).Value = _TotalizadorInicialRampac
                .Parameters.Add(New SqlParameter("@TotalizadorFinalRampac", SqlDbType.Int)).Value = _TotalizadorFinalRampac
                .ExecuteNonQuery()
            End With


            cmdRampac.CommandType = CommandType.StoredProcedure
            cmdRampac.Connection = cn
            cmdRampac.Transaction = Tran

            'Se insertan los registros a la tabla Rampac
            For Each objRampac In ListaPedidos

                cmdRampac.Parameters.Clear()
                cmdRampac.Parameters.Add(New SqlParameter("@AnoAtt", SqlDbType.SmallInt)).Value = _AnoAtt
                cmdRampac.Parameters.Add(New SqlParameter("@Folio", SqlDbType.Int)).Value = _Folio
                cmdRampac.Parameters.Add(New SqlParameter("@Consecutivo", SqlDbType.Int)).Value = objRampac.Consecutivo
                cmdRampac.Parameters.Add(New SqlParameter("@Cliente", SqlDbType.Int)).Value = objRampac.Cliente
                cmdRampac.Parameters.Add(New SqlParameter("@Litros", SqlDbType.Decimal)).Value = objRampac.Litros
                cmdRampac.Parameters.Add(New SqlParameter("@Importe", SqlDbType.Money)).Value = objRampac.Importe
                cmdRampac.Parameters.Add(New SqlParameter("@HoraInicio", SqlDbType.DateTime)).Value = objRampac.HoraInicio
                cmdRampac.Parameters.Add(New SqlParameter("@HoraFin", SqlDbType.DateTime)).Value = objRampac.HoraFinal
                cmdRampac.Parameters.Add(New SqlParameter("@FormaPago", SqlDbType.Char, 15)).Value = objRampac.FormaPago
                cmdRampac.Parameters.Add(New SqlParameter("@Fecha", SqlDbType.DateTime)).Value = objRampac.Fecha
                cmdRampac.Parameters.Add(New SqlParameter("@TipoOperacion", SqlDbType.Char, 15)).Value = objRampac.TipoOperacion
                cmdRampac.Parameters.Add(New SqlParameter("@ConsecutivoRampac", SqlDbType.SmallInt)).Value = objRampac.ConsecutivoRampac
                If EncabezadoCorrupto = False Then
                    cmdRampac.Parameters.Add(New SqlParameter("@Ruta", SqlDbType.SmallInt)).Value = objRampac.Ruta
                    cmdRampac.Parameters.Add(New SqlParameter("@Autotanque", SqlDbType.SmallInt)).Value = objRampac.Autotanque
                Else
                    cmdRampac.Parameters.Add(New SqlParameter("@Ruta", SqlDbType.SmallInt)).Value = _Ruta
                    cmdRampac.Parameters.Add(New SqlParameter("@Autotanque", SqlDbType.SmallInt)).Value = _Autotanque
                End If
                'cmdRampac.Parameters.Add(New SqlParameter("@DescuentoFijo", SqlDbType.SmallInt)).Value = objRampac.DescuentoFijo    '$A1-A
                'se desactivó porque no se requiere
                cmdRampac.ExecuteNonQuery()
            Next
            Tran.Commit()

            MessageBox.Show("Pedidos descargados correctamente.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)

            _Fecha = CType(Now.Date.ToShortDateString, Date)
            _Ruta = ComboRuta1.Ruta
            _FolioP = _Folio
            _Anioatt = _AnoAtt
            _Acepto = True
            _Descarga = True

            'todo: 22/11/2004
            _Celula = ComboCelula1.Celula

        Catch ex As Exception
            Tran.Rollback()
            MessageBox.Show(ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
                cn = Nothing
            End If

            Me.Close()

        End Try

    End Sub

    Private Sub ComboCelula1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboCelula1.SelectedIndexChanged
        ComboRuta1.CargaDatos(, CType(ComboCelula1.Celula, Byte), GLOBAL_ManejarClientesPortatil, False)
        _Autotanque = 0
        If EncabezadoCorrupto Then lblAutotanque.Text = ""
    End Sub

    Private Sub ComboRuta1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboRuta1.SelectedIndexChanged
        ConsultaATRuta()
        _Autotanque = 0
        If EncabezadoCorrupto Then
            lblRuta.Text = ComboRuta1.Ruta.ToString
            _Ruta = ComboRuta1.Ruta
            lblAutotanque.Text = ""
        End If
    End Sub

    Private Sub ConsultaATRuta()
        Dim strQuery As String = "exec spConsultaATRutaDia " & _
                ComboRuta1.Ruta & ",'" & Now.Date.ToShortDateString & "','" & _
                Now.Date.ToShortDateString & " 23:59:59'"
        'Dim strQuery As String = "exec spConsultaATRutaDia " & ComboRuta1.Ruta & "," & "'12/11/2002','12/11/2002 23:59:59'"
        'Dim q As New SqlClient.SqlCommand()
        'Dim da As New SqlDataAdapter(strQuery, SigaMetClasses.LeeInfoConexion(False))
        Dim da As New SqlDataAdapter(strQuery, GLOBAL_ConString)
        Dim dt As New DataTable("ATRuta")
        Dim ds As New DataSet()
        'q.CommandText = strQuery
        'q.Connection=
        'da.Fill(ds, "ATRuta")
        da.Fill(dt)

        grdAT.CaptionText = "Lista de autotanques asignados a la " & ComboRuta1.Descripcion.ToLower
        'grdAT.DataSource = ds.Tables("ATRuta")
        grdAT.DataSource = dt

    End Sub

    Private Sub grdAT_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdAT.CurrentCellChanged
        _AnoAtt = 0 : _Folio = 0 : _Autotanque = 0
        grdAT.Select(grdAT.CurrentRowIndex)
        _AnoAtt = CType(grdAT.Item(grdAT.CurrentRowIndex, 0), Short)
        _Folio = CType(grdAT.Item(grdAT.CurrentRowIndex, 1), Integer)
        _Autotanque = CType(grdAT.Item(grdAT.CurrentRowIndex, 2), Short)

        If EncabezadoCorrupto Then lblAutotanque.Text = _Autotanque.ToString
    End Sub

    Private Sub ComboCelula_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboCelula.SelectedIndexChanged
        ComboRuta.Text = ""
        ComboRuta.CargaDatos(, CType(ComboCelula.Celula, Byte), GLOBAL_ManejarClientesPortatil, False)
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        If ComboRuta.Ruta <= 0 Then
            MessageBox.Show("Debe seleccionar la ruta que desee cargar.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Cursor = Cursors.WaitCursor
        'Dim conn As New SqlConnection(SigaMetClasses.LeeInfoConexion(False, True))
        'SE CAMBIÓ LA LLAMADA A LEEINFOCONEXION POR LA LLAMADA A LA CONN STRING GLOBAL
        Dim conn As New SqlConnection(GLOBAL_ConString)
        Dim cmd As New SqlCommand("spCCCargaArchivoRampac", conn)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dtpFecha.Value.Date
            .Parameters.Add("@Celula", SqlDbType.TinyInt).Value = CType(ComboCelula.Celula, Byte)
            .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = ComboRuta.Ruta
        End With


        Dim da As New SqlDataAdapter(cmd)

        Try
            lblTotalTelefonicos.Text = ""
            lblTotalProgramados.Text = ""
            dsDatos = New DataSet()
            Try
                da.Fill(dsDatos, "Carga")
            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            If dsDatos.Tables("Carga").Rows.Count <= 0 Then
                grdPedidos.DataSource = Nothing
                grdPedidos.CaptionText = "No se encontraron datos en la " & ComboRuta.Descripcion
                btnGenerar.Enabled = False
            Else
                grdPedidos.DataSource = dsDatos.Tables("Carga")
                grdPedidos.CaptionText = "Lista de pedidos de la " & ComboRuta.Descripcion & ". Total de registros: " & dsDatos.Tables("Carga").Rows.Count

                Dim dr As DataRow, iTel As Integer, iProg As Integer
                For Each dr In dsDatos.Tables("Carga").Rows
                    If CType(dr("TipoPedidoDescripcion"), String) = "Telefonico" Then
                        iTel += 1
                    End If
                    If CType(dr("TipoPedidoDescripcion"), String) = "Programado" Then
                        iProg += 1
                    End If
                Next
                lblTotalTelefonicos.Text = iTel.ToString
                lblTotalProgramados.Text = iProg.ToString

                btnGenerar.Enabled = True
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub btnGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerar.Click

        'If dtpFecha.Value.DayOfWeek = DayOfWeek.Sunday Then
        '    MessageBox.Show("No puede generar rutas para un dia domingo. Verifique.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If


        Dim strNombreArchivo As String = "T" & Replace(RSet(ComboRuta.Ruta.ToString, 3), " ", "0") & Replace(RSet(dtpFecha.Value.Month.ToString, 2), " ", "0") & Replace(RSet(dtpFecha.Value.Day.ToString, 2), " ", "0")
        Dim fs As New FileStream(Application.StartupPath & "\Temp.dat", FileMode.Create, FileAccess.Write, FileShare.Write)
        Dim sw As StreamWriter, dr As DataRow
        Dim strEncabezado As String = "", strLinea As String = "", strRuta As String = "", strPrecio As String = ""
        strRuta = Replace(RSet(ComboRuta.Ruta.ToString, 3), " ", "0")
        Try
            sw = New StreamWriter(fs, System.Text.Encoding.Default)

            If Not (DescuentoDirecto) Then                                   '$D1-A
                'Encabezado
                strEncabezado = Replace(Space(285), " ", "0")
                Mid(strEncabezado, 3, 3) = strRuta
                Mid(strEncabezado, 18, 3) = strRuta
                sw.Write(strEncabezado)

                'Detalle de los pedidos
                For Each dr In dsDatos.Tables("Carga").Rows
                    strLinea &= Replace(RSet(CType(dr("Cliente"), String), 10), " ", "0")
                    strLinea &= "a"
                    strLinea &= Replace(RSet(CType(dr("Producto"), String), 3), " ", "0")
                    strLinea &= "1"
                    strLinea &= LSet(CType(dr("Nombre"), String), 20)
                    strLinea &= Replace(Space(20), " ", "0")
                    strLinea &= LSet(CType(dr("CalleNombre"), String), 20)
                    strLinea &= LSet(CType(dr("NumExterior"), String), 20)
                    strLinea &= LSet(CType(dr("NumInterior"), String), 20)
                    strLinea &= LSet(CType(dr("ColoniaNombre"), String), 20)
                    strLinea &= Replace(Space(13), " ", "0")
                    sw.Write(strLinea)
                    strLinea = ""
                Next
            Else                                                        '$D1-A
                ConsultarPrecio()                                       '#P1-A
                ConsultarCliente()                                      'P#1-A
                Dim FechaProgramacion As String = Nothing                     'A


                'Encabezado
                strEncabezado = Replace(Space(298), " ", "0")           'A
                Mid(strEncabezado, 3, 3) = strRuta                      'A
                Mid(strEncabezado, 12, 1) = "A"                         'A
                Mid(strEncabezado, 18, 3) = strRuta                     'A
                Mid(strEncabezado, 21, 6) = CType(dsPrecios.Tables("CargaPrecio").Rows(0).Item(0), String)  'A

                Mid(strEncabezado, 286, 10) = "9999999999"              'A
                Mid(strEncabezado, 296, 3) = CType(dsPrecios.Tables("CargaPrecio").Rows(0).Item(1), String) 'A
                sw.Write(strEncabezado)                                 'A

                'Precio
                Dim strPrecioBase As String = ""                        'A
                strPrecioBase = CType(dsPrecios.Tables("CargaPrecio").Rows(0).Item(2), String) + CType(dsPrecios.Tables("CargaPrecio").Rows(0).Item(3), String) '$B1-A
                Dim i As Integer = 0                                    'A
                For Each dr In dsPrecios.Tables("CargaPrecio").Rows     '$B1-A
                    strPrecio = (CType(dr("NumPrecio"), String) + CType(dr("Precio"), String))  '$B1-A
                    sw.Write(strPrecio)                                 '$B1-A
                    strPrecio = ""                                      'A
                    i = i + 1                                           'A
                Next                                                    '$B1-A
                If i < 15 Then                                          'A
                    While i < 15                                        'A
                        sw.Write(strPrecioBase)                         'A
                        i = i + 1                                       'A
                    End While                                           'A
                End If                                                  'A

                'Detalle de los pedidos
                For Each dr In dsClientes.Tables("CargaCliente").Rows   '$C1-A
                    strLinea &= CType(dr("NCliente"), String)           '$C1-A
                    strLinea &= "a"                                     '$C1-A
                    strLinea &= CType(dr("CProducto"), String)          '$C1-A
                    strLinea &= "1"                                     '$C1-A
                    strLinea &= LSet(CType(dr("Nombre"), String), 40)            '$C1-A

                    'EL PROCEDIMIENTO YA DEVUELVE EL FORMATO DE DIRECCION COMPLETA
                    strLinea &= LSet(CType(dr("DireccionCompleta"), String), 80) '$C1-A

                    'FORMATO DE LA DIRECCION POR PARTES
                    'strLinea &= LSet(CType(dr("CalleNombre"), String), 20) '$C1-A
                    'strLinea &= LSet(CType(dr("ColoniaNombre"), String), 20) '$C1-A
                    'strLinea &= LSet(CType(dr("Municipio"), String), 20)   '$C1-A
                    'strLinea &= LSet(CType(dr("Estado"), String), 20)      '$C1-A

                    strLinea &= Replace(Space(13), " ", "0")                '$C1-A
                    sw.Write(strLinea)                                      '$C1-A
                    strLinea = ""                                           '$C1-A
                Next                                                        '$C1-A




            End If                                                      '$D1-A


            sw.Flush()
            If Not sw Is Nothing Then
                sw.Close()

                Environment.CurrentDirectory = Application.StartupPath
                Shell(Application.StartupPath & "\ARMA  <Temp.dat> tempOut.dat", AppWinStyle.Hide, True)
                File.Copy(Application.StartupPath & "\tempOut.dat", RutaArchivosCarga & "\" & strNombreArchivo, True)
            End If

            MessageBox.Show("Interfase generada correctamente en " & RutaArchivosCarga & "\" & strNombreArchivo, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)

            'Catch exIO As IOException
            '    MessageBox.Show("No se puede escribir o crear el archivo de interfase." + vbCrLf + _
            '                    "Por favor verifique que tenga privilegios para crear archivos.", Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Finally

        End Try
    End Sub

    Private Sub mnuConfiguracion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConfiguracion.Click
        ConsultaConfiguracion()
    End Sub

    Private Sub mnuAcercade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAcercade.Click
        Dim frmAbout As New SigaMetClasses.AcercaDe("IRampac", Application.ProductVersion, Application.StartupPath)
        frmAbout.ShowDialog()
    End Sub


End Class
'Se removió de esta forma porque ya se ha declarado en la Interfase para Metro JAGD 15-12-2004
Friend Structure sRampacCorporativo
    Public AnoAtt As Short
    Public Folio As Integer
    Public Consecutivo As Integer
    Public Cliente As Integer
    Public Litros As Decimal
    Public Importe As Decimal
    Public HoraInicio As Date
    Public HoraFinal As Date
    Public FormaPago As String
    Public Fecha As Date
    Public TipoOperacion As String
    Public ConsecutivoRampac As Short
    Public Ruta As Short
    Public Autotanque As Short
    Public DescuentoFijo As Short
End Structure