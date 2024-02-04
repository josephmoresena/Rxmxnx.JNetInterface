namespace Rxmxnx.JNetInterface.Native.Access;

public partial record JMethodDefinition
{
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jLocal"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance that <paramref name="jLocal"/> class extends.</param>
	/// <param name="nonVirtual">Indicates whether current call must be non-virtual.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void Invoke(JMethodDefinition definition, JLocalObject jLocal, JClassObject? jClass = default,
		Boolean nonVirtual = false, IObject?[]? args = default)
	{
		IEnvironment env = jLocal.Environment;
		env.AccessFeature.CallInternalMethod(jLocal, jClass ?? jLocal.Class, definition, nonVirtual,
		                                     args ?? definition.CreateArgumentsArray());
	}
	/// <summary>
	/// Invokes <paramref name="definition"/> on <paramref name="jClass"/> which matches with current definition.
	/// </summary>
	/// <param name="definition">A <see cref="JMethodDefinition"/> definition.</param>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="args">The arguments to pass to.</param>
	internal static void StaticInvoke(JMethodDefinition definition, JClassObject jClass, IObject?[]? args = default)
	{
		IEnvironment env = jClass.Environment;
		env.AccessFeature.CallInternalStaticMethod(jClass, definition, args ?? definition.CreateArgumentsArray());
	}

	/// <summary>
	/// Create a <see cref="JMethodDefinition"/> instance for <paramref name="metadata"/>.
	/// </summary>
	/// <param name="methodName">Method name.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	/// <returns>A <see cref="JMethodDefinition"/> instance.</returns>
	public static JMethodDefinition Create(ReadOnlySpan<Byte> methodName, JArgumentMetadata[] metadata)
		=> new(methodName, metadata);
}