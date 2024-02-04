namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial record JReferenceTypeMetadata
{
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
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jLocal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jLocal))]
	internal abstract JLocalObject? ParseInstance(JLocalObject? jLocal);
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jGlobal"/> and
	/// <paramref name="env"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jGlobal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jGlobal))]
	internal abstract JLocalObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal);

	/// <inheritdoc cref="IReflectionMetadata.CreateFunctionDefinition(ReadOnlySpan{Byte}, JArgumentMetadata[])"/>
	internal abstract JFunctionDefinition CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] metadata);
	/// <inheritdoc cref="IReflectionMetadata.CreateFieldDefinition(ReadOnlySpan{Byte})"/>
	internal abstract JFieldDefinition CreateFieldDefinition(ReadOnlySpan<Byte> fieldName);

	/// <summary>
	/// Creates a <see cref="JArrayTypeMetadata"/> from current instance.
	/// </summary>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	internal abstract JArrayTypeMetadata? GetArrayMetadata();
}