
Public Class TerminoLiquidacion
    Inherits System.Windows.Forms.Form

    Public _Ruta As Integer
    Public _Celula As Integer
    Public _AnioAtt As Integer
    Public _Folio As Integer
    Public _StatusLogistica As String
    Public _Autotanque As Integer
    Public _TotalAuto As Decimal
    Public _TotalCreditoAuto As Decimal
    Public _TotalContadoAuto As Decimal
    Public _Eficiencia As Decimal
    Public _TipoLiquidacion As String
    Public _UsuarioLiquidacion As String
    Public _FechaLiquidacion As String
    Public _FolioRuta As Integer

    'Variables para cancelación total
    Dim _FechaMovimiento As Date
    Dim _FechaOperacion As Date
    Dim _Caja As Integer
    Dim _Consecutivo As Integer
    Dim _FolioCaja As Integer


#Region "Cancelación"

    Private Sub CancelarLiquidacion()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If MsgBox("¿Desea CANCELAR la liquidación?.", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
            Dim cn As New SqlClient.SqlConnection(GLOBAL_ConString)
            Dim Transaccion As SqlClient.SqlTransaction = Nothing
            Dim cmdInsert As New SqlClient.SqlCommand()
            Dim rdrInsert As SqlClient.SqlDataReader = Nothing
            Try
                cn.Open()
                cmdInsert.Connection = cn
                cmdInsert.CommandTimeout = 350
                Transaccion = cn.BeginTransaction
                cmdInsert.Transaction = Transaccion
                cmdInsert.CommandType = CommandType.StoredProcedure
                cmdInsert.CommandText = "sp_CancelacionLiquidacion"
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AnioAtt
                cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                cmdInsert.ExecuteNonQuery()
                Transaccion.Commit()
                cn.Close()
                Actualizar()
                MsgBox("La cancelación fue realizada con exito", MsgBoxStyle.Information, "Mensaje del sistema")
            Catch et As Exception
                Transaccion.Rollback()
                MsgBox(et.Message, MsgBoxStyle.Critical)
                cn.Close()
            Finally
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End Try
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Cancelar()
        If _StatusLogistica = "LIQUIDADO" Then
            CancelarLiquidacion()
        Else
            MsgBox("Con este status no se puede cancelar la liquidación. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
        End If
    End Sub

#End Region

    Private Sub Consultar()
        If _Folio <> 0 Then
            Cursor = Cursors.WaitCursor
            Dim oConsultaFolio As New SigaMetClasses.ConsultaATT(CType(_AnioAtt, Short), _Folio)
            oConsultaFolio.ShowDialog()
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Cheques()
        Dim frmCheques As CambioCheques = New CambioCheques()
        frmCheques.Entrada(_AnioAtt, _Folio)
        frmCheques.Dispose()
    End Sub

    Private Sub ContadoCredito(ByVal Pago As String, ByVal FormaPago As String)
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If MsgBox("¿Desea Cambiar de " & FormaPago & " a " & Pago.ToUpper & " el siguiente registro?.", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
            Dim cn As New SqlClient.SqlConnection(GLOBAL_ConString)
            Dim Transaccion As SqlClient.SqlTransaction = Nothing
            Dim cmdInsert As New SqlClient.SqlCommand()
            Dim rdrInsert As SqlClient.SqlDataReader = Nothing
            Try
                cn.Open()
                cmdInsert.Connection = cn
                cmdInsert.CommandTimeout = 200
                Transaccion = cn.BeginTransaction
                cmdInsert.Transaction = Transaccion
                cmdInsert.CommandType = CommandType.StoredProcedure
                cmdInsert.CommandText = "sp_Cambio" & Pago.ToLower
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@ClienteNuevo", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).Cliente
                cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).AñoPed
                cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).Celula
                cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).Pedido
                cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AnioAtt
                cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                cmdInsert.ExecuteNonQuery()
                Transaccion.Commit()
                cn.Close()
                Actualizar()
                MsgBox("El cambio fue realizado con exito", MsgBoxStyle.Information, "Mensaje del sistema")
            Catch et As Exception
                Transaccion.Rollback()
                MsgBox(et.Message, MsgBoxStyle.Critical)
                cn.Close()
            Finally
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End Try
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub TarjetaCredito_a_CreditoOperador()
        Me.Cursor = System.Windows.Forms.Cursors.WaitCursor
        If MsgBox("¿Desea Cambiar de TARJETA CREDITO a CREDITO OPERADOR el siguiente registro?.", MsgBoxStyle.YesNoCancel, "Mensaje del sistema") = MsgBoxResult.Yes Then
            Dim cn As New SqlClient.SqlConnection(GLOBAL_ConString)
            Dim Transaccion As SqlClient.SqlTransaction = Nothing
            Dim cmdInsert As New SqlClient.SqlCommand()
            Dim rdrInsert As SqlClient.SqlDataReader = Nothing
            Try
                cn.Open()
                cmdInsert.Connection = cn
                cmdInsert.CommandTimeout = 200
                Transaccion = cn.BeginTransaction
                cmdInsert.Transaction = Transaccion
                cmdInsert.CommandType = CommandType.Text
                cmdInsert.CommandText = " Update Pedido set TipoCobro=9 where Cliente=@Cliente and AñoPed=@AñoPed and Celula=@Celula and Pedido=@Pedido " & _
                                        " exec sp_BitacoraLiquidacionCambios @AñoAtt, @Folio, @AñoPed, @Pedido, @Celula, @Observaciones "
                cmdInsert.Parameters.Clear()
                cmdInsert.Parameters.Add("@Cliente", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).Cliente
                cmdInsert.Parameters.Add("@AñoPed", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).AñoPed
                cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).Celula
                cmdInsert.Parameters.Add("@Pedido", SqlDbType.Int).Value = DsModificacion.Pedido(GridView1.FocusedRowHandle).Pedido
                cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = _AnioAtt
                cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                cmdInsert.Parameters.Add("@Observaciones", SqlDbType.Char).Value = "Cambio de tarjeta credito  a CREDITO OPERADOR del pedido"

                cmdInsert.ExecuteNonQuery()
                Transaccion.Commit()
                cn.Close()
                Actualizar()
                MsgBox("El cambio fue realizado con exito", MsgBoxStyle.Information, "Mensaje del sistema")
            Catch et As Exception
                Transaccion.Rollback()
                MsgBox(et.Message, MsgBoxStyle.Critical)
                cn.Close()
            Finally
                Me.Cursor = System.Windows.Forms.Cursors.Default
            End Try
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub CambioContadoCredito(ByVal Pago As String)
        If _StatusLogistica = "LIQUIDADO" Then
            Dim FormaPago As String = "CONTADO"
            If Pago = "Contado" Then
                FormaPago = "CREDITO"
            End If
            If DsModificacion.Pedido(GridView1.FocusedRowHandle).Pago <> Pago Then
                ContadoCredito(Pago, FormaPago)
            Else
                MsgBox("Solo se puede cambiar a " & Pago.ToUpper & " un registro de " & FormaPago & ".", MsgBoxStyle.Information, "Mensaje del sistema")
            End If
        Else
            MsgBox("Con este status no se puede modificar la liquidación. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
        End If
    End Sub

    Private Sub Cliente()
        If _StatusLogistica = "LIQUIDADO" Or CType(_Nivel, Double) = 1 Then
            Dim frmCambioContrato As CambioContrato = New CambioContrato()
            frmCambioContrato.Entrada(DsModificacion.Pedido(GridView1.FocusedRowHandle).Cliente, DsModificacion.Pedido(GridView1.FocusedRowHandle).Nombre, DsModificacion.Pedido(GridView1.FocusedRowHandle).Pedido, DsModificacion.Pedido(GridView1.FocusedRowHandle).AñoPed, DsModificacion.Pedido(GridView1.FocusedRowHandle).Celula, DsModificacion.Pedido(GridView1.FocusedRowHandle).Total, DsModificacion.Pedido(GridView1.FocusedRowHandle).Litros, _AnioAtt, _Folio)
            frmCambioContrato.Dispose()
            Actualizar()
        Else
            MsgBox("Con este status no se puede modificar la liquidación. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
        End If
    End Sub

    Private Sub Actualizar()
        DsModificacion.Pedido.Clear()
        cmdCPedidos.Parameters("@Folio").Value = _Folio
        cmdCPedidos.Parameters("@AñoAtt").Value = _AnioAtt
        daPedidos.Fill(DsModificacion, "Pedido")

        DsModificacion.Rampac.Clear()
        cmdRampac.Parameters("@Folio").Value = _Folio
        cmdRampac.Parameters("@AñoAtt").Value = _AnioAtt
        daRampac.Fill(DsModificacion, "Rampac")

        CalculodeTotales()
    End Sub

    Private Sub CalculodeTotales()
        Dim TotalLitrosCredito, TotalLitrosContado, TotalRegistrosContado, TotalLitrosCreditoR, TotalLitrosContadoR As Decimal
        Dim i As Integer
        'Dim TotalLitrosCredito, TotalLitrosContado, TotalRegistrosContado, TotalLitrosCreditoR, TotalLitrosContadoR, i As Integer
        Dim TotalImporteCredito, TotalImporteContado, TotalImporteCreditoR, TotalImporteContadoR As Decimal

        For i = 0 To GridView1.RowCount - 1
            'Modificado por JAGD (muestra un error de cast), además cuenta como credito los registros sin tipo de pago
            If Not (GridView1.GetDataRow(i).Item("Pago") Is DBNull.Value) Then
                If CType(GridView1.GetDataRow(i).Item("Pago"), String).ToUpper = "CONTADO" Then
                    TotalRegistrosContado = TotalRegistrosContado + 1
                    TotalLitrosContado = TotalLitrosContado + CType(GridView1.GetDataRow(i).Item("Litros"), Decimal)
                    'TotalLitrosContado = TotalLitrosContado + CType(GridView1.GetDataRow(i).Item("Litros"), Integer)
                    TotalImporteContado = TotalImporteContado + CType(GridView1.GetDataRow(i).Item("Total"), Decimal)
                Else
                    TotalLitrosCredito = TotalLitrosCredito + CType(GridView1.GetDataRow(i).Item("Litros"), Decimal)
                    'TotalLitrosCredito = TotalLitrosCredito + CType(GridView1.GetDataRow(i).Item("Litros"), Integer)
                    TotalImporteCredito = TotalImporteCredito + CType(GridView1.GetDataRow(i).Item("Total"), Decimal)
                End If
            End If
        Next
        lbTotalRegistros.Text = GridView1.RowCount.ToString
        lbTotalLitros.Text = CStr(TotalLitrosContado + TotalLitrosCredito)
        lbTotalImporte.Text = FormatCurrency(CStr(TotalImporteContado + TotalImporteCredito))
        lbTAutotanque.Text = "Autotanqueturno: " & FormatCurrency(CType(_TotalAuto, String))
        lbNCredito.Text = CStr(GridView1.RowCount - TotalRegistrosContado)
        lbLitrosCredito.Text = CStr(TotalLitrosCredito)
        lbImporteCredito.Text = FormatCurrency(CStr(TotalImporteCredito))
        lbTAutotanqueC.Text = "Autotanqueturno: " & FormatCurrency(CType(_TotalCreditoAuto, String))
        lbNContado.Text = CStr(TotalRegistrosContado)
        lbLitrosContado.Text = CStr(TotalLitrosContado)
        lbImporteContado.Text = FormatCurrency(CStr(TotalImporteContado))
        lbTAutoTanqueCO.Text = "Autotanqueturno: " & FormatCurrency(CType(_TotalContadoAuto, String))

        For i = 0 To GridView2.RowCount - 1
            If CType(GridView2.GetDataRow(i).Item("Pago"), String).ToUpper = "CO" Then
                TotalLitrosContadoR = TotalLitrosContadoR + CType(GridView2.GetDataRow(i).Item("Litros"), Decimal)
                'TotalLitrosContadoR = TotalLitrosContadoR + CType(GridView2.GetDataRow(i).Item("Litros"), Integer)
                TotalImporteContadoR = TotalImporteContadoR + CType(GridView2.GetDataRow(i).Item("Importe"), Decimal)
            Else
                TotalLitrosCreditoR = TotalLitrosCreditoR + CType(GridView2.GetDataRow(i).Item("Litros"), Decimal)
                'TotalLitrosCreditoR = TotalLitrosCreditoR + CType(GridView2.GetDataRow(i).Item("Litros"), Integer)
                TotalImporteCreditoR = TotalImporteCreditoR + CType(GridView2.GetDataRow(i).Item("Importe"), Decimal)
            End If
        Next
        lbNotasRampac.Text = GridView2.RowCount.ToString
        lbLitrosRampac.Text = CStr(TotalLitrosContadoR + TotalLitrosCreditoR)
        lbImporteRampac.Text = FormatCurrency(CStr(TotalImporteContadoR + TotalImporteCreditoR))
        lbLitrosCreditoR.Text = CStr(TotalLitrosCreditoR)
        lbImporteCreditoR.Text = FormatCurrency(CStr(TotalImporteCreditoR))
        lbLitrosContadoR.Text = CStr(TotalLitrosContadoR)
        lbImporteContadoR.Text = FormatCurrency(CStr(TotalImporteContadoR))
        lbEficiencia.Text = "Eficiencia: " + CType(_Eficiencia, String)

        If _Eficiencia > 0 Then
            lbEficiencia.BackColor = Color.LightCoral
        Else
            lbEficiencia.BackColor = Color.PowderBlue
        End If

        If TotalImporteContado + TotalImporteCredito <> _TotalAuto Then
            lbRuta.BackColor = Color.LightCoral
        Else
            lbRuta.BackColor = Color.PeachPuff
        End If
    End Sub

    Private Function RegresaRuta(ByVal Cadena As String) As Integer
        Dim i As Integer
        Dim SRuta As String = Nothing
        Dim Termino As Boolean = False

        For i = 0 To Len(Cadena) - 1
            If Cadena.Chars(i) <> " " And Termino = False Then
                SRuta = SRuta + Cadena.Chars(i)
            Else
                Termino = True
            End If
        Next
        Return (CType(Trim(SRuta), Integer))
    End Function

    Private Sub MenuClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim Texto As String
        Dim i As Integer
        For i = 0 To ctmRutas.MenuItems.Count - 1
            ctmRutas.MenuItems(i).Checked = False
            If CType(sender, MenuItem).Text = ctmRutas.MenuItems(i).Text And CType(sender, MenuItem).MergeOrder = ctmRutas.MenuItems(i).MergeOrder Then
                Texto = ctmRutas.MenuItems(i).Text
                _Ruta = RegresaRuta(RTrim(LTrim(Texto.Remove(0, 5))))
                _Autotanque = CType(RTrim(Texto.Remove(0, 21)), Integer)
                _FolioRuta = ctmRutas.MenuItems(i).MergeOrder
                StatusAutotanqueTurno()
                Actualizar()
            End If
        Next
        CType(sender, MenuItem).Checked = True
    End Sub

    Private Sub CargarRutas(ByVal Celula As Integer)
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader
        Dim i As Integer = Nothing
        Dim Pedidos As Integer = Nothing
        Dim Litros As Integer = Nothing
        Dim Total As Integer = Nothing

        ctmRutas.MenuItems.Clear()
        cmdInsert.Connection = SqlConnection
        cmdInsert.CommandTimeout = 30
        'cmdInsert.CommandText = " Select Folio, Ruta, Autotanque, 'Ruta '+Convert(Char(3),Ruta)+' - AutoTanque '+Convert(VarChar(4),Autotanque) as Descripcion from AutotanqueTurno where (DAY(FTerminoRuta)=@Dia and MONTH(FTerminoRuta)=@Mes and YEAR(FTerminoRuta)=@Año) and Ruta<>0 and Celula=@Celula Order by Ruta, Autotanque "
        cmdInsert.CommandText = "spCCMLConsultaAutotanqueCelula"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Parameters.Clear()
        cmdInsert.Parameters.Add("@Celula", SqlDbType.Int).Value = Celula
        cmdInsert.Parameters.Add("@Dia", SqlDbType.Int).Value = dtpFecha.Value.Date.Day
        cmdInsert.Parameters.Add("@Mes", SqlDbType.Int).Value = dtpFecha.Value.Date.Month
        cmdInsert.Parameters.Add("@Año", SqlDbType.Int).Value = dtpFecha.Value.Date.Year
        rdrInsert = cmdInsert.ExecuteReader()
        i = 0
        While rdrInsert.Read()
            ctmRutas.MenuItems.Add(CType(rdrInsert("Descripcion"), String))
            ctmRutas.MenuItems(i).RadioCheck = True
            AddHandler ctmRutas.MenuItems(i).Click, AddressOf MenuClick
            ctmRutas.MenuItems(i).MergeOrder = CType(rdrInsert("Folio"), Integer)
            If i = 0 Then
                ctmRutas.MenuItems(i).Checked = True
                _Ruta = CType(rdrInsert("Ruta"), Integer)
                _Autotanque = CType(rdrInsert("Autotanque"), Integer)
                _FolioRuta = CType(rdrInsert("Folio"), Integer)
            End If
            i = i + 1
        End While
        rdrInsert.Close()
        cmdInsert.Dispose()
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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents cbCelula As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents tlbLiquidacion As System.Windows.Forms.ToolBar
    Friend WithEvents btnRutas As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnLitros As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCliente As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnContado As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCredito As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCancelar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnActualizar As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnCerrar As System.Windows.Forms.ToolBarButton
    Friend WithEvents ToolBarButton3 As System.Windows.Forms.ToolBarButton
    Friend WithEvents SqlConnection As System.Data.SqlClient.SqlConnection
    Friend WithEvents ctmRutas As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem12 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem13 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem14 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem15 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem16 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem17 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem18 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem19 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem20 As System.Windows.Forms.MenuItem
    Friend WithEvents cmdCelula As System.Data.SqlClient.SqlCommand
    Friend WithEvents DaCelula As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents DsModificacion As Sigamet.dsModificacion
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbRuta As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents cmdCPedidos As System.Data.SqlClient.SqlCommand
    Friend WithEvents daPedidos As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents lbTotalRegistros As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lbTotalLitros As System.Windows.Forms.Label
    Friend WithEvents lbTotalImporte As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents lbTAutotanqueC As System.Windows.Forms.Label
    Friend WithEvents lbTAutoTanqueCO As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents lbImporteCredito As System.Windows.Forms.Label
    Friend WithEvents lbLitrosCredito As System.Windows.Forms.Label
    Friend WithEvents lbNCredito As System.Windows.Forms.Label
    Friend WithEvents lbImporteContado As System.Windows.Forms.Label
    Friend WithEvents lbLitrosContado As System.Windows.Forms.Label
    Friend WithEvents lbNContado As System.Windows.Forms.Label
    Friend WithEvents daRampac As System.Data.SqlClient.SqlDataAdapter
    Friend WithEvents cmdRampac As System.Data.SqlClient.SqlCommand
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lbNotasRampac As System.Windows.Forms.Label
    Friend WithEvents lbLitrosRampac As System.Windows.Forms.Label
    Friend WithEvents lbImporteRampac As System.Windows.Forms.Label
    Friend WithEvents lbImporteContadoR As System.Windows.Forms.Label
    Friend WithEvents lbLitrosContadoR As System.Windows.Forms.Label
    Friend WithEvents lbImporteCreditoR As System.Windows.Forms.Label
    Friend WithEvents lbLitrosCreditoR As System.Windows.Forms.Label
    Friend WithEvents lbEficiencia As System.Windows.Forms.Label
    Friend WithEvents ctmLitros As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItem21 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem22 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem23 As System.Windows.Forms.MenuItem
    Friend WithEvents btnCheques As System.Windows.Forms.ToolBarButton
    Friend WithEvents btnConsultaFolio As System.Windows.Forms.ToolBarButton
    Friend WithEvents picWarning As System.Windows.Forms.PictureBox
    Friend WithEvents PersistentRepository1 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents dgPedidos As DevExpress.XtraGrid.GridControl
    Friend WithEvents PersistentRepository2 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEdit1 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents gcPedido As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcTipo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcLitros As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcPrecio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcTotal As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcFormaPago As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcContrato As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcCliente As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcDomicilio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents dgRampac As DevExpress.XtraGrid.GridControl
    Friend WithEvents PersistentRepository3 As DevExpress.XtraEditors.Repository.PersistentRepository
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents RepositoryItemTextEdit2 As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents gcRContrato As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRLitros As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRImporte As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRPago As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRTipo As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRInicio As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents gcRFin As DevExpress.XtraGrid.Columns.GridColumn
    Friend WithEvents lbTAutotanque As System.Windows.Forms.Label
    Friend WithEvents btnCancelarAll As System.Windows.Forms.ToolBarButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents MenuItem24 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem25 As System.Windows.Forms.MenuItem
    Friend WithEvents btnImprimir As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(TerminoLiquidacion))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu()
        Me.MenuItem5 = New System.Windows.Forms.MenuItem()
        Me.MenuItem6 = New System.Windows.Forms.MenuItem()
        Me.MenuItem3 = New System.Windows.Forms.MenuItem()
        Me.MenuItem1 = New System.Windows.Forms.MenuItem()
        Me.MenuItem2 = New System.Windows.Forms.MenuItem()
        Me.MenuItem4 = New System.Windows.Forms.MenuItem()
        Me.MenuItem24 = New System.Windows.Forms.MenuItem()
        Me.MenuItem25 = New System.Windows.Forms.MenuItem()
        Me.MenuItem7 = New System.Windows.Forms.MenuItem()
        Me.MenuItem8 = New System.Windows.Forms.MenuItem()
        Me.MenuItem19 = New System.Windows.Forms.MenuItem()
        Me.MenuItem20 = New System.Windows.Forms.MenuItem()
        Me.cbCelula = New System.Windows.Forms.ComboBox()
        Me.DsModificacion = New Sigamet.dsModificacion()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tlbLiquidacion = New System.Windows.Forms.ToolBar()
        Me.btnRutas = New System.Windows.Forms.ToolBarButton()
        Me.ctmRutas = New System.Windows.Forms.ContextMenu()
        Me.MenuItem9 = New System.Windows.Forms.MenuItem()
        Me.MenuItem10 = New System.Windows.Forms.MenuItem()
        Me.MenuItem11 = New System.Windows.Forms.MenuItem()
        Me.MenuItem12 = New System.Windows.Forms.MenuItem()
        Me.MenuItem13 = New System.Windows.Forms.MenuItem()
        Me.MenuItem14 = New System.Windows.Forms.MenuItem()
        Me.MenuItem15 = New System.Windows.Forms.MenuItem()
        Me.MenuItem16 = New System.Windows.Forms.MenuItem()
        Me.MenuItem17 = New System.Windows.Forms.MenuItem()
        Me.MenuItem18 = New System.Windows.Forms.MenuItem()
        Me.btnLitros = New System.Windows.Forms.ToolBarButton()
        Me.ctmLitros = New System.Windows.Forms.ContextMenu()
        Me.MenuItem21 = New System.Windows.Forms.MenuItem()
        Me.MenuItem22 = New System.Windows.Forms.MenuItem()
        Me.MenuItem23 = New System.Windows.Forms.MenuItem()
        Me.btnCliente = New System.Windows.Forms.ToolBarButton()
        Me.btnCredito = New System.Windows.Forms.ToolBarButton()
        Me.btnContado = New System.Windows.Forms.ToolBarButton()
        Me.btnCheques = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelar = New System.Windows.Forms.ToolBarButton()
        Me.ToolBarButton3 = New System.Windows.Forms.ToolBarButton()
        Me.btnConsultaFolio = New System.Windows.Forms.ToolBarButton()
        Me.btnActualizar = New System.Windows.Forms.ToolBarButton()
        Me.btnCancelarAll = New System.Windows.Forms.ToolBarButton()
        Me.btnCerrar = New System.Windows.Forms.ToolBarButton()
        Me.SqlConnection = New System.Data.SqlClient.SqlConnection()
        Me.cmdCelula = New System.Data.SqlClient.SqlCommand()
        Me.DaCelula = New System.Data.SqlClient.SqlDataAdapter()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dtpFecha = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.picWarning = New System.Windows.Forms.PictureBox()
        Me.lbRuta = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dgPedidos = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository2 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.RepositoryItemTextEdit1 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcPedido = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcTipo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcLitros = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcPrecio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcTotal = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcFormaPago = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcContrato = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcCliente = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcDomicilio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.lbNContado = New System.Windows.Forms.Label()
        Me.lbImporteContado = New System.Windows.Forms.Label()
        Me.lbLitrosContado = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.lbTAutoTanqueCO = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lbImporteCredito = New System.Windows.Forms.Label()
        Me.lbLitrosCredito = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lbNCredito = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.lbTAutotanqueC = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.lbTAutotanque = New System.Windows.Forms.Label()
        Me.lbTotalImporte = New System.Windows.Forms.Label()
        Me.lbTotalLitros = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbTotalRegistros = New System.Windows.Forms.Label()
        Me.PersistentRepository1 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.dgRampac = New DevExpress.XtraGrid.GridControl()
        Me.PersistentRepository3 = New DevExpress.XtraEditors.Repository.PersistentRepository()
        Me.RepositoryItemTextEdit2 = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.gcRContrato = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRLitros = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRImporte = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRPago = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRTipo = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRInicio = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.gcRFin = New DevExpress.XtraGrid.Columns.GridColumn()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.lbImporteCreditoR = New System.Windows.Forms.Label()
        Me.lbLitrosCreditoR = New System.Windows.Forms.Label()
        Me.lbImporteContadoR = New System.Windows.Forms.Label()
        Me.lbLitrosContadoR = New System.Windows.Forms.Label()
        Me.lbImporteRampac = New System.Windows.Forms.Label()
        Me.lbLitrosRampac = New System.Windows.Forms.Label()
        Me.lbNotasRampac = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.lbEficiencia = New System.Windows.Forms.Label()
        Me.cmdCPedidos = New System.Data.SqlClient.SqlCommand()
        Me.daPedidos = New System.Data.SqlClient.SqlDataAdapter()
        Me.cmdRampac = New System.Data.SqlClient.SqlCommand()
        Me.daRampac = New System.Data.SqlClient.SqlDataAdapter()
        Me.btnImprimir = New System.Windows.Forms.Button()
        CType(Me.DsModificacion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgPedidos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel9.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel14.SuspendLayout()
        CType(Me.dgRampac, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel13.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem5})
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 0
        Me.MenuItem5.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem6, Me.MenuItem3, Me.MenuItem1, Me.MenuItem2, Me.MenuItem4, Me.MenuItem24, Me.MenuItem25, Me.MenuItem7, Me.MenuItem8, Me.MenuItem19, Me.MenuItem20})
        Me.MenuItem5.Text = "Liquidación"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 0
        Me.MenuItem6.Text = "Faltante de litros"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 1
        Me.MenuItem3.Text = "-"
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 2
        Me.MenuItem1.Text = "Cambio de cliente"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 3
        Me.MenuItem2.Text = "Cambio contado por credito"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 4
        Me.MenuItem4.Text = "Cambio credito por contado"
        '
        'MenuItem24
        '
        Me.MenuItem24.Index = 5
        Me.MenuItem24.Text = "Cambio de tarjeta credito a credito operador"
        '
        'MenuItem25
        '
        Me.MenuItem25.Index = 6
        Me.MenuItem25.Text = "Cambio de datos de cheques"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 7
        Me.MenuItem7.Text = "-"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 8
        Me.MenuItem8.Text = "Cancelacion"
        '
        'MenuItem19
        '
        Me.MenuItem19.Index = 9
        Me.MenuItem19.Text = "-"
        '
        'MenuItem20
        '
        Me.MenuItem20.Index = 10
        Me.MenuItem20.Text = "Cerrar"
        '
        'cbCelula
        '
        Me.cbCelula.DataSource = Me.DsModificacion.Celula
        Me.cbCelula.DisplayMember = "Descripcion"
        Me.cbCelula.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbCelula.Font = New System.Drawing.Font("Tahoma", 7.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCelula.Location = New System.Drawing.Point(817, 18)
        Me.cbCelula.Name = "cbCelula"
        Me.cbCelula.Size = New System.Drawing.Size(120, 20)
        Me.cbCelula.TabIndex = 45
        Me.cbCelula.ValueMember = "Celula"
        '
        'DsModificacion
        '
        Me.DsModificacion.DataSetName = "dsModificacion"
        Me.DsModificacion.Locale = New System.Globalization.CultureInfo("es-MX")
        Me.DsModificacion.Namespace = "http://www.tempuri.org/dsModificacion.xsd"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label1.Location = New System.Drawing.Point(817, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 14)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "CELULA"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'tlbLiquidacion
        '
        Me.tlbLiquidacion.Buttons.AddRange(New System.Windows.Forms.ToolBarButton() {Me.btnRutas, Me.btnLitros, Me.btnCliente, Me.btnCredito, Me.btnContado, Me.btnCheques, Me.btnCancelar, Me.ToolBarButton3, Me.btnConsultaFolio, Me.btnActualizar, Me.btnCancelarAll, Me.btnCerrar})
        Me.tlbLiquidacion.ButtonSize = New System.Drawing.Size(70, 40)
        Me.tlbLiquidacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.tlbLiquidacion.DropDownArrows = True
        Me.tlbLiquidacion.ImageList = Me.ImageList1
        Me.tlbLiquidacion.Name = "tlbLiquidacion"
        Me.tlbLiquidacion.ShowToolTips = True
        Me.tlbLiquidacion.Size = New System.Drawing.Size(1284, 43)
        Me.tlbLiquidacion.TabIndex = 47
        '
        'btnRutas
        '
        Me.btnRutas.DropDownMenu = Me.ctmRutas
        Me.btnRutas.ImageIndex = 0
        Me.btnRutas.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton
        Me.btnRutas.Text = "Ruta"
        '
        'ctmRutas
        '
        Me.ctmRutas.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem9, Me.MenuItem10, Me.MenuItem11, Me.MenuItem12, Me.MenuItem13, Me.MenuItem14, Me.MenuItem15, Me.MenuItem16, Me.MenuItem17, Me.MenuItem18})
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 0
        Me.MenuItem9.MergeOrder = 1
        Me.MenuItem9.Text = "Ruta 1         "
        '
        'MenuItem10
        '
        Me.MenuItem10.Checked = True
        Me.MenuItem10.Index = 1
        Me.MenuItem10.MergeOrder = 2
        Me.MenuItem10.RadioCheck = True
        Me.MenuItem10.Text = "Ruta 2         "
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 2
        Me.MenuItem11.MergeOrder = 3
        Me.MenuItem11.Text = "Ruta 3         "
        '
        'MenuItem12
        '
        Me.MenuItem12.Index = 3
        Me.MenuItem12.Text = "Ruta 4         "
        '
        'MenuItem13
        '
        Me.MenuItem13.Index = 4
        Me.MenuItem13.Text = "Ruta 5         "
        '
        'MenuItem14
        '
        Me.MenuItem14.Index = 5
        Me.MenuItem14.Text = "Ruta 6         "
        '
        'MenuItem15
        '
        Me.MenuItem15.Index = 6
        Me.MenuItem15.Text = "Ruta 7         "
        '
        'MenuItem16
        '
        Me.MenuItem16.Index = 7
        Me.MenuItem16.Text = "Ruta 8         "
        '
        'MenuItem17
        '
        Me.MenuItem17.Index = 8
        Me.MenuItem17.Text = "Ruta 9         "
        '
        'MenuItem18
        '
        Me.MenuItem18.Index = 9
        Me.MenuItem18.Text = "Ruta 10         "
        '
        'btnLitros
        '
        Me.btnLitros.DropDownMenu = Me.ctmLitros
        Me.btnLitros.ImageIndex = 3
        Me.btnLitros.Style = System.Windows.Forms.ToolBarButtonStyle.DropDownButton
        Me.btnLitros.Text = "Litros"
        Me.btnLitros.ToolTipText = "Faltante de litros"
        '
        'ctmLitros
        '
        Me.ctmLitros.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem21, Me.MenuItem22, Me.MenuItem23})
        '
        'MenuItem21
        '
        Me.MenuItem21.Index = 0
        Me.MenuItem21.Text = "Litros de contado"
        '
        'MenuItem22
        '
        Me.MenuItem22.Index = 1
        Me.MenuItem22.Text = "-"
        '
        'MenuItem23
        '
        Me.MenuItem23.Index = 2
        Me.MenuItem23.Text = "Litros de credito"
        '
        'btnCliente
        '
        Me.btnCliente.ImageIndex = 4
        Me.btnCliente.Text = "Cliente"
        Me.btnCliente.ToolTipText = "Cambio de cliente"
        '
        'btnCredito
        '
        Me.btnCredito.ImageIndex = 5
        Me.btnCredito.Text = "Credito"
        Me.btnCredito.ToolTipText = "Cambio de contado a credito"
        '
        'btnContado
        '
        Me.btnContado.ImageIndex = 6
        Me.btnContado.Text = "Contado"
        Me.btnContado.ToolTipText = "Cambio de credito a contado"
        '
        'btnCheques
        '
        Me.btnCheques.ImageIndex = 8
        Me.btnCheques.Text = "Cheques"
        Me.btnCheques.ToolTipText = "Cambio de datos del cheque"
        '
        'btnCancelar
        '
        Me.btnCancelar.ImageIndex = 7
        Me.btnCancelar.Text = "Cancelacion"
        Me.btnCancelar.ToolTipText = "Cancelación de una liquidación"
        '
        'ToolBarButton3
        '
        Me.ToolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator
        '
        'btnConsultaFolio
        '
        Me.btnConsultaFolio.Enabled = False
        Me.btnConsultaFolio.ImageIndex = 9
        Me.btnConsultaFolio.Tag = "ConsultarFolio"
        Me.btnConsultaFolio.Text = "Consultar"
        Me.btnConsultaFolio.ToolTipText = "Consultar el folio seleccionado"
        '
        'btnActualizar
        '
        Me.btnActualizar.ImageIndex = 1
        Me.btnActualizar.Text = "Actualizar"
        Me.btnActualizar.ToolTipText = "Actualizar datos de la liquidación"
        '
        'btnCancelarAll
        '
        Me.btnCancelarAll.Enabled = False
        Me.btnCancelarAll.ImageIndex = 10
        Me.btnCancelarAll.Tag = "CancelacionTotal"
        Me.btnCancelarAll.Text = "Cancelación Total"
        Me.btnCancelarAll.ToolTipText = "Cancelación Total"
        '
        'btnCerrar
        '
        Me.btnCerrar.ImageIndex = 2
        Me.btnCerrar.Text = "Cerrar"
        Me.btnCerrar.ToolTipText = "Cerrar ventana"
        '
        'SqlConnection
        '
        Me.SqlConnection.ConnectionString = "data source=ERPMETRO;initial catalog=sigamet;persist security info=False;user id=" & _
        "SA;workstation id=DESARROLLO-4;packet size=4096"
        '
        'cmdCelula
        '
        Me.cmdCelula.CommandText = "Select Celula, Upper(Descripcion) [Descripcion] from Celula order by [Descripcion" & _
        "]"
        Me.cmdCelula.Connection = Me.SqlConnection
        '
        'DaCelula
        '
        Me.DaCelula.SelectCommand = Me.cmdCelula
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.Controls.AddRange(New System.Windows.Forms.Control() {Me.dtpFecha, Me.Label2, Me.Panel2})
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 43)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1284, 29)
        Me.Panel1.TabIndex = 48
        '
        'dtpFecha
        '
        Me.dtpFecha.CalendarTitleBackColor = System.Drawing.Color.YellowGreen
        Me.dtpFecha.CustomFormat = ""
        Me.dtpFecha.Location = New System.Drawing.Point(786, 5)
        Me.dtpFecha.Name = "dtpFecha"
        Me.dtpFecha.TabIndex = 51
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.Highlight
        Me.Label2.Location = New System.Drawing.Point(744, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 14)
        Me.Label2.TabIndex = 50
        Me.Label2.Text = "FECHA"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.PeachPuff
        Me.Panel2.Controls.AddRange(New System.Windows.Forms.Control() {Me.picWarning, Me.lbRuta})
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(735, 29)
        Me.Panel2.TabIndex = 1
        '
        'picWarning
        '
        Me.picWarning.BackColor = System.Drawing.Color.LightCoral
        Me.picWarning.Image = CType(resources.GetObject("picWarning.Image"), System.Drawing.Bitmap)
        Me.picWarning.Location = New System.Drawing.Point(544, 2)
        Me.picWarning.Name = "picWarning"
        Me.picWarning.Size = New System.Drawing.Size(24, 24)
        Me.picWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picWarning.TabIndex = 2
        Me.picWarning.TabStop = False
        Me.picWarning.Visible = False
        '
        'lbRuta
        '
        Me.lbRuta.BackColor = System.Drawing.Color.LightCoral
        Me.lbRuta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbRuta.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRuta.ForeColor = System.Drawing.SystemColors.HotTrack
        Me.lbRuta.Name = "lbRuta"
        Me.lbRuta.Size = New System.Drawing.Size(735, 29)
        Me.lbRuta.TabIndex = 0
        Me.lbRuta.Text = "RUTA X"
        Me.lbRuta.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.Control
        Me.Panel3.Controls.AddRange(New System.Windows.Forms.Control() {Me.btnImprimir, Me.dgPedidos, Me.Panel5})
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel3.Location = New System.Drawing.Point(0, 72)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(736, 489)
        Me.Panel3.TabIndex = 49
        '
        'dgPedidos
        '
        Me.dgPedidos.BackColor = System.Drawing.SystemColors.Control
        Me.dgPedidos.DataSource = Me.DsModificacion.Pedido
        Me.dgPedidos.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgPedidos.EditorsRepository = Me.PersistentRepository2
        Me.dgPedidos.MainView = Me.GridView1
        Me.dgPedidos.Name = "dgPedidos"
        Me.dgPedidos.Size = New System.Drawing.Size(736, 393)
        Me.dgPedidos.Styles.AddReplace("FooterPanel", New DevExpress.Utils.ViewStyle("FooterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.Lavender, System.Drawing.SystemColors.ControlText))
        Me.dgPedidos.Styles.AddReplace("GroupButton", New DevExpress.Utils.ViewStyle("GroupButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlText))
        Me.dgPedidos.Styles.AddReplace("FilterButtonPressed", New DevExpress.Utils.ViewStyle("FilterButtonPressed", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window))
        Me.dgPedidos.Styles.AddReplace("EvenRow", New DevExpress.Utils.ViewStyle("EvenRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", DevExpress.Utils.StyleOptions.None, True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightSkyBlue, System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("HideSelectionRow", New DevExpress.Utils.ViewStyle("HideSelectionRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.InactiveCaptionText))
        Me.dgPedidos.Styles.AddReplace("FilterButton", New DevExpress.Utils.ViewStyle("FilterButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlText))
        Me.dgPedidos.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(217, Byte), CType(230, Byte), CType(240, Byte)), System.Drawing.Color.MediumBlue))
        Me.dgPedidos.Styles.AddReplace("PressedColumn", New DevExpress.Utils.ViewStyle("PressedColumn", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "HeaderPanel", ((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlLightLight))
        Me.dgPedidos.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 8.75!, System.Drawing.FontStyle.Bold), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.Color.White))
        Me.dgPedidos.Styles.AddReplace("ColumnFilterButtonPressed", New DevExpress.Utils.ViewStyle("ColumnFilterButtonPressed", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.Color.Blue))
        Me.dgPedidos.Styles.AddReplace("Empty", New DevExpress.Utils.ViewStyle("Empty", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window))
        Me.dgPedidos.Styles.AddReplace("HeaderPanel", New DevExpress.Utils.ViewStyle("HeaderPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlText))
        Me.dgPedidos.Styles.AddReplace("GroupRow", New DevExpress.Utils.ViewStyle("GroupRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.ControlLight, System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("HorzLine", New DevExpress.Utils.ViewStyle("HorzLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.ControlDark))
        Me.dgPedidos.Styles.AddReplace("Style3", New DevExpress.Utils.ViewStyle("Style3", Nothing, New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(217, Byte), CType(230, Byte), CType(240, Byte)), System.Drawing.Color.MediumBlue))
        Me.dgPedidos.Styles.AddReplace("ColumnFilterButton", New DevExpress.Utils.ViewStyle("ColumnFilterButton", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlText))
        Me.dgPedidos.Styles.AddReplace("FocusedRow", New DevExpress.Utils.ViewStyle("FocusedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.HighlightText))
        Me.dgPedidos.Styles.AddReplace("VertLine", New DevExpress.Utils.ViewStyle("VertLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.ControlDark))
        Me.dgPedidos.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.Color.FromArgb(CType(217, Byte), CType(230, Byte), CType(240, Byte)), System.Drawing.Color.MediumBlue))
        Me.dgPedidos.Styles.AddReplace("GroupFooter", New DevExpress.Utils.ViewStyle("GroupFooter", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.ControlText))
        Me.dgPedidos.Styles.AddReplace("FocusedCell", New DevExpress.Utils.ViewStyle("FocusedCell", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("OddRow", New DevExpress.Utils.ViewStyle("OddRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", DevExpress.Utils.StyleOptions.None, True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.LightSalmon, System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("SelectedRow", New DevExpress.Utils.ViewStyle("SelectedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.HighlightText))
        Me.dgPedidos.Styles.AddReplace("FocusedGroup", New DevExpress.Utils.ViewStyle("FocusedGroup", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "FocusedRow", DevExpress.Utils.StyleOptions.StyleEnabled, True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.InactiveCaption, System.Drawing.SystemColors.HighlightText))
        Me.dgPedidos.Styles.AddReplace("Row", New DevExpress.Utils.ViewStyle("Row", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", DevExpress.Utils.StyleOptions.StyleEnabled, True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Window, System.Drawing.SystemColors.WindowText))
        Me.dgPedidos.Styles.AddReplace("FilterPanel", New DevExpress.Utils.ViewStyle("FilterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.ControlDark, System.Drawing.SystemColors.ControlLightLight))
        Me.dgPedidos.Styles.AddReplace("DetailTip", New DevExpress.Utils.ViewStyle("DetailTip", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Info, System.Drawing.SystemColors.InfoText))
        Me.dgPedidos.TabIndex = 1
        Me.dgPedidos.Text = "GridControl1"
        '
        'PersistentRepository2
        '
        Me.PersistentRepository2.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit1})
        '
        'RepositoryItemTextEdit1
        '
        Me.RepositoryItemTextEdit1.Name = "RepositoryItemTextEdit1"
        Me.RepositoryItemTextEdit1.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit1.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'GridView1
        '
        Me.GridView1.BorderStyle = DevExpress.XtraGrid.Views.Grid.ViewBorderStyle.None
        Me.GridView1.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcPedido, Me.gcTipo, Me.gcLitros, Me.gcPrecio, Me.gcTotal, Me.gcFormaPago, Me.gcContrato, Me.gcCliente, Me.gcDomicilio})
        Me.GridView1.DefaultEdit = Me.RepositoryItemTextEdit1
        Me.GridView1.GroupPanelText = "Numero de  folio"
        Me.GridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.GridView1.Name = "GridView1"
        Me.GridView1.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll
        Me.GridView1.VertScrollTipFieldName = Nothing
        '
        'gcPedido
        '
        Me.gcPedido.Caption = "Pedido"
        Me.gcPedido.FieldName = "Pedido"
        Me.gcPedido.FormatString = ""
        Me.gcPedido.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcPedido.Name = "gcPedido"
        Me.gcPedido.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcPedido.StyleName = "Style1"
        Me.gcPedido.VisibleIndex = 0
        Me.gcPedido.Width = 50
        '
        'gcTipo
        '
        Me.gcTipo.Caption = "Tipo"
        Me.gcTipo.FieldName = "Tipo"
        Me.gcTipo.Name = "gcTipo"
        Me.gcTipo.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcTipo.StyleName = "Style2"
        Me.gcTipo.VisibleIndex = 1
        Me.gcTipo.Width = 35
        '
        'gcLitros
        '
        Me.gcLitros.Caption = "Litros"
        Me.gcLitros.FieldName = "Litros"
        Me.gcLitros.FormatString = "#,##.00"
        Me.gcLitros.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcLitros.Name = "gcLitros"
        Me.gcLitros.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcLitros.StyleName = "Style1"
        Me.gcLitros.VisibleIndex = 2
        Me.gcLitros.Width = 50
        '
        'gcPrecio
        '
        Me.gcPrecio.Caption = "Precio"
        Me.gcPrecio.FieldName = "Precio"
        Me.gcPrecio.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcPrecio.Name = "gcPrecio"
        Me.gcPrecio.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcPrecio.StyleName = "Style1"
        Me.gcPrecio.VisibleIndex = 3
        Me.gcPrecio.Width = 52
        '
        'gcTotal
        '
        Me.gcTotal.Caption = "Total"
        Me.gcTotal.FieldName = "Total"
        Me.gcTotal.FormatString = "$ #,##.00"
        Me.gcTotal.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcTotal.Name = "gcTotal"
        Me.gcTotal.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcTotal.StyleName = "Style1"
        Me.gcTotal.VisibleIndex = 4
        Me.gcTotal.Width = 67
        '
        'gcFormaPago
        '
        Me.gcFormaPago.Caption = "Forma Pago"
        Me.gcFormaPago.FieldName = "Pago"
        Me.gcFormaPago.Name = "gcFormaPago"
        Me.gcFormaPago.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcFormaPago.StyleName = "Style1"
        Me.gcFormaPago.VisibleIndex = 5
        Me.gcFormaPago.Width = 70
        '
        'gcContrato
        '
        Me.gcContrato.Caption = "Contrato"
        Me.gcContrato.FieldName = "Cliente"
        Me.gcContrato.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcContrato.Name = "gcContrato"
        Me.gcContrato.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcContrato.StyleName = "Style3"
        Me.gcContrato.VisibleIndex = 6
        Me.gcContrato.Width = 62
        '
        'gcCliente
        '
        Me.gcCliente.Caption = "Cliente"
        Me.gcCliente.FieldName = "Nombre"
        Me.gcCliente.Name = "gcCliente"
        Me.gcCliente.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcCliente.StyleName = "Style1"
        Me.gcCliente.VisibleIndex = 7
        Me.gcCliente.Width = 150
        '
        'gcDomicilio
        '
        Me.gcDomicilio.Caption = "Domicilio"
        Me.gcDomicilio.FieldName = "Domicilio"
        Me.gcDomicilio.Name = "gcDomicilio"
        Me.gcDomicilio.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcDomicilio.StyleName = "Style1"
        Me.gcDomicilio.VisibleIndex = 8
        Me.gcDomicilio.Width = 114
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Lavender
        Me.Panel5.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel8, Me.Panel7, Me.Panel6})
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel5.Location = New System.Drawing.Point(0, 393)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(736, 96)
        Me.Panel5.TabIndex = 0
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.Panel8.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbNContado, Me.lbImporteContado, Me.lbLitrosContado, Me.Label14, Me.Label15, Me.Panel11, Me.Label16})
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(448, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(288, 96)
        Me.Panel8.TabIndex = 3
        '
        'lbNContado
        '
        Me.lbNContado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNContado.Location = New System.Drawing.Point(112, 10)
        Me.lbNContado.Name = "lbNContado"
        Me.lbNContado.Size = New System.Drawing.Size(96, 14)
        Me.lbNContado.TabIndex = 8
        Me.lbNContado.Text = "0"
        '
        'lbImporteContado
        '
        Me.lbImporteContado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImporteContado.Location = New System.Drawing.Point(113, 51)
        Me.lbImporteContado.Name = "lbImporteContado"
        Me.lbImporteContado.Size = New System.Drawing.Size(95, 14)
        Me.lbImporteContado.TabIndex = 13
        Me.lbImporteContado.Text = "0"
        '
        'lbLitrosContado
        '
        Me.lbLitrosContado.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosContado.Location = New System.Drawing.Point(112, 29)
        Me.lbLitrosContado.Name = "lbLitrosContado"
        Me.lbLitrosContado.Size = New System.Drawing.Size(96, 14)
        Me.lbLitrosContado.TabIndex = 12
        Me.lbLitrosContado.Text = "0"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(8, 50)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(108, 14)
        Me.Label14.TabIndex = 11
        Me.Label14.Text = "Importe contado :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(23, 29)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(93, 14)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Litros contado :"
        '
        'Panel11
        '
        Me.Panel11.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbTAutoTanqueCO})
        Me.Panel11.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel11.Location = New System.Drawing.Point(0, 72)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(288, 24)
        Me.Panel11.TabIndex = 7
        '
        'lbTAutoTanqueCO
        '
        Me.lbTAutoTanqueCO.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbTAutoTanqueCO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTAutoTanqueCO.ForeColor = System.Drawing.Color.MediumBlue
        Me.lbTAutoTanqueCO.Name = "lbTAutoTanqueCO"
        Me.lbTAutoTanqueCO.Size = New System.Drawing.Size(288, 24)
        Me.lbTAutoTanqueCO.TabIndex = 1
        Me.lbTAutoTanqueCO.Text = "Autotanqueturno :"
        Me.lbTAutoTanqueCO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(23, 9)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(94, 14)
        Me.Label16.TabIndex = 9
        Me.Label16.Text = "Notas contado :"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Khaki
        Me.Panel7.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbImporteCredito, Me.lbLitrosCredito, Me.Label8, Me.Label9, Me.Label10, Me.lbNCredito, Me.Panel10})
        Me.Panel7.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel7.Location = New System.Drawing.Point(224, 0)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(224, 96)
        Me.Panel7.TabIndex = 2
        '
        'lbImporteCredito
        '
        Me.lbImporteCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImporteCredito.Location = New System.Drawing.Point(106, 50)
        Me.lbImporteCredito.Name = "lbImporteCredito"
        Me.lbImporteCredito.Size = New System.Drawing.Size(110, 14)
        Me.lbImporteCredito.TabIndex = 13
        Me.lbImporteCredito.Text = "0"
        '
        'lbLitrosCredito
        '
        Me.lbLitrosCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosCredito.Location = New System.Drawing.Point(105, 28)
        Me.lbLitrosCredito.Name = "lbLitrosCredito"
        Me.lbLitrosCredito.Size = New System.Drawing.Size(111, 14)
        Me.lbLitrosCredito.TabIndex = 12
        Me.lbLitrosCredito.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 50)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(102, 14)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Importe credito :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(19, 28)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(87, 14)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Litros credito :"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(18, 8)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(88, 14)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Notas credito :"
        '
        'lbNCredito
        '
        Me.lbNCredito.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNCredito.Location = New System.Drawing.Point(105, 9)
        Me.lbNCredito.Name = "lbNCredito"
        Me.lbNCredito.Size = New System.Drawing.Size(111, 14)
        Me.lbNCredito.TabIndex = 8
        Me.lbNCredito.Text = "0"
        '
        'Panel10
        '
        Me.Panel10.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbTAutotanqueC})
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel10.Location = New System.Drawing.Point(0, 72)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(224, 24)
        Me.Panel10.TabIndex = 7
        '
        'lbTAutotanqueC
        '
        Me.lbTAutotanqueC.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbTAutotanqueC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTAutotanqueC.ForeColor = System.Drawing.Color.MediumBlue
        Me.lbTAutotanqueC.Name = "lbTAutotanqueC"
        Me.lbTAutotanqueC.Size = New System.Drawing.Size(224, 24)
        Me.lbTAutotanqueC.TabIndex = 1
        Me.lbTAutotanqueC.Text = "Autotanqueturno :"
        Me.lbTAutotanqueC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel9, Me.lbTotalImporte, Me.lbTotalLitros, Me.Label5, Me.Label4, Me.Label3, Me.lbTotalRegistros})
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(224, 96)
        Me.Panel6.TabIndex = 1
        '
        'Panel9
        '
        Me.Panel9.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbTAutotanque})
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel9.Location = New System.Drawing.Point(0, 72)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(224, 24)
        Me.Panel9.TabIndex = 6
        '
        'lbTAutotanque
        '
        Me.lbTAutotanque.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbTAutotanque.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTAutotanque.ForeColor = System.Drawing.Color.MediumBlue
        Me.lbTAutotanque.Name = "lbTAutotanque"
        Me.lbTAutotanque.Size = New System.Drawing.Size(224, 24)
        Me.lbTAutotanque.TabIndex = 0
        Me.lbTAutotanque.Text = "Autotanqueturno : "
        Me.lbTAutotanque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbTotalImporte
        '
        Me.lbTotalImporte.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalImporte.Location = New System.Drawing.Point(156, 50)
        Me.lbTotalImporte.Name = "lbTotalImporte"
        Me.lbTotalImporte.Size = New System.Drawing.Size(76, 14)
        Me.lbTotalImporte.TabIndex = 5
        Me.lbTotalImporte.Text = "0"
        '
        'lbTotalLitros
        '
        Me.lbTotalLitros.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalLitros.Location = New System.Drawing.Point(155, 28)
        Me.lbTotalLitros.Name = "lbTotalLitros"
        Me.lbTotalLitros.Size = New System.Drawing.Size(77, 14)
        Me.lbTotalLitros.TabIndex = 4
        Me.lbTotalLitros.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(45, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 14)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Total de Importe :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(62, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 14)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Total de litros :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(-1, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 14)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Total de notas liquidadas :"
        '
        'lbTotalRegistros
        '
        Me.lbTotalRegistros.BackColor = System.Drawing.Color.White
        Me.lbTotalRegistros.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotalRegistros.Location = New System.Drawing.Point(155, 9)
        Me.lbTotalRegistros.Name = "lbTotalRegistros"
        Me.lbTotalRegistros.Size = New System.Drawing.Size(77, 14)
        Me.lbTotalRegistros.TabIndex = 0
        Me.lbTotalRegistros.Text = "0"
        '
        'Panel4
        '
        Me.Panel4.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel14, Me.Panel13, Me.Panel12})
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(736, 72)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(548, 489)
        Me.Panel4.TabIndex = 50
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.SystemColors.Control
        Me.Panel14.Controls.AddRange(New System.Windows.Forms.Control() {Me.dgRampac})
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(548, 289)
        Me.Panel14.TabIndex = 3
        '
        'dgRampac
        '
        Me.dgRampac.DataSource = Me.DsModificacion.Rampac
        Me.dgRampac.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgRampac.EditorsRepository = Me.PersistentRepository3
        Me.dgRampac.MainView = Me.GridView2
        Me.dgRampac.Name = "dgRampac"
        Me.dgRampac.Size = New System.Drawing.Size(548, 289)
        Me.dgRampac.Styles.AddReplace("Preview", New DevExpress.Utils.ViewStyle("Preview", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Top, Nothing, System.Drawing.SystemColors.Window, System.Drawing.Color.ForestGreen))
        Me.dgRampac.Styles.AddReplace("FooterPanel", New DevExpress.Utils.ViewStyle("FooterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Far, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlText))
        Me.dgRampac.Styles.AddReplace("FilterButtonPressed", New DevExpress.Utils.ViewStyle("FilterButtonPressed", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Window))
        Me.dgRampac.Styles.AddReplace("HideSelectionRow", New DevExpress.Utils.ViewStyle("HideSelectionRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.YellowGreen, System.Drawing.Color.White))
        Me.dgRampac.Styles.AddReplace("Style2", New DevExpress.Utils.ViewStyle("Style2", Nothing, New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Info, System.Drawing.Color.ForestGreen))
        Me.dgRampac.Styles.AddReplace("GroupPanel", New DevExpress.Utils.ViewStyle("GroupPanel", "GridView", New System.Drawing.Font("Tahoma", 8.75!, System.Drawing.FontStyle.Bold), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, True, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.YellowGreen, System.Drawing.Color.White))
        Me.dgRampac.Styles.AddReplace("ColumnFilterButtonPressed", New DevExpress.Utils.ViewStyle("ColumnFilterButtonPressed", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.Color.ForestGreen))
        Me.dgRampac.Styles.AddReplace("Empty", New DevExpress.Utils.ViewStyle("Empty", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.Color.ForestGreen))
        Me.dgRampac.Styles.AddReplace("HorzLine", New DevExpress.Utils.ViewStyle("HorzLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.CadetBlue, System.Drawing.SystemColors.ControlDark))
        Me.dgRampac.Styles.AddReplace("FocusedRow", New DevExpress.Utils.ViewStyle("FocusedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.YellowGreen, System.Drawing.Color.White))
        Me.dgRampac.Styles.AddReplace("VertLine", New DevExpress.Utils.ViewStyle("VertLine", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.CadetBlue, System.Drawing.SystemColors.ControlDark))
        Me.dgRampac.Styles.AddReplace("Style1", New DevExpress.Utils.ViewStyle("Style1", Nothing, New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Center, DevExpress.Utils.VertAlignment.Default, Nothing, System.Drawing.SystemColors.Info, System.Drawing.Color.ForestGreen))
        Me.dgRampac.Styles.AddReplace("OddRow", New DevExpress.Utils.ViewStyle("OddRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", DevExpress.Utils.StyleOptions.None, True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.WindowText))
        Me.dgRampac.Styles.AddReplace("SelectedRow", New DevExpress.Utils.ViewStyle("SelectedRow", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseImage), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.Color.YellowGreen, System.Drawing.SystemColors.HighlightText))
        Me.dgRampac.Styles.AddReplace("Row", New DevExpress.Utils.ViewStyle("Row", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", DevExpress.Utils.StyleOptions.StyleEnabled, True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Info, System.Drawing.Color.ForestGreen))
        Me.dgRampac.Styles.AddReplace("FilterPanel", New DevExpress.Utils.ViewStyle("FilterPanel", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Near, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.ControlLightLight))
        Me.dgRampac.Styles.AddReplace("DetailTip", New DevExpress.Utils.ViewStyle("DetailTip", "GridView", New System.Drawing.Font("Microsoft Sans Serif", 8.0!), "", (((((((((DevExpress.Utils.StyleOptions.StyleEnabled Or DevExpress.Utils.StyleOptions.UseBackColor) _
                            Or DevExpress.Utils.StyleOptions.UseDrawEndEllipsis) _
                            Or DevExpress.Utils.StyleOptions.UseDrawFocusRect) _
                            Or DevExpress.Utils.StyleOptions.UseFont) _
                            Or DevExpress.Utils.StyleOptions.UseForeColor) _
                            Or DevExpress.Utils.StyleOptions.UseHorzAlignment) _
                            Or DevExpress.Utils.StyleOptions.UseImage) _
                            Or DevExpress.Utils.StyleOptions.UseWordWrap) _
                            Or DevExpress.Utils.StyleOptions.UseVertAlignment), True, False, False, DevExpress.Utils.HorzAlignment.Default, DevExpress.Utils.VertAlignment.Center, Nothing, System.Drawing.SystemColors.Info, System.Drawing.Color.ForestGreen))
        Me.dgRampac.TabIndex = 0
        '
        'PersistentRepository3
        '
        Me.PersistentRepository3.Items.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.RepositoryItemTextEdit2})
        '
        'RepositoryItemTextEdit2
        '
        Me.RepositoryItemTextEdit2.Name = "RepositoryItemTextEdit2"
        Me.RepositoryItemTextEdit2.Properties.AllowFocused = False
        Me.RepositoryItemTextEdit2.Properties.AutoHeight = False
        Me.RepositoryItemTextEdit2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        '
        'GridView2
        '
        Me.GridView2.BorderStyle = DevExpress.XtraGrid.Views.Grid.ViewBorderStyle.None
        Me.GridView2.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.gcRContrato, Me.gcRLitros, Me.gcRImporte, Me.gcRPago, Me.gcRTipo, Me.gcRInicio, Me.gcRFin})
        Me.GridView2.DefaultEdit = Me.RepositoryItemTextEdit2
        Me.GridView2.GroupPanelText = "Registros recuperados de la tarjeta Rampac"
        Me.GridView2.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Never
        Me.GridView2.Name = "GridView2"
        Me.GridView2.ScrollStyle = DevExpress.XtraGrid.Views.Grid.ScrollStyleFlags.LiveVertScroll
        Me.GridView2.VertScrollTipFieldName = Nothing
        '
        'gcRContrato
        '
        Me.gcRContrato.Caption = "Contrato"
        Me.gcRContrato.FieldName = "Cliente"
        Me.gcRContrato.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcRContrato.Name = "gcRContrato"
        Me.gcRContrato.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRContrato.StyleName = "Style2"
        Me.gcRContrato.VisibleIndex = 0
        Me.gcRContrato.Width = 58
        '
        'gcRLitros
        '
        Me.gcRLitros.Caption = "Litros"
        Me.gcRLitros.FieldName = "Litros"
        Me.gcRLitros.FormatString = "#,##.00"
        Me.gcRLitros.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcRLitros.Name = "gcRLitros"
        Me.gcRLitros.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRLitros.VisibleIndex = 1
        Me.gcRLitros.Width = 58
        '
        'gcRImporte
        '
        Me.gcRImporte.Caption = "Importe"
        Me.gcRImporte.FieldName = "Importe"
        Me.gcRImporte.FormatString = "$ #,##.00"
        Me.gcRImporte.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.Numeric
        Me.gcRImporte.Name = "gcRImporte"
        Me.gcRImporte.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRImporte.VisibleIndex = 2
        Me.gcRImporte.Width = 70
        '
        'gcRPago
        '
        Me.gcRPago.Caption = "Pago"
        Me.gcRPago.FieldName = "FormaPago"
        Me.gcRPago.Name = "gcRPago"
        Me.gcRPago.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRPago.VisibleIndex = 3
        Me.gcRPago.Width = 65
        '
        'gcRTipo
        '
        Me.gcRTipo.Caption = "Tipo"
        Me.gcRTipo.FieldName = "Tipo"
        Me.gcRTipo.Name = "gcRTipo"
        Me.gcRTipo.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRTipo.StyleName = "Style1"
        Me.gcRTipo.VisibleIndex = 4
        Me.gcRTipo.Width = 42
        '
        'gcRInicio
        '
        Me.gcRInicio.Caption = "Inicio"
        Me.gcRInicio.FieldName = "HoraInicio"
        Me.gcRInicio.FormatString = "hh:mm"
        Me.gcRInicio.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.DateTime
        Me.gcRInicio.Name = "gcRInicio"
        Me.gcRInicio.Options = (DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRInicio.VisibleIndex = 5
        Me.gcRInicio.Width = 42
        '
        'gcRFin
        '
        Me.gcRFin.Caption = "Fin"
        Me.gcRFin.FieldName = "HoraFin"
        Me.gcRFin.FormatString = "hh:mm"
        Me.gcRFin.FormatType = DevExpress.XtraGrid.Columns.FormatTypeEnum.DateTime
        Me.gcRFin.Name = "gcRFin"
        Me.gcRFin.Options = ((DevExpress.XtraGrid.Columns.ColumnOptions.CanSorted Or DevExpress.XtraGrid.Columns.ColumnOptions.ReadOnly) _
                    Or DevExpress.XtraGrid.Columns.ColumnOptions.ShowInCustomizationForm)
        Me.gcRFin.VisibleIndex = 6
        Me.gcRFin.Width = 53
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.PowderBlue
        Me.Panel13.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbImporteCreditoR, Me.lbLitrosCreditoR, Me.lbImporteContadoR, Me.lbLitrosContadoR, Me.lbImporteRampac, Me.lbLitrosRampac, Me.lbNotasRampac, Me.Label18, Me.Label17, Me.Label13, Me.Label12, Me.Label11, Me.Label7, Me.Label6})
        Me.Panel13.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel13.Location = New System.Drawing.Point(0, 289)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(548, 176)
        Me.Panel13.TabIndex = 2
        '
        'lbImporteCreditoR
        '
        Me.lbImporteCreditoR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImporteCreditoR.Location = New System.Drawing.Point(165, 95)
        Me.lbImporteCreditoR.Name = "lbImporteCreditoR"
        Me.lbImporteCreditoR.Size = New System.Drawing.Size(96, 14)
        Me.lbImporteCreditoR.TabIndex = 25
        Me.lbImporteCreditoR.Text = "0"
        '
        'lbLitrosCreditoR
        '
        Me.lbLitrosCreditoR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosCreditoR.Location = New System.Drawing.Point(165, 75)
        Me.lbLitrosCreditoR.Name = "lbLitrosCreditoR"
        Me.lbLitrosCreditoR.Size = New System.Drawing.Size(99, 14)
        Me.lbLitrosCreditoR.TabIndex = 24
        Me.lbLitrosCreditoR.Text = "0"
        '
        'lbImporteContadoR
        '
        Me.lbImporteContadoR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImporteContadoR.Location = New System.Drawing.Point(165, 140)
        Me.lbImporteContadoR.Name = "lbImporteContadoR"
        Me.lbImporteContadoR.Size = New System.Drawing.Size(96, 14)
        Me.lbImporteContadoR.TabIndex = 23
        Me.lbImporteContadoR.Text = "0"
        '
        'lbLitrosContadoR
        '
        Me.lbLitrosContadoR.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosContadoR.Location = New System.Drawing.Point(165, 120)
        Me.lbLitrosContadoR.Name = "lbLitrosContadoR"
        Me.lbLitrosContadoR.Size = New System.Drawing.Size(96, 14)
        Me.lbLitrosContadoR.TabIndex = 22
        Me.lbLitrosContadoR.Text = "0"
        '
        'lbImporteRampac
        '
        Me.lbImporteRampac.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbImporteRampac.Location = New System.Drawing.Point(165, 50)
        Me.lbImporteRampac.Name = "lbImporteRampac"
        Me.lbImporteRampac.Size = New System.Drawing.Size(96, 14)
        Me.lbImporteRampac.TabIndex = 21
        Me.lbImporteRampac.Text = "0"
        '
        'lbLitrosRampac
        '
        Me.lbLitrosRampac.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbLitrosRampac.Location = New System.Drawing.Point(165, 29)
        Me.lbLitrosRampac.Name = "lbLitrosRampac"
        Me.lbLitrosRampac.Size = New System.Drawing.Size(96, 14)
        Me.lbLitrosRampac.TabIndex = 20
        Me.lbLitrosRampac.Text = "0"
        '
        'lbNotasRampac
        '
        Me.lbNotasRampac.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNotasRampac.Location = New System.Drawing.Point(165, 9)
        Me.lbNotasRampac.Name = "lbNotasRampac"
        Me.lbNotasRampac.Size = New System.Drawing.Size(96, 14)
        Me.lbNotasRampac.TabIndex = 19
        Me.lbNotasRampac.Text = "0"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(62, 95)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(102, 14)
        Me.Label18.TabIndex = 18
        Me.Label18.Text = "Importe credito :"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(77, 75)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(87, 14)
        Me.Label17.TabIndex = 17
        Me.Label17.Text = "Litros credito :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(56, 140)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(108, 14)
        Me.Label13.TabIndex = 16
        Me.Label13.Text = "Importe contado :"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(70, 120)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(93, 14)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "Litros contado :"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(75, 50)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(88, 14)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Importe total :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(60, 29)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 14)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Litros liquidados :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(11, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(154, 14)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "Total de notas liquidadas :"
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.White
        Me.Panel12.Controls.AddRange(New System.Windows.Forms.Control() {Me.lbEficiencia})
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel12.Location = New System.Drawing.Point(0, 465)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(548, 24)
        Me.Panel12.TabIndex = 1
        '
        'lbEficiencia
        '
        Me.lbEficiencia.BackColor = System.Drawing.Color.PowderBlue
        Me.lbEficiencia.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbEficiencia.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbEficiencia.ForeColor = System.Drawing.Color.MediumBlue
        Me.lbEficiencia.Name = "lbEficiencia"
        Me.lbEficiencia.Size = New System.Drawing.Size(548, 24)
        Me.lbEficiencia.TabIndex = 0
        Me.lbEficiencia.Text = "Eficiencia : 0"
        Me.lbEficiencia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmdCPedidos
        '
        'Me.cmdCPedidos.CommandText = "SELECT P.Celula, P.AñoPed, P.Pedido, CASE P.TipoPedido WHEN 1 THEN 'T' WHEN 2 THE" & _
        '"N 'P' WHEN 3 THEN 'N' END AS Tipo, P.TipoPedido, P.Litros, P.Precio, P.Total, P." & _
        '"Cliente, P.TipoCobro, P.Autotanque, C.Nombre, CASE P.TipoCobro WHEN 5 THEN 'Cont" & _
        '"ado' WHEN 8 THEN 'Credito' WHEN 9 THEN 'Credito' WHEN 6 THEN 'Credito' END AS Pa" & _
        '"go, CONVERT(VarChar(60), CA.Nombre) + ' # ' + CONVERT(VarChar(9), C.NumExterior)" & _
        '" + ' ' + CONVERT(VarChar(50), ISNULL(C.NumInterior, '')) AS Domicilio FROM Pedid" & _
        '"o P INNER JOIN Cliente C ON P.Cliente = C.Cliente INNER JOIN Calle CA ON C.Calle" & _
        '" = CA.Calle LEFT OUTER JOIN Calle CA1 ON C.EntreCalle1 = CA1.Calle LEFT OUTER JO" & _
        '"IN Calle CA2 ON C.EntreCalle2 = CA2.Calle WHERE (P.Folio = @Folio) AND (P.AñoAtt" & _
        '" = @AñoAtt) AND (P.TipoPedido IN (1, 2, 3)) AND (P.TipoCargo = 1) ORDER BY P.Lit" & _
        '"ros"
        Me.cmdCPedidos.CommandText = "spCCMLConsultaDetallePedido"
        Me.cmdCPedidos.CommandType = CommandType.StoredProcedure
        Me.cmdCPedidos.Connection = Me.SqlConnection
        Me.cmdCPedidos.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Folio", System.Data.SqlDbType.Int, 4, "Folio"))
        Me.cmdCPedidos.Parameters.Add(New System.Data.SqlClient.SqlParameter("@AñoAtt", System.Data.SqlDbType.SmallInt, 2, "AñoAtt"))
        '
        'daPedidos
        '
        Me.daPedidos.SelectCommand = Me.cmdCPedidos
        '
        'cmdRampac
        '
        'Me.cmdRampac.CommandText = "SELECT Cliente, Litros, Importe, FormaPago, TipoOperacion, HoraInicio, HoraFin, C" & _
        '"ASE TipoOperacion WHEN 'NotaBlanca' THEN 'N' WHEN 'Tarjeta' THEN 'T' WHEN 'Dupli" & _
        '"cado' THEN 'D' END AS Tipo, CASE FormaPago WHEN 'Contado' THEN 'CO' WHEN 'Crédit" & _
        '"o' THEN 'CR' END AS Pago FROM Rampac WHERE (Folio = @Folio) AND (AñoAtt = @Añoat" & _
        '"t) ORDER BY ConsecutivoRampac"
        Me.cmdRampac.CommandText = "spCCMLConsultaDetalleRampac"
        Me.cmdRampac.CommandType = CommandType.StoredProcedure
        Me.cmdRampac.Connection = Me.SqlConnection
        Me.cmdRampac.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Folio", System.Data.SqlDbType.Int, 4, "Folio"))
        Me.cmdRampac.Parameters.Add(New System.Data.SqlClient.SqlParameter("@Añoatt", System.Data.SqlDbType.SmallInt, 2, "AñoAtt"))
        '
        'daRampac
        '
        Me.daRampac.SelectCommand = Me.cmdRampac
        '
        'btnImprimir
        '
        Me.btnImprimir.Anchor = (System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right)
        Me.btnImprimir.Image = CType(resources.GetObject("btnImprimir.Image"), System.Drawing.Bitmap)
        Me.btnImprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImprimir.Location = New System.Drawing.Point(656, 4)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.Size = New System.Drawing.Size(76, 24)
        Me.btnImprimir.TabIndex = 113
        Me.btnImprimir.Text = "     Imprimir"
        '
        'TerminoLiquidacion
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 14)
        Me.ClientSize = New System.Drawing.Size(1284, 561)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.Panel4, Me.Panel3, Me.Panel1, Me.cbCelula, Me.Label1, Me.tlbLiquidacion})
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.MainMenu1
        Me.Name = "TerminoLiquidacion"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Liquidaciones"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.DsModificacion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgPedidos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel8.ResumeLayout(False)
        Me.Panel11.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel10.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel9.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel14.ResumeLayout(False)
        CType(Me.dgRampac, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RepositoryItemTextEdit2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel13.ResumeLayout(False)
        Me.Panel12.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub tlbLiquidacion_ButtonClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolBarButtonClickEventArgs) Handles tlbLiquidacion.ButtonClick
        Select Case e.Button.Text
            Case "Cerrar" : MenuItem20_Click(sender, e)
            Case "Actualizar" : Actualizar()
            Case "Cliente" : Cliente()
            Case "Credito", "Contado" : CambioContadoCredito(e.Button.Text)
            Case "Cancelacion" : Cancelar()
            Case "Cheques" : Cheques()
            Case "Consultar" : Consultar()
            Case "Cancelación Total" : cancelacionTotal()
        End Select
    End Sub

    Private Sub MenuItem20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem20.Click
        Application.DoEvents()
        tlbLiquidacion.Focus()
        Me.Close()
    End Sub

    Private Sub TerminoLiquidacion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SqlConnection.ConnectionString = GLOBAL_ConString
            SqlConnection.Open()
        Catch dataException As Exception
            MsgBox(dataException.Message, MsgBoxStyle.OKOnly, "Mensaje de sistema")
        End Try

        DaCelula.Fill(DsModificacion, "Celula")
        cbCelula.SelectedValue = GLOBAL_Celula

        habilitaCancelaciónTotal()
    End Sub

    Private Sub StatusAutotanqueTurno()
        Dim cmdInsert As New SqlClient.SqlCommand()
        Dim rdrInsert As SqlClient.SqlDataReader = Nothing

        cmdInsert.Connection = SqlConnection
        cmdInsert.CommandTimeout = 30
        'cmdInsert.CommandText = " Select AñoAtt, Folio, StatusLogistica, IsNull(ImporteContado,0) as ImporteContado, IsNull(ImporteCredito,0) as ImporteCredito, (IsNull(ImporteContado,0)+IsNull(ImporteCredito,0)) as Total, IsNull(ImporteEficiencia,0) as ImporteEficiencia, IsNull(TipoLiquidacion,'') as TipoLiquidacion, IsNull(UsuarioLiquidacion,'') as Usuario, FPreliquidacion from AutotanqueTurno " & _
        '                        " where Ruta=@Ruta and (DAY(FTerminoRuta)=@Dia and MONTH(FTerminoRuta)=@Mes and YEAR(FTerminoRuta)=@Año) and Autotanque=@Autotanque and Folio=@Folio "
        cmdInsert.CommandText = "spCCMLConsultaDetalleAutotanque"
        cmdInsert.CommandType = CommandType.StoredProcedure
        cmdInsert.Parameters.Clear()
        'cmdInsert.Parameters.Add("@Ruta", SqlDbType.Int).Value = _Ruta
        'cmdInsert.Parameters.Add("@Autotanque", SqlDbType.Int).Value = _Autotanque
        'cmdInsert.Parameters.Add("@Dia", SqlDbType.Int).Value = dtpFecha.Value.Date.Day
        'cmdInsert.Parameters.Add("@Mes", SqlDbType.Int).Value = dtpFecha.Value.Date.Month
        'cmdInsert.Parameters.Add("@Año", SqlDbType.Int).Value = dtpFecha.Value.Date.Year
        'cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _FolioRuta

        cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.Int).Value = dtpFecha.Value.Year
        cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _FolioRuta

        _AnioAtt = 0
        _Folio = 0
        _StatusLogistica = "NINGUNO"
        _TotalAuto = 0
        _TotalCreditoAuto = 0
        _TotalContadoAuto = 0
        _Eficiencia = 0
        _TipoLiquidacion = "NINGUNA"
        _UsuarioLiquidacion = ""
        _FechaLiquidacion = ""

        Try
            rdrInsert = cmdInsert.ExecuteReader()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        While rdrInsert.Read()
            _AnioAtt = CType(rdrInsert("AñoAtt"), Integer)
            _Folio = CType(rdrInsert("Folio"), Integer)
            _StatusLogistica = RTrim(CType(rdrInsert("StatusLogistica"), String))
            _TotalAuto = CType(rdrInsert("Total"), Decimal)
            _TotalCreditoAuto = CType(rdrInsert("ImporteCredito"), Decimal)
            _TotalContadoAuto = CType(rdrInsert("ImporteContado"), Decimal)
            _Eficiencia = CType(rdrInsert("ImporteEficiencia"), Decimal)
            _TipoLiquidacion = CType(rdrInsert("TipoLiquidacion"), String)
            _UsuarioLiquidacion = CType(rdrInsert("Usuario"), String)

            If Not IsDBNull(rdrInsert("FPreliquidacion")) Then
                _FechaLiquidacion = Format(rdrInsert("FPreliquidacion"), "dd/MM/yyyy hh:mm:ss")
            End If

            'Folios descuadrados
            Me.picWarning.Visible = False
            If CType(IIf(rdrInsert("Observaciones") Is DBNull.Value, String.Empty, rdrInsert("Observaciones")), String).Trim = "DESCUADRADO" Then
                Me.lbRuta.BackColor = Color.LightCoral
                Me.picWarning.Visible = True
            End If
        End While

        rdrInsert.Close()
        cmdInsert.Dispose()
        Me.GridView1.GroupPanelText = "Folio: " + CType(_AnioAtt, String) + "-" + CType(_Folio, String) + " Status: " + RTrim(_StatusLogistica) + " Tipo: " + RTrim(_TipoLiquidacion) + " Usuario: " + RTrim(_UsuarioLiquidacion) + " Fecha: " + RTrim(_FechaLiquidacion)
        lbRuta.Text = "RUTA " + CType(_Ruta, String) + " - AUTOTANQUE " + CType(_Autotanque, String)
        btnConsultaFolio.Enabled = True
    End Sub

    'Private Sub ValidaCaja()
    '    Dim cmdInsert As New SqlClient.SqlCommand()
    '    Dim rdrInsert As SqlClient.SqlDataReader

    '    cmdInsert.Connection = SqlConnection
    '    cmdInsert.CommandTimeout = 30
    '    cmdInsert.CommandText = "SELECT Observaciones FROM vwFolioDescuadrado WHERE AñoAtt = @AñoAtt And Folio = @Folio"
    '    cmdInsert.Parameters.Clear()
    '    cmdInsert.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AnioAtt
    '    cmdInsert.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
    '    rdrInsert = cmdInsert.ExecuteReader()

    '    Me.picWarning.Visible = False

    '    If rdrInsert.Read Then
    '        If CType(rdrInsert("Observaciones"), String).Trim = "DESCUADRADO" Then
    '            Me.lbRuta.BackColor = Color.LightCoral
    '            Me.picWarning.Visible = True
    '        End If
    '    End If
    '    rdrInsert.Close()
    '    btnConsultaFolio.Enabled = True
    'End Sub

    Private Sub RefreshData()
        CargarRutas(CType(cbCelula.SelectedValue, Integer))
        StatusAutotanqueTurno()
        Actualizar()
        'ValidaCaja()
    End Sub

    Private Sub cbCelula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCelula.SelectedIndexChanged
        RefreshData()
    End Sub

    Private Sub dtpFecha_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpFecha.ValueChanged
        RefreshData()
    End Sub

    Private Sub SumaLitros(ByVal Tipo As Integer)
        If _StatusLogistica = "LIQUIDADO" Or CType(_Nivel, Double) = 1 Then
            Dim frmSumaLitros As SumaLitros = New SumaLitros()
            frmSumaLitros.Entrada(_AnioAtt, _Folio, _Autotanque, _Ruta, Tipo)
            frmSumaLitros.Dispose()
            Actualizar()
        Else
            MsgBox("Con este status no se puede modificar la liquidación. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
        End If
    End Sub

    Private Sub MenuItem21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem21.Click
        SumaLitros(0)
    End Sub

    Private Sub MenuItem23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem23.Click
        SumaLitros(1)
    End Sub

#Region "Cancelación total de liquidación"
    Private Sub habilitaCancelaciónTotal()
        If Not (oSeguridad.TieneAcceso("CancelacionTotalLiquidacion")) Then
            tlbLiquidacion.Buttons.Remove(btnCancelarAll)
        Else
            btnCancelarAll.Enabled = True
        End If
    End Sub

    Private Sub cargaDatosCancelacion(ByVal FolioAtt As Integer, ByVal AñoAtt As Integer)
        Dim selectCmd As New SqlClient.SqlCommand()
        selectCmd.CommandText = "SELECT	FOperacion, FMovimiento, Caja, Consecutivo, Folio " & _
                                                             "FROM MovimientoCaja " & _
                                                             "WHERE FolioAtt = @Folio AND AñoAtt = @AñoAtt"
        selectCmd.CommandType = CommandType.Text
        selectCmd.Connection = CnnSigamet
        selectCmd.Parameters.Add("@Folio", SqlDbType.Int).Value = FolioAtt
        selectCmd.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = AñoAtt
        Dim dr As SqlClient.SqlDataReader
        Try
            CnnSigamet.Open()
            dr = selectCmd.ExecuteReader
            While dr.Read
                _FechaOperacion = CType(dr.Item("FOperacion"), Date)
                _FechaMovimiento = CType(dr.Item("FMovimiento"), Date)
                _Caja = CType(dr.Item("Caja"), Integer)
                _Consecutivo = CType(dr.Item("Consecutivo"), Integer)
                _FolioCaja = CType(dr.Item("Folio"), Integer)
                dr.NextResult()
            End While
        Catch ex As SqlClient.SqlException
            MessageBox.Show("Ha ocurrido el siguiente error " & ex.Number & " " & ex.Message, "Error", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Ha ocurrido el siguiente error " & ex.Message, "Error", _
            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        If CnnSigamet.State = ConnectionState.Open Then
            CnnSigamet.Close()
        End If
    End Sub

    Private Sub cancelacionTotal()
        If oSeguridad.TieneAcceso("CancelacionTotalLiquidacion") AndAlso _StatusLogistica = "LIQCAJA" Then
            If MessageBox.Show("Se cancelará por completo la liquidación" & Chr(13) & _
                "¿Desea continuar?", "Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Information) _
                = DialogResult.Yes Then
                cargaDatosCancelacion(_Folio, _AnioAtt)
                Dim cancelaCmd As New SqlClient.SqlCommand()
                cancelaCmd.CommandText = "spCACancelacionTOTALdeLiquidacion"
                cancelaCmd.CommandTimeout = 200 'Tiempo de espera de 200 ms
                cancelaCmd.CommandType = CommandType.StoredProcedure
                cancelaCmd.Connection = CnnSigamet
                cancelaCmd.Parameters.Add("@AñoAtt", SqlDbType.SmallInt).Value = _AnioAtt
                cancelaCmd.Parameters.Add("@Folio", SqlDbType.Int).Value = _Folio
                cancelaCmd.Parameters.Add("@FMovimiento", SqlDbType.DateTime).Value = _FechaMovimiento
                cancelaCmd.Parameters.Add("@FOperacion", SqlDbType.DateTime).Value = _FechaOperacion
                cancelaCmd.Parameters.Add("@Caja", SqlDbType.TinyInt).Value = _Caja
                cancelaCmd.Parameters.Add("@Consecutivo", SqlDbType.TinyInt).Value = _Consecutivo
                cancelaCmd.Parameters.Add("@FolioCaja", SqlDbType.Int).Value = _Consecutivo
                cancelaCmd.Parameters.Add("@UsuarioCancelacion", SqlDbType.VarChar).Value = GLOBAL_Usuario
                Try
                    CnnSigamet.Open()
                    cancelaCmd.ExecuteNonQuery()
                Catch ex As SqlClient.SqlException
                    MessageBox.Show("Ha ocurrido el siguiente error " & ex.Number & " " & ex.Message, "Error", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Catch ex As Exception
                    MessageBox.Show("Ha ocurrido el siguiente error " & ex.Message, "Error", _
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    If CnnSigamet.State = ConnectionState.Open Then
                        CnnSigamet.Close()
                    End If
                    cancelaCmd.Dispose()
                End Try
            End If
        End If
    End Sub
#End Region

    Private Sub MenuItem24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem24.Click
        If _StatusLogistica = "LIQUIDADO" Then
            If DsModificacion.Pedido(GridView1.FocusedRowHandle).TipoCobro = 6 Then
                TarjetaCredito_a_CreditoOperador()
            Else
                MsgBox("Solo se puede cambiar a CREDITO OPERADOR un registro de TARJETA CREDITO.", MsgBoxStyle.Information, "Mensaje del sistema")
            End If
        Else
            MsgBox("Con este status no se puede modificar la liquidación. Verifique", MsgBoxStyle.Information, "Mensaje del sistema")
        End If
    End Sub

    Private Sub MenuItem25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MenuItem25.Click
        Dim frmCambioCheque As CambioCheque = New CambioCheque()
        frmCambioCheque.Entrada(_AnioAtt, _Folio, DsModificacion.Pedido(GridView1.FocusedRowHandle).Cliente, DsModificacion.Pedido(GridView1.FocusedRowHandle).AñoPed, DsModificacion.Pedido(GridView1.FocusedRowHandle).Pedido, DsModificacion.Pedido(GridView1.FocusedRowHandle).Celula)
        frmCambioCheque.Dispose()
    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Cursor = Cursors.WaitCursor
        Dim oReporte As New frmConsultaReporte(CType(_AnioAtt, Short), _Folio)
        oReporte.ShowDialog()
        Cursor = Cursors.Default
    End Sub
End Class

