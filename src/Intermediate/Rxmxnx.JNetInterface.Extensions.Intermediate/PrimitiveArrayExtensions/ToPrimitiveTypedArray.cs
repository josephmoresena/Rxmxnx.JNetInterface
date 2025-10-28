namespace Rxmxnx.JNetInterface;

public static partial class PrimitiveArrayExtensions
{
	/// <summary>
	/// Creates a java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	[return: NotNullIfNotNull(nameof(array))]
	public static JArrayObject<TPrimitive>? ToPrimitiveArray<TPrimitive>(this TPrimitive[]? array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<TPrimitive>, TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 2-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static JArrayObject<JArrayObject<TPrimitive>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions.CreateInitialArray<JArrayObject<JArrayObject<TPrimitive>>, TPrimitive>(
			array, env, array.AsSpan());
	/// <summary>
	/// Creates a 3-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>, TPrimitive>(
				array, env, array.AsSpan());
	/// <summary>
	/// Creates a 4-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>, TPrimitive>(
				array, env, array.AsSpan());
	/// <summary>
	/// Creates a 5-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 6-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 7-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>, TPrimitive>(
				array, env, array.AsSpan());
	/// <summary>
	/// Creates an 8-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>, TPrimitive>(
				array, env, array.AsSpan());
	/// <summary>
	/// Creates a 9-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>, TPrimitive>(
				array, env, array.AsSpan());
	/// <summary>
	/// Creates a 10-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>, TPrimitive>(
				array, env, array.AsSpan());
	/// <summary>
	/// Creates an 11-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>, TPrimitive>(
				array, env, array.AsSpan());
	/// <summary>
	/// Creates a 12-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>
				, TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 13-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 14-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 15-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 16-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 17-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>
				,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates an 18-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 19-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 20-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 21-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>
				, TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 22-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 23-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 24-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<
								JArrayObject<
									JArrayObject<
										JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 25-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<JArrayObject<
								JArrayObject<
									JArrayObject<
										JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 26-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<
								JArrayObject<
									JArrayObject<
										JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>
				, TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 27-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<
								JArrayObject<
									JArrayObject<
										JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 28-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<
									JArrayObject<
										JArrayObject<
											JArrayObject<
												JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 29-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<
									JArrayObject<
										JArrayObject<
											JArrayObject<
												JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 30-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
									JArrayObject<
										JArrayObject<
											JArrayObject<
												JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 31-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
									JArrayObject<
										JArrayObject<
											JArrayObject<
												JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
	/// <summary>
	/// Creates a 32-dimension java array of primitive values.
	/// </summary>
	/// <typeparam name="TPrimitive">The primitive type of the array elements.</typeparam>
	/// <param name="array">An array of <typeparamref name="TPrimitive"/> values used to initialize the java array.</param>
	/// <param name="env">A <see cref="IEnvironment"/> instance.</param>
	/// <returns>
	/// A newly created <see cref="JArrayObject"/> instance representing the array.
	/// </returns>
	public static
		JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
			JArrayObject
			<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
				JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		ToPrimitiveArray<TPrimitive>(this TPrimitive[,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,] array, IEnvironment env)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		=> PrimitiveArrayExtensions
			.CreateInitialArray<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
					JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
						JArrayObject<
							JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
								JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<JArrayObject<
									JArrayObject<JArrayObject<
										JArrayObject<
											JArrayObject<
												JArrayObject<JArrayObject<TPrimitive>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>,
				TPrimitive>(array, env, array.AsSpan());
}