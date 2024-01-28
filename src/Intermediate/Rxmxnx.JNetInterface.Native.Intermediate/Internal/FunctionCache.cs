namespace Rxmxnx.JNetInterface.Internal;

/// <summary>
/// This class stores a cache of heavily used functions.
/// </summary>
internal abstract class FunctionCache
{
	/// <summary>
	/// Returns the name of current instance.
	/// </summary>
	/// <param name="jEnum">A <see cref="JEnumObject"/> instance.</param>
	/// <returns>Returns the name of current instance.</returns>
	public abstract JStringObject GetName(JEnumObject jEnum);
	/// <summary>
	/// Returns the ordinal of <paramref name="jEnum"/>
	/// </summary>
	/// <param name="jEnum">A <see cref="JEnumObject"/> instance.</param>
	/// <returns>The ordinal of <paramref name="jEnum"/>.</returns>
	public abstract Int32 GetOrdinal(JEnumObject jEnum);

	/// <summary>
	/// Returns the fully qualified name of the class containing the execution point represented
	/// by this stack trace element.
	/// </summary>
	/// <param name="jStackTraceElement">A <see cref="JStackTraceElementObject"/> instance.</param>
	/// <returns>The fully qualified name of the class containing the execution point.</returns>
	public abstract JStringObject GetClassName(JStackTraceElementObject jStackTraceElement);
	/// <summary>
	/// Returns the line number of the source line containing the execution point represented
	/// by <paramref name="jStackTraceElement"/>.
	/// </summary>
	/// <param name="jStackTraceElement">A <see cref="JStackTraceElementObject"/> instance.</param>
	/// <returns>The line number of the source line containing the execution point.</returns>
	public abstract Int32 GetLineNumber(JStackTraceElementObject jStackTraceElement);
	/// <summary>
	/// Returns the name of the source file containing the execution point represented by this stack trace element.
	/// </summary>
	/// <param name="jStackTraceElement">A <see cref="JStackTraceElementObject"/> instance.</param>
	/// <returns>The name of the source file containing the execution point.</returns>
	public abstract JStringObject GetFileName(JStackTraceElementObject jStackTraceElement);
	/// <summary>
	/// Returns the name of the method containing the execution point represented by this stack trace element.
	/// </summary>
	/// <param name="jStackTraceElement">A <see cref="JStackTraceElementObject"/> instance.</param>
	/// <returns>The name of the method containing the execution point</returns>
	public abstract JStringObject GetMethodName(JStackTraceElementObject jStackTraceElement);
	/// <summary>
	/// Returns <see langword="true"/> if the method containing the execution point represented by this stack trace element is
	/// a native method.
	/// </summary>
	/// <param name="jStackTraceElement">A <see cref="JStackTraceElementObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if the method containing the execution point is a native method; otherwise,
	/// <see langword="false"/>.
	/// </returns>
	public abstract Boolean IsNativeMethod(JStackTraceElementObject jStackTraceElement);

	/// <summary>
	/// Returns the value of the specified number as a <typeparamref name="TPrimitive"/>, which may
	/// involve rounding or truncation.
	/// </summary>
	/// <typeparam name="TPrimitive">A <see cref="IPrimitiveType"/> numeric type.</typeparam>
	/// <param name="jNumber">A <see cref="JNumberObject"/> instance.</param>
	/// <returns>A <typeparamref name="TPrimitive"/> numeric value.</returns>
	public abstract TPrimitive GetPrimitiveValue<TPrimitive>(JNumberObject jNumber)
		where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>, IBinaryNumber<TPrimitive>, ISignedNumber<TPrimitive>;

	/// <summary>
	/// Retrieves the throwable message.
	/// </summary>
	/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance.</param>
	/// <returns>Throwable message.</returns>
	public abstract JStringObject GetMessage(JThrowableObject jThrowable);
	/// <summary>
	/// Provides programmatic access to the stack trace information printed by printStackTrace();
	/// </summary>
	/// <param name="jThrowable">A <see cref="JThrowableObject"/> instance.</param>
	/// <returns>Throwable stack trace.</returns>
	public abstract JArrayObject<JStackTraceElementObject> GetStackTrace(JThrowableObject jThrowable);

	/// <summary>
	/// Retrieves a <see cref="JStringObject"/> containing class name.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>A <see cref="JStringObject"/> instance.</returns>
	public abstract JStringObject GetClassName(JClassObject jClass);
	/// <summary>
	/// Indicates whether current class object is primitive.
	/// </summary>
	/// <param name="jClass">A <see cref="JClassObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jClass"/> is for primitive type;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public abstract Boolean IsPrimitiveClass(JClassObject jClass);
	/// <summary>
	/// Indicates whether current buffer object is direct.
	/// </summary>
	/// <param name="jBuffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns>
	/// <see langword="true"/> if <paramref name="jBuffer"/> is direct buffer;
	/// otherwise, <see langword="false"/>.
	/// </returns>
	public abstract Boolean IsDirectBuffer(JBufferObject jBuffer);
	/// <summary>
	/// Retrieves <paramref name="jBuffer"/> capacity.
	/// </summary>
	/// <param name="jBuffer">A <see cref="JBufferObject"/> instance.</param>
	/// <returns><paramref name="jBuffer"/> capacity.</returns>
	public abstract Int64 BufferCapacity(JBufferObject jBuffer);

	/// <summary>
	/// Returns the name of current member.
	/// </summary>
	/// <typeparam name="TMember">Type of member.</typeparam>
	/// <param name="jMember">A <see cref="JMemberObject"/> instance.</param>
	/// <returns>Returns the name of current instance.</returns>
	public abstract JStringObject GetName<TMember>(TMember jMember)
		where TMember : JLocalObject, IInterfaceObject<JMemberObject>;
	/// <summary>
	/// Returns an array of <c>Class</c> objects that represent the formal parameter types,
	/// in declaration order, of the executable represented by <paramref name="jExecutable"/>.
	/// </summary>
	/// <param name="jExecutable">A <see cref="JExecutableObject"/> instance.</param>
	/// <returns>Returns the name of current instance.</returns>
	public abstract JArrayObject<JClassObject> GetParameterTypes(JExecutableObject jExecutable);
	/// <summary>
	/// Returns a <c>Class</c> object that represents the formal return type of the method represented by
	/// <paramref name="jMethod"/>.
	/// </summary>
	/// <param name="jMethod">A <see cref="JExecutableObject"/> instance.</param>
	/// <returns>Returns the name of current instance.</returns>
	public abstract JClassObject? GetReturnType(JExecutableObject jMethod);
}