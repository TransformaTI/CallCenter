Public Class AltaDeposito
    Inherits System.Windows.Forms.Form
    Private _Contrato As Integer
    Public _Banco As Integer
    Public _FCheque As DateTime
    Public _Cuenta As String
    Public _Monto As Decimal
    Public _Nombre As String
    Public _Cliente As Integer
    Public _Cheque As String
    Public _NombreCliente As String
    Public _TPV As Boolean


    Public Sub Entrada(ByVal Contrato As Integer)

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        'daBancos.Fill(DsAltaCheque, "Banco")
        cboBanco.CargaDatos(CargaBancoCero:=True, MostrarClaves:=True, SoloActivos:=True)

        txtFCheque.Value = Now.Date
        _Contrato = Contrato

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
    Friend WithEvents txtMonto As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFCheque As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCuenta As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents DsAltaCheque As Sigamet.dsAltaCheque
    Friend WithEvents daBancos As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdBanco As System.Data.SqlClient.SqlCommand
    Friend WithEvents txtCliente As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboBanco As SigaMetClasses.Combos.ComboBanco
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtCheque As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbLocal As System.Windows.Forms.RadioButton
    Friend WithEvents rbAutotanque As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtMonto = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFCheque = New System.Windows.Forms.DateTimePicker
        Me.txtCuenta = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.DsAltaCheque = New Sigamet.dsAltaCheque
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection
        Me.daBancos = New System.Data.SqlClient.SqlDataAdapter
        Me.cmdBanco = New System.Data.SqlClient.SqlCommand
        Me.txtCliente = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboBanco = New SigaMetClasses.Combos.ComboBanco
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtCheque = New SigaMetClasses.Controles.txtNumeroEntero
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.rbLocal = New System.Windows.Forms.RadioButton
        Me.rbAutotanque = New System.Windows.Forms.RadioButton
        CType(Me.DsAltaCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMonto
        '
        Me.txtMonto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonto.Location = New System.Drawing.Point(112, 152)
        Me.txtMonto.MaxLength = 20
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(104, 21)
        Me.txtMonto.TabIndex = 5
        Me.txtMonto.Text = ""
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(24, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 17)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Monto :"
        '
        'txtFCheque
        '
        Me.txtFCheque.Location = New System.Drawing.Point(112, 72)
        Me.txtFCheque.Name = "txtFCheque"
        Me.txtFCheque.Size = New System.Drawing.Size(213, 21)
        Me.txtFCheque.TabIndex = 2
        '
        'txtCuenta
        '
        Me.txtCuenta.Location = New System.Drawing.Point(112, 128)
        Me.txtCuenta.MaxLength = 20
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(213, 21)
        Me.txtCuenta.TabIndex = 4
        Me.txtCuenta.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(24, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 17)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Autorización :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(24, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 17)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Fecha :"
        '
        'DsAltaCheque
        '
        Me.DsAltaCheque.DataSetName = "dsAltaCheque"
        Me.DsAltaCheque.Locale = New System.Globalization.CultureInfo("es-MX")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(24, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 17)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Banco :"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(344, 48)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(344, 16)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 6
        Me.btnAceptar.Text = "Aceptar"
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
        'txtCliente
        '
        Me.txtCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Location = New System.Drawing.Point(112, 16)
        Me.txtCliente.MaxLength = 20
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(104, 21)
        Me.txtCliente.TabIndex = 0
        Me.txtCliente.Text = ""
        Me.txtCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 17)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "Cliente :"
        '
        'cboBanco
        '
        Me.cboBanco.Location = New System.Drawing.Point(112, 48)
        Me.cboBanco.Name = "cboBanco"
        Me.cboBanco.Size = New System.Drawing.Size(213, 21)
        Me.cboBanco.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 96)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 17)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Tarjeta :"
        '
        'TxtCheque
        '
        Me.TxtCheque.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCheque.Location = New System.Drawing.Point(112, 96)
        Me.TxtCheque.MaxLength = 20
        Me.TxtCheque.Name = "TxtCheque"
        Me.TxtCheque.Size = New System.Drawing.Size(216, 21)
        Me.TxtCheque.TabIndex = 3
        Me.TxtCheque.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbLocal)
        Me.GroupBox1.Controls.Add(Me.rbAutotanque)
        Me.GroupBox1.Location = New System.Drawing.Point(24, 184)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(304, 80)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tipo de TPV"
        '
        'rbLocal
        '
        Me.rbLocal.Location = New System.Drawing.Point(16, 56)
        Me.rbLocal.Name = "rbLocal"
        Me.rbLocal.Size = New System.Drawing.Size(128, 16)
        Me.rbLocal.TabIndex = 27
        Me.rbLocal.Text = "LOCAL"
        '
        'rbAutotanque
        '
        Me.rbAutotanque.Checked = True
        Me.rbAutotanque.Location = New System.Drawing.Point(16, 24)
        Me.rbAutotanque.Name = "rbAutotanque"
        Me.rbAutotanque.Size = New System.Drawing.Size(128, 16)
        Me.rbAutotanque.TabIndex = 26
        Me.rbAutotanque.TabStop = True
        Me.rbAutotanque.Text = "AUTOTANQUE"
        '
        'AltaDeposito
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(426, 282)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtCheque)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCliente)
        Me.Controls.Add(Me.txtMonto)
        Me.Controls.Add(Me.txtCuenta)
        Me.Controls.Add(Me.cboBanco)
        Me.Controls.Add(Me.txtFCheque)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "AltaDeposito"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Alta Deposito"
        CType(Me.DsAltaCheque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub txtCuenta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCuenta.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtMonto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMonto.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Or e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        If txtCuenta.Text = "" Then
            MsgBox("Teclee un numero de cuenta.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        If txtMonto.Text = "" Then
            MsgBox("Teclee un monto para el cheque.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If


        _Banco = CType(cboBanco.SelectedValue, Integer)
        _FCheque = txtFCheque.Value.Date
        _Cuenta = TxtCheque.Text
        _Monto = CType(txtMonto.Text, Decimal)
        _Nombre = cboBanco.Text.Trim
        _Cliente = CType(txtCliente.Text, Integer)
        _Cheque = txtCuenta.Text.Trim

        If rbAutotanque.Checked Then
            _TPV = False
        Else
            _TPV = True
        End If

        SqlConnection.Close()
        DialogResult = DialogResult.OK

    End Sub


    Private Sub txtCliente_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCliente.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCliente_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCliente.Validated
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Registro As Integer
        Dim Nombre As String

        If txtCliente.Text <> "" Then
            cmdInsert.Connection = SqlConnection
            cmdInsert.CommandTimeout = 30
            cmdInsert.CommandText = "Select Count(*) Registro from Cliente where Cliente=@Cliente "
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(txtCliente.Text, Integer)
            rdrInsert = cmdInsert.ExecuteReader
            rdrInsert.Read()
            Registro = CType(rdrInsert("Registro"), Integer)
            rdrInsert.Close()
            If Registro > 0 Then
                cmdInsert.CommandText = "Select Nombre from Cliente where Cliente=@Cliente "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(txtCliente.Text, Integer)
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                Nombre = CType(rdrInsert("Nombre"), String)
                rdrInsert.Close()
                If MsgBox("Este es el nombre del cliente: " + Nombre + Chr(13) + "¿Es correcto?.", MsgBoxStyle.YesNo, "Mensaje del sistema") = MsgBoxResult.No Then
                    txtCliente.Select()
                Else
                    _NombreCliente = Nombre
                End If
            Else
                MsgBox("Teclee un numero de contrato valido.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                txtCliente.Select()
            End If
        End If

    End Sub


    Private Sub AltaDeposito_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TxtCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtCheque.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
