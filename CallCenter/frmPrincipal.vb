Imports System.Data.SqlClient
Imports RelacionNotas

Public Class frmPrincipal
    Inherits System.Windows.Forms.Form

    Private WithEvents oRI As RI500.InterfaseRI
    Private _URLGateway As String

#Region "Control de alarmas de los postit"

    Private Sub InicioControlAlarma()
        'Abre los postits que están configurados como alarmas y que son de la célula
        Cursor = Cursors.WaitCursor
        Dim strQuery As String =
        "Select * From PostIt Where Alarma = 1 And Cliente In " &
            "(Select Cliente From Cliente Where Celula In " &
                "(Select Celula From UsuarioCelula Where Usuario = '" & Main.GLOBAL_Usuario & "'))"
        '"SELECT Postit, Texto FROM Postit WHERE Alarma = 1"
        'Dim cnPostit As New SqlConnection(LeeInfoConexion(False))
        Dim cmd As New SqlCommand(strQuery, Main.CnnSigamet)
        Dim dr As SqlDataReader
        Dim mnuControlAlarma As ContextMenu
        Dim mnuItem As SigaMetClasses.cMenuItem
        Dim mnuSystemItem As MenuItem

        Try
            Main.AbreConexion()
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            ControlAlarma.ContextMenu = Nothing
            mnuControlAlarma = New ContextMenu()

            Dim _Total As Integer, _NotifyText As String
            While dr.Read

                mnuItem = New SigaMetClasses.cMenuItem(Mid(CType(dr("Texto"), String), 1, 30),
                CType(dr("Postit"), Integer),
                New EventHandler(AddressOf OnAlarmaClick))
                mnuControlAlarma.MenuItems.Add(mnuItem)

                _Total += 1

            End While


            mnuSystemItem = New MenuItem("-")
            mnuControlAlarma.MenuItems.Add(mnuSystemItem)

            mnuSystemItem = New MenuItem("Refrescar", New EventHandler(AddressOf OnAlarmaRefrescarClick))
            mnuControlAlarma.MenuItems.Add(mnuSystemItem)

            Me.ControlAlarma.ContextMenu = mnuControlAlarma

            'Ventana de notificación
            If _Total > 0 Then
                _NotifyText = "Tiene " & _Total.ToString &
                " alarmas pendientes." & Chr(13) & Chr(13) &
                "Puede revisarlas en el tablero de alarmas dando clic en este mensaje, o dando clic en el menú principal."
                Me.NotificationWindow.Notify(_NotifyText)
            End If

        Catch ex As Exception
            cmd.Dispose()
        Finally
            Main.CierraConexion()
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub OnAlarmaClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim oPostit As SigaMetClasses.Postit
        oPostit = New SigaMetClasses.Postit(CType(sender, SigaMetClasses.cMenuItem).Postit)
        Me.AddOwnedForm(oPostit)
        oPostit.Show()
    End Sub

    Private Sub OnAlarmaRefrescarClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        InicioControlAlarma()
    End Sub


#End Region

