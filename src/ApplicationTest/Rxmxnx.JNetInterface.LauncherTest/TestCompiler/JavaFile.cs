namespace Rxmxnx.JNetInterface.ApplicationTest;

public partial class TestCompiler
{
	private const String JavaCode = @"package com.rxmxnx.dotnet.test;

import java.io.File;
import java.lang.management.ManagementFactory;
import java.text.SimpleDateFormat;
import java.util.Date;

public class HelloDotnet {
    public static final int COUNT = 20;

    static {
        if (!Boolean.parseBoolean(System.getProperty(""jniLib.load.disable"")))
            try {
                System.loadLibrary(HelloDotnet.getLibraryName());
            }
            catch(Exception ex) {
                ex.printStackTrace(System.out);
            }
    }

    private String s_field;
    
    public native String getHelloString();
    public native int getThreadId();
    public native void printRuntimeInformation(String runtime_information);

    private void throwException() throws NullPointerException {
        throw new NullPointerException(""Thrown from Java code."");
    }
    
    private native void nativeFieldAccess();
    private native void nativeThrow() throws IllegalArgumentException;

    public static void main(String[] args) {
        if (args == null)
            System.out.println(""args: null"");
        else {
            System.out.println(""args: "" + args.length);
            for(String str : args)
                System.out.println(str);    
        }
        HelloDotnet instance = new HelloDotnet();
        Date load = new Date();
        String runtime_information = HelloDotnet.getRuntimeInformation(load);
        int[] intArr = HelloDotnet.getIntArray(10);

        instance.s_field = ""AbCdEfGhiJkMlNoPqRsTuVwXyZ"";

        System.out.println(""==== getHelloString() ===="");
        System.out.println(instance.getHelloString());

        System.out.println(""==== getThreadId() ===="");
        System.out.println(instance.getThreadId());

        System.out.println(""==== printRuntimeInformation(String) ===="");
        instance.printRuntimeInformation(runtime_information);

        System.out.println(""==== sumArray(int[]) ===="");
        System.out.println(HelloDotnet.sumArray(intArr));

        System.out.println(""==== getIntArrayArray(int) ===="");
        int[][] i2arr = HelloDotnet.getIntArrayArray(3);
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                 System.out.print("" "" + i2arr[i][j]);
            }
            System.out.println();
        }

        System.out.println(""==== nativeFieldAccess() ===="");
        instance.nativeFieldAccess();
        System.out.println(""s_field = "" + instance.s_field);

        System.out.println(""==== nativeThrow() ===="");
        try {
            instance.nativeThrow();
        } catch (Exception e) {
            System.out.println(""==== catch(Exception) ===="");
            System.out.println(e);
        }

        System.out.println(""==== printClass() ===="");
        HelloDotnet.printClass();

