Public Class HorariosColonia
    Inherits System.Windows.Forms.Form
    Private _DatosCargados As Boolean
    Private _DatosCargadosRuta As Boolean

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
    Friend WithEvents TaskPane1 As VbPowerPack.TaskPane
    Friend WithEvents TaskFrame1 As VbPowerPack.TaskFrame
    Friend WithEvents lnkCerrar As System.Windows.Forms.LinkLabel
    Friend WithEvents ImageButton4 As VbPowerPack.ImageButton
    Friend WithEvents lnkRefrescar As System.Windows.Forms.LinkLabel
    Friend WithEvents btnRefrescar As VbPowerPack.ImageButton
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboCelulaOrigen As SigaMetClasses.Combos.ComboUsuarioCelula
    Friend WithEvents cboRutaOrigen As SigaMetClasses.Combos.ComboRuta2
    Friend WithEvents dsHorarios As System.Data.DataSet
    Friend WithEvents dtColonia As System.Data.DataTable
    Friend WithEvents dtHorario As System.Data.DataTable
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PersistentRepository1 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents PersistentRepository2 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents dgColonia As DevExpress.XtraGrid.GridControl
    Private WithEvents gcCP As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcColonia As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcMunicipio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcEstado As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents PersistentRepository3 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents CardView1 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents gcDia As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcInicio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dgHorario As DevExpress.XtraGrid.GridControl
    Friend WithEvents ImageButton1 As VbPowerPack.ImageButton
    Friend WithEvents ImageButton2 As VbPowerPack.ImageButton
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents gcNumeroColonia As DevExpress.XtraGrid.Columns.GridColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HorariosColonia))
		Dim ColumnFilterInfo1 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Dim ColumnFilterInfo2 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Dim ColumnFilterInfo3 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Dim ColumnFilterInfo4 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Dim ColumnFilterInfo5 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Dim ColumnFilterInfo6 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Dim ColumnFilterInfo7 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Dim ColumnFilterInfo8 As DevExpress.XtraGrid.Columns.ColumnFilterInfo = New DevExpress.XtraGrid.Columns.ColumnFilterInfo()
		Me.TaskPane1 = New VbPowerPack.TaskPane()
		Me.TaskFrame1 = New VbPowerPack.TaskFrame()
		Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
		Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
		Me.ImageButton2 = New VbPowerPack.ImageButton()
		Me.ImageButton1 = New VbPowerPack.ImageButton()
		Me.cboCelulaOrigen = New SigaMetClasses.Combos.ComboUsuarioCelula()
		Me.cboRutaOrigen = New SigaMetClasses.Combos.ComboRuta2()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.lnkCerrar = New System.Windows.Forms.LinkLabel()
		Me.ImageButton4 = New VbPowerPack.ImageButton()
		Me.lnkRefrescar = New System.Windows.Forms.LinkLabel()
		Me.btnRefrescar = New VbPowerPack.ImageButton()
		Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
		Me.dsHorarios = New System.Data.DataSet()
		Me.dtColonia = New System.Data.DataTable()
		Me.dtHorario = New System.Data.DataTable()
		Me.Panel1 = New System.Windows.Forms.Panel()
		Me.dgHorario = New DevExpress.XtraGrid.GridControl()
		Me.PersistentRepository3 = New DevExpress.XtraEditors.Repository.PersistentRepository()
		Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
		Me.CardView1 = New DevExpress.XtraGrid.Views.Card.CardView()
		Me.gcDia = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gcInicio = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gcFin = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.PersistentRepository1 = New DevExpress.XtraEditors.Repository.PersistentRepository()
		Me.dgColonia = New DevExpress.XtraGrid.GridControl()
		Me.PersistentRepository2 = New DevExpress.XtraEditors.Repository.PersistentRepository()
		Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
		Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
		Me.gcCP = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gcNumeroColonia = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gcColonia = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gcMunicipio = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.gcEstado = New DevExpress.XtraGrid.Columns.GridColumn()
		Me.TaskPane1.SuspendLayout()
		Me.TaskFrame1.SuspendLayout()
		CType(Me.dsHorarios, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.dtColonia, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.dtHorario, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.Panel1.SuspendLayout()
		CType(Me.dgHorario, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CardView1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.dgColonia, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'TaskPane1
		'
		Me.TaskPane1.AutoScroll = True
		Me.TaskPane1.BackColor = System.Drawing.SystemColors.InactiveCaption
		Me.TaskPane1.Controls.Add(Me.TaskFrame1)
		Me.TaskPane1.Dock = System.Windows.Forms.DockStyle.Left
		Me.TaskPane1.Location = New System.Drawing.Point(0, 0)
		Me.TaskPane1.Name = "TaskPane1"
		Me.TaskPane1.Size = New System.Drawing.Size(208, 670)
		Me.TaskPane1.TabIndex = 66
		'
		'TaskFrame1
		'
		Me.TaskFrame1.AllowDrop = True
		Me.TaskFrame1.BackColor = System.Drawing.Color.LightSteelBlue
		Me.TaskFrame1.CaptionBlend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Horizontal, System.Drawing.SystemColors.Window, System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer)))
		Me.TaskFrame1.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption
		Me.TaskFrame1.Controls.Add(Me.LinkLabel2)
		Me.TaskFrame1.Controls.Add(Me.LinkLabel1)
		Me.TaskFrame1.Controls.Add(Me.ImageButton2)
		Me.TaskFrame1.Controls.Add(Me.ImageButton1)
		Me.TaskFrame1.Controls.Add(Me.cboCelulaOrigen)
		Me.TaskFrame1.Controls.Add(Me.cboRutaOrigen)
		Me.TaskFrame1.Controls.Add(Me.Label2)
		Me.TaskFrame1.Controls.Add(Me.Label1)
		Me.TaskFrame1.Controls.Add(Me.lnkCerrar)
		Me.TaskFrame1.Controls.Add(Me.ImageButton4)
		Me.TaskFrame1.Controls.Add(Me.lnkRefrescar)
		Me.TaskFrame1.Controls.Add(Me.btnRefrescar)
		Me.TaskFrame1.Location = New System.Drawing.Point(12, 34)
		Me.TaskFrame1.Name = "TaskFrame1"
		Me.TaskFrame1.Size = New System.Drawing.Size(184, 550)
		Me.TaskFrame1.TabIndex = 1
		Me.TaskFrame1.Text = "Rutas"
		'
		'LinkLabel2
		'
		Me.LinkLabel2.LinkColor = System.Drawing.Color.Black
		Me.LinkLabel2.Location = New System.Drawing.Point(32, 160)
		Me.LinkLabel2.Name = "LinkLabel2"
		Me.LinkLabel2.Size = New System.Drawing.Size(100, 16)
		Me.LinkLabel2.TabIndex = 71
		Me.LinkLabel2.TabStop = True
		Me.LinkLabel2.Text = "Eliminar"
		Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'LinkLabel1
		'
		Me.LinkLabel1.LinkColor = System.Drawing.Color.Black
		Me.LinkLabel1.Location = New System.Drawing.Point(32, 130)
		Me.LinkLabel1.Name = "LinkLabel1"
		Me.LinkLabel1.Size = New System.Drawing.Size(112, 16)
		Me.LinkLabel1.TabIndex = 70
		Me.LinkLabel1.TabStop = True
		Me.LinkLabel1.Text = "Agregar y modificar"
		Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'ImageButton2
		'
		Me.ImageButton2.BackColor = System.Drawing.Color.Transparent
		Me.ImageButton2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.ImageButton2.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.ImageButton2.Location = New System.Drawing.Point(8, 162)
		Me.ImageButton2.Name = "ImageButton2"
		Me.ImageButton2.NormalImage = CType(resources.GetObject("ImageButton2.NormalImage"), System.Drawing.Image)
		Me.ImageButton2.Size = New System.Drawing.Size(16, 16)
		Me.ImageButton2.TabIndex = 69
		'
		'ImageButton1
		'
		Me.ImageButton1.BackColor = System.Drawing.Color.Transparent
		Me.ImageButton1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.ImageButton1.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.ImageButton1.Location = New System.Drawing.Point(8, 130)
		Me.ImageButton1.Name = "ImageButton1"
		Me.ImageButton1.NormalImage = CType(resources.GetObject("ImageButton1.NormalImage"), System.Drawing.Image)
		Me.ImageButton1.Size = New System.Drawing.Size(16, 16)
		Me.ImageButton1.TabIndex = 68
		'
		'cboCelulaOrigen
		'
		Me.cboCelulaOrigen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboCelulaOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboCelulaOrigen.Location = New System.Drawing.Point(8, 29)
		Me.cboCelulaOrigen.Name = "cboCelulaOrigen"
		Me.cboCelulaOrigen.Size = New System.Drawing.Size(160, 21)
		Me.cboCelulaOrigen.TabIndex = 67
		'
		'cboRutaOrigen
		'
		Me.cboRutaOrigen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboRutaOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboRutaOrigen.Location = New System.Drawing.Point(8, 80)
		Me.cboRutaOrigen.Name = "cboRutaOrigen"
		Me.cboRutaOrigen.Size = New System.Drawing.Size(160, 21)
		Me.cboRutaOrigen.TabIndex = 66
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(8, 64)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(37, 13)
		Me.Label2.TabIndex = 10
		Me.Label2.Text = "Ruta :"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(8, 10)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(43, 13)
		Me.Label1.TabIndex = 8
		Me.Label1.Text = "Celula :"
		'
		'lnkCerrar
		'
		Me.lnkCerrar.LinkColor = System.Drawing.Color.Black
		Me.lnkCerrar.Location = New System.Drawing.Point(39, 237)
		Me.lnkCerrar.Name = "lnkCerrar"
		Me.lnkCerrar.Size = New System.Drawing.Size(100, 16)
		Me.lnkCerrar.TabIndex = 7
		Me.lnkCerrar.TabStop = True
		Me.lnkCerrar.Text = "Cerrar"
		Me.lnkCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'ImageButton4
		'
		Me.ImageButton4.BackColor = System.Drawing.Color.Transparent
		Me.ImageButton4.Cursor = System.Windows.Forms.Cursors.Hand
		Me.ImageButton4.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.ImageButton4.Location = New System.Drawing.Point(7, 237)
		Me.ImageButton4.Name = "ImageButton4"
		Me.ImageButton4.NormalImage = CType(resources.GetObject("ImageButton4.NormalImage"), System.Drawing.Image)
		Me.ImageButton4.Size = New System.Drawing.Size(16, 16)
		Me.ImageButton4.TabIndex = 6
		'
		'lnkRefrescar
		'
		Me.lnkRefrescar.LinkColor = System.Drawing.Color.Black
		Me.lnkRefrescar.Location = New System.Drawing.Point(33, 208)
		Me.lnkRefrescar.Name = "lnkRefrescar"
		Me.lnkRefrescar.Size = New System.Drawing.Size(100, 16)
		Me.lnkRefrescar.TabIndex = 5
		Me.lnkRefrescar.TabStop = True
		Me.lnkRefrescar.Text = "Refrescar"
		Me.lnkRefrescar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'btnRefrescar
		'
		Me.btnRefrescar.BackColor = System.Drawing.Color.Transparent
		Me.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand
		Me.btnRefrescar.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnRefrescar.Location = New System.Drawing.Point(8, 208)
		Me.btnRefrescar.Name = "btnRefrescar"
		Me.btnRefrescar.NormalImage = CType(resources.GetObject("btnRefrescar.NormalImage"), System.Drawing.Image)
		Me.btnRefrescar.Size = New System.Drawing.Size(16, 16)
		Me.btnRefrescar.TabIndex = 4
		'
		'SqlConnection
		'
		Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" &
	"SA;workstation id=DESARROLLO-4;packet size=4096"
		Me.SqlConnection.FireInfoMessageEventOnUserErrors = False
		'
		'dsHorarios
		'
		Me.dsHorarios.DataSetName = "dsHorarios"
		Me.dsHorarios.Locale = New System.Globalization.CultureInfo("es-MX")
		Me.dsHorarios.Tables.AddRange(New System.Data.DataTable() {Me.dtColonia, Me.dtHorario})
		'
		'dtColonia
		'
		Me.dtColonia.TableName = "Colonia"
		'
		'dtHorario
		'
		Me.dtHorario.TableName = "Horario"
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.dgHorario)
		Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.Panel1.Location = New System.Drawing.Point(208, 454)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(784, 216)
		Me.Panel1.TabIndex = 67
		'
		'dgHorario
		'
		Me.dgHorario.Dock = System.Windows.Forms.DockStyle.Fill
		Me.dgHorario.EditorsRepository = Me.PersistentRepository3
		Me.dgHorario.Location = New System.Drawing.Point(0, 0)
		Me.dgHorario.MainView = Me.CardView1
		Me.dgHorario.Name = "dgHorario"
		Me.dgHorario.Size = New System.Drawing.Size(784, 216)
		Me.dgHorario.TabIndex = 0
		Me.dgHorario.Text = "GridControl1"
		'
		'PersistentRepository3
		'
		Me.PersistentRepository3.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit2})
		'
		'RepositoryItemTextEdit2
		'
		Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
		Me.RepositoryItemTextEdit2.Properties.AllowFocused = False
		Me.RepositoryItemTextEdit2.Properties.AutoHeight = False
		Me.RepositoryItemTextEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		'
		'CardView1
		'
		Me.CardView1.CardCaptionFormat = ""
		Me.CardView1.CardWidth = 350
		Me.CardView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcDia, Me.gcInicio, Me.gcFin})
		Me.CardView1.DefaultEdit = Me.RepositoryItemTextEdit2
		Me.CardView1.FocusedCardTopFieldIndex = 0
		Me.CardView1.Name = "CardView1"
		'
		'gcDia
		'
		Me.gcDia.Caption = "Dia"
		Me.gcDia.FieldName = "Dia"
		Me.gcDia.FilterInfo = ColumnFilterInfo1
		Me.gcDia.Name = "gcDia"
		Me.gcDia.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
		Me.gcDia.VisibleIndex = 0
		'
		'gcInicio
		'
		Me.gcInicio.Caption = "Hora Inicial"
		Me.gcInicio.FieldName = "HoraInicio"
		Me.gcInicio.FilterInfo = ColumnFilterInfo2
		Me.gcInicio.Name = "gcInicio"
		Me.gcInicio.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
		Me.gcInicio.VisibleIndex = 1
		'
		'gcFin
		'
		Me.gcFin.Caption = "Hora Final"
		Me.gcFin.FieldName = "HoraTermino"
		Me.gcFin.FilterInfo = ColumnFilterInfo3
		Me.gcFin.Name = "gcFin"
		Me.gcFin.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.CanResized Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
		Me.gcFin.VisibleIndex = 2
		'
		'dgColonia
		'
		Me.dgColonia.Dock = System.Windows.Forms.DockStyle.Fill
		Me.dgColonia.EditorsRepository = Me.PersistentRepository2
		Me.dgColonia.Location = New System.Drawing.Point(208, 0)
		Me.dgColonia.MainView = Me.GridView1
		Me.dgColonia.Name = "dgColonia"
		Me.dgColonia.Size = New System.Drawing.Size(784, 454)
		Me.dgColonia.TabIndex = 68
		Me.dgColonia.Text = "GridControl1"
		'
		'PersistentRepository2
		'
		Me.PersistentRepository2.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
		'
		'RepositoryItemTextEdit1
		'
		Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
		Me.RepositoryItemTextEdit1.Properties.AllowFocused = False
		Me.RepositoryItemTextEdit1.Properties.AutoHeight = False
		Me.RepositoryItemTextEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
		'
		'GridView1
		'
		Me.GridView1.BehaviorOptions = CType((((((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowFilter Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.Editable) _
			Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
			Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
			Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
			Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowSort) _
			Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary) _
			Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.RowAutoHeight), DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags)
		Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcCP, Me.gcNumeroColonia, Me.gcColonia, Me.gcMunicipio, Me.gcEstado})
		Me.GridView1.DefaultEdit = Me.RepositoryItemTextEdit1
		Me.GridView1.GroupPanelText = "Colonias por ruta"
		Me.GridView1.GroupSummary.AddRange(New DevExpress.XtraGrid.GridSummaryItem() {New DevExpress.XtraGrid.GridSummaryItem(DevExpress.XtraGrid.SummaryItemType.Count, "CP", Me.gcCP, "#")})
		Me.GridView1.Name = "GridView1"
		Me.GridView1.VertScrollTipFieldName = Nothing
		Me.GridView1.ViewOptions = CType(((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
			Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFilterPanel) _
			Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFooter) _
			Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
			Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
			Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
			Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
			Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle), DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags)
		'
		'gcCP
		'
		Me.gcCP.Caption = "Codigo postal"
		Me.gcCP.FieldName = "CP"
		Me.gcCP.FilterInfo = ColumnFilterInfo4
		Me.gcCP.Name = "gcCP"
		Me.gcCP.Options = CType((((((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.[ReadOnly]) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
		Me.gcCP.SummaryItem.SummaryType = DevExpress.XtraGrid.SummaryItemType.Count
		Me.gcCP.VisibleIndex = 0
		Me.gcCP.Width = 100
		'
		'gcNumeroColonia
		'
		Me.gcNumeroColonia.Caption = "# Colonia"
		Me.gcNumeroColonia.FieldName = "Colonia"
		Me.gcNumeroColonia.FilterInfo = ColumnFilterInfo5
		Me.gcNumeroColonia.Name = "gcNumeroColonia"
		Me.gcNumeroColonia.VisibleIndex = 1
		'
		'gcColonia
		'
		Me.gcColonia.Caption = "Colonia"
		Me.gcColonia.FieldName = "Nombre"
		Me.gcColonia.FilterInfo = ColumnFilterInfo6
		Me.gcColonia.Name = "gcColonia"
		Me.gcColonia.Options = CType((((((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.[ReadOnly]) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
		Me.gcColonia.VisibleIndex = 2
		Me.gcColonia.Width = 300
		'
		'gcMunicipio
		'
		Me.gcMunicipio.Caption = "Municipio"
		Me.gcMunicipio.FieldName = "DesMunicipio"
		Me.gcMunicipio.FilterInfo = ColumnFilterInfo7
		Me.gcMunicipio.Name = "gcMunicipio"
		Me.gcMunicipio.Options = CType((((((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.[ReadOnly]) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
		Me.gcMunicipio.VisibleIndex = 3
		Me.gcMunicipio.Width = 183
		'
		'gcEstado
		'
		Me.gcEstado.Caption = "Estado"
		Me.gcEstado.FieldName = "DesEstado"
		Me.gcEstado.FilterInfo = ColumnFilterInfo8
		Me.gcEstado.Name = "gcEstado"
		Me.gcEstado.Options = CType((((((DevExpress.XtraGrid.Columns.ColumnOptions.CanFiltered Or DevExpress.XtraGrid.Columns.ColumnOptions.CanResized) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.[ReadOnly]) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
			Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
		Me.gcEstado.VisibleIndex = 4
		Me.gcEstado.Width = 187
		'
		'HorariosColonia
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
		Me.ClientSize = New System.Drawing.Size(992, 670)
		Me.Controls.Add(Me.dgColonia)
		Me.Controls.Add(Me.Panel1)
		Me.Controls.Add(Me.TaskPane1)
		Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Name = "HorariosColonia"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Horarios por colonia"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.TaskPane1.ResumeLayout(False)
		Me.TaskFrame1.ResumeLayout(False)
		Me.TaskFrame1.PerformLayout()
		CType(Me.dsHorarios, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.dtColonia, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.dtHorario, System.ComponentModel.ISupportInitialize).EndInit()
		Me.Panel1.ResumeLayout(False)
		CType(Me.dgHorario, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CardView1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.dgColonia, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

#End Region

	Private Sub lnkCerrar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCerrar.LinkClicked
        Me.Close()
    End Sub

    Private Sub HorariosColonia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        cboCelulaOrigen.CargaDatos(Main.GLOBAL_Usuario)
        If cboCelulaOrigen.Items.Count > 0 Then
            cboCelulaOrigen.SelectedIndex = 0
        End If

        Dim _shrCelulaOrigen As Short = CType(cboCelulaOrigen.SelectedValue, Short)
        cboRutaOrigen.CargaDatos(_shrCelulaOrigen)
        cboRutaOrigen.SelectedIndex = 0
        _DatosCargadosRuta = True

        _DatosCargados = True

    End Sub

    
    Private Sub cboCelulaOrigen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCelulaOrigen.SelectedIndexChanged
        If _DatosCargados Then
            cboRutaOrigen.CargaDatos(CType(cboCelulaOrigen.SelectedValue, Short))
            _DatosCargadosRuta = True
            If cboRutaOrigen.Items.Count > 0 Then
                cboRutaOrigen.SelectedIndex = 0
            End If
        End If

    End Sub

    Private Sub cboRutaOrigen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRutaOrigen.SelectedIndexChanged

        If _DatosCargadosRuta Then
            CargaDatos(CType(cboRutaOrigen.SelectedValue, Integer))
        End If

    End Sub

    Public Sub CargaDatos(ByVal Ruta As Integer)
        dsHorarios.Tables.Item("Colonia").Clear()
        Dim Query As String = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                " Select Distinct R.Ruta, R.CP, C.Colonia, C.Municipio, C.Nombre, M.Nombre as DesMunicipio, M.Estado, E.Nombre as DesEstado " & _
                                " from RutaCP R  Inner Join Colonia C ON R.CP=C.CP Inner Join Municipio M ON C.Municipio=M.Municipio Inner Join Estado E ON E.Estado=M.Estado " & _
                                " where Ruta=@Ruta and C.Nombre<>'' and C.Nombre<>'0' " & _
                                " Order by C.Nombre SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
        Dim cmdColonias As New SqlClient.SqlCommand(Query)
        cmdColonias.CommandType = CommandType.Text
        cmdColonias.Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = Ruta
        cmdColonias.Connection = SqlConnection

        Dim da As New SqlClient.SqlDataAdapter(cmdColonias)
        da.Fill(dsHorarios.Tables.Item("Colonia"))
        dgColonia.DataSource = dsHorarios.Tables.Item("Colonia")
        dgColonia.Refresh()
        da.Dispose()
        cmdColonias.Dispose()

    End Sub

    Private Sub CargaHorarios(ByVal Ruta As Integer, ByVal CP As String, ByVal Colonia As Integer)
        dsHorarios.Tables.Item("Horario").Clear()
        Dim Query As String = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                " select Case DiaSemana   when 1 then 'LUNES' when 2 then 'MARTES' when 3 then 'MIERCOLES' when 4 then 'JUEVES' when 5 then 'VIERNES' " & _
                                " 			when 6 then 'SABADO' when 7 then 'DOMINGO' end as Dia, Substring(Convert(VarChar(20),HoraInicio),12,8) as HoraInicio, " & _
                                " Substring(Convert(VarChar(20),HoraTermino),12,8) as HoraTermino " & _
                                " from RutaCPHorario where Ruta=@Ruta and CP=@CP and Colonia=@Colonia SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
        Dim cmdColonias As New SqlClient.SqlCommand(Query)
        cmdColonias.CommandType = CommandType.Text
        cmdColonias.Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = Ruta
        cmdColonias.Parameters.Add("@CP", SqlDbType.Char).Value = CP
        cmdColonias.Parameters.Add("@Colonia", SqlDbType.Char).Value = Colonia
        cmdColonias.Connection = SqlConnection

        Dim da As New SqlClient.SqlDataAdapter(cmdColonias)
        da.Fill(dsHorarios.Tables.Item("Horario"))
        dgHorario.DataSource = dsHorarios.Tables.Item("Horario")
        dgHorario.Refresh()
        da.Dispose()
        cmdColonias.Dispose()
    End Sub



    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        CargaHorarios(CType(cboRutaOrigen.SelectedValue, Integer), CType(GridView1.GetDataRow(e.FocusedRowHandle).Item("CP"), String), CType(GridView1.GetDataRow(e.FocusedRowHandle).Item("Colonia"), Integer))
    End Sub


    Private Sub lnkRefrescar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefrescar.LinkClicked
        If _DatosCargadosRuta Then
            CargaDatos(CType(cboRutaOrigen.SelectedValue, Integer))
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frmCapturaHorario As New CapturaHorario()
        frmCapturaHorario.Entrada(CType(cboRutaOrigen.SelectedValue, Integer), CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("CP"), String), CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Colonia"), Integer))
        frmCapturaHorario.ShowDialog()
        frmCapturaHorario.Dispose()
        CargaHorarios(CType(cboRutaOrigen.SelectedValue, Integer), CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("CP"), String), CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Colonia"), Integer))

    End Sub

    Private Sub LinkLabel2_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim cmdActualiza As New SqlClient.SqlCommand("Delete RutaCPHorario where Ruta=@Ruta and CP=@CP and Colonia=@Colonia")
        cmdActualiza.CommandType = CommandType.Text
        cmdActualiza.Connection = SqlConnection
        cmdActualiza.Parameters.Clear()
        cmdActualiza.Parameters.Add("@Ruta", SqlDbType.Int).Value = CType(cboRutaOrigen.SelectedValue, Integer)
        cmdActualiza.Parameters.Add("@CP", SqlDbType.Char).Value = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("CP"), String)
        cmdActualiza.Parameters.Add("@Colonia", SqlDbType.Int).Value = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Colonia"), Integer)
        cmdActualiza.ExecuteNonQuery()
        MsgBox("Los Horarios fueron eliminados.", MsgBoxStyle.Information)
        CargaHorarios(CType(cboRutaOrigen.SelectedValue, Integer), CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("CP"), String), CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Colonia"), Integer))
    End Sub

End Class
