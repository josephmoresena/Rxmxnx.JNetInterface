namespace Rxmxnx.JNetInterface.Internal;

internal partial class GlobalMainClasses
{
	/// <summary>
	/// Indicates whether VM initialization can continue if the class for <paramref name="typeInformation"/> does not exist.
	/// </summary>
	/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the class for <paramref name="typeInformation"/> is not mandatory for initialization;
	/// otherwise, <see langword="false"/>.
	/// </returns>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
	private static Boolean CanProceedWithout(ITypeInformation typeInformation)
		=> typeInformation.Hash switch
		{
			ClassNameHelper.VoidObjectHash or ClassNameHelper.BooleanObjectHash or
				ClassNameHelper.CharacterObjectHash or ClassNameHelper.NumberHash or ClassNameHelper.EnumHash or
				ClassNameHelper.BufferHash or ClassNameHelper.MemberHash or ClassNameHelper.ExecutableHash or
				ClassNameHelper.MethodHash or ClassNameHelper.FieldHash or ClassNameHelper.ByteObjectHash or
				ClassNameHelper.DoubleObjectHash or ClassNameHelper.FloatObjectHash or
				ClassNameHelper.IntegerObjectHash or ClassNameHelper.LongObjectHash or
				ClassNameHelper.ShortObjectHash => false,
			_ => true,
			// If the class is not basic, VM initialization should continue.
		};
	/// <summary>
	/// Loads main class.
	/// </summary>
	/// <param name="loader">A <see cref="IMainClassLoader"/> instance.</param>
	/// <param name="mainClass">A <see cref="JGlobal"/> main class instance.</param>
	/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
	private static void LoadMainClass(IMainClassLoader loader, JGlobal mainClass, ITypeInformation typeInformation)
	{
		JGlobalRef globalRef = loader.GetMainClassGlobalRef(typeInformation);
		mainClass.SetValue(globalRef);
		JTrace.MainClassLoaded(typeInformation.Signature, globalRef);
	}
}