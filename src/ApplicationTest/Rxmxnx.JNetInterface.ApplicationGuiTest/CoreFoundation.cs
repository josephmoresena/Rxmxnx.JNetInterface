using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Rxmxnx.JNetInterface.ApplicationTest;

internal static unsafe partial class CoreFoundation
{
	[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation",
	               EntryPoint = "CFRunLoopGetCurrent")]
	public static partial IntPtr RunLoopGetCurrent();
	public static void InitializeLoop(IntPtr loopRef)
	{
		RunLoopSourceContext context = new()
		{
			Version = 0,
			Info = IntPtr.Zero,
			Retain = IntPtr.Zero,
			Release = IntPtr.Zero,
			CopyDescription = IntPtr.Zero,
			Equal = IntPtr.Zero,
			Hash = IntPtr.Zero,
			Schedule = IntPtr.Zero,
			Cancel = IntPtr.Zero,
			Perform = &CoreFoundation.DummyCallback,
		};

		IntPtr sourceRef = CoreFoundation.RunLoopSourceCreate(IntPtr.Zero, 0, ref context);
		CoreFoundation.RunLoopAddSource(loopRef, sourceRef, CoreFoundation.GetCommonModes());
		CoreFoundation.RunLoopRun();
		CoreFoundation.ReleaseSource(sourceRef);
	}

	[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation",
	               EntryPoint = "CFRunLoopSourceCreate")]
	private static partial IntPtr RunLoopSourceCreate(IntPtr allocator, IntPtr order, ref RunLoopSourceContext context);
	[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation",
	               EntryPoint = "CFRunLoopAddSource")]
	private static partial void RunLoopAddSource(IntPtr runLoop, IntPtr source, IntPtr mode);
	[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", EntryPoint = "CFRunLoopRun")]
	private static partial void RunLoopRun();
	[LibraryImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", EntryPoint = "CFRelease")]
	private static partial void ReleaseSource(IntPtr sourceRef);

	private static IntPtr GetCommonModes()
	{
		IntPtr handle = NativeLibrary.Load("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation");
		NativeLibrary.TryGetExport(handle, "kCFRunLoopCommonModes", out IntPtr symbol);
		return Unsafe.AsRef<IntPtr>(symbol.ToPointer());
	}

	[UnmanagedCallersOnly]
	private static void DummyCallback(IntPtr _) { }

	[StructLayout(LayoutKind.Sequential)]
	private struct RunLoopSourceContext
	{
		public IntPtr Version;
		public IntPtr Info;
		public IntPtr Retain;
		public IntPtr Release;
		public IntPtr CopyDescription;
		public IntPtr Equal;
		public IntPtr Hash;
		public IntPtr Schedule;
		public IntPtr Cancel;
		public delegate* unmanaged<IntPtr, void> Perform;
	}
}