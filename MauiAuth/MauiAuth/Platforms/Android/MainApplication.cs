using Android.App;
using Android.Runtime;

namespace MauiAuth;

//Todo: ML: Added this as Android doesnt trust the SSL cert on a locally hosted API. 
//Need to look at a better way of doing this.
#if DEBUG
[Application(AllowBackup = false, Debuggable = true, UsesCleartextTraffic = true)]
#else
[Application]
#endif
//end Todo:
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
