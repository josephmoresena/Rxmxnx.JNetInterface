namespace Rxmxnx.JNetInterface;

public static partial class AndroidJniExtensions
{
	/// <summary>
	/// Creates a JNI (java.interop) object from a <see langword="JReferenceObject"/> object.
	/// </summary>
	/// <typeparam name="TResult">A <see cref="IJavaPeerable"/> type.</typeparam>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A <see cref="IJavaPeerable"/> instance.</returns>
	/// <remarks>
	/// If <paramref name="jObject"/> is a <see cref="JGlobalBase"/>, its JNI reference is released after the conversion.
	/// </remarks>
	internal static TResult?
		ToJniObject<[DynamicallyAccessedMembers(AndroidJniExtensions.JavaObjectMembers)] TResult>(
			this JReferenceObject? jObject) where TResult : class, IJavaPeerable
	{
		if (JObject.IsNullOrDefault(jObject)) return default;
		if (jObject is JGlobalBase jGlobal)
			try
			{
				return jGlobal.ToJniObject<TResult>();
			}
			finally
			{
				jGlobal.Dispose();
			}
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
	{
		if (host.Value.GetEnvironment() is INativeThread env) return env;
		IMessageResource resource = IMessageResource.GetInstance();
		throw new RunningStateException(resource.NotAttachedThread);
	}
}