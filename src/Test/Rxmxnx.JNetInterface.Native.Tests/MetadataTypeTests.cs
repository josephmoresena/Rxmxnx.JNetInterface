namespace Rxmxnx.JNetInterface.Tests;

[ExcludeFromCodeCoverage]
public sealed class MetadataTypeTests
{
	[Fact]
	internal void Test()
	{
		InvalidType.Metadata = InvalidType.CreateMetadata("package/class/Test1"u8);

		Assert.Equal(InvalidType.Metadata, IDataType.GetMetadata<InvalidType>());
		Assert.Equal(InvalidType.Metadata, IReferenceType.GetMetadata<InvalidType>());
		Assert.Equal(InvalidType.Metadata, IClassType.GetMetadata<InvalidType>());

		InvalidType.Metadata = IDataType.GetMetadata<JLocalObject>();

		Assert.Throws<ArgumentException>(IDataType.GetMetadata<InvalidType>);
		Assert.Throws<ArgumentException>(IReferenceType.GetMetadata<InvalidType>);
		Assert.Throws<ArgumentException>(IClassType.GetMetadata<InvalidType>);

		InvalidType.Metadata = InvalidType.CreateMetadata("package/class/Test2"u8);

		Assert.Equal(InvalidType.Metadata, IDataType.GetMetadata<InvalidType>());
		Assert.Equal(InvalidType.Metadata, IReferenceType.GetMetadata<InvalidType>());
		Assert.Equal(InvalidType.Metadata, IClassType.GetMetadata<InvalidType>());
	}

	private sealed class InvalidType : JLocalObject.Uninstantiable<InvalidType>, IUninstantiableType<InvalidType>
	{
		public static JDataTypeMetadata? Metadata { get; set; }

		static JClassTypeMetadata<InvalidType> IClassType<InvalidType>.Metadata
			=> (JClassTypeMetadata<InvalidType>)InvalidType.Metadata!;
		static JDataTypeMetadata IDataType<InvalidType>.Metadata => InvalidType.Metadata!;
		public static JClassTypeMetadata<InvalidType> CreateMetadata(ReadOnlySpan<Byte> typeName)
			=> TypeMetadataBuilder<InvalidType>.Create(typeName, JTypeModifier.Final).Build();
	}
}