namespace Rxmxnx.JNetInterface.Proxies;

/// <summary>
/// This object exposes a JNI proxy instance.
/// </summary>
#if !PACKAGE
[ExcludeFromCodeCoverage]
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS4136,
                 Justification = CommonConstants.NoMethodOverloadingJustification)]
#endif
public abstract partial class EnvironmentProxy
{
	/// <inheritdoc cref="IEnvironment.VirtualMachine"/>
	public abstract VirtualMachineProxy VirtualMachine { get; }
	/// <inheritdoc/>
	public abstract JClassObject ClassObject { get; }
	/// <inheritdoc/>
	public abstract JClassObject VoidPrimitive { get; }
	/// <inheritdoc cref="IEnvironment.PendingException"/>
	public abstract ThrowableException? PendingException { get; set; }
	/// <inheritdoc/>
	public abstract Int32 UsedStackBytes { get; }
	/// <inheritdoc/>
	public abstract Int32 UsableStackBytes { get; set; }
	/// <inheritdoc/>
	public abstract JEnvironmentRef Reference { get; }
	/// <inheritdoc/>
	public abstract Int32 Version { get; }
	/// <inheritdoc/>
	public virtual Int32? LocalCapacity { get; set; }

	/// <inheritdoc/>
	public virtual Boolean IsValidationAvoidable(JGlobalBase jGlobal) => true;
	/// <inheritdoc/>
	public virtual JReferenceType GetReferenceType(JObject jObject)
	{
		return jObject switch
		{
			JGlobal => JReferenceType.GlobalRefType,
			JWeak => JReferenceType.WeakGlobalRefType,
			JLocalObject => JReferenceType.LocalRefType,
			_ => JReferenceType.InvalidRefType,
		};
	}
	/// <inheritdoc/>
	public abstract Boolean IsSameObject(JObject jObject, JObject? jOther);
	/// <inheritdoc/>
	public abstract Boolean JniSecure();
	/// <inheritdoc/>
	public abstract void DescribeException();
	/// <inheritdoc/>
	public abstract Boolean? IsVirtual(JThreadObject jThread);

	#region IArrayFeature
	/// <inheritdoc cref="IArrayFeature.GetCopy{TPrimitive}(JArrayObject{TPrimitive}, Span{TPrimitive}, Int32)"/>
	public abstract void GetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, IFixedMemory<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	/// <inheritdoc cref="IArrayFeature.SetCopy{TPrimitive}(JArrayObject{TPrimitive}, ReadOnlySpan{TPrimitive}, Int32)"/>
	public abstract void SetCopy<TPrimitive>(JArrayObject<TPrimitive> jArray, IReadOnlyFixedMemory<TPrimitive> elements,
		Int32 startIndex = 0) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>;
	#endregion

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
	/// <param name="address">A <see cref="IntPtr"/> instance.</param>
	/// <returns>A direct <see cref="JDirectByteBufferObject"/> instance.</returns>
	public abstract JDirectByteBufferObject NewDirectByteBuffer(IntPtr address);
	/// <summary>
	/// Creates an ephemeral <see cref="JDirectByteBufferObject"/> instance.
	/// </summary>
	/// <param name="capacity">Capacity of created buffer.</param>
	/// <returns>A <see cref="JDirectByteBufferObject"/> instance.</returns>
	public abstract JDirectByteBufferObject NewDirectByteBuffer(Int32 capacity);
	#endregion

	#region IClassFeature
	/// <inheritdoc cref="IClassFeature.GetClass(ReadOnlySpan{Byte})"/>
	public abstract JClassObject GetClass(CString className);
	/// <inheritdoc cref="IClassFeature.LoadClass(ReadOnlySpan{Byte}, ReadOnlySpan{Byte}, JClassLoaderObject?)"/>
	public abstract JClassObject LoadClass(CString className, Byte[] rawClassBytes,
		JClassLoaderObject? jClassLoader = default);
	/// <inheritdoc cref="IClassFeature.LoadClass{TDataType}(ReadOnlySpan{Byte}, JClassLoaderObject?)"/>
	public abstract JClassObject
		LoadClass<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TDataType>(
			Byte[] rawClassBytes,
			JClassLoaderObject? jClassLoader = default) where TDataType : JReferenceObject, IReferenceType<TDataType>;
	/// <summary>
	/// Retrieves the class info.
	/// </summary>
	/// <param name="jClass">A <see cref="JReferenceObject"/> instance.</param>
	/// <returns>A <see cref="ITypeInformation"/> instance.</returns>
	public abstract IWrapper.IBase<ITypeInformation> GetClassInfo(JReferenceObject jClass);
	#endregion

	#region IStringFeature
	/// <summary>
	/// Creates a <see cref="JStringObject"/> instance initialized with <paramref name="data"/>.
	/// </summary>
	/// <param name="data">UTF-16 string data.</param>
	/// <returns>A new <see cref="JStringObject"/> instance.</returns>
	public abstract JStringObject Create(String data);
	/// <inheritdoc cref="IStringFeature.Create(ReadOnlySpan{Byte})"/>
	public abstract JStringObject Create(CString data);
	/// <inheritdoc cref="IStringFeature.GetCopy(JStringObject, Span{Char}, Int32)"/>
	public abstract void GetCopy(JStringObject jString, IFixedMemory<Char> chars, Int32 startIndex = 0);
	/// <inheritdoc cref="IStringFeature.GetUtf8Copy(JStringObject, Span{Byte}, Int32)"/>
	public abstract void GetUtf8Copy(JStringObject jString, IFixedMemory<Byte> utf8Units, Int32 startIndex = 0);
	#endregion
}