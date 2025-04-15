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
using Rxmxnx.JNetInterface.Util.Concurrent;
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
JVirtualMachine.SetMainClass<JCountDownLatchObject>();
JVirtualMachine.SetMainClass<JToolkitObject>();
JVirtualMachine.SetMainClass<JAwtEventObject>();

JVirtualMachineLibrary? jvmLib = default;

try
{
	if (args.Length < 1)
		throw new ArgumentException("Please set JVM library path.");

	jvmLib = JVirtualMachineLibrary.LoadLibrary(args[0]) ?? throw new ArgumentException("Invalid JVM library.");

	JVirtualMachineInitArg initArgs = GetInitialArgs(jvmLib);
	UIAdapter.Instance.PrintArgs(initArgs);
	using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);

	try
	{
		using JFrameObjectSwing frame = CreateFrame(env, "Hello .NET");
		using JCountDownLatchObject countDownLatch = GetCountDownAwait(frame);

		using (JLabelObject frameContent = CreateFrameLabel(env))
		{
			using (JButtonObject jButton = JButtonObject.Create(env, "Click me!!!"u8))
			using (JStringObject jString = JStringObject.Create(env, "Center"u8))
			{
				frameContent.Add(jButton, jString);
				using (JActionListenerObject actionListener =
				       JNativeCallback.CreateActionListener(env, new ShowDialogState(frame)))
					jButton.AddActionListener(actionListener);
			}

			frame.SetContentPane(frameContent);
		}

		frame.SetSize(400, 400);
		frame.SetRelativeTo();
		frame.SetCloseOperation(JFrameObjectSwing.CloseOperation.Exit);

		Show(frame);
		countDownLatch.Await();
	}
	catch (Exception ex)
	{
		if (ex is not ThrowableException tEx)
			UIAdapter.Instance.ShowError(ex);
		else
			UIAdapter.Instance.ShowError(tEx.WithSafeInvoke(t => t.ToString()));
		env.PendingException = default;
	}
}
catch (Exception ex)
{
	UIAdapter.Instance.ShowError(ex);
}
finally
{
	if (jvmLib is not null)
		NativeLibrary.Free(jvmLib.Handle);
}
return;

static JVirtualMachineInitArg GetInitialArgs(JVirtualMachineLibrary virtualMachineLibrary)
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
static String ExtractJar()
{
	String tempFile = Path.GetTempFileName() + ".jar";
	File.Delete(tempFile);
	File.WriteAllBytes(tempFile, GetImageBytes("NativeCallbacks.jar"));
	return tempFile;
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
static ReadOnlySpan<Byte> GetImageBytes(String fileName)
{
	Assembly assembly = Assembly.GetExecutingAssembly();
	String? resourceName = assembly.GetManifestResourceNames().FirstOrDefault(n => n.Contains(fileName));
	if (String.IsNullOrEmpty(resourceName)) throw new PlatformNotSupportedException("Unsupported platform.");
	using MemoryStream memStream = new();
	using Stream resStream = assembly.GetManifestResourceStream(resourceName)!;
	resStream.CopyTo(memStream);
	return memStream.ToArray();
}
static void Show(JComponentObject component)
{
	IEnvironment env = component.Environment;
	IWrapper<Boolean> setVisible = IWrapper.Create(true);
	using JRunnableObject runnable = JNativeCallback.CreateRunnable(env, new ShowComponentState(component, setVisible));

	UIAdapter.Instance.PrintThreadInfo(env.Reference);
	JSwingUtilitiesObject.InvokeAndWait(runnable);
}
static JFrameObjectSwing CreateFrame(IEnvironment env, String title)
{
	using JClassObject graphicsEnv = JClassObject.GetClass(env, "java/awt/GraphicsEnvironment"u8);
	JFunctionDefinition<JBoolean>.Parameterless isHeadlessDef = new("isHeadless"u8);
	if (isHeadlessDef.StaticInvoke(graphicsEnv).Value)
		throw new InvalidOperationException("Java is running in Headless mode.");

	return JFrameObjectSwing.Create(env, title);
}
static JCountDownLatchObject GetCountDownAwait(JWindowObject window)
{
	IEnvironment env = window.Environment;
	JCountDownLatchObject result = JCountDownLatchObject.Create(window.Environment, 1);
	using JToolkitObject toolkit = JToolkitObject.GetDefaultToolkit(env);
	using JAwtEventListenerObject listener =
		JNativeCallback.CreateAwtEventListener(env, new CloseCountDownState(window, result));
	toolkit.AddEventListener(listener, EventMask.Window);
	return result;
}
static JLabelObject CreateFrameLabel(IEnvironment env)
{
	String imageName = GetImageName();
	ReadOnlySpan<Byte> imageBytes = GetImageBytes(imageName);
	JLabelObject jLabel;

	using (JImageIconObject jIconImage = JImageIconObject.Create(env, imageBytes)!)
		jLabel = JLabelObject.Create(jIconImage);
	using (JFlowLayoutObject jLayout = JFlowLayoutObject.Create(env))
		jLabel.SetLayout(jLayout);

	return jLabel;
}