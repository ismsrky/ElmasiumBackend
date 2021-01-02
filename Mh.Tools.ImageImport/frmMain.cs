using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Mh.Business.Product;
using Mh.Business.Bo.Product;
using Mh.Utils;
using Mh.Business.Bo.Sys;
using Mh.Business.Bo.Image;
using Mh.Business.Image;

namespace Mh.Tools.ImageImport
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtImageDirectory.Text = @"C:\resimler";
        }

        private void btnInspect_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(txtImageDirectory.Text);


            foreach (string item in files)
            {
                addImage(item);
            }
        }

        void addImage(string fullFileName)
        {
            string fileName = "F" + Path.GetFileNameWithoutExtension(fullFileName);

            ProductBusiness productBusiness = new ProductBusiness();
            long? productId = productBusiness.GetProductIdFromCode(fileName);

            if (productId == null)
            {
                absentProductCount++;
                return;
            }
            else
            {
                existProductCount++;
            }

            Guid uniqueId = Guid.NewGuid();
            Image imgOriginal = Bitmap.FromFile(fullFileName);

            string fileTypeStr = Path.GetExtension(fullFileName).Replace(".", "").ToLower();
            Enums.FileTypes fileTypeId = (Enums.FileTypes)Enum.Parse(typeof(Enums.FileTypes), fileTypeStr);
            string fileNameUnique = uniqueId.ToString().ToUpper() + "." + fileTypeId.ToString();

            // We need to resize if image size higher than 1024.
            if (imgOriginal.Height > 640 && imgOriginal.Width > 480)
            {
                // We save the original image in case of any trouble in advance.
                string filePathOriginal = Path.Combine(Stc.ProductImageSourceUrl, "original", fileNameUnique);
                imgOriginal.Save(filePathOriginal);

                Image imgConverted = Img.FixedSize(imgOriginal, 640, 480);
                string filePath = Path.Combine(Stc.ProductImageSourceUrl, fileNameUnique);
                imgConverted.Save(filePath);
                imgConverted.Dispose();
            }
            else
            {
                string filePath = Path.Combine(Stc.ProductImageSourceUrl, fileNameUnique);
                imgOriginal.Save(filePath);
            }

            Image thumbnail = Img.FixedSize(imgOriginal, 96, 96);
            string filePathThumbnail = Path.Combine(Stc.ProductImageSourceUrl, "thumbnail", fileNameUnique);
            thumbnail.Save(filePathThumbnail);
            thumbnail.Dispose();

            Mh.Sessions.Session pseudoSession = new Sessions.Session();
            pseudoSession.RealPerson = new Sessions.SessionRealPerson();
            pseudoSession.RealPerson.Id = 1; // ismail
            pseudoSession.RealPerson.LanguageId = Enums.Languages.xTurkish;
            //ProductUpdateBo updateBo = new ProductUpdateBo()
            //{
            //    ProductId = productId.Value,
            //    ProductUpdateTypeId = Enums.ProductUpdateTypes.xImage,

            //    ImageUniqueId = uniqueId,
            //    ImageFileTypeId = fileTypeId,
               
            //    Session = pseudoSession
            //};

            //ResponseBo responseBo = productBusiness.Update(updateBo);


            ImageBo imageBo = new ImageBo
            {
                ImageTypeId = Enums.ImageTypes.Product,
                UniqueId = uniqueId,
                FileTypeId = fileTypeId,

                ProductId = productId,
                //PersonId = personId,

                Session = pseudoSession
            };

            ImageBusiness imageBusiness = new ImageBusiness();
            ResponseBo responseBo = imageBusiness.Save(imageBo);

            imgOriginal.Dispose();
        }

        int absentProductCount = 0;
        int existProductCount = 0;
    }
}