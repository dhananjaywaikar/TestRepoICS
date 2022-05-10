Imports System.Configuration
Imports System.Threading
Imports System.ServiceProcess
Imports System.IO

Public Class Service1

    Dim ScheduleSleepTime As Integer = Convert.ToInt32(ConfigurationSettings.AppSettings("ThreadSleepTimeInMin"))
    Dim worker As Thread = Nothing
    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        Try
            Dim start As ThreadStart = New ThreadStart(AddressOf Working)
            worker = New Thread(start)
            worker.Start()
        Catch ex As Exception
            Throw
        End Try


    End Sub
    Public Sub Working()
        While (True)
            Dim path As String = "D:\\sample.txt"
            Using writer As StreamWriter = New StreamWriter(path, True)
                writer.WriteLine(String.Format("Windows Service Called on " & DateTime.Now.ToString("dd /MM/yyyy hh:mm:ss tt")))
                writer.Close()
            End Using
            Thread.Sleep(ScheduleSleepTime * 60 * 1000)
        End While
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        Try
            If (worker IsNot Nothing) And worker.IsAlive Then
                worker.Abort()
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class
