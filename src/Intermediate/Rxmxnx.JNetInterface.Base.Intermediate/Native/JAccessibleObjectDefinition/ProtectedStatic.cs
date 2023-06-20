namespace Rxmxnx.JNetInterface.Native;

public partial record JAccessibleObjectDefinition
{
	/// <summary>
	/// Retrieves the return type from <typeparamref name="TReturn"/>.
	/// </summary>
	/// <typeparam name="TReturn"><see cref="IDataType"/> type.</typeparam>
	/// <returns>Type of return <typeparamref name="TReturn"/> type.</returns>
	protected static Type GetReturnType<TReturn>() where TReturn : IDataType
		=> TReturn.PrimitiveMetadata?.Type ?? typeof(TReturn);
}