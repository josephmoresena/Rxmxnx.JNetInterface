[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
module Program

open System
open System.Runtime.InteropServices
open Microsoft.FSharp.Control
open Microsoft.FSharp.Core
open Rxmxnx.JNetInterface
open Rxmxnx.JNetInterface.ApplicationTest
open Rxmxnx.JNetInterface.Native.Access
open Rxmxnx.PInvoke

let PrintException (env: IEnvironment, ex: ThrowableException) =
    Console.WriteLine(ex.WithSafeInvoke(fun t -> t.ToString()))
    env.PendingException <- null

let Execute (jvmLib: JVirtualMachineLibrary, classByteCode: byte[], args: string[]) =
    try
        let initArgs = jvmLib.GetDefaultArgument()

        if IVirtualMachine.TypeMetadataToStringEnabled then
            Console.WriteLine(initArgs)

        let vm, env = jvmLib.CreateVirtualMachine(initArgs)
        use v = vm

        try
            if IVirtualMachine.TypeMetadataToStringEnabled then
                JRuntimeInfo.PrintVirtualMachineInfo(env, v, jvmLib)

            let managedInstance = IManagedCallback.Default(vm)

            use helloJniClass =
                JHelloDotnetObject.LoadClass(env, classByteCode, managedInstance)

            Console.WriteLine("==== Begin psvm ===")
            JMainMethodDefinition.Instance.Invoke(helloJniClass, args)
            Console.WriteLine("==== End psvm ===")
            JHelloDotnetObject.GetObject(helloJniClass)
            helloJniClass.UnregisterNativeCalls()
        with :? ThrowableException as ex ->
            PrintException(env, ex)
    finally
        NativeLibrary.Free(jvmLib.Handle)

let MainAsync () =
    async {
        if IVirtualMachine.TypeMetadataToStringEnabled then
            JRuntimeInfo.PrintMetadataInfo()

        let reflectionDisabled = not ("{typeof<CString>}".Contains(nameof CString))
        let args = Environment.GetCommandLineArgs()

        let compiler =
            if args.Length = 3 then
                Some(JCompiler(JdkPath = args[0], CompilerPath = args[1], LibraryPath = args[2]))
            else
                JCompiler.GetCompilers() |> Seq.tryHead

        if not compiler.IsSome then
            Console.WriteLine("JDK not found.")
        else
            let comp = compiler.Value
            let! helloJniByteCode = comp.CompileHelloJniClassAsync() |> Async.AwaitTask
            let jvmLib = comp.GetLibrary()

            let jMainArgs =
                if reflectionDisabled then
                    [| $"System Path: %s{Environment.SystemDirectory}" |]
                else
                    [| $"System Path: %s{Environment.SystemDirectory}"
                       $"Runtime Name: %s{RuntimeInformation.FrameworkDescription}" |]

            Execute(jvmLib, helloJniByteCode, jMainArgs)
            IManagedCallback.PrintSwitches()
    }

MainAsync() |> Async.RunSynchronously
