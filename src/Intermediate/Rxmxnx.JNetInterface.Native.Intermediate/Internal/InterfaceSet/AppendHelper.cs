namespace Rxmxnx.JNetInterface.Internal;

internal partial class InterfaceSet : IAppendableProperty
{
	/// <inheritdoc/>
	public String PropertyName => nameof(JReferenceTypeMetadata.Interfaces);

	/// <inheritdoc/>
	public void AppendValue(StringBuilder stringBuilder)
	{
		stringBuilder.Append(MetadataTextUtilities.OpenArray);
		this.ForEach(new AppendHelper(stringBuilder), AppendHelper.Append);
		stringBuilder.Append(MetadataTextUtilities.CloseArray);
	}

	/// <summary>
	/// Helper class to append value.
	/// </summary>
	private sealed class AppendHelper(StringBuilder stringBuilder)
	{
		/// <summary>
		/// Current <see cref="StringBuilder"/> instance.
		/// </summary>
		private readonly StringBuilder _stringBuilder = stringBuilder;

		/// <summary>
		/// Indicates whether current interface is the first one.
		/// </summary>
		private Boolean _first = true;

		/// <summary>
		/// Appends <paramref name="metadata"/> name to internal <see cref="StringBuilder"/> instance.
		/// </summary>
		/// <param name="helper">A <see cref="AppendHelper"/> instance.</param>
		/// <param name="metadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		public static void Append(AppendHelper helper, JInterfaceTypeMetadata metadata)
		{
			if (!helper._first)
				helper._stringBuilder.Append(MetadataTextUtilities.Separator);

			helper._first = false;
			helper._stringBuilder.Append($"{metadata.ClassName}");
		}
	}
}