// ReSharper disable MemberCanBePrivate.Global

namespace Rxmxnx.JNetInterface;

/// <summary>
/// This class exposes the JNI hosted by Android-OS
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
#endif
public sealed partial class AndroidJniHost
{
	/// <summary>
	/// Indicates whether trace output is enabled.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean TraceEnabled => JTrace.TraceEnabled;
	/// <summary>
	/// Indicates whether final user-types should be treated as real classes at runtime.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean FinalUserTypeRuntimeEnabled => MetadataHelper.FinalUserTypeRuntimeEnabled;
	/// <summary>
	/// Main classes' information.
	/// </summary>
	public static IEnumerable<ITypeInformation> MainClassesInformation => AndroidJniHost.userMainClasses.Values;
	/// <summary>
	/// Android API level.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Int32 ApiLevel
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			Debug.Assert(AndroidHelper.ApiLevel.HasValue);
			return AndroidHelper.ApiLevel.Value;
		}
	}

	/// <summary>
	/// Registers <typeparamref name="TReference"/> as valid datatype for the current process.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TDataType}"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current datatype was registered; otherwise, <see langword="false"/>.
	/// </returns>
#if !NET8_0_OR_GREATER || ANDROID
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public static Boolean Register<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
		=> MetadataHelper.IsCompileCompliant<TReference>() && MetadataHelper.Register<TReference>();
	/// <summary>
	/// Sets <typeparamref name="TReference"/> as main class.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
#if !NET8_0_OR_GREATER || ANDROID
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetMainClass<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
		=> MainClasses.SetMainClass<TReference>(AndroidJniHost.userMainClasses);

	/// <summary>
	/// Initializes a builder for inline (currenTt thread) JNI context call.
	/// </summary>
	/// <returns>A <see cref="SyncContextBuilder"/> instance.</returns>
	public static SyncContextBuilder CreateSyncContext() => new();
	/// <summary>
	/// Initializes a builder for inline (current thread) JNI context call.
	/// </summary>
	/// <param name="threadName">Thread name.</param>
	/// <param name="threadGroup">JNI thread group.</param>
	/// <returns>A <see cref="SyncContextBuilder"/> instance.</returns>
	public static AsyncContextBuilder CreateAsyncContext(CString? threadName = default,
		JGlobalBase? threadGroup = default)
		=> new(threadName, threadGroup);
}