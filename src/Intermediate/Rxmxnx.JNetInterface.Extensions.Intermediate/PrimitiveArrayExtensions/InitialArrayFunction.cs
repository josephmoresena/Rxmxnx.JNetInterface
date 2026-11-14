namespace Rxmxnx.JNetInterface;

public static partial class PrimitiveArrayExtensions
{
	/// <summary>
	/// Creates and fills the initial multidimensional array inside a local frame.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType{TPrimitive}"/> type.</typeparam>
#if !PACKAGE
	[ExcludeFromCodeCoverage]
#endif
#if NET9_0_OR_GREATER
	private ref struct InitialArrayFunction<TPrimitive> : IFrameFunction<JArrayObject>
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		/// <summary>
		/// Represents a mapping structure used for filling and initializing a multidimensional array
		/// within the context of an array creation function.
		/// </summary>
		private ArrayFillMap<TPrimitive> _map;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="map">A <see cref="ArrayFillMap{TPrimitive}"/> instance used to fill the array.</param>
		public InitialArrayFunction(ArrayFillMap<TPrimitive> map) => this._map = map;
#else
	private struct InitialArrayFunction<TPrimitive> : IFixedPointerListFunction<JArrayObject>,
		IFrameFunction<JArrayObject> where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
	{
		/// <summary>
		/// The <see cref="JArrayTypeMetadata"/> instance for the array.
		/// </summary>
		private readonly JArrayTypeMetadata _arrayTypeMetadata;
		/// <summary>
		/// The current <see cref="IEnvironment"/> instance.
		/// </summary>
		private readonly IEnvironment _environment;
		/// <summary>
		/// The size of the required frame.
		/// </summary>
		private readonly Int32 _frameSize;
		/// <summary>
		/// Current <see cref="ArrayFillMap{TPrimitive}"/> instance.
		/// </summary>
		/// <remarks>
		/// The value of this field is build on the <see cref="IFixedPointerListFunction{TResult}.Apply(FixedPointerValueList)"/>
		/// method.
		/// </remarks>
		private ArrayFillMap<TPrimitive> _map;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="arrayTypeMetadata">The <see cref="JArrayTypeMetadata"/> instance for the array.</param>
		/// <param name="environment">The current <see cref="IEnvironment"/> instance.</param>
		/// <param name="frameSize">The size of the frame.</param>
		public InitialArrayFunction(JArrayTypeMetadata arrayTypeMetadata, IEnvironment environment, Int32 frameSize)
		{
			this._arrayTypeMetadata = arrayTypeMetadata;
			this._environment = environment;
			this._frameSize = frameSize;
			this._map = default;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		JArrayObject IFixedPointerListFunction<JArrayObject>.Apply(scoped FixedPointerValueList list)
		{
			FixedPointerValueList.ItemValue dimensions = list[0];
			FixedPointerValueList.ItemValue memory = list[1];
			ReadOnlyValPtr<Int32> dimensionsPtr = (ReadOnlyValPtr<Int32>)dimensions.Pointer;
			ReadOnlyValPtr<TPrimitive> memoryPtr = (ReadOnlyValPtr<TPrimitive>)memory.Pointer;
			this._map = new(new(dimensionsPtr, dimensions.Count, memoryPtr, memory.Count))
			{
				ArrayTypeMetadata = this._arrayTypeMetadata,
			};
			this.WithFrame(this._environment, this._frameSize, out JArrayObject result);
			return result;
		}
#endif
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		JArrayObject IFrameFunction<JArrayObject>.Apply(IEnvironment env,
			scoped ReadOnlySpan<JReferenceObject?> objects)
		{
			JArrayObject result = this._map.ArrayTypeMetadata.CreateInstance(env, this._map.State.GetLength(0));
			PrimitiveArrayExtensions.FillArray(result, 0, ref this._map.State);
			return result;
		}
	}
}