namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a non-typed class field definition.
/// </summary>
public sealed record JNonTypedFieldDefinition : JFieldDefinition
{
	/// <inheritdoc/>
	internal override Type Return => typeof(JReferenceObject);

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	/// <param name="signature">Signature field.</param>
	public JNonTypedFieldDefinition(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> signature) : base(
		name, JAccessibleObjectDefinition.ValidateSignature(signature)) { }

	/// <summary>
	/// Retrieves the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <returns>The <paramref name="jLocal"/> field's value.</returns>
	public JLocalObject? Get(JLocalObject jLocal, JClassObject? jClass = default)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessProvider.GetField<JLocalObject>(jLocal, jClass ?? jLocal.Class, this);
	}
	/// <summary>
	/// Retrieves the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jClass"/> field's value.</returns>
	public JLocalObject? StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessProvider.GetStaticField<JLocalObject>(jClass, this);
	}
	/// <summary>
	/// Sets the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="value">The field value to set to.</param>
	public void Set(JLocalObject jLocal, JLocalObject? value, JClassObject? jClass = default)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessProvider.SetField(jLocal, jClass ?? jLocal.Class, this, value);
	}
	/// <summary>
	/// Sets the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSet(JClassObject jClass, JLocalObject? value)
	{
		IEnvironment env = jClass.Environment;
		env.AccessProvider.SetStaticField(jClass, this, value);
	}
}