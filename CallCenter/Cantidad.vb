Public Class Cantidad
    Inherits System.Windows.Forms.Form
    Private _MontoMaximo As Decimal
    Public _Cantidad As Decimal

    Public Sub Entrada(ByVal MontoMaximo As Decimal, _
                           Optional ByVal Cliente As Integer = Nothing, _
                           Optional ByVal Litros As Double = Nothing, _
                           Optional ByVal PorPagar As Double = Nothing, _
                           Optional ByVal ImporteTotal As Double = Nothing, _
                           Optional ByVal Fecha As DateTime = #1/1/1950#)
        _MontoMaximo = MontoMaximo
        txtMonto.Text = Format(MontoMaximo, "#.00")
        lbMontoMaximo.Text = "Monto máximo : " + Format(MontoMaximo, "$ #.00")
        Me.Height = 88
        Try
            If Cliente <> Nothing And _
               Litros <> Nothing Then
                Dim objDescuento As New ImporteDescuento.Descuento(Cliente, CnnSigamet, Fecha)
                If objDescuento.DescuentoValido AndAlso objDescuento.DescuentoLt > 0 Then
                    'Dim _descuento As Double = objDescuento.DescuentoLt * Litros
                    Dim _descuento As Double
                    'Para controlar descuentos por porcentaje 06-12-2005
                    'If UCase(objDescuento.TipoDescuento).Trim = "PESOS POR LITRO" Then
                    '    _descuento = objDescuento.DescuentoLt * Litros
                    '    lblDescripcion.Text = objDescuento.DescuentoLt.ToString & " " & UCase(objDescuento.TipoDescuento)
                    'ElseIf UCase(objDescuento.TipoDescuento).Trim = "PORCENTAJE POR PRODUCTO" Then
                    '    _descuento = objDescuento.DescuentoLt * ImporteTotal
                    '    lblDescripcion.Text = (objDescuento.DescuentoLt * 100).ToString & "% " & UCase(objDescuento.TipoDescuento)
                    'End If

                    Select Case objDescuento.TipoDescuento
                        Case "PESOS POR LITRO"
                            _descuento = objDescuento.DescuentoLt * Litros
                            lblDescripcion.Text = objDescuento.DescuentoLt.ToString & " " & UCase(objDescuento.TipoDescuento)
                        Case "PORCENTAJE POR PRODUCTO"
                            _descuento = objDescuento.DescuentoLt * ImporteTotal
                            lblDescripcion.Text = (objDescuento.DescuentoLt * 100).ToString & "% " & UCase(objDescuento.TipoDescuento)
                            'Para mostrar el importe del descuento de clientes consentidos
                        Case "CLIENTE CONSENTIDO"
                            _descuento = objDescuento.ImporteDescuento
                            lblDescripcion.Text = objDescuento.TipoDescuento
                    End Select

                    pnlDescuento.Visible = True
                    Me.Height = 192

                    lblTotalPedido.Text = "Importe total pedido : " & Format(ImporteTotal, "$ #.00")
                    lblDescuento.Text = "Descuento : " & Format(_descuento, "$ #.00")
                    lblMontoDescuento.Text = "Importe con descuento : " & Format(ImporteTotal - _descuento, "$ #.00")
                    lblPorPagar.Text = "Por pagar : " & Format((PorPagar - _descuento), "$ #.00")
                End If
            End If
        Catch ex As Exception
        End Try

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
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMonto As System.Windows.Forms.TextBox
    Friend WithEvents lbMontoMaximo As System.Windows.Forms.Label
    Friend WithEvents lblDescuento As System.Windows.Forms.Label
    Friend WithEvents lblMontoDescuento As System.Windows.Forms.Label
    Friend WithEvents lblTotalPedido As System.Windows.Forms.Label
    Friend WithEvents pnlDescuento As System.Windows.Forms.Panel
    Friend WithEvents lblPorPagar As System.Windows.Forms.Label
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.lbMontoMaximo = New System.Windows.Forms.Label()
        Me.lblDescuento = New System.Windows.Forms.Label()
        Me.lblMontoDescuento = New System.Windows.Forms.Label()
        Me.lblTotalPedido = New System.Windows.Forms.Label()
        Me.pnlDescuento = New System.Windows.Forms.Panel()
        Me.lblPorPagar = New System.Windows.Forms.Label()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.pnlDescuento.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(256, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(256, 36)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Monto :"
        '
        'txtMonto
        '
        Me.txtMonto.Location = New System.Drawing.Point(84, 8)
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(152, 21)
        Me.txtMonto.TabIndex = 3
        Me.txtMonto.Text = ""
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbMontoMaximo
        '
        Me.lbMontoMaximo.AutoSize = True
        Me.lbMontoMaximo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbMontoMaximo.Location = New System.Drawing.Point(44, 36)
        Me.lbMontoMaximo.Name = "lbMontoMaximo"
        Me.lbMontoMaximo.Size = New System.Drawing.Size(192, 14)
        Me.lbMontoMaximo.TabIndex = 4
        Me.lbMontoMaximo.Text = "Monto máximo a asignar: $ 1000"
        '
        'lblDescuento
        '
        Me.lblDescuento.AutoSize = True
        Me.lblDescuento.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescuento.ForeColor = System.Drawing.Color.Maroon
        Me.lblDescuento.Location = New System.Drawing.Point(91, 48)
        Me.lblDescuento.Name = "lblDescuento"
        Me.lblDescuento.Size = New System.Drawing.Size(114, 14)
        Me.lblDescuento.TabIndex = 5
        Me.lblDescuento.Text = "Descuento : $ 1000"
        '
        'lblMontoDescuento
        '
        Me.lblMontoDescuento.AutoSize = True
        Me.lblMontoDescuento.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMontoDescuento.Location = New System.Drawing.Point(18, 64)
        Me.lblMontoDescuento.Name = "lblMontoDescuento"
        Me.lblMontoDescuento.Size = New System.Drawing.Size(187, 14)
        Me.lblMontoDescuento.TabIndex = 6
        Me.lblMontoDescuento.Text = "Importe con descuento : $ 1000"
        '
        'lblTotalPedido
        '
        Me.lblTotalPedido.AutoSize = True
        Me.lblTotalPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPedido.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblTotalPedido.Location = New System.Drawing.Point(32, 32)
        Me.lblTotalPedido.Name = "lblTotalPedido"
        Me.lblTotalPedido.Size = New System.Drawing.Size(173, 14)
        Me.lblTotalPedido.TabIndex = 7
        Me.lblTotalPedido.Text = "Importe total pedido : $ 1000"
        '
        'pnlDescuento
        '
        Me.pnlDescuento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlDescuento.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblDescripcion, Me.lblDescuento, Me.lblMontoDescuento, Me.lblTotalPedido, Me.lblPorPagar})
        Me.pnlDescuento.Location = New System.Drawing.Point(28, 56)
        Me.pnlDescuento.Name = "pnlDescuento"
        Me.pnlDescuento.Size = New System.Drawing.Size(220, 104)
        Me.pnlDescuento.TabIndex = 8
        Me.pnlDescuento.Visible = False
        '
        'lblPorPagar
        '
        Me.lblPorPagar.AutoSize = True
        Me.lblPorPagar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPorPagar.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblPorPagar.Location = New System.Drawing.Point(95, 80)
        Me.lblPorPagar.Name = "lblPorPagar"
        Me.lblPorPagar.Size = New System.Drawing.Size(110, 14)
        Me.lblPorPagar.TabIndex = 8
        Me.lblPorPagar.Text = "Por pagar : $ 1000"
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescripcion.Location = New System.Drawing.Point(8, 8)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(103, 14)
        Me.lblDescripcion.TabIndex = 10
        Me.lblDescripcion.Text = "TIPODESCUENTO"
        '
        'Cantidad
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(344, 170)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.pnlDescuento, Me.lbMontoMaximo, Me.Label1, Me.txtMonto, Me.btnCancelar, Me.btnAceptar})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Cantidad"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Teclee el monto"
        Me.pnlDescuento.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub txtMonto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMonto.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Or e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtMonto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMonto.TextChanged
        If txtMonto.Text <> "" Then
            If CType(txtMonto.Text, Decimal) > _MontoMaximo Then
                MsgBox("Este monto excede el monto maximo permitido. Verificar", MsgBoxStyle.Information, "Mensaje del ssistema")
                txtMonto.Text = Format(_MontoMaximo, "#.00")
                txtMonto.Select()
            End If
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If txtMonto.Text = "" Then
            MsgBox("Teclee un monto valido, para este movimiento.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        If CType(txtMonto.Text, Decimal) < 1 Then
            MsgBox("Teclee un monto valido, para este movimiento.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        _Cantidad = CType(txtMonto.Text, Decimal)
        DialogResult = DialogResult.OK

    End Sub

End Class
