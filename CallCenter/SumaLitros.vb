Public Class SumaLitros
    Inherits System.Windows.Forms.Form
    
    Private _AñoAtt As Integer
    Private _Folio As Integer
    Private _Autotanque As Integer
    Private _Ruta As Integer
    Private _Tipo As Integer


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
    Friend WithEvents txtLitros As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbTipo As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.txtContrato = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtLitros = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbTipo = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnBuscar
        '
        Me.btnBuscar.Location = New System.Drawing.Point(200, 40)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.TabIndex = 6
        Me.btnBuscar.Text = "Buscar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(200, 72)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(200, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 4
        Me.btnAceptar.Text = "Aceptar"
        '
        'txtContrato
        '
        Me.txtContrato.Location = New System.Drawing.Point(72, 9)
        Me.txtContrato.MaxLength = 10
        Me.txtContrato.Name = "txtContrato"
        Me.txtContrato.Size = New System.Drawing.Size(120, 21)
        Me.txtContrato.TabIndex = 0
        Me.txtContrato.Text = ""
        Me.txtContrato.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 14)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Contrato :"
        '
        'txtLitros
        '
        Me.txtLitros.Location = New System.Drawing.Point(72, 42)
        Me.txtLitros.MaxLength = 9
        Me.txtLitros.Name = "txtLitros"
        Me.txtLitros.Size = New System.Drawing.Size(120, 21)
        Me.txtLitros.TabIndex = 1
        Me.txtLitros.Text = ""
        Me.txtLitros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 14)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Litros :"
        '
        'lbTipo
        '
        Me.lbTipo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTipo.Location = New System.Drawing.Point(8, 80)
        Me.lbTipo.Name = "lbTipo"
        Me.lbTipo.Size = New System.Drawing.Size(184, 23)
        Me.lbTipo.TabIndex = 11
        Me.lbTipo.Text = "CONTADO"
        Me.lbTipo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'SumaLitros
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(282, 104)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbTipo, Me.Label2, Me.txtLitros, Me.btnBuscar, Me.btnCancelar, Me.btnAceptar, Me.txtContrato, Me.Label1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "SumaLitros"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modificacion de litros"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Entrada(ByVal Añoatt As Integer, ByVal Folio As Integer, ByVal Autotanque As Integer, ByVal Ruta As Integer, ByVal Tipo As Integer)
        _Tipo = Tipo
        _AñoAtt = Añoatt
        _Folio = Folio
        _Autotanque = Autotanque
        _Ruta = Ruta

        If Tipo = 0 Then
            lbTipo.Text = "CONTADO"
        Else
            lbTipo.Text = "CREDITO"
        End If

        Me.ShowDialog()

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim cn As New SqlClient.SqlConnection(GLOBAL_ConString)
        Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim SiGrabo As Boolean
        Dim Registro As Integer
        Dim Nombre As String = Nothing
        Dim NCalle As String = Nothing
        Dim NColonia As String = Nothing

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        cn.Open()

        If txtContrato.Text <> "" And txtLitros.Text <> "" Then
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

                If MsgBox("¿Desea Ingresar los siguientes " + txtLitros.Text + "?", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
                    Transaccion = cn.BeginTransaction
                    cmdInsert.Transaction = Transaccion
                    cmdInsert.CommandType = CommandType.StoredProcedure
                    Try
                        cmdInsert.CommandText = "sp_CargarLitros"
                        cmdInsert.Parameters.Clear()
                        cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AñoAtt
                        cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                        cmdInsert.Parameters.Add("@Litros", SqlDbType.Int).Value = CType(txtLitros.Text, Integer)
                        cmdInsert.Parameters.Add("@ClienteGeneral", SqlDbType.Int).Value = CType(txtContrato.Text, Integer)
                        cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                        cmdInsert.Parameters.Add("@AutoTanque", SqlDbType.Money).Value = _Autotanque
                        cmdInsert.Parameters.Add("@Tipo", SqlDbType.Decimal).Value = _Tipo
                        cmdInsert.ExecuteNonQuery()

                        Transaccion.Commit()
                        SiGrabo = True
                    Catch et As Exception
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        Transaccion.Rollback()
                        MsgBox(et.Message, MsgBoxStyle.Critical)
                        SiGrabo = False
                        cn.Close()
                    Finally
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        If SiGrabo = True Then
                            MsgBox("La conciliacion fue realizada con exito", MsgBoxStyle.Information, "Mensaje del sistema")
                            Me.Close()
                        End If
                    End Try
                End If
            Else
                MsgBox("El contrato con el que se quiere conciliar, no es un contrato valido o es un contrato INACTIVO. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
                txtContrato.Select()
            End If

        End If

        cn.Close()

    End Sub

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
            'frmBuscar.Dispose()

            If Contrato > 0 Then
                txtContrato.Text = CType(Contrato, String)
            End If
        End If

    End Sub

    Private Sub txtLitros_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLitros.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
