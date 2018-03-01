Public Class Relacion
    Inherits System.Windows.Forms.Form
    Private _Contrato As Integer
    Private _Importe As Decimal
    Private _Row As Integer

    Private _fecha As Date

    Public Sub Entrada(ByVal Contrato As Integer, ByVal Importe As Decimal, ByVal Fecha As Date)
        Dim Cheques As Decimal
        Dim Depositos As Decimal
        Dim Efectivo As Decimal
        Dim i As Integer

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        _Contrato = Contrato
        _Importe = Importe
        _fecha = Fecha

        DsLiquidacion.Detalle.DefaultView.RowFilter = " Cliente=" + CType(Contrato, String) + " and Tipo=0 "
        Cheques = 0
        For i = 0 To DsLiquidacion.Detalle.DefaultView.Count - 1
            Cheques = CType(DsLiquidacion.Detalle(i).Monto, Decimal) + Cheques
        Next
        DsLiquidacion.Detalle.DefaultView.RowFilter = ""


        DsLiquidacion.Detalle.DefaultView.RowFilter = " Cliente=" + CType(Contrato, String) + " and Tipo=1 "
        Depositos = 0
        For i = 0 To DsLiquidacion.Detalle.DefaultView.Count - 1
            Depositos = CType(DsLiquidacion.Detalle(i).Monto, Decimal) + Depositos
        Next

        DsLiquidacion.Detalle.DefaultView.RowFilter = ""
        DsLiquidacion.Detalle.DefaultView.RowFilter = " Cliente=" + CType(Contrato, String)
        Efectivo = Importe - (Cheques + Depositos)

        If DsLiquidacion.Documento.Count > 0 Then
            GridView1.SelectRow(0)
            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
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
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents daBancos As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdBanco As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsLiquidacion As Sigamet.dsLiquidacion
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnAltaCheque As System.Windows.Forms.Button
    Friend WithEvents btnEliminaCheque As System.Windows.Forms.Button
    Friend WithEvents btnAltaDeposito As System.Windows.Forms.Button
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents btnEliminar As System.Windows.Forms.Button
    Friend WithEvents PersistentRepository1 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents gridDocumento As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridColumn1 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn2 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn3 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn4 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn5 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn6 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn7 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn8 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn9 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn10 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn11 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn12 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn13 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridControl2 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView3 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridColumn14 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn15 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn16 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn17 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn18 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn19 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn20 As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents GridColumn23 As DevExpress.XtraGrid.Columns.GridColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Relacion))
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.DsLiquidacion = New Sigamet.dsLiquidacion()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.daBancos = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdBanco = New System.Data.SqlClient.SqlCommand()
        Me.btnAltaCheque = New System.Windows.Forms.Button()
        Me.btnEliminaCheque = New System.Windows.Forms.Button()
        Me.btnAltaDeposito = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.gridDocumento = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository1 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn1 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn2 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn3 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn4 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn5 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn6 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn7 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn8 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn9 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn10 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn11 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn12 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn13 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridControl2 = New DevExpress.XtraGrid.GridControl()
        Me.GridView3 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridColumn14 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn15 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn16 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn17 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn18 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn19 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn20 = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.GridColumn23 = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.DsLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.gridDocumento, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(878, 135)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(97, 23)
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "Cerrar"
        '
        'DsLiquidacion
        '
        Me.DsLiquidacion.DataSetName = "dsLiquidacion"
        Me.DsLiquidacion.Locale = New System.Globalization.CultureInfo("es-MX")
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "Data Source=Desarrollo; Initial Catalog=Sigamet;User ID =sa;Password =DEVELOPMENT" & _
        ""
        '
        'daBancos
        '
        Me.daBancos.SelectCommand = Me.cmdBanco
        '
        'cmdBanco
        '
        Me.cmdBanco.CommandText = "Select * from Banco Order by Nombre"
        Me.cmdBanco.Connection = Me.SqlConnection
        '
        'btnAltaCheque
        '
        Me.btnAltaCheque.Location = New System.Drawing.Point(878, 15)
        Me.btnAltaCheque.Name = "btnAltaCheque"
        Me.btnAltaCheque.Size = New System.Drawing.Size(94, 23)
        Me.btnAltaCheque.TabIndex = 17
        Me.btnAltaCheque.Text = "Alta Cheque"
        '
        'btnEliminaCheque
        '
        Me.btnEliminaCheque.Location = New System.Drawing.Point(880, 80)
        Me.btnEliminaCheque.Name = "btnEliminaCheque"
        Me.btnEliminaCheque.Size = New System.Drawing.Size(94, 23)
        Me.btnEliminaCheque.TabIndex = 18
        Me.btnEliminaCheque.Text = "Eliminar registro"
        '
        'btnAltaDeposito
        '
        Me.btnAltaDeposito.Location = New System.Drawing.Point(880, 48)
        Me.btnAltaDeposito.Name = "btnAltaDeposito"
        Me.btnAltaDeposito.Size = New System.Drawing.Size(94, 23)
        Me.btnAltaDeposito.TabIndex = 19
        Me.btnAltaDeposito.Text = "Alta TPV"
        '
        'btnAgregar
        '
        Me.btnAgregar.Image = CType(resources.GetObject("btnAgregar.Image"), System.Drawing.Image)
        Me.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAgregar.Location = New System.Drawing.Point(357, 316)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(74, 23)
        Me.btnAgregar.TabIndex = 28
        Me.btnAgregar.Text = "Agregar"
        Me.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnEliminar
        '
        Me.btnEliminar.Image = CType(resources.GetObject("btnEliminar.Image"), System.Drawing.Image)
        Me.btnEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEliminar.Location = New System.Drawing.Point(357, 348)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.Size = New System.Drawing.Size(74, 23)
        Me.btnEliminar.TabIndex = 29
        Me.btnEliminar.Text = "Eliminar"
        Me.btnEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gridDocumento
        '
        Me.gridDocumento.DataSource = Me.DsLiquidacion.Documento
        Me.gridDocumento.EditorsRepository = Me.PersistentRepository1
        Me.gridDocumento.Location = New System.Drawing.Point(8, 8)
        Me.gridDocumento.MainView = Me.GridView1
        Me.gridDocumento.Name = "gridDocumento"
        Me.gridDocumento.Size = New System.Drawing.Size(856, 144)
        Me.gridDocumento.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", CType((((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.Gold, System.Drawing.SystemColors.Highlight))
        Me.gridDocumento.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", CType((((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Black, System.Drawing.Color.White))
        Me.gridDocumento.Styles.AddReplace("HeaderPanel", New DevExpress.Utils.ViewStyle("HeaderPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", CType((((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Black, System.Drawing.Color.White))
        Me.gridDocumento.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", CType((((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.Color.Black))
        Me.gridDocumento.TabIndex = 31
        Me.gridDocumento.Text = "GridControl1"
        '
        'PersistentRepository1
        '
        Me.PersistentRepository1.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
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
        Me.GridView1.BehaviorOptions = CType(((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowZoomDetail Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary), DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags)
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn1, Me.GridColumn2, Me.GridColumn3, Me.GridColumn4, Me.GridColumn5, Me.GridColumn6, Me.GridColumn7, Me.GridColumn8, Me.GridColumn9})
        Me.GridView1.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style1", "CH", Nothing, Me.GridColumn1, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style2", "TC", Nothing, Me.GridColumn1, True)})
        Me.GridView1.GroupPanelText = "Cheques Disponibles"
        Me.GridView1.Name = "GridView1"
        Me.GridView1.VertScrollTipFieldName = Nothing
        Me.GridView1.ViewOptions = CType((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowDetailButtons) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle), DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags)
        '
        'GridColumn1
        '
        Me.GridColumn1.Caption = "Tipo"
        Me.GridColumn1.FieldName = "TipoDes"
        Me.GridColumn1.Name = "GridColumn1"
        Me.GridColumn1.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn1.VisibleIndex = 0
        Me.GridColumn1.Width = 30
        '
        'GridColumn2
        '
        Me.GridColumn2.Caption = "Banco"
        Me.GridColumn2.FieldName = "DesBanco"
        Me.GridColumn2.Name = "GridColumn2"
        Me.GridColumn2.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn2.VisibleIndex = 1
        Me.GridColumn2.Width = 111
        '
        'GridColumn3
        '
        Me.GridColumn3.Caption = "Cheque / Folio"
        Me.GridColumn3.FieldName = "Cheque"
        Me.GridColumn3.Name = "GridColumn3"
        Me.GridColumn3.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn3.VisibleIndex = 2
        Me.GridColumn3.Width = 120
        '
        'GridColumn4
        '
        Me.GridColumn4.Caption = "Cuenta"
        Me.GridColumn4.FieldName = "Cuenta"
        Me.GridColumn4.Name = "GridColumn4"
        Me.GridColumn4.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn4.VisibleIndex = 3
        Me.GridColumn4.Width = 120
        '
        'GridColumn5
        '
        Me.GridColumn5.Caption = "Monto"
        Me.GridColumn5.FieldName = "Monto"
        Me.GridColumn5.FormatString = "$ #,##.00"
        Me.GridColumn5.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn5.Name = "GridColumn5"
        Me.GridColumn5.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn5.VisibleIndex = 4
        Me.GridColumn5.Width = 81
        '
        'GridColumn6
        '
        Me.GridColumn6.Caption = "Disponible"
        Me.GridColumn6.FieldName = "Disponible"
        Me.GridColumn6.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn6.Name = "GridColumn6"
        Me.GridColumn6.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn6.VisibleIndex = 5
        Me.GridColumn6.Width = 81
        '
        'GridColumn7
        '
        Me.GridColumn7.Caption = "Contrato"
        Me.GridColumn7.FieldName = "Cliente"
        Me.GridColumn7.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn7.Name = "GridColumn7"
        Me.GridColumn7.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn7.VisibleIndex = 6
        Me.GridColumn7.Width = 85
        '
        'GridColumn8
        '
        Me.GridColumn8.Caption = "Nombre"
        Me.GridColumn8.FieldName = "Nombre"
        Me.GridColumn8.Name = "GridColumn8"
        Me.GridColumn8.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn8.VisibleIndex = 7
        Me.GridColumn8.Width = 153
        '
        'GridColumn9
        '
        Me.GridColumn9.Caption = "PF"
        Me.GridColumn9.FieldName = "PosFechado"
        Me.GridColumn9.Name = "GridColumn9"
        Me.GridColumn9.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn9.VisibleIndex = 8
        Me.GridColumn9.Width = 30
        '
        'GridControl1
        '
        Me.GridControl1.DataSource = Me.DsLiquidacion.Cliente
        Me.GridControl1.EditorsRepository = Me.PersistentRepository1
        Me.GridControl1.Location = New System.Drawing.Point(8, 176)
        Me.GridControl1.MainView = Me.GridView2
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(344, 368)
        Me.GridControl1.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", CType(((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.Color.Red))
        Me.GridControl1.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", CType((((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Firebrick, System.Drawing.Color.White))
        Me.GridControl1.Styles.AddReplace("FocusedRow", New DevExpress.Utils.ViewStyle("FocusedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", CType(((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseImage), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Highlight, System.Drawing.SystemColors.HighlightText))
        Me.GridControl1.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", CType(((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.Color.ForestGreen))
        Me.GridControl1.TabIndex = 32
        Me.GridControl1.Text = "GridControl1"
        '
        'GridView2
        '
        Me.GridView2.BehaviorOptions = CType(((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowZoomDetail Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary), DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags)
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn23, Me.GridColumn10, Me.GridColumn11, Me.GridColumn12, Me.GridColumn13})
        Me.GridView2.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView2.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style1", "CH", Nothing, Me.GridColumn23, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style2", "TC", Nothing, Me.GridColumn23, True)})
        Me.GridView2.GroupPanelText = "Contratos Disponibles"
        Me.GridView2.Name = "GridView2"
        Me.GridView2.ViewOptions = CType((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowDetailButtons) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle), DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags)
        '
        'GridColumn10
        '
        Me.GridColumn10.Caption = "Contrato"
        Me.GridColumn10.FieldName = "Cliente"
        Me.GridColumn10.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn10.Name = "GridColumn10"
        Me.GridColumn10.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn10.VisibleIndex = 1
        Me.GridColumn10.Width = 73
        '
        'GridColumn11
        '
        Me.GridColumn11.Caption = "Nombre"
        Me.GridColumn11.FieldName = "Nombre"
        Me.GridColumn11.Name = "GridColumn11"
        Me.GridColumn11.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn11.VisibleIndex = 2
        Me.GridColumn11.Width = 73
        '
        'GridColumn12
        '
        Me.GridColumn12.Caption = "Monto"
        Me.GridColumn12.FieldName = "Monto"
        Me.GridColumn12.FormatString = "$ #,##.00"
        Me.GridColumn12.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn12.Name = "GridColumn12"
        Me.GridColumn12.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn12.VisibleIndex = 3
        Me.GridColumn12.Width = 73
        '
        'GridColumn13
        '
        Me.GridColumn13.Caption = "Por pagar"
        Me.GridColumn13.FieldName = "Disponible"
        Me.GridColumn13.FormatString = "$ #,##.00"
        Me.GridColumn13.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn13.Name = "GridColumn13"
        Me.GridColumn13.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn13.VisibleIndex = 4
        Me.GridColumn13.Width = 76
        '
        'GridControl2
        '
        Me.GridControl2.DataSource = Me.DsLiquidacion.Detalle
        Me.GridControl2.EditorsRepository = Me.PersistentRepository1
        Me.GridControl2.Location = New System.Drawing.Point(439, 176)
        Me.GridControl2.MainView = Me.GridView3
        Me.GridControl2.Name = "GridControl2"
        Me.GridControl2.Size = New System.Drawing.Size(537, 368)
        Me.GridControl2.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", CType((((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), DevExpress.Utils.StyleOptions), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkGreen, System.Drawing.Color.White))
        Me.GridControl2.TabIndex = 33
        Me.GridControl2.Text = "GridControl2"
        '
        'GridView3
        '
        Me.GridView3.BehaviorOptions = CType(((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowZoomDetail Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary), DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags)
        Me.GridView3.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.GridColumn14, Me.GridColumn15, Me.GridColumn16, Me.GridColumn17, Me.GridColumn18, Me.GridColumn19, Me.GridColumn20})
        Me.GridView3.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView3.GroupPanelText = "Documentos registrados"
        Me.GridView3.Name = "GridView3"
        Me.GridView3.VertScrollTipFieldName = Nothing
        Me.GridView3.ViewOptions = CType((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowDetailButtons) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle), DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags)
        '
        'GridColumn14
        '
        Me.GridColumn14.Caption = "Tipo"
        Me.GridColumn14.FieldName = "DesTipo"
        Me.GridColumn14.Name = "GridColumn14"
        Me.GridColumn14.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn14.VisibleIndex = 0
        Me.GridColumn14.Width = 39
        '
        'GridColumn15
        '
        Me.GridColumn15.Caption = "Banco"
        Me.GridColumn15.FieldName = "NombreBanco"
        Me.GridColumn15.Name = "GridColumn15"
        Me.GridColumn15.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn15.VisibleIndex = 1
        Me.GridColumn15.Width = 79
        '
        'GridColumn16
        '
        Me.GridColumn16.Caption = "Cheque / Folio"
        Me.GridColumn16.FieldName = "Cheque"
        Me.GridColumn16.Name = "GridColumn16"
        Me.GridColumn16.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn16.VisibleIndex = 2
        Me.GridColumn16.Width = 79
        '
        'GridColumn17
        '
        Me.GridColumn17.Caption = "Cuenta"
        Me.GridColumn17.FieldName = "Cuenta"
        Me.GridColumn17.Name = "GridColumn17"
        Me.GridColumn17.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn17.VisibleIndex = 3
        Me.GridColumn17.Width = 79
        '
        'GridColumn18
        '
        Me.GridColumn18.Caption = "Monto"
        Me.GridColumn18.FieldName = "Monto"
        Me.GridColumn18.FormatString = "$ #,##.00"
        Me.GridColumn18.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.GridColumn18.Name = "GridColumn18"
        Me.GridColumn18.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn18.VisibleIndex = 4
        Me.GridColumn18.Width = 79
        '
        'GridColumn19
        '
        Me.GridColumn19.Caption = "Contrato"
        Me.GridColumn19.FieldName = "Cliente"
        Me.GridColumn19.Name = "GridColumn19"
        Me.GridColumn19.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn19.VisibleIndex = 5
        '
        'GridColumn20
        '
        Me.GridColumn20.Caption = "Nombre"
        Me.GridColumn20.FieldName = "Nombre"
        Me.GridColumn20.Name = "GridColumn20"
        Me.GridColumn20.Options = CType(((DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.CanFocused) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm), DevExpress.XtraGrid.Columns.ColumnOptions)
        Me.GridColumn20.VisibleIndex = 6
        Me.GridColumn20.Width = 93
        '
        'GridColumn23
        '
        Me.GridColumn23.Caption = "Tipo"
        Me.GridColumn23.FieldName = "DesTipo"
        Me.GridColumn23.Name = "GridColumn23"
        Me.GridColumn23.VisibleIndex = 0
        Me.GridColumn23.Width = 35
        '
        'Relacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(984, 558)
        Me.Controls.Add(Me.GridControl2)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.gridDocumento)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnAltaDeposito)
        Me.Controls.Add(Me.btnEliminaCheque)
        Me.Controls.Add(Me.btnAltaCheque)
        Me.Controls.Add(Me.btnAceptar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Relacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detalle de pago de contado"
        CType(Me.DsLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.gridDocumento, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Function fnTodosRelacionadosAndDisponibleCero() As Boolean
        Dim i As Integer
        fnTodosRelacionadosAndDisponibleCero = True
        For i = 0 To DsLiquidacion.Documento.Count - 1
            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(i).Banco, String) + " and Cuenta='" + DsLiquidacion.Documento(i).Cuenta + "' and Cheque='" + DsLiquidacion.Documento(i).Cheque + "' "
            If DsLiquidacion.Detalle.DefaultView.Count = 0 Then
                MsgBox("Existen cheques dados de alta que no se han relacionado. Verifique no puede cerrar.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                fnTodosRelacionadosAndDisponibleCero = False
            Else
                If CType(DsLiquidacion.Documento(i).Disponible, Decimal) > 0.0 Then
                    MsgBox("Existen cheques dados de alta cuyo Disponible es MAYOR a cero. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    fnTodosRelacionadosAndDisponibleCero = True
                End If
            End If
            If Not fnTodosRelacionadosAndDisponibleCero Then
                Exit For
            End If
        Next
    End Function

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If fnTodosRelacionadosAndDisponibleCero() Then
            Me.Close()
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAltaCheque.Click
        Dim Banco As Integer = 0
        Dim FCheque As DateTime
        Dim Cheque As String = ""
        Dim Cuenta As String = ""
        Dim Monto As Decimal = 0
        Dim Disponible As Decimal = 0
        Dim NombreBanco As String = ""
        Dim Cliente As Integer = 0
        Dim NombreCliente As String = ""
        Dim PosFechado As String = ""

        Dim i As Integer = Nothing

        Dim frmAltaCheque As AltaCheque = New AltaCheque()
        frmAltaCheque.Entrada(_Contrato)

        If frmAltaCheque.ShowDialog <> DialogResult.Cancel Then

            Banco = CType(frmAltaCheque._Banco, Integer)
            FCheque = CType(frmAltaCheque._FCheque, DateTime)
            Cheque = CType(frmAltaCheque._Cheque, String)
            Cuenta = CType(frmAltaCheque._Cuenta, String)
            Monto = CType(frmAltaCheque._Monto, Decimal)
            Disponible = CType(frmAltaCheque._Monto, Decimal)
            NombreBanco = CType(frmAltaCheque._Nombre, String)
            Cliente = CType(frmAltaCheque._Cliente, Integer)
            NombreCliente = CType(frmAltaCheque._NombreCliente, String)
            PosFechado = CType(frmAltaCheque._PosFechado, String)

            DsLiquidacion.Documento.DefaultView.RowFilter = " Banco = " + CType(Banco, String) + " and Cuenta='" + Cuenta + "' and Cheque='" + Cheque + "' "
            If DsLiquidacion.Documento.DefaultView.Count > 0 Then
                MsgBox("Este cheque ya fue dado de alta anteriormente. Verifique.", MsgBoxStyle.Exclamation, "Mensaje sistema")
                DsLiquidacion.Documento.DefaultView.RowFilter = ""
                GridView1.FocusedRowHandle = _Row
                Exit Sub
            End If
            DsLiquidacion.Documento.DefaultView.RowFilter = ""
            DsLiquidacion.Documento.AddDocumentoRow(Banco, Cheque, FCheque, Cuenta, Monto, Disponible, NombreBanco, DsLiquidacion.Documento.Count + 1, 0, "CH", Cliente, NombreCliente, PosFechado, False)

            _Row = DsLiquidacion.Documento.Rows.Count - 1

        End If
        frmAltaCheque.Dispose()

        GridView1.FocusedRowHandle = _Row

    End Sub


    Private Sub btnAltaDeposito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAltaDeposito.Click
        Dim Banco As Integer = 0
        Dim FCheque As DateTime
        Dim Cheque As String = ""
        Dim Cuenta As String = ""
        Dim Monto As Decimal = 0
        Dim Disponible As Decimal = 0
        Dim NombreBanco As String = ""
        Dim Cliente As Integer = 0
        Dim NombreCliente As String = ""
        Dim TPV As Boolean = False

        Dim i As Integer = Nothing

        Dim frmAltaDeposito As AltaDeposito = New AltaDeposito()
        frmAltaDeposito.Entrada(_Contrato)
        If frmAltaDeposito.ShowDialog <> DialogResult.Cancel Then

            Banco = CType(frmAltaDeposito._Banco, Integer)
            FCheque = CType(frmAltaDeposito._FCheque, DateTime)
            Cheque = CType(frmAltaDeposito._Cheque, String)
            Cuenta = CType(frmAltaDeposito._Cuenta, String)
            Monto = CType(frmAltaDeposito._Monto, Decimal)
            Disponible = CType(frmAltaDeposito._Monto, Decimal)
            NombreBanco = CType(frmAltaDeposito._Nombre, String)
            Cliente = CType(frmAltaDeposito._Cliente, Integer)
            NombreCliente = CType(frmAltaDeposito._NombreCliente, String)
            TPV = CType(frmAltaDeposito._TPV, Boolean)

            DsLiquidacion.Documento.DefaultView.RowFilter = " Banco = " + CType(Banco, String) + " and Cuenta='" + Cuenta + "' and Cheque='" + Cheque + "' "
            If DsLiquidacion.Documento.DefaultView.Count > 0 Then
                MsgBox("Este deposito ya fue dado de alta anteriormente. Verifique.", MsgBoxStyle.Exclamation, "Mensaje sistema")
                DsLiquidacion.Documento.DefaultView.RowFilter = ""
                GridView1.FocusedRowHandle = _Row
                Exit Sub
            End If
            DsLiquidacion.Documento.DefaultView.RowFilter = ""
            DsLiquidacion.Documento.AddDocumentoRow(Banco, Cheque, FCheque, Cuenta, Monto, Disponible, NombreBanco, DsLiquidacion.Documento.Count + 1, 1, "TC", Cliente, NombreCliente, "", TPV)

            _Row = DsLiquidacion.Documento.Rows.Count - 1

        End If

        frmAltaDeposito.Dispose()

        GridView1.FocusedRowHandle = _Row

    End Sub

    Private Sub btnEliminaCheque_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminaCheque.Click

        Dim i As Integer = Nothing

        If DsLiquidacion.Documento.Count > 0 Then
            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
            If DsLiquidacion.Detalle.DefaultView.Count > 0 Then
                MsgBox("No se puede eliminar este documento por que tiene pagos relacionados.", MsgBoxStyle.Exclamation, "Mensaje sistema")
                DsLiquidacion.Detalle.DefaultView.RowFilter = ""
                DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
                Exit Sub
            End If

            DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
            DsLiquidacion.Documento.Rows(GridView1.FocusedRowHandle).Delete()
        End If

    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim MontoMaximo As Decimal = 0
        Dim Disponible As Decimal = 0
        Dim Efectivo As Decimal = 0
        Dim Monto As Decimal = 0
        Dim Cantidad As Decimal = 0
        Dim Cheques As Decimal = 0
        Dim Depositos As Decimal = 0
        Dim Efectivo2 As Decimal = 0
        Dim i As Integer = 0

        If DsLiquidacion.Documento.Count > 0 Then

            If CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Tipo, Integer) <> CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Tipo, Integer) Then
                MsgBox("Los movimientos no son del mismo tipo. Verifique.", MsgBoxStyle.Exclamation, "Mensaje sistema")
                Exit Sub
            End If

            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' and Cliente= " + CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Cliente, String)
            If DsLiquidacion.Detalle.DefaultView.Count > 0 Then
                MsgBox("Este deposito ya fue dado de alta anteriormente a este cliente. Verifique.", MsgBoxStyle.Exclamation, "Mensaje sistema")
                DsLiquidacion.Detalle.DefaultView.RowFilter = ""
                DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
                Exit Sub
            End If

            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "

            MontoMaximo = CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Disponible, Decimal)

            If MontoMaximo > 0 Then

                Disponible = CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Disponible, Decimal)
                Efectivo = CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Disponible, Decimal)

                If Disponible < Efectivo Then
                    Monto = Disponible
                Else
                    Monto = Efectivo
                End If

                ''Para validar la captura de cheques a clientes con descuentos
                'Dim frmWarning As New SigaMetClasses.frmWarning(CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Cliente, Integer), False)

                'If frmWarning.DialogResult = DialogResult.OK Then
                '    Exit Sub
                'End If

                Dim _cliente As Integer
                Dim _litros As Double
                Dim _porPagar As Decimal
                Dim _importeTotal As Decimal

                Try
                    _cliente = CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Cliente, Integer)
                    _litros = CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Litros, Double)
                    _porPagar = Efectivo
                    _importeTotal = CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Monto, Decimal)
                Catch ex As Exception
                    _cliente = Nothing
                    _litros = Nothing
                    _porPagar = Nothing
                    _importeTotal = Nothing
                End Try


                Dim frmCantidad As Cantidad = New Cantidad()
                frmCantidad.Entrada(Monto, _cliente, _litros, _porPagar, _importeTotal, _fecha)

                If frmCantidad.ShowDialog <> DialogResult.Cancel Then
                    Cantidad = CType(frmCantidad._Cantidad, Decimal)
                    If CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).TipoDes, String) = "CH" Then
                        DsLiquidacion.Detalle.AddDetalleRow(CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Cliente, String), Cantidad, 0, "CH", CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, Long), CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String), CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String), CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).DesBanco, String), CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Nombre, String))
                    Else
                        DsLiquidacion.Detalle.AddDetalleRow(CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Cliente, String), Cantidad, 1, "TC", CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, Long), CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String), CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String), CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).DesBanco, String), CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Nombre, String))
                    End If

                    DsLiquidacion.Detalle.DefaultView.RowFilter = ""
                    DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "

                    DsLiquidacion.Documento.DefaultView.RowFilter = ""
                    DsLiquidacion.Documento.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
                    DsLiquidacion.Documento.DefaultView(0).Item(5) = CType(DsLiquidacion.Documento.DefaultView(0).Item(5), Decimal) - Cantidad
                    DsLiquidacion.Documento.DefaultView.RowFilter = ""

                    DsLiquidacion.Cliente.DefaultView.RowFilter = ""
                    DsLiquidacion.Cliente.DefaultView.RowFilter = "Cliente= " + CType(DsLiquidacion.Cliente(GridView2.FocusedRowHandle).Cliente, String)
                    DsLiquidacion.Cliente.DefaultView(0).Item(2) = CType(DsLiquidacion.Cliente.DefaultView(0).Item(2), Decimal) - Cantidad
                    DsLiquidacion.Cliente.DefaultView.RowFilter = ""
                End If

                frmCantidad.Dispose()
            Else
                MsgBox("El monto del registro ya fue cubierto. No se le puede asignar otro documento.", MsgBoxStyle.Information, "Mensaje del sistema")
            End If

        End If

        GridView1.FocusedRowHandle = _Row

    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Dim MontoMaximo As Decimal = 0
        Dim Disponible As Decimal = 0
        Dim Efectivo As Decimal = 0
        Dim Monto As Decimal = 0
        Dim Cantidad As Decimal = 0
        Dim Cheques As Decimal = 0
        Dim Depositos As Decimal = 0
        Dim Efectivo2 As Decimal = 0
        Dim i As Integer = 0

        If DsLiquidacion.Detalle.DefaultView.Count > 0 Then

            'DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            'DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' and Cliente= " + CType(DsLiquidacion.Detalle(GridView3.FocusedRowHandle).Cheque, String)
            Cantidad = CType(DsLiquidacion.Detalle.DefaultView(GridView3.FocusedRowHandle).Item("Monto"), Decimal)
            'MsgBox(CType(DsLiquidacion.Detalle.DefaultView(-1).Row.Item("Monto"), Decimal))


            'CType(DsLiquidacion.Detalle.DefaultView(GridView3.FocusedRowHandle).Monto, Decimal)
            'MsgBox(DsLiquidacion.Detalle.DefaultView(GridView3.FocusedRowHandle).Item("Monto"))

            DsLiquidacion.Cliente.DefaultView.RowFilter = ""
            DsLiquidacion.Cliente.DefaultView.RowFilter = "Cliente= " + CType(DsLiquidacion.Detalle.DefaultView(GridView3.FocusedRowHandle).Item("Cliente"), String)
            DsLiquidacion.Cliente.DefaultView(0).Item(2) = CType(DsLiquidacion.Cliente.DefaultView(0).Item(2), Decimal) + Cantidad
            DsLiquidacion.Cliente.DefaultView.RowFilter = ""

            DsLiquidacion.Detalle.DefaultView(GridView3.FocusedRowHandle).Delete()

            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "

            DsLiquidacion.Documento.DefaultView.RowFilter = ""
            DsLiquidacion.Documento.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
            DsLiquidacion.Documento.DefaultView(0).Item(5) = CType(DsLiquidacion.Documento.DefaultView(0).Item(5), Decimal) + Cantidad
            DsLiquidacion.Documento.DefaultView.RowFilter = ""

        End If

        GridView1.FocusedRowHandle = _Row

    End Sub

    Private Sub Relacion_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub


    Private Sub GridView1_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        DsLiquidacion.Detalle.DefaultView.RowFilter = ""
        DsLiquidacion.Detalle.DefaultView.RowFilter = " Banco = " + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Banco, String) + " and Cuenta='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cuenta, String) + "' and Cheque='" + CType(DsLiquidacion.Documento(GridView1.FocusedRowHandle).Cheque, String) + "' "
    End Sub


    Private Sub gridDocumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gridDocumento.Click
        _Row = GridView1.FocusedRowHandle
    End Sub

End Class
