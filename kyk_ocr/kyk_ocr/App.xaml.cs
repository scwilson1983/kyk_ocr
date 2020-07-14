using kyk_ocr.Core;
using Xamarin.Forms;

namespace kyk_ocr
{
    public partial class App : Application
    {
        public App()
        {
            DependencyService.Register<IOCRService, CloudFormRecognizerService>();
            InitializeComponent();
            var ocrService = DependencyService.Resolve<IOCRService>();
            MainPage = new MainPage(ocrService);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
