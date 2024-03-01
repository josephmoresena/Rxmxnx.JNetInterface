namespace Rxmxnx.JNetInterface.ApplicationTest;

internal partial class JHelloDotnetObject
{
	public const String JavaCode = @"package com.rxmxnx.dotnet.test;

public class HelloDotnet {

    public static void main(String[] args){
        if (args == null)
            System.out.println(""Args: Null"");
        else {
            System.out.println(""Args: "" + args.length);
            for(String str : args)
                System.out.println(str);    
        }
        
        HelloDotnet instance = new HelloDotnet();
        System.out.println(instance.getNativeString());
        System.out.println(instance.getNativeInt());
        instance.passNativeString(""texto XD"");
    }
    
    public native String getNativeString();
    public native int getNativeInt();
    public native void passNativeString(String value);

    public static Object getRandomObject(int value) {
        if (value == 1)
            return ""texto random"";
        if (value == 2)
            return -1;
        if (value == 3)
            return new Object();
        if (value == 4)
            return new String[10][0][0];
        if (value == 5)
            return new int[1][0][0];
        if (value == 6)
            return new char[0][1][0][0][0];
        if (value == 8)
            return new Math[1][0][0][0][0];
        if (value == 9)
            return new Process[0][0];
        return null;
    }
}";
}