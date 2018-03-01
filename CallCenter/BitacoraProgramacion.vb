Public Class BitacoraProgramacion
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents ToolBar1 As System.Windows.Forms.ToolBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMes As System.Windows.Forms.ComboBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents cmdBitacoraProgramacion As System.Data.SqlClient.SqlCommand
    Friend WithEvents daBitacoraProgramacion As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsBitacoraProgramacion As Sigamet.dsBitacoraProgramacion
    Friend WithEvents dgBitacora As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lbAccion As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ToolBar1 = New System.Windows.Forms.ToolBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbMes = New System.Windows.Forms.ComboBox()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbAccion = New System.Windows.Forms.Label()
        Me.DsBitacoraProgramacion = New Sigamet.dsBitacoraProgramacion()
        Me.dgBitacora = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.cmdBitacoraProgramacion = New System.Data.SqlClient.SqlCommand()
        Me.daBitacoraProgramacion = New System.Data.SqlClient.SqlDataAdapter()
        Me.Panel1.SuspendLayout()
        CType(Me.DsBitacoraProgramacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolBar1
        '
        Me.ToolBar1.DropDownArrows = True
        Me.ToolBar1.Name = "ToolBar1"
        Me.ToolBar1.ShowToolTips = True
        Me.ToolBar1.Size = New System.Drawing.Size(642, 39)
        Me.ToolBar1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 11)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Mes : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbMes
        '
        Me.cmbMes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMes.Items.AddRange(New Object() {"ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE"})
        Me.cmbMes.Location = New System.Drawing.Point(56, 8)
        Me.cmbMes.Name = "cmbMes"
        Me.cmbMes.Size = New System.Drawing.Size(136, 21)
        Me.cmbMes.TabIndex = 2
        '
        'btnAceptar
        '
        Me.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnAceptar.Location = New System.Drawing.Point(560, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 3
        Me.btnAceptar.Text = "Cerrar"
        '
        'Panel1
        '
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbAccion})
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 266)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(642, 80)
        Me.Panel1.TabIndex = 4
        '
        'lbAccion
        '
        Me.lbAccion.BackColor = System.Drawing.Color.White
        Me.lbAccion.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DsBitacoraProgramacion, "Bitacora.Accion"))
        Me.lbAccion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbAccion.Name = "lbAccion"
        Me.lbAccion.Size = New System.Drawing.Size(642, 80)
        Me.lbAccion.TabIndex = 0
        '
        'DsBitacoraProgramacion
        '
        Me.DsBitacoraProgramacion.DataSetName = "dsBitacoraProgramacion"
        Me.DsBitacoraProgramacion.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsBitacoraProgramacion.Namespace = "http://www.tempuri.org/dsBitacoraProgramacion.xsd"
        '
        'dgBitacora
        '
        Me.dgBitacora.AlternatingBackColor = System.Drawing.Color.LightGray
        Me.dgBitacora.BackColor = System.Drawing.Color.Gainsboro
        Me.dgBitacora.BackgroundColor = System.Drawing.Color.Silver
        Me.dgBitacora.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgBitacora.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.dgBitacora.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.dgBitacora.CaptionText = "Movimientos de programación"
        Me.dgBitacora.DataMember = ""
        Me.dgBitacora.DataSource = Me.DsBitacoraProgramacion.Bitacora
        Me.dgBitacora.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgBitacora.FlatMode = True
        Me.dgBitacora.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgBitacora.ForeColor = System.Drawing.Color.Black
        Me.dgBitacora.GridLineColor = System.Drawing.Color.DimGray
        Me.dgBitacora.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.dgBitacora.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgBitacora.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgBitacora.HeaderForeColor = System.Drawing.Color.White
        Me.dgBitacora.LinkColor = System.Drawing.Color.MidnightBlue
        Me.dgBitacora.Location = New System.Drawing.Point(0, 39)
        Me.dgBitacora.Name = "dgBitacora"
        Me.dgBitacora.ParentRowsBackColor = System.Drawing.Color.DarkGray
        Me.dgBitacora.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgBitacora.ReadOnly = True
        Me.dgBitacora.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.dgBitacora.SelectionForeColor = System.Drawing.Color.White
        Me.dgBitacora.Size = New System.Drawing.Size(642, 227)
        Me.dgBitacora.TabIndex = 5
        Me.dgBitacora.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.LightGray
        Me.DataGridTableStyle1.BackColor = System.Drawing.Color.Gainsboro
        Me.DataGridTableStyle1.DataGrid = Me.dgBitacora
        Me.DataGridTableStyle1.ForeColor = System.Drawing.Color.Black
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3, Me.DataGridTextBoxColumn4, Me.DataGridTextBoxColumn5, Me.DataGridTextBoxColumn6})
        Me.DataGridTableStyle1.GridLineColor = System.Drawing.Color.DimGray
        Me.DataGridTableStyle1.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.Color.White
        Me.DataGridTableStyle1.LinkColor = System.Drawing.Color.MidnightBlue
        Me.DataGridTableStyle1.MappingName = "Bitacora"
        Me.DataGridTableStyle1.ReadOnly = True
        Me.DataGridTableStyle1.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.DataGridTableStyle1.SelectionForeColor = System.Drawing.Color.White
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Usuario"
        Me.DataGridTextBoxColumn1.MappingName = "Usuario"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 70
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Fecha"
        Me.DataGridTextBoxColumn2.MappingName = "FAccion"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 130
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Tipo"
        Me.DataGridTextBoxColumn3.MappingName = "Tipo"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 110
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.HeaderText = "Contrato"
        Me.DataGridTextBoxColumn4.MappingName = "Cliente"
        Me.DataGridTextBoxColumn4.NullText = ""
        Me.DataGridTextBoxColumn4.Width = 70
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.HeaderText = "Nombre"
        Me.DataGridTextBoxColumn5.MappingName = "Nombre"
        Me.DataGridTextBoxColumn5.NullText = ""
        Me.DataGridTextBoxColumn5.Width = 190
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.MappingName = "Accion"
        Me.DataGridTextBoxColumn6.Width = 0
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=DESARROLLO-4\MHUERTA;initial catalog=sigamet;persist security info=Fa" & _
        "lse;user id=sa;workstation id=DESARROLLO-4;packet size=4096"
        '
        'cmdBitacoraProgramacion
        '
        Me.cmdBitacoraProgramacion.CommandText = "SELECT BP.Usuario, BP.FAccion, BP.Accion, BP.Cliente, BP.Tipo, C.Nombre FROM Bita" & _
        "coraProgramacion BP INNER JOIN Cliente C ON BP.Cliente = C.Cliente WHERE (MONTH(" & _
        "BP.FAccion) = @Mes) ORDER BY BP.FAccion"
        Me.cmdBitacoraProgramacion.CommandTimeout = 30
        Me.cmdBitacoraProgramacion.Connection = Me.SqlConnection
        Me.cmdBitacoraProgramacion.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Mes", System.Data.SqlDbType.Decimal))
        '
        'daBitacoraProgramacion
        '
        Me.daBitacoraProgramacion.SelectCommand = Me.cmdBitacoraProgramacion
        '
        'BitacoraProgramacion
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnAceptar
        Me.ClientSize = New System.Drawing.Size(642, 346)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.dgBitacora, Me.Panel1, Me.btnAceptar, Me.cmbMes, Me.Label1, Me.ToolBar1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "BitacoraProgramacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bitacora de programación"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DsBitacoraProgramacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.Close()
    End Sub

    Private Sub BitacoraProgramacion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        cmbMes.SelectedIndex = Now.Date.Month - 1

        DsBitacoraProgramacion.Bitacora.Clear()
        cmdBitacoraProgramacion.Parameters("@Mes").Value = cmbMes.SelectedIndex + 1
        daBitacoraProgramacion.Fill(DsBitacoraProgramacion, "Bitacora")

    End Sub

    Private Sub dgBitacora_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgBitacora.CurrentCellChanged
        If DsBitacoraProgramacion.Bitacora.Rows.Count > 0 Then
            lbAccion.Text = CType(dgBitacora.Item(dgBitacora.CurrentRowIndex, 5), String)
        Else
            lbAccion.Text = ""
        End If
    End Sub

    Private Sub dgBitacora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgBitacora.Click
        If DsBitacoraProgramacion.Bitacora.Rows.Count > 0 Then
            lbAccion.Text = CType(dgBitacora.Item(dgBitacora.CurrentRowIndex, 5), String)
        Else
            lbAccion.Text = ""
        End If
    End Sub

    Private Sub cmbMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbMes.SelectedIndexChanged
        DsBitacoraProgramacion.Bitacora.Clear()
        cmdBitacoraProgramacion.Parameters("@Mes").Value = cmbMes.SelectedIndex + 1
        daBitacoraProgramacion.Fill(DsBitacoraProgramacion, "Bitacora")
    End Sub

End Class
