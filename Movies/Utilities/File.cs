namespace Movies.Utilities
{
    public class File
    {
        public static string GetFileName(string path)
        {
            var lastSlash = path.LastIndexOf('/');
            var lastDot = path.LastIndexOf('.');
            var fileName = path.Substring(lastSlash + 1, lastDot - lastSlash - 1);

            return fileName;
        }

        public static string GetFileExtension(string path)
        {
            var lastDot = path.LastIndexOf('.');
            var fileExtension = path.Substring(lastDot + 1);

            return fileExtension;
        }

        public static string ReadFile(string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var streamReader = new StreamReader(fileStream);
                var fileContent = streamReader.ReadToEnd();
                return fileContent;
            }
        }
    }
}
