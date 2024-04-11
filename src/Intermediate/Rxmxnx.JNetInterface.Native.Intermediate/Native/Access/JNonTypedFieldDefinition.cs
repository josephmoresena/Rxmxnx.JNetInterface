namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a non-typed class field definition.
/// </summary>
public sealed class JNonTypedFieldDefinition : JFieldDefinition
{
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
		return env.AccessFeature.GetField<JLocalObject>(jLocal, jClass ?? jLocal.Class, this);
	}
	/// <summary>
	/// Retrieves the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jClass"/> field's value.</returns>
	public JLocalObject? StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.GetStaticField<JLocalObject>(jClass, this);
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
		env.AccessFeature.SetField(jLocal, jClass ?? jLocal.Class, this, value);
	}
	/// <summary>
	/// Sets the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSet(JClassObject jClass, JLocalObject? value)
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.SetStaticField(jClass, this, value);
	}

	/// <summary>
	/// Retrieves the value of a reflected field which matches with current definition.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <returns>The <paramref name="jLocal"/> field's value.</returns>
	public JLocalObject? GetReflected(JFieldObject jField, JLocalObject jLocal)
	{
		IEnvironment env = jField.Environment;
		return env.AccessFeature.GetField<JLocalObject>(jField, jLocal, this);
	}
	/// <summary>
	/// Retrieves the value of a reflected static field reflected which matches with current definition.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <returns>The static field's value.</returns>
	public JLocalObject? StaticGetReflected(JFieldObject jField)
	{
		IEnvironment env = jField.Environment;
		return env.AccessFeature.GetStaticField<JLocalObject>(jField, this);
	}
	/// <summary>
	/// Sets the value of a reflected field which matches with current definition.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void SetReflected(JFieldObject jField, JLocalObject jLocal, JLocalObject? value)
	{
		IEnvironment env = jField.Environment;
		env.AccessFeature.SetField(jField, jLocal, this, value);
	}
	/// <summary>
	/// Sets the value of a reflected static field which matches with current definition.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSetReflected(JFieldObject jField, JLocalObject? value)
	{
		IEnvironment env = jField.Environment;
		env.AccessFeature.SetStaticField(jField, this, value);
	}
}