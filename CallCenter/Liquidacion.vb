Public Class Liquidacion
    Inherits System.Windows.Forms.Form
    Private _Folio As Integer
    Private _AñoAtt As Short

    Private _Ruta As Integer
    Private _Celula As Integer
    Private _Linea As Integer
    Private _Linea2 As Integer
    Private _Cambio As String
    Private _Cambio2 As String
    Private _ClienteGlobal As Integer
    Private _Descarga As Boolean
    Private _CorrerDescarga As Boolean 'Indica si se descargó la tarjeta rampac.  Viene de la selección de la ruta.
    Private _Fecha As DateTime
    Private _Totalizador As Integer
    Private TotalContado As Decimal
    Private TotalCredito As Decimal
    Private TotalImporte As Decimal
    'TODO: validacion añadida el día 08/10/2004 declaracion de variable
    Dim _aplicaValidacionCreditocliente As Boolean

    Public Sub Entrada(ByVal Fecha As Date, _
                       ByVal Ruta As Integer, _
                       ByVal Folio As Integer, _
                       ByVal Anio As Integer, _
                       ByVal Descarga As Boolean, _
                       ByVal Celula As Integer)

        Cursor = Cursors.WaitCursor

        _Folio = Folio
        _AñoAtt = CType(Anio, Short)
        _Celula = Celula

        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader

        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
            'TODO NO DEBE ABRIR DESDE EL PRINCIPIO
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        cmdOperador.Parameters("@Folio").Value = Folio
        cmdOperador.Parameters("@Anio").Value = Anio
        daOperador.Fill(DsLiquidacion, "Operador")

        If DsLiquidacion.Operador.Count = 0 Then
            MsgBox("No existe tripulación en esta ruta para poder liquidar. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
            Cursor = Cursors.Default
            Me.Close()
            Exit Sub

        Else
            Me.Text = "Liquidación para la ruta " + CType(Ruta, String)
            _Ruta = Ruta

            lbOperador.Text = DsLiquidacion.Operador(0).Nombre
            lbAutotanque.Text = CType(DsLiquidacion.Operador(0).Autotanque, String)
            lbFecha.Text = Fecha.ToShortDateString
            _Totalizador = CType(DsLiquidacion.Operador(0).LitrosLiquidados, Integer)
            _Fecha = Fecha

            Try
                DsLiquidacion.TipoPago.Clear()
                DsLiquidacion.TipoPago.AddTipoPagoRow(1, "Efectivo")
                DsLiquidacion.TipoPago.AddTipoPagoRow(2, "Cheque")
                DsLiquidacion.TipoPago.AddTipoPagoRow(3, "Ficha deposito")
                DsLiquidacion.TipoPago.AddTipoPagoRow(4, "Tarjeta credito")
                DsLiquidacion.TipoPago.AddTipoPagoRow(5, "Credito GM")
                DsLiquidacion.TipoPago.AddTipoPagoRow(6, "Credito Operador")

                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandTimeout = 30                
                'cmdInsert.CommandText = "Select Cliente from ClienteVentaPublico where Celula =@Celula"
                'cmdInsert.Parameters.Clear()
                'cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                '***********************************************************************************************************
                'TODO: 14/12/04 Desactivado para permitir la carga del cliente VPG, según la ruta a liquidar ·JAG·
                '***********************************************************************************************************
                cmdInsert.CommandText = "SELECT Cliente FROM ClienteVentaPublico AS VPG JOIN Ruta AS R ON VPG.Celula = R.Celula " & _
                            "WHERE Ruta = @Ruta"
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta

                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                _ClienteGlobal = CType(rdrInsert("Cliente"), Integer)
                rdrInsert.Close()
                cmdInsert.Dispose()

            Catch ioEx As Exception
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try
            _CorrerDescarga = Descarga

            CargarGrid()
            CargarGrid2()
            _Descarga = False

            Dim Paneles As System.Windows.Forms.Control
            Dim Valor As Integer
            Valor = 2000001

            For Each Paneles In pnNotas.Controls
                If Paneles.Name.Substring(0, 5) = "txtNN" Then
                    Paneles.Text = CType(Valor, String)
                End If
                Valor = Valor + 1
            Next


            If CType(lstClientes.Items(0), String) = "" Then
                lstClientes.Items(0) = "0"
            End If


            If Descarga = True Then
                DescargaRampac()
            End If
        End If

        Try
            If CType(lstClientes.Items(0), String) <> "" Then
                cmbForma0.Select()
            End If
            txtNNota0.Select()
        Catch ex As Exception
        End Try

        Cursor = Cursors.Default
        Me.Show()

    End Sub

#Region "Sin revisar"
    Private Function ValidarPrecios() As Boolean
        Dim i As Integer
        Dim Regreso As Boolean

        Regreso = False
        'TODO: Preguntar sobre el precio
        For i = 0 To lstPrecio2.Items.Count - 1
            If (CType(lstPrecio2.Items(i), Decimal) <> GLOBAL_Precio) And (CType(lstPrecio2.Items(i), Decimal) <> GLOBAL_PrecioAnterior) And (CType(lstPrecio2.Items(i), Decimal) <> GLOBAL_PrecioToluca) And (CType(lstPrecio2.Items(i), Decimal) <> GLOBAL_PrecioAnteriorToluca) Then
                Regreso = True
            End If
        Next

        For i = 0 To lstPrecio.Items.Count - 1
            If (CType(lstPrecio.Items(i), Decimal) <> GLOBAL_Precio) And (CType(lstPrecio.Items(i), Decimal) <> GLOBAL_PrecioAnterior) And (CType(lstPrecio.Items(i), Decimal) <> GLOBAL_PrecioToluca) And (CType(lstPrecio.Items(i), Decimal) <> GLOBAL_PrecioAnteriorToluca) Then
                Regreso = True
            End If
        Next


        Return Regreso
    End Function


    Private Sub KeyDownRemision(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        If CType(sender, TextBox).Name.Substring(0, 4) = "txtL" Then
            If ((e.KeyCode = 38 And CType(CType(sender, TextBox).Tag, Integer) <> 0) Or (e.KeyCode = 39) Or (e.KeyCode = 40 And CType(CType(sender, TextBox).Tag, Integer) <> 59)) Then
                Dim Paneles As System.Windows.Forms.Control
                For Each Paneles In pnPedidos.Controls

                    If Paneles.Name.Substring(0, 4) = "txtL" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) - 1 And e.KeyCode = 38 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 4) = "txtL" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) + 1 And e.KeyCode = 40 Then
                        Paneles.Focus()
                    End If

                Next
            End If
        End If

    End Sub

    Private Sub KeyDownNotas(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        If CType(sender, TextBox).Name.Substring(0, 5) = "txtNN" Then
            If ((e.KeyCode = 38 And CType(CType(sender, TextBox).Tag, Integer) <> 0) Or (e.KeyCode = 39) Or (e.KeyCode = 40 And CType(CType(sender, TextBox).Tag, Integer) <> 59)) Then
                Dim cmdInsert As New SqlClient.SqlCommand()
                Dim rdrInsert As SqlClient.SqlDataReader
                Dim Paneles As System.Windows.Forms.Control
                Dim i As Integer
                Dim Registro As Integer

                If CType(sender, TextBox).Text <> "" Then
                    Try
                        cmdInsert.Connection = SqlConnection
                        cmdInsert.CommandTimeout = 30
                        cmdInsert.CommandType = CommandType.Text
                        cmdInsert.CommandText = "Select Count(*) as Registro from NotaBlanca where Codigo=@Codigo "
                        cmdInsert.Parameters.Clear()
                        cmdInsert.Parameters.Add("@Codigo", SqlDbType.Char).Value = CType(sender, TextBox).Text
                        rdrInsert = cmdInsert.ExecuteReader
                        rdrInsert.Read()
                        Registro = CType(rdrInsert("Registro"), Integer)
                        rdrInsert.Close()
                        If Registro > 0 Then
                            For i = 0 To lstNotas.Items.Count - 1
                                If CType(lstNotas.Items(i), String) = CType(CType(sender, TextBox).Text, String) And i <> CType(CType(sender, TextBox).Tag, Integer) Then
                                    MsgBox("Esta nota blanca ya la capturo. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                                    CType(sender, TextBox).Select()
                                    Exit Sub
                                End If
                            Next

                            For Each Paneles In pnNotas.Controls
                                If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                                    Paneles.Enabled = True
                                    If Paneles.Name.Substring(0, 5) = "txtNL" Then
                                        If Paneles.Text = "0" Then
                                            CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                                        Else
                                            CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = True
                                        End If
                                    End If

                                    If Paneles.Name.Substring(0, 5) = "txtNP" Then
                                        CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                                    End If
                                End If
                            Next

                            For Each Paneles In pnNotas.Controls
                                If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = (CType(CType(sender, TextBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                                    Paneles.Focus()
                                End If

                                If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) And e.KeyCode = 39 Then
                                    Paneles.Focus()
                                End If

                                If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = (CType(CType(sender, TextBox).Tag, Integer) + 1) And e.KeyCode = 40 Then
                                    Paneles.Focus()
                                End If
                            Next
                        Else
                            MsgBox("Esta nota blanca no existe o ya fue utilizada, Verifique.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                            CType(sender, TextBox).Select()
                            Exit Sub
                        End If
                    Catch ioEx As Exception
                        MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
                    End Try
                Else
                    For Each Paneles In pnNotas.Controls
                        If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = (CType(CType(sender, TextBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                            Paneles.Focus()
                        End If

                        If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) And e.KeyCode = 39 Then
                            Paneles.Focus()
                        End If

                        If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) + 1 And e.KeyCode = 40 Then
                            Paneles.Focus()
                        End If
                    Next
                End If

            End If
        End If


        If CType(sender, TextBox).Name.Substring(0, 5) = "txtNC" Then
            If ((e.KeyCode = 38 And CType(CType(sender, TextBox).Tag, Integer) <> 0) Or (e.KeyCode = 39) Or (e.KeyCode = 37) Or (e.KeyCode = 40 And CType(CType(sender, TextBox).Tag, Integer) <> 59)) Then
                Dim Paneles As System.Windows.Forms.Control
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) And e.KeyCode = 37 Then
                        Paneles.Focus()
                    End If


                    If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) And e.KeyCode = 39 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = (CType(CType(sender, TextBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = (CType(CType(sender, TextBox).Tag, Integer) + 1) And e.KeyCode = 40 Then
                        Paneles.Focus()
                    End If

                Next
            End If
        End If

        If CType(sender, TextBox).Name.Substring(0, 5) = "txtNL" Then
            If ((e.KeyCode = 38 And CType(CType(sender, TextBox).Tag, Integer) <> 0) Or (e.KeyCode = 39) Or (e.KeyCode = 37) Or (e.KeyCode = 40 And CType(CType(sender, TextBox).Tag, Integer) <> 59)) Then
                Dim Paneles As System.Windows.Forms.Control
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) And e.KeyCode = 37 Then
                        Paneles.Focus()
                    End If


                    If Paneles.Name.Substring(0, 5) = "cmbNF" And CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) And e.KeyCode = 39 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = (CType(CType(sender, TextBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = (CType(CType(sender, TextBox).Tag, Integer) + 1) And e.KeyCode = 40 Then
                        Paneles.Focus()
                    End If

                Next
            End If
        End If

    End Sub

    Private Sub KeyDownNotas2(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)

        If CType(sender, ComboBox).Name.Substring(0, 5) = "txtNN" Then
            If ((e.KeyCode = 38 And CType(CType(sender, ComboBox).Tag, Integer) <> 0) Or (e.KeyCode = 39) Or (e.KeyCode = 40 And CType(CType(sender, ComboBox).Tag, Integer) <> 59)) Then
                Dim cmdInsert As New SqlClient.SqlCommand()
                Dim rdrInsert As SqlClient.SqlDataReader
                Dim Paneles As System.Windows.Forms.Control
                Dim i As Integer
                Dim Registro As Integer

                If CType(sender, ComboBox).Text <> "" Then
                    Try
                        cmdInsert.Connection = SqlConnection
                        cmdInsert.CommandTimeout = 30
                        cmdInsert.CommandType = CommandType.Text
                        cmdInsert.CommandText = "Select Count(*) as Registro from NotaBlanca where Codigo=@Codigo "
                        cmdInsert.Parameters.Clear()
                        cmdInsert.Parameters.Add("@Codigo", SqlDbType.Char).Value = CType(sender, ComboBox).Text
                        rdrInsert = cmdInsert.ExecuteReader
                        rdrInsert.Read()
                        Registro = CType(rdrInsert("Registro"), Integer)
                        rdrInsert.Close()
                        If Registro > 0 Then
                            For i = 0 To lstNotas.Items.Count - 1
                                If CType(lstNotas.Items(i), String) = CType(CType(sender, ComboBox).Text, String) And i <> CType(CType(sender, ComboBox).Tag, Integer) Then
                                    MsgBox("Esta nota blanca ya la capturo. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                                    CType(sender, ComboBox).Select()
                                    Exit Sub
                                End If
                            Next

                            For Each Paneles In pnNotas.Controls
                                If CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) Then
                                    Paneles.Enabled = True
                                    If Paneles.Name.Substring(0, 5) = "txtNL" Then
                                        If Paneles.Text = "0" Then
                                            CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                                        Else
                                            CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = True
                                        End If
                                    End If

                                    If Paneles.Name.Substring(0, 5) = "txtNP" Then
                                        CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                                    End If
                                End If
                            Next

                            For Each Paneles In pnNotas.Controls
                                If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = (CType(CType(sender, ComboBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                                    Paneles.Focus()
                                End If

                                If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) And e.KeyCode = 39 Then
                                    Paneles.Focus()
                                End If

                                If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = (CType(CType(sender, ComboBox).Tag, Integer) + 1) And e.KeyCode = 40 Then
                                    Paneles.Focus()
                                End If
                            Next
                        Else
                            MsgBox("Esta nota blanca no existe o ya fue utilizada, Verifique.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                            CType(sender, ComboBox).Select()
                            Exit Sub
                        End If
                    Catch ioEx As Exception
                        MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
                    End Try
                Else
                    For Each Paneles In pnNotas.Controls
                        If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = (CType(CType(sender, ComboBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                            Paneles.Focus()
                        End If

                        If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) And e.KeyCode = 39 Then
                            Paneles.Focus()
                        End If

                        If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) + 1 And e.KeyCode = 40 Then
                            Paneles.Focus()
                        End If
                    Next
                End If

            End If
        End If


        If CType(sender, ComboBox).Name.Substring(0, 5) = "txtNC" Then
            If ((e.KeyCode = 38 And CType(CType(sender, ComboBox).Tag, Integer) <> 0) Or (e.KeyCode = 39) Or (e.KeyCode = 37) Or (e.KeyCode = 40 And CType(CType(sender, ComboBox).Tag, Integer) <> 59)) Then
                Dim Paneles As System.Windows.Forms.Control
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) And e.KeyCode = 37 Then
                        Paneles.Focus()
                    End If


                    If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) And e.KeyCode = 39 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = (CType(CType(sender, ComboBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = (CType(CType(sender, ComboBox).Tag, Integer) + 1) And e.KeyCode = 40 Then
                        Paneles.Focus()
                    End If

                Next
            End If
        End If

        If CType(sender, ComboBox).Name.Substring(0, 5) = "txtNL" Then
            If ((e.KeyCode = 38 And CType(CType(sender, ComboBox).Tag, Integer) <> 0) Or (e.KeyCode = 39) Or (e.KeyCode = 37) Or (e.KeyCode = 40 And CType(CType(sender, ComboBox).Tag, Integer) <> 59)) Then
                Dim Paneles As System.Windows.Forms.Control
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) And e.KeyCode = 37 Then
                        Paneles.Focus()
                    End If


                    If Paneles.Name.Substring(0, 5) = "cmbNF" And CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) And e.KeyCode = 39 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = (CType(CType(sender, ComboBox).Tag, Integer) - 1) And e.KeyCode = 38 Then
                        Paneles.Focus()
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = (CType(CType(sender, ComboBox).Tag, Integer) + 1) And e.KeyCode = 40 Then
                        Paneles.Focus()
                    End If

                Next
            End If
        End If
    End Sub

    Private Function Disponibles() As Boolean
        Dim j As Integer
        Dim Regreso As Boolean

        Regreso = False
        For j = 0 To DsLiquidacion.Documento.Count - 1
            If DsLiquidacion.Documento(j).Disponible > 0 Then
                Regreso = True
            End If
        Next

        Return Regreso
    End Function

    Private Function ValidarNumeroNota() As Boolean
        Dim i As Integer
        Dim Regreso As Boolean

        Regreso = False

        For i = 0 To lstLitros2.Items.Count - 1
            If CType(lstLitros2.Items(i), String) <> "0" Then
                If CType(lstClientes2.Items(i), String) <> "0" Then
                    If CType(lstNotas.Items(i), String) = "" Then
                        Regreso = True
                        Exit For
                    End If
                End If
            End If
        Next

        Return Regreso
    End Function

    Private Function ValidarNumeroCliente() As Boolean
        Dim i As Integer
        Dim Regreso As Boolean

        Regreso = False

        For i = 0 To lstLitros2.Items.Count - 1
            If CType(lstLitros2.Items(i), String) <> "0" Then
                If CType(lstClientes2.Items(i), String) = "0" Then
                    Regreso = True
                    Exit For
                End If
            End If
        Next

        Return Regreso
    End Function

    Private Function ValidarNumeroLitros() As Boolean
        Dim i As Integer
        Dim Regreso As Boolean

        Regreso = False

        For i = 0 To lstLitros2.Items.Count - 1
            If CType(lstLitros2.Items(i), String) = "0" Then
                If CType(lstNotas.Items(i), String) <> "" Then
                    If CType(lstClientes2.Items(i), String) <> "0" Then
                        Regreso = True
                        Exit For
                    End If
                End If
            End If
        Next

        Return Regreso

    End Function

    Private Sub DescargaRampac()
        Dim Paneles As System.Windows.Forms.Control
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim i As Integer
        Dim j As Integer = Nothing
        Dim ExisteCliente As Boolean
        Dim Registro As Integer
        Dim cn As SqlClient.SqlConnection

        Try
            cn = New SqlClient.SqlConnection(GLOBAL_ConString)
            cn.Open()

            _Descarga = True

            For Each Paneles In pnPedidos.Controls
                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = True
                    Paneles.BackColor = Color.White
                End If

                If Paneles.Name.Substring(0, 4) = "txtP" Then
                    'If _Ruta <> 150 Then
                    '    CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = True
                    'Else
                    CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                    'End If
                    Paneles.BackColor = Color.PaleGoldenrod
                End If
            Next


            For Each Paneles In pnNotas.Controls

                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = True
                    'Paneles.BackColor = Color.Black
                End If

                If Paneles.Name.Substring(0, 5) = "txtNP" Then
                    'If _Ruta <> 150 Then
                    '    CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = True
                    'Else
                    CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                    'End If
                    'Paneles.BackColor = Color.DarkRed
                End If
            Next

            cmdInsert.Connection = cn
            cmdInsert.CommandTimeout = 30

            cmdInsert.CommandText = "Select Count(*) as Registro from Rampac where Folio=@Folio and AñoAtt=@AñoAtt and TipoOperacion='Duplicado' "
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
            cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
            rdrInsert = cmdInsert.ExecuteReader()
            rdrInsert.Read()
            Registro = CType(rdrInsert("Registro"), Integer)
            rdrInsert.Close()

            If Registro > 0 Then
                lbUnificacion.Visible = True
            Else
                lbUnificacion.Visible = False
            End If

            If DsLiquidacion.Pedido.Count > 0 Then
                If _Celula <> 6 Then
                    cmdInsert.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                            " Select Cliente, SUM(Litros) as Litros , SUM(Importe) as Importe, (SUM(Importe)/SUM(Litros)) as Precio, FormaPago, Ruta, AñoAtt, Folio, Autotanque " & _
                                                        " from Rampac R " & _
                                                        " Where AñoAtt=@AñoAtt and Folio=@Folio and TipoOperacion  in ('Tarjeta','Duplicado') and Cliente<>0 " & _
                                                        " and Cliente in (select Cliente from Rampac where AñoAtt=R.AñoAtt and Folio=R.Folio and TipoOperacion='Tarjeta') " & _
                                                        " Group by Cliente, FormaPago, Ruta, AñoAtt, Folio, Autotanque " & _
                                                        " Order by  Litros asc  " & _
                                            " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                    cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
                Else
                    cmdInsert.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                            " Select Cliente, SUM(Litros) as Litros , (SUM(Importe)*1.15) as Importe, ((SUM(Importe)*1.15)/SUM(Litros)) as Precio, FormaPago, Ruta, AñoAtt, Folio, Autotanque " & _
                                                        " from Rampac R " & _
                                                        " Where AñoAtt=@AñoAtt and Folio=@Folio and TipoOperacion  in ('Tarjeta','Duplicado') and Cliente<>0 " & _
                                                        " and Cliente in (select Cliente from Rampac where AñoAtt=R.AñoAtt and Folio=R.Folio and TipoOperacion='Tarjeta') " & _
                                                        " Group by Cliente, FormaPago, Ruta, AñoAtt, Folio, Autotanque " & _
                                                        " Order by  Litros asc  " & _
                                            " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                    cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
                End If


                rdrInsert = cmdInsert.ExecuteReader()
                While rdrInsert.Read
                    For i = 0 To lstClientes.Items.Count - 1
                        If CType(lstClientes.Items(i), Integer) = CType(rdrInsert("Cliente"), Integer) Then

                            For Each Paneles In pnPedidos.Controls
                                If Paneles.Name.Substring(0, 4) = "txtL" And CType(Paneles.Tag, Integer) = i Then
                                    Paneles.Text = Format(rdrInsert("Litros"), "#.00")
                                End If

                                If Paneles.Name.Substring(0, 4) = "txtP" And CType(Paneles.Tag, Integer) = i Then
                                    Paneles.Text = Format(rdrInsert("Precio"), "#.00")
                                End If

                                If Paneles.Name.Substring(0, 4) = "cmbF" And CType(Paneles.Tag, Integer) = i Then
                                    If CType(rdrInsert("FormaPago"), String) = "Contado" Then
                                        Paneles.Text = "CONTADO"
                                    Else
                                        Paneles.Text = "CREDITO"
                                    End If
                                End If
                            Next

                        End If
                    Next
                End While
                rdrInsert.Close()
            End If


            If _Celula <> 6 Then
                cmdInsert.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " Select Cliente, SUM(Litros) as Litros , SUM(Importe) as Importe, (SUM(Importe)/SUM(Litros)) as Precio, FormaPago, Ruta, AñoAtt, Folio, Autotanque,0 as Consecutivo " & _
                                                " from Rampac R " & _
                                                " Where AñoAtt=@AñoAtt and Folio=@Folio and TipoOperacion  in ('NotaBlanca','Duplicado') and Cliente<>0 " & _
                                                " and Cliente in (select Cliente from Rampac where AñoAtt=R.AñoAtt and Folio=R.Folio and TipoOperacion='NotaBlanca') " & _
                                                " Group by Cliente, FormaPago, Ruta, AñoAtt, Folio, Autotanque " & _
                                                " Union " & _
                                                " Select Cliente, Litros, Importe, (Importe/Litros) as Precio, FormaPago, Ruta, AñoAtt, Folio, Autotanque, Consecutivo as Consecutivo " & _
                                                " from Rampac R " & _
                                                " Where AñoAtt=@AñoAtt and Folio=@Folio and TipoOperacion  in ('NotaBlanca','Duplicado') and Cliente=0 " & _
                                                " Order by 2 asc  " & _
                                        " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
            Else
                cmdInsert.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " Select Cliente, SUM(Litros) as Litros , (SUM(Importe)*1.15) as Importe, ((SUM(Importe)*1.15)/SUM(Litros)) as Precio, FormaPago, Ruta, AñoAtt, Folio, Autotanque,0 as Consecutivo " & _
                                    " from Rampac R " & _
                                    " Where AñoAtt=@AñoAtt and Folio=@Folio and TipoOperacion  in ('NotaBlanca','Duplicado') and Cliente<>0 " & _
                                    " and Cliente in (select Cliente from Rampac where AñoAtt=R.AñoAtt and Folio=R.Folio and TipoOperacion='NotaBlanca') " & _
                                    " Group by Cliente, FormaPago, Ruta, AñoAtt, Folio, Autotanque " & _
                                    " Union " & _
                                    " Select Cliente, Litros, (Importe*1.15) as Importe, ((Importe*1.15)/Litros) as Precio, FormaPago, Ruta, AñoAtt, Folio, Autotanque, Consecutivo as Consecutivo " & _
                                    " from Rampac R " & _
                                    " Where AñoAtt=@AñoAtt and Folio=@Folio and TipoOperacion  in ('NotaBlanca','Duplicado') and Cliente=0 " & _
                                    " Order by 2 asc  " & _
                                    " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
            End If

            rdrInsert = cmdInsert.ExecuteReader()
            i = 0
            While rdrInsert.Read
                ExisteCliente = True

                For Each Paneles In pnNotas.Controls
                    If Paneles.Name.Substring(0, 5) = "txtNC" And CType(Paneles.Tag, Integer) = i Then
                        If CType(rdrInsert("Cliente"), Integer) <> 0 And ExisteCliente = True Then
                            Paneles.Text = CType(rdrInsert("Cliente"), String)
                        End If
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = i And ExisteCliente = True Then
                        Paneles.Text = Format(rdrInsert("Litros"), "#.00")
                    End If

                    If Paneles.Name.Substring(0, 5) = "txtNP" And CType(Paneles.Tag, Integer) = i And ExisteCliente = True Then
                        Paneles.Text = Format(rdrInsert("Precio"), "#.00")
                    End If

                    If Paneles.Name.Substring(0, 5) = "cmbNF" And CType(Paneles.Tag, Integer) = i And ExisteCliente = True Then
                        If CType(rdrInsert("FormaPago"), String) = "Contado" Then
                            Paneles.Text = "CONTADO"
                        Else
                            Paneles.Text = "CREDITO"
                        End If
                    End If
                Next

                i = i + 1
            End While
            rdrInsert.Close()


            For i = 0 To lstLitros2.Items.Count - 1
                If CType(lstLitros2.Items(i), String) = "0" Then
                    For Each Paneles In pnNotas.Controls
                        If Paneles.Name.Substring(0, 5) = "txtNL" And CType(Paneles.Tag, Integer) = i Then
                            Paneles.BackColor = Color.White
                        End If

                        If Paneles.Name.Substring(0, 5) = "txtNP" And CType(Paneles.Tag, Integer) = i Then
                            Paneles.BackColor = Color.PaleGoldenrod
                        End If
                    Next
                End If
            Next
            cn.Close()
        Catch ioEx As Exception
            MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try
    End Sub

    Private Sub CalculoNotas()
        Dim NotasTotal As Integer
        Dim NotasRemision As Integer = Nothing
        Dim NotasRemisionSurtida As Integer
        Dim NotasRemisionNoSurtida As Integer
        Dim NotasBlancas As Integer
        Dim i As Integer

        NotasTotal = 0
        NotasRemisionSurtida = 0
        NotasRemisionNoSurtida = 0
        NotasBlancas = 0
        For i = 0 To lstLitros2.Items.Count - 1
            If CType(lstLitros2.Items(i), String) <> "0" Then
                NotasTotal = NotasTotal + 1
            End If
        Next

        For i = 0 To lstLitros.Items.Count - 1
            If CType(lstLitros.Items(i), String) <> "0" Then
                NotasRemisionSurtida = NotasRemisionSurtida + 1
            End If
        Next

        lbTotalNotaRemision.Text = CType(lstPrecio.Items.Count, String) + " notas."
        lbTotalRemisionSurtida.Text() = CType(NotasRemisionSurtida, String) + " notas."
        If (lstPrecio.Items.Count - NotasRemisionSurtida) < 0 Then
            lbTotalRemisionNo.Text = CType((0), String) + " notas."
        Else
            lbTotalRemisionNo.Text = CType((lstPrecio.Items.Count - NotasRemisionSurtida), String) + " notas."
        End If

        lbNotasBlancas.Text = CType((NotasTotal), String) + " notas."
        lbTotal.Text = CType((NotasTotal + NotasRemisionSurtida), String) + " notas."


    End Sub

    Private Sub SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Paneles As System.Windows.Forms.Control

        lstTipoPago.Items.Clear()
        For Each Paneles In pnPedidos.Controls
            If Paneles.Name.Substring(0, 4) = "cmbT" Then
                lstTipoPago.Items.Add(Paneles.Text)
            End If
        Next

        If CType(sender, ComboBox).Text = "Tarjeta credito" Then
            Try
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandTimeout = 30
                cmdInsert.CommandText = "Select Count(*) as Registro  from TarjetaCredito TC INNER JOIN Cliente C ON TC.Cliente=C.Cliente where TC.Status='ACTIVA' and C.Cliente=@Cliente "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes.Items(CType(CType(sender, ComboBox).Tag, Integer))
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                If CType(rdrInsert("Registro"), Integer) = 0 Then
                    MsgBox("Este Cliente no tiene una tarjeta de credito ACTIVA en el sistema. No puede liquidar de esta forma.")
                    CType(sender, ComboBox).SelectedIndex = 1
                End If
                rdrInsert.Close()
            Catch ioEx As Exception
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try
        End If

        If CType(sender, ComboBox).Text = "Credito GM" Then
            Try
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandText = "Select Count(*) as Registro from Cliente where FormaPago=2 and Cliente=@Cliente "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes.Items(CType(CType(sender, ComboBox).Tag, Integer))
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                If CType(rdrInsert("Registro"), Integer) = 0 Then
                    MsgBox("Este Cliente no tiene asignado credito por GAS METROPOLITANO. No puede liquidar de esta forma.")
                    CType(sender, ComboBox).SelectedIndex = 1
                End If
                rdrInsert.Close()
            Catch ioEx As Exception
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try
        End If

        cmdInsert.Dispose()
    End Sub

    Private Sub SelectedIndexChanged2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Paneles As System.Windows.Forms.Control

        lstTipoPago2.Items.Clear()
        For Each Paneles In pnNotas.Controls
            If Paneles.Name.Substring(0, 5) = "cmbNT" Then
                lstTipoPago2.Items.Add(Paneles.Text)
            End If
        Next

        If CType(sender, ComboBox).Text = "Tarjeta credito" Then
            Try
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandTimeout = 30
                cmdInsert.CommandText = "Select Count(*) as Registro  from TarjetaCredito TC INNER JOIN Cliente C ON TC.Cliente=C.Cliente where TC.Status='ACTIVA' and C.Cliente=@Cliente "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes2.Items(CType(CType(sender, ComboBox).Tag, Integer))
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                If CType(rdrInsert("Registro"), Integer) = 0 Then
                    MsgBox("Este Cliente no tiene una tarjeta de credito ACTIVA en el sistema. No puede liquidar de esta forma.")
                    CType(sender, ComboBox).SelectedIndex = 1
                End If
                rdrInsert.Close()
            Catch ioEx As Exception
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try
        End If

        If CType(sender, ComboBox).Text = "Credito GM" Then
            Try
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandText = "Select Count(*) as Registro from Cliente where FormaPago=2 and Cliente=@Cliente "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes2.Items(CType(CType(sender, ComboBox).Tag, Integer))
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                If CType(rdrInsert("Registro"), Integer) = 0 Then
                    MsgBox("Este Cliente no tiene asignado credito por GAS METROPOLITANO. No puede liquidar de esta forma.")
                    CType(sender, ComboBox).SelectedIndex = 1
                End If
                rdrInsert.Close()
            Catch ioEx As Exception
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try
        End If

        cmdInsert.Dispose()

    End Sub



    Private Sub Proc(ByVal Tipo As Integer, ByVal tag As Integer, ByVal Combo As ComboBox)
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim dato As Integer

        Combo.Items.Clear()
        If Tipo = 0 Then
            Combo.Items.Add("Contado")
            Combo.SelectedIndex = 0
            Combo.Enabled = False
            ContextMenu1.MenuItems(0).Enabled = True
        Else

            Combo.Enabled = True
            ContextMenu1.MenuItems(0).Enabled = False
            Combo.Items.Add("Credito GM")
            Combo.Items.Add("Credito Operador")
            Combo.Items.Add("Tarjeta credito")
            Try
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandTimeout = 30
                cmdInsert.CommandText = "Select Count(*) as Registro  from TarjetaCredito TC INNER JOIN Cliente C ON TC.Cliente=C.Cliente where TC.Status='ACTIVA' and C.Cliente=@Cliente "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes.Items(tag)
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                dato = CType(rdrInsert("Registro"), Integer)
                rdrInsert.Close()
                If dato > 0 Then
                    Combo.SelectedIndex = 2
                Else
                    cmdInsert.CommandText = "Select Count(*) as Registro from Cliente where FormaPago=2 and Cliente=@Cliente "
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes.Items(tag)
                    rdrInsert = cmdInsert.ExecuteReader
                    rdrInsert.Read()
                    dato = CType(rdrInsert("Registro"), Integer)
                    rdrInsert.Close()
                    If dato > 0 Then
                        Combo.SelectedIndex = 0
                    Else
                        Combo.SelectedIndex = 1
                    End If
                End If
            Catch ioEx As Exception
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try
        End If

    End Sub

    Private Sub Proc2(ByVal Tipo As Integer, ByVal tag As Integer, ByVal Combo As ComboBox)
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim dato As Integer

        Combo.Items.Clear()
        If Tipo = 0 Then
            Combo.Items.Add("Contado")
            Combo.SelectedIndex = 0
            Combo.Enabled = False
            ContextMenu2.MenuItems(0).Enabled = True
        Else
            Combo.Enabled = True
            ContextMenu2.MenuItems(0).Enabled = False
            Combo.Items.Add("Credito GM")
            Combo.Items.Add("Credito Operador")
            Combo.Items.Add("Tarjeta credito")
            Try
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandTimeout = 30
                cmdInsert.CommandText = "Select Count(*) as Registro  from TarjetaCredito TC INNER JOIN Cliente C ON TC.Cliente=C.Cliente where TC.Status='ACTIVA' and C.Cliente=@Cliente "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes2.Items(tag)
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                dato = CType(rdrInsert("Registro"), Integer)
                rdrInsert.Close()
                If dato > 0 Then
                    Combo.SelectedIndex = 2
                Else
                    cmdInsert.CommandText = "Select Count(*) as Registro from Cliente where FormaPago=2 and Cliente=@Cliente "
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = lstClientes2.Items(tag)
                    rdrInsert = cmdInsert.ExecuteReader
                    rdrInsert.Read()
                    dato = CType(rdrInsert("Registro"), Integer)
                    rdrInsert.Close()
                    If dato > 0 Then
                        Combo.SelectedIndex = 0
                    Else
                        Combo.SelectedIndex = 1
                    End If
                End If
            Catch ioEx As Exception
                MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
            End Try
        End If

    End Sub



    Private Sub MyValidated(ByVal sender As Object, ByVal e As System.EventArgs)
        If IsNumeric(CType(sender, TextBox).Text) = False Then
            CType(sender, TextBox).Text = "0"
        End If
    End Sub


    Private Sub ValidatedNota(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim Paneles As System.Windows.Forms.Control
        Dim i As Integer
        Dim Registro As Integer
        Dim Panel As System.Windows.Forms.Control = Nothing

        If CType(sender, TextBox).Text <> "" Then
            Try
                cmdInsert.Connection = SqlConnection
                cmdInsert.CommandTimeout = 30
                cmdInsert.CommandType = CommandType.Text
                cmdInsert.CommandText = "Select Count(*) as Registro from NotaBlanca where Codigo=@Codigo "
                'and Codigo not in (Select Codigo from Nota where Codigo=@Codigo)
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Codigo", SqlDbType.Char).Value = CType(sender, TextBox).Text
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                Registro = CType(rdrInsert("Registro"), Integer)
                rdrInsert.Close()
            Catch exNum As Exception
            End Try

            If Registro > 0 Then
                For i = 0 To lstNotas.Items.Count - 1
                    If CType(lstNotas.Items(i), String) = CType(sender, TextBox).Text And i <> CType(CType(sender, TextBox).Tag, Integer) Then
                        MsgBox("Esta nota blanca ya la capturo. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                        CType(sender, TextBox).Select()
                        Exit Sub
                    End If
                Next

                For Each Paneles In pnNotas.Controls
                    If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                        Paneles.Enabled = True
                        If Paneles.Name.Substring(0, 5) = "txtNL" Then

                            If Paneles.Text = "0" Then
                                CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                            Else
                                CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = True
                            End If

                        End If

                        If Paneles.Name.Substring(0, 5) = "txtNP" Then
                            CType(Paneles, System.Windows.Forms.TextBox).ReadOnly = False
                        End If

                    End If
                Next
            Else
                MsgBox("Esta nota blanca no existe o ya fue utilizada, Verifique.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                CType(sender, TextBox).Select()
                Exit Sub
            End If

        End If

    End Sub




    Private Sub ValidatedCliente(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing
        Dim Paneles As System.Windows.Forms.Control
        Dim i As Integer
        Dim Registro As Integer
        Dim Nombre As String
        Dim Pedido As String
        Dim Panel As System.Windows.Forms.Control = Nothing
        Dim ClienteRepetido As Boolean

        If CType(sender, TextBox).Text <> "" Then
            If CType(CType(sender, TextBox).Text, Double) <> _ClienteGlobal Then
                'validar la celula correcta
                Try
                    cmdInsert.Connection = SqlConnection
                    cmdInsert.CommandTimeout = 30
                    cmdInsert.CommandType = CommandType.Text
                    cmdInsert.CommandText = "Select Ruta, celula from Cliente where Cliente=@Cliente"
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(CType(sender, TextBox).Text, Integer)
                    'TODO: Agregado el día 17/10/2004 para validar el tipo de crédito del cliente
                    clienteParaValidacionCredito = CType(CType(sender, TextBox).Text, Integer)
                    rdrInsert = cmdInsert.ExecuteReader
                    rdrInsert.Read()
                    Registro = CType(rdrInsert("Celula"), Integer)
                    If Registro <> _Celula Then
                        Dim respuestaMsgC As MsgBoxResult
                        respuestaMsgC = MsgBox("La celula del operador no corresponde a la celula del cliente. ¿Desea liquidarlo?.", MsgBoxStyle.YesNo, "Mensaje del sistema")
                        If respuestaMsgC = MsgBoxResult.No Then
                            CType(sender, TextBox).Select()
                            CType(sender, TextBox).Text = ""
                            rdrInsert.Close()
                            Exit Sub
                        End If
                    End If

                    'validar la  ruta correcta
                    Registro = CType(rdrInsert("Ruta"), Integer)
                    rdrInsert.Close()
                    If Registro <> _Ruta Then
                        Dim respuestaMsg As MsgBoxResult
                        respuestaMsg = MsgBox("La ruta del cliente no corresponde a la ruta del operador. ¿Desea liquidarlo?.", MsgBoxStyle.YesNo, "Mensaje del sistema")
                        If respuestaMsg = MsgBoxResult.No Then
                            CType(sender, TextBox).Select()
                            CType(sender, TextBox).Text = ""
                            rdrInsert.Close()
                            Exit Sub
                        End If
                    End If
                    rdrInsert.Close()
                Catch ex As Exception
                    CType(sender, TextBox).Select()
                    rdrInsert.Close()
                End Try
            End If

            ClienteRepetido = False
            'TODO:  Modificación de Miguel Huerta
            'Modifico MHV mejora de permitir capturar un mismo cliente en la liquidacion. 28/10/2004 14:25 pm se quito ya no aplica

            For i = 0 To lstClientes.Items.Count - 1
                If CType(lstClientes.Items(i), String) = CType(sender, TextBox).Text Then
                    MsgBox("Este cliente esta capturado en la lista de arriba.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    CType(sender, TextBox).Select()
                    Exit Sub
                End If
            Next

            For i = 0 To lstClientes2.Items.Count - 1
                Try
                    If CType(CType(sender, TextBox).Text, Integer) <> _ClienteGlobal Then
                        If CType(lstClientes2.Items(i), String) = CType(sender, TextBox).Text And i <> CType(CType(sender, TextBox).Tag, Integer) Then
                            MsgBox("Este cliente ya lo tiene capturado en la lista de notas blancas.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                            CType(sender, TextBox).Select()
                            Exit Sub
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            Try
                cmdInsert.CommandText = "Select Count(*) Registro from Cliente where Cliente=@Cliente"
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(CType(sender, TextBox).Text, Integer)
                rdrInsert = cmdInsert.ExecuteReader
                rdrInsert.Read()
                Registro = CType(rdrInsert("Registro"), Integer)
                rdrInsert.Close()

                If Registro = 0 Then
                    MsgBox("Este no es un contrato valido. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    CType(sender, TextBox).Select()
                    rdrInsert.Close()
                    Exit Sub
                Else

                    If CType(CType(sender, TextBox).Text, Integer) <> _ClienteGlobal Then
                        Dim Acepto As Byte
                        Dim frmClienteExiste As New ClienteExiste()
                        frmClienteExiste.Entrada(CType(CType(sender, TextBox).Text, Integer))
                        Acepto = CType(frmClienteExiste._Acepta, Byte)
                        frmClienteExiste.Dispose()
                        If Acepto <> -1 Then
                            cmdInsert.Connection = SqlConnection
                            cmdInsert.CommandType = CommandType.Text
                            cmdInsert.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                                    " Select Nombre from Cliente where Cliente=@Cliente " & _
                                                    " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                            cmdInsert.Parameters.Clear()
                            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(CType(sender, TextBox).Text, Integer)
                            rdrInsert = cmdInsert.ExecuteReader
                            rdrInsert.Read()
                            Nombre = CType(rdrInsert("Nombre"), String)
                            rdrInsert.Close()

                            cmdInsert.Connection = SqlConnection
                            cmdInsert.CommandType = CommandType.Text
                            cmdInsert.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                                    " select Pedido, AñoPed, Celula from Pedido where Status='ACTIVO' and TipoPedido in (1,2,3) and TipoCargo=1 and Cliente=@Cliente and Producto<>4 " & _
                                                    " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                            cmdInsert.Parameters.Clear()
                            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(CType(sender, TextBox).Text, Integer)
                            rdrInsert = cmdInsert.ExecuteReader
                            If rdrInsert.Read = True Then
                                Pedido = CType(rdrInsert("Pedido"), String)
                            Else
                                Pedido = "0"
                            End If
                            rdrInsert.Close()

                            For Each Paneles In pnNotas.Controls
                                If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                                    If Paneles.Name = "lbNNombre" + CType(CType(sender, TextBox).Tag, String) Then
                                        If Nombre.Length > 18 Then
                                            Paneles.Text = Nombre.Substring(0, 18)
                                        Else
                                            Paneles.Text = Nombre
                                        End If
                                    End If
                                    If Paneles.Name = "lbNPedido" + CType(CType(sender, TextBox).Tag, String) Then
                                        If Pedido = "0" Then
                                            Paneles.Text = ""
                                        Else
                                            Paneles.Text = CType(Pedido, String)
                                        End If
                                    End If
                                End If
                            Next
                        Else
                            CType(sender, TextBox).Text = CType(_ClienteGlobal, String)
                            For Each Paneles In pnNotas.Controls
                                If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                                    If Paneles.Name = "lbNNombre" + CType(CType(sender, TextBox).Tag, String) Then
                                        Paneles.Text = "VENTA PUBLICO E"
                                    End If
                                End If
                            Next
                            Exit Sub
                        End If
                    End If
                End If

                For i = 0 To lstClientes.Items.Count - 1
                    If CType(lstClientes.Items(i), String) = CType(sender, TextBox).Text Then
                        MsgBox("Este cliente esta capturado en la lista de arriba.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
                    End If
                Next

            Catch ex As Exception
            End Try
        Else
            Try
                CType(sender, TextBox).Text = CType(_ClienteGlobal, String)
                For Each Paneles In pnNotas.Controls
                    If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                        If Paneles.Name = "lbNNombre" + CType(CType(sender, TextBox).Tag, String) Then
                            Paneles.Text = "VENTA PUBLICO E"
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try

        End If

    End Sub


    Private Sub TextChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control
        Dim FormaAnterior As Integer = Nothing
        Dim i As Integer
        Dim Total1 As Decimal
        Dim Total As Decimal

        If DsLiquidacion.Detalle.DefaultView.Count > 0 Then
            If _Cambio <> CType(sender, TextBox).Text Then
                MsgBox("No puedes modificar los montos, por que tienes pagos relacionados. Elimina primero estos pagos", MsgBoxStyle.Information, "Mensaje del sistema")
                CType(sender, TextBox).Text = _Cambio
                Exit Sub
            End If
        End If


        lbTotalLitros1.Text = "0"
        lbLitrosContado.Text = "0"
        lbLitrosCredito.Text = "0"
        lbTotalLitros.Text = "0"
        If lbLitrosNotas.Text = "" Then
            lbLitrosNotas.Text = "0.00"
        End If

        lbTotalImporte1.Text = "0"
        If lbTotalNotas.Text = "" Then
            lbTotalNotas.Text = "0.00"
        End If
        lbTotalContado.Text = "0"
        lbTotalCredito.Text = "0"
        'lbTotalImporte.Text = "0"

        lstForma.Items.Clear()
        lstPrecio.Items.Clear()
        lstLitros.Items.Clear()
        lstPedido.Items.Clear()
        lstClientes.Items.Clear()
        lstTipoPago.Items.Clear()

        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then

                If Paneles.Name.Substring(0, 4) = "cmbF" Then
                    lstForma.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 4) = "cmbT" Then
                    lstTipoPago.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 4) = "txtP" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstPrecio.Items.Add(Paneles.Text)
                    Else
                        lstPrecio.Items.Add("0")
                    End If

                End If

                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstLitros.Items.Add(Paneles.Text)
                    Else
                        lstLitros.Items.Add("0")
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbP" Then
                    lstPedido.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 3) = "lbC" Then
                    lstClientes.Items.Add(Paneles.Text)
                End If

            End If
        Next

        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 4) = "cmbT" Then
                    If CType(CType(sender, TextBox).Tag, Integer) = CType(Paneles.Tag, Integer) Then
                        If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                            Proc(0, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        Else
                            Proc(1, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        End If
                    End If
                End If
            End If
        Next


        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If IsNumeric(Paneles.Text) Then
                        Try
                            lbTotalLitros1.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        Catch exNum As Exception
                            lbTotalLitros1.Text = Format(CType(0, Decimal), "#.00")
                        End Try
                        If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                            Try
                                lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            Catch exNum As Exception
                                lbLitrosContado.Text = Format(CType(0, Decimal), "#.00")
                            End Try

                        Else
                            Try
                                lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            Catch exNum As Exception
                                lbLitrosCredito.Text = Format(CType(0, Decimal), "#.00")
                            End Try

                        End If
                        Try
                            lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                        Catch exNum As Exception
                            lbTotalLitros.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbI" Then
                    If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                        Try
                            Paneles.Text = Format(CType(lstLitros.Items(CType(Paneles.Tag, Integer)), Decimal) * CType(lstPrecio.Items(CType(Paneles.Tag, Integer)), Decimal), "$ #.00")
                        Catch exNum As Exception
                            Paneles.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If
            End If
        Next

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If IsNumeric(Paneles.Text) Then
                        'lbLitrosNotas.Text = Format(CType(lbLitrosNotas.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        If lstForma2.Items.Count > 0 Then
                            If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                                Try
                                    lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                                Catch exNum As Exception
                                    lbLitrosContado.Text = Format(CType(0, Decimal), "#.00")
                                End Try

                            Else
                                Try
                                    lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                                Catch exNum As Exception
                                    lbLitrosCredito.Text = Format(CType(0, Decimal), "#.00")
                                End Try

                            End If
                        End If
                        Try
                            lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                        Catch exNum As Exception
                            lbTotalLitros.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If
            End If
        Next


        Total1 = 0
        TotalContado = 0
        TotalCredito = 0
        Total = 0
        For i = 0 To lstLitros.Items.Count - 1

            If IsNumeric(lstLitros.Items(i)) = True And IsNumeric(lstPrecio.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                Try
                    lbTotalImporte1.Text = Format(Total1, "$ #.00")
                Catch exNum As Exception
                    lbTotalImporte1.Text = Format(CType(0, Decimal), "#.00")
                End Try
                If CType(lstForma.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                    Try
                        lbTotalContado.Text = Format(TotalContado, "$ #.00")
                    Catch exNum As Exception
                        lbTotalContado.Text = Format(CType(0, Decimal), "#.00")
                    End Try

                Else
                    If CType(lstForma.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                        Try
                            lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                        Catch exNum As Exception
                            lbTotalCredito.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If



            End If
        Next

        Total1 = 0
        Total = 0

        For i = 0 To lstLitros2.Items.Count - 1

            If IsNumeric(lstLitros2.Items(i)) = True And IsNumeric(lstPrecio2.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                Try
                    lbTotalNotas.Text = Format(Total1, "$ #.00")
                Catch exNum As Exception
                    lbTotalNotas.Text = Format(CType(0, Decimal), "#.00")
                End Try


                If CType(lstForma2.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                    Try
                        lbTotalContado.Text = Format(TotalContado, "$ #.00")
                    Catch exNum As Exception
                        lbTotalContado.Text = Format(CType(0, Decimal), "#.00")
                    End Try
                Else
                    If CType(lstForma2.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                        Try
                            lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                        Catch exNum As Exception
                            lbTotalCredito.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If

                'VERIFICACION DEL TOTAL DEL IMPORTE
                Dim totalImporte As Decimal = 0
                Try
                    totalImporte = CType(lbTotalNotas.Text, Decimal) + Total1
                Catch numEx As Exception
                    totalImporte = 0
                End Try
                If (totalImporte > 0) Then
                    lbTotalImporte.Text = Format(totalImporte, "$ #.00")
                Else
                    lbTotalImporte.Text = Format(0, "$ #.00")
                End If
            End If

        Next
        CalculoNotas()

        'VERIFICACION DEL TOTAL DEL IMPORTE
        totalImporte = TotalContado + TotalCredito
        lbTotalImporte.Text = Format(totalImporte, "$ #.00")

    End Sub

    Private Sub TextChanged2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control
        Dim FormaAnterior As Integer = Nothing
        Dim i As Integer
        Dim Total1 As Decimal
        Dim Total As Decimal

        If DsLiquidacion.Detalle.DefaultView.Count > 0 Then
            If _Cambio2 <> CType(sender, TextBox).Text Then
                MsgBox("No puedes modificar los montos, por que tienes pagos relacionados. Elimina primero estos pagos", MsgBoxStyle.Information, "Mensaje del sistema")
                CType(sender, TextBox).Text = _Cambio2
                Exit Sub
            End If
        End If

        lbLitrosNotas.Text = "0"
        lbLitrosContado.Text = "0"
        lbLitrosCredito.Text = "0"
        lbTotalLitros.Text = "0"
        If lbTotalLitros1.Text = "" Then
            lbTotalLitros1.Text = "0.00"
        End If


        lbTotalNotas.Text = "0"
        If lbTotalImporte1.Text = "" Then
            lbTotalImporte1.Text = "0.00"
        End If
        lbTotalContado.Text = "0"
        lbTotalCredito.Text = "0"
        'lbTotalImporte.Text = "0"

        lstForma2.Items.Clear()
        lstPrecio2.Items.Clear()
        lstLitros2.Items.Clear()
        lstPedido2.Items.Clear()
        lstClientes2.Items.Clear()
        lstTipoPago2.Items.Clear()
        lstNotas.Items.Clear()

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then

                If Paneles.Name.Substring(0, 5) = "cmbNF" Then
                    lstForma2.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 5) = "cmbNT" Then
                    lstTipoPago2.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 5) = "txtNP" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstPrecio2.Items.Add(Paneles.Text)
                    Else
                        lstPrecio2.Items.Add("0")
                    End If

                End If

                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstLitros2.Items.Add(Paneles.Text)
                    Else
                        lstLitros2.Items.Add("0")
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNP" Then
                    If Paneles.Text <> "" Then
                        lstPedido2.Items.Add(Paneles.Text)
                    Else
                        lstPedido2.Items.Add("0")
                    End If

                End If

                If Paneles.Name.Substring(0, 5) = "txtNC" Then
                    If Paneles.Text <> "" Then
                        lstClientes2.Items.Add(Paneles.Text)
                    Else
                        lstClientes2.Items.Add("0")
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNN" Then
                    lstNotas.Items.Add(Paneles.Text)
                End If


            End If
        Next

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 5) = "cmbNT" Then
                    If CType(CType(sender, TextBox).Tag, Integer) = CType(Paneles.Tag, Integer) Then
                        If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                            Proc2(0, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        Else
                            Proc2(1, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        End If
                    End If
                End If
            End If
        Next

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If IsNumeric(Paneles.Text) Then
                        lbLitrosNotas.Text = Format(CType(lbLitrosNotas.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        If lstForma2.Items.Count > 0 Then
                            If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                                lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            Else
                                lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            End If
                        End If

                        lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNI" Then
                    If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                        Paneles.Text = Format(CType(lstLitros2.Items(CType(Paneles.Tag, Integer)), Decimal) * CType(lstPrecio2.Items(CType(Paneles.Tag, Integer)), Decimal), "$ #.00")
                    End If
                End If
            End If
        Next

        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If IsNumeric(Paneles.Text) Then
                        'lbTotalLitros1.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        If lstForma.Items.Count > 0 Then
                            If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                                lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            Else
                                lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            End If
                        End If
                        lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                    End If
                End If
            End If
        Next


        Total1 = 0
        TotalContado = 0
        TotalCredito = 0
        Total = 0
        For i = 0 To lstLitros2.Items.Count - 1

            If IsNumeric(lstLitros2.Items(i)) = True And IsNumeric(lstPrecio2.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                Try
                    lbTotalNotas.Text = Format(Total1, "$ #.00")
                Catch ex As Exception
                    lbTotalNotas.Text = Format(CType(0, Decimal), "#.00")
                End Try


                If CType(lstForma2.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                    Try
                        lbTotalContado.Text = Format(TotalContado, "$ #.00")
                    Catch ex As Exception
                        lbTotalContado.Text = Format(CType(0, Decimal), "#.00")
                    End Try


                Else
                    If CType(lstForma2.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                        Try
                            lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                        Catch ex As Exception
                            lbTotalCredito.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If

            End If
        Next

        Total1 = 0
        Total = 0

        For i = 0 To lstLitros.Items.Count - 1

            If IsNumeric(lstLitros.Items(i)) = True And IsNumeric(lstPrecio.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                Try
                    lbTotalImporte1.Text = Format(Total1, "$ #.00")
                Catch ex As Exception
                    lbTotalImporte1.Text = Format(CType(0, Decimal), "#.00")
                End Try


                If CType(lstForma.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                    Try
                        lbTotalContado.Text = Format(TotalContado, "$ #.00")
                    Catch ex As Exception
                        lbTotalContado.Text = Format(CType(0, Decimal), "#.00")
                    End Try

                Else
                    If CType(lstForma.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                        Try
                            lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                        Catch ex As Exception
                            lbTotalCredito.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If



            End If
        Next

        CalculoNotas()
        'VERIFICACION DEL TOTAL DEL IMPORTE
        TotalImporte = TotalContado + TotalCredito



        lbTotalImporte.Text = Format(TotalImporte, "$ #.00")

    End Sub

    Private Sub TextChanged3(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control
        Dim FormaAnterior As Integer = Nothing
        Dim i As Integer
        Dim Total1 As Decimal
        Dim Total As Decimal

        If DsLiquidacion.Detalle.DefaultView.Count > 0 Then
            If _Cambio2 <> CType(sender, ComboBox).Text Then
                MsgBox("No puedes modificar los montos, por que tienes pagos relacionados. Elimina primero estos pagos", MsgBoxStyle.Information, "Mensaje del sistema")
                CType(sender, ComboBox).Text = _Cambio2
                Exit Sub
            End If
        End If

        'TODO: Se agregó el día 08/10/2004 VALIDACION DE CREDITO (NOTAS BLANCAS)
        maximoImporteCreditoExcedido(_aplicaValidacionCreditocliente, clienteParaValidacionCredito, _
            CType(lstLitros2.Items(_Linea), Decimal) * CType(lstPrecio2.Items(_Linea), Decimal), SqlConnection)

        lbLitrosNotas.Text = "0"
        lbLitrosContado.Text = "0"
        lbLitrosCredito.Text = "0"
        lbTotalLitros.Text = "0"
        If lbTotalLitros1.Text = "" Then
            lbTotalLitros1.Text = "0.00"
        End If


        lbTotalNotas.Text = "0"
        If lbTotalImporte1.Text = "" Then
            lbTotalImporte1.Text = "0.00"
        End If
        lbTotalContado.Text = "0"
        lbTotalCredito.Text = "0"

        lstForma2.Items.Clear()
        lstPrecio2.Items.Clear()
        lstLitros2.Items.Clear()
        lstPedido2.Items.Clear()
        lstClientes2.Items.Clear()
        lstTipoPago2.Items.Clear()
        lstNotas.Items.Clear()

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then

                If Paneles.Name.Substring(0, 5) = "cmbNF" Then
                    lstForma2.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 5) = "cmbNT" Then
                    lstTipoPago2.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 5) = "txtNP" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstPrecio2.Items.Add(Paneles.Text)
                    Else
                        lstPrecio2.Items.Add("0")
                    End If

                End If

                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstLitros2.Items.Add(Paneles.Text)
                    Else
                        lstLitros2.Items.Add("0")
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNP" Then
                    If Paneles.Text <> "" Then
                        lstPedido2.Items.Add(Paneles.Text)
                    Else
                        lstPedido2.Items.Add("0")
                    End If

                End If

                If Paneles.Name.Substring(0, 5) = "txtNC" Then
                    If Paneles.Text <> "" Then
                        lstClientes2.Items.Add(Paneles.Text)
                    Else
                        lstClientes2.Items.Add("0")
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNN" Then
                    lstNotas.Items.Add(Paneles.Text)
                End If


            End If
        Next

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 5) = "cmbNT" Then
                    If CType(CType(sender, ComboBox).Tag, Integer) = CType(Paneles.Tag, Integer) Then
                        If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                            Proc2(0, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        Else
                            Proc2(1, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        End If
                    End If
                End If
            End If
        Next

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If IsNumeric(Paneles.Text) Then
                        lbLitrosNotas.Text = Format(CType(lbLitrosNotas.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        If lstForma2.Items.Count > 0 Then
                            If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                                lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            Else
                                lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            End If
                        End If

                        lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNI" Then
                    If CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) Then
                        Paneles.Text = Format(CType(lstLitros2.Items(CType(Paneles.Tag, Integer)), Decimal) * CType(lstPrecio2.Items(CType(Paneles.Tag, Integer)), Decimal), "$ #.00")
                    End If
                End If
            End If
        Next

        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If IsNumeric(Paneles.Text) Then
                        If lstForma.Items.Count > 0 Then
                            If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                                lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            Else
                                lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            End If
                        End If
                        lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                    End If
                End If
            End If
        Next


        Total1 = 0
        TotalContado = 0
        TotalCredito = 0
        Total = 0
        For i = 0 To lstLitros2.Items.Count - 1

            If IsNumeric(lstLitros2.Items(i)) = True And IsNumeric(lstPrecio2.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                Try
                    lbTotalNotas.Text = Format(Total1, "$ #.00")
                Catch ex As Exception
                    lbTotalNotas.Text = Format(CType(0, Decimal), "#.00")
                End Try


                If CType(lstForma2.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                    Try
                        lbTotalContado.Text = Format(TotalContado, "$ #.00")
                    Catch ex As Exception
                        lbTotalContado.Text = Format(CType(0, Decimal), "#.00")
                    End Try


                Else
                    If CType(lstForma2.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                        Try
                            lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                        Catch ex As Exception
                            lbTotalCredito.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If



            End If
        Next

        Total1 = 0
        Total = 0

        For i = 0 To lstLitros.Items.Count - 1

            If IsNumeric(lstLitros.Items(i)) = True And IsNumeric(lstPrecio.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                Try
                    lbTotalImporte1.Text = Format(Total1, "$ #.00")
                Catch ex As Exception
                    lbTotalImporte1.Text = Format(CType(0, Decimal), "#.00")
                End Try


                If CType(lstForma.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                    Try
                        lbTotalContado.Text = Format(TotalContado, "$ #.00")
                    Catch ex As Exception
                        lbTotalContado.Text = Format(CType(0, Decimal), "#.00")
                    End Try

                Else
                    If CType(lstForma.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                        Try
                            lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                        Catch ex As Exception
                            lbTotalCredito.Text = Format(CType(0, Decimal), "#.00")
                        End Try

                    End If
                End If


            End If
        Next

        CalculoNotas()

        TotalImporte = TotalContado + TotalCredito
        lbTotalImporte.Text = Format(TotalImporte, "$ #.00")

    End Sub

    Private Sub TextChanged4(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control
        Dim FormaAnterior As Integer = Nothing
        Dim i As Integer
        Dim Total1 As Decimal
        Dim TotalContado As Decimal
        Dim TotalCredito As Decimal
        Dim Total As Decimal

        If DsLiquidacion.Detalle.DefaultView.Count > 0 Then
            If _Cambio <> CType(sender, ComboBox).Text Then
                MsgBox("No puedes modificar los montos, por que tienes pagos relacionados. Elimina primero estos pagos", MsgBoxStyle.Information, "Mensaje del sistema")
                CType(sender, ComboBox).Text = _Cambio
                Exit Sub
            End If
        End If


        'TODO: Se agregó el día 08/10/2004 VALIDACION DE CREDITO (PEDIDOS)
        maximoImporteCreditoExcedido(_aplicaValidacionCreditocliente, CType(lstClientes.Items(_Linea), Integer), _
            CType(lstLitros.Items(_Linea), Decimal) * CType(lstPrecio.Items(_Linea), Decimal), SqlConnection)

        lbTotalLitros1.Text = "0"
        lbLitrosContado.Text = "0"
        lbLitrosCredito.Text = "0"
        lbTotalLitros.Text = "0"
        If lbLitrosNotas.Text = "" Then
            lbLitrosNotas.Text = "0.00"
        End If

        lbTotalImporte1.Text = "0"
        If lbTotalNotas.Text = "" Then
            lbTotalNotas.Text = "0.00"
        End If
        lbTotalContado.Text = "0"
        lbTotalCredito.Text = "0"
        lbTotalImporte.Text = "0"

        lstForma.Items.Clear()
        lstPrecio.Items.Clear()
        lstLitros.Items.Clear()
        lstPedido.Items.Clear()
        lstClientes.Items.Clear()
        lstTipoPago.Items.Clear()

        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then

                If Paneles.Name.Substring(0, 4) = "cmbF" Then
                    lstForma.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 4) = "cmbT" Then
                    lstTipoPago.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 4) = "txtP" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstPrecio.Items.Add(Paneles.Text)
                    Else
                        lstPrecio.Items.Add("0")
                    End If

                End If

                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If IsNumeric(Paneles.Text) = True Then
                        lstLitros.Items.Add(Paneles.Text)
                    Else
                        lstLitros.Items.Add("0")
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbP" Then
                    lstPedido.Items.Add(Paneles.Text)
                End If

                If Paneles.Name.Substring(0, 3) = "lbC" Then
                    lstClientes.Items.Add(Paneles.Text)
                End If

            End If
        Next

        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 4) = "cmbT" Then
                    If CType(CType(sender, ComboBox).Tag, Integer) = CType(Paneles.Tag, Integer) Then
                        If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                            Proc(0, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        Else
                            Proc(1, CType(Paneles.Tag, Integer), CType(Paneles, ComboBox))
                        End If
                    End If
                End If
            End If
        Next


        For Each Paneles In pnPedidos.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If IsNumeric(Paneles.Text) Then
                        lbTotalLitros1.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                            lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        Else
                            lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                        End If
                        lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbI" Then
                    If CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) Then
                        Paneles.Text = Format(CType(lstLitros.Items(CType(Paneles.Tag, Integer)), Decimal) * CType(lstPrecio.Items(CType(Paneles.Tag, Integer)), Decimal), "$ #.00")
                    End If
                End If
            End If
        Next

        For Each Paneles In pnNotas.Controls
            If Paneles.Name <> "" Then
                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If IsNumeric(Paneles.Text) Then
                        If lstForma2.Items.Count > 0 Then
                            If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                                lbLitrosContado.Text = Format(CType(lbLitrosContado.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            Else
                                lbLitrosCredito.Text = Format(CType(lbLitrosCredito.Text, Decimal) + CType(Paneles.Text, Decimal), "#.00")
                            End If
                        End If
                        lbTotalLitros.Text = Format(CType(lbTotalLitros1.Text, Decimal) + CType(lbLitrosNotas.Text, Decimal), "#.00")
                    End If
                End If
            End If
        Next


        Total1 = 0
        TotalContado = 0
        TotalCredito = 0
        Total = 0
        For i = 0 To lstLitros.Items.Count - 1

            If IsNumeric(lstLitros.Items(i)) = True And IsNumeric(lstPrecio.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                lbTotalImporte1.Text = Format(Total1, "$ #.00")

                If CType(lstForma.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                    lbTotalContado.Text = Format(TotalContado, "$ #.00")
                Else
                    If CType(lstForma.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal))
                        lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                    End If
                End If

                lbTotalImporte.Text = Format((CType(lbTotalNotas.Text, Decimal) + Total1), "$ #.00")

            End If
        Next

        Total1 = 0
        Total = 0

        For i = 0 To lstLitros2.Items.Count - 1

            If IsNumeric(lstLitros2.Items(i)) = True And IsNumeric(lstPrecio2.Items(i)) = True Then
                Total1 = Total1 + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                lbTotalNotas.Text = Format(Total1, "$ #.00")

                If CType(lstForma2.Items(i), String) = "CONTADO" Then
                    TotalContado = TotalContado + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                    lbTotalContado.Text = Format(TotalContado, "$ #.00")
                Else
                    If CType(lstForma2.Items(i), String) = "CREDITO" Then
                        TotalCredito = TotalCredito + (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal))
                        lbTotalCredito.Text = Format(TotalCredito, "$ #.00")
                    End If
                End If

                lbTotalImporte.Text = Format((CType(lbTotalImporte1.Text, Decimal) + Total1), "$ #.00")

            End If

        Next

        CalculoNotas()

        'VERIFICACION DEL TOTAL DEL IMPORTE
        TotalImporte = TotalContado + TotalCredito
        lbTotalImporte.Text = Format(TotalImporte, "$ #.00")

    End Sub


    Private Sub MyKeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)

        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Or e.KeyChar = "." Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    Private Sub MyKeyPress2(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then 'Or e.KeyChar = ControlChars.Cr Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub EntradaGrid1(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control

        For Each Paneles In pnPedidos.Controls
            If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                Paneles.BackColor = Color.Navy
                Paneles.ForeColor = Color.White
                _Linea = CType(Paneles.Tag, Integer)
            Else
                'MsgBox(Paneles.Name.Substring(0, 4))
                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "txtP" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.PaleGoldenrod
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbI" Then

                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.DarkGreen
                        Paneles.ForeColor = Color.White
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If

                End If

                If Paneles.Name.Substring(0, 4) = "cmbF" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If

                End If

                If Paneles.Name.Substring(0, 4) = "cmbT" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If

                End If

                If Paneles.Name.Substring(0, 3) = "lbP" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbC" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbN" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If
            End If
        Next

        DsLiquidacion.Detalle.DefaultView.RowFilter = ""
        DsLiquidacion.Detalle.DefaultView.RowFilter = " Cliente =" + CType(lstClientes.Items(CType(CType(sender, TextBox).Tag, Integer)), String)
        _Cambio = CType(sender, TextBox).Text

    End Sub


    Private Sub EntradaGrid2(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control



        For Each Paneles In pnNotas.Controls
            If CType(Paneles.Tag, Integer) = CType(CType(sender, TextBox).Tag, Integer) Then
                Paneles.BackColor = Color.Navy
                Paneles.ForeColor = Color.White
                _Linea2 = CType(Paneles.Tag, Integer)
            Else

                If Paneles.Name.Substring(0, 5) = "txtNN" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNC" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNP" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNN" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNP" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.PaleGoldenrod
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNI" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.DarkGreen
                        Paneles.ForeColor = Color.White
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "cmbNF" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "cmbNT" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

            End If
        Next

        If lstClientes2.Items.Count > 0 Then
            If CType(lstClientes2.Items(CType(CType(sender, TextBox).Tag, Integer)), String) <> "" Then
                DsLiquidacion.Detalle.DefaultView.RowFilter = ""
                DsLiquidacion.Detalle.DefaultView.RowFilter = " Cliente =" + CType(lstClientes2.Items(CType(CType(sender, TextBox).Tag, Integer)), String)
            End If
        End If

        _Cambio2 = CType(sender, TextBox).Text

    End Sub

    Private Sub EntradaGrid3(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control



        For Each Paneles In pnNotas.Controls
            If CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) Then
                Paneles.BackColor = Color.Navy
                Paneles.ForeColor = Color.White
                _Linea2 = CType(Paneles.Tag, Integer)
            Else

                If Paneles.Name.Substring(0, 5) = "txtNN" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNC" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNP" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNN" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNL" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "txtNP" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.PaleGoldenrod
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "lbNI" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.DarkGreen
                        Paneles.ForeColor = Color.White
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "cmbNF" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 5) = "cmbNT" Then
                    If CType(lstForma2.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

            End If
        Next

        If lstClientes2.Items.Count > 0 Then
            If CType(lstClientes2.Items(CType(CType(sender, ComboBox).Tag, Integer)), String) <> "" Then
                DsLiquidacion.Detalle.DefaultView.RowFilter = ""
                DsLiquidacion.Detalle.DefaultView.RowFilter = " Cliente =" + CType(lstClientes2.Items(CType(CType(sender, ComboBox).Tag, Integer)), String)
            End If
        End If

        _Cambio2 = CType(sender, ComboBox).Text

    End Sub

    Private Sub EntradaGrid4(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Paneles As System.Windows.Forms.Control

        For Each Paneles In pnPedidos.Controls
            If CType(Paneles.Tag, Integer) = CType(CType(sender, ComboBox).Tag, Integer) Then
                Paneles.BackColor = Color.Navy
                Paneles.ForeColor = Color.White
                _Linea = CType(Paneles.Tag, Integer)
            Else
                'MsgBox(Paneles.Name.Substring(0, 4))
                If Paneles.Name.Substring(0, 4) = "txtL" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 4) = "txtP" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.PaleGoldenrod
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbI" Then

                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.DarkGreen
                        Paneles.ForeColor = Color.White
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If

                End If

                If Paneles.Name.Substring(0, 4) = "cmbF" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If

                End If

                If Paneles.Name.Substring(0, 4) = "cmbT" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = Color.White
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If

                End If

                If Paneles.Name.Substring(0, 3) = "lbP" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbC" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If

                If Paneles.Name.Substring(0, 3) = "lbN" Then
                    If CType(lstForma.Items(CType(Paneles.Tag, Integer)), String) = "CONTADO" Then
                        Paneles.BackColor = System.Drawing.SystemColors.Control
                        Paneles.ForeColor = Color.Black
                    Else
                        Paneles.BackColor = Color.DarkRed
                        Paneles.ForeColor = Color.White
                    End If
                End If
            End If
        Next

        DsLiquidacion.Detalle.DefaultView.RowFilter = ""
        DsLiquidacion.Detalle.DefaultView.RowFilter = " Cliente =" + CType(lstClientes.Items(CType(CType(sender, ComboBox).Tag, Integer)), String)
        _Cambio = CType(sender, ComboBox).Text

    End Sub

    Private Sub CargarGrid2()
        Dim i As Integer
        Dim Alto As Integer
        Dim DatoInicial As Decimal
        Alto = 23

        cmbNForma0.SelectedIndex = 0
        cmbNForma0.Text = "CONTADO"
        AddHandler txtNNota0.KeyPress, AddressOf MyKeyPress2
        AddHandler txtNNota0.Enter, AddressOf EntradaGrid1
        AddHandler txtNCliente0.Enter, AddressOf EntradaGrid1
        AddHandler txtNLitros0.Enter, AddressOf EntradaGrid1
        AddHandler txtNPrecio0.Enter, AddressOf EntradaGrid1
        AddHandler cmbNForma0.Enter, AddressOf EntradaGrid3
        AddHandler cmbNTipo0.Enter, AddressOf EntradaGrid3
        AddHandler txtNCliente0.KeyPress, AddressOf MyKeyPress2
        AddHandler txtNLitros0.KeyPress, AddressOf MyKeyPress
        AddHandler txtNPrecio0.KeyPress, AddressOf MyKeyPress

        AddHandler txtNNota0.Validated, AddressOf ValidatedNota
        AddHandler txtNNota0.TextChanged, AddressOf TextChanged2
        AddHandler txtNNota0.KeyDown, AddressOf KeyDownNotas

        AddHandler txtNCliente0.TextChanged, AddressOf TextChanged2
        AddHandler txtNCliente0.Validated, AddressOf ValidatedCliente
        AddHandler txtNCliente0.KeyDown, AddressOf KeyDownNotas

        AddHandler txtNLitros0.TextChanged, AddressOf TextChanged2
        AddHandler txtNLitros0.Validated, AddressOf MyValidated
        AddHandler txtNLitros0.KeyDown, AddressOf KeyDownNotas

        AddHandler txtNPrecio0.TextChanged, AddressOf TextChanged2
        AddHandler txtNPrecio0.Validated, AddressOf MyValidated

        AddHandler cmbNForma0.SelectedIndexChanged, AddressOf TextChanged3
        'AddHandler cmbNForma0.Validated, AddressOf ValidatedForma
        AddHandler cmbNForma0.KeyDown, AddressOf KeyDownNotas2


        cmbNTipo0.Items.Add("Contado")
        cmbNTipo0.SelectedIndex = 0
        txtNNota0.MaxLength = 19

        AddHandler cmbNTipo0.SelectedIndexChanged, AddressOf SelectedIndexChanged2

        If _Ruta <> 150 Then
            If _Fecha.Date.Month <> Now.Date.Month Then
                txtNPrecio0.Text = CType(GLOBAL_PrecioAnterior, String)
            Else
                txtNPrecio0.Text = CType(GLOBAL_Precio, String)
            End If
        Else
            If _Fecha.Date.Month <> Now.Date.Month Then
                txtNPrecio0.Text = CType(GLOBAL_PrecioAnteriorToluca, String)
            Else
                txtNPrecio0.Text = CType(GLOBAL_PrecioToluca, String)
            End If
        End If


        txtNNota0.Tag = 0
        cmbNForma0.Tag = 0
        lbNPedido0.Tag = 0
        txtNCliente0.Tag = 0
        lbNNombre0.Tag = 0
        txtNLitros0.Tag = 0
        txtNPrecio0.Tag = 0
        lbNImporte0.Tag = 0
        cmbNTipo0.Tag = 0

        txtNCliente0.ContextMenu = ContextMenu2

        For i = 1 To 59

            Dim EtiquetasNota As TextBox = New TextBox()
            EtiquetasNota.Name = "txtNNota" + CType(i, String)
            EtiquetasNota.Visible = True
            EtiquetasNota.TextAlign = HorizontalAlignment.Right
            EtiquetasNota.Text = ""
            EtiquetasNota.CharacterCasing = CharacterCasing.Upper
            EtiquetasNota.BackColor = Color.White
            EtiquetasNota.Size = New Size(80, 21)
            EtiquetasNota.BorderStyle = BorderStyle.FixedSingle
            EtiquetasNota.Location = New Point(0, Alto)
            EtiquetasNota.MaxLength = 19
            EtiquetasNota.BringToFront()
            AddHandler EtiquetasNota.Enter, AddressOf EntradaGrid2
            AddHandler EtiquetasNota.KeyPress, AddressOf MyKeyPress2
            AddHandler EtiquetasNota.Validated, AddressOf ValidatedNota
            AddHandler EtiquetasNota.TextChanged, AddressOf TextChanged2
            AddHandler EtiquetasNota.KeyDown, AddressOf KeyDownNotas
            EtiquetasNota.Tag = i
            pnNotas.Controls.Add(EtiquetasNota)


            Dim EtiquetasCliente As TextBox = New TextBox()
            EtiquetasCliente.Name = "txtNCliente" + CType(i, String)
            EtiquetasCliente.Visible = True
            EtiquetasCliente.TextAlign = HorizontalAlignment.Right
            EtiquetasCliente.Text = ""
            EtiquetasCliente.CharacterCasing = CharacterCasing.Upper
            EtiquetasCliente.BackColor = Color.White
            EtiquetasCliente.Size = New Size(80, 21)
            EtiquetasCliente.BorderStyle = BorderStyle.FixedSingle
            EtiquetasCliente.Location = New Point(79, Alto)
            EtiquetasCliente.BringToFront()
            EtiquetasCliente.ContextMenu = ContextMenu2
            EtiquetasCliente.MaxLength = 9
            AddHandler EtiquetasCliente.Enter, AddressOf EntradaGrid2
            AddHandler EtiquetasCliente.KeyPress, AddressOf MyKeyPress2
            AddHandler EtiquetasCliente.TextChanged, AddressOf TextChanged2
            AddHandler EtiquetasCliente.Validated, AddressOf ValidatedCliente
            AddHandler EtiquetasCliente.KeyDown, AddressOf KeyDownNotas
            CType(EtiquetasCliente, System.Windows.Forms.TextBox).ReadOnly = False
            EtiquetasCliente.Tag = i
            pnNotas.Controls.Add(EtiquetasCliente)

            Dim EtiquetasPedido As Label = New Label()
            EtiquetasPedido.Name = "lbNPedido" + CType(i, String)
            EtiquetasPedido.Visible = True
            EtiquetasPedido.Text = ""
            EtiquetasPedido.BackColor = System.Drawing.SystemColors.Control
            EtiquetasPedido.Size = New Size(80, 21)
            EtiquetasPedido.BorderStyle = BorderStyle.FixedSingle
            EtiquetasPedido.Location = New Point(158, Alto)
            EtiquetasPedido.BringToFront()
            EtiquetasPedido.Tag = i
            EtiquetasPedido.TextAlign = ContentAlignment.MiddleCenter
            EtiquetasPedido.Font = CType(lbPedido0.Font, Font)
            pnNotas.Controls.Add(EtiquetasPedido)

            Dim EtiquetasNombre As New Label()
            EtiquetasNombre.Name = "lbNNombre" + CType(i, String)
            EtiquetasNombre.Visible = True
            EtiquetasNombre.TextAlign = ContentAlignment.MiddleLeft
            EtiquetasNombre.Text = ""
            EtiquetasNombre.BackColor = System.Drawing.SystemColors.Control
            EtiquetasNombre.Size = New Size(123, 21)
            EtiquetasNombre.BorderStyle = BorderStyle.FixedSingle
            EtiquetasNombre.Location = New Point(237, Alto)
            EtiquetasNombre.BringToFront()
            EtiquetasNombre.Tag = i
            pnNotas.Controls.Add(EtiquetasNombre)

            Dim EtiquetasLitros As TextBox = New TextBox()
            EtiquetasLitros.Name = "txtNLitros" + CType(i, String)
            EtiquetasLitros.Visible = True
            EtiquetasLitros.TextAlign = HorizontalAlignment.Right
            EtiquetasLitros.Text = "0"
            EtiquetasLitros.MaxLength = 8
            EtiquetasLitros.CharacterCasing = CharacterCasing.Upper
            EtiquetasLitros.BackColor = Color.White
            EtiquetasLitros.Size = New Size(51, 21)
            EtiquetasLitros.BorderStyle = BorderStyle.FixedSingle
            EtiquetasLitros.Location = New Point(357, Alto)
            EtiquetasLitros.BringToFront()
            AddHandler EtiquetasLitros.Enter, AddressOf EntradaGrid2
            AddHandler EtiquetasLitros.KeyPress, AddressOf MyKeyPress
            AddHandler EtiquetasLitros.TextChanged, AddressOf TextChanged2
            AddHandler EtiquetasLitros.Validated, AddressOf MyValidated
            AddHandler EtiquetasLitros.KeyDown, AddressOf KeyDownNotas

            CType(EtiquetasLitros, System.Windows.Forms.TextBox).ReadOnly = False

            EtiquetasLitros.Tag = i
            pnNotas.Controls.Add(EtiquetasLitros)

            Dim EtiquetasPrecio As TextBox = New TextBox()
            EtiquetasPrecio.Name = "txtNPrecio" + CType(i, String)
            EtiquetasPrecio.Visible = True
            EtiquetasPrecio.TextAlign = HorizontalAlignment.Right

            If _Ruta <> 150 Then
                If _Fecha.Date.Month <> Now.Date.Month Then
                    EtiquetasPrecio.Text = CType(GLOBAL_PrecioAnterior, String)
                Else
                    EtiquetasPrecio.Text = CType(GLOBAL_Precio, String)
                End If
            Else
                If _Fecha.Date.Month <> Now.Date.Month Then
                    EtiquetasPrecio.Text = CType(GLOBAL_PrecioAnteriorToluca, String)
                Else
                    EtiquetasPrecio.Text = CType(GLOBAL_PrecioToluca, String)
                End If
            End If

            EtiquetasLitros.CharacterCasing = CharacterCasing.Upper
            EtiquetasPrecio.BackColor = Color.PaleGoldenrod
            EtiquetasPrecio.Size = New Size(57, 21)
            EtiquetasPrecio.BorderStyle = BorderStyle.FixedSingle
            EtiquetasPrecio.Location = New Point(407, Alto)
            EtiquetasPrecio.BringToFront()
            AddHandler EtiquetasPrecio.Enter, AddressOf EntradaGrid2
            AddHandler EtiquetasPrecio.KeyPress, AddressOf MyKeyPress
            AddHandler EtiquetasPrecio.TextChanged, AddressOf TextChanged2
            AddHandler EtiquetasPrecio.Validated, AddressOf MyValidated

            'If _Ruta <> 150 Then
            '    CType(EtiquetasPrecio, System.Windows.Forms.TextBox).ReadOnly = True
            'Else
            CType(EtiquetasPrecio, System.Windows.Forms.TextBox).ReadOnly = False
            'End If

            EtiquetasPrecio.Tag = i
            pnNotas.Controls.Add(EtiquetasPrecio)

            Dim EtiquetasImporte As Label = New Label()
            EtiquetasImporte.Name = "lbNImporte" + CType(i, String)
            EtiquetasImporte.Visible = True
            EtiquetasImporte.TextAlign = ContentAlignment.MiddleRight
            EtiquetasImporte.Text = "$ 0.00"
            EtiquetasImporte.BackColor = Color.DarkGreen
            EtiquetasImporte.ForeColor = Color.White
            EtiquetasImporte.Size = New Size(80, 21)
            EtiquetasImporte.BorderStyle = BorderStyle.FixedSingle
            EtiquetasImporte.Location = New Point(463, Alto)
            EtiquetasImporte.BringToFront()
            EtiquetasImporte.Tag = i
            pnNotas.Controls.Add(EtiquetasImporte)

            Dim EtiquetasForma As ComboBox = New ComboBox()
            EtiquetasForma.Name = "cmbNForma" + CType(i, String)
            EtiquetasForma.Visible = True
            EtiquetasForma.BackColor = Color.White
            EtiquetasForma.Size = New Size(83, 21)
            EtiquetasForma.Location = New Point(543, Alto)
            EtiquetasForma.DropDownStyle = ComboBoxStyle.DropDownList
            EtiquetasForma.Tag = i
            EtiquetasForma.Items.Add("CONTADO")
            EtiquetasForma.Items.Add("CREDITO")
            EtiquetasForma.SelectedIndex = 0
            EtiquetasForma.Enabled = True
            EtiquetasForma.Text = "CONTADO"
            '   EtiquetasForma.ContextMenu = ContextMenu3
            AddHandler EtiquetasForma.Enter, AddressOf EntradaGrid3
            AddHandler EtiquetasForma.SelectedIndexChanged, AddressOf TextChanged3
            'AddHandler EtiquetasForma.Validated, AddressOf ValidatedForma
            AddHandler EtiquetasForma.KeyDown, AddressOf KeyDownNotas2
            pnNotas.Controls.Add(EtiquetasForma)

            Dim EtiquetasTipo As ComboBox = New ComboBox()
            EtiquetasTipo.Name = "cmbNTipo" + CType(i, String)
            EtiquetasTipo.Visible = True
            EtiquetasTipo.BackColor = Color.White
            EtiquetasTipo.Size = New Size(120, 21)
            EtiquetasTipo.Location = New Point(624, Alto)
            EtiquetasTipo.DropDownStyle = ComboBoxStyle.DropDownList
            EtiquetasTipo.Enabled = True
            EtiquetasTipo.Tag = i
            ContextMenu1.MenuItems(0).Enabled = True
            EtiquetasTipo.Items.Add("Contado")
            EtiquetasTipo.SelectedIndex = 0
            AddHandler EtiquetasTipo.Enter, AddressOf EntradaGrid3
            AddHandler EtiquetasTipo.SelectedIndexChanged, AddressOf SelectedIndexChanged2
            pnNotas.Controls.Add(EtiquetasTipo)

            Alto = Alto + 20
        Next

        DatoInicial = CType(txtNLitros0.Text, Decimal)
        txtNLitros0.Text = "1"
        Try
            txtNLitros0.Text = CType(DatoInicial, String)
        Catch ex As Exception
            txtNLitros0.Text = "0"
        End Try

    End Sub



    Private Sub CargarGrid()
        Dim i As Integer
        Dim Alto As Integer
        Dim DatoInicial As Decimal
        Alto = 23

        cmbForma0.SelectedIndex = 0
        cmbForma0.Text = "CONTADO"
        AddHandler txtLitros0.Enter, AddressOf EntradaGrid1
        AddHandler txtPrecio0.Enter, AddressOf EntradaGrid1
        AddHandler cmbForma0.Enter, AddressOf EntradaGrid4
        AddHandler cmbTipo0.Enter, AddressOf EntradaGrid4
        AddHandler txtLitros0.KeyPress, AddressOf MyKeyPress
        AddHandler txtLitros0.TextChanged, AddressOf TextChanged1
        AddHandler txtLitros0.Validated, AddressOf MyValidated
        AddHandler txtPrecio0.KeyPress, AddressOf MyKeyPress
        AddHandler txtPrecio0.TextChanged, AddressOf TextChanged1
        AddHandler txtPrecio0.Validated, AddressOf MyValidated


        AddHandler cmbForma0.SelectedIndexChanged, AddressOf TextChanged4


        AddHandler cmbTipo0.SelectedIndexChanged, AddressOf SelectedIndexChanged1
        AddHandler txtLitros0.KeyDown, AddressOf KeyDownRemision
        cmbTipo0.Items.Add("Contado")
        cmbTipo0.SelectedIndex = 0
        cmbTipo0.Enabled = False

        CType(txtPrecio0, System.Windows.Forms.TextBox).ReadOnly = False


        cmbForma0.Tag = 0
        lbPedido0.Tag = 0
        lbCliente0.Tag = 0
        lbNombre0.Tag = 0
        txtLitros0.Tag = 0
        txtPrecio0.Tag = 0
        lbImporte0.Tag = 0
        cmbTipo0.Tag = 0
        txtPrecio0.Text = CType(GLOBAL_Precio, String)


        If _CorrerDescarga = False Then

            If _Celula <> 6 Then
                cmdPedido.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " SELECT N.Celula, N.Ruta, N.AñoNota, N.Nota, N.AñoPed, N.Pedido, N.Cliente, CONVERT(varChar(30), C.Nombre) AS Nombre FROM Nota N INNER JOIN Cliente C ON N.Cliente = C.Cliente WHERE (N.TipoNota = 1) AND (N.Status = 'PENDIENTE') AND (N.Ruta = @Ruta) ORDER BY C.Cliente " & _
                                        " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                cmdPedido.Parameters.Clear()
                cmdPedido.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
            Else
                cmdPedido.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " SELECT N.Celula, N.Ruta, N.AñoNota, N.Nota, N.AñoPed, N.Pedido, N.Cliente, CONVERT(varChar(30), C.Nombre) AS Nombre FROM Nota N INNER JOIN Cliente C ON N.Cliente = C.Cliente WHERE (N.TipoNota = 1) AND (N.Status = 'PENDIENTE') AND (N.Ruta = @Ruta) AND (N.FNota=@Fecha) ORDER BY C.Cliente " & _
                                        " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "

                cmdPedido.Parameters.Clear()
                cmdPedido.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                cmdPedido.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha.Date
            End If

            If _Ruta <> 150 Then
                If _Fecha.Date.Month <> Now.Date.Month Then
                    txtPrecio0.Text = CType(GLOBAL_PrecioAnterior, String)
                Else
                    txtPrecio0.Text = CType(GLOBAL_Precio, String)
                End If
            Else
                If _Fecha.Date.Month <> Now.Date.Month Then
                    txtPrecio0.Text = CType(GLOBAL_PrecioAnteriorToluca, String)
                Else
                    txtPrecio0.Text = CType(GLOBAL_PrecioToluca, String)
                End If
            End If


        Else
            If _Celula <> 6 Then
                cmdPedido.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " Select N.Celula, N.Ruta, N.AñoNota, N.Nota, N.AñoPed, N.Pedido, N.Cliente, " & _
                                                    "      CONVERT(varChar(30), C.Nombre) AS Nombre " & _
                                                    "from Nota N INNER JOIN Cliente C on N.Cliente=C.Cliente " & _
                                                    "            LEFT JOIN Rampac R ON R.Cliente=N.Cliente " & _
                                                    "WHERE     (N.TipoNota = 1) AND (N.Status = 'PENDIENTE') " & _
                                                    "AND (N.Ruta = @Ruta) and R.Añoatt=@AñoAtt and R.Folio=@Folio and R.TipoOperacion='Tarjeta' " & _
                                                    "Order by R.Litros " & _
                                        " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                cmdPedido.Parameters.Clear()
                cmdPedido.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                cmdPedido.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                cmdPedido.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
            Else
                cmdPedido.CommandText = " SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED " & _
                                        " Select N.Celula, N.Ruta, N.AñoNota, N.Nota, N.AñoPed, N.Pedido, N.Cliente, " & _
                                                                    "      CONVERT(varChar(30), C.Nombre) AS Nombre " & _
                                                                    "from Nota N INNER JOIN Cliente C on N.Cliente=C.Cliente " & _
                                                                    "            LEFT JOIN Rampac R ON R.Cliente=N.Cliente " & _
                                                                    "WHERE     (N.TipoNota = 1) AND (N.Status = 'PENDIENTE') AND (N.FNota=@Fecha) " & _
                                                                    "AND (N.Ruta = @Ruta) and R.Añoatt=@AñoAtt and R.Folio=@Folio and R.TipoOperacion='Tarjeta' " & _
                                                                    "Order by R.Litros " & _
                                        " SET TRANSACTION ISOLATION LEVEL READ COMMITTED "
                cmdPedido.Parameters.Clear()
                cmdPedido.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                cmdPedido.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                cmdPedido.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
                cmdPedido.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = _Fecha.Date
            End If
        End If

        daPedido.Fill(DsLiquidacion, "Pedido")

        'DataGrid1.DataSource = DsLiquidacion 'RDC

        If DsLiquidacion.Pedido.Count > 0 Then
            lbPedido0.Text = CType(DsLiquidacion.Pedido(0).Pedido, String)
            lbCliente0.Text = CType(DsLiquidacion.Pedido(0).Cliente, String)
            lbNombre0.Text = CType(DsLiquidacion.Pedido(0).Nombre, String)
        End If

        If DsLiquidacion.Pedido.Count > 1 Then
            For i = 1 To DsLiquidacion.Pedido.Count - 1
                Dim EtiquetasPedido As Label = New Label()
                EtiquetasPedido.Name = "lbPedido" + CType(i, String)
                EtiquetasPedido.Visible = True
                EtiquetasPedido.Text = CType(DsLiquidacion.Pedido(i).Pedido, String)
                EtiquetasPedido.BackColor = System.Drawing.SystemColors.Control
                EtiquetasPedido.Size = New Size(80, 21)
                EtiquetasPedido.BorderStyle = BorderStyle.FixedSingle
                EtiquetasPedido.Location = New Point(0, Alto)
                EtiquetasPedido.BringToFront()
                EtiquetasPedido.Tag = i
                EtiquetasPedido.TextAlign = ContentAlignment.MiddleCenter
                EtiquetasPedido.Font = CType(lbPedido0.Font, Font)
                pnPedidos.Controls.Add(EtiquetasPedido)

                Dim EtiquetasCliente As Label = New Label()
                EtiquetasCliente.Name = "lbCliente" + CType(i, String)
                EtiquetasCliente.Visible = True
                EtiquetasCliente.TextAlign = ContentAlignment.MiddleCenter
                EtiquetasCliente.Text = CType(DsLiquidacion.Pedido(i).Cliente, String)
                EtiquetasCliente.BackColor = System.Drawing.SystemColors.Control
                EtiquetasCliente.Size = New Size(80, 21)
                EtiquetasCliente.BorderStyle = BorderStyle.FixedSingle
                EtiquetasCliente.Location = New Point(79, Alto)
                EtiquetasCliente.BringToFront()
                EtiquetasCliente.Tag = i
                EtiquetasCliente.Font = CType(lbPedido0.Font, Font)
                pnPedidos.Controls.Add(EtiquetasCliente)

                Dim EtiquetasNombre As Label = New Label()
                EtiquetasNombre.Name = "lbNombre" + CType(i, String)
                EtiquetasNombre.Visible = True
                EtiquetasNombre.TextAlign = ContentAlignment.MiddleLeft
                If DsLiquidacion.Pedido(i).Nombre.Length > 30 Then
                    EtiquetasNombre.Text = CType(DsLiquidacion.Pedido(i).Nombre, String).Substring(0, 30)
                Else
                    EtiquetasNombre.Text = CType(DsLiquidacion.Pedido(i).Nombre, String)
                End If

                EtiquetasNombre.BackColor = System.Drawing.SystemColors.Control
                EtiquetasNombre.Size = New Size(200, 21)
                EtiquetasNombre.BorderStyle = BorderStyle.FixedSingle
                EtiquetasNombre.Location = New Point(158, Alto)
                EtiquetasNombre.BringToFront()
                EtiquetasNombre.Tag = i
                pnPedidos.Controls.Add(EtiquetasNombre)

                Dim EtiquetasLitros As TextBox = New TextBox()
                EtiquetasLitros.Name = "txtLitros" + CType(i, String)
                EtiquetasLitros.Visible = True
                EtiquetasLitros.TextAlign = HorizontalAlignment.Right
                EtiquetasLitros.Text = "0"
                EtiquetasLitros.MaxLength = 8
                EtiquetasLitros.CharacterCasing = CharacterCasing.Upper
                EtiquetasLitros.BackColor = Color.White
                EtiquetasLitros.Size = New Size(51, 21)
                EtiquetasLitros.BorderStyle = BorderStyle.FixedSingle
                EtiquetasLitros.Location = New Point(357, Alto)
                EtiquetasLitros.BringToFront()
                EtiquetasLitros.Tag = i
                AddHandler EtiquetasLitros.Enter, AddressOf EntradaGrid1
                AddHandler EtiquetasLitros.KeyPress, AddressOf MyKeyPress
                AddHandler EtiquetasLitros.TextChanged, AddressOf TextChanged1
                AddHandler EtiquetasLitros.Validated, AddressOf MyValidated
                AddHandler EtiquetasLitros.KeyDown, AddressOf KeyDownRemision
                pnPedidos.Controls.Add(EtiquetasLitros)

                Dim EtiquetasPrecio As TextBox = New TextBox()
                EtiquetasPrecio.Name = "txtPrecio" + CType(i, String)
                EtiquetasPrecio.Visible = True
                EtiquetasPrecio.TextAlign = HorizontalAlignment.Right
                If _CorrerDescarga = False Then
                    If _Ruta <> 150 Then
                        If _Fecha.Date.Month <> Now.Date.Month Then
                            EtiquetasPrecio.Text = CType(GLOBAL_PrecioAnterior, String)
                        Else
                            EtiquetasPrecio.Text = CType(GLOBAL_Precio, String)
                        End If
                    Else
                        If _Fecha.Date.Month <> Now.Date.Month Then
                            EtiquetasPrecio.Text = CType(GLOBAL_PrecioAnteriorToluca, String)
                        Else
                            EtiquetasPrecio.Text = CType(GLOBAL_PrecioToluca, String)
                        End If
                    End If
                Else
                    EtiquetasPrecio.Text = CType(GLOBAL_Precio, String)
                End If

                EtiquetasPrecio.MaxLength = 6
                EtiquetasPrecio.CharacterCasing = CharacterCasing.Upper
                EtiquetasPrecio.BackColor = Color.PaleGoldenrod
                EtiquetasPrecio.Size = New Size(57, 21)
                EtiquetasPrecio.BorderStyle = BorderStyle.FixedSingle
                EtiquetasPrecio.Location = New Point(407, Alto)

                CType(EtiquetasPrecio, System.Windows.Forms.TextBox).ReadOnly = False

                EtiquetasPrecio.BringToFront()
                AddHandler EtiquetasPrecio.Enter, AddressOf EntradaGrid1
                AddHandler EtiquetasPrecio.KeyPress, AddressOf MyKeyPress
                AddHandler EtiquetasPrecio.TextChanged, AddressOf TextChanged1
                AddHandler EtiquetasPrecio.Validated, AddressOf MyValidated
                EtiquetasPrecio.Tag = i
                pnPedidos.Controls.Add(EtiquetasPrecio)

                Dim EtiquetasImporte As Label = New Label()
                EtiquetasImporte.Name = "lbImporte" + CType(i, String)
                EtiquetasImporte.Visible = True
                EtiquetasImporte.TextAlign = ContentAlignment.MiddleRight
                EtiquetasImporte.Text = "$ 0.00"
                EtiquetasImporte.BackColor = Color.DarkGreen
                EtiquetasImporte.ForeColor = Color.White
                EtiquetasImporte.Size = New Size(80, 21)
                EtiquetasImporte.BorderStyle = BorderStyle.FixedSingle
                EtiquetasImporte.Location = New Point(463, Alto)
                EtiquetasImporte.BringToFront()
                EtiquetasImporte.Tag = i
                pnPedidos.Controls.Add(EtiquetasImporte)


                Dim EtiquetasForma As ComboBox = New ComboBox()
                EtiquetasForma.Name = "cmbForma" + CType(i, String)
                EtiquetasForma.Visible = True
                EtiquetasForma.BackColor = Color.White
                EtiquetasForma.Size = New Size(83, 21)
                EtiquetasForma.Location = New Point(543, Alto)
                EtiquetasForma.DropDownStyle = ComboBoxStyle.DropDownList
                EtiquetasForma.Tag = i
                EtiquetasForma.Items.Add("CONTADO")
                EtiquetasForma.Items.Add("CREDITO")
                EtiquetasForma.SelectedIndex = 0
                EtiquetasForma.Text = "CONTADO"
                'EtiquetasForma.ContextMenu = ContextMenu1
                AddHandler EtiquetasForma.Enter, AddressOf EntradaGrid4
                AddHandler EtiquetasForma.SelectedIndexChanged, AddressOf TextChanged4
                pnPedidos.Controls.Add(EtiquetasForma)

                Dim EtiquetasTipo As ComboBox = New ComboBox()
                EtiquetasTipo.Name = "cmbTipo" + CType(i, String)
                EtiquetasTipo.Visible = True
                EtiquetasTipo.BackColor = Color.White
                EtiquetasTipo.Size = New Size(120, 21)
                EtiquetasTipo.Location = New Point(624, Alto)
                EtiquetasTipo.DropDownStyle = ComboBoxStyle.DropDownList
                EtiquetasTipo.Enabled = False
                ContextMenu1.MenuItems(0).Enabled = True
                EtiquetasTipo.Items.Add("Contado")
                EtiquetasTipo.SelectedIndex = 0
                AddHandler EtiquetasTipo.Enter, AddressOf EntradaGrid4
                AddHandler EtiquetasTipo.SelectedIndexChanged, AddressOf SelectedIndexChanged1
                EtiquetasTipo.Tag = i
                pnPedidos.Controls.Add(EtiquetasTipo)

                Alto = Alto + 20
            Next

        End If
        DatoInicial = CType(txtLitros0.Text, Decimal)
        txtLitros0.Text = "1"
        txtLitros0.Text = CType(DatoInicial, String)


    End Sub




#Region " Windows Form Designer generated code "
    'TODO: validacion añadida el día 08/10/2004 sub new
    Public Sub New(Optional ByVal ValidacionCreditoCliente As Boolean = False)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'TODO: validacion añadida el día 08/10/2004 asignacion en sub new
        _aplicaValidacionCreditocliente = ValidacionCreditoCliente

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
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents cmdOperador As System.Data.SqlClient.SqlCommand
    Friend WithEvents daOperador As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsLiquidacion As Sigamet.dsLiquidacion
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    Friend WithEvents lbAutotanque As System.Windows.Forms.Label
    Friend WithEvents lbOperador As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnPedidos As System.Windows.Forms.Panel
    Friend WithEvents cmbForma0 As System.Windows.Forms.ComboBox
    Friend WithEvents lbImporte0 As System.Windows.Forms.Label
    Friend WithEvents txtLitros0 As System.Windows.Forms.TextBox
    Friend WithEvents lbCliente0 As System.Windows.Forms.Label
    Friend WithEvents lbPedido0 As System.Windows.Forms.Label
    Friend WithEvents lbNombre0 As System.Windows.Forms.Label
    Friend WithEvents cmbTipo0 As System.Windows.Forms.ComboBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPrecio0 As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lbFecha As System.Windows.Forms.Label
    Friend WithEvents lbTotalLitros1 As System.Windows.Forms.Label
    Friend WithEvents lbTotalImporte1 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lbNImporte0 As System.Windows.Forms.Label
    Friend WithEvents txtNPrecio0 As System.Windows.Forms.TextBox
    Friend WithEvents cmbNTipo0 As System.Windows.Forms.ComboBox
    Friend WithEvents lbNNombre0 As System.Windows.Forms.Label
    Friend WithEvents cmbNForma0 As System.Windows.Forms.ComboBox
    Friend WithEvents txtNLitros0 As System.Windows.Forms.TextBox
    Friend WithEvents txtNCliente0 As System.Windows.Forms.TextBox
    Friend WithEvents pnNotas As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents cmdPedido As System.Data.SqlClient.SqlCommand
    Friend WithEvents daPedido As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents lbLitrosNotas As System.Windows.Forms.Label
    Friend WithEvents lbTotalNotas As System.Windows.Forms.Label
    Friend WithEvents lbLitrosContado As System.Windows.Forms.Label
    Friend WithEvents lbTotalContado As System.Windows.Forms.Label
    Friend WithEvents lbLitrosCredito As System.Windows.Forms.Label
    Friend WithEvents lbTotalCredito As System.Windows.Forms.Label
    Friend WithEvents lbTotalLitros As System.Windows.Forms.Label
    Friend WithEvents lbTotalImporte As System.Windows.Forms.Label
    Friend WithEvents lstForma As System.Windows.Forms.ListBox
    Friend WithEvents lstPrecio As System.Windows.Forms.ListBox
    Friend WithEvents lstLitros As System.Windows.Forms.ListBox
    Friend WithEvents lstClientes As System.Windows.Forms.ListBox
    Friend WithEvents lstPedido As System.Windows.Forms.ListBox
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents dgPago As System.Windows.Forms.DataGrid
    Friend WithEvents dgctPago As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents DataGridTextBoxColumn1 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn2 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lstTipoPago As System.Windows.Forms.ListBox
    Friend WithEvents txtNNota0 As System.Windows.Forms.TextBox
    Friend WithEvents lbNPedido0 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents lstTipoPago2 As System.Windows.Forms.ListBox
    Friend WithEvents lstPedido2 As System.Windows.Forms.ListBox
    Friend WithEvents lstClientes2 As System.Windows.Forms.ListBox
    Friend WithEvents lstLitros2 As System.Windows.Forms.ListBox
    Friend WithEvents lstPrecio2 As System.Windows.Forms.ListBox
    Friend WithEvents lstForma2 As System.Windows.Forms.ListBox
    Friend WithEvents lstNotas As System.Windows.Forms.ListBox
    Friend WithEvents ContextMenu2 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenu3 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents lbTotalNotaRemision As System.Windows.Forms.Label
    Friend WithEvents lbTotalRemisionSurtida As System.Windows.Forms.Label
    Friend WithEvents lbTotalRemisionNo As System.Windows.Forms.Label
    Friend WithEvents lbNotasBlancas As System.Windows.Forms.Label
    Friend WithEvents pnTotales As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents lbTotal As System.Windows.Forms.Label
    Friend WithEvents btnDocumento As System.Windows.Forms.Button
    Friend WithEvents lstNotasBlancas As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtFolio As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Friend WithEvents ContextMenu4 As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents lbUnificacion As System.Windows.Forms.Label
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents ListBox8 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox5 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox4 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox6 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox7 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Liquidacion))
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.cmdOperador = New System.Data.SqlClient.SqlCommand()
        Me.daOperador = New System.Data.SqlClient.SqlDataAdapter()
        Me.DsLiquidacion = New Sigamet.dsLiquidacion()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.lbAutotanque = New System.Windows.Forms.Label()
        Me.lbOperador = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnPedidos = New System.Windows.Forms.Panel()
        Me.lbImporte0 = New System.Windows.Forms.Label()
        Me.txtPrecio0 = New System.Windows.Forms.TextBox()
        Me.lbCliente0 = New System.Windows.Forms.Label()
        Me.cmbTipo0 = New System.Windows.Forms.ComboBox()
        Me.lbNombre0 = New System.Windows.Forms.Label()
        Me.cmbForma0 = New System.Windows.Forms.ComboBox()
        Me.txtLitros0 = New System.Windows.Forms.TextBox()
        Me.lbPedido0 = New System.Windows.Forms.Label()
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbFecha = New System.Windows.Forms.Label()
        Me.lbTotalLitros1 = New System.Windows.Forms.Label()
        Me.lbTotalImporte1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.pnNotas = New System.Windows.Forms.Panel()
        Me.lbNNombre0 = New System.Windows.Forms.Label()
        Me.txtNLitros0 = New System.Windows.Forms.TextBox()
        Me.lbNPedido0 = New System.Windows.Forms.Label()
        Me.txtNCliente0 = New System.Windows.Forms.TextBox()
        Me.txtNNota0 = New System.Windows.Forms.TextBox()
        Me.lbNImporte0 = New System.Windows.Forms.Label()
        Me.txtNPrecio0 = New System.Windows.Forms.TextBox()
        Me.cmbNTipo0 = New System.Windows.Forms.ComboBox()
        Me.cmbNForma0 = New System.Windows.Forms.ComboBox()
        Me.ContextMenu3 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.lbLitrosNotas = New System.Windows.Forms.Label()
        Me.lbTotalNotas = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lbLitrosContado = New System.Windows.Forms.Label()
        Me.lbTotalContado = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.lbLitrosCredito = New System.Windows.Forms.Label()
        Me.lbTotalCredito = New System.Windows.Forms.Label()
        Me.lbTotalLitros = New System.Windows.Forms.Label()
        Me.lbTotalImporte = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.cmdPedido = New System.Data.SqlClient.SqlCommand()
        Me.daPedido = New System.Data.SqlClient.SqlDataAdapter()
        Me.lstForma = New System.Windows.Forms.ListBox()
        Me.lstPrecio = New System.Windows.Forms.ListBox()
        Me.lstLitros = New System.Windows.Forms.ListBox()
        Me.lstClientes = New System.Windows.Forms.ListBox()
        Me.lstPedido = New System.Windows.Forms.ListBox()
        Me.dgPago = New System.Windows.Forms.DataGrid()
        Me.dgctPago = New System.Windows.Forms.DataGridTableStyle()
        Me.DataGridTextBoxColumn1 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn2 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lstTipoPago = New System.Windows.Forms.ListBox()
        Me.lstTipoPago2 = New System.Windows.Forms.ListBox()
        Me.lstPedido2 = New System.Windows.Forms.ListBox()
        Me.lstClientes2 = New System.Windows.Forms.ListBox()
        Me.lstLitros2 = New System.Windows.Forms.ListBox()
        Me.lstPrecio2 = New System.Windows.Forms.ListBox()
        Me.lstForma2 = New System.Windows.Forms.ListBox()
        Me.lstNotas = New System.Windows.Forms.ListBox()
        Me.ContextMenu2 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.lbTotalNotaRemision = New System.Windows.Forms.Label()
        Me.lbTotalRemisionSurtida = New System.Windows.Forms.Label()
        Me.lbTotalRemisionNo = New System.Windows.Forms.Label()
        Me.lbNotasBlancas = New System.Windows.Forms.Label()
        Me.pnTotales = New System.Windows.Forms.Panel()
        Me.lbTotal = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.btnDocumento = New System.Windows.Forms.Button()
        Me.lstNotasBlancas = New System.Windows.Forms.ListBox()
        Me.txtFolio = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.ContextMenu4 = New System.Windows.Forms.ContextMenu()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.lbUnificacion = New System.Windows.Forms.Label()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.ListBox8 = New System.Windows.Forms.ListBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.ListBox5 = New System.Windows.Forms.ListBox()
        Me.ListBox4 = New System.Windows.Forms.ListBox()
        Me.ListBox6 = New System.Windows.Forms.ListBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ListBox7 = New System.Windows.Forms.ListBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        CType(Me.DsLiquidacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnPedidos.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.pnNotas.SuspendLayout()
        CType(Me.dgPago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnTotales.SuspendLayout()
        Me.SuspendLayout()
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'cmdOperador
        '
        Me.cmdOperador.CommandText = "SELECT Att.AñoAtt, Att.Folio, Att.Ruta, Att.Celula, Att.Autotanque, O.Operador, E" & _
        ".Nombre, Att.LitrosLiquidados FROM AutotanqueTurno Att INNER JOIN TripulacionTur" & _
        "no TT ON Att.Folio = TT.Folio AND Att.AñoAtt = TT.AñoAtt INNER JOIN Operador O O" & _
        "N TT.Operador = O.Operador INNER JOIN Empleado E ON O.Empleado = E.Empleado WHER" & _
        "E (Att.Folio = @Folio) AND (Att.AñoAtt = @Anio)"
        Me.cmdOperador.Connection = Me.SqlConnection
        Me.cmdOperador.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Folio", System.Data.SqlDbType.Int, 4, "Folio"))
        Me.cmdOperador.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Anio", System.Data.SqlDbType.SmallInt, 2, "AñoAtt"))
        '
        'daOperador
        '
        Me.daOperador.SelectCommand = Me.cmdOperador
        '
        'DsLiquidacion
        '
        Me.DsLiquidacion.DataSetName = "dsLiquidacion"
        Me.DsLiquidacion.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsLiquidacion.Namespace = "http://www.tempuri.org/dsLiquidacion.xsd"
        '
        'btnImprimir
        '
        Me.btnImprimir.Location = New System.Drawing.Point(864, 128)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(88, 23)
        Me.btnImprimir.TabIndex = 3
        Me.btnImprimir.Text = "Imprimir"
        '
        'btnCancelar
        '
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Bitmap)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(864, 58)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(88, 23)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Bitmap)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(864, 31)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(88, 23)
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbAutotanque
        '
        Me.lbAutotanque.AutoSize = True
        Me.lbAutotanque.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAutotanque.ForeColor = System.Drawing.Color.Blue
        Me.lbAutotanque.Location = New System.Drawing.Point(120, 5)
        Me.lbAutotanque.Name = "lbAutotanque"
        Me.lbAutotanque.Size = New System.Drawing.Size(22, 16)
        Me.lbAutotanque.TabIndex = 30
        Me.lbAutotanque.Text = "45"
        Me.lbAutotanque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbOperador
        '
        Me.lbOperador.AutoSize = True
        Me.lbOperador.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbOperador.ForeColor = System.Drawing.Color.Blue
        Me.lbOperador.Location = New System.Drawing.Point(120, 21)
        Me.lbOperador.Name = "lbOperador"
        Me.lbOperador.Size = New System.Drawing.Size(96, 16)
        Me.lbOperador.TabIndex = 31
        Me.lbOperador.Text = "Pancho Perez"
        Me.lbOperador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 16)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Autotanque:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(24, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 16)
        Me.Label2.TabIndex = 33
        Me.Label2.Text = "Operador:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Khaki
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label3})
        Me.Panel1.Location = New System.Drawing.Point(24, 37)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(768, 24)
        Me.Panel1.TabIndex = 34
        '
        'Label3
        '
        Me.Label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(768, 24)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Pedidos de las notas de remison y la tarjeta rampac"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnPedidos
        '
        Me.pnPedidos.AutoScroll = True
        Me.pnPedidos.BackColor = System.Drawing.SystemColors.Control
        Me.pnPedidos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnPedidos.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbImporte0, Me.txtPrecio0, Me.lbCliente0, Me.cmbTipo0, Me.lbNombre0, Me.cmbForma0, Me.txtLitros0, Me.lbPedido0})
        Me.pnPedidos.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnPedidos.Location = New System.Drawing.Point(24, 82)
        Me.pnPedidos.Name = "pnPedidos"
        Me.pnPedidos.Size = New System.Drawing.Size(768, 182)
        Me.pnPedidos.TabIndex = 0
        '
        'lbImporte0
        '
        Me.lbImporte0.BackColor = System.Drawing.Color.DarkGreen
        Me.lbImporte0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbImporte0.ForeColor = System.Drawing.Color.White
        Me.lbImporte0.Location = New System.Drawing.Point(463, 3)
        Me.lbImporte0.Name = "lbImporte0"
        Me.lbImporte0.Size = New System.Drawing.Size(80, 21)
        Me.lbImporte0.TabIndex = 5
        Me.lbImporte0.Text = "$ 0.00"
        Me.lbImporte0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrecio0
        '
        Me.txtPrecio0.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.txtPrecio0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPrecio0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtPrecio0.Location = New System.Drawing.Point(407, 3)
        Me.txtPrecio0.MaxLength = 6
        Me.txtPrecio0.Name = "txtPrecio0"
        Me.txtPrecio0.Size = New System.Drawing.Size(57, 21)
        Me.txtPrecio0.TabIndex = 4
        Me.txtPrecio0.Text = "0.00"
        Me.txtPrecio0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbCliente0
        '
        Me.lbCliente0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbCliente0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCliente0.Location = New System.Drawing.Point(79, 3)
        Me.lbCliente0.Name = "lbCliente0"
        Me.lbCliente0.Size = New System.Drawing.Size(80, 21)
        Me.lbCliente0.TabIndex = 1
        Me.lbCliente0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbTipo0
        '
        Me.cmbTipo0.BackColor = System.Drawing.SystemColors.Control
        Me.cmbTipo0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo0.DropDownWidth = 120
        Me.cmbTipo0.Location = New System.Drawing.Point(624, 3)
        Me.cmbTipo0.Name = "cmbTipo0"
        Me.cmbTipo0.Size = New System.Drawing.Size(120, 21)
        Me.cmbTipo0.TabIndex = 7
        '
        'lbNombre0
        '
        Me.lbNombre0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbNombre0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNombre0.Location = New System.Drawing.Point(158, 3)
        Me.lbNombre0.Name = "lbNombre0"
        Me.lbNombre0.Size = New System.Drawing.Size(200, 21)
        Me.lbNombre0.TabIndex = 2
        Me.lbNombre0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbForma0
        '
        Me.cmbForma0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbForma0.DropDownWidth = 88
        Me.cmbForma0.Items.AddRange(New Object() {"CONTADO", "CREDITO"})
        Me.cmbForma0.Location = New System.Drawing.Point(543, 3)
        Me.cmbForma0.Name = "cmbForma0"
        Me.cmbForma0.Size = New System.Drawing.Size(83, 21)
        Me.cmbForma0.TabIndex = 6
        '
        'txtLitros0
        '
        Me.txtLitros0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtLitros0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtLitros0.Location = New System.Drawing.Point(357, 3)
        Me.txtLitros0.MaxLength = 8
        Me.txtLitros0.Name = "txtLitros0"
        Me.txtLitros0.Size = New System.Drawing.Size(51, 21)
        Me.txtLitros0.TabIndex = 3
        Me.txtLitros0.Text = "0"
        Me.txtLitros0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbPedido0
        '
        Me.lbPedido0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbPedido0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPedido0.Location = New System.Drawing.Point(0, 3)
        Me.lbPedido0.Name = "lbPedido0"
        Me.lbPedido0.Size = New System.Drawing.Size(80, 21)
        Me.lbPedido0.TabIndex = 0
        Me.lbPedido0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ContextMenu1
        '
        Me.ContextMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.Text = "Detalle de pago de contado"
        '
        'Panel2
        '
        Me.Panel2.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label12, Me.Label11, Me.Label10, Me.Label9, Me.Label8, Me.Label7, Me.Label6, Me.Label5, Me.Label4})
        Me.Panel2.Location = New System.Drawing.Point(24, 61)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(768, 24)
        Me.Panel2.TabIndex = 35
        '
        'Label12
        '
        Me.Label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label12.Location = New System.Drawing.Point(744, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(24, 24)
        Me.Label12.TabIndex = 8
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label11.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label11.Location = New System.Drawing.Point(624, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 24)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Pago"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label10
        '
        Me.Label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label10.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label10.Location = New System.Drawing.Point(544, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(80, 24)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Tipo"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label9.Location = New System.Drawing.Point(464, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(80, 24)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "Importe"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label8
        '
        Me.Label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label8.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label8.Location = New System.Drawing.Point(408, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 24)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Precio"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label7
        '
        Me.Label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label7.Location = New System.Drawing.Point(360, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 24)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Litros"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label6.Location = New System.Drawing.Point(160, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(200, 24)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Cliente"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label5.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label5.Location = New System.Drawing.Point(80, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 24)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Contrato"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 24)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Pedido"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbFecha
        '
        Me.lbFecha.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFecha.Location = New System.Drawing.Point(560, 21)
        Me.lbFecha.Name = "lbFecha"
        Me.lbFecha.Size = New System.Drawing.Size(232, 16)
        Me.lbFecha.TabIndex = 36
        Me.lbFecha.Text = "FECHA"
        Me.lbFecha.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalLitros1
        '
        Me.lbTotalLitros1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalLitros1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalLitros1.Location = New System.Drawing.Point(336, 267)
        Me.lbTotalLitros1.Name = "lbTotalLitros1"
        Me.lbTotalLitros1.Size = New System.Drawing.Size(96, 24)
        Me.lbTotalLitros1.TabIndex = 37
        Me.lbTotalLitros1.Text = "0.00"
        Me.lbTotalLitros1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalImporte1
        '
        Me.lbTotalImporte1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalImporte1.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalImporte1.ForeColor = System.Drawing.Color.Green
        Me.lbTotalImporte1.Location = New System.Drawing.Point(464, 267)
        Me.lbTotalImporte1.Name = "lbTotalImporte1"
        Me.lbTotalImporte1.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalImporte1.TabIndex = 38
        Me.lbTotalImporte1.Text = "$ 0.00"
        Me.lbTotalImporte1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel3.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label13})
        Me.Panel3.Location = New System.Drawing.Point(24, 308)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(768, 24)
        Me.Panel3.TabIndex = 39
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Orange
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(768, 24)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Notas blancas, boletines y otros cargos"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.Controls.AddRange(New System.Windows.Forms.Control() {Me.Label15, Me.Label16, Me.Label17, Me.Label18, Me.Label19, Me.Label24, Me.Label14, Me.Label20, Me.Label21, Me.Label22})
        Me.Panel4.Location = New System.Drawing.Point(24, 332)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(768, 24)
        Me.Panel4.TabIndex = 40
        '
        'Label15
        '
        Me.Label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label15.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label15.Location = New System.Drawing.Point(624, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 24)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Pago"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label16.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label16.Location = New System.Drawing.Point(544, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(80, 24)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "Tipo"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label17
        '
        Me.Label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label17.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label17.Location = New System.Drawing.Point(464, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(80, 24)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "Importe"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label18.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label18.Location = New System.Drawing.Point(408, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(56, 24)
        Me.Label18.TabIndex = 11
        Me.Label18.Text = "Precio"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label19.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label19.Location = New System.Drawing.Point(360, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(48, 24)
        Me.Label19.TabIndex = 10
        Me.Label19.Text = "Litros"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label24
        '
        Me.Label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label24.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label24.Location = New System.Drawing.Point(240, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(120, 24)
        Me.Label24.TabIndex = 9
        Me.Label24.Text = "Cliente"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Location = New System.Drawing.Point(240, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(528, 24)
        Me.Label14.TabIndex = 8
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label20.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label20.Location = New System.Drawing.Point(160, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(80, 24)
        Me.Label20.TabIndex = 2
        Me.Label20.Text = "Pedido"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label21
        '
        Me.Label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label21.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label21.Location = New System.Drawing.Point(80, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(80, 24)
        Me.Label21.TabIndex = 1
        Me.Label21.Text = "Contrato"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label22.Dock = System.Windows.Forms.DockStyle.Left
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(80, 24)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "Nota"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnNotas
        '
        Me.pnNotas.AutoScroll = True
        Me.pnNotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnNotas.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbNNombre0, Me.txtNLitros0, Me.lbNPedido0, Me.txtNCliente0, Me.txtNNota0, Me.lbNImporte0, Me.txtNPrecio0, Me.cmbNTipo0, Me.cmbNForma0})
        Me.pnNotas.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnNotas.Location = New System.Drawing.Point(24, 354)
        Me.pnNotas.Name = "pnNotas"
        Me.pnNotas.Size = New System.Drawing.Size(768, 186)
        Me.pnNotas.TabIndex = 41
        '
        'lbNNombre0
        '
        Me.lbNNombre0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbNNombre0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNNombre0.Location = New System.Drawing.Point(237, 3)
        Me.lbNNombre0.Name = "lbNNombre0"
        Me.lbNNombre0.Size = New System.Drawing.Size(123, 21)
        Me.lbNNombre0.TabIndex = 2
        Me.lbNNombre0.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNLitros0
        '
        Me.txtNLitros0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNLitros0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNLitros0.Location = New System.Drawing.Point(357, 3)
        Me.txtNLitros0.MaxLength = 8
        Me.txtNLitros0.Name = "txtNLitros0"
        Me.txtNLitros0.Size = New System.Drawing.Size(51, 21)
        Me.txtNLitros0.TabIndex = 3
        Me.txtNLitros0.Text = "0"
        Me.txtNLitros0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbNPedido0
        '
        Me.lbNPedido0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbNPedido0.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNPedido0.Location = New System.Drawing.Point(158, 3)
        Me.lbNPedido0.Name = "lbNPedido0"
        Me.lbNPedido0.Size = New System.Drawing.Size(80, 21)
        Me.lbNPedido0.TabIndex = 8
        Me.lbNPedido0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtNCliente0
        '
        Me.txtNCliente0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNCliente0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNCliente0.Location = New System.Drawing.Point(79, 3)
        Me.txtNCliente0.MaxLength = 9
        Me.txtNCliente0.Name = "txtNCliente0"
        Me.txtNCliente0.Size = New System.Drawing.Size(80, 21)
        Me.txtNCliente0.TabIndex = 1
        Me.txtNCliente0.Text = ""
        Me.txtNCliente0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtNNota0
        '
        Me.txtNNota0.AcceptsTab = True
        Me.txtNNota0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNNota0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNNota0.Location = New System.Drawing.Point(0, 3)
        Me.txtNNota0.Name = "txtNNota0"
        Me.txtNNota0.Size = New System.Drawing.Size(80, 21)
        Me.txtNNota0.TabIndex = 0
        Me.txtNNota0.Text = ""
        Me.txtNNota0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbNImporte0
        '
        Me.lbNImporte0.BackColor = System.Drawing.Color.DarkGreen
        Me.lbNImporte0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbNImporte0.ForeColor = System.Drawing.Color.White
        Me.lbNImporte0.Location = New System.Drawing.Point(463, 3)
        Me.lbNImporte0.Name = "lbNImporte0"
        Me.lbNImporte0.Size = New System.Drawing.Size(80, 21)
        Me.lbNImporte0.TabIndex = 5
        Me.lbNImporte0.Text = "$ 0.00"
        Me.lbNImporte0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNPrecio0
        '
        Me.txtNPrecio0.BackColor = System.Drawing.Color.PaleGoldenrod
        Me.txtNPrecio0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNPrecio0.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNPrecio0.Location = New System.Drawing.Point(407, 3)
        Me.txtNPrecio0.Name = "txtNPrecio0"
        Me.txtNPrecio0.Size = New System.Drawing.Size(57, 21)
        Me.txtNPrecio0.TabIndex = 4
        Me.txtNPrecio0.Text = "0.00"
        Me.txtNPrecio0.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbNTipo0
        '
        Me.cmbNTipo0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNTipo0.DropDownWidth = 120
        Me.cmbNTipo0.Location = New System.Drawing.Point(624, 3)
        Me.cmbNTipo0.Name = "cmbNTipo0"
        Me.cmbNTipo0.Size = New System.Drawing.Size(120, 21)
        Me.cmbNTipo0.TabIndex = 7
        '
        'cmbNForma0
        '
        Me.cmbNForma0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNForma0.DropDownWidth = 88
        Me.cmbNForma0.Items.AddRange(New Object() {"CONTADO", "CREDITO"})
        Me.cmbNForma0.Location = New System.Drawing.Point(543, 3)
        Me.cmbNForma0.Name = "cmbNForma0"
        Me.cmbNForma0.Size = New System.Drawing.Size(83, 21)
        Me.cmbNForma0.TabIndex = 6
        '
        'ContextMenu3
        '
        Me.ContextMenu3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem3})
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 0
        Me.MenuItem3.Text = "Detalle de pago de contado"
        '
        'lbLitrosNotas
        '
        Me.lbLitrosNotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLitrosNotas.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosNotas.Location = New System.Drawing.Point(338, 543)
        Me.lbLitrosNotas.Name = "lbLitrosNotas"
        Me.lbLitrosNotas.Size = New System.Drawing.Size(96, 24)
        Me.lbLitrosNotas.TabIndex = 42
        Me.lbLitrosNotas.Text = "0.00"
        Me.lbLitrosNotas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalNotas
        '
        Me.lbTotalNotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalNotas.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalNotas.ForeColor = System.Drawing.Color.Green
        Me.lbTotalNotas.Location = New System.Drawing.Point(465, 543)
        Me.lbTotalNotas.Name = "lbTotalNotas"
        Me.lbTotalNotas.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalNotas.TabIndex = 43
        Me.lbTotalNotas.Text = "$ 0.00"
        Me.lbTotalNotas.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(202, 576)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(136, 14)
        Me.Label26.TabIndex = 46
        Me.Label26.Text = "De contado y cheques :"
        '
        'lbLitrosContado
        '
        Me.lbLitrosContado.BackColor = System.Drawing.Color.White
        Me.lbLitrosContado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLitrosContado.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosContado.ForeColor = System.Drawing.Color.Blue
        Me.lbLitrosContado.Location = New System.Drawing.Point(338, 572)
        Me.lbLitrosContado.Name = "lbLitrosContado"
        Me.lbLitrosContado.Size = New System.Drawing.Size(96, 24)
        Me.lbLitrosContado.TabIndex = 47
        Me.lbLitrosContado.Text = "0.00"
        Me.lbLitrosContado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalContado
        '
        Me.lbTotalContado.BackColor = System.Drawing.Color.White
        Me.lbTotalContado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalContado.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalContado.ForeColor = System.Drawing.Color.Blue
        Me.lbTotalContado.Location = New System.Drawing.Point(464, 572)
        Me.lbTotalContado.Name = "lbTotalContado"
        Me.lbTotalContado.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalContado.TabIndex = 48
        Me.lbTotalContado.Text = "0.00"
        Me.lbTotalContado.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(267, 604)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(70, 14)
        Me.Label29.TabIndex = 49
        Me.Label29.Text = "De credito :"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lbLitrosCredito
        '
        Me.lbLitrosCredito.BackColor = System.Drawing.Color.White
        Me.lbLitrosCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbLitrosCredito.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosCredito.ForeColor = System.Drawing.Color.Maroon
        Me.lbLitrosCredito.Location = New System.Drawing.Point(338, 602)
        Me.lbLitrosCredito.Name = "lbLitrosCredito"
        Me.lbLitrosCredito.Size = New System.Drawing.Size(96, 24)
        Me.lbLitrosCredito.TabIndex = 50
        Me.lbLitrosCredito.Text = "0.00"
        Me.lbLitrosCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalCredito
        '
        Me.lbTotalCredito.BackColor = System.Drawing.Color.White
        Me.lbTotalCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalCredito.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalCredito.ForeColor = System.Drawing.Color.Maroon
        Me.lbTotalCredito.Location = New System.Drawing.Point(464, 602)
        Me.lbTotalCredito.Name = "lbTotalCredito"
        Me.lbTotalCredito.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalCredito.TabIndex = 51
        Me.lbTotalCredito.Text = "0.00"
        Me.lbTotalCredito.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalLitros
        '
        Me.lbTotalLitros.BackColor = System.Drawing.SystemColors.Control
        Me.lbTotalLitros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalLitros.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalLitros.ForeColor = System.Drawing.Color.Black
        Me.lbTotalLitros.Location = New System.Drawing.Point(337, 633)
        Me.lbTotalLitros.Name = "lbTotalLitros"
        Me.lbTotalLitros.Size = New System.Drawing.Size(96, 24)
        Me.lbTotalLitros.TabIndex = 52
        Me.lbTotalLitros.Text = "0.00"
        Me.lbTotalLitros.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbTotalImporte
        '
        Me.lbTotalImporte.BackColor = System.Drawing.SystemColors.Control
        Me.lbTotalImporte.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbTotalImporte.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalImporte.ForeColor = System.Drawing.Color.Black
        Me.lbTotalImporte.Location = New System.Drawing.Point(464, 633)
        Me.lbTotalImporte.Name = "lbTotalImporte"
        Me.lbTotalImporte.Size = New System.Drawing.Size(104, 24)
        Me.lbTotalImporte.TabIndex = 53
        Me.lbTotalImporte.Text = "0.00"
        Me.lbTotalImporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(297, 638)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(40, 14)
        Me.Label34.TabIndex = 54
        Me.Label34.Text = "Total :"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cmdPedido
        '
        Me.cmdPedido.CommandText = "SELECT N.Celula, N.Ruta, N.AñoNota, N.Nota, N.AñoPed, N.Pedido, N.Cliente, CONVER" & _
        "T(varChar(30), C.Nombre) AS Nombre FROM Nota N INNER JOIN Cliente C ON N.Cliente" & _
        " = C.Cliente WHERE (N.TipoNota = 1) AND (N.Status = 'PENDIENTE') AND (N.Ruta = @" & _
        "Ruta) ORDER BY C.Cliente"
        Me.cmdPedido.Connection = Me.SqlConnection
        Me.cmdPedido.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Ruta", System.Data.SqlDbType.SmallInt, 2, "Ruta"))
        '
        'daPedido
        '
        Me.daPedido.SelectCommand = Me.cmdPedido
        '
        'lstForma
        '
        Me.lstForma.BackColor = System.Drawing.SystemColors.Info
        Me.lstForma.Location = New System.Drawing.Point(800, 376)
        Me.lstForma.Name = "lstForma"
        Me.lstForma.Size = New System.Drawing.Size(64, 43)
        Me.lstForma.TabIndex = 55
        Me.lstForma.Visible = False
        '
        'lstPrecio
        '
        Me.lstPrecio.BackColor = System.Drawing.SystemColors.Info
        Me.lstPrecio.Location = New System.Drawing.Point(912, 472)
        Me.lstPrecio.Name = "lstPrecio"
        Me.lstPrecio.Size = New System.Drawing.Size(32, 43)
        Me.lstPrecio.TabIndex = 56
        Me.lstPrecio.Visible = False
        '
        'lstLitros
        '
        Me.lstLitros.BackColor = System.Drawing.SystemColors.Info
        Me.lstLitros.Location = New System.Drawing.Point(872, 376)
        Me.lstLitros.Name = "lstLitros"
        Me.lstLitros.Size = New System.Drawing.Size(88, 43)
        Me.lstLitros.TabIndex = 57
        Me.lstLitros.Visible = False
        '
        'lstClientes
        '
        Me.lstClientes.BackColor = System.Drawing.SystemColors.Info
        Me.lstClientes.Location = New System.Drawing.Point(896, 424)
        Me.lstClientes.Name = "lstClientes"
        Me.lstClientes.Size = New System.Drawing.Size(40, 43)
        Me.lstClientes.TabIndex = 58
        Me.lstClientes.Visible = False
        '
        'lstPedido
        '
        Me.lstPedido.BackColor = System.Drawing.SystemColors.Info
        Me.lstPedido.Location = New System.Drawing.Point(800, 424)
        Me.lstPedido.Name = "lstPedido"
        Me.lstPedido.Size = New System.Drawing.Size(88, 43)
        Me.lstPedido.TabIndex = 59
        Me.lstPedido.Visible = False
        '
        'dgPago
        '
        Me.dgPago.AllowSorting = False
        Me.dgPago.AlternatingBackColor = System.Drawing.Color.WhiteSmoke
        Me.dgPago.BackColor = System.Drawing.Color.Gainsboro
        Me.dgPago.BackgroundColor = System.Drawing.Color.White
        Me.dgPago.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.dgPago.CaptionBackColor = System.Drawing.Color.DarkKhaki
        Me.dgPago.CaptionFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgPago.CaptionForeColor = System.Drawing.Color.Black
        Me.dgPago.CaptionText = "Documentos relacionados"
        Me.dgPago.DataMember = ""
        Me.dgPago.DataSource = Me.DsLiquidacion.Detalle
        Me.dgPago.FlatMode = True
        Me.dgPago.Font = New System.Drawing.Font("Times New Roman", 9.0!)
        Me.dgPago.ForeColor = System.Drawing.Color.Black
        Me.dgPago.GridLineColor = System.Drawing.Color.Silver
        Me.dgPago.HeaderBackColor = System.Drawing.Color.Black
        Me.dgPago.HeaderFont = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.dgPago.HeaderForeColor = System.Drawing.Color.White
        Me.dgPago.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgPago.Location = New System.Drawing.Point(800, 232)
        Me.dgPago.Name = "dgPago"
        Me.dgPago.ParentRowsBackColor = System.Drawing.Color.LightGray
        Me.dgPago.ParentRowsForeColor = System.Drawing.Color.Black
        Me.dgPago.ReadOnly = True
        Me.dgPago.SelectionBackColor = System.Drawing.Color.Firebrick
        Me.dgPago.SelectionForeColor = System.Drawing.Color.White
        Me.dgPago.Size = New System.Drawing.Size(168, 304)
        Me.dgPago.TabIndex = 60
        Me.dgPago.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.dgctPago})
        '
        'dgctPago
        '
        Me.dgctPago.AllowSorting = False
        Me.dgctPago.AlternatingBackColor = System.Drawing.Color.WhiteSmoke
        Me.dgctPago.BackColor = System.Drawing.Color.Gainsboro
        Me.dgctPago.DataGrid = Me.dgPago
        Me.dgctPago.ForeColor = System.Drawing.Color.Black
        Me.dgctPago.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.DataGridTextBoxColumn1, Me.DataGridTextBoxColumn2})
        Me.dgctPago.GridLineColor = System.Drawing.Color.Silver
        Me.dgctPago.HeaderBackColor = System.Drawing.Color.Black
        Me.dgctPago.HeaderForeColor = System.Drawing.Color.White
        Me.dgctPago.LinkColor = System.Drawing.Color.DarkSlateBlue
        Me.dgctPago.MappingName = "Detalle"
        Me.dgctPago.ReadOnly = True
        Me.dgctPago.SelectionBackColor = System.Drawing.Color.Firebrick
        Me.dgctPago.SelectionForeColor = System.Drawing.Color.White
        '
        'DataGridTextBoxColumn1
        '
        Me.DataGridTextBoxColumn1.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.DataGridTextBoxColumn1.Format = ""
        Me.DataGridTextBoxColumn1.FormatInfo = Nothing
        Me.DataGridTextBoxColumn1.HeaderText = "Tipo"
        Me.DataGridTextBoxColumn1.MappingName = "DesTipo"
        Me.DataGridTextBoxColumn1.NullText = ""
        Me.DataGridTextBoxColumn1.Width = 35
        '
        'DataGridTextBoxColumn2
        '
        Me.DataGridTextBoxColumn2.Alignment = System.Windows.Forms.HorizontalAlignment.Right
        Me.DataGridTextBoxColumn2.Format = "$ #.00"
        Me.DataGridTextBoxColumn2.FormatInfo = Nothing
        Me.DataGridTextBoxColumn2.HeaderText = "Monto"
        Me.DataGridTextBoxColumn2.MappingName = "Monto"
        Me.DataGridTextBoxColumn2.NullText = ""
        Me.DataGridTextBoxColumn2.Width = 65
        '
        'lstTipoPago
        '
        Me.lstTipoPago.BackColor = System.Drawing.SystemColors.Info
        Me.lstTipoPago.Location = New System.Drawing.Point(800, 472)
        Me.lstTipoPago.Name = "lstTipoPago"
        Me.lstTipoPago.Size = New System.Drawing.Size(104, 43)
        Me.lstTipoPago.TabIndex = 61
        Me.lstTipoPago.Visible = False
        '
        'lstTipoPago2
        '
        Me.lstTipoPago2.BackColor = System.Drawing.SystemColors.Info
        Me.lstTipoPago2.Location = New System.Drawing.Point(104, 603)
        Me.lstTipoPago2.Name = "lstTipoPago2"
        Me.lstTipoPago2.Size = New System.Drawing.Size(104, 43)
        Me.lstTipoPago2.TabIndex = 67
        Me.lstTipoPago2.Visible = False
        '
        'lstPedido2
        '
        Me.lstPedido2.BackColor = System.Drawing.SystemColors.Info
        Me.lstPedido2.Location = New System.Drawing.Point(8, 552)
        Me.lstPedido2.Name = "lstPedido2"
        Me.lstPedido2.Size = New System.Drawing.Size(88, 43)
        Me.lstPedido2.TabIndex = 66
        Me.lstPedido2.Visible = False
        '
        'lstClientes2
        '
        Me.lstClientes2.BackColor = System.Drawing.SystemColors.Info
        Me.lstClientes2.Location = New System.Drawing.Point(104, 552)
        Me.lstClientes2.Name = "lstClientes2"
        Me.lstClientes2.Size = New System.Drawing.Size(80, 43)
        Me.lstClientes2.TabIndex = 65
        Me.lstClientes2.Visible = False
        '
        'lstLitros2
        '
        Me.lstLitros2.BackColor = System.Drawing.SystemColors.Info
        Me.lstLitros2.Location = New System.Drawing.Point(8, 603)
        Me.lstLitros2.Name = "lstLitros2"
        Me.lstLitros2.Size = New System.Drawing.Size(88, 43)
        Me.lstLitros2.TabIndex = 64
        Me.lstLitros2.Visible = False
        '
        'lstPrecio2
        '
        Me.lstPrecio2.BackColor = System.Drawing.SystemColors.Info
        Me.lstPrecio2.Location = New System.Drawing.Point(264, 552)
        Me.lstPrecio2.Name = "lstPrecio2"
        Me.lstPrecio2.Size = New System.Drawing.Size(56, 43)
        Me.lstPrecio2.TabIndex = 63
        Me.lstPrecio2.Visible = False
        '
        'lstForma2
        '
        Me.lstForma2.BackColor = System.Drawing.SystemColors.Info
        Me.lstForma2.Location = New System.Drawing.Point(192, 552)
        Me.lstForma2.Name = "lstForma2"
        Me.lstForma2.Size = New System.Drawing.Size(64, 43)
        Me.lstForma2.TabIndex = 62
        Me.lstForma2.Visible = False
        '
        'lstNotas
        '
        Me.lstNotas.BackColor = System.Drawing.SystemColors.Info
        Me.lstNotas.Location = New System.Drawing.Point(210, 604)
        Me.lstNotas.Name = "lstNotas"
        Me.lstNotas.Size = New System.Drawing.Size(88, 43)
        Me.lstNotas.TabIndex = 68
        Me.lstNotas.Visible = False
        '
        'ContextMenu2
        '
        Me.ContextMenu2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem2})
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 0
        Me.MenuItem2.Text = "Busqueda de un contrato"
        '
        'lbTotalNotaRemision
        '
        Me.lbTotalNotaRemision.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalNotaRemision.ForeColor = System.Drawing.Color.Black
        Me.lbTotalNotaRemision.Location = New System.Drawing.Point(264, 53)
        Me.lbTotalNotaRemision.Name = "lbTotalNotaRemision"
        Me.lbTotalNotaRemision.Size = New System.Drawing.Size(88, 11)
        Me.lbTotalNotaRemision.TabIndex = 70
        Me.lbTotalNotaRemision.Text = "0"
        Me.lbTotalNotaRemision.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTotalRemisionSurtida
        '
        Me.lbTotalRemisionSurtida.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalRemisionSurtida.ForeColor = System.Drawing.Color.Green
        Me.lbTotalRemisionSurtida.Location = New System.Drawing.Point(264, 5)
        Me.lbTotalRemisionSurtida.Name = "lbTotalRemisionSurtida"
        Me.lbTotalRemisionSurtida.Size = New System.Drawing.Size(88, 16)
        Me.lbTotalRemisionSurtida.TabIndex = 71
        Me.lbTotalRemisionSurtida.Text = "0"
        Me.lbTotalRemisionSurtida.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbTotalRemisionNo
        '
        Me.lbTotalRemisionNo.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalRemisionNo.ForeColor = System.Drawing.Color.Red
        Me.lbTotalRemisionNo.Location = New System.Drawing.Point(264, 23)
        Me.lbTotalRemisionNo.Name = "lbTotalRemisionNo"
        Me.lbTotalRemisionNo.Size = New System.Drawing.Size(88, 16)
        Me.lbTotalRemisionNo.TabIndex = 72
        Me.lbTotalRemisionNo.Text = "0"
        Me.lbTotalRemisionNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lbNotasBlancas
        '
        Me.lbNotasBlancas.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNotasBlancas.ForeColor = System.Drawing.Color.Black
        Me.lbNotasBlancas.Location = New System.Drawing.Point(264, 68)
        Me.lbNotasBlancas.Name = "lbNotasBlancas"
        Me.lbNotasBlancas.Size = New System.Drawing.Size(88, 16)
        Me.lbNotasBlancas.TabIndex = 73
        Me.lbNotasBlancas.Text = "0"
        Me.lbNotasBlancas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnTotales
        '
        Me.pnTotales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnTotales.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbTotal, Me.Label31, Me.Label30, Me.Label28, Me.lbTotalNotaRemision, Me.Label27, Me.Label25, Me.Label23, Me.lbNotasBlancas, Me.lbTotalRemisionNo, Me.lbTotalRemisionSurtida, Me.Label33})
        Me.pnTotales.Location = New System.Drawing.Point(592, 543)
        Me.pnTotales.Name = "pnTotales"
        Me.pnTotales.Size = New System.Drawing.Size(368, 112)
        Me.pnTotales.TabIndex = 74
        '
        'lbTotal
        '
        Me.lbTotal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotal.ForeColor = System.Drawing.Color.Indigo
        Me.lbTotal.Location = New System.Drawing.Point(264, 90)
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(88, 16)
        Me.lbTotal.TabIndex = 80
        Me.lbTotal.Text = "0"
        Me.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Indigo
        Me.Label31.Location = New System.Drawing.Point(97, 90)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(168, 16)
        Me.Label31.TabIndex = 79
        Me.Label31.Text = "Total de notas a liquidar :"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label30
        '
        Me.Label30.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(256, 36)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(96, 16)
        Me.Label30.TabIndex = 78
        Me.Label30.Text = "----------------------"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label28
        '
        Me.Label28.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.ForeColor = System.Drawing.Color.Black
        Me.Label28.Location = New System.Drawing.Point(109, 67)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(152, 16)
        Me.Label28.TabIndex = 77
        Me.Label28.Text = "Total de notas blancas :"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label27
        '
        Me.Label27.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.Red
        Me.Label27.Location = New System.Drawing.Point(8, 22)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(256, 16)
        Me.Label27.TabIndex = 76
        Me.Label27.Text = "Total de notas de remisión NO Surtidas : "
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.Green
        Me.Label25.Location = New System.Drawing.Point(29, 4)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(237, 16)
        Me.Label25.TabIndex = 75
        Me.Label25.Text = "Total de notas de remisión Surtidas : "
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(84, 50)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(184, 16)
        Me.Label23.TabIndex = 74
        Me.Label23.Text = "Total de notas de remisión : "
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.Location = New System.Drawing.Point(256, 80)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(96, 16)
        Me.Label33.TabIndex = 81
        Me.Label33.Text = "----------------------"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnDocumento
        '
        Me.btnDocumento.Location = New System.Drawing.Point(864, 100)
        Me.btnDocumento.Name = "btnDocumento"
        Me.btnDocumento.Size = New System.Drawing.Size(88, 23)
        Me.btnDocumento.TabIndex = 2
        Me.btnDocumento.Text = "Documentos"
        '
        'lstNotasBlancas
        '
        Me.lstNotasBlancas.BackColor = System.Drawing.Color.LightBlue
        Me.lstNotasBlancas.Location = New System.Drawing.Point(808, 256)
        Me.lstNotasBlancas.Name = "lstNotasBlancas"
        Me.lstNotasBlancas.Size = New System.Drawing.Size(104, 95)
        Me.lstNotasBlancas.TabIndex = 75
        Me.lstNotasBlancas.Visible = False
        '
        'txtFolio
        '
        Me.txtFolio.BackColor = System.Drawing.Color.White
        Me.txtFolio.Location = New System.Drawing.Point(24, 280)
        Me.txtFolio.Name = "txtFolio"
        Me.txtFolio.TabIndex = 76
        Me.txtFolio.Text = ""
        Me.txtFolio.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(128, 280)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 23)
        Me.Button1.TabIndex = 77
        Me.Button1.Text = "Generar folios"
        Me.Button1.Visible = False
        '
        'btnBuscar
        '
        Me.btnBuscar.ContextMenu = Me.ContextMenu4
        Me.btnBuscar.Location = New System.Drawing.Point(864, 168)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(88, 23)
        Me.btnBuscar.TabIndex = 78
        Me.btnBuscar.Text = "Buscar litros"
        '
        'ContextMenu4
        '
        Me.ContextMenu4.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem4})
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 0
        Me.MenuItem4.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.MenuItem4.Text = "Buscar"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem5})
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 0
        Me.MenuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem6, Me.MenuItem8})
        Me.MenuItem5.Text = "Liquidación"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 0
        Me.MenuItem6.Shortcut = System.Windows.Forms.Shortcut.F1
        Me.MenuItem6.Text = "Buscar Litros"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 1
        Me.MenuItem8.Shortcut = System.Windows.Forms.Shortcut.F2
        Me.MenuItem8.Text = "Buscar cliente"
        '
        'lbUnificacion
        '
        Me.lbUnificacion.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbUnificacion.ForeColor = System.Drawing.Color.Red
        Me.lbUnificacion.Location = New System.Drawing.Point(512, 2)
        Me.lbUnificacion.Name = "lbUnificacion"
        Me.lbUnificacion.Size = New System.Drawing.Size(280, 16)
        Me.lbUnificacion.TabIndex = 79
        Me.lbUnificacion.Text = "VARIAS REMISIONES EN UN MISMO CONTRATO"
        Me.lbUnificacion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbUnificacion.Visible = False
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.ContextMenu = Me.ContextMenu4
        Me.btnBuscarCliente.Location = New System.Drawing.Point(864, 200)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(88, 23)
        Me.btnBuscarCliente.TabIndex = 80
        Me.btnBuscarCliente.Text = "Buscar cliente"
        '
        'ListBox8
        '
        Me.ListBox8.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox8.Location = New System.Drawing.Point(16, 608)
        Me.ListBox8.Name = "ListBox8"
        Me.ListBox8.Size = New System.Drawing.Size(88, 43)
        Me.ListBox8.TabIndex = 64
        Me.ListBox8.Visible = False
        '
        'ListBox3
        '
        Me.ListBox3.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox3.Location = New System.Drawing.Point(24, 603)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(64, 43)
        Me.ListBox3.TabIndex = 64
        Me.ListBox3.Visible = False
        '
        'ListBox5
        '
        Me.ListBox5.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox5.Location = New System.Drawing.Point(272, 552)
        Me.ListBox5.Name = "ListBox5"
        Me.ListBox5.Size = New System.Drawing.Size(56, 43)
        Me.ListBox5.TabIndex = 63
        Me.ListBox5.Visible = False
        '
        'ListBox4
        '
        Me.ListBox4.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox4.Location = New System.Drawing.Point(200, 552)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.Size = New System.Drawing.Size(64, 43)
        Me.ListBox4.TabIndex = 62
        Me.ListBox4.Visible = False
        '
        'ListBox6
        '
        Me.ListBox6.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox6.Location = New System.Drawing.Point(120, 552)
        Me.ListBox6.Name = "ListBox6"
        Me.ListBox6.Size = New System.Drawing.Size(80, 43)
        Me.ListBox6.TabIndex = 65
        Me.ListBox6.Visible = False
        '
        'ListBox1
        '
        Me.ListBox1.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox1.Location = New System.Drawing.Point(112, 552)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(80, 43)
        Me.ListBox1.TabIndex = 65
        Me.ListBox1.Visible = False
        '
        'ListBox7
        '
        Me.ListBox7.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox7.Location = New System.Drawing.Point(24, 552)
        Me.ListBox7.Name = "ListBox7"
        Me.ListBox7.Size = New System.Drawing.Size(88, 43)
        Me.ListBox7.TabIndex = 66
        Me.ListBox7.Visible = False
        '
        'ListBox2
        '
        Me.ListBox2.BackColor = System.Drawing.SystemColors.Info
        Me.ListBox2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListBox2.Location = New System.Drawing.Point(8, 552)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(88, 43)
        Me.ListBox2.TabIndex = 66
        Me.ListBox2.Visible = False
        '
        'Liquidacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(984, 670)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnBuscarCliente, Me.dgPago, Me.lbUnificacion, Me.btnBuscar, Me.Button1, Me.txtFolio, Me.lstNotasBlancas, Me.btnDocumento, Me.pnTotales, Me.lstNotas, Me.lstTipoPago2, Me.lstPedido2, Me.lstClientes2, Me.lstLitros2, Me.lstPrecio2, Me.lstForma2, Me.lstTipoPago, Me.btnImprimir, Me.btnCancelar, Me.btnAceptar, Me.lstPedido, Me.lstClientes, Me.lstLitros, Me.lstPrecio, Me.lstForma, Me.Label34, Me.Label29, Me.Label26, Me.lbAutotanque, Me.lbOperador, Me.Label2, Me.Label1, Me.lbTotalImporte, Me.lbTotalLitros, Me.lbTotalCredito, Me.lbLitrosCredito, Me.lbTotalContado, Me.lbLitrosContado, Me.lbTotalNotas, Me.lbLitrosNotas, Me.pnNotas, Me.Panel4, Me.Panel3, Me.lbTotalImporte1, Me.lbTotalLitros1, Me.lbFecha, Me.Panel2, Me.pnPedidos, Me.Panel1, Me.ListBox3, Me.ListBox1, Me.ListBox2, Me.ListBox4, Me.ListBox5, Me.ListBox6, Me.ListBox7, Me.ListBox8})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "Liquidacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidaciones"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DsLiquidacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.pnPedidos.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.pnNotas.ResumeLayout(False)
        CType(Me.dgPago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnTotales.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub


    Private Sub MenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem1.Click
        'Dim Importe As Decimal
        'Dim i As Integer
        'Importe = CType(lstLitros.Items(_Linea), Decimal) * CType(lstPrecio.Items(_Linea), Decimal)

        'Dim frmRelacion As Relacion = New Relacion()

        'frmRelacion.DsLiquidacion.Documento.Clear()
        'For i = 0 To DsLiquidacion.Documento.Count - 1
        '    frmRelacion.DsLiquidacion.Documento.AddDocumentoRow(DsLiquidacion.Documento(i).Banco, DsLiquidacion.Documento(i).Cheque, _
        '                                DsLiquidacion.Documento(i).FCheque, DsLiquidacion.Documento(i).Cuenta, _
        '                                DsLiquidacion.Documento(i).Monto, DsLiquidacion.Documento(i).Disponible, _
        '                                DsLiquidacion.Documento(i).DesBanco, DsLiquidacion.Documento(i).Llave, _
        '                                DsLiquidacion.Documento(i).Tipo, DsLiquidacion.Documento(i).TipoDes, DsLiquidacion.Documento(i).Cliente, DsLiquidacion.Documento(i).Nombre)
        'Next

        'frmRelacion.DsLiquidacion.Detalle.Clear()
        'For i = 0 To DsLiquidacion.Detalle.Count - 1
        '    frmRelacion.DsLiquidacion.Detalle.AddDetalleRow(DsLiquidacion.Detalle(i).Cliente, DsLiquidacion.Detalle(i).Monto, _
        '                                                    DsLiquidacion.Detalle(i).Tipo, DsLiquidacion.Detalle(i).DesTipo, _
        '                                                    DsLiquidacion.Detalle(i).Banco, DsLiquidacion.Detalle(i).Cheque, _
        '                                                    DsLiquidacion.Detalle(i).Cuenta, DsLiquidacion.Detalle(i).NombreBanco)
        'Next


        'frmRelacion.Entrada(lstClientes.Items(_Linea), Importe)

        'DsLiquidacion.Documento.Clear()
        'For i = 0 To frmRelacion.DsLiquidacion.Documento.Count - 1
        '    DsLiquidacion.Documento.AddDocumentoRow(frmRelacion.DsLiquidacion.Documento(i).Banco, frmRelacion.DsLiquidacion.Documento(i).Cheque, _
        '                                            frmRelacion.DsLiquidacion.Documento(i).FCheque, frmRelacion.DsLiquidacion.Documento(i).Cuenta, _
        '                                            frmRelacion.DsLiquidacion.Documento(i).Monto, frmRelacion.DsLiquidacion.Documento(i).Disponible, _
        '                                            frmRelacion.DsLiquidacion.Documento(i).DesBanco, frmRelacion.DsLiquidacion.Documento(i).Llave, _
        '                                            frmRelacion.DsLiquidacion.Documento(i).Tipo, frmRelacion.DsLiquidacion.Documento(i).TipoDes, frmRelacion.DsLiquidacion.Documento(i).Cliente, frmRelacion.DsLiquidacion.Documento(i).Nombre)
        'Next

        'DsLiquidacion.Detalle.Clear()
        'For i = 0 To frmRelacion.DsLiquidacion.Detalle.Count - 1
        '    DsLiquidacion.Detalle.AddDetalleRow(frmRelacion.DsLiquidacion.Detalle(i).Cliente, frmRelacion.DsLiquidacion.Detalle(i).Monto, _
        '                                        frmRelacion.DsLiquidacion.Detalle(i).Tipo, frmRelacion.DsLiquidacion.Detalle(i).DesTipo, _
        '                                        frmRelacion.DsLiquidacion.Detalle(i).Banco, frmRelacion.DsLiquidacion.Detalle(i).Cheque, _
        '                                        frmRelacion.DsLiquidacion.Detalle(i).Cuenta, frmRelacion.DsLiquidacion.Detalle(i).NombreBanco)
        'Next

        'frmRelacion.Dispose()

    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Dim Transaccion As SqlClient.SqlTransaction = Nothing
        Dim cmdInsert As New SqlClient.SqlCommand()
        'Dim rdrInsert As SqlClient.SqlDataReader
        Dim cmdInsertCliente As New SqlClient.SqlCommand()
        Dim SiGrabo As Boolean
        Dim i As Integer
        Dim j As Integer
        Dim num As Decimal
        Dim FosaComun As Decimal = Nothing
        Dim Cobro As Integer = Nothing
        Dim Diferencia As Decimal = Nothing
        Dim Efectivo As Decimal = Nothing
        Dim Impuesto As Decimal = Nothing
        Dim PedidoNota As Integer = Nothing
        Dim TipoLiquidacion As String
        Dim ex As System.EventArgs = Nothing
        Dim cn As SqlClient.SqlConnection = Nothing

        Try
            cn = New SqlClient.SqlConnection(GLOBAL_ConString)
            cn.Open()
        Catch ioEx As Exception
            MsgBox(ioEx.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try
        SelectedIndexChanged2(cmbNTipo0, ex)

        'If Disponibles() = True Then
        '    cn.Close()
        '    MsgBox("De los cheques dados de alta, existe saldo por aplicar. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
        '    Exit Sub
        'End If

        'verificar excepciones
        Try
            num = CType(lbTotalLitros.Text, Integer)
        Catch exNum As Exception
            lbTotalLitros.Text = "0"
        End Try

        If _Totalizador <> CType(lbTotalLitros.Text, Integer) Then
            If MsgBox("El litraje a liquidar es DIFERENTE al TOTALIZADOR de la bascula." + Chr(13) + "El litraje a liquidar es de : " + CType(CType(lbTotalLitros.Text, Integer), String) + " litros." + Chr(13) + "El litraje del totalizador de la bascula es : " + CType(_Totalizador, String) + " litros." + Chr(13) + Chr(13) + "Aun asi ¿Desea realizar la liquidación?.", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") <> MsgBoxResult.Yes Then
                cn.Close()
                Exit Sub
            End If
        End If


        If ValidarPrecios() = True Then
            cn.Close()
            MsgBox("Existe capturado un precio en el sistema que no es valido. No es el precio vigente o el precio anterior vigente." + Chr(13) + Chr(13) + "Los Precios Vigentes son: " + Chr(13) + "Precio : " + CType(GLOBAL_Precio, String) + Chr(13) + "Precio toluca : " + CType(GLOBAL_PrecioToluca, String), MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        If ValidarNumeroNota() = True Then
            cn.Close()
            MsgBox("Existe un registro de nota blanca con litraje mayor a cero que no se le ha capturado su numero de nota blanca. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        If ValidarNumeroCliente() = True Then
            cn.Close()
            MsgBox("Existe un registro de nota blanca con litraje mayor a cero que no se le ha capturado su contrato. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        If ValidarNumeroLitros() = True Then
            cn.Close()
            MsgBox("Existe un registro de nota blanca con litraje igual a cero. Verifique", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            Exit Sub
        End If

        If MsgBox("¿Esta apunto de guardar la liquidación?" + Chr(13) + "Importe Contado : " + lbTotalContado.Text + Chr(13) + "Importe credito: " + lbTotalCredito.Text, MsgBoxStyle.YesNoCancel, "Mensaje del sistema") <> MsgBoxResult.Yes Then
            cn.Close()
            Exit Sub
        End If

        If TotalImporte = 0 Then
            MsgBox("No hay importes a liquidar. Verifique.", MsgBoxStyle.Information, "Mensaje del sistema")
            cn.Close()
            Exit Sub
        End If


        Application.DoEvents()

        SiGrabo = False

        Try
            Transaccion = cn.BeginTransaction
            cmdInsert.Connection = cn
            cmdInsert.CommandTimeout = 300
            cmdInsert.Transaction = Transaccion
            cmdInsert.CommandType = CommandType.StoredProcedure
            Me.Cursor = System.Windows.Forms.Cursors.WaitCursor

            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "spCCBorraLiquidacion"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
            cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
            cmdInsert.ExecuteNonQuery()

            'ESTO YA SE QUITO POR QUE SE METIO EN EL PROCEDIMIENTO DE ARRIBA

            'cmdInsert.CommandType = CommandType.Text
            'cmdInsert.CommandText = "Delete from LiquidacionCheques where AñoAtt=@AñoAtt and Folio=@Folio"
            'cmdInsert.Parameters.Clear()
            'cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
            'cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
            'cmdInsert.ExecuteNonQuery()


            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            If DsLiquidacion.Detalle.DefaultView.Count > 0 Then
                For j = 0 To DsLiquidacion.Detalle.DefaultView.Count - 1
                    DsLiquidacion.Documento.DefaultView.RowFilter = ""
                    DsLiquidacion.Documento.DefaultView.RowFilter = " Banco = " + CType(CType(DsLiquidacion.Detalle.DefaultView(j).Item(4), Integer), String) + " and Cuenta='" + CType(DsLiquidacion.Detalle.DefaultView(j).Item(6), String) + "' and Cheque='" + CType(DsLiquidacion.Detalle.DefaultView(j).Item(5), String) + "' "

                    cmdInsert.CommandType = CommandType.Text
                    cmdInsert.CommandText = "Insert into Liquidacioncheques (AñoAtt, Folio, Cliente, Banco, Cheque, FCheque, Cuenta, MontoTotal, Disponible, TipoCobro, ClientePadre, MontoHijo) Values (@AñoAtt, @Folio, @Cliente, @Banco, @Cheque, @FCheque, @Cuenta, @MontoTotal, @Disponible, @TipoCobro, @ClientePadre, @MontoHijo) "
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                    cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(DsLiquidacion.Detalle.DefaultView(j).Item(0), Integer)
                    cmdInsert.Parameters.Add("@Banco", SqlDbType.Int).Value = CType(DsLiquidacion.Detalle.DefaultView(j).Item(4), Integer)
                    cmdInsert.Parameters.Add("@Cheque", SqlDbType.Char).Value = CType(DsLiquidacion.Detalle.DefaultView(j).Item(5), String)
                    cmdInsert.Parameters.Add("@FCheque", SqlDbType.DateTime).Value = CType(DsLiquidacion.Documento.DefaultView(0).Item(2), DateTime)
                    cmdInsert.Parameters.Add("@Cuenta", SqlDbType.Char).Value = CType(DsLiquidacion.Detalle.DefaultView(j).Item(6), String)
                    cmdInsert.Parameters.Add("@MontoTotal", SqlDbType.Decimal).Value = CType(DsLiquidacion.Documento.DefaultView(0).Item(4), Decimal)
                    cmdInsert.Parameters.Add("@Disponible", SqlDbType.Decimal).Value = CType(DsLiquidacion.Documento.DefaultView(0).Item(5), Decimal)
                    If CType(DsLiquidacion.Documento.DefaultView(0).Item(8), Integer) = 0 Then
                        cmdInsert.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = 3
                    Else
                        cmdInsert.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = 7
                    End If
                    cmdInsert.Parameters.Add("@ClientePadre", SqlDbType.Int).Value = CType(DsLiquidacion.Documento.DefaultView(0).Item(10), Integer)
                    cmdInsert.Parameters.Add("@MontoHijo", SqlDbType.Decimal).Value = CType(DsLiquidacion.Detalle.DefaultView(j).Item(1), Decimal)
                    cmdInsert.ExecuteNonQuery()
                Next
            End If

            For i = 0 To lstPedido.Items.Count - 1
                If CType(lstLitros.Items(i), Integer) > 0 Then
                    cmdInsert.CommandType = CommandType.StoredProcedure
                    cmdInsert.CommandText = "sp_LiquidacionMaestra"
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                    cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
                    cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = CType(lstPedido.Items(i), Integer)
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lstClientes.Items(i), Integer)
                    cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                    cmdInsert.Parameters.Add("@Codigo", SqlDbType.Char).Value = ""
                    cmdInsert.Parameters.Add("@Litros", SqlDbType.Int).Value = CType(lstLitros.Items(i), Integer)
                    cmdInsert.Parameters.Add("@Precio", SqlDbType.Decimal).Value = CType(lstPrecio.Items(i), Decimal)
                    cmdInsert.Parameters.Add("@Importe", SqlDbType.Decimal).Value = (CType(lstPrecio.Items(i), Decimal) * CType(lstLitros.Items(i), Decimal))
                    If CType(lstForma.Items(i), String) = "CONTADO" Then
                        cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 5
                    Else
                        If CType(lstTipoPago.Items(i), String) = "Tarjeta credito" Then
                            cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 6
                        Else
                            If CType(lstTipoPago.Items(i), String) = "Credito GM" Then
                                cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 8
                            Else
                                cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 9
                            End If
                        End If

                    End If
                    cmdInsert.Parameters.Add("@Iva", SqlDbType.Decimal).Value = ((CType(lstPrecio.Items(i), Decimal) * CType(lstLitros.Items(i), Decimal)) * 0.15)
                    cmdInsert.Parameters.Add("@Autotanque", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Autotanque
                    cmdInsert.Parameters.Add("@Lista", SqlDbType.Int).Value = 0


                    cmdInsert.Parameters.Add("@Banco", SqlDbType.Int).Value = 0
                    cmdInsert.Parameters.Add("@Cheque", SqlDbType.Char).Value = ""
                    cmdInsert.Parameters.Add("@FCheque", SqlDbType.DateTime).Value = Now.Date
                    cmdInsert.Parameters.Add("@Cuenta", SqlDbType.Char).Value = ""
                    cmdInsert.Parameters.Add("@MontoTotal", SqlDbType.Decimal).Value = 0
                    cmdInsert.Parameters.Add("@Disponible", SqlDbType.Decimal).Value = 0
                    cmdInsert.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = 0
                    cmdInsert.Parameters.Add("@ClientePadre", SqlDbType.Int).Value = 0
                    cmdInsert.Parameters.Add("@MontoHijo", SqlDbType.Decimal).Value = 0

                    cmdInsert.ExecuteNonQuery()
                End If
            Next

            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Documento.DefaultView.RowFilter = ""

            For i = 0 To lstNotas.Items.Count - 1
                If RTrim(CType(lstNotas.Items(i), String)) <> "" Then
                    If CType(lstLitros2.Items(i), Integer) > 0 Then
                        cmdInsert.CommandType = CommandType.StoredProcedure
                        cmdInsert.CommandText = "sp_LiquidacionMaestra"
                        cmdInsert.Parameters.Clear()
                        cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
                        cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
                        cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = 0
                        cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = CType(lstClientes2.Items(i), Integer)
                        cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                        cmdInsert.Parameters.Add("@Codigo", SqlDbType.Char).Value = CType(lstNotas.Items(i), String)
                        cmdInsert.Parameters.Add("@Litros", SqlDbType.Int).Value = CType(lstLitros2.Items(i), Integer)
                        cmdInsert.Parameters.Add("@Precio", SqlDbType.Decimal).Value = CType(lstPrecio2.Items(i), Decimal)
                        cmdInsert.Parameters.Add("@Importe", SqlDbType.Decimal).Value = (CType(lstPrecio2.Items(i), Decimal) * CType(lstLitros2.Items(i), Decimal))
                        If CType(lstForma2.Items(i), String) = "CONTADO" Then
                            cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 5
                        Else
                            If CType(lstTipoPago2.Items(i), String) = "Tarjeta credito" Then
                                cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 6
                            Else
                                If CType(lstTipoPago2.Items(i), String) = "Credito GM" Then
                                    cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 8
                                Else
                                    cmdInsert.Parameters.Add("@Tipo", SqlDbType.Int).Value = 9
                                End If
                            End If

                        End If
                        cmdInsert.Parameters.Add("@Iva", SqlDbType.Decimal).Value = ((CType(lstPrecio2.Items(i), Decimal) * CType(lstLitros2.Items(i), Decimal)) * 0.15)
                        cmdInsert.Parameters.Add("@Autotanque", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Autotanque
                        cmdInsert.Parameters.Add("@Lista", SqlDbType.Int).Value = 1

                        cmdInsert.Parameters.Add("@Banco", SqlDbType.Int).Value = 0
                        cmdInsert.Parameters.Add("@Cheque", SqlDbType.Char).Value = ""
                        cmdInsert.Parameters.Add("@FCheque", SqlDbType.DateTime).Value = Now.Date
                        cmdInsert.Parameters.Add("@Cuenta", SqlDbType.Char).Value = ""
                        cmdInsert.Parameters.Add("@MontoTotal", SqlDbType.Decimal).Value = 0
                        cmdInsert.Parameters.Add("@Disponible", SqlDbType.Decimal).Value = 0
                        cmdInsert.Parameters.Add("@TipoCobro", SqlDbType.Int).Value = 0
                        cmdInsert.Parameters.Add("@ClientePadre", SqlDbType.Int).Value = 0
                        cmdInsert.Parameters.Add("@MontoHijo", SqlDbType.Decimal).Value = 0

                        cmdInsert.ExecuteNonQuery()
                    End If
                    
                End If
            Next

            DsLiquidacion.Detalle.DefaultView.RowFilter = ""
            DsLiquidacion.Documento.DefaultView.RowFilter = ""

            Application.DoEvents()

            If _CorrerDescarga = True Then
                TipoLiquidacion = "AUTOMATICA"
            Else
                TipoLiquidacion = "MANUAL"
            End If

            Dim totalCreditoA As Decimal = CType(lbTotalCredito.Text, Decimal)
            Dim totalContadoA As Decimal = CType(lbTotalContado.Text, Decimal)

            cmdInsert.CommandType = CommandType.StoredProcedure
            cmdInsert.CommandText = "sp_LiquidacionMaestraGeneral"
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = DsLiquidacion.Operador(0).AñoAtt
            cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = DsLiquidacion.Operador(0).Folio
            cmdInsert.Parameters.Add("@ClienteGeneral", SqlDbType.Int).Value = _ClienteGlobal
            cmdInsert.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = Now.Date
            cmdInsert.Parameters.Add("@ImporteCredito", SqlDbType.Money).Value = totalCreditoA
            cmdInsert.Parameters.Add("@ImporteContado", SqlDbType.Money).Value = totalContadoA
            cmdInsert.Parameters.Add("@LitrosCredito", SqlDbType.Int).Value = CType(lbLitrosCredito.Text, Integer)
            cmdInsert.Parameters.Add("@LitrosContado", SqlDbType.Int).Value = CType(lbLitrosContado.Text, Integer)
            cmdInsert.Parameters.Add("@TipoLiquidacion", SqlDbType.Char).Value = TipoLiquidacion
            cmdInsert.ExecuteNonQuery()

            'Application.DoEvents()

            Transaccion.Commit()
            SiGrabo = True
            Me.Cursor = Cursors.Default
            'Application.DoEvents 
        Catch et As Data.SqlClient.SqlException
            Transaccion.Rollback()
            'rdrInsert.Close()
            cn.Close()

            SiGrabo = False

            MessageBox.Show(et.Message, et.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

            Me.Cursor = System.Windows.Forms.Cursors.Default
        Finally
            If SiGrabo = True Then

                btnAceptar.Enabled = False
                btnDocumento.Enabled = False
                'Application.DoEvents()
                MsgBox("El proceso de liquidación ya terminó. Gracias.", MsgBoxStyle.Information, "Mensaje del sistema")

                If MessageBox.Show("¿Desea imprimir los totales del reporte de liquidación?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                    Cursor = Cursors.WaitCursor
                    Dim oReporte As New frmConsultaReporte(_AñoAtt, _Folio)
                    oReporte.ShowDialog()
                    Cursor = Cursors.Default
                End If
                cn.Close()
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Try

        cn.Dispose()
        'Application.DoEvents()

    End Sub

    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        Dim Contrato As Integer
        Dim Paneles As System.Windows.Forms.Control

        Dim frmBuscar As New SigaMetClasses.BusquedaCliente()
        If frmBuscar.ShowDialog() = DialogResult.OK Then
            Contrato = frmBuscar.Cliente

            If Contrato > 0 Then
                For Each Paneles In pnNotas.Controls
                    If CType(Paneles.Tag, Integer) = _Linea2 Then
                        If Paneles.Name = "txtNCliente" + CType(_Linea2, String) Then
                            Paneles.Text = CType(Contrato, String)
                        End If
                    End If
                Next
            End If

        End If

        'frmBuscar.Dispose()
    End Sub


    Private Sub Liquidacion_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub


    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Cursor = Cursors.WaitCursor
        Dim oReporte As New frmConsultaReporte(_AñoAtt, _Folio)
        oReporte.ShowDialog()
        Cursor = Cursors.Default
        'Dim frmReporte As frmLiquidacion = New frmLiquidacion()
        'frmReporte.Entrada(DsLiquidacion.Operador(0).Folio)
        'frmReporte.Dispose()
    End Sub

    Private Sub btnDocumento_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocumento.Click
        Dim Importe As Decimal
        Dim i As Integer
        Dim j As Integer
        Dim Encontro As Boolean
        Dim Paneles As System.Windows.Forms.Control

        Importe = CType(lstLitros.Items(_Linea), Decimal) * CType(lstPrecio.Items(_Linea), Decimal)

        Dim frmRelacion As Relacion = New Relacion()

        frmRelacion.DsLiquidacion.Documento.Clear()
        For i = 0 To DsLiquidacion.Documento.Count - 1
            'frmRelacion.DsLiquidacion.Documento.AddDocumentoRow(DsLiquidacion.Documento(i).Banco, DsLiquidacion.Documento(i).Cheque, _
            '                            DsLiquidacion.Documento(i).FCheque, DsLiquidacion.Documento(i).Cuenta, _
            '                            DsLiquidacion.Documento(i).Monto, DsLiquidacion.Documento(i).Disponible, _
            '                            DsLiquidacion.Documento(i).DesBanco, DsLiquidacion.Documento(i).Llave, _
            '                            DsLiquidacion.Documento(i).Tipo, DsLiquidacion.Documento(i).TipoDes, DsLiquidacion.Documento(i).Cliente, DsLiquidacion.Documento(i).Nombre, DsLiquidacion.Documento(i).PosFechado)
        Next

        frmRelacion.DsLiquidacion.Detalle.Clear()
        For i = 0 To DsLiquidacion.Detalle.Count - 1
            frmRelacion.DsLiquidacion.Detalle.AddDetalleRow(DsLiquidacion.Detalle(i).Cliente, DsLiquidacion.Detalle(i).Monto, _
                                                            DsLiquidacion.Detalle(i).Tipo, DsLiquidacion.Detalle(i).DesTipo, _
                                                            DsLiquidacion.Detalle(i).Banco, DsLiquidacion.Detalle(i).Cheque, _
                                                            DsLiquidacion.Detalle(i).Cuenta, DsLiquidacion.Detalle(i).NombreBanco, DsLiquidacion.Detalle(i).Nombre)
        Next

        frmRelacion.DsLiquidacion.Cliente.Clear()
        For i = 0 To DsLiquidacion.Cliente.Count - 1
            frmRelacion.DsLiquidacion.Cliente.AddClienteRow(DsLiquidacion.Cliente(i).Monto, DsLiquidacion.Cliente(i).Nombre, DsLiquidacion.Cliente(i).Disponible, DsLiquidacion.Cliente(i).Cliente, 0, DsLiquidacion.Cliente(i).Tipo, DsLiquidacion.Cliente(i).DesTipo)
        Next

        For i = 0 To lstClientes.Items.Count - 1
            Encontro = False
            If CType(lstLitros.Items(i), Integer) <> 0 And CType(lstForma.Items(i), String) = "CONTADO" Then
                For j = 0 To DsLiquidacion.Cliente.Count - 1
                    If DsLiquidacion.Cliente(j).Cliente = CType(lstClientes.Items(i), Integer) Then
                        Encontro = True
                    End If
                Next

                If Encontro = False Then
                    Dim Nombre As String
                    For Each Paneles In pnPedidos.Controls
                        If Paneles.Name.Substring(0, 3) = "lbN" And CType(Paneles.Tag, Integer) = i Then
                            Nombre = Paneles.Text
                        End If
                    Next
                    ' frmRelacion.DsLiquidacion.Cliente.AddClienteRow((CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal)), Nombre, (CType(lstLitros.Items(i), Decimal) * CType(lstPrecio.Items(i), Decimal)), CType(lstClientes.Items(i), Integer), 0)
                End If

            End If
        Next

        For i = 0 To lstClientes2.Items.Count - 1
            Encontro = False
            If CType(lstLitros2.Items(i), Integer) <> 0 And CType(lstClientes2.Items(i), Integer) <> 0 And CType(lstForma2.Items(i), String) = "CONTADO" Then
                If CType(lstClientes2.Items(i), Integer) <> _ClienteGlobal Then
                    For j = 0 To DsLiquidacion.Cliente.Count - 1
                        If DsLiquidacion.Cliente(j).Cliente = CType(lstClientes2.Items(i), Integer) Then
                            Encontro = True
                        End If
                    Next

                    If Encontro = False Then
                        Dim Nombre As String
                        For Each Paneles In pnNotas.Controls
                            If Paneles.Name.Substring(0, 4) = "lbNN" And CType(Paneles.Tag, Integer) = i Then
                                Nombre = Paneles.Text
                            End If
                        Next
                        'frmRelacion.DsLiquidacion.Cliente.AddClienteRow((CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal)), Nombre, (CType(lstLitros2.Items(i), Decimal) * CType(lstPrecio2.Items(i), Decimal)), CType(lstClientes2.Items(i), Integer), 0)
                    End If

                End If
            End If
        Next


        frmRelacion.Entrada(0, 0, _Fecha)

        DsLiquidacion.Documento.Clear()
        For i = 0 To frmRelacion.DsLiquidacion.Documento.Count - 1
            'DsLiquidacion.Documento.AddDocumentoRow(frmRelacion.DsLiquidacion.Documento(i).Banco, frmRelacion.DsLiquidacion.Documento(i).Cheque, _
            '                                        frmRelacion.DsLiquidacion.Documento(i).FCheque, frmRelacion.DsLiquidacion.Documento(i).Cuenta, _
            '                                        frmRelacion.DsLiquidacion.Documento(i).Monto, frmRelacion.DsLiquidacion.Documento(i).Disponible, _
            '                                        frmRelacion.DsLiquidacion.Documento(i).DesBanco, frmRelacion.DsLiquidacion.Documento(i).Llave, _
            '                                        frmRelacion.DsLiquidacion.Documento(i).Tipo, frmRelacion.DsLiquidacion.Documento(i).TipoDes, frmRelacion.DsLiquidacion.Documento(i).Cliente, frmRelacion.DsLiquidacion.Documento(i).Nombre, frmRelacion.DsLiquidacion.Documento(i).PosFechado)
        Next

        DsLiquidacion.Detalle.Clear()
        For i = 0 To frmRelacion.DsLiquidacion.Detalle.Count - 1
            DsLiquidacion.Detalle.AddDetalleRow(frmRelacion.DsLiquidacion.Detalle(i).Cliente, frmRelacion.DsLiquidacion.Detalle(i).Monto, _
                                                frmRelacion.DsLiquidacion.Detalle(i).Tipo, frmRelacion.DsLiquidacion.Detalle(i).DesTipo, _
                                                frmRelacion.DsLiquidacion.Detalle(i).Banco, frmRelacion.DsLiquidacion.Detalle(i).Cheque, _
                                                frmRelacion.DsLiquidacion.Detalle(i).Cuenta, frmRelacion.DsLiquidacion.Detalle(i).NombreBanco, frmRelacion.DsLiquidacion.Detalle(i).Nombre)
        Next


        DsLiquidacion.Cliente.Clear()
        For i = 0 To frmRelacion.DsLiquidacion.Cliente.Count - 1
            ' DsLiquidacion.Cliente.AddClienteRow(frmRelacion.DsLiquidacion.Cliente(i).Monto, frmRelacion.DsLiquidacion.Cliente(i).Nombre, frmRelacion.DsLiquidacion.Cliente(i).Disponible, frmRelacion.DsLiquidacion.Cliente(i).Cliente, 0)
        Next

        frmRelacion.Dispose()

    End Sub


    Private Sub txtFolio_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFolio.KeyPress
        If e.KeyChar = "0" Or e.KeyChar = "1" Or e.KeyChar = "2" Or e.KeyChar = "3" Or e.KeyChar = "4" Or e.KeyChar = "5" Or e.KeyChar = "6" Or e.KeyChar = "7" Or e.KeyChar = "8" Or e.KeyChar = "9" Or e.KeyChar = ControlChars.Back Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Paneles As System.Windows.Forms.Control
        Dim Valor As Integer
        Valor = CType(txtFolio.Text, Integer)

        For Each Paneles In pnNotas.Controls
            If Paneles.Name.Substring(0, 5) = "txtNN" Then
                Paneles.Text = CType(Valor, String)
            End If
            Valor = Valor + 1
        Next

    End Sub

    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim frmBuscarLitros As BuscarLitros = New BuscarLitros()
        Dim Litros As Decimal
        Dim LineaEncontro As Integer
        Dim Panel As Integer
        Dim Encontro As Boolean

        frmBuscarLitros.ShowDialog()
        Litros = frmBuscarLitros._Litros
        Panel = frmBuscarLitros._Panel

        If Litros > 0 Then
            Encontro = False
            Dim Paneles As System.Windows.Forms.Control
            If Panel = 1 Then
                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "txtL" Then
                        If CType(Paneles.Text, Integer) = Litros Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If
                Next

                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "cmbF" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next

            End If

            If Panel = 2 Then
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNL" Then
                        If CType(Paneles.Text, Integer) = Litros Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If

                Next

                For Each Paneles In pnNotas.Controls
                    If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next
            End If

        End If
        frmBuscarLitros.Dispose()

    End Sub

    Private Sub MenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem4.Click
        Dim frmBuscarLitros As BuscarLitros = New BuscarLitros()
        Dim Litros As Decimal
        Dim LineaEncontro As Integer
        Dim Panel As Integer
        Dim Encontro As Boolean

        frmBuscarLitros.ShowDialog()
        Litros = frmBuscarLitros._Litros
        Panel = frmBuscarLitros._Panel

        If Litros > 0 Then
            Encontro = False
            Dim Paneles As System.Windows.Forms.Control
            If Panel = 1 Then
                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "txtL" Then
                        If CType(Paneles.Text, Integer) = Litros Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If
                Next

                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "cmbF" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next

            End If

            If Panel = 2 Then
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNL" Then
                        If CType(Paneles.Text, Integer) = Litros Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If

                Next

                For Each Paneles In pnNotas.Controls
                    If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next
            End If

        End If
        frmBuscarLitros.Dispose()

    End Sub

    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem6.Click

        Dim frmBuscarLitros As BuscarLitros = New BuscarLitros()
        Dim Litros As Decimal
        Dim LineaEncontro As Integer
        Dim Panel As Integer
        Dim Encontro As Boolean

        frmBuscarLitros.ShowDialog()
        Litros = frmBuscarLitros._Litros
        Panel = frmBuscarLitros._Panel

        If Litros > 0 Then
            Encontro = False
            Dim Paneles As System.Windows.Forms.Control
            If Panel = 1 Then
                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "txtL" Then
                        If CType(Paneles.Text, Integer) = Litros Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If
                Next

                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "cmbF" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next

            End If

            If Panel = 2 Then
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNL" Then
                        If CType(Paneles.Text, Integer) = Litros Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If

                Next

                For Each Paneles In pnNotas.Controls
                    If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next
            End If

        End If
        frmBuscarLitros.Dispose()

    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCliente.Click
        Dim frmBuscarCliente As BuscarCliente = New BuscarCliente()
        Dim Cliente As Integer
        Dim LineaEncontro As Integer
        Dim Panel As Integer
        Dim Encontro As Boolean

        frmBuscarCliente.ShowDialog()
        Cliente = frmBuscarCliente._Cliente
        Panel = frmBuscarCliente._Panel

        If Cliente > 0 Then
            Encontro = False
            Dim Paneles As System.Windows.Forms.Control
            If Panel = 1 Then
                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 3) = "lbC" Then
                        If CType(Paneles.Text, Integer) = Cliente Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If
                Next

                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "cmbF" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next

            End If

            If Panel = 2 Then
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNC" Then
                        If IsNumeric(Paneles.Text) = True Then
                            If CType(Paneles.Text, Integer) = Cliente Then
                                LineaEncontro = CType(Paneles.Tag, Integer)
                                Encontro = True
                            End If
                        End If
                    End If

                Next

                For Each Paneles In pnNotas.Controls
                    If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next
            End If

        End If
        frmBuscarCliente.Dispose()

    End Sub

    Private Sub MenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem8.Click
        Dim frmBuscarCliente As BuscarCliente = New BuscarCliente()
        Dim Cliente As Integer
        Dim LineaEncontro As Integer
        Dim Panel As Integer
        Dim Encontro As Boolean

        frmBuscarCliente.ShowDialog()
        Cliente = frmBuscarCliente._Cliente
        Panel = frmBuscarCliente._Panel

        If Cliente > 0 Then
            Encontro = False
            Dim Paneles As System.Windows.Forms.Control
            If Panel = 1 Then
                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 3) = "lbC" Then
                        If CType(Paneles.Text, Integer) = Cliente Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If
                Next

                For Each Paneles In pnPedidos.Controls
                    If Paneles.Name.Substring(0, 4) = "cmbF" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next

            End If

            If Panel = 2 Then
                For Each Paneles In pnNotas.Controls

                    If Paneles.Name.Substring(0, 5) = "txtNC" Then
                        If CType(Paneles.Text, Integer) = Cliente Then
                            LineaEncontro = CType(Paneles.Tag, Integer)
                            Encontro = True
                        End If
                    End If

                Next

                For Each Paneles In pnNotas.Controls
                    If Paneles.Name.Substring(0, 5) = "txtNN" And CType(Paneles.Tag, Integer) = LineaEncontro And Encontro = True Then
                        Paneles.Select()
                        Exit Sub
                    End If
                Next
            End If

        End If
        frmBuscarCliente.Dispose()

    End Sub
#End Region

    'TODO: Se agregó el día 08/10/2004
#Region "Validacion de creditos"
    'TODO: Vaidacion de importe máximo de crédito
    Dim clienteParaValidacionCredito As Integer
    Public Sub maximoImporteCreditoExcedido(ByVal AplicaValidacion As Boolean, ByVal Cliente As Integer, _
         ByVal Importe As Decimal, ByVal Connection As SqlClient.SqlConnection)
        'TODO: Parametrizar celula 6
        If AplicaValidacion And (GLOBAL_Celula <> 6) Then
            Dim cmdSelect As New SqlClient.SqlCommand()
            cmdSelect.CommandText = "SELECT Saldo, MaxImporteCredito, Cartera FROM Cliente WHERE Cliente = @Cliente" 
            cmdSelect.CommandType = CommandType.Text
            cmdSelect.Connection = Connection
            cmdSelect.Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
            Dim reader As SqlClient.SqlDataReader = Nothing
            Dim saldo As Decimal
            Dim maxCredito As Decimal
            Dim tipoCartera As Byte
            Try
                reader = cmdSelect.ExecuteReader
                While reader.Read
                    saldo = CType(IIf(CType(reader("saldo"), Decimal) < 0, 0, reader("saldo")), Decimal)
                    maxCredito = CType(reader("maximportecredito"), Integer)
                    tipoCartera = CType(IIf(reader("Cartera") Is DBNull.Value, 0, reader("Cartera")), Byte)
                    reader.NextResult()
                    End While
                If tipoCartera = GLOBAL_ClaveCreditoAutorizado Then
                    If (saldo + Importe) > maxCredito Then
                        MessageBox.Show("El importe máximo de crédito para este cliente ($" & CStr(Importe) & ")" & _
                        Chr(13) & "ha sido rebasado", "Cliente no. " & CStr(Cliente), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        End If
                Else
                    MessageBox.Show("Este cliente no tiene línea de crédito", _
                        "Cliente no. " & CStr(Cliente), MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
            Catch ex As Exception
                    'Se debería mostrar una advertencia de error
            Finally
                reader.Close()
                cmdSelect.Dispose()
                End Try
        End If
    End Sub

#End Region

End Class

