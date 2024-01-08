namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
/// <see cref="JEnumObject"/> instance.
/// </summary>
public record EnumObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// Internal ordinal.
	/// </summary>
	public Int32? Ordinal { get; init; }
	/// <summary>
	/// Internal string name.
	/// </summary>
	public String? Name { get; init; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="ObjectMetadata"/> instance.</param>
	internal EnumObjectMetadata(ObjectMetadata metadata) : base(metadata)
	{
		if (metadata is not EnumObjectMetadata enumMetadata)
			return;
		this.Ordinal = enumMetadata.Ordinal;
		this.Name = enumMetadata.Name;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="metadata"><see cref="EnumObjectMetadata"/> instance.</param>
	protected EnumObjectMetadata(EnumObjectMetadata metadata) : base(metadata)
	{
		this.Ordinal = metadata.Ordinal;
		this.Name = metadata.Name;
	}
}