namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <summary>
	/// Creates a <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.
	/// </summary>
	/// <param name="env">A <see cref="EnvironmentProxy"/> instance.</param>
	/// <returns>A <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.</returns>
	public static JClassObject CreateClassObject(EnvironmentProxy env) => new(env);
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	public static JLocalObject CreateObject(JClassObject jClass, JObjectLocalRef localRef)
		=> new(EnvironmentProxy.ThrowIfNotProxy(jClass), localRef);
	/// <summary>
	/// Creates a <typeparamref name="TGlobal"/> instance.
	/// </summary>
	/// <typeparam name="TGlobal">The type of global object.</typeparam>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <typeparamref name="TGlobal"/> instance.</returns>
	public static TGlobal CreateGlobal<TGlobal>(JReferenceObject jObject, JObjectLocalRef localRef)
		where TGlobal : JGlobalBase
	{
		EnvironmentProxy.ThrowIfNotProxy(jObject);
		JGlobalBase? result = default;
		if (typeof(TGlobal) == typeof(JGlobal))
			result = jObject switch
			{
				ILocalObject jLocal => new JGlobal(jLocal, localRef.Transform<JObjectLocalRef, JGlobalRef>()),
				JGlobalBase jGlobal => new JGlobal(jGlobal, localRef.Transform<JObjectLocalRef, JGlobalRef>()),
				_ => result,
			};
		else if (typeof(TGlobal) == typeof(JWeak))
			result = jObject switch
			{
				ILocalObject jLocal => new JWeak(jLocal, localRef.Transform<JObjectLocalRef, JWeakRef>()),
				JGlobalBase jGlobal => new JWeak(jGlobal, localRef.Transform<JObjectLocalRef, JWeakRef>()),
				_ => result,
			};
		return (TGlobal?)result ?? throw new ArgumentException("Invalid global type.");
	}
	/// <summary>
	/// Sets <paramref name="text"/> information as <paramref name="jString"/> value.
	/// </summary>
	/// <param name="jString">A <see cref="JStringObject"/> instance.</param>
	/// <param name="text">A <see cref="String"/> value.</param>
	public static void SetText(JStringObject jString, String text)
	{
		EnvironmentProxy.ThrowIfNotProxy(jString);
		StringObjectMetadata objectMetadata = new(new(jString.Class))
		{
			Value = text, Length = text.Length, Utf8Length = ((CString)text).Length,
		};
		ILocalObject.ProcessMetadata(jString, objectMetadata);
	}
	/// <summary>
	/// Sets <paramref name="info"/> information as <paramref name="element"/> information.
	/// </summary>
	/// <param name="element">A <see cref="JStackTraceElementObject"/> instance.</param>
	/// <param name="info">A <see cref="StackTraceInfo"/> instance.</param>
	public static void SetStackTrace(JStackTraceElementObject element, StackTraceInfo info)
	{
		EnvironmentProxy.ThrowIfNotProxy(element);
		StackTraceElementObjectMetadata objectMetadata = new(new(element.Class)) { Information = info, };
		ILocalObject.ProcessMetadata(element, objectMetadata);
	}

	/// <summary>
	/// Throws an exception if <paramref name="jObject"/> is proxy.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <exception cref="ArgumentException">Throws an exception if <paramref name="jObject"/> is proxy.</exception>
	[return: NotNullIfNotNull(nameof(jObject))]
	private static TObject? ThrowIfNotProxy<TObject>(TObject? jObject) where TObject : JReferenceObject
	{
		if (jObject is not null && jObject.IsProxy)
			throw new ArgumentException("Java Object must be proxy to perform this operation.");
		return jObject;
	}
}