Public Class CapturaDatosReporte
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbCelula As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents cmbRuta As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents chkEntregadas As System.Windows.Forms.CheckBox
    Friend WithEvents chkExtraviadas As System.Windows.Forms.CheckBox
    Friend WithEvents txtFecha1 As DevExpress.XtraEditors.DateEdit
    Friend WithEvents txtFecha2 As DevExpress.XtraEditors.DateEdit
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFecha1 = New DevExpress.XtraEditors.DateEdit()
        Me.txtFecha2 = New DevExpress.XtraEditors.DateEdit()
        Me.cmbCelula = New System.Windows.Forms.ComboBox()
        Me.cmbRuta = New System.Windows.Forms.ComboBox()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.chkEntregadas = New System.Windows.Forms.CheckBox()
        Me.chkExtraviadas = New System.Windows.Forms.CheckBox()
        CType(Me.txtFecha1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFecha2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Fecha Inicio :"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(24, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Fecha Final :"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(24, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 23)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Celula :"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(24, 120)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 23)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Ruta :"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(24, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Status :"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(24, 184)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 23)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "TipoNota :"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFecha1
        '
        Me.txtFecha1.DateTime = New Date(2005, 4, 8, 0, 0, 0, 0)
        Me.txtFecha1.Location = New System.Drawing.Point(96, 27)
        Me.txtFecha1.Name = "txtFecha1"
        Me.txtFecha1.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtFecha1.Properties.Format = Nothing
        Me.txtFecha1.Properties.FormatString = "dd ""de"" MMMM ""de"" yyyy"
        Me.txtFecha1.Properties.LayerColor = System.Drawing.Color.IndianRed
        Me.txtFecha1.Properties.Style = New DevExpress.Utils.ViewStyle("ControlStyle", "BaseEdit", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                        Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                        Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                        Or DevExpress.Utils.StyleOptions.UseFont) _
                        Or DevExpress.Utils.StyleOptions.UseForeColor) _
                        Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                        Or DevExpress.Utils.StyleOptions.UseImage) _
                        Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                        Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.WindowText)
        Me.txtFecha1.Size = New System.Drawing.Size(160, 22)
        Me.txtFecha1.TabIndex = 7
        '
        'txtFecha2
        '
        Me.txtFecha2.DateTime = New Date(2005, 4, 8, 0, 0, 0, 0)
        Me.txtFecha2.Location = New System.Drawing.Point(96, 56)
        Me.txtFecha2.Name = "txtFecha2"
        Me.txtFecha2.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo))
        Me.txtFecha2.Properties.Format = Nothing
        Me.txtFecha2.Properties.FormatString = "dd ""de"" MMMM ""de"" yyyy"
        Me.txtFecha2.Properties.LayerColor = System.Drawing.Color.IndianRed
        Me.txtFecha2.Properties.Style = New DevExpress.Utils.ViewStyle("ControlStyle", "BaseEdit", New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte)), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                        Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                        Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                        Or DevExpress.Utils.StyleOptions.UseFont) _
                        Or DevExpress.Utils.StyleOptions.UseForeColor) _
                        Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                        Or DevExpress.Utils.StyleOptions.UseImage) _
                        Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                        Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.WindowText)
        Me.txtFecha2.Size = New System.Drawing.Size(160, 22)
        Me.txtFecha2.TabIndex = 8
        '
        'cmbCelula
        '
        Me.cmbCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCelula.Items.AddRange(New Object() {"TODAS LAS CELULAS", "CÉLULA 1", "Célula 2", "Célula 3", "Célula 4", "CÉLULA 5", "Célula 6", "CELULA A"})
        Me.cmbCelula.Location = New System.Drawing.Point(96, 88)
        Me.cmbCelula.Name = "cmbCelula"
        Me.cmbCelula.Size = New System.Drawing.Size(160, 21)
        Me.cmbCelula.TabIndex = 9
        '
        'cmbRuta
        '
        Me.cmbRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRuta.Items.AddRange(New Object() {"TODAS LAS RUTAS", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "125", "126", "127", "128", "150", "151", "152", "153", "601", "602", "603", "604", "605", "606", "607", "608", "609", "610"})
        Me.cmbRuta.Location = New System.Drawing.Point(96, 116)
        Me.cmbRuta.Name = "cmbRuta"
        Me.cmbRuta.Size = New System.Drawing.Size(160, 21)
        Me.cmbRuta.TabIndex = 10
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(288, 59)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 14
        Me.btnCancelar.Text = "Cancelar"
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(288, 27)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 13
        Me.btnAceptar.Text = "Aceptar"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.Items.AddRange(New Object() {"TODAS LOS STATUS", "ATASQUE", "CANCELADA", "PENDIENTE", "LIQUIDADA", "IMPRESA"})
        Me.cmbStatus.Location = New System.Drawing.Point(96, 152)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(160, 21)
        Me.cmbStatus.TabIndex = 15
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.Items.AddRange(New Object() {"TODAS LAS NOTAS", "REMISION", "BLANCA"})
        Me.cmbTipo.Location = New System.Drawing.Point(96, 184)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(160, 21)
        Me.cmbTipo.TabIndex = 16
        '
        'chkEntregadas
        '
        Me.chkEntregadas.Location = New System.Drawing.Point(24, 224)
        Me.chkEntregadas.Name = "chkEntregadas"
        Me.chkEntregadas.TabIndex = 17
        Me.chkEntregadas.Text = "Entregadas"
        '
        'chkExtraviadas
        '
        Me.chkExtraviadas.Location = New System.Drawing.Point(152, 224)
        Me.chkExtraviadas.Name = "chkExtraviadas"
        Me.chkExtraviadas.TabIndex = 18
        Me.chkExtraviadas.Text = "Extraviadas"
        '
        'CapturaDatosReporte
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(378, 286)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.chkExtraviadas, Me.chkEntregadas, Me.cmbTipo, Me.cmbStatus, Me.btnCancelar, Me.btnAceptar, Me.cmbRuta, Me.cmbCelula, Me.txtFecha2, Me.txtFecha1, Me.Label6, Me.Label5, Me.Label4, Me.Label3, Me.Label2, Me.Label1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CapturaDatosReporte"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Captura datos reporte"
        CType(Me.txtFecha1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFecha2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub CapturaDatosReporte_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim Celula As Short
        Dim Ruta As Integer
        Dim Status As String
        Dim TipoNota As Short
        Dim Entregadas As Short
        Dim Extraviadas As Short

        If cmbCelula.SelectedIndex = 0 Then
            Celula = 0
        Else
            Celula = CType(cmbCelula.SelectedIndex, Short)
        End If

        If cmbRuta.SelectedIndex = 0 Then
            Ruta = 0
        Else
            Ruta = CType(cmbRuta.Text.Trim, Integer)
        End If

        If cmbStatus.SelectedIndex = 0 Then
            Status = ""
        Else
            Status = cmbStatus.Text.Trim
        End If

        If cmbTipo.SelectedIndex = 0 Then
            TipoNota = 0
        Else
            TipoNota = CType(cmbTipo.SelectedIndex, Short)
        End If

        If chkEntregadas.Checked Then
            Entregadas = 1
        Else
            Entregadas = 0
        End If

        If chkExtraviadas.Checked Then
            Extraviadas = 1
        Else
            Extraviadas = 0
        End If

        Cursor = Cursors.WaitCursor
        Dim oReporte As New frmConsultaReporteNota(txtFecha1.DateTime, txtFecha2.DateTime, Celula, Ruta, Status, TipoNota, Entregadas, Extraviadas)
        oReporte.ShowDialog()
        Cursor = Cursors.Default

    End Sub
End Class
