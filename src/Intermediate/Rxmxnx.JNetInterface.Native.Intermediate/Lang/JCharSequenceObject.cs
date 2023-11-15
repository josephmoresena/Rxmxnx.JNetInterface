namespace Rxmxnx.JNetInterface.Lang;

/// <summary>
/// This class represents a local <c>java.lang.CharSequence</c> instance.
/// </summary>
public sealed class JCharSequenceObject : JInterfaceObject<JCharSequenceObject>, IInterfaceType<JCharSequenceObject>
{
	/// <summary>
	/// Datatype metadata.
	/// </summary>
	private static readonly JInterfaceTypeMetadata typeMetadata = JTypeMetadataBuilder<JCharSequenceObject>
	                                                              .Create(UnicodeClassNames.JCharSequenceInterfaceName)
	                                                              .WithSignature(
		                                                              UnicodeObjectSignatures
			                                                              .JCharSequenceObjectSignature).Build();

	static JDataTypeMetadata IDataType.Metadata => JCharSequenceObject.typeMetadata;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="env"><see cref="IEnvironment"/> instance.</param>
	/// <param name="jGlobal"><see cref="JGlobalBase"/> instance.</param>
	public JCharSequenceObject(IEnvironment env, JGlobalBase jGlobal) : base(
		env, JLocalObject.Validate<JCharSequenceObject>(jGlobal, env)) { }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
	private JCharSequenceObject(JLocalObject jLocal) : base(jLocal) { }

	/// <inheritdoc/>
	static JCharSequenceObject? IReferenceType<JCharSequenceObject>.Create(JObject? jObject)
		=> jObject is JLocalObject jLocal ? new(JLocalObject.Validate<JCharSequenceObject>(jLocal)) : default;
}