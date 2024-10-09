namespace Rxmxnx.JNetInterface.Tests;

public sealed partial class TypeMetadataBuilderTests
{
	public sealed class JStringBuilderObject : JLocalObject, IClassType<JStringBuilderObject>,
		IInterfaceObject<JSerializableObject>, IInterfaceObject<JAppendableObject>,
		IInterfaceObject<JCharSequenceObject>, IInterfaceObject<JComparableObject>
	{
		private static readonly JClassTypeMetadata<JStringBuilderObject> typeMetadata =
			TypeMetadataBuilder<JStringBuilderObject>
				.Create(TypeMetadataBuilderTests.classNames[typeof(JStringBuilderObject)])
				.Implements<JSerializableObject>().Implements<JAppendableObject>().Implements<JCharSequenceObject>()
				.Implements<JComparableObject>().Build();
		static JClassTypeMetadata<JStringBuilderObject> IClassType<JStringBuilderObject>.Metadata
			=> JStringBuilderObject.typeMetadata;
		private JStringBuilderObject(IReferenceType.ClassInitializer initializer) : base(initializer) { }
		private JStringBuilderObject(IReferenceType.GlobalInitializer initializer) : base(initializer) { }
		private JStringBuilderObject(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static JStringBuilderObject IClassType<JStringBuilderObject>.Create(IReferenceType.ClassInitializer initializer)
			=> new(initializer);
		static JStringBuilderObject IClassType<JStringBuilderObject>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
		static JStringBuilderObject IClassType<JStringBuilderObject>.Create(
			IReferenceType.GlobalInitializer initializer)
			=> new(initializer);
	}
}