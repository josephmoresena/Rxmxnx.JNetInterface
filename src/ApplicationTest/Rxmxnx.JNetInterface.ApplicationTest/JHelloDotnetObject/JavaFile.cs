namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	public const String JavaCode = @"package com.rxmxnx.dotnet.test;

import java.lang.management.ManagementFactory;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.format.DateTimeFormatter;

public class HelloDotnet {
    public static final int COUNT_RANDOM = 14;
    
    public native String getNativeString();
    public native int getNativeInt();
    public native void passNativeString(String value);

    public static native Integer sumArray(int[] value);

    public static void main(String[] args) {
        if (args == null)
            System.out.println(""args: null"");
        else {
            System.out.println(""args: "" + args.length);
            for(String str : args)
                System.out.println(str);    
        }
        HelloDotnet instance = new HelloDotnet();
        LocalDateTime load = LocalDateTime.now();
        String runtime_information = HelloDotnet.getRuntimeInformation(load);
        int[] intArr = HelloDotnet.getIntArray(10);

        System.out.println(""==== getNativeString() ===="");
        System.out.println(instance.getNativeString());

        System.out.println(""==== getNativeInt() ===="");
        System.out.println(instance.getNativeInt());

        System.out.println(""==== passNativeString(String) ===="");
        instance.passNativeString(runtime_information);

        System.out.println(""==== sumArray(int[]) ===="");
        System.out.println(HelloDotnet.sumArray(intArr));
    }

    public static Object getRandomObject(int value) {
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

    private static String getThreadInfo() {
        Thread currentThread = Thread.currentThread();
        String threadName = currentThread.getName();
        long threadId = currentThread.getId();

        return threadName.isEmpty() ? ""Thread ID: "" + threadId : ""Thread Name: "" + threadName + "", Thread ID: "" + threadId;
    }
    private static String getRuntimeInformation(LocalDateTime call) {
        long load_ms = ManagementFactory.getRuntimeMXBean().getStartTime();
        LocalDateTime load = Instant.ofEpochMilli(load_ms)
                .atZone(ZoneId.systemDefault())
                .toLocalDateTime();
        String os = System.getProperty(""os.name"");
        String osArch = System.getProperty(""os.arch"");
        String osVersion = System.getProperty(""os.version"");
        String user = System.getProperty(""user.name"");
        String currentPath = System.getProperty(""user.dir"");
        String jvmVersion = System.getProperty(""java.version"");
        String jvmVendor = System.getProperty(""java.vendor"");
        String runtimeName = ManagementFactory.getRuntimeMXBean().getVmName();
        String runtimeVersion = ManagementFactory.getRuntimeMXBean().getSpecVersion();
        int cores = Runtime.getRuntime().availableProcessors();

        return ""Load: "" + load.format(DateTimeFormatter.ofPattern(""yyyy-MM-dd HH:mm:ss.SSS"")) + ""\n""
                + ""Call: "" + call.format(DateTimeFormatter.ofPattern(""yyyy-MM-dd HH:mm:ss.SSS"")) + ""\n""
                + ""Number of Cores: "" + cores + ""\n""
                + ""OS: "" + os + ""\n""
                + ""OS Arch: "" + osArch + ""\n""
                + ""OS Version: "" + osVersion + ""\n""
                + ""User: "" + user + ""\n""
                + ""Current Path: "" + currentPath + ""\n""
                + ""Process Arch: "" + osArch + ""\n""
                + ""JVM Version: "" + jvmVersion + ""\n""
                + ""JVM Vendor: "" + jvmVendor + ""\n""
                + ""Runtime Name: "" + runtimeName + ""\n""
                + ""Runtime Version: "" + runtimeVersion;
    }
    private static int[] getIntArray(int length) {
        int[] arr = new int[length];
        for (int i = 0; i < length; i++) {
            arr[i] = i;
        }
        return arr;
    }
}";
}