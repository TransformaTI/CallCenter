Public Class CapturaHorario
    Inherits System.Windows.Forms.Form
    Private _Ruta As Integer
    Private _CP As String
    Private _Colonia As Integer
    
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
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtInicio1 As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtInicio2 As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtFin2 As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents txtFin1 As DevExpress.XtraEditors.SpinEdit
    Friend WithEvents cmbDia As System.Windows.Forms.ComboBox
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(CapturaHorario))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbDia = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtInicio1 = New DevExpress.XtraEditors.SpinEdit()
        Me.txtInicio2 = New DevExpress.XtraEditors.SpinEdit()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFin2 = New DevExpress.XtraEditors.SpinEdit()
        Me.txtFin1 = New DevExpress.XtraEditors.SpinEdit()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        CType(Me.txtInicio1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtInicio2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFin2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtFin1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Dia :"
        '
        'cmbDia
        '
        Me.cmbDia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDia.Items.AddRange(New Object() {"LUNES", "MARTES", "MIERCOLES", "JUEVES", "VIERNES", "SABADO", "DOMINGO"})
        Me.cmbDia.Location = New System.Drawing.Point(48, 24)
        Me.cmbDia.Name = "cmbDia"
        Me.cmbDia.Size = New System.Drawing.Size(248, 21)
        Me.cmbDia.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(328, 24)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Guardar"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(328, 56)
        Me.Button2.Name = "Button2"
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "Cerrar"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 66)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Hora Inicio:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Hora Final:"
        '
        'txtInicio1
        '
        Me.txtInicio1.Location = New System.Drawing.Point(80, 64)
        Me.txtInicio1.Name = "txtInicio1"
        Me.txtInicio1.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton())
        Me.txtInicio1.Properties.Format = CType(resources.GetObject("resource.Format"), System.Globalization.NumberFormatInfo)
        Me.txtInicio1.Properties.MaxLength = 2
        Me.txtInicio1.Properties.MaxValue = New Decimal(New Integer() {23, 0, 0, 0})
        Me.txtInicio1.Size = New System.Drawing.Size(56, 21)
        Me.txtInicio1.TabIndex = 8
        Me.txtInicio1.Text = "0"
        '
        'txtInicio2
        '
        Me.txtInicio2.Location = New System.Drawing.Point(191, 65)
        Me.txtInicio2.Name = "txtInicio2"
        Me.txtInicio2.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton())
        Me.txtInicio2.Properties.Format = CType(resources.GetObject("resource.Format1"), System.Globalization.NumberFormatInfo)
        Me.txtInicio2.Properties.MaxLength = 2
        Me.txtInicio2.Properties.MaxValue = New Decimal(New Integer() {59, 0, 0, 0})
        Me.txtInicio2.Size = New System.Drawing.Size(56, 21)
        Me.txtInicio2.TabIndex = 9
        Me.txtInicio2.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(139, 67)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "horas"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(253, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "minutos"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(253, 103)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "minutos"
        '
        'txtFin2
        '
        Me.txtFin2.Location = New System.Drawing.Point(191, 100)
        Me.txtFin2.Name = "txtFin2"
        Me.txtFin2.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton())
        Me.txtFin2.Properties.Format = CType(resources.GetObject("resource.Format2"), System.Globalization.NumberFormatInfo)
        Me.txtFin2.Properties.MaxLength = 2
        Me.txtFin2.Properties.MaxValue = New Decimal(New Integer() {59, 0, 0, 0})
        Me.txtFin2.Size = New System.Drawing.Size(56, 21)
        Me.txtFin2.TabIndex = 13
        Me.txtFin2.Text = "0"
        '
        'txtFin1
        '
        Me.txtFin1.Location = New System.Drawing.Point(80, 99)
        Me.txtFin1.Name = "txtFin1"
        Me.txtFin1.Properties.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton())
        Me.txtFin1.Properties.Format = CType(resources.GetObject("resource.Format3"), System.Globalization.NumberFormatInfo)
        Me.txtFin1.Properties.MaxLength = 2
        Me.txtFin1.Properties.MaxValue = New Decimal(New Integer() {23, 0, 0, 0})
        Me.txtFin1.Size = New System.Drawing.Size(56, 21)
        Me.txtFin1.TabIndex = 12
        Me.txtFin1.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(141, 103)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "horas"
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'CapturaHorario
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(434, 152)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label7, Me.Label6, Me.txtFin2, Me.txtFin1, Me.Label5, Me.Label4, Me.txtInicio2, Me.txtInicio1, Me.Label3, Me.Label2, Me.Button2, Me.Button1, Me.cmbDia, Me.Label1})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CapturaHorario"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Captura horarios"
        CType(Me.txtInicio1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtInicio2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFin2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtFin1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub Entrada(ByVal Ruta As Integer, ByVal CP As String, ByVal Colonia As Integer)
        _Ruta = Ruta
        _CP = CP
        _Colonia = Colonia

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub CapturaHorario_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDia.SelectedIndex = 0
        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click


        If CType(txtInicio1.EditValue, Integer) > CType(txtFin1.EditValue, Integer) Then
            MsgBox("La Hora de Inicio no puede ser mayor a la hora Final", MsgBoxStyle.Information)
            Exit Sub
        Else
            If CType(txtInicio1.EditValue, Integer) = CType(txtFin1.EditValue, Integer) Then
                If CType(txtInicio2.EditValue, Integer) >= CType(txtFin2.EditValue, Integer) Then
                    MsgBox("La Hora de Inicio no puede ser mayor a la hora Final", MsgBoxStyle.Information)
                    Exit Sub
                End If
            End If

        End If

        Dim cmdActualiza As New SqlClient.SqlCommand("spActualizaHorarios")
        cmdActualiza.CommandType = CommandType.StoredProcedure
        cmdActualiza.Connection = SqlConnection
        cmdActualiza.Parameters.Clear()
        cmdActualiza.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
        cmdActualiza.Parameters.Add("@CP", SqlDbType.Char).Value = _CP
        cmdActualiza.Parameters.Add("@Colonia", SqlDbType.Int).Value = _Colonia
        cmdActualiza.Parameters.Add("@Dia", SqlDbType.Char).Value = cmbDia.SelectedIndex + 1
        cmdActualiza.Parameters.Add("@HoraInicio", SqlDbType.SmallInt).Value = txtInicio1.EditValue
        cmdActualiza.Parameters.Add("@MinutosInicio", SqlDbType.SmallInt).Value = txtInicio2.EditValue
        cmdActualiza.Parameters.Add("@HoraFin", SqlDbType.SmallInt).Value = txtFin1.EditValue
        cmdActualiza.Parameters.Add("@MinutosFin", SqlDbType.SmallInt).Value = txtFin2.EditValue
        cmdActualiza.ExecuteNonQuery()
        MsgBox("Datos actualizados", MsgBoxStyle.Information)
    End Sub
End Class

