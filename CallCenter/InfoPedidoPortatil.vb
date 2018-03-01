Imports System.Data.SqlClient
Imports Microsoft.VisualBasic.ControlChars

Public Class InfoPedidoPortatil
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New(ByVal Cliente As Integer, ByVal Pedido As Integer)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        _Cliente = Cliente
        _Pedido = Pedido
        CargaDatos()
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
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents lblFPedido As System.Windows.Forms.Label
    Friend WithEvents lblFCompromiso As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblRuta As System.Windows.Forms.Label
    Friend WithEvents txtCliente As System.Windows.Forms.TextBox
    Friend WithEvents txtFecha As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtFCompromiso As System.Windows.Forms.TextBox
    Friend WithEvents txtRuta As System.Windows.Forms.TextBox
    Friend WithEvents rtxtDetalle As System.Windows.Forms.RichTextBox
    Friend WithEvents lblLinea As System.Windows.Forms.Label
    Friend WithEvents picTanque As System.Windows.Forms.PictureBox
    Friend WithEvents txtMotivoCancelacion As System.Windows.Forms.TextBox
    Friend WithEvents lblMotivoCancelacion As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(InfoPedidoPortatil))
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.lblFPedido = New System.Windows.Forms.Label()
        Me.lblFCompromiso = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblRuta = New System.Windows.Forms.Label()
        Me.txtCliente = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.txtFCompromiso = New System.Windows.Forms.TextBox()
        Me.txtRuta = New System.Windows.Forms.TextBox()
        Me.rtxtDetalle = New System.Windows.Forms.RichTextBox()
        Me.lblLinea = New System.Windows.Forms.Label()
        Me.picTanque = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMotivoCancelacion = New System.Windows.Forms.TextBox()
        Me.lblMotivoCancelacion = New System.Windows.Forms.Label()
        CType(Me.picTanque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblCliente
        '
        Me.lblCliente.AutoSize = True
        Me.lblCliente.Location = New System.Drawing.Point(8, 6)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(44, 13)
        Me.lblCliente.TabIndex = 2
        Me.lblCliente.Text = "Cliente:"
        '
        'lblFPedido
        '
        Me.lblFPedido.AutoSize = True
        Me.lblFPedido.Location = New System.Drawing.Point(80, 31)
        Me.lblFPedido.Name = "lblFPedido"
        Me.lblFPedido.Size = New System.Drawing.Size(40, 13)
        Me.lblFPedido.TabIndex = 2
        Me.lblFPedido.Text = "Fecha:"
        '
        'lblFCompromiso
        '
        Me.lblFCompromiso.AutoSize = True
        Me.lblFCompromiso.Location = New System.Drawing.Point(224, 31)
        Me.lblFCompromiso.Name = "lblFCompromiso"
        Me.lblFCompromiso.Size = New System.Drawing.Size(69, 13)
        Me.lblFCompromiso.TabIndex = 2
        Me.lblFCompromiso.Text = "Compromiso:"
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(80, 55)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 13)
        Me.lblStatus.TabIndex = 2
        Me.lblStatus.Text = "Status:"
        '
        'lblRuta
        '
        Me.lblRuta.AutoSize = True
        Me.lblRuta.Location = New System.Drawing.Point(224, 55)
        Me.lblRuta.Name = "lblRuta"
        Me.lblRuta.Size = New System.Drawing.Size(34, 13)
        Me.lblRuta.TabIndex = 2
        Me.lblRuta.Text = "Ruta:"
        '
        'txtCliente
        '
        Me.txtCliente.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtCliente.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtCliente.ForeColor = System.Drawing.Color.Black
        Me.txtCliente.Location = New System.Drawing.Point(61, 6)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.ReadOnly = True
        Me.txtCliente.Size = New System.Drawing.Size(323, 14)
        Me.txtCliente.TabIndex = 3
        '
        'txtFecha
        '
        Me.txtFecha.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFecha.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFecha.ForeColor = System.Drawing.Color.Black
        Me.txtFecha.Location = New System.Drawing.Point(122, 31)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.Size = New System.Drawing.Size(86, 14)
        Me.txtFecha.TabIndex = 4
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtStatus.ForeColor = System.Drawing.Color.Black
        Me.txtStatus.Location = New System.Drawing.Point(122, 55)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(86, 14)
        Me.txtStatus.TabIndex = 5
        '
        'txtFCompromiso
        '
        Me.txtFCompromiso.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtFCompromiso.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtFCompromiso.ForeColor = System.Drawing.Color.Black
        Me.txtFCompromiso.Location = New System.Drawing.Point(294, 31)
        Me.txtFCompromiso.Name = "txtFCompromiso"
        Me.txtFCompromiso.ReadOnly = True
        Me.txtFCompromiso.Size = New System.Drawing.Size(88, 14)
        Me.txtFCompromiso.TabIndex = 4
        '
        'txtRuta
        '
        Me.txtRuta.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtRuta.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtRuta.ForeColor = System.Drawing.Color.Black
        Me.txtRuta.Location = New System.Drawing.Point(294, 55)
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.ReadOnly = True
        Me.txtRuta.Size = New System.Drawing.Size(88, 14)
        Me.txtRuta.TabIndex = 6
        '
        'rtxtDetalle
        '
        Me.rtxtDetalle.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.rtxtDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtxtDetalle.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.rtxtDetalle.Location = New System.Drawing.Point(0, 129)
        Me.rtxtDetalle.Name = "rtxtDetalle"
        Me.rtxtDetalle.ReadOnly = True
        Me.rtxtDetalle.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None
        Me.rtxtDetalle.Size = New System.Drawing.Size(392, 16)
        Me.rtxtDetalle.TabIndex = 7
        Me.rtxtDetalle.Text = ""
        '
        'lblLinea
        '
        Me.lblLinea.AutoSize = True
        Me.lblLinea.Location = New System.Drawing.Point(3, 12)
        Me.lblLinea.Name = "lblLinea"
        Me.lblLinea.Size = New System.Drawing.Size(391, 13)
        Me.lblLinea.TabIndex = 8
        Me.lblLinea.Text = "________________________________________________________________"
        '
        'picTanque
        '
        Me.picTanque.Image = CType(resources.GetObject("picTanque.Image"), System.Drawing.Image)
        Me.picTanque.Location = New System.Drawing.Point(26, 35)
        Me.picTanque.Name = "picTanque"
        Me.picTanque.Size = New System.Drawing.Size(32, 32)
        Me.picTanque.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTanque.TabIndex = 9
        Me.picTanque.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-7, 105)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(391, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "________________________________________________________________"
        '
        'txtMotivoCancelacion
        '
        Me.txtMotivoCancelacion.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.txtMotivoCancelacion.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtMotivoCancelacion.ForeColor = System.Drawing.Color.Black
        Me.txtMotivoCancelacion.Location = New System.Drawing.Point(187, 79)
        Me.txtMotivoCancelacion.Name = "txtMotivoCancelacion"
        Me.txtMotivoCancelacion.ReadOnly = True
        Me.txtMotivoCancelacion.Size = New System.Drawing.Size(136, 14)
        Me.txtMotivoCancelacion.TabIndex = 12
        Me.txtMotivoCancelacion.Visible = False
        '
        'lblMotivoCancelacion
        '
        Me.lblMotivoCancelacion.AutoSize = True
        Me.lblMotivoCancelacion.Location = New System.Drawing.Point(80, 79)
        Me.lblMotivoCancelacion.Name = "lblMotivoCancelacion"
        Me.lblMotivoCancelacion.Size = New System.Drawing.Size(101, 13)
        Me.lblMotivoCancelacion.TabIndex = 11
        Me.lblMotivoCancelacion.Text = "Motivo cancelación:"
        Me.lblMotivoCancelacion.Visible = False
        '
        'InfoPedidoPortatil
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.ClientSize = New System.Drawing.Size(392, 145)
        Me.Controls.Add(Me.txtMotivoCancelacion)
        Me.Controls.Add(Me.lblMotivoCancelacion)
        Me.Controls.Add(Me.picTanque)
        Me.Controls.Add(Me.rtxtDetalle)
        Me.Controls.Add(Me.txtRuta)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.txtCliente)
        Me.Controls.Add(Me.lblCliente)
        Me.Controls.Add(Me.lblFPedido)
        Me.Controls.Add(Me.lblFCompromiso)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.lblRuta)
        Me.Controls.Add(Me.txtFCompromiso)
        Me.Controls.Add(Me.lblLinea)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "InfoPedidoPortatil"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.TopMost = True
        CType(Me.picTanque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Variables globales"
    Private _Cliente, _Pedido As Integer
#End Region

    Private Sub CargaDatos()
        Dim cmdCallCenter As New SqlCommand("exec spCCDatosPedidoPortatil @Cliente,@Pedido", CnnSigamet)
        Dim daCallCenter As New SqlDataAdapter(cmdCallCenter)
        Dim rdPedido As SqlDataReader
        Dim dtProducto As New DataTable()
        Dim Row As DataRow
        Dim Seleccion As Integer
        cmdCallCenter.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
        cmdCallCenter.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
        Try
            AbreConexion()
            rdPedido = cmdCallCenter.ExecuteReader
            rdPedido.Read()
            txtCliente.Text = CStr(rdPedido("Nombre"))
            txtFecha.Text = CDate(rdPedido("FAlta")).ToShortDateString
            txtFCompromiso.Text = CDate(rdPedido("FCompromiso")).ToShortDateString
            txtStatus.Text = CStr(rdPedido("Status"))
            txtRuta.Text = CStr(rdPedido("Ruta"))

            If RTrim(txtStatus.Text) = "CANCELADO" Then
                lblMotivoCancelacion.Visible = True
                txtMotivoCancelacion.Visible = True
                txtMotivoCancelacion.Text = CStr(rdPedido("MotivoCancelacion"))
            End If
            rdPedido.Close()

            cmdCallCenter.CommandText = "exec spCCDetallePedidoPortatil @Pedido"
            daCallCenter.Fill(dtProducto)
            rtxtDetalle.Rtf = "{\rtf1\ansi\deff0{\fonttbl{\f0\fswiss\fcharset0 Arial;}}" & _
                        "\viewkind4\uc1\pard\lang2058\ul\b\f0\fs20 PRODUCTO\ulnone\tab\tab\tab\tab\ul CANTIDAD\ulnone\b0\par\pard\ul\b\par}"
            Seleccion = rtxtDetalle.Text.Length - 1
            For Each Row In dtProducto.Rows
                If CInt(Row("Cantidad")) > 0 Then
                    rtxtDetalle.AppendText(CStr(Row("Descripcion")) & Tab & Tab & CStr(Row("Cantidad")).PadLeft(20, CChar(" ")) & CrLf)
                    rtxtDetalle.Height += 16
                End If
            Next
            rtxtDetalle.Select(Seleccion, rtxtDetalle.Text.Length - Seleccion)
            rtxtDetalle.SelectionFont = Me.Font
            rtxtDetalle.SelectionLength = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, Application.ProductName & " v." & Application.ProductVersion, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
        End Try
    End Sub

    Private Sub rtxtDetalle_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles rtxtDetalle.Resize
        If RTrim(txtStatus.Text) = "CANCELADO" Then            
            Me.Height = rtxtDetalle.Height + 97
        Else
            Me.Height = rtxtDetalle.Height + 87
        End If
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub


End Class
