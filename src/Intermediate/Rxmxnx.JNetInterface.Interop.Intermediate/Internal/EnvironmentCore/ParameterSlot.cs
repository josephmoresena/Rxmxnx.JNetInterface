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
		/// <inheritdoc/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetParameterValue<TObject>(Byte index, TObject? value) where TObject : IObject
		{
			ImplementationValidationUtilities.ThrowIfInvalidIndex(index, count);
			if (value is null)
			{
				this.SetNullValue(index);
				return;
			}
			ValPtr<JValue> result = buffer + index;
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
		public void SetNullValue(Byte index) => Unsafe.Add(ref buffer.Reference, index) = JValue.Empty;
	}
}