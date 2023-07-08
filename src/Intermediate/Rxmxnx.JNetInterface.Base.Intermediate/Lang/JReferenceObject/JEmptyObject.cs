namespace Rxmxnx.JNetInterface.Lang;

public abstract partial class JReferenceObject 
{
	/// <summary>
	/// This class represents a java null-reference type instance.
	/// </summary>
	private sealed class JEmptyObject : JReferenceObject
	{
		/// <inheritdoc/>
		public override CString ObjectClassName => JObject.JObjectClassName;
		/// <inheritdoc/>
		public override CString ObjectSignature => JObject.JObjectSignature;
	
		/// <summary>
		/// Parameterless constructor.
		/// </summary>
		public JEmptyObject() : base(false) { }
	}
}