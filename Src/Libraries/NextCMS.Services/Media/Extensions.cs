using System.IO;
using System.Web;

namespace NextCMS.Services.Media
{
    /// <summary>
    /// 扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="postedFile">Posted file</param>
        /// <returns>文件字节</returns>
        public static byte[] GetDownloadBits(this HttpPostedFileBase postedFile)
        {
            Stream fs = postedFile.InputStream;
            int size = postedFile.ContentLength;
            byte[] binary = new byte[size];
            fs.Read(binary, 0, size);
            return binary;
        }

        /// <summary>
        /// 获取图片大小
        /// </summary>
        /// <param name="postedFile">Posted file</param>
        /// <returns>图片字节</returns>
        public static byte[] GetPictureBits(this HttpPostedFileBase postedFile)
        {
            Stream fs = postedFile.InputStream;
            int size = postedFile.ContentLength;
            byte[] img = new byte[size];
            fs.Read(img, 0, size);
            return img;
        }
    }
}
