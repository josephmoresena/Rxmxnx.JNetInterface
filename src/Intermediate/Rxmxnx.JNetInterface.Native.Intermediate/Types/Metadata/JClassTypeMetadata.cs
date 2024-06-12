namespace Rxmxnx.JNetInterface.Types.Metadata;

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract class JClassTypeMetadata : JReferenceTypeMetadata
{
	/// <inheritdoc/>
	public override JTypeKind Kind => JTypeKind.Class;

	/// <summary>
	/// Base class name.
	/// </summary>
	public CString? BaseClassName => this.BaseMetadata?.ClassName;

	/// <inheritdoc/>
	private protected JClassTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	private protected JClassTypeMetadata(CStringSequence information) : base(information) { }

	/// <inheritdoc/>
	public override String ToString()
		=> $"{base.ToString()}{nameof(JDataTypeMetadata.Modifier)} = {this.Modifier}, " +
			$"{nameof(JClassTypeMetadata.BaseClassName)} = {this.BaseClassName}, ";
}

/// <summary>
/// This record stores the metadata for a class <see cref="IDataType"/> type.
/// </summary>
public abstract partial class JClassTypeMetadata<
	[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] TClass> : JClassTypeMetadata
	where TClass : JLocalObject, IClassType<TClass>
{
	/// <inheritdoc/>
	public override Type Type => typeof(TClass);
	/// <inheritdoc/>
	public override JArgumentMetadata ArgumentMetadata => JArgumentMetadata.Get<TClass>();

	/// <inheritdoc/>
	private protected JClassTypeMetadata(ReadOnlySpan<Byte> className, ReadOnlySpan<Byte> signature) : base(
		className, signature) { }
	/// <inheritdoc/>
	private protected JClassTypeMetadata(CStringSequence information) : base(information) { }

	/// <inheritdoc/>
	public override JArrayTypeMetadata GetArrayMetadata() => JReferenceTypeMetadata.GetArrayMetadata<TClass>();

	/// <inheritdoc/>
	internal override Boolean IsInstance(JReferenceObject jObject) => jObject is TClass || jObject.InstanceOf<TClass>();
	/// <inheritdoc/>
	internal override JFunctionDefinition<TClass> CreateFunctionDefinition(ReadOnlySpan<Byte> functionName,
		JArgumentMetadata[] paramsMetadata)
		=> JFunctionDefinition<TClass>.Create(functionName, paramsMetadata);
	/// <inheritdoc/>
	internal override JFieldDefinition<TClass> CreateFieldDefinition(ReadOnlySpan<Byte> fieldName) => new(fieldName);
}