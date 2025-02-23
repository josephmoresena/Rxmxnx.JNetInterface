Imports System.IO
Imports System.Runtime.InteropServices
Imports Rxmxnx.JNetInterface.Lang
Imports Rxmxnx.JNetInterface.Native
Imports Rxmxnx.JNetInterface.Native.Access
Imports Rxmxnx.PInvoke

Partial Module Program
    Sub Main(args As String())
        MainAsync(args).Wait()
    End Sub

    Private Async Function MainAsync(args As String()) As Task
        If args.Length < 1 Then
            Throw New ArgumentException("Please set JVM library path.")
        End If

        Dim listener As ConsoleTraceListener = Nothing
        If JVirtualMachine.TraceEnabled Then
            listener = New ConsoleTraceListener()
            Trace.Listeners.Add(listener)
        End If

        Dim helloJniByteCode As Byte() = Await File.ReadAllBytesAsync("HelloDotnet.class")
        Dim jvmLib As JVirtualMachineLibrary = JVirtualMachineLibrary.LoadLibrary(args(0))

        If jvmLib is Nothing Then
            Throw New ArgumentException("Please set JVM library path.")
        End If

        Dim jMainArgs As String() = If(AotInfo.IsReflectionDisabled,
                                       {$"System Path: {Environment.SystemDirectory}"},
                                       { _
                                           $"System Path: {Environment.SystemDirectory}",
                                           $"Runtime Name: {RuntimeInformation.FrameworkDescription}"
                                       })

        Try
            Execute(jvmLib, helloJniByteCode, jMainArgs)
        Finally
            IF Not IsNothing(listener) Then
                Trace.Listeners.Remove(listener)
                listener.Dispose()
            End If
        End Try

        IManagedCallback.PrintSwitches()
    End Function

    Private ReadOnly VmOptions As String() = {"-DjniLib.load.disable=true"}

    Private Sub Execute(jvmLib As JVirtualMachineLibrary, classByteCode As Byte(), ByVal ParamArray args As String())

        Try
            Dim initArgs As JVirtualMachineInitArg = jvmLib.GetDefaultArgument()

            initArgs = New JVirtualMachineInitArg(initArgs.Version) With  { 
                .Options = New CStringSequence(VmOptions)}

            Dim env as IEnvironment = Nothing
            Using vm As IInvokedVirtualMachine = jvmLib.CreateVirtualMachine(initArgs, env)
                Try
                    Dim managedInstance As New IManagedCallback.Default(vm, Console.Out)
                    Using _
                        helloJniClass As JClassObject =
                            JHelloDotnetObject.LoadClass(env, classByteCode, managedInstance)
                        Console.WriteLine("==== Begin psvm ===")
                        JMainMethodDefinition.Instance.Invoke(helloJniClass, args)
                        Console.WriteLine("==== End psvm ===")
                        JHelloDotnetObject.GetObject(helloJniClass)
                        helloJniClass.UnregisterNativeCalls()
                    End Using
                Catch ex As ThrowableException
                    Console.WriteLine(ex.WithSafeInvoke(Function(t) t.ToString()))
                    env.PendingException = Nothing
                End Try
            End Using
        Finally
            NativeLibrary.Free(jvmLib.Handle)
        End Try
    End Sub
End Module
