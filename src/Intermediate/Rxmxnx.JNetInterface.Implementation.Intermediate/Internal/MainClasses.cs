namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// Stores initial classes.
/// </summary>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1694,
                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
internal abstract partial class MainClasses
{
	/// <summary>
	/// Indicates whether <see cref="JVoidObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean VoidObjectMainClassEnabled => false;
	/// <summary>
	/// Indicates whether <see cref="JBooleanObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean BooleanObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JByteObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean ByteObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JCharacterObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean CharacterObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JDoubleObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean DoubleObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JDoubleObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean FloatObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JIntegerObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean IntegerObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JLongObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean LongObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether <see cref="JShortObject"/> class is a main class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean ShortObjectMainClassEnabled => true;
	/// <summary>
	/// Indicates whether primitive classes are main classes.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean PrimitiveMainClassesEnabled => true;

	/// <summary>
	/// Creates user main classes' dictionary.
	/// </summary>
	/// <returns>A <see cref="ConcurrentDictionary{String,JDataTypeMetadata}"/> instance.</returns>
	public static ConcurrentDictionary<String, JDataTypeMetadata> CreateMainClassesDictionary()
	{
		ConcurrentDictionary<String, JDataTypeMetadata> mainClasses = new();
		MainClasses.AppendInitialClass<JVoidObject>(MainClasses.VoidObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JBooleanObject>(MainClasses.BooleanObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JByteObject>(MainClasses.ByteObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JCharacterObject>(MainClasses.CharacterObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JDoubleObject>(MainClasses.DoubleObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JFloatObject>(MainClasses.FloatObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JIntegerObject>(MainClasses.IntegerObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JLongObject>(MainClasses.LongObjectMainClassEnabled, mainClasses);
		MainClasses.AppendInitialClass<JShortObject>(MainClasses.ShortObjectMainClassEnabled, mainClasses);
		return mainClasses;
	}
	/// <summary>
	/// Set main class.
	/// </summary>
	/// <param name="mainClasses">Main classes dictionary.</param>
	/// <param name="typeMetadata">A <see cref="JDataTypeMetadata"/> instance.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static void AppendMainClass(IDictionary<String, JDataTypeMetadata> mainClasses,
		JDataTypeMetadata typeMetadata)
	{
		if (mainClasses.ContainsKey(typeMetadata.Hash)) return;
		mainClasses.TryAdd(typeMetadata.Hash, typeMetadata);
		MainClasses.AppendMainClass(mainClasses, typeMetadata as JReferenceTypeMetadata);
	}
}

/// <summary>
/// Stores initial classes.
/// </summary>
/// <typeparam name="TClass">Type of class.</typeparam>
#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS1694,
                 Justification = CommonConstants.InternalInheritanceJustification)]
#endif
internal abstract class MainClasses<TClass> : MainClasses where TClass : JReferenceObject
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
}