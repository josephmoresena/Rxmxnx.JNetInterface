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
	
	/// <inheritdoc cref="JFieldDefinition{TField}.Get(JLocalObject)"/>
	public JLocalObject? Get(JLocalObject jLocal)
	{
		IEnvironment env = jLocal.Environment;
		return env.Accessor.GetField<JLocalObject>(jLocal, this);
	}
	/// <inheritdoc cref="JFieldDefinition{TField}.StaticGet(JClassObject)"/>
	public JLocalObject? StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		return env.Accessor.GetStaticField<JLocalObject>(jClass, this);
	}
	/// <inheritdoc cref="JFieldDefinition{TField}.Set(JLocalObject, TField?)"/>
	public void Set(JLocalObject jLocal, JLocalObject? value)
	{
		IEnvironment env = jLocal.Environment;
		env.Accessor.SetField(jLocal, this, value);
	}
	/// <inheritdoc cref="JFieldDefinition{TField}.StaticSet(JClassObject, TField?)"/>
	public void StaticSet(JClassObject jClass, JLocalObject? value)
	{
		IEnvironment env = jClass.Environment;
		env.Accessor.SetStaticField(jClass, this, value);
	}
}