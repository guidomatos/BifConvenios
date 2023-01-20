Imports System.Reflection
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports ADODB
Imports BIFData.GOIntranet

Namespace BIFConvenios

    Public Class SystemParameterDO

        Public Function SystemParametersListar(ByVal lobjEntidad As SystemParameterBE) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()
            Dim Cmd As New SqlCommand

            Try

                myConnection.Open()
                Cmd.Connection = myConnection
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "SystemParametersListar"
                Cmd.Parameters.AddWithValue("@iGrupoId", lobjEntidad.GrupoId)
                Cmd.Parameters.AddWithValue("@iParametroId", lobjEntidad.ParametroId)
                Cmd.Parameters.AddWithValue("@vDescripcion", lobjEntidad.Descripcion)
                Cmd.Parameters.AddWithValue("@iVisible", lobjEntidad.Visible)
                Cmd.Parameters.AddWithValue("@iEstado", lobjEntidad.Estado)

                Dim adapter As New SqlDataAdapter(Cmd)
                adapter.SelectCommand.CommandTimeout = 300

                'Fill the dataset
                adapter.Fill(oDS)
                myConnection.Close()

                Return oDS
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function SystemParametersModificar(ByVal lobjEntidad As SystemParameterBE, ByVal lobjEntidad_Org As SystemParameterBE) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim result As Boolean
            Dim oAdapter As New SqlDataAdapter()
            Dim Cmd As New SqlCommand

            Try

                myConnection.Open()
                Cmd.Connection = myConnection
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "SystemParametersModificar"
                Cmd.Parameters.AddWithValue("@iGrupoId", lobjEntidad.GrupoId)
                Cmd.Parameters.AddWithValue("@iParametroId", lobjEntidad.ParametroId)
                Cmd.Parameters.AddWithValue("@vDescripcion", lobjEntidad.Descripcion)
                Cmd.Parameters.AddWithValue("@vValor", lobjEntidad.Valor)
                Cmd.Parameters.AddWithValue("@iOrden", lobjEntidad.Orden)
                Cmd.Parameters.AddWithValue("@iEstado", lobjEntidad.Estado)

                Cmd.Parameters.AddWithValue("@dFechaInicio", lobjEntidad.FechaInicio)
                Cmd.Parameters.AddWithValue("@dFechaFin", lobjEntidad.FechaFin)
                Cmd.Parameters.AddWithValue("@iVisible", lobjEntidad.Visible)

                Cmd.Parameters.AddWithValue("@vDescripcionO", lobjEntidad_Org.Descripcion)
                Cmd.Parameters.AddWithValue("@vValorO", lobjEntidad_Org.Valor)
                Cmd.Parameters.AddWithValue("@iOrdenO", lobjEntidad_Org.Orden)
                Cmd.Parameters.AddWithValue("@iEstadoO", lobjEntidad_Org.Estado)

                Cmd.Parameters.AddWithValue("@dFechaInicioO", lobjEntidad_Org.FechaInicio)
                Cmd.Parameters.AddWithValue("@dFechaFinO", lobjEntidad_Org.FechaFin)
                Cmd.Parameters.AddWithValue("@iVisibleO", lobjEntidad_Org.Visible)

                Cmd.Parameters.AddWithValue("@vUsuarioModificacion", lobjEntidad.UsuarioModificacion)

                Dim adapter As New SqlDataAdapter(Cmd)
                adapter.SelectCommand.CommandTimeout = 300

                result = Cmd.ExecuteNonQuery()
                myConnection.Close()

                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Function SystemParametersInsertar(ByVal lobjEntidad As SystemParameterBE) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim result As Boolean
            Dim oAdapter As New SqlDataAdapter()
            Dim Cmd As New SqlCommand

            Try

                myConnection.Open()
                Cmd.Connection = myConnection
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "SystemParametersInsertar"
                Cmd.Parameters.AddWithValue("@iGrupoId", lobjEntidad.GrupoId)
                Cmd.Parameters.AddWithValue("@iParametroId", lobjEntidad.ParametroId)
                Cmd.Parameters.AddWithValue("@vDescripcion", lobjEntidad.Descripcion)
                Cmd.Parameters.AddWithValue("@vValor", lobjEntidad.Valor)
                Cmd.Parameters.AddWithValue("@iOrden", lobjEntidad.Orden)
                Cmd.Parameters.AddWithValue("@iVisible", lobjEntidad.Visible)

                Cmd.Parameters.AddWithValue("@dFechaInicio", lobjEntidad.FechaInicio)
                Cmd.Parameters.AddWithValue("@dFechaFin", lobjEntidad.FechaFin)
                Cmd.Parameters.AddWithValue("@iEstado", lobjEntidad.Estado)

                Cmd.Parameters.AddWithValue("@vUsuarioCreacion", lobjEntidad.UsuarioCreacion)

                Dim adapter As New SqlDataAdapter(Cmd)
                adapter.SelectCommand.CommandTimeout = 300

                result = Cmd.ExecuteNonQuery()
                myConnection.Close()

                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Public Function SystemParametersEliminar(ByVal lobjEntidad As SystemParameterBE) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim result As Boolean
            Dim oAdapter As New SqlDataAdapter()
            Dim Cmd As New SqlCommand

            Try

                myConnection.Open()
                Cmd.Connection = myConnection
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.CommandText = "SystemParametersEliminar"
                Cmd.Parameters.AddWithValue("@iGrupoId", lobjEntidad.GrupoId)
                Cmd.Parameters.AddWithValue("@iParametroId", lobjEntidad.ParametroId)
                Cmd.Parameters.AddWithValue("@iEstado", lobjEntidad.Estado)
                Cmd.Parameters.AddWithValue("@vUsuarioModificacion", lobjEntidad.UsuarioModificacion)

                Dim adapter As New SqlDataAdapter(Cmd)
                adapter.SelectCommand.CommandTimeout = 300

                result = Cmd.ExecuteNonQuery()
                myConnection.Close()

                Return result
            Catch ex As Exception
                Throw ex
            End Try
        End Function


    End Class

End Namespace