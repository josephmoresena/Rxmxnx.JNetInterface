namespace Rxmxnx.JNetInterface.Native.Proxies;

/// <summary>
/// This object exposes a JNI proxy instance.
/// </summary>
public abstract partial class EnvironmentProxy
{
	/// <inheritdoc cref="IEnvironment.VirtualMachine"/>
	public abstract VirtualMachineProxy VirtualMachine { get; }
	/// <inheritdoc/>
	public abstract JClassObject ClassObject { get; }
	/// <inheritdoc cref="IEnvironment.PendingException"/>
	public abstract ThrowableException? PendingException { get; set; }
	/// <inheritdoc/>
	public abstract JEnvironmentRef Reference { get; }
	/// <inheritdoc/>
	public abstract Int32 Version { get; }
	/// <inheritdoc/>
	public abstract Int32? LocalCapacity { get; set; }

	/// <inheritdoc/>
	public abstract Boolean IsValidationAvoidable(JGlobalBase jGlobal);
	/// <inheritdoc/>
	public abstract JReferenceType GetReferenceType(JObject jObject);
	/// <inheritdoc/>
	public abstract Boolean IsSameObject(JObject jObject, JObject? jOther);
	/// <inheritdoc/>
	public abstract Boolean JniSecure();
	/// <inheritdoc/>
	public abstract void WithFrame(Int32 capacity, Action action);
	/// <inheritdoc/>
	public abstract void WithFrame<TState>(Int32 capacity, TState state, Action<TState> action);
	/// <inheritdoc/>
	public abstract TResult WithFrame<TResult>(Int32 capacity, Func<TResult> func);
	/// <inheritdoc/>
	public abstract TResult WithFrame<TResult, TState>(Int32 capacity, TState state, Func<TState, TResult> func);
	/// <inheritdoc/>
	public abstract void DescribeException();

	#region IReferenceFeature
	/// <summary>
	/// Retrieves the original <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>The original <see cref="JLocalObject"/> instance for <paramref name="localRef"/>.</returns>
	public abstract JLocalObject? GetSourceInstance(JObjectLocalRef localRef);
	/// <summary>
	/// Creates a <see cref="JBooleanObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JBoolean"/> value.</param>
	/// <returns>A <see cref="JBooleanObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JBooleanObject CreateWrapper(JBoolean value);
	/// <summary>
	/// Creates a <see cref="JByteObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JByte"/> value.</param>
	/// <returns>A <see cref="JByteObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JByteObject CreateWrapper(JByte value);
	/// <summary>
	/// Creates a <see cref="JCharacterObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JChar"/> value.</param>
	/// <returns>A <see cref="JCharacterObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JCharacterObject CreateWrapper(JChar value);
	/// <summary>
	/// Creates a <see cref="JDoubleObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JDouble"/> value.</param>
	/// <returns>A <see cref="JDoubleObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JDoubleObject CreateWrapper(JDouble value);
	/// <summary>
	/// Creates a <see cref="JFloatObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JFloat"/> value.</param>
	/// <returns>A <see cref="JFloatObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JFloatObject CreateWrapper(JFloat value);
	/// <summary>
	/// Creates a <see cref="JIntegerObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JInt"/> value.</param>
	/// <returns>A <see cref="JIntegerObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JIntegerObject CreateWrapper(JInt value);
	/// <summary>
	/// Creates a <see cref="JLongObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JLong"/> value.</param>
	/// <returns>A <see cref="JLongObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JLongObject CreateWrapper(JLong value);
	/// <summary>
	/// Creates a <see cref="JShortObject"/> wrapper instance for <paramref name="value"/>.
	/// </summary>
	/// <param name="value">A <see cref="JShort"/> value.</param>
	/// <returns>A <see cref="JShortObject"/> wrapper instance for <paramref name="value"/>.</returns>
	public abstract JShortObject CreateWrapper(JShort value);
	#endregion

	#region INioFeature
	/// <summary>
	/// Creates a <see cref="JDirectByteBufferObject"/> instance.
	/// </summary>
	/// <param name="memory">A <see cref="IFixedMemory"/> instance.</param>
	/// <returns>A direct <see cref="JDirectByteBufferObject"/> instance.</returns>
	public abstract JDirectByteBufferObject NewDirectByteBuffer(IFixedMemory.IDisposable memory);
	/// <summary>
	/// Creates an ephemeral <see cref="JDirectByteBufferObject"/> instance.
	/// </summary>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <returns>A <see cref="JDirectByteBufferObject"/> instance.</returns>
	public abstract JDirectByteBufferObject CreateEphemeralByteBuffer(Int32 capacity);
	#endregion

	#region IClassFeature
	/// <inheritdoc cref="IClassFeature.GetClass(ReadOnlySpan{Byte})"/>
	public abstract JClassObject GetClass(CString className);
	/// <inheritdoc cref="IClassFeature.LoadClass(ReadOnlySpan{Byte}, ReadOnlySpan{Byte}, JClassLoaderObject?)"/>
	public abstract JClassObject LoadClass(CString className, Byte[] rawClassBytes,
		JClassLoaderObject? jClassLoader = default);
	/// <inheritdoc cref="IClassFeature.LoadClass{TDataType}(ReadOnlySpan{Byte}, JClassLoaderObject?)"/>
	public abstract JClassObject LoadClass<TDataType>(Byte[] rawClassBytes, JClassLoaderObject? jClassLoader = default)
		where TDataType : JLocalObject, IReferenceType<TDataType>;
	/// <summary>
	/// Retrieves the class info.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="ITypeInformation"/> instance.</returns>
	public abstract ITypeInformation GetClassInfo(JClassObject jClass);
	#endregion

	#region IStringFeature
	/// <inheritdoc cref="IStringFeature.Create(ReadOnlySpan{Char})"/>
	public abstract JStringObject Create(String data);
	/// <inheritdoc cref="IStringFeature.Create(ReadOnlySpan{Byte})"/>
	public abstract JStringObject Create(CString data);
	#endregion
}