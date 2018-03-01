Imports System.Data.SqlClient
Imports System.Configuration

Module Main

    'Texis Para obtener el nombre de  la estacion o planta
    Public GLOBAL_Estacion As String

    Public oSeguridad As SigaMetClasses.cSeguridad

    Public GLOBAL_ConString As String
    Public GLOBAL_Servidor As String
    Public GLOBAL_Database As String
    Public GLOBAL_Usuario As String
    Public GLOBAL_Password As String
    Public GLOBAL_FechaServidor As Date
    Public GLOBAL_UsaRutaReportesLocal As Boolean
    Public GLOBAL_RutaReportes As String
    Public GLOBAL_UsuarioNombre As String
    Public GLOBAL_UsuarioNT As String
    Public GLOBAL_SeguridadNT As Boolean
    Public GLOBAL_Remoto As Boolean
    Public GLOBAL_Celula As Byte
    Public GLOBAL_CelulaDescripcion As String
    Public GLOBAL_CelulaComercial As Boolean
    Public GLOBAL_CelulaAdmin As Boolean
    Public GLOBAL_Empleado As Integer
    Public GLOBAL_EmpleadoNombre As String
    'Para manejar clientes de portatil 28/09/2004
    Public GLOBAL_ManejarClientesPortatil As Boolean
    'Para mostrar y activar el botón AT más cercano
    Public GLOBAL_AplicaATMasCercano As Boolean
    'Para activar/desactivar la funcialidad de clientes hijos
    Public GLOBAL_AplicaAdministracionEdificios As Boolean
    'Si está activa la adm. de edificios, aquí se guarda el valor del ramo
    Public GLOBAL_RamoClienteAdmEdificios As String
    '09-10-2006, guardar el valor de la clave del campo de edificios administrados
    Public GLOBAL_ClaveRamoClienteAdmEdificios As Short
    '11-10-2006
    Public GLOBAL_AdmEdificiosLiquidacionCredito As Boolean

    'Habilita la posibilidad de cambio de fecha compromiso --05/10/2004
    Public GLOBAL_AplicaCambioFechaCompromiso As Boolean
    'almacena la clave "Credito autorizado" en cartera
    Public GLOBAL_ClaveCreditoAutorizado As Byte
    'Almacena el estado de la validación de líneas de cédito para la liquidación
    Public GLOBAL_AplicaValidacionCredito As Boolean

    'Habilita la captura de saldos a favor en la liquidación
    Public GLOBAL_SaldoAFavorLiquidacion As Boolean

    'Almacena el motivo de llamada de confirmación de boletin
    Public GLOBAL_MotivoConfirmacionBoletin As Short
    'Almacena el motivo de llamada de boletin al operador
    Public GLOBAL_MotivoBoletinOperador As Short

    'liquidación con remisión mhv 12/11/2004
    'Almacena la serie de la remision para la liquidacion
    Public Global_SerieRemision As String

    'Almacena el tipo de liquidacion con o sin remision
    Public Global_TipoLiquidacion As String

    'Liquidaciòn: Validación de serie en la remisión
    Public GLOBAL_ValidaSerieRemision As Boolean

    'Almacena la zona económica por defecto para carga de precios
    Public GLOBAL_ZonaEconomicaDefault As String

    'Almacena el mensaje a mostrar cuando se excedió el importe a crédito
    Public GLOBAL_MensajeCreditoExcedido As String

    'Habilita/Inhabilita la funcionalida de avance de programaciones
    Public GLOBAL_AvanzaProgramacion As Boolean
    'Habilita/Inhabilita que se fuerze la captura de observaciones de programación
    Public GLOBAL_CapturaObservacionesInactivacionProg As Boolean
    'Habilita la seguridad de reportes
    Public GLOBAL_SeguridadReportes As Boolean

    'Habilita la selección multiple en el boletín
    Public GLOBAL_ConfirmaBoletinGrupo As Boolean

    'Habilita el control de ventas multinivel
    Public GLOBAL_VentasMultinivel As Boolean

    'Habilita la asignación de promotores a los clientes
    Public GLOBAL_GruposPromocionales As Boolean
    Public GLOBAL_PagoPromocionAPromotor As Boolean

    'Registro de inicio de sesión
    Public GLOBAL_REGISTRO_INICIO_SESION As Boolean
    Public GLOBAL_MOTIVO_LLAMADA_INICIOSESI As Integer
    Public GLOBAL_MOTIVO_LLAMADA_FIN_SESION As Integer

    Public GLOBAL_Precio As Decimal
    Public GLOBAL_PrecioToluca As Decimal
    Public GLOBAL_PrecioAnterior As Decimal
    Public GLOBAL_PrecioAnteriorToluca As Decimal

    'Corporativo y sucursal
    Public GLOBAL_Corporativo As Short
    Public GLOBAL_Sucursal As Short

    Public _Nivel As String
    Public _numeroCelda As Integer

    Public CnnSigamet As New SqlConnection()

    'Predeterminados
    Public PRED_TipoCliente As Byte
    Public PRED_TipoClienteDescripcion As String
    Public PRED_ClasificacionCliente As Byte
    Public PRED_ClasificacionClienteDescripcion As String
    Public PRED_RamoCliente As Byte
    Public PRED_RamoClienteDescripcion As String
    Public PRED_Cartera As Byte
    Public PRED_CarteraDescripcion As String
    Public PRED_OrigenCliente As Byte
    Public PRED_Status As String
    Public PRED_StatusCalidad As String
    Public PRED_TipoFactura As Byte
    Public PRED_TipoFacturaDescripcion As String

    'Configuración del CallCenter
    Public CONFIG_ColorFondo As Color = Color.Gainsboro
    Public CONFIG_ColorFondoAlterno As Color = Color.Gainsboro
    Public CONFIG_BotonesGrandes As Boolean = True
    Public CONFIG_AbreNotasClienteAuto As Boolean = False

    'UserInfo    
    Public GLOBAL_UserInfo As SigaMetClasses.cUserInfo

    'Para el control del alta de calles y colonias
    Public GLOBAL_AltaCalleColonia As Boolean

    'Control de contrato firmado para sigamet corporativo
    Public GLOBAL_CapturaContratoFirmado As Boolean
    Public GLOBAL_CapturaFactorComision As Boolean

    'Para permitir cambios a clientes inactivos
    Public GLOBAL_ManejoClientesInactivos As Boolean

    'Habilita la descarga de datos de la base de datos del UDS
    Public GLOBAL_DescargaUDS As Boolean



    'Web services
    Public GLOBAL_ConsultaPedidosWebURL As String
    Public GLOBAL_TiempoActualizacionConsultaPedidos As Integer

    Public GLOBAL_OrigenClientePortal As Integer
    Public GLOBAL_ObservacionesCancelacion As String
    Public GLOBAL_MotivoLlamadaCancelacionWeb As Integer

    Public GLOBAL_URLWebserviceBoletin As String

    'Activación del módulo de quejas
    Public GLOBAL_ModuloQuejas As Boolean

    'Temporal, activación de la nueva versión de liquidación
    Public GLOBAL_NuevaVersionLiquidacion As Boolean


    'RFA 24/07/09
    'Cadena de conexión para exportación de cargos
    Public GLOBAL_CadenaConexionExport As String

    Public GLOBAL_UsuarioReportes As String
    Public GLOBAL_PasswordReportes As String


    '26-01-2007
    'Selección del tipo de crédito
    Public GLOBAL_SeleccionTipoCreditoLiq As Boolean
    'Liquidación Obsequios
    Public GLOBAL_LiquidacionObsequios As Boolean
    'Validacion final liquidacion
    Public GLOBAL_ValidacionFinalLiquidacion As Boolean
    'Descuento por pronto pago
    Public GLOBAL_DescuentoProntoPago As Boolean

    'Validar límite de crédito liquidación
    Public GLOBAL_ValidacionLimiteCredito As Boolean

    'Indicará si se agrupan los pedidos en la liquidación
    Public GLOBAL_AgrupacionPedidosLiquidacion As Boolean

    '02/03/2012 #CASALA - Indicará si para la carga de rutas en frmcallcenter se hace por las celulas de usuario
    'Y en la busqueda de clientes solo se busca sobre los clientes de la celula del usuario
    Public GLOBAL_CelulasUsuario As Boolean
    Public GLOBAL_dtCelulasUsuario As New DataTable("Celula")


    'Nombre de la empresa en la que está corriendo la aplicación
    Public GLOBAL_NombreEmpresa As String

    'Consulta de información por medio de webservices
    Public GLOBAL_UsarSigametServices As Boolean
    'Determina la versión de movilgas
    Public GLOBAL_VersionMovilGas As Integer

    Public GLOBAL_MGConnectionString As String

    'Indica si la venta es principalmente portatil
    Public GLOBAL_PrioridadPortatil As Boolean

    'Define si se muestra la RutaBoletin en la pantalla de Boletín
    Public Global_MuestraRutaBoletin As Boolean

    'Define si muestra la opción para reasignar pedidos boletinados en la pantalla de Boletin
    Public Global_MuestraReasignacionPedidos As Boolean

    'Define si muestra la opción para deshabilitar la validación de posición GPS al boletinar pedidos
    Public Global_MuestraNoValidarGPS As Boolean

    'Define si muestra el botón para georeferenciar
    Public Global_HabilitaBtnGeoReferencia As Boolean
   

    Public Function PuedeCerrar(ByVal Celula As Integer) As Integer
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim ClienteGeneral As Integer
        Dim Registros As Integer

        cmdInsert.Connection = CnnSigamet

        Try
            AbreConexion()
            cmdInsert.CommandTimeout = 200
            cmdInsert.CommandType = CommandType.Text
            cmdInsert.CommandText = " Select IsNull(Cliente,0) as Cliente from ClienteVentaPublico where Celula=@Celula "
            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Celula", SqlDbType.Char).Value = GLOBAL_Celula
            rdrInsert = cmdInsert.ExecuteReader
            ClienteGeneral = 0

            While rdrInsert.Read
                ClienteGeneral = CType(rdrInsert("Cliente"), Integer)
            End While
            rdrInsert.Close()

            cmdInsert.CommandType = CommandType.Text
            cmdInsert.CommandText = " Select Count(*) as Registro from Nota N INNER JOIN Pedido P ON P.Pedido = N.Pedido AND P.AñoPed = N.AñoPed AND N.Celula = P.Celula " & _
                                    " where N.Status='LIQUIDADA' and N.FAlta >'23/06/2003' and P.TipoPedido=3 and P.Celula=@Celula and N.StatusConciliacion is null " & _
                                    " and P.Cliente=@ClienteGeneral "

            cmdInsert.Parameters.Clear()
            cmdInsert.Parameters.Add("@Celula", SqlDbType.Char).Value = GLOBAL_Celula
            cmdInsert.Parameters.Add("@ClienteGeneral", SqlDbType.Char).Value = ClienteGeneral
            rdrInsert = cmdInsert.ExecuteReader
            Registros = 0

            While rdrInsert.Read
                Registros = CType(rdrInsert("Registro"), Integer)
            End While
            rdrInsert.Close()

            Return Registros

        Catch ex As Exception
            Registros = 0
            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CierraConexion()
            cmdInsert.Dispose()


        End Try

    End Function

    Public Function LlamadasCliente(ByVal Cliente As Integer) As DataTable

        Dim cmd As New SqlCommand("spCCConsultaLlamadasCliente", CnnSigamet)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable("Llamada")

        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
        End With

        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw ex

        Finally
            da.Dispose()
            cmd.Dispose()

        End Try
    End Function



    Public Function TotalRampac(ByVal AñoAtt As Short, ByVal Folio As Integer) As Integer
        Dim strQuery As String = _
        "SELECT Count(*) From Rampac Where AñoAtt = " & AñoAtt.ToString & " And Folio = " & Folio.ToString
        Dim cmd As New SqlCommand(strQuery, CnnSigamet)
        cmd.CommandType = CommandType.Text
        Try
            AbreConexion()
            Dim Resultado As Integer = CType(cmd.ExecuteScalar, Integer)
            Return Resultado
        Catch ex As Exception
            Throw ex
            Return 0
        Finally
            cmd.Dispose()
            CierraConexion()
        End Try
    End Function

    Public Sub AbreConexion()
        If Not CnnSigamet Is Nothing Then
            If CnnSigamet.State = ConnectionState.Closed Then
                CnnSigamet.Open()
            End If
        End If
    End Sub

    Public Sub CierraConexion()
        If Not CnnSigamet Is Nothing Then
            If CnnSigamet.State = ConnectionState.Open Then
                CnnSigamet.Close()
            End If
        End If
    End Sub

    Public Sub Main()
        'Splash
        Dim oSplash As New frmSplash()
        oSplash.ShowDialog()


        'Dim oLogin As New SigaMetClasses.Seguridad(1, Application.ProductVersion, "LightGray")
        Dim frmAcceso As New SigametSeguridad.UI.Acceso(Application.StartupPath + "\Default.Seguridad y administracion.exe.config", _
            True, 1, New Bitmap(Application.StartupPath + "\Boletin.ico"))

        If frmAcceso.ShowDialog = DialogResult.OK Then

            GLOBAL_ConString = frmAcceso.CadenaConexion


            Dim obLogin As New SigaMetClasses.Seguridad(4, frmAcceso.CadenaConexion, frmAcceso.Usuario.IdUsuario, frmAcceso.Usuario.ClaveDesencriptada)

            'Para los componentes de portátil
            PortatilClasses.Globals.GetInstance.ConfiguraModulo(frmAcceso.Usuario.IdUsuario, frmAcceso.Usuario.ClaveDesencriptada, _
            frmAcceso.CadenaConexion, frmAcceso.Usuario.Corporativo, _
            frmAcceso.Usuario.Sucursal)


            '****RFA 24/07/09
            'Para usar el usuario y password del usuario de reportes 
            consultaParametrosConexionReportesEspeciales(SigaMetClasses.DataLayer.Conexion)
            GLOBAL_CadenaConexionExport = "Data Source=" & CStr(obLogin.Parametros("NombreServidorReportes")) & ";Initial Catalog=" & _
                CStr(obLogin.Parametros("NombreBaseDatosReportes")) & ";UID=" & GLOBAL_UsuarioReportes & _
                ";Password=" & GLOBAL_PasswordReportes & ";"




            'TODO Tomar la cadena de conexión de la nueva estructura de seguridad
            'Dim oLogin As New SigaMetClasses.Seguridad(1, Application.ProductVersion, "LightGray")
            Dim oLogin As New SigaMetClasses.Seguridad(1, GLOBAL_ConString, frmAcceso.Usuario.IdUsuario, frmAcceso.Usuario.ClaveDesencriptada)

            GLOBAL_FechaServidor = SigaMetClasses.FechaServidor.Date

            CnnSigamet.ConnectionString = GLOBAL_ConString

            'IMPLEMENTACIÓN DEL UpDATER (ACTUALIZACION AUTOMÁTICA) 14/09/2004
            Dim updateSys As New SIGAMETSecurity.Updater(1, Application.ProductVersion, Application.StartupPath, GLOBAL_ConString)
            If updateSys.Desactualizado = True Then
                'Necesita actualizarse
                Application.Exit()
                Exit Sub
            End If

            GLOBAL_Usuario = oLogin.UserID
            GLOBAL_Password = oLogin.Password
            GLOBAL_Servidor = oLogin.Servidor
            GLOBAL_Database = oLogin.BaseDatos

            GLOBAL_Corporativo = oLogin.Corporativo
            GLOBAL_Sucursal = oLogin.Sucursal

            Try
                oSeguridad = New SigaMetClasses.cSeguridad(GLOBAL_Usuario, 1)
            Catch ex As Exception
                MessageBox.Show("Ha ocurrido un error: " & vbCrLf & _
                    ex.Message & vbCrLf & _
                    ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            GLOBAL_UsuarioNombre = oLogin.UsuarioNombre
            GLOBAL_Celula = oLogin.Celula
            GLOBAL_CelulaDescripcion = oLogin.CelulaDescripcion
            GLOBAL_CelulaComercial = oLogin.CelulaComercial
            GLOBAL_CelulaAdmin = oLogin.CelulaAdmin
            GLOBAL_UsuarioNT = oLogin.UsuarioNT
            GLOBAL_Empleado = oLogin.Empleado
            GLOBAL_EmpleadoNombre = oLogin.EmpleadoNombre
            GLOBAL_Remoto = oLogin.Remoto

            If GLOBAL_Remoto Then
                GLOBAL_UserInfo = New SigaMetClasses.cUserInfo(GLOBAL_Usuario, GLOBAL_Password, GLOBAL_Database, GLOBAL_Servidor)
            End If

            Try
                GLOBAL_UsaRutaReportesLocal = CType(System.Configuration.ConfigurationManager.AppSettings("UsaRutaReportesLocal"), Boolean)

                If GLOBAL_UsaRutaReportesLocal Then
                    GLOBAL_RutaReportes = System.Configuration.ConfigurationManager.AppSettings("RutaReportesLocal")
                Else
                    'GLOBAL_RutaReportes = CType(oLogin.Parametros("RutaReportes"), String).Trim
                    GLOBAL_RutaReportes = _
                        CType(SigametSeguridad.Seguridad.Parametros(1, CType(GLOBAL_Corporativo, Byte), _
                        CType(GLOBAL_Sucursal, Byte)).ValorParametro("RutaReportesW7"), String)
                End If
            Catch
                GLOBAL_RutaReportes = _
                        CType(SigametSeguridad.Seguridad.Parametros(1, CType(GLOBAL_Corporativo, Byte), _
                        CType(GLOBAL_Sucursal, Byte)).ValorParametro("RutaReportesW7"), String)
                'GLOBAL_RutaReportes = CType(oLogin.Parametros("RutaReportes"), String).Trim                
            End Try            
            GLOBAL_SeguridadReportes = False

            Try
                'Se agregó el 28/09/2004
                GLOBAL_ManejarClientesPortatil = CType(oLogin.Parametros("ManejarClientesPortatil"), Boolean)
                'Se agregó el 29/09/2004
                GLOBAL_AplicaATMasCercano = CType(oLogin.Parametros("ActivarATMasCercano"), Boolean)
                'Se(agregó) el 29/09/2004
                GLOBAL_AplicaAdministracionEdificios = CType(oLogin.Parametros("AplicaAdmEdificios"), Boolean)
                'Se(agregó) el 29/09/2004 para manejar el ramo de los edificios administrados
                If GLOBAL_AplicaAdministracionEdificios Then
                    GLOBAL_RamoClienteAdmEdificios = CType(oLogin.Parametros("RamoAdmEdificios"), String).Trim
                    GLOBAL_ClaveRamoClienteAdmEdificios = CType(oLogin.Parametros("ClaveRamoAdmEdificios"), Short)
                    GLOBAL_AdmEdificiosLiquidacionCredito = CType(oLogin.Parametros("LiqCreditoAdmEdificios"), Boolean)
                End If

                GLOBAL_AplicaCambioFechaCompromiso = CType(oLogin.Parametros("AplicaCambioFechaCompromi"), Boolean)

                GLOBAL_AplicaValidacionCredito = CType(oLogin.Parametros("AplicaValidacionCredito"), Boolean)

                If GLOBAL_AplicaValidacionCredito Then
                    GLOBAL_ClaveCreditoAutorizado = CType(oLogin.Parametros("ClaveCreditoAutorizado"), Byte)
                End If

                GLOBAL_MotivoConfirmacionBoletin = CType(oLogin.Parametros("MotivoLlamadaConfirmacion"), Short)
                GLOBAL_MotivoBoletinOperador = CType(oLogin.Parametros("MotivoLlamadaBoletin"), Short)

                'almacena el manesaje para importe de crédito excedido
                GLOBAL_MensajeCreditoExcedido = CType(oLogin.Parametros("MensajeCreditoExcedido"), String)

                GLOBAL_ZonaEconomicaDefault = CType(oLogin.Parametros("ZonaEconomicaDefault"), String).Trim

                'TODO: Activación del avance de programación JAGD 15-01-2004
                GLOBAL_AvanzaProgramacion = CType(oSeguridad.TieneAcceso("AvanzaProgramacion"), Boolean)

                'TODO: Indica sí se fuerza la captura de observaciones de inactivación de la programación
                GLOBAL_CapturaObservacionesInactivacionProg = CType(oLogin.Parametros("CapturaObservacionesInact"), Boolean)

                'TODO: Valor de la seguridad de reportes
                GLOBAL_SeguridadReportes = CType(oLogin.Parametros("SeguridadReportes"), Boolean)

                'TODO: Multiselect del boletín
                GLOBAL_ConfirmaBoletinGrupo = CType(oLogin.Parametros("ConfirmaBoletinGrupo"), Boolean)

                'Validación de la serie de la remisión en la liquidación
                GLOBAL_ValidaSerieRemision = CType(oLogin.Parametros("ValidarSerieRemision"), Boolean)

                'Verifica si se pueden agregar calles y colonias
                GLOBAL_AltaCalleColonia = CBool(oLogin.Parametros("PermitirAltaCalles")) OrElse _
                    oSeguridad.TieneAcceso("AltaCalles") 'OrElse GLOBAL_Remoto

                'Verifica si es posible capturar el factor de comisión por cliente
                GLOBAL_CapturaFactorComision = CBool(oLogin.Parametros("CAPTURAFACTORCOMISION"))

                'Verifica si es posible marcar un contrato como "Firmado", sigamet corporativo
                GLOBAL_CapturaContratoFirmado = CBool(oLogin.Parametros("CAPTURACONTRATOFIRMADO"))

                'Captura de saldos a favor en liquidación
                GLOBAL_SaldoAFavorLiquidacion = CBool(oLogin.Parametros("SALDOAFAVORLIQUIDACION"))

                'Control parametrizado de ventas multinivel
                GLOBAL_VentasMultinivel = CBool(oLogin.Parametros("VentasMultinivel"))

                'registro de inicio y fin de sesión
                GLOBAL_REGISTRO_INICIO_SESION = CBool(oLogin.Parametros("REGISTRO_INICIO_SESION"))
                GLOBAL_MOTIVO_LLAMADA_INICIOSESI = CInt(oLogin.Parametros("MOTIVO_LLAMADA_INICIOSESI"))
                GLOBAL_MOTIVO_LLAMADA_FIN_SESION = CInt(oLogin.Parametros("MOTIVO_LLAMADA_FIN_SESION"))

                'Captura de clientes para grupos promocionales
                GLOBAL_GruposPromocionales = CBool(oLogin.Parametros("GRUPOS_PROMOCIONALES"))
                GLOBAL_PagoPromocionAPromotor = CType(oLogin.Parametros("PagoPromocionAPromotor"), Boolean)

                'Cambios a clientes inactivos
                GLOBAL_ManejoClientesInactivos = CBool(oLogin.Parametros("ManejoClientesInactivos"))

                'Descarga de datos de la base de datos para el uds
                GLOBAL_DescargaUDS = CBool(oLogin.Parametros("LiquidacionUDS"))

                'Consulta del URL parametrizado para el Web Service de pedidos web
                GLOBAL_ConsultaPedidosWebURL = CType(oLogin.Parametros("URL_CONSULTAPEDIDOWEB"), String)
                'Consulta del tiempo de actualización de la consulta de pedidos del portal
                GLOBAL_TiempoActualizacionConsultaPedidos = CType(oLogin.Parametros("TMP_ACTCONSULTAWB"), Integer)
                'Consulta del origen del cliente para los clientes del portal
                GLOBAL_OrigenClientePortal = CType(oLogin.Parametros("ORIGEN_CLIENTEPORTAL"), Integer)
                'Consulta de las observaciones a capturar en las llamadas por cancelación de pedidos del portal
                GLOBAL_ObservacionesCancelacion = CType(oLogin.Parametros("OBSERV_CNCPEDWB"), String)
                GLOBAL_MotivoLlamadaCancelacionWeb = CType(oLogin.Parametros("MTV_CNCPEDWB"), Integer)

                'Texis obtener planta o estacion 
                GLOBAL_Estacion = CType(oLogin.Parametros("Estacion"), String)

                'URL del servicio web
                GLOBAL_URLWebserviceBoletin = System.Configuration.ConfigurationManager.AppSettings.Get("URLServicioPedidos")

                'Activación del módulo de quejas
                GLOBAL_ModuloQuejas = CType(oLogin.Parametros("QUEJAS_Activo"), Boolean)

                'Temporal activación de la nueva versión de la liquidación
                GLOBAL_NuevaVersionLiquidacion = CType(oLogin.Parametros("NuevaVersionLiquidacion"), Boolean)

                'Selección del tipo de crédito en la liquidación
                GLOBAL_SeleccionTipoCreditoLiq = CType(oLogin.Parametros("SelTipoCreditoLiquidacion"), Boolean)
                'Activación de la liquidación de pedidos sin cargo
                GLOBAL_LiquidacionObsequios = CType(oLogin.Parametros("LiquidacionObsequios"), Boolean)
                'Activación de la validación final de pedidos de la liquidación
                GLOBAL_ValidacionFinalLiquidacion = CType(oLogin.Parametros("ValidacionFinLiquidacion"), Boolean)
                'Descuentos por pronto pago liquidación
                GLOBAL_DescuentoProntoPago = CType(oLogin.Parametros("LiqDescuentoProntoPago"), Boolean)

                'Validar límite de crédito en la liquidación
                GLOBAL_ValidacionLimiteCredito = CType(oLogin.Parametros("ValidarLimiteCreditoLiq"), Boolean)

                'Indicará si se agrupan pedidos en la liquidación
                GLOBAL_AgrupacionPedidosLiquidacion = CType(oLogin.Parametros("Liq_AgruparPedidos"), Boolean)

                '02/03/2012 #CASALA - Indicará si para la carga de rutas en frmcallcenter se hace por las celulas de usuario
                'Y en la busqueda de clientes solo se busca sobre los clientes de la celula del usuario
                GLOBAL_CelulasUsuario = CType(oLogin.Parametros("CelulasUsuario"), Boolean)

                'Nombre de la empresa en la que está corriendo la aplicación
                Dim _datosEmpresa As New NombreEmpresa.DatosEmpresa()
                GLOBAL_NombreEmpresa = _datosEmpresa.NombreEmpresa

                GLOBAL_UsarSigametServices = CType(oLogin.Parametros("UsarSigametServices"), Boolean)

                ''LUSATE MobileGas
                'GLOBAL_UsarMobileGas = CType(oLogin.Parametros("UsaMobileGas"), Boolean)
                'If GLOBAL_UsarMobileGas Then
                '    GLOBAL_MGConnectionString = CType(oLogin.Parametros("MGConnectionString"), String)
                'End If

                'LUSATE Define la prioridad para busqueda de cliente(se habilita el check de portatil en la busqueda)
                GLOBAL_PrioridadPortatil = CType(oLogin.Parametros("PrioridadPortatil"), Boolean)

                Global_MuestraRutaBoletin = CType(oLogin.Parametros("MuestraRutaBoletin"), Boolean)

                'Determina versión movilgas
                GLOBAL_VersionMovilGas = CType(oLogin.Parametros("VersionMovilGas"), Integer)
                If GLOBAL_VersionMovilGas = 1 Then
                    GLOBAL_MGConnectionString = CType(oLogin.Parametros("MGConnectionString"), String)
                End If


            Catch ArgEx As ArgumentException
                MessageBox.Show("El sistema no puede leer la información del parámetro.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            Catch ex As Exception
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

            End Try



            CargaPrecios()

            CargaPredeterminados()

            CargaConfiguracion()


            'Validacion de una u otra forma de liquidar segun parametros MHV 02/11/2004
            CargaparametrosLiquidacion()

            '02/03/2012 #CASALA - Carga un Datatable con las celulas del usuario
            CargaCelulasUsuario()

            'System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA
            System.Threading.Thread.CurrentThread.SetApartmentState(Threading.ApartmentState.STA)
            Dim eh As New CustomExceptionHandler()
            AddHandler Application.ThreadException, AddressOf eh.OnThreadException
            Application.Run(New frmPrincipal())
        End If
    End Sub
    '///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub consultaParametrosConexionReportesEspeciales(ByVal Connection As System.Data.SqlClient.SqlConnection)
        Dim alterConfig As SigaMetClasses.cConfig = New SigaMetClasses.cConfig(9)
        GLOBAL_UsuarioReportes = CStr(alterConfig.Parametros("UsuarioReportes")).Trim
        Dim oUsuarioReportes As New SigaMetClasses.cUserInfo()
        oUsuarioReportes.ConsultaDatosUsuarioReportes(GLOBAL_UsuarioReportes)
        GLOBAL_PasswordReportes = oUsuarioReportes.Password
    End Sub
    '//////////////////////////////////////////////////////////////////////////////////////////////////////////////

    Public Sub CargaparametrosLiquidacion()
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader

        cmdInsert.Connection = CnnSigamet
        cmdInsert.CommandTimeout = 30
        cmdInsert.CommandText = "select IsNull(Valor,'N') as TipoLiquidacion from Parametro where Modulo=1 and Parametro='REMISION' "

        Try
            AbreConexion()

            cmdInsert.Parameters.Clear()


            rdrInsert = cmdInsert.ExecuteReader
            rdrInsert.Read()

            Global_TipoLiquidacion = CType(rdrInsert("TipoLiquidacion"), String)

            rdrInsert.Close()

            cmdInsert.CommandText = "select IsNull(Valor,'A') as Serie from Parametro where Modulo=1 and Parametro='SERIEREMISION' "
            cmdInsert.Parameters.Clear()
            rdrInsert = cmdInsert.ExecuteReader
            rdrInsert.Read()

            Global_SerieRemision = CType(rdrInsert("Serie"), String)


            rdrInsert.Close()


        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error al cargar parametros de inicio para liquidación: " & ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmdInsert.Dispose()
            CierraConexion()
        End Try


    End Sub


    Public Sub CargaConfiguracion()
        Dim a, r, g, b As Integer
        a = CONFIG_ColorFondo.A
        r = CONFIG_ColorFondo.R
        g = CONFIG_ColorFondo.G
        b = CONFIG_ColorFondo.B

        a = CType(GetSetting("CallCenter", "CallCenter", "BackColor_A", a.ToString), Integer)
        r = CType(GetSetting("CallCenter", "CallCenter", "BackColor_R", r.ToString), Integer)
        g = CType(GetSetting("CallCenter", "CallCenter", "BackColor_G", g.ToString), Integer)
        b = CType(GetSetting("CallCenter", "CallCenter", "BackColor_B", b.ToString), Integer)

        CONFIG_ColorFondo = Color.FromArgb(a, r, g, b)

        a = CONFIG_ColorFondoAlterno.A
        r = CONFIG_ColorFondoAlterno.R
        g = CONFIG_ColorFondoAlterno.G
        b = CONFIG_ColorFondoAlterno.B

        a = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_A", a.ToString), Integer)
        r = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_R", r.ToString), Integer)
        g = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_G", g.ToString), Integer)
        b = CType(GetSetting("CallCenter", "CallCenter", "BackColorAlterno_B", b.ToString), Integer)

        CONFIG_ColorFondoAlterno = Color.FromArgb(a, r, g, b)

        CONFIG_BotonesGrandes = CType(GetSetting("CallCenter", "CallCenter", "BotonesGrandes", "1"), Boolean)
        CONFIG_AbreNotasClienteAuto = CType(GetSetting("CallCenter", "CallCenter", "AbreNotasClienteAuto", "0"), Boolean)

    End Sub

    Private Sub CargaPrecios()
        'TODO: Parametrizar carga de precios
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader

        cmdInsert.Connection = CnnSigamet
        cmdInsert.CommandTimeout = 30

        'TODO: La carga del precio 1 se realiza con base en el valor del parámetro ZonaEconomicaDefault 01/12/2004
        cmdInsert.CommandText = "Select IsNull(Precio,0) as Precio from Precio where ZonaEconomica = " & _
            GLOBAL_ZonaEconomicaDefault & " and Producto=1 and Vigente=1 "

        Try
            AbreConexion()

            cmdInsert.Parameters.Clear()


            rdrInsert = cmdInsert.ExecuteReader
            While rdrInsert.Read()

                GLOBAL_Precio = CType(rdrInsert("Precio"), Decimal)
                rdrInsert.NextResult()
            End While

            rdrInsert.Close()

            cmdInsert.CommandText = "Select IsNull(Precio,0) as PrecioToluca from Precio where ZonaEconomica=1 and Producto=11 and Vigente=1 "
            cmdInsert.Parameters.Clear()
            rdrInsert = cmdInsert.ExecuteReader
            While rdrInsert.Read()

                GLOBAL_PrecioToluca = CType(rdrInsert("PrecioToluca"), Decimal)
                rdrInsert.NextResult()
            End While


            rdrInsert.Close()
            'TODO: La carga del precio anterior 1 se realiza con base en el valor del parámetro
            'ZonaEconomicaDefault(1 / 12 / 2004)
            cmdInsert.CommandText = "Select IsNull(Precio,0) as Precio from Precio where ZonaEconomica = " & GLOBAL_ZonaEconomicaDefault & _
                " and Producto=1 and Secuencia in (Select Max(Secuencia) from Precio where ZonaEconomica=" & GLOBAL_ZonaEconomicaDefault & " and Producto = 1 and Vigente = 0)"
            cmdInsert.Parameters.Clear()
            rdrInsert = cmdInsert.ExecuteReader
            While rdrInsert.Read()

                GLOBAL_PrecioAnterior = CType(rdrInsert("Precio"), Decimal)
                rdrInsert.NextResult()
            End While

            rdrInsert.Close()

            cmdInsert.CommandText = "Select IsNull(Precio,0) as Precio from Precio where ZonaEconomica=1 and Producto=11 and Secuencia in (Select Secuencia-1 from Precio where ZonaEconomica=1 and Producto=11 and Vigente=1)"
            cmdInsert.Parameters.Clear()
            rdrInsert = cmdInsert.ExecuteReader
            While rdrInsert.Read()
                GLOBAL_PrecioAnteriorToluca = CType(rdrInsert("Precio"), Decimal)
                rdrInsert.NextResult()
            End While

            rdrInsert.Close()

        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error al leer los precios: " & ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            cmdInsert.Dispose()
            CierraConexion()
        End Try

    End Sub

    Private Sub CargaPredeterminados()
        'Carga los predeterminados para el CallCenter
        Dim daValores As New SqlClient.SqlDataAdapter()
        Dim rdrValores As SqlClient.SqlDataReader
        Dim cmdValores As New SqlClient.SqlCommand("Select Tabla, RTrim(Campo) as Campo, RTrim(Valor) as Valor from ValorPredeterminado where Tabla='Cliente' Order by Campo", CnnSigamet)

        daValores.SelectCommand = cmdValores

        Dim dtValores As New DataTable("Valores")
        daValores.Fill(dtValores)

        Dim rowValores As DataRow

        If dtValores.Rows.Count > 0 Then

            AbreConexion()

            For Each rowValores In dtValores.Rows
                Select Case rowValores("Campo").ToString()
                    Case "ClasificacionCliente"
                        Dim cmdDatos As New SqlClient.SqlCommand("Select Descripcion as ClasificacionCliente from ClasificacionCliente where ClasificacionCliente=@Clasificacion ", CnnSigamet)
                        cmdDatos.Parameters.Add("@Clasificacion", SqlDbType.Int).Value = CType(rowValores("Valor"), Integer)
                        PRED_ClasificacionCliente = CType(rowValores("Valor"), Byte)
                        rdrValores = cmdDatos.ExecuteReader()
                        rdrValores.Read()
                        PRED_ClasificacionClienteDescripcion = CType(rdrValores("ClasificacionCliente"), String).Trim
                        rdrValores.Close()
                        cmdDatos.Dispose()

                    Case "RamoCliente"
                        '02/12/2016 Se cambia la consulta de los ramos clientes para hacerla desde un stored procedure
                        'Dim cmdDatos As New SqlClient.SqlCommand("SELECT Descripcion as RamoCliente from RamoCliente where RamoCliente=@RamoCliente ", CnnSigamet)
                        Dim cmdDatos As New SqlClient.SqlCommand("spCCConsultaRamoCliente", CnnSigamet)
                        cmdDatos.Parameters.Add("@RamoCliente", SqlDbType.Int).Value = CType(rowValores("Valor"), Integer)
                        PRED_RamoCliente = CType(rowValores("Valor"), Byte)
                        rdrValores = cmdDatos.ExecuteReader()
                        rdrValores.Read()
                        'PRED_RamoClienteDescripcion = CType(rdrValores("RamoCliente"), String).Trim
                        PRED_RamoClienteDescripcion = CType(rdrValores("Descripcion"), String).Trim
                        rdrValores.Close()
                        cmdDatos.Dispose()

                    Case "Status"
                        PRED_Status = CType(rowValores("Valor"), String).Trim

                    Case "StatusCalidad"
                        PRED_StatusCalidad = CType(rowValores("Valor"), String).Trim

                    Case "TipoCliente"
                        Dim cmdDatos As New SqlClient.SqlCommand("SELECT Descripcion as TipoCliente from TipoCliente where TipoCliente=@TipoCliente ", CnnSigamet)
                        cmdDatos.Parameters.Add("@TipoCliente", SqlDbType.Int).Value = CType(rowValores("Valor"), Integer)
                        PRED_TipoCliente = CType(rowValores("Valor"), Byte)
                        rdrValores = cmdDatos.ExecuteReader()
                        rdrValores.Read()
                        PRED_TipoClienteDescripcion = CType(rdrValores("TipoCliente"), String).Trim
                        rdrValores.Close()
                        cmdDatos.Dispose()

                    Case "Cartera"
                        Dim cmdDatos As New SqlClient.SqlCommand("SELECT Descripcion as Cartera from Cartera where Cartera=@Cartera ", CnnSigamet)
                        cmdDatos.Parameters.Add("@Cartera", SqlDbType.TinyInt).Value = CType(rowValores("Valor"), Integer)
                        PRED_Cartera = CType(rowValores("Valor"), Byte)
                        rdrValores = cmdDatos.ExecuteReader()
                        rdrValores.Read()
                        PRED_CarteraDescripcion = CType(rdrValores("Cartera"), String).Trim
                        rdrValores.Close()
                        cmdDatos.Dispose()

                    Case "OrigenCliente"
                        PRED_OrigenCliente = CType(rowValores("Valor"), Byte)

                    Case "TipoFactura"
                        Dim cmdDatos As New SqlClient.SqlCommand("SELECT Descripcion as TipoFactura from TipoFactura where TipoFactura=@TipoFactura ", CnnSigamet)
                        cmdDatos.Parameters.Add("@TipoFactura", SqlDbType.TinyInt).Value = CType(rowValores("Valor"), Byte)
                        PRED_TipoFactura = CType(rowValores("Valor"), Byte)
                        rdrValores = cmdDatos.ExecuteReader()
                        rdrValores.Read()
                        PRED_TipoFacturaDescripcion = CType(rdrValores("TipoFactura"), String).Trim
                        rdrValores.Close()
                        cmdDatos.Dispose()


                End Select

            Next

            CierraConexion()

        End If
    End Sub


    '02/03/2012 #CASALA - Carga un Datatable con las celulas del usuario
    Private Sub CargaCelulasUsuario()
        'Carga los predeterminados para el CallCenter

        Dim da As SqlDataAdapter
        Dim cmdValores As New SqlClient.SqlCommand("SELECT  C.Celula, C.Descripcion AS CelulaDescripcion, S.Sucursal, S.Descripcion AS SucursalDescripcion FROM  UsuarioCelula UC JOIN Celula C ON UC.Celula = C.Celula LEFT JOIN Sucursal S ON C.Sucursal = S.Sucursal WHERE Usuario = '" + GLOBAL_Usuario + "'", CnnSigamet)

        da = New SqlDataAdapter(cmdValores)
        da.Fill(GLOBAL_dtCelulasUsuario)
    End Sub






#Region "Sigamet portatil"

    Public Function LlamadasClientePortatil(ByVal Cliente As Integer) As DataTable

        Dim cmd As New SqlCommand("spCCConsultaLlamadasClientePortatil", CnnSigamet)
        Dim da As New SqlDataAdapter(cmd)
        Dim dt As New DataTable("Llamada")

        With cmd
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add("@Cliente", SqlDbType.Int).Value = Cliente
        End With

        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Return dt
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Throw ex

        Finally
            da.Dispose()
            cmd.Dispose()

        End Try
    End Function



#End Region

End Module

Friend Class CustomExceptionHandler
    Public Sub OnThreadException(ByVal Sender As Object, ByVal t As System.Threading.ThreadExceptionEventArgs)

        Dim result As DialogResult = DialogResult.Cancel
        Try
            result = Me.ShowThreadExceptionDialog(t.Exception)
        Catch ex As Exception
            Try
                EventLog.WriteEntry("CyC", ex.Message, EventLogEntryType.Error)
                MessageBox.Show(ex.Message, "Error en el módulo", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Stop)
            Finally
                Application.Exit()
            End Try
        End Try

        If (result = DialogResult.Abort) Then
            Application.Exit()
        End If
    End Sub

    Private Function ShowThreadExceptionDialog(ByVal e As Exception) As DialogResult
        Dim errorMsg As String = "Ha ocurrido un error.  Por favor contacte al administrador del " & _
                                 "sistema con la siguiente información:" & vbCrLf & vbCrLf
        errorMsg &= e.Message & vbCrLf & vbCrLf & "Stack Trace:" & _
                    vbCrLf & e.StackTrace
        Return MessageBox.Show(errorMsg, _
                                "Error en la aplicación", _
                                MessageBoxButtons.OK, _
                                MessageBoxIcon.Stop)
    End Function


End Class

Friend Class validacionCreditoOperador

    Private _maxImporteCredito, _
            _maxLitrosCredito, _
            _saldo, _
            _saldoLitros As Double, _
            _valida As Boolean, _
            _connection As SqlConnection

    Private _msgString As String = "Se ha rebasado el límite de crédito máximo" & Chr(13) & _
                                   "del operador de esta ruta", _
            _msgCaption As String = "Crédito de operador"


    'Public ReadOnly Property MaxImporteCredito() As Double
    '    Get
    '        Return _maxImporteCredito
    '    End Get
    'End Property

    'Public ReadOnly Property MaxLitrosCredito() As Double
    '    Get
    '        Return _maxLitrosCredito
    '    End Get
    'End Property

    Public Sub New(ByVal Operador As Integer, ByVal Connection As SqlClient.SqlConnection)
        _connection = Connection
        Dim dt As DataTable = validaCreditoOperador(Operador)
        If Not (dt Is Nothing) And dt.Rows.Count > 0 Then
            _maxImporteCredito = CType(dt.Rows(0).Item("MaxImporteCredito"), Double)
            _maxLitrosCredito = CType(dt.Rows(0).Item("MaxLitrosCredito"), Double)
            _saldo = CType(dt.Rows(0).Item("Saldo"), Double)
            _saldoLitros = CType(dt.Rows(0).Item("SaldoLitros"), Double)
            _valida = True
        Else
            _valida = False
        End If
    End Sub

    Public Sub ValidaSaldoOperador(Optional ByVal litros As Double = Nothing, _
                                   Optional ByVal importe As Double = Nothing)
        If _valida Then
            If (litros + _saldoLitros) > _maxLitrosCredito Or (importe + _saldo) > _maxImporteCredito Then
                MessageBox.Show(_msgString, _msgCaption, _
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If
        End If

    End Sub


    Private Function validaCreditoOperador(ByVal Operador As Integer) As DataTable
        Dim cmd As New SqlClient.SqlCommand(), _
            da As New SqlClient.SqlDataAdapter(), _
            dt As New DataTable("ParamCredito")
        With cmd
            .CommandText = "spCCLIQParametrosCreditoOperador"
            .CommandType = CommandType.StoredProcedure
            .Connection = _connection
            .Parameters.Add("@Operador", SqlDbType.Int).Value = Operador
        End With
        da.SelectCommand = cmd
        Try
            _connection.Open()
            da.Fill(dt)
        Catch ex As Exception
            dt = Nothing
        Finally
            If _connection.State = ConnectionState.Open Then
                _connection.Close()
            End If
            cmd.Dispose()
            da.Dispose()
        End Try
        Return dt
    End Function



    Private Sub consultaParametrosConexionReportesEspeciales(ByVal Connection As System.Data.SqlClient.SqlConnection)
        Dim alterConfig As SigaMetClasses.cConfig = New SigaMetClasses.cConfig(9)
        GLOBAL_UsuarioReportes = CStr(alterConfig.Parametros("UsuarioReportes")).Trim
        Dim oUsuarioReportes As New SigaMetClasses.cUserInfo()
        oUsuarioReportes.ConsultaDatosUsuarioReportes(GLOBAL_UsuarioReportes)
        GLOBAL_PasswordReportes = oUsuarioReportes.Password
    End Sub


End Class

#Region "Garbage"
'Public Sub Main()
'    'Splash
'    Dim oSplash As New frmSplash()
'    oSplash.ShowDialog()


'    'Dim oLogin As New SigaMetClasses.Seguridad(1, Application.ProductVersion, "LightGray")
'    Dim frmAcceso As New SigametSeguridad.UI.Acceso(Application.StartupPath + "\Default.Seguridad y administracion.exe.config", _
'        True, 1, New Bitmap(Application.StartupPath + "\Boletin.ico"))
'    Dim oLogin As New SigaMetClasses.Seguridad(1, Application.ProductVersion, "LightGray")
'    If oLogin.ShowDialog = DialogResult.OK Then

'        GLOBAL_ConString = oLogin.CadenaConexion
'        'TODO Tomar la cadena de conexión de la nueva estructura de seguridad
'        GLOBAL_FechaServidor = SigaMetClasses.FechaServidor.Date

'        CnnSigamet.ConnectionString = GLOBAL_ConString
'        'IMPLEMENTACIÓN DEL UpDATER (ACTUALIZACION AUTOMÁTICA) 14/09/2004
'        Dim updateSys As New SIGAMETSecurity.Updater(1, Application.ProductVersion, Application.StartupPath, CnnSigamet)
'        If updateSys.Desactualizado = True Then
'            'Necesita actualizarse
'            Application.Exit()
'            Exit Sub
'        End If

'        GLOBAL_Usuario = oLogin.UserID
'        GLOBAL_Password = oLogin.Password
'        GLOBAL_Servidor = oLogin.Servidor
'        GLOBAL_Database = oLogin.BaseDatos

'        oSeguridad = New SigaMetClasses.cSeguridad(GLOBAL_Usuario, 1)

'        GLOBAL_UsuarioNombre = oLogin.UsuarioNombre
'        GLOBAL_Celula = oLogin.Celula
'        GLOBAL_CelulaDescripcion = oLogin.CelulaDescripcion
'        GLOBAL_CelulaComercial = oLogin.CelulaComercial
'        GLOBAL_CelulaAdmin = oLogin.CelulaAdmin
'        GLOBAL_UsuarioNT = oLogin.UsuarioNT
'        GLOBAL_Empleado = oLogin.Empleado
'        GLOBAL_EmpleadoNombre = oLogin.EmpleadoNombre
'        GLOBAL_Remoto = oLogin.Remoto

'        If GLOBAL_Remoto Then
'            GLOBAL_UserInfo = New SigaMetClasses.cUserInfo(GLOBAL_Usuario, GLOBAL_Password, GLOBAL_Database, GLOBAL_Servidor)
'        End If

'        Try
'            GLOBAL_UsaRutaReportesLocal = CType(ConfigurationSettings.AppSettings("UsaRutaReportesLocal"), Boolean)

'            If GLOBAL_UsaRutaReportesLocal Then
'                GLOBAL_RutaReportes = ConfigurationSettings.AppSettings("RutaReportesLocal")
'            Else
'                GLOBAL_RutaReportes = CType(oLogin.Parametros("RutaReportes"), String).Trim
'            End If
'        Catch
'            GLOBAL_RutaReportes = CType(oLogin.Parametros("RutaReportes"), String).Trim
'        End Try

'        Try
'            'Se agregó el 28/09/2004
'            GLOBAL_ManejarClientesPortatil = CType(oLogin.Parametros("ManejarClientesPortatil"), Boolean)
'            'Se agregó el 29/09/2004
'            GLOBAL_AplicaATMasCercano = CType(oLogin.Parametros("ActivarATMasCercano"), Boolean)
'            'Se(agregó) el 29/09/2004
'            GLOBAL_AplicaAdministracionEdificios = CType(oLogin.Parametros("AplicaAdmEdificios"), Boolean)
'            'Se(agregó) el 29/09/2004 para manejar el ramo de los edificios administrados
'            If GLOBAL_AplicaAdministracionEdificios Then
'                GLOBAL_RamoClienteAdmEdificios = CType(oLogin.Parametros("RamoAdmEdificios"), String).Trim
'            End If

'            GLOBAL_AplicaCambioFechaCompromiso = CType(oLogin.Parametros("AplicaCambioFechaCompromi"), Boolean)

'            GLOBAL_AplicaValidacionCredito = CType(oLogin.Parametros("AplicaValidacionCredito"), Boolean)

'            If GLOBAL_AplicaValidacionCredito Then
'                GLOBAL_ClaveCreditoAutorizado = CType(oLogin.Parametros("ClaveCreditoAutorizado"), Byte)
'            End If

'            GLOBAL_MotivoConfirmacionBoletin = CType(oLogin.Parametros("MotivoLlamadaConfirmacion"), Short)
'            GLOBAL_MotivoBoletinOperador = CType(oLogin.Parametros("MotivoLlamadaBoletin"), Short)

'            'almacena el manesaje para importe de crédito excedido
'            GLOBAL_MensajeCreditoExcedido = CType(oLogin.Parametros("MensajeCreditoExcedido"), String)

'            GLOBAL_ZonaEconomicaDefault = CType(oLogin.Parametros("ZonaEconomicaDefault"), String).Trim

'            'TODO: Activación del avance de programación JAGD 15-01-2004
'            GLOBAL_AvanzaProgramacion = CType(oSeguridad.TieneAcceso("AvanzaProgramacion"), Boolean)

'            'TODO: Indica sí se fuerza la captura de observaciones de inactivación de la programación
'            GLOBAL_CapturaObservacionesInactivacionProg = CType(oLogin.Parametros("CapturaObservacionesInact"), Boolean)

'            'TODO: Valor de la seguridad de reportes
'            GLOBAL_SeguridadReportes = CType(oLogin.Parametros("SeguridadReportes"), Boolean)

'            'TODO: Multiselect del boletín
'            GLOBAL_ConfirmaBoletinGrupo = CType(oLogin.Parametros("ConfirmaBoletinGrupo"), Boolean)

'            'Validación de la serie de la remisión en la liquidación
'            GLOBAL_ValidaSerieRemision = CType(oLogin.Parametros("ValidarSerieRemision"), Boolean)

'            'Verifica si se pueden agregar calles y colonias
'            GLOBAL_AltaCalleColonia = CBool(oLogin.Parametros("PermitirAltaCalles")) OrElse GLOBAL_Remoto

'            'Verifica si es posible capturar el factor de comisión por cliente
'            GLOBAL_CapturaFactorComision = CBool(oLogin.Parametros("CAPTURAFACTORCOMISION"))

'            'Verifica si es posible marcar un contrato como "Firmado", sigamet corporativo
'            GLOBAL_CapturaContratoFirmado = CBool(oLogin.Parametros("CAPTURACONTRATOFIRMADO"))

'            'Captura de saldos a favor en liquidación
'            GLOBAL_SaldoAFavorLiquidacion = CBool(oLogin.Parametros("SALDOAFAVORLIQUIDACION"))

'            'Control parametrizado de ventas multinivel
'            GLOBAL_VentasMultinivel = CBool(oLogin.Parametros("VentasMultinivel"))

'            'registro de inicio y fin de sesión
'            GLOBAL_REGISTRO_INICIO_SESION = CBool(oLogin.Parametros("REGISTRO_INICIO_SESION"))
'            GLOBAL_MOTIVO_LLAMADA_INICIOSESI = CInt(oLogin.Parametros("MOTIVO_LLAMADA_INICIOSESI"))
'            GLOBAL_MOTIVO_LLAMADA_FIN_SESION = CInt(oLogin.Parametros("MOTIVO_LLAMADA_FIN_SESION"))

'            'Captura de clientes para grupos promocionales
'            GLOBAL_GruposPromocionales = CBool(oLogin.Parametros("GRUPOS_PROMOCIONALES"))

'        Catch ArgEx As ArgumentException
'            MessageBox.Show("El sistema no puede leer la información del parámetro.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

'        Catch ex As Exception
'            MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error)

'        End Try



'        CargaPrecios()

'        CargaPredeterminados()

'        CargaConfiguracion()

'        'Validacion de una u otra forma de liquidar segun parametros MHV 02/11/2004
'        CargaparametrosLiquidacion()



'        System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA
'        Dim eh As New CustomExceptionHandler()
'        AddHandler Application.ThreadException, AddressOf eh.OnThreadException
'        Application.Run(New frmPrincipal())

'    End If

'End Sub

#End Region