Imports System.Web.Services
Public Class frmListaPedidosWeb
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "
    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
        RemoveHandler _timer.TimerStop, AddressOf StopTimer
        _timer.StopTimer()
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents VwGrd1 As CustGrd.vwGrd
    Friend WithEvents WebClientData1 As DLCPedidosWeb.WebClientData
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents btnActualizar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnConfirmar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnConsultar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents btnSeguimiento As System.Windows.Forms.ToolBarButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cboFiltro As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblMensajeTiempoActualización As System.Windows.Forms.Label
    Friend WithEvents Cronograph1 As Timer.cronograph
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboStatus As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmListaPedidosWeb))
        Me.VwGrd1 = New CustGrd.vwGrd()
        Me.WebClientData1 = New DLCPedidosWeb.WebClientData()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.btnConfirmar = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelar = New System.Windows.Forms.ToolBarButton()
        Me.btnConsultar = New System.Windows.Forms.ToolBarButton()
        Me.btnSeguimiento = New System.Windows.Forms.ToolBarButton()
        Me.btnActualizar = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboStatus = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboFiltro = New System.Windows.Forms.ComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblMensajeTiempoActualización = New System.Windows.Forms.Label()
        Me.Cronograph1 = New Timer.cronograph()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'VwGrd1
        '
        Me.VwGrd1.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.VwGrd1.BackColor = System.Drawing.Color.Honeydew
        Me.VwGrd1.ColumnMargin = 30
        Me.VwGrd1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VwGrd1.FullRowSelect = True
        Me.VwGrd1.GridLines = True
        Me.VwGrd1.Location = New System.Drawing.Point(0, 72)
        Me.VwGrd1.Name = "VwGrd1"
        Me.VwGrd1.Size = New System.Drawing.Size(792, 340)
        Me.VwGrd1.TabIndex = 0
        Me.VwGrd1.View = System.Windows.Forms.View.Details
        '
        'WebClientData1
        '
        Me.WebClientData1.AnioRegistro = CType(0, Short)
        Me.WebClientData1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.WebClientData1.Celula = ""
        Me.WebClientData1.Cliente = ""
        Me.WebClientData1.CorreoElectronico = ""
        Me.WebClientData1.Direccion = ""
        Me.WebClientData1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.WebClientData1.FechaCompromiso = New Date(2006, 4, 29, 11, 44, 14, 532)
        Me.WebClientData1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WebClientData1.Lada = ""
        Me.WebClientData1.Location = New System.Drawing.Point(0, 413)
        Me.WebClientData1.Name = "WebClientData1"
        Me.WebClientData1.Nombre = ""
        Me.WebClientData1.Observaciones = ""
        Me.WebClientData1.Registro = 0
        Me.WebClientData1.Ruta = ""
        Me.WebClientData1.Size = New System.Drawing.Size(792, 160)
        Me.WebClientData1.TabIndex = 1
        Me.WebClientData1.Telefono1 = ""
        Me.WebClientData1.Telefono2 = ""
        '
        'ToolBar1
        '
        Me.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat
        Me.ToolBar1.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnConfirmar, Me.btnCancelar, Me.btnConsultar, Me.btnSeguimiento, Me.btnActualizar, Me.btnCerrar})
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.ImageList = Me.ImageList1
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(792, 39)
        Me.ToolBar1.TabIndex = 2
        '
        'btnConfirmar
        '
        Me.btnConfirmar.ImageIndex = 0
        Me.btnConfirmar.Tag = "Confirmar"
        Me.btnConfirmar.Text = "Confirmar"
        Me.btnConfirmar.ToolTipText = "Confirmar el pedido del cliente"
        '
        'btnCancelar
        '
        Me.btnCancelar.ImageIndex = 1
        Me.btnCancelar.Tag = "Cancelar"
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.ToolTipText = "Cancela el servicio solicitado después de la confirmación del cliente"
        '
        'btnConsultar
        '
        Me.btnConsultar.ImageIndex = 2
        Me.btnConsultar.Tag = "Consultar"
        Me.btnConsultar.Text = "Consultar"
        Me.btnConsultar.ToolTipText = "Consultar los datos completos del cliente seleccionado"
        '
        'btnSeguimiento
        '
        Me.btnSeguimiento.ImageIndex = 5
        Me.btnSeguimiento.Tag = "Seguimiento"
        Me.btnSeguimiento.Text = "Seguimiento"
        Me.btnSeguimiento.ToolTipText = "Consultar el estado del pedido"
        '
        'btnActualizar
        '
        Me.btnActualizar.ImageIndex = 3
        Me.btnActualizar.Tag = "Actualizar"
        Me.btnActualizar.Text = "Actualizar"
        Me.btnActualizar.ToolTipText = "Actualizar la vista actual"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 4
        Me.btnCerrar.Tag = "Cerrar"
        Me.btnCerrar.Text = "Cerrar"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'ImageList2
        '
        Me.ImageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList2.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        '
        'Panel2
        '
        Me.Panel2.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label2, Me.cboStatus, Me.Label1, Me.cboFiltro})
        Me.Panel2.Location = New System.Drawing.Point(396, 40)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(392, 28)
        Me.Panel2.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Filtrar:"
        Me.Label2.Visible = False
        '
        'cboStatus
        '
        Me.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatus.Items.AddRange(New Object() {"ACTIVO", "MIGRADO", "CANCELADO"})
        Me.cboStatus.Location = New System.Drawing.Point(56, 3)
        Me.cboStatus.Name = "cboStatus"
        Me.cboStatus.Size = New System.Drawing.Size(136, 21)
        Me.cboStatus.TabIndex = 3
        Me.cboStatus.Tag = "Filtro por estatus del registro"
        Me.cboStatus.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(200, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Mostrar:"
        '
        'cboFiltro
        '
        Me.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFiltro.Items.AddRange(New Object() {"Todos", "Solo clientes nuevos", "Solo clientes existentes"})
        Me.cboFiltro.Location = New System.Drawing.Point(252, 3)
        Me.cboFiltro.Name = "cboFiltro"
        Me.cboFiltro.Size = New System.Drawing.Size(136, 21)
        Me.cboFiltro.TabIndex = 1
        Me.cboFiltro.Tag = "Filtro por tipo de cliente"
        '
        'Panel1
        '
        Me.Panel1.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.Panel1.BackColor = System.Drawing.Color.LightGray
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblMensajeTiempoActualización, Me.Cronograph1})
        Me.Panel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(396, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(392, 32)
        Me.Panel1.TabIndex = 5
        '
        'lblMensajeTiempoActualización
        '
        Me.lblMensajeTiempoActualización.AutoSize = True
        Me.lblMensajeTiempoActualización.BackColor = System.Drawing.SystemColors.Control
        Me.lblMensajeTiempoActualización.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMensajeTiempoActualización.Location = New System.Drawing.Point(6, 7)
        Me.lblMensajeTiempoActualización.Name = "lblMensajeTiempoActualización"
        Me.lblMensajeTiempoActualización.Size = New System.Drawing.Size(0, 16)
        Me.lblMensajeTiempoActualización.TabIndex = 4
        Me.lblMensajeTiempoActualización.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cronograph1
        '
        Me.Cronograph1.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.Cronograph1.BackColor = System.Drawing.SystemColors.Control
        Me.Cronograph1.BTITimer = Nothing
        Me.Cronograph1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cronograph1.Location = New System.Drawing.Point(252, 3)
        Me.Cronograph1.Name = "Cronograph1"
        Me.Cronograph1.Size = New System.Drawing.Size(132, 24)
        Me.Cronograph1.TabIndex = 3
        '
        'frmListaPedidosWeb
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.LightGray
        Me.ClientSize = New System.Drawing.Size(792, 573)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel2, Me.Panel1, Me.ToolBar1, Me.WebClientData1, Me.VwGrd1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmListaPedidosWeb"
        Me.Text = "Lista de pedidos WEB"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        currentHeight = Me.Height
        vwgrdCurrentHeight = Me.VwGrd1.Height
        _timer = New CTimer.Timer()
        _timer.WaitPeriod = GLOBAL_TiempoActualizacionConsultaPedidos
        lblMensajeTiempoActualización.Text = "Esta vista se actualiza cada " & _
            (GLOBAL_TiempoActualizacionConsultaPedidos / 60).ToString & _
            " minutos"
        Cronograph1.BTITimer = _timer

        _llamada = New CallCenterCControls.LlamadaDAC(CnnSigamet)

        cboFiltro.SelectedIndex = 0
        cboStatus.SelectedIndex = 0

        AddHandler cboFiltro.SelectedIndexChanged, AddressOf cboFiltro_SelectedIndexChanged
        AddHandler cboStatus.SelectedIndexChanged, AddressOf cboStatus_SelectedIndexChanged
    End Sub

#Region "Private members"
    Private data As DLCPedidosWeb.ConsultaPedidosYClientesWEB
    Private currentHeight As Integer
    Private vwgrdCurrentHeight As Integer

    Private WithEvents _timer As CTimer.Timer

    Private _llamada As CallCenterCControls.LlamadaDAC
#End Region

#Region "Toolbar handling"
    Private Sub ToolBar1_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles ToolBar1.ButtonClick
        Select Case e.Button.Tag.ToString()
            Case "Actualizar"
                Actualizar_Click(sender, e)
            Case "Confirmar"
                WebClientData1_Click(sender, e)
            Case "Cancelar"
                Cancelar(sender, e)
            Case "Consultar"
                ButtonConsultarClick(sender, e)
            Case "Seguimiento"
                Seguimiento()
            Case "Cerrar"
                Me.Close()
        End Select
    End Sub

    Private Sub ButtonBuscarClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles WebClientData1.ButtonBuscarClick
        Dim frmBuscar As New SigaMetClasses.BusquedaCliente()
        If frmBuscar.ShowDialog() <> DialogResult.Cancel Then
            cargaClienteBD(frmBuscar.Cliente)
        End If
    End Sub

    Private Sub ButtonConsultarClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles WebClientData1.ButtonConsultarClick
        Dim cliente As Integer
        cliente = CType(Val(WebClientData1.Cliente), Integer)
        If cliente <> 0 Then
            Dim frmConsultar As New SigaMetClasses.ModificaCliente(cliente, "", False)
            frmConsultar.ShowDialog()
        End If
    End Sub

    Private Sub Seguimiento()
        Dim frmSeguimiento As New DLCPedidosWeb.ConsultaDetallesPedido(data)
        If frmSeguimiento.ShowDialog() = DialogResult.OK Then
            'data.SelectedRow(frmSeguimiento.AnioRec, frmSeguimiento.Rec)
            Actualizar_Click(Nothing, Nothing)
            searchItemByRecNo(frmSeguimiento.AnioRec, frmSeguimiento.Rec)
        End If
    End Sub
#End Region

#Region "Data loading methods"
    Private Sub Actualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        data = New DLCPedidosWeb.ConsultaPedidosYClientesWEB(GLOBAL_Usuario, CnnSigamet, GLOBAL_ConsultaPedidosWebURL)
        _timer.StartTimer()
        data.ConsultaDatos()
        gridDataBinding(data.ConsultaLocalDatos(cboFiltro.SelectedIndex, cboStatus.SelectedIndex))
    End Sub

    Private Sub gridDataBinding(ByVal DTSource As DataTable)
        VwGrd1.Columns.Clear()
        VwGrd1.DataSource = Nothing
        VwGrd1.DataSource = DTSource
        VwGrd1.AutoColumnHeader()
        VwGrd1.DataAdd()
        TableFormatter()
    End Sub

    Private Sub StopTimer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _timer.TimerStop
        _timer.ResetTimer()
        Actualizar_Click(Nothing, Nothing)
    End Sub

    Private Sub cargaClienteBD(ByVal Cliente As Integer)
        Dim dt As DataTable = data.ConsultaCliente(Cliente)
        webDataBind(WebClientData1.AnioRegistro, WebClientData1.Registro, _
            CType(dt.Rows(0).Item("Celula"), String), CType(dt.Rows(0).Item("Ruta"), String), _
            CType(dt.Rows(0).Item("Cliente"), Integer), CType(dt.Rows(0).Item("Nombre"), String), _
            CType(dt.Rows(0).Item("DireccionCompleta"), String), CType(dt.Rows(0).Item("Lada"), String), _
            CType(dt.Rows(0).Item("Telefono"), String), String.Empty, _
            WebClientData1.FechaCompromiso, WebClientData1.Observaciones, String.Empty)
    End Sub

    Private Sub webDataBind(ByVal AnioRegistro As Short, ByVal Registro As Integer, _
            ByVal Celula As String, ByVal Ruta As String, ByVal Cliente As Integer, _
            ByVal Nombre As String, ByVal Domicilio As String, ByVal Lada As String, _
            ByVal Telefono1 As String, ByVal Telefono2 As String, _
            ByVal FechaCompromiso As DateTime, ByVal Observaciones As String, ByVal CorreoElectronico As String)
        WebClientData1.AnioRegistro = AnioRegistro
        WebClientData1.Registro = Registro
        WebClientData1.Celula = Celula
        WebClientData1.Ruta = Ruta
        WebClientData1.Cliente = Cliente.ToString
        WebClientData1.Nombre = Nombre.ToString
        WebClientData1.Direccion = Domicilio
        WebClientData1.Lada = Lada
        WebClientData1.Telefono1 = Telefono1
        WebClientData1.Telefono2 = Telefono2
        WebClientData1.FechaCompromiso = FechaCompromiso
        WebClientData1.Observaciones = Observaciones
        WebClientData1.CorreoElectronico = CorreoElectronico
    End Sub
#End Region

#Region "Misc methods"
    Private Sub frmListaPedidosWeb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Actualizar_Click(sender, e)
    End Sub

    Public Sub TableFormatter()
        Dim chDelete As ColumnHeader = VwGrd1.Columns(5)
        VwGrd1.Columns.Remove(chDelete)
        chDelete = VwGrd1.Columns(5)
        VwGrd1.Columns.Remove(chDelete)
        chDelete = VwGrd1.Columns(5)
        VwGrd1.Columns.Remove(chDelete)
        chDelete = VwGrd1.Columns(5)
        VwGrd1.Columns.Remove(chDelete)

        'Dim lvi As ListViewItem
        'For Each lvi In VwGrd1.Items
        '    If CType(lvi.SubItems(2).Text, Integer) = 0 Then
        '        lvi.ImageIndex = 1
        '    Else
        '        lvi.ImageIndex = 0
        '    End If
        'Next
    End Sub

    Private Sub frmListaPedidosWeb_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If currentHeight <> 0 Then
            VwGrd1.Height = vwgrdCurrentHeight + (Me.Height - currentHeight) - 5
        End If
    End Sub
#End Region

#Region "Grid handling"
    Private Sub VwGrd1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles VwGrd1.SelectedIndexChanged
        Dim lvi As ListViewItem
        For Each lvi In VwGrd1.Items
            If lvi.Selected Then
                Dim dr As DataRow = data.SelectedRow(CType(lvi.SubItems(0).Text, Short), _
                    CType(lvi.SubItems(1).Text, Integer))
                If (CType(dr("Cliente"), Integer) <> 0) Then
                    Dim dt As DataTable = data.ConsultaCliente(CType(dr("Cliente"), Integer))
                    webDataBind(CType(dr("AñoRegistro"), Short), CType(dr("Registro"), Integer), _
                        CType(dt.Rows(0).Item("Celula"), String), CType(dt.Rows(0).Item("Ruta"), String), _
                        CType(dt.Rows(0).Item("Cliente"), Integer), CType(dt.Rows(0).Item("Nombre"), String), _
                        CType(dt.Rows(0).Item("DireccionCompleta"), String), CType(dt.Rows(0).Item("Lada"), String), _
                        CType(dr("Teléfono"), String), _
                        CType(dt.Rows(0).Item("Telefono"), String), CType(dr("Fecha Compromiso"), Date), _
                        CType(dr("Observaciones"), String), String.Empty)
                Else
                    webDataBind(CType(dr("AñoRegistro"), Short), CType(dr("Registro"), Integer), _
                        String.Empty, String.Empty, CType(dr("Cliente"), Integer), _
                        CType(dr("Nombre"), String), CType(dr("Domicilio"), String), _
                        CType(dr("Lada"), String), CType(dr("Teléfono"), String), _
                        String.Empty, CType(dr("Fecha Compromiso"), Date), _
                        CType(dr("Observaciones"), String), CType(dr("Correo Electrónico"), String))
                End If
                Exit Sub
            End If
        Next
    End Sub

    Private Sub searchItemByRecNo(ByVal ARec As Short, ByVal Rec As Integer)
        Dim lvi As ListViewItem
        For Each lvi In VwGrd1.Items
            If CType(lvi.SubItems(0).Text, Short) = ARec AndAlso _
                Rec = CType(lvi.SubItems(1).Text, Integer) Then
                lvi.Selected = True
                VwGrd1_SelectedIndexChanged(Nothing, Nothing)
            End If
        Next
    End Sub
#End Region

#Region "Request confirm methods"
    Private Sub WebClientData1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WebClientData1.Click
        If MessageBox.Show("¿Desea confirmar el alta de este pedido?", "Pedido WEB", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question) = DialogResult.Yes Then

            If Not validaFCompromiso() Then
                Exit Sub
            End If

            If CType(WebClientData1.Cliente, Integer) = 0 Then
                If MessageBox.Show("Este pedido corresponde a un cliente nuevo" & vbCrLf & _
                "Debe darlo de alta para confirmar el pedido" & vbCrLf & _
                "¿Desea continuar?", _
                "Pedido WEB", _
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Dim frmCC As New frmCallCenter(WebClientData1.Nombre, _
                        WebClientData1.Direccion, _
                        WebClientData1.Lada, _
                        WebClientData1.Telefono1, _
                        WebClientData1.CorreoElectronico, _
                        WebClientData1.FechaCompromiso, _
                        WebClientData1.Observaciones)
                    If frmCC.ShowDialog() = DialogResult.OK Then
                        WebClientData1.Cliente = frmCC.Cliente.ToString
                        WebClientData1.Celula = frmCC.Celula.ToString
                        WebClientData1.Ruta = frmCC.Ruta.ToString
                    Else
                        Exit Sub
                    End If
                Else
                    Exit Sub
                End If
            End If
            Try
                If data.pedidoAlta(WebClientData1.AnioRegistro, _
                    WebClientData1.Registro, _
                    CType(WebClientData1.Cliente, Integer), _
                    CType(WebClientData1.FechaCompromiso, Date), _
                    WebClientData1.Observaciones, _
                    CType(WebClientData1.Celula, Byte), _
                    CType(WebClientData1.Ruta, Short), _
                    GLOBAL_Usuario) Then
                    MessageBox.Show("Se registró correctamente el pedido", _
                        "Pedido WEB", _
                        MessageBoxButtons.OK, _
                        MessageBoxIcon.Information)
                    gridDataBinding(data.ConsultaLocalDatos(cboFiltro.SelectedIndex, cboStatus.SelectedIndex))
                    WebClientData1.ResetData()
                End If
            Catch Ex As Exception
                MessageBox.Show(Ex.ToString, "Pedido WEB", MessageBoxButtons.OK)
            End Try
        End If
    End Sub

    Private Sub Cancelar(ByVal sender As Object, ByVal e As System.EventArgs) Handles WebClientData1.ButtonCancelarClick
        If MessageBox.Show("¿Desea confirmar la cancelación de este pedido?", "Pedido WEB", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question) = DialogResult.Yes Then
            If data.pedidoCancela(WebClientData1.AnioRegistro, _
                WebClientData1.Registro, _
                GLOBAL_Usuario) Then
                LlamadaCancelacion(CType(WebClientData1.Cliente, Integer), _
                WebClientData1.Telefono1, _
                WebClientData1.Celula, _
                WebClientData1.Nombre)

                MessageBox.Show("Pedido cancelado", "Pedido WEB", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Information)
                gridDataBinding(data.ConsultaLocalDatos(cboFiltro.SelectedIndex, cboStatus.SelectedIndex))
            End If
        End If
    End Sub

    Private Sub LlamadaCancelacion(ByVal Cliente As Integer, ByVal Telefono As String, ByVal Celula As String, _
        ByVal Nombre As String)
        Dim _celula As Byte

        If Cliente = 0 Then
            _celula = 0
        Else
            _celula = CType(Celula, Byte)
        End If

        _llamada.LlamadaAlta(Cliente, GLOBAL_ObservacionesCancelacion, Telefono, _
            GLOBAL_MotivoLlamadaCancelacionWeb, _celula, 0, 0, 0, 0, Nombre)
    End Sub

    Private Function validaFCompromiso() As Boolean
        If WebClientData1.FechaCompromiso < Date.Today.Date OrElse _
            WebClientData1.FechaCompromiso > Date.Today.AddMonths(4) Then
            MessageBox.Show("La fecha compromiso no puede ser menor al día de hoy" & vbCrLf & _
                "o mayor a 4 meses", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        Else
            Return True
        End If
    End Function


#End Region

    Private Sub cboFiltro_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        gridDataBinding(data.ConsultaLocalDatos(cboFiltro.SelectedIndex, cboStatus.SelectedIndex))
    End Sub

    Private Sub cboStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        gridDataBinding(data.ConsultaLocalDatos(cboFiltro.SelectedIndex, cboStatus.SelectedIndex))
    End Sub

End Class
