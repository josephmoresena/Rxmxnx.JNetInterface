namespace Rxmxnx.JNetInterface.Native;

/// <summary>
/// This class stores a class field definition.
/// </summary>
public abstract record JFieldDefinition : JAccessibleObjectDefinition
{
	/// <inheritdoc/>
	internal override String ToStringFormat => "xField: {0} Descriptor: {1}";

	/// <summary>
	/// Return type.
	/// </summary>
	internal abstract Type Return { get; }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	/// <param name="signature">Signature field.</param>
	internal JFieldDefinition(CString name, CString signature) : base(new CStringSequence(name, signature)) { }
}

/// <summary>
/// This class stores a class field definition.
/// </summary>
/// <typeparam name="TField"><see cref="IDataType"/> type of field result.</typeparam>
public sealed record FieldDefinition<TField> : JFieldDefinition where TField : IDataType<TField>, IObject
{
	/// <inheritdoc/>
	internal override Type Return => JAccessibleObjectDefinition.GetReturnType<TField>();

	/// <inheritdoc/>
	public override String ToString() => base.ToString();
	/// <inheritdoc/>
	public override Int32 GetHashCode() => base.GetHashCode();

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name">Field name.</param>
	public FieldDefinition(CString name) : base(name, TField.Signature) { }
}