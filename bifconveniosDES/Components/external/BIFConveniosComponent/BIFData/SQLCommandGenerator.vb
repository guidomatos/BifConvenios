
Imports System
Imports System.Reflection
Imports System.Data
Imports System.Data.SqlClient

Imports StackTrace = System.Diagnostics.StackTrace

'
' SQLCommandGenerator
'
' Genera un objeto Command basado en el metodo
'
' Ejemplo de utilización:
'
' Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
'    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
'    New Object() {MethodParameter1, MethodParameter2})
'
' Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
'    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
'    New Object() {IIf(PortalId <> -1, PortalId, SqlInt16.Null)})
'

Namespace GOIntranet

    Public NotInheritable Class SqlCommandGenerator

        Private Sub New()
            Throw New NotSupportedException()
        End Sub

        Public Shared ReturnValueParameterName As String = "RETURN_VALUE"
        Public Shared NoValues() As Object

        Public Shared Function GenerateCommand(ByVal Connection As SqlConnection, ByVal Method As MethodInfo, ByVal Values() As Object, Optional ByVal SQLCommandType As CommandType = CommandType.StoredProcedure, Optional ByVal SQLCommandText As String = "") As SqlCommand

            Dim _CommandType As CommandType
            Dim _CommandText As String

            If Method Is Nothing Then
                Method = CType(New StackTrace().GetFrame(1).GetMethod(), MethodInfo)
            End If

            Dim command As New SqlCommand()
            command.Connection = Connection
            command.CommandType = SQLCommandType

            If SQLCommandText.Length = 0 Then
                command.CommandText = Method.Name
            Else
                command.CommandText = SQLCommandText
            End If

            If command.CommandType = CommandType.StoredProcedure Then
                GenerateCommandParameters(command, Method, Values)

                command.Parameters.Add(ReturnValueParameterName, SqlDbType.Int).Direction = ParameterDirection.ReturnValue
            End If

            Return command

        End Function

        Private Shared Sub GenerateCommandParameters(ByVal command As SqlCommand, ByVal method As MethodInfo, ByVal values() As Object)

            Dim methodParameters As ParameterInfo() = method.GetParameters()

            Dim paramIndex As Integer = 0

            Dim paramInfo As ParameterInfo
            For Each paramInfo In methodParameters

                If Not Attribute.IsDefined(paramInfo, GetType(NonCommandParameterAttribute)) Then

                    Dim paramAttribute As SqlParameterAttribute = CType(Attribute.GetCustomAttribute(paramInfo, GetType(SqlParameterAttribute)), SqlParameterAttribute)

                    If paramAttribute Is Nothing Then
                        paramAttribute = New SqlParameterAttribute()
                    End If

                    Dim sqlParameter As New SqlParameter()

                    If paramAttribute.IsNameDefined Then
                        sqlParameter.ParameterName = paramAttribute.Name
                    Else
                        sqlParameter.ParameterName = paramInfo.Name
                    End If
                    If Not sqlParameter.ParameterName.StartsWith("@") Then
                        sqlParameter.ParameterName = "@" + sqlParameter.ParameterName
                    End If
                    If paramAttribute.IsTypeDefined Then
                        sqlParameter.SqlDbType = paramAttribute.SqlDbType
                    End If
                    If paramAttribute.IsSizeDefined Then
                        sqlParameter.Size = paramAttribute.Size
                    End If
                    If paramAttribute.IsScaleDefined Then
                        sqlParameter.Scale = paramAttribute.Scale
                    End If
                    If paramAttribute.IsPrecisionDefined Then
                        sqlParameter.Precision = paramAttribute.Precision
                    End If
                    If paramAttribute.IsDirectionDefined Then
                        sqlParameter.Direction = paramAttribute.Direction
                    Else
                        If paramInfo.ParameterType.IsByRef Then
                            If paramInfo.IsOut Then
                                sqlParameter.Direction = ParameterDirection.Output
                            Else
                                sqlParameter.Direction = ParameterDirection.InputOutput
                            End If
                        Else
                            sqlParameter.Direction = ParameterDirection.Input
                        End If
                    End If

                    sqlParameter.Value = values(paramIndex)
                    command.Parameters.Add(sqlParameter)

                    paramIndex += 1
                End If
            Next paramInfo

        End Sub

    End Class

End Namespace
