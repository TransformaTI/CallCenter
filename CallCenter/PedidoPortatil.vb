Imports System.Data.SqlClient

Public Class frmPedidoPortatil
    Inherits System.Windows.Forms.Form    



#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Cliente As Integer, Optional ByVal Pedido As Integer = 0, Optional ByVal PermitirCambios As Boolean = False)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()


        'LUSATE Consultar precios de productos por zona económica
        Dim cmdZE As SqlCommand = New SqlCommand("spCCConsultaZonaEconomicaPtl", CnnSigamet)
        cmdZE.CommandType = CommandType.StoredProcedure
        cmdZE.Parameters.Add("@ClientePortatil", SqlDbType.Int).Value = Cliente
        cmdZE.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = GLOBAL_Usuario

        Try
            AbreConexion()
            Dim drZE As SqlDataReader = cmdZE.ExecuteReader()
            If drZE.HasRows Then
                drZE.Read()
                If Not IsDBNull(drZE("ZonaEconomica1")) Then
                    ZonaEconomica = CShort(drZE("ZonaEconomica1"))
                ElseIf Not IsDBNull(drZE("ZonaEconomica2")) Then
                    ZonaEconomica = CShort(drZE("ZonaEconomica2"))
                Else
                    ZonaEconomica = CShort(drZE("ZonaEconomica3"))
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
        End Try


        'Add any initialization after the InitializeComponent() call
        'Dim daCallCenter As New SqlDataAdapter("Select Producto, Descripcion, convert(tinyint,0) as Cantidad,'Pruebas'  from Producto where TipoProducto = 5 order by Descripcion", CnnSigamet)
        Dim daCallCenter As New SqlDataAdapter("spCCConsultaProductosPtl @ZonaEconomica = " & ZonaEconomica, CnnSigamet)

        _Cliente = Cliente
        _Pedido = Pedido
        Try
            daCallCenter.Fill(dtProducto)
            grdDetalle.DataSource = dtProducto
            maxProducto = dtProducto.Rows.Count
            CargaZonasEconomicas()            
            CargaDatos(PermitirCambios)
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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
    Friend WithEvents grpPedido As System.Windows.Forms.GroupBox
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblFAlta As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents lblFCompromiso As System.Windows.Forms.Label
    Friend WithEvents lblObservaciones As System.Windows.Forms.Label
    Friend WithEvents txtCliente As System.Windows.Forms.TextBox
    Friend WithEvents txtRuta As System.Windows.Forms.TextBox
    Friend WithEvents txtFAlta As System.Windows.Forms.TextBox
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents dtpFCompromiso As System.Windows.Forms.DateTimePicker
    Friend WithEvents grpDetalle As System.Windows.Forms.GroupBox
    Friend WithEvents tbsDetalle As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents colProducto As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colCantidad As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colPrecio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colTotal As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblTot As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents cboZonaEconomica As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblLeyendaPrecios As System.Windows.Forms.Label
    Friend WithEvents lblFinRuta As System.Windows.Forms.Label
    Friend WithEvents grdDetalle As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPedidoPortatil))
        Me.grpPedido = New System.Windows.Forms.GroupBox()
        Me.lblFinRuta = New System.Windows.Forms.Label()
        Me.cboZonaEconomica = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFCompromiso = New System.Windows.Forms.DateTimePicker()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblFAlta = New System.Windows.Forms.Label()
        Me.lblFCompromiso = New System.Windows.Forms.Label()
        Me.lblObservaciones = New System.Windows.Forms.Label()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.txtFAlta = New System.Windows.Forms.TextBox()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.txtRuta = New System.Windows.Forms.TextBox()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.grpDetalle = New System.Windows.Forms.GroupBox()
        Me.grdDetalle = New System.Windows.Forms.DataGrid()
        Me.tbsDetalle = New System.Windows.Forms.DataGridTableStyle()
        Me.colProducto = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colCantidad = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colPrecio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colTotal = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.lblTot = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblLeyendaPrecios = New System.Windows.Forms.Label()
        Me.grpPedido.SuspendLayout()
        Me.grpDetalle.SuspendLayout()
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grpPedido
        '
        Me.grpPedido.Controls.Add(Me.lblFinRuta)
        Me.grpPedido.Controls.Add(Me.cboZonaEconomica)
        Me.grpPedido.Controls.Add(Me.Label1)
        Me.grpPedido.Controls.Add(Me.dtpFCompromiso)
        Me.grpPedido.Controls.Add(Me.lblCliente)
        Me.grpPedido.Controls.Add(Me.lblFAlta)
        Me.grpPedido.Controls.Add(Me.lblFCompromiso)
        Me.grpPedido.Controls.Add(Me.lblObservaciones)
        Me.grpPedido.Controls.Add(Me.txtCliente)
        Me.grpPedido.Controls.Add(Me.txtFAlta)
        Me.grpPedido.Controls.Add(Me.txtObservaciones)
        Me.grpPedido.Controls.Add(Me.txtRuta)
        Me.grpPedido.Controls.Add(Me.lblRuta)
        Me.grpPedido.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpPedido.Location = New System.Drawing.Point(3, 0)
        Me.grpPedido.Name = "grpPedido"
        Me.grpPedido.Size = New System.Drawing.Size(420, 279)
        Me.grpPedido.TabIndex = 0
        Me.grpPedido.TabStop = False
        Me.grpPedido.Text = "Pedido"
        '
        'lblFinRuta
        '
        Me.lblFinRuta.AutoSize = True
        Me.lblFinRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinRuta.ForeColor = System.Drawing.Color.Firebrick
        Me.lblFinRuta.Location = New System.Drawing.Point(166, 89)
        Me.lblFinRuta.Name = "lblFinRuta"
        Me.lblFinRuta.Size = New System.Drawing.Size(198, 13)
        Me.lblFinRuta.TabIndex = 108
        Me.lblFinRuta.Text = "Ya se ralizó el fin de día de la ruta."
        Me.lblFinRuta.Visible = False
        '
        'cboZonaEconomica
        '
        Me.cboZonaEconomica.FormattingEnabled = True
        Me.cboZonaEconomica.Location = New System.Drawing.Point(112, 246)
        Me.cboZonaEconomica.Name = "cboZonaEconomica"
        Me.cboZonaEconomica.Size = New System.Drawing.Size(277, 21)
        Me.cboZonaEconomica.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 249)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "&Zona económica:"
        '
        'dtpFCompromiso
        '
        Me.dtpFCompromiso.Location = New System.Drawing.Point(112, 111)
        Me.dtpFCompromiso.Name = "dtpFCompromiso"
        Me.dtpFCompromiso.Size = New System.Drawing.Size(200, 21)
        Me.dtpFCompromiso.TabIndex = 1
        '
        'lblCliente
        '
        Me.lblCliente.AutoSize = True
        Me.lblCliente.Location = New System.Drawing.Point(8, 24)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(44, 13)
        Me.lblCliente.TabIndex = 0
        Me.lblCliente.Text = "Cliente:"
        '
        'lblFAlta
        '
        Me.lblFAlta.AutoSize = True
        Me.lblFAlta.Location = New System.Drawing.Point(8, 54)
        Me.lblFAlta.Name = "lblFAlta"
        Me.lblFAlta.Size = New System.Drawing.Size(76, 13)
        Me.lblFAlta.TabIndex = 0
        Me.lblFAlta.Text = "Fecha de alta:"
        '
        'lblFCompromiso
        '
        Me.lblFCompromiso.AutoSize = True
        Me.lblFCompromiso.Location = New System.Drawing.Point(8, 114)
        Me.lblFCompromiso.Name = "lblFCompromiso"
        Me.lblFCompromiso.Size = New System.Drawing.Size(99, 13)
        Me.lblFCompromiso.TabIndex = 0
        Me.lblFCompromiso.Text = "&Fecha compromiso:"
        '
        'lblObservaciones
        '
        Me.lblObservaciones.AutoSize = True
        Me.lblObservaciones.Location = New System.Drawing.Point(8, 144)
        Me.lblObservaciones.Name = "lblObservaciones"
        Me.lblObservaciones.Size = New System.Drawing.Size(82, 13)
        Me.lblObservaciones.TabIndex = 2
        Me.lblObservaciones.Text = "&Observaciones:"
        '
        'txtCliente
        '
        Me.txtCliente.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCliente.Location = New System.Drawing.Point(112, 21)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(296, 21)
        Me.txtCliente.TabIndex = 1
        Me.txtCliente.TabStop = False
        '
        'txtFAlta
        '
        Me.txtFAlta.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFAlta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFAlta.Location = New System.Drawing.Point(112, 51)
        Me.txtFAlta.Name = "txtFAlta"
        Me.txtFAlta.ReadOnly = True
        Me.txtFAlta.Size = New System.Drawing.Size(200, 21)
        Me.txtFAlta.TabIndex = 1
        Me.txtFAlta.TabStop = False
        '
        'txtObservaciones
        '
        Me.txtObservaciones.BackColor = System.Drawing.Color.White
        Me.txtObservaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtObservaciones.Location = New System.Drawing.Point(112, 141)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservaciones.Size = New System.Drawing.Size(296, 99)
        Me.txtObservaciones.TabIndex = 3
        '
        'txtRuta
        '
        Me.txtRuta.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtRuta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRuta.Location = New System.Drawing.Point(112, 81)
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.ReadOnly = True
        Me.txtRuta.Size = New System.Drawing.Size(48, 21)
        Me.txtRuta.TabIndex = 1
        Me.txtRuta.TabStop = False
        '
        'lblRuta
        '
        Me.lblRuta.AutoSize = True
        Me.lblRuta.Location = New System.Drawing.Point(8, 84)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(34, 13)
        Me.lblRuta.TabIndex = 0
        Me.lblRuta.Text = "Ruta:"
        '
        'grpDetalle
        '
        Me.grpDetalle.Controls.Add(Me.grdDetalle)
        Me.grpDetalle.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpDetalle.Location = New System.Drawing.Point(3, 279)
        Me.grpDetalle.Name = "grpDetalle"
        Me.grpDetalle.Size = New System.Drawing.Size(420, 155)
        Me.grpDetalle.TabIndex = 1
        Me.grpDetalle.TabStop = False
        Me.grpDetalle.Text = "Detalle"
        '
        'grdDetalle
        '
        Me.grdDetalle.AllowSorting = False
        Me.grdDetalle.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.grdDetalle.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grdDetalle.BackgroundColor = System.Drawing.Color.LightGray
        Me.grdDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdDetalle.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdDetalle.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.grdDetalle.CaptionVisible = False
        Me.grdDetalle.DataMember = ""
        Me.grdDetalle.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDetalle.FlatMode = True
        Me.grdDetalle.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdDetalle.ForeColor = System.Drawing.Color.MidnightBlue
        Me.grdDetalle.GridLineColor = System.Drawing.Color.Gainsboro
        Me.grdDetalle.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdDetalle.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.grdDetalle.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdDetalle.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdDetalle.LinkColor = System.Drawing.Color.Teal
        Me.grdDetalle.Location = New System.Drawing.Point(3, 17)
        Me.grdDetalle.Name = "grdDetalle"
        Me.grdDetalle.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.grdDetalle.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.grdDetalle.RowHeaderWidth = 5
        Me.grdDetalle.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.grdDetalle.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdDetalle.Size = New System.Drawing.Size(414, 135)
        Me.grdDetalle.TabIndex = 0
        Me.grdDetalle.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.tbsDetalle})
        '
        'tbsDetalle
        '
        Me.tbsDetalle.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.tbsDetalle.BackColor = System.Drawing.Color.WhiteSmoke
        Me.tbsDetalle.DataGrid = Me.grdDetalle
        Me.tbsDetalle.ForeColor = System.Drawing.Color.MidnightBlue
        Me.tbsDetalle.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colProducto, Me.colCantidad, Me.colPrecio, Me.colTotal})
        Me.tbsDetalle.GridLineColor = System.Drawing.Color.Gainsboro
        Me.tbsDetalle.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.tbsDetalle.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.tbsDetalle.LinkColor = System.Drawing.Color.Teal
        Me.tbsDetalle.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.tbsDetalle.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        '
        'colProducto
        '
        Me.colProducto.Format = ""
        Me.colProducto.FormatInfo = Nothing
        Me.colProducto.HeaderText = "Producto"
        Me.colProducto.MappingName = "Descripcion"
        Me.colProducto.ReadOnly = True
        Me.colProducto.Width = 230
        '
        'colCantidad
        '
        Me.colCantidad.Format = ""
        Me.colCantidad.FormatInfo = Nothing
        Me.colCantidad.HeaderText = "Cantidad"
        Me.colCantidad.MappingName = "Cantidad"
        Me.colCantidad.NullText = "0"
        Me.colCantidad.Width = 50
        '
        'colPrecio
        '
        Me.colPrecio.Format = "###.##"
        Me.colPrecio.FormatInfo = Nothing
        Me.colPrecio.HeaderText = "Precio"
        Me.colPrecio.MappingName = "Precio"
        Me.colPrecio.NullText = "0.00"
        Me.colPrecio.ReadOnly = True
        Me.colPrecio.Width = 50
        '
        'colTotal
        '
        Me.colTotal.Format = "###.##"
        Me.colTotal.FormatInfo = Nothing
        Me.colTotal.HeaderText = "Total"
        Me.colTotal.MappingName = "Total"
        Me.colTotal.NullText = "0.00"
        Me.colTotal.ReadOnly = True
        Me.colTotal.Width = 55
        '
        'btnAceptar
        '
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(115, 483)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(227, 483)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTot
        '
        Me.lblTot.AutoSize = True
        Me.lblTot.Location = New System.Drawing.Point(320, 466)
        Me.lblTot.Name = "lblTot"
        Me.lblTot.Size = New System.Drawing.Size(35, 13)
        Me.lblTot.TabIndex = 4
        Me.lblTot.Text = "Total:"
        '
        'lblTotal
        '
        Me.lblTotal.Location = New System.Drawing.Point(349, 466)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(57, 13)
        Me.lblTotal.TabIndex = 5
        Me.lblTotal.Text = "0.00"
        Me.lblTotal.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLeyendaPrecios
        '
        Me.lblLeyendaPrecios.AutoSize = True
        Me.lblLeyendaPrecios.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLeyendaPrecios.ForeColor = System.Drawing.Color.Red
        Me.lblLeyendaPrecios.Location = New System.Drawing.Point(30, 442)
        Me.lblLeyendaPrecios.Name = "lblLeyendaPrecios"
        Me.lblLeyendaPrecios.Size = New System.Drawing.Size(360, 13)
        Me.lblLeyendaPrecios.TabIndex = 6
        Me.lblLeyendaPrecios.Text = "Los precios solo son informativos. Verifique la zona económica."
        '
        'frmPedidoPortatil
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(426, 519)
        Me.Controls.Add(Me.lblLeyendaPrecios)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lblTot)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.grpDetalle)
        Me.Controls.Add(Me.grpPedido)
        Me.Controls.Add(Me.btnCancelar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmPedidoPortatil"
        Me.Padding = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.grpPedido.ResumeLayout(False)
        Me.grpPedido.PerformLayout()
        Me.grpDetalle.ResumeLayout(False)
        CType(Me.grdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Variables globales"
    Private _Cliente, _Pedido As Integer
    Private dtProducto As New DataTable()
    Private maxProducto As Integer
    Private ZonaEconomica As Short
#End Region
#Region "Rutinas de carga de datos"
    Private Sub CargaDatos(ByVal PermitirCambios As Boolean)
        Dim cmdCallCenter As New SqlCommand("exec spCCDatosPedidoPortatil @Cliente, @Pedido", CnnSigamet)
        Dim daCallCenter As New SqlDataAdapter(cmdCallCenter)
        Dim rdPedido As SqlDataReader
        cmdCallCenter.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        cmdCallCenter.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
        Try
            AbreConexion()
            rdPedido = cmdCallCenter.ExecuteReader
            rdPedido.Read()
            txtCliente.Text = CStr(rdPedido("Nombre"))
            txtRuta.Text = CStr(rdPedido("Ruta"))
            If Not Microsoft.VisualBasic.IsDBNull(rdPedido("PedidoPortatil")) Then
                dtpFCompromiso.MinDate = Now.Date
                _Pedido = CInt(rdPedido("PedidoPortatil"))
                txtFAlta.Text = CStr(rdPedido("FAlta"))
                dtpFCompromiso.Value = CDate(rdPedido("FCompromiso"))
                txtObservaciones.Text = CStr(rdPedido("Observaciones"))
                If dtpFCompromiso.Value.Date < Now.Date AndAlso Not PermitirCambios Then
                    dtpFCompromiso.Enabled = False
                    txtObservaciones.Enabled = False
                    grdDetalle.Enabled = False
                End If
                If Not Microsoft.VisualBasic.IsDBNull(rdPedido("ZonaEconomica")) Then
                    ZonaEconomica = CInt(rdPedido("ZonaEconomica"))
                End If
                cmdCallCenter.CommandText = "exec spCCDetallePedidoPortatil @Pedido,@ZonaEconomica"
                cmdCallCenter.Parameters("@Pedido").Value = _Pedido
                cmdCallCenter.Parameters.Add("@ZonaEconomica", SqlDbType.TinyInt).Value = ZonaEconomica
                rdPedido.Close()
                dtProducto.Clear()
                daCallCenter.Fill(dtProducto)
                grdDetalle.DataSource = dtProducto
                maxProducto = dtProducto.Rows.Count
            Else
                dtpFCompromiso.MinDate = Now.Date
                txtFAlta.Text = Now.ToLongDateString
            End If
            'LUSATE Consultar precios de productos por zona económica

            Dim Row As DataRow
            lblTotal.Text = "0.00"
            For Each Row In dtProducto.Rows
                Row("Total") = CType(Row("Cantidad"), Decimal) * CType(Row("Precio"), Decimal)
                lblTotal.Text = CType(lblTotal.Text, Decimal) + CType(Row("Total"), Decimal)
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            cboZonaEconomica.SelectedValue = ZonaEconomica
        End Try
    End Sub
#End Region

    Private Sub grdDetalle_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDetalle.CurrentCellChanged
        On Error Resume Next
        If grdDetalle.CurrentCell.RowNumber < maxProducto Then
            grdDetalle.Select(grdDetalle.CurrentCell.RowNumber)
        Else
            dtProducto.AcceptChanges()
            grdDetalle.DataSource = dtProducto
        End If

        'LUSATE Consultar precios de productos por zona económica
        lblTotal.Text = "0.00"
        Dim Row As DataRow
        For Each Row In dtProducto.Rows
            Row("Total") = "0.00"
            Row("Total") = CType(Row("Cantidad"), Decimal) * CType(Row("Precio"), Decimal)
            lblTotal.Text = CType(lblTotal.Text, Decimal) + CType(Row("Total"), Decimal)
        Next
    End Sub


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim Row As DataRow
        For Each Row In dtProducto.Rows
            If Microsoft.VisualBasic.IsDBNull(Row("Cantidad")) Then
                Row("Cantidad") = 0
            End If
        Next
        dtProducto.AcceptChanges()
        If Convert.ToInt32(dtProducto.Compute("Sum(Cantidad)", "")) > 0 Then
            Dim cmdCallCenter As New SqlCommand("spCCPedidoPortatilAlta", CnnSigamet)
            Dim Pedido As Integer
            cmdCallCenter.CommandType = CommandType.StoredProcedure
            cmdCallCenter.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
            cmdCallCenter.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
            cmdCallCenter.Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = CInt(txtRuta.Text)
            cmdCallCenter.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = Trim(txtObservaciones.Text)
            cmdCallCenter.Parameters.Add("@Pedido", SqlDbType.Int).Direction = ParameterDirection.Output
            cmdCallCenter.Parameters.Add("@ZonaEconomica", SqlDbType.TinyInt).Value = cboZonaEconomica.SelectedValue
            Try
                AbreConexion()
                cmdCallCenter.Transaction = CnnSigamet.BeginTransaction()
                If _Pedido = 0 Then
                    cmdCallCenter.ExecuteNonQuery()
                    If Not Microsoft.VisualBasic.IsDBNull(cmdCallCenter.Parameters("@Pedido").Value) AndAlso Not cmdCallCenter.Parameters("@Pedido").Value Is Nothing Then
                        Pedido = CInt(cmdCallCenter.Parameters("@Pedido").Value)
                    End If
                Else
                    Pedido = _Pedido
                    cmdCallCenter.Parameters.Clear()
                    cmdCallCenter.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                    cmdCallCenter.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = dtpFCompromiso.Value.Date
                    cmdCallCenter.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = Trim(txtObservaciones.Text)
                    cmdCallCenter.Parameters.Add("@ZonaEconomica", SqlDbType.TinyInt).Value = cboZonaEconomica.SelectedValue
                    cmdCallCenter.CommandType = CommandType.Text
                    cmdCallCenter.CommandText = "exec spCCPedidoPortatilModificacion @Pedido, @FCompromiso, @Observaciones,@ZonaEconomica"
                    cmdCallCenter.ExecuteNonQuery()
                    cmdCallCenter.CommandText = "Delete Pedidoportatilproducto where PedidoPortatil = @Pedido"
                    cmdCallCenter.ExecuteNonQuery()
                End If

                cmdCallCenter.CommandText = "spCCPedidoPortatilProductoAlta"
                cmdCallCenter.Parameters.Clear()
                cmdCallCenter.Parameters.Add("@Pedido", SqlDbType.Int).Value = Pedido
                cmdCallCenter.Parameters.Add("@Producto", SqlDbType.SmallInt)
                cmdCallCenter.Parameters.Add("@Cantidad", SqlDbType.TinyInt)
                cmdCallCenter.CommandType = CommandType.StoredProcedure
                For Each Row In dtProducto.Rows
                    If Not Microsoft.VisualBasic.IsDBNull(Row("Cantidad")) AndAlso CInt(Row("Cantidad")) > 0 Then
                        cmdCallCenter.Parameters("@Producto").Value = CInt(Row("Producto"))
                        cmdCallCenter.Parameters("@Cantidad").Value = CInt(Row("Cantidad"))
                        cmdCallCenter.ExecuteNonQuery()
                    End If
                Next
                cmdCallCenter.Transaction.Commit()
                Me.Close()
                Me.Dispose()
            Catch ex As Exception
                cmdCallCenter.Transaction.Rollback()
                MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
            End Try
        Else
            MessageBox.Show("No ha especificado algún producto.", Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            grdDetalle.Focus()
        End If
    End Sub

    Private Sub grdDetalle_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles grdDetalle.KeyDown
    End Sub

    Private Sub grdDetalle_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles grdDetalle.PreviewKeyDown
        If e.KeyCode = 46 Then
            grdDetalle.ReadOnly = True
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey( _
               ByRef msg As Message, _
               ByVal keyData As Keys) As Boolean

        ' Si el control DataGrid no tiene el foco, abandonamos la función.
        '
        If (Not (m_IsDataGridFocused)) Then
            Return False
        End If


        ' Si se ha pulsado la tecla Supr, abandonamos la función.
        If (keyData = Keys.Delete) Then
            Return True
        Else
            grdDetalle.ReadOnly = False
            Return False
        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    Private m_IsDataGridFocused As Boolean

    Private Sub grdDetalle_Enter(ByVal sender As Object, ByVal e As EventArgs) Handles grdDetalle.Enter

        m_IsDataGridFocused = True

    End Sub

    Private Sub grdDetalle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles grdDetalle.MouseClick

        m_IsDataGridFocused = True

    End Sub

    Private Sub grdDetalle_MousseDown(ByVal sender As Object, ByVal e As EventArgs) Handles grdDetalle.MouseDown

        m_IsDataGridFocused = True

    End Sub
    Private Sub grdDetalle_MoussUp(ByVal sender As Object, ByVal e As EventArgs) Handles grdDetalle.MouseUp

        m_IsDataGridFocused = True

    End Sub

    Private Sub grdDetalle_MoussDoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles grdDetalle.MouseDoubleClick


        m_IsDataGridFocused = True

    End Sub

    Private Sub grdDetalle_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles grdDetalle.Leave

        m_IsDataGridFocused = False

    End Sub


    Private Sub CargaZonasEconomicas()
        Dim daZE As New SqlDataAdapter("spCCConsultaZonaEconomica", CnnSigamet)
        Try
            AbreConexion()
            Dim dtZE As New DataTable
            daZE.Fill(dtZE)
            cboZonaEconomica.DataSource = dtZE
            cboZonaEconomica.DisplayMember = "Descripcion"
            cboZonaEconomica.ValueMember = "ZonaEconomica"

        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
        End Try
    End Sub

    Private Sub cboZonaEconomica_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboZonaEconomica.SelectedIndexChanged
        'LUSATE Consultar precios de productos por zona económica
        If cboZonaEconomica.SelectedValue <> -1 Then
            'If CDec(lblTotal.Text) <> 0 Or _Pedido <> 0 Then
            Dim cmdProductos As New SqlCommand("spCCConsultaProductosPtl", CnnSigamet)
            cmdProductos.CommandType = CommandType.StoredProcedure
            cmdProductos.Parameters.Add("@ZonaEconomica", SqlDbType.TinyInt).Value = cboZonaEconomica.SelectedValue

            Try
                AbreConexion()
                Dim dr As SqlDataReader
                dr = cmdProductos.ExecuteReader()
                lblTotal.Text = "0.00"
                While dr.Read()
                    Dim Row As DataRow
                    For Each Row In dtProducto.Rows
                        If Row("Producto") = dr("Producto") Then
                            Row("Total") = "0.00"
                            Row("Precio") = dr("Precio")
                            If Not IsDBNull(Row("Cantidad")) Then
                                Row("Total") = CType(Row("Cantidad"), Decimal) * CType(Row("Precio"), Decimal)
                                lblTotal.Text = CType(lblTotal.Text, Decimal) + CType(Row("Total"), Decimal)
                            End If
                        End If
                    Next
                End While
            Catch ex As Exception
                MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
            End Try


            'Else
            '    Dim daCallCenter As New SqlDataAdapter("spCCConsultaProductosPtl @ZonaEconomica = " & cboZonaEconomica.SelectedValue, CnnSigamet)
            '    Try
            '        dtProducto.Clear()
            '        grdDetalle.DataSource = Nothing
            '        daCallCenter.Fill(dtProducto)
            '        grdDetalle.DataSource = dtProducto
            '        maxProducto = dtProducto.Rows.Count
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    End Try
            'End If
        End If
    End Sub

   
    Private Sub frmPedidoPortatil_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        ConsultaFinDeDia()
    End Sub
    Private Sub ConsultaFinDeDia()
        Dim cmdSelect As New SqlClient.SqlCommand()
        cmdSelect.CommandText = "spConsultaFinDeDia"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Connection = CnnSigamet
        cmdSelect.Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = CType(txtRuta.Text, Integer)

        Dim drFinDia As SqlClient.SqlDataReader

        Try
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If

            drFinDia = cmdSelect.ExecuteReader()
            drFinDia.Read()
            If CType(drFinDia("Llamada"), Integer) <> 0 Then
                lblFinRuta.Visible = True
            End If


        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error no." & CStr(ex.Number) & Chr(13) & _
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error." & Chr(13) & _
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()

        End Try
    End Sub

    Private Sub grdDetalle_Navigate(sender As System.Object, ne As System.Windows.Forms.NavigateEventArgs) Handles grdDetalle.Navigate

    End Sub

    Private Sub dtpFCompromiso_ValueChanged(sender As System.Object, e As System.EventArgs) Handles dtpFCompromiso.ValueChanged

    End Sub
End Class
