namespace Rxmxnx.JNetInterface;

/// <summary>
/// Android JNI extensions.
/// </summary>
public static class AndroidJniExtensions
{
	/// <summary>
	/// The <see cref="DynamicallyAccessedMemberTypes"/> required for <see cref="IJavaPeerable"/> instantiation.
	/// </summary>
	public const DynamicallyAccessedMemberTypes JavaObjectMembers = DynamicallyAccessedMemberTypes.PublicConstructors |
		DynamicallyAccessedMemberTypes.NonPublicConstructors | DynamicallyAccessedMemberTypes.Interfaces |
		DynamicallyAccessedMemberTypes.PublicParameterlessConstructor;

	/// <summary>
	/// Indicates whether <paramref name="jGlobal"/> is valid.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the <paramref name="jGlobal"/> is still valid; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean IsValid([NotNullWhen(true)] this JGlobalBase? jGlobal)
	{
		if (jGlobal is null) return false;
		IVirtualMachineHost host = AndroidJniHost.GetAndroidHost();
		INativeThread env = host.GetNativeThread();
		return jGlobal.IsValid(env);
	}
	/// <summary>
	/// Creates a JNI (java.interop) object from a <see langword="JReferenceObject"/> object.
	/// </summary>
	/// <typeparam name="TResult">A <see cref="IJavaPeerable"/> type.</typeparam>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <see cref="IJavaPeerable"/> instance.</returns>
	public static TResult?
		ToJniObject<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
			this JGlobalBase? jGlobal)
		where TResult : class, IJavaPeerable
	{
		if (!jGlobal.IsValid()) return default;
		JniObjectReferenceType referenceType =
			jGlobal is JGlobal ? JniObjectReferenceType.Global : JniObjectReferenceType.WeakGlobal;
		JniObjectReference jniReference = new(jGlobal.As<JObjectLocalRef>().Pointer, referenceType);
		return JniRuntime.CurrentRuntime.ValueManager.GetValue<TResult>(
			ref jniReference, JniObjectReferenceOptions.Copy);
	}
	/// <summary>
	/// Creates a JNI (java.interop) object from a <see langword="JReferenceObject"/> object.
	/// </summary>
	/// <param name="jObject"></param>
	/// <typeparam name="TResult"></typeparam>
	/// <returns></returns>
	internal static TResult?
		ToJniObject<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
			this JReferenceObject? jObject) where TResult : class, IJavaPeerable
	{
		if (JObject.IsNullOrDefault(jObject)) return default;
		JniObjectReference jniReference = new(jObject.As<JObjectLocalRef>().Pointer, JniObjectReferenceType.Local);
		return JniRuntime.CurrentRuntime.ValueManager.GetValue<TResult>(
			ref jniReference, JniObjectReferenceOptions.Copy);
	}
	/// <summary>
	/// Creates a <see cref="INativeThread"/> instance using <paramref name="host"/>.
	/// </summary>
	/// <param name="host">A <see cref="IVirtualMachineHost"/> instance.</param>
	/// <returns>A <see cref="INativeThread"/> instance.</returns>
	internal static INativeThread GetNativeThread(this IVirtualMachineHost host)
		=> host.Value.GetEnvironment() as INativeThread ?? throw new InvalidOperationException(); //TODO: Message
}