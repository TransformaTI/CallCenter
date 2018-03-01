Public Class Pedido
    Inherits System.Windows.Forms.Form
    Private _Cliente As Integer
    Private _Celula As Integer
    Private _Ruta As Integer
    Private _Tipo As Integer
    Private _Pedido As Integer
    Private _Anio As Integer
    Private _TienePrograma As Boolean
    Private _FPrograma1 As DateTime
    Private _FPrograma2 As DateTime
    Private _numeroCelda As Integer
    Private noGrabar As Boolean = False

    Private fechaHoy As Date
    Friend WithEvents lblFinRuta As System.Windows.Forms.Label

    Private _mensajeCreditoExcedido As String = ""

#Region "Funciones de semana"
    'Private Function Lunes() As Integer
    '    Dim Dias As Integer

    '    If RTrim(DsPedido.Horario(0).Martes) <> "" Then
    '        Dias = 1
    '    Else
    '        If RTrim(DsPedido.Horario(0).Miercoles) <> "" Then
    '            Dias = 2
    '        Else
    '            If RTrim(DsPedido.Horario(0).Jueves) <> "" Then
    '                Dias = 3
    '            Else
    '                If RTrim(DsPedido.Horario(0).Viernes) <> "" Then
    '                    Dias = 4
    '                Else
    '                    If RTrim(DsPedido.Horario(0).Sabado) <> "" Then
    '                        Dias = 5
    '                    Else
    '                        If RTrim(DsPedido.Horario(0).Domingo) <> "" Then
    '                            Dias = 6
    '                        Else
    '                            If RTrim(DsPedido.Horario(0).Lunes) <> "" Then
    '                                Dias = 7
    '                            Else
    '                                Dias = 0
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return (Dias)
    'End Function

    'Private Function Martes() As Integer
    '    Dim Dias As Integer

    '    If RTrim(DsPedido.Horario(0).Miercoles) <> "" Then
    '        Dias = 1
    '    Else
    '        If RTrim(DsPedido.Horario(0).Jueves) <> "" Then
    '            Dias = 2
    '        Else
    '            If RTrim(DsPedido.Horario(0).Viernes) <> "" Then
    '                Dias = 3
    '            Else
    '                If RTrim(DsPedido.Horario(0).Sabado) <> "" Then
    '                    Dias = 4
    '                Else
    '                    If RTrim(DsPedido.Horario(0).Domingo) <> "" Then
    '                        Dias = 5
    '                    Else
    '                        If RTrim(DsPedido.Horario(0).Lunes) <> "" Then
    '                            Dias = 6
    '                        Else
    '                            If RTrim(DsPedido.Horario(0).Martes) <> "" Then
    '                                Dias = 7
    '                            Else
    '                                Dias = 0
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return (Dias)
    'End Function

    'Private Function Miercoles() As Integer
    '    Dim Dias As Integer

    '    If RTrim(DsPedido.Horario(0).Jueves) <> "" Then
    '        Dias = 1
    '    Else
    '        If RTrim(DsPedido.Horario(0).Viernes) <> "" Then
    '            Dias = 2
    '        Else
    '            If RTrim(DsPedido.Horario(0).Sabado) <> "" Then
    '                Dias = 3
    '            Else
    '                If RTrim(DsPedido.Horario(0).Domingo) <> "" Then
    '                    Dias = 4
    '                Else
    '                    If RTrim(DsPedido.Horario(0).Lunes) <> "" Then
    '                        Dias = 5
    '                    Else
    '                        If RTrim(DsPedido.Horario(0).Martes) <> "" Then
    '                            Dias = 6
    '                        Else
    '                            If RTrim(DsPedido.Horario(0).Miercoles) <> "" Then
    '                                Dias = 7
    '                            Else
    '                                Dias = 0
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return (Dias)
    'End Function

    'Private Function Jueves() As Integer
    '    Dim Dias As Integer

    '    If RTrim(DsPedido.Horario(0).Viernes) <> "" Then
    '        Dias = 1
    '    Else
    '        If RTrim(DsPedido.Horario(0).Sabado) <> "" Then
    '            Dias = 2
    '        Else
    '            If RTrim(DsPedido.Horario(0).Domingo) <> "" Then
    '                Dias = 3
    '            Else
    '                If RTrim(DsPedido.Horario(0).Lunes) <> "" Then
    '                    Dias = 4
    '                Else
    '                    If RTrim(DsPedido.Horario(0).Martes) <> "" Then
    '                        Dias = 5
    '                    Else
    '                        If RTrim(DsPedido.Horario(0).Miercoles) <> "" Then
    '                            Dias = 6
    '                        Else
    '                            If RTrim(DsPedido.Horario(0).Jueves) <> "" Then
    '                                Dias = 7
    '                            Else
    '                                Dias = 0
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return (Dias)
    'End Function

    'Private Function Viernes() As Integer
    '    Dim Dias As Integer

    '    If RTrim(DsPedido.Horario(0).Sabado) <> "" Then
    '        Dias = 1
    '    Else
    '        If RTrim(DsPedido.Horario(0).Domingo) <> "" Then
    '            Dias = 2
    '        Else
    '            If RTrim(DsPedido.Horario(0).Lunes) <> "" Then
    '                Dias = 3
    '            Else
    '                If RTrim(DsPedido.Horario(0).Martes) <> "" Then
    '                    Dias = 4
    '                Else
    '                    If RTrim(DsPedido.Horario(0).Miercoles) <> "" Then
    '                        Dias = 5
    '                    Else
    '                        If RTrim(DsPedido.Horario(0).Jueves) <> "" Then
    '                            Dias = 6
    '                        Else
    '                            If RTrim(DsPedido.Horario(0).Viernes) <> "" Then
    '                                Dias = 7
    '                            Else
    '                                Dias = 0
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return (Dias)
    'End Function

    'Private Function Sabado() As Integer
    '    Dim Dias As Integer

    '    If RTrim(DsPedido.Horario(0).Domingo) <> "" Then
    '        Dias = 1
    '    Else
    '        If RTrim(DsPedido.Horario(0).Lunes) <> "" Then
    '            Dias = 2
    '        Else
    '            If RTrim(DsPedido.Horario(0).Martes) <> "" Then
    '                Dias = 3
    '            Else
    '                If RTrim(DsPedido.Horario(0).Miercoles) <> "" Then
    '                    Dias = 4
    '                Else
    '                    If RTrim(DsPedido.Horario(0).Jueves) <> "" Then
    '                        Dias = 5
    '                    Else
    '                        If RTrim(DsPedido.Horario(0).Viernes) <> "" Then
    '                            Dias = 6
    '                        Else
    '                            If RTrim(DsPedido.Horario(0).Sabado) <> "" Then
    '                                Dias = 7
    '                            Else
    '                                Dias = 0
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return (Dias)
    'End Function

    'Private Function Domingo() As Integer
    '    Dim Dias As Integer

    '    If RTrim(DsPedido.Horario(0).Lunes) <> "" Then
    '        Dias = 1
    '    Else
    '        If RTrim(DsPedido.Horario(0).Martes) <> "" Then
    '            Dias = 2
    '        Else
    '            If RTrim(DsPedido.Horario(0).Miercoles) <> "" Then
    '                Dias = 3
    '            Else
    '                If RTrim(DsPedido.Horario(0).Jueves) <> "" Then
    '                    Dias = 4
    '                Else
    '                    If RTrim(DsPedido.Horario(0).Viernes) <> "" Then
    '                        Dias = 5
    '                    Else
    '                        If RTrim(DsPedido.Horario(0).Sabado) <> "" Then
    '                            Dias = 6
    '                        Else
    '                            If RTrim(DsPedido.Horario(0).Domingo) <> "" Then
    '                                Dias = 7
    '                            Else
    '                                Dias = 0
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    End If

    '    Return (Dias)
    'End Function
