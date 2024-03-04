namespace Rxmxnx.JNetInterface.Internal.ConstantValues;

/// <summary>
/// Internal class of constant <see cref="String"/> values.
/// </summary>
internal static class CommonConstants
{
	public const String CriticalExceptionMessage =
		"The execution is in critical state. No calls can be made through native interface.";

	public const String CodeQuality = "CodeQuality";
	public const String CSharpSquid = "csharpsquid";
	public const String CheckId0051 = "IDE0051:Remove unused private members";
	public const String CheckIdS1144 = "S1144:Remove the unused private field";
	public const String CheckIdS2292 = "S2292:Trivial properties should be auto-implemented";
	public const String CheckIdS3881 = "S3881:\"IDisposable\" should be implemented correctly";
	public const String CheckIdS110 = "S110:Inheritance tree of classes should not be too deep";
	public const String CheckIdS3459 = "S3459:Unassigned members should be removed";
	public const String CheckIdS2436 = "S2436:Types and methods should not have too many generic parameters";
	public const String CheckIdS2234 = "S2234:Arguments should be passed in the same order as the method parameters";
	public const String CheckIdS4035 = "S4035:Classes implementing \"IEquatable<T>\" should be sealed";
	public const String CheckIdS4136 = "S4136:Method overloads should be grouped together";
	public const String CheckIdS2953 = "S2953:Methods named \"Dispose\" should implement \"IDisposable.Dispose\"";
	public const String CheckIdS3218 =
		"S3218:Inner class members should not shadow outer class \"static\" or type members";
	public const String CheckIdS3963 = "S3963:\"static\" fields should be initialized inline";
	public const String CheckIdS3011 =
		"S3011:Reflection should not be used to increase accessibility of classes, methods, or fields";
	public const String CheckIdS2743 =
		"S2743:A static field in a generic type is not shared among instances of different close constructed types";
	public const String CheckIdS1006 = "S1006:Use the default parameter value defined in the overridden method";
	public const String CheckIdS1210 =
		"S1210:\"Equals\" and the comparison operators should be overridden when implementing \"IComparable\"";
	public const String CheckIdS1206 = "S1206:\"Equals(Object)\" and \"GetHashCode()\" should be overridden in pairs";
	public const String BinaryStructJustification = "This struct is created only by binary operations.";
	public const String AbstractProxyJustification = "This object is an abstract proxy.";
	public const String JniThreadRequiredJustification = "Global object disposing requires a JNI thread.";
	public const String JavaInheritanceJustification =
		"Any JReferenceObject type tree of classes is inherently longer than a normal C# class.";
	public const String ReferenceableFieldJustification = "Field value can be set using a managed reference.";
	public const String BackwardOperationJustification = "Backward operation is needed.";
	public const String InternalInheritanceJustification = "Only internal inheritance is supported.";
	public const String PublicInitPrivateSetJustification = "The property must be publicly 'init' but privately 'set'.";
	public const String NoMethodOverloadingJustification =
		"Homonymous functions are different and should not be considered overloading.";
	public const String ReflectionFreeModeJustification =
		"Reflection use should be avoidable in NativeAOT reflection free mode.";
	public const String ReflectionPrivateUseJustification =
		"Reflection use is limited privately and is used only to avoid infinity recursive initialization type.";
	public const String StaticAbstractPropertyUseJustification =
		"There is no static field, but abstract/virtual property.";
	public const String DefaultValueTypeJustification = "Default value for ValueType is not null.";
	public const String NoStringComparisonOperatorsJustification = "System.String has no comparison operators.";
}