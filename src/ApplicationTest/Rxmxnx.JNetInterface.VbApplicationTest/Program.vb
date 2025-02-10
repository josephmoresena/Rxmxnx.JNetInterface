Imports System.Diagnostics.CodeAnalysis
Imports System.IO
Imports System.Runtime.InteropServices
Imports Rxmxnx.JNetInterface.Lang
Imports Rxmxnx.JNetInterface.Native
Imports Rxmxnx.JNetInterface.Native.Access
Imports Rxmxnx.PInvoke

<ExcludeFromCodeCoverage>
Partial Module Program
    Sub Main(args As String())
        MainAsync(args).Wait()
    End Sub

    Private Async Function MainAsync(args As String()) As Task
        If IVirtualMachine.TypeMetadataToStringEnabled Then
            JRuntimeInfo.PrintMetadataInfo()
        End If

        If args.Length < 1 Then
            Throw New ArgumentException("Please set JVM library path.")
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

        Execute(jvmLib, helloJniByteCode, jMainArgs)

        IManagedCallback.PrintSwitches()
    End Function

    Private ReadOnly VmOptions As String() = {"-Dno-native-load=true"}

    Private Sub Execute(jvmLib As JVirtualMachineLibrary, classByteCode As Byte(), ByVal ParamArray args As String())

        Try
            Dim initArgs As JVirtualMachineInitArg = jvmLib.GetDefaultArgument()
            If IVirtualMachine.TypeMetadataToStringEnabled Then
                Console.WriteLine(initArgs)
            End If
            initArgs = New JVirtualMachineInitArg(initArgs.Version) With  { 
                .Options = New CStringSequence(VmOptions)}

            Dim env as IEnvironment = Nothing
            Using vm As IInvokedVirtualMachine = jvmLib.CreateVirtualMachine(initArgs, env)
                Try
                    If IVirtualMachine.TypeMetadataToStringEnabled Then
                        JRuntimeInfo.PrintVirtualMachineInfo(env, vm, jvmLib)
                    End If

                    Dim managedInstance As New IManagedCallback.Default(vm)
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
