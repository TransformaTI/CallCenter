Public Class ControlNotas
    Inherits System.Windows.Forms.Form

    Private _PrimeraVez As Boolean

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tlbLiquidacion As System.Windows.Forms.ToolBar
    Friend WithEvents btnActualizar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton2 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton4 As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnExtravio As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnEntrega As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnImprimir As System.Windows.Forms.ToolBarButton
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents tbDocumentos As System.Windows.Forms.TabControl
    Friend WithEvents tbpNotas As System.Windows.Forms.TabPage
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents dsDocumentos As System.Data.DataSet
    Friend WithEvents dtNota As System.Data.DataTable
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBuscar As System.Windows.Forms.ToolBarButton
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PersistentRepository1 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents dgNotas As DevExpress.XtraGrid.GridControl
    Friend WithEvents ViewNotas As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcCelula As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRuta As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcSerie As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFolio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcExtraviada As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcTipoNota As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFNota As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcStatus As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcExiste As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcEntregado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dtPedido As System.Data.DataTable
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents PersistentRepository2 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents dgPedidos As DevExpress.XtraGrid.GridControl
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents dgFacturas As DevExpress.XtraGrid.GridControl
    Friend WithEvents dtFactura As System.Data.DataTable
    Friend WithEvents dgValesCredito As DevExpress.XtraGrid.GridControl
    Friend WithEvents dtValeCredito As System.Data.DataTable
    Friend WithEvents dtLiquidacion As System.Data.DataTable
    Friend WithEvents dgLiquidacion As DevExpress.XtraGrid.GridControl
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents CardView1 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents RepositoryItemTextEdit3 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents txtExtraviada As DevExpress.XtraEditors.Repository.RepositoryItemPickImage
    Friend WithEvents txtEntregado As DevExpress.XtraEditors.Repository.RepositoryItemPickImage
    Friend WithEvents txtExiste As DevExpress.XtraEditors.Repository.RepositoryItemPickImage
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CardView2 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents RepositoryItemTextEdit5 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CardView3 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents RepositoryItemTextEdit4 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn21 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn22 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents CardView4 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents GridColumn24 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn25 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn26 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn27 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn28 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn29 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn30 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn31 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn32 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn33 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn34 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn35 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents cbTipoNota As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFecha As DevExpress.XtraEditors.DateEdit
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter2 As System.Windows.Forms.Splitter
    Friend WithEvents Splitter3 As System.Windows.Forms.Splitter
    Friend WithEvents p1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents p2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents p3 As System.Windows.Forms.PictureBox
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents p4 As System.Windows.Forms.PictureBox
    Friend WithEvents btnObservaciones As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblObservacionesNota As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(ControlNotas))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tlbLiquidacion = New System.Windows.Forms.ToolBar()
        Me.btnExtravio = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton2 = New System.Windows.Forms.ToolBarButton()
        Me.btnEntrega = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
        Me.btnBuscar = New System.Windows.Forms.ToolBarButton()
        Me.btnImprimir = New System.Windows.Forms.ToolBarButton()
        Me.btnActualizar = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton4 = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.tbDocumentos = New System.Windows.Forms.TabControl()
        Me.tbpNotas = New System.Windows.Forms.TabPage()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.dgFacturas = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository2 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.RepositoryItemTextEdit3 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemTextEdit4 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.RepositoryItemTextEdit5 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.CardView2 = New DevExpress.XtraGrid.Views.Card.CardView()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Splitter3 = New System.Windows.Forms.Splitter()
        Me.dgValesCredito = New DevExpress.XtraGrid.GridControl()
        Me.CardView3 = New DevExpress.XtraGrid.Views.Card.CardView()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn21 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn22 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.dgLiquidacion = New DevExpress.XtraGrid.GridControl()
        Me.CardView4 = New DevExpress.XtraGrid.Views.Card.CardView()
        Me.GridColumn24 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn25 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn26 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn27 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn28 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn29 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn30 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn31 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn32 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn33 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn34 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn35 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Splitter2 = New System.Windows.Forms.Splitter()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.lblObservacionesNota = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.dgPedidos = New DevExpress.XtraGrid.GridControl()
        Me.CardView1 = New DevExpress.XtraGrid.Views.Card.CardView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnObservaciones = New System.Windows.Forms.Button()
        Me.dgNotas = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository1 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.txtExtraviada = New DevExpress.XtraEditors.Repository.RepositoryItemPickImage()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.txtEntregado = New DevExpress.XtraEditors.Repository.RepositoryItemPickImage()
        Me.txtExiste = New DevExpress.XtraEditors.Repository.RepositoryItemPickImage()
        Me.ViewNotas = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcSerie = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFolio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCelula = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRuta = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcTipoNota = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcStatus = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFNota = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcExtraviada = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcEntregado = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcExiste = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.txtFecha = New DevExpress.XtraEditors.DateEdit()
        Me.cbTipoNota = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.dsDocumentos = New System.Data.DataSet()
        Me.dtNota = New System.Data.DataTable()
        Me.dtPedido = New System.Data.DataTable()
        Me.dtFactura = New System.Data.DataTable()
        Me.dtValeCredito = New System.Data.DataTable()
        Me.dtLiquidacion = New System.Data.DataTable()
        Me.p1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.p2 = New System.Windows.Forms.PictureBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.p3 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.p4 = New System.Windows.Forms.PictureBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.tbDocumentos.SuspendLayout()
        Me.tbpNotas.SuspendLayout()
        Me.Panel7.SuspendLayout()
        CType(Me.dgFacturas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        CType(Me.dgValesCredito, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        CType(Me.dgLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.dgPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.dgNotas, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExtraviada, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtEntregado, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtExiste, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ViewNotas, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        CType(Me.txtFecha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dsDocumentos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtNota, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtFactura, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtValeCredito, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3, Me.MenuItem4, Me.MenuItem9, Me.MenuItem5, Me.MenuItem6, Me.MenuItem7, Me.MenuItem8, Me.MenuItem2})
        Me.MenuItem1.Text = "Control de documentos"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.Text = "Extravio"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        Me.MenuItem4.Text = "Entrega"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 2
        Me.MenuItem9.Shortcut = System.Windows.Forms.Shortcut.CtrlB
        Me.MenuItem9.Text = "Buscar"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 3
        Me.MenuItem5.Text = "-"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 4
        Me.MenuItem6.Text = "Imprimir"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 5
        Me.MenuItem7.Text = "Actualizar"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 6
        Me.MenuItem8.Text = "-"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 7
        Me.MenuItem2.Text = "Salir"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'tlbLiquidacion
        '
        Me.tlbLiquidacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.tlbLiquidacion.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnExtravio, Me.ToolBarButton2, Me.btnEntrega, Me.ToolBarButton3, Me.btnBuscar, Me.btnImprimir, Me.btnActualizar, Me.ToolBarButton4, Me.btnCerrar})
        Me.tlbLiquidacion.ButtonSize = New System.Drawing.Size(70, 40)
        Me.tlbLiquidacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlbLiquidacion.DropDownArrows = True
        Me.tlbLiquidacion.ImageList = Me.ImageList1
        Me.tlbLiquidacion.Name = "tlbLiquidacion"
        Me.tlbLiquidacion.ShowToolTips = True
        Me.tlbLiquidacion.Size = New System.Drawing.Size(1264, 44)
        Me.tlbLiquidacion.TabIndex = 48
        '
        'btnExtravio
        '
        Me.btnExtravio.ImageIndex = 10
        Me.btnExtravio.Text = "Extravio"
        Me.btnExtravio.ToolTipText = "Cambio de cliente"
        '
        'ToolBarButton2
        '
        Me.ToolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnEntrega
        '
        Me.btnEntrega.ImageIndex = 11
        Me.btnEntrega.Text = "Entrega"
        Me.btnEntrega.ToolTipText = "Cambio de contado a credito"
        '
        'ToolBarButton3
        '
        Me.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnBuscar
        '
        Me.btnBuscar.ImageIndex = 9
        Me.btnBuscar.Text = "Buscar"
        '
        'btnImprimir
        '
        Me.btnImprimir.ImageIndex = 4
        Me.btnImprimir.Text = "Reportes"
        Me.btnImprimir.ToolTipText = "Cambio de credito a contado"
        '
        'btnActualizar
        '
        Me.btnActualizar.ImageIndex = 0
        Me.btnActualizar.Text = "Actualizar"
        Me.btnActualizar.ToolTipText = "Actualizar datos de la liquidación"
        '
        'ToolBarButton4
        '
        Me.ToolBarButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 1
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.ToolTipText = "Cerrar ventana"
        '
        'tbDocumentos
        '
        Me.tbDocumentos.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.tbDocumentos.Controls.AddRange(New System.Windows.Forms.Control() {Me.tbpNotas})
        Me.tbDocumentos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbDocumentos.ItemSize = New System.Drawing.Size(120, 30)
        Me.tbDocumentos.Location = New System.Drawing.Point(0, 44)
        Me.tbDocumentos.Multiline = True
        Me.tbDocumentos.Name = "tbDocumentos"
        Me.tbDocumentos.SelectedIndex = 0
        Me.tbDocumentos.Size = New System.Drawing.Size(1264, 721)
        Me.tbDocumentos.TabIndex = 49
        '
        'tbpNotas
        '
        Me.tbpNotas.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel7, Me.Panel6, Me.Panel5, Me.Panel4, Me.Panel3, Me.Panel2})
        Me.tbpNotas.Location = New System.Drawing.Point(4, 4)
        Me.tbpNotas.Name = "tbpNotas"
        Me.tbpNotas.Size = New System.Drawing.Size(1256, 683)
        Me.tbpNotas.TabIndex = 0
        Me.tbpNotas.Text = "Notas de blancas y de remisión"
        '
        'Panel7
        '
        Me.Panel7.Controls.AddRange(New System.Windows.Forms.Control() {Me.dgFacturas})
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel7.Location = New System.Drawing.Point(736, 672)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(520, 11)
        Me.Panel7.TabIndex = 5
        '
        'dgFacturas
        '
        Me.dgFacturas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgFacturas.EditorsRepository = Me.PersistentRepository2
        Me.dgFacturas.MainView = Me.CardView2
        Me.dgFacturas.Name = "dgFacturas"
        Me.dgFacturas.Size = New System.Drawing.Size(520, 11)
        Me.dgFacturas.Styles.AddReplace("EmptySpace", New DevExpress.Utils.ViewStyle("EmptySpace", "CardView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGray, System.Drawing.SystemColors.WindowText))
        Me.dgFacturas.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(255, Byte), CType(217, Byte), CType(179, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgFacturas.Styles.AddReplace("CardCaption", New DevExpress.Utils.ViewStyle("CardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGoldenrod, System.Drawing.Color.White))
        Me.dgFacturas.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(255, Byte), CType(217, Byte), CType(179, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgFacturas.Styles.AddReplace("FieldValue", New DevExpress.Utils.ViewStyle("FieldValue", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.FromArgb(CType(255, Byte), CType(217, Byte), CType(179, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgFacturas.Styles.AddReplace("FocusedCardCaption", New DevExpress.Utils.ViewStyle("FocusedCardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGoldenrod, System.Drawing.Color.White))
        Me.dgFacturas.Styles.AddReplace("FieldCaption", New DevExpress.Utils.ViewStyle("FieldCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Top, Nothing, System.Drawing.Color.FromArgb(CType(255, Byte), CType(217, Byte), CType(179, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgFacturas.TabIndex = 2
        '
        'PersistentRepository2
        '
        Me.PersistentRepository2.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit3, Me.RepositoryItemTextEdit4, Me.RepositoryItemTextEdit2, Me.RepositoryItemTextEdit5})
        '
        'RepositoryItemTextEdit3
        '
        Me.RepositoryItemTextEdit3.Name = "RepositoryItemTextEdit3"
        Me.RepositoryItemTextEdit3.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit3.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'RepositoryItemTextEdit4
        '
        Me.RepositoryItemTextEdit4.Name = "RepositoryItemTextEdit4"
        Me.RepositoryItemTextEdit4.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit4.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit4.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        Me.RepositoryItemTextEdit2.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit2.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'RepositoryItemTextEdit5
        '
        Me.RepositoryItemTextEdit5.Name = "RepositoryItemTextEdit5"
        Me.RepositoryItemTextEdit5.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit5.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit5.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'CardView2
        '
        Me.CardView2.BehaviorOptions = ((DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.AutoHorzWidth Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.Sizeable) _
                    Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.FieldAutoHeight)
        Me.CardView2.CardCaptionFormat = "FACTURAS"
        Me.CardView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn7, Me.GridColumn18, Me.GridColumn12, Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn19})
        Me.CardView2.DefaultEdit = Me.RepositoryItemTextEdit5
        Me.CardView2.FocusedCardTopFieldIndex = 0
        Me.CardView2.Name = "CardView2"
        Me.CardView2.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto
        Me.CardView2.ViewOptions = (DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowCardCaption Or DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowHorzScrollBar)
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Factura"
        Me.GridColumn7.FieldName = "Folio"
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn7.StyleName = "Style2"
        Me.GridColumn7.VisibleIndex = 0
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Serie"
        Me.GridColumn18.FieldName = "Serie"
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn18.StyleName = "Style1"
        Me.GridColumn18.VisibleIndex = 1
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "FFactura"
        Me.GridColumn12.FieldName = "FFactura"
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn12.VisibleIndex = 2
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "FCancelación"
        Me.GridColumn14.FieldName = "FCancelacion"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn14.VisibleIndex = 3
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Importe"
        Me.GridColumn15.FieldName = "Total"
        Me.GridColumn15.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn15.VisibleIndex = 4
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Status"
        Me.GridColumn16.FieldName = "Status"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn16.StyleName = "Style1"
        Me.GridColumn16.VisibleIndex = 5
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Tipo"
        Me.GridColumn17.FieldName = "TipoDes"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn17.VisibleIndex = 6
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Reemplazo"
        Me.GridColumn19.FieldName = "FacturaRemplazo"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn19.VisibleIndex = 7
        '
        'Panel6
        '
        Me.Panel6.Controls.AddRange(New System.Windows.Forms.Control() {Me.Splitter3, Me.dgValesCredito})
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel6.Location = New System.Drawing.Point(736, 512)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(520, 160)
        Me.Panel6.TabIndex = 4
        '
        'Splitter3
        '
        Me.Splitter3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter3.Location = New System.Drawing.Point(0, 157)
        Me.Splitter3.Name = "Splitter3"
        Me.Splitter3.Size = New System.Drawing.Size(520, 3)
        Me.Splitter3.TabIndex = 3
        Me.Splitter3.TabStop = False
        '
        'dgValesCredito
        '
        Me.dgValesCredito.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgValesCredito.EditorsRepository = Me.PersistentRepository2
        Me.dgValesCredito.MainView = Me.CardView3
        Me.dgValesCredito.Name = "dgValesCredito"
        Me.dgValesCredito.Size = New System.Drawing.Size(520, 160)
        Me.dgValesCredito.Styles.AddReplace("EmptySpace", New DevExpress.Utils.ViewStyle("EmptySpace", "CardView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGray, System.Drawing.SystemColors.WindowText))
        Me.dgValesCredito.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.LightGreen, System.Drawing.SystemColors.WindowText))
        Me.dgValesCredito.Styles.AddReplace("CardCaption", New DevExpress.Utils.ViewStyle("CardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGreen, System.Drawing.Color.White))
        Me.dgValesCredito.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.LightGreen, System.Drawing.SystemColors.WindowText))
        Me.dgValesCredito.Styles.AddReplace("FieldValue", New DevExpress.Utils.ViewStyle("FieldValue", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightGreen, System.Drawing.SystemColors.WindowText))
        Me.dgValesCredito.Styles.AddReplace("FocusedCardCaption", New DevExpress.Utils.ViewStyle("FocusedCardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGreen, System.Drawing.Color.White))
        Me.dgValesCredito.Styles.AddReplace("FieldCaption", New DevExpress.Utils.ViewStyle("FieldCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Top, Nothing, System.Drawing.Color.LightGreen, System.Drawing.SystemColors.WindowText))
        Me.dgValesCredito.TabIndex = 2
        '
        'CardView3
        '
        Me.CardView3.BehaviorOptions = ((DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.AutoHorzWidth Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.Sizeable) _
                    Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.FieldAutoHeight)
        Me.CardView3.CardCaptionFormat = "VALES DE CREDITO"
        Me.CardView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn20, Me.GridColumn21, Me.GridColumn22, Me.GridColumn23})
        Me.CardView3.DefaultEdit = Me.RepositoryItemTextEdit4
        Me.CardView3.FocusedCardTopFieldIndex = 0
        Me.CardView3.Name = "CardView3"
        Me.CardView3.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto
        Me.CardView3.ViewOptions = (DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowCardCaption Or DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowHorzScrollBar)
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Vale"
        Me.GridColumn20.FieldName = "ValeCredito"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn20.StyleName = "Style2"
        Me.GridColumn20.VisibleIndex = 0
        '
        'GridColumn21
        '
        Me.GridColumn21.Caption = "FVale"
        Me.GridColumn21.FieldName = "FVale"
        Me.GridColumn21.Name = "GridColumn21"
        Me.GridColumn21.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn21.VisibleIndex = 1
        '
        'GridColumn22
        '
        Me.GridColumn22.Caption = "Status"
        Me.GridColumn22.FieldName = "Status"
        Me.GridColumn22.Name = "GridColumn22"
        Me.GridColumn22.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn22.StyleName = "Style1"
        Me.GridColumn22.VisibleIndex = 2
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "FImpresión"
        Me.GridColumn23.FieldName = "FImpresion"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn23.VisibleIndex = 3
        '
        'Panel5
        '
        Me.Panel5.Controls.AddRange(New System.Windows.Forms.Control() {Me.dgLiquidacion, Me.Splitter2})
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(736, 248)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(520, 264)
        Me.Panel5.TabIndex = 3
        '
        'dgLiquidacion
        '
        Me.dgLiquidacion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgLiquidacion.EditorsRepository = Me.PersistentRepository2
        Me.dgLiquidacion.MainView = Me.CardView4
        Me.dgLiquidacion.Name = "dgLiquidacion"
        Me.dgLiquidacion.Size = New System.Drawing.Size(520, 261)
        Me.dgLiquidacion.Styles.AddReplace("EmptySpace", New DevExpress.Utils.ViewStyle("EmptySpace", "CardView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGray, System.Drawing.SystemColors.WindowText))
        Me.dgLiquidacion.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(220, Byte), CType(185, Byte), CType(255, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgLiquidacion.Styles.AddReplace("CardCaption", New DevExpress.Utils.ViewStyle("CardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkSlateBlue, System.Drawing.Color.White))
        Me.dgLiquidacion.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(220, Byte), CType(185, Byte), CType(255, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgLiquidacion.Styles.AddReplace("FieldValue", New DevExpress.Utils.ViewStyle("FieldValue", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.FromArgb(CType(220, Byte), CType(185, Byte), CType(255, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgLiquidacion.Styles.AddReplace("FocusedCardCaption", New DevExpress.Utils.ViewStyle("FocusedCardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkSlateBlue, System.Drawing.Color.White))
        Me.dgLiquidacion.Styles.AddReplace("FieldCaption", New DevExpress.Utils.ViewStyle("FieldCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Top, Nothing, System.Drawing.Color.FromArgb(CType(220, Byte), CType(185, Byte), CType(255, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgLiquidacion.TabIndex = 4
        '
        'CardView4
        '
        Me.CardView4.BehaviorOptions = ((DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.AutoHorzWidth Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.Sizeable) _
                    Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.FieldAutoHeight)
        Me.CardView4.CardCaptionFormat = "LIQUIDACION Y BASCULA"
        Me.CardView4.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn24, Me.GridColumn25, Me.GridColumn26, Me.GridColumn27, Me.GridColumn28, Me.GridColumn29, Me.GridColumn30, Me.GridColumn31, Me.GridColumn32, Me.GridColumn33, Me.GridColumn34, Me.GridColumn35})
        Me.CardView4.DefaultEdit = Me.RepositoryItemTextEdit2
        Me.CardView4.FocusedCardTopFieldIndex = 0
        Me.CardView4.Name = "CardView4"
        Me.CardView4.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto
        Me.CardView4.ViewOptions = (DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowCardCaption Or DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowHorzScrollBar)
        '
        'GridColumn24
        '
        Me.GridColumn24.Caption = "Año"
        Me.GridColumn24.FieldName = "AñoAtt"
        Me.GridColumn24.Name = "GridColumn24"
        Me.GridColumn24.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn24.VisibleIndex = 0
        '
        'GridColumn25
        '
        Me.GridColumn25.Caption = "Folio"
        Me.GridColumn25.FieldName = "Folio"
        Me.GridColumn25.Name = "GridColumn25"
        Me.GridColumn25.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn25.StyleName = "Style2"
        Me.GridColumn25.VisibleIndex = 1
        '
        'GridColumn26
        '
        Me.GridColumn26.Caption = "Status"
        Me.GridColumn26.FieldName = "StatusLogistica"
        Me.GridColumn26.Name = "GridColumn26"
        Me.GridColumn26.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn26.StyleName = "Style1"
        Me.GridColumn26.VisibleIndex = 2
        '
        'GridColumn27
        '
        Me.GridColumn27.Caption = "Celula"
        Me.GridColumn27.FieldName = "Celula"
        Me.GridColumn27.Name = "GridColumn27"
        Me.GridColumn27.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn27.VisibleIndex = 3
        '
        'GridColumn28
        '
        Me.GridColumn28.Caption = "Ruta"
        Me.GridColumn28.FieldName = "Ruta"
        Me.GridColumn28.Name = "GridColumn28"
        Me.GridColumn28.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn28.VisibleIndex = 4
        '
        'GridColumn29
        '
        Me.GridColumn29.Caption = "Autotanque"
        Me.GridColumn29.FieldName = "Autotanque"
        Me.GridColumn29.Name = "GridColumn29"
        Me.GridColumn29.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn29.VisibleIndex = 5
        '
        'GridColumn30
        '
        Me.GridColumn30.Caption = "Venta"
        Me.GridColumn30.FieldName = "VentaImporte"
        Me.GridColumn30.Name = "GridColumn30"
        Me.GridColumn30.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn30.VisibleIndex = 6
        '
        'GridColumn31
        '
        Me.GridColumn31.Caption = "Litraje"
        Me.GridColumn31.FieldName = "LitrosLiquidados"
        Me.GridColumn31.Name = "GridColumn31"
        Me.GridColumn31.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn31.VisibleIndex = 7
        '
        'GridColumn32
        '
        Me.GridColumn32.Caption = "Fecha"
        Me.GridColumn32.FieldName = "FPreliquidacion"
        Me.GridColumn32.Name = "GridColumn32"
        Me.GridColumn32.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn32.VisibleIndex = 8
        '
        'GridColumn33
        '
        Me.GridColumn33.Caption = "Tipo"
        Me.GridColumn33.FieldName = "TipoLiquidacion"
        Me.GridColumn33.Name = "GridColumn33"
        Me.GridColumn33.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn33.VisibleIndex = 9
        '
        'GridColumn34
        '
        Me.GridColumn34.Caption = "Usuario"
        Me.GridColumn34.FieldName = "UsuarioLiquidacion"
        Me.GridColumn34.Name = "GridColumn34"
        Me.GridColumn34.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn34.StyleName = "Style1"
        Me.GridColumn34.VisibleIndex = 10
        '
        'GridColumn35
        '
        Me.GridColumn35.Caption = "# Notas"
        Me.GridColumn35.FieldName = "NotasLiquidadas"
        Me.GridColumn35.Name = "GridColumn35"
        Me.GridColumn35.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn35.VisibleIndex = 11
        '
        'Splitter2
        '
        Me.Splitter2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter2.Location = New System.Drawing.Point(0, 261)
        Me.Splitter2.Name = "Splitter2"
        Me.Splitter2.Size = New System.Drawing.Size(520, 3)
        Me.Splitter2.TabIndex = 2
        Me.Splitter2.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblObservacionesNota, Me.Splitter1, Me.dgPedidos})
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(736, 32)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(520, 216)
        Me.Panel4.TabIndex = 2
        '
        'lblObservacionesNota
        '
        Me.lblObservacionesNota.Anchor = (((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lblObservacionesNota.BackColor = System.Drawing.Color.Transparent
        Me.lblObservacionesNota.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblObservacionesNota.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObservacionesNota.Location = New System.Drawing.Point(12, 140)
        Me.lblObservacionesNota.Name = "lblObservacionesNota"
        Me.lblObservacionesNota.Size = New System.Drawing.Size(496, 52)
        Me.lblObservacionesNota.TabIndex = 2
        Me.lblObservacionesNota.Visible = False
        '
        'Splitter1
        '
        Me.Splitter1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Splitter1.Location = New System.Drawing.Point(0, 213)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(520, 3)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'dgPedidos
        '
        Me.dgPedidos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPedidos.EditorsRepository = Me.PersistentRepository2
        Me.dgPedidos.MainView = Me.CardView1
        Me.dgPedidos.Name = "dgPedidos"
        Me.dgPedidos.Size = New System.Drawing.Size(520, 216)
        Me.dgPedidos.Styles.AddReplace("EmptySpace", New DevExpress.Utils.ViewStyle("EmptySpace", "CardView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGray, System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(253, Byte), CType(206, Byte), CType(198, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("CardCaption", New DevExpress.Utils.ViewStyle("CardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkRed, System.Drawing.Color.White))
        Me.dgPedidos.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(253, Byte), CType(206, Byte), CType(198, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("FieldValue", New DevExpress.Utils.ViewStyle("FieldValue", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.FromArgb(CType(253, Byte), CType(206, Byte), CType(198, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("FocusedCardCaption", New DevExpress.Utils.ViewStyle("FocusedCardCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkRed, System.Drawing.Color.White))
        Me.dgPedidos.Styles.AddReplace("FieldCaption", New DevExpress.Utils.ViewStyle("FieldCaption", "CardView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Top, Nothing, System.Drawing.Color.FromArgb(CType(253, Byte), CType(206, Byte), CType(198, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.TabIndex = 0
        '
        'CardView1
        '
        Me.CardView1.BehaviorOptions = ((DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.AutoHorzWidth Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.Sizeable) _
                    Or DevExpress.XtraGrid.Views.Card.CardBehaviorOptionsFlags.FieldAutoHeight)
        Me.CardView1.CardCaptionFormat = "PEDIDOS"
        Me.CardView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn8, Me.GridColumn9, Me.GridColumn10, Me.GridColumn11, Me.GridColumn13})
        Me.CardView1.DefaultEdit = Me.RepositoryItemTextEdit3
        Me.CardView1.FocusedCardTopFieldIndex = 0
        Me.CardView1.Name = "CardView1"
        Me.CardView1.VertScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Auto
        Me.CardView1.ViewOptions = (DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowCardCaption Or DevExpress.XtraGrid.Views.Card.CardViewOptionsFlags.ShowHorzScrollBar)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Documento"
        Me.GridColumn1.FieldName = "PedidoReferencia"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn1.StyleName = "Style2"
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 80
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Tipo"
        Me.GridColumn2.FieldName = "TipoPedido"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn2.VisibleIndex = 1
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "FPedido"
        Me.GridColumn3.FieldName = "FPedido"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn3.VisibleIndex = 2
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "FSuministro"
        Me.GridColumn4.FieldName = "FSuministro"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn4.VisibleIndex = 3
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "FCancelación"
        Me.GridColumn5.FieldName = "FCancelacion"
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn5.VisibleIndex = 4
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Litros"
        Me.GridColumn6.FieldName = "Litros"
        Me.GridColumn6.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn6.VisibleIndex = 5
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Status"
        Me.GridColumn8.FieldName = "Status"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn8.StyleName = "Style1"
        Me.GridColumn8.VisibleIndex = 6
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "Contrato"
        Me.GridColumn9.FieldName = "Cliente"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn9.StyleName = "Style1"
        Me.GridColumn9.VisibleIndex = 7
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Cliente"
        Me.GridColumn10.FieldName = "Nombre"
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn10.StyleName = "Style1"
        Me.GridColumn10.VisibleIndex = 8
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Ruta"
        Me.GridColumn11.FieldName = "Ruta"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn11.VisibleIndex = 9
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Cobro"
        Me.GridColumn13.FieldName = "TipoCobroDes"
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly
        Me.GridColumn13.VisibleIndex = 10
        '
        'Panel3
        '
        Me.Panel3.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnObservaciones, Me.dgNotas})
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 32)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(736, 651)
        Me.Panel3.TabIndex = 1
        '
        'btnObservaciones
        '
        Me.btnObservaciones.Image = CType(resources.GetObject("btnObservaciones.Image"), System.Drawing.Bitmap)
        Me.btnObservaciones.Location = New System.Drawing.Point(688, 8)
        Me.btnObservaciones.Name = "btnObservaciones"
        Me.btnObservaciones.Size = New System.Drawing.Size(28, 23)
        Me.btnObservaciones.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnObservaciones, "Captura de observaciones para la nota")
        '
        'dgNotas
        '
        Me.dgNotas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgNotas.EditorsRepository = Me.PersistentRepository1
        Me.dgNotas.MainView = Me.ViewNotas
        Me.dgNotas.Name = "dgNotas"
        Me.dgNotas.Size = New System.Drawing.Size(736, 651)
        Me.dgNotas.Styles.AddReplace("Style5", New DevExpress.Utils.ViewStyle("Style5", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.LightCoral, System.Drawing.SystemColors.WindowText))
        Me.dgNotas.Styles.AddReplace("FooterPanel", New DevExpress.Utils.ViewStyle("FooterPanel", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.PeachPuff, System.Drawing.SystemColors.ControlText))
        Me.dgNotas.Styles.AddReplace("FilterButton", New DevExpress.Utils.ViewStyle("FilterButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.RoyalBlue, System.Drawing.Color.White))
        Me.dgNotas.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.CornflowerBlue, System.Drawing.SystemColors.WindowText))
        Me.dgNotas.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.SteelBlue, System.Drawing.Color.White))
        Me.dgNotas.Styles.AddReplace("Empty", New DevExpress.Utils.ViewStyle("Empty", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGray, System.Drawing.SystemColors.Window))
        Me.dgNotas.Styles.AddReplace("Style3", New DevExpress.Utils.ViewStyle("Style3", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.Black, System.Drawing.Color.White))
        Me.dgNotas.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.Khaki, System.Drawing.SystemColors.WindowText))
        Me.dgNotas.Styles.AddReplace("GroupFooter", New DevExpress.Utils.ViewStyle("GroupFooter", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.SteelBlue, System.Drawing.Color.White))
        Me.dgNotas.Styles.AddReplace("FilterPanel", New DevExpress.Utils.ViewStyle("FilterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkSeaGreen, System.Drawing.SystemColors.ControlLightLight))
        Me.dgNotas.Styles.AddReplace("Style4", New DevExpress.Utils.ViewStyle("Style4", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.LightGreen, System.Drawing.SystemColors.WindowText))
        Me.dgNotas.Styles.AddReplace("Style6", New DevExpress.Utils.ViewStyle("Style6", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.White, System.Drawing.SystemColors.WindowText))
        Me.dgNotas.TabIndex = 0
        Me.dgNotas.Text = "GridControl1"
        '
        'PersistentRepository1
        '
        Me.PersistentRepository1.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1, Me.txtExtraviada, Me.txtEntregado, Me.txtExiste})
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        Me.RepositoryItemTextEdit1.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit1.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'txtExtraviada
        '
        Me.txtExtraviada.Name = "txtExtraviada"
        Me.txtExtraviada.Properties.AllowFocused = False
        Me.txtExtraviada.Properties.AutoHeight = False
        Me.txtExtraviada.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtExtraviada.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtExtraviada.Properties.ButtonsBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtExtraviada.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("", False, 0))
        Me.txtExtraviada.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("", True, 1))
        Me.txtExtraviada.Properties.LargeImages = Me.ImageList2
        Me.txtExtraviada.Properties.ReadOnly = True
        Me.txtExtraviada.Properties.ShowDropDown = DevExpress.XtraEditors.Controls.ShowDropDown.Never
        Me.txtExtraviada.Properties.SmallImages = Me.ImageList2
        Me.txtExtraviada.Properties.UseCtrlScroll = True
        '
        'ImageList2
        '
        Me.ImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList2.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.White
        '
        'txtEntregado
        '
        Me.txtEntregado.Name = "txtEntregado"
        Me.txtEntregado.Properties.AllowFocused = False
        Me.txtEntregado.Properties.AutoHeight = False
        Me.txtEntregado.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtEntregado.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtEntregado.Properties.ButtonsBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtEntregado.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("", False, 0))
        Me.txtEntregado.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("", True, 2))
        Me.txtEntregado.Properties.LargeImages = Me.ImageList2
        Me.txtEntregado.Properties.ReadOnly = True
        Me.txtEntregado.Properties.SmallImages = Me.ImageList2
        Me.txtEntregado.Properties.UseCtrlScroll = True
        '
        'txtExiste
        '
        Me.txtExiste.Name = "txtExiste"
        Me.txtExiste.Properties.AllowFocused = False
        Me.txtExiste.Properties.AutoHeight = False
        Me.txtExiste.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.txtExiste.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtExiste.Properties.ButtonsBorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat
        Me.txtExiste.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("", "A", 3))
        Me.txtExiste.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("", "B", 0))
        Me.txtExiste.Properties.Items.Add(New DevExpress.XtraEditors.Controls.PickImageItem("", "C", 4))
        Me.txtExiste.Properties.LargeImages = Me.ImageList2
        Me.txtExiste.Properties.ReadOnly = True
        Me.txtExiste.Properties.SmallImages = Me.ImageList2
        Me.txtExiste.Properties.Style = New DevExpress.Utils.ViewStyle("ControlStyle", "BaseEdit", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                        Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                        Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                        Or DevExpress.Utils.StyleOptions.UseFont) _
                        Or DevExpress.Utils.StyleOptions.UseForeColor) _
                        Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                        Or DevExpress.Utils.StyleOptions.UseImage) _
                        Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                        Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.WindowText)
        Me.txtExiste.Properties.UseCtrlScroll = True
        '
        'ViewNotas
        '
        Me.ViewNotas.BehaviorOptions = ((((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowFilter Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowSort) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.RowAutoHeight)
        Me.ViewNotas.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcSerie, Me.gcFolio, Me.gcCelula, Me.gcRuta, Me.gcTipoNota, Me.gcStatus, Me.gcFNota, Me.gcExtraviada, Me.gcEntregado, Me.gcExiste})
        Me.ViewNotas.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.ViewNotas.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus
        Me.ViewNotas.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style1", "PENDIENTE", Nothing, Me.gcStatus, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style2", "NO IMPRESA", Nothing, Me.gcStatus, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style3", "ATASQUE", Nothing, Me.gcStatus, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style4", "IMPRESA", Nothing, Me.gcStatus, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style5", "CANCELADA", Nothing, Me.gcStatus, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style6", "LIQUIDADA", Nothing, Me.gcStatus, True)})
        Me.ViewNotas.GroupPanelText = "NOTAS BLANCAS O DE REMISION"
        Me.ViewNotas.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridSummaryItem(DevExpress.XtraGrid.SummaryItemType.Count, "FolioNota", Me.gcFolio, "")})
        Me.ViewNotas.Name = "ViewNotas"
        Me.ViewNotas.ViewOptions = ((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFilterPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFooter) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle)
        '
        'gcSerie
        '
        Me.gcSerie.Caption = "Serie"
        Me.gcSerie.FieldName = "Serie"
        Me.gcSerie.Name = "gcSerie"
        Me.gcSerie.Options = DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused
        Me.gcSerie.VisibleIndex = 0
        Me.gcSerie.Width = 42
        '
        'gcFolio
        '
        Me.gcFolio.Caption = "Folio"
        Me.gcFolio.FieldName = "FolioNota"
        Me.gcFolio.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcFolio.Name = "gcFolio"
        Me.gcFolio.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused)
        Me.gcFolio.SummaryItem.SummaryType = DevExpress.XtraGrid.SummaryItemType.Count
        Me.gcFolio.VisibleIndex = 1
        Me.gcFolio.Width = 106
        '
        'gcCelula
        '
        Me.gcCelula.Caption = "Celula"
        Me.gcCelula.FieldName = "Celula"
        Me.gcCelula.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcCelula.Name = "gcCelula"
        Me.gcCelula.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused)
        Me.gcCelula.VisibleIndex = 2
        Me.gcCelula.Width = 63
        '
        'gcRuta
        '
        Me.gcRuta.Caption = "Ruta"
        Me.gcRuta.FieldName = "Ruta"
        Me.gcRuta.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcRuta.Name = "gcRuta"
        Me.gcRuta.Options = (((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused)
        Me.gcRuta.VisibleIndex = 3
        Me.gcRuta.Width = 63
        '
        'gcTipoNota
        '
        Me.gcTipoNota.Caption = "Tipo"
        Me.gcTipoNota.FieldName = "TipoNotaDes"
        Me.gcTipoNota.Name = "gcTipoNota"
        Me.gcTipoNota.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused)
        Me.gcTipoNota.VisibleIndex = 4
        Me.gcTipoNota.Width = 85
        '
        'gcStatus
        '
        Me.gcStatus.Caption = "Status"
        Me.gcStatus.FieldName = "Status"
        Me.gcStatus.Name = "gcStatus"
        Me.gcStatus.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused)
        Me.gcStatus.VisibleIndex = 5
        Me.gcStatus.Width = 115
        '
        'gcFNota
        '
        Me.gcFNota.Caption = "FNota"
        Me.gcFNota.FieldName = "FNota"
        Me.gcFNota.Name = "gcFNota"
        Me.gcFNota.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused)
        Me.gcFNota.VisibleIndex = 6
        Me.gcFNota.Width = 84
        '
        'gcExtraviada
        '
        Me.gcExtraviada.Caption = "EX"
        Me.gcExtraviada.ColumnEdit = Me.txtExtraviada
        Me.gcExtraviada.FieldName = "Extraviada"
        Me.gcExtraviada.Name = "gcExtraviada"
        Me.gcExtraviada.Options = DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered
        Me.gcExtraviada.VisibleIndex = 7
        Me.gcExtraviada.Width = 43
        '
        'gcEntregado
        '
        Me.gcEntregado.Caption = "EN"
        Me.gcEntregado.ColumnEdit = Me.txtEntregado
        Me.gcEntregado.FieldName = "Entregado"
        Me.gcEntregado.Name = "gcEntregado"
        Me.gcEntregado.Options = DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered
        Me.gcEntregado.VisibleIndex = 8
        Me.gcEntregado.Width = 41
        '
        'gcExiste
        '
        Me.gcExiste.Caption = "Secuencia"
        Me.gcExiste.ColumnEdit = Me.txtExiste
        Me.gcExiste.FieldName = "Existe"
        Me.gcExiste.Name = "gcExiste"
        Me.gcExiste.Options = DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered
        Me.gcExiste.VisibleIndex = 9
        Me.gcExiste.Width = 80
        '
        'Panel2
        '
        Me.Panel2.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtFecha, Me.cbTipoNota, Me.Label3, Me.Label1})
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1256, 32)
        Me.Panel2.TabIndex = 0
        '
        'txtFecha
        '
        Me.txtFecha.DateTime = New Date(CType(0, Long))
        Me.txtFecha.Location = New System.Drawing.Point(64, 5)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtFecha.Properties.Format = Nothing
        Me.txtFecha.Properties.FormatString = "dd ""de"" MMMM ""de"" yyyy"
        Me.txtFecha.Properties.LayerColor = System.Drawing.Color.IndianRed
        Me.txtFecha.Properties.Style = New DevExpress.Utils.ViewStyle("ControlStyle", "BaseEdit", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                        Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                        Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                        Or DevExpress.Utils.StyleOptions.UseFont) _
                        Or DevExpress.Utils.StyleOptions.UseForeColor) _
                        Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                        Or DevExpress.Utils.StyleOptions.UseImage) _
                        Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                        Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.WindowText)
        Me.txtFecha.Size = New System.Drawing.Size(160, 22)
        Me.txtFecha.TabIndex = 6
        '
        'cbTipoNota
        '
        Me.cbTipoNota.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTipoNota.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTipoNota.ItemHeight = 13
        Me.cbTipoNota.Items.AddRange(New Object() {"REMISION", "NOTA BLANCA"})
        Me.cbTipoNota.Location = New System.Drawing.Point(320, 6)
        Me.cbTipoNota.Name = "cbTipoNota"
        Me.cbTipoNota.Size = New System.Drawing.Size(120, 21)
        Me.cbTipoNota.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(236, 5)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 23)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Tipo de Nota:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fecha :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'dsDocumentos
        '
        Me.dsDocumentos.DataSetName = "dsDocumentos"
        Me.dsDocumentos.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.dsDocumentos.Tables.AddRange(New System.Data.DataTable() {Me.dtNota, Me.dtPedido, Me.dtFactura, Me.dtValeCredito, Me.dtLiquidacion})
        '
        'dtNota
        '
        Me.dtNota.TableName = "Nota"
        '
        'dtPedido
        '
        Me.dtPedido.TableName = "Pedido"
        '
        'dtFactura
        '
        Me.dtFactura.TableName = "Factura"
        '
        'dtValeCredito
        '
        Me.dtValeCredito.TableName = "ValeCredito"
        '
        'dtLiquidacion
        '
        Me.dtLiquidacion.TableName = "Liquidacion"
        '
        'p1
        '
        Me.p1.Location = New System.Drawing.Point(456, 16)
        Me.p1.Name = "p1"
        Me.p1.Size = New System.Drawing.Size(24, 16)
        Me.p1.TabIndex = 50
        Me.p1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(485, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(116, 16)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Documento entregado"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(636, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 16)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Documento extraviado"
        '
        'p2
        '
        Me.p2.Location = New System.Drawing.Point(609, 16)
        Me.p2.Name = "p2"
        Me.p2.Size = New System.Drawing.Size(24, 16)
        Me.p2.TabIndex = 52
        Me.p2.TabStop = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(787, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 16)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Salto en el consecutivo"
        '
        'p3
        '
        Me.p3.Location = New System.Drawing.Point(757, 16)
        Me.p3.Name = "p3"
        Me.p3.Size = New System.Drawing.Size(24, 16)
        Me.p3.TabIndex = 54
        Me.p3.TabStop = False
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(960, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(136, 16)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Consecutivo en otra fecha"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(928, 16)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 16)
        Me.PictureBox1.TabIndex = 55
        Me.PictureBox1.TabStop = False
        '
        'p4
        '
        Me.p4.Location = New System.Drawing.Point(930, 16)
        Me.p4.Name = "p4"
        Me.p4.Size = New System.Drawing.Size(24, 16)
        Me.p4.TabIndex = 57
        Me.p4.TabStop = False
        '
        'ControlNotas
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(1264, 765)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.p4, Me.Label6, Me.Label5, Me.p3, Me.Label4, Me.p2, Me.Label2, Me.p1, Me.tbDocumentos, Me.tlbLiquidacion, Me.PictureBox1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Menu = Me.MainMenu1
        Me.Name = "ControlNotas"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control de documentos"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.tbDocumentos.ResumeLayout(False)
        Me.tbpNotas.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        CType(Me.dgFacturas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        CType(Me.dgValesCredito, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        CType(Me.dgLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        CType(Me.dgPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgNotas, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExtraviada, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtEntregado, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtExiste, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ViewNotas, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        CType(Me.txtFecha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dsDocumentos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtNota, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtFactura, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtValeCredito, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ControlNotas_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        _PrimeraVez = True
        tbDocumentos.SelectedIndex = 0
        p1.Image = ImageList2.Images.Item(2)
        p2.Image = ImageList2.Images.Item(1)
        p3.Image = ImageList2.Images.Item(3)
        p4.Image = ImageList2.Images.Item(4)
        LLenaComboNota()
        Notas()

    End Sub


    Private Sub LLenaComboNota()
        Dim cmdNotas As New SqlClient.SqlCommand("Select TipoNota, Descripcion from TipoNota Order by 1")
        cmdNotas.CommandType = CommandType.Text        
        cmdNotas.Connection = SqlConnection
        Dim rdrControl As SqlClient.SqlDataReader
        rdrControl = cmdNotas.ExecuteReader
        cbTipoNota.Items.Clear()
        While rdrControl.Read()
            cbTipoNota.Items.Add(rdrControl.Item("Descripcion"))
        End While
        rdrControl.Close()
        cmdNotas.Dispose()
    End Sub

    Private Sub Notas()

        'Dim rdrControl As SqlClient.SqlDataReader
        'Dim cmdAñoNota As New SqlClient.SqlCommand("SELECT Distinct AñoNota FROM Nota Order by AñoNota desc ")
        'cmdAñoNota.CommandType = CommandType.Text
        'cmdAñoNota.Connection = SqlConnection
        'rdrControl = cmdAñoNota.ExecuteReader
        'While rdrControl.Read()
        '    cbAñoNota.Items.Add(rdrControl.Item("AñoNota"))
        'End While
        'rdrControl.Close()

        If _PrimeraVez = True Then
            txtFecha.DateTime = Now
            cbTipoNota.SelectedIndex = 0
        End If


    End Sub

    Private Sub tbDocumentos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbDocumentos.SelectedIndexChanged
        Select Case tbDocumentos.SelectedIndex
            Case 0 : Notas()
        End Select
    End Sub

    Private Sub ControlNotas_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Sub Buscar()
        Dim Folio As String
        Dim Serie As String
        Dim frmBuscarFolio As New BuscarFolio()
        If frmBuscarFolio.ShowDialog = DialogResult.OK Then
            Folio = frmBuscarFolio._Folio
            Serie = frmBuscarFolio._Folio
            Serie = Serie.Remove(1, Folio.Length - 1)
            Folio = Folio.Remove(0, 1)
            'MsgBox(Serie)

            Dim Registro As Integer
            Dim Fecha As String
            Dim rdrControl As SqlClient.SqlDataReader
            Dim cmdInsert As New SqlClient.SqlCommand("Select FNota, Count(FolioNota) as Registro from Nota where RTrim(Serie)=@Serie and FolioNota=@FolioNota and FNota not between @FNota and @FNota+' 23:59:59' group by FNota ")
            cmdInsert.Connection = SqlConnection
            cmdInsert.CommandType = CommandType.Text
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Serie", SqlDbType.Char).Value = CType(Serie, String)
            cmdInsert.Parameters.Add("@FolioNota", SqlDbType.Int).Value = CType(Folio, Integer)
            cmdInsert.Parameters.Add("@FNota", SqlDbType.DateTime).Value = CType(IIf(txtFecha.Text = "", Now, txtFecha.DateTime), Date)
            rdrControl = cmdInsert.ExecuteReader
            If rdrControl.Read() Then
                Registro = CType(rdrControl.Item("Registro"), Integer)
                Fecha = CType(rdrControl.Item("FNota"), String)

                If Registro <> 0 Then
                    MsgBox("El Folio que esta buscando se encuentra en otra fecha distinta a la seleccionada. Intente con la fecha : " + Fecha, MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Exit Sub
                End If
            End If
            rdrControl.Close()



            If Folio <> "" Then
                'ViewNotas.LocateByValue(1, ViewNotas.Columns.Item("FolioNota"), CType(Folio, Integer))
                Dim i As Integer
                For i = 0 To ViewNotas.RowCount - 1
                    If CType(ViewNotas.GetDataRow(i).Item("FolioNota"), Integer) = CType(Folio, Integer) Then
                        ViewNotas.FocusedRowHandle = i
                        dgNotas.Focus()

                        Exit For
                    End If
                Next



            End If
        End If
        frmBuscarFolio.Dispose()
    End Sub

    Friend Sub refreshData()
        Dim sender As System.Object = Nothing
        Dim e As System.EventArgs = Nothing
        txtFecha_ValueChanged(sender, e)
    End Sub

    Private Sub tlbLiquidacion_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbLiquidacion.ButtonClick
        Select Case e.Button.Text
            Case "Cerrar" : Me.Close()
            Case "Extravio" : Extravio()
            Case "Entrega" : Entrega()
            Case "Actualizar" : Actualizar()
            Case "Reportes" : Imprimir()
            Case "Buscar" : Buscar()
        End Select
    End Sub

    Private Sub Imprimir()
        Dim rep As New SigaMetClasses.cConfig(18)
        Dim ruta As String = CStr(rep.Parametros("RutaReportes"))
        Dim CatRep As New ReporteDinamico.frmListaReporte(18, ruta, GLOBAL_Servidor, GLOBAL_Database, GLOBAL_Usuario, CnnSigamet, True)
        CatRep.MdiParent = Me.MdiParent
        CatRep.Show()
        'Dim frmCapturaDatosReporte As New CapturaDatosReporte()
        'frmCapturaDatosReporte.ShowDialog()
        'frmCapturaDatosReporte.Dispose()
    End Sub

    Private Sub Actualizar()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        dsDocumentos.Tables.Item("Nota").Clear()
        Dim cmdNotas As New SqlClient.SqlCommand("Set transaction isolation level read uncommitted " & _
                    " Select N.Celula, N.Ruta, AñoNota, Nota, N.Serie, IsNull(N.Operador,0) as Operador,IsNull(FolioNota,0) as FolioNota, IsNull(Extraviada,0) as Extraviada, IsNull(Entregado,0) as Entregado, " & _
                    "       N.Autotanque, IsNull(N.AñoPed,0) as AñoPed, IsNull(N.Pedido,0) as Pedido, T.Descripcion as TipoNotaDes, N.FNota, rtrim(N.Status) AS Status, N.FImpresion, N.Usuario, N.StatusConciliacion, " & _
                    "       N.FConciliacion, IsNull(O.Nombre,'') as OperadorNombre, dbo.ExisteNuevaNotaAnterior(AñoNota,Month(FNota),T.TipoNota,FolioNota,Serie, @FNota) as Existe, IsNull(P.AñoAtt,0) as AñoAtt, IsNull(P.Folio,0) as Folio, IsNull(N.Observaciones,'') as Observaciones " & _
                    " from Nota N Inner Join TipoNota T ON N.TipoNota=T.TipoNota" & _
                    "	    Left Join vwCYCOperador O ON N.Operador=O.Operador Left Join Pedido P ON N.Celula=P.Celula and N.AñoPed=P.AñoPed and N.Pedido=P.Pedido " & _
                    " where FNota between @FNota and @FNota+' 23:59:59' and T.TipoNota=@TipoNota " & _
                    " Order by FolioNota " & _
                    " Set transaction isolation level read committed ")
        cmdNotas.CommandType = CommandType.Text
        cmdNotas.Parameters.Clear()
        cmdNotas.Parameters.Add("@FNota", SqlDbType.DateTime).Value = CType(IIf(txtFecha.Text = "", Now, txtFecha.DateTime), Date)
        cmdNotas.Parameters.Add("@TipoNota", SqlDbType.Int).Value = CType(IIf(cbTipoNota.SelectedIndex < 0, 1, cbTipoNota.SelectedIndex + 1), Integer)
        cmdNotas.Connection = SqlConnection

        Dim da As New SqlClient.SqlDataAdapter(cmdNotas)
        da.Fill(dsDocumentos.Tables.Item("Nota"))
        dgNotas.DataSource = dsDocumentos.Tables.Item("Nota")
        dgNotas.Refresh()
        da.Dispose()
        cmdNotas.Dispose()
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Extravio()

        Dim frmExtravio As New Extravio()
        frmExtravio.EntradaExtravio(CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("FolioNota"), Integer))
        If frmExtravio.ShowDialog = DialogResult.OK Then
            Actualizar()
        End If
        frmExtravio.Dispose()

    End Sub

    Private Sub Entrega()
        Dim frmEntrega As New EntregaDocumento()
        frmEntrega.Entrada(cbTipoNota.SelectedIndex + 1)
        frmEntrega.ShowDialog()
        frmEntrega.Dispose()
        Actualizar()
    End Sub


    Private Sub ViewNotas_FocusedRowChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles ViewNotas.FocusedRowChanged

        'MsgBox(CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Celula"), Integer))
        'MsgBox(CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Pedido"), Integer))
        'MsgBox(CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("AñoPed"), Integer))

        Dim Añoped As Integer
        Dim Pedido As Integer
        Dim Celula As Integer

        Celula = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Celula"), Integer)
        Pedido = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Pedido"), Integer)
        Añoped = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("AñoPed"), Integer)

        'If IsDBNull(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Celula")) Then
        '    Celula = 0
        'Else
        '    
        'End If

        'If IsDBNull(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Pedido")) Then
        '    Pedido = 0
        'Else
        '    
        'End If

        'If IsDBNull(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("AñoPed")) Then
        '    Añoped = 0
        'Else
        '    
        'End If


        Try
            dsDocumentos.Tables.Item("Pedido").Clear()
            'Dim cmdPedidos As New SqlClient.SqlCommand("Set transaction isolation level read uncommitted " & _
            '        " select IsNull(P.Celula,0) as Celula, IsNull(P.Pedido,0) as Pedido, IsNull(P.AñoPed,0) as AñoPed, PedidoReferencia, T.Descripcion as TipoPedido,  " & _
            '        " FPedido, FSuministro, FCancelacion, Litros, Total, P.Status, P.Cliente, C.Nombre, Case P.Status when 'SURTIDO' Then P.RutaSuministro else P.Ruta end as Ruta, " & _
            '        " TC.Descripcion as TipoCobro, AñoAtt, Folio, P.Autotanque " & _
            '        " from Pedido P Inner Join TipoPedido T ON P.TipoPedido=T.TipoPedido Inner Join Cliente C ON P.Cliente=C.Cliente " & _
            '        " Left Join TipoCobro TC ON P.TipoCobro=TC.TipoCobro	      " & _
            '        " where P.Celula=@Celula and AñoPed=@AñoPed and Pedido=@Pedido " & _
            '        " Set transaction isolation level read committed ")
            Dim cmdPedidos As New SqlClient.SqlCommand("spCFConsultaPedidoNota")
            cmdPedidos.CommandType = CommandType.StoredProcedure
            cmdPedidos.Parameters.Clear()
            cmdPedidos.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
            cmdPedidos.Parameters.Add("@AñoPed", SqlDbType.Int).Value = Añoped
            cmdPedidos.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
            cmdPedidos.Connection = SqlConnection

            Dim da As New SqlClient.SqlDataAdapter(cmdPedidos)
            da.Fill(dsDocumentos.Tables.Item("Pedido"))
            dgPedidos.DataSource = dsDocumentos.Tables.Item("Pedido")
            dgPedidos.Refresh()
            da.Dispose()
            cmdPedidos.Dispose()

            dsDocumentos.Tables.Item("Factura").Clear()
            Dim cmdFacturas As New SqlClient.SqlCommand("Set transaction isolation level read uncommitted " & _
                    " select Distinct F.Factura,FFactura, Fcancelacion, F.Total, F.Status, F.TipoFactura, TF.Descripcion as TipoDes, F.Folio, F.Serie, F.FacturaRemplazo " & _
                    " from Factura F Inner Join TipoFactura TF ON F.TipoFactura=TF.TipoFactura " & _
                    "  Inner Join FacturaPedido FP ON FP.Factura=F.Factura " & _
                    " where FP.Celula=@Celula and FP.AñoPed=@AñoPed and FP.Pedido=@Pedido " & _
                    " Set transaction isolation level read committed ")
            cmdFacturas.CommandType = CommandType.Text
            cmdFacturas.Parameters.Clear()
            cmdFacturas.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
            cmdFacturas.Parameters.Add("@AñoPed", SqlDbType.Int).Value = Añoped
            cmdFacturas.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
            cmdFacturas.Connection = SqlConnection

            Dim daF As New SqlClient.SqlDataAdapter(cmdFacturas)
            daF.Fill(dsDocumentos.Tables.Item("Factura"))
            dgFacturas.DataSource = dsDocumentos.Tables.Item("Factura")
            dgFacturas.Refresh()
            daF.Dispose()
            cmdFacturas.Dispose()

            dsDocumentos.Tables.Item("ValeCredito").Clear()
            Dim cmdVales As New SqlClient.SqlCommand("Set transaction isolation level read uncommitted " & _
                    " select ValeCredito, Celula, AñoPed, FVale, Status, Pedido, FImpresion  " & _
                    " from ValeCredito " & _
                    " where Celula=@Celula and AñoPed=@AñoPed and Pedido=@Pedido  " & _
                    " Set transaction isolation level read committed ")
            cmdVales.CommandType = CommandType.Text
            cmdVales.Parameters.Clear()
            cmdVales.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
            cmdVales.Parameters.Add("@AñoPed", SqlDbType.Int).Value = Añoped
            cmdVales.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
            cmdVales.Connection = SqlConnection

            Dim daV As New SqlClient.SqlDataAdapter(cmdVales)
            daV.Fill(dsDocumentos.Tables.Item("ValeCredito"))
            dgValesCredito.DataSource = dsDocumentos.Tables.Item("ValeCredito")
            dgValesCredito.Refresh()
            daV.Dispose()
            cmdVales.Dispose()

            dsDocumentos.Tables.Item("Liquidacion").Clear()
            Dim cmdLiquidacion As New SqlClient.SqlCommand("Set transaction isolation level read uncommitted " & _
                    " Select AñoAtt, Folio, StatusLogistica, Celula,Ruta,Autotanque,(ImporteCredito+ ImporteContado) as VentaImporte,  " & _
                    " LitrosLiquidados,FPreLiquidacion, TipoLiquidacion, UsuarioLiquidacion, NotasLiquidadas " & _
                    " from  AutotanqueTurno where AñoAtt=@AñoAtt and Folio=@Folio  " & _
                    " Set transaction isolation level read committed ")
            cmdLiquidacion.CommandType = CommandType.Text
            cmdLiquidacion.Parameters.Clear()
            cmdLiquidacion.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("AñoAtt"), Integer)
            cmdLiquidacion.Parameters.Add("@Folio", SqlDbType.Int).Value = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Folio"), Integer)
            cmdLiquidacion.Connection = SqlConnection

            Dim daL As New SqlClient.SqlDataAdapter(cmdLiquidacion)
            daL.Fill(dsDocumentos.Tables.Item("Liquidacion"))
            dgLiquidacion.DataSource = dsDocumentos.Tables.Item("Liquidacion")
            dgLiquidacion.Refresh()
            daL.Dispose()
            cmdLiquidacion.Dispose()

            'Consulta de observaciones de la nota
            consultaObservaciones()

        Catch et As Data.SqlClient.SqlException
            MessageBox.Show(et.ToString, et.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub



    Private Sub txtFecha_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFecha.ValueChanged, cbTipoNota.SelectedIndexChanged
        If _PrimeraVez = False Then
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
            dsDocumentos.Tables.Item("Nota").Clear()
            Dim cmdNotas As New SqlClient.SqlCommand("Set transaction isolation level read uncommitted " & _
                    "select N.Celula, N.Ruta, AñoNota, Nota, N.Serie, IsNull(N.Operador,0) as Operador,IsNull(FolioNota,0) as FolioNota, IsNull(Extraviada,0) as Extraviada, IsNull(Entregado,0) as Entregado, " & _
                    "       N.Autotanque, IsNull(N.AñoPed,0) as AñoPed, IsNull(N.Pedido,0) as Pedido, T.Descripcion as TipoNotaDes, N.FNota, rtrim(N.Status) AS Status, N.FImpresion, N.Usuario, N.StatusConciliacion, " & _
                    "       N.FConciliacion, IsNull(O.Nombre,'') as OperadorNombre, dbo.ExisteNuevaNotaAnterior(AñoNota,Month(FNota),T.TipoNota,FolioNota,Serie, @FNota) as Existe, IsNull(P.AñoAtt,0) as AñoAtt, IsNull(P.Folio,0) as Folio, IsNull(N.Observaciones,'') as Observaciones " & _
                    " from Nota N Inner Join TipoNota T ON N.TipoNota=T.TipoNota" & _
                    "	    Left Join vwCYCOperador O ON N.Operador=O.Operador Left Join Pedido P ON N.Celula=P.Celula and N.AñoPed=P.AñoPed and N.Pedido=P.Pedido " & _
                    " where FNota between @FNota and @FNota+' 23:59:59' and T.TipoNota=@TipoNota " & _
                    " Order by FolioNota " & _
                    " Set transaction isolation level read committed ")
            cmdNotas.CommandType = CommandType.Text
            cmdNotas.Parameters.Clear()
            cmdNotas.Parameters.Add("@FNota", SqlDbType.DateTime).Value = CType(IIf(txtFecha.Text = "", Now, txtFecha.DateTime), Date)
            cmdNotas.Parameters.Add("@TipoNota", SqlDbType.Int).Value = CType(IIf(cbTipoNota.SelectedIndex < 0, 1, cbTipoNota.SelectedIndex + 1), Integer)
            cmdNotas.Connection = SqlConnection

            Dim da As New SqlClient.SqlDataAdapter(cmdNotas)
            da.Fill(dsDocumentos.Tables.Item("Nota"))
            dgNotas.DataSource = dsDocumentos.Tables.Item("Nota")
            dgNotas.Refresh()
            da.Dispose()
            cmdNotas.Dispose()
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If
        _PrimeraVez = False
    End Sub

    Private Sub MenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem9.Click
        Buscar()
    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub btnObservaciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnObservaciones.Click
        If ViewNotas.RowCount > 0 Then
            Dim Añoped As Integer
            Dim Pedido As Integer
            Dim Celula As Integer
            Dim Serie As String
            Dim Remision As Integer

            Celula = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Celula"), Integer)
            Pedido = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Pedido"), Integer)
            Añoped = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("AñoPed"), Integer)
            Serie = CType(IIf(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Serie") Is DBNull.Value, _
                           "", ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Serie")), String)
            Remision = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("FolioNota"), Integer)

            If Remision <> 0 Then
                Dim frmNota As New ControlDocumentosDll.ObservacionesNota(Serie, Remision, CnnSigamet)
                frmNota.ShowDialog()
            End If
        End If
    End Sub

    Private Sub consultaObservaciones()
        lblObservacionesNota.Visible = False
        lblObservacionesNota.Text = ""
        Dim Serie As String
        Dim Remision As Integer

        Serie = CType(IIf(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Serie") Is DBNull.Value, _
                       "", ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("Serie")), String)
        Remision = CType(ViewNotas.GetDataRow(ViewNotas.FocusedRowHandle).Item("FolioNota"), Integer)

        If Remision <> 0 Then
            Dim observacionesNota As New ControlDocumentosDll.CapturaObservacionesNota(Serie, Remision, CnnSigamet)
            observacionesNota.ConsultaDatos()
            If observacionesNota.Observaciones.Length > 0 Then
                lblObservacionesNota.Visible = True
                lblObservacionesNota.Text = "Observaciones: " & observacionesNota.Observaciones
            End If
        End If
    End Sub

    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click

    End Sub
End Class
