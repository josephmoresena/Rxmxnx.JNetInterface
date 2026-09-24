namespace Rxmxnx.JNetInterface.Internal;

// ReSharper disable once ClassCannotBeInstantiated
internal partial class NativeFunctionSetImpl
{
	/// <summary>
	/// Frame function to check if an array type is final.
	/// </summary>
	/// <param name="arrayClass">A <see cref="JClassObject"/> instance.</param>
#if !PACKAGE
	public readonly struct IsFinalArrayTypeFunc(JClassObject arrayClass) : IFrameFunction<Boolean>
#else
	private readonly struct IsFinalArrayTypeFunc(JClassObject arrayClass) : IFrameFunction<Boolean>
#endif
	{
		Int32 IFrameFunction<Boolean>.RequiredCapacity => IVirtualMachine.IsFinalArrayCapacity;
		Boolean IFrameFunction<Boolean>.Apply(IEnvironment env, ReadOnlySpan<JLocalObject?> objects)
		{
			Int32 dimension = arrayClass.ArrayDimension;
			if (dimension + 1 == arrayClass.ClassSignature.Length) return true;
			JClassObject elementClass =
				env.ClassFeature.GetClass(arrayClass.ClassSignature.AsSpan()[(dimension + 1)..^1]);
			_ = env.ClassFeature.GetTypeMetadata(elementClass);
			return elementClass.IsFinal;
		}
	}
}