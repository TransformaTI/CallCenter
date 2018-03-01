Public Class Extravio
    Inherits System.Windows.Forms.Form

    Private _FolioNota As Integer

    Public Sub EntradaExtravio(ByVal FolioNota As Integer)
        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try
        _FolioNota = FolioNota
        Me.Text = "Extravio de la nota " + CStr(FolioNota)

    End Sub

#Region " Código generado por el Diseñador de Windows Forms "

    Public Sub New()
        MyBase.New()

        'El Diseñador de Windows Forms requiere esta llamada.
        InitializeComponent()

        'Agregar cualquier inicialización después de la llamada a InitializeComponent()

    End Sub

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms requiere el siguiente procedimiento
    'Puede modificarse utilizando el Diseñador de Windows Forms. 
    'No lo modifique con el editor de código.
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'txtObservaciones
        '
        Me.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservaciones.Location = New System.Drawing.Point(16, 52)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(248, 152)
        Me.txtObservaciones.TabIndex = 11
        Me.txtObservaciones.Text = ""
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(272, 52)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 10
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnAceptar.Location = New System.Drawing.Point(272, 20)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 9
        Me.btnAceptar.Text = "Aceptar"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(15, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Observaciones :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Extravio
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(362, 224)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtObservaciones, Me.btnCancelar, Me.btnAceptar, Me.Label1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Extravio"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Control de documentos"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        'Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()

        Try
            'Transaccion = SqlConnection.BeginTransaction
            cmdInsert.Connection = SqlConnection
            'cmdInsert.Transaction = Transaccion
            cmdInsert.CommandType = CommandType.StoredProcedure
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "spCDDevolucionNota"
            cmdInsert.Parameters.Clear()

            '@Operacion tinyint,
            '@Serie varchar(5),
            '@Folio int,
            '@Observaciones varchar(255) = NULL

            cmdInsert.Parameters.Add("@Operacion", SqlDbType.TinyInt).Value = 1
            cmdInsert.Parameters.Add("@FolioNota", SqlDbType.Int).Value = _FolioNota
            cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Char).Value = txtObservaciones.Text

            cmdInsert.ExecuteNonQuery()

            'Transaccion.Commit()
            Me.Cursor = Cursors.Default
            Me.DialogResult = DialogResult.OK

        Catch et As Data.SqlClient.SqlException
            'Transaccion.Rollback()
            MessageBox.Show(et.ToString, et.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.DialogResult = DialogResult.Cancel
        End Try

    End Sub


    Private Sub Extravio_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub
End Class
