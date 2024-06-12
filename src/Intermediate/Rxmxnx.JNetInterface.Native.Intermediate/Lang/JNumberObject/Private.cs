namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JClassTypeMetadata<JNumberObject>;

public partial class JNumberObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JNumberObject>
	                                                    .Create("java/lang/Number"u8, JTypeModifier.Abstract)
	                                                    .Implements<JSerializableObject>().Build();

	static TypeMetadata IClassType<JNumberObject>.Metadata => JNumberObject.typeMetadata;

	static JNumberObject IClassType<JNumberObject>.Create(IReferenceType.ClassInitializer initializer)
		=> new(initializer);
	static JNumberObject IClassType<JNumberObject>.Create(IReferenceType.ObjectInitializer initializer)
		=> new(initializer);
	static JNumberObject IClassType<JNumberObject>.Create(IReferenceType.GlobalInitializer initializer)
		=> new(initializer);
}