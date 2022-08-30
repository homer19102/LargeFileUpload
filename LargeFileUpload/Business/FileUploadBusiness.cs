using LargeFileUpload.Business.ModelBusiness;

namespace LargeFileUpload.Business
{
    public class FileUploadBusiness : IFileUploadBusiness
    {

        public FileUploadBusiness() { }

        public async Task UploadChunks(string chunkNumber, string fileName, Stream body) 
        {
            string newpath = Path.Combine($"C:\\Teste\\Temp", $"{fileName}{chunkNumber}");

            using (FileStream fs = System.IO.File.Create(newpath))
            {
                byte[] bytes = new byte[1048576 * 10];
               
                int bytesRead = 0;

                while ((bytesRead = await body.ReadAsync(bytes, 0, bytes.Length)) > 0)
                {
                    fs.Write(bytes, 0, bytesRead);
                }
            }
        }

        public async Task UploadComplete(string fileName) 
        {
            string temp = "C:\\Teste" + "\\Temp";

            string newPath = Path.Combine(temp, fileName);

            string[] filePaths = Directory.GetFiles(temp).Where(p => p.Contains(fileName)).OrderBy(p => Int32.Parse(p.Replace(fileName, "$").Split('$')[1])).ToArray();

            foreach (string filePath in filePaths)
            {
                MergeChunks(newPath, filePath);
            }
            System.IO.File.Move(Path.Combine(temp, fileName), Path.Combine("", fileName));
        }

        public void MergeChunks(string finalChunk, string mergeChunck) 
        {
            FileStream? finalFile = null;
            FileStream? mergeFile = null;

            try
            {
                finalFile = File.Open(finalChunk, FileMode.Append);

                mergeFile = File.Open(mergeChunck, FileMode.Open);

                byte[] mergeFileContent = new byte[mergeFile.Length];

                mergeFile.Read(mergeFileContent, 0, (int)mergeFile.Length);

                finalFile.Write(mergeFileContent, 0, (int)mergeFile.Length);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                if (finalFile != null)
                    finalFile.Close();

                if (mergeFile != null)
                    mergeFile.Close();

                System.IO.File.Delete(mergeChunck);
            }

        }
    
    }
}
