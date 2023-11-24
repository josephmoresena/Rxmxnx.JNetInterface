namespace Rxmxnx.JNetInterface;

public partial class JEnvironment
{
	private partial record JEnvironmentCache : IReferenceProvider
	{
		public JLocalObject CreateWrapper<TPrimitive>(TPrimitive primitive) => throw new NotImplementedException();
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
		public TObject? Cast<TObject>(JLocalObject? jLocal) where TObject : IDataType<TObject>
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