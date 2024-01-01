namespace Rxmxnx.JNetInterface.Lang;

public partial class JEnumObject
{
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
		if (metadata is not JEnumObjectMetadata enumMetadata) return;
		this._ordinal ??= enumMetadata.Ordinal;
		this._name ??= enumMetadata.Name;
	}
}