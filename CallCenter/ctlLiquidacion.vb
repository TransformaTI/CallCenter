Public Class ctlLiquidacion
    Inherits System.Windows.Forms.UserControl
    Private _Pedido, _Cliente As Integer
    Private _Nombre As String
    Private _Litros, _Precio, _Importe As Decimal
    Public Event HaCambiado()
    Private _DatosCargados As Boolean = False

#Region "Propiedades"

    Public ReadOnly Property Litros() As Decimal
        Get
            Return _Litros
        End Get
    End Property

    Public ReadOnly Property Importe() As Decimal
        Get
            Return _Importe
        End Get
    End Property


#End Region

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents lblPedido As System.Windows.Forms.Label
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblNombre As System.Windows.Forms.Label
    Friend WithEvents txtLitros As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtPrecio As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents lblImporte As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.lblPedido = New System.Windows.Forms.Label()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblNombre = New System.Windows.Forms.Label()
        Me.txtLitros = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtPrecio = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.lblImporte = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblPedido
        '
        Me.lblPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPedido.Name = "lblPedido"
        Me.lblPedido.Size = New System.Drawing.Size(64, 21)
        Me.lblPedido.TabIndex = 0
        Me.lblPedido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCliente
        '
        Me.lblCliente.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCliente.Location = New System.Drawing.Point(64, 0)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(80, 21)
        Me.lblCliente.TabIndex = 1
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNombre
        '
        Me.lblNombre.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblNombre.Location = New System.Drawing.Point(144, 0)
        Me.lblNombre.Name = "lblNombre"
        Me.lblNombre.Size = New System.Drawing.Size(256, 21)
        Me.lblNombre.TabIndex = 2
        Me.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtLitros
        '
        Me.txtLitros.Location = New System.Drawing.Point(400, 0)
        Me.txtLitros.Name = "txtLitros"
        Me.txtLitros.Size = New System.Drawing.Size(48, 21)
        Me.txtLitros.TabIndex = 3
        Me.txtLitros.Text = ""
        '
        'txtPrecio
        '
        Me.txtPrecio.Location = New System.Drawing.Point(448, 0)
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.Size = New System.Drawing.Size(48, 21)
        Me.txtPrecio.TabIndex = 4
        Me.txtPrecio.Text = ""
        '
        'lblImporte
        '
        Me.lblImporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblImporte.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImporte.Location = New System.Drawing.Point(496, 0)
        Me.lblImporte.Name = "lblImporte"
        Me.lblImporte.Size = New System.Drawing.Size(88, 21)
        Me.lblImporte.TabIndex = 5
        Me.lblImporte.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ctlLiquidacion
        '
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblImporte, Me.txtPrecio, Me.txtLitros, Me.lblNombre, Me.lblCliente, Me.lblPedido})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "ctlLiquidacion"
        Me.Size = New System.Drawing.Size(608, 21)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal Pedido As Integer, _
                   ByVal Cliente As Integer, _
                   ByVal Nombre As String, _
                   ByVal Litros As Decimal, _
                   ByVal Precio As Decimal)
        MyBase.New()
        InitializeComponent()

        _Pedido = Pedido
        _Cliente = Cliente
        _Nombre = Nombre
        _Litros = Litros
        _Precio = Precio

        lblPedido.Text = _Pedido.ToString
        lblCliente.Text = _Cliente.ToString
        lblNombre.Text = _Nombre.Trim
        txtLitros.Text = _Litros.ToString
        txtPrecio.Text = _Precio.ToString

        _Importe = _Litros * _Precio
        lblImporte.Text = _Importe.ToString("N")

        _DatosCargados = True

    End Sub


    Private Sub ctlLiquidacion_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Enter
        Me.BackColor = Color.RoyalBlue
    End Sub

    Private Sub ctlLiquidacion_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Leave
        Me.BackColor = Color.WhiteSmoke
    End Sub

#Region "Control de las etiquetas"
    Private Sub EtiquetasMouseEnter(ByVal sender As Object, _
                                        ByVal e As System.EventArgs) Handles _
                                                lblPedido.MouseEnter, _
                                                lblCliente.MouseEnter, _
                                                lblNombre.MouseEnter, _
                                                lblImporte.MouseEnter
        CType(sender, Label).ForeColor = Color.Red
    End Sub

    Private Sub EtiquetasMouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblPedido.MouseLeave, lblCliente.MouseLeave, lblNombre.MouseLeave, lblImporte.MouseLeave
        CType(sender, Label).ForeColor = Color.Black
    End Sub

    Private Sub EtiquetasDoubleClick(ByVal sender As Object, _
                                    ByVal e As System.EventArgs) Handles _
                                            lblPedido.DoubleClick, _
                                            lblCliente.DoubleClick, _
                                            lblNombre.DoubleClick, _
                                            lblImporte.DoubleClick

        Cursor = Cursors.WaitCursor
        Dim oConsultaCliente As New SigaMetClasses.frmConsultaCliente(_Cliente)
        oConsultaCliente.ShowDialog()
        Cursor = Cursors.Default

    End Sub

    Private Sub EtiquetasClick(ByVal sender As Object, _
                               ByVal e As System.EventArgs) Handles _
                                        lblPedido.Click, _
                                        lblCliente.Click, _
                                        lblNombre.Click, _
                                        lblImporte.Click
        Me.Focus()

    End Sub
#End Region

    Private Sub TextHasChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLitros.TextChanged, txtPrecio.TextChanged
        If Not _DatosCargados Then Exit Sub

        If txtLitros.Text.Trim = "" Then
            _Litros = 0
        Else
            _Litros = CType(txtLitros.Text, Decimal)
        End If

        If txtPrecio.Text.Trim = "" Then
            _Precio = 0
        Else
            _Precio = CType(txtPrecio.Text, Decimal)
        End If

        _Importe = _Litros * _Precio

        lblImporte.Text = _Importe.ToString("N")

        RaiseEvent HaCambiado()

    End Sub

End Class
