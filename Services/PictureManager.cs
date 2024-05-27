namespace Services
{
    public static class PictureManager
    {
        private static readonly string _filepath = Path.Combine(Environment.CurrentDirectory, "Pictures");

        public static string GetPicture(string file)
        {
            try
            {
                if (!Path.Exists(_filepath))
                    Directory.CreateDirectory(_filepath);

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
                if (!Path.Exists(_filepath))
                    Directory.CreateDirectory(_filepath);

                string fullname = Path.Combine(_filepath, $"{name}.jpg");
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
                if (!Path.Exists(_filepath))
                    Directory.CreateDirectory(_filepath);
                File.Delete(oldImgPath);

                string fullname = $"{Path.GetFileName(oldImgPath)}.jpg";
                File.WriteAllBytes(Path.Combine(_filepath, fullname), Convert.FromBase64String(newImg));

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
                if (!Path.Exists(_filepath))
                    Directory.CreateDirectory(_filepath);
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