#Region "VALIDADO"

    Private Sub mnuTableroAlarma_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTableroAlarma.Click
        TableroAlarma()
    End Sub

    Private Sub BitacoraUsuario()
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmBitacoraUsuario" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        Dim oBitacoraUsuario As New frmBitacoraUsuario(Main.GLOBAL_Usuario)
        oBitacoraUsuario.MdiParent = Me
        oBitacoraUsuario.Show()
        Cursor = Cursors.Default

    End Sub


    Private Sub TableroAlarma()
        Cursor = Cursors.WaitCursor
        Dim x As New SigaMetClasses.PostitLista(Main.GLOBAL_Usuario)
        x.ShowDialog()
        Cursor = Cursors.Default

    End Sub

    Private Sub NotificationWindow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TableroAlarma()
    End Sub

    Private Sub mnuBitacora_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBitacora.Click
        BitacoraUsuario()
    End Sub

    Private Sub HorarioRuta()
        Cursor = Cursors.WaitCursor
        Dim frmHorario As New Horario()
        frmHorario.Entrada()
        Cursor = Cursors.Default
    End Sub

    'Private Sub Bitacora()
    '    Cursor = Cursors.WaitCursor
    '    Dim frmBitacora As New Bitacora()
    '    frmBitacora.Entrada(GLOBAL_Usuario, GLOBAL_UsuarioNombre)
    '    frmBitacora.setCelda(_numeroCelda)
    '    _numeroCelda = frmBitacora.getCelda()
    '    frmBitacora.Dispose()
    '    Cursor = Cursors.Default
    'End Sub

    Private Sub CallCenter()
        Cursor = Cursors.WaitCursor
        Dim oCallCenter As New frmCallCenter(_URLGateway)
        oCallCenter.MdiParent = Me
        oCallCenter.Show()
        oCallCenter.BringToFront()
        Cursor = Cursors.Default
    End Sub

    Private Sub mnuServicioTecnico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServicioTecnico.Click
        ServicioTecnico()
    End Sub

    Private Sub Conciliacion()
        Dim x As Form
        For Each x In Me.MdiChildren
            If x.Name = "Conciliacion" Then
                x.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        Dim mdiConciliacion As Conciliacion = New Conciliacion()
        mdiConciliacion.MdiParent = Me
        mdiConciliacion.Entrada()
        Cursor = Cursors.Default
    End Sub

    Private Sub ServicioTecnico()
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmServProgramacion" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        Dim oServ As New SigametST.frmServProgramacion(GLOBAL_Usuario,
                                                       GLOBAL_Password,
                                                       GLOBAL_ConString,
                                                       GLOBAL_Corporativo,
                                                       GLOBAL_Sucursal,
                                                       _URLGateway)
        oServ.MdiParent = Me
        oServ.WindowState = FormWindowState.Maximized
        oServ.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mniAcerca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniAcerca.Click
        AcercaDe()
    End Sub

    Private Sub frmPrincipal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim oConfig As New SigaMetClasses.cConfig(1, GLOBAL_Corporativo, GLOBAL_Sucursal)

        Try
            Dim oConfig2 As New SigaMetClasses.cConfig(1, GLOBAL_Corporativo, GLOBAL_Sucursal)
            _URLGateway = CType(oConfig2.Parametros("URLGateway"), String)

        Catch saex As System.ArgumentException
            If saex.Message.Contains("Index") Then
                _URLGateway = ""
            End If
        End Try


        'Registrar el inicio de sesión en el módulo
        If GLOBAL_REGISTRO_INICIO_SESION Then
            RegistroOperacion(GLOBAL_MOTIVO_LLAMADA_FIN_SESION)
        End If

        Me.Text = "SigaMET" & Chr(178)
        staPrincipal.Panels(1).Text = GLOBAL_UsuarioNombre
        staPrincipal.Panels(0).Text = GLOBAL_Usuario
        staPrincipal.Panels(2).Text = GLOBAL_CelulaDescripcion
        staPrincipal.Panels(3).Text = "Versión: " & Application.ProductVersion
        sbpServidor.Text = GLOBAL_Servidor
        sbpBaseDeDatos.Text = GLOBAL_Database
        stapFecha.Text = GLOBAL_FechaServidor.ToShortDateString
        sbpVersion.Text = "Versión: " & Application.ProductVersion

        'Título de la aplicación con el nombre de la empresa
        Me.Text = Me.Text + " - " + GLOBAL_NombreEmpresa

        activarMenuAdmEdificios(GLOBAL_AplicaAdministracionEdificios)

        If GLOBAL_VentasMultinivel Then
            VentasMultinivel.ValLayer.VentasMultinivelLib.Instance.Inicializar(CnnSigamet, GLOBAL_Usuario, GLOBAL_Celula)
        End If
        mniVentasMultinivel.Enabled = GLOBAL_VentasMultinivel AndAlso (oSeguridad.TieneAcceso("ConsultaRelacionesMultinivel") OrElse oSeguridad.TieneAcceso("ModificaRelacionesMultinivel"))
        mniVentasMultinivel.Visible = GLOBAL_VentasMultinivel AndAlso (oSeguridad.TieneAcceso("ConsultaRelacionesMultinivel") OrElse oSeguridad.TieneAcceso("ModificaRelacionesMultinivel"))

        If GLOBAL_GruposPromocionales Then
            VentasPromotor.ValLayer.VentasPromotorLib.Instance.Inicializar(CnnSigamet, GLOBAL_Usuario, GLOBAL_Celula,
                oSeguridad.TieneAcceso("AGREGAR ASOCIACIONES"), oSeguridad.TieneAcceso("REALIZAR PAGOS PROMOCIONES"),
                GLOBAL_PagoPromocionAPromotor)
            mnuPromotor.Enabled = GLOBAL_GruposPromocionales AndAlso (oSeguridad.TieneAcceso("VENTAS POR PROMOCIONES") OrElse oSeguridad.TieneAcceso("AGREGAR ASOCIACIONES"))
            mnuPromotor.Visible = mnuPromotor.Enabled
        End If

        mnuCalidad.Enabled = oSeguridad.TieneAcceso("Calidad")
        mnuCalidad.Visible = oSeguridad.TieneAcceso("Calidad")
        mniDepuradorCalles.Enabled = oSeguridad.TieneAcceso("Depuracion")
        mniDepuradorColonias.Enabled = oSeguridad.TieneAcceso("Depuracion")
        mniHistorialDepuracion.Enabled = oSeguridad.TieneAcceso("Depuracion")
        mniDepuradorCalles.Visible = oSeguridad.TieneAcceso("Depuracion")
        mniDepuradorColonias.Visible = oSeguridad.TieneAcceso("Depuracion")
        mniHistorialDepuracion.Visible = oSeguridad.TieneAcceso("Depuracion")


        activarMenuFolios(oSeguridad.TieneAcceso("Control de folios"))
        activarMenuTraspasos(oSeguridad.TieneAcceso("Traspaso de rutas"))

        activarMenuRemisionesCelA(oSeguridad.TieneAcceso("RemisionesCelulaA"))
        activarMenuHorarioRuta(oSeguridad.TieneAcceso("HorariosPorRuta"))
        activarMenuControlDocumentos(oSeguridad.TieneAcceso("Control_Documentos"))
        activarMenuConfirmacionPedidos(CType(oConfig.Parametros("Confirma_Pedidos_Programa"), Boolean))

        activarValesPromocionales(oSeguridad.TieneAcceso("PROMOCION VALES"))
        activarMenuReactivacionPedidos(oSeguridad.TieneAcceso("Reactivacion_Pedidos"))
        activarMenusObsoletos(oSeguridad.TieneAcceso("MenusObsoletos"))

        'Lusate Activa los menús de Seguimiento a Fugas y Reporte RAF
        activarMenuReporteRaf(oSeguridad.TieneAcceso("SeguimientoRAF"))
        activarMenuSeguimientoFugas(oSeguridad.TieneAcceso("SeguimientoFugasPtl"))



        'Para registro de pedidos WEB
        MenuRemoval("REGISTRO_PEDIDOSWEB", mnuPedidoWeb, Me.Menu)

        'Para registro y consulta de pedidos suministro pronosticado
        MenuRemoval("SUM_PRONOSTICADO", mnuSuministroPronosticado, Me.Menu)

        'Descarga de datos de la base de datos UDS
        mnuDescargaUDS.Visible = GLOBAL_DescargaUDS

        'Acceso al editor html para contenidos del portal
        mnuEditorPortal.Visible = oSeguridad.TieneAcceso("EDITOR_HTML_PORTAL")

        'Habilita reasignación de pedidos en pantalla boletin
        Global_MuestraReasignacionPedidos = oSeguridad.TieneAcceso("ReasignarPedidosBoletinados")

        'Habilita check para poder deshabilitar la validación de posición GPS al boletinar pedidos
        Global_MuestraNoValidarGPS = oSeguridad.TieneAcceso("DeshabilitaGPS")

        'Habilita check para poder deshabilitar la validación de posición GPS al boletinar pedidos
        Global_HabilitaBtnGeoReferencia = oSeguridad.TieneAcceso("HabilitaGeoReferencia")
        If Main.GLOBAL_Remoto Then
            Me.Menu = Me.mnuRemoto
        End If

        InicioControlAlarma()

    End Sub

    Private Sub frmPrincipal_Closing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Cursor = Cursors.WaitCursor

        'Registrar el inicio de sesión en el módulo
        If GLOBAL_REGISTRO_INICIO_SESION Then
            RegistroOperacion(GLOBAL_MOTIVO_LLAMADA_FIN_SESION)
        End If

        Dim _Pendientes As Integer = PuedeCerrar(GLOBAL_Celula)
        If _Pendientes > 0 Then
            Dim strMensaje As String = _
            "No puede cerrar la aplicacion por que todavia tiene " & _
            _Pendientes.ToString & _
            " conciliaciones pendientes de realizar." & _
            Chr(13) & "¿Desea salir de todas formas?"
            If MessageBox.Show(strMensaje, "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = _
            DialogResult.No Then
                e.Cancel = True
                Cursor = Cursors.Default
            End If
        End If
    End Sub

    Private Sub mnuReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportes.Click
        ConsultaReportes()
    End Sub

    Private Sub ConsultaReportesModulo()
        Cursor = Cursors.WaitCursor
        Dim oListaReporteModulo As New ReporteDinamico.frmListaReporteRemoto(Main.GLOBAL_Celula, Main.GLOBAL_Servidor, Main.GLOBAL_Database, Main.GLOBAL_RutaReportes)
        oListaReporteModulo.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub ConsultaReportes()
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmListaReporte" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor

        'todo Manejar si se estan usando los reportes locales
        'Ya no pasa como parámetro el usuario y el password ya que está hard-code en el componente de reportes
        Dim frmReporteDinamico As ReporteDinamico.frmListaReporte
        Try                        
            frmReporteDinamico = New ReporteDinamico.frmListaReporte(1, GLOBAL_RutaReportes, _
                GLOBAL_Servidor, GLOBAL_Database, GLOBAL_Usuario, CnnSigamet, _
                GLOBAL_Corporativo, GLOBAL_Sucursal, GLOBAL_SeguridadReportes)

            frmReporteDinamico.MdiParent = Me
            frmReporteDinamico.WindowState = FormWindowState.Maximized
            frmReporteDinamico.Show()
            Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            Cursor = Cursors.Default
        End Try

    End Sub

    Private Sub CatalogoEmpresa()
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "CatalogoEmpresa" Then
                f.Focus()
                Exit Sub
            End If
        Next

        Cursor = Cursors.WaitCursor
        Dim oCatalogoEmpresa As New SigaMetClasses.CatalogoEmpresa(PermiteModificar:=oSeguridad.TieneAcceso("Cambio de datos fiscales"))
        oCatalogoEmpresa.MdiParent = Me
        oCatalogoEmpresa.Show()
        Cursor = Cursors.Default
    End Sub

#End Region

    Private Sub Liquidacion()
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmSeleccionaRutaPreliquidacion" Then
                f.Focus()
                Exit Sub
            End If
        Next

        'Dim frmLecturaOperador As LecturaOperador = New LecturaOperador()
        Cursor = Cursors.WaitCursor
        Dim oSelRuta As New frmSeleccionaRutaPreliquidacion()
        If oSelRuta.ShowDialog = DialogResult.OK Then
            Dim Fecha As Date
            Dim Ruta As Integer
            Dim Folio As Integer
            Dim Anioatt As Integer
            Dim Acepto As Boolean = Nothing
            Dim Descarga As Boolean
            Dim Celula As Integer

            Fecha = oSelRuta.Fecha
            Ruta = oSelRuta.Ruta
            Folio = oSelRuta.Folio
            Anioatt = oSelRuta.AñoAtt
            Descarga = oSelRuta.Descarga
            Celula = oSelRuta._Celula
            'TODO: VALIDACION DE CREDITO DE CLIENTE EN LIQUIDACION incluida el día 17/10/2004
            LlamadaLiquidacion(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            ''Validacion segun el tipo de liquidacion de parametros. MHV 02/11/2004
            'If Not GLOBAL_NuevaVersionLiquidacion Then
            '    'If Global_TipoLiquidacion = "N" Then
            '    Dim mdiLiquidacion As Liquidacion2005 = New Liquidacion2005(GLOBAL_AplicaValidacionCredito)
            '    mdiLiquidacion.MdiParent = Me
            '    Cursor = Cursors.Default
            '    mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            '    'mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga)
            '    mdiLiquidacion.Show()
            '    'Else
            '    '    'Liquidación anterior con remisión
            '    '    'Dim mdiLiquidacion As LiquidacionRemision = New LiquidacionRemision(GLOBAL_AplicaValidacionCredito)
            '    '    'Liquidación nueva con remisión
            '    '    Dim mdiLiquidacion As LiquidacionRemision2005 = New LiquidacionRemision2005(GLOBAL_AplicaValidacionCredito)
            '    '    mdiLiquidacion.MdiParent = Me
            '    '    Cursor = Cursors.Default
            '    '    mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            '    '    mdiLiquidacion.Show()
            '    'End If
            '    'Quitar el comentario para la nueva versión pruebas
            'Else
            '    Dim mdiLiquidacion As Liquidacion2.Liquidacion2005 = _
            '        New Liquidacion2.Liquidacion2005(Global_TipoLiquidacion, _
            '            GLOBAL_AdmEdificiosLiquidacionCredito, _
            '            GLOBAL_SaldoAFavorLiquidacion, _
            '            GLOBAL_ValidaSerieRemision, _
            '            GLOBAL_RamoClienteAdmEdificios, _
            '            GLOBAL_ClaveRamoClienteAdmEdificios, _
            '            GLOBAL_VentasMultinivel, _
            '            GLOBAL_ClaveCreditoAutorizado, _
            '            GLOBAL_AplicaValidacionCredito, _
            '            GLOBAL_Usuario, _
            '            GLOBAL_Password, _
            '            CnnSigamet, _
            '            GLOBAL_SeleccionTipoCreditoLiq, _
            '            GLOBAL_LiquidacionObsequios, _
            '            GLOBAL_ValidacionFinalLiquidacion, _
            '            GLOBAL_DescuentoProntoPago, _
            '            GLOBAL_ValidacionLimiteCredito, _
            '            GLOBAL_AgrupacionPedidosLiquidacion)
            '    mdiLiquidacion.MdiParent = Me
            '    Cursor = Cursors.Default
            '    mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            '    mdiLiquidacion.Show()
            'End If
        End If
        Cursor = Cursors.Default
    End Sub

    Private Sub LlamadaLiquidacion(ByVal Fecha As Date, ByVal Ruta As Integer, ByVal Folio As Integer, ByVal AnioAtt As Integer, _
    ByVal Descarga As Boolean, ByVal Celula As Integer)
        'Validacion segun el tipo de liquidacion de parametros. MHV 02/11/2004
        If Not GLOBAL_NuevaVersionLiquidacion Then
            'If Global_TipoLiquidacion = "N" Then
            Dim mdiLiquidacion As Liquidacion2005 = New Liquidacion2005(GLOBAL_AplicaValidacionCredito)
            mdiLiquidacion.MdiParent = Me
            Cursor = Cursors.Default
            mdiLiquidacion.Entrada(Fecha, Ruta, Folio, AnioAtt, Descarga, Celula)
            'mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga)
            mdiLiquidacion.Show()
            'Else
            '    'Liquidación anterior con remisión
            '    'Dim mdiLiquidacion As LiquidacionRemision = New LiquidacionRemision(GLOBAL_AplicaValidacionCredito)
            '    'Liquidación nueva con remisión
            '    Dim mdiLiquidacion As LiquidacionRemision2005 = New LiquidacionRemision2005(GLOBAL_AplicaValidacionCredito)
            '    mdiLiquidacion.MdiParent = Me
            '    Cursor = Cursors.Default
            '    mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            '    mdiLiquidacion.Show()
            'End If
            'Quitar el comentario para la nueva versión pruebas
        Else
            Dim mdiLiquidacion As Liquidacion2.Liquidacion2005 = _
                New Liquidacion2.Liquidacion2005(Global_TipoLiquidacion, _
                    GLOBAL_AdmEdificiosLiquidacionCredito, _
                    GLOBAL_SaldoAFavorLiquidacion, _
                    GLOBAL_ValidaSerieRemision, _
                    GLOBAL_RamoClienteAdmEdificios, _
                    GLOBAL_ClaveRamoClienteAdmEdificios, _
                    GLOBAL_VentasMultinivel, _
                    GLOBAL_ClaveCreditoAutorizado, _
                    GLOBAL_AplicaValidacionCredito, _
                    GLOBAL_Usuario, _
                    GLOBAL_Password, _
                    CnnSigamet, _
                    GLOBAL_SeleccionTipoCreditoLiq, _
                    GLOBAL_LiquidacionObsequios, _
                    GLOBAL_ValidacionFinalLiquidacion, _
                    GLOBAL_DescuentoProntoPago, _
                    GLOBAL_ValidacionLimiteCredito, _
                    GLOBAL_AgrupacionPedidosLiquidacion, _
                    GLOBAL_Corporativo, _
                    GLOBAL_Sucursal)
            mdiLiquidacion.MdiParent = Me
            Cursor = Cursors.Default
            mdiLiquidacion.Entrada(Fecha, Ruta, Folio, AnioAtt, Descarga, Celula)
            mdiLiquidacion.Show()
        End If
    End Sub


#Region "Windows Form Designer generated code "

    Public Sub New(Optional ByVal URLGateway As String = Nothing)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        _URLGateway = URLGateway

        'Add any initialization after the InitializeComponent() call
        mniRelacionNotas.Enabled = oSeguridad.TieneAcceso("RelacionNotas")
        mniRelacionNotas.Visible = oSeguridad.TieneAcceso("RelacionNotas")
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
    Friend WithEvents mnuPrincipal As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents mniArchivo As System.Windows.Forms.MenuItem
    Friend WithEvents mniCallCenter As System.Windows.Forms.MenuItem
    Friend WithEvents mniSalir As System.Windows.Forms.MenuItem
    Friend WithEvents mniAyuda As System.Windows.Forms.MenuItem
    Friend WithEvents mniVentana As System.Windows.Forms.MenuItem
    Friend WithEvents mniCascada As System.Windows.Forms.MenuItem
    Friend WithEvents mniAcerca As System.Windows.Forms.MenuItem
    Friend WithEvents mniOrganizar As System.Windows.Forms.MenuItem
    Friend WithEvents staPrincipal As System.Windows.Forms.StatusBar
    Friend WithEvents stapUsuario As System.Windows.Forms.StatusBarPanel
    Friend WithEvents stapNombre As System.Windows.Forms.StatusBarPanel
    Friend WithEvents stapCelula As System.Windows.Forms.StatusBarPanel
    Friend WithEvents stapFecha As System.Windows.Forms.StatusBarPanel
    Friend WithEvents mniMosaicoHorizontal As System.Windows.Forms.MenuItem
    Friend WithEvents mniMosaicoVertical As System.Windows.Forms.MenuItem
    Friend WithEvents mniEmpresas As System.Windows.Forms.MenuItem
    Friend WithEvents mniEmpresa As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem36 As System.Windows.Forms.MenuItem
    Friend WithEvents sbpServidor As System.Windows.Forms.StatusBarPanel
    Friend WithEvents sbpBaseDeDatos As System.Windows.Forms.StatusBarPanel
    Friend WithEvents mnuReporte As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReportes As System.Windows.Forms.MenuItem
    Friend WithEvents mnuServicioTecnico As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCancelacionPedido As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRuta80 As System.Windows.Forms.MenuItem
    Friend WithEvents sbpVersion As System.Windows.Forms.StatusBarPanel
    Friend WithEvents mnuConsultaBoletin As System.Windows.Forms.MenuItem
    Friend WithEvents mnuClienteRuta As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDescargaRI As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCargaRI As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCambiarClave As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCargaTarjetaRampac As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDescargaTarjetaRampac As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLiquidacionManual As System.Windows.Forms.MenuItem
    Friend WithEvents mnuConciliacionNotasBlancas As System.Windows.Forms.MenuItem
    Friend WithEvents mnuReposicionNotasBlancas As System.Windows.Forms.MenuItem
    Friend WithEvents mnuModificaLiquidacion As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLiquidacionesPendientes As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSep1 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSep2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSep3 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRemoto As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremArchivo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremCallCenter As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremSalir As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremBitacora As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremReportes As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremHelp As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremCambioPassword As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremAcercade As System.Windows.Forms.MenuItem
    Friend WithEvents mnuremServiciosTecnicos As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents ControlAlarma As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenu1 As System.Windows.Forms.ContextMenu
    Friend WithEvents NotificationWindow As VbPowerPack.NotificationWindow
    Friend WithEvents mnuBitacora As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTableroAlarma As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAdmEdificios As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDivAdm As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCalidad As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFolios As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAsignacionFolios As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCancelacionFolios As System.Windows.Forms.MenuItem
    Friend WithEvents mnuTraspaso As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRemCelulaA As System.Windows.Forms.MenuItem
    Friend WithEvents mnuHorariosRuta As System.Windows.Forms.MenuItem
    Friend WithEvents mnuRutaApoyo As System.Windows.Forms.MenuItem
    Friend WithEvents mnuControlDocumentos As System.Windows.Forms.MenuItem
    Friend WithEvents mnuConfirmacion As System.Windows.Forms.MenuItem
    Friend WithEvents TabBar1 As Sigamet.TabBar
    Friend WithEvents mniCatalgoCallesColonias As System.Windows.Forms.MenuItem
    Friend WithEvents mniDepuradorColonias As System.Windows.Forms.MenuItem
    Friend WithEvents mniDepuradorCalles As System.Windows.Forms.MenuItem
    Friend WithEvents mniHistorialDepuracion As System.Windows.Forms.MenuItem
    Friend WithEvents mniRelacionNotas As System.Windows.Forms.MenuItem
    Friend WithEvents mniVentasMultinivel As System.Windows.Forms.MenuItem
    Friend WithEvents mniRelacionClientes As System.Windows.Forms.MenuItem
    Friend WithEvents mnuVales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuComodato As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPromotor As System.Windows.Forms.MenuItem
    Friend WithEvents mnuActivacionPedidos As System.Windows.Forms.MenuItem
    Friend WithEvents mnuDescargaUDS As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPedidoWeb As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents mniPwdCambio As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSuministroPronosticado As System.Windows.Forms.MenuItem
    Friend WithEvents mniContactos As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEditorPortal As System.Windows.Forms.MenuItem
    Friend WithEvents mnuQuejas As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSolicitudCredito As System.Windows.Forms.MenuItem
    Friend WithEvents mnuMonedero As System.Windows.Forms.MenuItem
    Friend WithEvents mnuNotasRuta As System.Windows.Forms.MenuItem
    Friend WithEvents mniReportesEspeciales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuGruposComerciales As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAuxilioFallas As System.Windows.Forms.MenuItem
    Friend WithEvents mnuFugasPortatil As System.Windows.Forms.MenuItem    
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrincipal))
        Me.mnuPrincipal = New System.Windows.Forms.MainMenu(Me.components)
        Me.mniArchivo = New System.Windows.Forms.MenuItem()
        Me.mniCallCenter = New System.Windows.Forms.MenuItem()
        Me.mnuConsultaBoletin = New System.Windows.Forms.MenuItem()
        Me.mnuPedidoWeb = New System.Windows.Forms.MenuItem()
        Me.mnuBitacora = New System.Windows.Forms.MenuItem()
        Me.mnuSuministroPronosticado = New System.Windows.Forms.MenuItem()
        Me.mnuClienteRuta = New System.Windows.Forms.MenuItem()
        Me.mnuTraspaso = New System.Windows.Forms.MenuItem()
        Me.mnuRemCelulaA = New System.Windows.Forms.MenuItem()
        Me.mnuHorariosRuta = New System.Windows.Forms.MenuItem()
        Me.mnuRutaApoyo = New System.Windows.Forms.MenuItem()
        Me.mnuNotasRuta = New System.Windows.Forms.MenuItem()
        Me.mnuActivacionPedidos = New System.Windows.Forms.MenuItem()
        Me.mnuControlDocumentos = New System.Windows.Forms.MenuItem()
        Me.mniRelacionNotas = New System.Windows.Forms.MenuItem()
        Me.mnuSep1 = New System.Windows.Forms.MenuItem()
        Me.mnuCargaTarjetaRampac = New System.Windows.Forms.MenuItem()
        Me.mnuSep2 = New System.Windows.Forms.MenuItem()
        Me.mnuDescargaTarjetaRampac = New System.Windows.Forms.MenuItem()
        Me.mnuDescargaUDS = New System.Windows.Forms.MenuItem()
        Me.mnuLiquidacionManual = New System.Windows.Forms.MenuItem()
        Me.mnuVales = New System.Windows.Forms.MenuItem()
        Me.mnuSep3 = New System.Windows.Forms.MenuItem()
        Me.mnuConciliacionNotasBlancas = New System.Windows.Forms.MenuItem()
        Me.mnuReposicionNotasBlancas = New System.Windows.Forms.MenuItem()
        Me.MenuItem13 = New System.Windows.Forms.MenuItem()
        Me.mnuModificaLiquidacion = New System.Windows.Forms.MenuItem()
        Me.mnuLiquidacionesPendientes = New System.Windows.Forms.MenuItem()
        Me.MenuItem36 = New System.Windows.Forms.MenuItem()
        Me.mnuCancelacionPedido = New System.Windows.Forms.MenuItem()
        Me.mnuRuta80 = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.mnuServicioTecnico = New System.Windows.Forms.MenuItem()
        Me.mnuComodato = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.mnuAdmEdificios = New System.Windows.Forms.MenuItem()
        Me.mnuDivAdm = New System.Windows.Forms.MenuItem()
        Me.mnuDescargaRI = New System.Windows.Forms.MenuItem()
        Me.mnuCargaRI = New System.Windows.Forms.MenuItem()
        Me.mnuTableroAlarma = New System.Windows.Forms.MenuItem()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.mniSalir = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.mnuConfirmacion = New System.Windows.Forms.MenuItem()
        Me.mniVentasMultinivel = New System.Windows.Forms.MenuItem()
        Me.mniRelacionClientes = New System.Windows.Forms.MenuItem()
        Me.mnuPromotor = New System.Windows.Forms.MenuItem()
        Me.mniEmpresas = New System.Windows.Forms.MenuItem()
        Me.mniEmpresa = New System.Windows.Forms.MenuItem()
        Me.mnuReporte = New System.Windows.Forms.MenuItem()
        Me.mnuReportes = New System.Windows.Forms.MenuItem()
        Me.mniReportesEspeciales = New System.Windows.Forms.MenuItem()
        Me.mnuCalidad = New System.Windows.Forms.MenuItem()
        Me.mniCatalgoCallesColonias = New System.Windows.Forms.MenuItem()
        Me.mniDepuradorColonias = New System.Windows.Forms.MenuItem()
        Me.mniDepuradorCalles = New System.Windows.Forms.MenuItem()
        Me.mniHistorialDepuracion = New System.Windows.Forms.MenuItem()
        Me.mnuFolios = New System.Windows.Forms.MenuItem()
        Me.mnuAsignacionFolios = New System.Windows.Forms.MenuItem()
        Me.mnuCancelacionFolios = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.mniPwdCambio = New System.Windows.Forms.MenuItem()
        Me.mniContactos = New System.Windows.Forms.MenuItem()
        Me.mnuEditorPortal = New System.Windows.Forms.MenuItem()
        Me.mnuQuejas = New System.Windows.Forms.MenuItem()
        Me.mnuSolicitudCredito = New System.Windows.Forms.MenuItem()
        Me.mnuMonedero = New System.Windows.Forms.MenuItem()
        Me.mnuGruposComerciales = New System.Windows.Forms.MenuItem()
        Me.mnuAuxilioFallas = New System.Windows.Forms.MenuItem()
        Me.mnuFugasPortatil = New System.Windows.Forms.MenuItem()
        Me.mniVentana = New System.Windows.Forms.MenuItem()
        Me.mniCascada = New System.Windows.Forms.MenuItem()
        Me.mniMosaicoHorizontal = New System.Windows.Forms.MenuItem()
        Me.mniMosaicoVertical = New System.Windows.Forms.MenuItem()
        Me.mniOrganizar = New System.Windows.Forms.MenuItem()
        Me.mniAyuda = New System.Windows.Forms.MenuItem()
        Me.mnuCambiarClave = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.mniAcerca = New System.Windows.Forms.MenuItem()
        Me.staPrincipal = New System.Windows.Forms.StatusBar()
        Me.stapUsuario = New System.Windows.Forms.StatusBarPanel()
        Me.stapNombre = New System.Windows.Forms.StatusBarPanel()
        Me.stapCelula = New System.Windows.Forms.StatusBarPanel()
        Me.stapFecha = New System.Windows.Forms.StatusBarPanel()
        Me.sbpServidor = New System.Windows.Forms.StatusBarPanel()
        Me.sbpBaseDeDatos = New System.Windows.Forms.StatusBarPanel()
        Me.sbpVersion = New System.Windows.Forms.StatusBarPanel()
        Me.mnuRemoto = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuremArchivo = New System.Windows.Forms.MenuItem()
        Me.mnuremCallCenter = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.mnuremBitacora = New System.Windows.Forms.MenuItem()
        Me.mnuremServiciosTecnicos = New System.Windows.Forms.MenuItem()
        Me.mnuremReportes = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.mnuremSalir = New System.Windows.Forms.MenuItem()
        Me.mnuremHelp = New System.Windows.Forms.MenuItem()
        Me.mnuremCambioPassword = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.mnuremAcercade = New System.Windows.Forms.MenuItem()
        Me.ControlAlarma = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenu1 = New System.Windows.Forms.ContextMenu()
        Me.NotificationWindow = New VbPowerPack.NotificationWindow(Me.components)
        Me.TabBar1 = New Sigamet.TabBar()
        CType(Me.stapUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stapNombre, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stapCelula, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.stapFecha, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpServidor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpBaseDeDatos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.sbpVersion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'mnuPrincipal
        '
        Me.mnuPrincipal.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mniArchivo, Me.mnuConfirmacion, Me.mniVentasMultinivel, Me.mnuPromotor, Me.mniEmpresas, Me.mnuReporte, Me.mnuCalidad, Me.mnuFolios, Me.MenuItem7, Me.mniVentana, Me.mniAyuda})
        '
        'mniArchivo
        '
        Me.mniArchivo.Index = 0
        Me.mniArchivo.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mniCallCenter, Me.mnuConsultaBoletin, Me.mnuPedidoWeb, Me.mnuBitacora, Me.mnuSuministroPronosticado, Me.mnuClienteRuta, Me.mnuTraspaso, Me.mnuRemCelulaA, Me.mnuHorariosRuta, Me.mnuRutaApoyo, Me.mnuNotasRuta, Me.mnuActivacionPedidos, Me.mnuControlDocumentos, Me.mniRelacionNotas, Me.mnuSep1, Me.mnuCargaTarjetaRampac, Me.mnuSep2, Me.mnuDescargaTarjetaRampac, Me.mnuDescargaUDS, Me.mnuLiquidacionManual, Me.mnuVales, Me.mnuSep3, Me.mnuConciliacionNotasBlancas, Me.mnuReposicionNotasBlancas, Me.MenuItem13, Me.mnuModificaLiquidacion, Me.mnuLiquidacionesPendientes, Me.MenuItem36, Me.mnuCancelacionPedido, Me.mnuRuta80, Me.MenuItem17, Me.mnuServicioTecnico, Me.mnuComodato, Me.MenuItem3, Me.mnuAdmEdificios, Me.mnuDivAdm, Me.mnuDescargaRI, Me.mnuCargaRI, Me.mnuTableroAlarma, Me.MenuItem5, Me.mniSalir, Me.MenuItem2})
        Me.mniArchivo.Text = "&Archivo"
        '
        'mniCallCenter
        '
        Me.mniCallCenter.Index = 0
        Me.mniCallCenter.Shortcut = System.Windows.Forms.Shortcut.CtrlF5
        Me.mniCallCenter.Text = "&Call center"
        '
        'mnuConsultaBoletin
        '
        Me.mnuConsultaBoletin.Index = 1
        Me.mnuConsultaBoletin.Shortcut = System.Windows.Forms.Shortcut.CtrlF9
        Me.mnuConsultaBoletin.Text = "Boletines"
        '
        'mnuPedidoWeb
        '
        Me.mnuPedidoWeb.Index = 2
        Me.mnuPedidoWeb.Text = "Pedidos WEB"
        '
        'mnuBitacora
        '
        Me.mnuBitacora.Index = 3
        Me.mnuBitacora.Text = "Bitácora"
        '
        'mnuSuministroPronosticado
        '
        Me.mnuSuministroPronosticado.Index = 4
        Me.mnuSuministroPronosticado.Text = "&Suministro pronosticado"
        '
        'mnuClienteRuta
        '
        Me.mnuClienteRuta.Index = 5
        Me.mnuClienteRuta.Text = "Clientes por ruta"
        '
        'mnuTraspaso
        '
        Me.mnuTraspaso.Index = 6
        Me.mnuTraspaso.Text = "Traspaso de clientes por ruta"
        '
        'mnuRemCelulaA
        '
        Me.mnuRemCelulaA.Index = 7
        Me.mnuRemCelulaA.Text = "Remisiones para Celula A"
        '
        'mnuHorariosRuta
        '
        Me.mnuHorariosRuta.Index = 8
        Me.mnuHorariosRuta.Text = "&Horarios por ruta"
        '
        'mnuRutaApoyo
        '
        Me.mnuRutaApoyo.Index = 9
        Me.mnuRutaApoyo.Text = "&Rutas de apoyo"
        '
        'mnuNotasRuta
        '
        Me.mnuNotasRuta.Index = 10
        Me.mnuNotasRuta.Text = "Notas blancas por ruta"
        '
        'mnuActivacionPedidos
        '
        Me.mnuActivacionPedidos.Index = 11
        Me.mnuActivacionPedidos.Text = "Reactivación de pedidos cancelados"
        '
        'mnuControlDocumentos
        '
        Me.mnuControlDocumentos.Index = 12
        Me.mnuControlDocumentos.Text = "&Control de documentos"
        '
        'mniRelacionNotas
        '
        Me.mniRelacionNotas.Index = 13
        Me.mniRelacionNotas.Text = "&Relación de notas"
        '
        'mnuSep1
        '
        Me.mnuSep1.Index = 14
        Me.mnuSep1.Text = "-"
        '
        'mnuCargaTarjetaRampac
        '
        Me.mnuCargaTarjetaRampac.Index = 15
        Me.mnuCargaTarjetaRampac.Text = "Carga de &tarjeta rampac (generación de remisiones y programaciones)"
        '
        'mnuSep2
        '
        Me.mnuSep2.Index = 16
        Me.mnuSep2.Text = "-"
        '
        'mnuDescargaTarjetaRampac
        '
        Me.mnuDescargaTarjetaRampac.Index = 17
        Me.mnuDescargaTarjetaRampac.Text = "&Descarga de tarjeta rampac y liquidación de ruta"
        '
        'mnuDescargaUDS
        '
        Me.mnuDescargaUDS.Index = 18
        Me.mnuDescargaUDS.Text = "Descarga de datos del archivo &UDS"
        '
        'mnuLiquidacionManual
        '
        Me.mnuLiquidacionManual.Index = 19
        Me.mnuLiquidacionManual.Text = "&Liquidación manual de ruta"
        '
        'mnuVales
        '
        Me.mnuVales.Index = 20
        Me.mnuVales.Text = "Captura de &vales promocionales"
        '
        'mnuSep3
        '
        Me.mnuSep3.Index = 21
        Me.mnuSep3.Text = "-"
        '
        'mnuConciliacionNotasBlancas
        '
        Me.mnuConciliacionNotasBlancas.Index = 22
        Me.mnuConciliacionNotasBlancas.Text = "Conciliación de &notas blancas"
        '
        'mnuReposicionNotasBlancas
        '
        Me.mnuReposicionNotasBlancas.Index = 23
        Me.mnuReposicionNotasBlancas.Text = "&Reposición de notas blancas"
        Me.mnuReposicionNotasBlancas.Visible = False
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 24
        Me.MenuItem13.Text = "-"
        '
        'mnuModificaLiquidacion
        '
        Me.mnuModificaLiquidacion.Index = 25
        Me.mnuModificaLiquidacion.Text = "Modificación de Liquidaciones"
        '
        'mnuLiquidacionesPendientes
        '
        Me.mnuLiquidacionesPendientes.Index = 26
        Me.mnuLiquidacionesPendientes.Text = "Liquidaciones pendientes de realizar"
        '
        'MenuItem36
        '
        Me.MenuItem36.Index = 27
        Me.MenuItem36.Text = "-"
        '
        'mnuCancelacionPedido
        '
        Me.mnuCancelacionPedido.Index = 28
        Me.mnuCancelacionPedido.Text = "Cancelación de pedidos"
        '
        'mnuRuta80
        '
        Me.mnuRuta80.Index = 29
        Me.mnuRuta80.Text = "Ruta 80"
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 30
        Me.MenuItem17.Text = "-"
        '
        'mnuServicioTecnico
        '
        Me.mnuServicioTecnico.Index = 31
        Me.mnuServicioTecnico.Text = "Servicios técnicos"
        '
        'mnuComodato
        '
        Me.mnuComodato.Index = 32
        Me.mnuComodato.Text = "Comodato"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 33
        Me.MenuItem3.Text = "-"
        '
        'mnuAdmEdificios
        '
        Me.mnuAdmEdificios.Index = 34
        Me.mnuAdmEdificios.Text = "&Administración de edificios"
        '
        'mnuDivAdm
        '
        Me.mnuDivAdm.Index = 35
        Me.mnuDivAdm.Text = "-"
        '
        'mnuDescargaRI
        '
        Me.mnuDescargaRI.Index = 36
        Me.mnuDescargaRI.Text = "Descarga RI"
        '
        'mnuCargaRI
        '
        Me.mnuCargaRI.Index = 37
        Me.mnuCargaRI.Text = "Carga RI"
        '
        'mnuTableroAlarma
        '
        Me.mnuTableroAlarma.Index = 38
        Me.mnuTableroAlarma.Text = "Tablero de alarmas"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 39
        Me.MenuItem5.Text = "-"
        '
        'mniSalir
        '
        Me.mniSalir.Index = 40
        Me.mniSalir.Text = "&Salir"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 41
        Me.MenuItem2.Text = "***LIQ"
        Me.MenuItem2.Visible = False
        '
        'mnuConfirmacion
        '
        Me.mnuConfirmacion.Index = 1
        Me.mnuConfirmacion.Text = "&Confirmación de pedidos"
        '
        'mniVentasMultinivel
        '
        Me.mniVentasMultinivel.Index = 2
        Me.mniVentasMultinivel.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mniRelacionClientes})
        Me.mniVentasMultinivel.Text = "Ventas &multinivel"
        '
        'mniRelacionClientes
        '
        Me.mniRelacionClientes.Index = 0
        Me.mniRelacionClientes.Text = "&Relación de clientes"
        '
        'mnuPromotor
        '
        Me.mnuPromotor.Enabled = False
        Me.mnuPromotor.Index = 3
        Me.mnuPromotor.Text = "&Promotores"
        Me.mnuPromotor.Visible = False
        '
        'mniEmpresas
        '
        Me.mniEmpresas.Index = 4
        Me.mniEmpresas.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mniEmpresa})
        Me.mniEmpresas.MergeOrder = 12
        Me.mniEmpresas.Text = "&Empresas"
        '
        'mniEmpresa
        '
        Me.mniEmpresa.Index = 0
        Me.mniEmpresa.Text = "&Empresas"
        '
        'mnuReporte
        '
        Me.mnuReporte.Index = 5
        Me.mnuReporte.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuReportes, Me.mniReportesEspeciales})
        Me.mnuReporte.Text = "&Reportes"
        '
        'mnuReportes
        '
        Me.mnuReportes.Index = 0
        Me.mnuReportes.Text = "&Reportes..."
        '
        'mniReportesEspeciales
        '
        Me.mniReportesEspeciales.Index = 1
        Me.mniReportesEspeciales.Text = "&Reportes especiales"
        '
        'mnuCalidad
        '
        Me.mnuCalidad.Index = 6
        Me.mnuCalidad.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mniCatalgoCallesColonias, Me.mniDepuradorColonias, Me.mniDepuradorCalles, Me.mniHistorialDepuracion})
        Me.mnuCalidad.Text = "&Calidad"
        '
        'mniCatalgoCallesColonias
        '
        Me.mniCatalgoCallesColonias.Index = 0
        Me.mniCatalgoCallesColonias.Text = "&Catalogo de calles y colonias"
        '
        'mniDepuradorColonias
        '
        Me.mniDepuradorColonias.Index = 1
        Me.mniDepuradorColonias.Text = "&Depurador de colonias"
        '
        'mniDepuradorCalles
        '
        Me.mniDepuradorCalles.Index = 2
        Me.mniDepuradorCalles.Text = "D&epurador de calles"
        '
        'mniHistorialDepuracion
        '
        Me.mniHistorialDepuracion.Index = 3
        Me.mniHistorialDepuracion.Text = "&Historial de depuracion"
        '
        'mnuFolios
        '
        Me.mnuFolios.Index = 7
        Me.mnuFolios.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAsignacionFolios, Me.mnuCancelacionFolios})
        Me.mnuFolios.Text = "&Control de folios"
        '
        'mnuAsignacionFolios
        '
        Me.mnuAsignacionFolios.Index = 0
        Me.mnuAsignacionFolios.Text = "&Asignación de folios"
        '
        'mnuCancelacionFolios
        '
        Me.mnuCancelacionFolios.Index = 1
        Me.mnuCancelacionFolios.Text = "&Cancelación de folios"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 8
        Me.MenuItem7.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mniPwdCambio, Me.mniContactos, Me.mnuEditorPortal, Me.mnuQuejas, Me.mnuSolicitudCredito, Me.mnuMonedero, Me.mnuGruposComerciales, Me.mnuAuxilioFallas, Me.mnuFugasPortatil})
        Me.MenuItem7.Text = "&Herramientas"
        '
        'mniPwdCambio
        '
        Me.mniPwdCambio.Index = 0
        Me.mniPwdCambio.Text = "&Cambio de password"
        '
        'mniContactos
        '
        Me.mniContactos.Index = 1
        Me.mniContactos.Text = "C&ontactos"
        '
        'mnuEditorPortal
        '
        Me.mnuEditorPortal.Index = 2
        Me.mnuEditorPortal.Text = "&Editor HTML"
        '
        'mnuQuejas
        '
        Me.mnuQuejas.Index = 3
        Me.mnuQuejas.Text = "&Quejas"
        '
        'mnuSolicitudCredito
        '
        Me.mnuSolicitudCredito.Index = 4
        Me.mnuSolicitudCredito.Text = "&Solicitudes de crédito"
        '
        'mnuMonedero
        '
        Me.mnuMonedero.Index = 5
        Me.mnuMonedero.Text = "&Monedero electrónico"
        '
        'mnuGruposComerciales
        '
        Me.mnuGruposComerciales.Index = 6
        Me.mnuGruposComerciales.Text = "&Grupos Comerciales"
        '
        'mnuAuxilioFallas
        '
        Me.mnuAuxilioFallas.Index = 7
        Me.mnuAuxilioFallas.Text = "Auxilio y fallas"
        '
        'mnuFugasPortatil
        '
        Me.mnuFugasPortatil.Index = 8
        Me.mnuFugasPortatil.Text = "Fugas portatil"
        '
        'mniVentana
        '
        Me.mniVentana.Index = 9
        Me.mniVentana.MdiList = True
        Me.mniVentana.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mniCascada, Me.mniMosaicoHorizontal, Me.mniMosaicoVertical, Me.mniOrganizar})
        Me.mniVentana.MergeOrder = 16
        Me.mniVentana.ShowShortcut = False
        Me.mniVentana.Text = "&Ventana"
        '
        'mniCascada
        '
        Me.mniCascada.Index = 0
        Me.mniCascada.Text = "&Cascada"
        '
        'mniMosaicoHorizontal
        '
        Me.mniMosaicoHorizontal.Index = 1
        Me.mniMosaicoHorizontal.Text = "Mosaico &Horizontal"
        '
        'mniMosaicoVertical
        '
        Me.mniMosaicoVertical.Index = 2
        Me.mniMosaicoVertical.Text = "Mosaico &Vertical"
        '
        'mniOrganizar
        '
        Me.mniOrganizar.Index = 3
        Me.mniOrganizar.Text = "&Organizar"
        '
        'mniAyuda
        '
        Me.mniAyuda.Index = 10
        Me.mniAyuda.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuCambiarClave, Me.MenuItem1, Me.mniAcerca})
        Me.mniAyuda.MergeOrder = 17
        Me.mniAyuda.Text = "&?X"
        '
        'mnuCambiarClave
        '
        Me.mnuCambiarClave.Index = 0
        Me.mnuCambiarClave.Text = "Cambiar contraseña SigaMet"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 1
        Me.MenuItem1.Text = "-"
        '
        'mniAcerca
        '
        Me.mniAcerca.Index = 2
        Me.mniAcerca.Text = "&Acerca de..."
        '
        'staPrincipal
        '
        Me.staPrincipal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.staPrincipal.Location = New System.Drawing.Point(0, -41)
        Me.staPrincipal.Name = "staPrincipal"
        Me.staPrincipal.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.stapUsuario, Me.stapNombre, Me.stapCelula, Me.stapFecha, Me.sbpServidor, Me.sbpBaseDeDatos, Me.sbpVersion})
        Me.staPrincipal.ShowPanels = True
        Me.staPrincipal.Size = New System.Drawing.Size(1031, 21)
        Me.staPrincipal.TabIndex = 1
        '
        'stapUsuario
        '
        Me.stapUsuario.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.stapUsuario.Icon = CType(resources.GetObject("stapUsuario.Icon"), System.Drawing.Icon)
        Me.stapUsuario.Name = "stapUsuario"
        Me.stapUsuario.ToolTipText = "Usuario del sistema"
        Me.stapUsuario.Width = 150
        '
        'stapNombre
        '
        Me.stapNombre.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.stapNombre.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.stapNombre.Name = "stapNombre"
        Me.stapNombre.ToolTipText = "Nombre del usuario del sistema"
        Me.stapNombre.Width = 252
        '
        'stapCelula
        '
        Me.stapCelula.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.stapCelula.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
        Me.stapCelula.Name = "stapCelula"
        Me.stapCelula.ToolTipText = "Célula a la que corresponde el usuario"
        Me.stapCelula.Width = 252
        '
        'stapFecha
        '
        Me.stapFecha.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.stapFecha.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents
        Me.stapFecha.Name = "stapFecha"
        Me.stapFecha.Width = 10
        '
        'sbpServidor
        '
        Me.sbpServidor.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.sbpServidor.Icon = CType(resources.GetObject("sbpServidor.Icon"), System.Drawing.Icon)
        Me.sbpServidor.Name = "sbpServidor"
        Me.sbpServidor.ToolTipText = "Nombre del servidor"
        Me.sbpServidor.Width = 150
        '
        'sbpBaseDeDatos
        '
        Me.sbpBaseDeDatos.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.sbpBaseDeDatos.Name = "sbpBaseDeDatos"
        Me.sbpBaseDeDatos.ToolTipText = "Nombre de la base de datos"
        '
        'sbpVersion
        '
        Me.sbpVersion.Alignment = System.Windows.Forms.HorizontalAlignment.Center
        Me.sbpVersion.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None
        Me.sbpVersion.Name = "sbpVersion"
        '
        'mnuRemoto
        '
        Me.mnuRemoto.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuremArchivo, Me.mnuremHelp})
        '
        'mnuremArchivo
        '
        Me.mnuremArchivo.Index = 0
        Me.mnuremArchivo.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuremCallCenter, Me.MenuItem6, Me.mnuremBitacora, Me.mnuremServiciosTecnicos, Me.mnuremReportes, Me.MenuItem4, Me.mnuremSalir})
        Me.mnuremArchivo.Text = "&Archivo"
        '
        'mnuremCallCenter
        '
        Me.mnuremCallCenter.Index = 0
        Me.mnuremCallCenter.Shortcut = System.Windows.Forms.Shortcut.F5
        Me.mnuremCallCenter.Text = "&CallCenter"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 1
        Me.MenuItem6.Text = "&Boletines"
        '
        'mnuremBitacora
        '
        Me.mnuremBitacora.Index = 2
        Me.mnuremBitacora.Text = "&Bitácora"
        '
        'mnuremServiciosTecnicos
        '
        Me.mnuremServiciosTecnicos.Index = 3
        Me.mnuremServiciosTecnicos.Text = "&Servicios técnicos"
        '
        'mnuremReportes
        '
        Me.mnuremReportes.Index = 4
        Me.mnuremReportes.Text = "&Reportes"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 5
        Me.MenuItem4.Text = "-"
        '
        'mnuremSalir
        '
        Me.mnuremSalir.Index = 6
        Me.mnuremSalir.Text = "&Salir"
        '
        'mnuremHelp
        '
        Me.mnuremHelp.Index = 1
        Me.mnuremHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuremCambioPassword, Me.MenuItem10, Me.mnuremAcercade})
        Me.mnuremHelp.Text = "&?"
        '
        'mnuremCambioPassword
        '
        Me.mnuremCambioPassword.Index = 0
        Me.mnuremCambioPassword.Text = "Cambio de contraseña"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "-"
        '
        'mnuremAcercade
        '
        Me.mnuremAcercade.Index = 2
        Me.mnuremAcercade.Text = "Acerca de..."
        '
        'ControlAlarma
        '
        Me.ControlAlarma.Icon = CType(resources.GetObject("ControlAlarma.Icon"), System.Drawing.Icon)
        Me.ControlAlarma.Text = "Alarmas"
        Me.ControlAlarma.Visible = True
        '
        'NotificationWindow
        '
        Me.NotificationWindow.Blend = New VbPowerPack.BlendFill(VbPowerPack.BlendStyle.Vertical, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.Window)
        Me.NotificationWindow.DefaultText = Nothing
        Me.NotificationWindow.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NotificationWindow.ForeColor = System.Drawing.SystemColors.ControlText
        '
        'TabBar1
        '
        Me.TabBar1.AutoHide = True
        Me.TabBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TabBar1.FocusedColor = System.Drawing.Color.LemonChiffon
        Me.TabBar1.Location = New System.Drawing.Point(0, 0)
        Me.TabBar1.Name = "TabBar1"
        Me.TabBar1.Size = New System.Drawing.Size(1031, 23)
        Me.TabBar1.TabIndex = 3
        Me.TabBar1.UnfocusedColor = System.Drawing.SystemColors.Control
        '
        'frmPrincipal
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(1031, -20)
        Me.Controls.Add(Me.staPrincipal)
        Me.Controls.Add(Me.TabBar1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Menu = Me.mnuPrincipal
        Me.Name = "frmPrincipal"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SigaMET"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.stapUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stapNombre, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stapCelula, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.stapFecha, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpServidor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpBaseDeDatos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.sbpVersion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Ventanas"

    Private Sub mniCascada_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniCascada.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub mniMosaico_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniMosaicoHorizontal.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub mniMosaicoVertical_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniMosaicoVertical.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub
    Private Sub mniOrganizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniOrganizar.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

#End Region

    Private Sub mniCallCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniCallCenter.Click
        CallCenter()
    End Sub

    Private Sub mniSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniSalir.Click
        Me.Close()
    End Sub

    Private Sub mniEmpresa_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniEmpresa.Click
        CatalogoEmpresa()
    End Sub

    Private Sub MenuItem35_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCancelacionPedido.Click
        Dim frmCancelacionC6 As CancelacionC6 = New CancelacionC6()
        frmCancelacionC6.ShowDialog()
        frmCancelacionC6.Dispose()
    End Sub

    Private Sub MenuItem50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRuta80.Click
        Dim frmRuta80 As Ruta80 = New Ruta80()
        frmRuta80.ShowDialog()
        frmRuta80.Dispose()
    End Sub

    Private Sub MenuItem54_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frmBitacoraProgramacion As BitacoraProgramacion = New BitacoraProgramacion()
        frmBitacoraProgramacion.ShowDialog()
        frmBitacoraProgramacion.Dispose()
    End Sub

    Private Sub mnuHorarioRuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        HorarioRuta()
    End Sub

    Private Sub mnuConsultaBoletin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConsultaBoletin.Click
        Boletin(_URLGateway)
    End Sub

    Private Sub Boletin(Optional ByVal URLGateway As String = "")
        If (oSeguridad.TieneAcceso("Boletin")) Then
            Dim f As Form
            For Each f In Me.MdiChildren
                If f.Name = "frmBoletin" Then
                    f.Focus()
                    Exit Sub
                End If
            Next
            Cursor = Cursors.WaitCursor
            'Si el UrlGateway no es recuperado, se usa el constructor por defecto
            If (String.IsNullOrEmpty(URLGateway)) Then
                Dim oBoletin As New frmBoletin()
                oBoletin.MdiParent = Me
                oBoletin.Show()
            Else
                'Si el UrlGateway  es recuperado, se usa el constructor con _urlGateway
                Dim oBoletin As New frmBoletin(_URLGateway)
                oBoletin.MdiParent = Me
                oBoletin.Show()
            End If


            Cursor = Cursors.Default
        Else
            MessageBox.Show("No tiene acceso a esta operación.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub ClienteRuta()
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmClienteRuta" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        Dim oClienteRuta As New frmClienteRuta()
        oClienteRuta.MdiParent = Me
        oClienteRuta.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mnuClienteRuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClienteRuta.Click
        ClienteRuta()
    End Sub

    Private Sub CambioClave()
        Cursor = Cursors.WaitCursor
        Dim oPassword As New SigaMetClasses.CambioClave(Main.GLOBAL_Usuario)
        oPassword.ShowDialog()
        Cursor = Cursors.Default
    End Sub

    Private Sub AcercaDe()
        Dim oAcercaDe As New SigaMetClasses.AcercaDe("CallCenter", Application.ProductVersion, "Gas Metropolitano, S.A. de C.V.")
        oAcercaDe.ShowDialog()
    End Sub

#Region "RI"
    Private Sub mnuDescargaRI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDescargaRI.Click
        Cursor = Cursors.WaitCursor
        oRI = New RI500.InterfaseRI(Main.CnnSigamet, Main.GLOBAL_Usuario, Main.GLOBAL_Celula, RI500.InterfaseRI.TipoInterfaz.Descarga, CType(System.Configuration.ConfigurationManager.AppSettings("DSNFileRI"), String).Trim)
        oRI.MdiParent = Me
        oRI.Show()

        Cursor = Cursors.Default

    End Sub

    Private Sub oRI_Liquidacion(ByVal sender As Object, ByVal e As RI500.RI500.DescargaPedidosEventArgs) Handles oRI.Liquidacion

    End Sub
#End Region

    Private Sub mnuCambiarClave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCambiarClave.Click
        CambioClave()
    End Sub

#Region "Menú"
    Private Sub mnuCargaTarjetaRampac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCargaTarjetaRampac.Click
        Dim Fecha As DateTime
        Dim Ruta As Integer
        Dim Folio As Integer
        Dim Anioatt As Integer
        Dim Acepto As Boolean
        Dim Descarga As Boolean

        If Global_TipoLiquidacion = "S" Then
            'TODO: Descarga rampac para liquidación con remisión, para llamar a la pantalla de interfase con la estructura para
            'de rampac para sigamet corporativo 15/12/2004 JAGD
            Dim frmRampac As frmInterfaseCorporativo = New frmInterfaseCorporativo()
            frmRampac.Entrada(0)
            frmRampac.ShowDialog()
            Acepto = frmRampac._Acepto
            If Acepto = True Then
                Fecha = frmRampac._Fecha
                Ruta = frmRampac._Ruta
                Folio = frmRampac._FolioP
                Anioatt = frmRampac._Anioatt
                Descarga = frmRampac._Descarga
            End If
            frmRampac.Dispose()
        Else
            'TODO: Descarga rampac para Liquidación para Gas Metropolitano 15/12/2004 JAGD
            Dim frmRampac As frmInterfase = New frmInterfase()
            frmRampac.Entrada(0)
            frmRampac.ShowDialog()
            Acepto = frmRampac._Acepto
            If Acepto = True Then
                Fecha = frmRampac._Fecha
                Ruta = frmRampac._Ruta
                Folio = frmRampac._FolioP
                Anioatt = frmRampac._Anioatt
                Descarga = frmRampac._Descarga
            End If
            frmRampac.Dispose()
        End If


    End Sub

    Private Sub mnuDescargaTarjetaRampac_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDescargaTarjetaRampac.Click
        Dim Fecha As DateTime
        Dim Ruta As Integer
        Dim Folio As Integer
        Dim Anioatt As Integer
        Dim Acepto As Boolean
        Dim Descarga As Boolean
        Dim Celula As Integer 'Modificacion para cualquier celula por usuario. MHV.

        If Global_TipoLiquidacion = "N" Then

            'TODO: Descarga Rampac, para sigamet metro
            Dim frmRampac As frmInterfase = New frmInterfase()
            frmRampac.Entrada(1)
            frmRampac.ShowDialog()
            Acepto = frmRampac._Acepto
            If Acepto = True Then
                Fecha = frmRampac._Fecha
                Ruta = frmRampac._Ruta
                Folio = frmRampac._FolioP
                Anioatt = frmRampac._Anioatt
                Descarga = frmRampac._Descarga
                Celula = frmRampac._Celula
            End If
            frmRampac.Dispose()

        Else

            'TODO: Descarga Rampac, para sigamet Corporativo
            Dim frmRampac As frmInterfaseCorporativo = New frmInterfaseCorporativo()
            frmRampac.Entrada(1)
            frmRampac.ShowDialog()
            Acepto = frmRampac._Acepto
            If Acepto = True Then
                Fecha = frmRampac._Fecha
                Ruta = frmRampac._Ruta
                Folio = frmRampac._FolioP
                Anioatt = frmRampac._Anioatt
                Descarga = frmRampac._Descarga
                Celula = frmRampac._Celula
            End If
            frmRampac.Dispose()

        End If

        If Acepto = True Then
            Dim x As Form = Nothing

            'TODO: VALIDACION DE CREDITO DE CLIENTE EN LIQUIDACION incluida el día 17/10/2004

            'Validacion de una u otra forma de liquidar segun parametros MHV 02/11/2004

            'TODO: Validar permiso
            'oSeguridad.TieneAcceso("LiquidacionManual")
            If oSeguridad.TieneAcceso("LiquidacionManual") Then
                LlamadaLiquidacion(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
                'No debe abrir la pantalla de liquidación si no tiene permisos.
                'Else
                '    MessageBox.Show("No tiene acceso a esta operación.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
            'If Global_TipoLiquidacion = "N" Then
            '    For Each x In Me.MdiChildren
            '        If x.Name = "Liquidacion" Then
            '            x.Focus()
            '            Exit Sub
            '        End If
            '    Next

            '    Dim mdiLiquidacion As Liquidacion2005 = New Liquidacion2005(GLOBAL_AplicaValidacionCredito)
            '    mdiLiquidacion.MdiParent = Me
            '    Me.Cursor = Cursors.WaitCursor
            '    mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            '    Me.Cursor = Cursors.Default
            '    'mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga)
            'Else
            '    For Each x In Me.MdiChildren
            '        If x.Name = "LiquidacionRemision" Then
            '            x.Focus()
            '            Exit Sub
            '        End If
            '    Next

            '    'Dim mdiLiquidacion As LiquidacionRemision = New LiquidacionRemision(GLOBAL_AplicaValidacionCredito)
            '    'mdiLiquidacion.MdiParent = Me
            '    'mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)

            '    Dim mdiLiquidacion As Liquidacion2.Liquidacion2005 = New Liquidacion2.Liquidacion2005(Global_TipoLiquidacion, _
            '            GLOBAL_AdmEdificiosLiquidacionCredito, _
            '            GLOBAL_SaldoAFavorLiquidacion, _
            '            GLOBAL_ValidaSerieRemision, _
            '            GLOBAL_RamoClienteAdmEdificios, _
            '            GLOBAL_ClaveRamoClienteAdmEdificios, _
            '            GLOBAL_VentasMultinivel, _
            '            GLOBAL_ClaveCreditoAutorizado, _
            '            GLOBAL_AplicaValidacionCredito, _
            '            GLOBAL_Usuario, _
            '            GLOBAL_Password, _
            '            CnnSigamet)

            '    mdiLiquidacion.MdiParent = Me
            '    mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            '    mdiLiquidacion.Show()
            'End If

        End If

    End Sub

    'TODO: Implementar permiso para acceder a esta funcionalidad
    Private Sub mnuLiquidacionManual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLiquidacionManual.Click
        'If oSeguridad.TieneAcceso("Administración de edificios") Then
        If oSeguridad.TieneAcceso("LiquidacionManual") Then
            Liquidacion()
        Else
            MessageBox.Show("No tiene acceso a esta operación.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub mnuConciliacionNotasBlancas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuConciliacionNotasBlancas.Click
        Conciliacion()
    End Sub

    'Private Sub mnuReposicionNotasBlancas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReposicionNotasBlancas.Click
    '    Dim frmReposicion As Reposicion = New Reposicion()
    '    frmReposicion.Entrada()
    '    frmReposicion.Dispose()
    'End Sub

    Private Sub mnuModificaLiquidacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuModificaLiquidacion.Click
        Dim x As Form
        For Each x In Me.MdiChildren
            If x.Name = "TerminoLiquidacion" Then
                x.Focus()
                Exit Sub
            End If
        Next


        Dim mdiModifica As TerminoLiquidacion = New TerminoLiquidacion()
        mdiModifica.MdiParent = Me
        mdiModifica.Show()
    End Sub


    Private Sub mnuLiquidacionesPendientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLiquidacionesPendientes.Click
        Cursor = Cursors.WaitCursor
        Dim oLiqPendiente As New frmLiquidacionesPendientes()
        oLiqPendiente.ShowDialog()
        Cursor = Cursors.Default
    End Sub


#End Region


#Region "Menú para acceso remoto"
    Private Sub mnuremCallCenter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremCallCenter.Click
        CallCenter()
    End Sub

    Private Sub mnuremServiciosTecnicos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremServiciosTecnicos.Click
        ServicioTecnico()
    End Sub

    Private Sub mnuremReportes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremReportes.Click
        ConsultaReportesModulo()
    End Sub

    Private Sub mnuremSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremSalir.Click
        Me.Close()
    End Sub

    Private Sub mnuremCambioPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremCambioPassword.Click
        CambioClave()
    End Sub

    Private Sub mnuremAcercade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremAcercade.Click
        AcercaDe()
    End Sub

    Private Sub mnuremBitacora_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuremBitacora.Click
        BitacoraUsuario()
    End Sub

#End Region


    Private Sub MenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem2.Click
        'Dim oSelRuta As New frmSeleccionaRutaPreliquidacion()
        'If oSelRuta.ShowDialog = DialogResult.OK Then
        '    Dim oLiqEst As New frmLiqEstacionario(oSelRuta.AñoAtt, oSelRuta.Folio)
        '    oLiqEst.Show()

        'End If

        'TODO: Código para llamar la nueva liquidación
        'Dim f As Form
        'For Each f In Me.MdiChildren
        '    If f.Name = "frmSeleccionaRutaPreliquidacion" Then
        '        f.Focus()
        '        Exit Sub
        '    End If
        'Next

        'Dim frmLecturaOperador As LecturaOperador = New LecturaOperador()
        'Cursor = Cursors.WaitCursor
        'Dim oSelRuta As New frmSeleccionaRutaPreliquidacion()
        'If oSelRuta.ShowDialog = DialogResult.OK Then
        '    Dim Fecha As Date
        '    Dim Ruta As Integer
        '    Dim Folio As Integer
        '    Dim Anioatt As Integer
        '    Dim Acepto As Boolean
        '    Dim Descarga As Boolean

        '    Fecha = oSelRuta.Fecha
        '    Ruta = oSelRuta.Ruta
        '    Folio = oSelRuta.Folio
        '    Anioatt = oSelRuta.AñoAtt
        '    Descarga = oSelRuta.Descarga


        '    Dim oLiquidacion As New LiquidacionEstacionario.Liquidacion(CType(Anioatt, Short), Folio, Main.GLOBAL_Precio, Main.GLOBAL_Celula, Descarga, False, False)
        '    oLiquidacion.ShowDialog()

        '    Cursor = Cursors.Default


        'End If
        'Cursor = Cursors.Default

        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmSeleccionaRutaPreliquidacion" Then
                f.Focus()
                Exit Sub
            End If
        Next

        'Dim frmLecturaOperador As LecturaOperador = New LecturaOperador()
        Cursor = Cursors.WaitCursor
        Dim oSelRuta As New frmSeleccionaRutaPreliquidacion()
        If oSelRuta.ShowDialog = DialogResult.OK Then
            Dim Fecha As Date
            Dim Ruta As Integer
            Dim Folio As Integer
            Dim Anioatt As Integer
            Dim Acepto As Boolean = Nothing
            Dim Descarga As Boolean
            Dim Celula As Integer

            Fecha = oSelRuta.Fecha
            Ruta = oSelRuta.Ruta
            Folio = oSelRuta.Folio
            Anioatt = oSelRuta.AñoAtt
            Descarga = oSelRuta.Descarga
            Celula = oSelRuta._Celula
            'TODO: Llamada a la liquidación sin validación
            'En esta llamada no aplica la validación de créditos del parámetro GLOBAL_AplicaValidacionCredito
            Dim mdiLiquidacion As Liquidacion = New Liquidacion(False)
            mdiLiquidacion.MdiParent = Me
            Cursor = Cursors.Default
            mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga, Celula)
            'mdiLiquidacion.Entrada(Fecha, Ruta, Folio, Anioatt, Descarga)
            mdiLiquidacion.Show()

        End If
        Cursor = Cursors.Default

    End Sub

#Region "Administracion de edificios"
    'Se agregó el 30/09/2004, funcionalidad para captura de lecturas de administracion de edificios JAGD
    'TODO: Desactivar el menú, si el yuser no tiene la opererion asignada
    Private Sub mnuAdmEdificios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim admEdificios As New AdmEdificios.frmCatEdificios(GLOBAL_Celula, CnnSigamet, GLOBAL_Usuario)
        'admEdificios.StartPosition = FormStartPosition.CenterScreen
        'admEdificios.ShowDialog()


        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmCatEdificios" Then
                f.Focus()
                Exit Sub
            End If
        Next

        Cursor = Cursors.WaitCursor
        Dim admEdificios As New AdmEdificios.frmCatEdificios(GLOBAL_Celula,
                                                             CnnSigamet,
                                                             GLOBAL_Usuario,
                                                             GLOBAL_Corporativo,
                                                             GLOBAL_Sucursal,
                                                             _URLGateway,
                                                             GLOBAL_ConString)
        admEdificios.MdiParent = Me
        admEdificios.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub activarMenuAdmEdificios(ByVal activar As Boolean)
        If activar Then
            If oSeguridad.TieneAcceso("Administración de edificios") Then
                AddHandler mnuAdmEdificios.Click, AddressOf mnuAdmEdificios_Click
            Else
                mnuAdmEdificios.Enabled = False
            End If
        Else
            mnuAdmEdificios.Visible = False
            mnuDivAdm.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuAdmEdificios)
            mnuPrincipal.MenuItems.Remove(mnuDivAdm)
        End If
    End Sub
