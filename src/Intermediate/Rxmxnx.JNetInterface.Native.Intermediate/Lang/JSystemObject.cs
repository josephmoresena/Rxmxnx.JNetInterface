namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JSystemObject>;

/// <summary>
/// This class represents a local <c>java.lang.System</c> instance.
/// </summary>
public sealed class JSystemObject : JLocalObject.Uninstantiable<JSystemObject>, IUninstantiableType<JSystemObject>
{
	/// <summary>
	/// Datatype information.
	/// </summary>
	private static readonly TypeInfoSequence typeInfo = new(ClassNameHelper.SystemHash, 16);
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata =
		JLocalObject.CreateBuiltInMetadata<JSystemObject>(JSystemObject.typeInfo, JTypeModifier.Final);

	static TypeMetadata IClassType<JSystemObject>.Metadata => JSystemObject.typeMetadata;
	static JRuntimeVersion IDataType.Since => JRuntimeVersion.SEd0;

	//TODO: GET/SET Property functions
}