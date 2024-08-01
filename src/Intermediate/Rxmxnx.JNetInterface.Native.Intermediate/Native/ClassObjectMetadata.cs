namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
/// <see cref="JGlobalBase"/> instance.
/// </summary>
public sealed partial record ClassObjectMetadata : ObjectMetadata
{
	/// <summary>
	/// <see cref="ClassObjectMetadata"/> instance for Java <c>void</c> type.
	/// </summary>
	public static readonly ClassObjectMetadata VoidMetadata = new(JPrimitiveTypeMetadata.VoidMetadata);

	/// <summary>
	/// Class name of the current type.
	/// </summary>
	public CString Name { get; internal init; }
	/// <summary>
	/// Class signature of the current type.
	/// </summary>
	public CString ClassSignature { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is interface.
	/// </summary>
	public Boolean? IsInterface { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is enum.
	/// </summary>
	public Boolean? IsEnum { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is annotation.
	/// </summary>
	public Boolean? IsAnnotation { get; internal init; }
	/// <summary>
	/// Array type dimension.
	/// </summary>
	public Int32? ArrayDimension { get; internal init; }
	/// <summary>
	/// Indicates whether the class of the current type is final.
	/// </summary>
	public Boolean? IsFinal { get; internal init; }
	/// <summary>
	/// Class hash of the current type.
	/// </summary>
	public String Hash { get; internal init; }

	/// <inheritdoc/>
	public override String ToTraceText()
		=> $"name: {this.Name} signature: {this.ClassSignature} final: {this.IsFinal} interface: {this.IsInterface} enum: {this.IsEnum} annotation: {this.IsAnnotation} hash: {ITypeInformation.GetPrintableHash(this.Hash, out String lastChar)}{lastChar}";

	/// <summary>
	/// Creates a <see cref="ClassObjectMetadata"/> for given <typeparamref name="TDataType"/> type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IReferenceType"/> type.</typeparam>
	/// <returns>A <see cref="ClassObjectMetadata"/> instance.</returns>
	public static ClassObjectMetadata Create<TDataType>() where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata metadata = IDataType.GetMetadata<TDataType>();
		return new(metadata);
	}
}