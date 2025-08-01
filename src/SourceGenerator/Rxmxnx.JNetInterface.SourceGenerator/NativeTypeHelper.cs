using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator;

/// <summary>
/// Native type helper.
/// </summary>
[ExcludeFromCodeCoverage]
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
	public NativeTypeHelper(ISymbol typeSymbol, IImmutableSet<String> interfaces)
	{
		this._typeSymbol = typeSymbol;
		this._isPointer = interfaces.Contains("Rxmxnx.PInvoke.IFixedPointer");
		this._isPrimitive = interfaces.Contains("Rxmxnx.JNetInterface.Types.IPrimitiveType");
		this._isArrRef = interfaces.Contains("Rxmxnx.JNetInterface.Types.IArrayReferenceType");
		this._isNumeric = this._isPrimitive && interfaces.Contains("Rxmxnx.JNetInterface.Types.IPrimitiveNumericType");
		this._isInteger = this._isNumeric && interfaces.Contains("Rxmxnx.JNetInterface.Types.IPrimitiveIntegerType");
		this._isFloatingPoint = this._isNumeric &&
			interfaces.Contains("Rxmxnx.JNetInterface.Types.IPrimitiveFloatingPointType");
		this._isObjRef = this._isArrRef || interfaces.Contains("Rxmxnx.JNetInterface.Types.IObjectReferenceType");
	}

	/// <summary>
	/// Appends the source code for the current symbol.
	/// </summary>
	/// <param name="context">Generation context.</param>
	public void AddSourceCode(SourceProductionContext context)
	{
		String valueName = this.GetInternalValueName();
		if (!this._isPrimitive)
		{
			this._typeSymbol.GenerateNativeStructToString(context, this._isPointer);
		}
		else
		{
			String underlineType = this.GetUnderlinePrimitiveType();
			this._typeSymbol.GeneratePrimitiveToString(context, valueName);
			this._typeSymbol.GeneratePrimitiveOperators(context, underlineType, valueName, this._isNumeric);
			if (this._isNumeric)
				this._typeSymbol.GeneratePrimitiveNumericOperators(context, underlineType);
			if (this._isInteger)
				this._typeSymbol.GenerateNumericPrimitiveIntegerOperators(context, underlineType);
			if (this._isFloatingPoint)
				this._typeSymbol.GenerateNumericPrimitiveFloatingPointOperators(context, underlineType);
		}
		if (this._isArrRef)
			this._typeSymbol.GenerateArrayRefOperators(context);
		if (this._isObjRef)
			this._typeSymbol.GenerateObjectRefOperators(context);
		if (this._isPointer)
			this._typeSymbol.GenerateNativePointerOperators(context, valueName);
	}

	/// <summary>
	/// Retrieves the name of underline type for the current symbol.
	/// </summary>
	/// <returns>
	/// The name of underline type for the current symbol if it is primitive; otherwise,
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
	/// Retrieves the name of internal value for the current symbol.
	/// </summary>
	/// <returns>
	/// The name of internal value for the current symbol.
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
			"JObjectLocalRef" => "Pointer",
			"JFieldId" => "Pointer",
			"JMethodId" => "Pointer",
			_ => "_value",
		};
}