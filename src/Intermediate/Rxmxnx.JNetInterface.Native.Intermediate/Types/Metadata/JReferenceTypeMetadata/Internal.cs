namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial class JReferenceTypeMetadata
{
	/// <summary>
	/// Interfaces properties.
	/// </summary>
	private protected IAppendableProperty? InterfaceProperties => this.Interfaces as IAppendableProperty;

	/// <summary>
	/// Retrieves the class for current data type.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>A <see cref="JClassObject"/> instance.</returns>
	internal abstract JClassObject GetClass(IEnvironment env);
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="localRef"/> using
	/// <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="realClass">Indicates whether <paramref name="jClass"/> is instance real class.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="localRef"/> and <paramref name="jClass"/>.</returns>
	internal abstract JLocalObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
		Boolean realClass = false);
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jLocal"/>.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	/// <param name="dispose">
	/// Indicates whether current instance should be disposed after casting.
	/// </param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jLocal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jLocal))]
	internal abstract JReferenceObject? ParseInstance(JLocalObject? jLocal, Boolean dispose = false);
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jGlobal"/> and
	/// <paramref name="env"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jGlobal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jGlobal))]
	internal abstract JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal);

	/// <summary>
	/// Creates an exception instance from a <see cref="JGlobalBase"/> throwable instance.
	/// </summary>
	/// <param name="jGlobalThrowable">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <param name="exceptionMessage">Exception message.</param>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	internal virtual ThrowableException? CreateException(JGlobalBase jGlobalThrowable,
		String? exceptionMessage = default)
		=> default;

	/// <summary>
	/// Creates a <see cref="JFunctionDefinition"/> instance from <paramref name="functionName"/> and
	/// <paramref name="paramsMetadata"/>.
	/// </summary>
	/// <param name="functionName">Function name.</param>
	/// <param name="paramsMetadata">Metadata of the types of call arguments.</param>
	/// <returns>A new <see cref="JFunctionDefinition"/> instance.</returns>
	internal abstract JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		ReadOnlySpan<JArgumentMetadata> paramsMetadata);
	/// <summary>
	/// Creates a <see cref="JFunctionDefinition"/> instance from <paramref name="fieldName"/>.
	/// </summary>
	/// <param name="fieldName">Field name.</param>
	/// <returns>A new <see cref="JFieldDefinition"/> instance.</returns>
	internal abstract JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName);
}