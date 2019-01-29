Imports System.Data.SqlClient

Public Class frmClienteRutaTraspaso
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private _DatosCargados As Boolean
    Private _DatosCargadosDestino As Boolean
    Private _DatosSeleccionados As Integer
    Private _Datos As Integer


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
    Friend WithEvents imgLista As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn3 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn4 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn5 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents TaskPane1 As VbPowerPack.TaskPane
    Friend WithEvents TaskFrame1 As VbPowerPack.TaskFrame
    Friend WithEvents ImageButton4 As VbPowerPack.ImageButton
    Friend WithEvents lnkConsultar As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkRefrescar As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCerrar As System.Windows.Forms.LinkLabel
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
    Friend WithEvents btnRefrescar As VbPowerPack.ImageButton
    Friend WithEvents btnConsultar As VbPowerPack.ImageButton
    Friend WithEvents cboRutaOrigen As SigaMetClasses.Combos.ComboRuta2
    Friend WithEvents cboCelulaOrigen As SigaMetClasses.Combos.ComboUsuarioCelula
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboCelulaDestino As SigaMetClasses.Combos.ComboUsuarioCelula
    Friend WithEvents cboRutaDestino As SigaMetClasses.Combos.ComboRuta2
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn8 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn9 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn10 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridBoolColumn1 As System.Windows.Forms.DataGridBoolColumn
    Friend WithEvents chkTodos As System.Windows.Forms.CheckBox
    Friend WithEvents ImageButton1 As VbPowerPack.ImageButton
    Friend WithEvents lnkGenerar As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboOperadorOrigen As System.Windows.Forms.ComboBox
    Friend WithEvents cboOperadorDestino As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ImageButton2 As VbPowerPack.ImageButton
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClienteRutaTraspaso))
		Me.imgLista = New System.Windows.Forms.ImageList(Me.components)
		Me.cboRutaOrigen = New SigaMetClasses.Combos.ComboRuta2()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.DataGridTextBoxColumn3 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn4 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn5 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.TaskPane1 = New VbPowerPack.TaskPane()
		Me.TaskFrame1 = New VbPowerPack.TaskFrame()
		Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
		Me.ImageButton2 = New VbPowerPack.ImageButton()
		Me.cboOperadorDestino = New System.Windows.Forms.ComboBox()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.cboOperadorOrigen = New System.Windows.Forms.ComboBox()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.lnkGenerar = New System.Windows.Forms.LinkLabel()
		Me.ImageButton1 = New VbPowerPack.ImageButton()
		Me.chkTodos = New System.Windows.Forms.CheckBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.cboCelulaDestino = New SigaMetClasses.Combos.ComboUsuarioCelula()
		Me.cboRutaDestino = New SigaMetClasses.Combos.ComboRuta2()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.lnkCargarDatos = New System.Windows.Forms.LinkLabel()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.cboCelulaOrigen = New SigaMetClasses.Combos.ComboUsuarioCelula()
		Me.lnkCerrar = New System.Windows.Forms.LinkLabel()
		Me.ImageButton4 = New VbPowerPack.ImageButton()
		Me.lnkRefrescar = New System.Windows.Forms.LinkLabel()
		Me.btnRefrescar = New VbPowerPack.ImageButton()
		Me.btnConsultar = New VbPowerPack.ImageButton()
		Me.lnkConsultar = New System.Windows.Forms.LinkLabel()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.grdCliente = New System.Windows.Forms.DataGrid()
		Me.styClienteRuta = New System.Windows.Forms.DataGridTableStyle()
		Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn8 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn9 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridTextBoxColumn10 = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.DataGridBoolColumn1 = New System.Windows.Forms.DataGridBoolColumn()
		Me.colCliente = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.colNombre = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.colDireccionCompleta = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.colStatus = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.colStatusCalidad = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.colRamoCliente = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.colGiroCliente = New System.Windows.Forms.DataGridTextBoxColumn()
		Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
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
		'cboRutaOrigen
		'
		Me.cboRutaOrigen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboRutaOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboRutaOrigen.Location = New System.Drawing.Point(16, 213)
		Me.cboRutaOrigen.Name = "cboRutaOrigen"
		Me.cboRutaOrigen.Size = New System.Drawing.Size(160, 21)
		Me.cboRutaOrigen.TabIndex = 63
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(16, 197)
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
		Me.TaskPane1.Dock = System.Windows.Forms.DockStyle.Left
		Me.TaskPane1.Location = New System.Drawing.Point(0, 0)
		Me.TaskPane1.Name = "TaskPane1"
		Me.TaskPane1.Size = New System.Drawing.Size(208, 598)
		Me.TaskPane1.TabIndex = 65
		'
		'TaskFrame1
		'
		Me.TaskFrame1.AllowDrop = True
		Me.TaskFrame1.BackColor = System.Drawing.Color.LightSteelBlue
		Me.TaskFrame1.CaptionBlend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Horizontal, System.Drawing.SystemColors.Window, System.Drawing.Color.FromArgb(CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer), CType(CType(160, Byte), Integer)))
		Me.TaskFrame1.CaptionHighlightColor = System.Drawing.SystemColors.ActiveCaption
		Me.TaskFrame1.Controls.Add(Me.LinkLabel1)
		Me.TaskFrame1.Controls.Add(Me.ImageButton2)
		Me.TaskFrame1.Controls.Add(Me.cboOperadorDestino)
		Me.TaskFrame1.Controls.Add(Me.Label8)
		Me.TaskFrame1.Controls.Add(Me.cboOperadorOrigen)
		Me.TaskFrame1.Controls.Add(Me.Label7)
		Me.TaskFrame1.Controls.Add(Me.lnkGenerar)
		Me.TaskFrame1.Controls.Add(Me.ImageButton1)
		Me.TaskFrame1.Controls.Add(Me.chkTodos)
		Me.TaskFrame1.Controls.Add(Me.Label5)
		Me.TaskFrame1.Controls.Add(Me.cboCelulaDestino)
		Me.TaskFrame1.Controls.Add(Me.cboRutaDestino)
		Me.TaskFrame1.Controls.Add(Me.Label6)
		Me.TaskFrame1.Controls.Add(Me.Label4)
		Me.TaskFrame1.Controls.Add(Me.lnkCargarDatos)
		Me.TaskFrame1.Controls.Add(Me.Label2)
		Me.TaskFrame1.Controls.Add(Me.cboCelulaOrigen)
		Me.TaskFrame1.Controls.Add(Me.lnkCerrar)
		Me.TaskFrame1.Controls.Add(Me.ImageButton4)
		Me.TaskFrame1.Controls.Add(Me.lnkRefrescar)
		Me.TaskFrame1.Controls.Add(Me.btnRefrescar)
		Me.TaskFrame1.Controls.Add(Me.btnConsultar)
		Me.TaskFrame1.Controls.Add(Me.lnkConsultar)
		Me.TaskFrame1.Controls.Add(Me.cboRutaOrigen)
		Me.TaskFrame1.Controls.Add(Me.Label1)
		Me.TaskFrame1.Controls.Add(Me.Label3)
		Me.TaskFrame1.Location = New System.Drawing.Point(12, 33)
		Me.TaskFrame1.Name = "TaskFrame1"
		Me.TaskFrame1.Size = New System.Drawing.Size(184, 550)
		Me.TaskFrame1.TabIndex = 1
		Me.TaskFrame1.Text = "Clientes por ruta"
		'
		'LinkLabel1
		'
		Me.LinkLabel1.LinkColor = System.Drawing.Color.Black
		Me.LinkLabel1.Location = New System.Drawing.Point(48, 488)
		Me.LinkLabel1.Name = "LinkLabel1"
		Me.LinkLabel1.Size = New System.Drawing.Size(100, 16)
		Me.LinkLabel1.TabIndex = 79
		Me.LinkLabel1.TabStop = True
		Me.LinkLabel1.Text = "Folios"
		Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'ImageButton2
		'
		Me.ImageButton2.BackColor = System.Drawing.Color.Transparent
		Me.ImageButton2.Cursor = System.Windows.Forms.Cursors.Hand
		Me.ImageButton2.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.ImageButton2.Location = New System.Drawing.Point(16, 488)
		Me.ImageButton2.Name = "ImageButton2"
		Me.ImageButton2.NormalImage = CType(resources.GetObject("ImageButton2.NormalImage"), System.Drawing.Image)
		Me.ImageButton2.Size = New System.Drawing.Size(16, 16)
		Me.ImageButton2.TabIndex = 16
		'
		'cboOperadorDestino
		'
		Me.cboOperadorDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboOperadorDestino.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboOperadorDestino.Location = New System.Drawing.Point(16, 443)
		Me.cboOperadorDestino.Name = "cboOperadorDestino"
		Me.cboOperadorDestino.Size = New System.Drawing.Size(160, 19)
		Me.cboOperadorDestino.TabIndex = 78
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(16, 424)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(92, 13)
		Me.Label8.TabIndex = 77
		Me.Label8.Text = "Operador recibe :"
		'
		'cboOperadorOrigen
		'
		Me.cboOperadorOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboOperadorOrigen.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.cboOperadorOrigen.Location = New System.Drawing.Point(16, 277)
		Me.cboOperadorOrigen.Name = "cboOperadorOrigen"
		Me.cboOperadorOrigen.Size = New System.Drawing.Size(160, 19)
		Me.cboOperadorOrigen.TabIndex = 76
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(16, 259)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(101, 13)
		Me.Label7.TabIndex = 75
		Me.Label7.Text = "Operador entrega :"
		'
		'lnkGenerar
		'
		Me.lnkGenerar.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lnkGenerar.LinkColor = System.Drawing.Color.Black
		Me.lnkGenerar.Location = New System.Drawing.Point(40, 16)
		Me.lnkGenerar.Name = "lnkGenerar"
		Me.lnkGenerar.Size = New System.Drawing.Size(100, 16)
		Me.lnkGenerar.TabIndex = 1
		Me.lnkGenerar.TabStop = True
		Me.lnkGenerar.Text = "Generar"
		Me.lnkGenerar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'ImageButton1
		'
		Me.ImageButton1.BackColor = System.Drawing.Color.Transparent
		Me.ImageButton1.Cursor = System.Windows.Forms.Cursors.Hand
		Me.ImageButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.ImageButton1.Location = New System.Drawing.Point(16, 16)
		Me.ImageButton1.Name = "ImageButton1"
		Me.ImageButton1.NormalImage = CType(resources.GetObject("ImageButton1.NormalImage"), System.Drawing.Image)
		Me.ImageButton1.Size = New System.Drawing.Size(16, 16)
		Me.ImageButton1.TabIndex = 2
		Me.ImageButton1.TransparentColor = System.Drawing.SystemColors.InactiveCaptionText
		'
		'chkTodos
		'
		Me.chkTodos.ForeColor = System.Drawing.Color.DarkGreen
		Me.chkTodos.Location = New System.Drawing.Point(16, 42)
		Me.chkTodos.Name = "chkTodos"
		Me.chkTodos.Size = New System.Drawing.Size(144, 32)
		Me.chkTodos.TabIndex = 74
		Me.chkTodos.Text = "Selecciona todos los clientes"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Location = New System.Drawing.Point(16, 341)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(40, 13)
		Me.Label5.TabIndex = 73
		Me.Label5.Text = "Célula:"
		'
		'cboCelulaDestino
		'
		Me.cboCelulaDestino.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboCelulaDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboCelulaDestino.Location = New System.Drawing.Point(16, 357)
		Me.cboCelulaDestino.Name = "cboCelulaDestino"
		Me.cboCelulaDestino.Size = New System.Drawing.Size(160, 21)
		Me.cboCelulaDestino.TabIndex = 72
		'
		'cboRutaDestino
		'
		Me.cboRutaDestino.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboRutaDestino.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboRutaDestino.Location = New System.Drawing.Point(16, 397)
		Me.cboRutaDestino.Name = "cboRutaDestino"
		Me.cboRutaDestino.Size = New System.Drawing.Size(160, 21)
		Me.cboRutaDestino.TabIndex = 70
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(16, 381)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(34, 13)
		Me.Label6.TabIndex = 71
		Me.Label6.Text = "Ruta:"
		'
		'Label4
		'
		Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label4.ForeColor = System.Drawing.Color.DarkRed
		Me.Label4.Location = New System.Drawing.Point(16, 320)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(136, 23)
		Me.Label4.TabIndex = 69
		Me.Label4.Text = "Destino de los datos"
		'
		'lnkCargarDatos
		'
		Me.lnkCargarDatos.LinkArea = New System.Windows.Forms.LinkArea(0, 15)
		Me.lnkCargarDatos.LinkColor = System.Drawing.Color.Black
		Me.lnkCargarDatos.Location = New System.Drawing.Point(102, 237)
		Me.lnkCargarDatos.Name = "lnkCargarDatos"
		Me.lnkCargarDatos.Size = New System.Drawing.Size(72, 16)
		Me.lnkCargarDatos.TabIndex = 67
		Me.lnkCargarDatos.TabStop = True
		Me.lnkCargarDatos.Text = "Cargar datos"
		Me.lnkCargarDatos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		Me.lnkCargarDatos.UseCompatibleTextRendering = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(16, 155)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(40, 13)
		Me.Label2.TabIndex = 66
		Me.Label2.Text = "Célula:"
		'
		'cboCelulaOrigen
		'
		Me.cboCelulaOrigen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cboCelulaOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.cboCelulaOrigen.Location = New System.Drawing.Point(16, 171)
		Me.cboCelulaOrigen.Name = "cboCelulaOrigen"
		Me.cboCelulaOrigen.Size = New System.Drawing.Size(160, 21)
		Me.cboCelulaOrigen.TabIndex = 65
		'
		'lnkCerrar
		'
		Me.lnkCerrar.LinkColor = System.Drawing.Color.Black
		Me.lnkCerrar.Location = New System.Drawing.Point(48, 524)
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
		Me.ImageButton4.Location = New System.Drawing.Point(16, 524)
		Me.ImageButton4.Name = "ImageButton4"
		Me.ImageButton4.NormalImage = CType(resources.GetObject("ImageButton4.NormalImage"), System.Drawing.Image)
		Me.ImageButton4.Size = New System.Drawing.Size(16, 16)
		Me.ImageButton4.TabIndex = 6
		'
		'lnkRefrescar
		'
		Me.lnkRefrescar.LinkColor = System.Drawing.Color.Black
		Me.lnkRefrescar.Location = New System.Drawing.Point(48, 131)
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
		Me.btnRefrescar.Location = New System.Drawing.Point(16, 131)
		Me.btnRefrescar.Name = "btnRefrescar"
		Me.btnRefrescar.NormalImage = CType(resources.GetObject("btnRefrescar.NormalImage"), System.Drawing.Image)
		Me.btnRefrescar.Size = New System.Drawing.Size(16, 16)
		Me.btnRefrescar.TabIndex = 4
		'
		'btnConsultar
		'
		Me.btnConsultar.BackColor = System.Drawing.Color.Transparent
		Me.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand
		Me.btnConsultar.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.btnConsultar.Enabled = False
		Me.btnConsultar.Location = New System.Drawing.Point(16, 107)
		Me.btnConsultar.Name = "btnConsultar"
		Me.btnConsultar.NormalImage = CType(resources.GetObject("btnConsultar.NormalImage"), System.Drawing.Image)
		Me.btnConsultar.Size = New System.Drawing.Size(16, 16)
		Me.btnConsultar.TabIndex = 3
		'
		'lnkConsultar
		'
		Me.lnkConsultar.Enabled = False
		Me.lnkConsultar.LinkColor = System.Drawing.Color.Black
		Me.lnkConsultar.Location = New System.Drawing.Point(48, 107)
		Me.lnkConsultar.Name = "lnkConsultar"
		Me.lnkConsultar.Size = New System.Drawing.Size(100, 16)
		Me.lnkConsultar.TabIndex = 2
		Me.lnkConsultar.TabStop = True
		Me.lnkConsultar.Text = "Consultar"
		Me.lnkConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label3
		'
		Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(16, 80)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(136, 23)
		Me.Label3.TabIndex = 68
		Me.Label3.Text = "Origen de los datos"
		'
		'grdCliente
		'
		Me.grdCliente.AlternatingBackColor = System.Drawing.Color.Lavender
		Me.grdCliente.BackColor = System.Drawing.Color.WhiteSmoke
		Me.grdCliente.BackgroundColor = System.Drawing.Color.LightGray
		Me.grdCliente.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.grdCliente.CaptionBackColor = System.Drawing.Color.LightSteelBlue
		Me.grdCliente.CaptionForeColor = System.Drawing.Color.MidnightBlue
		Me.grdCliente.DataMember = ""
		Me.grdCliente.Dock = System.Windows.Forms.DockStyle.Fill
		Me.grdCliente.FlatMode = True
		Me.grdCliente.Font = New System.Drawing.Font("Tahoma", 8.0!)
		Me.grdCliente.ForeColor = System.Drawing.Color.MidnightBlue
		Me.grdCliente.GridLineColor = System.Drawing.Color.IndianRed
		Me.grdCliente.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None
		Me.grdCliente.HeaderBackColor = System.Drawing.Color.MidnightBlue
		Me.grdCliente.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
		Me.grdCliente.HeaderForeColor = System.Drawing.Color.WhiteSmoke
		Me.grdCliente.LinkColor = System.Drawing.Color.Teal
		Me.grdCliente.Location = New System.Drawing.Point(208, 0)
		Me.grdCliente.Name = "grdCliente"
		Me.grdCliente.ParentRowsBackColor = System.Drawing.Color.Gainsboro
		Me.grdCliente.ParentRowsForeColor = System.Drawing.Color.MidnightBlue
		Me.grdCliente.ReadOnly = True
		Me.grdCliente.SelectionBackColor = System.Drawing.Color.CadetBlue
		Me.grdCliente.SelectionForeColor = System.Drawing.Color.WhiteSmoke
		Me.grdCliente.Size = New System.Drawing.Size(552, 575)
		Me.grdCliente.TabIndex = 1
		Me.grdCliente.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.styClienteRuta})
		'
		'styClienteRuta
		'
		Me.styClienteRuta.DataGrid = Me.grdCliente
		Me.styClienteRuta.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2, Me.DataGridTextBoxColumn6, Me.DataGridTextBoxColumn7, Me.DataGridTextBoxColumn8, Me.DataGridTextBoxColumn9, Me.DataGridTextBoxColumn10, Me.DataGridBoolColumn1})
		Me.styClienteRuta.HeaderForeColor = System.Drawing.SystemColors.ControlText
		Me.styClienteRuta.MappingName = "Cliente"
		'
		'DataGridTextBoxColumn1
		'
		Me.DataGridTextBoxColumn1.Format = ""
		Me.DataGridTextBoxColumn1.FormatInfo = Nothing
		Me.DataGridTextBoxColumn1.HeaderText = "Cliente"
		Me.DataGridTextBoxColumn1.MappingName = "Cliente"
		Me.DataGridTextBoxColumn1.ReadOnly = True
		Me.DataGridTextBoxColumn1.Width = 75
		'
		'DataGridTextBoxColumn2
		'
		Me.DataGridTextBoxColumn2.Format = ""
		Me.DataGridTextBoxColumn2.FormatInfo = Nothing
		Me.DataGridTextBoxColumn2.HeaderText = "Nombre"
		Me.DataGridTextBoxColumn2.MappingName = "Nombre"
		Me.DataGridTextBoxColumn2.ReadOnly = True
		Me.DataGridTextBoxColumn2.Width = 200
		'
		'DataGridTextBoxColumn6
		'
		Me.DataGridTextBoxColumn6.Format = ""
		Me.DataGridTextBoxColumn6.FormatInfo = Nothing
		Me.DataGridTextBoxColumn6.HeaderText = "DireccionCompleta"
		Me.DataGridTextBoxColumn6.MappingName = "DireccionCompleta"
		Me.DataGridTextBoxColumn6.ReadOnly = True
		Me.DataGridTextBoxColumn6.Width = 250
		'
		'DataGridTextBoxColumn7
		'
		Me.DataGridTextBoxColumn7.Format = ""
		Me.DataGridTextBoxColumn7.FormatInfo = Nothing
		Me.DataGridTextBoxColumn7.HeaderText = "Status"
		Me.DataGridTextBoxColumn7.MappingName = "Status"
		Me.DataGridTextBoxColumn7.ReadOnly = True
		Me.DataGridTextBoxColumn7.Width = 75
		'
		'DataGridTextBoxColumn8
		'
		Me.DataGridTextBoxColumn8.Format = ""
		Me.DataGridTextBoxColumn8.FormatInfo = Nothing
		Me.DataGridTextBoxColumn8.HeaderText = "StatusCalidad"
		Me.DataGridTextBoxColumn8.MappingName = "StatusCalidad"
		Me.DataGridTextBoxColumn8.ReadOnly = True
		Me.DataGridTextBoxColumn8.Width = 75
		'
		'DataGridTextBoxColumn9
		'
		Me.DataGridTextBoxColumn9.Format = ""
		Me.DataGridTextBoxColumn9.FormatInfo = Nothing
		Me.DataGridTextBoxColumn9.HeaderText = "RamoCliente"
		Me.DataGridTextBoxColumn9.MappingName = "RamoClienteDescripcion"
		Me.DataGridTextBoxColumn9.ReadOnly = True
		Me.DataGridTextBoxColumn9.Width = 120
		'
		'DataGridTextBoxColumn10
		'
		Me.DataGridTextBoxColumn10.Format = ""
		Me.DataGridTextBoxColumn10.FormatInfo = Nothing
		Me.DataGridTextBoxColumn10.HeaderText = "GiroCliente"
		Me.DataGridTextBoxColumn10.MappingName = "GiroClienteDescripcion"
		Me.DataGridTextBoxColumn10.ReadOnly = True
		Me.DataGridTextBoxColumn10.Width = 75
		'
		'DataGridBoolColumn1
		'
		Me.DataGridBoolColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
		Me.DataGridBoolColumn1.HeaderText = "Traspaso"
		Me.DataGridBoolColumn1.MappingName = "Traspaso"
		Me.DataGridBoolColumn1.Width = 75
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
		'ProgressBar1
		'
		Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.ProgressBar1.Location = New System.Drawing.Point(208, 575)
		Me.ProgressBar1.Name = "ProgressBar1"
		Me.ProgressBar1.Size = New System.Drawing.Size(552, 23)
		Me.ProgressBar1.TabIndex = 66
		'
		'frmClienteRutaTraspaso
		'
		Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
		Me.ClientSize = New System.Drawing.Size(760, 598)
		Me.Controls.Add(Me.grdCliente)
		Me.Controls.Add(Me.ProgressBar1)
		Me.Controls.Add(Me.TaskPane1)
		Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "frmClienteRutaTraspaso"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Clientes por ruta (Traspasos)"
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
        lnkConsultar.Enabled = False
        btnConsultar.Enabled = False

        Dim cmd As New SqlCommand("spCCConsultaClienteRutaTraspaso")
        With cmd
            .CommandTimeout = 120
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = CType(cboRutaOrigen.SelectedValue, Short)
            .Connection = CnnSigamet
        End With

        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable("Cliente")
        Try
            AbreConexion()

            da.Fill(dt)


            _DatosSeleccionados = 0
            _Datos = dt.Rows.Count

            If dt.Rows.Count > 0 Then
                grdCliente.DataSource = dt
                grdCliente.CaptionText = "Lista de clientes de " & cboRutaOrigen.Text.Trim & " (" & dt.Rows.Count.ToString & " en total) - " & _DatosSeleccionados.ToString & " clientes seleccionados para cambiar."
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

    End Sub


    Private Sub frmClienteRuta_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboCelulaOrigen.CargaDatos(Main.GLOBAL_Usuario)
        If cboCelulaOrigen.Items.Count > 0 Then
            cboCelulaOrigen.SelectedIndex = 0
        End If
        Dim _shrCelulaOrigen As Short = CType(cboCelulaOrigen.SelectedValue, Short)
        cboRutaOrigen.CargaDatos(_shrCelulaOrigen)
        _DatosCargados = True


        cboCelulaDestino.CargaDatos(Main.GLOBAL_Usuario)
        If cboCelulaDestino.Items.Count > 0 Then
            cboCelulaDestino.SelectedIndex = 0
        End If
        Dim _shrCelulaDestino As Short = CType(cboCelulaDestino.SelectedValue, Short)
        cboRutaDestino.CargaDatos(_shrCelulaDestino)
        _DatosCargadosDestino = True

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
            lnkConsultar.Enabled = True
            btnConsultar.Enabled = True
        Catch
            _Cliente = 0
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



    Private Sub cboCelulaOrigen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCelulaOrigen.SelectedIndexChanged
        If _DatosCargados Then
            cboRutaOrigen.CargaDatos(CType(cboCelulaOrigen.SelectedValue, Short))
            If cboRutaOrigen.Items.Count > 0 Then
                cboRutaOrigen.SelectedIndex = 0
            End If


            Dim cmd As New SqlCommand("select Empleado, Nombre from vwCMOperador where Tipo='Titular' and Status='ACTIVO' and idCelula=@Celula")
            With cmd
                .CommandTimeout = 120
                .CommandType = CommandType.Text
                .Parameters.Add("@Celula", SqlDbType.SmallInt).Value = CType(cboCelulaOrigen.SelectedValue, Short)
                .Connection = CnnSigamet
            End With

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable("OperadoOrigen")
            Try
                AbreConexion()

                da.Fill(dt)

                cboOperadorOrigen.DisplayMember = "Nombre"
                cboOperadorOrigen.ValueMember = "Empleado"

                cboOperadorOrigen.DataSource = dt
                
                If cboOperadorOrigen.Items.Count > 0 Then
                    cboOperadorOrigen.SelectedIndex = 0
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
                Cursor = Cursors.Default
                da.Dispose()
                cmd.Dispose()
            End Try

        End If
    End Sub


    Private Sub lnkCargarDatos_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkCargarDatos.LinkClicked
        Me.CargaDatos()
    End Sub



    Private Sub grdCliente_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCliente.DoubleClick

        If grdCliente.VisibleRowCount > 0 Then
            If CType(grdCliente.Item(grdCliente.CurrentRowIndex, 7), Boolean) = True Then
                grdCliente.Item(grdCliente.CurrentRowIndex, 7) = False
                _DatosSeleccionados = _DatosSeleccionados - 1
            Else
                grdCliente.Item(grdCliente.CurrentRowIndex, 7) = True
                _DatosSeleccionados = _DatosSeleccionados + 1
            End If

            grdCliente.Select(grdCliente.CurrentRowIndex)
            grdCliente.Refresh()
            grdCliente.CaptionText = "Lista de clientes de " & cboRutaOrigen.Text.Trim & " (" & _Datos.ToString & " en total) - " & _DatosSeleccionados.ToString & " clientes seleccionados para cambiar."
        End If

    End Sub



    Private Sub chkTodos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTodos.Click
        If _Datos > 0 Then

            If chkTodos.Checked Then
                Dim i As Integer
                _DatosSeleccionados = 0
                For i = 0 To _Datos - 1
                    grdCliente.Item(i, 7) = True
                    _DatosSeleccionados = _DatosSeleccionados + 1

                Next
            Else
                Dim i As Integer
                _DatosSeleccionados = 0
                For i = 0 To _Datos - 1
                    grdCliente.Item(i, 7) = False

                Next
            End If

            grdCliente.Refresh()
            grdCliente.CaptionText = "Lista de clientes de " & cboRutaOrigen.Text.Trim & " (" & _Datos.ToString & " en total) - " & _DatosSeleccionados.ToString & " clientes seleccionados para cambiar."

        Else
            MsgBox("No existen datos a seleccionar.", MsgBoxStyle.OKOnly, "Mensaje")
            If chkTodos.Checked Then
                chkTodos.CheckState = CheckState.Unchecked
            Else
                chkTodos.CheckState = CheckState.Checked
            End If
        End If

    End Sub


    Private Sub lnkGenerar_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lnkGenerar.LinkClicked
        Me.Generar()
    End Sub

    Private Function VerificaLiquidacionRuta(ByVal Celula As Integer, ByVal ruta As Integer) As Boolean

        Dim cmd As New SqlCommand("Select IsNull(dbo.CCRutaLiquidada(@Celula, @Ruta),0) as Registro")
        With cmd
            .CommandTimeout = 120
            .CommandType = CommandType.Text
            .Parameters.Add("@Celula", SqlDbType.SmallInt).Value = Celula
            .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = ruta
            .Connection = CnnSigamet
        End With
        Dim Registro As Integer
        Registro = 0
        Dim Regresa As Boolean

        Try
            AbreConexion()

            Dim rdrLectura As SqlDataReader
            rdrLectura = cmd.ExecuteReader()
            If rdrLectura.Read() = True Then
                Registro = CType(rdrLectura("Registro"), Integer)
            End If

            If Registro = 0 Then
                Regresa = False
            Else
                Regresa = True
            End If
            rdrLectura.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            Cursor = Cursors.Default
            cmd.Dispose()
        End Try

        Return Regresa

    End Function

    Private Sub Generar()

        If _Datos = 0 Then
            MsgBox("No existen datos a traspasar. Verificar.", MsgBoxStyle.OKOnly, "Mensaje")
            Exit Sub
        End If

        If _DatosSeleccionados = 0 Then
            MsgBox("No existen datos seleccionados a traspasar. Verificar.", MsgBoxStyle.OKOnly, "Mensaje")
            Exit Sub
        End If


        If cboRutaOrigen.Text.Trim = cboRutaDestino.Text.Trim Then
            MsgBox("La ruta de Destino no puede ser igual a la ruta de Origen. Verificar.", MsgBoxStyle.OKOnly, "Mensaje")
            Exit Sub
        End If


        If CType(cboOperadorOrigen.SelectedValue, Integer) = CType(cboOperadorDestino.SelectedValue, Integer) Then
            MsgBox("El operador que recibe no puede ser igual al Operador que entrega. Verificar.", MsgBoxStyle.OKOnly, "Mensaje")
            Exit Sub
        End If

        If Not VerificaLiquidacionRuta(CType(cboCelulaOrigen.SelectedValue, Short), CType(cboRutaOrigen.SelectedValue, Short)) Then
            MsgBox("El ultimo folio de liquidación de la ruta ORIGEN  no esta cerrado. No se puede traspasar.", MsgBoxStyle.OKOnly, "Mensaje")
            Exit Sub
        End If

        If Not VerificaLiquidacionRuta(CType(cboCelulaDestino.SelectedValue, Short), CType(cboRutaDestino.SelectedValue, Short)) Then
            MsgBox("El ultimo folio de liquidación de la ruta DESTINO  no esta cerrado. No se puede traspasar.", MsgBoxStyle.OKOnly, "Mensaje")
            Exit Sub
        End If

        If _Datos = _DatosSeleccionados Then
            If MsgBox("¿Desea traspasar todos los clientes de la " & cboRutaOrigen.Text.Trim & Chr(13) & " a la " & cboRutaDestino.Text.Trim & "?.", MsgBoxStyle.YesNoCancel, "Confirmación") <> MsgBoxResult.Yes Then
                Exit Sub
            End If
        End If

        If MsgBox("Se van a traspasar " & _DatosSeleccionados.ToString & " clientes de " & cboRutaOrigen.Text.Trim & " a la " & cboRutaDestino.Text.Trim & ". " & Chr(13) & _
                  "¿Desea continuar?", MsgBoxStyle.YesNoCancel, "Confirmación") = MsgBoxResult.Yes Then
            Me.Guardar()
        Else
            Exit Sub
        End If

    End Sub

    Private Sub ImageButton1_Click() Handles ImageButton1.Click
        Me.Generar()
    End Sub


    Private Sub Guardar()
        Dim Transaccion As SqlTransaction
        Dim cmd As New SqlCommand()
        With cmd
            .CommandText = "exec spTraspasoClienteRuta @CelulaOrigen, @RutaOrigen, @CelulaDestino, @RutaDestino, @OperadorOrigen, @OperadorDestino, @Usuario, @TerminoRuta"
            .CommandTimeout = 120
            .CommandType = CommandType.Text
            .Parameters.Add("@CelulaOrigen", SqlDbType.SmallInt).Value = CType(cboCelulaOrigen.SelectedValue, Short)
            .Parameters.Add("@RutaOrigen", SqlDbType.SmallInt).Value = CType(cboRutaOrigen.SelectedValue, Short)
            .Parameters.Add("@CelulaDestino", SqlDbType.SmallInt).Value = CType(cboCelulaDestino.SelectedValue, Short)
            .Parameters.Add("@RutaDestino", SqlDbType.SmallInt).Value = CType(cboRutaDestino.SelectedValue, Short)
            .Parameters.Add("@OperadorOrigen", SqlDbType.SmallInt).Value = CType(cboOperadorOrigen.SelectedValue, Short)
            .Parameters.Add("@OperadorDestino", SqlDbType.SmallInt).Value = CType(cboOperadorDestino.SelectedValue, Short)
            .Parameters.Add("@Usuario", SqlDbType.Char).Value = GLOBAL_Usuario
            If _Datos = _DatosSeleccionados Then
                .Parameters.Add("@TerminoRuta", SqlDbType.SmallInt).Value = 1
            Else
                .Parameters.Add("@TerminoRuta", SqlDbType.SmallInt).Value = 0
            End If
            .Connection = CnnSigamet
        End With

        Try
            AbreConexion()
        Catch
            MessageBox.Show(SigaMetClasses.M_NO_CONEXION, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End Try

        Dim FolioTraspaso As Integer

        Transaccion = CnnSigamet.BeginTransaction
        cmd.Transaction = Transaccion
        Try

            Dim rdrLectura As SqlDataReader
            rdrLectura = cmd.ExecuteReader()
            If rdrLectura.Read() = True Then
                FolioTraspaso = CType(rdrLectura("FolioTraspaso"), Integer)
            End If
            rdrLectura.Close()


            ProgressBar1.Maximum = _DatosSeleccionados
            ProgressBar1.Value = 0

            Dim i As Integer
            For i = 0 To _Datos - 1
                If CType(grdCliente.Item(i, 7), Boolean) = True Then

                    cmd.CommandText = "spTraspasoClienteRutaDetalle"
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Clear()
                    cmd.Parameters.Add("@CelulaDestino", SqlDbType.SmallInt).Value = CType(cboCelulaDestino.SelectedValue, Short)
                    cmd.Parameters.Add("@RutaDestino", SqlDbType.SmallInt).Value = CType(cboRutaDestino.SelectedValue, Short)
                    cmd.Parameters.Add("@FolioTraspaso", SqlDbType.Int).Value = FolioTraspaso
                    cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(grdCliente.Item(i, 0), Integer)
                    cmd.ExecuteNonQuery()

                    ProgressBar1.Value = ProgressBar1.Value + 1

                End If

            Next

            Transaccion.Commit()

            MsgBox("Concluyo satisfactoriamente el traspaso de clientes.", MsgBoxStyle.Information, "Mensaje de sistema")
            ProgressBar1.Value = 0
            grdCliente.DataSource = Nothing
            grdCliente.CaptionText = ""

        Catch ex As Exception
            Transaccion.Rollback()
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            Cursor = Cursors.Default
        End Try


    End Sub

    Private Sub cboCelulaDestino_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCelulaDestino.SelectedIndexChanged
        If _DatosCargadosDestino Then
            cboRutaDestino.CargaDatos(CType(cboCelulaDestino.SelectedValue, Short))
            If cboRutaDestino.Items.Count > 0 Then
                cboRutaDestino.SelectedIndex = 0
            End If


            Dim cmd As New SqlCommand("select Empleado, Nombre from vwCMOperador where Tipo='Titular' and Status='ACTIVO' and idCelula=@Celula")
            With cmd
                .CommandTimeout = 120
                .CommandType = CommandType.Text
                .Parameters.Add("@Celula", SqlDbType.SmallInt).Value = CType(cboCelulaDestino.SelectedValue, Short)
                .Connection = CnnSigamet
            End With

            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable("OperadoDestino")
            Try
                AbreConexion()

                da.Fill(dt)

                cboOperadorDestino.DisplayMember = "Nombre"
                cboOperadorDestino.ValueMember = "Empleado"

                cboOperadorDestino.DataSource = dt

                If cboOperadorDestino.Items.Count > 0 Then
                    cboOperadorDestino.SelectedIndex = 0
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                CierraConexion()
                Cursor = Cursors.Default
                da.Dispose()
                cmd.Dispose()
            End Try

        End If

    End Sub

    Private Sub cboRutaOrigen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboRutaOrigen.SelectedIndexChanged
        grdCliente.DataSource = Nothing
        grdCliente.CaptionText = ""
    End Sub

End Class
