namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private partial record EnvironmentCache
	{
		private static JStringLocalRef CreateString(in IReadOnlyFixedContext<Char> ctx, EnvironmentCache cache)
		{
			NewStringDelegate newString = cache.GetDelegate<NewStringDelegate>();
			JStringLocalRef result = newString(cache.Reference, (ReadOnlyValPtr<Char>)ctx.Pointer, ctx.Values.Length);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}
		private static JStringLocalRef CreateUtf8String(in IReadOnlyFixedContext<Byte> ctx, EnvironmentCache cache)
		{
			NewStringUtfDelegate newUtf8String = cache.GetDelegate<NewStringUtfDelegate>();
			JStringLocalRef result = newUtf8String(cache.Reference, (ReadOnlyValPtr<Byte>)ctx.Pointer);
			if (result.Value == default) cache.CheckJniError();
			return result;
		}
		private static void GetStringRegion(in IFixedContext<Char> ctx,
			(EnvironmentCache cache, JStringObject jString, Int32 startIndex) args)
		{
			GetStringRegionDelegate getStringRegion = args.cache.GetDelegate<GetStringRegionDelegate>();
			using INativeTransaction jniTransaction = args.cache.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(args.jString);
			getStringRegion(args.cache.Reference, stringRef, args.startIndex, ctx.Values.Length,
			                (ValPtr<Char>)ctx.Pointer);
		}
	}
}