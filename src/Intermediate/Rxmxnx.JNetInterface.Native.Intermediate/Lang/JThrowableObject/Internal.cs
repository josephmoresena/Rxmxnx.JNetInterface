namespace Rxmxnx.JNetInterface.Lang;

public partial class JThrowableObject
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JThrowableTypeMetadata<JThrowableObject> typeMetadata =
		TypeMetadataBuilder<JThrowableObject>.Create(UnicodeClassNames.ThrowableObject())
		                                      .Implements<JSerializableObject>().Build();

	static JThrowableTypeMetadata<JThrowableObject> IThrowableType<JThrowableObject>.Metadata
		=> JThrowableObject.typeMetadata;
	static Type IDataType.FamilyType => typeof(JThrowableObject);

	/// <summary>
	/// Retrieves a <see cref="JStringObject"/> containing throwable message.
	/// </summary>
	/// <param name="throwableClass">A <see cref="IEnvironment"/> instance.</param>
	/// <param name="throwableRef">A <see cref="JThrowableLocalRef"/> reference.</param>
	/// <returns>A <see cref="JStringObject"/> instance.</returns>
	internal static JStringObject GetThrowableMessage(JClassObject throwableClass, JThrowableLocalRef throwableRef)
	{
		IEnvironment env = throwableClass.Environment;
		using JThrowableObject tempThrowable = new(throwableClass, throwableRef);
		try
		{
			return env.FunctionSet.GetMessage(tempThrowable);
		}
		finally
		{
			tempThrowable.ClearValue();
		}
	}
}