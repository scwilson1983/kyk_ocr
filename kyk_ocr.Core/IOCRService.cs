using Azure;
using Azure.AI.FormRecognizer.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace kyk_ocr.Core
{
    public interface IOCRService
    {
        Task<IEnumerable<OCRFieldValue>> GetOCRResultsAsync(Stream stream);
    }

    public class OCRFieldValue
    {
        public string Name { get; set; }
        public float Confidence { get; set; }
        public string Value { get; set; }
    }
}
