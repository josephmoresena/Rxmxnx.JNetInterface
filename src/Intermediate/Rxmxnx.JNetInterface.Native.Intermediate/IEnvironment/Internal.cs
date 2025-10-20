namespace Rxmxnx.JNetInterface;

public partial interface IEnvironment
{
	/// <summary>
	/// Accessing feature.
	/// </summary>
	internal IAccessFeature AccessFeature { get; }
	/// <summary>
	/// Classing feature.
	/// </summary>
	internal IClassFeature ClassFeature { get; }
	/// <summary>
	/// Referencing feature.
	/// </summary>
	internal IReferenceFeature ReferenceFeature { get; }
	/// <summary>
	/// String feature.
	/// </summary>
	internal IStringFeature StringFeature { get; }
	/// <summary>
	/// Array feature.
	/// </summary>
	internal IArrayFeature ArrayFeature { get; }
	/// <summary>
	/// Native I/O feature.
	/// </summary>
	internal INioFeature NioFeature { get; }
	/// <summary>
	/// Function cache.
	/// </summary>
	internal NativeFunctionSet FunctionSet { get; }
	/// <summary>
	/// Indicates whether the current instance is not a proxy.
	/// </summary>
	internal Boolean NoProxy { get; }

	/// <summary>
	/// Indicates whether validation of <paramref name="jGlobal"/> can be avoided.
	/// </summary>
	/// <param name="jGlobal">A <see cref="JGlobalBase"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jGlobal"/> validation can be avoided;
	/// otherwise, <see langword="false"/>;
	/// </returns>
	internal Boolean IsValidationAvoidable(JGlobalBase jGlobal);
}