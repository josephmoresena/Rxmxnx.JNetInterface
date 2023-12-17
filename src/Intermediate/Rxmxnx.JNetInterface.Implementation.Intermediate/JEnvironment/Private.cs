namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	/// <summary>
	/// Tests whether two references refer to the same object.
	/// </summary>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="otherRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <returns>
	/// <see langword="true"/> if both references refer to the same object; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	private Boolean IsSame(JObjectLocalRef localRef, JObjectLocalRef otherRef)
	{
		IsSameObjectDelegate isSameObject = this._cache.GetDelegate<IsSameObjectDelegate>();
		Byte result = isSameObject(this._cache.Reference, localRef, otherRef);
		this._cache.CheckJniError();
		return result == JBoolean.TrueValue;
	}
	/// <summary>
	/// Creates a new local reference frame.
	/// </summary>
	/// <param name="capacity">Frame capacity.</param>
	/// <exception cref="InvalidOperationException"/>
	/// <exception cref="JniException"/>
	private void CreateLocalFrame(Int32 capacity)
	{
		if (!this.JniSecure()) throw new InvalidOperationException();
		PushLocalFrameDelegate pushLocalFrame = this._cache.GetDelegate<PushLocalFrameDelegate>();
		JResult jResult = pushLocalFrame(this.Reference, capacity);
		if (jResult != JResult.Ok) throw new JniException(jResult);
	}
	/// <summary>
	/// Creates a new global reference to <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JGlobalRef CreateGlobalRef(JReferenceObject jLocal)
		=> this._cache.CreateGlobalRef(jLocal.As<JObjectLocalRef>());
	
	/// <inheritdoc cref="IEquatable{TEquatable}.Equals(TEquatable)"/>
	private static Boolean? EqualEquatable<TEquatable>(IEquatable<TEquatable>? obj, TEquatable? other)
	{
		if (obj is null || other is null) return default;
		return obj.Equals(other);
	}
	/// <summary>
	/// Retrieves field identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private static JFieldId GetFieldId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetFieldIdDelegate getFieldId = args.env._cache.GetDelegate<GetFieldIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getFieldId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
	/// <summary>
	/// Retrieves static field identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JFieldId"/> identifier.</returns>
	private static JFieldId GetStaticFieldId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetStaticFieldIdDelegate getStaticFieldId = args.env._cache.GetDelegate<GetStaticFieldIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getStaticFieldId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
	/// <summary>
	/// Retrieves method identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private static JMethodId GetMethodId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetMethodIdDelegate getMethodId = args.env._cache.GetDelegate<GetMethodIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getMethodId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
	/// <summary>
	/// Retrieves static method identifier for given definition in given class.
	/// </summary>
	/// <param name="memoryList">Definition information.</param>
	/// <param name="args">Environment and Class instance.</param>
	/// <returns>A <see cref="JMethodId"/> identifier.</returns>
	private static JMethodId GetStaticMethodId(ReadOnlyFixedMemoryList memoryList,
		(JEnvironment env, JClassLocalRef classRef) args)
	{
		GetStaticMethodIdDelegate getStaticMethodId = args.env._cache.GetDelegate<GetStaticMethodIdDelegate>();
		ReadOnlyValPtr<Byte> namePtr = (ReadOnlyValPtr<Byte>)memoryList[0].Pointer;
		ReadOnlyValPtr<Byte> signaturePtr = (ReadOnlyValPtr<Byte>)memoryList[1].Pointer;
		return getStaticMethodId(args.env.Reference, args.classRef, namePtr, signaturePtr);
	}
}