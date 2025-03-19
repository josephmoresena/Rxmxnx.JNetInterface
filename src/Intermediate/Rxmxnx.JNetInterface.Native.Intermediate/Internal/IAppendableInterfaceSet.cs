namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Appendable interface metadata set.
/// </summary>
internal interface IAppendableInterfaceSet : IInterfaceSet, IAppendableProperty
{
	String IAppendableProperty.PropertyName => nameof(JReferenceTypeMetadata.Interfaces);

	void IAppendableProperty.AppendValue(StringBuilder stringBuilder)
	{
		AppendHelper helper = new(stringBuilder);
		MetadataTextUtilities.AppendArrayBegin(stringBuilder);
		this.ForEach(helper, AppendHelper.Append);
		MetadataTextUtilities.AppendArrayEnd(stringBuilder, helper.First);
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
		public Boolean First { get; private set; } = true;

		/// <summary>
		/// Appends <paramref name="metadata"/> name to internal <see cref="StringBuilder"/> instance.
		/// </summary>
		/// <param name="helper">A <see cref="AppendHelper"/> instance.</param>
		/// <param name="metadata">A <see cref="JInterfaceTypeMetadata"/> instance.</param>
		public static void Append(AppendHelper helper, JInterfaceTypeMetadata metadata)
		{
			MetadataTextUtilities.AppendItem(helper._stringBuilder, ITypeInformation.GetJavaClassName(metadata),
			                                 helper.First);
			helper.First = false;
		}
	}
}