using Mh.Business.Bo.Image;
using Mh.Business.Bo.Product;
using Mh.Business.Bo.Sys;
using Mh.Business.Contract.Image;
using Mh.Business.Contract.Product;
using Mh.Service.Dto.Sys;
using Mh.Sessions;
using Mh.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Mh.Service.WebApi.Image
{
    public class ImageController : BaseController
    {
        readonly IImageBusiness imageBusiness;
        readonly IProductBusiness productBusiness;
        public ImageController(
            IImageBusiness _imageBusiness,
            IProductBusiness _productBusiness)
        {
            imageBusiness = _imageBusiness;
            productBusiness = _productBusiness;
        }

        [HttpPost]
        public ResponseDto Upload()
        {
            ResponseDto responseDto = new ResponseDto();

            try
            {
                var httpRequest = HttpContext.Current.Request;
 
                long? productId = httpRequest.Form["productId"].ToInt64Null();
                long? personId = httpRequest.Form["personId"].ToInt64Null();
                long? personProductId = httpRequest.Form["personProductId"].ToInt64Null();

                if (httpRequest.Files.Count > 0)
                {
                    HttpPostedFile postedFile = httpRequest.Files[0];
                    if (postedFile.ContentLength >= 10000000)
                    {
                        responseDto.IsSuccess = false;
                        responseDto.Message = "Resim 10 mb'dan küçük olmalıdır.";
                        return responseDto;
                    }

                    Enums.ImageTypes imageTypeId = (Enums.ImageTypes)httpRequest.Form["imageTypeId"].ToInt32();

                    if (
                        (imageTypeId == Enums.ImageTypes.Product && productId == null)
                        || (imageTypeId == Enums.ImageTypes.Profile && personId == null)
                        || (imageTypeId == Enums.ImageTypes.PersonProduct && personProductId == null)
                        )
                    {
                        return new ResponseDto()
                        {
                            IsSuccess = false,
                            Message = Business.Stc.GetDicValue("xInvalidData", Enums.Languages.xEnglish)
                        };
                    }

                    string urlPath = null;
                    if (imageTypeId == Enums.ImageTypes.Product || imageTypeId == Enums.ImageTypes.PersonProduct)
                    {
                        urlPath = Path.Combine(Stc.ImageSourceUrl, "product");
                    }
                    else if (imageTypeId == Enums.ImageTypes.Profile)
                    {
                        urlPath = Path.Combine(Stc.ImageSourceUrl, "profile");
                    }

                    Guid uniqueId = Guid.NewGuid();
                    System.Drawing.Image imgOriginal = Bitmap.FromStream(postedFile.InputStream);

                    string fileTypeStr = postedFile.ContentType.Split('/')[1];//.Replace(".", "").ToLower();
                    Enums.FileTypes fileTypeId = (Enums.FileTypes)Enum.Parse(typeof(Enums.FileTypes), fileTypeStr);
                    string fileName = uniqueId.ToString().ToUpper() + "." + fileTypeId.ToString();

                    // We need to resize if image size higher than 1024.
                    if (imgOriginal.Height > 640 && imgOriginal.Width > 480)
                    {
                        // We save the original image in case of any trouble in advance.
                        string filePathOriginal = Path.Combine(urlPath, "original", fileName);
                        postedFile.SaveAs(filePathOriginal);

                        System.Drawing.Image imgConverted = Img.FixedSize(imgOriginal, 640, 480);
                        string filePath = Path.Combine(urlPath, fileName);
                        imgConverted.Save(filePath);
                        imgConverted.Dispose();
                    }
                    else
                    {
                        string filePath = Path.Combine(urlPath, fileName);
                        postedFile.SaveAs(filePath);
                    }

                    System.Drawing.Image thumbnail = Img.FixedSize(imgOriginal, 96, 96);
                    string filePathThumbnail = Path.Combine(urlPath, "thumbnail", fileName);
                    thumbnail.Save(filePathThumbnail);
                    thumbnail.Dispose();

                    ImageBo imageBo = new ImageBo
                    {
                        ImageTypeId = imageTypeId,
                        UniqueId = uniqueId,
                        FileTypeId = fileTypeId,

                        ProductId = productId,
                        PersonId = personId,
                        PersonProductId = personProductId,

                        Session = Session
                    };

                    ResponseBo responseBo = imageBusiness.Save(imageBo);
                    responseDto = responseBo.ToResponseDto();

                    //if (responseBo.IsSuccess)
                    //{
                    //    if (imageTypeId == Enums.ImageTypes.Product)
                    //    {
                    //        ProductUpdateBo updateBo = new ProductUpdateBo()
                    //        {
                    //            ProductId = productId.Value,
                    //            ProductUpdateTypeId = Enums.ProductUpdateTypes.xImage,

                    //            ImageUniqueId = uniqueId,
                    //            ImageFileTypeId = fileTypeId,

                    //            Session = Session
                    //        };

                    //        responseDto = productBusiness.Update(updateBo).ToResponseDto();
                    //    }
                    //    else
                    //    {
                    //        responseDto = responseBo.ToResponseDto();
                    //    }
                    //}
                    //else
                    //{
                    //}

                    imgOriginal.Dispose();

                    if (responseDto.IsSuccess)
                    {
                        responseDto.Message = base.GetImageName(uniqueId, fileTypeId);
                    }
                }
                else
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = Business.Stc.GetDicValue("xInvalidData", Session.RealPerson.LanguageId);
                }
            }
            catch (Exception ex)
            {
                responseDto = base.SaveExLog(ex, this.GetType(), MethodBase.GetCurrentMethod().Name);
            }

            return responseDto;
        }
    }
}