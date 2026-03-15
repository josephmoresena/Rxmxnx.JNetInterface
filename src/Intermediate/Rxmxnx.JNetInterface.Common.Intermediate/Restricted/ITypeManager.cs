namespace Rxmxnx.JNetInterface.Restricted;

internal interface ITypeManager
{
	/// <summary>
	/// Indicates whether final user-types should be treated as real classes at runtime.
	/// </summary>
	static abstract Boolean FinalUserTypeRuntimeEnabled { get; }

	/// <summary>
	/// Indicates whether <typeparamref name="TDataType"/> is compatible with current compilation.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current datatype is compatible with the current compilation; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	static abstract Boolean IsCompileCompliant<TDataType>() where TDataType : IDataType;
}