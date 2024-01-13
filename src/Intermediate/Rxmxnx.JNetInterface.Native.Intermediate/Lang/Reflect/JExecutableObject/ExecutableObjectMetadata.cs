namespace Rxmxnx.JNetInterface.Reflect;

public partial class JExecutableObject
{
	/// <summary>
	/// 
	/// </summary>
	protected record ExecutableObjectMetadata
	{
		/// <summary>
		/// Instance definition.
		/// </summary>
		public JCallDefinition? Definition { get; }
		
		//public ExecutableObjectMetadata()
	}
}