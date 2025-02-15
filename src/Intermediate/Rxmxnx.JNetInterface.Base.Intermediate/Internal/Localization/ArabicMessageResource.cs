namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Arabic message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class ArabicMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly ArabicMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => ArabicMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private ArabicMessageResource() { }

	String IMessageResource.InvalidType => "القيمة المطلوبة لا يمكن أن تكون داخل JValue.";
	String IMessageResource.VoidArgument => "لا يمكن استخدام النوع void كمعامل مثيل.";
	String IMessageResource.VoidInstantiation => "لا يمكن إنشاء كائن من النوع void.";
	String IMessageResource.VoidArray => "لا يمكن استخدام النوع void كنوع عنصر في مصفوفة.";
	String IMessageResource.VoidEquality => "لا يمكن أن يكون كائن Void قابلاً للمقارنة.";
	String IMessageResource.SignatureNotAllowed => "التوقيع غير مسموح به.";
	String IMessageResource.CriticalExceptionMessage
		=> "التنفيذ في حالة حرجة. هناك استثناء معلق، ولكن لا يمكن إجراء أي استدعاءات عبر الواجهة الأصلية.";
	String IMessageResource.UnknownExceptionMessage
		=> "هناك استثناء معلق، لكن لم نتمكن من إنشاء كائن ThrowableException.";
	String IMessageResource.InvalidSignatureMessage => "توقيع غير صالح.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "التعريف ليس من النوع الأساسي.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "نوع أساسي غير صالح.";
	String IMessageResource.ProxyOnNonProxyProcess => "لا يمكن للكائن الوكيل معالجة بيانات التعريف غير الوكيلة.";
	String IMessageResource.NonProxyOnProxyProcess => "لا يمكن لكائن غير وكيل معالجة بيانات التعريف الوكيلة.";
	String IMessageResource.NotConstructorDefinition => "تعريف الاستدعاء الحالي ليس منشئًا.";
	String IMessageResource.NotPrimitiveObject => "الكائن ليس من النوع الأساسي.";
	String IMessageResource.InvalidProxyObject => "يجب أن يكون كائن Java وكيلًا لتنفيذ هذه العملية.";
	String IMessageResource.InvalidGlobalObject => "كائن عالمي غير صالح.";
	String IMessageResource.OnlyLocalReferencesAllowed => "تسمح استدعاءات JNI بالمراجع المحلية فقط.";
	String IMessageResource.ClassRedefinition => "إعادة تعريف الفئة غير مدعومة.";
	String IMessageResource.StackTraceFixed => "إطار التكديس الحالي غير قابل للتغيير.";
	String IMessageResource.InvalidClass => "كائن فئة غير صالح.";
	String IMessageResource.UnloadedClass => "تم تفريغ الفئة.";
	String IMessageResource.NotClassObject => "الكائن ليس كائن فئة.";
	String IMessageResource.SingleReferenceTransaction => "يمكن لهذه المعاملة الاحتفاظ بمرجع واحد فقط.";
	String IMessageResource.InvalidReferenceObject => "كائن JReferenceObject غير صالح.";
	String IMessageResource.InvalidObjectMetadata => "بيانات التعريف للكائن غير صالحة.";
	String IMessageResource.DisposedObject => "تم التخلص من كائن JReferenceObject.";
	String IMessageResource.InvalidArrayLength => "يجب أن يكون طول المصفوفة صفرًا أو رقمًا موجبًا.";
	String IMessageResource.DeadVirtualMachine => "الآلة الافتراضية JVM الحالية لا تعمل.";
	String IMessageResource.NotAttachedThread => "لم يتم إرفاق الخيط الحالي بـ JVM.";
	String IMessageResource.IncompatibleLibrary => "مكتبة JVM غير متوافقة.";
	String IMessageResource.UnmanagedMemoryContext => "كتلة الذاكرة غير مدارة.";

	String IMessageResource.InvalidInstantiation(CString className) => $"{className} ليس نوعًا قابلاً للإنشاء.";
	String IMessageResource.InvalidCastTo(Type type) => $"تحويل غير صالح إلى {type}.";
	String IMessageResource.InvalidCastTo(CString className) => $"لا يمكن تحويل المثيل الحالي إلى النوع {className}.";
	String IMessageResource.EmptyString(String paramName) => $"يجب أن يكون {paramName} سلسلة غير فارغة.";
	String IMessageResource.InvalidMetadata(CString className, Type typeOfT)
		=> $"بيانات التعريف للنوع {className} لا تتطابق مع النوع {typeOfT}.";
	String IMessageResource.AbstractClass(CString className) => $"{className} هو نوع مجرد.";
	String IMessageResource.InvalidDerivationType(String typeName) => $"{typeName} لا يمكن أن يرث من نوع يعتمد عليه.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} لا يمكن أن يمدد واجهة تقوم بتمديده.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} والواجهة العليا لهما لا يمكن أن يكونا متطابقين.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} والفئة العليا لهما لا يمكن أن يكونا متطابقين.";
	String IMessageResource.AnnotationType(CString interfaceName, String annotationName)
		=> $"لا يمكن تمديد {interfaceName}. {annotationName} هو تعليق توضيحي.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className)
		=> $"{className} لا ينفذ {interfaceName}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		CString missingSuperInterface)
		=> $"{className} لا ينفذ {interfaceName}. المفقود: {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{className} لا ينفذ {interfaceName}. القيم المفقودة: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName)
		=> $"{interfaceName} لا يمكن أن يمتد {superInterfaceName}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		CString missingSuperInterface)
		=> $"{interfaceName} لا يمكن أن يمتد {superInterfaceName}. مفقود: {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{interfaceName} لا يمكن أن يمتد {superInterfaceName}. القيم المفقودة: {String.Join(", ", missingSuperInterfaces)}.";

	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"الترتيب الخاص بـ {enumTypeName} يجب أن يكون صفرًا أو رقمًا موجبًا.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} يحتوي بالفعل على ترتيب {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName) => $"يجب أن يكون اسم {enumTypeName} غير فارغ.";

	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} يحتوي بالفعل على حقل باسم '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> ArabicMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{ArabicMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} القيم المفقودة: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"يجب بناء {typeName} باستخدام {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName) => $"{typeName} ليس نوع مرجع صالح.";
	String IMessageResource.NotTypeObject(CString objectClassName, CString className)
		=> $"{objectClassName} ليس كائن نوع صالح لـ {className}.";

	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"حدث خطأ أثناء إنشاء مرجع JNI عالمي للفئة {mainClassName}.";
	String IMessageResource.MainClassUnavailable(String mainClassName) => $"الفئة الرئيسية {mainClassName} غير متاحة.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"الفئة البدائية {primitiveClassName} غير متوفرة.";

	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"تجاوز سعة المعاملة: {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"التعريفات غير متطابقة: {definition} مقابل {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"بيئة JNI ({envRef}) مخصصة لسلك مختلف. السلك الحالي: {Environment.CurrentManagedThreadId}، المحاولة على السلك: {threadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"بيئة JNI الحالية في حالة غير صالحة لاستدعاء {functionName} بأمان.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} يتطلب الإصدار 0x{requiredVersion:x8}، لكن الإصدار الحالي هو 0x{currentVersion:x8}.";

	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"قائمة القيم لـ {enumTypeName} غير صالحة. العدد: {count}، الترتيب الأقصى: {maxOrdinal}.";
}