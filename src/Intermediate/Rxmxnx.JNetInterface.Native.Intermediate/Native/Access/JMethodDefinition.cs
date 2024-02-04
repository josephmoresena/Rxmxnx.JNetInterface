namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a method definition.
/// </summary>
public partial record JMethodDefinition : JCallDefinition
{
	/// <inheritdoc/>
	internal override Type? Return => default;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <remarks>This constructor should be never inherited.</remarks>
	public JMethodDefinition(ReadOnlySpan<Byte> methodName) : base(methodName) { }

	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JMethodObject"/> instance.</returns>
	public JMethodObject GetReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedMethod(this, declaringClass, false);
	}
	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current static definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JMethodObject"/> instance.</returns>
	public JMethodObject GetStaticReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedMethod(this, declaringClass, true);
	}

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
	/// <inheritdoc/>
	public override Int32 GetHashCode() => base.GetHashCode();
}