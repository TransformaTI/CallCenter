
Public Class Conciliacion
    Inherits System.Windows.Forms.Form
    Private _ClienteGlobal As Integer
    Private _Ruta As Integer

    Private Sub Actualizar()
        Dim datReader As SqlClient.SqlDataAdapter
        Dim ds As New DataSet()

        cmdPedido.CommandType = CommandType.StoredProcedure
        cmdPedido.CommandText = "spSelectConciliaciones"
        cmdPedido.Parameters.Clear()
        cmdPedido.Parameters.Add("@Ruta", SqlDbType.Int).Value = CInt(cmbRuta.SelectedValue)
        datReader = New SqlClient.SqlDataAdapter(cmdPedido)
        datReader.Fill(ds, "Tabla")
        dgConciliar.Refresh()
        dgConciliar.DataSource = Nothing
        dgConciliar.DataSource = ds.Tables("Tabla")
        datReader.Dispose()
        ds.Dispose()
        dgConciliar.Refresh()
    End Sub

    Public Sub Entrada()
        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try
        DsConciliacion.Ruta.Clear()
        daRuta.Fill(DsConciliacion, "Ruta")
        DsConciliacion.Ruta.AddRutaRow(0, "<Todas las Rutas>")
        DsConciliacion.Ruta.DefaultView.Sort = "Ruta"
        cmbRuta.SelectedValue = 0
        Actualizar()

        Me.Show()
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
    Friend WithEvents daOperador As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdOperador As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsConciliacion As Sigamet.dsConciliacion
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents daPedido As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdPedido As System.Data.SqlClient.SqlCommand
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbRuta As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents daRuta As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdRuta As System.Data.SqlClient.SqlCommand
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents gcCelula As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRuta As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcAutoTanque As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFecha As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcCodigo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcContrato As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcNombre As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcPedido As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcLitros As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gdPrecio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcImporte As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcTipoPago As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dgConciliar As DevExpress.XtraGrid.GridControl
    Friend WithEvents PersistentRepository1 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Conciliacion))
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.daOperador = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdOperador = New System.Data.SqlClient.SqlCommand()
        Me.DsConciliacion = New Sigamet.dsConciliacion()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.daPedido = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdPedido = New System.Data.SqlClient.SqlCommand()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbRuta = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dgConciliar = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository1 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcCelula = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRuta = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcAutoTanque = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFecha = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCodigo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcContrato = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcNombre = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcPedido = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcLitros = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gdPrecio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcImporte = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcTipoPago = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.daRuta = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdRuta = New System.Data.SqlClient.SqlCommand()
        CType(Me.DsConciliacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.dgConciliar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "Data Source=ERPMETRO; Initial Catalog=Sigamet;User ID =sa;Password =DEVELOPMENT"
        '
        'daOperador
        '
        Me.daOperador.SelectCommand = Me.cmdOperador
        '
        'cmdOperador
        '
        Me.cmdOperador.CommandText = "SELECT Att.AñoAtt, Att.Folio, Att.Ruta, Att.Celula, Att.Autotanque, O.Operador, E" & _
        ".Nombre FROM AutotanqueTurno Att INNER JOIN TripulacionTurno TT ON Att.Folio = T" & _
        "T.Folio AND Att.AñoAtt = TT.AñoAtt INNER JOIN Operador O ON TT.Operador = O.Oper" & _
        "ador INNER JOIN Empleado E ON O.Empleado = E.Empleado WHERE (Att.Folio = @Folio)" & _
        " AND (Att.AñoAtt = @Anio)"
        Me.cmdOperador.Connection = Me.SqlConnection
        Me.cmdOperador.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Folio", System.Data.SqlDbType.Int, 4, "Folio"))
        Me.cmdOperador.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Anio", System.Data.SqlDbType.SmallInt, 2, "AñoAtt"))
        '
        'DsConciliacion
        '
        Me.DsConciliacion.DataSetName = "dsConciliacion"
        Me.DsConciliacion.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsConciliacion.Namespace = "http://www.tempuri.org/dsConciliacion.xsd"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(928, 40)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 1
        Me.btnAceptar.Text = "Cerrar"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(928, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Conciliar"
        '
        'daPedido
        '
        Me.daPedido.SelectCommand = Me.cmdPedido
        '
        'cmdPedido
        '
        Me.cmdPedido.CommandText = "SELECT N.Codigo, C.Cliente, C.Nombre, P.Pedido, P.AñoPed, P.Celula, P.Litros, P.P" & _
        "recio, P.Importe, TP.Descripcion AS Tipo, att.AñoAtt, att.Folio, att.Ruta, att.A" & _
        "utotanque, N.FNota, O.Operador, E.Nombre AS NombreOperador, N.StatusConciliacion" & _
        " FROM Cliente C INNER JOIN Pedido P ON P.Cliente = C.Cliente INNER JOIN Nota N O" & _
        "N P.Pedido = N.Pedido AND P.AñoPed = N.AñoPed AND N.Celula = P.Celula INNER JOIN" & _
        " TipoCobro TC ON TC.TipoCobro = P.TipoCobro INNER JOIN TipoPago TP ON TC.TipoPag" & _
        "o = TP.TipoPago INNER JOIN AutotanqueTurno att ON att.Folio = P.Folio AND att.Añ" & _
        "oAtt = P.AñoAtt INNER JOIN TripulacionTurno TT ON att.Folio = TT.Folio AND att.A" & _
        "ñoAtt = TT.AñoAtt INNER JOIN Operador O ON TT.Operador = O.Operador INNER JOIN E" & _
        "mpleado E ON O.Empleado = E.Empleado WHERE (N.Status = 'LIQUIDADA') AND (P.TipoP" & _
        "edido = 3) AND (P.Celula = @Celula) AND (TT.TipoAsignacionOperador = 1) AND (O.C" & _
        "ategoriaOperador = 1) AND (N.StatusConciliacion IS NULL) ORDER BY P.Pedido"
        Me.cmdPedido.Connection = Me.SqlConnection
        Me.cmdPedido.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Celula", System.Data.SqlDbType.TinyInt, 1, "Celula"))
        '
        'Panel1
        '
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmbRuta, Me.Label13, Me.dgConciliar})
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(915, 526)
        Me.Panel1.TabIndex = 3
        '
        'cmbRuta
        '
        Me.cmbRuta.BackColor = System.Drawing.SystemColors.Window
        Me.cmbRuta.DataSource = Me.DsConciliacion.Ruta
        Me.cmbRuta.DisplayMember = "Descripcion"
        Me.cmbRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbRuta.Location = New System.Drawing.Point(757, 11)
        Me.cmbRuta.Name = "cmbRuta"
        Me.cmbRuta.TabIndex = 47
        Me.cmbRuta.ValueMember = "Ruta"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.White
        Me.Label13.Location = New System.Drawing.Point(708, 14)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(43, 15)
        Me.Label13.TabIndex = 46
        Me.Label13.Text = "Ruta :"
        '
        'dgConciliar
        '
        Me.dgConciliar.BackColor = System.Drawing.SystemColors.Control
        Me.dgConciliar.EditorsRepository = Me.PersistentRepository1
        Me.dgConciliar.MainView = Me.GridView1
        Me.dgConciliar.Name = "dgConciliar"
        Me.dgConciliar.Size = New System.Drawing.Size(915, 528)
        Me.dgConciliar.Styles.AddReplace("FilterButton", New DevExpress.Utils.ViewStyle("FilterButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.RoyalBlue, System.Drawing.Color.White))
        Me.dgConciliar.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 9.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.Color.White))
        Me.dgConciliar.Styles.AddReplace("Empty", New DevExpress.Utils.ViewStyle("Empty", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window))
        Me.dgConciliar.Styles.AddReplace("Style3", New DevExpress.Utils.ViewStyle("Style3", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(232, Byte), CType(201, Byte), CType(200, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgConciliar.Styles.AddReplace("FocusedRow", New DevExpress.Utils.ViewStyle("FocusedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.HighlightText))
        Me.dgConciliar.Styles.AddReplace("SelectedRow", New DevExpress.Utils.ViewStyle("SelectedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.HighlightText))
        Me.dgConciliar.Styles.AddReplace("Row", New DevExpress.Utils.ViewStyle("Row", "GridView", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", DevExpress.Utils.StyleOptions.StyleEnabled, True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.White, System.Drawing.SystemColors.WindowText))
        Me.dgConciliar.Styles.AddReplace("FilterPanel", New DevExpress.Utils.ViewStyle("FilterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.DarkSeaGreen, System.Drawing.SystemColors.ControlLightLight))
        Me.dgConciliar.Styles.AddReplace("Style4", New DevExpress.Utils.ViewStyle("Style4", Nothing, New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(217, Byte), CType(230, Byte), CType(240, Byte)), System.Drawing.SystemColors.WindowText))
        Me.dgConciliar.TabIndex = 95
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
        Me.GridView1.BehaviorOptions = ((((((((DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AllowFilter Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.Editable) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnableMasterViewMode) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.SmartVertScrollBar) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.UseTabKey) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.EnterMoveNextColumn) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoUpdateTotalSummary) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.AutoMoveRowFocus) _
                    Or DevExpress.XtraGrid.Views.Grid.BehaviorOptionsFlags.RowAutoHeight)
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcCelula, Me.gcRuta, Me.gcAutoTanque, Me.gcFecha, Me.gcCodigo, Me.gcContrato, Me.gcNombre, Me.gcPedido, Me.gcLitros, Me.gdPrecio, Me.gcImporte, Me.gcTipoPago})
        Me.GridView1.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView1.FormatConditions.AddRange(New DevExpress.XtraGrid.StyleFormatCondition() {New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.NotEqual, Nothing, "Style4", 0, Nothing, Me.gcLitros, True), New DevExpress.XtraGrid.StyleFormatCondition(DevExpress.XtraGrid.FormatConditionEnum.Equal, Nothing, "Style3", "CREDITO", Nothing, Me.gcTipoPago, True)})
        Me.GridView1.GroupPanelText = "Notas blancas"
        Me.GridView1.Name = "GridView1"
        Me.GridView1.RowHeight = 24
        Me.GridView1.ViewOptions = ((((((((DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.AutoWidth Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowColumns) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFilterPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowFooter) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowGroupPanel) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowHorzLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowIndicator) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.ShowVertLines) _
                    Or DevExpress.XtraGrid.Views.Grid.ViewOptionsFlags.SingleFocusStyle)
        '
        'gcCelula
        '
        Me.gcCelula.Caption = "Celula"
        Me.gcCelula.FieldName = "celula"
        Me.gcCelula.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcCelula.Name = "gcCelula"
        Me.gcCelula.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcCelula.VisibleIndex = 0
        Me.gcCelula.Width = 45
        '
        'gcRuta
        '
        Me.gcRuta.Caption = "Ruta"
        Me.gcRuta.FieldName = "Ruta"
        Me.gcRuta.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcRuta.Name = "gcRuta"
        Me.gcRuta.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRuta.VisibleIndex = 1
        Me.gcRuta.Width = 45
        '
        'gcAutoTanque
        '
        Me.gcAutoTanque.Caption = "Auto Tanque"
        Me.gcAutoTanque.FieldName = "Auto Tanque"
        Me.gcAutoTanque.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcAutoTanque.Name = "gcAutoTanque"
        Me.gcAutoTanque.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcAutoTanque.VisibleIndex = 2
        Me.gcAutoTanque.Width = 65
        '
        'gcFecha
        '
        Me.gcFecha.Caption = "Fecha"
        Me.gcFecha.FieldName = "Fecha"
        Me.gcFecha.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.DateTime
        Me.gcFecha.Name = "gcFecha"
        Me.gcFecha.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcFecha.VisibleIndex = 3
        Me.gcFecha.Width = 60
        '
        'gcCodigo
        '
        Me.gcCodigo.Caption = "Codigo"
        Me.gcCodigo.FieldName = "Codigo"
        Me.gcCodigo.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcCodigo.Name = "gcCodigo"
        Me.gcCodigo.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcCodigo.VisibleIndex = 4
        Me.gcCodigo.Width = 60
        '
        'gcContrato
        '
        Me.gcContrato.Caption = "Contrato"
        Me.gcContrato.FieldName = "Contrato"
        Me.gcContrato.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcContrato.Name = "gcContrato"
        Me.gcContrato.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcContrato.VisibleIndex = 5
        Me.gcContrato.Width = 70
        '
        'gcNombre
        '
        Me.gcNombre.Caption = "Nombre"
        Me.gcNombre.FieldName = "Nombre"
        Me.gcNombre.Name = "gcNombre"
        Me.gcNombre.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcNombre.VisibleIndex = 6
        Me.gcNombre.Width = 155
        '
        'gcPedido
        '
        Me.gcPedido.Caption = "Pedido"
        Me.gcPedido.FieldName = "Pedido"
        Me.gcPedido.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcPedido.Name = "gcPedido"
        Me.gcPedido.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcPedido.VisibleIndex = 7
        Me.gcPedido.Width = 50
        '
        'gcLitros
        '
        Me.gcLitros.Caption = "Litros"
        Me.gcLitros.FieldName = "Litros"
        Me.gcLitros.FormatString = "#,##"
        Me.gcLitros.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcLitros.Name = "gcLitros"
        Me.gcLitros.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcLitros.VisibleIndex = 8
        Me.gcLitros.Width = 50
        '
        'gdPrecio
        '
        Me.gdPrecio.Caption = "Precio"
        Me.gdPrecio.FieldName = "Precio"
        Me.gdPrecio.FormatString = "#,##.00"
        Me.gdPrecio.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gdPrecio.Name = "gdPrecio"
        Me.gdPrecio.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gdPrecio.VisibleIndex = 9
        Me.gdPrecio.Width = 50
        '
        'gcImporte
        '
        Me.gcImporte.Caption = "Importe"
        Me.gcImporte.FieldName = "Importe"
        Me.gcImporte.FormatString = "$ #,##.00"
        Me.gcImporte.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcImporte.Name = "gcImporte"
        Me.gcImporte.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcImporte.VisibleIndex = 10
        Me.gcImporte.Width = 70
        '
        'gcTipoPago
        '
        Me.gcTipoPago.Caption = "Tipo"
        Me.gcTipoPago.FieldName = "Tipo"
        Me.gcTipoPago.Name = "gcTipoPago"
        Me.gcTipoPago.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcTipoPago.VisibleIndex = 11
        Me.gcTipoPago.Width = 70
        '
        'daRuta
        '
        Me.daRuta.SelectCommand = Me.cmdRuta
        '
        'cmdRuta
        '
        Me.cmdRuta.CommandText = "SELECT Ruta, Descripcion FROM Ruta WHERE ruta <> 0 and celula in (Select celula f" & _
        "rom Celula where Comercial=1 and Celula <> 0)"
        Me.cmdRuta.Connection = Me.SqlConnection
        '
        'Conciliacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(1016, 526)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel1, Me.Button1, Me.btnAceptar})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Conciliacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Conciliación de notas blancas"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DsConciliacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgConciliar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim frmConciliarCliente As ClienteConciliar = New ClienteConciliar()
        Dim contrato As Integer = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Contrato"), Integer)
        Dim pedido As Integer = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Pedido"), Integer)
        Dim anio As Integer = CType(Year(CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Fecha"), Date)), Integer)
        Dim importe As Decimal = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Importe"), Decimal)
        Dim litros As Decimal = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Litros"), Decimal)
        Dim nombre As String = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Nombre"), String)
        Dim celula As Integer = CType(GridView1.GetDataRow(GridView1.FocusedRowHandle).Item("Celula"), Integer)
        frmConciliarCliente.Entrada(contrato, nombre, pedido, anio, celula, importe, litros)
        frmConciliarCliente.Dispose()
        Actualizar()
    End Sub

    Private Sub cmbRuta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbRuta.SelectedIndexChanged
        Actualizar()
    End Sub

    Private Sub Conciliacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GridView1.FocusedRowHandle = 0
    End Sub

End Class
