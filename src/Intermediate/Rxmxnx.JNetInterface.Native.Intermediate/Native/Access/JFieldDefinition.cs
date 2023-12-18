namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a class field definition.
/// </summary>
public abstract record JFieldDefinition : JAccessibleObjectDefinition
{
	/// <inheritdoc/>
	internal override String ToStringFormat => "xField: {0} Descriptor: {1}";

	/// <summary>
	/// Return type.
	/// </summary>
	internal abstract Type Return { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	/// <param name="signature">Signature field.</param>
	internal JFieldDefinition(CString name, CString signature) : base(new CStringSequence(name, signature)) { }
}

/// <summary>
/// This class stores a class field definition.
/// </summary>
/// <typeparam name="TField"><see cref="IDataType"/> type of field result.</typeparam>
public sealed record JFieldDefinition<TField> : JFieldDefinition where TField : IDataType<TField>, IObject
{
	/// <inheritdoc/>
	internal override Type Return => JAccessibleObjectDefinition.ReturnType<TField>();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	public JFieldDefinition(CString name) : base(name, IDataType.GetMetadata<TField>().Signature) { }
	/// <inheritdoc/>
	internal JFieldDefinition(CString name, CString signature) : base(name, signature) { }
	/// <inheritdoc/>
	internal JFieldDefinition(JFieldDefinition definition) : base(definition) { }

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
	/// <inheritdoc/>
	public override Int32 GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Retrieves the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jLocal"/> field's value.</returns>
	public TField? Get(JLocalObject jLocal, JClassObject? jClass = default)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessProvider.GetField<TField>(jLocal, jClass ?? jLocal.Class, this);
	}
	/// <summary>
	/// Retrieves the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jClass"/> field's value.</returns>
	public TField? StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessProvider.GetStaticField<TField>(jClass, this);
	}
	/// <summary>
	/// Sets the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	public void Set(JLocalObject jLocal, TField? value, JClassObject? jClass = default)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessProvider.SetField(jLocal, jClass ?? jLocal.Class, this, value);
	}
	/// <summary>
	/// Sets the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSet(JClassObject jClass, TField? value)
	{
		IEnvironment env = jClass.Environment;
		env.AccessProvider.SetStaticField(jClass, this, value);
	}
}