#End Region

#Region "Calidad"

    Private Sub mnuCallesColonias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniCatalgoCallesColonias.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmCallesColonias" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        CallesColonias.Globales.GetInstance.cnSigamet = CnnSigamet
        CallesColonias.Globales.GetInstance.ModificarStatus = oSeguridad.TieneAcceso("ModificarStatusCalidad")
        Dim frmCallesColonias As New CallesColonias.frmCallesColonias()
        frmCallesColonias.MdiParent = Me
        frmCallesColonias.Show()
        Cursor = Cursors.Default
    End Sub



#End Region

#Region "Control de folios"
    'Se integró a callcenter el día 20/11/2004
    'JAGD
    Private Sub mnuAsignacionFolios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As Form

        'Dim parametro As New SigaMetClasses.cConfig(16, GLOBAL_Corporativo, GLOBAL_Sucursal)
        'Dim rutaReporte As String = CType(parametro.Parametros.Item("RutaReportes"), String)
        'parametro.Dispose()

        For Each f In Me.MdiChildren
            If f.Name = "frmConsultaFoliosAsignados" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Dim frmControlFolios As New ControlFolios.frmConsultaFoliosAsignados()
        'Dim frmControlFolios As New _
        'ControlFolios.frmConsultaFoliosAsignados(GLOBAL_Servidor, GLOBAL_Database, _
        '    GLOBAL_Usuario, GLOBAL_Password, rutaReporte)
        frmControlFolios.MdiParent = Me
        frmControlFolios.WindowState = FormWindowState.Maximized
        frmControlFolios.Show()
    End Sub

    Private Sub mnuCancelacionFolios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As Form
        'Dim parametro As New SigaMetClasses.cConfig(16, GLOBAL_Corporativo, GLOBAL_Sucursal)
        'Dim rutaReporte As String = CType(parametro.Parametros.Item("RutaReportes"), String)
        'parametro.Dispose()
        For Each f In Me.MdiChildren
            If f.Name = "frmConsultaFoliosCancelados" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Dim frmControlFolios As New ControlFolios.frmConsultaFoliosCancelados()
        'Dim frmControlFolios As New _
        'ControlFolios.frmConsultaFoliosCancelados(GLOBAL_Servidor, GLOBAL_Database, _
        '    GLOBAL_Usuario, GLOBAL_Password, rutaReporte)
        frmControlFolios.MdiParent = Me
        frmControlFolios.WindowState = FormWindowState.Maximized
        frmControlFolios.Show()
    End Sub

    Private Sub activarMenuFolios(Optional ByVal activar As Boolean = False)
        If activar Then
            AddHandler mnuAsignacionFolios.Click, AddressOf mnuAsignacionFolios_Click
            AddHandler mnuCancelacionFolios.Click, AddressOf mnuCancelacionFolios_Click

            'Nueva versión de control de folios patrón singleton 16/06/2009
            ControlFolios.Globals.GetInstance.ConfiguraModulo(GLOBAL_Servidor, GLOBAL_Database, _
                GLOBAL_Usuario, GLOBAL_Password, GLOBAL_Corporativo, GLOBAL_Sucursal, _
                GLOBAL_RutaReportes, GLOBAL_ConString)
        Else
            mnuFolios.Enabled = False
            mnuFolios.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuFolios)
        End If

    End Sub

    Private Sub activarValesPromocionales(Optional ByVal activar As Boolean = False)
        If Not activar Then
            mnuVales.Enabled = False
            mnuVales.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuVales)
        End If

    End Sub


    Private Sub activarMenuTraspasos(Optional ByVal activar As Boolean = False)
        If activar Then
            AddHandler mnuTraspaso.Click, AddressOf mnuTraspaso_Click
        Else
            mnuTraspaso.Enabled = False
            mnuTraspaso.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuTraspaso)
        End If
    End Sub

    Private Sub activarMenuRemisionesCelA(Optional ByVal activar As Boolean = False)
        If activar Then
            AddHandler mnuRemCelulaA.Click, AddressOf mnuCelA
        Else
            mnuRemCelulaA.Enabled = False
            mnuRemCelulaA.Visible = False
            mnuRemCelulaA.MenuItems.Remove(mnuTraspaso)
        End If
    End Sub

    Private Sub activarMenuHorarioRuta(Optional ByVal activar As Boolean = False)
        If activar Then
            AddHandler mnuHorariosRuta.Click, AddressOf mnuHorariosColonia_Click
        Else
            mnuHorariosRuta.Enabled = False
            mnuHorariosRuta.Visible = False
            mnuHorariosRuta.MenuItems.Remove(mnuHorariosRuta)
        End If
    End Sub

    Private Sub activarMenuControlDocumentos(Optional ByVal activar As Boolean = False)
        If activar Then
            AddHandler mnuControlDocumentos.Click, AddressOf mnuControlDocumentos_Click
        Else
            mnuControlDocumentos.Enabled = False
            mnuControlDocumentos.Visible = False
            mnuControlDocumentos.MenuItems.Remove(mnuControlDocumentos)
        End If
    End Sub

