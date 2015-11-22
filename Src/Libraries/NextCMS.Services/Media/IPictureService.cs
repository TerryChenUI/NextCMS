using System.Collections.Generic;
using NextCMS.Core;
using NextCMS.Core.Domain.Media;

namespace NextCMS.Services.Media
{
    /// <summary>
    /// 图片
    /// </summary>
    public partial interface IPictureService
    {
        #region 获取图片当地路径和Url方法

        /// <summary>
        /// 获取默认图片路径
        /// </summary>
        /// <param name="targetSize">图片大小</param>
        /// <param name="defaultPictureType">默认图片类型</param>
        /// <returns>图片路径</returns>
        string GetDefaultPictureUrl(int targetSize = 0,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="pictureId">图片主键Id</param>
        /// <param name="targetSize">图片大小</param>
        /// <param name="showDefaultPicture">是否显示默认图像</param>
        /// <param name="defaultPictureType">默认图片类型</param>
        /// <returns>图片路径</returns>
        string GetPictureUrl(int pictureId,
            int targetSize = 0,
            bool showDefaultPicture = true,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="picture">图片对象</param>
        /// <param name="targetSize">图片大小</param>
        /// <param name="showDefaultPicture">是否显示默认图像</param>
        /// <param name="defaultPictureType">默认图片类型</param>
        /// <returns>图片路径</returns>
        string GetPictureUrl(Picture picture,
            int targetSize = 0,
            bool showDefaultPicture = true,
            PictureType defaultPictureType = PictureType.Entity);

        /// <summary>
        /// 获取缩略图路径
        /// </summary>
        /// <param name="picture">图片对象</param>
        /// <param name="targetSize">图片大小</param>
        /// <param name="showDefaultPicture">是否显示默认图像</param>
        /// <returns>缩略图路径</returns>
        string GetThumbLocalPath(Picture picture, int targetSize = 0, bool showDefaultPicture = true);

        #endregion

        #region 图片基本操作

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="pictureId">图片主键Id</param>
        /// <returns>图片对象</returns>
        Picture GetPictureById(int pictureId);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="picture">Picture</param>
        void DeletePicture(Picture picture);

        /// <summary>
        /// 获取图片集合
        /// </summary>
        /// <param name="pageIndex">当前页数</param>
        /// <param name="pageSize">每页显示个数</param>
        /// <returns>图片集合</returns>
        IEnumerable<Picture> GetPictures(int pageIndex, int pageSize);

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="pictureBinary">字节大小</param>
        /// <param name="mimeType">图片后缀</param>
        /// <param name="seoFilename">Seo 文件名</param>
        /// <param name="isNew">是否新图片</param>
        /// <param name="validateBinary">该值表示是否要提供验证图片的二进制</param>
        /// <returns>图片对象</returns>
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, bool isNew, bool validateBinary = true);

        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="pictureId">图片主键Id</param>
        /// <param name="pictureBinary">字节大小</param>
        /// <param name="mimeType">图片后缀</param>
        /// <param name="seoFilename">Seo 文件名</param>
        /// <param name="isNew">是否新图片</param>
        /// <param name="validateBinary">该值表示是否要提供验证图片的二进制</param>
        /// <returns>图片对象</returns>
        Picture UpdatePicture(int pictureId, byte[] pictureBinary, string mimeType, string seoFilename, bool isNew, bool validateBinary = true);

        /// <summary>
        /// 验证图片二进制
        /// </summary>
        /// <param name="pictureBinary">图片字节大小</param>
        /// <param name="mimeType">图片后缀类型</param>
        /// <returns>图片字节大小或抛出异常</returns>
        byte[] ValidatePicture(byte[] pictureBinary, string mimeType);

        #endregion
    }
}
