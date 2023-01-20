Imports System.Reflection
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports ADODB
Imports BIFData.GOIntranet

Namespace BIFConvenios


    Public Class SystemParameterBL

        Public Function GetSystemParameterBL(ByVal lobjEntidad As SystemParameterBE) As DataSet

            Dim lobj As New SystemParameterDO()
            Return lobj.SystemParametersListar(lobjEntidad)

        End Function


        Public Function UpdateSystemParameterBL(ByVal lobjEntidad As SystemParameterBE, ByVal lobjEntidad_Org As SystemParameterBE) As Boolean

            Dim result As Boolean
            Dim lobj As New SystemParameterDO()

            Try
                lobj.SystemParametersModificar(lobjEntidad, lobjEntidad_Org)
                result = True

            Catch ex As Exception
                result = False
            End Try

            Return result

        End Function

        Public Function InsertSystemParameterBL(ByVal lobjEntidad As SystemParameterBE) As Boolean

            Dim result As Boolean
            Dim lobj As New SystemParameterDO()

            Try
                lobj.SystemParametersInsertar(lobjEntidad)
                result = True

            Catch ex As Exception
                result = False
            End Try

            Return result

        End Function

        Public Function DeleteSystemParameterBL(ByVal lobjEntidad As SystemParameterBE) As Boolean

            Dim result As Boolean
            Dim lobj As New SystemParameterDO()

            Try
                lobj.SystemParametersEliminar(lobjEntidad)
                result = True

            Catch ex As Exception
                result = False
            End Try

            Return result

        End Function


    End Class

End Namespace