#End Region

#Region "Liquidacion"

    Private Sub Liquidacion(ByVal useLiquidacion As Boolean)
        If useLiquidacion Then
            Dim mnu As MenuItem
            For Each mnu In mnuPrincipal.MenuItems
                'If mnu.name = "" Then
            Next
        End If
    End Sub

#End Region

#Region "Traspaso de clientes por ruta"

    Private Sub mnuTraspaso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If frm.Name = "" Then
                frm.Focus()
                Exit Sub
            End If
        Next

        Dim frmTraspaso As New frmClienteRutaTraspaso()
        frmTraspaso.MdiParent = Me
        Cursor = Cursors.Default
        frmTraspaso.Show()

    End Sub

#End Region

#Region "Célula A"

    Private Sub mnuCelA(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If frm.Name = "" Then
                frm.Focus()
                Exit Sub
            End If
        Next

        Dim frmRemisionesCelulaA As New RemisionesCelulaA.frmClienteRuta(CnnSigamet)
        frmRemisionesCelulaA.MdiParent = Me
        frmRemisionesCelulaA.Text = "Clientes Célula A"
        Cursor = Cursors.Default
        frmRemisionesCelulaA.Show()

    End Sub

#End Region


    Private Sub MenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem6.Click
        Boletin()
    End Sub

    Private Sub mnuHorariosColonia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "HorariosColonia" Then
                f.Focus()
                Exit Sub
            End If
        Next

        Cursor = Cursors.WaitCursor
        Dim frmHorariosColonia As New HorariosColonia()
        frmHorariosColonia.MdiParent = Me
        frmHorariosColonia.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mniArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniArchivo.Click

    End Sub

#Region "Registro de operacion"

    'Registro de inicios y fines de sesión

    Private Sub RegistroOperacion(ByVal Motivo As Integer)
        Cursor = Cursors.WaitCursor
        Dim strQuery As String = "sp_InsertaLlamada"

        Dim cmd As New SqlCommand(strQuery, Main.CnnSigamet)
        cmd.CommandType = CommandType.StoredProcedure

        Try
            Main.AbreConexion()
            cmd.Parameters.Clear()
            cmd.Parameters.Add("@Cliente", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Observaciones", SqlDbType.Char).Value = "Registro de operaciones"
            cmd.Parameters.Add("@TelefonoOrigen", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@Motivollamada", SqlDbType.Int).Value = Motivo
            cmd.Parameters.Add("@Celula", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@AñoPed", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Pedido", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Operador", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Autotanque", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@Demandante", SqlDbType.Char).Value = ""
            cmd.ExecuteNonQuery()
        Catch ex As Exception

        Finally
            cmd.Dispose()
            Main.CierraConexion()
            Cursor = Cursors.Default
        End Try
    End Sub

#End Region

    Private Sub mnuRutaApoyo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRutaApoyo.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "RutaApoyo" Then
                f.Focus()
                Exit Sub
            End If
        Next

        Cursor = Cursors.WaitCursor
        Dim frmRutaApoyo As New RutaApoyo()
        frmRutaApoyo.StartPosition = FormStartPosition.CenterScreen
        frmRutaApoyo.MdiParent = Me
        frmRutaApoyo.Show()
        Cursor = Cursors.Default
    End Sub


#Region "Sigamet corporativo"

#Region "Control de documentos"

    Private Sub mnuControlDocumentos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "ControlNotas" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        Dim frmControlNotas As New ControlNotas()
        frmControlNotas.MdiParent = Me
        frmControlNotas.Show()
        Cursor = Cursors.Default
    End Sub

#End Region

#Region "Confirmación de pedidos programados"

    Private Sub mnuConfirmacion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim f As Form
        'For Each f In Me.MdiChildren
        '    If Trim(f.Name) = "frmSeleccionaRuta" Then
        '        f.Focus()
        '        Exit Sub
        '    End If
        'Next

        'Dim oRuta As ConfirmacionProgramados.frmSeleccionaRuta
        ''oRuta.MdiParent = Me

        ''TODO: ¿Por qué se pide el password de nuevo?
        'oRuta = New ConfirmacionProgramados.frmSeleccionaRuta(GLOBAL_Usuario, GLOBAL_Password, CnnSigamet)

        'If oRuta.ShowDialog = DialogResult.OK Then
        '    Dim oProgramados As New ConfirmacionProgramados.frmConfirmaProgramados(oRuta.Ruta, oRuta.Fecha)
        '    oProgramados.Show()
        'End If
        If Not oSeguridad.TieneAcceso("ConfirmacionPedidos") Then
            MessageBox.Show(SigaMetClasses.M_NO_PRIVILEGIOS, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim f As Form
        For Each f In Me.MdiChildren
            If Trim(f.Name) = "frmSeleccionaRuta" Then
                f.Focus()
                Exit Sub
            End If
        Next

        ConfirmacionProgramados.Globals.GetInstance.ConfiguraModulo(GLOBAL_Usuario, GLOBAL_Password, GLOBAL_ConString, GLOBAL_Servidor, GLOBAL_Database, CnnSigamet)
        Dim oRuta As New ConfirmacionProgramados.frmSeleccionaRuta()
        If oRuta.ShowDialog = DialogResult.OK Then
            Dim oProgramados As New ConfirmacionProgramados.frmConfirmaProgramados(oRuta.Ruta, oRuta.Fecha, oRuta.RutaNombre)
            oProgramados.MdiParent = Me
            oProgramados.Show()
        End If
    End Sub

    Private Sub activarMenuConfirmacionPedidos(Optional ByVal activar As Boolean = False)
        If activar Then
            AddHandler mnuConfirmacion.Click, AddressOf mnuConfirmacion_Click
        Else
            mnuConfirmacion.Enabled = False
            mnuConfirmacion.Visible = False
            mnuConfirmacion.MenuItems.Remove(mnuConfirmacion)
        End If
    End Sub

    Private Sub activarMenuReactivacionPedidos(Optional ByVal activar As Boolean = False)
        If Not activar Then
            mnuActivacionPedidos.Enabled = False
            mnuActivacionPedidos.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuActivacionPedidos)
        End If
    End Sub

    Private Sub activarMenusObsoletos(Optional ByVal activar As Boolean = False)
        If Not activar Then
            mnuRutaApoyo.Enabled = False
            mnuRutaApoyo.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuRutaApoyo)

            mnuNotasRuta.Enabled = False
            mnuNotasRuta.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuNotasRuta)

            mnuDescargaTarjetaRampac.Enabled = False
            mnuDescargaTarjetaRampac.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuDescargaTarjetaRampac)

            mnuCargaTarjetaRampac.Enabled = False
            mnuCargaTarjetaRampac.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuCargaTarjetaRampac)

            mnuLiquidacionManual.Enabled = False
            mnuLiquidacionManual.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuLiquidacionManual)

            mnuConciliacionNotasBlancas.Enabled = False
            mnuConciliacionNotasBlancas.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuConciliacionNotasBlancas)

            mnuRuta80.Enabled = False
            mnuRuta80.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuRuta80)

            mnuDescargaRI.Enabled = False
            mnuDescargaRI.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuDescargaRI)

            mnuCargaRI.Enabled = False
            mnuCargaRI.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuCargaRI)

        End If
    End Sub

    Private Sub activarMenuSeguimientoFugas(Optional ByVal activar As Boolean = False)
        If Not activar Then
            mnuFugasPortatil.Enabled = False
            mnuFugasPortatil.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuFugasPortatil)
        End If
    End Sub

    Private Sub activarMenuReporteRaf(Optional ByVal activar As Boolean = False)
        If Not activar Then
            mnuAuxilioFallas.Enabled = False
            mnuAuxilioFallas.Visible = False
            mnuPrincipal.MenuItems.Remove(mnuAuxilioFallas)
        End If
    End Sub

