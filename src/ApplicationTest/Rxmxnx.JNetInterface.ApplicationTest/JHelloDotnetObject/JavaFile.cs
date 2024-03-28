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
        switch (value) {
            case 1:
                return ""texto random"";
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
    public static final int COUNT_RANDOM = 14;
}";
}