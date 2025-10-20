using System.Globalization;

namespace Rxmxnx.JNetInterface;

#if !PACKAGE
[SuppressMessage(CommonConstants.CSharpSquid, CommonConstants.CheckIdS6640,
                 Justification = CommonConstants.SecureUnsafeCodeJustification)]
#endif
partial class JEnvironment
{
	private sealed unsafe partial class EnvironmentCache
	{
		JClassObject IClassFeature.ClassObject => this.GetLoadedClass(this.ClassObject);
		JClassObject IClassFeature.ThrowableObject => this.GetLoadedClass(this.ThrowableObject);
		JClassObject IClassFeature.StackTraceElementObject => this.GetLoadedClass(this.StackTraceElementObject);
		JClassObject IClassFeature.SystemObject => this.GetLoadedClass(this.SystemObject);

		JClassObject IClassFeature.VoidPrimitive => this.GetLoadedClass(this.VoidPrimitive);
		JClassObject IClassFeature.BooleanPrimitive => this.GetLoadedClass(this.BooleanPrimitive);
		JClassObject IClassFeature.BytePrimitive => this.GetLoadedClass(this.BytePrimitive);
		JClassObject IClassFeature.CharPrimitive => this.GetLoadedClass(this.CharPrimitive);
		JClassObject IClassFeature.DoublePrimitive => this.GetLoadedClass(this.DoublePrimitive);
		JClassObject IClassFeature.FloatPrimitive => this.GetLoadedClass(this.FloatPrimitive);
		JClassObject IClassFeature.IntPrimitive => this.GetLoadedClass(this.IntPrimitive);
		JClassObject IClassFeature.LongPrimitive => this.GetLoadedClass(this.LongPrimitive);
		JClassObject IClassFeature.ShortPrimitive => this.GetLoadedClass(this.ShortPrimitive);

		/// <summary>
		/// Retrieves JRE Version <c>java.specification.version</c> property.
		/// </summary>
		/// <param name="systemClassRef"><c>java.lang.System</c> class reference.</param>
		/// <param name="getPropertyMethodId"><c>String java.lang.System.getProperty(String)</c> method ID.</param>
		/// <returns>JRE Version.</returns>
		public Decimal GetRuntimeVersion(JClassLocalRef systemClassRef, JMethodId getPropertyMethodId)
		{
			JStringLocalRef propNameRef = this.CreateUtf8String("java.specification.version"u8);
			JStringLocalRef propValueRef = default;
			try
			{
				ref readonly NativeInterface nativeInterface =
					ref this.GetNativeInterface<NativeInterface>(NativeInterface.CallStaticObjectMethodInfo);
				JValue* ptr = stackalloc JValue[1];

				Unsafe.AsRef<JStringLocalRef>(ptr) = propNameRef;
				propValueRef = new(nativeInterface.StaticMethodFunctions.CallObjectMethod.Call(
					                   this.Reference, systemClassRef, getPropertyMethodId, ptr));
				if (propValueRef != default)
				{
					Int32 versionLength =
						nativeInterface.StringFunctions.Utf16.GetStringLength(this.Reference, propValueRef);
					if (versionLength > 0)
					{
						Char* charsPtr = stackalloc Char[versionLength];
						ReadOnlySpan<Char> chars = new(charsPtr, versionLength);
						nativeInterface.StringRegionFunctions.Utf16.GetStringRegion(
							this.Reference, propValueRef, 0, versionLength, charsPtr);
						if (Decimal.TryParse(chars, CultureInfo.InvariantCulture, out Decimal result)) return result;
					}
				}
			}
			finally
			{
				if (propNameRef != default)
					this._env.DeleteLocalRef(propNameRef.Value);
				if (propValueRef != default)
					this._env.DeleteLocalRef(propValueRef.Value);
			}
			return Decimal.Zero;
		}
	}
}