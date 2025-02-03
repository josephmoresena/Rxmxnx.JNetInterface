namespace Rxmxnx.JNetInterface;

public partial class JVirtualMachine
{
	private partial class GlobalMainClasses
	{
		/// <summary>
		/// Indicates whether VM initialization can continue if the class for <paramref name="typeInformation"/> does not exist.
		/// </summary>
		/// <param name="typeInformation">A <see cref="ITypeInformation"/> instance.</param>
		/// <returns>
		/// <see langword="true"/> if the class for <paramref name="typeInformation"/> is not mandatory for initialization;
		/// otherwise, <see langword="false"/>.
		/// </returns>
		[ExcludeFromCodeCoverage]
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
	}
}