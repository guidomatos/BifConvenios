Imports System.Data
Imports System.Data.SqlClient
Imports ADODB
Imports System.Configuration
Imports System.Collections.Generic

Public Class Envio_informacion_cobranza_IBS

    Dim conexionIBS As String = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")
    Dim conexionConvenios As String = BIFUtils.WS.Utils.CadenaConexion("ConnectionString")



    Public Sub DataEnvioAS400(ByRef codigo_proceso As String)

        '' busca los pagares que va a cobrar que sean mayores que cero

        Dim sql As String = "EnvioDescuentos_AS400"
        Dim reader As SqlDataReader
        Dim cn As New SqlConnection(Me.conexionConvenios)
        Dim cmd As New SqlCommand(sql, cn)
        Dim objDLREC As DLREC

        Dim PROCESO_ENVIO_AS400 As Boolean = False

        Dim list_dlrec As List(Of DLREC) = Nothing
        Dim list_dlrec_ESTADO As List(Of DLREC) = Nothing

        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add(New SqlParameter("@codigo_proceso", codigo_proceso))
        cn.Open()

        reader = cmd.ExecuteReader()

        If reader.HasRows Then

            list_dlrec = New List(Of DLREC)

            While reader.Read()

                Try
                    objDLREC = New DLREC()

                    objDLREC.DLAG = reader("DLAG")
                    objDLREC.DLAN = reader("DLAN")
                    objDLREC.DLAP = reader("DLAP")
                    objDLREC.DLCC = reader("DLCC")
                    objDLREC.DLCM = reader("DLCM")
                    objDLREC.DLCO = reader("DLCO")
                    objDLREC.DLCR = reader("DLCR")
                    objDLREC.DLER = reader("DLER")
                    objDLREC.DLFP = reader("DLFP")
                    objDLREC.DLIC = reader("DLIC")
                    objDLREC.DLID = reader("DLID")
                    objDLREC.DLMO = reader("DLMO")
                    objDLREC.DLMP = reader("DLMP")
                    objDLREC.DLNE = reader("DLNE")
                    objDLREC.DLNP = reader("DLNP")
                    objDLREC.DLST = reader("DLST")
                    objDLREC.Estado = reader("Estado")


                    'objDLREC.DLAG = reader("DLRAG")
                    'objDLREC.DLAN = reader("DLRAN")
                    'objDLREC.DLAP = reader("DLRAP")
                    'objDLREC.DLCC = reader("DLRCC")
                    'objDLREC.DLCM = reader("DLRCM")
                    'objDLREC.DLCO = reader("DLRCO")
                    'objDLREC.DLCR = reader("DLRCR")
                    'objDLREC.DLER = reader("DLRER")
                    'objDLREC.DLFP = reader("DLRFP")
                    'objDLREC.DLIC = reader("DLRIC")
                    'objDLREC.DLID = reader("DLRID")
                    'objDLREC.DLMO = reader("DLRMO")
                    'objDLREC.DLMP = reader("DLRMP")
                    'objDLREC.DLNE = reader("DLRNE")
                    'objDLREC.DLNP = reader("DLRNP")
                    'objDLREC.DLST = reader("DLRST")
                    'objDLREC.Estado = ""


                    list_dlrec.Add(objDLREC)

                    objDLREC = Nothing


                Catch ex As Exception
                    objDLREC = Nothing
                End Try
                
            End While

        End If

        reader.Close()
        cn.Close()
        ''cn.Dispose()


        ''' asignamos a lista list_dlrec_ESTADO lo de la valores de lista list_dlrec para efectos de modificacion de estados

        ''list_dlrec_ESTADO = list_dlrec


        ''  Envìo al AS400
        Try

            Dim ADODBConn As New ADODB.Connection()
            Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()


            ADODBConn.CursorLocation = CursorLocationEnum.adUseClient
            ADODBConn.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

            Dim ADODBCmd As ADODB.Command

            list_dlrec_ESTADO = New List(Of DLREC)

            For Each rec As DLREC In list_dlrec

                Try

                    ''ADODBConn.Execute("INSERT INTO BIFCYFILES.DLREC (DLRAG,DLRAN,DLRAP,DLRCC,DLRCM,DLRCO,DLRCR,DLRER,DLRFP,DLRIC,DLRID,DLRMO,DLRMP,DLRNE,DLRNP,DLRST)" _
                    ADODBConn.Execute("INSERT INTO BIFCYFILES.DLREC (DLRAG,DLRAN,DLRAP,DLRCC,DLRCM,DLRCO,DLRCR,DLRER,DLRFP,DLRIC,DLRID,DLRMO,DLRMP,DLRNE,DLRNP,DLRST)" _
                    & " VALUES ( " _
                    & rec.DLAG.ToString & "," _
                    & rec.DLAN.ToString & "," _
                    & rec.DLAP.ToString & "," _
                    & rec.DLCC.ToString & ",'" _
                    & rec.DLCM.ToString & "'," _
                    & rec.DLCO.ToString & ",'" _
                    & rec.DLCR.ToString & "','" _
                    & rec.DLER.ToString & "','" _
                    & rec.DLFP.ToString & "'," _
                    & rec.DLIC.ToString & "," _
                    & rec.DLID.ToString & ",'" _
                    & rec.DLMO.ToString & "','" _
                    & rec.DLMP.ToString & "','" _
                    & rec.DLNE.ToString & "'," _
                    & rec.DLNP.ToString & ",'" _
                    & rec.DLST.ToString & "')")

                    rec.Estado = "A4"

                    list_dlrec_ESTADO.Add(rec)

                Catch ex As Exception

                    ''list_dlrec.Remove(rec)
                    ''list_dlrec.Remove(rec)
                    rec.Estado = "EE"
                    list_dlrec_ESTADO.Add(rec)

                End Try
            Next

            '' si proceso termina correctamente
            PROCESO_ENVIO_AS400 = True

        Catch ex As Exception

            PROCESO_ENVIO_AS400 = False

        End Try



        ' Cambia estado  de pagarès en BIFConvenios
        Try

            ''ABRE NUEVAMENTE LA CONEXIÒN
            cn.Open()

            cmd.CommandType = CommandType.Text

            Dim strsql = String.Empty

            If PROCESO_ENVIO_AS400 Then

                Try

                    For Each recc As DLREC In list_dlrec_ESTADO

                        strsql = "update clientecuota set estado = '" & recc.Estado & "' where codigo_proceso ='" & codigo_proceso & "' and dlnp = '" & recc.DLNP.Trim() & "'"
                        cmd = New SqlCommand(strsql, cn)
                        cmd.ExecuteNonQuery()

                    Next

                    ''FinalizadoEnvioInformacionAS400
                    ''strsql = "update proceso set estado = 'A2' where codigo_proceso ='" & codigo_proceso & "' "
                    strsql = "FinalizadoEnvioInformacionAS400"

                    Dim cmd2 As New SqlCommand(strsql, cn)
                    cmd2.CommandType = CommandType.StoredProcedure
                    cmd2.Parameters.Add(New SqlParameter("@Codigo_proceso", codigo_proceso))
                    cmd2.Parameters.Add(New SqlParameter("@usuario", String.Empty))

                    cmd2.ExecuteNonQuery()

                Catch ex As Exception

                End Try

            Else
                '' si ocurre un error en el envìo , marca como error en el proceso

                strsql = "update proceso set estado = 'EA' where codigo_proceso ='" & codigo_proceso & "' "
                cmd = New SqlCommand(strsql, cn)
                cmd.ExecuteNonQuery()

            End If


        Catch ex As Exception

        End Try

        cn.Dispose()



    End Sub

End Class
