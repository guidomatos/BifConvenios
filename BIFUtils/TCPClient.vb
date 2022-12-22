Imports System.Text
Imports System.Net.Sockets
Imports System.Net

Public Class TCPClient

    Public Shared Function SendReceive(ByVal pIP As String, ByVal pPort As String, ByVal pData As String) As String
        Dim lipa As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(pIP)
        Dim lep As New System.Net.IPEndPoint(lipa.AddressList(0), pPort)

        Dim s As New Socket(lep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)

        s.Connect(lep)

        Dim msg As Byte() = System.Text.Encoding.UTF8.GetBytes(pData)

        ' Blocks until send returns.
        Dim i As Integer = s.Send(msg, 0, msg.Length, SocketFlags.None)

        s.Shutdown(SocketShutdown.Send)

        ' Blocks until read returns.
        Dim bytes(7) As Byte
        Dim len As Integer
        Dim cadena As String = ""
        len = s.Receive(bytes, 2, SocketFlags.[Partial])
        len = s.Receive(bytes, 8, SocketFlags.[Partial])

        While (len > 0)
            cadena = cadena + System.Text.Encoding.UTF8.GetString(bytes, 0, len)
            len = s.Receive(bytes, 8, SocketFlags.None)
        End While

        s.Close()

        Return cadena
    End Function

End Class
