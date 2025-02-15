namespace Rxmxnx.JNetInterface.Internal.Localization;

/// <summary>
/// Russian message resource.
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class RussianMessageResource : IMessageResource
{
	/// <inheritdoc cref="IMessageResource.Instance"/>
	private static readonly RussianMessageResource instance = new();

	static IMessageResource IMessageResource.Instance => RussianMessageResource.instance;

	/// <summary>
	/// Private constructor.
	/// </summary>
	private RussianMessageResource() { }

	String IMessageResource.InvalidType => "Запрошенное значение не может быть содержаться в JValue.";
	String IMessageResource.VoidArgument => "Тип void не может использоваться в качестве аргумента экземпляра.";
	String IMessageResource.VoidInstantiation => "Тип void не может быть создан.";
	String IMessageResource.VoidArray => "Тип void не может быть использован как тип элемента массива.";
	String IMessageResource.VoidEquality => "Экземпляр Void не может быть сравнимым.";
	String IMessageResource.SignatureNotAllowed => "Подпись не разрешена.";
	String IMessageResource.CriticalExceptionMessage
		=> "Выполнение находится в критическом состоянии. Есть ожидающее исключение, но вызовы через родной интерфейс невозможны.";
	String IMessageResource.UnknownExceptionMessage
		=> "Есть ожидающее исключение, но экземпляр ThrowableException не может быть создан.";
	String IMessageResource.InvalidSignatureMessage => "Недопустимая подпись.";
	String IMessageResource.InvalidPrimitiveDefinitionMessage => "Определение не является примитивным типом.";
	String IMessageResource.InvalidPrimitiveTypeMessage => "Недопустимый примитивный тип.";
	String IMessageResource.ProxyOnNonProxyProcess => "Прокси-объект не может обрабатывать метаданные непро-кси.";
	String IMessageResource.NonProxyOnProxyProcess => "Непрокси-объект не может обрабатывать метаданные прокси.";
	String IMessageResource.NotConstructorDefinition => "Текущая определенная функция не является конструктором.";
	String IMessageResource.NotPrimitiveObject => "Объект не является примитивным.";
	String IMessageResource.InvalidProxyObject => "Java-объект должен быть прокси для выполнения этой операции.";
	String IMessageResource.InvalidGlobalObject => "Недопустимый глобальный объект.";
	String IMessageResource.OnlyLocalReferencesAllowed => "Вызовы JNI разрешены только для локальных ссылок.";
	String IMessageResource.ClassRedefinition => "Переопределение класса не поддерживается.";
	String IMessageResource.StackTraceFixed => "Текущий стек-фрейм неизменяем.";
	String IMessageResource.InvalidClass => "Недопустимый объект класса.";
	String IMessageResource.UnloadedClass => "Класс был выгружен.";
	String IMessageResource.NotClassObject => "Объект не является классом.";
	String IMessageResource.SingleReferenceTransaction => "Эта транзакция может содержать только одну ссылку.";
	String IMessageResource.InvalidReferenceObject => "Недопустимый JReferenceObject.";
	String IMessageResource.InvalidObjectMetadata => "Недопустимый ObjectMetadata.";
	String IMessageResource.DisposedObject => "JReferenceObject был удален.";
	String IMessageResource.InvalidArrayLength => "Длина массива должна быть нулевой или положительной.";
	String IMessageResource.DeadVirtualMachine => "Текущая JVM не запущена.";
	String IMessageResource.NotAttachedThread => "Текущий поток не подключен к JVM.";
	String IMessageResource.IncompatibleLibrary => "Несовместимая библиотека JVM.";
	String IMessageResource.UnmanagedMemoryContext => "Блок памяти не управляем.";

	String IMessageResource.InvalidInstantiation(CString className) => $"{className} не является создаваемым типом.";
	String IMessageResource.InvalidCastTo(Type type) => $"Недопустимое приведение к {type}.";
	String IMessageResource.InvalidCastTo(CString className)
		=> $"Текущий экземпляр не может быть приведен к типу {className}.";
	String IMessageResource.EmptyString(String paramName) => $"{paramName} должен быть непустой строкой.";
	String IMessageResource.InvalidMetadata(CString className, Type typeOfT)
		=> $"Метаданные типа данных для {className} не соответствуют типу {typeOfT}.";
	String IMessageResource.AbstractClass(CString className) => $"{className} является абстрактным типом.";
	String IMessageResource.InvalidDerivationType(String typeName)
		=> $"{typeName} не может быть унаследован от типа, основанного на нем.";
	String IMessageResource.InvalidInterfaceExtension(String interfaceName)
		=> $"{interfaceName} не может расширять интерфейс, который уже его расширяет.";
	String IMessageResource.SameInterfaceExtension(String interfaceName)
		=> $"{interfaceName} и его супер-интерфейс не могут быть одинаковыми.";
	String IMessageResource.SameClassExtension(String className)
		=> $"{className} и его суперкласс не могут быть одинаковыми.";
	String IMessageResource.AnnotationType(CString interfaceName, String annotationName)
		=> $"Невозможно расширить {interfaceName}. {annotationName} является аннотацией.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className)
		=> $"{className} не реализует {interfaceName}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		CString missingSuperInterface)
		=> $"{className} не реализует {interfaceName}. Отсутствует: {missingSuperInterface}.";
	String IMessageResource.InvalidImplementation(CString interfaceName, String className,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{className} не реализует {interfaceName}. Отсутствуют: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName)
		=> $"{interfaceName} не может расширять {superInterfaceName}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		CString missingSuperInterface)
		=> $"{interfaceName} не может расширять {superInterfaceName}. Отсутствует: {missingSuperInterface}.";
	String IMessageResource.InvalidExtension(CString superInterfaceName, String interfaceName,
		IReadOnlySet<CString> missingSuperInterfaces)
		=> $"{interfaceName} не может расширять {superInterfaceName}. Отсутствуют: {String.Join(", ", missingSuperInterfaces)}.";
	String IMessageResource.InvalidOrdinal(String enumTypeName)
		=> $"Порядковый номер для {enumTypeName} должен быть нулевым или положительным.";
	String IMessageResource.DuplicateOrdinal(String enumTypeName, Int32 ordinal)
		=> $"{enumTypeName} уже содержит поле с порядковым номером {ordinal}.";
	String IMessageResource.InvalidValueName(String enumTypeName) => $"Имя для {enumTypeName} должно быть непустым.";
	String IMessageResource.DuplicateValueName(String enumTypeName, CString valueName)
		=> $"{enumTypeName} уже содержит поле с именем '{valueName}'.";
	String IMessageResource.InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> RussianMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal);
	String IMessageResource.
		InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal, IReadOnlySet<Int32> missing)
		=> $"{RussianMessageResource.InvalidValueList(enumTypeName, count, maxOrdinal)} Отсутствуют: {String.Join(", ", missing)}.";
	String IMessageResource.InvalidBuilderType(String typeName, String expectedBuilder)
		=> $"{typeName} должен быть построен с использованием {expectedBuilder}.";
	String IMessageResource.InvalidReferenceType(String typeName)
		=> $"{typeName} не является допустимым ссылочным типом.";
	String IMessageResource.NotTypeObject(CString objectClassName, CString className)
		=> $"{objectClassName} не является объектом типа для {className}.";
	String IMessageResource.MainClassGlobalError(String mainClassName)
		=> $"Ошибка при создании глобальной ссылки JNI для класса {mainClassName}.";
	String IMessageResource.MainClassUnavailable(String mainClassName) => $"Основной класс {mainClassName} недоступен.";
	String IMessageResource.PrimitiveClassUnavailable(String primitiveClassName)
		=> $"Примитивный класс {primitiveClassName} недоступен.";
	String IMessageResource.OverflowTransactionCapacity(Int32 transactionCapacity)
		=> $"Переполнение емкости транзакции: {transactionCapacity}.";
	String IMessageResource.DefinitionNotMatch(JAccessibleObjectDefinition definition,
		JAccessibleObjectDefinition otherDefinition)
		=> $"Определения не совпадают: {definition} vs {otherDefinition}.";
	String IMessageResource.DifferentThread(JEnvironmentRef envRef, Int32 threadId)
		=> $"Среда JNI ({envRef}) назначена другому потоку. Операция выполнена в другом потоке: {threadId}. Текущий поток: {Environment.CurrentManagedThreadId}.";
	String IMessageResource.CallOnUnsafe(String functionName)
		=> $"Текущая среда JNI находится в некорректном состоянии для безопасного вызова {functionName}.";
	String IMessageResource.InvalidCallVersion(Int32 currentVersion, String functionName, Int32 requiredVersion)
		=> $"{functionName} требует версию 0x{requiredVersion:x8}, но текущая версия 0x{currentVersion:x8}.";
	/// <inheritdoc cref="IMessageResource.InvalidValueList(String, Int32, Int32)"/>
	private static String InvalidValueList(String enumTypeName, Int32 count, Int32 maxOrdinal)
		=> $"Список полей перечисления для {enumTypeName} некорректен. Количество: {count}, Максимальный порядковый номер: {maxOrdinal}.";
}