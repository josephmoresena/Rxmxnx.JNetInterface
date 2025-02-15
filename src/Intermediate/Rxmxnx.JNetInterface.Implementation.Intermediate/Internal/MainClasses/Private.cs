namespace Rxmxnx.JNetInterface.Internal;

internal abstract partial class MainClasses
{
	/// <summary>
	/// Appends <typeparamref name="TReference"/> as main class.
	/// </summary>
	/// <typeparam name="TReference">A <see cref="IReferenceType{TReference}"/> type.</typeparam>
	/// <param name="isMainClass">Indicates whether <typeparamref name="TReference"/> is main class.</param>
	/// <param name="mainClasses">Main classes dictionary.</param>
#pragma warning disable CA1859
	private static void AppendInitialClass<TReference>(Boolean isMainClass,
		IDictionary<String, JDataTypeMetadata> mainClasses)
		where TReference : JReferenceObject, IReferenceType<TReference>
	{
		if (!isMainClass) return;
		JReferenceTypeMetadata typeMetadata = (JReferenceTypeMetadata)MetadataHelper.GetExactMetadata<TReference>();
		mainClasses.TryAdd(typeMetadata.Hash, typeMetadata);

		if (typeMetadata.BaseMetadata?.Hash == ClassNameHelper.NumberHash)
			mainClasses.TryAdd(ClassNameHelper.NumberHash, typeMetadata.BaseMetadata);
	}
	/// <summary>
	/// Set main class.
	/// </summary>
	/// <param name="mainClasses">Main classes dictionary.</param>
	/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	[ExcludeFromCodeCoverage]
	private static void AppendMainClass(IDictionary<String, JDataTypeMetadata> mainClasses,
		JReferenceTypeMetadata? typeMetadata)
	{
		if (typeMetadata is null) return;
		JClassTypeMetadata? baseMetadata = MainClasses.MainSuperClass(typeMetadata);
		if (baseMetadata is not null && !mainClasses.ContainsKey(baseMetadata.Hash))
			mainClasses.TryAdd(baseMetadata.Hash, baseMetadata);
		JInterfaceTypeMetadata? interfaceMetadata = MainClasses.MainInterface(typeMetadata);
		if (interfaceMetadata is not null && !mainClasses.ContainsKey(interfaceMetadata.Hash))
			mainClasses.TryAdd(interfaceMetadata.Hash, interfaceMetadata);
	}
	/// <summary>
	/// Retrieves required main super class metadata for <paramref name="typeMetadata"/>.
	/// </summary>
	/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>A <see cref="JClassTypeMetadata"/> instance.</returns>
	[ExcludeFromCodeCoverage]
	private static JClassTypeMetadata? MainSuperClass(JReferenceTypeMetadata typeMetadata)
	{
		while (typeMetadata.BaseMetadata is { } baseMetadata)
		{
			switch (baseMetadata.Hash)
			{
				case ClassNameHelper.ObjectHash:
					return default;
				case ClassNameHelper.BufferHash:
				case ClassNameHelper.ExecutableHash:
				case ClassNameHelper.NumberHash:
				case ClassNameHelper.EnumHash:
					return baseMetadata;
				default:
					typeMetadata = baseMetadata;
					break;
			}
		}
		return default;
	}
	/// <summary>
	/// Retrieves required main interface metadata for <paramref name="typeMetadata"/>.
	/// </summary>
	/// <param name="typeMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>A <see cref="JClassTypeMetadata"/> instance.</returns>
	[ExcludeFromCodeCoverage]
	private static JInterfaceTypeMetadata? MainInterface(JReferenceTypeMetadata typeMetadata)
		=> typeMetadata.Hash switch
		{
			ClassNameHelper.ExecutableHash or ClassNameHelper.ConstructorHash or ClassNameHelper.MethodHash or
				ClassNameHelper.FieldHash => MetadataHelper.GetExactMetadata(ClassNameHelper.MemberHash) as
					JInterfaceTypeMetadata,
			_ => default,
		};
#pragma warning restore CA1859
}