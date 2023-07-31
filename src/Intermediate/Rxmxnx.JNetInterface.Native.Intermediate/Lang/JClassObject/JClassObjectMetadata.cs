namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private sealed record JClassObjectMetadata : JObjectMetadata
	{
		/// <inheritdoc cref="IClass.Name"/>
		public CString Name { get; init; }
		/// <inheritdoc cref="IClass.ClassSignature"/>
		public CString ClassSignature { get; init; }
		/// <inheritdoc cref="IClass.Hash"/>
		public String Hash { get; }
		/// <inheritdoc cref="IClass.IsFinal"/>
		public Boolean? IsFinal { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
		public JClassObjectMetadata(JObjectMetadata metadata) : base(metadata)
		{
			this.Name = default!;
			this.ClassSignature = default!;
			this.Hash = default!;
		}
	}
}