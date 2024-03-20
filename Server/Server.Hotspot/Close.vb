Public Class Close
    Public Shared Sub StopHotspot()
        Dim stopHotspot As New ProcessStartInfo With {
            .FileName = "netsh",
            .Arguments = "wlan stop hostednetwork",
            .WindowStyle = ProcessWindowStyle.Hidden
        }
        Process.Start(stopHotspot)
    End Sub
End Class
