using _Microsoft.Android.Resource.Designer;

using Android;
using Android.Content.PM;
using Android.OS;
using Android.Views;

using Rxmxnx.JNetInterface.Native;

using Activity = Android.App.Activity;

#if !RELEASE_PACKAGE
using Rxmxnx.JNetInterface.Lang;

using Environment = System.Environment;
#endif

namespace Rxmxnx.JNetInterface.ApplicationTest;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity, View.IOnClickListener
{
	private static readonly DateTime load = DateTime.Now;

	private readonly JGlobal? _androidContext;
	private readonly JGlobal? _toastClass;

	private Task _backgroundThread = Task.CompletedTask;
	private Int32 _count;
	private Boolean _disposed;

	public MainActivity()
		=> (this._androidContext, this._toastClass) =
			AndroidJniHost.CreateSyncContext().With(this).Invoke(MobileMethods.InitializeClasses);

	public void OnClick(View? v)
	{
		if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
		{
#pragma warning disable CA1416
			if (this.CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
				this.RequestPermissions([Manifest.Permission.PostNotifications,], 101);
#pragma warning restore CA1416
		}
		if (v is not TextView text) return;
		this._count++;
#if !RELEASE_PACKAGE
		text.Text = $"{Environment.NewLine}Package: {JObject.CompilationFramework}" +
			MobileMethods.GetRuntimeInformation(DateTime.Now, MainActivity.load, this._count);
#else
		text.Text = ExportedMethods.GetRuntimeInformation(DateTime.Now, MainActivity.load, this._count);
#endif
		if (!this._backgroundThread.IsCompleted) return;
		this._backgroundThread = this.ToastBackground();
	}

	protected override void OnCreate(Bundle? savedInstanceState)
	{
		base.OnCreate(savedInstanceState);

		// Set our view from the "main" layout resource
		this.SetContentView(ResourceConstant.Layout.activity_main);

		TextView? text = this.FindViewById<TextView>(ResourceConstant.Id.textView1);
		text?.SetOnClickListener(this);
	}
	protected override void Dispose(Boolean disposing)
	{
		this._backgroundThread.Wait();
		this.ReleaseUnmanagedResources();
		base.Dispose(disposing);
	}

	~MainActivity() { this.ReleaseUnmanagedResources(); }

	private void ReleaseUnmanagedResources()
	{
		if (this._disposed) return;
		this._disposed = true;
		this._toastClass?.Dispose(); // Removes global class instance.
		this._androidContext?.Dispose();
	}
	private async Task ToastBackground()
	{
		Int32 i = 0;
		while (this._count - i > 0)
		{
			String textToToast = $"Random {i}: {Random.Shared.Next()}";
			await AndroidJniHost.CreateAsyncContext().Post(Application.SynchronizationContext,
			                                               (this._androidContext, textToToast),
			                                               MobileMethods.ToastBackground);
			await Task.Delay(1000);
			i++;
		}
	}
}