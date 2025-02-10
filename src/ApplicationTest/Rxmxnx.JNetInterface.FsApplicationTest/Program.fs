[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
module Program

open System
open System.IO
open System.Runtime.InteropServices
open Microsoft.FSharp.Control
open Microsoft.FSharp.Core
open Rxmxnx.JNetInterface
open Rxmxnx.JNetInterface.ApplicationTest
open Rxmxnx.JNetInterface.Native
open Rxmxnx.JNetInterface.Native.Access
open Rxmxnx.PInvoke

let PrintException (env: IEnvironment, ex: ThrowableException) =
    Console.WriteLine(ex.WithSafeInvoke(fun t -> t.ToString()))
    env.PendingException <- null

let Execute (jvmLib: JVirtualMachineLibrary, classByteCode: byte[], args: string[]) =
    try
        let mutable initArgs = jvmLib.GetDefaultArgument()

        initArgs <- JVirtualMachineInitArg(initArgs.Version, Options = CStringSequence [ "-DjniLib.load.disable=true" ])

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

        let args = Environment.GetCommandLineArgs()

        if args.Length < 1 then
            raise (ArgumentException("Please set JVM library path."))

        let! helloJniByteCode = File.ReadAllBytesAsync("HelloDotnet.class") |> Async.AwaitTask
        let jvmLib = JVirtualMachineLibrary.LoadLibrary(args[0])

        if jvmLib = null then
            raise (ArgumentException("Invalid JVM library."))

        let jMainArgs =
            if AotInfo.IsReflectionDisabled then
                [| $"System Path: %s{Environment.SystemDirectory}" |]
            else
                [| $"System Path: %s{Environment.SystemDirectory}"
                   $"Runtime Name: %s{RuntimeInformation.FrameworkDescription}" |]

        Execute(jvmLib, helloJniByteCode, jMainArgs)
        IManagedCallback.PrintSwitches()
    }

MainAsync() |> Async.RunSynchronously
