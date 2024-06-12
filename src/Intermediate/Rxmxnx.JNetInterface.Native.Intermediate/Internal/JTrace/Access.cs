namespace Rxmxnx.JNetInterface.Internal;

[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6670,
                 Justification = CommonConstants.NonStandardTraceJustification)]
internal static partial class JTrace
{
	/// <summary>
	/// Writes a category name and the retrieval of a field to the trace listeners.
	/// </summary>
	/// <param name="jLocal">Field instance object class.</param>
	/// <param name="jClass">Field declaring class.</param>
	/// <param name="definition">Call definition.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetField(JLocalObject? jLocal, JClassObject jClass, JFieldDefinition definition,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(jLocal is null ? //Static?
			                $"thread: {Environment.CurrentManagedThreadId} {jClass.ToTraceText()} {definition.ToTraceText()}" :
			                $"thread: {Environment.CurrentManagedThreadId} {jLocal.ToTraceText()} {definition.ToTraceText()}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the assignment of a value to a field to the trace listeners.
	/// </summary>
	/// <param name="jLocal">Field instance object class.</param>
	/// <param name="jClass">Field declaring class.</param>
	/// <param name="definition">Call definition.</param>
	/// <param name="value">Value to set.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void SetField<TDataType>(JLocalObject? jLocal, JClassObject jClass, JFieldDefinition definition,
		TDataType? value, [CallerMemberName] String callerMethod = "") where TDataType : IObject
	{
		if (!IVirtualMachine.TraceEnabled) return;
		String textValue = value is not null ?
			(value as JReferenceObject)?.ToTraceText() ?? $"{value.ObjectSignature}: {value}" :
			"value: null";
		Trace.WriteLine(jLocal is null ? //Static?
			                $"thread: {Environment.CurrentManagedThreadId} {jClass.ToTraceText()} {definition.ToTraceText()} {textValue}" :
			                $"thread: {Environment.CurrentManagedThreadId} {jLocal.ToTraceText()} {definition.ToTraceText()} {textValue}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the assignment of a value to a primitive field to the trace listeners.
	/// </summary>
	/// <param name="jLocal">Field instance object class.</param>
	/// <param name="jClass">Field declaring class.</param>
	/// <param name="definition">Call definition.</param>
	/// <param name="bytes">Binary span containing value to set to.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void SetField<TPrimitive>(JLocalObject? jLocal, JClassObject jClass, JFieldDefinition definition,
		ReadOnlySpan<Byte> bytes, [CallerMemberName] String callerMethod = "")
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		if (!IVirtualMachine.TraceEnabled) return;
		JPrimitiveTypeMetadata typeMetadata = IPrimitiveType.GetMetadata<TPrimitive>();
		JTrace.SetField(jLocal, jClass, definition, typeMetadata.CreateInstance(bytes), callerMethod);
	}
	/// <summary>
	/// Writes a category name and the call of a method to the trace listeners.
	/// </summary>
	/// <param name="jLocal">Call instance object class.</param>
	/// <param name="jClass">Method declaring class.</param>
	/// <param name="definition">Call definition.</param>
	/// <param name="nonVirtual">Indicates call is non-virtual.</param>
	/// <param name="args">Call parameters.</param>
	/// <param name="callerMethod">Caller member name.</param>
	internal static void CallMethod(JLocalObject? jLocal, JClassObject jClass, JCallDefinition definition,
		Boolean nonVirtual, IObject?[] args, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		StringBuilder strBuilder = new();
		if (jLocal is null)
			if (CommonNames.Constructor().SequenceEqual(definition.Name))
				strBuilder.AppendLine(
					$"thread: {Environment.CurrentManagedThreadId} {jClass.Name} {definition.ToTraceText()}");
			else
				strBuilder.AppendLine(
					$"thread: {Environment.CurrentManagedThreadId} {jClass.Name} static {definition.ToTraceText()}");
		else if (nonVirtual)
			strBuilder.AppendLine(
				$"thread: {Environment.CurrentManagedThreadId} {jLocal.ToTraceText()} {jClass.Name} non-virtual {definition.ToTraceText()}");
		else
			strBuilder.AppendLine(
				$"thread: {Environment.CurrentManagedThreadId} {jLocal.ToTraceText()} {definition.ToTraceText()}");
		for (Int32 i = 0; i < args.Length; i++)
		{
			switch (args[i])
			{
				case IPrimitiveType jPrimitive:
					strBuilder.AppendLine($"{i}: {jPrimitive.ObjectSignature} {jPrimitive}");
					break;
				case JReferenceObject jObject:
					strBuilder.AppendLine($"{i}: {jObject.ToTraceText()}");
					break;
				default:
					strBuilder.AppendLine($"{i}: null");
					break;
			}
		}
		Trace.WriteLine(strBuilder, callerMethod);
	}
	/// <summary>
	/// Writes a category name and the assignment of a value to a primitive field to the trace listeners.
	/// </summary>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="signature">Primitive char signature.</param>
	/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
	/// <param name="value">Value to set to.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void SetPrimitiveField<TValue>(JObjectLocalRef localRef, JClassLocalRef classRef, Byte signature,
		JFieldId fieldId, TValue value, [CallerMemberName] String callerMethod = "") where TValue : unmanaged
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(localRef == default ? //Static?
			                $"thread: {Environment.CurrentManagedThreadId} {classRef} {fieldId} {(Char)signature}: {value}" :
			                $"thread: {Environment.CurrentManagedThreadId} {localRef} {fieldId} {(Char)signature}: {value}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the assignment of a value to an object field to the trace listeners.
	/// </summary>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
	/// <param name="value">Value to set to.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void SetObjectField(JObjectLocalRef localRef, JClassLocalRef classRef, JFieldId fieldId,
		JObjectLocalRef value, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(localRef == default ? //Static?
			                $"thread: {Environment.CurrentManagedThreadId} {classRef} {fieldId} {value}" :
			                $"thread: {Environment.CurrentManagedThreadId} {localRef} {fieldId} {value}", callerMethod);
	}
	/// <summary>
	/// Writes a category name and the retrieval of a primitive field to the trace listeners.
	/// </summary>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="signature">Primitive char signature.</param>
	/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
	/// <param name="result">Current value.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetPrimitiveField<TValue>(JObjectLocalRef localRef, JClassLocalRef classRef, Byte signature,
		JFieldId fieldId, TValue result, [CallerMemberName] String callerMethod = "") where TValue : unmanaged
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(localRef == default ? //Static?
			                $"thread: {Environment.CurrentManagedThreadId} {classRef} {fieldId} Result {(Char)signature}: {result}" :
			                $"thread: {Environment.CurrentManagedThreadId} {localRef} {fieldId} Result {(Char)signature}: {result}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the retrieval of an object field to the trace listeners.
	/// </summary>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="fieldId"><see cref="JFieldId"/> identifier.</param>
	/// <param name="result">Current value.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetObjectField(JObjectLocalRef localRef, JClassLocalRef classRef, JFieldId fieldId,
		JObjectLocalRef result, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(localRef == default ? //Static?
			                $"thread: {Environment.CurrentManagedThreadId} {classRef} {fieldId} Result {result}" :
			                $"thread: {Environment.CurrentManagedThreadId} {localRef} {fieldId} Result {result}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the invocation of a primitive function to the trace listeners.
	/// </summary>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="signature">Primitive char signature.</param>
	/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
	/// <param name="result">Current value.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void CallPrimitiveFunction<TValue>(JObjectLocalRef localRef, JClassLocalRef classRef, Byte signature,
		JMethodId methodId, TValue result, [CallerMemberName] String callerMethod = "") where TValue : unmanaged
	{
		if (!IVirtualMachine.TraceEnabled) return;
		if (localRef == default) //Static
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} {classRef} {methodId} Result {(Char)signature}: {result}",
				callerMethod);
		else if (classRef.IsDefault) //Instance
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} {localRef} {methodId} Result {(Char)signature}: {result}",
				callerMethod);
		else //Non-Virtual
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} {localRef} {classRef} {methodId} Result {(Char)signature}: {result}",
				callerMethod);
	}
	/// <summary>
	/// Writes a category name and the invocation of an object function to the trace listeners.
	/// </summary>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
	/// <param name="result">Current value.</param>
	/// <param name="isConstructor">Indicates whether call is for a constructor.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void CallObjectFunction(JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		JObjectLocalRef result, Boolean isConstructor, [CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;

		if (localRef == default) //Static or Constructor
			Trace.WriteLine(
				!isConstructor ?
					$"thread: {Environment.CurrentManagedThreadId} {classRef} {methodId} Result {result}" :
					$"{classRef} {methodId} New {result}", callerMethod);
		else if (classRef.IsDefault) //Instance
			Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {localRef} {methodId} Result {result}",
			                callerMethod);
		else //Non-Virtual
			Trace.WriteLine(
				$"thread: {Environment.CurrentManagedThreadId} {localRef} {classRef} {methodId} Result {result}",
				callerMethod);
	}
	/// <summary>
	/// Writes a category name and the call of method to the trace listeners.
	/// </summary>
	/// <param name="localRef"><see cref="JObjectLocalRef"/> reference.</param>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="methodId"><see cref="JMethodId"/> identifier.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void CallMethod(JObjectLocalRef localRef, JClassLocalRef classRef, JMethodId methodId,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;

		if (localRef == default) //Static
			Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {classRef} {methodId}", callerMethod);
		else if (classRef.IsDefault) //Instance
			Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {localRef} {methodId}", callerMethod);
		else //Non-Virtual
			Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {localRef} {classRef} {methodId}",
			                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the retrieving accessible identifier to the trace listeners.
	/// </summary>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="definition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetAccessibleId(JClassLocalRef classRef, JAccessibleObjectDefinition definition,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine($"thread: {Environment.CurrentManagedThreadId} {classRef} {definition.ToTraceText()}",
		                callerMethod);
	}
	/// <summary>
	/// Writes a category name and the retrieving accessible identifier to the trace listeners.
	/// </summary>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="definition">A <see cref="JAccessibleObjectDefinition"/> instance.</param>
	/// <param name="accessibleId">Accessible object identifier.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetAccessibleId<TAccessibleId>(JClassLocalRef classRef, JAccessibleObjectDefinition definition,
		TAccessibleId accessibleId, [CallerMemberName] String callerMethod = "")
		where TAccessibleId : unmanaged, IAccessibleIdentifierType,
		IEqualityOperators<TAccessibleId, TAccessibleId, Boolean>
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			accessibleId != default ?
				$"thread: {Environment.CurrentManagedThreadId} {classRef} {definition.ToTraceText()} {accessibleId}" :
				$"thread: {Environment.CurrentManagedThreadId} {classRef} {definition.ToTraceText()} Not found.",
			callerMethod);
	}
	/// <summary>
	/// Writes a category name and the retrieving access cache to the trace listeners.
	/// </summary>
	/// <param name="classRef"><see cref="JClassLocalRef"/> reference.</param>
	/// <param name="type">Cache reference.</param>
	/// <param name="exists">Indicates whether exists an access cache for this class.</param>
	/// <param name="callerMethod">Caller member name.</param>
	public static void GetAccessCache(JClassLocalRef classRef, JReferenceType type, Boolean exists,
		[CallerMemberName] String callerMethod = "")
	{
		if (!IVirtualMachine.TraceEnabled) return;
		Trace.WriteLine(
			exists ?
				$"thread: {Environment.CurrentManagedThreadId} {classRef} {type} Found." :
				$"thread: {Environment.CurrentManagedThreadId} {classRef} Not found.", callerMethod);
	}
}