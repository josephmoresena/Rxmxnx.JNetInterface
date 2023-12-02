using System.Runtime.CompilerServices;

namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IReferenceProvider
	{
		public JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive) where TPrimitive : unmanaged, IPrimitiveType<TPrimitive>
		{
			JPrimitiveTypeMetadata metadata = IPrimitiveType.GetMetadata<TPrimitive>();
			IEnvironment env = this.VirtualMachine.GetEnvironment(this.Reference);
			return metadata.Signature[0] switch
			{
				0x90 => //Z
					JBooleanObject.Create(env, Unsafe.As<TPrimitive, JBoolean>(ref primitive)),
				0x66 => //B
					JByteObject.Create(env, Unsafe.As<TPrimitive, JByte>(ref primitive)),
				0x67 => //C
					JCharacterObject.Create(env, Unsafe.As<TPrimitive, JChar>(ref primitive)),
				0x68 => //D
					JDoubleObject.Create(env, Unsafe.As<TPrimitive, JDouble>(ref primitive)),
				0x70 => //F
					JFloatObject.Create(env, Unsafe.As<TPrimitive, JFloat>(ref primitive)),
				0x73 => //I
					JIntegerObject.Create(env, Unsafe.As<TPrimitive, JInt>(ref primitive)),
				0x74 => //J
					JLongObject.Create(env, Unsafe.As<TPrimitive, JLong>(ref primitive)),
				0x83 => //S
					JShortObject.Create(env, Unsafe.As<TPrimitive, JShort>(ref primitive)),
				_ => throw new InvalidOperationException("Object is not primitive."),
			};
		}
		public Boolean ReloadObject(JLocalObject jLocal) => throw new NotImplementedException();
		public TGlobal Create<TGlobal>(JLocalObject jLocal) where TGlobal : JGlobalBase
			=> throw new NotImplementedException();
		public Boolean Unload(JLocalObject jLocal) => throw new NotImplementedException();
		public Boolean Unload(JGlobalBase jGlobal) => throw new NotImplementedException();

		/// <summary>
		/// Applies cast from <see cref="JLocalObject"/> to <typeparamref name="TObject"/>.
		/// </summary>
		/// <typeparam name="TObject">A <see cref="IDataType{TObject}"/> type.</typeparam>
		/// <param name="jLocal">A <see cref="JLocalObject"/> instance.</param>
		/// <returns>A <typeparamref name="TObject"/> instance.</returns>
		private static TObject? Cast<TObject>(JLocalObject? jLocal) where TObject : IDataType<TObject>
		{
			if (jLocal is null || jLocal.IsDefault) return default;
			if (typeof(TObject) == typeof(JLocalObject))
				return (TObject)(Object)jLocal;
			JReferenceTypeMetadata metadata = (JReferenceTypeMetadata)IDataType.GetMetadata<TObject>();
			TObject result = (TObject)(Object)metadata.ParseInstance(jLocal);
			jLocal.Dispose();
			return result;
		}
	}
}