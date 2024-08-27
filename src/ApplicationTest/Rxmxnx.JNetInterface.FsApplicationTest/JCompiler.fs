namespace Rxmxnx.JNetInterface.ApplicationTest

open System
open System.IO
open System.Diagnostics
open System.Runtime.InteropServices
open System.Threading.Tasks
open Rxmxnx.JNetInterface

type JCompiler() =
    [<Literal>]
    let JavaCode =
        """package com.rxmxnx.dotnet.test;

import java.lang.management.ManagementFactory;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;

public class HelloDotnet {
    public static final int COUNT = 14;

    private String s_field;
    
    public native String getHelloString();
    public native int getThreadId();
    public native void printRuntimeInformation(String runtime_information);

    private void throwException() throws NullPointerException {
        throw new NullPointerException("Thrown from Java code.");
    }
    
    private native void nativeFieldAccess();
    private native void nativeThrow() throws IllegalArgumentException;

    public static void main(String[] args) {
        if (args == null)
            System.out.println("args: null");
        else {
            System.out.println("args: " + args.length);
            for(String str : args)
                System.out.println(str);    
        }
        HelloDotnet instance = new HelloDotnet();
        LocalDateTime load = LocalDateTime.now();
        String runtime_information = HelloDotnet.getRuntimeInformation(load);
        int[] intArr = HelloDotnet.getIntArray(10);

        instance.s_field = "AbCdEfGhiJkMlNoPqRsTuVwXyZ";

        System.out.println("==== getHelloString() ====");
        System.out.println(instance.getHelloString());

        System.out.println("==== getThreadId() ====");
        System.out.println(instance.getThreadId());

        System.out.println("==== printRuntimeInformation(String) ====");
        instance.printRuntimeInformation(runtime_information);

        System.out.println("==== sumArray(int[]) ====");
        System.out.println(HelloDotnet.sumArray(intArr));

        System.out.println("==== getIntArrayArray(int) ====");
        int[][] i2arr = HelloDotnet.getIntArrayArray(3);
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                 System.out.print(" " + i2arr[i][j]);
            }
            System.out.println();
        }

        System.out.println("==== nativeFieldAccess() ====");
        instance.nativeFieldAccess();
        System.out.println("s_field = " + instance.s_field);

        System.out.println("==== nativeThrow() ====");
        try {
            instance.nativeThrow();
        } catch (Exception e) {
            System.out.println("==== catch(Exception) ====");
            System.out.println(e);
        }

        System.out.println("==== printClass() ====");
        HelloDotnet.printClass();

        System.out.println("==== getVoidClass() ====");
        System.out.println(HelloDotnet.getVoidClass());
        System.out.println("==== getMainClasses() ====");
        Class[] primitiveClasses = HelloDotnet.getPrimitiveClasses();
        for (int i = 0; i < primitiveClasses.length; i++) {
            System.out.println(i + " = " + primitiveClasses[i].getName());
        }
    }

    public static Object getObject(int value) {
        switch (value) {
            case 1:
                return HelloDotnet.getThreadInfo();
            case 2:
                return -1;
            case 3:
                return new Object();
            case 4:
                return new String[10][][];
            case 5:
                return new int[1][][];
            case 6:
                return new char[0][][][][];
            case 7:
                return String.class;
            case 8:
                return new Math[1][][][][]; 
            case 9:
                return new Void[2][]; 
            case 10:
                return int.class;
            case 11:
                return new Process[2][]; 
            case 12:
                return Math.E; 
            case 13:
                return (float)Math.PI; 
            default:
                return null;
        }
    }

    @SuppressWarnings("deprecation")
    private static String getThreadInfo() {
        Thread currentThread = Thread.currentThread();
        String threadName = currentThread.getName();
        long threadId = currentThread.getId();

        return threadName.isEmpty() ? "Thread ID: " + threadId : "Thread Name: " + threadName + ", Thread ID: " + threadId;
    }
    private static String getRuntimeInformation(LocalDateTime call) {
        long load_ms = ManagementFactory.getRuntimeMXBean().getStartTime();
        LocalDateTime load = Instant.ofEpochMilli(load_ms)
                .atZone(ZoneId.systemDefault())
                .toLocalDateTime();
        String os = System.getProperty("os.name");
        String osArch = System.getProperty("os.arch");
        String osVersion = System.getProperty("os.version");
        String user = System.getProperty("user.name");
        String currentPath = System.getProperty("user.dir");
        String jvmVersion = System.getProperty("java.version");
        String jvmVendor = System.getProperty("java.vendor");
        String runtimeName = ManagementFactory.getRuntimeMXBean().getVmName();
        String runtimeVersion = ManagementFactory.getRuntimeMXBean().getSpecVersion();
        int cores = Runtime.getRuntime().availableProcessors();

        return "Load: " + load.format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss.SSS")) + "\n"
                + "Call: " + call.format(DateTimeFormatter.ofPattern("yyyy-MM-dd HH:mm:ss.SSS")) + "\n"
                + "Number of Cores: " + cores + "\n"
                + "OS: " + os + "\n"
                + "OS Arch: " + osArch + "\n"
                + "OS Version: " + osVersion + "\n"
                + "User: " + user + "\n"
                + "Current Path: " + currentPath + "\n"
                + "Process Arch: " + osArch + "\n"
                + "JVM Version: " + jvmVersion + "\n"
                + "JVM Vendor: " + jvmVendor + "\n"
                + "Runtime Name: " + runtimeName + "\n"
                + "Runtime Version: " + runtimeVersion;
    }
    private static int[] getIntArray(int length) {
        int[] arr = new int[length];
        for (int i = 0; i < length; i++) {
            arr[i] = i;
        }
        return arr;
    }
    
    private static native Integer sumArray(int[] value);
    private static native int[][] getIntArrayArray(int length);
    private static native void printClass();
    private static native Class getVoidClass();
    private static native Class[] getPrimitiveClasses();
}"""

    member val JdkPath: string = "" with get, set
    member val CompilerPath: string = "" with get, set
    member val LibraryPath: string = "" with get, set

    member this.CompileHelloJniClassAsync() : Task<byte[]> =
        task {
            let javaFilePath = Path.Combine(Path.GetTempPath(), "HelloDotnet.java")
            let classFilePath = Path.Combine(Path.GetTempPath(), "HelloDotnet.class")

            try
                File.Delete(javaFilePath)
                File.Delete(classFilePath)
                do! File.WriteAllTextAsync(javaFilePath, JavaCode) |> Async.AwaitTask

                let startInfo = ProcessStartInfo(Path.Combine(this.JdkPath, this.CompilerPath))

                startInfo.ArgumentList.Add(javaFilePath)

                let javac = Process.Start(startInfo)
                do! javac.WaitForExitAsync() |> Async.AwaitTask

                return! File.ReadAllBytesAsync(classFilePath) |> Async.AwaitTask
            finally
                File.Delete(javaFilePath)
                File.Delete(classFilePath)
        }

    member this.GetLibrary() =
        let result =
            JVirtualMachineLibrary.LoadLibrary(Path.Combine(this.JdkPath, this.LibraryPath))

        if isNull result then
            raise (InvalidOperationException("Invalid JVM library."))

        result

    static member GetCompilers() : JCompiler[] =
        if RuntimeInformation.IsOSPlatform(OSPlatform.Windows) then
            JCompiler.GetWindowsCompilers()
        elif RuntimeInformation.IsOSPlatform(OSPlatform.OSX) then
            JCompiler.GetMacCompilers()
        elif RuntimeInformation.IsOSPlatform(OSPlatform.Linux) then
            JCompiler.GetLinuxCompilers()
        else
            [||]

    static member private GetWindowsCompilers() =
        let javaPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Java")

        JCompiler.GetCompilers(javaPath, "jdk-*", "javac.exe", "jvm.dll")

    static member private GetMacCompilers() =
        let javaPath = "/Library/Java/JavaVirtualMachines"
        JCompiler.GetCompilers(javaPath, "*.jdk", "javac", "libjvm.dylib")

    static member private GetLinuxCompilers() =
        let javaPath = "/usr/lib/jvm"
        JCompiler.GetCompilers(javaPath, "jdk-*", "javac", "libjvm.so")

    static member private GetCompilers
        (
            javaPath: string,
            jdkPattern: string,
            javacName: string,
            jvmName: string
        ) : JCompiler[] =
        let javaDirectory = DirectoryInfo(javaPath)

        let jdkDirectories =
            if javaDirectory.Exists then
                javaDirectory.GetDirectories(jdkPattern)
            else
                [||]

        if jdkDirectories.Length = 0 then
            [||]
        else
            jdkDirectories
            |> Array.choose (fun jdkDirectory ->
                let javacFile =
                    jdkDirectory.GetFiles(javacName, SearchOption.AllDirectories) |> Array.tryHead

                let jvmFile =
                    jdkDirectory.GetFiles(jvmName, SearchOption.AllDirectories) |> Array.tryHead

                match javacFile, jvmFile with
                | Some javac, Some jvm ->
                    Some(
                        JCompiler(
                            JdkPath = jdkDirectory.FullName,
                            CompilerPath = Path.GetRelativePath(jdkDirectory.FullName, javac.FullName),
                            LibraryPath = Path.GetRelativePath(jdkDirectory.FullName, jvm.FullName)
                        )
                    )
                | _ -> None)
