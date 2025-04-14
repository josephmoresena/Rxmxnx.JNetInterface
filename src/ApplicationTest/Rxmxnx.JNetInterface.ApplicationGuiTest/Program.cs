using System.Reflection;
using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.Awt;
using Rxmxnx.JNetInterface.Awt.Event;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.JNetInterface.Swing;
using Rxmxnx.PInvoke;

if (OperatingSystem.IsWindows())
{
	Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
	Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
}

JVirtualMachine.SetMainClass<JFrameObjectSwing>();
JVirtualMachine.SetMainClass<JWindowObject>();
JVirtualMachine.SetMainClass<JComponentObject>();
JVirtualMachine.SetMainClass<JContainerObject>();
JVirtualMachine.SetMainClass<JAbstractButtonObject>();

UIAdapter ui = UIAdapter.Instance;
JVirtualMachineLibrary? jvmLib = default;
Boolean initializeFrame = false;

try
{
	if (args.Length < 1)
		throw new ArgumentException("Please set JVM library path.");

	jvmLib = JVirtualMachineLibrary.LoadLibrary(args[0]) ?? throw new ArgumentException("Invalid JVM library.");

	JVirtualMachineInitArg initArgs = GetInitArgs(jvmLib);
	ui.PrintArgs(initArgs);
	IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);
	try
	{
		InitGui(env, ui);
		initializeFrame = true;
	}
	catch (Exception ex)
	{
		initializeFrame = false;
		if (ex is not ThrowableException tEx)
			ui.ShowError(ex);
		else
			ui.ShowError(tEx.WithSafeInvoke(t => t.ToString()));
		env.PendingException = default;
	}
	finally
	{
		if (!initializeFrame)
			vm.Dispose();
	}
}
catch (Exception ex)
{
	ui.ShowError(ex);
}
finally
{
	if (!initializeFrame && jvmLib is not null)
		NativeLibrary.Free(jvmLib.Handle);
}
return;

static JVirtualMachineInitArg GetInitArgs(JVirtualMachineLibrary virtualMachineLibrary)
{
	JVirtualMachineInitArg virtualMachineInitArg = virtualMachineLibrary.GetDefaultArgument();
	String jarPath = ExtractJar().Replace(" ", @"\ ");
	if (JVirtualMachine.TraceEnabled)
		virtualMachineInitArg = new(virtualMachineInitArg.Version)
		{
			Options = new("-verbose:jni", "-verbose:class", "-verbose:gc", "-Djava.awt.headless=false",
			              $"-Djava.class.path={jarPath}"),
		};
	else
		virtualMachineInitArg = new(virtualMachineInitArg.Version)
		{
			Options = new("-Djava.awt.headless=false", $"-Djava.class.path={jarPath}"),
		};
	return virtualMachineInitArg;
}
static String GetImageName()
{
	if (OperatingSystem.IsWindows())
		return "windows.png";
	if (OperatingSystem.IsLinux())
		return "linux.png";
	if (OperatingSystem.IsFreeBSD())
		return "freebsd.png";
	return !OperatingSystem.IsBrowser() ?
		"macosx.png" :
		throw new PlatformNotSupportedException("Unsupported platform.");
}
static String ExtractJar()
{
	String tempFile = Path.GetTempFileName() + ".jar";
	File.Delete(tempFile);
	File.WriteAllBytes(tempFile, GetImageFromResources("NativeCallbacks.jar"));
	return tempFile;
}
static void InitGui(IEnvironment env, UIAdapter ui)
{
	ValidateHeadless(env);

	using JFrameObjectSwing jFrame = JFrameObjectSwing.Create(env, "Hello .NET");

	using (JImageIconObject jIconImage = JImageIconObject.Create(env, GetImageFromResources(GetImageName()))!)
	using (JLabelObject jLabel = JLabelObject.Create(jIconImage))
	{
		using (JBorderLayoutObject jLayout = JBorderLayoutObject.Create(env))
			jLabel.SetLayout(jLayout);

		using (JButtonObject jButton = JButtonObject.Create(env, "Click me!!!"u8))
		using (JStringObject jString = JStringObject.Create(env, "Center"u8))
		{
			jLabel.Add(jButton, jString);
			using (JActionListenerObject actionListener =
			       JNativeCallback.CreateActionListener(env, new ShowDialogState(jFrame)))
				jButton.AddActionListener(actionListener);
		}

		jFrame.SetContentPane(jLabel);
	}

	jFrame.SetSize(400, 400);
	jFrame.SetCloseOperation(JFrameObjectSwing.CloseOperation.Exit);

	IWrapper<Boolean> setVisible = IWrapper.Create(true);
	using JRunnableObject runnable = JNativeCallback.CreateRunnable(env, new ShowFrameState(jFrame, ui, setVisible));

	ui.PrintThreadInfo(env.Reference);
	JSwingUtilitiesObject.InvokeAndWait(runnable);
}
static void ValidateHeadless(IEnvironment environment)
{
	using JClassObject graphicsEnv = JClassObject.GetClass(environment, "java/awt/GraphicsEnvironment"u8);
	JFunctionDefinition<JBoolean>.Parameterless isHeadlessDef = new("isHeadless"u8);
	if (isHeadlessDef.StaticInvoke(graphicsEnv).Value)
		throw new InvalidOperationException("Java is running in Headless mode.");
}
static ReadOnlySpan<Byte> GetImageFromResources(String fileName)
{
	Assembly assembly = Assembly.GetExecutingAssembly();
	String? resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(fileName));
	if (String.IsNullOrEmpty(resourceName)) throw new PlatformNotSupportedException("Unsupported platform.");
	using MemoryStream memStream = new();
	using Stream resStream = assembly.GetManifestResourceStream(resourceName)!;
	resStream.CopyTo(memStream);
	return memStream.ToArray();
}