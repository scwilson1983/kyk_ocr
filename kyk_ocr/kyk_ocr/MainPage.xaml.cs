using kyk_ocr.Core;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace kyk_ocr
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IOCRService _ocrService;
        List<string> _fields;
        public MainPage(IOCRService ocrService)
        {
            InitializeComponent();
            _ocrService = ocrService;
            _fields = (new List<string> { "Manufacturer", "Model", "Serial Number" }).OrderBy(x => x).ToList();
            foreach (var fieldName in _fields)
            {
                var entry = new Entry { Placeholder = fieldName };
                FieldLayout.Children.Add(entry);
            }
        }

        async void OpenOCRScanner(object sender, EventArgs e)
        {
            await TakePhoto();
        }

        async Task TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            FieldLayout.IsVisible = false;
            ScanBtn.IsVisible = false;
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Images",
                Name = $"{Guid.NewGuid()}.jpg",
                CompressionQuality = 100,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Large
            });

            LoaderLayout.IsVisible = true;
            if (file == null)
            {
                StopLoading();
                return;
            }
            var results = (await _ocrService.GetOCRResultsAsync(file.GetStream())).ToList();
            for(var i = 0; i < _fields.Count; i++)
            {
                var fieldValue = results.FirstOrDefault(x => x.Name == _fields[i])?.Value;
                if(!string.IsNullOrEmpty(fieldValue))
                {
                    var fieldEntry = FieldLayout.Children[i] as Entry;
                    fieldEntry.Text = fieldValue;
                }
            }
            StopLoading();
        }

        void StopLoading()
        {
            LoaderLayout.IsVisible = false;
            FieldLayout.IsVisible = true;
            ScanBtn.IsVisible = true;
        }
    }
}
