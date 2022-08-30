namespace LargeFileUpload.Business.ModelBusiness
{
    public interface IFileUploadBusiness
    {
        Task UploadChunks(string id, string fileName, Stream body);

        Task UploadComplete(string fileName);
    }
}
