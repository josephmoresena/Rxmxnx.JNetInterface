module Program

open System
open System.Runtime.InteropServices
open System.Threading.Tasks
open Microsoft.FSharp.Control
open Microsoft.FSharp.Core
open Rxmxnx.JNetInterface
open Rxmxnx.JNetInterface.ApplicationTest
open Rxmxnx.JNetInterface.Io
open Rxmxnx.JNetInterface.Lang
open Rxmxnx.JNetInterface.Lang.Annotation
open Rxmxnx.JNetInterface.Lang.Reflect
open Rxmxnx.JNetInterface.Native
open Rxmxnx.JNetInterface.Native.Access
open Rxmxnx.JNetInterface.Primitives
open Rxmxnx.JNetInterface.Types
open Rxmxnx.JNetInterface.Types.Metadata
open Rxmxnx.PInvoke

let PrintBuiltInMetadata () =
    Console.WriteLine("====== Primitive types ======")
    Console.WriteLine(JPrimitiveTypeMetadata.VoidMetadata)
    Console.WriteLine(IDataType.GetMetadata<JBoolean>())
    Console.WriteLine(IDataType.GetMetadata<JByte>())
    Console.WriteLine(IDataType.GetMetadata<JChar>())
    Console.WriteLine(IDataType.GetMetadata<JShort>())
    Console.WriteLine(IDataType.GetMetadata<JInt>())
    Console.WriteLine(IDataType.GetMetadata<JLong>())
    Console.WriteLine(IDataType.GetMetadata<JFloat>())
    Console.WriteLine(IDataType.GetMetadata<JDouble>())

    Console.WriteLine("====== Reference types ======")
    Console.WriteLine(IDataType.GetMetadata<JLocalObject>())
    Console.WriteLine(IDataType.GetMetadata<JClassObject>())
    Console.WriteLine(IDataType.GetMetadata<JStringObject>())
    Console.WriteLine(IDataType.GetMetadata<JEnumObject>())
    Console.WriteLine(IDataType.GetMetadata<JNumberObject>())
    Console.WriteLine(IDataType.GetMetadata<JThrowableObject>())
    Console.WriteLine(IDataType.GetMetadata<JStackTraceElementObject>())
    Console.WriteLine(IDataType.GetMetadata<JExceptionObject>())
    Console.WriteLine(IDataType.GetMetadata<JRuntimeExceptionObject>())
    Console.WriteLine(IDataType.GetMetadata<JErrorObject>())

    Console.WriteLine("====== Wrapper types ======")
    Console.WriteLine(IDataType.GetMetadata<JVoidObject>())
    Console.WriteLine(IDataType.GetMetadata<JBooleanObject>())
    Console.WriteLine(IDataType.GetMetadata<JByteObject>())
    Console.WriteLine(IDataType.GetMetadata<JDoubleObject>())
    Console.WriteLine(IDataType.GetMetadata<JFloatObject>())
    Console.WriteLine(IDataType.GetMetadata<JIntegerObject>())
    Console.WriteLine(IDataType.GetMetadata<JCharacterObject>())
    Console.WriteLine(IDataType.GetMetadata<JLongObject>())
    Console.WriteLine(IDataType.GetMetadata<JShortObject>())

    Console.WriteLine("====== Array types ======")
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JBoolean>>())
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JByte>>())
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JChar>>())
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JShort>>())
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JInt>>())
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JLong>>())
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JFloat>>())
    Console.WriteLine(IDataType.GetMetadata<JArrayObject<JDouble>>())

    Console.WriteLine("====== Interfaces types ======")
    Console.WriteLine(IDataType.GetMetadata<JCharSequenceObject>())
    Console.WriteLine(IDataType.GetMetadata<JCloneableObject>())
    Console.WriteLine(IDataType.GetMetadata<JComparableObject>())
    Console.WriteLine(IDataType.GetMetadata<JSerializableObject>())
    Console.WriteLine(IDataType.GetMetadata<JAnnotatedElementObject>())
    Console.WriteLine(IDataType.GetMetadata<JGenericDeclarationObject>())
    Console.WriteLine(IDataType.GetMetadata<JTypeObject>())
    Console.WriteLine(IDataType.GetMetadata<JAnnotationObject>())

let PrintNestedArrayMetadata (arrMetadata: JArrayTypeMetadata option, printCurrent: bool) =
    match arrMetadata with
    | Some metadata when printCurrent -> Console.WriteLine(metadata.Signature)
    | _ -> ()

    let mutable current = arrMetadata

    while Option.isSome current do
        Console.WriteLine(current.Value.ElementMetadata.Signature)

        match current.Value.ElementMetadata with
        | :? JArrayTypeMetadata as nested -> current <- nested |> Option.ofObj
        | _ -> current <- None

