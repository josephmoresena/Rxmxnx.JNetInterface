namespace Rxmxnx.JNetInterface.Internal.Native.Access;

/// <summary>
/// This class stores a primitive field definition.
/// </summary>
internal sealed record JPrimitiveFieldDefinition : JFieldDefinition
{
	/// <inheritdoc cref="JPrimitiveFunctionDefinition.Return"/>
	private readonly Type _type;

	/// <inheritdoc/>
	internal override Type Return => this._type;

	/// <inheritdoc/>
	private JPrimitiveFieldDefinition(CString name, CString signature, Type type) : base(name, signature)
		=> this._type = type;

	/// <summary>
	/// Retrieves the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The <paramref name="jLocal"/> field's value.</returns>
	public TField Get<TField>(JLocalObject jLocal) where TField : unmanaged
	{
		IEnvironment env = jLocal.Environment;
		Span<TField> result = stackalloc TField[1];
		env.AccessProvider.GetPrimitiveField(result.AsBytes(), jLocal, this);
		return result[0];
	}
	/// <summary>
	/// Retrieves the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jClass"/> field's value.</returns>
	public TField? StaticGet<TField>(JClassObject jClass) where TField : unmanaged
	{
		IEnvironment env = jClass.Environment;
		Span<TField> result = stackalloc TField[1];
		env.AccessProvider.GetPrimitiveStaticField(result.AsBytes(), jClass, this);
		return result[0];
	}
	/// <summary>
	/// Sets the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void Set<TField>(JLocalObject jLocal, in TField value) where TField : unmanaged
	{
		IEnvironment env = jLocal.Environment;
		env.AccessProvider.SetPrimitiveField(jLocal, this, NativeUtilities.AsBytes(value));
	}
	/// <summary>
	/// Sets the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSet<TField>(JClassObject jClass, in TField value) where TField : unmanaged
	{
		IEnvironment env = jClass.Environment;
		env.AccessProvider.SetPrimitiveStaticField(jClass, this, NativeUtilities.AsBytes(value));
	}

	/// <summary>
	/// Creates a <c>boolean</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateBooleanDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JBooleanSignature, typeof(Byte));
	/// <summary>
	/// Creates a <c>byte</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateByteDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JByteSignature, typeof(SByte));
	/// <summary>
	/// Creates a <c>char</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateCharDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JCharSignature, typeof(Char));
	/// <summary>
	/// Creates a <c>double</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateDoubleDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JDoubleSignature, typeof(Double));
	/// <summary>
	/// Creates a <c>float</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateFloatDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JFloatSignature, typeof(Single));
	/// <summary>
	/// Creates a <c>int</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateIntDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JIntSignature, typeof(Int32));
	/// <summary>
	/// Creates a <c>long</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateLongDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JLongSignature, typeof(Int64));
	/// <summary>
	/// Creates a <c>short</c> field definition.
	/// </summary>
	/// <param name="functionName">Field name.</param>
	/// <returns>A <see cref="JPrimitiveFieldDefinition"/> instance.</returns>
	public static JPrimitiveFieldDefinition CreateShortDefinition(CString functionName)
		=> new(functionName, UnicodePrimitiveSignatures.JShortSignature, typeof(Int16));
}