
Public Class CambioCheque
    Inherits System.Windows.Forms.Form

    Private _AnioAtt, _Folio, _Cliente, _AnioPed, _Pedido, _Celula, _Row As Integer

    Public Sub Entrada(ByVal AnioAtt As Integer, ByVal Folio As Integer, ByVal Cliente As Integer, _
                       ByVal AnioPed As Integer, ByVal Pedido As Integer, ByVal Celula As Integer)
        Try
            _AnioAtt = AnioAtt
            _Folio = Folio
            _Cliente = Cliente
            _AnioPed = AnioPed
            _Pedido = Pedido
            _Celula = Celula
            _Row = 0
            'TODO Esta conexión no debe estar abierta siempre
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection = CnnSigamet
            SqlConnection.Open()
            cboBanco.CargaDatos(CargaBancoCero:=False, MostrarClaves:=True, SoloActivos:=True)

            CambioCheque("Consulta")

            If dsCambioCheque.Tables.Item("Cheque").Rows.Count > 0 Then
                LlenaDatos()
                Me.ShowDialog()
            Else
                MsgBox("No existe ningún cheque que pueda ser modificado. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
            End If

        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

    End Sub

    Private Sub LlenaDatos()
        txtCliente.Text = Trim(CType(dsCambioCheque.Tables.Item("Cheque").Rows(_Row).Item("Cliente"), String))
        cboBanco.SelectedValue = CType(dsCambioCheque.Tables.Item("Cheque").Rows(_Row).Item("Banco"), Integer)
        txtFCheque.Value = CType(dsCambioCheque.Tables.Item("Cheque").Rows(_Row).Item("FCheque"), Date)
        txtCheque.Text = Trim(CType(dsCambioCheque.Tables.Item("Cheque").Rows(_Row).Item("NumeroCheque"), String))
        txtCuenta.Text = Trim(CType(dsCambioCheque.Tables.Item("Cheque").Rows(_Row).Item("NumeroCuenta"), String))
        txtMonto.Text = Trim(CStr(Format(CType(dsCambioCheque.Tables.Item("Cheque").Rows(_Row).Item("Total"), Decimal), "###,###,##0.00")))
    End Sub

    Private Sub CambioCheque(ByVal Tipo As String)
        Dim Cobro, AñoCobro As Integer

        If Tipo = "Consulta" Then
            dsCambioCheque.Tables.Item("Cheque").Clear()
        Else
            Cobro = CType(dsCambioCheque.Tables.Item("Cheque").Rows(GridView1.FocusedRowHandle).Item("Cobro"), Integer)
            AñoCobro = CType(dsCambioCheque.Tables.Item("Cheque").Rows(GridView1.FocusedRowHandle).Item("AñoCobro"), Integer)
        End If

        Dim cmdCheques As New SqlClient.SqlCommand("sp_CambioCheque")
        cmdCheques.CommandType = CommandType.StoredProcedure
        cmdCheques.Parameters.Add("@Tipo", SqlDbType.Char).Value = Tipo
        cmdCheques.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AnioAtt
        cmdCheques.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
        cmdCheques.Parameters.Add("@AñoPed", SqlDbType.Int).Value = _AnioPed
        cmdCheques.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
        cmdCheques.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
        cmdCheques.Parameters.Add("@Banco", SqlDbType.Int).Value = IIf(Tipo = "Update", CType(cboBanco.SelectedValue, Integer), 0)
        cmdCheques.Parameters.Add("@Cuenta", SqlDbType.Char).Value = IIf(Tipo = "Update", txtCuenta.Text.Trim, "")
        cmdCheques.Parameters.Add("@Cheque", SqlDbType.Char).Value = IIf(Tipo = "Update", txtCheque.Text.Trim, "")
        cmdCheques.Parameters.Add("@FCheque", SqlDbType.DateTime).Value = IIf(Tipo = "Update", txtFCheque.Value.Date, Now.Date)
        cmdCheques.Parameters.Add("@Cobro", SqlDbType.Int).Value = Cobro
        cmdCheques.Parameters.Add("@AñoCobro", SqlDbType.Int).Value = AñoCobro
        cmdCheques.Connection = SqlConnection

        Dim da As New SqlClient.SqlDataAdapter(cmdCheques)
        da.Fill(dsCambioCheque.Tables.Item("Cheque"))
        da.Dispose()
        cmdCheques.Dispose()
        dgCheques.DataSource = dsCambioCheque.Tables.Item("Cheque")
        dgCheques.Refresh()

        If Tipo = "Update" Then
            CambioCheque("Consulta")
        Else
            If dsCambioCheque.Tables.Item("Cheque").Rows.Count > 0 Then
                GridView1.FocusedRowHandle = _Row
                LlenaDatos()
            End If
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
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents cmdBanco As System.Data.SqlClient.SqlCommand
    Friend WithEvents daBancos As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents PersistentRepository1 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents dsCambioCheque As System.Data.DataSet
    Friend WithEvents dtCheque As System.Data.DataTable
    Friend WithEvents cboBanco As SigaMetClasses.Combos.ComboBanco
    Friend WithEvents txtCuenta As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtCheque As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtCliente As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtMonto As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents lbPosfechado As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFCheque As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents dgCheques As DevExpress.XtraGrid.GridControl
    Friend WithEvents PersistentRepository2 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents gcCliente As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcBanco As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcCheque As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFCheque As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcCuenta As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcMonto As DevExpress.XtraGrid.Columns.GridColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CambioCheque))
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.cmdBanco = New System.Data.SqlClient.SqlCommand()
        Me.daBancos = New System.Data.SqlClient.SqlDataAdapter()
        Me.PersistentRepository1 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.dsCambioCheque = New System.Data.DataSet()
        Me.dtCheque = New System.Data.DataTable()
        Me.cboBanco = New SigaMetClasses.Combos.ComboBanco()
        Me.txtCuenta = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtCheque = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtCliente = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtMonto = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.lbPosfechado = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFCheque = New System.Windows.Forms.DateTimePicker()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.dgCheques = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository2 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcCliente = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcBanco = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCheque = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFCheque = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCuenta = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcMonto = New DevExpress.XtraGrid.Columns.GridColumn()
        CType(Me.dsCambioCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dtCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgCheques, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "Data Source=Desarrollo; Initial Catalog=Sigamet;User ID =sa;Password =DEVELOPMENT" & _
        ""
        '
        'cmdBanco
        '
        Me.cmdBanco.CommandText = "Select * from Banco Order by Nombre"
        Me.cmdBanco.Connection = Me.SqlConnection
        '
        'daBancos
        '
        Me.daBancos.SelectCommand = Me.cmdBanco
        '
        'dsCambioCheque
        '
        Me.dsCambioCheque.DataSetName = "dsCambioCheque"
        Me.dsCambioCheque.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.dsCambioCheque.Tables.AddRange(New System.Data.DataTable() {Me.dtCheque})
        '
        'dtCheque
        '
        Me.dtCheque.TableName = "Cheque"
        '
        'cboBanco
        '
        Me.cboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBanco.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBanco.Location = New System.Drawing.Point(104, 240)
        Me.cboBanco.Name = "cboBanco"
        Me.cboBanco.Size = New System.Drawing.Size(288, 26)
        Me.cboBanco.TabIndex = 7
        '
        'txtCuenta
        '
        Me.txtCuenta.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCuenta.Location = New System.Drawing.Point(104, 336)
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(288, 26)
        Me.txtCuenta.TabIndex = 10
        Me.txtCuenta.Text = ""
        '
        'txtCheque
        '
        Me.txtCheque.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheque.Location = New System.Drawing.Point(104, 272)
        Me.txtCheque.Name = "txtCheque"
        Me.txtCheque.Size = New System.Drawing.Size(288, 26)
        Me.txtCheque.TabIndex = 8
        Me.txtCheque.Text = ""
        '
        'txtCliente
        '
        Me.txtCliente.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtCliente.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Location = New System.Drawing.Point(104, 208)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(160, 26)
        Me.txtCliente.TabIndex = 6
        Me.txtCliente.Text = ""
        '
        'txtMonto
        '
        Me.txtMonto.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.txtMonto.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonto.ForeColor = System.Drawing.Color.Red
        Me.txtMonto.Location = New System.Drawing.Point(104, 368)
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.ReadOnly = True
        Me.txtMonto.Size = New System.Drawing.Size(160, 26)
        Me.txtMonto.TabIndex = 11
        Me.txtMonto.Text = ""
        '
        'lbPosfechado
        '
        Me.lbPosfechado.AutoSize = True
        Me.lbPosfechado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPosfechado.ForeColor = System.Drawing.Color.Red
        Me.lbPosfechado.Location = New System.Drawing.Point(456, 312)
        Me.lbPosfechado.Name = "lbPosfechado"
        Me.lbPosfechado.Size = New System.Drawing.Size(93, 14)
        Me.lbPosfechado.TabIndex = 14
        Me.lbPosfechado.Text = "POST FECHADO"
        Me.lbPosfechado.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 376)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 14)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Monto:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 344)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "No. Cuenta:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 312)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 14)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Fecha cheque:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 280)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "No. cheque:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 248)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Banco:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 216)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 14)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Cliente:"
        '
        'txtFCheque
        '
        Me.txtFCheque.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFCheque.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.txtFCheque.Location = New System.Drawing.Point(104, 304)
        Me.txtFCheque.Name = "txtFCheque"
        Me.txtFCheque.Size = New System.Drawing.Size(288, 26)
        Me.txtFCheque.TabIndex = 9
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(472, 240)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 24)
        Me.btnCancelar.TabIndex = 13
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(472, 208)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 12
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dgCheques
        '
        Me.dgCheques.EditorsRepository = Me.PersistentRepository2
        Me.dgCheques.ForeColor = System.Drawing.Color.Navy
        Me.dgCheques.Location = New System.Drawing.Point(16, 16)
        Me.dgCheques.MainView = Me.GridView1
        Me.dgCheques.Name = "dgCheques"
        Me.dgCheques.Size = New System.Drawing.Size(608, 168)
        Me.dgCheques.Styles.AddReplace("FilterButtonPressed", New DevExpress.Utils.ViewStyle("FilterButtonPressed", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightCoral, System.Drawing.SystemColors.Window))
        Me.dgCheques.Styles.AddReplace("HideSelectionRow", New DevExpress.Utils.ViewStyle("HideSelectionRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.Color.White))
        Me.dgCheques.Styles.AddReplace("FilterButton", New DevExpress.Utils.ViewStyle("FilterButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightCoral, System.Drawing.Color.LightCoral))
        Me.dgCheques.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 10.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.PeachPuff, System.Drawing.Color.FromArgb(CType(0, Byte), CType(0, Byte), CType(192, Byte))))
        Me.dgCheques.Styles.AddReplace("Empty", New DevExpress.Utils.ViewStyle("Empty", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window))
        Me.dgCheques.Styles.AddReplace("HeaderPanel", New DevExpress.Utils.ViewStyle("HeaderPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.ControlDark, System.Drawing.Color.Navy))
        Me.dgCheques.Styles.AddReplace("HorzLine", New DevExpress.Utils.ViewStyle("HorzLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightCoral, System.Drawing.Color.LightCoral))
        Me.dgCheques.Styles.AddReplace("FocusedRow", New DevExpress.Utils.ViewStyle("FocusedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.Color.White))
        Me.dgCheques.Styles.AddReplace("VertLine", New DevExpress.Utils.ViewStyle("VertLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightCoral, System.Drawing.Color.LightCoral))
        Me.dgCheques.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.PeachPuff, System.Drawing.Color.Navy))
        Me.dgCheques.Styles.AddReplace("FocusedCell", New DevExpress.Utils.ViewStyle("FocusedCell", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.ControlText))
        Me.dgCheques.Styles.AddReplace("Row", New DevExpress.Utils.ViewStyle("Row", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", DevExpress.Utils.StyleOptions.StyleEnabled, True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.PeachPuff, System.Drawing.Color.Navy))
        Me.dgCheques.Styles.AddReplace("FilterPanel", New DevExpress.Utils.ViewStyle("FilterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightCoral, System.Drawing.SystemColors.ControlLightLight))
        Me.dgCheques.TabIndex = 15
        Me.dgCheques.Text = "GridControl1"
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
        Me.RepositoryItemTextEdit1.Properties.DisabledBackColor = System.Drawing.Color.LightCoral
        Me.RepositoryItemTextEdit1.Properties.DisabledForeColor = System.Drawing.Color.LightCoral
        '
        'GridView1
        '
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcCliente, Me.gcBanco, Me.gcCheque, Me.gcFCheque, Me.gcCuenta, Me.gcMonto})
        Me.GridView1.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView1.GroupPanelText = "Cheques"
        Me.GridView1.Name = "GridView1"
        '
        'gcCliente
        '
        Me.gcCliente.Caption = "Cliente"
        Me.gcCliente.FieldName = "Cliente"
        Me.gcCliente.Name = "gcCliente"
        Me.gcCliente.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm
        Me.gcCliente.StyleName = "Style1"
        Me.gcCliente.VisibleIndex = 0
        '
        'gcBanco
        '
        Me.gcBanco.Caption = "Banco"
        Me.gcBanco.FieldName = "Nombre"
        Me.gcBanco.Name = "gcBanco"
        Me.gcBanco.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm
        Me.gcBanco.VisibleIndex = 1
        Me.gcBanco.Width = 164
        '
        'gcCheque
        '
        Me.gcCheque.Caption = "Cheque"
        Me.gcCheque.FieldName = "NumeroCheque"
        Me.gcCheque.Name = "gcCheque"
        Me.gcCheque.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm
        Me.gcCheque.VisibleIndex = 2
        '
        'gcFCheque
        '
        Me.gcFCheque.Caption = "Fecha cheque"
        Me.gcFCheque.FieldName = "FCheque"
        Me.gcFCheque.Name = "gcFCheque"
        Me.gcFCheque.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm
        Me.gcFCheque.VisibleIndex = 3
        Me.gcFCheque.Width = 90
        '
        'gcCuenta
        '
        Me.gcCuenta.Caption = "Cuenta"
        Me.gcCuenta.FieldName = "NumeroCuenta"
        Me.gcCuenta.Name = "gcCuenta"
        Me.gcCuenta.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm
        Me.gcCuenta.VisibleIndex = 4
        Me.gcCuenta.Width = 89
        '
        'gcMonto
        '
        Me.gcMonto.Caption = "Monto"
        Me.gcMonto.FieldName = "Total"
        Me.gcMonto.FormatString = "###,##0.00"
        Me.gcMonto.Name = "gcMonto"
        Me.gcMonto.Options = DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm
        Me.gcMonto.VisibleIndex = 5
        Me.gcMonto.Width = 101
        '
        'CambioCheque
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(642, 408)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.dgCheques, Me.cboBanco, Me.txtCuenta, Me.txtCheque, Me.txtCliente, Me.txtMonto, Me.lbPosfechado, Me.Label5, Me.Label4, Me.Label3, Me.Label2, Me.Label1, Me.Label6, Me.txtFCheque, Me.btnCancelar, Me.btnAceptar})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CambioCheque"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambio Cheque"
        CType(Me.dsCambioCheque, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dtCheque, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgCheques, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        If txtCheque.Text.Trim = "" Or txtCheque.Text.Trim.Length < 7 Then
            MessageBox.Show("Debe teclear el número de cheque de 7 dígitos.", "Cambio de Cheque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCheque.Focus()
            Exit Sub
        End If
        If txtCuenta.Text.Trim = "" Then
            MessageBox.Show("Debe teclear el número de cuenta.", "Cambio de Cheque", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCuenta.Focus()
            Exit Sub
        End If

        Try
            CambioCheque("Update")
            MsgBox("El cheque se modificó con éxito.", MsgBoxStyle.Information, "Mensaje del sistema")

        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

    End Sub

    Private Sub txtFCheque_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFCheque.ValueChanged
        lbPosfechado.Visible = CBool(IIf(txtFCheque.Value.Date > Now.Date, True, False))
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        SqlConnection.Close()
        Me.Close()
    End Sub

    Private Sub dgCheques_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgCheques.Click
        _Row = GridView1.FocusedRowHandle
        LlenaDatos()
    End Sub

End Class
