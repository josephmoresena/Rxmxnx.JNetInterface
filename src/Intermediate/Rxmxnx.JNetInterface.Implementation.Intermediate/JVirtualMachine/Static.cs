namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	/// <summary>
	/// Indicates whether trace output is enabled.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean TraceEnabled => JTrace.TraceEnabled;
	/// <summary>
	/// Indicates whether final user-types should be treated as real classes at runtime.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean FinalUserTypeRuntimeEnabled => MetadataHelper.FinalUserTypeRuntimeEnabled;
	/// <summary>
	/// Indicates whether native call adapters should check parameter references type.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean CheckRefTypeNativeCallEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !AppContext.TryGetSwitch("JNetInterface.DisableCheckRefTypeNativeCall", out Boolean disable) || !disable;
	}
	/// <summary>
	/// Indicates whether native call adapters should check parameter class object class.
	/// </summary>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	public static Boolean CheckClassRefNativeCallEnabled
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
			=> !AppContext.TryGetSwitch("JNetInterface.DisableCheckClassRefNativeCall", out Boolean disable) ||
				!disable;
	}
	/// <summary>
	/// Main classes' information.
	/// </summary>
	public static IEnumerable<ITypeInformation> MainClassesInformation => JVirtualMachine.userMainClasses.Values;

	/// <summary>
	/// Registers <typeparamref name="TReference"/> as valid datatype for the current process.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TDataType}"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current datatype was registered; otherwise, <see langword="false"/>.
	/// </returns>
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public static Boolean Register<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
		=> MetadataHelper.IsCompileCompliant<TReference>() && MetadataHelper.Register<TReference>();
	/// <summary>
	/// Retrieves the <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <returns>The <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.</returns>
	public static IVirtualMachine GetVirtualMachine(JVirtualMachineRef reference)
		=> ReferenceCache.Instance.Get(reference, out _);

	/// <summary>
	/// Removes the <see cref="IVirtualMachine"/> instance referenced by <paramref name="reference"/>.
	/// </summary>
	/// <param name="reference">A <see cref="JVirtualMachineRef"/> reference.</param>
	/// <returns>
	/// <set langword="true"/> if the <see cref="IVirtualMachine"/> instance referenced by
	/// <paramref name="reference"/> was removed successfully; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean RemoveVirtualMachine(JVirtualMachineRef reference)
	{
		ReferenceCache.Instance.Get(reference)?._cache.ClearCache();
		return ReferenceCache.Instance.Remove(reference);
	}
	/// <summary>
	/// Sets <typeparamref name="TReference"/> as main class.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
#if !NET8_0_OR_GREATER
	[UnconditionalSuppressMessage("Trimming", "IL2091")]
#endif
	public static void SetMainClass<TReference>() where TReference : JReferenceObject, IReferenceType<TReference>
	{
		if (JavaStandardFeature.IsFixedRuntimeVersion && TReference.Since is JRuntimeVersion.Undefined)
			return; // Datatype is not compatible with Java Standard Edition.
		if (AndroidFeature.IsFixedAndroid && TReference.AndroidApiLevel == -1)
			return; // Datatype is not compatible with Android Runtime.
		if (AndroidFeature.ApiLevel is { } apiLevel && apiLevel < TReference.AndroidApiLevel)
			return; // Fixed Android API level doesn't support the type. 
		if (JavaStandardFeature.GetRuntimeVersion() is { } jreVersion && jreVersion < TReference.Since)
			return; // Fixed Java runtime version doesn't support the type.
		JDataTypeMetadata typeMetadata = MetadataHelper.GetExactMetadata<TReference>();
		MainClasses.AppendMainClass(JVirtualMachine.userMainClasses, typeMetadata);
	}
}