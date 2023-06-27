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
	/// Name of native type internal value.
	/// </summary>
	private readonly String? _internalPointerName;
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
	/// Indicates whether the native type is a primitive value.
	/// </summary>
	private readonly Boolean _isPrimitive;
	/// <summary>
	/// Native type symbol.
	/// </summary>
	private readonly ISymbol _typeSymbol;
	/// <summary>
	/// Type of primitive type internal value.
	/// </summary>
	private readonly String _underlinePrimitiveType;

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="typeSymbol">Native type symbol.</param>
	/// <param name="interfaces">Native type symbol interfaces.</param>
	private NativeTypeHelper(ISymbol typeSymbol, IImmutableSet<String> interfaces)
	{
		Boolean isPointer = interfaces.Contains("Rxmxnx.PInvoke.IFixedPointer");

		this._typeSymbol = typeSymbol;
		this._isPrimitive = interfaces.Contains("Rxmxnx.JNetInterface.IPrimitive");
		this._isArrRef = interfaces.Contains("Rxmxnx.JNetInterface.Internal.IArrayReference");
		this._isNumeric = this._isPrimitive && interfaces.Contains("Rxmxnx.JNetInterface.IPrimitiveNumeric");
		this._isInteger = this._isNumeric && interfaces.Contains("Rxmxnx.JNetInterface.IPrimitiveInteger");
		this._isFloatingPoint = this._isNumeric && interfaces.Contains("Rxmxnx.JNetInterface.IFloatingPoint");
		this._isObjRef = this._isArrRef || interfaces.Contains("Rxmxnx.JNetInterface.Internal.IObjectReference");
		this._internalPointerName = isPointer ? typeSymbol.Name.EndsWith("Value") ? "_functions" : "_value" : default;
		this._underlinePrimitiveType = NativeTypeHelper.GetUnderlinePrimitiveType(typeSymbol);
	}

	/// <summary>
	/// Appends the source code for current symbol.
	/// </summary>
	/// <param name="context">Generation context.</param>
	public void AddSourceCode(GeneratorExecutionContext context)
	{
		if (this._isPrimitive)
		{
			this._typeSymbol.GeneratePrimitiveToString(context);
			this._typeSymbol.GeneratePrimitiveOperators(context, this._underlinePrimitiveType!);
			if (this._isNumeric)
				this._typeSymbol.GenerateNumericPrimitiveOperators(context, this._underlinePrimitiveType!);
		}
		else
		{
			this._typeSymbol.GenerateNativeStructToString(context);
		}
		if (this._isArrRef)
			this._typeSymbol.GenerateArrayRefOperators(context);
		if (this._isObjRef)
			this._typeSymbol.GenerateObjectRefOperators(context);
		if (this._internalPointerName is not null)
			this._typeSymbol.GenerateNativePointerOperators(context, this._internalPointerName);
	}

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

	/// <summary>
	/// Retrieves the name of underline type for <paramref name="typeSymbol"/>.
	/// </summary>
	/// <param name="typeSymbol">Type symbol.</param>
	/// <returns>
	/// The name of underline type for <paramref name="typeSymbol"/> if it is primitive; otherwise,
	/// <see langword="null"/>.
	/// </returns>
	private static String GetUnderlinePrimitiveType(ISymbol typeSymbol)
		=> typeSymbol.Name switch
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
}