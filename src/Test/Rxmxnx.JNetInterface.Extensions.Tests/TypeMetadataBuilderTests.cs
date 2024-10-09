namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed partial class TypeMetadataBuilderTests
{
	private static readonly Dictionary<Type, CString> classNames = new()
	{
		{ typeof(JAssertionErrorObject), new("java/lang/AssertionError"u8) },
		{ typeof(JProcessHandleObject), new("java/lang/ProcessHandle"u8) },
		{ typeof(JOpenOptionObject), new("java/nio/file/OpenOption"u8) },
		{ typeof(JStringBuilderObject), new("java/lang/StringBuilder"u8) },
		{ typeof(JThreadLocalObject), new("java/lang/ThreadLocal"u8) },
		{ typeof(JFunctionalInterfaceObject), new("java/lang/FunctionalInterface"u8) },
		{ typeof(JUnsupportedOperationExceptionObject), new("java/lang/UnsupportedOperationException"u8) },
		{ typeof(JCloneNotSupportedExceptionObject), new("java/lang/CloneNotSupportedException"u8) },
		{ typeof(JThreadStateObject), new("java/lang/Thread$State"u8) },
		{ typeof(JStandardOpenOptionObject), new("java/nio/file/StandardOpenOption"u8) },
		{ typeof(JInheritableThreadLocalObject), new("java/lang/InheritableThreadLocal"u8) },
	};
	private static readonly Dictionary<Type, CString> classSignatures =
		TypeMetadataBuilderTests.classNames.ToDictionary(p => p.Key, p => CString.Concat("L"u8, p.Value, ";"u8));
	private static readonly Dictionary<Type, CString> arraySignatures =
		TypeMetadataBuilderTests.classSignatures.ToDictionary(p => p.Key, p => CString.Concat("["u8, p.Value));
	private static readonly Dictionary<Type, CStringSequence> hashes =
		TypeMetadataBuilderTests.classSignatures.Keys.ToDictionary(
			t => t,
			t => new CStringSequence(TypeMetadataBuilderTests.classNames[t],
			                         TypeMetadataBuilderTests.classSignatures[t],
			                         TypeMetadataBuilderTests.arraySignatures[t]));

	[Fact]
	internal void AssertionErrorTest()
		=> TypeMetadataBuilderTests.MetadataTest<JAssertionErrorObject>(IClassType.GetMetadata<JErrorObject>());
	[Fact]
	internal void ProcessHandleTest()
		=> TypeMetadataBuilderTests.MetadataTest<JProcessHandleObject>(
			default, IInterfaceType.GetMetadata<JComparableObject>());
	[Fact]
	internal void OpenOptionTest() => TypeMetadataBuilderTests.MetadataTest<JOpenOptionObject>(default);
	[Fact]
	internal void StringBuilderTest()
		=> TypeMetadataBuilderTests.MetadataTest<JStringBuilderObject>(IClassType.GetMetadata<JLocalObject>(),
		                                                               IInterfaceType
			                                                               .GetMetadata<JSerializableObject>(),
		                                                               IInterfaceType.GetMetadata<JAppendableObject>(),
		                                                               IInterfaceType
			                                                               .GetMetadata<JCharSequenceObject>(),
		                                                               IInterfaceType.GetMetadata<JComparableObject>());
	[Fact]
	internal void ThreadLocalTest()
		=> TypeMetadataBuilderTests.MetadataTest<JThreadLocalObject>(IClassType.GetMetadata<JLocalObject>());
	[Fact]
	internal void FunctionalInterfaceTest()
		=> TypeMetadataBuilderTests.MetadataTest<JFunctionalInterfaceObject>(
			default, IInterfaceType.GetMetadata<JAnnotationObject>());
	[Fact]
	internal void UnsupportedOperationExceptionTest()
		=> TypeMetadataBuilderTests.MetadataTest<JUnsupportedOperationExceptionObject>(
			IClassType.GetMetadata<JRuntimeExceptionObject>());
	[Fact]
	internal void CloneNotSupportedExceptionTest()
		=> TypeMetadataBuilderTests.MetadataTest<JCloneNotSupportedExceptionObject>(
			IClassType.GetMetadata<JExceptionObject>());
	[Fact]
	internal void ThreadStateTest()
		=> TypeMetadataBuilderTests.MetadataTest<JThreadStateObject>(IClassType.GetMetadata<JEnumObject>());
	[Fact]
	internal void StandardOpenOptionTest()
		=> TypeMetadataBuilderTests.MetadataTest<JStandardOpenOptionObject>(
			IClassType.GetMetadata<JEnumObject>(), IInterfaceType.GetMetadata<JOpenOptionObject>());
	[Fact]
	internal void InheritableThreadLocalTest()
		=> TypeMetadataBuilderTests.MetadataTest<JInheritableThreadLocalObject>(
			IClassType.GetMetadata<JThreadLocalObject>());

	private static void MetadataTest<TReferenceType>(JClassTypeMetadata? baseMetadata,
		params JInterfaceTypeMetadata[] interfaces)
		where TReferenceType : JReferenceObject, IReferenceType<TReferenceType>
	{
		JReferenceTypeMetadata typeMetadata = IReferenceType.GetMetadata<TReferenceType>();
		JTypeKind kind = typeMetadata is JEnumTypeMetadata ? JTypeKind.Enum :
			typeMetadata is JInterfaceTypeMetadata ?
				interfaces.Contains(IInterfaceType.GetMetadata<JAnnotationObject>()) ?
					JTypeKind.Annotation :
					JTypeKind.Interface : JTypeKind.Class;

		Assert.Equal(IntPtr.Size, typeMetadata.SizeOf);
		Assert.Equal(JArrayObject<TReferenceType>.Metadata, typeMetadata.GetArrayMetadata());
		Assert.Equal(typeof(TReferenceType), typeMetadata.Type);
		Assert.Equal(kind, typeMetadata.Kind);
		Assert.Equal(baseMetadata, typeMetadata.BaseMetadata);
		Assert.Equal(TypeMetadataBuilderTests.classNames[typeof(TReferenceType)], typeMetadata.ClassName);
		Assert.Equal(TypeMetadataBuilderTests.classSignatures[typeof(TReferenceType)], typeMetadata.Signature);
		Assert.Equal(TypeMetadataBuilderTests.arraySignatures[typeof(TReferenceType)], typeMetadata.ArraySignature);
		Assert.Equal(TypeMetadataBuilderTests.hashes[typeof(TReferenceType)].ToString(), typeMetadata.Hash);
		foreach (JInterfaceTypeMetadata interfaceTypeMetadata in interfaces)
			Assert.True(typeMetadata.Interfaces.Contains(interfaceTypeMetadata));
	}
}