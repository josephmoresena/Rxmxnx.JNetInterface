Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.InteropServices
Imports Rxmxnx.JNetInterface.Lang
Imports Rxmxnx.JNetInterface.Native
Imports Rxmxnx.JNetInterface.Native.Access

<ExcludeFromCodeCoverage>
Partial Module Program
    Sub Main(args As String())
        MainAsync(args).Wait()
    End Sub

    Private Async Function MainAsync(args As String()) As Task
        If IVirtualMachine.TypeMetadataToStringEnabled Then
            PrintMetadataInfo()
        End If

        Dim reflectionDisabled As Boolean = Not $"{GetType(Program)}".Contains(NameOf(Program))

        Dim compiler As JCompiler = If(args.Length = 3,
                                       New JCompiler With {
                                          .JdkPath = args(0),
                                          .CompilerPath = args(1),
                                          .LibraryPath = args(2)
                                          },
                                       JCompiler.GetCompilers().FirstOrDefault()
                                       )

        If compiler Is Nothing Then
            Console.WriteLine("JDK not found.")
            Return
        End If

        Dim helloJniByteCode As Byte() = Await compiler.CompileHelloJniClassAsync()
        Dim jvmLib As JVirtualMachineLibrary = compiler.GetLibrary()

        Dim jMainArgs As String() = If(reflectionDisabled,
                                       {$"System Path: {Environment.SystemDirectory}"},
                                       { _
                                           $"System Path: {Environment.SystemDirectory}",
                                           $"Runtime Name: {RuntimeInformation.FrameworkDescription}"
                                       })

        Execute(jvmLib, helloJniByteCode, jMainArgs)

        IManagedCallback.PrintSwitches()
    End Function

    Private Sub Execute(jvmLib As JVirtualMachineLibrary, classByteCode As Byte(), ByVal ParamArray args As String())
        Try
            Dim initArgs As JVirtualMachineInitArg = jvmLib.GetDefaultArgument()
            If IVirtualMachine.TypeMetadataToStringEnabled Then
                Console.WriteLine(initArgs)
            End If
            Dim env as IEnvironment = Nothing
            Using vm As IInvokedVirtualMachine = jvmLib.CreateVirtualMachine(initArgs, env)
                Try
                    If IVirtualMachine.TypeMetadataToStringEnabled Then
                        PrintVirtualMachineInfo(env, vm, jvmLib)
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
