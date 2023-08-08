namespace Rxmxnx.JNetInterface.Lang;

public partial class JClassObject
{
	/// <summary>
	/// This record stores the metadata of a <see cref="JClassObject"/> in order to create a
	/// <see cref="JGlobalBase"/> instance.
	/// </summary>
	private sealed record JClassObjectMetadata : JObjectMetadata
	{
		/// <summary>
		/// Class name of current object.
		/// </summary>
		public CString Name { get; init; }
		/// <summary>
		/// Class signature of current object.
		/// </summary>
		public CString ClassSignature { get; init; }
		/// <summary>
		/// Class hash of current object.
		/// </summary>
		public String Hash { get; }
		/// <summary>
		/// Indicates whether the class of current object is final.
		/// </summary>
		public Boolean? IsFinal { get; init; }

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="metadata"><see cref="JObjectMetadata"/> instance.</param>
		public JClassObjectMetadata(JObjectMetadata metadata) : base(metadata)
		{
			JClassObjectMetadata? classMetadata = metadata as JClassObjectMetadata;
			this.Name = classMetadata?.Name!;
			this.ClassSignature = classMetadata?.ClassSignature!;
			this.Hash = classMetadata?.Hash!;
		}
	}
}