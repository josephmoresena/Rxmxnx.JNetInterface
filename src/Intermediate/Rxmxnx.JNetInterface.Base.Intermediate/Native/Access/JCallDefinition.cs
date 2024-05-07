namespace Rxmxnx.JNetInterface.Native.Access;

/// <summary>
/// This class stores a java call definition.
/// </summary>
[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract partial class JCallDefinition : JAccessibleObjectDefinition
{
	/// <summary>
	/// Total size in bytes of call parameters.
	/// </summary>
	public Int32 Size => this._callSize;
	/// <summary>
	/// Total count of call parameters.
	/// </summary>
	public Int32 Count => this._sizes.Length;

	/// <summary>
	/// List of size in bytes of each call argument.
	/// </summary>
	internal IReadOnlyList<Int32> Sizes => this._sizes;
	/// <summary>
	/// Count of reference parameters.
	/// </summary>
	internal Int32 ReferenceCount => this._referenceCount;

	/// <inheritdoc/>
	private protected override String ToStringFormat => "{{ Method: {0} Descriptor: {1} }}";

	/// <summary>
	/// Creates the argument array for the current call.
	/// </summary>
	/// <returns>A new array to be used as argument for the current call.</returns>
	protected IObject?[] CreateArgumentsArray()
		=> this._sizes.Length != 0 ? new IObject?[this._sizes.Length] : Array.Empty<IObject?>();
}