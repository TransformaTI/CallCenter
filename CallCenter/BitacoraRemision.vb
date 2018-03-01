Public Class BitacoraRemision
    Inherits System.Windows.Forms.Form

    Private _Fecha As DateTime

    Public Sub Entrada(ByVal Fecha As DateTime)
        _Fecha = Fecha
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
    Friend WithEvents gbBitacora As System.Windows.Forms.GroupBox
    Friend WithEvents Panel31 As System.Windows.Forms.Panel
    Friend WithEvents dgBitacora As System.Windows.Forms.DataGrid
    Friend WithEvents Panel30 As System.Windows.Forms.Panel
    Friend WithEvents lbAccion As System.Windows.Forms.Label
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents daBitacora As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdBitacora As System.Data.SqlClient.SqlCommand
    Friend WithEvents DsBitacoraRemision As Sigamet.dsBitacoraRemision
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.gbBitacora = New System.Windows.Forms.GroupBox()
        Me.Panel31 = New System.Windows.Forms.Panel()
        Me.dgBitacora = New System.Windows.Forms.DataGrid()
        Me.DsBitacoraRemision = New Sigamet.dsBitacoraRemision()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.Panel30 = New System.Windows.Forms.Panel()
        Me.lbAccion = New System.Windows.Forms.Label()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.daBitacora = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdBitacora = New System.Data.SqlClient.SqlCommand()
        Me.gbBitacora.SuspendLayout()
        Me.Panel31.SuspendLayout()
        CType(Me.dgBitacora, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DsBitacoraRemision, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel30.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbBitacora
        '
        Me.gbBitacora.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel31, Me.Panel30})
        Me.gbBitacora.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbBitacora.Name = "gbBitacora"
        Me.gbBitacora.Size = New System.Drawing.Size(408, 270)
        Me.gbBitacora.TabIndex = 1
        Me.gbBitacora.TabStop = False
        '
        'Panel31
        '
        Me.Panel31.Controls.AddRange(New System.Windows.Forms.Control() {Me.dgBitacora})
        Me.Panel31.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel31.Location = New System.Drawing.Point(3, 17)
        Me.Panel31.Name = "Panel31"
        Me.Panel31.Size = New System.Drawing.Size(274, 250)
        Me.Panel31.TabIndex = 2
        '
        'dgBitacora
        '
        Me.dgBitacora.AllowSorting = False
        Me.dgBitacora.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.dgBitacora.BackColor = System.Drawing.Color.GhostWhite
        Me.dgBitacora.BackgroundColor = System.Drawing.Color.Lavender
        Me.dgBitacora.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgBitacora.CaptionBackColor = System.Drawing.Color.RoyalBlue
        Me.dgBitacora.CaptionForeColor = System.Drawing.Color.White
        Me.dgBitacora.CaptionVisible = False
        Me.dgBitacora.DataMember = ""
        Me.dgBitacora.DataSource = Me.DsBitacoraRemision.Bitacora
        Me.dgBitacora.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgBitacora.FlatMode = True
        Me.dgBitacora.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.dgBitacora.ForeColor = System.Drawing.Color.MidnightBlue
        Me.dgBitacora.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.dgBitacora.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.dgBitacora.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgBitacora.HeaderForeColor = System.Drawing.Color.Lavender
        Me.dgBitacora.LinkColor = System.Drawing.Color.Teal
        Me.dgBitacora.Name = "dgBitacora"
        Me.dgBitacora.ParentRowsBackColor = System.Drawing.Color.Lavender
        Me.dgBitacora.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.dgBitacora.ReadOnly = True
        Me.dgBitacora.SelectionBackColor = System.Drawing.Color.Teal
        Me.dgBitacora.SelectionForeColor = System.Drawing.Color.PaleGreen
        Me.dgBitacora.Size = New System.Drawing.Size(274, 250)
        Me.dgBitacora.TabIndex = 1
        Me.dgBitacora.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DsBitacoraRemision
        '
        Me.DsBitacoraRemision.DataSetName = "dsBitacoraRemision"
        Me.DsBitacoraRemision.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsBitacoraRemision.Namespace = "http://www.tempuri.org/dsBitacoraRemision.xsd"
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.AllowSorting = False
        Me.DataGridTableStyle1.AlternatingBackColor = System.Drawing.Color.GhostWhite
        Me.DataGridTableStyle1.BackColor = System.Drawing.Color.GhostWhite
        Me.DataGridTableStyle1.DataGrid = Me.dgBitacora
        Me.DataGridTableStyle1.ForeColor = System.Drawing.Color.MidnightBlue
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn3})
        Me.DataGridTableStyle1.GridLineColor = System.Drawing.Color.RoyalBlue
        Me.DataGridTableStyle1.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.Color.Lavender
        Me.DataGridTableStyle1.LinkColor = System.Drawing.Color.Teal
        Me.DataGridTableStyle1.MappingName = "Bitacora"
        Me.DataGridTableStyle1.ReadOnly = True
        Me.DataGridTableStyle1.SelectionBackColor = System.Drawing.Color.Teal
        Me.DataGridTableStyle1.SelectionForeColor = System.Drawing.Color.PaleGreen
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Usuario"
        Me.DataGridTextBoxColumn1.MappingName = "Usuario"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 75
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Format = ""
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "FAccion"
        Me.DataGridTextBoxColumn2.MappingName = "FAccion"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 120
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.HeaderText = "Accion"
        Me.DataGridTextBoxColumn3.MappingName = "Accion"
        Me.DataGridTextBoxColumn3.NullText = ""
        Me.DataGridTextBoxColumn3.Width = 0
        '
        'Panel30
        '
        Me.Panel30.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbAccion})
        Me.Panel30.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel30.Location = New System.Drawing.Point(277, 17)
        Me.Panel30.Name = "Panel30"
        Me.Panel30.Size = New System.Drawing.Size(128, 250)
        Me.Panel30.TabIndex = 1
        '
        'lbAccion
        '
        Me.lbAccion.BackColor = System.Drawing.Color.White
        Me.lbAccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbAccion.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbAccion.Name = "lbAccion"
        Me.lbAccion.Size = New System.Drawing.Size(128, 250)
        Me.lbAccion.TabIndex = 0
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=DELL2600;initial catalog=Sigamet;persist security info=False;user id=" & _
        "sa;workstation id=DESARROLLO-4;packet size=4096"
        '
        'daBitacora
        '
        Me.daBitacora.SelectCommand = Me.cmdBitacora
        '
        'cmdBitacora
        '
        Me.cmdBitacora.CommandText = "SELECT Usuario, Fecha, FAccion, Accion FROM BitacoraRemisiones WHERE (Fecha = @Fe" & _
        "cha)"
        Me.cmdBitacora.CommandTimeout = 30
        Me.cmdBitacora.Connection = Me.SqlConnection
        Me.cmdBitacora.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Fecha", System.Data.SqlDbType.DateTime))
        '
        'BitacoraRemision
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(408, 270)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.gbBitacora})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "BitacoraRemision"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bitacora de remisiones"
        Me.gbBitacora.ResumeLayout(False)
        Me.Panel31.ResumeLayout(False)
        CType(Me.dgBitacora, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DsBitacoraRemision, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel30.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub BitacoraRemision_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        cmdBitacora.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " + cmdBitacora.CommandText + " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
        cmdBitacora.Parameters("@Fecha").Value = _Fecha.Date
        daBitacora.Fill(DsBitacoraRemision, "Bitacora")

        If DsBitacoraRemision.Bitacora.Rows.Count > 0 Then
            dgBitacora.Select(0)
            lbAccion.Text = CType(dgBitacora.Item(dgBitacora.CurrentRowIndex, 2), String)
        Else
            lbAccion.Text = ""
        End If


        Me.Cursor = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub dgBitacora_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgBitacora.CurrentCellChanged
        If DsBitacoraRemision.Bitacora.Rows.Count > 0 Then
            lbAccion.Text = CType(dgBitacora.Item(dgBitacora.CurrentRowIndex, 2), String)
        Else
            lbAccion.Text = ""
        End If

    End Sub
End Class
