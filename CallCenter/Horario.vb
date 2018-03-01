Public Class Horario
    Inherits System.Windows.Forms.Form

    Private _numeroCelda As Integer

    Public Sub Entrada()

        Try
            SqlConnection.Close()
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        daRuta.Fill(DsHorarioRuta, "Ruta")

        DsHorarioRuta.Horario.Clear()
        cmdCHorario.Parameters("@RutaT").Value = 0
        daHorario.Fill(DsHorarioRuta, "Horario")

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
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cbRuta As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents daHorario As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCHorario As System.Data.SqlClient.SqlCommand
    Friend WithEvents daRuta As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCRuta As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsHorarioRuta As Sigamet.dsHorarioRuta
    Friend WithEvents dgHorario As System.Windows.Forms.DataGrid
    Friend WithEvents dgstHorario As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.cbRuta = New System.Windows.Forms.ComboBox()
        Me.DsHorarioRuta = New Sigamet.dsHorarioRuta()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.daHorario = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCHorario = New System.Data.SqlClient.SqlCommand()
        Me.daRuta = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCRuta = New System.Data.SqlClient.SqlCommand()
        Me.dgHorario = New System.Windows.Forms.DataGrid()
        Me.dgstHorario = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Panel1.SuspendLayout()
        CType(Me.DsHorarioRuta, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgHorario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightGray
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnCerrar, Me.cbRuta, Me.Label19})
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 56)
        Me.Panel1.TabIndex = 0
        '
        'btnCerrar
        '
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Location = New System.Drawing.Point(718, 14)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.TabIndex = 99
        Me.btnCerrar.Text = "Cerrar"
        '
        'cbRuta
        '
        Me.cbRuta.BackColor = System.Drawing.SystemColors.Window
        Me.cbRuta.DataSource = Me.DsHorarioRuta.Ruta
        Me.cbRuta.DisplayMember = "Descripcion"
        Me.cbRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRuta.Location = New System.Drawing.Point(48, 13)
        Me.cbRuta.Name = "cbRuta"
        Me.cbRuta.Size = New System.Drawing.Size(248, 21)
        Me.cbRuta.TabIndex = 97
        Me.cbRuta.ValueMember = "Ruta"
        '
        'DsHorarioRuta
        '
        Me.DsHorarioRuta.DataSetName = "dsHorarioRuta"
        Me.DsHorarioRuta.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsHorarioRuta.Namespace = "http://www.tempuri.org/dsHorarioRuta.xsd"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(8, 17)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(39, 14)
        Me.Label19.TabIndex = 98
        Me.Label19.Text = "Ruta :"
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=Digital5000;initial catalog=Sigamet;persist security info=False;user " & _
        "id=sa;workstation id=FHURTADO;packet size=4096"
        '
        'daHorario
        '
        Me.daHorario.SelectCommand = Me.cmdCHorario
        '
        'cmdCHorario
        '
        Me.cmdCHorario.CommandText = "EXEC sp_HorarioRuta @RutaT"
        Me.cmdCHorario.Connection = Me.SqlConnection
        Me.cmdCHorario.Parameters.Add(New System.Data.SqlClient.SqlParameter("@RutaT", System.Data.SqlDbType.Int))
        '
        'daRuta
        '
        Me.daRuta.SelectCommand = Me.cmdCRuta
        '
        'cmdCRuta
        '
        Me.cmdCRuta.CommandText = "select Ruta, Descripcion, Celula from Ruta Order by Descripcion"
        Me.cmdCRuta.Connection = Me.SqlConnection
        '
        'dgHorario
        '
        Me.dgHorario.AlternatingBackColor = System.Drawing.Color.LightGray
        Me.dgHorario.BackColor = System.Drawing.Color.White
        Me.dgHorario.BackgroundColor = System.Drawing.Color.LightGray
        Me.dgHorario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgHorario.CaptionBackColor = System.Drawing.Color.LightGoldenrodYellow
        Me.dgHorario.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dgHorario.CaptionForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgHorario.CaptionVisible = False
        Me.dgHorario.DataMember = ""
        Me.dgHorario.DataSource = Me.DsHorarioRuta.Horario
        Me.dgHorario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgHorario.FlatMode = True
        Me.dgHorario.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgHorario.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgHorario.GridLineColor = System.Drawing.Color.Peru
        Me.dgHorario.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.dgHorario.HeaderBackColor = System.Drawing.Color.Maroon
        Me.dgHorario.HeaderFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.dgHorario.HeaderForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.dgHorario.LinkColor = System.Drawing.Color.Maroon
        Me.dgHorario.Location = New System.Drawing.Point(0, 56)
        Me.dgHorario.Name = "dgHorario"
        Me.dgHorario.ParentRowsBackColor = System.Drawing.Color.BurlyWood
        Me.dgHorario.ParentRowsForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgHorario.ReadOnly = True
        Me.dgHorario.SelectionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgHorario.SelectionForeColor = System.Drawing.Color.GhostWhite
        Me.dgHorario.Size = New System.Drawing.Size(800, 438)
        Me.dgHorario.TabIndex = 103
        Me.dgHorario.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.dgstHorario})
        '
        'dgstHorario
        '
        Me.dgstHorario.AllowSorting = False
        Me.dgstHorario.AlternatingBackColor = System.Drawing.Color.LightGray
        Me.dgstHorario.BackColor = System.Drawing.Color.White
        Me.dgstHorario.DataGrid = Me.dgHorario
        Me.dgstHorario.ForeColor = System.Drawing.Color.DarkSlateBlue
        Me.dgstHorario.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8})
        Me.dgstHorario.GridLineColor = System.Drawing.Color.Peru
        Me.dgstHorario.HeaderBackColor = System.Drawing.Color.Maroon
        Me.dgstHorario.HeaderForeColor = System.Drawing.Color.LightGoldenrodYellow
        Me.dgstHorario.LinkColor = System.Drawing.Color.Maroon
        Me.dgstHorario.MappingName = "Horario"
        Me.dgstHorario.SelectionBackColor = System.Drawing.Color.DarkSlateBlue
        Me.dgstHorario.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Colonia"
        Me.DataGridTextBoxColumn1.MappingName = "Colonia"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 165
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Lunes"
        Me.DataGridTextBoxColumn2.MappingName = "Lunes"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 80
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Martes"
        Me.DataGridTextBoxColumn3.MappingName = "Martes"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 80
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Miercoles"
        Me.DataGridTextBoxColumn4.MappingName = "Miercoles"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 80
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Jueves"
        Me.DataGridTextBoxColumn5.MappingName = "Jueves"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 80
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Viernes"
        Me.DataGridTextBoxColumn6.MappingName = "Viernes"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 80
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Sabado"
        Me.DataGridTextBoxColumn7.MappingName = "Sabado"
        Me.DataGridTextBoxColumn7.NullText = ""
        Me.DataGridTextBoxColumn7.Width = 80
        '
        'DataGridTextBoxColumn8
        '
        Me.DataGridTextBoxColumn8.Format = ""
        Me.DataGridTextBoxColumn8.FormatInfo = Nothing
        Me.DataGridTextBoxColumn8.HeaderText = "Domingo"
        Me.DataGridTextBoxColumn8.MappingName = "Domingo"
        Me.DataGridTextBoxColumn8.NullText = ""
        Me.DataGridTextBoxColumn8.Width = 80
        '
        'Horario
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(800, 494)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.dgHorario, Me.Panel1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Horario"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Horario de las rutas"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DsHorarioRuta, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgHorario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub cbRuta_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbRuta.SelectedIndexChanged
        'DsHorarioRuta.Horario.DefaultView.RowFilter = "Ruta = " + CType(cbRuta.SelectedValue, String)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        DsHorarioRuta.Horario.Clear()
        cmdCHorario.Parameters("@RutaT").Value = cbRuta.SelectedValue
        daHorario.Fill(DsHorarioRuta, "Horario")
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    'Funcion para encontrar la referencia de la celda del grid de pedidos
    Public Function getCelda() As Integer
        Return _numeroCelda
    End Function

    'Funcion para referenciar la celda del grid de pedidos
    Public Function setCelda(ByVal numVal As Integer) As Integer
        _numeroCelda = numVal
    End Function

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub Horario_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Sub Panel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class
