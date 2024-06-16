namespace Rxmxnx.JNetInterface.Native.Access;

public sealed partial class JArgumentMetadata
{
	/// <summary>
	/// Creates a <see cref="IAppendableProperty"/> instance for current instance.
	/// </summary>
	/// <param name="propertyName">Name of property.</param>
	/// <returns>A <see cref="IAppendableProperty"/> instance.</returns>
	internal IAppendableProperty ToAppendableProperty(String propertyName) => new Appendable(propertyName, this);

	/// <summary>
	/// Appendable argument type.
	/// </summary>
	/// <param name="propertyName">Property name.</param>
	/// <param name="argumentMetadata">A <see cref="JArgumentMetadata"/> instance.</param>
	private sealed class Appendable(String propertyName, JArgumentMetadata argumentMetadata) : IAppendableProperty
	{
		/// <summary>
		/// Current instance.
		/// </summary>
		private readonly JArgumentMetadata _instance = argumentMetadata;

		/// <inheritdoc/>
		public String PropertyName { get; } = propertyName;

		/// <inheritdoc/>
		public void AppendValue(StringBuilder stringBuilder)
		{
			stringBuilder.Append(MetadataTextUtilities.OpenObject);
			stringBuilder.Append(nameof(JArgumentMetadata.Signature));
			stringBuilder.Append(MetadataTextUtilities.EqualsText);
			stringBuilder.Append($"{this._instance.Signature}");
			stringBuilder.Append(MetadataTextUtilities.Separator);
			stringBuilder.Append(nameof(JArgumentMetadata.Size));
			stringBuilder.Append(MetadataTextUtilities.EqualsText);
			stringBuilder.Append($"{this._instance.Size}");
			stringBuilder.Append(MetadataTextUtilities.CloseObject);
		}
	}
}