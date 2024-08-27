Imports System.IO

<CodeAnalysis.ExcludeFromCodeCoverage>
public NotInheritable Partial Class JCompiler
    Public Property JdkPath As String
    Public Property CompilerPath As String
    Public Property LibraryPath As String

    Public Async Function CompileHelloJniClassAsync() As Task(Of Byte())
        Dim javaFilePath As String = Path.Combine(Path.GetTempPath(), "HelloDotnet.java")
        Dim classFilePath As String = Path.Combine(Path.GetTempPath(), "HelloDotnet.class")
        Try
            File.Delete(javaFilePath)
            File.Delete(classFilePath)
            Await File.WriteAllTextAsync(javaFilePath, JavaCode)
            Dim _
                info As _
                    New ProcessStartInfo(Path.Combine(Me.JdkPath, Me.CompilerPath), Chr(32) & javaFilePath & Chr(32))
            Dim javac As Process = Process.Start(info)
            Await javac.WaitForExitAsync()
            Return Await File.ReadAllBytesAsync(classFilePath)
        Finally
            File.Delete(javaFilePath)
            File.Delete(classFilePath)
        End Try
    End Function

    Public Function GetLibrary() As JVirtualMachineLibrary
        Dim result as JVirtualMachineLibrary = JVirtualMachineLibrary.LoadLibrary(Path.Combine(Me.JdkPath,
                                                                                               Me.LibraryPath))
        If result is Nothing Then
            Throw New InvalidOperationException("Invalid JVM library.")
        End If
        Return result
    End Function
End Class