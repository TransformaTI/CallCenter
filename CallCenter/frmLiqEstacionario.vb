Imports System.Data.SqlClient

Public Class frmLiqEstacionario
    Inherits System.Windows.Forms.Form

    Private _AñoAtt As Short
    Private _Folio As Integer
    Private oATT As SigaMetClasses.cAutotanqueTurno
    Private _Titulo As String = "Liquidación"
    Private WithEvents oPed As ctlLiquidacion
    'Private _Ruta As Short
    'Private _Celula As Byte
    'Private _Autotanque As Short
    'Private _LitrosLiquidados As Integer


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
    Friend WithEvents pnlPedido As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents InfoTripulacion As SigaMetClasses.InfoTripulacionTurno
    Friend WithEvents Label1 As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlPedido = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.InfoTripulacion = New SigaMetClasses.InfoTripulacionTurno()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'pnlPedido
        '
        Me.pnlPedido.AutoScroll = True
        Me.pnlPedido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlPedido.Location = New System.Drawing.Point(8, 96)
        Me.pnlPedido.Name = "pnlPedido"
        Me.pnlPedido.Size = New System.Drawing.Size(688, 256)
        Me.pnlPedido.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(8, 456)
        Me.Button1.Name = "Button1"
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Button1"
        '
        'InfoTripulacion
        '
        Me.InfoTripulacion.AñoAtt = CType(0, Short)
        Me.InfoTripulacion.Folio = 0
        Me.InfoTripulacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InfoTripulacion.Location = New System.Drawing.Point(8, 8)
        Me.InfoTripulacion.Name = "InfoTripulacion"
        Me.InfoTripulacion.Size = New System.Drawing.Size(472, 80)
        Me.InfoTripulacion.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(336, 368)
        Me.Label1.Name = "Label1"
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Label1"
        '
        'frmLiqEstacionario
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(704, 501)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label1, Me.InfoTripulacion, Me.Button1, Me.pnlPedido})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmLiqEstacionario"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmLiqEstacionario"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Public Sub New(ByVal AñoAtt As Short, ByVal Folio As Integer)
        MyBase.New()
        InitializeComponent()

        _AñoAtt = AñoAtt
        _Folio = Folio

        oATT = New SigaMetClasses.cAutotanqueTurno(_AñoAtt, _Folio)

        InfoTripulacion.CargaDatos(_AñoAtt, _Folio)

        CargaDatos()

    End Sub

    Private Sub CargaDatos()

        Dim conn As New SqlConnection(SigaMetClasses.LeeInfoConexion(False, True))
        Try
            conn.Open()
        Catch
            MessageBox.Show(SigaMetClasses.M_NO_CONEXION, _Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        Dim cmd As New SqlCommand("spLIQConsultaPedidos", conn)
        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = oATT.Ruta
        End With

        Dim dr As SqlDataReader

        Dim _Top As Integer = 0

        Try
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            While dr.Read
                oPed = New ctlLiquidacion(CType(dr("Pedido"), Integer), CType(dr("Cliente"), Integer), CType(dr("Nombre"), String).Trim, 0, Main.GLOBAL_Precio)
                oPed.Top = _Top
                oPed.Show()

                Me.pnlPedido.Controls.Add(oPed)

                _Top += 21
            End While

        Catch ex As Exception
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Not IsNothing(conn) Then
                If conn.State = ConnectionState.Open Then conn.Close()
            End If
        End Try
        

    End Sub


    Private Sub RecalculaPedido()
        Dim _Ped As ctlLiquidacion
        Dim _TotalImporte, _TotalLitros As Decimal

        For Each _Ped In Me.pnlPedido.Controls
            _TotalImporte += _Ped.Importe
            _TotalLitros += _Ped.Litros
        Next

        Me.Label1.Text = _TotalImporte.ToString("N")
    End Sub

    Private Sub oPed_HaCambiado() Handles oPed.HaCambiado
        RecalculaPedido()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        RecalculaPedido()

    End Sub
End Class