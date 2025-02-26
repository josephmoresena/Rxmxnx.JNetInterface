namespace Rxmxnx.JNetInterface.Proxies;

public abstract partial class EnvironmentProxy
{
	/// <summary>
	/// Java methods based <see cref="NativeFunctionSet"/> instance.
	/// </summary>
	public static NativeFunctionSet JniFunctionSet => NativeFunctionSetImpl.Instance;

	/// <summary>
	/// Creates a <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.
	/// </summary>
	/// <param name="env">A <see cref="EnvironmentProxy"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JClassObject"/> for <c>java.lang.Class&lt;?&gt;</c> data type.</returns>
	public static JClassObject CreateClassObject(EnvironmentProxy env, JClassLocalRef classRef = default)
	{
		JClassObject result = new(env);
		result.SetValue(classRef);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="JClassObject"/> for <typeparamref name="TDataType"/> data type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <param name="jClass">The <c>java.lang.Class&lt;?&gt;</c> <see cref="JClassObject"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JClassObject"/> for <typeparamref name="TDataType"/> data type.</returns>
	public static JClassObject CreateClassObject<TDataType>(JClassObject jClass, JClassLocalRef classRef = default)
		where TDataType : IDataType<TDataType>
	{
		EnvironmentProxy.ThrowIfNotProxy(jClass);
		EnvironmentProxy.ThrowIfNotClass(jClass, IDataType.GetMetadata<JClassObject>());
		return new(jClass, IDataType.GetMetadata<TDataType>(), classRef);
	}
	/// <summary>
	/// Creates a <see cref="JClassObject"/> for <paramref name="className"/> data type.
	/// </summary>
	/// <param name="jClass">The <c>java.lang.Class&lt;?&gt;</c> <see cref="JClassObject"/> instance.</param>
	/// <param name="className">Class name.</param>
	/// <param name="kind">Class kind.</param>
	/// <param name="isFinal">Indicates whether resulting class is final.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JClassObject"/> for <paramref name="className"/> data type.</returns>
	public static JClassObject CreateClassObject(JClassObject jClass, ReadOnlySpan<Byte> className, JTypeKind kind,
		Boolean? isFinal = default, JClassLocalRef classRef = default)
	{
		EnvironmentProxy.ThrowIfNotProxy(jClass);
		EnvironmentProxy.ThrowIfNotClass(jClass, IDataType.GetMetadata<JClassObject>());
		ClassObjectMetadata classMetadata = new(jClass, new(className), kind, isFinal);
		return new(jClass, classMetadata, classRef);
	}
	/// <summary>
	/// Creates a <see cref="JClassObject"/> for <c>void</c> data type.
	/// </summary>
	/// <param name="jClass">The <c>java.lang.Class&lt;?&gt;</c> <see cref="JClassObject"/> instance.</param>
	/// <param name="classRef">A <see cref="JClassLocalRef"/> reference.</param>
	/// <returns>A <see cref="JClassObject"/> for <c>void</c> data type.</returns>
	public static JClassObject CreateVoidClassObject(JClassObject jClass, JClassLocalRef classRef = default)
	{
		EnvironmentProxy.ThrowIfNotProxy(jClass);
		EnvironmentProxy.ThrowIfNotClass(jClass, IDataType.GetMetadata<JClassObject>());
		return new(jClass, JPrimitiveTypeMetadata.VoidMetadata, classRef);
	}
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	public static JLocalObject CreateObject(JClassObject jClass, JObjectLocalRef localRef)
		=> new(EnvironmentProxy.ThrowIfNotProxy(jClass), localRef);
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="stringRef">A <see cref="JStringLocalRef"/> reference.</param>
	/// <param name="value">String value.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	public static JStringObject CreateString(JClassObject jClass, JStringLocalRef stringRef, String? value = default)
	{
		EnvironmentProxy.ThrowIfNotProxy(jClass);
		EnvironmentProxy.ThrowIfNotClass(jClass, IDataType.GetMetadata<JStringObject>());
		return new(jClass, stringRef, value);
	}
	/// <summary>
	/// Creates a <see cref="JLocalObject"/> instance.
	/// </summary>
	/// <typeparam name="TWrapper">Primitive wrapper type.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="value">Primitive value.</param>
	/// <returns>A <see cref="JLocalObject"/> instance.</returns>
	public static TWrapper CreteWrapperObject<TWrapper>(JClassObject jClass, JObjectLocalRef localRef,
		IPrimitiveType? value = default) where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper>
	{
		JClassTypeMetadata typeMetadata = IClassType.GetMetadata<TWrapper>();
		EnvironmentProxy.ThrowIfNotProxy(jClass);
		EnvironmentProxy.ThrowIfNotClass(jClass, typeMetadata);
		TWrapper result = (TWrapper)typeMetadata.CreateInstance(jClass, localRef, true);
		if (value is not null)
			result.SetPrimitiveValue(value);
		return result;
	}
	/// <summary>
	/// Creates a <see cref="JArrayObject{TDataType}"/> instance.
	/// </summary>
	/// <typeparam name="TDataType">Array element type.</typeparam>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="arrayRef">A <see cref="JArrayLocalRef"/> reference.</param>
	/// <param name="length">Array length.</param>
	/// <returns>A <see cref="JArrayObject{TDataType}"/> instance.</returns>
	public static JArrayObject<TDataType> CreateArrayObject<TDataType>(JClassObject jClass, JArrayLocalRef arrayRef,
		Int32 length) where TDataType : IObject, IDataType<TDataType>
	{
		EnvironmentProxy.ThrowIfNotProxy(jClass);
		EnvironmentProxy.ThrowIfNotArrayClass(jClass, IDataType.GetMetadata<TDataType>());
		return new(jClass, arrayRef, length);
	}
	/// <summary>
	/// Creates a <see cref="ThrowableException"/> instance.
	/// </summary>
	/// <typeparam name="TThrowable">A <see cref="IThrowableType{TThrowable}"/> type.</typeparam>
	/// <param name="vm">A <see cref="VirtualMachineProxy"/> instance.</param>
	/// <param name="globalRef">A <see cref="JGlobalRef"/> reference.</param>
	/// <param name="jGlobal">Throwable global instance.</param>
	/// <param name="message">Throwable message.</param>
	/// <param name="stackTrace">Throwable stacktrace.</param>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	public static ThrowableException CreateException<TThrowable>(VirtualMachineProxy vm, JGlobalRef globalRef,
		out JGlobalBase jGlobal, String? message = default, params StackTraceInfo[] stackTrace)
		where TThrowable : JThrowableObject, IThrowableType<TThrowable>
	{
		JClassTypeMetadata throwableMetadata = IClassType.GetMetadata<TThrowable>();
		ThrowableObjectMetadata objectMetadata = new(throwableMetadata, message, true) { StackTrace = stackTrace, };
		jGlobal = new JGlobal(vm, objectMetadata, globalRef);
		return throwableMetadata.CreateException(jGlobal, message)!;
	}
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
		return (TGlobal?)result ?? throw new ArgumentException(IMessageResource.GetInstance().InvalidGlobalObject);
	}
	/// <summary>
	/// Sets <paramref name="length"/> as <paramref name="jArray"/>'s length.
	/// </summary>
	/// <param name="jArray">A <see cref="JArrayObject"/> instance.</param>
	/// <param name="length">Array length value.</param>
	public static void SetArrayLength(JArrayObject jArray, Int32 length)
	{
		EnvironmentProxy.ThrowIfNotProxy(jArray);
		ArrayObjectMetadata objectMetadata = new(new(jArray.Class)) { Length = length, };
		ILocalObject.ProcessMetadata(jArray, objectMetadata);
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
			Value = text, Length = text.Length, Utf8Length = Encoding.UTF8.GetByteCount(text),
		};
		ILocalObject.ProcessMetadata(jString, objectMetadata);
	}
	/// <summary>
	/// Sets <paramref name="value"/> as <paramref name="jWrapper"/> value.
	/// </summary>
	/// <typeparam name="TWrapper">Primitive wrapper type.</typeparam>
	/// <typeparam name="TPrimitive">Primitive type.</typeparam>
	/// <param name="jWrapper">A <typeparamref name="TWrapper"/> instance.</param>
	/// <param name="value">A <typeparamref name="TPrimitive"/> value.</param>
	public static void SetValue<TWrapper, TPrimitive>(TWrapper jWrapper, TPrimitive value)
		where TWrapper : JLocalObject, IPrimitiveWrapperType<TWrapper, TPrimitive>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		EnvironmentProxy.ThrowIfNotProxy(jWrapper);
		PrimitiveWrapperObjectMetadata<TPrimitive> objectMetadata = new(new(jWrapper.Class)) { Value = value, };
		ILocalObject.ProcessMetadata(jWrapper, objectMetadata);
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
	/// Sets <see cref="JThrowableObject"/> instance.
	/// </summary>
	/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance.</param>
	/// <param name="threadId">Thread identifier to set to.</param>
	public static void SetThreadId(JThrowableObject jThrowable, Int32? threadId)
	{
		EnvironmentProxy.ThrowIfNotProxy(jThrowable);
		jThrowable.ThreadId = threadId;
	}
	/// <summary>
	/// Retrieves proxy identifier.
	/// </summary>
	/// <param name="jObject">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>Proxy object identifier.</returns>
	public static Int64 GetProxyId(JReferenceObject? jObject)
	{
		EnvironmentProxy.ThrowIfNotProxy(jObject);
		return jObject?.Id ?? default;
	}
}