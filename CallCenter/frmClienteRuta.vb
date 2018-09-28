Imports System.Data.SqlClient

Public Class frmClienteRuta
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private _DatosCargados As Boolean

    'Para modificar el status de calidad
    Private _StatusCalidad As String
    'para no modificar clientes inactivos
    Private _Status As String

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
    Friend WithEvents cboRuta As SigaMetClasses.Combos.ComboRuta2
    Friend WithEvents imgLista As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TaskPane1 As VbPowerPack.TaskPane
    Friend WithEvents TaskFrame1 As VbPowerPack.TaskFrame
    Friend WithEvents ImageButton4 As VbPowerPack.ImageButton
    Friend WithEvents lnkModificar As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkConsultar As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkRefrescar As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCerrar As System.Windows.Forms.LinkLabel
    Friend WithEvents cboCelula As SigaMetClasses.Combos.ComboUsuarioCelula
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents grdCliente As System.Windows.Forms.DataGrid
    Friend WithEvents styClienteRuta As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents colCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colNombre As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colDireccionCompleta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colStatus As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colStatusCalidad As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colRamoCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colGiroCliente As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lnkCargarDatos As System.Windows.Forms.LinkLabel
    Friend WithEvents btnModificar As VbPowerPack.ImageButton
    Friend WithEvents btnRefrescar As VbPowerPack.ImageButton
    Friend WithEvents btnConsultar As VbPowerPack.ImageButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClienteRuta))
        Me.imgLista = New System.Windows.Forms.ImageList(Me.components)
        Me.cboRuta = New SigaMetClasses.Combos.ComboRuta2()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TaskPane1 = New VbPowerPack.TaskPane()
        Me.TaskFrame1 = New VbPowerPack.TaskFrame()
        Me.lnkCargarDatos = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboCelula = New SigaMetClasses.Combos.ComboUsuarioCelula()
        Me.lnkCerrar = New System.Windows.Forms.LinkLabel()
        Me.ImageButton4 = New VbPowerPack.ImageButton()
        Me.lnkRefrescar = New System.Windows.Forms.LinkLabel()
        Me.btnRefrescar = New VbPowerPack.ImageButton()
        Me.btnConsultar = New VbPowerPack.ImageButton()
        Me.lnkConsultar = New System.Windows.Forms.LinkLabel()
        Me.lnkModificar = New System.Windows.Forms.LinkLabel()
        Me.btnModificar = New VbPowerPack.ImageButton()
        Me.grdCliente = New System.Windows.Forms.DataGrid()
        Me.styClienteRuta = New System.Windows.Forms.DataGridTableStyle()
        Me.colCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colNombre = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colDireccionCompleta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colStatus = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colStatusCalidad = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colRamoCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colGiroCliente = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.TaskPane1.SuspendLayout()
        Me.TaskFrame1.SuspendLayout()
        CType(Me.grdCliente, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'imgLista
        '
        Me.imgLista.ImageStream = CType(resources.GetObject("imgLista.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgLista.TransparentColor = System.Drawing.Color.Transparent
        Me.imgLista.Images.SetKeyName(0, "")
        Me.imgLista.Images.SetKeyName(1, "")
        Me.imgLista.Images.SetKeyName(2, "")
        Me.imgLista.Images.SetKeyName(3, "")
        '
        'cboRuta
        '
        Me.cboRuta.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRuta.Location = New System.Drawing.Point(16, 160)
        Me.cboRuta.Name = "cboRuta"
        Me.cboRuta.Size = New System.Drawing.Size(144, 21)
        Me.cboRuta.TabIndex = 63
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 144)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 64
        Me.Label1.Text = "Ruta:"
        '
        'DataGridTextBoxColumn3
        '
        Me.DataGridTextBoxColumn3.Format = ""
        Me.DataGridTextBoxColumn3.FormatInfo = Nothing
        Me.DataGridTextBoxColumn3.Width = 75
        '
        'DataGridTextBoxColumn4
        '
        Me.DataGridTextBoxColumn4.Format = ""
        Me.DataGridTextBoxColumn4.FormatInfo = Nothing
        Me.DataGridTextBoxColumn4.Width = 75
        '
        'DataGridTextBoxColumn5
        '
        Me.DataGridTextBoxColumn5.Format = ""
        Me.DataGridTextBoxColumn5.FormatInfo = Nothing
        Me.DataGridTextBoxColumn5.Width = 75
        '
        'TaskPane1
        '
        Me.TaskPane1.AutoScroll = True
        Me.TaskPane1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.TaskPane1.Controls.Add(Me.TaskFrame1)
        Me.TaskPane1.CornerStyle = VbPowerPack.TaskFrameCornerStyle.SystemDefault
        Me.TaskPane1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TaskPane1.Location = New System.Drawing.Point(0, 0)
        Me.TaskPane1.Name = "TaskPane1"
        Me.TaskPane1.Size = New System.Drawing.Size(192, 413)
        Me.TaskPane1.TabIndex = 65
        '
        'TaskFrame1
        '
        Me.TaskFrame1.AllowDrop = True
        Me.TaskFrame1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TaskFrame1.CaptionBlend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Horizontal, System.Drawing.SystemColors.Window, System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer)))
        Me.TaskFrame1.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption
        Me.TaskFrame1.Controls.Add(Me.lnkCargarDatos)
        Me.TaskFrame1.Controls.Add(Me.Label2)
        Me.TaskFrame1.Controls.Add(Me.cboCelula)
        Me.TaskFrame1.Controls.Add(Me.lnkCerrar)
        Me.TaskFrame1.Controls.Add(Me.ImageButton4)
        Me.TaskFrame1.Controls.Add(Me.lnkRefrescar)
        Me.TaskFrame1.Controls.Add(Me.btnRefrescar)
        Me.TaskFrame1.Controls.Add(Me.btnConsultar)
        Me.TaskFrame1.Controls.Add(Me.lnkConsultar)
        Me.TaskFrame1.Controls.Add(Me.lnkModificar)
        Me.TaskFrame1.Controls.Add(Me.btnModificar)
        Me.TaskFrame1.Controls.Add(Me.cboRuta)
        Me.TaskFrame1.Controls.Add(Me.Label1)
        Me.TaskFrame1.Location = New System.Drawing.Point(12, 33)
        Me.TaskFrame1.Name = "TaskFrame1"
        Me.TaskFrame1.Size = New System.Drawing.Size(168, 250)
        Me.TaskFrame1.TabIndex = 1
        Me.TaskFrame1.Text = "Clientes por ruta"
        '
        'lnkCargarDatos
        '
        Me.lnkCargarDatos.LinkColor = System.Drawing.Color.Black
        Me.lnkCargarDatos.Location = New System.Drawing.Point(88, 184)
        Me.lnkCargarDatos.Name = "lnkCargarDatos"
        Me.lnkCargarDatos.Size = New System.Drawing.Size(72, 16)
        Me.lnkCargarDatos.TabIndex = 67
        Me.lnkCargarDatos.TabStop = True
        Me.lnkCargarDatos.Text = "Cargar datos"
        Me.lnkCargarDatos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 66
        Me.Label2.Text = "Célula:"
        '
        'cboCelula
        '
        Me.cboCelula.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCelula.Location = New System.Drawing.Point(16, 112)
        Me.cboCelula.Name = "cboCelula"
        Me.cboCelula.Size = New System.Drawing.Size(144, 21)
        Me.cboCelula.TabIndex = 65
        '
        'lnkCerrar
        '
        Me.lnkCerrar.LinkColor = System.Drawing.Color.Black
        Me.lnkCerrar.Location = New System.Drawing.Point(48, 224)
        Me.lnkCerrar.Name = "lnkCerrar"
        Me.lnkCerrar.Size = New System.Drawing.Size(100, 16)
        Me.lnkCerrar.TabIndex = 7
        Me.lnkCerrar.TabStop = True
        Me.lnkCerrar.Text = "Cerrar"
        Me.lnkCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ImageButton4
        '
        Me.ImageButton4.BackColor = System.Drawing.Color.Transparent
        Me.ImageButton4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ImageButton4.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.ImageButton4.Location = New System.Drawing.Point(16, 224)
        Me.ImageButton4.Name = "ImageButton4"
        Me.ImageButton4.NormalImage = CType(resources.GetObject("ImageButton4.NormalImage"), System.Drawing.Image)
        Me.ImageButton4.Size = New System.Drawing.Size(16, 16)
        Me.ImageButton4.SizeMode = VbPowerPack.ImageButtonSizeMode.AutoSize
        Me.ImageButton4.TabIndex = 6
        Me.ImageButton4.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lnkRefrescar
        '
        Me.lnkRefrescar.LinkColor = System.Drawing.Color.Black
        Me.lnkRefrescar.Location = New System.Drawing.Point(48, 64)
        Me.lnkRefrescar.Name = "lnkRefrescar"
        Me.lnkRefrescar.Size = New System.Drawing.Size(100, 16)
        Me.lnkRefrescar.TabIndex = 5
        Me.lnkRefrescar.TabStop = True
        Me.lnkRefrescar.Text = "Refrescar"
        Me.lnkRefrescar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRefrescar
        '
        Me.btnRefrescar.BackColor = System.Drawing.Color.Transparent
        Me.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnRefrescar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnRefrescar.Location = New System.Drawing.Point(16, 64)
        Me.btnRefrescar.Name = "btnRefrescar"
        Me.btnRefrescar.NormalImage = CType(resources.GetObject("btnRefrescar.NormalImage"), System.Drawing.Image)
        Me.btnRefrescar.Size = New System.Drawing.Size(16, 16)
        Me.btnRefrescar.SizeMode = VbPowerPack.ImageButtonSizeMode.AutoSize
        Me.btnRefrescar.TabIndex = 4
        Me.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'btnConsultar
        '
        Me.btnConsultar.BackColor = System.Drawing.Color.Transparent
        Me.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnConsultar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnConsultar.Enabled = False
        Me.btnConsultar.Location = New System.Drawing.Point(16, 40)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.NormalImage = CType(resources.GetObject("btnConsultar.NormalImage"), System.Drawing.Image)
        Me.btnConsultar.Size = New System.Drawing.Size(16, 16)
        Me.btnConsultar.SizeMode = VbPowerPack.ImageButtonSizeMode.AutoSize
        Me.btnConsultar.TabIndex = 3
        Me.btnConsultar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lnkConsultar
        '
        Me.lnkConsultar.Enabled = False
        Me.lnkConsultar.LinkColor = System.Drawing.Color.Black
        Me.lnkConsultar.Location = New System.Drawing.Point(48, 40)
        Me.lnkConsultar.Name = "lnkConsultar"
        Me.lnkConsultar.Size = New System.Drawing.Size(100, 16)
        Me.lnkConsultar.TabIndex = 2
        Me.lnkConsultar.TabStop = True
        Me.lnkConsultar.Text = "Consultar"
        Me.lnkConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lnkModificar
        '
        Me.lnkModificar.Enabled = False
        Me.lnkModificar.LinkColor = System.Drawing.Color.Black
        Me.lnkModificar.Location = New System.Drawing.Point(48, 16)
        Me.lnkModificar.Name = "lnkModificar"
        Me.lnkModificar.Size = New System.Drawing.Size(100, 16)
        Me.lnkModificar.TabIndex = 1
        Me.lnkModificar.TabStop = True
        Me.lnkModificar.Text = "Modificar"
        Me.lnkModificar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnModificar
        '
        Me.btnModificar.BackColor = System.Drawing.Color.Transparent
        Me.btnModificar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnModificar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnModificar.Enabled = False
        Me.btnModificar.Location = New System.Drawing.Point(16, 16)
        Me.btnModificar.Name = "btnModificar"
        Me.btnModificar.NormalImage = CType(resources.GetObject("btnModificar.NormalImage"), System.Drawing.Image)
        Me.btnModificar.Size = New System.Drawing.Size(16, 16)
        Me.btnModificar.SizeMode = VbPowerPack.ImageButtonSizeMode.AutoSize
        Me.btnModificar.TabIndex = 0
        Me.btnModificar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'grdCliente
        '
        Me.grdCliente.AlternatingBackColor = System.Drawing.Color.Lavender
        Me.grdCliente.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCliente.BackColor = System.Drawing.Color.WhiteSmoke
        Me.grdCliente.BackgroundColor = System.Drawing.Color.LightGray
        Me.grdCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdCliente.CaptionBackColor = System.Drawing.Color.LightSteelBlue
        Me.grdCliente.CaptionForeColor = System.Drawing.Color.MidnightBlue
        Me.grdCliente.DataMember = ""
        Me.grdCliente.FlatMode = True
        Me.grdCliente.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grdCliente.ForeColor = System.Drawing.Color.MidnightBlue
        Me.grdCliente.GridLineColor = System.Drawing.Color.Gainsboro
        Me.grdCliente.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
        Me.grdCliente.HeaderBackColor = System.Drawing.Color.MidnightBlue
        Me.grdCliente.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.grdCliente.HeaderForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdCliente.LinkColor = System.Drawing.Color.Teal
        Me.grdCliente.Location = New System.Drawing.Point(192, 0)
        Me.grdCliente.Name = "grdCliente"
        Me.grdCliente.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.grdCliente.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
        Me.grdCliente.ReadOnly = True
        Me.grdCliente.SelectionBackColor = System.Drawing.Color.CadetBlue
        Me.grdCliente.SelectionForeColor = System.Drawing.Color.WhiteSmoke
        Me.grdCliente.Size = New System.Drawing.Size(536, 416)
        Me.grdCliente.TabIndex = 1
        Me.grdCliente.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.styClienteRuta})
        '
        'styClienteRuta
        '
        Me.styClienteRuta.DataGrid = Me.grdCliente
        Me.styClienteRuta.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.styClienteRuta.MappingName = "Cliente"
        Me.styClienteRuta.ReadOnly = True
        Me.styClienteRuta.RowHeadersVisible = False
        '
        'colCliente
        '
        Me.colCliente.Format = ""
        Me.colCliente.FormatInfo = Nothing
        Me.colCliente.HeaderText = "Cliente"
        Me.colCliente.MappingName = "Cliente"
        Me.colCliente.Width = 90
        '
        'colNombre
        '
        Me.colNombre.Format = ""
        Me.colNombre.FormatInfo = Nothing
        Me.colNombre.HeaderText = "Nombre"
        Me.colNombre.MappingName = "Nombre"
        Me.colNombre.Width = 250
        '
        'colDireccionCompleta
        '
        Me.colDireccionCompleta.Format = ""
        Me.colDireccionCompleta.FormatInfo = Nothing
        Me.colDireccionCompleta.HeaderText = "Dirección"
        Me.colDireccionCompleta.MappingName = "DireccionCompleta"
        Me.colDireccionCompleta.Width = 200
        '
        'colStatus
        '
        Me.colStatus.Format = ""
        Me.colStatus.FormatInfo = Nothing
        Me.colStatus.HeaderText = "Estatus"
        Me.colStatus.MappingName = "Status"
        Me.colStatus.Width = 75
        '
        'colStatusCalidad
        '
        Me.colStatusCalidad.Format = ""
        Me.colStatusCalidad.FormatInfo = Nothing
        Me.colStatusCalidad.HeaderText = "E.Calidad"
        Me.colStatusCalidad.MappingName = "StatusCalidad"
        Me.colStatusCalidad.Width = 75
        '
        'colRamoCliente
        '
        Me.colRamoCliente.Format = ""
        Me.colRamoCliente.FormatInfo = Nothing
        Me.colRamoCliente.HeaderText = "Ramo del cliente"
        Me.colRamoCliente.MappingName = "RamoClienteDescripcion"
        Me.colRamoCliente.Width = 75
        '
        'colGiroCliente
        '
        Me.colGiroCliente.Format = ""
        Me.colGiroCliente.FormatInfo = Nothing
        Me.colGiroCliente.HeaderText = "Giro del cliente"
        Me.colGiroCliente.MappingName = "GiroClienteDescripcion"
        Me.colGiroCliente.Width = 75
        '
        'frmClienteRuta
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(728, 413)
        Me.Controls.Add(Me.TaskPane1)
        Me.Controls.Add(Me.grdCliente)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmClienteRuta"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Clientes por ruta"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TaskPane1.ResumeLayout(False)
        Me.TaskFrame1.ResumeLayout(False)
        Me.TaskFrame1.PerformLayout()
        CType(Me.grdCliente, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CargaDatos()
        Cursor = Cursors.WaitCursor
        lnkModificar.Enabled = False
        btnModificar.Enabled = False
        lnkConsultar.Enabled = False
        btnConsultar.Enabled = False

        Dim cmd As New SqlCommand("spCCConsultaClienteRuta")
        With cmd
            .CommandTimeout = 120
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = CType(cboRuta.SelectedValue, Short)
            .Connection = CnnSigamet
        End With

        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable("Cliente")
        Try
            AbreConexion()

            da.Fill(dt)


            If dt.Rows.Count > 0 Then
                grdCliente.DataSource = dt
                grdCliente.CaptionText = "Lista de clientes de " & cboRuta.Text.Trim & " (" & dt.Rows.Count.ToString & " en total)"
            Else
                grdCliente.DataSource = Nothing
                grdCliente.CaptionText = "No existen clientes en la ruta seleccionada"
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            Cursor = Cursors.Default
            da.Dispose()
            cmd.Dispose()
        End Try

        'Info de rutas inactivas
        Dim RutaWarning As New SigaMetClasses.frmWarning(CType(cboRuta.SelectedValue, Integer))
    End Sub


    Private Sub frmClienteRuta_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboCelula.CargaDatos(Main.GLOBAL_Usuario)
        If cboCelula.Items.Count > 0 Then
            cboCelula.SelectedIndex = 0
        End If
        Dim _shrCelula As Short = CType(cboCelula.SelectedValue, Short)
        cboRuta.CargaDatos(_shrCelula)
        _DatosCargados = True
    End Sub

    Private Sub Modificar()
        If _Cliente <> 0 Then
            If Not GLOBAL_ManejoClientesInactivos AndAlso _
                _StatusCalidad.Trim.ToUpper = "INACTIVO" AndAlso Not oSeguridad.TieneAcceso("Calidad") Then
                MessageBox.Show("El cliente no puede ser modificado porque se encuentra INACTIVO.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If _StatusCalidad <> "VERIFICADO" Or oSeguridad.TieneAcceso("Calidad") Then
                Cursor = Cursors.WaitCursor
                Dim oModifica As New SigaMetClasses.ModificaCliente(_Cliente, GLOBAL_Usuario, PermiteModificarStatus:=oSeguridad.TieneAcceso("Calidad"), _
                    PermiteModificarStatusCalidad:=oSeguridad.TieneAcceso("ModificarStatusCalidad"))
                If oModifica.ShowDialog() = DialogResult.OK Then
                    CargaDatos()
                End If
                Cursor = Cursors.Default
            Else
                MessageBox.Show("El cliente no puede ser modificado porque tiene estatus VERIFICADO.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub Consultar()
        If _Cliente <> 0 Then
            Cursor = Cursors.WaitCursor
            Dim oConsultaCliente As New SigaMetClasses.frmConsultaCliente(_Cliente, PermiteCapturarNotas:=False, Nuevo:=0)
            oConsultaCliente.ShowDialog()
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Refrescar()
        CargaDatos()
    End Sub

    Private Sub grdCliente_CurrentCellChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdCliente.CurrentCellChanged
        grdCliente.Select(grdCliente.CurrentRowIndex)
        Try
            _Cliente = CType(grdCliente.Item(grdCliente.CurrentRowIndex, 0), Integer)
            _StatusCalidad = CType(grdCliente.Item(grdCliente.CurrentRowIndex, 4), String).Trim
            'Evitar la modificación de clientes inactivos
            _Status = CType(grdCliente.Item(grdCliente.CurrentRowIndex, 3), String).Trim
            lnkModificar.Enabled = True
            btnModificar.Enabled = True
            lnkConsultar.Enabled = True
            btnConsultar.Enabled = True
        Catch
            _Cliente = 0
            _StatusCalidad = ""
            lnkModificar.Enabled = False
            btnModificar.Enabled = False
            lnkConsultar.Enabled = False
            btnConsultar.Enabled = False
        End Try

    End Sub

    Private Sub lnkCerrar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCerrar.LinkClicked
        Me.Close()
    End Sub

    Private Sub lnkRefrescar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefrescar.LinkClicked
        Me.Refrescar()
    End Sub

    Private Sub btnRefrescar_Click() Handles btnRefrescar.Click
        Me.Refrescar()
    End Sub

    Private Sub lnkConsultar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkConsultar.LinkClicked
        Me.Consultar()
    End Sub

    Private Sub lnkModificar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkModificar.LinkClicked
        Me.Modificar()
    End Sub

    Private Sub cboCelula_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCelula.SelectedIndexChanged
        If _DatosCargados Then
            cboRuta.CargaDatos(CType(cboCelula.SelectedValue, Short))
            If cboRuta.Items.Count > 0 Then
                cboRuta.SelectedIndex = 0
            End If
        End If
    End Sub


    Private Sub lnkCargarDatos_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCargarDatos.LinkClicked
        Me.CargaDatos()
    End Sub



End Class
