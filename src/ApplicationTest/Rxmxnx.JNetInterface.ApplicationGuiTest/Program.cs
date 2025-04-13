using System.Runtime.InteropServices;

using Rxmxnx.JNetInterface;
using Rxmxnx.JNetInterface.ApplicationTest;
using Rxmxnx.JNetInterface.ApplicationTest.Awt;
using Rxmxnx.JNetInterface.ApplicationTest.Swing;
using Rxmxnx.JNetInterface.Lang;
using Rxmxnx.JNetInterface.Native;
using Rxmxnx.JNetInterface.Native.Access;
using Rxmxnx.JNetInterface.Primitives;
using Rxmxnx.PInvoke;

if (OperatingSystem.IsWindows())
{
	Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
	Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
}
JVirtualMachine.SetMainClass<JFrameObjectSwing>();
JVirtualMachine.SetMainClass<JWindowObject>();
JVirtualMachine.SetMainClass<JComponentObject>();

if (args.Length < 1)
	throw new ArgumentException("Please set JVM library path.");

JVirtualMachineLibrary jvmLib = JVirtualMachineLibrary.LoadLibrary(args[0]) ??
	throw new ArgumentException("Invalid JVM library.");

try
{
	JVirtualMachineInitArg initArgs = jvmLib.GetDefaultArgument();
	if (JVirtualMachine.TraceEnabled)
		initArgs = new(initArgs.Version)
		{
			Options = new("-verbose:jni", "-verbose:class", "-verbose:gc", "-Djava.awt.headless=false",
			              "-Djava.class.path=com.rxmxnx.jnetinterface.jar"),
		};
	else
		initArgs = new(initArgs.Version)
		{
			Options = new("-Djava.awt.headless=false", "-Djava.class.path=com.rxmxnx.jnetinterface.jar"),
		};
	using IInvokedVirtualMachine vm = jvmLib.CreateVirtualMachine(initArgs, out IEnvironment env);

	try
	{
		using (JClassObject graphicsEnv = JClassObject.GetClass(env, "java/awt/GraphicsEnvironment"u8))
		{
			JFunctionDefinition<JBoolean>.Parameterless isHeadlessDef = new("isHeadless"u8);
			Console.WriteLine($"Headless = {isHeadlessDef.StaticInvoke(graphicsEnv)}");
		}
		using JFrameObjectSwing jFrame = JFrameObjectSwing.Create(env, "Hello .NET");
		jFrame.SetSize(400, 400);
		jFrame.SetCloseOperation(JFrameObjectSwing.CloseOperation.Exit);

		IWrapper<Boolean> setVisible = IWrapper.Create(true);
		using JRunnableObject runnable = JNativeCallback.CreateRunnable(env, new ShowFrameState(jFrame, setVisible));

		Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}, {env.Reference}.");
		JSwingUtilitiesObject.InvokeAndWait(runnable);
	}
	catch (ThrowableException ex)
	{
		Console.WriteLine(ex.WithSafeInvoke(t => t.ToString()));
		env.PendingException = default;
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
}
finally
{
	NativeLibrary.Free(jvmLib.Handle);
}

internal sealed class ShowFrameState(JFrameObjectAwt frame, IWrapper<Boolean> setVisible)
	: JNativeCallback.RunnableState
{
	private readonly JWeak _frame = frame.Weak;

	public override void Run()
	{
		using IThread thread = this._frame.VirtualMachine.InitializeThread();
		Console.WriteLine($"Thread: {Environment.CurrentManagedThreadId}, {thread.Reference}.");
		if (this._frame.IsValid(thread))
			this._frame.AsLocal<JFrameObjectAwt>(thread).SetVisible(setVisible.Value);
	}
}