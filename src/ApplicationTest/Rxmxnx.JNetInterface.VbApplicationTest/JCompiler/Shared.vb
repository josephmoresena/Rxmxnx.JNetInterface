Imports System.IO
Imports System.Runtime.InteropServices

public Partial Class JCompiler
    Public Shared Function GetCompilers() As JCompiler()
        If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then
            Return GetWindowsCompilers()
        End If
        If RuntimeInformation.IsOSPlatform(OSPlatform.OSX) Then
            Return GetMacCompilers()
        End If
        Return _
            If _
                (RuntimeInformation.IsOSPlatform(OSPlatform.Linux), GetLinuxCompilers(),
                 Array.Empty (Of JCompiler)())
    End Function

    Private Shared Function GetWindowsCompilers() As JCompiler()
        Dim javaPath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Java")
        Return GetCompilers(javaPath, "jdk-*", "javac.exe", "jvm.dll")
    End Function

    Private Shared Function GetMacCompilers() As JCompiler()
        Dim javaPath = "/Library/Java/JavaVirtualMachines"
        Return GetCompilers(javaPath, "*.jdk", "javac", "libjvm.dylib")
    End Function

    Private Shared Function GetLinuxCompilers() As JCompiler()
        Dim javaPath = "/usr/lib/jvm"
        Return GetCompilers(javaPath, "jdk-*", "javac", "libjvm.so")
    End Function

    Private Shared Function GetCompilers(javaPath As String, jdkPattern As String, javacName As String,
                                         jvmName As String) As JCompiler()
        Dim javaDirectory = New DirectoryInfo(javaPath)
        Dim jdkDirectories =
                If(javaDirectory.Exists, javaDirectory.GetDirectories(jdkPattern), Array.Empty (Of DirectoryInfo))
        If jdkDirectories.Length = 0 Then
            Return Array.Empty (Of JCompiler)
        End If
        Dim result = new List(of JCompiler)(jdkDirectories.Length)
        result.AddRange(
            From jdkDirectory In jdkDirectories
                           Let _
                           javacFile = jdkDirectory.GetFiles(javacName, SearchOption.AllDirectories).FirstOrDefault()
                           Let jvmFile = jdkDirectory.GetFiles(jvmName, SearchOption.AllDirectories).FirstOrDefault()
                           Where Not javacFile Is Nothing And Not jvmFile Is Nothing Select new JCompiler() With {
                           .JdkPath = jdkDirectory.FullName,
                           .CompilerPath = Path.GetRelativePath(jdkDirectory.FullName, javacFile.FullName),
                           .LibraryPath = Path.GetRelativePath(jdkDirectory.FullName, jvmFile.FullName)})
        Return result.ToArray()
    End Function
End Class