[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
module Program

open System
open System.Diagnostics
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

        let vm, env = jvmLib.CreateVirtualMachine(initArgs)
        use v = vm

        try
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
        let args = Environment.GetCommandLineArgs()

        if args.Length < 2 then
            raise (ArgumentException("Please set JVM library path."))

        let! helloJniByteCode = File.ReadAllBytesAsync("HelloDotnet.class") |> Async.AwaitTask

        let jvmLib =
            match JVirtualMachineLibrary.LoadLibrary(Array.last args) with
            | null -> raise (ArgumentException "Invalid JVM library.")
            | obj -> obj

        let jMainArgs =
            if AotInfo.IsReflectionDisabled then
                [| $"System Path: %s{Environment.SystemDirectory}" |]
            else
                [| $"System Path: %s{Environment.SystemDirectory}"
                   $"Runtime Name: %s{RuntimeInformation.FrameworkDescription}" |]

        let mutable listener: ConsoleTraceListener option = None

        if JVirtualMachine.TraceEnabled then
            let newListener = new ConsoleTraceListener()
            let _ = Trace.Listeners.Add(newListener)
            listener <- Some newListener

        try
            Execute(jvmLib, helloJniByteCode, jMainArgs)
        finally
            match listener with
            | Some l ->
                Trace.Listeners.Remove(l)
                l.Dispose()
            | None -> ()

        IManagedCallback.PrintSwitches()
    }

MainAsync() |> Async.RunSynchronously
