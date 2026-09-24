namespace Rxmxnx.JNetInterface.Internal;

internal sealed partial class EnvironmentCore
{
	/// <summary>
	/// Represents a parameter slot used for storing JNI call parameters data.
	/// </summary>
	internal readonly struct ParameterSlot(
		EnvironmentCore core,
		INativeTransaction jniTransaction,
		ValPtr<JValue> buffer,
		Int32 count) : IParameterSlot
	{
		/// <summary>
		/// Configures the parameter slot with the provided arguments and call definition.
		/// </summary>
		/// <typeparam name="TArgs">The type of the arguments for the JNI call.</typeparam>
		/// <param name="args">The reference to the arguments to configure. If null, the parameter buffer is cleared.</param>
		/// <param name="callDefinition">The definition of the JNI call to configure the arguments with.</param>
#if !PACKAGE
		[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
		                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
		public unsafe void Configure<TArgs>(ref TArgs? args, JCallDefinition callDefinition)
#if !NET9_0_OR_GREATER
			where TArgs : ICallArgument
#else
			where TArgs : ICallArgument, allows ref struct
#endif
		{
			NativeMemory.Clear(buffer, (UIntPtr)(count * sizeof(JValue)));
			if (args is not null)
				args.Configure(this, callDefinition);
		}

		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe void SetParameterValues<TObject>(Byte index, ReadOnlySpan<TObject?> values)
			where TObject : IObject
		{
			if (values.Length == 0) return;
			ImplementationValidationUtilities.ThrowIfInvalidIndex(index, count);
			ImplementationValidationUtilities.ThrowIfInvalidIndex(index + values.Length - 1, count);
			Span<JValue> result = buffer.Pointer.GetUnsafeSpan<JValue>(values.Length);
			if (!RuntimeHelpers.IsReferenceOrContainsReferences<TObject>())
			{
				delegate*<void*, Span<JValue>, void> copy = Unsafe.SizeOf<TObject>() switch
				{
					1 => &ParameterSlot.CopyInteger<Byte>,
					2 => &ParameterSlot.CopyInteger<UInt16>,
					4 => &ParameterSlot.CopyInteger<UInt32>,
					8 => &ParameterSlot.CopyInteger<UInt64>,
					_ => throw new InvalidOperationException(IMessageResource.GetInstance().NotPrimitiveObject),
				};
				fixed (void* ptr = &Unsafe.As<TObject?, Byte>(ref MemoryMarshal.GetReference(values)))
					copy(ptr, result);
				return;
			}
			for (Int32 i = 0; i < values.Length; i++)
			{
				TObject? value = values[i];
				if (value is null) continue;
				if (value is not JReferenceObject jObject)
				{
					value.CopyTo(result, i);
					continue;
				}
				ImplementationValidationUtilities.ThrowIfProxy(jObject);
				core.ReloadClass(jObject as JClassObject);
				ImplementationValidationUtilities.ThrowIfDefault(jObject, index);
				Unsafe.As<JValue, JObjectLocalRef>(ref result[i]) = jniTransaction.Add(jObject);
			}
		}
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetParameterValue<TObject>(Byte index, TObject? value) where TObject : IObject
		{
			ImplementationValidationUtilities.ThrowIfInvalidIndex(index, count);
			ValPtr<JValue> result = buffer + index;
			if (value is null) return;
			if (value is not JReferenceObject jObject)
			{
				// It's a primitive value. No boxing!
				value.GetValue(out result.Reference);
				return;
			}
			ImplementationValidationUtilities.ThrowIfProxy(jObject);
			core.ReloadClass(jObject as JClassObject);
			ImplementationValidationUtilities.ThrowIfDefault(jObject, index);
			Unsafe.As<JValue, JObjectLocalRef>(ref result.Reference) = jniTransaction.Add(jObject);
		}
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetParameterNull(Byte index) => Unsafe.Add(ref buffer.Reference, index) = JValue.Empty;

		/// <summary>
		/// Copies the primitive values from the given pointer to the given span.
		/// </summary>
		/// <typeparam name="TInteger">Type of <see cref="IPrimitiveType"/> value type.</typeparam>
		/// <param name="ptr">Pointer to primitives.</param>
		/// <param name="destination">Destination span.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static unsafe void CopyInteger<TInteger>(void* ptr, Span<JValue> destination)
			where TInteger : unmanaged, IBinaryInteger<TInteger>, IUnsignedNumber<TInteger>
		{
			ReadOnlySpan<TInteger> source = new(ptr, destination.Length);
			for (Int32 i = 0; i < source.Length; i++)
				Unsafe.As<JValue, TInteger>(ref destination[i]) = source[i];
		}
	}
}