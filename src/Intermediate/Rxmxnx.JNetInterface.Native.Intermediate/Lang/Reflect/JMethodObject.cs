namespace Rxmxnx.JNetInterface.Reflect;

/// <summary>
/// This class represents a local <c>java.lang.reflect.Method</c> instance.
/// </summary>
public sealed class JMethodObject// : JExecutableObject, IClassType<JMethodObject>
{
	// /// <summary>
	// /// class metadata.
	// /// </summary>
	// private static readonly JClassTypeMetadata metadata = JTypeMetadataBuilder<JExecutableObject>
	//                                                       .Create<JMethodObject>(
	// 	                                                      UnicodeClassNames.MethodObject(),
	// 	                                                      JTypeModifier.Abstract)
	//                                                       .Implements<JAnnotatedElementObject>()
	//                                                       .Implements<JGenericDeclarationObject>()
	//                                                       .Implements<JMemberObject>().Build();
	//
	// static JDataTypeMetadata IDataType.Metadata => JMethodObject.metadata;
	
	// static JMethodObject? IReferenceType<JMethodObject>.Create(JLocalObject? jLocal)
	// 	=> !JObject.IsNullOrDefault(jLocal) ? new(JLocalObject.Validate<JMethodObject>(jLocal)) : default;
	// static JMethodObject? IReferenceType<JMethodObject>.Create(IEnvironment env, JGlobalBase? jGlobal)
	// 	=> !JObject.IsNullOrDefault(jGlobal) ?
	// 		new(env, JLocalObject.Validate<JMethodObject>(jGlobal, env)) :
	// 		default;
}