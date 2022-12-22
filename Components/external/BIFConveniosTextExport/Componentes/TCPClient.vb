Imports System.Text
Imports System.Net.Sockets

Imports System.Net

'Cliente de comunicacion TCP 
Public Class TCPClient

    ''Funcion de envio de información via TCP
    'Public Shared Function sendData(ByVal ip As String, ByVal port As String, ByVal data As String) As Integer
    '    'Dim socketClient As New System.Net.Sockets.Socket(
    '    Dim tcpClient As New System.Net.Sockets.TcpClient()
    '    tcpClient.Connect(ip, port)
    '    Dim netStream As System.Net.Sockets.NetworkStream = tcpClient.GetStream
    '    Dim message As [Byte]() = System.Text.Encoding.UTF8.GetBytes(data)
    '    netStream.Write(message, 0, message.Length)
    '    netStream.Close()
    '    tcpClient.Close()
    'End Function


    Public Shared Function SendReceive(ByVal ip As String, ByVal port As String, ByVal data As String) As String
        Dim lipa As System.Net.IPHostEntry = System.Net.Dns.GetHostByAddress(ip)   'Dns.Resolve("host.contoso.com")
        Dim lep As New System.Net.IPEndPoint(lipa.AddressList(0), port)

        Dim s As New Socket(lep.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
        'Try
        s.Connect(lep)
        'Catch e As Exception

        'Console.WriteLine(("Exception Thrown: " + e.ToString()))
        'End Try

        Dim msg As Byte() = System.Text.Encoding.UTF8.GetBytes(data)

        ' Blocks until send returns.
        Dim i As Integer = s.Send(msg, 0, msg.Length, SocketFlags.None)

        s.Shutdown(SocketShutdown.Send)


        ' Blocks until read returns.
        Dim bytes(7) As Byte
        Dim len As Integer
        Dim cadena As String
        len = s.Receive(bytes, 2, SocketFlags.[Partial])
        len = s.Receive(bytes, 8, SocketFlags.[Partial])

        While (len > 0)
            cadena = cadena + System.Text.Encoding.UTF8.GetString(bytes, 0, len)
            len = s.Receive(bytes, 8, SocketFlags.None)
        End While

        'len = s.Receive(bytes, s.Available, SocketFlags.None)

        's.Receive(bytes, 0, s.Available, SocketFlags.None)
        ' Console.WriteLine(s.Available)
        ''Displays to the screen.
        'Console.WriteLine(cadena)
        ' s.Shutdown(SocketShutdown.Both)
        s.Close()
        'Console.ReadLine()
        Return cadena
    End Function 'SendReceive4

End Class
