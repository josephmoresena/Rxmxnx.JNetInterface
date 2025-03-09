namespace Rxmxnx.JNetInterface.Native.Access;

public partial class JMethodDefinition
{
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <remarks>This constructor should be never inherited.</remarks>
	private JMethodDefinition(ReadOnlySpan<Byte> methodName) : base(methodName, []) { }
	/// <inheritdoc/>
	private protected JMethodDefinition(AccessibleInfoSequence info, Int32 callSize, Int32[] sizes,
		Int32 referenceCount) : base(info, callSize, sizes, referenceCount) { }

	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void Invoke(JMethodDefinition definition, JLocalObject jLocal, JClassObject? jClass = default,
		Boolean nonVirtual = false, ReadOnlySpan<IObject?> args = default)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallInternalMethod(jLocal, jClass ?? jLocal.Class, definition, nonVirtual, args);
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void StaticInvoke(JMethodDefinition definition, JClassObject jClass,
		ReadOnlySpan<IObject?> args = default)
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallInternalStaticMethod(jClass, definition, args);
	}
	/// <summary>
	/// Create a <see cref="JMethodDefinition"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JMethodDefinition"/> instance.</returns>
	internal static JMethodDefinition Create(ReadOnlySpan<Byte> methodName,
		ReadOnlySpan<JArgumentMetadata> metadata = default)
		=> metadata.Length > 0 ? new JMethodDefinition(methodName, metadata) : new Parameterless(methodName);
}