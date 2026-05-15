// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMemberInSuper.Global

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
	// ReSharper disable MemberCanBeProtected.Global
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
	/// Sets <typeparamref name="TReference"/> as main class.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="mainClasses">Main classes dictionary.</param>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
#if !NET8_0_OR_GREATER || ANDROID
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public static void SetMainClass<TReference>(IDictionary<String, JDataTypeMetadata> mainClasses)
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
#if !ANDROID
		if (JavaStandardFeature.IsFixedRuntimeVersion && TReference.Since is JRuntimeVersion.Undefined)
			return; // Datatype is not compatible with Java Standard Edition.
#endif
		if (AndroidFeature.IsFixedAndroid && TReference.AndroidApiLevel == -1)
			return; // Datatype is not compatible with Android Runtime.
		if (AndroidFeature.ApiLevel is { } apiLevel && apiLevel < TReference.AndroidApiLevel)
			return; // Fixed Android API level doesn't support the type. 
#if !ANDROID
		if (JavaStandardFeature.GetRuntimeVersion() is { } jreVersion && jreVersion < TReference.Since)
			return; // Fixed Java runtime version doesn't support the type.
#endif
		JDataTypeMetadata typeMetadata = MetadataHelper.GetExactMetadata<TReference>();
		MainClasses.AppendMainClass(mainClasses, typeMetadata);
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
	/// <summary>
	/// Class for <see cref="SystemObject"/>
	/// </summary>
	public abstract TClass SystemObject { get; }
}