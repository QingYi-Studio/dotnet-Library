Public Class Open
    Public Shared Sub StartHotspot(ssid As String, key As String)
        Dim startHotspot As New ProcessStartInfo With {
            .FileName = "netsh",
            .Arguments = $"wlan set hostednetwork mode=allow ssid={ssid} key={key}",
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        Process.Start(startHotspot)
    End Sub
End Class
