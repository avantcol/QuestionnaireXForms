

using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace QuestionnaireXForms.Services
{
    public static class AttachmentService
    {
        public enum AttachmentType
        {
            QuestionnaireAnswers = 1,
            Signature = 2

        }

        public static async Task<string> UploadBitmapAsync(List<MediaFile> files, long questionnaireAnswersID)
        {
            //converting bitmap into byte stream
            //var stream = new MemoryStream();
            //bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, file.GetStream() );
            //var bitmapData = stream.ToArray();

            string boundary = "---8d0f01e6b3b5dafaaadaada";

            var requestParameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("iImageType", "4"),
                new KeyValuePair<string, string>("iObjectId", "16242714"),
                new KeyValuePair<string, string>("id", "44012208")
            };
            var multipartFormDataContent = new MultipartFormDataContent(boundary);
            multipartFormDataContent.Add(new FormUrlEncodedContent(requestParameters));

            //MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);

            foreach (var file in files)
            {
                var memoryStream = new MemoryStream();
                file.GetStream().CopyTo(memoryStream);
                //file.Dispose();

                var fileContent = new ByteArrayContent(memoryStream.ToArray());

                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = Path.GetFileName(file.Path)
                };

                multipartFormDataContent.Add(fileContent);
            }

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient
                .PostAsync(App.BaseUrl + "/questionnaire/attachment?type="+AttachmentType.QuestionnaireAnswers+"&quUserSession" + App.User.quUserSession
                           + "&questionnaireAnswersID=" + questionnaireAnswersID, multipartFormDataContent);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return null;

        }

        public static async Task<string> UploadSignatureAsync(long questionnaireAnswersID)
        {
            string boundary = "---8d0f01e6b3b5dafaaadaada";

            var multipartFormDataContent = new MultipartFormDataContent(boundary);


            var memoryStream = new MemoryStream();
            DataSource.SignatureStream.CopyTo(memoryStream);
            //file.Dispose();

            var fileContent = new ByteArrayContent(memoryStream.ToArray());

            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "file",
                FileName = Path.GetFileName("Signature.png")
            };

            multipartFormDataContent.Add(fileContent);

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient
                .PostAsync(App.BaseUrl + "/questionnaire/attachment?type="+AttachmentType.Signature+"&quUserSession" + App.User.quUserSession
                           + "&questionnaireAnswersID=" + questionnaireAnswersID, multipartFormDataContent);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return null;

        }
   }
}