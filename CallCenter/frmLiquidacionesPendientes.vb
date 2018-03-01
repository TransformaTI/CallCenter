Imports System.Data.SqlClient

Public Class frmLiquidacionesPendientes
    Inherits System.Windows.Forms.Form
    Private _DatosCargados As Boolean
    Private _AñoAtt As Short
    Private _Folio As Integer

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
    Friend WithEvents grdLiquidacion As System.Windows.Forms.DataGrid
    Friend WithEvents cboCelula As SigaMetClasses.Combos.ComboUsuarioCelula
    Friend WithEvents Estilo1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents colAñoAtt As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colFolio As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colFTerminoRuta As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colRutaDescripcion As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colAutotanque As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colStatusLogistica As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents btnConsultar As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmLiquidacionesPendientes))
        Me.grdLiquidacion = New System.Windows.Forms.DataGrid()
        Me.Estilo1 = New System.Windows.Forms.DataGridTableStyle()
        Me.colRutaDescripcion = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colFTerminoRuta = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colAñoAtt = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colFolio = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colAutotanque = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colStatusLogistica = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.cboCelula = New SigaMetClasses.Combos.ComboUsuarioCelula()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblError = New System.Windows.Forms.Label()
        Me.btnConsultar = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.grdLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdLiquidacion
        '
        Me.grdLiquidacion.AlternatingBackColor = System.Drawing.Color.White
        Me.grdLiquidacion.BackColor = System.Drawing.Color.White
        Me.grdLiquidacion.BackgroundColor = System.Drawing.Color.Gainsboro
        Me.grdLiquidacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.grdLiquidacion.CaptionBackColor = System.Drawing.Color.Silver
        Me.grdLiquidacion.CaptionFont = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grdLiquidacion.CaptionForeColor = System.Drawing.Color.Black
        Me.grdLiquidacion.DataMember = ""
        Me.grdLiquidacion.FlatMode = True
        Me.grdLiquidacion.Font = New System.Drawing.Font("Courier New", 9.0!)
        Me.grdLiquidacion.ForeColor = System.Drawing.Color.DarkSlateGray
        Me.grdLiquidacion.GridLineColor = System.Drawing.Color.DarkGray
        Me.grdLiquidacion.HeaderBackColor = System.Drawing.Color.DarkGreen
        Me.grdLiquidacion.HeaderFont = New System.Drawing.Font("Courier New", 10.0!, System.Drawing.FontStyle.Bold)
        Me.grdLiquidacion.HeaderForeColor = System.Drawing.Color.White
        Me.grdLiquidacion.LinkColor = System.Drawing.Color.DarkGreen
        Me.grdLiquidacion.Location = New System.Drawing.Point(8, 56)
        Me.grdLiquidacion.Name = "grdLiquidacion"
        Me.grdLiquidacion.ParentRowsBackColor = System.Drawing.Color.Gainsboro
        Me.grdLiquidacion.ParentRowsForeColor = System.Drawing.Color.Black
        Me.grdLiquidacion.ReadOnly = True
        Me.grdLiquidacion.SelectionBackColor = System.Drawing.Color.DarkSeaGreen
        Me.grdLiquidacion.SelectionForeColor = System.Drawing.Color.Black
        Me.grdLiquidacion.Size = New System.Drawing.Size(584, 312)
        Me.grdLiquidacion.TabIndex = 0
        Me.grdLiquidacion.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.Estilo1})
        '
        'Estilo1
        '
        Me.Estilo1.DataGrid = Me.grdLiquidacion
        Me.Estilo1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colRutaDescripcion, Me.colFTerminoRuta, Me.colAñoAtt, Me.colFolio, Me.colAutotanque, Me.colStatusLogistica})
        Me.Estilo1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.Estilo1.MappingName = "Liquidacion"
        Me.Estilo1.ReadOnly = True
        Me.Estilo1.RowHeadersVisible = False
        Me.Estilo1.SelectionBackColor = System.Drawing.Color.SteelBlue
        '
        'colRutaDescripcion
        '
        Me.colRutaDescripcion.Format = ""
        Me.colRutaDescripcion.FormatInfo = Nothing
        Me.colRutaDescripcion.HeaderText = "Ruta"
        Me.colRutaDescripcion.MappingName = "RutaDescripcion"
        Me.colRutaDescripcion.Width = 75
        '
        'colFTerminoRuta
        '
        Me.colFTerminoRuta.Format = ""
        Me.colFTerminoRuta.FormatInfo = Nothing
        Me.colFTerminoRuta.HeaderText = "F.Término"
        Me.colFTerminoRuta.MappingName = "FTerminoRuta"
        Me.colFTerminoRuta.Width = 180
        '
        'colAñoAtt
        '
        Me.colAñoAtt.Format = ""
        Me.colAñoAtt.FormatInfo = Nothing
        Me.colAñoAtt.HeaderText = "Año"
        Me.colAñoAtt.MappingName = "AñoAtt"
        Me.colAñoAtt.Width = 75
        '
        'colFolio
        '
        Me.colFolio.Format = ""
        Me.colFolio.FormatInfo = Nothing
        Me.colFolio.HeaderText = "Folio"
        Me.colFolio.MappingName = "Folio"
        Me.colFolio.Width = 75
        '
        'colAutotanque
        '
        Me.colAutotanque.Format = ""
        Me.colAutotanque.FormatInfo = Nothing
        Me.colAutotanque.HeaderText = "A.T."
        Me.colAutotanque.MappingName = "Autotanque"
        Me.colAutotanque.Width = 75
        '
        'colStatusLogistica
        '
        Me.colStatusLogistica.Format = ""
        Me.colStatusLogistica.FormatInfo = Nothing
        Me.colStatusLogistica.HeaderText = "Estatus"
        Me.colStatusLogistica.MappingName = "StatusLogistica"
        Me.colStatusLogistica.Width = 90
        '
        'cboCelula
        '
        Me.cboCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCelula.Location = New System.Drawing.Point(64, 16)
        Me.cboCelula.Name = "cboCelula"
        Me.cboCelula.Size = New System.Drawing.Size(136, 21)
        Me.cboCelula.TabIndex = 1
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCerrar.Image = CType(resources.GetObject("btnCerrar.Image"), System.Drawing.Bitmap)
        Me.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCerrar.Location = New System.Drawing.Point(517, 15)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.TabIndex = 2
        Me.btnCerrar.Text = "&Cerrar"
        Me.btnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 14)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Célula:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.ForeColor = System.Drawing.Color.Red
        Me.lblError.Location = New System.Drawing.Point(64, 40)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(390, 14)
        Me.lblError.TabIndex = 4
        Me.lblError.Text = "Su usuario no tiene células asignadas.  Asigne una célula e intente de nuevo."
        Me.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblError.Visible = False
        '
        'btnConsultar
        '
        Me.btnConsultar.BackColor = System.Drawing.SystemColors.Control
        Me.btnConsultar.Enabled = False
        Me.btnConsultar.Location = New System.Drawing.Point(560, 60)
        Me.btnConsultar.Name = "btnConsultar"
        Me.btnConsultar.Size = New System.Drawing.Size(24, 16)
        Me.btnConsultar.TabIndex = 5
        Me.btnConsultar.Text = "..."
        Me.ToolTip1.SetToolTip(Me.btnConsultar, "Consulta más datos de este folio")
        '
        'frmLiquidacionesPendientes
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.CancelButton = Me.btnCerrar
        Me.ClientSize = New System.Drawing.Size(602, 383)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnConsultar, Me.lblError, Me.Label1, Me.btnCerrar, Me.cboCelula, Me.grdLiquidacion})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLiquidacionesPendientes"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidaciones pendientes"
        CType(Me.grdLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmLiquidacionesPendientes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cboCelula.CargaDatos(Main.GLOBAL_Usuario)
        _DatosCargados = True
        If cboCelula.Items.Count > 0 Then
            cboCelula.SelectedIndex = 0
        Else
            cboCelula.Enabled = False
            lblError.Visible = True
        End If
    End Sub

    Private Sub CargaDatos()
        Cursor = Cursors.WaitCursor

        Dim oSplash As New SigaMetClasses.frmWait()
        oSplash.Show()
        oSplash.Refresh()

        btnConsultar.Enabled = False

        Dim cmd As SqlCommand
        cmd = New SqlCommand("spCCLiquidacionesPendientes", Main.CnnSigamet)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Celula", SqlDbType.TinyInt).Value = CType(cboCelula.SelectedValue, Byte)
        End With
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable("Liquidacion")

        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                grdLiquidacion.DataSource = dt
                grdLiquidacion.CaptionText = "Lista de liquidaciones pendientes en: " & cboCelula.Text.Trim & " (" & dt.Rows.Count.ToString & " en total)"
            Else
                grdLiquidacion.DataSource = Nothing
                grdLiquidacion.CaptionText = "No existen liquidaciones pendientes por realizar"
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            da.Dispose()
            cmd.Dispose()
            oSplash.Close()
            oSplash.Dispose()

            Cursor = Cursors.Default

        End Try
    End Sub


    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub cboCelula_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCelula.SelectedValueChanged
        If _DatosCargados Then
            CargaDatos()
        End If
    End Sub

    Private Sub grdLiquidacion_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLiquidacion.CurrentCellChanged
        grdLiquidacion.Select(grdLiquidacion.CurrentRowIndex)

        _AñoAtt = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 2), Short)
        _Folio = CType(grdLiquidacion.Item(grdLiquidacion.CurrentRowIndex, 3), Integer)
        If _AñoAtt <> 0 And _Folio <> 0 Then
            btnConsultar.Enabled = True
        Else
            btnConsultar.Enabled = False
        End If

    End Sub

    Private Sub btnConsultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsultar.Click
        ConsultaATT()
    End Sub

    Private Sub ConsultaATT()
        If _AñoAtt <> 0 And _Folio <> 0 Then
            Cursor = Cursors.WaitCursor
            Dim oConsultaATT As New SigaMetClasses.ConsultaATT(_AñoAtt, _Folio)
            oConsultaATT.ShowDialog()
            Cursor = Cursors.Default
        End If
    End Sub
End Class
