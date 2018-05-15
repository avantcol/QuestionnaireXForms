
using System;
using System.Net.Http;
using ModernHttpClient;

namespace QuestionnaireXForms.Services
{
    public static class ServiceUtils
    {
        public static HttpClient GetHttpClient()
        {
            return new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(App.BaseUrl)
            };
        }
    }
}
