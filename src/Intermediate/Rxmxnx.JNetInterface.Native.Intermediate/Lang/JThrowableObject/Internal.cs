namespace Rxmxnx.JNetInterface.Lang;

using TypeMetadata = JThrowableTypeMetadata<JThrowableObject>;

public partial class JThrowableObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly TypeMetadata typeMetadata = TypeMetadataBuilder<JThrowableObject>
	                                                    .Create("java/lang/Throwable"u8)
	                                                    .Implements<JSerializableObject>().Build();

	static TypeMetadata IThrowableType<JThrowableObject>.Metadata => JThrowableObject.typeMetadata;
	static Type IDataType.FamilyType => typeof(JThrowableObject);

	/// <summary>
	/// Throwable exception thread identifier.
	/// </summary>
	internal Int32? ThreadId { get; set; }

	/// <summary>
	/// Creates an exception instance from a <see cref="JGlobalBase"/> throwable instance.
	/// </summary>
	/// <param name="jGlobalThrowable">A <see cref="JGlobalBase"/> throwable instance.</param>
	/// <param name="exceptionMessage">Exception message.</param>
	/// <returns>A <see cref="ThrowableException"/> instance.</returns>
	internal static ThrowableException CreateException(JGlobalBase jGlobalThrowable, String? exceptionMessage)
		=> IClassType.GetMetadata<JThrowableObject>().CreateException(jGlobalThrowable, exceptionMessage)!;
}