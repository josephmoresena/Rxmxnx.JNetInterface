using IMessageResource = Rxmxnx.JNetInterface.Internal.Localization.IMessageResource;

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
	[Fact]
	internal void InvalidInheritanceTest()
	{
		InvalidOperationException classException =
			Assert.Throws<InvalidOperationException>(() => InvalidSubClass.Metadata);
		InvalidOperationException interfaceException =
			Assert.Throws<InvalidOperationException>(() => InvalidSubInterface.Metadata);
		InvalidOperationException annotationException =
			Assert.Throws<InvalidOperationException>(() => InvalidAnnotation.Metadata);
		NotImplementedException invalidImplementation1Exception =
			Assert.Throws<NotImplementedException>(() => InvalidImplementation1.Metadata);
		NotImplementedException invalidImplementation2Exception =
			Assert.Throws<NotImplementedException>(() => InvalidImplementation2.Metadata);
		NotImplementedException invalidImplementation3Exception =
			Assert.Throws<NotImplementedException>(() => InvalidImplementation3.Metadata);
		NotImplementedException invalidExtension1Exception =
			Assert.Throws<NotImplementedException>(() => InvalidExtension1.Metadata);
		NotImplementedException invalidExtension2Exception =
			Assert.Throws<NotImplementedException>(() => InvalidExtension2.Metadata);
		NotImplementedException invalidExtension3Exception =
			Assert.Throws<NotImplementedException>(() => InvalidExtension3.Metadata);

		IMessageResource resource = IMessageResource.GetInstance();

		Assert.Equal(resource.SameClassExtension(new CString(InvalidSubClass.Name).ToString()), classException.Message);
		Assert.Equal(resource.SameInterfaceExtension(new CString(InvalidSubInterface.Name).ToString()),
		             interfaceException.Message);
		Assert.Equal(
			resource.AnnotationType(InvalidAnnotation.SerializableName, new CString(InvalidAnnotation.Name).ToString()),
			annotationException.Message);

		Assert.Equal(
			resource.InvalidImplementation(InvalidImplementation1.Error,
			                               new CString(InvalidImplementation1.Name).ToString(),
			                               InvalidImplementation1.Missing), invalidImplementation1Exception.Message);
		Assert.Equal(
			resource.InvalidExtension(InvalidExtension1.Error, new CString(InvalidExtension1.Name).ToString(),
			                          InvalidExtension1.Missing), invalidExtension1Exception.Message);

		Assert.True(SubInterface.Interfaces.All(i => invalidImplementation2Exception.Message.Contains(i.ToString())));
		Assert.True(SubInterface.Interfaces.All(i => invalidExtension2Exception.Message.Contains(i.ToString())));

		Assert.Equal(
			resource.InvalidImplementation(InvalidImplementation3.Error,
			                               new CString(InvalidImplementation3.Name).ToString()),
			invalidImplementation3Exception.Message);
		Assert.Equal(resource.InvalidExtension(InvalidExtension3.Error, new CString(InvalidExtension3.Name).ToString()),
		             invalidExtension3Exception.Message);
	}

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

	private sealed class InvalidSubClass : JLocalObject.Uninstantiable<InvalidSubClass>,
		IUninstantiableType<InvalidSubClass>
	{
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidSubClass"u8;
		public static JClassTypeMetadata<InvalidSubClass> Metadata
			=> TypeMetadataBuilder<InvalidSubClass>.Create<InvalidSubClass>(InvalidSubClass.Name).Build();
	}

	private sealed class InvalidSubInterface : JInterfaceObject<InvalidSubInterface>,
		IInterfaceType<InvalidSubInterface>
	{
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidSubInterface"u8;
		public static JInterfaceTypeMetadata<InvalidSubInterface> Metadata
			=> TypeMetadataBuilder<InvalidSubInterface>.Create(InvalidSubInterface.Name).Extends<InvalidSubInterface>()
			                                           .Build();

		private InvalidSubInterface(IReferenceType.ObjectInitializer initializer) : base(initializer) { }

		static InvalidSubInterface IInterfaceType<InvalidSubInterface>.Create(
			IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}

	private sealed class InvalidAnnotation : JAnnotationObject<InvalidAnnotation>, IInterfaceType<InvalidAnnotation>
	{
		public static CString SerializableName => IInterfaceType.GetMetadata<JSerializableObject>().ClassName;
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidSubInterface"u8;
		public static JInterfaceTypeMetadata<InvalidAnnotation> Metadata
			=> TypeMetadataBuilder<InvalidAnnotation>.Create(InvalidAnnotation.Name).Extends<JSerializableObject>()
			                                         .Build();
		private InvalidAnnotation(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static InvalidAnnotation IInterfaceType<InvalidAnnotation>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}

	private sealed class SubInterface : JInterfaceObject<SubInterface>, IInterfaceType<SubInterface>,
		IInterfaceObject<JGenericDeclarationObject>, IInterfaceObject<JAnnotatedElementObject>,
		IInterfaceObject<JCloneableObject>
	{
		public static readonly CString[] Interfaces =
			IInterfaceType.GetMetadata<SubInterface>().Interfaces.Select(i => i.ClassName).ToArray();
		public static ReadOnlySpan<Byte> Name => "fake/invalid/SubInterface"u8;
		public static JInterfaceTypeMetadata<SubInterface> Metadata
			=> TypeMetadataBuilder<SubInterface>.Create(SubInterface.Name).Extends<JGenericDeclarationObject>()
			                                    .Extends<JCloneableObject>().Build();
		private SubInterface(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static SubInterface IInterfaceType<SubInterface>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}

	private sealed class InvalidImplementation1 : JLocalObject.Uninstantiable<InvalidImplementation1>,
		IInterfaceObject<JGenericDeclarationObject>, IUninstantiableType<InvalidImplementation1>
	{
		public static CString Missing => IInterfaceType.GetMetadata<JAnnotatedElementObject>().ClassName;
		public static CString Error => IInterfaceType.GetMetadata<JGenericDeclarationObject>().ClassName;
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidImplementation1"u8;

		public static JClassTypeMetadata<InvalidImplementation1> Metadata
			=> TypeMetadataBuilder<InvalidImplementation1>.Create(InvalidImplementation1.Name)
			                                              .Implements<JGenericDeclarationObject>().Build();
	}

	private sealed class InvalidImplementation2 : JLocalObject.Uninstantiable<InvalidImplementation2>,
		IUninstantiableType<InvalidImplementation2>, IInterfaceObject<SubInterface>
	{
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidImplementation2"u8;

		public static JClassTypeMetadata<InvalidImplementation2> Metadata
			=> TypeMetadataBuilder<InvalidImplementation2>.Create(InvalidImplementation2.Name)
			                                              .Implements<SubInterface>().Build();
	}

	private sealed class InvalidImplementation3 : JLocalObject.Uninstantiable<InvalidImplementation3>,
		IUninstantiableType<InvalidImplementation3>
	{
		public static CString Error => IInterfaceType.GetMetadata<JGenericDeclarationObject>().ClassName;
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidImplementation3"u8;

		public static JClassTypeMetadata<InvalidImplementation3> Metadata
			=> TypeMetadataBuilder<InvalidImplementation3>.Create(InvalidImplementation3.Name)
			                                              .Implements<JGenericDeclarationObject>().Build();
	}

	private sealed class InvalidExtension1 : JInterfaceObject<InvalidExtension1>,
		IInterfaceObject<JGenericDeclarationObject>, IInterfaceType<InvalidExtension1>
	{
		public static CString Missing => IInterfaceType.GetMetadata<JAnnotatedElementObject>().ClassName;
		public static CString Error => IInterfaceType.GetMetadata<JGenericDeclarationObject>().ClassName;
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidExtension1"u8;
		public static JInterfaceTypeMetadata<InvalidExtension1> Metadata
			=> TypeMetadataBuilder<InvalidExtension1>.Create(InvalidExtension1.Name)
			                                         .Extends<JGenericDeclarationObject>().Build();

		private InvalidExtension1(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static InvalidExtension1 IInterfaceType<InvalidExtension1>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}

	private sealed class InvalidExtension2 : JInterfaceObject<InvalidExtension2>, IInterfaceType<InvalidExtension2>,
		IInterfaceObject<SubInterface>
	{
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidExtension2"u8;
		public static JInterfaceTypeMetadata<InvalidExtension2> Metadata
			=> TypeMetadataBuilder<InvalidExtension2>.Create(InvalidExtension2.Name).Extends<SubInterface>().Build();

		private InvalidExtension2(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static InvalidExtension2 IInterfaceType<InvalidExtension2>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}

	private sealed class InvalidExtension3 : JInterfaceObject<InvalidExtension3>, IInterfaceType<InvalidExtension3>
	{
		public static CString Error => IInterfaceType.GetMetadata<JGenericDeclarationObject>().ClassName;
		public static ReadOnlySpan<Byte> Name => "fake/invalid/InvalidExtension3"u8;
		public static JInterfaceTypeMetadata<InvalidExtension3> Metadata
			=> TypeMetadataBuilder<InvalidExtension3>.Create(InvalidExtension3.Name)
			                                         .Extends<JGenericDeclarationObject>().Build();

		private InvalidExtension3(IReferenceType.ObjectInitializer initializer) : base(initializer) { }
		static InvalidExtension3 IInterfaceType<InvalidExtension3>.Create(IReferenceType.ObjectInitializer initializer)
			=> new(initializer);
	}
}