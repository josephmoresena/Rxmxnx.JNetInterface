namespace Rxmxnx.JNetInterface.Native;

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
	public JNonTypedFieldDefinition(CString name, CString signature) 
		: base(name, JAccessibleObjectDefinition.ValidateSignature(signature)) { }

	/// <summary>
	/// Retrieves the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The <paramref name="jLocal"/> field's value.</returns>
	public JLocalObject? Get(JLocalObject jLocal)
	{
		IEnvironment env = jLocal.Environment;
		return env.Accessor.GetField<JLocalObject>(jLocal, this);
	}
	/// <summary>
	/// Retrieves the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jClass"/> field's value.</returns>
	public JLocalObject? StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		return env.Accessor.GetStaticField<JLocalObject>(jClass, this);
	}
	/// <summary>
	/// Sets the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void Set(JLocalObject jLocal, JLocalObject? value)
	{
		IEnvironment env = jLocal.Environment;
		env.Accessor.SetField(jLocal, this, value);
	}
	/// <summary>
	/// Sets the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSet(JClassObject jClass, JLocalObject? value)
	{
		IEnvironment env = jClass.Environment;
		env.Accessor.SetStaticField(jClass, this, value);
	}
}