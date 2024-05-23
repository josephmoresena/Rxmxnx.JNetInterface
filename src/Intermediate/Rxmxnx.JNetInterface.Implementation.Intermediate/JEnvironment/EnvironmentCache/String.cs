namespace Rxmxnx.JNetInterface;

partial class JEnvironment
{
	private sealed partial record EnvironmentCache
	{
		private static unsafe JStringLocalRef CreateString(in IReadOnlyFixedContext<Char> ctx, EnvironmentCache cache)
		{
			ref readonly NativeInterface nativeInterface =
				ref cache.GetNativeInterface<NativeInterface>(NativeInterface.NewStringInfo);
			JStringLocalRef result =
				nativeInterface.StringFunctions.NewString(cache.Reference, (ReadOnlyValPtr<Char>)ctx.Pointer,
				                                          ctx.Values.Length);
			if (result.IsDefault) cache.CheckJniError();
			return result;
		}
		private static unsafe JStringLocalRef CreateUtf8String(in IReadOnlyFixedContext<Byte> ctx,
			EnvironmentCache cache)
		{
			ref readonly NativeInterface nativeInterface =
				ref cache.GetNativeInterface<NativeInterface>(NativeInterface.NewStringUtfInfo);
			JStringLocalRef result =
				nativeInterface.StringFunctions.NewStringUtf(cache.Reference, (ReadOnlyValPtr<Byte>)ctx.Pointer);
			if (result.IsDefault) cache.CheckJniError();
			return result;
		}
		private static unsafe void GetStringRegion(in IFixedContext<Char> ctx,
			(EnvironmentCache cache, JStringObject jString, Int32 startIndex) args)
		{
			ref readonly NativeInterface nativeInterface =
				ref args.cache.GetNativeInterface<NativeInterface>(NativeInterface.NewStringUtfInfo);
			using INativeTransaction jniTransaction = args.cache.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(args.jString);
			nativeInterface.StringRegionFunctions.Utf16.GetStringRegion(args.cache.Reference, stringRef,
			                                                            args.startIndex, ctx.Values.Length,
			                                                            (ValPtr<Char>)ctx.Pointer);
		}
		private static unsafe void GetStringUtf8Region(in IFixedContext<Byte> ctx,
			(EnvironmentCache cache, JStringObject jString, Int32 startIndex) args)
		{
			ref readonly NativeInterface nativeInterface =
				ref args.cache.GetNativeInterface<NativeInterface>(NativeInterface.NewStringUtfInfo);
			using INativeTransaction jniTransaction = args.cache.VirtualMachine.CreateTransaction(1);
			JStringLocalRef stringRef = jniTransaction.Add(args.jString);
			nativeInterface.StringRegionFunctions.Utf8.GetStringRegion(args.cache.Reference, stringRef, args.startIndex,
			                                                           ctx.Values.Length, (ValPtr<Byte>)ctx.Pointer);
		}
	}
}