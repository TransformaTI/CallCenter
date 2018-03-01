Public Class AltaCheque
    Inherits System.Windows.Forms.Form
    Private _Contrato As Integer
    Public _Banco As Integer
    Public _FCheque As DateTime
    Public _Cheque As String
    Public _Cuenta As String
    Public _Monto As Decimal
    Public _Nombre As String
    Public _Cliente As Integer
    Public _NombreCliente As String
    Public _PosFechado As String
    Private _Titulo As String = "Captura de cheque"


    Public Sub Entrada(ByVal Contrato As Integer)

        'Try
        '    SqlConnection.ConnectionString = GLOBAL_ConString
        '    SqlConnection.Open()
        'Catch dataException As Exception
        '    MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        'End Try

        'daBancos.Fill(DsAltaCheque, "Banco")
        cboBanco.CargaDatos(CargaBancoCero:=False, MostrarClaves:=True, SoloActivos:=True)
        _PosFechado = ""
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
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdBanco As System.Data.SqlClient.SqlCommand
    Friend WithEvents daBancos As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsAltaCheque As Sigamet.dsAltaCheque
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFCheque As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnLeer As System.Windows.Forms.Button
    Friend WithEvents lbPosfechado As System.Windows.Forms.Label
    Friend WithEvents txtMonto As SigaMetClasses.Controles.txtNumeroDecimal
    Friend WithEvents txtCliente As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtCheque As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents txtCuenta As SigaMetClasses.Controles.txtNumeroEntero
    Friend WithEvents cboBanco As SigaMetClasses.Combos.ComboBanco
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(AltaCheque))
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DsAltaCheque = New Sigamet.dsAltaCheque()
        Me.cmdBanco = New System.Data.SqlClient.SqlCommand()
        Me.daBancos = New System.Data.SqlClient.SqlDataAdapter()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFCheque = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnLeer = New System.Windows.Forms.Button()
        Me.lbPosfechado = New System.Windows.Forms.Label()
        Me.txtMonto = New SigaMetClasses.Controles.txtNumeroDecimal()
        Me.txtCliente = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtCheque = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.txtCuenta = New SigaMetClasses.Controles.txtNumeroEntero()
        Me.cboBanco = New SigaMetClasses.Combos.ComboBanco()
        CType(Me.DsAltaCheque, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(432, 16)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 7
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(432, 48)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 8
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "Data Source=Desarrollo; Initial Catalog=Sigamet;User ID =sa;Password =DEVELOPMENT" & _
        ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 14)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Banco:"
        '
        'DsAltaCheque
        '
        Me.DsAltaCheque.DataSetName = "dsAltaCheque"
        Me.DsAltaCheque.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsAltaCheque.Namespace = "http://www.tempuri.org/dsAltaCheque.xsd"
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
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "No. cheque:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 14)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Fecha cheque:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 182)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 14)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "No. Cuenta:"
        '
        'txtFCheque
        '
        Me.txtFCheque.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFCheque.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.txtFCheque.Location = New System.Drawing.Point(104, 144)
        Me.txtFCheque.Name = "txtFCheque"
        Me.txtFCheque.Size = New System.Drawing.Size(288, 26)
        Me.txtFCheque.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 214)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 14)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "Monto:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 14)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Cliente:"
        '
        'txtCodigo
        '
        Me.txtCodigo.BackColor = System.Drawing.Color.Black
        Me.txtCodigo.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigo.ForeColor = System.Drawing.Color.Gold
        Me.txtCodigo.Location = New System.Drawing.Point(104, 16)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(288, 18)
        Me.txtCodigo.TabIndex = 0
        Me.txtCodigo.Text = ""
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 18)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 14)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Usar lector:"
        '
        'btnLeer
        '
        Me.btnLeer.Location = New System.Drawing.Point(264, 16)
        Me.btnLeer.Name = "btnLeer"
        Me.btnLeer.Size = New System.Drawing.Size(48, 18)
        Me.btnLeer.TabIndex = 12
        '
        'lbPosfechado
        '
        Me.lbPosfechado.AutoSize = True
        Me.lbPosfechado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPosfechado.ForeColor = System.Drawing.Color.Red
        Me.lbPosfechado.Location = New System.Drawing.Point(416, 150)
        Me.lbPosfechado.Name = "lbPosfechado"
        Me.lbPosfechado.Size = New System.Drawing.Size(93, 14)
        Me.lbPosfechado.TabIndex = 13
        Me.lbPosfechado.Text = "POST FECHADO"
        Me.lbPosfechado.Visible = False
        '
        'txtMonto
        '
        Me.txtMonto.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMonto.ForeColor = System.Drawing.Color.Red
        Me.txtMonto.Location = New System.Drawing.Point(104, 208)
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.Size = New System.Drawing.Size(160, 26)
        Me.txtMonto.TabIndex = 6
        Me.txtMonto.Text = ""
        '
        'txtCliente
        '
        Me.txtCliente.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCliente.Location = New System.Drawing.Point(104, 48)
        Me.txtCliente.Name = "txtCliente"
        Me.txtCliente.Size = New System.Drawing.Size(160, 26)
        Me.txtCliente.TabIndex = 1
        Me.txtCliente.Text = ""
        '
        'txtCheque
        '
        Me.txtCheque.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheque.Location = New System.Drawing.Point(104, 112)
        Me.txtCheque.Name = "txtCheque"
        Me.txtCheque.Size = New System.Drawing.Size(288, 26)
        Me.txtCheque.TabIndex = 3
        Me.txtCheque.Text = ""
        '
        'txtCuenta
        '
        Me.txtCuenta.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCuenta.Location = New System.Drawing.Point(104, 176)
        Me.txtCuenta.Name = "txtCuenta"
        Me.txtCuenta.Size = New System.Drawing.Size(288, 26)
        Me.txtCuenta.TabIndex = 5
        Me.txtCuenta.Text = ""
        '
        'cboBanco
        '
        Me.cboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBanco.Font = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBanco.Location = New System.Drawing.Point(104, 80)
        Me.cboBanco.Name = "cboBanco"
        Me.cboBanco.Size = New System.Drawing.Size(288, 26)
        Me.cboBanco.TabIndex = 14
        '
        'AltaCheque
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(514, 250)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cboBanco, Me.txtCuenta, Me.txtCheque, Me.txtCliente, Me.txtMonto, Me.lbPosfechado, Me.Label7, Me.Label5, Me.Label4, Me.Label3, Me.Label2, Me.Label1, Me.Label6, Me.txtCodigo, Me.txtFCheque, Me.btnCancelar, Me.btnAceptar, Me.btnLeer})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "AltaCheque"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Alta Cheque"
        CType(Me.DsAltaCheque, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click

        If txtCliente.Text.Trim = "" Then
            MessageBox.Show("Debe teclear el número de cliente.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCliente.Focus()
            Exit Sub
        End If

        If txtCheque.Text.Trim = "" Then
            MessageBox.Show("Debe teclear el número de cheque.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCheque.Focus()
            Exit Sub
        End If

        If txtCheque.Text.Trim.Length < 7 Then
            MessageBox.Show("El número de cheque debe ser de 7 dígitos.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCheque.Focus()
            Exit Sub
        End If

        If txtCuenta.Text.Trim = "" Then
            MessageBox.Show("Debe teclear el número de cuenta.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCuenta.Focus()
            Exit Sub
        End If

        If txtMonto.Text.Trim = "" Then
            MessageBox.Show("Debe teclear el importe del cheque.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtMonto.SelectAll()
            txtMonto.Focus()
            Exit Sub
        End If


        _Banco = CType(cboBanco.SelectedValue, Integer)
        _FCheque = txtFCheque.Value.Date
        _Cheque = txtCheque.Text.Trim
        _Cuenta = txtCuenta.Text.Trim
        _Monto = CType(txtMonto.Text.Trim, Decimal)
        _Nombre = cboBanco.Text.Trim
        _Cliente = CType(txtCliente.Text, Integer)

        SqlConnection.Close()
        DialogResult = DialogResult.OK

    End Sub


    Private Sub txtCliente_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCliente.Validated
        If txtCliente.Text.Trim <> "" Then
            Try
                If CType(txtCliente.Text.Trim, Integer) <> 0 Then
                    Dim oCliente As SigaMetClasses.cCliente = Nothing
                    Try
                        oCliente = New SigaMetClasses.cCliente(CType(txtCliente.Text.Trim, Integer))
                        If oCliente.Nombre.Trim <> "" Then
                            If MessageBox.Show("El cliente es: " & oCliente.Nombre.Trim & Chr(13) & SigaMetClasses.M_ESTAN_CORRECTOS, _Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                                _NombreCliente = oCliente.Nombre
                            Else
                                _NombreCliente = ""
                                txtCliente.SelectAll()
                                txtCliente.Focus()
                            End If
                        Else
                            MessageBox.Show("El cliente no existe en la base de datos.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            txtCliente.SelectAll()
                            txtCliente.Focus()
                            Exit Sub
                        End If
                    Catch
                        MessageBox.Show("El cliente no existe en la base de datos.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        txtCliente.SelectAll()
                        txtCliente.Focus()
                        Exit Sub
                    Finally
                        oCliente.Dispose()
                    End Try
                End If
            Catch
                txtCliente.SelectAll()
                txtCliente.Focus()
            End Try
        End If

        'Dim cmdInsert As New SqlClient.SqlCommand()
        'Dim rdrInsert As SqlClient.SqlDataReader
        'Dim Registro As Integer
        'Dim Nombre As String

        'If txtCliente.Text <> "" Then
        '    cmdInsert.Connection = SqlConnection
        '    cmdInsert.CommandTimeout = 30
        '    cmdInsert.CommandText = "Select Count(*) Registro from Cliente where Cliente=@Cliente "
        '    cmdInsert.Parameters.Clear()
        '    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(txtCliente.Text, Integer)
        '    rdrInsert = cmdInsert.ExecuteReader
        '    rdrInsert.Read()
        '    Registro = CType(rdrInsert("Registro"), Integer)
        '    rdrInsert.Close()
        '    If Registro > 0 Then
        '        cmdInsert.CommandText = "Select Nombre from Cliente where Cliente=@Cliente "
        '        cmdInsert.Parameters.Clear()
        '        cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(txtCliente.Text, Integer)
        '        rdrInsert = cmdInsert.ExecuteReader
        '        rdrInsert.Read()
        '        Nombre = CType(rdrInsert("Nombre"), String)
        '        rdrInsert.Close()
        '        If MsgBox("Este es el nombre del cliente: " + Nombre + Chr(13) + "¿Es correcto?.", MsgBoxStyle.YesNo, "Mensaje del sistema") = MsgBoxResult.No Then
        '            txtCliente.Select()
        '        Else
        '            _NombreCliente = Nombre
        '        End If
        '    Else
        '        MessageBox.Show("Teclee un numero de contrato valido.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        txtCliente.Select()
        '    End If
        'End If

    End Sub

    Private Sub txtCodigo_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.Enter
        Me.AcceptButton = btnLeer
    End Sub

    Private Sub txtCodigo_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodigo.Leave
        Me.AcceptButton = btnAceptar
    End Sub

    Private Sub btnLeer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLeer.Click
        Dim strCodigo As String = txtCodigo.Text.Trim
        Dim NumeroCuenta As String
        Dim NumeroCheque As String
        NumeroCuenta = Mid(strCodigo, 16, 11)
        NumeroCheque = Mid(strCodigo, 28, 7)

        txtCuenta.Text = NumeroCuenta
        txtCheque.Text = NumeroCheque
        txtCodigo.Text = ""
    End Sub

    Private Sub txtFCheque_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFCheque.ValueChanged
        If txtFCheque.Value.Date > Now.Date Then
            lbPosfechado.Visible = True
            _PosFechado = "P"
        Else
            lbPosfechado.Visible = False
            _PosFechado = ""
        End If
    End Sub

    Private Sub txtCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCheque.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCuenta_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCuenta.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
