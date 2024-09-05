namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class helper stores a <see cref="JDataTypeMetadata"/>
/// </summary>
internal static partial class MetadataHelper
{
	/// <summary>
	/// Indicates whether <typeparamref name="TDataType"/> is final type.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type</typeparam>
	/// <returns>
	/// <see langword="true"/> if <typeparamref name="TDataType"/> is final type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean IsFinalType<TDataType>() where TDataType : IDataType<TDataType>
	{
		JDataTypeMetadata typeMetadata = MetadataHelper.GetExactMetadata<TDataType>();
		if (typeMetadata.Modifier is not JTypeModifier.Final)
			return false;
		return JVirtualMachine.FinalUserTypeRuntimeEnabled || MetadataHelper.IsBuiltInFinalType(typeMetadata);
	}
	/// <summary>
	/// Retrieves <see cref="JArgumentMetadata"/> metadata for <paramref name="jClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JArgumentMetadata"/> instance.</returns>
	public static JArgumentMetadata GetArgumentMetadata(JClassObject jClass)
	{
		switch (jClass.ClassSignature[0])
		{
			case CommonNames.BooleanSignatureChar:
				return JArgumentMetadata.Get<JBoolean>();
			case CommonNames.ByteSignatureChar:
				return JArgumentMetadata.Get<JByte>();
			case CommonNames.CharSignatureChar:
				return JArgumentMetadata.Get<JChar>();
			case CommonNames.DoubleSignatureChar:
				return JArgumentMetadata.Get<JDouble>();
			case CommonNames.FloatSignatureChar:
				return JArgumentMetadata.Get<JFloat>();
			case CommonNames.IntSignatureChar:
				return JArgumentMetadata.Get<JInt>();
			case CommonNames.LongSignatureChar:
				return JArgumentMetadata.Get<JLong>();
			case CommonNames.ShortSignatureChar:
				return JArgumentMetadata.Get<JShort>();
			default:
				if (MetadataHelper.runtimeMetadata.TryGetValue(jClass.Hash, out JReferenceTypeMetadata? typeMetadata))
				{
					MetadataHelper.reflectionMetadata.TryRemove(jClass.Hash, out _); // Removes unknown if exists.
					return typeMetadata.ArgumentMetadata;
				}
				if (MetadataHelper.reflectionMetadata.TryGetValue(jClass.Hash, out UnknownReflectionMetadata unknown))
					return unknown.ArgumentMetadata;

				// Create unknown metadata for reflection.
				unknown = new(jClass.Name);
				MetadataHelper.reflectionMetadata[jClass.Hash] = unknown;
				return unknown.ArgumentMetadata;
		}
	}
	/// <summary>
	/// Retrieves <see cref="JFieldDefinition"/> metadata for <paramref name="jClass"/> and
	/// <paramref name="fieldName"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="fieldName">Definition field name.</param>
	/// <returns>A <see cref="JFieldDefinition"/> instance.</returns>
	public static JFieldDefinition GetFieldDefinition(JClassObject jClass, ReadOnlySpan<Byte> fieldName)
	{
		switch (jClass.ClassSignature[0])
		{
			case CommonNames.BooleanSignatureChar:
				return new JFieldDefinition<JBoolean>(fieldName);
			case CommonNames.ByteSignatureChar:
				return new JFieldDefinition<JByte>(fieldName);
			case CommonNames.CharSignatureChar:
				return new JFieldDefinition<JChar>(fieldName);
			case CommonNames.DoubleSignatureChar:
				return new JFieldDefinition<JDouble>(fieldName);
			case CommonNames.FloatSignatureChar:
				return new JFieldDefinition<JFloat>(fieldName);
			case CommonNames.IntSignatureChar:
				return new JFieldDefinition<JInt>(fieldName);
			case CommonNames.LongSignatureChar:
				return new JFieldDefinition<JLong>(fieldName);
			case CommonNames.ShortSignatureChar:
				return new JFieldDefinition<JShort>(fieldName);
			default:
				if (MetadataHelper.runtimeMetadata.TryGetValue(jClass.Hash, out JReferenceTypeMetadata? typeMetadata))
				{
					MetadataHelper.reflectionMetadata.TryRemove(jClass.Hash, out _); // Removes unknown if exists.
					return typeMetadata.CreateFieldDefinition(fieldName);
				}
				if (MetadataHelper.reflectionMetadata.TryGetValue(jClass.Hash, out UnknownReflectionMetadata unknown))
					return unknown.CreateFieldDefinition(fieldName);

				// Create unknown metadata for reflection.
				unknown = new(jClass.Name);
				MetadataHelper.reflectionMetadata[jClass.Hash] = unknown;
				return unknown.CreateFieldDefinition(fieldName);
		}
	}
	/// <summary>
	/// Retrieves <see cref="JFieldDefinition"/> metadata for <paramref name="jClass"/>,
	/// <paramref name="callName"/> and <paramref name="paramsMetadata"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="callName">Definition call name.</param>
	/// <param name="paramsMetadata">Definition parameters metadata.</param>
	/// <returns>A <see cref="JCallDefinition"/> instance.</returns>
	public static JCallDefinition GetCallDefinition(JClassObject jClass, ReadOnlySpan<Byte> callName,
		ReadOnlySpan<JArgumentMetadata> paramsMetadata)
	{
		switch (jClass.ClassSignature[0])
		{
			case CommonNames.VoidSignatureChar: // Void call is a Method.
				return JMethodDefinition.Create(callName, paramsMetadata);
			case CommonNames.BooleanSignatureChar:
				return JFunctionDefinition<JBoolean>.Create(callName, paramsMetadata);
			case CommonNames.ByteSignatureChar:
				return JFunctionDefinition<JByte>.Create(callName, paramsMetadata);
			case CommonNames.CharSignatureChar:
				return JFunctionDefinition<JChar>.Create(callName, paramsMetadata);
			case CommonNames.DoubleSignatureChar:
				return JFunctionDefinition<JDouble>.Create(callName, paramsMetadata);
			case CommonNames.FloatSignatureChar:
				return JFunctionDefinition<JFloat>.Create(callName, paramsMetadata);
			case CommonNames.IntSignatureChar:
				return JFunctionDefinition<JInt>.Create(callName, paramsMetadata);
			case CommonNames.LongSignatureChar:
				return JFunctionDefinition<JLong>.Create(callName, paramsMetadata);
			case CommonNames.ShortSignatureChar:
				return JFunctionDefinition<JShort>.Create(callName, paramsMetadata);
			default:
				if (MetadataHelper.runtimeMetadata.TryGetValue(jClass.Hash, out JReferenceTypeMetadata? typeMetadata))
				{
					MetadataHelper.reflectionMetadata.TryRemove(jClass.Hash, out _); // Removes unknown if exists.
					return typeMetadata.CreateFunctionDefinition(callName, paramsMetadata);
				}
				if (MetadataHelper.reflectionMetadata.TryGetValue(jClass.Hash, out UnknownReflectionMetadata unknown))
					return unknown.CreateFunctionDefinition(callName, paramsMetadata);

				// Create unknown metadata for reflection.
				unknown = new(jClass.Name);
				MetadataHelper.reflectionMetadata[jClass.Hash] = unknown;
				return unknown.CreateFunctionDefinition(callName, paramsMetadata);
		}
	}
	/// <summary>
	/// Retrieves metadata from hash.
	/// </summary>
	/// <param name="hash">A JNI class hash.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JReferenceTypeMetadata? GetMetadata(String hash)
	{
		JReferenceTypeMetadata? result = MetadataHelper.runtimeMetadata.GetValueOrDefault(hash);
		if (result is not null) return result;
		if (MetadataHelper.classTree.TryGetValue(hash, out String? value))
			result = MetadataHelper.GetMetadata(value); // Retrieves metadata from cache.
		else if (MetadataHelper.viewTree.TryGetValue(hash, out HashesSet? set))
			result = set.GetViewMetadata();
		return result;
	}
	/// <summary>
	/// Retrieves exact metadata from hash.
	/// </summary>
	/// <param name="hash">A JNI class hash.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JReferenceTypeMetadata? GetExactMetadata(String hash)
		=> MetadataHelper.runtimeMetadata.GetValueOrDefault(hash);
	/// <summary>
	/// Retrieves exact <see cref="JDataTypeMetadata"/> metadata.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>A <see cref="JDataTypeMetadata"/> metadata.</returns>
	public static JDataTypeMetadata GetExactMetadata<TDataType>() where TDataType : IDataType<TDataType>
	{
		MetadataHelper.Register<TDataType>();
		return IDataType.GetMetadata<TDataType>();
	}
	/// <summary>
	/// Retrieves exact array metadata from element class hash.
	/// </summary>
	/// <param name="elementHash">A JNI class hash.</param>
	/// <returns>A <see cref="JReferenceTypeMetadata"/> instance.</returns>
	public static JArrayTypeMetadata? GetExactArrayMetadata(String elementHash)
	{
		JReferenceTypeMetadata? elementMetadata = MetadataHelper.runtimeMetadata.GetValueOrDefault(elementHash);
		JArrayTypeMetadata? result = elementMetadata?.GetArrayMetadata();
		MetadataHelper.Register(result);
		return result;
	}
	/// <summary>
	/// Retrieves exact array metadata from <paramref name="elementMetadata"/>.
	/// </summary>
	/// <param name="elementMetadata">A <see cref="JReferenceTypeMetadata"/> instance.</param>
	/// <returns>A <see cref="JArrayTypeMetadata"/> instance.</returns>
	public static JArrayTypeMetadata? GetExactArrayMetadata(JReferenceTypeMetadata? elementMetadata)
	{
		JArrayTypeMetadata? result = elementMetadata?.GetArrayMetadata();
		MetadataHelper.Register(result);
		return result;
	}
	/// <summary>
	/// Registers <typeparamref name="TDataType"/> as valid datatype for the current process.
	/// </summary>
	/// <typeparam name="TDataType">A <see cref="IDataType{TDataType}"/> type.</typeparam>
	/// <returns>
	/// <see langword="true"/> if current datatype was registered; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean Register<TDataType>() where TDataType : IDataType<TDataType>
	{
		JReferenceTypeMetadata? metadata = IDataType.GetMetadata<TDataType>() as JReferenceTypeMetadata;
		Boolean result = MetadataHelper.Register(metadata);
		return result;
	}
	/// <summary>
	/// Retrieves the class has from current <paramref name="className"/>.
	/// </summary>
	/// <param name="className">A java type name.</param>
	/// <param name="escape">Indicates whether <paramref name="className"/> should be escaped.</param>
	/// <returns><see cref="CStringSequence"/> with class information for given type.</returns>
	public static CStringSequence GetClassInformation(ReadOnlySpan<Byte> className, Boolean escape)
	{
		ReadOnlySpan<Byte> jniClassName = escape ? JDataTypeMetadata.JniEscapeClassName(className) : className;
		return JDataTypeMetadata.CreateInformationSequence(jniClassName);
	}
	/// <summary>
	/// Determines statically whether an object of <paramref name="jClass"/> can be safely cast to
	/// <paramref name="otherClass"/>.
	/// </summary>
	/// <param name="jClass">Java class instance.</param>
	/// <param name="otherClass">Other java class instance.</param>
	/// <returns>
	/// <see langword="true"/> if an object of <paramref name="jClass"/> can be safely cast to
	/// <paramref name="otherClass"/>; <see langword="null"/> if an object of <paramref name="otherClass"/>
	/// can be safely cast to <paramref name="jClass"/>; otherwise, <see langword="false"/>.
	/// </returns>
	public static Boolean? IsAssignable(JClassObject jClass, JClassObject otherClass)
	{
		AssignationKey key = new() { FromHash = jClass.Hash, ToHash = otherClass.Hash, };
		if (key.IsSame) return true;
		if (MetadataHelper.assignationCache.TryGetValue(key, out Boolean result))
			return result;
		return MetadataHelper.assignationCache.ContainsKey(key.Reverse()) ? false : default(Boolean?);
	}
	/// <summary>
	/// Sets <paramref name="isAssignable"/> as assignation from <paramref name="jClass"/> to
	/// <paramref name="otherClass"/>.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="otherClass">A <see cref="JClassObject"/> instance.</param>
	/// <param name="isAssignable">
	/// Indicates whether assignation from <paramref name="jClass"/> to <paramref name="otherClass"/> is allowed.
	/// </param>
	/// <returns>Value from <paramref name="isAssignable"/></returns>
	public static Boolean SetAssignable(JClassObject jClass, JClassObject otherClass, Boolean isAssignable)
	{
		AssignationKey key = new() { FromHash = jClass.Hash, ToHash = otherClass.Hash, };
		MetadataHelper.assignationCache[key] = isAssignable;
		return isAssignable;
	}
	/// <summary>
	/// Register class tree.
	/// </summary>
	/// <param name="hashClass">Hash class.</param>
	/// <param name="superClassHash">Super class hash.</param>
	public static void RegisterSuperClass(String hashClass, String superClassHash)
	{
		AssignationKey assignationKey = new() { FromHash = hashClass, ToHash = superClassHash, };
		if (assignationKey.IsSame) return;
		MetadataHelper.classTree[assignationKey.FromHash] = assignationKey.ToHash;
		MetadataHelper.assignationCache[assignationKey] = true;
	}
	/// <summary>
	/// Register class tree.
	/// </summary>
	/// <param name="hashView">Hash class view.</param>
	/// <param name="superViewHash">Super view hash.</param>
	public static void RegisterSuperView(String hashView, String superViewHash)
	{
		AssignationKey assignationKey = new() { FromHash = hashView, ToHash = superViewHash, };
		if (assignationKey.IsSame) return;
		if (!MetadataHelper.viewTree.TryGetValue(assignationKey.FromHash, out HashesSet? set))
		{
			set = new();
			MetadataHelper.viewTree[hashView] = set;
		}
		set.Add(superViewHash);
		MetadataHelper.assignationCache[assignationKey] = true;
	}
}