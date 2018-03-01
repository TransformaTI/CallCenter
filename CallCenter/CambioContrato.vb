Public Class CambioContrato
    Inherits System.Windows.Forms.Form

    Private _ClienteConciliar As Integer
    Private _NombreConciliar As String
    Private _Pedido As Integer
    Private _Anio As Integer
    Private _Celula As Integer
    Private _Importe As Decimal
    Private _Litros As Decimal
    Private _AñoAtt As Integer
    Private _Folio As Integer


    Public Sub Entrada(ByVal Conciliar As Integer, ByVal Nombre As String, ByVal Pedido As Integer, ByVal Anio As Integer, ByVal Celula As Integer, ByVal Importe As Decimal, ByVal Litros As Decimal, ByVal Añoatt As Integer, ByVal Folio As Integer)

        _ClienteConciliar = Conciliar
        _NombreConciliar = Nombre
        _Pedido = Pedido
        _Anio = Anio
        _Celula = Celula
        _Importe = Importe
        _Litros = Litros
        _AñoAtt = Añoatt
        _Folio = Folio

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
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents txtContrato As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.txtContrato = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(201, 43)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.TabIndex = 17
        Me.btnBuscar.Text = "Buscar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(201, 75)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 15
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(201, 11)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 14
        Me.btnAceptar.Text = "Aceptar"
        '
        'txtContrato
        '
        Me.txtContrato.Location = New System.Drawing.Point(73, 30)
        Me.txtContrato.MaxLength = 10
        Me.txtContrato.Name = "txtContrato"
        Me.txtContrato.Size = New System.Drawing.Size(120, 21)
        Me.txtContrato.TabIndex = 12
        Me.txtContrato.Text = ""
        Me.txtContrato.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Contrato :"
        '
        'CambioContrato
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(282, 104)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnBuscar, Me.btnCancelar, Me.btnAceptar, Me.txtContrato, Me.Label1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CambioContrato"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambio de contrato"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub txtContrato_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtContrato.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim Contrato As Integer

        Dim frmBuscar As New SigaMetClasses.BusquedaCliente()
        If frmBuscar.ShowDialog() = DialogResult.OK Then
            Contrato = frmBuscar.Cliente
            If Contrato > 0 Then
                txtContrato.Text = CType(Contrato, String)
            End If
        End If
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim cn As New SqlClient.SqlConnection(GLOBAL_ConString)
        Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim SiGrabo As Boolean
        Dim Registro As Integer
        Dim Nombre As String
        Dim NCalle As String
        Dim NColonia As String

        cn.Open()

        If txtContrato.Text <> "" Then
            cmdInsert.Connection = cn
            cmdInsert.CommandTimeout = 30
            cmdInsert.CommandText = "Select Count(*) as Registro  from Cliente where Cliente=@Cliente and Status<>'INACTIVO' "
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(txtContrato.Text, Integer)
            rdrInsert = cmdInsert.ExecuteReader
            rdrInsert.Read()
            Registro = CType(rdrInsert("Registro"), Integer)
            rdrInsert.Close()

            If Registro > 0 Then
                If CType(txtContrato.Text, Integer) <> _ClienteConciliar Then
                    cmdInsert.CommandText = " Select C.Nombre, Convert(Varchar(60),CA.Nombre)+' '+Convert(VarChar(9),C.NumExterior) as Calle, " & _
                                            " Convert(Varchar(80),CO.Nombre) as Colonia      " & _
                                            " from Cliente C INNER JOIN Calle CA ON C.Calle=CA.Calle " & _
                                            " INNER JOIN Colonia CO ON C.Colonia=CO.Colonia " & _
                                            " where(Cliente = @Cliente) "
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(txtContrato.Text, Integer)
                    rdrInsert = cmdInsert.ExecuteReader
                    rdrInsert.Read()
                    Nombre = CType(rdrInsert("Nombre"), String)
                    NCalle = CType(rdrInsert("Calle"), String)
                    NColonia = CType(rdrInsert("Colonia"), String)
                    rdrInsert.Close()
                    If MsgBox("El cliente de la liquidación es : " + _NombreConciliar + Chr(13) + "El cliente por el cual se va hacer la conciliación es : " + Chr(13) + Chr(13) + Nombre + Chr(13) + NCalle + Chr(13) + "Colonia " + NColonia + Chr(13) + Chr(13) + "¿Es esto correcto?.", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
                        Transaccion = cn.BeginTransaction
                        cmdInsert.Transaction = Transaccion
                        cmdInsert.CommandType = CommandType.StoredProcedure
                        Try
                            cmdInsert.CommandText = "sp_CambioCliente"
                            cmdInsert.Parameters.Clear()
                            cmdInsert.Parameters.Add("@ClienteViejo", SqlDbType.Int).Value = _ClienteConciliar
                            cmdInsert.Parameters.Add("@ClienteNuevo", SqlDbType.Int).Value = CType(txtContrato.Text, Integer)
                            cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = _Anio
                            cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                            cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                            cmdInsert.Parameters.Add("@Importe", SqlDbType.Money).Value = _Importe
                            cmdInsert.Parameters.Add("@Litros", SqlDbType.Decimal).Value = _Litros
                            cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Decimal).Value = _AñoAtt
                            cmdInsert.Parameters.Add("@Folio", SqlDbType.Decimal).Value = _Folio
                            cmdInsert.ExecuteNonQuery()

                            Transaccion.Commit()
                            SiGrabo = True
                        Catch et As Exception
                            Transaccion.Rollback()
                            MsgBox(et.Message, MsgBoxStyle.Critical)
                            SiGrabo = False
                        Finally
                            If SiGrabo = True Then
                                MsgBox("La conciliacion fue realizada con exito", MsgBoxStyle.Information, "Mensaje del sistema")
                                Me.Close()
                            End If
                        End Try
                    End If
                End If
            Else
                MsgBox("El contrato con el que se quiere conciliar, no es un contrato valido o es un contrato INACTIVO. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
                txtContrato.Select()
            End If

        End If

        cn.Close()

    End Sub
End Class
