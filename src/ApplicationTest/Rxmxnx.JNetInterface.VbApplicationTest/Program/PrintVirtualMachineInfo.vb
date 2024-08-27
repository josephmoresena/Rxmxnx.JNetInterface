Imports Rxmxnx.PInvoke

Partial Module Program
    Private Sub PrintVirtualMachineInfo(env As IEnvironment, vm As IInvokedVirtualMachine,
                                        jvmLib As JVirtualMachineLibrary)
        PrintAttachedThreadInfo(env)
        PrintAttachThreadInfo(vm, "Main thread Re-Attached", env)
        Dim jvmT As Task = Task.Factory.StartNew(AddressOf PrintAttachedThreadInfo, vm, TaskCreationOptions.LongRunning)
        jvmT.Wait()
        Console.WriteLine($"Supported version: 0x{jvmLib.GetLatestSupportedVersion():x8}")
        Dim vms As IVirtualMachine() = jvmLib.GetCreatedVirtualMachines()
        For Each jvm In vms
            Console.WriteLine($"VM: {jvm.Reference} Type: {jvm.GetType()}")
        Next
    End Sub

    Private Sub PrintAttachedThreadInfo(obj As Object)
        If Not TypeOf obj Is IVirtualMachine Then Return
        Dim vm = DirectCast(obj, IVirtualMachine)
        Console.WriteLine($"New Thread {vm.GetEnvironment() Is Nothing}")
        PrintAttachThreadInfo(vm, "New thread 1")
        PrintAttachThreadInfo(vm, "New thread 2")
    End Sub

    Private Sub PrintAttachedThreadInfo(env As IEnvironment)
        Dim vm As IVirtualMachine = env.VirtualMachine
        Console.WriteLine(vm.Reference)
        Console.WriteLine($"VM Version: 0x{env.Version:x8}")
        Console.WriteLine(
            $"Ref Equality: {CType(env, Object).Equals(vm.GetEnvironment())} - Instance Equality: { _
                             ReferenceEquals(env, vm.GetEnvironment())}")
        Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId} {env.Reference} Type: {env.GetType()}")
    End Sub

    Private Sub PrintAttachThreadInfo(vm As IVirtualMachine, threadName As CString,
                                      Optional ByVal env As IEnvironment = Nothing)
        Dim attached As Boolean = vm.GetEnvironment() Is Nothing
        Console.WriteLine(If(env Is Nothing, $"Thread new: {attached}", $"Thread attached: {attached}"))

        Using thread As IThread = vm.InitializeThread(threadName)
            PrintAttachedThreadInfo(thread)
            If env Is Nothing Then
                PrintAttachThreadInfo(vm, CString.Concat(threadName, " Nested"), thread)
            End If
        End Using

        Console.WriteLine($"Thread detached: {vm.GetEnvironment() Is Nothing}")
    End Sub
End Module