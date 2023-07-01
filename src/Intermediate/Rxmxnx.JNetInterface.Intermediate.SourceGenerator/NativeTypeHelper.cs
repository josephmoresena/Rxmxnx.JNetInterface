using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

/// <summary>
/// Native type helper.
/// </summary>
internal sealed record NativeTypeHelper
{
	/// <summary>
	/// Indicates whether the native type is an array reference.
	/// </summary>
	private readonly Boolean _isArrRef;
	/// <summary>
	/// Indicates whether the native type is a floating point value.
	/// </summary>
	private readonly Boolean _isFloatingPoint;
	/// <summary>
	/// Indicates whether the native type is an integer value.
	/// </summary>
	private readonly Boolean _isInteger;
	/// <summary>
	/// Indicates whether the native type is a numeric value.
	/// </summary>
	private readonly Boolean _isNumeric;
	/// <summary>
	/// Indicates whether the native type is an object reference.
	/// </summary>
	private readonly Boolean _isObjRef;
	/// <summary>
	/// Indicates whether the native type is a pointer.
	/// </summary>
	private readonly Boolean _isPointer;
	/// <summary>
	/// Indicates whether the native type is a primitive value.
	/// </summary>
	private readonly Boolean _isPrimitive;
	/// <summary>
	/// Native type symbol.
	/// </summary>
	private readonly ISymbol _typeSymbol;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="typeSymbol">Native type symbol.</param>
	/// <param name="interfaces">Native type symbol interfaces.</param>
	private NativeTypeHelper(ISymbol typeSymbol, IImmutableSet<String> interfaces)
	{
		this._typeSymbol = typeSymbol;
		this._isPointer = interfaces.Contains("Rxmxnx.PInvoke.IFixedPointer");
		this._isPrimitive = interfaces.Contains("Rxmxnx.JNetInterface.IPrimitive");
		this._isArrRef = interfaces.Contains("Rxmxnx.JNetInterface.Internal.IArrayReference");
		this._isNumeric = this._isPrimitive && interfaces.Contains("Rxmxnx.JNetInterface.Internal.IPrimitiveNumeric");
		this._isInteger = this._isNumeric && interfaces.Contains("Rxmxnx.JNetInterface.Internal.IPrimitiveInteger");
		this._isFloatingPoint =
			this._isNumeric && interfaces.Contains("Rxmxnx.JNetInterface.Internal.IPrimitiveFloatingPoint");
		this._isObjRef = this._isArrRef || interfaces.Contains("Rxmxnx.JNetInterface.Internal.IObjectReference");
	}

	/// <summary>
	/// Appends the source code for current symbol.
	/// </summary>
	/// <param name="context">Generation context.</param>
	public void AddSourceCode(GeneratorExecutionContext context)
	{
		String valueName = this.GetInternalValueName();
		if (this._isPrimitive)
		{
			String underlineType = this.GetUnderlinePrimitiveType();
			this._typeSymbol.GeneratePrimitiveToString(context, valueName);
			this._typeSymbol.GeneratePrimitiveOperators(context, underlineType, valueName);
			if (this._isNumeric)
				this._typeSymbol.GeneratePrimitiveNumericOperators(context, underlineType);
			if (this._isInteger)
				this._typeSymbol.GenerateNumericPrimitiveIntegerOperators(context, underlineType);
			if (this._isFloatingPoint)
				this._typeSymbol.GenerateNumericPrimitiveFloatingPointOperators(context, underlineType);
		}
		else
		{
			this._typeSymbol.GenerateNativeStructToString(context);
		}
		if (this._isArrRef)
			this._typeSymbol.GenerateArrayRefOperators(context);
		if (this._isObjRef)
			this._typeSymbol.GenerateObjectRefOperators(context);
		if (this._isPointer)
			this._typeSymbol.GenerateNativePointerOperators(context, valueName);
	}

	/// <summary>
	/// Retrieves the name of underline type for current symbol.
	/// </summary>
	/// <returns>
	/// The name of underline type for current symbol if it is primitive; otherwise,
	/// <see cref="String.Empty"/>.
	/// </returns>
	private String GetUnderlinePrimitiveType()
		=> this._typeSymbol.Name switch
		{
			"JBoolean" => "Boolean",
			"JByte" => "SByte",
			"JChar" => "Char",
			"JDouble" => "Double",
			"JFloat" => "Single",
			"JInt" => "Int32",
			"JLong" => "Int64",
			"JShort" => "Int16",
			_ => String.Empty,
		};

	/// <summary>
	/// Retrieves the name of internal value for current symbol.
	/// </summary>
	/// <returns>
	/// The name of internal value for current symbol.
	/// </returns>
	private String GetInternalValueName()
		=> this._typeSymbol.Name switch
		{
			"JBoolean" => "Value",
			"JEnvironmentValue" => "_functions",
			"JVirtualMachineValue" => "_functions",
			"JValue" => String.Empty,
			"JInvokeInterface" => String.Empty,
			"JNativeInterface" => String.Empty,
			_ => "_value",
		};

	/// <summary>
	/// Creates a new helper if <paramref name="typeSymbol"/> is a native type.
	/// </summary>
	/// <param name="typeSymbol">Type symbol.</param>
	/// <param name="interfaces">Type symbol interfaces.</param>
	/// <returns>Created helper instance.</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static NativeTypeHelper? Create(INamedTypeSymbol typeSymbol, IImmutableSet<String> interfaces)
	{
		if (typeSymbol.TypeKind == TypeKind.Struct && interfaces.Contains("Rxmxnx.JNetInterface.INative"))
			return new(typeSymbol, interfaces);
		return default;
	}
}