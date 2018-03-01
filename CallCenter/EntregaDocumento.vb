Public Class EntregaDocumento
    Inherits System.Windows.Forms.Form

    Private _TipoNota As Integer
    Public Sub Entrada(ByVal TipoNota As Integer)

        _TipoNota = TipoNota
        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents txtFolio As System.Windows.Forms.TextBox
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Folio de la nota :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(336, 16)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cerrar"
        '
        'txtFolio
        '
        Me.txtFolio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtFolio.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFolio.ForeColor = System.Drawing.Color.DarkRed
        Me.txtFolio.Location = New System.Drawing.Point(104, 64)
        Me.txtFolio.MaxLength = 15
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.Size = New System.Drawing.Size(304, 38)
        Me.txtFolio.TabIndex = 0
        Me.txtFolio.Text = ""
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'EntregaDocumento
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(418, 160)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.txtFolio, Me.btnCancelar, Me.Label1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "EntregaDocumento"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Entrega de documentos"
        Me.ResumeLayout(False)

    End Sub

#End Region


    Private Sub txtFolio_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtFolio.KeyUp

        If e.KeyCode = Keys.Enter Then
            If txtFolio.Text <> "" Then

                Dim Registro As Integer
                Dim Folio As String
                Dim Serie As String
                Dim TipoNota As Integer
                Dim Mensaje As String = Nothing

                Folio = txtFolio.Text
                Serie = txtFolio.Text
                Serie = Serie.Remove(1, Folio.Length - 1)
                Folio = Folio.Remove(0, 1)

                If Not IsNumeric(Folio) Then
                    MsgBox("Este codigo de nota no es correcto.Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    Exit Sub
                End If

                Dim rdrControl As SqlClient.SqlDataReader
                Dim cmdInsert As New SqlClient.SqlCommand("Select IsNull(TipoNota,0) as TipoNota, Count(FolioNota) as Registro from Nota where RTrim(Serie)=@Serie and FolioNota=@FolioNota group by TipoNota ")
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Serie", SqlDbType.Char).Value = CType(Serie, String)
                cmdInsert.Parameters.Add("@FolioNota", SqlDbType.Int).Value = CType(Folio, Integer)
                rdrControl = cmdInsert.ExecuteReader
                If rdrControl.Read() Then
                    Registro = CType(rdrControl.Item("Registro"), Integer)
                    TipoNota = CType(rdrControl.Item("TipoNota"), Integer)
                Else
                    Registro = 0
                End If

                rdrControl.Close()

                If Registro = 0 Then
                    'MsgBox("El folio de esta nota no existe.Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")

                    If MessageBox.Show("El folio de esta nota no existe en la base de datos" & Chr(13) & _
                                       "¿Desea darlo de alta?", "Control de documentos", _
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                        SaveData(Serie, CInt(Folio), CnnSigamet)
                        TipoNota = _TipoNota
                    Else
                        Exit Sub
                    End If
                End If

                If _TipoNota <> TipoNota Then
                    Select Case _TipoNota
                        Case (1)
                            Mensaje = "Solo se pueden escanear notas de REMISION en esta pantalla. Verifique."

                        Case (2)
                            Mensaje = "Solo se pueden escanear notas de BLANCAS en esta pantalla. Verifique."

                        Case (3)
                            Mensaje = "Solo se pueden escanear notas de EDIFICIOS en esta pantalla. Verifique."
                    End Select

                    MessageBox.Show(Mensaje, "Control de documentos", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                Dim Transaccion As SqlClient.SqlTransaction = Nothing

                Try
                    'Transaccion = SqlConnection.BeginTransaction
                    'cmdInsert.Transaction = Transaccion

                    Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

                    cmdInsert.CommandType = CommandType.Text
                    'If _TipoNota = 3 Then
                    '    'TODO: Generar procedimiento almacenado
                    '    'cmdInsert.CommandText = "Update Nota set Entregado=1, Extraviada=0, FStatus=getdate(), Status='LIQUIDADA' where RTrim(Serie)=@Serie and FolioNota=@FolioNota "
                    '    cmdInsert.CommandText = "Update Nota set Entregado=1, Extraviada=0, FStatus=getdate() where RTrim(Serie)=@Serie and FolioNota=@FolioNota "
                    'Else
                    '    cmdInsert.CommandText = "Update Nota set Entregado=1, Extraviada=0, FStatus=getdate() where RTrim(Serie)=@Serie and FolioNota=@FolioNota "
                    'End If
                    cmdInsert.CommandText = "spCDDevolucionNota"
                    cmdInsert.CommandType = CommandType.StoredProcedure
                    cmdInsert.Parameters.Clear()

                    cmdInsert.Parameters.Add("@Operacion", SqlDbType.TinyInt).Value = 2
                    cmdInsert.Parameters.Add("@Serie", SqlDbType.Char).Value = CType(Serie, String)
                    cmdInsert.Parameters.Add("@FolioNota", SqlDbType.Int).Value = CType(Folio, Integer)

                    cmdInsert.ExecuteNonQuery()

                    'Transaccion.Commit()
                    Me.Cursor = Cursors.Default
                    txtFolio.Text = ""
                Catch et As Data.SqlClient.SqlException
                    'Transaccion.Rollback()
                    MessageBox.Show(et.ToString, et.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            End If
        End If


    End Sub

    Private Sub SaveData(ByVal Serie As String, ByVal Folio As Integer, ByVal Connection As SqlClient.SqlConnection)
        'Guarda las notas a cancelar que no se encuentran en la base de datos JAGD27092005
        Try
            Dim altaDocumento As New ControlDocumentosDll.NotaCancelada(Serie, CInt(Val(Folio)), CType(_TipoNota, Short), Connection)
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error:" & Chr(13) & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub EntregaDocumento_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Sub txtFolio_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFolio.TextChanged

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click

    End Sub
End Class
