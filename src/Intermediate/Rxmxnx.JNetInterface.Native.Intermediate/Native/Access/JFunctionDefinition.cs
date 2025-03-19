namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a function definition.
/// </summary>
public abstract partial class JFunctionDefinition : JCallDefinition
{
	/// <summary>
	/// Internal constructor.
	/// </summary>
	/// <param name="functionName">Method defined name.</param>
	/// <param name="returnTypeSignature">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	private protected JFunctionDefinition(ReadOnlySpan<Byte> functionName, ReadOnlySpan<Byte> returnTypeSignature,
		ReadOnlySpan<JArgumentMetadata> metadata = default) : base(functionName, returnTypeSignature, metadata) { }
	/// <inheritdoc/>
	private protected JFunctionDefinition(JFunctionDefinition definition) : base(definition) { }
	/// <inheritdoc/>
	private protected JFunctionDefinition(AccessibleInfoSequence info, Int32 callSize, Int32[] sizes,
		Int32 referenceCount) : base(info, callSize, sizes, referenceCount) { }

	/// <summary>
	/// Retrieves a <see cref="JMethodObject"/> reflected from current definition on
	/// <paramref name="declaringClass"/>.
	/// </summary>
	/// <param name="declaringClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JMethodObject"/> instance.</returns>
	public JMethodObject GetReflected(JClassObject declaringClass)
	{
		IEnvironment env = declaringClass.Environment;
		return env.AccessFeature.GetReflectedFunction(this, declaringClass, false);
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
		return env.AccessFeature.GetReflectedFunction(this, declaringClass, true);
	}
}

/// <summary>
/// This class stores a function definition.
/// </summary>
/// <typeparam name="TResult"><see cref="IDataType"/> type of function result.</typeparam>
public partial class JFunctionDefinition<TResult> : JFunctionDefinition where TResult : IDataType<TResult>
{
	/// <summary>
	/// Internal Constructor.
	/// </summary>
	/// <param name="definition">Function definition name.</param>
	internal JFunctionDefinition(JFunctionDefinition definition) : base(definition) { }
	/// <summary>
	/// Internal Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="returnTypeSignature">Method return type defined signature.</param>
	/// <param name="metadata">Metadata of the types of call arguments.</param>
	private protected JFunctionDefinition(ReadOnlySpan<Byte> functionName, ReadOnlySpan<Byte> returnTypeSignature,
		ReadOnlySpan<JArgumentMetadata> metadata) : base(functionName, returnTypeSignature, metadata) { }
	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <remarks>This constructor should be never inherited.</remarks>
	private JFunctionDefinition(ReadOnlySpan<Byte> functionName) : base(functionName,
	                                                                    IDataType.GetMetadata<TResult>().Signature) { }
	/// <inheritdoc/>
	private JFunctionDefinition(AccessibleInfoSequence info, Int32 callSize, Int32[] sizes, Int32 referenceCount) :
		base(info, callSize, sizes, referenceCount) { }
}