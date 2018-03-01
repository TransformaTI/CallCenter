Imports System.Data.SqlClient

Public Class frmAvanzaProgramacion
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private _Titulo As String = "Avanzar programación"

#Region " Windows Form Designer generated code "

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
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents lblCliente As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblProgramaCliente As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAvanzaProgramacion))
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.lblCliente = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblProgramaCliente = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtObservaciones
        '
        Me.txtObservaciones.AutoSize = False
        Me.txtObservaciones.Location = New System.Drawing.Point(8, 176)
        Me.txtObservaciones.MaxLength = 100
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.Size = New System.Drawing.Size(272, 56)
        Me.txtObservaciones.TabIndex = 1
        Me.txtObservaciones.Text = ""
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(296, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 2
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCliente
        '
        Me.lblCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCliente.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblCliente.Location = New System.Drawing.Point(8, 24)
        Me.lblCliente.Name = "lblCliente"
        Me.lblCliente.Size = New System.Drawing.Size(272, 21)
        Me.lblCliente.TabIndex = 0
        Me.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(296, 40)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(76, 14)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Observaciones"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 14)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Cliente"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 14)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Programación"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblProgramaCliente
        '
        Me.lblProgramaCliente.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgramaCliente.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblProgramaCliente.Location = New System.Drawing.Point(8, 72)
        Me.lblProgramaCliente.Name = "lblProgramaCliente"
        Me.lblProgramaCliente.Size = New System.Drawing.Size(272, 80)
        Me.lblProgramaCliente.TabIndex = 6
        '
        'frmAvanzaProgramacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(378, 239)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label3, Me.lblProgramaCliente, Me.Label2, Me.Label1, Me.btnCancelar, Me.lblCliente, Me.btnAceptar, Me.txtObservaciones})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAvanzaProgramacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Avanzar programación"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal Cliente As Integer)
        MyBase.New()
        InitializeComponent()

        _Cliente = Cliente

        Dim oCliente As SigaMetClasses.cCliente = Nothing

        Try
            oCliente = New SigaMetClasses.cCliente(_Cliente)
            lblCliente.Text = _Cliente.ToString & " " & oCliente.Nombre.Trim
            lblProgramaCliente.Text = oCliente.ProgramaClienteTexto
            btnAceptar.Enabled = oCliente.Programacion
        Catch
            btnAceptar.Enabled = False
        Finally
            If Not oCliente Is Nothing Then
                oCliente.Dispose()
            End If
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If MessageBox.Show("¿Está seguro que desea avanzar al siguiente ciclo de programación del cliente " & _Cliente.ToString & "?", _Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.Yes Then
            Cursor = Cursors.WaitCursor
            Dim cmd As New SqlCommand("spPROGAvanzaProgramacion", Main.CnnSigamet)
            With cmd
                .CommandType = CommandType.StoredProcedure
                .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                .Parameters.Add("@Observaciones", SqlDbType.VarChar, 100).Value = txtObservaciones.Text.Trim
            End With
            Try
                Main.AbreConexion()
            Catch ex As Exception
                MessageBox.Show(SigaMetClasses.M_NO_CONEXION, _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
                Main.CierraConexion()
                Cursor = Cursors.Default
                Exit Sub
            End Try

            Try
                cmd.ExecuteNonQuery()
                Me.DialogResult = DialogResult.OK

            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Cursor = Cursors.Default
                CierraConexion()

            End Try
        End If

    End Sub

End Class
