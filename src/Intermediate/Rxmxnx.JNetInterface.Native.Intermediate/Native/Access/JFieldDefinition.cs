﻿namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a class field definition.
/// </summary>
public abstract class JFieldDefinition : JAccessibleObjectDefinition
{
	/// <inheritdoc/>
	private protected override String ToStringFormat => "{{ Field: {0} Descriptor: {1} }}";

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	/// <param name="signature">Signature field.</param>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private protected JFieldDefinition(ReadOnlySpan<Byte> name, ReadOnlySpan<Byte> signature) : base(
		AccessibleInfoSequence.CreateFieldInfo(name, signature)) { }
	/// <inheritdoc/>
	private protected JFieldDefinition(JFieldDefinition definition) : base(definition) { }
	/// <inheritdoc/>
	private protected JFieldDefinition(AccessibleInfoSequence info) : base(info) { }

	/// <summary>
	/// Retrieves a <see cref="JFieldObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JFieldObject"/> instance.</returns>
	public JFieldObject GetReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedField(this, declaringClass, false);
	}
	/// <summary>
	/// Retrieves a <see cref="JFieldObject"/> reflected from current static definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JFieldObject"/> instance.</returns>
	public JFieldObject GetStaticReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedField(this, declaringClass, true);
	}

	/// <inheritdoc/>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public sealed override String ToTraceText() => $"field: {this.Name} {this.Descriptor}";
}

/// <summary>
/// This class stores a class field definition.
/// </summary>
/// <typeparam name="TField"><see cref="IDataType"/> type of field result.</typeparam>
public sealed class JFieldDefinition<TField> : JFieldDefinition where TField : IDataType<TField>, IObject
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	public JFieldDefinition(ReadOnlySpan<Byte> name) : base(name, IDataType.GetMetadata<TField>().Signature) { }
	/// <inheritdoc/>
	internal JFieldDefinition(JFieldDefinition definition) : base(definition) { }
	/// <inheritdoc/>
	internal JFieldDefinition(AccessibleInfoSequence info) : base(info) { }

	/// <summary>
	/// Retrieves the value of a field on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jLocal"/> field's value.</returns>
	public TField? Get(JLocalObject jLocal, JClassObject? jClass = default)
	{
		IEnvironment env = jLocal.Environment;
		return env.AccessFeature.GetField<TField>(jLocal, jClass ?? jLocal.Class, this);
	}
	/// <summary>
	/// Retrieves the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>The <paramref name="jClass"/> field's value.</returns>
	public TField? StaticGet(JClassObject jClass)
	{
		IEnvironment env = jClass.Environment;
		return env.AccessFeature.GetStaticField<TField>(jClass, this);
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
		env.AccessFeature.SetField(jLocal, jClass ?? jLocal.Class, this, value);
	}
	/// <summary>
	/// Sets the value of a static field on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSet(JClassObject jClass, TField? value)
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
	public TField? GetReflected(JFieldObject jField, JLocalObject jLocal)
	{
		IEnvironment env = jField.Environment;
		return env.AccessFeature.GetField<TField>(jField, jLocal, this);
	}
	/// <summary>
	/// Retrieves the value of a reflected static field reflected which matches with current definition.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <returns>The static field's value.</returns>
	public TField? StaticGetReflected(JFieldObject jField)
	{
		IEnvironment env = jField.Environment;
		return env.AccessFeature.GetStaticField<TField>(jField, this);
	}
	/// <summary>
	/// Sets the value of a reflected field which matches with current definition.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void SetReflected(JFieldObject jField, JLocalObject jLocal, TField? value)
	{
		IEnvironment env = jField.Environment;
		env.AccessFeature.SetField(jField, jLocal, this, value);
	}
	/// <summary>
	/// Sets the value of a reflected static field which matches with current definition.
	/// </summary>
	/// <param name="jField">A <see cref="JFieldObject"/> instance.</param>
	/// <param name="value">The field value to set to.</param>
	public void StaticSetReflected(JFieldObject jField, TField? value)
	{
		IEnvironment env = jField.Environment;
		env.AccessFeature.SetStaticField(jField, this, value);
	}
}