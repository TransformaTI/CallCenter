Imports System.Data.SqlClient

Public Class frmCalendarioCliente
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private Fechas As ArrayList

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
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmCalendarioCliente))
        '
        'frmCalendarioCliente
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.MintCream
        Me.ClientSize = New System.Drawing.Size(920, 669)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCalendarioCliente"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Calendario"

    End Sub

#End Region

    Public Sub New(ByVal Cliente As Integer)

        MyBase.New()
        InitializeComponent()

        _Cliente = Cliente

        Me.Text &= " [" & _Cliente.ToString & "]"

        CargaCalendarioProgramacion()

        Dim oCalendario As Calendario.Calendario, i As Integer = 0, x As Integer = 10, y As Integer = 10
        For i = 0 To 11
            oCalendario = New Calendario.Calendario(True)
            oCalendario.Location = New Point(x, y)
            oCalendario.PintaCalendario(CType(i + 1, Byte), CType(Now.Year, Short))

            oCalendario.FechasSeleccionadas(Me.Fechas)

            oCalendario.Show()

            Me.Controls.Add(oCalendario)

            x += oCalendario.Width + 10
            If i = 3 Then
                y += oCalendario.Height + 10
                x = 10
            End If

            If i = 7 Then
                y += oCalendario.Height + 10
                x = 10
            End If

        Next


    End Sub

    Public Sub CargaCalendarioProgramacion()
        Dim cmd As New SqlCommand("spCCCalendarioProgramacionCliente", Main.CnnSigamet)
        With cmd
            .CommandTimeout = 300
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
            .Parameters.Add("@Año", SqlDbType.SmallInt).Value = Now.Year
        End With

        AbreConexion()
        Dim dr As SqlDataReader = Nothing

        Try
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

        End Try

        Dim oDia As Calendario.Dia, _Color As System.Drawing.Color

        Fechas = New ArrayList()
        While dr.Read
            Select Case CType(dr("Tipo"), String)
                Case "S"
                    _Color = Color.MediumSeaGreen
                Case "P"
                    _Color = Color.DarkOrange
                Case Else
                    _Color = Color.White
            End Select

            oDia = New Calendario.Dia(CType(dr("Fecha"), Date), _Color, "Ultimo suministro")
            Fechas.Add(oDia)
        End While

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    CargaCalendarioProgramacion()

    '    Dim oCalendario As Control
    '    For Each oCalendario In Me.Controls
    '        If TypeOf oCalendario Is Calendario.Calendario Then
    '            CType(oCalendario, Calendario.Calendario).FechasSeleccionadas(Fechas)
    '            Me.Refresh()
    '        End If
    '    Next
    'End Sub
End Class