#End Region

    Public Sub Entrada(ByVal Cliente As Integer, _
                       ByVal Nombre As String, _
                       ByVal CP As String, _
                       ByVal Ruta As Integer, _
                       ByVal Colonia As Integer, _
                       ByVal Celula As Integer, _
                       ByVal rutatexto As String, _
                       ByVal Tipo As Integer, _
                       Optional ByVal mensajeCreditoExcedido As String = "", _
                       Optional ByVal ColoniaNombre As String = "")
        '31/12/2004 Se agregó el mensaje de crédito excedido para imprimir en la remisión JAGD 1981

        Dim Dias As Integer = Nothing
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing
        Dim Prioridad As Integer
        Dim Producto As Integer
        Dim TipoProgramacion As Boolean = False
        Dim cadenaFecha As String = ""

        Me.Text = "Pedido - [" + Nombre + "]"
        _Cliente = Cliente
        _Celula = Celula
        _Ruta = Ruta
        _Tipo = Tipo

        Try
            SqlConnection.Close()
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        'cmdCHorario.Parameters("@CP").Value = ""
        'cmdCHorario.Parameters("@Ruta").Value = 0
        'daHorario.Fill(DsPedido, "Horario")
        'DsPedido.Tables("Horario").Clear()
        'cmdCHorario.Parameters("@CP").Value = CP
        'cmdCHorario.Parameters("@Ruta").Value = Ruta
        'daHorario.Fill(DsPedido, "Horario")
        'dgHorario.DataSource = DsPedido.Tables("Horario")

        'dgHorario.DataSource = fncHorario(CP, Ruta, Colonia)
        'grdHorario.DataSource = fncHorario(CP, Ruta, Colonia)


        'JAGD 1981 Se incluye el mensaje de credito excedido en las observaciones de pedido para imprimir en la remisión
        If Len(Trim(mensajeCreditoExcedido)) > 0 Then
            _mensajeCreditoExcedido = mensajeCreditoExcedido
        End If

        Try
            cmdInsert.Connection = SqlConnection
            cmdInsert.CommandTimeout = 30
            cmdInsert.CommandText = "select getdate() as fecha"
            cmdInsert.Parameters.Clear()
            rdrInsert = cmdInsert.ExecuteReader()
            If rdrInsert.Read() = True Then
                fechaHoy = CType(rdrInsert("fecha"), Date)
            End If
        Catch ioEx As Exception
            fechaHoy = Now
        End Try
        rdrInsert.Close()
        cmdInsert.Dispose()

        If _Tipo = 1 Then

            txtFCompromiso.Value = fechaHoy
            'txtFCompromiso.Value = CType(cadenaFecha, Date)
            'txtFCompromiso.Value = txtFCompromiso.Value.AddDays(CType(cadenaFecha, Date))

            Try
                cmdInsert.Connection = SqlConnection
                'llamada al spCCConsultaPedidoCteEstacionario para consulta de detalles
                'de pedido 16/11/2004 jagd
                cmdInsert.CommandText = "EXECUTE spCCConsultaPedidoCteEstacionario @Cliente"
                'Seccipon deshabilitada por la llamada al spCCConsultaPedidoCteEstacionario
                'cmdInsert.CommandTimeout = 30
                'cmdInsert.CommandText = "Select Pedido.Pedido, Pedido.AñoPed, Pedido.Producto, Pedido.Celula, Pedido.FPedido, Pedido.FCompromiso,Pedido.Observaciones, Pedido.Ruta, Ruta.Descripcion as DesRuta, Pedido.PrioridadPedido from Pedido INNER JOIN Producto " & _
                '                        "     ON Pedido.Producto=Producto.Producto and Producto.TipoProducto=1 INNER JOIN Ruta ON Pedido.Ruta=Ruta.Ruta where Cliente=@Cliente and Pedido.Status='ACTIVO' and TipoCargo=1 "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                rdrInsert = cmdInsert.ExecuteReader()
                rdrInsert.Read()
                lbFPedido.Text = Format(rdrInsert("FPedido"), "dd/MM/yyyy")
                lbRuta.Text = CType(rdrInsert("DesRuta"), String)
                _Ruta = CType(rdrInsert("Ruta"), Integer)
                txtFCompromiso.Value = CType(rdrInsert("FCompromiso"), Date)


                _Pedido = CType(rdrInsert("Pedido"), Integer)
                _Celula = CType(rdrInsert("Celula"), Integer)
                _Anio = CType(rdrInsert("AñoPed"), Integer)

                txtObservaciones.Text = CType(rdrInsert("Observaciones"), String).Trim

                'Para remover el mensaje de crédito excedido de las observaciones de pedido
                If Len(Trim(_mensajeCreditoExcedido)) > 0 Then
                    txtObservaciones.Text = txtObservaciones.Text.Replace(_mensajeCreditoExcedido.ToUpper, "")
                End If

                Producto = CType(rdrInsert("Producto"), Integer)
                Prioridad = CType(rdrInsert("PrioridadPedido"), Integer)

                rdrInsert.Close()
                cmdInsert.Dispose()

                daTipoPedido.Fill(DsPedido, "TipoPedido")
                daProducto.Fill(DsPedido, "Producto")
                daPrioridad.Fill(DsPedido, "Prioridad")

                cmbProducto.SelectedValue = Producto
                cbPrioridad.SelectedValue = Prioridad

                'cmdCHorario.Parameters("@CP").Value = ""
                'cmdCHorario.Parameters("@Ruta").Value = 0
                'daHorario.Fill(DsPedido, "Horario")
                DsPedido.Tables("Horario").Clear()

                'cmdCHorario.Parameters("@CP").Value = CP
                'cmdCHorario.Parameters("@Ruta").Value = Ruta
                'daHorario.Fill(DsPedido, "Horario")
                'dgHorario.DataSource = DsPedido.Tables("Horario")
                grdHorario.DataSource = fncHorario(CP, Ruta, Colonia)
                'dgHorario.CaptionText = "Horario para la ruta " + CType(Ruta, String) + " en la colonia " + Colonia

                lblGrdCaption.Text = "HORARIO PARA LA RUTA " & rutatexto & " EN LA COLONIA " & ColoniaNombre
                'Agregado el día 05/10/2004 para deshabilitar el cambio de fecha
                'compromiso
                If _Pedido <> 0 Then
                    If Not (GLOBAL_AplicaCambioFechaCompromiso) Then
                        txtFCompromiso.Enabled = False
                    End If
                End If
            Catch ioEx As Exception
                MessageBox.Show(ioex.Message, "Error cargando datos de pedido", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            cmbTipoPedido.Enabled = False
            cmbProducto.Enabled = False
            cbPrioridad.Enabled = False

        Else

            lbFPedido.Text = Format(fechaHoy, "dd/MM/yyyy")
            lbRuta.Text = rutatexto
            Try
                daTipoPedido.Fill(DsPedido, "TipoPedido")
                daProducto.Fill(DsPedido, "Producto")
                daPrioridad.Fill(DsPedido, "Prioridad")

                'cmdCHorario.Parameters("@CP").Value = ""
                'cmdCHorario.Parameters("@Ruta").Value = 0
                'daHorario.Fill(DsPedido, "Horario")
                DsPedido.Tables("Horario").Clear()

                'cmdCHorario.Parameters("@CP").Value = CP
                'cmdCHorario.Parameters("@Ruta").Value = Ruta
                'daHorario.Fill(DsPedido, "Horario")
                'grdHorario.DataSource = DsPedido.Tables("Horario")
                'dgHorario.DataSource = DsPedido.Tables("Horario")
                'dgHorario.CaptionText = "Horario para la ruta " + CType(Ruta, String) + " en la colonia " + Colonia

                lblGrdCaption.Text = "HORARIO PARA LA RUTA " & rutatexto & " EN LA COLONIA " & ColoniaNombre

                grdHorario.DataSource = fncHorario(CP, Ruta, Colonia)
            Catch ioEx As Exception
            End Try

            'Select Case fechaHoy.DayOfWeek()
            '    Case DayOfWeek.Monday
            '        'Dias = Lunes()
            '        cadenaFecha = " Lunes, "
            '    Case DayOfWeek.Tuesday
            '        'Dias = Martes()
            '        cadenaFecha = " Martes, "
            '    Case DayOfWeek.Wednesday
            '        'Dias = Miercoles()
            '        cadenaFecha = " Miercoles, "
            '    Case DayOfWeek.Thursday
            '        'Dias = Jueves()
            '        cadenaFecha = " Jueves, "
            '    Case DayOfWeek.Friday
            '        'Dias = Viernes()
            '        cadenaFecha = " Viernes, "
            '    Case DayOfWeek.Saturday
            '        cadenaFecha = " Sábado, "
            '        'Dias = Sabado()
            '    Case DayOfWeek.Sunday
            '        'Dias = Domingo()
            '        cadenaFecha = " Domingo, "
            'End Select

            'If fechaHoy.Day < 10 Then
            '    cadenaFecha = cadenaFecha & "0" & fechaHoy.Day & " de " & fechaHoy.Month & " de " & fechaHoy.Year
            'Else
            '    cadenaFecha = cadenaFecha & fechaHoy.Day & " de " & fechaHoy.Month & " de " & fechaHoy.Year
            'End If

            txtFCompromiso.Value = fechaHoy
            'txtFCompromiso.Value = CType(cadenaFecha, Date)
            'txtFCompromiso.Value = txtFCompromiso.Value.AddDays(CType(cadenaFecha, Date))
        End If

        Try
            cmdInsert.Connection = SqlConnection
            'cmdInsert.CommandText = "Select Top 2 (FProgramacion + Corrimiento) as Fecha from Programacion where Cliente=@Cliente Order by 1 "
            cmdInsert.CommandText = "Select Top 2 FProgramacion as Fecha from ProgramacionBeta where Cliente=@Cliente Order by 1 "
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
            rdrInsert = cmdInsert.ExecuteReader()
            If rdrInsert.Read = False Then
                lbProgramado.Text = "No tiene pedidos programados."
                lbDiaPrograma.Text = ""
                _TienePrograma = False
            Else
                lbProgramado.Text = "El cliente tiene un pedido programado para el dia "
                lbDiaPrograma.Text = Format(rdrInsert("Fecha"), "D")
                _TienePrograma = True
                _FPrograma1 = CType(rdrInsert("Fecha"), Date)
            End If


            If rdrInsert.Read = False Then
                _FPrograma2 = CType("01/01/1950", Date)
            Else
                _FPrograma2 = CType(rdrInsert("Fecha"), Date)
            End If

            rdrInsert.Close()
            cmbTipoPedido.Visible = True

            'VERIFICAR SI TIENE PROGRAMACIONES, EN CASO DE, ASIGNAR 
            'LA PROPIEDAD DE PROGRAMABLE AL COMBO DE TIPO DE PEDIDO
            If Len(txtObservaciones.Text.Trim) = 0 Then
                'TODO: Preguntar para que @%&#@ sirve esto
                cmdInsert.CommandText = "Select IsNull(Observaciones,'') as Observaciones from ProgramaCliente where Cliente=@Cliente"
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                rdrInsert = cmdInsert.ExecuteReader()
                If rdrInsert.Read = True Then
                    txtObservaciones.Text = CType(rdrInsert("Observaciones"), String)
                End If
                rdrInsert.Close()
                cmdInsert.Dispose()
            End If
        Catch ioEx As Exception

            'MessageBox.Show(ioex.Message, "Error cargando datos de pedido", _
            '    MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        txtObservaciones.Select()
        Me.ShowDialog()


    End Sub

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
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Friend WithEvents cmbTipoPedido As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbProducto As System.Windows.Forms.ComboBox
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbFPedido As System.Windows.Forms.Label
    Friend WithEvents lbRuta As System.Windows.Forms.Label
    Friend WithEvents cbPrioridad As System.Windows.Forms.ComboBox
    Friend WithEvents daTipoPedido As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsPedido As Sigamet.dsPedido
    Friend WithEvents cmdCProducto As System.Data.SqlClient.SqlCommand
    Friend WithEvents daProducto As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdCPrioridad As System.Data.SqlClient.SqlCommand
    Friend WithEvents daPrioridad As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DataGridTextBoxColumn6 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents DataGridTextBoxColumn7 As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents txtFCompromiso As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmdCTipoPedido As System.Data.SqlClient.SqlCommand
    Friend WithEvents lbProgramado As System.Windows.Forms.Label
    Friend WithEvents lbDiaPrograma As System.Windows.Forms.Label
    Friend WithEvents grdHorario As System.Windows.Forms.DataGrid
    Friend WithEvents DataGridTableStyle1 As System.Windows.Forms.DataGridTableStyle
    Friend WithEvents colLunes As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colMartes As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colMiercoles As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colJueves As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colViernes As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colSabado As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents colDomingo As System.Windows.Forms.DataGridTextBoxColumn
    Friend WithEvents lblGrdCaption As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Pedido))
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnAceptar = New System.Windows.Forms.Button()
        Me.txtFCompromiso = New System.Windows.Forms.DateTimePicker()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.cmbTipoPedido = New System.Windows.Forms.ComboBox()
        Me.DsPedido = New Sigamet.dsPedido()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbProducto = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.lbFPedido = New System.Windows.Forms.Label()
        Me.lbRuta = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbPrioridad = New System.Windows.Forms.ComboBox()
        Me.cmdCTipoPedido = New System.Data.SqlClient.SqlCommand()
        Me.daTipoPedido = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCProducto = New System.Data.SqlClient.SqlCommand()
        Me.daProducto = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdCPrioridad = New System.Data.SqlClient.SqlCommand()
        Me.daPrioridad = New System.Data.SqlClient.SqlDataAdapter()
        Me.DataGridTextBoxColumn6 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.DataGridTextBoxColumn7 = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lbProgramado = New System.Windows.Forms.Label()
        Me.lbDiaPrograma = New System.Windows.Forms.Label()
        Me.grdHorario = New System.Windows.Forms.DataGrid()
        Me.DataGridTableStyle1 = New System.Windows.Forms.DataGridTableStyle()
        Me.colLunes = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colMartes = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colMiercoles = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colJueves = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colViernes = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colSabado = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.colDomingo = New System.Windows.Forms.DataGridTextBoxColumn()
        Me.lblGrdCaption = New System.Windows.Forms.Label()
        Me.lblFinRuta = New System.Windows.Forms.Label()
        CType(Me.DsPedido, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdHorario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 12)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Fecha del pedido:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Fecha compromiso:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Ruta:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 179)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(82, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Observaciones:"
        '
        'btnCancelar
        '
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.Image = CType(resources.GetObject("btnCancelar.Image"), System.Drawing.Image)
        Me.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancelar.Location = New System.Drawing.Point(528, 40)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 8
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAceptar
        '
        Me.btnAceptar.Image = CType(resources.GetObject("btnAceptar.Image"), System.Drawing.Image)
        Me.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAceptar.Location = New System.Drawing.Point(528, 8)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 7
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFCompromiso
        '
        Me.txtFCompromiso.Location = New System.Drawing.Point(128, 91)
        Me.txtFCompromiso.Name = "txtFCompromiso"
        Me.txtFCompromiso.Size = New System.Drawing.Size(240, 21)
        Me.txtFCompromiso.TabIndex = 3
        '
        'txtObservaciones
        '
        Me.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtObservaciones.Location = New System.Drawing.Point(128, 180)
        Me.txtObservaciones.Multiline = True
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtObservaciones.Size = New System.Drawing.Size(480, 116)
        Me.txtObservaciones.TabIndex = 6
        '
        'cmbTipoPedido
        '
        Me.cmbTipoPedido.DataSource = Me.DsPedido.TipoPedido
        Me.cmbTipoPedido.DisplayMember = "Descripcion"
        Me.cmbTipoPedido.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipoPedido.Location = New System.Drawing.Point(128, 35)
        Me.cmbTipoPedido.Name = "cmbTipoPedido"
        Me.cmbTipoPedido.Size = New System.Drawing.Size(240, 21)
        Me.cmbTipoPedido.TabIndex = 1
        Me.cmbTipoPedido.ValueMember = "TipoPedido"
        '
        'DsPedido
        '
        Me.DsPedido.DataSetName = "dsPedido"
        Me.DsPedido.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsPedido.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Tipo de pedido:"
        '
        'cmbProducto
        '
        Me.cmbProducto.BackColor = System.Drawing.SystemColors.Menu
        Me.cmbProducto.DataSource = Me.DsPedido.Producto
        Me.cmbProducto.DisplayMember = "Descripcion"
        Me.cmbProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProducto.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbProducto.ItemHeight = 13
        Me.cmbProducto.Location = New System.Drawing.Point(128, 64)
        Me.cmbProducto.Name = "cmbProducto"
        Me.cmbProducto.Size = New System.Drawing.Size(240, 21)
        Me.cmbProducto.TabIndex = 2
        Me.cmbProducto.ValueMember = "Producto"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(11, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Producto:"
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=Digital5000;initial catalog=Sigamet;persist security info=False;user " & _
    "id=sa;workstation id=FHURTADO;packet size=4096"
        Me.SqlConnection.FireInfoMessageEventOnUserErrors = False
        '
        'lbFPedido
        '
        Me.lbFPedido.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbFPedido.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFPedido.Location = New System.Drawing.Point(128, 8)
        Me.lbFPedido.Name = "lbFPedido"
        Me.lbFPedido.Size = New System.Drawing.Size(120, 24)
        Me.lbFPedido.TabIndex = 0
        Me.lbFPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbRuta
        '
        Me.lbRuta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lbRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRuta.Location = New System.Drawing.Point(128, 117)
        Me.lbRuta.Name = "lbRuta"
        Me.lbRuta.Size = New System.Drawing.Size(120, 24)
        Me.lbRuta.TabIndex = 4
        Me.lbRuta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(102, 13)
        Me.Label2.TabIndex = 101
        Me.Label2.Text = "Prioridad pedido:"
        '
        'cbPrioridad
        '
        Me.cbPrioridad.DataSource = Me.DsPedido.Prioridad
        Me.cbPrioridad.DisplayMember = "Descripcion"
        Me.cbPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPrioridad.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrioridad.ItemHeight = 13
        Me.cbPrioridad.Location = New System.Drawing.Point(128, 152)
        Me.cbPrioridad.Name = "cbPrioridad"
        Me.cbPrioridad.Size = New System.Drawing.Size(240, 21)
        Me.cbPrioridad.TabIndex = 5
        Me.cbPrioridad.ValueMember = "PrioridadPedido"
        '
        'cmdCTipoPedido
        '
        Me.cmdCTipoPedido.CommandText = "SELECT TipoPedido, Descripcion FROM TipoPedido WHERE (TipoPedido = 1)"
        Me.cmdCTipoPedido.Connection = Me.SqlConnection
        '
        'daTipoPedido
        '
        Me.daTipoPedido.SelectCommand = Me.cmdCTipoPedido
        '
        'cmdCProducto
        '
        Me.cmdCProducto.CommandText = "SELECT Producto, Descripcion FROM Producto WHERE (TipoProducto = 1)"
        Me.cmdCProducto.Connection = Me.SqlConnection
        '
        'daProducto
        '
        Me.daProducto.SelectCommand = Me.cmdCProducto
        '
        'cmdCPrioridad
        '
        Me.cmdCPrioridad.CommandText = "SELECT PrioridadPedido, Descripcion FROM PrioridadPedido"
        Me.cmdCPrioridad.Connection = Me.SqlConnection
        '
        'daPrioridad
        '
        Me.daPrioridad.SelectCommand = Me.cmdCPrioridad
        '
        'DataGridTextBoxColumn6
        '
        Me.DataGridTextBoxColumn6.Format = ""
        Me.DataGridTextBoxColumn6.FormatInfo = Nothing
        Me.DataGridTextBoxColumn6.HeaderText = "Sabado"
        Me.DataGridTextBoxColumn6.MappingName = "Sabado"
        Me.DataGridTextBoxColumn6.NullText = ""
        Me.DataGridTextBoxColumn6.Width = 78
        '
        'DataGridTextBoxColumn7
        '
        Me.DataGridTextBoxColumn7.Format = ""
        Me.DataGridTextBoxColumn7.FormatInfo = Nothing
        Me.DataGridTextBoxColumn7.HeaderText = "Domingo"
        Me.DataGridTextBoxColumn7.MappingName = "Domingo"
        Me.DataGridTextBoxColumn7.NullText = ""
        Me.DataGridTextBoxColumn7.Width = 78
        '
        'lbProgramado
        '
        Me.lbProgramado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbProgramado.Location = New System.Drawing.Point(8, 432)
        Me.lbProgramado.Name = "lbProgramado"
        Me.lbProgramado.Size = New System.Drawing.Size(288, 16)
        Me.lbProgramado.TabIndex = 103
        Me.lbProgramado.Text = "El cliente tiene un pedido programado para el dia "
        '
        'lbDiaPrograma
        '
        Me.lbDiaPrograma.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDiaPrograma.ForeColor = System.Drawing.Color.Firebrick
        Me.lbDiaPrograma.Location = New System.Drawing.Point(296, 432)
        Me.lbDiaPrograma.Name = "lbDiaPrograma"
        Me.lbDiaPrograma.Size = New System.Drawing.Size(312, 16)
        Me.lbDiaPrograma.TabIndex = 104
        Me.lbDiaPrograma.Text = "Martes 12 de Noviembre del 2002"
        '
        'grdHorario
        '
        Me.grdHorario.CaptionVisible = False
        Me.grdHorario.DataMember = ""
        Me.grdHorario.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdHorario.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.grdHorario.Location = New System.Drawing.Point(8, 332)
        Me.grdHorario.Name = "grdHorario"
        Me.grdHorario.ReadOnly = True
        Me.grdHorario.Size = New System.Drawing.Size(600, 92)
        Me.grdHorario.TabIndex = 105
        Me.grdHorario.TableStyles.AddRange(New System.Windows.Forms.DataGridTableStyle() {Me.DataGridTableStyle1})
        '
        'DataGridTableStyle1
        '
        Me.DataGridTableStyle1.DataGrid = Me.grdHorario
        Me.DataGridTableStyle1.GridColumnStyles.AddRange(New System.Windows.Forms.DataGridColumnStyle() {Me.colLunes, Me.colMartes, Me.colMiercoles, Me.colJueves, Me.colViernes, Me.colSabado, Me.colDomingo})
        Me.DataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.DataGridTableStyle1.MappingName = "Horarios"
        '
        'colLunes
        '
        Me.colLunes.Format = ""
        Me.colLunes.FormatInfo = Nothing
        Me.colLunes.HeaderText = "Lunes"
        Me.colLunes.MappingName = "Lunes"
        Me.colLunes.Width = 80
        '
        'colMartes
        '
        Me.colMartes.Format = ""
        Me.colMartes.FormatInfo = Nothing
        Me.colMartes.HeaderText = "Martes"
        Me.colMartes.MappingName = "Martes"
        Me.colMartes.Width = 80
        '
        'colMiercoles
        '
        Me.colMiercoles.Format = ""
        Me.colMiercoles.FormatInfo = Nothing
        Me.colMiercoles.HeaderText = "Miércoles"
        Me.colMiercoles.MappingName = "Miércoles"
        Me.colMiercoles.Width = 80
        '
        'colJueves
        '
        Me.colJueves.Format = ""
        Me.colJueves.FormatInfo = Nothing
        Me.colJueves.HeaderText = "Jueves"
        Me.colJueves.MappingName = "Jueves"
        Me.colJueves.Width = 80
        '
        'colViernes
        '
        Me.colViernes.Format = ""
        Me.colViernes.FormatInfo = Nothing
        Me.colViernes.HeaderText = "Viernes"
        Me.colViernes.MappingName = "Viernes"
        Me.colViernes.Width = 80
        '
        'colSabado
        '
        Me.colSabado.Format = ""
        Me.colSabado.FormatInfo = Nothing
        Me.colSabado.HeaderText = "Sábado"
        Me.colSabado.MappingName = "Sábado"
        Me.colSabado.Width = 80
        '
        'colDomingo
        '
        Me.colDomingo.Format = ""
        Me.colDomingo.FormatInfo = Nothing
        Me.colDomingo.HeaderText = "Domingo"
        Me.colDomingo.MappingName = "Domingo"
        Me.colDomingo.Width = 80
        '
        'lblGrdCaption
        '
        Me.lblGrdCaption.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrdCaption.Location = New System.Drawing.Point(8, 308)
        Me.lblGrdCaption.Name = "lblGrdCaption"
        Me.lblGrdCaption.Size = New System.Drawing.Size(596, 16)
        Me.lblGrdCaption.TabIndex = 106
        Me.lblGrdCaption.Text = "lblGrdCaption"
        '
        'lblFinRuta
        '
        Me.lblFinRuta.AutoSize = True
        Me.lblFinRuta.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinRuta.ForeColor = System.Drawing.Color.Firebrick
        Me.lblFinRuta.Location = New System.Drawing.Point(287, 122)
        Me.lblFinRuta.Name = "lblFinRuta"
        Me.lblFinRuta.Size = New System.Drawing.Size(198, 13)
        Me.lblFinRuta.TabIndex = 107
        Me.lblFinRuta.Text = "Ya se ralizó el fin de día de la ruta."
        Me.lblFinRuta.Visible = False
        '
        'Pedido
        '
        Me.AcceptButton = Me.btnAceptar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(616, 462)
        Me.Controls.Add(Me.lblFinRuta)
        Me.Controls.Add(Me.lblGrdCaption)
        Me.Controls.Add(Me.grdHorario)
        Me.Controls.Add(Me.lbDiaPrograma)
        Me.Controls.Add(Me.lbProgramado)
        Me.Controls.Add(Me.cbPrioridad)
        Me.Controls.Add(Me.lbRuta)
        Me.Controls.Add(Me.lbFPedido)
        Me.Controls.Add(Me.cmbProducto)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me.txtFCompromiso)
        Me.Controls.Add(Me.cmbTipoPedido)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Pedido"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Pedido - [Cliente]             "
        CType(Me.DsPedido, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdHorario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Me.txtObservaciones.Focus()

        ValidaFechaCompromiso()

        Cursor = Cursors.WaitCursor
        Dim Transaccion As SqlClient.SqlTransaction
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing
        Dim Existe As Integer = Nothing
        Dim Existe2 As Integer = Nothing
        Dim SiGrabo As Boolean
        Dim EliminoPrograma As String = Nothing
        Dim RespetoPrograma As String = Nothing

        ''ValidaFechaCompromiso()

        If noGrabar = False Then
            Try
                SqlConnection.Close()
                SqlConnection.ConnectionString = GLOBAL_ConString
                SqlConnection.Open()
            Catch dataException As Exception
                MsgBox(dataException.Message, MsgBoxStyle.OkOnly, "Mensaje de sistema")
            End Try

            cmdInsert.Connection = SqlConnection
            Transaccion = SqlConnection.BeginTransaction
            cmdInsert.CommandTimeout = 100
            cmdInsert.Transaction = Transaccion
            SiGrabo = False
            Try
                If _Tipo = 0 Then

                    'JAGD 31/12/2004 Se incluye el mensaje de credito excedido en las observaciones de pedido para imprimir en la remisión
                    If Len(Trim(_mensajeCreditoExcedido)) > 0 Then
                        txtObservaciones.Text = txtObservaciones.Text & " " & _mensajeCreditoExcedido
                    End If

                    cmdInsert.CommandType = CommandType.StoredProcedure
                    cmdInsert.CommandText = "sp_InsertaPedido"
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                    cmdInsert.Parameters.Add("@TipoPedido", SqlDbType.Int).Value = CType(cmbTipoPedido.SelectedValue, Integer)
                    cmdInsert.Parameters.Add("@Producto", SqlDbType.Int).Value = CType(cmbProducto.SelectedValue, Integer)
                    cmdInsert.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = txtFCompromiso.Value.Date
                    cmdInsert.Parameters.Add("@PrioridadPedido", SqlDbType.Int).Value = CType(cbPrioridad.SelectedValue, Integer)
                    cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                    cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                    cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
                    'cmdInsert.Parameters.Add("@TipoCargo", SqlDbType.Int).Value = 1
                    cmdInsert.ExecuteNonQuery()
                Else

                    'JAGD 31/12/2004 Se incluye el mensaje de credito excedido en las observaciones de pedido para imprimir en la remisión
                    If Len(Trim(_mensajeCreditoExcedido)) > 0 Then
                        txtObservaciones.Text = txtObservaciones.Text.Trim & " " & _mensajeCreditoExcedido
                    End If

                    cmdInsert.CommandType = CommandType.StoredProcedure
                    cmdInsert.CommandText = "sp_ModificaPedido"
                    cmdInsert.Parameters.Clear()
                    cmdInsert.Parameters.Add("@FCompromiso", SqlDbType.DateTime).Value = txtFCompromiso.Value.Date
                    cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Text).Value = txtObservaciones.Text
                    cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = _Celula
                    cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = _Pedido
                    cmdInsert.Parameters.Add("@Anio", SqlDbType.Int).Value = _Anio
                    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                    cmdInsert.ExecuteNonQuery()
                End If

                '***************************************************************
                'EL SIGUIENTE BLOQUE FUE QUITADO YA QUE LA GENERACION DEL
                'SIGUIENTE PEDIDO PROGRAMADO SE HACE DENTRO DE LOS STORED PROCS.
                '***************************************************************
                'If _TienePrograma Then
                '    'EJECUTAR EL SP PARA VERIFICAR LAS PROGRAMACIONES AL MOMENTO
                '    cmdInsert.CommandType = CommandType.StoredProcedure
                '    cmdInsert.CommandText = "spPROGGeneraSiguientePedidoCliente"
                '    cmdInsert.Parameters.Clear()
                '    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                '    'cmdInsert.Parameters.Add("@TipoEspecial", SqlDbType.Int).Value = 2
                '    cmdInsert.ExecuteNonQuery()

                '    'Else
                '    '    'EJECUTAR EL SP PARA GENERAR SU NUEVA PROGRAMACION
                '    '    cmdInsert.CommandType = CommandType.StoredProcedure
                '    '    cmdInsert.CommandText = "sp_InsertaProgramacionEspecial"
                '    '    cmdInsert.Parameters.Clear()
                '    '    cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = _Cliente
                '    '    cmdInsert.ExecuteNonQuery()
                'End If

                Transaccion.Commit()
                SiGrabo = True

            Catch er As Exception
                Try
                    Transaccion.Rollback()
                Catch ioEx As Exception
                End Try
                MsgBox(er.Message, MsgBoxStyle.Critical)
                SiGrabo = False
            Finally
                Cursor = Cursors.WaitCursor
                If SiGrabo Then
                    Me.Close()
                End If
            End Try
        End If

    End Sub

    'Funcion para encontrar la referencia de la celda del grid de pedidos
    Public Function getCelda() As Integer
        Return _numeroCelda
    End Function

    'Funcion para referenciar la celda del grid de pedidos
    Public Function setCelda(ByVal numVal As Integer) As Integer
        _numeroCelda = numVal
    End Function

    Private Sub Pedido_Closed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        SqlConnection.Close()
    End Sub

    Private Function fncHorario(ByVal CP As String, ByVal Ruta As Integer, ByVal Colonia As Integer) As DataTable
        Dim retTable As New DataTable("Horarios")
        Dim cmdSelect As New SqlClient.SqlCommand()
        cmdSelect.CommandText = "sp_HorarioRutaGridV3"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Connection = CnnSigamet
        cmdSelect.Parameters.Add("@Ruta", SqlDbType.Int).Value = Ruta
        cmdSelect.Parameters.Add("@Colonia", SqlDbType.Int).Value = Colonia
        'cmdSelect.Parameters.Add("@CP", SqlDbType.Char, 5).Value = CP
        Dim daHorario As New SqlClient.SqlDataAdapter(cmdSelect)
        Try
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If
            daHorario.Fill(retTable)
        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error no." & CStr(ex.Number) & Chr(13) & _
                                                ex.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error." & Chr(13) & _
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()
            daHorario.Dispose()
        End Try
        Return retTable
    End Function

    Private Sub Pedido_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.txtFCompromiso.MinDate = Date.Today

        ConsultaFinDeDia()
    End Sub

    Private Sub ConsultaFinDeDia()
        Dim cmdSelect As New SqlClient.SqlCommand()
        cmdSelect.CommandText = "spConsultaFinDeDia"
        cmdSelect.CommandType = CommandType.StoredProcedure
        cmdSelect.Connection = CnnSigamet
        cmdSelect.Parameters.Add("@Ruta", SqlDbType.SmallInt).Value = _Ruta

        Dim drFinDia As SqlClient.SqlDataReader

        Try
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If

            drFinDia = cmdSelect.ExecuteReader()
            drFinDia.Read()
            If CType(drFinDia("Llamada"), Integer) <> 0 Then
                lblFinRuta.Visible = True
            End If


        Catch ex As SqlClient.SqlException
            MessageBox.Show("Error no." & CStr(ex.Number) & Chr(13) & _
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error." & Chr(13) & _
                                                ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
            cmdSelect.Dispose()

        End Try
    End Sub

    Private Sub ValidaFechaCompromiso()
        Dim fechas As String = ""
        Dim fechaFinal As Date

        fechas = fechaHoy.Day & "/" & fechaHoy.Month & "/" & fechaHoy.Year
        fechaFinal = CType(fechas, Date)

        If (CType(txtFCompromiso.Value, Date) < CType(fechas, Date)) Or (CType(txtFCompromiso.Value, Date) > CType(fechaFinal.AddMonths(4), Date)) Then
            MsgBox("La fecha compromiso no puede ser menor a la día de hoy o mayor a cuatro meses. Verifique.", MsgBoxStyle.Exclamation, "Mensaje del sistema")
            txtFCompromiso.Select()
            noGrabar = True
            Cursor = Cursors.Default
        Else
            noGrabar = False
        End If
    End Sub

    Private Sub txtFCompromiso_Validated(sender As System.Object, e As System.EventArgs) Handles txtFCompromiso.Validated
        ValidaFechaCompromiso()
    End Sub
End Class
