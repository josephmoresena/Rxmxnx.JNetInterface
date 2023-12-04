namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
	/// <summary>
	/// Function name of <c>java.lang.Enum.ordinal().</c>
	/// </summary>
	private static readonly CString ordinalName = new(() => "ordinal"u8);
	/// <summary>
	/// Function name of <c>java.lang.Enum.name().</c>
	/// </summary>
	private static readonly CString nameName = new(() => "name"u8);
	/// <summary>
	/// String of enum value.
	/// </summary>
	private String? _name;

	/// <summary>
	/// Ordinal of enum value.
	/// </summary>
	private Int32? _ordinal;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal"><see cref="JLocalObject"/> instance.</param>
	private JEnumObject(JLocalObject jLocal) : base(
		jLocal.ForExternalUse(out JClassObject jClass, out JObjectMetadata metadata), jClass)
	{
		if (metadata is JEnumObjectMetadata enumMetadata)
		{
			this._ordinal ??= enumMetadata.Ordinal;
			this._name ??= enumMetadata.Name;
		}
	}

	/// <summary>
	/// Returns the ordinal of current instance.
	/// </summary>
	/// <returns>The ordinal of current instance.</returns>
	private Int32 GetOrdinal()
	{
		JPrimitiveFunctionDefinition definition =
			JPrimitiveFunctionDefinition.CreateIntDefinition(JEnumObject.ordinalName);
		return JPrimitiveFunctionDefinition.Invoke<Int32>(definition, this);
	}
}