#End Region

#End Region

#Region "Garbage Code1"
    'Estas formas ya no se usan, se retiraron del proyecto
    'Private Sub mnuRelacionNotas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim f As Form
    '    For Each f In Me.MdiChildren
    '        If f.Name = "frmSeleccionaRutaPreliquidacion" Then
    '            f.Focus()
    '            Exit Sub
    '        End If
    '    Next

    '    'Dim frmLecturaOperador As LecturaOperador = New LecturaOperador()
    '    Cursor = Cursors.WaitCursor
    '    Dim oSelRuta As New frmSeleccionaRutaPreliquidacion()
    '    oSelRuta.Entrada(True)
    '    If oSelRuta.ShowDialog = DialogResult.OK Then
    '        Dim Fecha As Date
    '        Dim Ruta As Integer
    '        Dim Folio As Integer
    '        Dim Anioatt As Integer
    '        Dim Acepto As Boolean
    '        Dim Descarga As Boolean
    '        Dim Celula As Integer

    '        Fecha = oSelRuta.Fecha
    '        Ruta = oSelRuta.Ruta
    '        Folio = oSelRuta.Folio
    '        Anioatt = oSelRuta.AñoAtt
    '        Descarga = oSelRuta.Descarga
    '        Celula = oSelRuta._Celula
    '        Dim frmRelacionNotas As RelacionNotas = New RelacionNotas()
    '        frmRelacionNotas.Entrada(Fecha, Ruta, Folio, Anioatt, Celula)
    '        frmRelacionNotas.ShowDialog()

    '    End If

    '    Cursor = Cursors.Default

    'End Sub
