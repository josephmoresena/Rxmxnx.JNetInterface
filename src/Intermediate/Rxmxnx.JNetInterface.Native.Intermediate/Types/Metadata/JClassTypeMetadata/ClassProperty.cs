namespace Rxmxnx.JNetInterface.Types.Metadata;

public partial class JClassTypeMetadata
{
	/// <summary>
	/// Class metadata property.
	/// </summary>
	private protected sealed class ClassProperty : IAppendableProperty
	{
		/// <summary>
		/// Property value:
		/// </summary>
		public String? Value { get; init; }
		/// <inheritdoc/>
		public String PropertyName { get; init; } = default!;
		/// <inheritdoc/>
		public void AppendValue(StringBuilder stringBuilder) => stringBuilder.Append(this.Value);
	}
}