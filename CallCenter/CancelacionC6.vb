Public Class CancelacionC6
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents txtFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbRuta As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents daRuta As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdRuta As System.Data.SqlClient.SqlCommand
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents DsRutaCelula6 As Sigamet.dsRutaCelula6
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.DateTimePicker()
        Me.cmbRuta = New System.Windows.Forms.ComboBox()
        Me.DsRutaCelula6 = New Sigamet.dsRutaCelula6()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.daRuta = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdRuta = New System.Data.SqlClient.SqlCommand()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        CType(Me.DsRutaCelula6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(352, 15)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(352, 47)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(18, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 16)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Fecha de cancelación:"
        '
        'txtFecha
        '
        Me.txtFecha.Location = New System.Drawing.Point(128, 15)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Size = New System.Drawing.Size(208, 21)
        Me.txtFecha.TabIndex = 3
        '
        'cmbRuta
        '
        Me.cmbRuta.DataSource = Me.DsRutaCelula6.Ruta
        Me.cmbRuta.DisplayMember = "Descripcion"
        Me.cmbRuta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRuta.Location = New System.Drawing.Point(128, 50)
        Me.cmbRuta.Name = "cmbRuta"
        Me.cmbRuta.Size = New System.Drawing.Size(136, 21)
        Me.cmbRuta.TabIndex = 5
        Me.cmbRuta.ValueMember = "Ruta"
        '
        'DsRutaCelula6
        '
        Me.DsRutaCelula6.DataSetName = "dsRutaCelula6"
        Me.DsRutaCelula6.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsRutaCelula6.Namespace = "http://www.tempuri.org/dsRutaCelula6.xsd"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(92, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 14)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Ruta :"
        '
        'daRuta
        '
        Me.daRuta.SelectCommand = Me.cmdRuta
        '
        'cmdRuta
        '
        Me.cmdRuta.CommandText = "SELECT Ruta, Descripcion FROM Ruta R WHERE (Celula = 6) ORDER BY Descripcion"
        Me.cmdRuta.CommandTimeout = 30
        Me.cmdRuta.Connection = Me.SqlConnection
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "sa;workstation id=DESARROLLO-4;packet size=4096"
        '
        'CancelacionC6
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(434, 88)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.cmbRuta, Me.Label2, Me.txtFecha, Me.Label1, Me.btnCancelar, Me.btnAceptar})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "CancelacionC6"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cancelación de pedidos celula 6"
        CType(Me.DsRutaCelula6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub CancelacionC6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        txtFecha.Value = Now.Date
        daRuta.Fill(DsRutaCelula6, "Ruta")

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing

        If txtFecha.Value.Date > Now.Date Then
            MsgBox("No se pueden cancelar pedidos mayores al dia de Hoy. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        If MsgBox("¿Desea cancelar los pedidos de la celula 6 de la ruta " + CType(cmbRuta.SelectedValue, String) + " para el dia " + CType(txtFecha.Value.Date, String) + "?. ", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
            cmdInsert.Connection = SqlConnection
            cmdInsert.CommandTimeout = 200
            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "sp_CancelaPedidoMismoDiaC6"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = txtFecha.Value.Date
            cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = cmbRuta.SelectedValue
            cmdInsert.ExecuteNonQuery()
            cmdInsert.Dispose()
            SqlConnection.Close()

            MsgBox("Los pedidos han sido cancelados", MsgBoxStyle.Information, "Mensaje del sistema")
            Me.Close()

        End If


    End Sub
End Class