let PrintArrayMetadata (arrMetadata: JArrayTypeMetadata, dimension: int) =
    Console.WriteLine(arrMetadata.ElementMetadata.Signature)
    let mutable stopMetadata = false
    let mutable currentMetadata = arrMetadata

    let rec nestedArrayLoop i =
        if i < dimension - 1 && not stopMetadata then
            Console.WriteLine(currentMetadata.Signature)
            let arrMet2 = currentMetadata.GetArrayMetadata()

            if not (isNull arrMet2) then
                currentMetadata <- arrMet2
                nestedArrayLoop (i + 1)

    nestedArrayLoop 0

    if not stopMetadata then
        Console.WriteLine(currentMetadata.Signature)

    PrintNestedArrayMetadata(Some currentMetadata, false)

let PrintMetadataInfo () =
    PrintBuiltInMetadata()
    PrintArrayMetadata(JArrayObject<JBoolean>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JByte>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JChar>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JDouble>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JFloat>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JInt>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JLong>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JShort>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JLocalObject>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JClassObject>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JThrowableObject>.Metadata, 5)
    PrintArrayMetadata(JArrayObject<JStringObject>.Metadata, 5)

let PrintAttachedThreadInfo (env: IEnvironment) =
    let vm = env.VirtualMachine
    Console.WriteLine(vm.Reference)
    Console.WriteLine $"VM Version: 0x%x{env.Version}"

    Console.WriteLine
        $"Ref Equality: %b{env.Equals(vm.GetEnvironment())} - Instance Equality: %b{Object.ReferenceEquals(env, vm.GetEnvironment())}"

    Console.WriteLine $"Thread: %d{Environment.CurrentManagedThreadId} {vm.Reference} Type: {vm.GetType()}"

let rec PrintAttachThreadInfo (vm: IVirtualMachine, threadName: CString, env: IEnvironment) =
    let attached = isNull (vm.GetEnvironment())

    if isNull env then
        Console.WriteLine($"Thread attached: %b{attached}")
    else
        Console.WriteLine($"Thread new: %b{attached}")

    use thread = vm.InitializeThread(threadName)
    PrintAttachedThreadInfo(thread)

    if isNull env then
        PrintAttachThreadInfo(vm, CString.Concat(threadName, " Nested"B), thread)

    Console.WriteLine $"Thread detached: %b{isNull (vm.GetEnvironment())}"

let PrintAttachedThreadVmInfo (obj: Object) =
    match obj with
    | :? IVirtualMachine as vm ->
        Console.WriteLine($"New Thread {isNull (vm.GetEnvironment())}")
        PrintAttachThreadInfo(vm, CString "New thread 1\0"B, null)
        PrintAttachThreadInfo(vm, CString "New thread 2\0"B, null)
    | _ -> ()

let PrintException (env: IEnvironment, ex: ThrowableException) =
    Console.WriteLine(ex.WithSafeInvoke(fun t -> t.ToString()))
    env.PendingException <- null

let PrintVirtualMachineInfo (env: IEnvironment, vm: IInvokedVirtualMachine, jvmLib: JVirtualMachineLibrary) =
    PrintAttachedThreadInfo(env)
    PrintAttachThreadInfo(vm, CString "Main thread Re-Attached"B, env)

    Task.Factory
        .StartNew(fun () -> PrintAttachedThreadVmInfo(vm), TaskCreationOptions.LongRunning)
        .Wait()

    Console.WriteLine $"Supported version: 0x%x{jvmLib.GetLatestSupportedVersion()}"
    let vms = jvmLib.GetCreatedVirtualMachines()

    for jvm in vms do
        Console.WriteLine $"VM: {jvm.Reference} Type: {jvm.GetType()}"

let Execute (jvmLib: JVirtualMachineLibrary, classByteCode: byte[], args: string[]) =
    try
        let initArgs = jvmLib.GetDefaultArgument()

        if IVirtualMachine.TypeMetadataToStringEnabled then
            Console.WriteLine(initArgs)

        let vm, env = jvmLib.CreateVirtualMachine(initArgs)
        use v = vm

        try
            if IVirtualMachine.TypeMetadataToStringEnabled then
                PrintVirtualMachineInfo(env, v, jvmLib)

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
            PrintMetadataInfo()

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
