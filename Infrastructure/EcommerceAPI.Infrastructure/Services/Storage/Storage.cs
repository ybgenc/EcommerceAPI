namespace EcommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected  async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileFunc)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);

            string regulatedName = $"{NameOperation.NameRegulation(oldName)}{extension}";
            int i = 2;

            while (hasFileFunc(pathOrContainerName,regulatedName))
            {
                regulatedName = $"{NameOperation.NameRegulation(oldName)}-{i}{extension}";
                i++;
            }

            return await Task.FromResult(regulatedName);


        }
    }
}
