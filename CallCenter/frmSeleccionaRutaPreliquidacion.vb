Imports System.Data.SqlClient

Public Class frmSeleccionaRutaPreliquidacion
    Inherits System.Windows.Forms.Form
    Private _AñoAtt As Short
    Public _Celula As Integer
    Private _Folio As Integer
    Private _Ruta As Short
    Private _Fecha As Date
    Private _Descarga As Boolean
    Private _DatosCargados As Boolean
    Private _Titulo As String = "Pre-liquidación de rutas"
    Private _Relacion As Boolean = False

#Region "Propiedades"
    Public Property AñoAtt() As Short
        Get
            Return _AñoAtt
        End Get
        Set(ByVal Value As Short)
            _AñoAtt = Value
        End Set
    End Property

    Public Property Folio() As Integer
        Get
            Return _Folio
        End Get
        Set(ByVal Value As Integer)
            _Folio = Value
        End Set
    End Property

    Public Property Ruta() As Short
        Get
            Return _Ruta
        End Get
        Set(ByVal Value As Short)
            _Ruta = Value
        End Set
    End Property

    Public Property Fecha() As Date
        Get
            Return _Fecha
        End Get
        Set(ByVal Value As Date)
            _Fecha = Value
        End Set
    End Property

    Public Property Descarga() As Boolean
        Get
            Return _Descarga
        End Get
        Set(ByVal Value As Boolean)
            _Descarga = Value
        End Set
    End Property
