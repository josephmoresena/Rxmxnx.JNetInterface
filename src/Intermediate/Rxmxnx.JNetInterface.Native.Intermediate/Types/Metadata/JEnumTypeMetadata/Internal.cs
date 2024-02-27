namespace Rxmxnx.JNetInterface.Types.Metadata;

public abstract partial record JEnumTypeMetadata
{
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="localRef"/> using
	/// <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="localRef">A <see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="realClass">Indicates whether <paramref name="jClass"/> is instance real class.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="localRef"/> and <paramref name="jClass"/>.</returns>
	internal abstract JEnumObject CreateInstance(JClassObject jClass, JObjectLocalRef localRef,
		Boolean realClass = false);
	/// <summary>
	/// Creates a <see cref="IDataType"/> instance from <paramref name="jGlobal"/> and
	/// <paramref name="env"/>.
	/// </summary>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>A <see cref="IDataType"/> instance from <paramref name="jGlobal"/>.</returns>
	[return: NotNullIfNotNull(nameof(jGlobal))]
	internal abstract JEnumObject? ParseInstance(IEnvironment env, JGlobalBase? jGlobal);
}