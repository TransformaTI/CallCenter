Imports System.Data.SqlClient
Public Class frmBitacoraUsuario
    Inherits System.Windows.Forms.Form
    Private _Usuario As String
    Private _DatosCargados As Boolean = False
    Private _Columna As Integer
    Private _Cliente As Integer
    Private _Portatil As Boolean

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
    Friend WithEvents TaskPane1 As VbPowerPack.TaskPane
    Friend WithEvents TaskFrame1 As VbPowerPack.TaskFrame
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents dtpFLlamada As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lvwBitacoraUsuario As System.Windows.Forms.ListView
    Friend WithEvents colFLlamada As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRuta As System.Windows.Forms.ColumnHeader
    Friend WithEvents colCliente As System.Windows.Forms.ColumnHeader
    Friend WithEvents lnkCerrar As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkRefrescar As System.Windows.Forms.LinkLabel
    Friend WithEvents btnRefrescar As VbPowerPack.ImageButton
    Friend WithEvents colDomicilio As System.Windows.Forms.ColumnHeader
    Friend WithEvents colTelefonoOrigen As System.Windows.Forms.ColumnHeader
    Friend WithEvents colMotivoLlamadaDescripcion As System.Windows.Forms.ColumnHeader
    Friend WithEvents colUsuario As System.Windows.Forms.ColumnHeader
    Friend WithEvents btnCerrar As VbPowerPack.ImageButton
    Friend WithEvents TaskFrame2 As VbPowerPack.TaskFrame
    Friend WithEvents lblEstatus As System.Windows.Forms.Label
    Friend WithEvents lvwBitacoraUsuarioResumen As System.Windows.Forms.ListView
    Friend WithEvents colRESMotivoLlamadaDescripcion As System.Windows.Forms.ColumnHeader
    Friend WithEvents colRESTotal As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboUsuarioCelula As SigaMetClasses.Combos.ComboUsuarioCelulaControl
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents colTipoCliente As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmBitacoraUsuario))
        Me.TaskPane1 = New VbPowerPack.TaskPane()
        Me.TaskFrame1 = New VbPowerPack.TaskFrame()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.cboUsuarioCelula = New SigaMetClasses.Combos.ComboUsuarioCelulaControl()
        Me.lnkRefrescar = New System.Windows.Forms.LinkLabel()
        Me.btnRefrescar = New VbPowerPack.ImageButton()
        Me.lnkCerrar = New System.Windows.Forms.LinkLabel()
        Me.btnCerrar = New VbPowerPack.ImageButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFLlamada = New System.Windows.Forms.DateTimePicker()
        Me.TaskFrame2 = New VbPowerPack.TaskFrame()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lvwBitacoraUsuarioResumen = New System.Windows.Forms.ListView()
        Me.colRESMotivoLlamadaDescripcion = New System.Windows.Forms.ColumnHeader()
        Me.colRESTotal = New System.Windows.Forms.ColumnHeader()
        Me.lblEstatus = New System.Windows.Forms.Label()
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.lvwBitacoraUsuario = New System.Windows.Forms.ListView()
        Me.colFLlamada = New System.Windows.Forms.ColumnHeader()
        Me.colRuta = New System.Windows.Forms.ColumnHeader()
        Me.colCliente = New System.Windows.Forms.ColumnHeader()
        Me.colDomicilio = New System.Windows.Forms.ColumnHeader()
        Me.colTelefonoOrigen = New System.Windows.Forms.ColumnHeader()
        Me.colMotivoLlamadaDescripcion = New System.Windows.Forms.ColumnHeader()
        Me.colUsuario = New System.Windows.Forms.ColumnHeader()
        Me.colTipoCliente = New System.Windows.Forms.ColumnHeader()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TaskPane1.SuspendLayout()
        Me.TaskFrame1.SuspendLayout()
        Me.TaskFrame2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TaskPane1
        '
        Me.TaskPane1.AutoScroll = True
        Me.TaskPane1.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.TaskPane1.Controls.AddRange(New System.Windows.Forms.Control() {Me.TaskFrame1, Me.TaskFrame2})
        Me.TaskPane1.CornerStyle = VbPowerPack.TaskFrameCornerStyle.SystemDefault
        Me.TaskPane1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TaskPane1.Name = "TaskPane1"
        Me.TaskPane1.Size = New System.Drawing.Size(208, 565)
        Me.TaskPane1.TabIndex = 0
        '
        'TaskFrame1
        '
        Me.TaskFrame1.AllowDrop = True
        Me.TaskFrame1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TaskFrame1.CaptionBlend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.Lavender, System.Drawing.Color.LightSkyBlue)
        Me.TaskFrame1.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption
        Me.TaskFrame1.Controls.AddRange(New System.Windows.Forms.Control() {Me.lblUsuario, Me.cboUsuarioCelula, Me.lnkRefrescar, Me.btnRefrescar, Me.lnkCerrar, Me.btnCerrar, Me.Label1, Me.dtpFLlamada})
        Me.TaskFrame1.Location = New System.Drawing.Point(12, 33)
        Me.TaskFrame1.Name = "TaskFrame1"
        Me.TaskFrame1.Size = New System.Drawing.Size(184, 180)
        Me.TaskFrame1.TabIndex = 1
        Me.TaskFrame1.Text = "Bitácora del usuario"
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Location = New System.Drawing.Point(16, 8)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(64, 14)
        Me.lblUsuario.TabIndex = 13
        Me.lblUsuario.Text = "Del usuario:"
        '
        'cboUsuarioCelula
        '
        Me.cboUsuarioCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUsuarioCelula.DropDownWidth = 250
        Me.cboUsuarioCelula.Location = New System.Drawing.Point(16, 24)
        Me.cboUsuarioCelula.MaxDropDownItems = 10
        Me.cboUsuarioCelula.Name = "cboUsuarioCelula"
        Me.cboUsuarioCelula.Nombre = Nothing
        Me.cboUsuarioCelula.Size = New System.Drawing.Size(152, 21)
        Me.cboUsuarioCelula.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cboUsuarioCelula, "Lista de usuarios de la(s) célula(s)")
        Me.cboUsuarioCelula.Usuario = Nothing
        Me.cboUsuarioCelula.UsuarioNombre = Nothing
        '
        'lnkRefrescar
        '
        Me.lnkRefrescar.LinkColor = System.Drawing.Color.Black
        Me.lnkRefrescar.Location = New System.Drawing.Point(48, 104)
        Me.lnkRefrescar.Name = "lnkRefrescar"
        Me.lnkRefrescar.Size = New System.Drawing.Size(100, 16)
        Me.lnkRefrescar.TabIndex = 11
        Me.lnkRefrescar.TabStop = True
        Me.lnkRefrescar.Text = "Refrescar"
        Me.lnkRefrescar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnRefrescar
        '
        Me.btnRefrescar.BackColor = System.Drawing.Color.Transparent
        Me.btnRefrescar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnRefrescar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnRefrescar.Location = New System.Drawing.Point(16, 104)
        Me.btnRefrescar.Name = "btnRefrescar"
        Me.btnRefrescar.NormalImage = CType(resources.GetObject("btnRefrescar.NormalImage"), System.Drawing.Bitmap)
        Me.btnRefrescar.Size = New System.Drawing.Size(16, 16)
        Me.btnRefrescar.SizeMode = VbPowerPack.ImageButtonSizeMode.AutoSize
        Me.btnRefrescar.TabIndex = 10
        Me.btnRefrescar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'lnkCerrar
        '
        Me.lnkCerrar.LinkColor = System.Drawing.Color.Black
        Me.lnkCerrar.Location = New System.Drawing.Point(48, 144)
        Me.lnkCerrar.Name = "lnkCerrar"
        Me.lnkCerrar.Size = New System.Drawing.Size(100, 16)
        Me.lnkCerrar.TabIndex = 9
        Me.lnkCerrar.TabStop = True
        Me.lnkCerrar.Text = "Cerrar"
        Me.lnkCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.Color.Transparent
        Me.btnCerrar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnCerrar.Location = New System.Drawing.Point(16, 144)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.NormalImage = CType(resources.GetObject("btnCerrar.NormalImage"), System.Drawing.Bitmap)
        Me.btnCerrar.Size = New System.Drawing.Size(16, 16)
        Me.btnCerrar.SizeMode = VbPowerPack.ImageButtonSizeMode.AutoSize
        Me.btnCerrar.TabIndex = 8
        Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 14)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Del día:"
        '
        'dtpFLlamada
        '
        Me.dtpFLlamada.Format = System.Windows.Forms.DateTimePickerFormat.Short
        Me.dtpFLlamada.Location = New System.Drawing.Point(16, 64)
        Me.dtpFLlamada.Name = "dtpFLlamada"
        Me.dtpFLlamada.Size = New System.Drawing.Size(152, 21)
        Me.dtpFLlamada.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.dtpFLlamada, "Día de la bitácora")
        '
        'TaskFrame2
        '
        Me.TaskFrame2.AllowDrop = True
        Me.TaskFrame2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.TaskFrame2.CaptionBlend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.Color.Lavender, System.Drawing.Color.LightSkyBlue)
        Me.TaskFrame2.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption
        Me.TaskFrame2.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label2, Me.lvwBitacoraUsuarioResumen, Me.lblEstatus})
        Me.TaskFrame2.Location = New System.Drawing.Point(12, 246)
        Me.TaskFrame2.Name = "TaskFrame2"
        Me.TaskFrame2.Size = New System.Drawing.Size(184, 300)
        Me.TaskFrame2.TabIndex = 3
        Me.TaskFrame2.Text = "Información"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 14)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Resúmen:"
        '
        'lvwBitacoraUsuarioResumen
        '
        Me.lvwBitacoraUsuarioResumen.Anchor = ((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right)
        Me.lvwBitacoraUsuarioResumen.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colRESMotivoLlamadaDescripcion, Me.colRESTotal})
        Me.lvwBitacoraUsuarioResumen.Location = New System.Drawing.Point(8, 104)
        Me.lvwBitacoraUsuarioResumen.Name = "lvwBitacoraUsuarioResumen"
        Me.lvwBitacoraUsuarioResumen.Size = New System.Drawing.Size(168, 184)
        Me.lvwBitacoraUsuarioResumen.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lvwBitacoraUsuarioResumen, "Resúmen de los registros de la bitácora")
        Me.lvwBitacoraUsuarioResumen.View = System.Windows.Forms.View.Details
        '
        'colRESMotivoLlamadaDescripcion
        '
        Me.colRESMotivoLlamadaDescripcion.Text = "Motivo llamada"
        Me.colRESMotivoLlamadaDescripcion.Width = 100
        '
        'colRESTotal
        '
        Me.colRESTotal.Text = "Total"
        Me.colRESTotal.Width = 40
        '
        'lblEstatus
        '
        Me.lblEstatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstatus.Location = New System.Drawing.Point(8, 8)
        Me.lblEstatus.Name = "lblEstatus"
        Me.lblEstatus.Size = New System.Drawing.Size(168, 80)
        Me.lblEstatus.TabIndex = 0
        Me.lblEstatus.Text = "Información de la bitácora"
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(208, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 565)
        Me.Splitter1.TabIndex = 1
        Me.Splitter1.TabStop = False
        '
        'lvwBitacoraUsuario
        '
        Me.lvwBitacoraUsuario.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.lvwBitacoraUsuario.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colFLlamada, Me.colRuta, Me.colCliente, Me.colDomicilio, Me.colTelefonoOrigen, Me.colMotivoLlamadaDescripcion, Me.colUsuario, Me.colTipoCliente})
        Me.lvwBitacoraUsuario.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lvwBitacoraUsuario.FullRowSelect = True
        Me.lvwBitacoraUsuario.Location = New System.Drawing.Point(211, 0)
        Me.lvwBitacoraUsuario.Name = "lvwBitacoraUsuario"
        Me.lvwBitacoraUsuario.Size = New System.Drawing.Size(653, 565)
        Me.lvwBitacoraUsuario.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lvwBitacoraUsuario, "Dé doble clic para abrir la ventana de CallCenter para este cliente")
        Me.lvwBitacoraUsuario.View = System.Windows.Forms.View.Details
        '
        'colFLlamada
        '
        Me.colFLlamada.Text = "F.Llamada"
        Me.colFLlamada.Width = 80
        '
        'colRuta
        '
        Me.colRuta.Text = "Ruta"
        '
        'colCliente
        '
        Me.colCliente.Text = "Cliente"
        Me.colCliente.Width = 90
        '
        'colDomicilio
        '
        Me.colDomicilio.Text = "Domicilio"
        Me.colDomicilio.Width = 200
        '
        'colTelefonoOrigen
        '
        Me.colTelefonoOrigen.Text = "Orígen"
        Me.colTelefonoOrigen.Width = 80
        '
        'colMotivoLlamadaDescripcion
        '
        Me.colMotivoLlamadaDescripcion.Text = "Motivo de llamada"
        Me.colMotivoLlamadaDescripcion.Width = 180
        '
        'colUsuario
        '
        Me.colUsuario.Text = "Usuario"
        '
        'colTipoCliente
        '
        Me.colTipoCliente.Text = "Tipo"
        Me.colTipoCliente.Width = 15
        '
        'frmBitacoraUsuario
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(864, 565)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.lvwBitacoraUsuario, Me.Splitter1, Me.TaskPane1})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBitacoraUsuario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bitácora del Usuario"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TaskPane1.ResumeLayout(False)
        Me.TaskFrame1.ResumeLayout(False)
        Me.TaskFrame2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal Usuario As String)
        MyBase.New()
        InitializeComponent()

        _Usuario = Usuario

        dtpFLlamada.Value = SigaMetClasses.FechaServidor.Date
        dtpFLlamada.MaxDate = dtpFLlamada.Value

        If oSeguridad.TieneAcceso("BITACORA_SUPERVISOR") Then
            cboUsuarioCelula.CargaDatos(_Usuario, GLOBAL_ConString)
            If cboUsuarioCelula.Items.Count <= 0 Then
                cboUsuarioCelula.Enabled = False
            End If
            cboUsuarioCelula.SelectedValue = _Usuario
        Else
            lblUsuario.Text = Main.GLOBAL_UsuarioNombre
            lblUsuario.Font = New Font("Tahoma", 8, FontStyle.Bold)
            cboUsuarioCelula.Visible = False
        End If

    End Sub


    Private Sub CargaDatos()
        Dim oSplash As New SigaMetClasses.frmWait()
        oSplash.Owner = Me
        oSplash.Show()
        oSplash.Refresh()


        Cursor = Cursors.WaitCursor
        Dim cmd As New SqlCommand("spCCBitacoraUsuario", Main.CnnSigamet)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Clear()
            .Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = _Usuario
            .Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dtpFLlamada.Value.Date
        End With
        Dim dr As SqlDataReader
        Dim item As ListViewItem = Nothing

        Try
            AbreConexion()
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            lvwBitacoraUsuario.Items.Clear()

            While dr.Read
                If Not IsDBNull(dr("FLLamada")) Then
                    item = New ListViewItem(CType(dr("FLLamada"), Date).ToShortTimeString)
                Else
                    item.SubItems.Add("")
                End If

                Try
                    item.ForeColor = System.Drawing.Color.FromName(CType(dr("Color"), String).Trim)
                Catch
                    item.ForeColor = Color.Black
                End Try

                If Not IsDBNull(dr("RutaDescripcion")) Then
                    item.SubItems.Add(CType(dr("RutaDescripcion"), String).Trim)
                Else
                    item.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Cliente")) Then
                    item.SubItems.Add(CType(dr("Cliente"), String))
                Else
                    item.SubItems.Add("")
                End If

                If Not IsDBNull(dr("DomicilioCompleto")) Then
                    item.SubItems.Add(CType(dr("DomicilioCompleto"), String).Trim)
                Else
                    item.SubItems.Add("")
                End If

                If Not IsDBNull(dr("TelefonoOrigen")) Then
                    item.SubItems.Add(CType(dr("TelefonoOrigen"), String).Trim)
                Else
                    item.SubItems.Add("")
                End If

                If Not IsDBNull(dr("MotivoLLamadaDescripcion")) Then
                    item.SubItems.Add(CType(dr("MotivoLLamadaDescripcion"), String).Trim)
                Else
                    item.SubItems.Add("")
                End If

                If Not IsDBNull(dr("Usuario")) Then
                    item.SubItems.Add(CType(dr("Usuario"), String).Trim)
                Else
                    item.SubItems.Add("")
                End If

                item.SubItems.Add("E")

                lvwBitacoraUsuario.Items.Add(item)

            End While

            If lvwBitacoraUsuario.Items.Count > 0 Then
                lblEstatus.Text = "Bitácora del usuario: " & _Usuario & _
                        " del día: " & _
                        Me.dtpFLlamada.Value.ToLongDateString & _
                        " (" & lvwBitacoraUsuario.Items.Count.ToString & " registro(s))"
            Else
                lblEstatus.Text = "No existen registros en la bitácora del usuario " & _Usuario & " del día: " & Me.dtpFLlamada.Value.ToLongDateString
            End If

            dr.NextResult()

            'Resúmen de la Bitácora
            'dr.Close()
            'CierraConexion()
            'cmd.CommandText = "spCCBitacoraUsuarioResumen"

            'AbreConexion()

            'dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            lvwBitacoraUsuarioResumen.Items.Clear()

            While dr.Read
                item = New ListViewItem(CType(dr("MotivoLLamadaDescripcion"), String).Trim)
                item.SubItems.Add(CType(dr("Total"), Decimal).ToString)
                item.ForeColor = Color.FromName(CType(dr("Color"), String))

                lvwBitacoraUsuarioResumen.Items.Add(item)
            End While

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            CierraConexion()
            oSplash.Close()
            oSplash.Dispose()

        End Try
        If GLOBAL_ManejarClientesPortatil Then
            CargaDatosPortatil()
        End If
    End Sub


    Private Sub dtpFLlamada_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFLlamada.ValueChanged
        If _DatosCargados Then CargaDatos()

    End Sub


    Private Sub CargaDatosPortatil()
        Dim oSplash As New SigaMetClasses.frmWait()
        oSplash.Owner = Me
        oSplash.Show()
        oSplash.Refresh()


        Cursor = Cursors.WaitCursor
        Dim cmd As New SqlCommand("spCCBitacoraUsuarioPortatil", Main.CnnSigamet)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Clear()
            .Parameters.Add("@Usuario", SqlDbType.VarChar, 15).Value = _Usuario
            .Parameters.Add("@Fecha", SqlDbType.DateTime).Value = dtpFLlamada.Value.Date
        End With
        Dim dr As SqlDataReader
        Dim item As ListViewItem

        Try
            AbreConexion()
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            'lvwBitacoraUsuario.Items.Clear()

            While dr.Read
                item = New ListViewItem(CType(dr("FLLamada"), Date).ToShortTimeString)
                Try
                    item.ForeColor = System.Drawing.Color.FromName(CType(dr("Color"), String).Trim)
                Catch
                    item.ForeColor = Color.Black
                End Try

                item.SubItems.Add(CType(dr("RutaDescripcion"), String).Trim)
                item.SubItems.Add(CType(dr("Cliente"), String))
                item.SubItems.Add(CType(dr("DomicilioCompleto"), String).Trim)
                If Not IsDBNull(dr("TelefonoOrigen")) Then
                    item.SubItems.Add(CType(dr("TelefonoOrigen"), String).Trim)
                Else
                    item.SubItems.Add("")
                End If
                item.SubItems.Add(CType(dr("MotivoLLamadaDescripcion"), String).Trim)
                If Not IsDBNull(dr("Usuario")) Then
                    item.SubItems.Add(CType(dr("Usuario"), String).Trim)
                Else
                    item.SubItems.Add("")
                End If

                item.SubItems.Add("P")

                lvwBitacoraUsuario.Items.Add(item)

            End While

            If lvwBitacoraUsuario.Items.Count > 0 Then
                lblEstatus.Text = "Bitácora del usuario: " & _Usuario & _
                        " del día: " & _
                        Me.dtpFLlamada.Value.ToLongDateString & _
                        " (" & lvwBitacoraUsuario.Items.Count.ToString & " registro(s))"
            Else
                lblEstatus.Text = "No existen registros en la bitácora del usuario " & _Usuario & " del día: " & Me.dtpFLlamada.Value.ToLongDateString
            End If

            dr.NextResult()

            'Resúmen de la Bitácora
            'dr.Close()
            'CierraConexion()
            'cmd.CommandText = "spCCBitacoraUsuarioResumen"

            'AbreConexion()

            'dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            'lvwBitacoraUsuarioResumen.Items.Clear()

            While dr.Read
                item = New ListViewItem(CType(dr("MotivoLLamadaDescripcion"), String).Trim)
                item.SubItems.Add(CType(dr("Total"), Decimal).ToString)
                item.ForeColor = Color.FromName(CType(dr("Color"), String))

                lvwBitacoraUsuarioResumen.Items.Add(item)
            End While

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
            CierraConexion()
            oSplash.Close()
            oSplash.Dispose()

        End Try
    End Sub

    Private Sub lnkRefrescar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkRefrescar.LinkClicked
        Me.CargaDatos()
    End Sub

    Private Sub lnkCerrar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCerrar.LinkClicked
        Me.Close()
    End Sub

    Private Sub lvwBitacoraUsuario_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lvwBitacoraUsuario.ColumnClick
        Try
            If e.Column <> _Columna Then
                _Columna = e.Column
                lvwBitacoraUsuario.Sorting = System.Windows.Forms.SortOrder.Ascending
            Else
                If lvwBitacoraUsuario.Sorting = SortOrder.Ascending Then
                    lvwBitacoraUsuario.Sorting = System.Windows.Forms.SortOrder.Descending
                Else
                    lvwBitacoraUsuario.Sorting = System.Windows.Forms.SortOrder.Ascending
                End If
            End If
            lvwBitacoraUsuario.Sort()

            Select Case e.Column
                Case 2
                    lvwBitacoraUsuario.ListViewItemSorter = New SigaMetClasses.ListViewComparador(e.Column, lvwBitacoraUsuario.Sorting, SigaMetClasses.ListViewComparador.enumTipoDatoComparacion.Numerico)
                Case 0
                    lvwBitacoraUsuario.ListViewItemSorter = New SigaMetClasses.ListViewComparador(e.Column, lvwBitacoraUsuario.Sorting, SigaMetClasses.ListViewComparador.enumTipoDatoComparacion.Fecha)
                Case Else
                    lvwBitacoraUsuario.ListViewItemSorter = New SigaMetClasses.ListViewComparador(e.Column, lvwBitacoraUsuario.Sorting)
            End Select

        Catch
            lvwBitacoraUsuario.Refresh()
        End Try
    End Sub

    Private Sub cboUsuarioCelula_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUsuarioCelula.SelectedIndexChanged
        If _DatosCargados = True Then
            If cboUsuarioCelula.SelectedIndex > -1 Then
                _Usuario = CType(cboUsuarioCelula.SelectedValue, String).Trim
                CargaDatos()
            End If
        End If
    End Sub

    Private Sub frmBitacoraUsuario_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CargaDatos()
        _DatosCargados = True
    End Sub

    Private Sub lvwBitacoraUsuario_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvwBitacoraUsuario.DoubleClick
        If _Cliente <> 0 Then
            Cursor = Cursors.WaitCursor
            Dim oCallCenter As frmCallCenter
            If GLOBAL_ManejarClientesPortatil Then
                oCallCenter = New frmCallCenter(_Cliente, _Portatil)
            Else
                oCallCenter = New frmCallCenter(_Cliente)
            End If
            oCallCenter.MdiParent = Me.ParentForm
            oCallCenter.Show()
            Cursor = Cursors.Default

        End If
    End Sub

    Private Sub lvwBitacoraUsuario_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvwBitacoraUsuario.SelectedIndexChanged
        Try
            _Cliente = CType(lvwBitacoraUsuario.FocusedItem.SubItems(2).Text, Integer)
            If lvwBitacoraUsuario.FocusedItem.SubItems(7).Text = "P" Then
                _Portatil = True
            Else
                _Portatil = False
            End If
        Catch
            _Cliente = 0
        End Try

    End Sub
End Class
