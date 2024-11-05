namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores initial classes.
/// </summary>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1694,
                 Justification = CommonConstants.InternalInheritanceJustification)]
public abstract class MainClasses
{
	/// <summary>
	/// Indicates whether <see cref="JVoidObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean VoidObjectMainClassEnabled => false;
	/// <summary>
	/// Indicates whether <see cref="JBooleanObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean BooleanObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JByteObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean ByteObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JCharacterObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean CharacterObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JDoubleObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean DoubleObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JDoubleObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean FloatObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JIntegerObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean IntegerObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JLongObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean LongObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JShortObject"/> class is a main class.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean ShortObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether primitive classes are main classes.
	/// </summary>
	[ExcludeFromCodeCoverage]
	public static Boolean PrimitiveMainClassesEnabled => true;

	/// <summary>
	/// Creates user main classes dictionary.
	/// </summary>
	/// <returns>A <see cref="ConcurrentDictionary{String,ClassObjectMetadata}"/> instance.</returns>
	public static ConcurrentDictionary<String, ClassObjectMetadata> CreateMainClassesDictionary()
	{
		ConcurrentDictionary<String, ClassObjectMetadata> mainClasses = new();
		MainClasses.AppendMainClass<JVoidObject>(MainClasses.VoidObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JBooleanObject>(MainClasses.BooleanObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JByteObject>(MainClasses.ByteObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JCharacterObject>(MainClasses.CharacterObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JDoubleObject>(MainClasses.DoubleObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JFloatObject>(MainClasses.FloatObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JIntegerObject>(MainClasses.IntegerObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JLongObject>(MainClasses.LongObjectMainClassEnabled, mainClasses);
		MainClasses.AppendMainClass<JShortObject>(MainClasses.ShortObjectMainClassEnabled, mainClasses);
		return mainClasses;
	}
	/// <summary>
	/// Appends <typeparamref name="TReference"/> as main class.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="isMainClass">Indicates whether <typeparamref name="TReference"/> is main class.</param>
	/// <param name="mainClasses">Main classes dictionary.</param>
	private static void AppendMainClass<TReference>(Boolean isMainClass,
		IDictionary<String, ClassObjectMetadata> mainClasses)
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
		if (!isMainClass) return;
		String hash = MetadataHelper.GetExactMetadata<TReference>().Hash;
		if (!mainClasses.ContainsKey(hash))
			mainClasses.TryAdd(hash, ClassObjectMetadata.Create<TReference>());
	}
}

/// <summary>
/// Stores initial classes.
/// </summary>
/// <typeparam name="TClass">Type of class.</typeparam>
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1694,
                 Justification = CommonConstants.InternalInheritanceJustification)]
public abstract class MainClasses<TClass> : MainClasses where TClass : JReferenceObject
{
	/// <summary>
	/// Class for <see cref="JClassObject"/>
	/// </summary>
	public abstract TClass ClassObject { get; }
	/// <summary>
	/// Class for <see cref="JThrowableObject"/>
	/// </summary>
	public abstract TClass ThrowableObject { get; }
	/// <summary>
	/// Class for <see cref="JStackTraceElementObject"/>
	/// </summary>
	public abstract TClass StackTraceElementObject { get; }

	/// <summary>
	/// Class for Java <c>void</c> type.
	/// </summary>
	public abstract TClass VoidPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JBoolean"/>.
	/// </summary>
	public abstract TClass BooleanPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JByte"/>.
	/// </summary>
	public abstract TClass BytePrimitive { get; }
	/// <summary>
	/// Class for <see cref="JChar"/>.
	/// </summary>
	public abstract TClass CharPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JDouble"/>.
	/// </summary>
	public abstract TClass DoublePrimitive { get; }
	/// <summary>
	/// Class for <see cref="JFloat"/>.
	/// </summary>
	public abstract TClass FloatPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JInt"/>.
	/// </summary>
	public abstract TClass IntPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JLong"/>.
	/// </summary>
	public abstract TClass LongPrimitive { get; }
	/// <summary>
	/// Class for <see cref="JShort"/>.
	/// </summary>
	public abstract TClass ShortPrimitive { get; }
}