Public Class Ruta80
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
    Friend WithEvents txtFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents btnAbrir As System.Windows.Forms.Button
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAbrir = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.btnCerrar = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtFecha
        '
        Me.txtFecha.Location = New System.Drawing.Point(104, 19)
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.Size = New System.Drawing.Size(232, 20)
        Me.txtFecha.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(24, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Fecha de ruta :"
        '
        'btnAbrir
        '
        Me.btnAbrir.Location = New System.Drawing.Point(347, 19)
        Me.btnAbrir.Name = "btnAbrir"
        Me.btnAbrir.TabIndex = 6
        Me.btnAbrir.Text = "Abrir Ruta"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Location = New System.Drawing.Point(344, 80)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.TabIndex = 7
        Me.btnCancelar.Text = "Cancelar"
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "sa;workstation id=DESARROLLO-4;packet size=4096"
        '
        'btnCerrar
        '
        Me.btnCerrar.Location = New System.Drawing.Point(344, 48)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.TabIndex = 10
        Me.btnCerrar.Text = "Cerrar Ruta"
        '
        'Ruta80
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(434, 112)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnCerrar, Me.txtFecha, Me.Label1, Me.btnAbrir, Me.btnCancelar})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Ruta80"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Manejo de la ruta 80"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub Ruta80_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        txtFecha.Value = Now.Date


    End Sub

    Private Sub btnAbrir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAbrir.Click
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Folio As Integer

        If MsgBox("¿Desea generar el inicio de la ruta 80 para el dia " + CType(txtFecha.Value.Date, String) + "?. ", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
            cmdInsert.Connection = SqlConnection
            cmdInsert.CommandTimeout = 100
            cmdInsert.Parameters.Clear()
            cmdInsert.CommandType = CommandType.Text
            cmdInsert.CommandText = "Select Max(Folio)+1 as Folio from AutoTanqueTurno where AñoAtt=Year(getdate())"
            rdrInsert = cmdInsert.ExecuteReader()
            rdrInsert.Read()
            Folio = CType(rdrInsert("Folio"), Integer)
            rdrInsert.Close()

            cmdInsert.Parameters.Clear()
            cmdInsert.CommandType = CommandType.Text
            cmdInsert.CommandText = "INSERT  INTO AutotanqueTurno (Ruta,FAsignacion,Folio,LitrosVendidos,FinicioRuta,FTerminoRuta,TotalizadorInicial,TotalizadorFinal,PesoSalida,PesoLlegada,PesoDiferencia,LitrosLiquidados,LitrosDiferencia,KilometrajeInicial,kilometrajeFinal,Kilometraje,PedidosenTarjeta,PedidosSurtidos,PedidosCancelados,Boletines,PedidosContado,PedidosCredito,ImporteCredito,ImporteContado,Celula,Autotanque,DestinoTransporte,ObservacionesInicioRuta,StatusBascula,Transportadora,OrigenTransporte,UsuarioApertura,UsuarioCierre,MotivoBaja,MotivoCambio,NumeroEmbarque,Porcentaje,PesoSinTara,PesoTaraLLeno,PesoTaraVacio,RemisionPemex,Presion100,PorcentajeGasInicial,LitrosGasNoVendido,PorcentajeGasNoVendido,Eficiencia,ImporteEficiencia,StatusLogistica,MecanismoPeso,PorcentajeEficiencia,PorcentajeImporteEficiencia,TipoAsignacion,LitrosGasInicial,ObservacionesCierreRuta,AñoAtt,PresionReal,OperadorVehiculoExterno,VehiculoExterno,PorcentajeLlenado,TipoProducto,TipoVehiculo,Capacidad)" & _
                                    "VALUES (@Ruta,@FAsignacion,@Folio," & _
                                    "@LitrosVendidos,@FinicioRuta,@FTerminoRuta, " & _
                                    "@TotalizadorInicial,@TotalizadorFinal,@PesoSalida," & _
                                    "@PesoLlegada,@PesoDiferencia,@LitrosLiquidados," & _
                                    "@LitrosDiferencia,@KilometrajeInicial,@KilometrajeFinal," & _
                                    "@Kilometraje,@PedidosenTarjeta,@PedidosSurtidos," & _
                                    "@PedidosCancelados,@Boletines,@PedidosContado," & _
                                    "@PedidosCredito,@ImporteCredito,@ImporteContado," & _
                                    "@Celula,@Autotanque,@DestinoTransporte," & _
                                    "@ObservacionesInicioRuta,@StatusBascula,@Transportadora," & _
                                    "@OrigenTransporte,@UsuarioApertura,@UsuarioCierre," & _
                                    "@MotivoBaja,@MotivoCambio,@NumeroEmbarque,@Porcentaje," & _
                                    "@PesoSinTara,@PesoTaraLLeno,@PesoTaraVacio,@RemisionPemex," & _
                                    "@Presion100,@PorcentajeGasInicial,@LitrosGasNoVendido,@PorcentajeGasNoVendido, " & _
                                    "@Eficiencia,@ImporteEficiencia,@StatusLogistica,@MecanismoPeso," & _
                                    "@PorcentajeEficiencia,@PorcentajeImporteEficiencia,@TipoAsignacion,@LitrosGasInicial,@ObservacionesCierreRuta,@AñoAtt, " & _
                                    "@PresionReal,@OperadorVehiculoExterno,@VehiculoExterno,@PorcentajeLlenado,@TipoProducto,@TipoVehiculo,@Capacidad)"
            cmdInsert.Parameters.Clear()

            With cmdInsert.Parameters
                .Add("@AñoAtt", SqlDbType.SmallInt).Value = CType(Today.Year, Integer)
                .Add("@Folio", SqlDbType.Int).Value = Folio
                .Add("@Ruta", SqlDbType.SmallInt).Value = 80 'SUPER JUVENIL DE MIGUEL 
                .Add("@FAsignacion", SqlDbType.DateTime).Value = txtFecha.Value.Date
                .Add("@FinicioRuta", SqlDbType.DateTime).Value = txtFecha.Value.Date
                .Add("@FTerminoRuta", SqlDbType.DateTime).Value = txtFecha.Value.Date
                .Add("@TotalizadorInicial", SqlDbType.Int).Value = 0
                .Add("@TotalizadorFinal", SqlDbType.Int).Value = 0
                .Add("@Porcentaje", SqlDbType.Decimal).Value = 0
                .Add("@PesoSalida", SqlDbType.Int).Value = 0
                .Add("@PesoLlegada", SqlDbType.Int).Value = 0
                .Add("@PesoDiferencia", SqlDbType.Int).Value = 0
                .Add("@LitrosLiquidados", SqlDbType.Int).Value = 1
                .Add("@LitrosDiferencia", SqlDbType.Int).Value = 0
                .Add("@KilometrajeInicial", SqlDbType.Int).Value = 0
                .Add("@KilometrajeFinal", SqlDbType.Int).Value = 0
                .Add("@Kilometraje", SqlDbType.Int).Value = 0
                .Add("@PedidosenTarjeta", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosSurtidos", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosCancelados", SqlDbType.SmallInt).Value = 0
                .Add("@Boletines", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosContado", SqlDbType.SmallInt).Value = 0
                .Add("@PedidosCredito", SqlDbType.SmallInt).Value = 0
                .Add("@ImporteCredito", SqlDbType.Money).Value = 0
                .Add("@ImporteContado", SqlDbType.Money).Value = 0
                .Add("@Celula", SqlDbType.TinyInt).Value = 6
                .Add("@Autotanque", SqlDbType.SmallInt).Value = 8888
                .Add("@DestinoTransporte", SqlDbType.SmallInt).Value = 1
                .Add("@ObservacionesInicioRuta", SqlDbType.VarChar).Value = ""
                .Add("@ObservacionesCierreRuta", SqlDbType.VarChar).Value = ""
                .Add("@StatusBascula", SqlDbType.Char).Value = "ABIERTO"
                .Add("@Transportadora", SqlDbType.TinyInt).Value = 1
                .Add("@OrigenTransporte", SqlDbType.SmallInt).Value = 1
                .Add("@UsuarioApertura", SqlDbType.Char).Value = GLOBAL_Usuario
                .Add("@UsuarioCierre", SqlDbType.Char).Value = ""
                .Add("@MotivoBaja", SqlDbType.VarChar).Value = ""
                .Add("@MotivoCambio", SqlDbType.VarChar).Value = ""
                .Add("@NumeroEmbarque", SqlDbType.Char).Value = ""
                .Add("@PesoSinTara", SqlDbType.Int).Value = 0
                .Add("@PesoTaraLLeno", SqlDbType.Int).Value = 0
                .Add("@PesoTaraVacio", SqlDbType.Int).Value = 0
                .Add("@RemisionPemex", SqlDbType.Char).Value = ""
                .Add("@Presion100", SqlDbType.Char).Value = "0"
                .Add("@PorcentajeGasInicial", SqlDbType.Decimal).Value = 0
                .Add("@LitrosVendidos", SqlDbType.Int).Value = 0
                .Add("@LitrosGasNoVendido", SqlDbType.Int).Value = 0
                .Add("@PorcentajeGasNoVendido", SqlDbType.Decimal).Value = 0
                .Add("@Eficiencia", SqlDbType.Int).Value = 0
                .Add("@ImporteEficiencia", SqlDbType.Money).Value = 0
                .Add("@StatusLogistica", SqlDbType.Char).Value = "INICIO"
                .Add("@MecanismoPeso", SqlDbType.TinyInt).Value = 1
                .Add("@PorcentajeEficiencia", SqlDbType.Decimal).Value = 0
                .Add("@PorcentajeImporteEficiencia", SqlDbType.Money).Value = 0
                .Add("@TipoAsignacion", SqlDbType.TinyInt).Value = 1
                .Add("@LitrosGasInicial", SqlDbType.Int).Value = 0
                .Add("@PresionReal", SqlDbType.Char).Value = ""
                .Add("@OperadorVehiculoExterno", SqlDbType.Char).Value = ""
                .Add("@VehiculoExterno", SqlDbType.Char).Value = "99999"
                .Add("@PorcentajeLlenado", SqlDbType.Decimal).Value = 0
                .Add("@TipoProducto", SqlDbType.TinyInt).Value = 1
                .Add("@TipoVehiculo", SqlDbType.TinyInt).Value = 1
                .Add("@Capacidad", SqlDbType.Int).Value = 0
            End With

            cmdInsert.ExecuteNonQuery()
            cmdInsert.Dispose()
            SqlConnection.Close()

            MsgBox("La ruta ha sido iniciada.", MsgBoxStyle.Information, "Mensaje del sistema")
            Me.Close()

        End If

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing

        If MsgBox("¿Desea generar el cierre de la ruta 80 para el dia " + CType(txtFecha.Value.Date, String) + "?. ", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
            cmdInsert.Connection = SqlConnection
            cmdInsert.CommandTimeout = 100
            cmdInsert.CommandType = CommandType.Text
            cmdInsert.CommandText = "Update AutoTanqueTurno set StatusBascula='CERRADO', StatusLogistica='CIERRE' where AñoAtt=Year(@Fecha) and Ruta=80 and FInicioRuta=@Fecha "
            cmdInsert.Parameters.Clear()

            With cmdInsert.Parameters
                .Add("@Fecha", SqlDbType.DateTime).Value = txtFecha.Value.Date
            End With

            cmdInsert.ExecuteNonQuery()
            cmdInsert.Dispose()
            SqlConnection.Close()

            MsgBox("La ruta ha sido cerrada.", MsgBoxStyle.Information, "Mensaje del sistema")
            Me.Close()
        End If


    End Sub
End Class
