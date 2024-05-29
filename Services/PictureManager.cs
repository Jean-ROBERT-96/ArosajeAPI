namespace Services
{
    public static class PictureManager
    {
        public static string Filepath = string.Empty;

        public static string GetPicture(string file)
        {
            try
            {
                if (!Path.Exists(Filepath))
                    Directory.CreateDirectory(Filepath);

                var imgArray = File.ReadAllBytes(file);
                return Convert.ToBase64String(imgArray);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string SavePicture(string img64, string name)
        {
            try
            {
                if (!Path.Exists(Filepath))
                    Directory.CreateDirectory(Filepath);

                string fullname = Path.Combine(Filepath, $"{name}.jpg");
                File.WriteAllBytes(fullname, Convert.FromBase64String(img64));
                return fullname;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool EditPicture(string newImg, string oldImgPath)
        {
            try
            {
                if (!Path.Exists(Filepath))
                    Directory.CreateDirectory(Filepath);
                File.Delete(oldImgPath);

                string fullname = $"{Path.GetFileName(oldImgPath)}.jpg";
                File.WriteAllBytes(Path.Combine(Filepath, fullname), Convert.FromBase64String(newImg));

                return true;
            }
            catch

            {
                return false;
            }
        }

        public static bool DeletePicture(string imgPath)
        {
            try
            {
                if (!Path.Exists(Filepath))
                    Directory.CreateDirectory(Filepath);
                File.Delete(imgPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
