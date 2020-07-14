using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace kyk_ocr.Core
{
    public class CloudFormRecognizerService : IOCRService
    {
        const string Endpoint = "https://kykrecognizer.cognitiveservices.azure.com/";
        const string Key = "fc8a09a86b5047709aa2d772df80f57b";
        const string ModelId = "c1747f0c-dff1-46f4-83d7-952ac7c3047d";
        FormRecognizerClient _client => GetClient();

        public async Task<IEnumerable<OCRFieldValue>> GetOCRResultsAsync(Stream stream) 
        { 
            var results = await _client.StartRecognizeCustomFormsAsync(ModelId, stream, new RecognizeOptions { IncludeFieldElements = true }).WaitForCompletionAsync();
            return results.Value.SelectMany(x => x.Fields).Select(x => new OCRFieldValue { Name = x.Value.Name, Value = x.Value.ValueData.Text });
        }

        FormRecognizerClient GetClient()
        {
            var credential = new AzureKeyCredential(Key);
            return new FormRecognizerClient(new Uri(Endpoint), credential);
        }
    }
}