#End Region


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
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents dtpFTerminoRuta As System.Windows.Forms.DateTimePicker
    Friend WithEvents cboRuta As System.Windows.Forms.ComboBox
    Friend WithEvents lblAutotanque As System.Windows.Forms.Label
    Friend WithEvents lblLitrosLiquidados As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblAñoAtt As System.Windows.Forms.Label
    Friend WithEvents lblFolio As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmSeleccionaRutaPreliquidacion))
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.dtpFTerminoRuta = New System.Windows.Forms.DateTimePicker()
        Me.cboRuta = New System.Windows.Forms.ComboBox()
        Me.lblAutotanque = New System.Windows.Forms.Label()
        Me.lblLitrosLiquidados = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblAñoAtt = New System.Windows.Forms.Label()
        Me.lblFolio = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(80, 216)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 4
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(176, 216)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 5
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpFTerminoRuta
        '
        Me.dtpFTerminoRuta.Location = New System.Drawing.Point(104, 16)
        Me.dtpFTerminoRuta.Name = "dtpFTerminoRuta"
        Me.dtpFTerminoRuta.Size = New System.Drawing.Size(208, 21)
        Me.dtpFTerminoRuta.TabIndex = 0
        '
        'cboRuta
        '
        Me.cboRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRuta.Location = New System.Drawing.Point(104, 48)
        Me.cboRuta.Name = "cboRuta"
        Me.cboRuta.Size = New System.Drawing.Size(208, 21)
        Me.cboRuta.TabIndex = 1
        '
        'lblAutotanque
        '
        Me.lblAutotanque.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAutotanque.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutotanque.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblAutotanque.Location = New System.Drawing.Point(96, 56)
        Me.lblAutotanque.Name = "lblAutotanque"
        Me.lblAutotanque.Size = New System.Drawing.Size(200, 21)
        Me.lblAutotanque.TabIndex = 2
        Me.lblAutotanque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLitrosLiquidados
        '
        Me.lblLitrosLiquidados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblLitrosLiquidados.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLitrosLiquidados.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblLitrosLiquidados.Location = New System.Drawing.Point(96, 80)
        Me.lblLitrosLiquidados.Name = "lblLitrosLiquidados"
        Me.lblLitrosLiquidados.Size = New System.Drawing.Size(200, 21)
        Me.lblLitrosLiquidados.TabIndex = 3
        Me.lblLitrosLiquidados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 14)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Del día:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 14)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Ruta:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 56)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 14)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Autotanque:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 14)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Litros:"
        '
        'lblAñoAtt
        '
        Me.lblAñoAtt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblAñoAtt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAñoAtt.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblAñoAtt.Location = New System.Drawing.Point(96, 32)
        Me.lblAñoAtt.Name = "lblAñoAtt"
        Me.lblAñoAtt.Size = New System.Drawing.Size(64, 21)
        Me.lblAñoAtt.TabIndex = 10
        Me.lblAñoAtt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFolio
        '
        Me.lblFolio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblFolio.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFolio.ForeColor = System.Drawing.Color.MediumBlue
        Me.lblFolio.Location = New System.Drawing.Point(200, 32)
        Me.lblFolio.Name = "lblFolio"
        Me.lblFolio.Size = New System.Drawing.Size(96, 21)
        Me.lblFolio.TabIndex = 11
        Me.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(42, 14)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "AñoAtt:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(168, 35)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 14)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Folio:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label1, Me.lblLitrosLiquidados, Me.lblFolio, Me.Label2, Me.lblAutotanque, Me.lblAñoAtt, Me.Label7, Me.Label8})
        Me.GroupBox1.Location = New System.Drawing.Point(9, 80)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 120)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos del folio"
        '
        'frmSeleccionaRutaPreliquidacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.Gainsboro
        Me.ClientSize = New System.Drawing.Size(330, 255)
        Me.ControlBox = False
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.GroupBox1, Me.Label4, Me.Label3, Me.cboRuta, Me.dtpFTerminoRuta, Me.btnCancelar, Me.btnAceptar})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmSeleccionaRutaPreliquidacion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pre-liquidación de rutas"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Entrada(ByVal Tipo As Boolean)
        _Relacion = Tipo
        CargaDatos()
    End Sub

    Private Sub frmSeleccionaRutaPreliquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFTerminoRuta.Value = GLOBAL_FechaServidor.Date
        _DatosCargados = True
        'NO ESTABA ESTO
        CargaDatos()
    End Sub

    Private Sub LimpiaDatos()
        _Ruta = 0
        _AñoAtt = 0
        _Folio = 0
        lblAñoAtt.Text = ""
        lblFolio.Text = ""
        lblLitrosLiquidados.Text = ""
        lblAutotanque.Text = ""
    End Sub

    Private Sub CargaDatos()

        Cursor = Cursors.WaitCursor
        Dim strQuery1 As String = _
        "SELECT AñoAtt, Folio, Celula, Ruta, RutaDescripcion, Autotanque, LitrosLiquidados " & _
        "FROM vwCCFoliosPendientesPreLiq " & _
        "WHERE FTerminoRuta Between '" & dtpFTerminoRuta.Value.Date.ToShortDateString & "' AND '" & _
        dtpFTerminoRuta.Value.Date.ToShortDateString & " 23:59:59'"

        Dim strQuery2 As String = _
        "SELECT AñoAtt, Folio, Celula, Ruta, RutaDescripcion, Autotanque, LitrosLiquidados " & _
        "FROM vwCCFoliosLiquidados " & _
                "WHERE FTerminoRuta Between '" & dtpFTerminoRuta.Value.Date.ToShortDateString & "' AND '" & _
                dtpFTerminoRuta.Value.Date.ToShortDateString & " 23:59:59'"

        Dim strQuery As String
        If _Relacion = False Then
            strQuery = strQuery1
        Else
            strQuery = strQuery2
        End If

        Dim da As New SqlDataAdapter(strQuery, GLOBAL_ConString)

        Dim dt As New DataTable("Preliq")

        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                cboRuta.ValueMember = "Ruta"
                cboRuta.DisplayMember = "RutaDescripcion"
                cboRuta.DataSource = dt

                Me.DataBindings.Clear()
                lblAñoAtt.DataBindings.Clear()
                lblFolio.DataBindings.Clear()
                lblAutotanque.DataBindings.Clear()
                lblLitrosLiquidados.DataBindings.Clear()

                Me.DataBindings.Add("AñoAtt", dt, "AñoAtt")
                Me.DataBindings.Add("Folio", dt, "Folio")
                lblAñoAtt.DataBindings.Add("Text", dt, "AñoAtt")
                lblFolio.DataBindings.Add("Text", dt, "Folio")
                lblAutotanque.DataBindings.Add("Text", dt, "Autotanque")
                lblLitrosLiquidados.DataBindings.Add("Text", dt, "LitrosLiquidados")

                _Celula = CType(dt.Rows.Item(0).Item("Celula"), Integer)

                btnAceptar.Enabled = True
                cboRuta.Enabled = True
            Else
                cboRuta.DataSource = Nothing
                cboRuta.Items.Clear()
                cboRuta.Text = ""
                cboRuta.Enabled = False
                btnAceptar.Enabled = False

                LimpiaDatos()

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        Finally
            Cursor = Cursors.Default
            da.Dispose()
        End Try

    End Sub

    Private Sub dtpFTerminoRuta_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFTerminoRuta.ValueChanged
        If _DatosCargados Then CargaDatos()
        _Fecha = dtpFTerminoRuta.Value.Date
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If _Relacion = False Then
            Dim iRampac As Integer = TotalRampac(_AñoAtt, _Folio)
            If iRampac = 0 Then
                MessageBox.Show("La tarjeta rampac no ha sido descargada, " & Chr(13) & "la liquidación sera de forma manual.", _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information)
                _Descarga = False
            Else
                If MessageBox.Show("La tarjeta rampac ya fue descargada." & Chr(13) & "¿Desea seguir con la liquidación manual?", _Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    _Descarga = False
                Else
                    _Descarga = True
                End If
            End If

        End If
        DialogResult = DialogResult.OK
    End Sub

    Private Sub cboRuta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRuta.SelectedIndexChanged
        _Ruta = CType(cboRuta.SelectedValue, Short)

    End Sub
End Class
