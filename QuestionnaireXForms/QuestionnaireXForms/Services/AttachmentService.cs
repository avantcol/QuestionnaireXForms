

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
        public static async Task<string> UploadBitmapAsync( List<MediaFile> files )
        {
            //converting bitmap into byte stream
            //var stream = new MemoryStream();
            //bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, file.GetStream() );
            //var bitmapData = stream.ToArray();

            string boundary = "---8d0f01e6b3b5dafaaadaada";
            MultipartFormDataContent multipartContent = new MultipartFormDataContent(boundary);

            foreach (var file in files )
            {
                var memoryStream = new MemoryStream();
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();

                var fileContent = new ByteArrayContent( memoryStream.ToArray() );

                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse ("application/octet-stream");
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = "questionnaire.jpg"
                };
            

                System.Console.WriteLine(">>>>>>>>>>>>>>>>>>>> UploadBitmapAsync 2");

                var par = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("questionnaireAnswersID", "1")
                };
            
            
                multipartContent.Add( fileContent );
                
            }

            /*
            if (file == null)
            {
                System.Console.WriteLine( "file is null" );
                return null;
            }
            
            System.Console.WriteLine( file.ToString() );

            System.Console.WriteLine(">>>>>>>>>>>>>>>>>>>> UploadBitmapAsync");
                        */

            
            


            //multipartContent.Add(fileContent2);
            //multipartContent.Add(fileContent3);

            System.Console.WriteLine(">>>>>>>>>>>>>>>>>>>> UploadBitmapAsync 3");

            HttpClient httpClient = new HttpClient ();
            HttpResponseMessage response = await httpClient.PostAsync ( App.BaseUrl +"/questionnaire/attachment?a=1", multipartContent);
            
            System.Console.WriteLine(">>>>>>>>>>>>>>>>>>>> UploadBitmapAsync 4");

            if (response.IsSuccessStatusCode) {
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            return null;
            
        }
    }
}