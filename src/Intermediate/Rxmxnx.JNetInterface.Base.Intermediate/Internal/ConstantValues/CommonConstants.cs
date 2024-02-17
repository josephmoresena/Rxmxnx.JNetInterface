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
	public const String BinaryStructJustification = "This struct is created only by binary operations.";
	public const String AbstractProxyJustification = "This object is an abstract proxy.";
	public const String JavaInheritanceJustification =
		"Any JReferenceObject type tree of classes is inherently longer than a normal C# class.";
	public const String ReferenceableFieldJustification = "Field value can be set using a managed reference.";
	public const String BackwardOperationJustification = "Backward operation is needed.";
	public const String InternalInheritanceJustification = "Only internal inheritance is supported.";
	public const String PublicInitPrivateSetJustification = "The property must be publicly 'init' but privately 'set'.";
	public const String NoMethodOverloadingJustification =
		"Homonymous functions are different and should not be considered overloading.";
}