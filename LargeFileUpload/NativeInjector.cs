using LargeFileUpload.Business;
using LargeFileUpload.Business.ModelBusiness;

namespace LargeFileUpload
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFileUploadBusiness, FileUploadBusiness>();
           

        }
    }
}