        System.out.println(""==== getVoidClass() ===="");
        System.out.println(HelloDotnet.getVoidClass());
        System.out.println(""==== getMainClasses() ===="");
        Class[] primitiveClasses = HelloDotnet.getPrimitiveClasses();
        for (int i = 0; i < primitiveClasses.length; i++) {
            System.out.println(i + "" = "" + primitiveClasses[i].getName());
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
            case 14:
                return (long)1000; 
            case 15:
                return 'X'; 
            case 16:
                return false; 
            case 17:
                return (short)1033; 
            case 18:
                return (byte)120; 
            case 19:
                return true; 
            default:
                return null;
        }
    }

    @SuppressWarnings(""deprecation"")
    private static String getThreadInfo() {
        Thread currentThread = Thread.currentThread();
        String threadName = currentThread.getName();
        long threadId = currentThread.getId();

        return threadName.isEmpty() ? ""Thread ID: "" + threadId : ""Thread Name: "" + threadName + "", Thread ID: "" + threadId;
    }
    private static String getRuntimeInformation(Date call) {
        long load_ms = ManagementFactory.getRuntimeMXBean().getStartTime();
        Date load = new Date(load_ms);
        SimpleDateFormat dateFormat = new SimpleDateFormat(""yyyy-MM-dd HH:mm:ss.SSS"");
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

        return ""Load: "" + dateFormat.format(load) + ""\n""
                + ""Call: "" + dateFormat.format(call) + ""\n""
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
    
    private static native Integer sumArray(int[] value);
    private static native int[][] getIntArrayArray(int length);
    private static native void printClass();
    private static native Class getVoidClass();
    private static native Class[] getPrimitiveClasses();

    private static String getLibraryName() {
        String libraryName = ""HelloJni"";
        String currentPath = new File(""."").getAbsolutePath();
        String libraryPath = System.getProperty(""java.library.path"");
        String dotnetVersion = System.getProperty(""dotnet.runtime.version"");
        String arch = System.getProperty(""os.arch"");
        
        currentPath = currentPath.substring(0, currentPath.length() - 2);
        
        if (!libraryPath.equals(currentPath) && !libraryPath.contains(currentPath + File.pathSeparator) && !libraryPath.contains(File.pathSeparator + currentPath))
            System.setProperty(""java.library.path"", currentPath + File.pathSeparator + libraryPath);
        
        if (arch.equals(""amd64"") || arch.equals(""x86_64""))
            libraryName += "".x64"";
        else if (arch.equals(""aarch64"") || arch.equals(""arm64""))
            libraryName += "".arm64"";
        else if (arch.equals(""x86"") || arch.equals(""i386"") || arch.equals(""i686""))
            libraryName += "".x86"";
        else if (arch.equals(""arm""))
            libraryName += "".arm"";
        
        if (dotnetVersion != null && !dotnetVersion.equals("""")) 
            libraryName += '.' + dotnetVersion;
        
        if (Boolean.parseBoolean(System.getProperty(""dotnet.reflection.disable"")))
            libraryName += "".RFM"";
        
        return libraryName;
    }
}";
	private const String JarManifest = @"Main-Class: com.rxmxnx.dotnet.test.HelloDotnet
";
	private const String JniConfig = @"[
  {
    ""name"": ""java.lang.Void"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ]
  },
  {
    ""name"": ""java.lang.Boolean"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""methods"": [
      {
        ""name"": ""booleanValue"",
        ""parameterTypes"": []
      }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""boolean""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Byte"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""byte""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Character"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""methods"": [
      {
        ""name"": ""charValue"",
        ""parameterTypes"": []
      }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""char""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Double"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""double""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Float"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""float""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Integer"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""int""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Long"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""long""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Short"",
    ""fields"": [
      { ""name"": ""TYPE"" }
    ],
    ""constructors"": [
      {
        ""parameterTypes"": [
          ""short""
        ]
      }
    ]
  },
  {
    ""name"": ""java.lang.Number"",
    ""methods"": [
      {
        ""name"": ""byteValue"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""doubleValue"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""shortValue"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""intValue"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""longValue"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""floatValue"",
        ""parameterTypes"": []
      }
    ]
  },
  {
    ""name"": ""[Z""
  },
  {
    ""name"": ""[B""
  },
  {
    ""name"": ""[C""
  },
  {
    ""name"": ""[D""
  },
  {
    ""name"": ""[F""
  },
  {
    ""name"": ""[I""
  },
  {
    ""name"": ""[J""
  },
  {
    ""name"": ""[S""
  },
  {
    ""name"": ""java.lang.Class"",
    ""methods"": [
      {
        ""name"": ""getName"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""isPrimitive"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""getModifiers"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""getInterfaces"",
        ""parameterTypes"": []
      }
    ]
  },
  {
    ""name"": ""java.lang.Throwable"",
    ""methods"": [
      {
        ""name"": ""getMessage"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""getStackTrace"",
        ""parameterTypes"": []
      }
    ]
  },
  {
    ""name"": ""java.lang.StackTraceElement"",
    ""methods"": [
      {
        ""name"": ""getClassName"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""getLineNumber"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""getFileName"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""getMethodName"",
        ""parameterTypes"": []
      },
      {
        ""name"": ""isNativeMethod"",
        ""parameterTypes"": []
      }
    ]
  },
  
  {
    ""name"": ""java.lang.IllegalArgumentException""
  },
  {
    ""name"": ""java.lang.Math""
  },
  {
    ""name"": ""java.lang.Process""
  },
  {
    ""name"": ""[[I""
  },
  {
    ""name"": ""com.rxmxnx.dotnet.test.HelloDotnet"",
    ""fields"": [
      { ""name"": ""s_field"" },
      { ""name"": ""COUNT"" }
    ],
    ""methods"": [
      {
        ""name"": ""getObject"",
        ""parameterTypes"": [ ""int"" ]
      },
      {
        ""name"": ""throwException"",
        ""parameterTypes"": []
      }
    ]
  }
]";
}