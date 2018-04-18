

using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.Graphics;

namespace QuestionnaireXForms.Services
{
    public class AttachmentService
    {
        public async Task<string> UploadBitmapAsync(Bitmap bitmap)
        {
            //converting bitmap into byte stream
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
            var bitmapData = stream.ToArray();
            var fileContent = new ByteArrayContent(bitmapData);

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse ("application/octet-stream");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = "questionnaire.jpg"
            };
            
            
            string boundary = "---8d0f01e6b3b5dafaaadaada";
            MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary)
            {
                fileContent
            };
            //multipartContent.Add(fileContent2);
            //multipartContent.Add(fileContent3);

            HttpClient httpClient = new HttpClient ();
            HttpResponseMessage response = await httpClient.PostAsync ( App.BaseUrl +"/questionnaire/attachment/", multipartContent);
            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return null;
            
        }
    }
}