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
}";
}