#End Region

    Private Sub mniDepuradorColonias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniDepuradorColonias.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmDepuracionColonia" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        CallesColonias.Globales.GetInstance.cnSigamet = CnnSigamet
        Dim frmDepuracionColonia As New CallesColonias.frmDepuracionColonia()
        frmDepuracionColonia.MdiParent = Me
        frmDepuracionColonia.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mniDepuradorCalles_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniDepuradorCalles.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmDepuracionCalle" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        CallesColonias.Globales.GetInstance.cnSigamet = CnnSigamet
        Dim frmDepuracionCalle As New CallesColonias.frmDepuracionCalle()
        frmDepuracionCalle.MdiParent = Me
        frmDepuracionCalle.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mniHistorialDepuracion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniHistorialDepuracion.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmHistorialDepuracion" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        CallesColonias.Globales.GetInstance.cnSigamet = CnnSigamet
        CallesColonias.Globales.GetInstance.ModificarStatus = oSeguridad.TieneAcceso("DeshacerDepuracion")
        Dim frmHistorialDepuracion As New CallesColonias.frmHistorialDepuracion()
        frmHistorialDepuracion.MdiParent = Me
        frmHistorialDepuracion.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mniRelacionNotas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniRelacionNotas.Click
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If frm.Name = "frmRelacionNotas" Then
                frm.Focus()
                Exit Sub
            End If
        Next
        frm = New frmRelacionNotas(Main.CnnSigamet)
        frm.MdiParent = Me
        frm.Show()
    End Sub


    Private Sub mniRelacionClientes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniRelacionClientes.Click
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If frm.Name = "frmRelacionCliente" Then
                frm.Focus()
                Exit Sub
            End If
        Next
        Me.Cursor = Cursors.WaitCursor
        frm = New VentasMultinivel.UILayer.frmRelacionCliente(oSeguridad.TieneAcceso("ModificaRelacionesMultinivel"))
        Me.Cursor = Cursors.Default
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuVales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuVales.Click
        Dim frm As New ValesPromocionales.frmValePromocional(CnnSigamet)
        frm.ShowDialog()
    End Sub

    Private Sub mnuComodato_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuComodato.Click
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If UCase(frm.Name) = "frmPantallaComodato" Then
                frm.Focus()
                Exit Sub
            End If
        Next
        Me.Cursor = Cursors.WaitCursor
        frm = New SigametST.frmPantallaComodato(GLOBAL_Usuario)
        Me.Cursor = Cursors.Default
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Private Sub mnuPromotor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPromotor.Click
        Dim frm As Form
        For Each frm In Me.MdiChildren
            If frm.Name = "frmRelacionClientePromotor" Then
                frm.Focus()
                Exit Sub
            End If
        Next
        Me.Cursor = Cursors.WaitCursor
        VentasPromotor.ValLayer.VentasPromotorLib.Instance.Inicializar(CnnSigamet, GLOBAL_Usuario, GLOBAL_Celula, _
            oSeguridad.TieneAcceso("AGREGAR ASOCIACIONES"), oSeguridad.TieneAcceso("REALIZAR PAGOS PROMOCIONES"), _
            GLOBAL_PagoPromocionAPromotor)
        frm = New VentasPromotor.UILayer.frmRelacionClientePromotor()
        frm.MdiParent = Me
        frm.Show()
        Me.Cursor = Cursors.Default

        'frm.Show()
    End Sub

    Private Sub mnuActivacionPedidos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuActivacionPedidos.Click
        Dim frm As Form = Nothing
        If formFocus(frm, "ReactivacionPedidos") Then
            Exit Sub
        End If
        Me.Cursor = Cursors.WaitCursor
        frm = New ReactivacionPedidos.ReactivacionPedidos(CnnSigamet)
        'Reactivación de pedidos de fechas anteriores, por perfil, solo para grmeco
        DirectCast(frm, ReactivacionPedidos.ReactivacionPedidos).FechaCancelacionBloqueada = _
            False
        '    oSeguridad.TieneAcceso("Reactivacion_PedidosFull")

        frm.WindowState = FormWindowState.Maximized
        Me.Cursor = Cursors.Default
        frm.MdiParent = Me
        frm.Show()
    End Sub

    Public Function formFocus(ByRef frm As Form, ByVal FormName As String) As Boolean
        For Each frm In Me.MdiChildren
            If frm.Name = FormName Then
                frm.Focus()
                Return True
                Exit For
            End If
        Next
        Return False
    End Function

    Private Sub mnuDescargaUDS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDescargaUDS.Click
        If MessageBox.Show("Esta por iniciar el proceso de los archivos UDS" & vbCrLf & _
        "Asegurese de haber efectuado el corte correspondiente" & vbCrLf & _
        "¿Desea continuar?", "Liquidación UDS", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim frmUDS As New UIUDS.FrmMain(CnnSigamet)
            frmUDS.WindowState = FormWindowState.Maximized
            frmUDS.ShowDialog()
        End If
    End Sub

    Private Sub mnuPedidoWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPedidoWeb.Click
        Dim frmPedidoWeb As New frmListaPedidosWeb()
        frmPedidoWeb.MdiParent = Me
        frmPedidoWeb.Show()
    End Sub

    Private Sub MenuRemoval(ByVal OperationName As String, ByRef MenuItem As MenuItem, ByRef Menu As MainMenu)
        If Not oSeguridad.TieneAcceso(OperationName) Then
            MenuItem.Enabled = False
            MenuItem.Visible = False
        End If
    End Sub

    Private Sub mniPwdCambio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniPwdCambio.Click
        Dim frmCambioClave As New SigaMetClasses.CambioClave(Main.GLOBAL_Usuario)
        frmCambioClave.ShowDialog()
    End Sub

    Private Sub mniQuejas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuQuejas.Click
        Try
            QuejasLibrary.Public.[Global].ConfiguraLibrary(SigametSeguridad.Seguridad.Conexion.ConnectionString, SigametSeguridad.Seguridad.Conexion, GLOBAL_Usuario, 1)
            Dim frm As Form = Nothing
            If formFocus(frm, "frmSeguimientoQueja") Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            frm = New QuejasLibrary.frmSeguimientoQueja()
            Me.Cursor = Cursors.Default
            frm.WindowState = FormWindowState.Maximized
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error:" & vbCrLf & ex.Message & vbCrLf & _
                ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MnuSuministroPronosticado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSuministroPronosticado.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmClienteProximoSuministro" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        f = New frmClienteProximoSuministro()
        f.MdiParent = Me
        f.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mniCatalogos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniContactos.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "frmClienteProximoSuministro" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        f = New CRMContactos.ListaContactos(CnnSigamet)
        f.MdiParent = Me
        f.Show()
        Cursor = Cursors.Default
    End Sub

    Private Sub mnuEditorPortal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditorPortal.Click

    End Sub

    Private Sub mnuSolicitudCredito_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSolicitudCredito.Click
        Dim f As Form
        For Each f In Me.MdiChildren
            If f.Name = "Solicitantes" Then
                f.Focus()
                Exit Sub
            End If
        Next
        Cursor = Cursors.WaitCursor
        Cursor = Cursors.Default
        Try
            Dim _autorizacionCyC As New AutorizacionCredito.Solicitantes(SigaMetClasses.DataLayer.Conexion)
            _autorizacionCyC.WindowState = FormWindowState.Maximized
            _autorizacionCyC.MdiParent = Me

            _autorizacionCyC.Show()
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub mnuMonedero_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMonedero.Click
        If (oSeguridad.TieneAcceso("CapturaMonederoElectronico")) Then
            Try
                ClienteMonederoElectronico.Public.[Global].ConfiguraLibrary(SigametSeguridad.Seguridad.Conexion.ConnectionString, SigametSeguridad.Seguridad.Conexion, GLOBAL_Usuario, 1)
                Dim frm As Form = Nothing
                If formFocus(frm, "RelacionarClienteME") Then
                    Exit Sub
                End If
                Me.Cursor = Cursors.WaitCursor
                frm = New ClienteMonederoElectronico.RelacionarClienteME()
                Me.Cursor = Cursors.Default
                frm.WindowState = FormWindowState.Maximized
                frm.MdiParent = Me
                frm.Show()
            Catch ex As Exception
                MessageBox.Show("Ha ocurrido un error:" & vbCrLf & ex.Message & vbCrLf & _
                    ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("No tiene acceso a esta operación.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub


    Private Sub mnuNotasRuta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNotasRuta.Click
        Try
            Dim f As Form
            For Each f In Me.MdiChildren
                If f.Name = "frmFolios" Then
                    f.Focus()
                    Exit Sub
                End If
            Next
            Cursor = Cursors.WaitCursor
            f = New UINotas.frmFolios(SigaMetClasses.DataLayer.Conexion, GLOBAL_Usuario)
            f.MdiParent = Me
            f.Show()
            Cursor = Cursors.Default
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error:" & vbCrLf & ex.Message & vbCrLf & _
                ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mniReportesEspeciales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mniReportesEspeciales.Click
        If (oSeguridad.TieneAcceso("ReportesEspeciales")) Then
            Dim f As Form
            For Each f In Me.MdiChildren
                If f.Name = "frmListaReportes" Then
                    f.Focus()
                    Exit Sub
                End If
            Next
            Cursor = Cursors.WaitCursor

            Dim frmReporteExportacion As New ExportacionDirectaReportes.frmListaReportes(4, GLOBAL_Corporativo, GLOBAL_Sucursal)
            frmReporteExportacion.MdiParent = Me
            frmReporteExportacion.WindowState = FormWindowState.Maximized
            Cursor = Cursors.Default
            frmReporteExportacion.Show()
        Else
            MessageBox.Show("No tiene acceso a esta operación.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub mnuGruposComerciales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGruposComerciales.Click
        If (oSeguridad.TieneAcceso("GruposComerciales")) Then
            Cursor = Cursors.WaitCursor
            Dim frmGruposComerciales As New SigaMetClasses.CatalogoGrupoComercial()
            frmGruposComerciales.MdiParent = Me
            frmGruposComerciales.WindowState = FormWindowState.Maximized
            frmGruposComerciales.Show()
            Cursor = Cursors.Default
        Else
            MessageBox.Show("No tiene acceso a esta operación.", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub mnuAuxilioFallas_Click(sender As System.Object, e As System.EventArgs) Handles mnuAuxilioFallas.Click
        Try
            BitacoraAutotanqueAuxilio.Public.Global.ConfiguraLibrary(SigametSeguridad.Seguridad.Conexion.ConnectionString, _
                                                                     SigametSeguridad.Seguridad.Conexion, GLOBAL_Usuario, 1, _
                                                                     GLOBAL_CelulasUsuario, Main.GLOBAL_Estacion, GLOBAL_URLWebserviceBoletin)
            Dim frm As Form = Nothing
            If formFocus(frm, "frmBitacoraAuxilios") Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            frm = New BitacoraAutotanqueAuxilio.Formas.frmBitacoraAuxilios()
            Me.Cursor = Cursors.Default
            frm.WindowState = FormWindowState.Maximized
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error:" & vbCrLf & ex.Message & vbCrLf & _
                ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

 
    Private Sub mnuFugasPortatil_Click(sender As System.Object, e As System.EventArgs) Handles mnuFugasPortatil.Click
        Try            
            Dim frm As Form = Nothing
            If formFocus(frm, "frmFugaPortatilProcesos") Then
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            frm = New ControlFugasPortatilClasses.frmFugaPortatilProcesos()
            Me.Cursor = Cursors.Default
            frm.WindowState = FormWindowState.Maximized            
            frm.MdiParent = Me
            frm.Show()
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido un error:" & vbCrLf & ex.Message & vbCrLf & _
               ex.StackTrace, Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
