Imports System.Data.SqlClient

Public Class Liquidacion2005
    Inherits System.Windows.Forms.Form

    Private _Folio As Integer
    Private _AñoAtt As Short
    Private _Ruta As Integer
    Private _Celula As Integer
    Private _ClienteGlobal As Integer
    Private _Fecha As DateTime
    Private _Totalizador As Decimal
    Private _TotalContado As Decimal
    Private _TotalCredito As Decimal
    Private _TotalImporte As Decimal
    Private _Descarga As Boolean
    Private _CorrerDescarga As Boolean
    Private _Carburacion As Boolean
    Private _CerroLiquidacion As Boolean
    Private _PrecioAuxiliar As Decimal
    Private _PrecioVigente As Decimal
    Private _ParametroDecimal As String
    Dim _aplicaValidacionCreditocliente As Boolean
    Private _TimeOut As Integer
    Private _MultiPrecios As Boolean

    'Variables para validar el crédito de operador
    Private _maxImporteCreditoOP, _
            _maxLitrosCreditoOP As Double, _
            _operador As Integer

    Private validacionCreditoOperador As validacionCreditoOperador

    Private _DescuentoDirecto, _
            _AplicarDescuentoVariable As Boolean


    Private _PreciosValidos As New DataTable("Precios")

    Private _permitirLiquidacion As Boolean = True

    Private _serieRemisionRuta As String = String.Empty
    Private _folioNota As DocumentosBSR.SerieDocumento = New DocumentosBSR.SerieDocumento()

    'FA, para permitir la liquidación a crédito de clientes con crédito excedido
    Private _permitirLiquidacionCredito As Boolean = True

    '29112006
    Private _fechaSuministro As DateTime

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New(Optional ByVal ValidacionCreditoCliente As Boolean = False)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'TODO: validacion añadida el día 08/10/2004 asignacion en sub new
        _aplicaValidacionCreditocliente = ValidacionCreditoCliente

        'Add any initialization after the InitializeComponent() call
    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents PersistentRepository1 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dsLiquidacion2005 As System.Data.DataSet
    Friend WithEvents dtRemision As System.Data.DataTable
    Friend WithEvents dgRemision As DevExpress.XtraGrid.GridControl
    Friend WithEvents gcCelula As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRuta As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcAñoPed As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcPedido As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcCliente As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcNombre As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gdPrecio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcLitros As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcImporte As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcTipoPago As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFormaPago As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbCTotalizador As System.Windows.Forms.Label
    Friend WithEvents lbTotalizador As System.Windows.Forms.Label
    Friend WithEvents lbTipoLiquidacion As System.Windows.Forms.Label
    Friend WithEvents lbRuta As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lbCelula As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbFolio As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbAutotanque As System.Windows.Forms.Label
    Friend WithEvents lbOperador As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbFecha As System.Windows.Forms.Label
    Friend WithEvents dgNotaBlanca As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents dtNotaBlanca As System.Data.DataTable
    Friend WithEvents gcCelula1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRuta1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcAñoPed1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcPedido1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcCliente1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcNombre1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcPrecio1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcLitros1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcImporte1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcTipoPago1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFormaPago1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbUnificacion As System.Windows.Forms.Label
    Friend WithEvents txtLitros As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents txtTipoPago As DevExpress.XtraEditors.Repository.RepositoryItemPickImage
    Friend WithEvents txtFormaPago As DevExpress.XtraEditors.Repository.RepositoryItemPickImage
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents txtCliente As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents lbTotalImporte As System.Windows.Forms.Label
    Friend WithEvents lbTotalLitros As System.Windows.Forms.Label
    Friend WithEvents lbTotalCredito As System.Windows.Forms.Label
    Friend WithEvents lbLitrosCredito As System.Windows.Forms.Label
    Friend WithEvents lbTotalContado As System.Windows.Forms.Label
    Friend WithEvents lbLitrosContado As System.Windows.Forms.Label
    Friend WithEvents memNombre As DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit
    Friend WithEvents pnTotales As System.Windows.Forms.Panel
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lbTotalNotaRemision As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lbNotasBlancas As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnDocumento As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents dtDocumento As System.Data.DataTable
    Friend WithEvents DataColumn1 As System.Data.DataColumn
    Friend WithEvents DataColumn2 As System.Data.DataColumn
    Friend WithEvents DataColumn3 As System.Data.DataColumn
    Friend WithEvents DataColumn4 As System.Data.DataColumn
    Friend WithEvents DataColumn5 As System.Data.DataColumn
    Friend WithEvents DataColumn6 As System.Data.DataColumn
    Friend WithEvents DataColumn7 As System.Data.DataColumn
    Friend WithEvents DataColumn8 As System.Data.DataColumn
    Friend WithEvents DataColumn9 As System.Data.DataColumn
    Friend WithEvents DataColumn10 As System.Data.DataColumn
    Friend WithEvents DataColumn11 As System.Data.DataColumn
    Friend WithEvents DataColumn12 As System.Data.DataColumn
    Friend WithEvents DataColumn13 As System.Data.DataColumn
    Friend WithEvents PersistentRepository2 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents dtDetalle As System.Data.DataTable
    Friend WithEvents DataColumn14 As System.Data.DataColumn
    Friend WithEvents DataColumn15 As System.Data.DataColumn
    Friend WithEvents DataColumn16 As System.Data.DataColumn
    Friend WithEvents DataColumn17 As System.Data.DataColumn
    Friend WithEvents DataColumn18 As System.Data.DataColumn
    Friend WithEvents DataColumn19 As System.Data.DataColumn
    Friend WithEvents DataColumn20 As System.Data.DataColumn
    Friend WithEvents DataColumn21 As System.Data.DataColumn
    Friend WithEvents DataColumn22 As System.Data.DataColumn
    Friend WithEvents dtCliente As System.Data.DataTable
    Friend WithEvents DataColumn23 As System.Data.DataColumn
    Friend WithEvents DataColumn24 As System.Data.DataColumn
    Friend WithEvents DataColumn25 As System.Data.DataColumn
    Friend WithEvents DataColumn26 As System.Data.DataColumn
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents ContextMenu2 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents gcDireccion1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbPrecioAuxiliar As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTotalCheques As System.Windows.Forms.Label
    Friend WithEvents lbLitrosCheques As System.Windows.Forms.Label
    Friend WithEvents gridDocumento As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView4 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents DataColumn27 As System.Data.DataColumn
    Friend WithEvents txtLitros1 As DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit
    Friend WithEvents cboPrecio As DevExpress.XtraEditors.Repository.RepositoryItemComboBox
    Friend WithEvents gcNota As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcNota1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents txtNota As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents lblTotalObsequio As System.Windows.Forms.Label
    Friend WithEvents lblLitrosObsequio As System.Windows.Forms.Label
    Friend WithEvents lbObsequio As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents lblImporteAutoCarb As System.Windows.Forms.Label
    Friend WithEvents lblImporteObsequio As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents lblObsequio As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lblAutoCarb As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents pnObsequio As System.Windows.Forms.Panel
    Friend WithEvents DataColumn28 As System.Data.DataColumn
    Friend WithEvents DataColumn29 As System.Data.DataColumn
    Friend WithEvents lbLitrosTPV As System.Windows.Forms.Label
    Friend WithEvents lbTotalTPV As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents DataColumn30 As System.Data.DataColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Liquidacion2005))
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.dgRemision = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository1 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.txtLitros = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.txtTipoPago = New DevExpress.XtraEditors.Repository.RepositoryItemPickImage()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.txtFormaPago = New DevExpress.XtraEditors.Repository.RepositoryItemPickImage()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.txtCliente = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.ContextMenu2 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.memNombre = New DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.txtLitros1 = New DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit()
        Me.cboPrecio = New DevExpress.XtraEditors.Repository.RepositoryItemComboBox()
        Me.txtNota = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcNota = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCliente = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcNombre = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCelula = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRuta = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcAñoPed = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcPedido = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcLitros = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdPrecio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcImporte = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcTipoPago = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFormaPago = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.dsLiquidacion2005 = New System.Data.DataSet()
        Me.dtRemision = New System.Data.DataTable()
        Me.dtNotaBlanca = New System.Data.DataTable()
        Me.dtDocumento = New System.Data.DataTable()
        Me.DataColumn1 = New System.Data.DataColumn()
        Me.DataColumn2 = New System.Data.DataColumn()
        Me.DataColumn3 = New System.Data.DataColumn()
        Me.DataColumn4 = New System.Data.DataColumn()
        Me.DataColumn5 = New System.Data.DataColumn()
        Me.DataColumn6 = New System.Data.DataColumn()
        Me.DataColumn7 = New System.Data.DataColumn()
        Me.DataColumn8 = New System.Data.DataColumn()
        Me.DataColumn9 = New System.Data.DataColumn()
        Me.DataColumn10 = New System.Data.DataColumn()
        Me.DataColumn11 = New System.Data.DataColumn()
        Me.DataColumn12 = New System.Data.DataColumn()
        Me.DataColumn13 = New System.Data.DataColumn()
        Me.DataColumn30 = New System.Data.DataColumn()
        Me.dtDetalle = New System.Data.DataTable()
        Me.DataColumn14 = New System.Data.DataColumn()
        Me.DataColumn15 = New System.Data.DataColumn()
        Me.DataColumn16 = New System.Data.DataColumn()
        Me.DataColumn17 = New System.Data.DataColumn()
        Me.DataColumn18 = New System.Data.DataColumn()
        Me.DataColumn19 = New System.Data.DataColumn()
        Me.DataColumn20 = New System.Data.DataColumn()
        Me.DataColumn21 = New System.Data.DataColumn()
        Me.DataColumn22 = New System.Data.DataColumn()
        Me.dtCliente = New System.Data.DataTable()
        Me.DataColumn23 = New System.Data.DataColumn()
        Me.DataColumn24 = New System.Data.DataColumn()
        Me.DataColumn25 = New System.Data.DataColumn()
        Me.DataColumn26 = New System.Data.DataColumn()
        Me.DataColumn27 = New System.Data.DataColumn()
        Me.DataColumn28 = New System.Data.DataColumn()
        Me.DataColumn29 = New System.Data.DataColumn()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbPrecioAuxiliar = New System.Windows.Forms.Label()
        Me.lbUnificacion = New System.Windows.Forms.Label()
        Me.lbCTotalizador = New System.Windows.Forms.Label()
        Me.lbTotalizador = New System.Windows.Forms.Label()
        Me.lbTipoLiquidacion = New System.Windows.Forms.Label()
        Me.lbRuta = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lbCelula = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbFolio = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbAutotanque = New System.Windows.Forms.Label()
        Me.lbOperador = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbFecha = New System.Windows.Forms.Label()
        Me.dgNotaBlanca = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcNota1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCliente1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcNombre1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCelula1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRuta1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcAñoPed1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcPedido1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcLitros1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcPrecio1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcImporte1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcTipoPago1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFormaPago1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcDireccion1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lbTotalImporte = New System.Windows.Forms.Label()
        Me.lbTotalLitros = New System.Windows.Forms.Label()
        Me.lbTotalCredito = New System.Windows.Forms.Label()
        Me.lbLitrosCredito = New System.Windows.Forms.Label()
        Me.lbTotalContado = New System.Windows.Forms.Label()
        Me.lbLitrosContado = New System.Windows.Forms.Label()
        Me.pnTotales = New System.Windows.Forms.Panel()
        Me.lbTotal = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.lbTotalNotaRemision = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.lbNotasBlancas = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnDocumento = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.PersistentRepository2 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblTotalCheques = New System.Windows.Forms.Label()
        Me.lbLitrosCheques = New System.Windows.Forms.Label()
        Me.gridDocumento = New DevExpress.XtraGrid.GridControl()
        Me.GridView4 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.lbObsequio = New System.Windows.Forms.Label()
        Me.lblTotalObsequio = New System.Windows.Forms.Label()
        Me.lblLitrosObsequio = New System.Windows.Forms.Label()
        Me.pnObsequio = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblImporteAutoCarb = New System.Windows.Forms.Label()
        Me.lblImporteObsequio = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.lblObsequio = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.lblAutoCarb = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lbLitrosTPV = New System.Windows.Forms.Label()
        Me.lbTotalTPV = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.dgRemision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLitros, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtTipoPago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFormaPago, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.memNombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtLitros1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cboPrecio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtNota, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsLiquidacion2005, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtRemision, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtNotaBlanca, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dgNotaBlanca, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnTotales.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnObsequio.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = -1
        Me.MenuItem1.Text = "Liquidación"
        '
        'dgRemision
        '
        Me.dgRemision.EditorsRepository = Me.PersistentRepository1
        Me.dgRemision.Location = New System.Drawing.Point(4, 92)
        Me.dgRemision.MainView = Me.GridView1
        Me.dgRemision.Name = "dgRemision"
        Me.dgRemision.Size = New System.Drawing.Size(940, 256)
        Me.dgRemision.Styles.AddReplace("FooterPanel", New DevExpress.Utils.ViewStyle("FooterPanel", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.PeachPuff, System.Drawing.SystemColors.ControlText))
        Me.dgRemision.Styles.AddReplace("FilterButton", New DevExpress.Utils.ViewStyle("FilterButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.RoyalBlue, System.Drawing.Color.White))
        Me.dgRemision.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(189, Byte), CType(221, Byte), CType(194, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgRemision.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.SteelBlue, System.Drawing.Color.White))
        Me.dgRemision.Styles.AddReplace("Empty", New DevExpress.Utils.ViewStyle("Empty", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window))
        Me.dgRemision.Styles.AddReplace("Style3", New DevExpress.Utils.ViewStyle("Style3", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(232, Byte), CType(201, Byte), CType(200, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgRemision.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", ((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.Khaki, System.Drawing.Color.Black))
        Me.dgRemision.Styles.AddReplace("Row", New DevExpress.Utils.ViewStyle("Row", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", DevExpress.Utils.StyleOptions.StyleEnabled, True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.White, System.Drawing.SystemColors.WindowText))
        Me.dgRemision.Styles.AddReplace("FilterPanel", New DevExpress.Utils.ViewStyle("FilterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkSeaGreen, System.Drawing.SystemColors.ControlLightLight))
        Me.dgRemision.Styles.AddReplace("Style4", New DevExpress.Utils.ViewStyle("Style4", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(217, Byte), CType(230, Byte), CType(240, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgRemision.TabIndex = 94
        '
        'PersistentRepository1
        '
        Me.PersistentRepository1.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.txtLitros, Me.txtTipoPago, Me.txtFormaPago, Me.txtCliente, Me.memNombre, Me.RepositoryItemTextEdit1, Me.RepositoryItemTextEdit2, Me.txtLitros1, Me.cboPrecio, Me.txtNota})
        '
        'txtLitros
        '
        Me.txtLitros.Name = "txtLitros"
        Me.txtLitros.Properties.AllowFocused = False
        Me.txtLitros.Properties.AutoHeight = False
        Me.txtLitros.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtLitros.Properties.Format = CType(resources.GetObject("txtLitros.Properties.Format"), System.Globalization.NumberFormatInfo)
        Me.txtLitros.Properties.FormatString = "#,##"
        Me.txtLitros.Properties.MaskData.BeepOnError = True
        Me.txtLitros.Properties.MaskData.Blank = " "
        Me.txtLitros.Properties.MaskData.EditMask = "999999"
        Me.txtLitros.Properties.MaskData.MaskType = DevExpress.XtraEditors.Controls.MaskType.Simple
        Me.txtLitros.Properties.MaskData.SaveLiteral = False
        Me.txtLitros.Properties.Style = New DevExpress.Utils.ViewStyle("ControlStyle", "BaseEdit", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                        Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                        Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                        Or DevExpress.Utils.StyleOptions.UseFont) _
                        Or DevExpress.Utils.StyleOptions.UseForeColor) _
                        Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                        Or DevExpress.Utils.StyleOptions.UseImage) _
                        Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                        Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.WindowText)
        '
        'txtTipoPago
        '
        Me.txtTipoPago.Name = "txtTipoPago"
        Me.txtTipoPago.Properties.AllowFocused = False
        Me.txtTipoPago.Properties.AutoHeight = False
        Me.txtTipoPago.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtTipoPago.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtTipoPago.Properties.ButtonsBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtTipoPago.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtTipoPago.Properties.ImmediatePopup = False
        Me.txtTipoPago.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("CONTADO", "CONTADO", 0))
        Me.txtTipoPago.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("CREDITO", "CREDITO", 1))
        Me.txtTipoPago.Properties.LargeImages = Me.ImageList1
        Me.txtTipoPago.Properties.ShowPopupShadow = False
        Me.txtTipoPago.Properties.SmallImages = Me.ImageList1
        Me.txtTipoPago.Properties.UseCtrlScroll = True
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Silver
        '
        'txtFormaPago
        '
        Me.txtFormaPago.Name = "txtFormaPago"
        Me.txtFormaPago.Properties.AllowFocused = False
        Me.txtFormaPago.Properties.AutoHeight = False
        Me.txtFormaPago.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtFormaPago.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtFormaPago.Properties.ButtonsBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtFormaPago.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFormaPago.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("Efectivo", 1, 0))
        Me.txtFormaPago.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("Credito Empresa", 2, 1))
        Me.txtFormaPago.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("Credito Operador", 3, 2))
        Me.txtFormaPago.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("Tarjeta Credito", 4, 3))
        Me.txtFormaPago.Properties.LargeImages = Me.ImageList2
        Me.txtFormaPago.Properties.ShowPopupShadow = False
        Me.txtFormaPago.Properties.SmallImages = Me.ImageList2
        Me.txtFormaPago.Properties.UseCtrlScroll = True
        '
        'ImageList2
        '
        Me.ImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList2.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Silver
        '
        'txtCliente
        '
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Properties.AllowFocused = False
        Me.txtCliente.Properties.AutoHeight = False
        Me.txtCliente.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtCliente.Properties.ContextMenu = Me.ContextMenu2
        Me.txtCliente.Properties.Format = CType(resources.GetObject("txtCliente.Properties.Format"), System.Globalization.NumberFormatInfo)
        Me.txtCliente.Properties.FormatString = "#"
        Me.txtCliente.Properties.MaskData.BeepOnError = True
        Me.txtCliente.Properties.MaskData.Blank = " "
        Me.txtCliente.Properties.MaskData.EditMask = "999999999"
        Me.txtCliente.Properties.MaskData.MaskType = DevExpress.XtraEditors.Controls.MaskType.Simple
        Me.txtCliente.Properties.MaskData.SaveLiteral = False
        '
        'ContextMenu2
        '
        Me.ContextMenu2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "Busqueda de un contrato"
        '
        'memNombre
        '
        Me.memNombre.Name = "memNombre"
        Me.memNombre.Properties.AcceptsReturn = False
        Me.memNombre.Properties.AllowFocused = False
        Me.memNombre.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.memNombre.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.memNombre.Properties.ReadOnly = True
        Me.memNombre.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        Me.RepositoryItemTextEdit1.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit1.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        Me.RepositoryItemTextEdit2.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit2.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'txtLitros1
        '
        Me.txtLitros1.Name = "txtLitros1"
        Me.txtLitros1.Properties.AllowFocused = False
        Me.txtLitros1.Properties.AutoHeight = False
        Me.txtLitros1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtLitros1.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton())
        Me.txtLitros1.Properties.ButtonsBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtLitros1.Properties.Format = CType(resources.GetObject("txtLitros1.Properties.Format"), System.Globalization.NumberFormatInfo)
        Me.txtLitros1.Properties.Increment = New Decimal(New Integer() {0, 0, 0, 0})
        Me.txtLitros1.Properties.UseCtrlIncrement = True
        '
        'cboPrecio
        '
        Me.cboPrecio.Name = "cboPrecio"
        Me.cboPrecio.Properties.AllowFocused = False
        Me.cboPrecio.Properties.AutoHeight = False
        Me.cboPrecio.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.cboPrecio.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.cboPrecio.Properties.ButtonsBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.cboPrecio.Properties.Format = CType(resources.GetObject("cboPrecio.Properties.Format"), System.Globalization.NumberFormatInfo)
        Me.cboPrecio.Properties.FormatString = "#,##.00"
        Me.cboPrecio.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.DoubleClick
        Me.cboPrecio.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.cboPrecio.Properties.UseCtrlScroll = True
        '
        'txtNota
        '
        Me.txtNota.Name = "txtNota"
        Me.txtNota.Properties.AllowFocused = False
        Me.txtNota.Properties.AutoHeight = False
        Me.txtNota.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtNota.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNota.Properties.Format = CType(resources.GetObject("txtNota.Properties.Format"), System.Globalization.NumberFormatInfo)
        Me.txtNota.Properties.FormatString = "#"
        '
        'GridView1
        '
        Me.GridView1.BehaviorOptions = ((((((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowFilter Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.Editable) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnterMoveNextColumn) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoMoveRowFocus) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.RowAutoHeight)
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcNota, Me.gcCliente, Me.gcNombre, Me.gcCelula, Me.gcRuta, Me.gcAñoPed, Me.gcPedido, Me.gcLitros, Me.gdPrecio, Me.gcImporte, Me.gcTipoPago, Me.gcFormaPago})
        Me.GridView1.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.NotEqual, Nothing, "Style4", 0, Nothing, Me.gcLitros, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style3", "CREDITO", Nothing, Me.gcTipoPago, True)})
        Me.GridView1.GroupPanelText = "Pedidos de las notas de remison y la tarjeta rampac"
        Me.GridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridSummaryItem(DevExpress.XtraGrid.SummaryItemType.Sum, "Litros", Me.gcLitros, ""), New DevExpress.XtraGrid.GridSummaryItem(DevExpress.XtraGrid.SummaryItemType.Sum, "Importe", Me.gcImporte, "")})
        Me.GridView1.Name = "GridView1"
        Me.GridView1.RowHeight = 29
        Me.GridView1.VertScrollTipFieldName = Nothing
        Me.GridView1.ViewOptions = ((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFilterPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFooter) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle)
        '
        'gcNota
        '
        Me.gcNota.Caption = "Nota"
        Me.gcNota.ColumnEdit = Me.txtNota
        Me.gcNota.FieldName = "Codigo"
        Me.gcNota.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcNota.Name = "gcNota"
        Me.gcNota.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcNota.VisibleIndex = 0
        '
        'gcCliente
        '
        Me.gcCliente.Caption = "Contrato"
        Me.gcCliente.FieldName = "Cliente"
        Me.gcCliente.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcCliente.Name = "gcCliente"
        Me.gcCliente.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcCliente.VisibleIndex = 1
        '
        'gcNombre
        '
        Me.gcNombre.Caption = "Nombre"
        Me.gcNombre.ColumnEdit = Me.memNombre
        Me.gcNombre.FieldName = "Nombre"
        Me.gcNombre.Name = "gcNombre"
        Me.gcNombre.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcNombre.VisibleIndex = 2
        Me.gcNombre.Width = 151
        '
        'gcCelula
        '
        Me.gcCelula.Caption = "Celula"
        Me.gcCelula.FieldName = "Celula"
        Me.gcCelula.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcCelula.Name = "gcCelula"
        Me.gcCelula.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcCelula.VisibleIndex = 3
        Me.gcCelula.Width = 49
        '
        'gcRuta
        '
        Me.gcRuta.Caption = "Ruta"
        Me.gcRuta.FieldName = "Ruta"
        Me.gcRuta.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcRuta.Name = "gcRuta"
        Me.gcRuta.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRuta.VisibleIndex = 4
        Me.gcRuta.Width = 49
        '
        'gcAñoPed
        '
        Me.gcAñoPed.Caption = "Año"
        Me.gcAñoPed.FieldName = "AñoPed"
        Me.gcAñoPed.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcAñoPed.Name = "gcAñoPed"
        Me.gcAñoPed.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcAñoPed.VisibleIndex = 5
        Me.gcAñoPed.Width = 55
        '
        'gcPedido
        '
        Me.gcPedido.Caption = "Pedido"
        Me.gcPedido.FieldName = "Pedido"
        Me.gcPedido.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcPedido.Name = "gcPedido"
        Me.gcPedido.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcPedido.VisibleIndex = 6
        Me.gcPedido.Width = 69
        '
        'gcLitros
        '
        Me.gcLitros.Caption = "Litros"
        Me.gcLitros.ColumnEdit = Me.txtLitros1
        Me.gcLitros.FieldName = "Litros"
        Me.gcLitros.FormatString = "#,##.00"
        Me.gcLitros.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcLitros.Name = "gcLitros"
        Me.gcLitros.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcLitros.SummaryItem.SummaryType = DevExpress.XtraGrid.SummaryItemType.Sum
        Me.gcLitros.VisibleIndex = 7
        Me.gcLitros.Width = 73
        '
        'gdPrecio
        '
        Me.gdPrecio.Caption = "Precio"
        Me.gdPrecio.ColumnEdit = Me.cboPrecio
        Me.gdPrecio.FieldName = "Precio"
        Me.gdPrecio.FormatString = "#,##.00"
        Me.gdPrecio.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gdPrecio.Name = "gdPrecio"
        Me.gdPrecio.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gdPrecio.StyleName = "Style1"
        Me.gdPrecio.VisibleIndex = 8
        Me.gdPrecio.Width = 58
        '
        'gcImporte
        '
        Me.gcImporte.Caption = "Importe"
        Me.gcImporte.FieldName = "Importe"
        Me.gcImporte.FormatString = "$ #,##.00"
        Me.gcImporte.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcImporte.Name = "gcImporte"
        Me.gcImporte.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcImporte.StyleName = "Style2"
        Me.gcImporte.SummaryItem.SummaryType = DevExpress.XtraGrid.SummaryItemType.Sum
        Me.gcImporte.VisibleIndex = 9
        Me.gcImporte.Width = 95
        '
        'gcTipoPago
        '
        Me.gcTipoPago.Caption = "Tipo"
        Me.gcTipoPago.ColumnEdit = Me.txtTipoPago
        Me.gcTipoPago.FieldName = "TipoPago"
        Me.gcTipoPago.Name = "gcTipoPago"
        Me.gcTipoPago.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcTipoPago.VisibleIndex = 10
        Me.gcTipoPago.Width = 105
        '
        'gcFormaPago
        '
        Me.gcFormaPago.Caption = "Pago"
        Me.gcFormaPago.ColumnEdit = Me.txtFormaPago
        Me.gcFormaPago.FieldName = "FormaPago"
        Me.gcFormaPago.Name = "gcFormaPago"
        Me.gcFormaPago.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcFormaPago.VisibleIndex = 11
        Me.gcFormaPago.Width = 143
        '
        'dsLiquidacion2005
        '
        Me.dsLiquidacion2005.DataSetName = "dsLiquidacion2005"
        Me.dsLiquidacion2005.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.dsLiquidacion2005.Tables.AddRange(New System.Data.DataTable() {Me.dtRemision, Me.dtNotaBlanca, Me.dtDocumento, Me.dtDetalle, Me.dtCliente})
        '
        'dtRemision
        '
        Me.dtRemision.TableName = "Remision"
        '
        'dtNotaBlanca
        '
        Me.dtNotaBlanca.TableName = "NotaBlanca"
        '
        'dtDocumento
        '
        Me.dtDocumento.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn1, Me.DataColumn2, Me.DataColumn3, Me.DataColumn4, Me.DataColumn5, Me.DataColumn6, Me.DataColumn7, Me.DataColumn8, Me.DataColumn9, Me.DataColumn10, Me.DataColumn11, Me.DataColumn12, Me.DataColumn13, Me.DataColumn30})
        Me.dtDocumento.TableName = "Documento"
        '
        'DataColumn1
        '
        Me.DataColumn1.ColumnName = "Banco"
        Me.DataColumn1.DataType = GetType(System.Int64)
        '
        'DataColumn2
        '
        Me.DataColumn2.ColumnName = "Cheque"
        '
        'DataColumn3
        '
        Me.DataColumn3.ColumnName = "FCheque"
        Me.DataColumn3.DataType = GetType(System.DateTime)
        '
        'DataColumn4
        '
        Me.DataColumn4.ColumnName = "Cuenta"
        '
        'DataColumn5
        '
        Me.DataColumn5.ColumnName = "Monto"
        Me.DataColumn5.DataType = GetType(System.Decimal)
        '
        'DataColumn6
        '
        Me.DataColumn6.ColumnName = "Disponible"
        Me.DataColumn6.DataType = GetType(System.Decimal)
        '
        'DataColumn7
        '
        Me.DataColumn7.ColumnName = "DesBanco"
        '
        'DataColumn8
        '
        Me.DataColumn8.ColumnName = "Llave"
        Me.DataColumn8.DataType = GetType(System.Int64)
        '
        'DataColumn9
        '
        Me.DataColumn9.ColumnName = "Tipo"
        Me.DataColumn9.DataType = GetType(System.Int64)
        '
        'DataColumn10
        '
        Me.DataColumn10.ColumnName = "TipoDes"
        '
        'DataColumn11
        '
        Me.DataColumn11.ColumnName = "Cliente"
        Me.DataColumn11.DataType = GetType(System.Int64)
        '
        'DataColumn12
        '
        Me.DataColumn12.ColumnName = "Nombre"
        '
        'DataColumn13
        '
        Me.DataColumn13.ColumnName = "PosFechado"
        '
        'DataColumn30
        '
        Me.DataColumn30.Caption = "TPV"
        Me.DataColumn30.ColumnName = "TPV"
        Me.DataColumn30.DataType = GetType(System.Boolean)
        '
        'dtDetalle
        '
        Me.dtDetalle.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn14, Me.DataColumn15, Me.DataColumn16, Me.DataColumn17, Me.DataColumn18, Me.DataColumn19, Me.DataColumn20, Me.DataColumn21, Me.DataColumn22})
        Me.dtDetalle.TableName = "Detalle"
        '
        'DataColumn14
        '
        Me.DataColumn14.ColumnName = "Cliente"
        '
        'DataColumn15
        '
        Me.DataColumn15.ColumnName = "Monto"
        Me.DataColumn15.DataType = GetType(System.Decimal)
        '
        'DataColumn16
        '
        Me.DataColumn16.ColumnName = "Tipo"
        Me.DataColumn16.DataType = GetType(System.Int64)
        '
        'DataColumn17
        '
        Me.DataColumn17.ColumnName = "Destipo"
        '
        'DataColumn18
        '
        Me.DataColumn18.ColumnName = "Banco"
        Me.DataColumn18.DataType = GetType(System.Int64)
        '
        'DataColumn19
        '
        Me.DataColumn19.ColumnName = "Cheque"
        '
        'DataColumn20
        '
        Me.DataColumn20.ColumnName = "Cuenta"
        '
        'DataColumn21
        '
        Me.DataColumn21.ColumnName = "NombreBanco"
        '
        'DataColumn22
        '
        Me.DataColumn22.ColumnName = "Nombre"
        '
        'dtCliente
        '
        Me.dtCliente.Columns.AddRange(New System.Data.DataColumn() {Me.DataColumn23, Me.DataColumn24, Me.DataColumn25, Me.DataColumn26, Me.DataColumn27, Me.DataColumn28, Me.DataColumn29})
        Me.dtCliente.TableName = "Cliente"
        '
        'DataColumn23
        '
        Me.DataColumn23.ColumnName = "Monto"
        Me.DataColumn23.DataType = GetType(System.Decimal)
        '
        'DataColumn24
        '
        Me.DataColumn24.ColumnName = "Nombre"
        '
        'DataColumn25
        '
        Me.DataColumn25.ColumnName = "Disponible"
        Me.DataColumn25.DataType = GetType(System.Decimal)
        '
        'DataColumn26
        '
        Me.DataColumn26.ColumnName = "Cliente"
        Me.DataColumn26.DataType = GetType(System.Int64)
        '
        'DataColumn27
        '
        Me.DataColumn27.Caption = "Litros"
        Me.DataColumn27.ColumnName = "Litros"
        Me.DataColumn27.DataType = GetType(System.Double)
        '
        'DataColumn28
        '
        Me.DataColumn28.ColumnName = "Tipo"
        Me.DataColumn28.DataType = GetType(System.Int16)
        '
        'DataColumn29
        '
        Me.DataColumn29.ColumnName = "DesTipo"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbPrecioAuxiliar, Me.lbUnificacion, Me.lbCTotalizador, Me.lbTotalizador, Me.lbTipoLiquidacion, Me.lbRuta, Me.Label6, Me.lbCelula, Me.Label4, Me.lbFolio, Me.Label3, Me.lbAutotanque, Me.lbOperador, Me.Label2, Me.Label1, Me.lbFecha})
        Me.Panel1.Location = New System.Drawing.Point(4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(940, 84)
        Me.Panel1.TabIndex = 95
        '
        'lbPrecioAuxiliar
        '
        Me.lbPrecioAuxiliar.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPrecioAuxiliar.ForeColor = System.Drawing.Color.Firebrick
        Me.lbPrecioAuxiliar.Location = New System.Drawing.Point(544, 60)
        Me.lbPrecioAuxiliar.Name = "lbPrecioAuxiliar"
        Me.lbPrecioAuxiliar.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lbPrecioAuxiliar.Size = New System.Drawing.Size(384, 16)
        Me.lbPrecioAuxiliar.TabIndex = 109
        Me.lbPrecioAuxiliar.Text = "Teclee F4 para asignar a el cliente el otro precio vigente"
        '
        'lbUnificacion
        '
        Me.lbUnificacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUnificacion.ForeColor = System.Drawing.Color.Red
        Me.lbUnificacion.Location = New System.Drawing.Point(648, 8)
        Me.lbUnificacion.Name = "lbUnificacion"
        Me.lbUnificacion.Size = New System.Drawing.Size(280, 16)
        Me.lbUnificacion.TabIndex = 108
        Me.lbUnificacion.Text = "VARIAS REMISIONES EN UN MISMO CONTRATO"
        Me.lbUnificacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbUnificacion.Visible = False
        '
        'lbCTotalizador
        '
        Me.lbCTotalizador.AutoSize = True
        Me.lbCTotalizador.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCTotalizador.Location = New System.Drawing.Point(535, 43)
        Me.lbCTotalizador.Name = "lbCTotalizador"
        Me.lbCTotalizador.Size = New System.Drawing.Size(165, 16)
        Me.lbCTotalizador.TabIndex = 107
        Me.lbCTotalizador.Text = "Totalizador de Bascula :"
        Me.lbCTotalizador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbTotalizador
        '
        Me.lbTotalizador.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalizador.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.lbTotalizador.Location = New System.Drawing.Point(695, 41)
        Me.lbTotalizador.Name = "lbTotalizador"
        Me.lbTotalizador.Size = New System.Drawing.Size(120, 16)
        Me.lbTotalizador.TabIndex = 106
        Me.lbTotalizador.Text = "0.00"
        Me.lbTotalizador.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbTipoLiquidacion
        '
        Me.lbTipoLiquidacion.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTipoLiquidacion.ForeColor = System.Drawing.Color.Red
        Me.lbTipoLiquidacion.Location = New System.Drawing.Point(331, 43)
        Me.lbTipoLiquidacion.Name = "lbTipoLiquidacion"
        Me.lbTipoLiquidacion.Size = New System.Drawing.Size(197, 16)
        Me.lbTipoLiquidacion.TabIndex = 105
        Me.lbTipoLiquidacion.Text = "Liquidación MANUAL"
        Me.lbTipoLiquidacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbRuta
        '
        Me.lbRuta.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRuta.ForeColor = System.Drawing.Color.Blue
        Me.lbRuta.Location = New System.Drawing.Point(296, 43)
        Me.lbRuta.Name = "lbRuta"
        Me.lbRuta.Size = New System.Drawing.Size(32, 16)
        Me.lbRuta.TabIndex = 104
        Me.lbRuta.Text = "123"
        Me.lbRuta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(248, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(45, 16)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "Ruta :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbCelula
        '
        Me.lbCelula.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCelula.ForeColor = System.Drawing.Color.Blue
        Me.lbCelula.Location = New System.Drawing.Point(200, 43)
        Me.lbCelula.Name = "lbCelula"
        Me.lbCelula.Size = New System.Drawing.Size(48, 16)
        Me.lbCelula.TabIndex = 102
        Me.lbCelula.Text = "1234456789"
        Me.lbCelula.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(144, 43)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 16)
        Me.Label4.TabIndex = 101
        Me.Label4.Text = "Celula :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbFolio
        '
        Me.lbFolio.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFolio.ForeColor = System.Drawing.Color.Green
        Me.lbFolio.Location = New System.Drawing.Point(52, 43)
        Me.lbFolio.Name = "lbFolio"
        Me.lbFolio.Size = New System.Drawing.Size(96, 16)
        Me.lbFolio.TabIndex = 100
        Me.lbFolio.Text = "1234456789"
        Me.lbFolio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 16)
        Me.Label3.TabIndex = 99
        Me.Label3.Text = "Folio:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbAutotanque
        '
        Me.lbAutotanque.AutoSize = True
        Me.lbAutotanque.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAutotanque.ForeColor = System.Drawing.Color.Blue
        Me.lbAutotanque.Location = New System.Drawing.Point(104, 8)
        Me.lbAutotanque.Name = "lbAutotanque"
        Me.lbAutotanque.Size = New System.Drawing.Size(22, 16)
        Me.lbAutotanque.TabIndex = 94
        Me.lbAutotanque.Text = "45"
        Me.lbAutotanque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbOperador
        '
        Me.lbOperador.AutoSize = True
        Me.lbOperador.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbOperador.ForeColor = System.Drawing.Color.Blue
        Me.lbOperador.Location = New System.Drawing.Point(104, 25)
        Me.lbOperador.Name = "lbOperador"
        Me.lbOperador.Size = New System.Drawing.Size(96, 16)
        Me.lbOperador.TabIndex = 95
        Me.lbOperador.Text = "Pancho Perez"
        Me.lbOperador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 16)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Operador:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "Autotanque:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbFecha
        '
        Me.lbFecha.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFecha.Location = New System.Drawing.Point(8, 62)
        Me.lbFecha.Name = "lbFecha"
        Me.lbFecha.Size = New System.Drawing.Size(232, 16)
        Me.lbFecha.TabIndex = 98
        Me.lbFecha.Text = "FECHA"
        Me.lbFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dgNotaBlanca
        '
        Me.dgNotaBlanca.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.dgNotaBlanca.EditorsRepository = Me.PersistentRepository1
        Me.dgNotaBlanca.Location = New System.Drawing.Point(4, 372)
        Me.dgNotaBlanca.MainView = Me.GridView2
        Me.dgNotaBlanca.Name = "dgNotaBlanca"
        Me.dgNotaBlanca.Size = New System.Drawing.Size(1136, 320)
        Me.dgNotaBlanca.Styles.AddReplace("Style5", New DevExpress.Utils.ViewStyle("Style5", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.White, System.Drawing.SystemColors.WindowText))
        Me.dgNotaBlanca.Styles.AddReplace("FooterPanel", New DevExpress.Utils.ViewStyle("FooterPanel", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.PeachPuff, System.Drawing.SystemColors.ControlText))
        Me.dgNotaBlanca.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(189, Byte), CType(221, Byte), CType(194, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgNotaBlanca.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Firebrick, System.Drawing.Color.White))
        Me.dgNotaBlanca.Styles.AddReplace("Empty", New DevExpress.Utils.ViewStyle("Empty", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window))
        Me.dgNotaBlanca.Styles.AddReplace("Style3", New DevExpress.Utils.ViewStyle("Style3", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(232, Byte), CType(201, Byte), CType(200, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgNotaBlanca.Styles.AddReplace("ColumnFilterButton", New DevExpress.Utils.ViewStyle("ColumnFilterButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.RoyalBlue, System.Drawing.Color.White))
        Me.dgNotaBlanca.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.Khaki, System.Drawing.SystemColors.WindowText))
        Me.dgNotaBlanca.Styles.AddReplace("GroupFooter", New DevExpress.Utils.ViewStyle("GroupFooter", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.PeachPuff, System.Drawing.SystemColors.ControlText))
        Me.dgNotaBlanca.Styles.AddReplace("Row", New DevExpress.Utils.ViewStyle("Row", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", DevExpress.Utils.StyleOptions.StyleEnabled, True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.White, System.Drawing.SystemColors.WindowText))
        Me.dgNotaBlanca.Styles.AddReplace("FilterPanel", New DevExpress.Utils.ViewStyle("FilterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkSeaGreen, System.Drawing.SystemColors.ControlLightLight))
        Me.dgNotaBlanca.Styles.AddReplace("Style4", New DevExpress.Utils.ViewStyle("Style4", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(217, Byte), CType(230, Byte), CType(240, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgNotaBlanca.Styles.AddReplace("Style6", New DevExpress.Utils.ViewStyle("Style6", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.Yellow, System.Drawing.Color.Black))
        Me.dgNotaBlanca.TabIndex = 96
        '
        'GridView2
        '
        Me.GridView2.BehaviorOptions = ((((((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowFilter Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.Editable) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnterMoveNextColumn) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoMoveRowFocus) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.RowAutoHeight)
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcNota1, Me.gcCliente1, Me.gcNombre1, Me.gcCelula1, Me.gcRuta1, Me.gcAñoPed1, Me.gcPedido1, Me.gcLitros1, Me.gcPrecio1, Me.gcImporte1, Me.gcTipoPago1, Me.gcFormaPago1, Me.gcDireccion1})
        Me.GridView2.DefaultEdit = Me.RepositoryItemTextEdit2
        Me.GridView2.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.NotEqual, Nothing, "Style4", 0, Nothing, Me.gcLitros1, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style3", "CREDITO", Nothing, Me.gcTipoPago1, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style5", 0, Nothing, Me.gcCliente1, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.NotEqual, Nothing, "Style6", Nothing, Nothing, Me.gcCelula1, False), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.NotEqual, Nothing, "Style6", Nothing, Nothing, Me.gcRuta1, False), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style5", 0, Nothing, Me.gcCliente1, True)})
        Me.GridView2.GroupPanelText = "Notas blancas, boletines y otros cargos"
        Me.GridView2.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridSummaryItem(DevExpress.XtraGrid.SummaryItemType.Sum, "Litros", Me.gcLitros1, ""), New DevExpress.XtraGrid.GridSummaryItem(DevExpress.XtraGrid.SummaryItemType.Sum, "Importe", Me.gcImporte1, "")})
        Me.GridView2.Name = "GridView2"
        Me.GridView2.RowHeight = 29
        Me.GridView2.ViewOptions = ((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFilterPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFooter) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle)
        '
        'gcNota1
        '
        Me.gcNota1.Caption = "Nota"
        Me.gcNota1.ColumnEdit = Me.txtNota
        Me.gcNota1.FieldName = "Codigo"
        Me.gcNota1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcNota1.Name = "gcNota1"
        Me.gcNota1.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcNota1.VisibleIndex = 0
        '
        'gcCliente1
        '
        Me.gcCliente1.Caption = "Contrato"
        Me.gcCliente1.ColumnEdit = Me.txtCliente
        Me.gcCliente1.FieldName = "Cliente"
        Me.gcCliente1.FormatString = "#"
        Me.gcCliente1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcCliente1.Name = "gcCliente1"
        Me.gcCliente1.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcCliente1.VisibleIndex = 1
        '
        'gcNombre1
        '
        Me.gcNombre1.Caption = "Nombre"
        Me.gcNombre1.ColumnEdit = Me.memNombre
        Me.gcNombre1.FieldName = "Nombre"
        Me.gcNombre1.Name = "gcNombre1"
        Me.gcNombre1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcNombre1.VisibleIndex = 2
        Me.gcNombre1.Width = 151
        '
        'gcCelula1
        '
        Me.gcCelula1.Caption = "Celula"
        Me.gcCelula1.FieldName = "Celula"
        Me.gcCelula1.FormatString = "#"
        Me.gcCelula1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcCelula1.Name = "gcCelula1"
        Me.gcCelula1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcCelula1.VisibleIndex = 3
        Me.gcCelula1.Width = 49
        '
        'gcRuta1
        '
        Me.gcRuta1.Caption = "Ruta"
        Me.gcRuta1.FieldName = "Ruta"
        Me.gcRuta1.FormatString = "#"
        Me.gcRuta1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcRuta1.Name = "gcRuta1"
        Me.gcRuta1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRuta1.VisibleIndex = 4
        Me.gcRuta1.Width = 49
        '
        'gcAñoPed1
        '
        Me.gcAñoPed1.Caption = "Año"
        Me.gcAñoPed1.FieldName = "AñoPed"
        Me.gcAñoPed1.FormatString = "#"
        Me.gcAñoPed1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcAñoPed1.Name = "gcAñoPed1"
        Me.gcAñoPed1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcAñoPed1.VisibleIndex = 5
        Me.gcAñoPed1.Width = 55
        '
        'gcPedido1
        '
        Me.gcPedido1.Caption = "Pedido"
        Me.gcPedido1.FieldName = "Pedido"
        Me.gcPedido1.FormatString = "#"
        Me.gcPedido1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcPedido1.Name = "gcPedido1"
        Me.gcPedido1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcPedido1.VisibleIndex = 6
        Me.gcPedido1.Width = 69
        '
        'gcLitros1
        '
        Me.gcLitros1.Caption = "Litros"
        Me.gcLitros1.ColumnEdit = Me.txtLitros1
        Me.gcLitros1.FieldName = "Litros"
        Me.gcLitros1.FormatString = "#,##.00"
        Me.gcLitros1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcLitros1.Name = "gcLitros1"
        Me.gcLitros1.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcLitros1.SummaryItem.SummaryType = DevExpress.XtraGrid.SummaryItemType.Sum
        Me.gcLitros1.VisibleIndex = 7
        Me.gcLitros1.Width = 73
        '
        'gcPrecio1
        '
        Me.gcPrecio1.Caption = "Precio"
        Me.gcPrecio1.ColumnEdit = Me.cboPrecio
        Me.gcPrecio1.FieldName = "Precio"
        Me.gcPrecio1.FormatString = "#,##.00"
        Me.gcPrecio1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcPrecio1.Name = "gcPrecio1"
        Me.gcPrecio1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcPrecio1.StyleName = "Style1"
        Me.gcPrecio1.VisibleIndex = 8
        Me.gcPrecio1.Width = 58
        '
        'gcImporte1
        '
        Me.gcImporte1.Caption = "Importe"
        Me.gcImporte1.FieldName = "Importe"
        Me.gcImporte1.FormatString = "$ #,##.00"
        Me.gcImporte1.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcImporte1.Name = "gcImporte1"
        Me.gcImporte1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcImporte1.StyleName = "Style2"
        Me.gcImporte1.SummaryItem.SummaryType = DevExpress.XtraGrid.SummaryItemType.Sum
        Me.gcImporte1.VisibleIndex = 9
        Me.gcImporte1.Width = 95
        '
        'gcTipoPago1
        '
        Me.gcTipoPago1.Caption = "Tipo"
        Me.gcTipoPago1.ColumnEdit = Me.txtTipoPago
        Me.gcTipoPago1.FieldName = "TipoPago"
        Me.gcTipoPago1.Name = "gcTipoPago1"
        Me.gcTipoPago1.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcTipoPago1.VisibleIndex = 10
        Me.gcTipoPago1.Width = 105
        '
        'gcFormaPago1
        '
        Me.gcFormaPago1.Caption = "Pago"
        Me.gcFormaPago1.ColumnEdit = Me.txtFormaPago
        Me.gcFormaPago1.FieldName = "FormaPago"
        Me.gcFormaPago1.Name = "gcFormaPago1"
        Me.gcFormaPago1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcFormaPago1.VisibleIndex = 11
        Me.gcFormaPago1.Width = 143
        '
        'gcDireccion1
        '
        Me.gcDireccion1.Caption = "Direccion"
        Me.gcDireccion1.ColumnEdit = Me.memNombre
        Me.gcDireccion1.FieldName = "Direccion"
        Me.gcDireccion1.Name = "gcDireccion1"
        Me.gcDireccion1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcDireccion1.VisibleIndex = 12
        Me.gcDireccion1.Width = 216
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(312, 144)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(40, 14)
        Me.Label34.TabIndex = 106
        Me.Label34.Text = "Total :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(284, 88)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(70, 14)
        Me.Label29.TabIndex = 101
        Me.Label29.Text = "De credito :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(276, 4)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(76, 14)
        Me.Label26.TabIndex = 98
        Me.Label26.Text = "De contado :"
        '
        'lbTotalImporte
        '
        Me.lbTotalImporte.BackColor = System.Drawing.SystemColors.Control
        Me.lbTotalImporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalImporte.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalImporte.ForeColor = System.Drawing.Color.Black
        Me.lbTotalImporte.Location = New System.Drawing.Point(456, 140)
        Me.lbTotalImporte.Name = "lbTotalImporte"
        Me.lbTotalImporte.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalImporte.TabIndex = 105
        Me.lbTotalImporte.Text = "0.00"
        Me.lbTotalImporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalLitros
        '
        Me.lbTotalLitros.BackColor = System.Drawing.SystemColors.Control
        Me.lbTotalLitros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalLitros.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalLitros.ForeColor = System.Drawing.Color.Black
        Me.lbTotalLitros.Location = New System.Drawing.Point(356, 140)
        Me.lbTotalLitros.Name = "lbTotalLitros"
        Me.lbTotalLitros.Size = New System.Drawing.Size(96, 24)
        Me.lbTotalLitros.TabIndex = 104
        Me.lbTotalLitros.Text = "0.00"
        Me.lbTotalLitros.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalCredito
        '
        Me.lbTotalCredito.BackColor = System.Drawing.Color.White
        Me.lbTotalCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalCredito.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalCredito.ForeColor = System.Drawing.Color.Maroon
        Me.lbTotalCredito.Location = New System.Drawing.Point(456, 84)
        Me.lbTotalCredito.Name = "lbTotalCredito"
        Me.lbTotalCredito.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalCredito.TabIndex = 103
        Me.lbTotalCredito.Text = "0.00"
        Me.lbTotalCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbLitrosCredito
        '
        Me.lbLitrosCredito.BackColor = System.Drawing.Color.White
        Me.lbLitrosCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLitrosCredito.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosCredito.ForeColor = System.Drawing.Color.Maroon
        Me.lbLitrosCredito.Location = New System.Drawing.Point(356, 84)
        Me.lbLitrosCredito.Name = "lbLitrosCredito"
        Me.lbLitrosCredito.Size = New System.Drawing.Size(96, 24)
        Me.lbLitrosCredito.TabIndex = 102
        Me.lbLitrosCredito.Text = "0.00"
        Me.lbLitrosCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalContado
        '
        Me.lbTotalContado.BackColor = System.Drawing.Color.White
        Me.lbTotalContado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalContado.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalContado.ForeColor = System.Drawing.Color.Blue
        Me.lbTotalContado.Location = New System.Drawing.Point(456, 0)
        Me.lbTotalContado.Name = "lbTotalContado"
        Me.lbTotalContado.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalContado.TabIndex = 100
        Me.lbTotalContado.Text = "0.00"
        Me.lbTotalContado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbLitrosContado
        '
        Me.lbLitrosContado.BackColor = System.Drawing.Color.White
        Me.lbLitrosContado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLitrosContado.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosContado.ForeColor = System.Drawing.Color.Blue
        Me.lbLitrosContado.Location = New System.Drawing.Point(356, 0)
        Me.lbLitrosContado.Name = "lbLitrosContado"
        Me.lbLitrosContado.Size = New System.Drawing.Size(96, 24)
        Me.lbLitrosContado.TabIndex = 99
        Me.lbLitrosContado.Text = "0.00"
        Me.lbLitrosContado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnTotales
        '
        Me.pnTotales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnTotales.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbTotal, Me.Label31, Me.Label28, Me.lbTotalNotaRemision, Me.Label23, Me.lbNotasBlancas, Me.Label33})
        Me.pnTotales.Name = "pnTotales"
        Me.pnTotales.Size = New System.Drawing.Size(248, 92)
        Me.pnTotales.TabIndex = 107
        '
        'lbTotal
        '
        Me.lbTotal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotal.ForeColor = System.Drawing.Color.Red
        Me.lbTotal.Location = New System.Drawing.Point(184, 61)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(56, 16)
        Me.lbTotal.TabIndex = 80
        Me.lbTotal.Text = "0"
        Me.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Red
        Me.Label31.Location = New System.Drawing.Point(19, 58)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(168, 16)
        Me.Label31.TabIndex = 79
        Me.Label31.Text = "Total de notas a liquidar :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(30, 30)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(152, 16)
        Me.Label28.TabIndex = 77
        Me.Label28.Text = "Total de notas blancas :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTotalNotaRemision
        '
        Me.lbTotalNotaRemision.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalNotaRemision.ForeColor = System.Drawing.Color.Black
        Me.lbTotalNotaRemision.Location = New System.Drawing.Point(187, 11)
        Me.lbTotalNotaRemision.Name = "lbTotalNotaRemision"
        Me.lbTotalNotaRemision.Size = New System.Drawing.Size(53, 11)
        Me.lbTotalNotaRemision.TabIndex = 70
        Me.lbTotalNotaRemision.Text = "0"
        Me.lbTotalNotaRemision.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(6, 8)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(184, 16)
        Me.Label23.TabIndex = 74
        Me.Label23.Text = "Total de notas de remisión : "
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbNotasBlancas
        '
        Me.lbNotasBlancas.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNotasBlancas.ForeColor = System.Drawing.Color.Black
        Me.lbNotasBlancas.Location = New System.Drawing.Point(187, 30)
        Me.lbNotasBlancas.Name = "lbNotasBlancas"
        Me.lbNotasBlancas.Size = New System.Drawing.Size(53, 16)
        Me.lbNotasBlancas.TabIndex = 73
        Me.lbNotasBlancas.Text = "0"
        Me.lbNotasBlancas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(176, 50)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(67, 16)
        Me.Label33.TabIndex = 81
        Me.Label33.Text = "----------------------"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Teal
        Me.Label5.Location = New System.Drawing.Point(4, 352)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(464, 20)
        Me.Label5.TabIndex = 108
        Me.Label5.Text = "Teclee F1 para asignar el cliente de venta publico a una nota blanca"
        '
        'btnDocumento
        '
        Me.btnDocumento.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnDocumento.Image = CType(resources.GetObject("btnDocumento.Image"), System.Drawing.Bitmap)
        Me.btnDocumento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDocumento.Location = New System.Drawing.Point(1028, 88)
        Me.btnDocumento.Name = "btnDocumento"
        Me.btnDocumento.Size = New System.Drawing.Size(112, 24)
        Me.btnDocumento.TabIndex = 111
        Me.btnDocumento.Text = "Documentos"
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Bitmap)
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.Location = New System.Drawing.Point(1028, 60)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(112, 24)
        Me.btnImprimir.TabIndex = 112
        Me.btnImprimir.Text = "Imprimir"
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(1028, 32)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(112, 24)
        Me.btnCancelar.TabIndex = 110
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(1028, 4)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(112, 24)
        Me.btnAceptar.TabIndex = 109
        Me.btnAceptar.Text = "Aceptar"
        '
        'GridControl1
        '
        Me.GridControl1.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.GridControl1.DataSource = Me.dtDetalle
        Me.GridControl1.EditorsRepository = Me.PersistentRepository1
        Me.GridControl1.Location = New System.Drawing.Point(948, 128)
        Me.GridControl1.MainView = Me.GridView3
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(192, 220)
        Me.GridControl1.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Khaki, System.Drawing.SystemColors.ControlText))
        Me.GridControl1.Styles.AddReplace("HeaderPanel", New DevExpress.Utils.ViewStyle("HeaderPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Black, System.Drawing.Color.White))
        Me.GridControl1.TabIndex = 113
        Me.GridControl1.Text = "GridControl1"
        '
        'GridView3
        '
        Me.GridView3.BehaviorOptions = ((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowZoomDetail Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary)
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn10, Me.GridColumn11})
        Me.GridView3.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView3.GroupPanelText = "Pago con Cheque o TPV"
        Me.GridView3.Name = "GridView3"
        Me.GridView3.VertScrollTipFieldName = Nothing
        Me.GridView3.ViewOptions = (((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowDetailButtons) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle)
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Tipo"
        Me.GridColumn10.FieldName = "Destipo"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn10.VisibleIndex = 0
        Me.GridColumn10.Width = 40
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Monto"
        Me.GridColumn11.FieldName = "Monto"
        Me.GridColumn11.FormatString = "$ #,##.00"
        Me.GridColumn11.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn11.VisibleIndex = 1
        Me.GridColumn11.Width = 146
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(272, 36)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 14)
        Me.Label7.TabIndex = 114
        Me.Label7.Text = "De Cheques :"
        '
        'lblTotalCheques
        '
        Me.lblTotalCheques.BackColor = System.Drawing.Color.White
        Me.lblTotalCheques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalCheques.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCheques.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(64, Byte), CType(0, Byte))
        Me.lblTotalCheques.Location = New System.Drawing.Point(456, 28)
        Me.lblTotalCheques.Name = "lblTotalCheques"
        Me.lblTotalCheques.Size = New System.Drawing.Size(104, 24)
        Me.lblTotalCheques.TabIndex = 116
        Me.lblTotalCheques.Text = "0.00"
        Me.lblTotalCheques.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbLitrosCheques
        '
        Me.lbLitrosCheques.BackColor = System.Drawing.Color.White
        Me.lbLitrosCheques.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLitrosCheques.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosCheques.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(64, Byte), CType(0, Byte))
        Me.lbLitrosCheques.Location = New System.Drawing.Point(356, 28)
        Me.lbLitrosCheques.Name = "lbLitrosCheques"
        Me.lbLitrosCheques.Size = New System.Drawing.Size(96, 24)
        Me.lbLitrosCheques.TabIndex = 117
        Me.lbLitrosCheques.Text = "0.00"
        Me.lbLitrosCheques.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbLitrosCheques.Visible = False
        '
        'gridDocumento
        '
        Me.gridDocumento.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.gridDocumento.EditorsRepository = Me.PersistentRepository1
        Me.gridDocumento.Location = New System.Drawing.Point(564, 0)
        Me.gridDocumento.MainView = Me.GridView4
        Me.gridDocumento.Name = "gridDocumento"
        Me.gridDocumento.Size = New System.Drawing.Size(572, 164)
        Me.gridDocumento.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.OliveDrab, System.Drawing.Color.White))
        Me.gridDocumento.Styles.AddReplace("HeaderPanel", New DevExpress.Utils.ViewStyle("HeaderPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Black, System.Drawing.Color.White))
        Me.gridDocumento.Styles.AddReplace("HorzLine", New DevExpress.Utils.ViewStyle("HorzLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.OliveDrab, System.Drawing.SystemColors.ControlDark))
        Me.gridDocumento.Styles.AddReplace("VertLine", New DevExpress.Utils.ViewStyle("VertLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.OliveDrab, System.Drawing.SystemColors.ControlDark))
        Me.gridDocumento.TabIndex = 118
        Me.gridDocumento.Text = "GridControl1"
        '
        'GridView4
        '
        Me.GridView4.BehaviorOptions = (((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowZoomDetail Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowGroup) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary)
        Me.GridView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.GridView4.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView4.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
        Me.GridView4.GroupPanelText = "Documentos Disponibles (Cheques o TPV)"
        Me.GridView4.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridSummaryItem(DevExpress.XtraGrid.SummaryItemType.Sum, "Monto", Me.GridColumn5, "$ #,##.00")})
        Me.GridView4.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always
        Me.GridView4.Name = "GridView4"
        Me.GridView4.VertScrollTipFieldName = Nothing
        Me.GridView4.ViewOptions = ((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowDetailButtons) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo"
        Me.GridColumn1.FieldName = "TipoDes"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Options = ((((DevExpress.XtraGrid.Columns.ColumnOptions.CanGrouped Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 49
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Banco"
        Me.GridColumn2.FieldName = "DesBanco"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 130
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cheque"
        Me.GridColumn3.FieldName = "Cheque"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cuenta"
        Me.GridColumn4.FieldName = "Cuenta"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Monto"
        Me.GridColumn5.FieldName = "Monto"
        Me.GridColumn5.FormatString = "$ #,##.00"
        Me.GridColumn5.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Disponible"
        Me.GridColumn6.FieldName = "Disponible"
        Me.GridColumn6.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Contrato"
        Me.GridColumn7.FieldName = "Cliente"
        Me.GridColumn7.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 80
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Nombre"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 160
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "PF"
        Me.GridColumn9.FieldName = "PosFechado"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.GridColumn9.VisibleIndex = 8
        Me.GridColumn9.Width = 40
        '
        'lbObsequio
        '
        Me.lbObsequio.AutoSize = True
        Me.lbObsequio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbObsequio.Location = New System.Drawing.Point(256, 116)
        Me.lbObsequio.Name = "lbObsequio"
        Me.lbObsequio.Size = New System.Drawing.Size(95, 14)
        Me.lbObsequio.TabIndex = 152
        Me.lbObsequio.Text = "Obs./Autocarb.:"
        Me.lbObsequio.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.lbObsequio.Visible = False
        '
        'lblTotalObsequio
        '
        Me.lblTotalObsequio.BackColor = System.Drawing.Color.White
        Me.lblTotalObsequio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalObsequio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalObsequio.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblTotalObsequio.Location = New System.Drawing.Point(456, 112)
        Me.lblTotalObsequio.Name = "lblTotalObsequio"
        Me.lblTotalObsequio.Size = New System.Drawing.Size(104, 24)
        Me.lblTotalObsequio.TabIndex = 151
        Me.lblTotalObsequio.Text = "0.00"
        Me.lblTotalObsequio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblTotalObsequio.Visible = False
        '
        'lblLitrosObsequio
        '
        Me.lblLitrosObsequio.BackColor = System.Drawing.Color.White
        Me.lblLitrosObsequio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLitrosObsequio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLitrosObsequio.ForeColor = System.Drawing.Color.MidnightBlue
        Me.lblLitrosObsequio.Location = New System.Drawing.Point(356, 112)
        Me.lblLitrosObsequio.Name = "lblLitrosObsequio"
        Me.lblLitrosObsequio.Size = New System.Drawing.Size(96, 24)
        Me.lblLitrosObsequio.TabIndex = 150
        Me.lblLitrosObsequio.Text = "0.00"
        Me.lblLitrosObsequio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblLitrosObsequio.Visible = False
        '
        'pnObsequio
        '
        Me.pnObsequio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnObsequio.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel4, Me.Label21, Me.Label22, Me.Label24, Me.Label25, Me.Label27, Me.Label30})
        Me.pnObsequio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnObsequio.Location = New System.Drawing.Point(0, 120)
        Me.pnObsequio.Name = "pnObsequio"
        Me.pnObsequio.Size = New System.Drawing.Size(248, 44)
        Me.pnObsequio.TabIndex = 153
        Me.pnObsequio.Visible = False
        '
        'Panel4
        '
        Me.Panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel4.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblImporteAutoCarb, Me.lblImporteObsequio, Me.Label17, Me.lblObsequio, Me.Label19, Me.lblAutoCarb})
        Me.Panel4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel4.Location = New System.Drawing.Point(-1, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(281, 64)
        Me.Panel4.TabIndex = 143
        '
        'lblImporteAutoCarb
        '
        Me.lblImporteAutoCarb.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporteAutoCarb.ForeColor = System.Drawing.Color.Black
        Me.lblImporteAutoCarb.Location = New System.Drawing.Point(182, 24)
        Me.lblImporteAutoCarb.Name = "lblImporteAutoCarb"
        Me.lblImporteAutoCarb.Size = New System.Drawing.Size(58, 11)
        Me.lblImporteAutoCarb.TabIndex = 79
        Me.lblImporteAutoCarb.Text = "0"
        Me.lblImporteAutoCarb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblImporteObsequio
        '
        Me.lblImporteObsequio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporteObsequio.ForeColor = System.Drawing.Color.Black
        Me.lblImporteObsequio.Location = New System.Drawing.Point(184, 7)
        Me.lblImporteObsequio.Name = "lblImporteObsequio"
        Me.lblImporteObsequio.Size = New System.Drawing.Size(56, 11)
        Me.lblImporteObsequio.TabIndex = 78
        Me.lblImporteObsequio.Text = "0"
        Me.lblImporteObsequio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.Black
        Me.Label17.Location = New System.Drawing.Point(1, 22)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(102, 14)
        Me.Label17.TabIndex = 77
        Me.Label17.Text = "Autocarburación:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblObsequio
        '
        Me.lblObsequio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObsequio.ForeColor = System.Drawing.Color.Black
        Me.lblObsequio.Location = New System.Drawing.Point(110, 8)
        Me.lblObsequio.Name = "lblObsequio"
        Me.lblObsequio.Size = New System.Drawing.Size(64, 11)
        Me.lblObsequio.TabIndex = 70
        Me.lblObsequio.Text = "0"
        Me.lblObsequio.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1, 4)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(67, 14)
        Me.Label19.TabIndex = 74
        Me.Label19.Text = "Obsequios:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAutoCarb
        '
        Me.lblAutoCarb.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoCarb.ForeColor = System.Drawing.Color.Black
        Me.lblAutoCarb.Location = New System.Drawing.Point(110, 24)
        Me.lblAutoCarb.Name = "lblAutoCarb"
        Me.lblAutoCarb.Size = New System.Drawing.Size(64, 16)
        Me.lblAutoCarb.TabIndex = 73
        Me.lblAutoCarb.Text = "0"
        Me.lblAutoCarb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.Red
        Me.Label21.Location = New System.Drawing.Point(187, 44)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(53, 16)
        Me.Label21.TabIndex = 80
        Me.Label21.Text = "0"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Red
        Me.Label22.Location = New System.Drawing.Point(19, 44)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(149, 14)
        Me.Label22.TabIndex = 79
        Me.Label22.Text = "Total de notas a liquidar :"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.Black
        Me.Label24.Location = New System.Drawing.Point(29, 22)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(139, 14)
        Me.Label24.TabIndex = 77
        Me.Label24.Text = "Total de notas blancas :"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Black
        Me.Label25.Location = New System.Drawing.Point(187, 4)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(53, 11)
        Me.Label25.TabIndex = 70
        Me.Label25.Text = "0"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(6, 4)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(162, 14)
        Me.Label27.TabIndex = 74
        Me.Label27.Text = "Total de notas de remisión :"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(187, 22)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(53, 16)
        Me.Label30.TabIndex = 73
        Me.Label30.Text = "0"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbLitrosTPV
        '
        Me.lbLitrosTPV.BackColor = System.Drawing.Color.White
        Me.lbLitrosTPV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLitrosTPV.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosTPV.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(64, Byte), CType(0, Byte))
        Me.lbLitrosTPV.Location = New System.Drawing.Point(356, 56)
        Me.lbLitrosTPV.Name = "lbLitrosTPV"
        Me.lbLitrosTPV.Size = New System.Drawing.Size(96, 24)
        Me.lbLitrosTPV.TabIndex = 154
        Me.lbLitrosTPV.Text = "0.00"
        Me.lbLitrosTPV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbLitrosTPV.Visible = False
        '
        'lbTotalTPV
        '
        Me.lbTotalTPV.BackColor = System.Drawing.Color.White
        Me.lbTotalTPV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalTPV.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalTPV.ForeColor = System.Drawing.Color.FromArgb(CType(64, Byte), CType(64, Byte), CType(0, Byte))
        Me.lbTotalTPV.Location = New System.Drawing.Point(456, 56)
        Me.lbTotalTPV.Name = "lbTotalTPV"
        Me.lbTotalTPV.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalTPV.TabIndex = 155
        Me.lbTotalTPV.Text = "0.00"
        Me.lbTotalTPV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(300, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 14)
        Me.Label8.TabIndex = 156
        Me.Label8.Text = "De TPV :"
        '
        'Panel2
        '
        Me.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.Panel2.AutoScroll = True
        Me.Panel2.Controls.AddRange(New System.Windows.Forms.Control() {Me.gridDocumento, Me.lbTotalCredito, Me.lbTotalContado, Me.lbLitrosCheques, Me.lbLitrosCredito, Me.lbTotalTPV, Me.Label29, Me.Label7, Me.lbLitrosContado, Me.lbLitrosTPV, Me.Label26, Me.Label8, Me.lblTotalCheques, Me.lbObsequio, Me.lbTotalLitros, Me.Label34, Me.lblLitrosObsequio, Me.lblTotalObsequio, Me.lbTotalImporte, Me.pnObsequio, Me.pnTotales})
        Me.Panel2.Location = New System.Drawing.Point(4, 696)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1136, 164)
        Me.Panel2.TabIndex = 157
        '
        'Liquidacion2005
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(1144, 865)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel2, Me.GridControl1, Me.dgNotaBlanca, Me.dgRemision, Me.btnDocumento, Me.btnImprimir, Me.btnCancelar, Me.btnAceptar, Me.Label5, Me.Panel1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "Liquidacion2005"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidación"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.dgRemision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLitros, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtTipoPago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFormaPago, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtCliente, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.memNombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtLitros1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cboPrecio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtNota, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsLiquidacion2005, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtRemision, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtNotaBlanca, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDocumento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgNotaBlanca, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnTotales.ResumeLayout(False)
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridDocumento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnObsequio.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Entrada(ByVal Fecha As Date, _
                       ByVal Ruta As Integer, _
                       ByVal Folio As Integer, _
                       ByVal Anio As Integer, _
                       ByVal Descarga As Boolean, _
                       ByVal Celula As Integer)

        Cursor = Cursors.WaitCursor

        _Folio = Folio
        _AñoAtt = CType(Anio, Short)
        _Celula = Celula
        _Fecha = Fecha
        _Ruta = Ruta
        _Descarga = Descarga
        _CerroLiquidacion = False

        'Consulta de precios válidos
        consultaPrecios()


        GridView2.FormatConditions.Item(3).Value1 = _Celula
        GridView2.FormatConditions.Item(4).Value1 = _Ruta

        'Dim cmdOperador As New SqlClient.SqlCommand("SELECT Att.AñoAtt, Att.Folio, Att.Ruta, Att.Celula, Att.Autotanque, O.Operador, E.Nombre, isnull(Att.LitrosLiquidados,0) as LitrosLiquidados, IsNull(R.ClaseRuta,1) as ClaseRuta, isnull(dbo.PrecioRuta(@Fecha,1),0) as PrecioAuxiliar, isnull(dbo.PrecioRuta(@Fecha, Att.Ruta),0) as PrecioVigente, dbo.TimeOutLiquidacion(1) as TimeOut  " & _
        '                                            "FROM AutotanqueTurno Att INNER JOIN TripulacionTurno TT ON Att.Folio = TT.Folio AND Att.AñoAtt = TT.AñoAtt " & _
        '                                            "INNER JOIN Operador O ON TT.Operador = O.Operador INNER JOIN Empleado E ON O.Empleado = E.Empleado INNER JOIN Ruta R ON R.Ruta=Att.Ruta " & _
        '                                            "WHERE (Att.Folio = @Folio) AND (Att.AñoAtt = @Anio) AND TT.CategoriaOperador = 1 ") '")
        'Esta consulta se cambió por el procedimiento almacenado spCCLiqConsultaDatosRutaPreliquidacion
        Dim cmdOperador As New SqlClient.SqlCommand("EXEC spCCLiqConsultaDatosRutaPreliquidacion @Folio, @Anio, @Fecha") '")
        cmdOperador.CommandType = CommandType.Text
        cmdOperador.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
        cmdOperador.Parameters.Add("@Anio", SqlDbType.SmallInt).Value = _AñoAtt
        cmdOperador.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
        cmdOperador.Connection = SqlConnection

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        Dim rdrLiquidacion As SqlClient.SqlDataReader
        rdrLiquidacion = cmdOperador.ExecuteReader

        'TODO: Revisar si se cierra el reader antes de enviar el mensaje a usuario
        If Not rdrLiquidacion.Read Then
            MsgBox("No existe tripulación en esta ruta para poder liquidar. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
            Cursor = Cursors.Default
            _CerroLiquidacion = True
            Me.Close() 'Cerrar la forma
            _permitirLiquidacion = False
            Exit Sub
        Else
            Me.Text = "Liquidación para la ruta " + CType(_Ruta, String) + " Folio del Autotanque : " + CType(_Folio, String)

            'TODO: Validación de crédito de operador JAGD25072005
            _operador = CType(rdrLiquidacion("Operador"), Integer)

            lbOperador.Text = CType(rdrLiquidacion("Nombre"), String)
            lbAutotanque.Text = CType(rdrLiquidacion("Autotanque"), String)
            _PrecioAuxiliar = CType(rdrLiquidacion("PrecioAuxiliar"), Decimal)
            _PrecioVigente = CType(rdrLiquidacion("PrecioVigente"), Decimal)
            lbFecha.Text = _Fecha.ToShortDateString
            _Totalizador = CType(rdrLiquidacion("LitrosLiquidados"), Decimal)
            _ParametroDecimal = CType(rdrLiquidacion("ParametroDecimal"), String)
            _TimeOut = CType(rdrLiquidacion("TimeOut"), Integer)
            _Carburacion = CBool(IIf(CType(rdrLiquidacion("ClaseRuta"), Short) = 4, True, False))
            lbPrecioAuxiliar.Visible = CBool(IIf(CType(rdrLiquidacion("ClaseRuta"), Short) = 5, True, False))

            If Not rdrLiquidacion("SerieRemision") Is DBNull.Value Then
                _serieRemisionRuta = CType(rdrLiquidacion("SerieRemision"), String)
            End If

            _Celula = CType(rdrLiquidacion("Celula"), Integer)
            _MultiPrecios = CType(rdrLiquidacion("MultiplePrecios"), Boolean)

            'Validar la liquidación a crédito Flama Azul 11/11/2006
            _permitirLiquidacionCredito = Not (CType(rdrLiquidacion("ValidarLiquidacionCredito"), Boolean))

            'Corección de liquidación fterminoruta 29112006
            _fechaSuministro = CType(rdrLiquidacion("FTerminoRuta"), DateTime)

            _CorrerDescarga = _Descarga

            rdrLiquidacion.Close()
            cmdOperador.Dispose()

            lbFolio.Text = CType(_Folio, String)
            lbCelula.Text = CType(_Celula, String)
            lbRuta.Text = CType(_Ruta, String)
            lbTotalizador.Text = Format(_Totalizador, "0")
            lbTipoLiquidacion.Text = CStr(IIf(_CorrerDescarga, "LIQUIDACION AUTOMATICA", "LIQUIDACION MANUAL"))

            'TODO: Se ejecutatá en un sp
            Dim cmdVentaPublico As New SqlClient.SqlCommand("EXEC spCCLIQConsultaClienteVentaPublico @Ruta")
            cmdVentaPublico.CommandType = CommandType.Text
            cmdVentaPublico.Parameters.Clear()
            cmdVentaPublico.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
            cmdVentaPublico.Connection = SqlConnection
            If Not rdrLiquidacion.IsClosed Then
                rdrLiquidacion.Close()
            End If

            Try
                rdrLiquidacion = cmdVentaPublico.ExecuteReader
                rdrLiquidacion.Read()
                _ClienteGlobal = CType(rdrLiquidacion("Cliente"), Integer)
                rdrLiquidacion.Close()
                cmdVentaPublico.Dispose()
            Catch ioEx As Exception
                rdrLiquidacion.Close()
                cmdVentaPublico.Dispose()
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try

            CargaRemisiones()
            CargaNotasBlancas()

            If _Descarga Then
                DescargaRampac()
            End If

            dsLiquidacion2005.Tables.Item("Documento").Clear()

            If Global_TipoLiquidacion = "S" Then
                Panel1.BackColor = Color.Gainsboro
                gdPrecio.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
                gcPrecio1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
                gcNota.VisibleIndex = 0
                gcNota1.VisibleIndex = 0
                txtTipoPago.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("OBSEQUIO", "OBSEQUIO", 2))
                lbObsequio.Visible = True
                lblLitrosObsequio.Visible = True
                lblTotalObsequio.Visible = True
                pnObsequio.Visible = True
            Else
                gcNota.VisibleIndex = -1
                gcNota1.VisibleIndex = -1
            End If

            If _MultiPrecios Then
                gdPrecio.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
                gcPrecio1.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
            End If

            'Actualiza fecha de inicio de liquidación en tabla autotanqueturno
            UpdateFInicioLiquidacíonAutotanqueturno()

            Cursor = Cursors.Default

        End If


        Me.Show()

        If _CerroLiquidacion Then
            Me.Close()
        End If

        'para validar el crédito de operador
        validacionCreditoOperador = New validacionCreditoOperador(_operador, CnnSigamet)
        validacionCreditoOperador.ValidaSaldoOperador()

        'error de cambio de contratos
        'AddHandler GridView2.FocusedColumnChanged, AddressOf GridView2_FocusedColumnChanged
    End Sub

    Private Sub InactivaControls()
        btnAceptar.Enabled = False
        btnDocumento.Enabled = False
        gcLitros.ColumnEditProperties.ReadOnly = True
        gcTipoPago.ColumnEditProperties.ReadOnly = True
        gcFormaPago.ColumnEditProperties.ReadOnly = True
        gcCliente1.ColumnEditProperties.ReadOnly = True
        gcLitros1.ColumnEditProperties.ReadOnly = True
        gcTipoPago1.ColumnEditProperties.ReadOnly = True
        gcFormaPago1.ColumnEditProperties.ReadOnly = True
        gdPrecio.ColumnEditProperties.ReadOnly = True
        gcPrecio1.ColumnEditProperties.ReadOnly = True
    End Sub

    Private Sub UpdateFInicioLiquidacíonAutotanqueturno()
        Dim cmdUpdate As New SqlClient.SqlCommand()
        cmdUpdate.CommandText = "EXEC spCCLIQActualizaFInicioLiquidacion @AñoAtt, @Folio"
        cmdUpdate.CommandType = CommandType.Text
        cmdUpdate.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
        cmdUpdate.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
        cmdUpdate.Connection = SqlConnection
        cmdUpdate.ExecuteNonQuery()
        cmdUpdate.Dispose()
    End Sub

    Private Sub CargaRemisiones()
        dsLiquidacion2005.Tables.Item("Remision").Clear()

        Dim cmdRemisiones As New SqlClient.SqlCommand("EXECUTE spCCCargaRemisiones @Carburacion, @CorrerDescarga, @Fecha, @Ruta, @AñoAtt, @Folio")
        cmdRemisiones.CommandType = CommandType.Text

        cmdRemisiones.Parameters.Add("@Carburacion", SqlDbType.Int).Value = _Carburacion

        cmdRemisiones.Parameters.Add("@CorrerDescarga", SqlDbType.Int).Value = _CorrerDescarga

        cmdRemisiones.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
        cmdRemisiones.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
        'If _CorrerDescarga Then
        cmdRemisiones.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AñoAtt
        cmdRemisiones.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
        'End If
        cmdRemisiones.Connection = SqlConnection

        Dim da As New SqlClient.SqlDataAdapter(cmdRemisiones)
        Try
            da.Fill(dsLiquidacion2005.Tables.Item("Remision"))
            dgRemision.DataSource = dsLiquidacion2005.Tables.Item("Remision")
            dgRemision.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            da.Dispose()
            cmdRemisiones.Dispose()
        End Try

        validacionEdificiosRemisiones()

    End Sub

    Private Sub CargaNotasBlancas()
        dsLiquidacion2005.Tables.Item("NotaBlanca").Clear()
        Dim cmdNotasBlancas As New SqlClient.SqlCommand("exec spCCGeneraTablaNotaBlanca @Ruta,@Fecha")
        cmdNotasBlancas.CommandType = CommandType.Text
        cmdNotasBlancas.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
        cmdNotasBlancas.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
        cmdNotasBlancas.Connection = SqlConnection

        Dim da As New SqlClient.SqlDataAdapter(cmdNotasBlancas)
        da.Fill(dsLiquidacion2005.Tables.Item("NotaBlanca"))
        dgNotaBlanca.DataSource = dsLiquidacion2005.Tables.Item("NotaBlanca")
        dgNotaBlanca.Refresh()
        da.Dispose()
        cmdNotasBlancas.Dispose()
    End Sub

    Function TarjetaAndOrCredito(ByRef rdrLiquidacion As SqlClient.SqlDataReader, ByVal Cliente As Integer) As Int16
        Dim cmdCreditos As New SqlClient.SqlCommand()
        'TODO: Colocar en sp
        cmdCreditos.CommandText = "Select dbo.TieneTarjetaAndOrCredito(@Cliente) as Tiene "
        cmdCreditos.CommandType = CommandType.Text
        cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
        cmdCreditos.Connection = SqlConnection
        rdrLiquidacion = cmdCreditos.ExecuteReader
        rdrLiquidacion.Read()
        TarjetaAndOrCredito = CType(rdrLiquidacion("Tiene"), Int16)
        rdrLiquidacion.Close()
        cmdCreditos.Dispose()
    End Function

    Private Sub DescargaRampac()
        'TODO: Todos los scripts SQL para descarga deberían estar en sp's
        Dim Registro As Int16
        Dim cmdDuplicados As New SqlClient.SqlCommand("EXEC spCCLIQCuentaDuplicadosRampac @AñoAtt, @Folio")
        cmdDuplicados.CommandType = CommandType.Text
        cmdDuplicados.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
        cmdDuplicados.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AñoAtt
        cmdDuplicados.Connection = SqlConnection

        Dim rdrLiquidacion As SqlClient.SqlDataReader
        rdrLiquidacion = cmdDuplicados.ExecuteReader
        rdrLiquidacion.Read()
        Registro = CType(rdrLiquidacion("Registro"), Int16)
        rdrLiquidacion.Close()
        cmdDuplicados.Dispose()

        If Registro > 0 Then
            lbUnificacion.Visible = True
        Else
            lbUnificacion.Visible = False
        End If


        If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then

            Dim cmdRampac As New SqlClient.SqlCommand("EXECUTE spCCDescargaRampac @TipoLiquidacion, @Carburacion, @Fecha, @Ruta, @AñoAtt, @Folio")
            cmdRampac.CommandType = CommandType.Text
            cmdRampac.CommandTimeout = _TimeOut

            cmdRampac.Parameters.Add("@TipoLiquidacion", SqlDbType.Char).Value = Global_TipoLiquidacion
            cmdRampac.Parameters.Add("@Carburacion", SqlDbType.Int).Value = _Carburacion


            cmdRampac.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
            cmdRampac.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
            cmdRampac.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AñoAtt
            cmdRampac.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio

            cmdRampac.Connection = SqlConnection

            rdrLiquidacion = cmdRampac.ExecuteReader
            Dim i As Integer
            While rdrLiquidacion.Read()
                For i = 0 To dsLiquidacion2005.Tables.Item("Remision").Rows.Count - 1

                    If CType(rdrLiquidacion("Cliente"), Integer) = CType(dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("Cliente"), Integer) Then
                        'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
                        dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("Litros") = CType(rdrLiquidacion("Litros"), Double)
                        dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("Precio") = CType(rdrLiquidacion("Precio"), Decimal)
                        dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("Importe") = CType(rdrLiquidacion("Importe"), Decimal)

                        If Trim(CType(rdrLiquidacion("FormaPago"), String)) = "Contado" Then 'se agregó el trim porque este campo trae trailing blanks
                            dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("TipoPago") = "CONTADO"
                        Else
                            dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("TipoPago") = "CREDITO"
                        End If
                    End If

                Next

            End While
            rdrLiquidacion.Close()
            cmdRampac.Dispose()

            For i = 0 To dsLiquidacion2005.Tables("Remision").Rows.Count - 1
                'Validación de crédito para edificios administrados 10-10-2006
                Dim _datosCliente As DatosCliente = consultaDatosCliente(CType(dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("Cliente"), Integer))
                If GLOBAL_AdmEdificiosLiquidacionCredito AndAlso (_datosCliente.RamoCliente = GLOBAL_ClaveRamoClienteAdmEdificios) Then
                    dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("TipoPago") = "CREDITO"
                End If

                If CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("TipoPago"), String) = "CREDITO" Then
                    Dim cmdCreditos As New SqlClient.SqlCommand()
                    cmdCreditos.CommandText = "EXEC spCCLIQConsultaTarjetaCreditoCliente @Cliente"
                    cmdCreditos.CommandType = CommandType.Text
                    cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Cliente"), Integer)
                    cmdCreditos.Connection = SqlConnection

                    Dim Registro1 As Int16
                    rdrLiquidacion = cmdCreditos.ExecuteReader
                    rdrLiquidacion.Read()
                    Registro1 = CType(rdrLiquidacion("Tarjeta"), Int16)
                    rdrLiquidacion.Close()

                    If Registro1 = 1 Then
                        dsLiquidacion2005.Tables.Item("Remision").Rows(i).Item("FormaPago") = 4
                    Else
                        Dim Credito As Int16
                        cmdCreditos.CommandText = "EXEC spCCLIQConsultaCreditoCliente @Cliente"
                        cmdCreditos.Parameters.Clear()
                        cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Cliente"), Integer)
                        rdrLiquidacion = cmdCreditos.ExecuteReader
                        rdrLiquidacion.Read()
                        Credito = CType(rdrLiquidacion("Credito"), Int16)
                        rdrLiquidacion.Close()

                        If Credito = 1 Then
                            dsLiquidacion2005.Tables.Item("Remision").Rows(i).Item("FormaPago") = 2
                        Else
                            dsLiquidacion2005.Tables.Item("Remision").Rows(i).Item("FormaPago") = 3
                        End If
                    End If
                    cmdCreditos.Dispose()

                End If

            Next
        End If



        Dim cmdRampacNotaBlanca As New SqlClient.SqlCommand("EXECUTE spCCDescargaRampacNotaBlanca @TipoLiquidacion, @Carburacion, @Fecha, @Ruta, @AñoAtt, @Folio")
        cmdRampacNotaBlanca.CommandTimeout = _TimeOut
        cmdRampacNotaBlanca.Connection = SqlConnection

        cmdRampacNotaBlanca.CommandType = CommandType.Text
        cmdRampacNotaBlanca.CommandTimeout = _TimeOut

        cmdRampacNotaBlanca.Parameters.Add("@TipoLiquidacion", SqlDbType.Char).Value = Global_TipoLiquidacion
        cmdRampacNotaBlanca.Parameters.Add("@Carburacion", SqlDbType.Int).Value = _Carburacion


        cmdRampacNotaBlanca.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
        cmdRampacNotaBlanca.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
        cmdRampacNotaBlanca.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AñoAtt
        cmdRampacNotaBlanca.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio

        rdrLiquidacion = cmdRampacNotaBlanca.ExecuteReader

        Dim j As Integer
        j = 0
        While rdrLiquidacion.Read()
            If CType(rdrLiquidacion("Cliente"), Integer) <> 0 Then
                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Item(j).Item("Cliente") = CType(rdrLiquidacion("Cliente"), Integer)
            End If
            'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Item(j).Item("Litros") = CType(rdrLiquidacion("Litros"), Double)
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Item(j).Item("Precio") = CType(rdrLiquidacion("Precio"), Decimal)
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Item(j).Item("Importe") = CType(rdrLiquidacion("Importe"), Decimal)

            If Trim(CType(rdrLiquidacion("FormaPago"), String)) = "Contado" Then 'se agregó el trim porque este campo trae trailing blanks
                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Item(j).Item("TipoPago") = "CONTADO"
            Else
                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Item(j).Item("TipoPago") = "CREDITO"
            End If

            'para liquidación uds incluyendo el folio de nota
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Item(j).Item("Codigo") = CType(rdrLiquidacion("FolioNota"), Decimal)

            j = j + 1
        End While

        rdrLiquidacion.Close()
        cmdRampacNotaBlanca.Dispose()


        Dim a As Integer
        For a = 0 To dsLiquidacion2005.Tables("NotaBlanca").Rows.Count - 1
            If CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente"), Integer) <> 0 Then
                'Dim cmdCliente As New SqlClient.SqlCommand()
                'cmdCliente.CommandText = "Select dbo.ClienteValido(@Cliente) as Valido, dbo.CelulaCliente(@Cliente) as Celula, dbo.RutaCliente(@Cliente) as Ruta, dbo.NombreCliente(@Cliente) as Nombre, dbo.DireccionCliente(@Cliente) as Direccion "
                'cmdCliente.CommandType = CommandType.Text
                'cmdCliente.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente"), Integer)
                'cmdCliente.Connection = SqlConnection

                'Dim Valido As Int16
                'Dim Nombre As String
                'Dim Celula As Integer
                'Dim Ruta As Integer
                'Dim Direccion As String

                'rdrLiquidacion = cmdCliente.ExecuteReader
                'rdrLiquidacion.Read()
                'Valido = CType(rdrLiquidacion("Valido"), Int16)
                'Nombre = CType(rdrLiquidacion("Nombre"), String)
                'Celula = CType(rdrLiquidacion("Celula"), Integer)
                'Ruta = CType(rdrLiquidacion("Ruta"), Integer)
                'Direccion = CType(rdrLiquidacion("Direccion"), String)

                'rdrLiquidacion.Close()
                'cmdCliente.Dispose()

                'Validación de crédito para edificios administrados 10-10-2006
                Dim _datosCliente As DatosCliente = consultaDatosCliente(CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente"), Integer))

                'If _datosCliente.Valido = 1 Then
                If _datosCliente.Valido Then
                    dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Nombre") = _datosCliente.Nombre
                    dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Direccion") = _datosCliente.Direccion

                    Dim Existe As Int16
                    If ClienteExiste(CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente"), Integer)) Then
                        Existe = 1
                    Else
                        Existe = 0
                    End If

                    Dim cmdPedido As New SqlClient.SqlCommand()
                    cmdPedido.CommandText = "exec spCCGeneraPedidoLiquidacion @Cliente, @CelulaCliente, @RutaCliente, @AñoAtt, @Folio, @Existe, @Fecha, @Autotanque"
                    cmdPedido.CommandType = CommandType.Text
                    cmdPedido.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente"), Integer)
                    cmdPedido.Parameters.Add("@CelulaCliente", SqlDbType.Int).Value = _datosCliente.Celula
                    cmdPedido.Parameters.Add("@RutaCliente", SqlDbType.Int).Value = _datosCliente.Ruta
                    cmdPedido.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                    cmdPedido.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                    cmdPedido.Parameters.Add("@Existe", SqlDbType.Int).Value = Existe
                    cmdPedido.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
                    cmdPedido.Parameters.Add("@Autotanque", SqlDbType.Int).Value = CType(lbAutotanque.Text, Integer)


                    cmdPedido.Connection = SqlConnection

                    rdrLiquidacion = cmdPedido.ExecuteReader
                    rdrLiquidacion.Read()

                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("Celula") = CType(rdrLiquidacion("Celula"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("Ruta") = CType(rdrLiquidacion("Ruta"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("AñoPed") = CType(rdrLiquidacion("AñoPed"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("Pedido") = CType(rdrLiquidacion("Pedido"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("AñoNota") = CType(rdrLiquidacion("AñoNota"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("Nota") = CType(rdrLiquidacion("Nota"), Integer)

                    'Validación de crédito para edificios administrados 10-10-2006
                    If GLOBAL_AdmEdificiosLiquidacionCredito AndAlso (_datosCliente.RamoCliente = GLOBAL_ClaveRamoClienteAdmEdificios) Then
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("TipoPago") = "CREDITO"
                    End If

                    Application.DoEvents()

                    rdrLiquidacion.Close()
                    cmdPedido.Dispose()

                    If CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("TipoPago"), String) = "CREDITO" Then
                        Dim cmdCreditos As New SqlClient.SqlCommand()
                        cmdCreditos.CommandText = "EXEC spCCLIQConsultaTarjetaCreditoCliente @Cliente"
                        cmdCreditos.CommandType = CommandType.Text
                        cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente"), Integer)
                        cmdCreditos.Connection = SqlConnection

                        Dim Registro1 As Int16
                        rdrLiquidacion = cmdCreditos.ExecuteReader
                        rdrLiquidacion.Read()
                        Registro1 = CType(rdrLiquidacion("Tarjeta"), Int16)
                        rdrLiquidacion.Close()

                        If Registro1 = 1 Then
                            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("FormaPago") = 4
                        Else
                            Dim Credito As Int16
                            cmdCreditos.CommandText = "EXEC spCCLIQConsultaCreditoCliente @Cliente"
                            cmdCreditos.Parameters.Clear()
                            cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente"), Integer)
                            rdrLiquidacion = cmdCreditos.ExecuteReader
                            rdrLiquidacion.Read()
                            Credito = CType(rdrLiquidacion("Credito"), Int16)
                            rdrLiquidacion.Close()

                            If Credito = 1 Then
                                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("FormaPago") = 2
                            Else
                                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(a).Item("FormaPago") = 3
                            End If
                        End If
                        cmdCreditos.Dispose()
                    End If

                Else
                    dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("Cliente") = 0
                    dsLiquidacion2005.Tables("NotaBlanca").Rows(a).Item("FormaPago") = 3
                End If

            End If
        Next
    End Sub

    Private Sub CalCulaTotales()
        'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
        Dim ContadosRemisiones, CreditosRemisiones As Double
        Dim i, k As Integer
        Dim j As Integer = Nothing

        Dim ContadosNotasBlancas, CreditosNotasBlancas, ContadosCheques, ContadosTPV, ObsequiosNotasBlancas, ObsequiosRemisiones As Double
        Dim NotasBlancas, NotasRemision As Integer
        Dim TotalImporteContado, TotalImporteCheques, TotalImporteTPV, TotalImporteDisponible, TotalImporteDisponibleTPV, TotalImporteCredito, Calculo, TotalImporteObsequios As Decimal
        Dim Tabla As String = "Remision"

        ObsequiosNotasBlancas = 0
        ObsequiosRemisiones = 0

        If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("Remision").Compute("SUM(Litros)", "TipoPago='CONTADO'")) Then
                ContadosRemisiones = CType(dsLiquidacion2005.Tables.Item("Remision").Compute("SUM(Litros)", "TipoPago='CONTADO'"), Double)
            End If
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("Remision").Compute("SUM(Litros)", "TipoPago='CREDITO'")) Then
                CreditosRemisiones = CType(dsLiquidacion2005.Tables.Item("Remision").Compute("SUM(Litros)", "TipoPago='CREDITO'"), Double)
            End If
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("Remision").Compute("SUM(Litros)", "TipoPago='OBSEQUIO'")) Then
                ObsequiosRemisiones = CType(dsLiquidacion2005.Tables.Item("Remision").Compute("SUM(Litros)", "TipoPago='OBSEQUIO'"), Decimal)
            End If
            NotasRemision = CType(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Cliente)", "Litros>0"), Integer)
        End If

        If dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count > 0 Then
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("SUM(Litros)", "TipoPago='CONTADO'")) Then
                ContadosNotasBlancas = CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("SUM(Litros)", "TipoPago='CONTADO'"), Double)
            End If
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("SUM(Litros)", "TipoPago='CREDITO'")) Then
                CreditosNotasBlancas = CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("SUM(Litros)", "TipoPago='CREDITO'"), Double)
            End If
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("SUM(Litros)", "TipoPago='OBSEQUIO'")) Then
                ObsequiosNotasBlancas = CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("SUM(Litros)", "TipoPago='OBSEQUIO'"), Decimal)
            End If
            NotasBlancas = CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Litros>0 and Cliente<>0"), Integer)
        End If

        For k = 0 To 1
            For i = 0 To dsLiquidacion2005.Tables(Tabla).Rows.Count - 1
                'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
                If CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), Double) > 0 And CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Cliente"), Integer) <> 0 Then
                    Calculo = (CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), Decimal) * CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Precio"), Decimal))
                    Select Case CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("TipoPago"), String)
                        Case "CONTADO"
                            TotalImporteContado = TotalImporteContado + Calculo
                        Case "CREDITO"
                            TotalImporteCredito = TotalImporteCredito + Calculo
                            'TODO: Aquí va lo de obsequios
                        Case "OBSEQUIO"
                            TotalImporteObsequios = TotalImporteObsequios + Calculo
                    End Select
                End If
            Next
            Tabla = "NotaBlanca"
        Next

        'For i = 0 To dsLiquidacion2005.Tables("Detalle").Rows.Count - 1
        '    Tabla = "Remision"
        '    For k = 0 To 1
        '        For j = 0 To dsLiquidacion2005.Tables(Tabla).Rows.Count - 1
        '            If CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Cliente"), Integer) = CType(dsLiquidacion2005.Tables.Item(Tabla).Rows(j).Item("Cliente"), Integer) Then
        '                'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
        '                If CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Tipo"), Integer) = 0 Then
        '                    ContadosCheques = ContadosCheques + CType(dsLiquidacion2005.Tables.Item(Tabla).Rows(j).Item("Litros"), Double)
        '                Else
        '                    ContadosTPV = ContadosTPV + CType(dsLiquidacion2005.Tables.Item(Tabla).Rows(j).Item("Litros"), Double)
        '                End If

        '                Tabla = ""
        '                Exit For
        '            End If
        '        Next

        '        If Tabla = "" Then
        '            Exit For
        '        Else
        '            Tabla = "NotaBlanca"
        '        End If
        '    Next
        'Next

        For i = 0 To dsLiquidacion2005.Tables("Documento").Rows.Count - 1
            If CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Tipo"), Integer) = 0 Then
                TotalImporteCheques = TotalImporteCheques + CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Monto"), Decimal)
                TotalImporteDisponible = TotalImporteDisponible + CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Disponible"), Decimal)
            Else
                TotalImporteTPV = TotalImporteTPV + CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Monto"), Decimal)
                TotalImporteDisponibleTPV = TotalImporteDisponibleTPV + CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Disponible"), Decimal)
            End If

        Next

        lbLitrosContado.Text = Format(((ContadosNotasBlancas + ContadosRemisiones) - ContadosCheques - ContadosTPV), "#,##.00")
        lbLitrosCheques.Text = Format(ContadosCheques, "#,##.00")

        lbLitrosTPV.Text = Format(ContadosTPV, "#,##.00")

        lbLitrosCredito.Text = Format(CreditosNotasBlancas + CreditosRemisiones, "#,##.00")
        lbTotalLitros.Text = Format((ContadosNotasBlancas + ContadosRemisiones) + _
                                    (CreditosNotasBlancas + CreditosRemisiones) + _
                                    (ObsequiosNotasBlancas + ObsequiosRemisiones), "#,#0.00")

        lbTotalContado.Text = Format(((TotalImporteContado - (TotalImporteCheques - TotalImporteDisponible)) - (TotalImporteTPV - TotalImporteDisponibleTPV)), "$ #,##.00")
        lblTotalCheques.Text = Format(TotalImporteCheques, "$ #,##.00")
        lbTotalTPV.Text = Format(TotalImporteTPV, "$ #,##.00")

        lbTotalCredito.Text = Format(TotalImporteCredito, "$ #,##.00")
        lbTotalImporte.Text = Format((TotalImporteContado + TotalImporteCredito + TotalImporteObsequios), "$ #,##.00")

        lbTotalNotaRemision.Text = Format(NotasRemision, "#,##.00")
        lbNotasBlancas.Text = Format(NotasBlancas, "#,##.00")
        lbTotal.Text = Format(NotasBlancas + NotasRemision, "#,##.00")

        lblTotalObsequio.Text = Format(TotalImporteObsequios, "$ #,##.00")
        lblLitrosObsequio.Text = Format((ObsequiosNotasBlancas + ObsequiosRemisiones), "#,#0.00")

        lbTotalNotaRemision.Text = Format(NotasRemision, "#,#0")
        lbNotasBlancas.Text = Format(NotasBlancas, "#,#0")
        lbTotal.Text = Format(NotasBlancas + NotasRemision, "#,#0")

        If Global_TipoLiquidacion = "S" Then
            CalculoSubtotalesObsequios(dsLiquidacion2005.Tables("Remision"), dsLiquidacion2005.Tables("NotaBlanca"))
        End If

    End Sub

    Private Sub GridView1_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged
        If e.Column.Name = "gcLitros" Then
            'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
            GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = IIf(Not IsNumeric(e.Value), 0, CType(e.Value, Double) * CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio"), Decimal))

            Dim frmDescuento As ImporteDescuento.frmDiscount
            If GLOBAL_VentasMultinivel Then

                frmDescuento = New ImporteDescuento.frmDiscount(CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer), _
                                                                     _Fecha, CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Celula"), Integer), _
                                                                     CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("AñoPed"), Integer), _
                                                                     CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Pedido"), Integer), CnnSigamet)
                If frmDescuento.DescuentoValido Then
                    If frmDescuento.ShowDialog() = DialogResult.OK And _DescuentoDirecto Then
                        GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = frmDescuento.ImporteMenosDescuento
                    End If
                End If
            End If

        End If

        If e.Column.Name = "gcTipoPago" Then
            Select Case CType(e.Value, String)
                Case "CONTADO"
                    'Validación de crédito para edificios administrados 10-10-2006
                    If GLOBAL_AdmEdificiosLiquidacionCredito Then
                        Dim _datosCliente As DatosCliente = consultaDatosCliente(CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer))
                        'Validación de crédito para edificios administrados 10-10-2006
                        If _datosCliente.RamoCliente = GLOBAL_ClaveRamoClienteAdmEdificios Then
                            MessageBox.Show("Los edificios administrados deben ser liquidados a crédito," & vbCrLf & _
                                "por favor verifique", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            GridView1.GetDataRow(e.RowHandle).Item("TipoPago") = "CREDITO"
                            Exit Sub
                        End If
                    End If

                    GridView1.GetDataRow(e.RowHandle).Item("FormaPago") = 1
                    dsLiquidacion2005.Tables.Item("Remision").Rows(e.RowHandle).Item("FormaPago") = 1
                Case "CREDITO"
                    If Not maximoImporteCreditoExcedido(_aplicaValidacionCreditocliente, CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer), _
                                                                CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe"), Decimal), SqlConnection) Then
                        GridView1.GetDataRow(e.RowHandle).Item("TipoPago") = "CONTADO"
                        GridView1.GetDataRow(e.RowHandle).Item("FormaPago") = 1
                        dsLiquidacion2005.Tables.Item("Remision").Rows(e.RowHandle).Item("FormaPago") = 1
                        Exit Sub
                    End If
                    Dim rdrLiquidacion As SqlClient.SqlDataReader = Nothing
                    Dim TieneTarjetaCredito As Int16 = TarjetaAndOrCredito(rdrLiquidacion, CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer))
                    GridView1.GetDataRow(e.RowHandle).Item("FormaPago") = TieneTarjetaCredito
                    dsLiquidacion2005.Tables.Item("Remision").Rows(e.RowHandle).Item("FormaPago") = TieneTarjetaCredito

                    If dsLiquidacion2005.Tables("Cliente").DefaultView.Count > 0 Then
                        dsLiquidacion2005.Tables("Cliente").DefaultView.Item(0).Delete()
                    End If
                Case "OBSEQUIO"
                        If Not validacionObsequio(CType(GridView2.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer)) Then
                            GridView1.GetDataRow(e.RowHandle).Item("TipoPago") = "CONTADO"
                            MessageBox.Show("El cliente no. " & _
                                            CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer).ToString & _
                                            " no es un cliente válido para obsequio o autocarburación" & Chr(13) & _
                                            "No puede liquidarlo de esta forma", _
                                            "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Else
                            GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros"), Double) * _
                                                                                               CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio"), Decimal)
                        End If
            End Select
            Application.DoEvents()
        End If


        If e.Column.Name = "gcFormaPago" Then
            If CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("TipoPago"), String) = "CONTADO" Or CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("TipoPago"), String) = "OBSEQUIO" Then

                If (CType(e.Value, Integer) <> 1) And (CType(e.Value, Integer) <> 4) Then

                    GridView1.GetDataRow(e.RowHandle).Item("FormaPago") = 1
                    dsLiquidacion2005.Tables.Item("Remision").Rows(e.RowHandle).Item("FormaPago") = 1

                    If Global_TipoLiquidacion = "S" Then
                        MsgBox("El tipo de pago de CONTADO u OBSEQUIO solo se puede pagar o asignar en efectivo y tarjeta de credito TPV", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Else
                        MsgBox("El tipo de pago de CONTADO solo se puede pagar en efectivo y tarjeta de credito TPV", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    End If

                End If

            Else

                If CType(e.Value, Integer) = 1 Then
                    GridView1.GetDataRow(e.RowHandle).Item("FormaPago") = 3
                    dsLiquidacion2005.Tables.Item("Remision").Rows(e.RowHandle).Item("FormaPago") = 3
                    Application.DoEvents()
                    MsgBox("El pago a CREDITO no puede pagarse en EFECTIVO.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                End If

                If CType(e.Value, Integer) = 4 Then
                    Dim cmdCreditos As New SqlClient.SqlCommand()
                    cmdCreditos.CommandText = "Select dbo.TieneTarjetaCredito(@Cliente) as Tarjeta "
                    cmdCreditos.CommandType = CommandType.Text
                    cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer)
                    cmdCreditos.Connection = SqlConnection
                    Dim rdrLiquidacion As SqlClient.SqlDataReader
                    rdrLiquidacion = cmdCreditos.ExecuteReader
                    rdrLiquidacion.Read()
                    Dim Registro As Int16 = CType(rdrLiquidacion("Tarjeta"), Int16)
                    rdrLiquidacion.Close()
                    If Registro = 0 Then
                        GridView1.GetDataRow(e.RowHandle).Item("FormaPago") = 3
                        dsLiquidacion2005.Tables.Item("Remision").Rows(e.RowHandle).Item("FormaPago") = 3
                        Application.DoEvents()
                        MsgBox("Este Cliente no tiene una tarjeta de credito ACTIVA en el sistema. No puede liquidar de esta forma.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    End If
                    cmdCreditos.Dispose()
                End If

                If CType(e.Value, Integer) = 2 Then
                    Dim cmdCreditos As New SqlClient.SqlCommand()
                    'TODO: Colocar en sp
                    cmdCreditos.CommandText = "Select dbo.TieneCredito(@Cliente) as Credito "
                    cmdCreditos.CommandType = CommandType.Text
                    cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer)
                    cmdCreditos.Connection = SqlConnection
                    Dim rdrLiquidacion As SqlClient.SqlDataReader
                    rdrLiquidacion = cmdCreditos.ExecuteReader
                    rdrLiquidacion.Read()
                    Dim Registro As Int16 = CType(rdrLiquidacion("Credito"), Int16)
                    rdrLiquidacion.Close()
                    If Registro = 0 Then
                        GridView1.GetDataRow(e.RowHandle).Item("FormaPago") = 3
                        dsLiquidacion2005.Tables.Item("Remision").Rows(e.RowHandle).Item("FormaPago") = 3
                        Application.DoEvents()
                        MsgBox("Este Cliente no tiene asignado credito por la Empresa. No puede liquidar de esta forma.")
                    End If
                    cmdCreditos.Dispose()
                End If
            End If

            Application.DoEvents()
        End If

        If Global_TipoLiquidacion = "S" Then
            If e.Column.Name = "gcNota" Then
                If CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Codigo"), String) <> "" Then
                    ValidaCodigo("Remision", sender, e)
                End If
            End If

            If e.Column.Name = "gdPrecio" Then
                Dim APrecio As Decimal = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio"), Decimal)
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio") = APrecio
                dsLiquidacion2005.Tables.Item("Remision").Rows(GridView1.FocusedRowHandle).Item("Precio") = APrecio
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros"), Decimal) * APrecio
                'TODO: Modularizar
                Dim frmDescuento As New ImporteDescuento.frmDiscount(CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer), _
                                                                     CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros"), Double), _
                                                                     CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe"), Double), _
                                                                     CnnSigamet, _Fecha, _AplicarDescuentoVariable)
                If frmDescuento.DescuentoValido Then
                    If frmDescuento.ShowDialog() = DialogResult.OK And _DescuentoDirecto Then
                        GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = frmDescuento.ImporteMenosDescuento
                    End If
                End If
            End If

        End If

        If e.Column.Name = "gdPrecio" Then
            Dim APrecio As Decimal = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio"), Decimal)
            GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio") = APrecio
            dsLiquidacion2005.Tables.Item("Remision").Rows(GridView1.FocusedRowHandle).Item("Precio") = APrecio
            GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros"), Decimal) * APrecio
            'TODO: Modularizar
            'Dim frmDescuento As New ImporteDescuento.frmDiscount(CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), Integer), _
            '                                                     CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros"), Double), _
            '                                                     CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe"), Double), _
            '                                                     CnnSigamet, _AplicarDescuentoVariable)
            'If frmDescuento.DescuentoValido Then
            '    If frmDescuento.ShowDialog() = DialogResult.OK And _DescuentoDirecto Then
            '        GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = frmDescuento.ImporteMenosDescuento
            '    End If
            'End If
        End If


    End Sub

    Private Sub GridView1_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.LostFocus
        Application.DoEvents()
    End Sub

    Private Sub GridView1_CellValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanging
        If e.Column.Name = "gcLitros" Then
            If CType(e.Value, String) = "" Then
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros") = 0
            End If
        End If

        If dsLiquidacion2005.Tables("Detalle").DefaultView.Count > 0 Then
            GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros") = dsLiquidacion2005.Tables.Item("Remision").Rows(GridView1.FocusedRowHandle).Item("Litros")
            GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("TipoPago") = dsLiquidacion2005.Tables.Item("Remision").Rows(GridView1.FocusedRowHandle).Item("TipoPago")
            GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("FormaPago") = dsLiquidacion2005.Tables.Item("Remision").Rows(GridView1.FocusedRowHandle).Item("FormaPago")
            MsgBox("Este cliente tiene pagos de cheques relacionados y no puede ser modificado.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Application.DoEvents()
            Exit Sub
        End If

    End Sub

    Private Sub txtTipoPago_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTipoPago.ValueChanged
        Application.DoEvents()
    End Sub

    Private Sub txtTipoPago_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTipoPago.SelectedIndexChanged
        Application.DoEvents()
    End Sub

    Private Sub GridView2_LostFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView2.LostFocus
        Application.DoEvents()
    End Sub

    Private Sub AsignaValoresGridView2(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs, ByVal LitrosImporte As Boolean, ByVal Cliente As Boolean)
        GridView2.GetDataRow(e.RowHandle).Item("Celula") = 0
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Celula") = 0
        GridView2.GetDataRow(e.RowHandle).Item("Ruta") = 0
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Ruta") = 0
        GridView2.GetDataRow(e.RowHandle).Item("AñoPed") = 0
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("AñoPed") = 0
        GridView2.GetDataRow(e.RowHandle).Item("Pedido") = 0
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Pedido") = 0
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("AñoNota") = 0
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Nota") = 0
        GridView2.GetDataRow(e.RowHandle).Item("TipoPago") = "CONTADO"
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("TipoPago") = "CONTADO"
        GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = 1
        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = 1
        If LitrosImporte Then
            GridView2.GetDataRow(e.RowHandle).Item("Litros") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Litros") = 0
            GridView2.GetDataRow(e.RowHandle).Item("Importe") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Importe") = 0
        End If
        If Cliente Then
            GridView2.GetDataRow(e.RowHandle).Item("Cliente") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Cliente") = 0
            GridView2.GetDataRow(e.RowHandle).Item("Nombre") = ""
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Nombre") = ""
            GridView2.GetDataRow(e.RowHandle).Item("Direccion") = ""
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Direccion") = ""
        End If
        Application.DoEvents()
    End Sub

    Private Sub GridView2_CellValueChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanged

        If e.Column.Name = "gcLitros1" Then
            'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = IIf(Not IsNumeric(e.Value), 0, CType(e.Value, Double) * CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio"), Decimal))
            Dim frmDescuento As ImporteDescuento.frmDiscount

            If GLOBAL_VentasMultinivel Then
                frmDescuento = New ImporteDescuento.frmDiscount(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer), _
                                                                     _Fecha, CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Celula"), Integer), _
                                                                     CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("AñoPed"), Integer), _
                                                                     CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Pedido"), Integer), CnnSigamet)
                If frmDescuento.DescuentoValido Then
                    If frmDescuento.ShowDialog() = DialogResult.OK And _DescuentoDirecto Then
                        GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = frmDescuento.ImporteMenosDescuento
                    End If
                End If

            End If

        End If

        If e.Column.Name = "gcTipoPago1" Then

            Select Case CType(e.Value, String)
                Case "CONTADO"

                    If GLOBAL_AdmEdificiosLiquidacionCredito Then
                        'Validación de crédito para edificios administrados 10-10-2006
                        Dim _datosCliente As DatosCliente = consultaDatosCliente(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer))
                        'Validación de crédito para edificios administrados 10-10-2006
                        If _datosCliente.RamoCliente = GLOBAL_ClaveRamoClienteAdmEdificios Then
                            MessageBox.Show("Los edificios administrados deben ser liquidados a crédito," & vbCrLf & _
                                "por favor verifique", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            GridView2.GetDataRow(e.RowHandle).Item("TipoPago") = "CREDITO"
                            Exit Sub
                        End If
                    End If

                    GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = 1
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = 1
                Case "OBSEQUIO"
                    'TODO: Aquí va la validación de obsequios y autocarburaciones
                    If Not validacionObsequio(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer)) Then
                        GridView2.GetDataRow(e.RowHandle).Item("TipoPago") = "CONTADO"
                        MessageBox.Show("El cliente no. " & _
                                        CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer).ToString & _
                                        " no es un cliente válido para obsequio o autocarburación" & Chr(13) & _
                                        "No puede liquidarlo de esta forma", _
                                        "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else 'Para meter el importe de obequio sin descuento
                        GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Double) * _
                                                                                       CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio"), Decimal)
                    End If
                Case Else
                    If Not maximoImporteCreditoExcedido(_aplicaValidacionCreditocliente, _
                                                 CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer), _
                                                 CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe"), Decimal), SqlConnection) Then
                        GridView2.GetDataRow(e.RowHandle).Item("TipoPago") = "CONTADO"
                        GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = 1
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = 1
                        Exit Sub
                    End If
                    Dim rdrLiquidacion As SqlClient.SqlDataReader = Nothing
                    Dim TieneTarjetaCredito As Int16 = TarjetaAndOrCredito(rdrLiquidacion, _
                                                                           CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer))
                    GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = TieneTarjetaCredito
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = TieneTarjetaCredito

                    If dsLiquidacion2005.Tables("Cliente").DefaultView.Count > 0 Then
                        dsLiquidacion2005.Tables("Cliente").DefaultView.Item(0).Delete()
                    End If

            End Select
            Application.DoEvents()
        End If

        If e.Column.Name = "gcFormaPago1" Then
            If CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("TipoPago"), String) = "CONTADO" Or CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("TipoPago"), String) = "OBSEQUIO" Then


                If (CType(e.Value, Integer) <> 1) And (CType(e.Value, Integer) <> 4) Then
                    GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = 1
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = 1
                    If Global_TipoLiquidacion = "S" Then
                        MsgBox("El tipo de pago de CONTADO u OBSEQUIO solo se puede pagar o asignar en efectivo y tarjeta de credito TPV", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Else
                        MsgBox("El tipo de pago de CONTADO solo se puede pagar en efectivo y tarjeta de credito TPV", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    End If
                End If

            Else

                If CType(e.Value, Integer) = 1 Then
                    GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = 3
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = 3
                    Application.DoEvents()
                    MsgBox("El pago a CREDITO no puede pagarse en EFECTIVO.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                End If

                If CType(e.Value, Integer) = 4 Then 'Si es pago por tarjeta valida que el cliente tenga una tarjeta activa
                    Dim cmdCreditos As New SqlClient.SqlCommand()
                    'TODO: Colocar en sp
                    cmdCreditos.CommandText = "Select dbo.TieneTarjetaCredito(@Cliente) as Tarjeta "
                    cmdCreditos.CommandType = CommandType.Text
                    cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer)
                    cmdCreditos.Connection = SqlConnection
                    Dim rdrLiquidacion As SqlClient.SqlDataReader
                    rdrLiquidacion = cmdCreditos.ExecuteReader
                    rdrLiquidacion.Read()
                    Dim Registro As Int16 = CType(rdrLiquidacion("Tarjeta"), Int16)
                    rdrLiquidacion.Close()
                    If Registro = 0 Then
                        GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = 3
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = 3
                        Application.DoEvents()
                        MsgBox("Este Cliente no tiene una tarjeta de credito ACTIVA en el sistema. No puede liquidar de esta forma.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    End If
                    cmdCreditos.Dispose()
                End If

                If CType(e.Value, Integer) = 2 Then
                    Dim cmdCreditos As New SqlClient.SqlCommand()
                    'TODO: Colocar en sp
                    cmdCreditos.CommandText = "Select dbo.TieneCredito(@Cliente) as Credito "
                    cmdCreditos.CommandType = CommandType.Text
                    cmdCreditos.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer)
                    cmdCreditos.Connection = SqlConnection
                    Dim rdrLiquidacion As SqlClient.SqlDataReader
                    rdrLiquidacion = cmdCreditos.ExecuteReader
                    rdrLiquidacion.Read()
                    Dim Registro As Int16 = CType(rdrLiquidacion("Credito"), Int16)
                    rdrLiquidacion.Close()
                    If Registro = 0 Then
                        GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = 3
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = 3
                        Application.DoEvents()
                        MsgBox("Este Cliente no tiene asignado credito por la Empresa. No puede liquidar de esta forma.")
                    End If
                    cmdCreditos.Dispose()
                End If
            End If
            Application.DoEvents()
        End If

        If e.Column.Name = "gcCliente1" Then
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = ""
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = "Cliente=" + CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), String)

            If dsLiquidacion2005.Tables("Detalle").DefaultView.Count > 0 Then
                MsgBox("Este cliente tiene pagos de cheques relacionados y no se puede agregar nuevamente.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                AsignaValoresGridView2(sender, e, False, True)
                Exit Sub
            End If


            If CType(e.Value, Integer) = _ClienteGlobal Then
                AsignaValoresGridView2(sender, e, False, False)
            Else
                If CType(e.Value, Integer) = 0 Then
                    AsignaValoresGridView2(sender, e, True, True)
                Else
                    Dim cmdCliente As New SqlClient.SqlCommand()

                    'TODO: Debería ser un stored procedure
                    'cmdCliente.CommandText = "Select dbo.ClienteValido(@Cliente) as Valido, dbo.CelulaCliente(@Cliente) as Celula, dbo.RutaCliente(@Cliente) as Ruta, dbo.NombreCliente(@Cliente) as Nombre, dbo.DireccionCliente(@Cliente) as Direccion, dbo.ClienteHijoEdificio(@Cliente) as ClienteEdificio, dbo.ClienteActivo(@Cliente) as ClienteActivo "

                    'cmdCliente.CommandType = CommandType.Text
                    'cmdCliente.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer)
                    'cmdCliente.Connection = SqlConnection


                    'rdrLiquidacion = cmdCliente.ExecuteReader
                    'rdrLiquidacion.Read()
                    'Dim Valido As Int16 = CType(rdrLiquidacion("Valido"), Int16)
                    'Dim Celula As Integer = CType(rdrLiquidacion("Celula"), Integer)
                    'Dim Ruta As Integer = CType(rdrLiquidacion("Ruta"), Integer)
                    'Dim Nombre As String = CType(rdrLiquidacion("Nombre"), String)
                    'Dim Direccion As String = CType(rdrLiquidacion("Direccion"), String)

                    'Dim ClienteEdificio As Boolean = CType(rdrLiquidacion("ClienteEdificio"), Boolean)

                    ''Contratos inactivos
                    'Dim clienteActivo As Boolean = CType(rdrLiquidacion("ClienteActivo"), Boolean)

                    'rdrLiquidacion.Close()
                    'cmdCliente.Dispose()
                    Dim rdrLiquidacion As SqlClient.SqlDataReader
                    'Validación de crédito para edificios administrados 10-10-2006
                    Dim _datosCliente As DatosCliente = consultaDatosCliente(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer))

                    'If _datosCliente.Valido = 0 Then
                    If Not _datosCliente.Valido Then
                        AsignaValoresGridView2(sender, e, True, True)
                        If Not _datosCliente.ClienteEdificio Then
                            MsgBox("Este no es un numero de contrato válido. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                            Exit Sub
                        Else
                            MessageBox.Show("Este contrato es un cliente hijo de edificios administrados," & Chr(13) & _
                            "no se puede liquidar. " & "Verifique por favor", "Mensaje del sistema", _
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End If

                    'Validación de contratos inactivos JAGD 10-03-2006
                    If Not _datosCliente.ClienteActivo Then
                        MessageBox.Show("Este contrato no está activo," & Chr(13) & _
                            "no se puede liquidar. " & "Verifique por favor", "Mensaje del sistema", _
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        AsignaValoresGridView2(sender, e, True, True)
                        Exit Sub
                    End If

                    If _datosCliente.Celula <> _Celula Then
                        If MsgBox("Este Cliente no pertenece a la celula de la ruta que esta siendo liquidada. ¿Desea liquidarlo de cualquier manera?", MsgBoxStyle.YesNo, "Mensaje del sistema") = MsgBoxResult.No Then
                            AsignaValoresGridView2(sender, e, True, True)
                            Exit Sub
                        End If
                    End If

                    If _datosCliente.Ruta <> _Ruta Then
                        If MsgBox("Este Cliente no pertenece a la ruta que esta siendo liquidada. ¿Desea liquidarlo de cualquier manera?", MsgBoxStyle.YesNo, "Mensaje del sistema") = MsgBoxResult.No Then
                            AsignaValoresGridView2(sender, e, True, True)
                            Exit Sub
                        End If
                    End If

                    Dim frmClienteExiste As New ClienteLiquidacion()
                    frmClienteExiste.Entrada(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer), SqlConnection)
                    'Dim Acepto As Byte = CType(frmClienteExiste._Acepta, Byte)
                    'frmClienteExiste.Dispose()

                    'If Acepto = -1 Then
                    If frmClienteExiste.DialogResult = DialogResult.Cancel Then
                        AsignaValoresGridView2(sender, e, True, True)
                        Exit Sub
                    End If

                    GridView2.GetDataRow(e.RowHandle).Item("Nombre") = _datosCliente.Nombre
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Nombre") = _datosCliente.Nombre
                    GridView2.GetDataRow(e.RowHandle).Item("Direccion") = _datosCliente.Direccion
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Direccion") = _datosCliente.Direccion
                    Application.DoEvents()

                    Dim Existe As Int16
                    If ClienteExiste(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer)) Then
                        Existe = 1
                        MsgBox("Este Cliente ya se encuentra capturado en esta liquidación.", MsgBoxStyle.Information, "Mensaje del sistema")
                    End If

                    'para evitar el error de cambio de contrato
                    currentClient = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer)

                    Dim cmdPedido As New SqlClient.SqlCommand()
                    cmdPedido.CommandText = "exec spCCGeneraPedidoLiquidacion @Cliente, @CelulaCliente, @RutaCliente, @AñoAtt, @Folio, @Existe, @Fecha, @Autotanque"
                    cmdPedido.CommandType = CommandType.Text
                    cmdPedido.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer)
                    cmdPedido.Parameters.Add("@CelulaCliente", SqlDbType.Int).Value = _datosCliente.Celula
                    cmdPedido.Parameters.Add("@RutaCliente", SqlDbType.Int).Value = _datosCliente.Ruta
                    cmdPedido.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                    cmdPedido.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                    cmdPedido.Parameters.Add("@Existe", SqlDbType.Int).Value = Existe
                    cmdPedido.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
                    cmdPedido.Parameters.Add("@Autotanque", SqlDbType.Int).Value = CType(lbAutotanque.Text, Integer)
                    cmdPedido.Connection = SqlConnection
                    rdrLiquidacion = cmdPedido.ExecuteReader
                    rdrLiquidacion.Read()
                    GridView2.GetDataRow(e.RowHandle).Item("Celula") = CType(rdrLiquidacion("Celula"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Celula") = CType(rdrLiquidacion("Celula"), Int16)
                    GridView2.GetDataRow(e.RowHandle).Item("Ruta") = CType(rdrLiquidacion("Ruta"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Ruta") = CType(rdrLiquidacion("Ruta"), Int16)
                    GridView2.GetDataRow(e.RowHandle).Item("AñoPed") = CType(rdrLiquidacion("AñoPed"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("AñoPed") = CType(rdrLiquidacion("AñoPed"), Int16)
                    GridView2.GetDataRow(e.RowHandle).Item("Pedido") = CType(rdrLiquidacion("Pedido"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Pedido") = CType(rdrLiquidacion("Pedido"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("AñoNota") = CType(rdrLiquidacion("AñoNota"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("Nota") = CType(rdrLiquidacion("Nota"), Integer)
                    Application.DoEvents()
                    rdrLiquidacion.Close()
                    cmdPedido.Dispose()

                    'Validación de crédito para edificios administrados 10-10-2006
                    If GLOBAL_AdmEdificiosLiquidacionCredito AndAlso (_datosCliente.RamoCliente = GLOBAL_ClaveRamoClienteAdmEdificios) Then
                        GridView2.GetDataRow(e.RowHandle).Item("TipoPago") = "CREDITO"
                    End If

                    If currentClient <> CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer) Then
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Cliente") = currentClient
                        GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente") = currentClient
                    End If

                    If CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("TipoPago"), String) = "CREDITO" Then
                        Dim TieneTarjetaCredito As Int16 = TarjetaAndOrCredito(rdrLiquidacion, CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer))
                        GridView2.GetDataRow(e.RowHandle).Item("FormaPago") = TieneTarjetaCredito
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(e.RowHandle).Item("FormaPago") = TieneTarjetaCredito
                        Application.DoEvents()
                    End If

                    If GLOBAL_VentasMultinivel Then
                        If _DescuentoDirecto And CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Double) > 0 Then
                            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Double) * _
                                                                                               CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio"), Decimal)
                            Dim frmDescuento As New ImporteDescuento.frmDiscount(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer), _
                                                         CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Double), _
                                                         CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe"), Double), _
                                                         CnnSigamet, _Fecha, _AplicarDescuentoVariable)
                            If frmDescuento.DescuentoValido Then
                                If frmDescuento.ShowDialog() = DialogResult.OK And _DescuentoDirecto Then
                                    GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = frmDescuento.ImporteMenosDescuento
                                End If
                            End If
                        End If
                    End If

                End If
            End If
        End If

        If Global_TipoLiquidacion = "S" Then
            If e.Column.Name = "gcNota1" Then
                If CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Codigo"), String) <> "" Then
                    ValidaCodigo("NotaBlanca", sender, e)
                End If
            End If

            If e.Column.Name = "gcPrecio1" Then
                If CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Codigo"), String) <> "" Then
                    ValidaCodigo("NotaBlanca", sender, e)
                End If

                Dim APrecio As Decimal = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio"), Decimal)

                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio") = APrecio
                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Precio") = APrecio
                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Decimal) * APrecio
                'para el importe menos descuento
                Dim frmDescuento As New ImporteDescuento.frmDiscount(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer), _
                                                                     CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Double), _
                                                                     CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe"), Double), _
                                                                     CnnSigamet, _Fecha, _AplicarDescuentoVariable)
                If frmDescuento.DescuentoValido Then
                    If frmDescuento.ShowDialog() = DialogResult.OK Then
                        GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = frmDescuento.ImporteMenosDescuento
                    End If
                End If

            End If

        End If

        If e.Column.Name = "gcPrecio1" Then
            If CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Codigo"), String) <> "" Then
                ValidaCodigo("NotaBlanca", sender, e)
            End If

            Dim APrecio As Decimal = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio"), Decimal)

            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio") = APrecio
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Precio") = APrecio
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Decimal) * APrecio
            'para el importe menos descuento          
        End If


    End Sub

    Private Function ClienteExiste(ByVal Cliente As Integer) As Boolean
        ClienteExiste = False
        Dim Tabla As String = "Remision"
        Dim i As Int16
        For i = 0 To 1
            If dsLiquidacion2005.Tables.Item(Tabla).Rows.Count > 0 Then
                If Cliente <> 0 And Cliente <> _ClienteGlobal Then
                    If Not IsDBNull(dsLiquidacion2005.Tables.Item(Tabla).Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String))) Then
                        ClienteExiste = CBool(IIf(CType(dsLiquidacion2005.Tables.Item(Tabla).Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > i, True, False))
                    End If
                End If
            End If
            Tabla = "NotaBlanca"
        Next
    End Function

    Private Function ClienteExisteCheque(ByVal Cliente As Integer, ByVal Tipo As Int16) As Boolean
        'ClienteExisteCheque = False
        'Dim Tabla As String = "Remision"
        'Dim i, Count As Integer
        'For i = 0 To 1
        '    Count = CInt(IIf(Tipo <> 1, i, IIf(i = 0, 1, 0)))
        '    If dsLiquidacion2005.Tables.Item(Tabla).Rows.Count > 0 Then
        '        If Cliente <> 0 And Cliente <> _ClienteGlobal Then
        '            If Not IsDBNull(dsLiquidacion2005.Tables.Item(Tabla).Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String))) Then
        '                ClienteExisteCheque = CBool(IIf(CType(dsLiquidacion2005.Tables.Item(Tabla).Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > Count, True, False))
        '            End If
        '        End If
        '    End If
        '    Tabla = "NotaBlanca"
        'Next

        Dim Existe As Boolean
        Existe = False

        If Tipo = 1 Then
            If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then
                If Cliente <> 0 And Cliente <> _ClienteGlobal Then
                    If Not IsDBNull(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String))) Then
                        If CType(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > 1 Then
                            Existe = True
                        End If
                    End If
                End If
            End If

            If dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count > 0 Then
                If Cliente <> 0 And Cliente <> _ClienteGlobal Then
                    If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String))) Then
                        If CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > 0 Then
                            Existe = True
                        End If
                    End If
                End If
            End If

        Else

            If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then
                If Cliente <> 0 And Cliente <> _ClienteGlobal Then
                    If Not IsDBNull(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String))) Then
                        If CType(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > 0 Then
                            Existe = True
                        End If
                    End If
                End If
            End If

            If dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count > 0 Then
                If Cliente <> 0 And Cliente <> _ClienteGlobal Then
                    If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String))) Then
                        If CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > 1 Then
                            Existe = True
                        End If
                    End If
                End If
            End If

        End If

        Return Existe

    End Function

    Private Sub GridView2_CellValueChanging(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView2.CellValueChanging
        If e.Column.Name = "gcLitros1" Then
            If CType(e.Value, String) = "" Then
                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros") = 0
            End If
        End If

        If e.Column.Name = "gcCliente1" Then
            If CType(e.Value, String) = "" Then
                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente") = 0
            End If
        End If

        If dsLiquidacion2005.Tables("Detalle").DefaultView.Count > 0 Then
            MsgBox("Este cliente tiene pagos de cheques relacionados y no puede ser modificado.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente") = dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Cliente")
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros") = dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Litros")
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("TipoPago") = dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("TipoPago")
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("FormaPago") = dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("FormaPago")
            Application.DoEvents()
        End If
    End Sub

    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        CalCulaTotales()
        If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = ""
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = "Cliente=" + CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), String)

            dsLiquidacion2005.Tables("Cliente").DefaultView.RowFilter = ""
            dsLiquidacion2005.Tables("Cliente").DefaultView.RowFilter = "Cliente= " + CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), String)
        End If
    End Sub

    Private Sub GridView2_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView2.FocusedRowChanged
        CalCulaTotales()
        'para el error de cambio de contrato
        'TODO: Podría generar duplicidad en contratos (como un cut and paste)
        currentClient = 0

        If dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count > 0 Then
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = ""
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = "Cliente=" + CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), String)

            dsLiquidacion2005.Tables("Cliente").DefaultView.RowFilter = ""
            dsLiquidacion2005.Tables("Cliente").DefaultView.RowFilter = "Cliente= " + CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), String)
        End If
    End Sub

    Private Function ClienteExisteParaCheque(ByVal Cliente As Integer) As Boolean
        ClienteExisteParaCheque = False
        Dim Tabla As String = "Remision"
        Dim i As Int16
        Dim Contador As Integer = 0
        For i = 0 To 1
            If dsLiquidacion2005.Tables.Item(Tabla).Rows.Count > 0 Then
                If Cliente <> 0 And Cliente <> _ClienteGlobal Then
                    If Not IsDBNull(dsLiquidacion2005.Tables.Item(Tabla).Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String))) Then
                        If CType(dsLiquidacion2005.Tables.Item(Tabla).Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > 0 Then
                            Contador = Contador + 1
                        End If
                        'ClienteExisteParaCheque = CBool(IIf(CType(dsLiquidacion2005.Tables.Item(Tabla).Compute("Count(Cliente)", "Cliente=" + CType(Cliente, String)), Integer) > i, True, False))
                    End If
                End If
            End If
            Tabla = "NotaBlanca"
        Next

        If Contador > 1 Then
            ClienteExisteParaCheque = True
        End If

    End Function

    Private Sub txtCliente_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCliente.KeyUp
        If e.KeyCode = Keys.F1 Then
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente") = _ClienteGlobal
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Cliente") = _ClienteGlobal
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Nombre") = "VENTA PUBLICO GENERAL"
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Nombre") = "VENTA PUBLICO GENERAL"
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Direccion") = ""
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Direccion") = ""
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Celula") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Celula") = 0
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Ruta") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Ruta") = 0
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("AñoPed") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("AñoPed") = 0
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Pedido") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Pedido") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("AñoNota") = 0
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Nota") = 0
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("TipoPago") = "CONTADO"
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("TipoPago") = "CONTADO"
            GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("FormaPago") = 1
            dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("FormaPago") = 1
            Application.DoEvents()
        End If
    End Sub

    Private Sub btnDocumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumento.Click
        Dim frmRelacion As Relacion = New Relacion()
        Dim i, j As Integer
        Dim Encontro As Boolean

        dsLiquidacion2005.Tables.Item("NotaBlanca").AcceptChanges()
        dsLiquidacion2005.Tables.Item("Remision").AcceptChanges()

        frmRelacion.DsLiquidacion.Documento.Clear()

        For i = 0 To dsLiquidacion2005.Tables("Documento").Rows.Count - 1
            frmRelacion.DsLiquidacion.Documento.AddDocumentoRow(CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Banco"), Integer), CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Cheque"), String), _
                                                                CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("FCheque"), DateTime), CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Cuenta"), String), _
                                                                CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Monto"), Decimal), CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Disponible"), Decimal), _
                                                                CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("DesBanco"), String), CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Llave"), Integer), _
                                                                CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Tipo"), Integer), CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("TipoDes"), String), _
                                                                CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Cliente"), Integer), CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("Nombre"), String), _
                                                                CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("PosFechado"), String), CType(dsLiquidacion2005.Tables("Documento").Rows(i).Item("TPV"), Boolean))
        Next

        frmRelacion.DsLiquidacion.Detalle.Clear()
        For i = 0 To dsLiquidacion2005.Tables("Detalle").Rows.Count - 1
            frmRelacion.DsLiquidacion.Detalle.AddDetalleRow(CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Cliente"), String), CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Monto"), Decimal), _
                                                            CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Tipo"), Integer), CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("DesTipo"), String), _
                                                            CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Banco"), Integer), CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Cheque"), String), _
                                                            CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Cuenta"), String), CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("NombreBanco"), String), CType(dsLiquidacion2005.Tables("Detalle").Rows(i).Item("Nombre"), String))
        Next

        frmRelacion.DsLiquidacion.Cliente.Clear()
        For i = 0 To dsLiquidacion2005.Tables("Cliente").Rows.Count - 1
            If Not ClienteExisteParaCheque(CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Cliente"), Integer)) Then
                frmRelacion.DsLiquidacion.Cliente.AddClienteRow(CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Monto"), Decimal), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Nombre"), String), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Disponible"), Decimal), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Cliente"), Integer), _
                CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Litros"), Double), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Tipo"), Integer), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("DesTipo"), String))
            End If
        Next


        'For i = 0 To dsLiquidacion2005.Tables("Cliente").Rows.Count - 1
        '    frmRelacion.DsLiquidacion.Cliente.AddClienteRow(CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Monto"), Decimal), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Nombre"), String), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Disponible"), Decimal), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Cliente"), Integer), _
        '    CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Litros"), Double), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("Tipo"), Integer), CType(dsLiquidacion2005.Tables("Cliente").Rows(i).Item("DesTipo"), String))
        'Next

        For i = 0 To dsLiquidacion2005.Tables.Item("Remision").Rows.Count - 1
            Encontro = False
            'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
            If CType(dsLiquidacion2005.Tables.Item("Remision").Rows(i).Item("Litros"), Double) <> 0 And CType(dsLiquidacion2005.Tables.Item("Remision").Rows(i).Item("TipoPago"), String) = "CONTADO" Then
                For j = 0 To dsLiquidacion2005.Tables("Cliente").Rows.Count - 1
                    If CType(dsLiquidacion2005.Tables("Cliente").Rows(j).Item("Cliente"), Integer) = CType(dsLiquidacion2005.Tables.Item("Remision").Rows(i).Item("Cliente"), Integer) Then
                        Encontro = True
                    End If
                Next

                Dim Tipo As Integer
                Dim DesTipo As String

                If Encontro = False Then

                    If CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("FormaPago"), Integer) = 1 Then
                        Tipo = 0
                        DesTipo = "CH"
                    Else
                        Tipo = 1
                        DesTipo = "TC"
                    End If

                    If Not ClienteExisteCheque(CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Cliente"), Integer), 1) Then
                        frmRelacion.DsLiquidacion.Cliente.AddClienteRow(CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Importe"), Decimal), CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Nombre"), String), CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Importe"), Decimal), CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Cliente"), Integer), _
                            CType(dsLiquidacion2005.Tables("Remision").Rows(i).Item("Litros"), Double), Tipo, DesTipo)
                    End If

                End If

            End If

        Next

        For j = 0 To frmRelacion.DsLiquidacion.Tables("Cliente").Rows.Count - 1
            dsLiquidacion2005.Tables("Remision").DefaultView.RowFilter = " Cliente = " + CType(frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Cliente"), String)
            If dsLiquidacion2005.Tables("Remision").DefaultView.Count > 0 Then
                Dim Tipo As Integer
                Dim DesTipo As String
                If CType(dsLiquidacion2005.Tables("Remision").DefaultView(0).Item("FormaPago"), Integer) = 1 Then
                    Tipo = 0
                    DesTipo = "CH"
                Else
                    Tipo = 1
                    DesTipo = "TC"
                End If
                frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Destipo") = DesTipo
                frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Tipo") = Tipo
                frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Monto") = CType(dsLiquidacion2005.Tables("Remision").DefaultView(0).Item("Importe"), Decimal)

            End If
        Next
        dsLiquidacion2005.Tables("Remision").DefaultView.RowFilter = ""


        For i = 0 To dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count - 1
            Encontro = False
            If CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(i).Item("Litros"), Integer) <> 0 And CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(i).Item("TipoPago"), String) = "CONTADO" And CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(i).Item("Cliente"), Integer) <> 0 Then
                If CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(i).Item("Cliente"), Integer) <> _ClienteGlobal Then
                    For j = 0 To dsLiquidacion2005.Tables("Cliente").Rows.Count - 1
                        If CType(dsLiquidacion2005.Tables("Cliente").Rows(j).Item("Cliente"), Integer) = CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(i).Item("Cliente"), Integer) Then
                            Encontro = True
                        End If
                    Next
                    If Encontro = False Then
                        Dim Tipo As Integer
                        Dim DesTipo As String
                        If CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(i).Item("FormaPago"), Integer) = 1 Then
                            Tipo = 0
                            DesTipo = "CH"
                        Else
                            Tipo = 1
                            DesTipo = "TC"
                        End If

                        If Not ClienteExisteCheque(CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(i).Item("Cliente"), Integer), 2) Then
                            frmRelacion.DsLiquidacion.Cliente.AddClienteRow(CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(i).Item("Importe"), Decimal), CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(i).Item("Nombre"), String), CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(i).Item("Importe"), Decimal), CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(i).Item("Cliente"), Integer), _
                            CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(i).Item("Litros"), Double), Tipo, DesTipo)
                        End If
                    End If
                End If
            End If
        Next

        For j = 0 To frmRelacion.DsLiquidacion.Tables("Cliente").Rows.Count - 1
            dsLiquidacion2005.Tables("NotaBlanca").DefaultView.RowFilter = " Cliente = " + CType(frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Cliente"), String)
            If dsLiquidacion2005.Tables("NotaBlanca").DefaultView.Count > 0 Then
                Dim Tipo As Integer
                Dim DesTipo As String
                If CType(dsLiquidacion2005.Tables("NotaBlanca").DefaultView(0).Item("FormaPago"), Integer) = 1 Then
                    Tipo = 0
                    DesTipo = "CH"
                Else
                    Tipo = 1
                    DesTipo = "TC"
                End If
                frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Destipo") = DesTipo
                frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Tipo") = Tipo
                frmRelacion.DsLiquidacion.Tables("Cliente").Rows(j).Item("Monto") = CType(dsLiquidacion2005.Tables("NotaBlanca").DefaultView(0).Item("Importe"), Decimal)
            End If
        Next
        dsLiquidacion2005.Tables("NotaBlanca").DefaultView.RowFilter = ""

        frmRelacion.Entrada(0, 0, _Fecha)

        dsLiquidacion2005.Tables.Item("Documento").Clear()
        For i = 0 To frmRelacion.DsLiquidacion.Documento.Count - 1
            Dim myRow As DataRow
            myRow = dsLiquidacion2005.Tables.Item("Documento").NewRow()
            myRow("Banco") = frmRelacion.DsLiquidacion.Documento(i).Banco
            myRow("Cheque") = frmRelacion.DsLiquidacion.Documento(i).Cheque
            myRow("FCheque") = frmRelacion.DsLiquidacion.Documento(i).FCheque
            myRow("Cuenta") = frmRelacion.DsLiquidacion.Documento(i).Cuenta
            myRow("Monto") = frmRelacion.DsLiquidacion.Documento(i).Monto
            myRow("Disponible") = frmRelacion.DsLiquidacion.Documento(i).Disponible
            myRow("DesBanco") = frmRelacion.DsLiquidacion.Documento(i).DesBanco
            myRow("Llave") = frmRelacion.DsLiquidacion.Documento(i).Llave
            myRow("Tipo") = frmRelacion.DsLiquidacion.Documento(i).Tipo
            myRow("TipoDes") = frmRelacion.DsLiquidacion.Documento(i).TipoDes
            myRow("Cliente") = frmRelacion.DsLiquidacion.Documento(i).Cliente
            myRow("Nombre") = frmRelacion.DsLiquidacion.Documento(i).Nombre
            myRow("PosFechado") = frmRelacion.DsLiquidacion.Documento(i).PosFechado()
            myRow("TPV") = frmRelacion.DsLiquidacion.Documento(i).TPV
            dsLiquidacion2005.Tables.Item("Documento").Rows.Add(myRow)
        Next

        dsLiquidacion2005.Tables.Item("Detalle").Clear()
        For i = 0 To frmRelacion.DsLiquidacion.Detalle.Count - 1
            Dim myRow As DataRow
            myRow = dsLiquidacion2005.Tables.Item("Detalle").NewRow()
            myRow("Cliente") = frmRelacion.DsLiquidacion.Detalle(i).Cliente
            myRow("Monto") = frmRelacion.DsLiquidacion.Detalle(i).Monto
            myRow("Tipo") = frmRelacion.DsLiquidacion.Detalle(i).Tipo
            myRow("DesTipo") = frmRelacion.DsLiquidacion.Detalle(i).DesTipo
            myRow("Banco") = frmRelacion.DsLiquidacion.Detalle(i).Banco
            myRow("Cheque") = frmRelacion.DsLiquidacion.Detalle(i).Cheque
            myRow("Cuenta") = frmRelacion.DsLiquidacion.Detalle(i).Cuenta
            myRow("Nombrebanco") = frmRelacion.DsLiquidacion.Detalle(i).NombreBanco
            myRow("Nombre") = frmRelacion.DsLiquidacion.Detalle(i).Nombre
            dsLiquidacion2005.Tables.Item("Detalle").Rows.Add(myRow)
        Next

        dsLiquidacion2005.Tables.Item("Cliente").Clear()
        For i = 0 To frmRelacion.DsLiquidacion.Cliente.Count - 1
            Dim myRow As DataRow
            myRow = dsLiquidacion2005.Tables.Item("Cliente").NewRow()
            myRow("Monto") = frmRelacion.DsLiquidacion.Cliente(i).Monto
            myRow("Nombre") = frmRelacion.DsLiquidacion.Cliente(i).Nombre
            myRow("Disponible") = frmRelacion.DsLiquidacion.Cliente(i).Disponible
            myRow("Cliente") = frmRelacion.DsLiquidacion.Cliente(i).Cliente
            myRow("Litros") = frmRelacion.DsLiquidacion.Cliente(i).Litros
            myRow("Tipo") = frmRelacion.DsLiquidacion.Cliente(i).Tipo
            myRow("DesTipo") = frmRelacion.DsLiquidacion.Cliente(i).DesTipo
            dsLiquidacion2005.Tables.Item("Cliente").Rows.Add(myRow)
        Next

        frmRelacion.Dispose()

        gridDocumento.DataSource = dsLiquidacion2005.Tables.Item("Documento")
        gridDocumento.Refresh()

        CalCulaTotales()
    End Sub

    Private Sub GridView2_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView2.GotFocus
        If dsLiquidacion2005.Tables("NotaBlanca").Rows.Count > 0 Then
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = ""
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = "Cliente=" + CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), String)
        End If
    End Sub

    Private Sub GridView1_GotFocus(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView1.GotFocus
        If dsLiquidacion2005.Tables("Remision").Rows.Count > 0 Then
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = ""
            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = "Cliente=" + CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Cliente"), String)
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Cursor = Cursors.WaitCursor
        Dim oReporte As New frmConsultaReporte(_AñoAtt, _Folio)
        oReporte.ShowDialog()
        Cursor = Cursors.Default
    End Sub

#Region "Validacion de creditos"
#Region "Crédito del cliente"
    'TODO: Vaidacion de importe máximo de crédito
    Dim clienteParaValidacionCredito As Integer
    'Para bloquear la liquidación a crédito del cliente se cambia de sub a function
    Public Function maximoImporteCreditoExcedido(ByVal AplicaValidacion As Boolean, ByVal Cliente As Integer, _
         ByVal Importe As Decimal, ByVal Connection As SqlClient.SqlConnection) As Boolean
        'TODO: Parametrizar celula 6
        If AplicaValidacion And (_Carburacion = False) Then
            Dim cmdSelect As New SqlClient.SqlCommand()
            'TODO: SE COLOCARÁ EN PROCEDIMIENTO ALMACENADO, ANALIZAR COMO INTEGRAR A LA CONSULTA GENERAL DE CLIENTES
            'cmdSelect.CommandText = "SELECT Saldo, MaxImporteCredito, Cartera FROM Cliente WHERE Cliente = @Cliente"
            cmdSelect.CommandText = "spCCLiqConsultaDatosCreditoPreliquidacion"
            cmdSelect.CommandType = CommandType.StoredProcedure
            cmdSelect.Connection = Connection
            cmdSelect.Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
            Dim reader As SqlClient.SqlDataReader = Nothing
            Dim saldo As Decimal
            Dim maxCredito As Decimal
            Dim tipoCartera As Byte
            'Para bloquear la liquidación a crédito del cliente
            Dim creditoAutorizado As Boolean = True
            Dim mensaje As New System.Text.StringBuilder()
            '**
            Try
                reader = cmdSelect.ExecuteReader
                While reader.Read
                    saldo = CType(IIf(CType(reader("saldo"), Decimal) < 0, 0, reader("saldo")), Decimal)
                    maxCredito = CType(reader("maximportecredito"), Integer)
                    tipoCartera = CType(IIf(reader("Cartera") Is DBNull.Value, 0, reader("Cartera")), Byte)
                    reader.NextResult()
                End While
                If tipoCartera = GLOBAL_ClaveCreditoAutorizado Then
                    If (saldo + Importe) > maxCredito Then


                        mensaje.Append("El importe máximo de crédito para este cliente ($" & CStr(Importe) & ")" & _
                        vbCrLf & "ha sido rebasado")

                        If Not _permitirLiquidacionCredito Then
                            creditoAutorizado = False

                            mensaje.Append(vbCrLf & _
                            "Debe liquidar de contado o solicitar la ampliación" & vbCrLf & _
                            "del límite de crédito del cliente.")
                        End If

                        MessageBox.Show(mensaje.ToString(), "Cliente no. " & CStr(Cliente), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Else
                    mensaje.Append("Este cliente no tiene línea de crédito")

                    If Not _permitirLiquidacionCredito Then
                        creditoAutorizado = False

                        mensaje.Append(vbCrLf & _
                        "Debe liquidar de contado o solicitar la autorización" & vbCrLf & _
                        "de crédito correspondiente.")
                    End If


                    MessageBox.Show(mensaje.ToString(), _
                        "Cliente no. " & CStr(Cliente), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If


            Catch ex As Exception
                'Se debería mostrar una advertencia de error
                Debug.WriteLine(ex.Message)
            Finally
                reader.Close()
                cmdSelect.Dispose()
            End Try

            Return creditoAutorizado
        End If
    End Function
#End Region

    Private Sub validacionCreditoOperadorCierre()
        Dim litros, _
            importe As Double
        Dim row As DataRow = Nothing

        If Not IsDBNull(dsLiquidacion2005.Tables("NotaBlanca").Compute("SUM(Litros)", "FormaPago=3")) AndAlso _
            Not IsDBNull(dsLiquidacion2005.Tables("NotaBlanca").Compute("SUM(Importe)", "FormaPago=3")) Then
            litros += CType(dsLiquidacion2005.Tables("NotaBlanca").Compute("SUM(Litros)", "FormaPago=3"), Double)
            importe += CType(dsLiquidacion2005.Tables("NotaBlanca").Compute("SUM(Importe)", "FormaPago=3"), Double)
        End If

        If Not IsDBNull(dsLiquidacion2005.Tables("Remision").Compute("SUM(Litros)", "FormaPago=3")) AndAlso _
            Not IsDBNull(dsLiquidacion2005.Tables("Remision").Compute("SUM(Importe)", "FormaPago=3")) Then
            litros += CType(dsLiquidacion2005.Tables("Remision").Compute("SUM(Litros)", "FormaPago=3"), Double)
            importe += CType(dsLiquidacion2005.Tables("Remision").Compute("SUM(Importe)", "FormaPago=3"), Double)
        End If

        If litros > 0 AndAlso importe > 0 Then
            validacionCreditoOperador.ValidaSaldoOperador(litros, importe)
        End If


    End Sub

#End Region

    Private Sub Liquidacion2005_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        'If Not _CerroLiquidacion Then
        '    Dim cmdBorra As New SqlClient.SqlCommand()
        '    cmdBorra.CommandText = "Delete from Pedido where Status='PORAPLICAR' and AñoAtt=@AñoAtt and Folio=@Folio and TipoPedido=3 and Usuario=dbo.UserName() "
        '    cmdBorra.CommandType = CommandType.Text
        '    cmdBorra.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
        '    cmdBorra.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
        '    cmdBorra.Connection = SqlConnection

        '    'Se valida si la conexión está cerrada, si está cerrada se abre nuevamente jagd 21/08/2006
        '    Try
        '        If SqlConnection.State = ConnectionState.Closed Then
        '            SqlConnection.Open()
        '        End If
        '        cmdBorra.ExecuteNonQuery()
        '        cmdBorra.Dispose()
        '    Catch ex As Exception
        '        MessageBox.Show("Error al restablecer registros no aplicados" & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Finally
        '        SqlConnection.Close()
        '    End Try
        'End If
        If SqlConnection.State = ConnectionState.Open Then
            SqlConnection.Close()
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim Transaccion As SqlClient.SqlTransaction = Nothing
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim NuevaConexion As New SqlClient.SqlConnection()



        Dim i, j As Integer

        If dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count > 0 Then
            dsLiquidacion2005.Tables.Item("NotaBlanca").AcceptChanges()
        End If

        If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then
            dsLiquidacion2005.Tables.Item("Remision").AcceptChanges()
        End If

        CalCulaTotales()

        If _CerroLiquidacion Then
            _CerroLiquidacion = False
        End If

        'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
        If _Totalizador <> CType(lbTotalLitros.Text, Decimal) Then
            If MsgBox("El litraje a liquidar es DIFERENTE al TOTALIZADOR de la bascula." + Chr(13) + "El litraje a liquidar es de : " + CType(CType(lbTotalLitros.Text, Double), String) + " litros." + Chr(13) + "El litraje del totalizador de la bascula es : " + CType(_Totalizador, String) + " litros." + Chr(13) + Chr(13) + "Aun asi ¿Desea realizar la liquidación?.", MsgBoxStyle.YesNo, "Mensaje del sistema") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If

        If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente=0 and Litros<>0")) Then
            If CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente=0 and Litros<>0"), Integer) > 0 Then
                MsgBox("Existe un registro de nota blanca con litraje mayor a CERO y sin un contrato asignado. Verificar", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                Exit Sub
            End If
        End If

        If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente<>0 and Litros=0")) Then
            If CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Cliente)", "Cliente<>0 and Litros=0"), Integer) > 0 Then
                MsgBox("Existe un registro de nota blanca con litraje igual a CERO y con un contrato asignado. Verificar", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                Exit Sub
            End If
        End If

        If Global_TipoLiquidacion = "S" Then

            If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then
                If Not IsDBNull(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Codigo)", "Codigo='' and Cliente<>0 and Litros<>0")) Then
                    If CType(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Codigo)", "Codigo='' and Cliente<>0 and Litros<>0"), Integer) > 0 Then
                        MsgBox("Faltan notas de remision por asignar de folio.", MsgBoxStyle.Information, "Mensaje del sistema")
                        Exit Sub
                    End If
                End If
            End If

            If dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count > 0 Then
                If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Codigo)", "Codigo='' and Cliente<>0 and Litros<>0")) Then
                    If CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Codigo)", "Codigo='' and Cliente<>0 and Litros<>0"), Integer) > 0 Then
                        MsgBox("Falta notas blancas por asignar de folio.", MsgBoxStyle.Information, "Mensaje del sistema")
                        Exit Sub
                    End If
                End If
            End If

        End If


        'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
        If CType(lbTotalLitros.Text, Double) = 0 Then
            MsgBox("No hay NINGUN importe a liquidar. Verifique.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        Dim Tabla As String = "Remision"
        For j = 0 To 1
            For i = 0 To dsLiquidacion2005.Tables(Tabla).Rows.Count - 1
                'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
                If CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), Double) > 0 Then
                    If CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("TipoPago"), String) = "CREDITO" And _
                       CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("FormaPago"), Integer) = 1 Then
                        MsgBox("Se tiene capturado el cliente " & _
                                CStr(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Cliente")) & _
                                " en " & Tabla & " con TipoPago CREDITO y FormaPago EFECTIVO, " & _
                                "favor de verificar.", MsgBoxStyle.Information, "Mensaje del sistema")
                        Exit Sub
                    End If
                End If

            Next
            Tabla = "NotaBlanca"
        Next

        Try
            NuevaConexion.ConnectionString = GLOBAL_ConString
            NuevaConexion.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        If _CorrerDescarga Then
            Tabla = "Remision"
            For j = 0 To 1
                For i = 0 To dsLiquidacion2005.Tables(Tabla).Rows.Count - 1
                    'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
                    If CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), Double) > 0 Then

                        Dim cmdLitrajeRampac As New SqlClient.SqlCommand()
                        cmdLitrajeRampac.CommandText = "EXEC spCCLIQValidaLitrosRampac @AñoAtt, @Folio, @Litros"
                        cmdLitrajeRampac.CommandType = CommandType.Text
                        cmdLitrajeRampac.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AñoAtt
                        cmdLitrajeRampac.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                        'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
                        cmdLitrajeRampac.Parameters.Add("@Litros", SqlDbType.Decimal).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), Double)
                        cmdLitrajeRampac.Connection = NuevaConexion
                        Dim rdrLiquidacion As SqlClient.SqlDataReader

                        'Se valida si la conexión está cerrada, si está cerrada se abre nuevamente jagd 21/08/2006
                        If NuevaConexion.State = ConnectionState.Closed Then
                            NuevaConexion.Open()
                        End If

                        rdrLiquidacion = cmdLitrajeRampac.ExecuteReader
                        rdrLiquidacion.Read()
                        Dim Cantidad As Int16 = CType(rdrLiquidacion("Cantidad"), Int16)
                        rdrLiquidacion.Close()
                        If Cantidad = 0 Then
                            If MessageBox.Show("¿El litraje de " & CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), String) & _
                                                                           " litros no existe en la Rampac. Desea continuar?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                                Exit Sub
                            Else
                                Tabla = ""
                                Exit For
                            End If
                        End If
                        cmdLitrajeRampac.Dispose()
                    End If

                Next

                If Tabla = "" Then
                    Exit For
                Else
                    Tabla = "NotaBlanca"
                End If

            Next
        End If

        Dim SaldoAFavor As Boolean = False
        If GLOBAL_SaldoAFavorLiquidacion Then
            Dim Saldo As Double = SaldoAFavorCheques(dsLiquidacion2005.Tables("Documento"))
            If Saldo > 0 Then
                SaldoAFavor = (MessageBox.Show("Los cheques capturados generan un sobrante de: " & FormatCurrency(Saldo, 2) & Chr(13) & _
                 "¿Desea registrarlo como saldo a favor?", Me.Text, _
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes)
            End If
        End If

        If MessageBox.Show("Esta a punto de guardar la liquidación" & Chr(13) & _
                           "con los siguientes datos:" & Chr(13) & _
                           "Importe Contado : " & lbTotalContado.Text & Chr(13) & _
                           "Importe Cheques : " & lblTotalCheques.Text & Chr(13) & _
                           "Importe TPV : " & lbTotalTPV.Text & Chr(13) & _
                           "Importe credito: " & lbTotalCredito.Text & Chr(13) & _
                           "¿Desea continuar?", "Preliquidación", _
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> MsgBoxResult.Yes Then
            Exit Sub
        End If

        validacionCreditoOperadorCierre()

        Application.DoEvents()

        Try
            btnAceptar.Enabled = False

            RemoveHandler btnCancelar.Click, AddressOf btnCancelar_Click
            RemoveHandler Me.Closed, AddressOf Liquidacion2005_Closed

            'Se valida si la conexión está cerrada, si está cerrada se abre nuevamente jagd 21/08/2006
            'If NuevaConexion.State = ConnectionState.Closed Then
            'NuevaConexion.Open()
            'End If

            Transaccion = NuevaConexion.BeginTransaction
            cmdInsert.Connection = NuevaConexion
            cmdInsert.Transaction = Transaccion
            cmdInsert.CommandTimeout = _TimeOut
            cmdInsert.CommandType = CommandType.StoredProcedure
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "spCCBorraLiquidacion"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
            cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio

            cmdInsert.ExecuteNonQuery()

            dsLiquidacion2005.Tables("Detalle").DefaultView.RowFilter = ""
            If dsLiquidacion2005.Tables("Detalle").DefaultView.Count > 0 Then
                For j = 0 To dsLiquidacion2005.Tables("Detalle").DefaultView.Count - 1
                    dsLiquidacion2005.Tables("Documento").DefaultView.RowFilter = ""
                    dsLiquidacion2005.Tables("Documento").DefaultView.RowFilter = " Banco = " + CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Banco"), String) + " and Cuenta='" + CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Cuenta"), String) + "' and Cheque='" + CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Cheque"), String) + "' "

                    cmdInsert.CommandType = CommandType.StoredProcedure
                    cmdInsert.CommandText = "spCCLiquidacionCheques"
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                    cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Cliente"), Integer)
                    cmdInsert.Parameters.Add("@Banco", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Banco"), Integer)
                    cmdInsert.Parameters.Add("@Cheque", SqlDbType.Char).Value = CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Cheque"), String)
                    cmdInsert.Parameters.Add("@FCheque", SqlDbType.DateTime).Value = CType(dsLiquidacion2005.Tables("Documento").DefaultView.Item(0).Item("FCheque"), DateTime)
                    cmdInsert.Parameters.Add("@Cuenta", SqlDbType.Char).Value = CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Cuenta"), String)
                    cmdInsert.Parameters.Add("@MontoTotal", SqlDbType.Money).Value = CType(dsLiquidacion2005.Tables("Documento").DefaultView.Item(0).Item("Monto"), Decimal)
                    cmdInsert.Parameters.Add("@Disponible", SqlDbType.Money).Value = CType(dsLiquidacion2005.Tables("Documento").DefaultView.Item(0).Item("Disponible"), Decimal)
                    If CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Tipo"), Integer) = 0 Then
                        cmdInsert.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = 3
                    Else
                        cmdInsert.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = 6
                    End If


                    cmdInsert.Parameters.Add("@ClientePadre", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables("Documento").DefaultView.Item(0).Item("Cliente"), Integer)
                    cmdInsert.Parameters.Add("@MontoHijo", SqlDbType.Money).Value = CType(dsLiquidacion2005.Tables("Detalle").Rows(j).Item("Monto"), Decimal)
                    cmdInsert.Parameters.Add("@TPV", SqlDbType.Bit).Value = CType(dsLiquidacion2005.Tables("Documento").DefaultView.Item(0).Item("TPV"), Boolean)

                    'Se valida si la conexión está cerrada, si está cerrada se abre nuevamente jagd 21/08/2006
                    'If SqlConnection.State = ConnectionState.Closed Then
                    'SqlConnection.Open()
                    'End If

                    cmdInsert.ExecuteNonQuery()
                Next
            End If

            Tabla = "Remision"
            For j = 0 To 1
                For i = 0 To dsLiquidacion2005.Tables(Tabla).Rows.Count - 1

                    If CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), Integer) > 0 Then

                        Dim Tipo As Integer
                        Select Case CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("TipoPago"), String)
                            Case "CREDITO"
                                Select Case CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("FormaPago"), Integer)
                                    Case 2 : Tipo = 8
                                    Case 3 : Tipo = 9
                                    Case 4 : Tipo = 6
                                End Select
                            Case "OBSEQUIO"
                                Tipo = 15
                            Case "CONTADO"
                                'Select Case CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("FormaPago"), Integer)
                                '    Case 1 : Tipo = 5
                                '    Case 4 : Tipo = 6
                                'End Select
                                Tipo = 5
                            Case Else
                                Tipo = 5
                        End Select

                        cmdInsert.CommandType = CommandType.StoredProcedure
                        cmdInsert.CommandText = "spCCLiquidacionTabla"
                        cmdInsert.Parameters.Clear()
                        cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                        cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                        cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Pedido"), Integer)
                        cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("AñoPed"), Integer)
                        cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Celula"), Integer)
                        cmdInsert.Parameters.Add("@CelulaCliente", SqlDbType.Int).Value = _Celula
                        cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Cliente"), Integer)
                        cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                        cmdInsert.Parameters.Add("@RutaCliente", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Ruta"), Integer)
                        'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
                        cmdInsert.Parameters.Add("@Litros", SqlDbType.Decimal).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Litros"), Double)
                        cmdInsert.Parameters.Add("@Precio", SqlDbType.Decimal).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Precio"), Decimal)
                        cmdInsert.Parameters.Add("@Importe", SqlDbType.Decimal).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Importe"), Decimal)
                        cmdInsert.Parameters.Add("@Iva", SqlDbType.Decimal).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Importe"), Decimal) * 0.15
                        cmdInsert.Parameters.Add("@Autotanque", SqlDbType.Int).Value = CType(lbAutotanque.Text, Integer)
                        cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = Tipo
                        cmdInsert.Parameters.Add("@AñoNota", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("AñoNota"), Integer)
                        cmdInsert.Parameters.Add("@Nota", SqlDbType.Int).Value = CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Nota"), Integer)
                        cmdInsert.Parameters.Add("@Banco", SqlDbType.Int).Value = 0
                        cmdInsert.Parameters.Add("@Cheque", SqlDbType.Char).Value = ""
                        cmdInsert.Parameters.Add("@FCheque", SqlDbType.DateTime).Value = Now.Date
                        cmdInsert.Parameters.Add("@Cuenta", SqlDbType.Char).Value = ""
                        cmdInsert.Parameters.Add("@MontoTotal", SqlDbType.Decimal).Value = 0
                        cmdInsert.Parameters.Add("@Disponible", SqlDbType.Decimal).Value = 0
                        cmdInsert.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = 0
                        cmdInsert.Parameters.Add("@ClientePadre", SqlDbType.Int).Value = 0
                        cmdInsert.Parameters.Add("@MontoHijo", SqlDbType.Decimal).Value = 0
                        cmdInsert.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha

                        'para la liquidación del uds
                        If Tabla = "NotaBlanca" Then
                            If CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Codigo"), String) <> "" Then
                                DocumentosBSR.SerieDocumento.SeparaSerie(CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Codigo"), String))
                                cmdInsert.Parameters.Add("@FolioNota", SqlDbType.Int).Value = DocumentosBSR.SerieDocumento.FolioNota 'CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Codigo"), Integer)
                                cmdInsert.Parameters.Add("@SerieRemision", SqlDbType.VarChar).Value = DocumentosBSR.SerieDocumento.Serie 'CType(dsLiquidacion2005.Tables(Tabla).Rows(i).Item("Codigo"), Integer)
                            Else
                                cmdInsert.Parameters.Add("@FolioNota", SqlDbType.Int).Value = 0
                            End If
                        End If

                        'Se valida si la conexión está cerrada, si está cerrada se abre nuevamente jagd 21/08/2006
                        'If SqlConnection.State = ConnectionState.Closed Then
                        'SqlConnection.Open()
                        'End If
                        cmdInsert.ExecuteNonQuery()

                    End If


                Next
                Tabla = "NotaBlanca"
            Next

            Application.DoEvents()

            Dim TipoLiquidacion As String = CStr(IIf(_CorrerDescarga, "AUTOMATICA", "MANUAL"))

            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "spCCLiquidacionGeneral"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
            cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
            cmdInsert.Parameters.Add("@ClienteGeneral", SqlDbType.Int).Value = _ClienteGlobal
            cmdInsert.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
            cmdInsert.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = CType(lbTotalCredito.Text, Decimal)
            cmdInsert.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = CType(lbTotalContado.Text, Decimal) + CType(lblTotalCheques.Text, Decimal) + CType(lbTotalTPV.Text, Decimal)
            'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
            cmdInsert.Parameters.Add("@LitrosCredito", SqlDbType.Decimal).Value = CType(lbLitrosCredito.Text, Double)
            'TODO: Se cambia el tipo de dato de los litros a decimal JAGD 25-10-2005
            cmdInsert.Parameters.Add("@LitrosContado", SqlDbType.Decimal).Value = CType(lbLitrosContado.Text, Double) + CType(lbLitrosCheques.Text, Double) + CType(lbLitrosTPV.Text, Double)
            cmdInsert.Parameters.Add("@TipoLiquidacion", SqlDbType.Char).Value = TipoLiquidacion

            'Corrección de liquidación 29112006
            cmdInsert.Parameters.Add("@FechaRealSuministro", SqlDbType.DateTime).Value = _fechaSuministro
            cmdInsert.Parameters.Add("@RutaSuministro", SqlDbType.SmallInt).Value = _Ruta

            'Para control de saldos a favor JAGD 25-10-2005
            If SaldoAFavor Then
                cmdInsert.Parameters.Add("@SaldoAFavor", SqlDbType.Bit).Value = SaldoAFavor
            End If



            'Se valida si la conexión está cerrada, si está cerrada se abre nuevamente jagd 21/08/2006
            'If SqlConnection.State = ConnectionState.Closed Then
            'SqlConnection.Open()
            'End If
            cmdInsert.ExecuteNonQuery()

            Transaccion.Commit()
            _CerroLiquidacion = True

            Me.Cursor = Cursors.Default

        Catch et As Exception
            'Data.SqlClient.SqlException()
            Dim errorMessage As String = Nothing

            'La transacción es "deshecha" por el servidor, pero la conexión se rompe
            Try
                Transaccion.Rollback()
            Catch ex As Exception
                errorMessage = ex.Message
            End Try

            btnAceptar.Enabled = True
            _CerroLiquidacion = False

            Dim fileName As String
            fileName = Application.StartupPath + "\dsLiquidacion" + _AñoAtt.ToString + _Folio.ToString + _
                Now.Day.ToString + Now.Month.ToString + Now.Year.ToString + _
                Now.Hour.ToString + Now.Minute.ToString + Now.Second.ToString
            Try
                dsLiquidacion2005.WriteXml(fileName)
            Catch ex As Exception

            End Try

            'If et.Number = 10024 Then
            'MsgBox("En este momento el servidor esta ocupado. Intente nuevamente por favor.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            'Else
            MessageBox.Show(et.ToString & vbCrLf & errorMessage, et.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            'End If

        Finally
            If _CerroLiquidacion Then
                InactivaControls()
                MsgBox("El proceso de liquidación ya terminó. Gracias.", MsgBoxStyle.Information, "Mensaje del sistema")
                If MessageBox.Show("¿Desea imprimir los totales del reporte de liquidación?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    Cursor = Cursors.WaitCursor
                    Dim oReporte As New frmConsultaReporte(_AñoAtt, _Folio)
                    oReporte.ShowDialog()
                    Cursor = Cursors.Default

                End If
            End If

            NuevaConexion.Close()
            Me.Cursor = System.Windows.Forms.Cursors.Default

            AddHandler btnCancelar.Click, AddressOf btnCancelar_Click
            AddHandler Me.Closed, AddressOf Liquidacion2005_Closed
        End Try
    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Dim Contrato As Integer

        Dim frmBuscar As New SigaMetClasses.BusquedaCliente()
        If frmBuscar.ShowDialog() = DialogResult.OK Then
            Contrato = frmBuscar.Cliente

            If Contrato > 0 Then
                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente") = Contrato
                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Cliente") = Contrato

                'Dim cmdCliente As New SqlClient.SqlCommand()
                'cmdCliente.CommandText = "Select dbo.ClienteValido(@Cliente) as Valido, dbo.CelulaCliente(@Cliente) as Celula, dbo.RutaCliente(@Cliente) as Ruta, dbo.NombreCliente(@Cliente) as Nombre, dbo.DireccionCliente(@Cliente) as Direccion "
                'cmdCliente.CommandType = CommandType.Text
                'cmdCliente.Parameters.Add("@Cliente", SqlDbType.Int).Value = Contrato
                'cmdCliente.Connection = SqlConnection

                'Dim rdrLiquidacion As SqlClient.SqlDataReader
                'rdrLiquidacion = cmdCliente.ExecuteReader
                'rdrLiquidacion.Read()
                'Dim Valido As Int16 = CType(rdrLiquidacion("Valido"), Int16)
                'Dim Nombre As String = CType(rdrLiquidacion("Nombre"), String)
                'Dim Celula As Integer = CType(rdrLiquidacion("Celula"), Integer)
                'Dim Ruta As Integer = CType(rdrLiquidacion("Ruta"), Integer)
                'Dim Direccion As String = CType(rdrLiquidacion("Direccion"), String)
                'rdrLiquidacion.Close()
                'cmdCliente.Dispose()

                Dim rdrLiquidacion As SqlClient.SqlDataReader
                'Validación de crédito para edificios administrados 10-10-2006
                Dim _datosCliente As DatosCliente = consultaDatosCliente(Contrato)
                'Validación de crédito para edificios administrados 10-10-2006
                'If _datosCliente.Valido = 1 Then
                If _datosCliente.Valido Then
                    dsLiquidacion2005.Tables("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Nombre") = _datosCliente.Nombre
                    dsLiquidacion2005.Tables("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Direccion") = _datosCliente.Direccion

                    Dim Existe As Int16
                    If ClienteExiste(CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Cliente"), Integer)) Then
                        Existe = 1
                        MsgBox("Este Cliente ya se encuentra capturado en esta liquidación.", MsgBoxStyle.Information, "Mensaje del sistema")
                    End If

                    currentClient = Contrato

                    Dim cmdPedido As New SqlClient.SqlCommand()
                    cmdPedido.CommandText = "exec spCCGeneraPedidoLiquidacion @Cliente, @CelulaCliente, @RutaCliente, @AñoAtt, @Folio, @Existe, @Fecha, @Autotanque"
                    cmdPedido.CommandType = CommandType.Text
                    cmdPedido.Parameters.Add("@Cliente", SqlDbType.Int).Value = Contrato
                    cmdPedido.Parameters.Add("@CelulaCliente", SqlDbType.Int).Value = _datosCliente.Celula
                    cmdPedido.Parameters.Add("@RutaCliente", SqlDbType.Int).Value = _datosCliente.Ruta
                    cmdPedido.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                    cmdPedido.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                    cmdPedido.Parameters.Add("@Existe", SqlDbType.Int).Value = Existe
                    cmdPedido.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha
                    cmdPedido.Parameters.Add("@Autotanque", SqlDbType.Int).Value = CType(lbAutotanque.Text, Integer)
                    cmdPedido.Connection = SqlConnection
                    rdrLiquidacion = cmdPedido.ExecuteReader
                    rdrLiquidacion.Read()

                    If currentClient <> CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente"), Integer) Then
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Cliente") = currentClient
                        GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Cliente") = currentClient
                    End If

                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Celula") = CType(rdrLiquidacion("Celula"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Ruta") = CType(rdrLiquidacion("Ruta"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("AñoPed") = CType(rdrLiquidacion("AñoPed"), Int16)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Pedido") = CType(rdrLiquidacion("Pedido"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("AñoNota") = CType(rdrLiquidacion("AñoNota"), Integer)
                    dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Nota") = CType(rdrLiquidacion("Nota"), Integer)

                    'Validación de crédito para edificios administrados 10-10-2006
                    If GLOBAL_AdmEdificiosLiquidacionCredito AndAlso (_datosCliente.RamoCliente = GLOBAL_ClaveRamoClienteAdmEdificios) Then
                        dsLiquidacion2005.Tables("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("TipoPago") = "CREDITO"
                    End If

                    Application.DoEvents()

                    rdrLiquidacion.Close()
                    cmdPedido.Dispose()

                    If CType(dsLiquidacion2005.Tables("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("TipoPago"), String) = "CREDITO" Then
                        dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("FormaPago") = TarjetaAndOrCredito(rdrLiquidacion, Contrato)
                    End If
                End If
            End If
        End If
        frmBuscar.Dispose()
    End Sub

    Private Sub GridView2_ColumnFilterChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GridView2.ColumnFilterChanged
        If dsLiquidacion2005.Tables("NotaBlanca").Rows.Count > 0 Then
            If CType(GridView2.GetDataRow(0).Item("TipoPago"), String) = "CONTADO" And CType(GridView2.GetDataRow(0).Item("FormaPago"), Integer) <> 1 Then
                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("FormaPago") = 1
                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("FormaPago") = 1
            End If
        End If
    End Sub

    Private Sub txtLitros_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLitros.KeyUp, txtLitros1.KeyUp
        If e.KeyCode = Keys.F4 Then
            If GridView1.IsEditorFocused Then
                Dim APrecio As Decimal = CDec(IIf(CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio"), Decimal) = _PrecioAuxiliar, _PrecioVigente, _PrecioAuxiliar))
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Precio") = APrecio
                dsLiquidacion2005.Tables.Item("Remision").Rows(GridView1.FocusedRowHandle).Item("Precio") = APrecio
                GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe") = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros"), Integer) * APrecio
            End If
            If GridView2.IsEditorFocused Then
                Dim APrecio As Decimal = CDec(IIf(CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio"), Decimal) = _PrecioAuxiliar, _PrecioVigente, _PrecioAuxiliar))
                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Precio") = Aprecio
                dsLiquidacion2005.Tables.Item("NotaBlanca").Rows(GridView2.FocusedRowHandle).Item("Precio") = APrecio
                GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Importe") = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Litros"), Integer) * APrecio
            End If
        End If
    End Sub

    Private Sub txtLitros1_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLitros1.Enter
        'para decimales en liquidación
        DirectCast(sender, DevExpress.XtraEditors.SpinEdit).SelectAll()
    End Sub

    'para control de saldos a favor, recibe la tabla de cheques del dataset y devuelve el valor sobrante
    Private Function SaldoAFavorCheques(ByVal TablaCheques As DataTable) As Double
        Dim saldo As Double = 0
        saldo = CDbl(IIf((TablaCheques.Compute("SUM(Disponible)", "") Is DBNull.Value), _
                    0, _
                    TablaCheques.Compute("SUM(Disponible)", "")))
        Return saldo
    End Function

    'Para evitar el error de cambio de contrato
    Dim currentClient As Integer = 0

#Region "Consulta de precios válidos"
    'Consulta de precios
    Private Sub consultaPrecios()
        Dim cmdSelect As New SqlCommand(), _
            da As New SqlDataAdapter()
        cmdSelect.CommandText = "spCCLIQConsultaPrecioGLP"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Connection = CnnSigamet
        da.SelectCommand = cmdSelect
        Try
            da.Fill(_PreciosValidos)

            Dim dr As DataRow, i As Integer = 0


            For Each dr In _PreciosValidos.Rows
                cboPrecio.Properties.Items.Insert(i, CType(dr.Item("Precio"), String))
            Next

        Catch ex As SqlException
            MessageBox.Show("Ha ocurrido el siguiente error:" & ex.Number & " " & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido el siguiente error:" & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmdSelect.Dispose()
            da.Dispose()
        End Try
    End Sub

    'Selección de precios
    Private Function SeleccionaPrecios() As Double
        Dim _PrecioSeleccionado As Double
        Static index As Integer
        _PrecioSeleccionado = CType(_PreciosValidos.Rows(index).Item("Precio"), Double)
        index += 1
        If _PreciosValidos.Rows.Count = index Then
            index = 0
        End If
        Return _PrecioSeleccionado
    End Function

#End Region

#Region "Cálculo de subtotales"

    Private Sub CalculoSubtotalesObsequios(ByVal DatosNB As DataTable, ByVal DatosRemision As DataTable)
        Dim row As DataRow, _
            LitrosAutoCarburacion, _
            LitrosObsequio, _
            ImporteAutoCarburacion, _
            ImporteObsequio As Double

        For Each row In DatosNB.Rows
            If CType(row.Item("TipoPago"), String) = "OBSEQUIO" Then
                If ClienteAutoCarburacion(CType(row.Item("Cliente"), Integer)) Then
                    LitrosAutoCarburacion += CType(row.Item("Litros"), Double)
                    ImporteAutoCarburacion += CType(row.Item("Importe"), Double)
                Else
                    LitrosObsequio += CType(row.Item("Litros"), Double)
                    ImporteObsequio += CType(row.Item("Importe"), Double)
                End If
            End If
        Next

        For Each row In DatosRemision.Rows
            If CType(row.Item("TipoPago"), String) = "OBSEQUIO" Then
                If ClienteAutoCarburacion(CType(row.Item("Cliente"), Integer)) Then
                    LitrosAutoCarburacion += CType(row.Item("Litros"), Double)
                    ImporteAutoCarburacion += CType(row.Item("Importe"), Double)
                Else
                    LitrosObsequio += CType(row.Item("Litros"), Double)
                    ImporteObsequio += CType(row.Item("Importe"), Double)
                End If
            End If
        Next

        lblAutoCarb.Text = LitrosAutoCarburacion.ToString & " lt."
        lblImporteAutoCarb.Text = "$ " & ImporteAutoCarburacion.ToString

        lblObsequio.Text = LitrosObsequio.ToString & " lt."
        lblImporteObsequio.Text = "$ " & ImporteObsequio.ToString

    End Sub

    Private Function ClienteAutoCarburacion(ByVal Cliente As Integer) As Boolean
        Dim cmdSelect As New SqlCommand(), _
            retValue As Boolean
        cmdSelect.CommandText = "SELECT Cliente" & " " & _
                                "FROM   ClienteObsequio" & " " & _
                                "WHERE  Cliente = @Cliente" & " " & _
                                "       AND (TipoObsequio = 0" & " " & _
                                "            OR  TipoObsequio IS NULL)" & " " & _
                                "       AND Status = 'ACTIVO'"
        cmdSelect.CommandType = CommandType.Text
        cmdSelect.Parameters.Add("@Cliente", SqlDbType.VarChar).Value = Cliente
        cmdSelect.Connection = CnnSigamet
        Try
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If
            retValue = CType((Cliente = CType(cmdSelect.ExecuteScalar, Integer)), Boolean)
        Catch ex As SqlException
            MessageBox.Show("Ha ocurrido el siguiente error:" & " " & ex.Number & " " & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            retValue = False
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido el siguiente error:" & " " & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            retValue = False
        Finally
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()
        End Try
        Return retValue
    End Function

#End Region

#Region "Validación de clientes de obsequio y carburacióm"

    Private Function validacionObsequio(ByVal Cliente As Integer) As Boolean
        Dim cmdSelect As New SqlCommand(), _
                    da As New SqlDataAdapter(), _
                    tmpDataTable As New DataTable("Pedido"), _
                    retValue As Boolean = False

        cmdSelect.CommandText = "spCCLIQConsultaClienteObsequio"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Parameters.Add("@Cliente", SqlDbType.VarChar).Value = Cliente
        cmdSelect.Connection = CnnSigamet
        da.SelectCommand = cmdSelect

        Try
            da.Fill(tmpDataTable)

            If tmpDataTable.Rows.Count > 0 Then
                retValue = (CType(tmpDataTable.Rows(0).Item("Cliente"), Integer) = Cliente)
            End If
        Catch ex As SqlException
            MessageBox.Show("Ha ocurrido el siguiente error:" & " " & ex.Number & " " & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            retValue = False
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido el siguiente error:" & " " & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            retValue = False
        Finally
            cmdSelect.Dispose()
            da.Dispose()
        End Try
        Return retValue
    End Function

#End Region

    Private Sub ValidaCodigo(ByVal Tabla As String, ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs)

        Dim Codigo As String

        If Tabla = "Remision" Then
            Codigo = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Codigo"), String)
        Else
            Codigo = CType(GridView2.GetDataRow(GridView2.FocusedRowHandle).Item("Codigo"), String)
        End If

        If CodigoExisteGrid(Codigo) Then
            'MsgBox("Este Código se encuentra antes capturado en esta liquidación. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
            MsgBox("Este Código ya se capturó en esta liquidación. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
        Else
            If GLOBAL_ValidaSerieRemision Then

                'Dim rdrLiquidacion As SqlClient.SqlDataReader
                'Dim cmdCliente As New SqlClient.SqlCommand()
                'cmdCliente.CommandText = "Select dbo.SerieNotaCorrecto (@Codigo,@Ruta) as Registro"
                'cmdCliente.CommandType = CommandType.Text
                'cmdCliente.Parameters.Clear()
                'cmdCliente.Parameters.Add("@Codigo", SqlDbType.Char).Value = Codigo
                'cmdCliente.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                'cmdCliente.Connection = SqlConnection
                'rdrLiquidacion = cmdCliente.ExecuteReader
                'rdrLiquidacion.Read()
                'Dim Registro As Integer = CType(rdrLiquidacion("Registro"), Integer)
                'rdrLiquidacion.Close()
                'cmdCliente.Dispose()

                ''_serieRemisionRuta

                'If Registro = 0 Then
                '    MsgBox("La serie de esta nota no corresponde a la ruta.", MsgBoxStyle.Information, "Mensaje del sistema")
                'Else
                '    If Tabla = "Remision" Then
                '        GridView1.GetDataRow(e.RowHandle).Item("Codigo") = Codigo
                '    Else
                '        GridView2.GetDataRow(e.RowHandle).Item("Codigo") = Codigo
                '    End If

                '    dsLiquidacion2005.Tables.Item(Tabla).Rows(e.RowHandle).Item("Codigo") = Codigo
                '    Application.DoEvents()

                'End If

                DocumentosBSR.SerieDocumento.SeparaSerie(Codigo)

                If _serieRemisionRuta.Trim() <> DocumentosBSR.SerieDocumento.Serie Then
                    MessageBox.Show("La serie de esta nota no corresponde a la ruta.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    If Tabla = "Remision" Then
                        GridView1.GetDataRow(e.RowHandle).Item("Codigo") = Codigo

                    Else
                        GridView2.GetDataRow(e.RowHandle).Item("Codigo") = Codigo
                    End If

                    Exit Sub
                Else
                    If Tabla = "Remision" Then
                        GridView1.GetDataRow(e.RowHandle).Item("Codigo") = Codigo
                    Else
                        GridView2.GetDataRow(e.RowHandle).Item("Codigo") = Codigo
                    End If

                    dsLiquidacion2005.Tables.Item(Tabla).Rows(e.RowHandle).Item("Codigo") = Codigo
                    Application.DoEvents()

                End If

                'End If
            End If
        End If

        If remisionValida(Codigo) Then
            MessageBox.Show("El número de remisión " & Codigo & " ya fue capturado" & Chr(13) & _
                            "en una liquidación anterior. Favor de verificar", "Remisión no válida", _
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If


    End Sub

    Private Function CodigoExisteGrid(ByVal Codigo As String) As Boolean
        Dim CountRemision, CountNotaBlanca As Integer
        If dsLiquidacion2005.Tables.Item("Remision").Rows.Count > 0 Then
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Codigo)", "Codigo='" & CType(Codigo, String) & "'")) Then
                CountRemision = CType(dsLiquidacion2005.Tables.Item("Remision").Compute("Count(Codigo)", "Codigo='" & CType(Codigo, String) & "'"), Integer)
            End If
        End If
        If dsLiquidacion2005.Tables.Item("NotaBlanca").Rows.Count > 0 Then
            If Not IsDBNull(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Codigo)", "Codigo='" & CType(Codigo, String) & "'")) Then
                CountNotaBlanca = CType(dsLiquidacion2005.Tables.Item("NotaBlanca").Compute("Count(Codigo)", "Codigo='" & CType(Codigo, String) & "'"), Integer)
            End If
        End If
        CodigoExisteGrid = CBool(IIf(CountRemision + CountNotaBlanca > 0, True, False))
    End Function

    Private Function remisionValida(ByVal Codigo As String) As Boolean
        Dim cmdSelect As New SqlCommand()
        Dim reader As SqlDataReader = Nothing
        Dim retValue As Boolean
        'da As New SqlDataAdapter(), _
        'tmpDataTable As New DataTable("Pedido"), _

        cmdSelect.CommandText = "spCCLIQValidaRemision"
        cmdSelect.CommandType = CommandType.StoredProcedure
        'cmdSelect.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = Codigo
        DocumentosBSR.SerieDocumento.SeparaSerie(Codigo)
        cmdSelect.Parameters.Add("@Serie", SqlDbType.VarChar).Value = DocumentosBSR.SerieDocumento.Serie
        cmdSelect.Parameters.Add("@FolioNota", SqlDbType.VarChar).Value = DocumentosBSR.SerieDocumento.FolioNota
        cmdSelect.Connection = CnnSigamet

        'da.SelectCommand = cmdSelect
        Try
            ' da.Fill(tmpDataTable)
            CnnSigamet.Open()
            reader = cmdSelect.ExecuteReader()

            If reader.Read() Then
                retValue = True
            End If


            'If tmpDataTable.Rows.Count > 0 Then
            '    retValue = True
            'End If
        Catch ex As SqlException
            MessageBox.Show("Ha ocurrido el siguiente error:" & " " & ex.Number & " " & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            retValue = False
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido el siguiente error:" & " " & ex.message, _
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            retValue = False
        Finally
            reader.Close()
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()
            'da.Dispose()
        End Try
        Return retValue
    End Function

    Private Sub Liquidacion2005_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        btnAceptar.Enabled = _permitirLiquidacion
        btnImprimir.Enabled = _permitirLiquidacion
        btnDocumento.Enabled = _permitirLiquidacion
    End Sub

    Private Sub dgRemision_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgRemision.Click

    End Sub

    Private Sub lbUnificacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbUnificacion.Click

    End Sub

#Region "Validación de liquidación para edificios administrados"

    Private Structure DatosCliente
        Dim Valido As Boolean
        Dim Celula As Integer
        Dim Ruta As Integer
        Dim Nombre As String
        Dim Direccion As String
        Dim ClienteEdificio As Boolean
        Dim ClienteActivo As Boolean
        Dim RamoCliente As Short
    End Structure

    Private Function consultaDatosCliente(ByVal Cliente As Integer) As DatosCliente
        Dim _datosCliente As DatosCliente = Nothing

        Dim cmdCliente As New SqlClient.SqlCommand()
        'Se cambia por sp
        'cmdCliente.CommandText = "Select dbo.ClienteValido(@Cliente) as Valido, dbo.CelulaCliente(@Cliente) as Celula, dbo.RutaCliente(@Cliente) as Ruta, dbo.NombreCliente(@Cliente) as Nombre, dbo.DireccionCliente(@Cliente) as Direccion, dbo.ClienteHijoEdificio(@Cliente) as ClienteEdificio, dbo.ClienteActivo(@Cliente) as ClienteActivo "
        cmdCliente.CommandText = "EXECUTE spLIQConsultaDatosCliente @Cliente"
        cmdCliente.CommandType = CommandType.Text
        cmdCliente.Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
        cmdCliente.Connection = SqlConnection

        Dim rdrLiquidacion As SqlClient.SqlDataReader = Nothing
        Try
            rdrLiquidacion = cmdCliente.ExecuteReader
            While rdrLiquidacion.Read()

                _datosCliente.Valido = CType(rdrLiquidacion("Valido"), Boolean)
                _datosCliente.Celula = CType(rdrLiquidacion("Celula"), Integer)
                _datosCliente.Ruta = CType(rdrLiquidacion("Ruta"), Integer)
                _datosCliente.Nombre = CType(rdrLiquidacion("Nombre"), String)
                _datosCliente.Direccion = CType(rdrLiquidacion("Direccion"), String)
                _datosCliente.ClienteEdificio = CType(rdrLiquidacion("ClienteEdificio"), Boolean)

                'Contratos inactivos
                _datosCliente.ClienteActivo = CType(rdrLiquidacion("ClienteActivo"), Boolean)

                'Ramo del cliente para clientes de edificios administrados, validación de liquidación a crédito
                _datosCliente.RamoCliente = CType(rdrLiquidacion("RamoCliente"), Short)
            End While
        Catch ex As Exception
            Throw ex
        Finally
            If Not rdrLiquidacion.IsClosed Then
                rdrLiquidacion.Close()
            End If
            cmdCliente.Dispose()
        End Try
        Return _datosCliente
    End Function

    Public Sub validacionEdificiosRemisiones()
        If Not GLOBAL_AdmEdificiosLiquidacionCredito Then
            Exit Sub
        End If
        Dim i As Integer
        For i = 0 To dsLiquidacion2005.Tables("Remision").Rows.Count - 1
            'Validación de crédito para edificios administrados 10-10-2006
            Dim cliente As Integer = CType(dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("Cliente"), Integer)
            Dim _datosCliente As DatosCliente = consultaDatosCliente(cliente)
            If _datosCliente.RamoCliente = GLOBAL_ClaveRamoClienteAdmEdificios Then
                dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("TipoPago") = "CREDITO"


                Dim rdrLiquidacion As SqlDataReader = Nothing
                Dim TieneTarjetaCredito As Int16 = TarjetaAndOrCredito(rdrLiquidacion, cliente)
                dsLiquidacion2005.Tables.Item("Remision").Rows.Item(i).Item("FormaPago") = TieneTarjetaCredito
                Application.DoEvents()
            End If
        Next
    End Sub

    'cmdCliente.CommandText = "EXECUTE spLIQConsultaDatosCliente @Cliente"

    'Validación de crédito para edificios administrados
    'If ramoCliente = GLOBAL_ClaveRamoClienteAdmEdificios Then
    '    GridView2.GetDataRow(e.RowHandle).Item("TipoPago") = "CREDITO"
    'End If


#End Region

End Class
