﻿namespace Rxmxnx.JNetInterface.Native.Values;

/// <summary>
/// <c>JNIInvokeInterface_</c> struct. Contains all pointers to the functions of the Invocation API.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1144,
                 Justification = CommonConstants.BinaryStructJustification)]
internal readonly partial struct JInvokeInterface : INativeType<JInvokeInterface>
{
	/// <inheritdoc/>
	public static JNativeType Type => JNativeType.JInvokeInterface;

	/// <summary>
	/// Internal reserved entries.
	/// </summary>
#pragma warning disable CS0169
	private readonly ComReserved _reserved;
#pragma warning restore CS0169

	/// <summary>
	/// Pointer to <c>DestroyJavaVM</c> function. Unloads a JVM and reclaims its resources.
	/// </summary>
	internal IntPtr DestroyJavaVmPointer { get; [ExcludeFromCodeCoverage] init; }
	/// <summary>
	/// Pointer to <c>AttachCurrentThread</c> function. Attaches the current thread to a JVM.
	/// </summary>
	internal IntPtr AttachCurrentThreadPointer { get; [ExcludeFromCodeCoverage] init; }
	/// <summary>
	/// Pointer to <c>DetachCurrentThread</c> function. Detaches the current thread from a JVM.
	/// </summary>
	internal IntPtr DetachCurrentThreadPointer { get; [ExcludeFromCodeCoverage] init; }
	/// <summary>
	/// Pointer to <c>GetEnv</c> function. Retrieves the <c>JNIEnv</c> pointer for current thread.
	/// </summary>
	internal IntPtr GetEnvPointer { get; [ExcludeFromCodeCoverage] init; }
	/// <summary>
	/// Pointer to <c>AttachCurrentThreadAsDaemon</c> function. Same as AttachCurrentThread, but the
	/// newly-created <c>java.lang.Thread</c> instance is a daemon.
	/// </summary>
	internal IntPtr AttachCurrentThreadAsDaemonPointer { get; [ExcludeFromCodeCoverage] init; }
}