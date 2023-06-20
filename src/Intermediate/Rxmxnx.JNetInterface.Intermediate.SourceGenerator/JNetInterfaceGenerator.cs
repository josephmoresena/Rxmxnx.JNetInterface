using System;
using System.ComponentModel;
using System.Linq;

using Microsoft.CodeAnalysis;

namespace Rxmxnx.JNetInterface.SourceGenerator
{
	[Generator]
	public class JNetInterfaceGenerator : ISourceGenerator
	{
		public void Execute(GeneratorExecutionContext context)
		{
			switch (context.Compilation.AssemblyName)
			{
				case BaseGenerator.AssemblyName:
                    BaseGenerator.Execute(context);
					break;
				case NativeGenerator.AssemblyName:
					NativeGenerator.Execute(context);
					break;
			}
		}

		public void Initialize(GeneratorInitializationContext context)
		{
			// No initialization required for this one
#if DEBUG
			if (!System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Launch();
#endif
		}

		internal static String GetLiteralValue(ISymbol symbol)
		{
			if (symbol.CanBeReferencedByName)
			{
				AttributeData attribute = symbol.GetAttributes()
					.FirstOrDefault(a => a.AttributeClass.Name == nameof(DefaultValueAttribute));
				TypedConstant? paramsAttr = (attribute?.ConstructorArguments)?.FirstOrDefault();
				return paramsAttr?.Value as String;
			}

			return default;
		}
		internal static String GetOrdinalSuffix(Int32 num)
		{
			String number = num.ToString();
			if (number.EndsWith("11")) return "th";
			if (number.EndsWith("12")) return "th";
			if (number.EndsWith("13")) return "th";
			if (number.EndsWith("1")) return "st";
			if (number.EndsWith("2")) return "nd";
			if (number.EndsWith("3")) return "rd";
			return "th";
		}
	}
}