using Mh.Business.Bo.Image;
using Mh.Business.Bo.Sys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TinifyAPI;

namespace Mh.Tools.ImageCompress
{
    class Program
    {
        public static List<ImageProcess> imageProcessList { get; set; }
        public static void Main(string[] args)
        {
            imageProcessList = null;

            Stc.ReadConfigs(); //Reads app.config file
            Business.Stc.ConnStr = Stc.ConnStr;

            Tinify.Key = Stc.TinifyKey;

            System.Timers.Timer tm = new System.Timers.Timer();
            tm.Interval = 1000;
            tm.Elapsed += Tm_Elapsed;
            tm.Start();

            //dene();
            Console.ReadLine();
        }

        static void dene()
        {
            var source = Tinify.FromFile(@"C:\Users\ismail-PC\Desktop\barcodes\images\8680530125330.jpg");
            TaskAwaiter taskAwaiter = source.ToFile(@"C:\Users\ismail-PC\Desktop\barcodes\images\compressed\8680530125330.jpg").GetAwaiter();
            taskAwaiter.OnCompleted(() =>
            Console.WriteLine("bitti")
            );
        }

        static void process(ImageProcess imageProcess)
        {
            string urlPath = null;
            if (imageProcess.ImageTypeId == Enums.ImageTypes.Product)
            {
                urlPath = Path.Combine(Stc.ImageSourceUrl, "product");
            }
            else if (imageProcess.ImageTypeId == Enums.ImageTypes.Profile)
            {
                urlPath = Path.Combine(Stc.ImageSourceUrl, "profile");
            }

            string fileName = Path.Combine(urlPath,
               imageProcess.StoreTypeId == StoreType.Normal ? "" : "thumbnail", imageProcess.UniqueId.ToString().ToUpper() + "." + imageProcess.FileTypeId.ToString());

            var source = Tinify.FromFile(fileName);
            TaskAwaiter taskAwaiter = source.ToFile(fileName).GetAwaiter();
            taskAwaiter.OnCompleted(() =>
            done(imageProcess)
            );
        }

        static void done(ImageProcess imageProcess)
        {
            imageProcess.Done = true;

            Business.Image.ImageBusiness imageBusiness = new Business.Image.ImageBusiness();
            imageBusiness.MarkAsCompressed(imageProcess.ProductImageId, imageProcess.StoreTypeId == StoreType.Normal ? false : true);
        }

        private static void Tm_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //((System.Timers.Timer)sender).Stop();
            if (imageProcessList != null && imageProcessList.Count(x => x.Done == false) > 0) return;

            try
            {
                if (DateTime.Now.Hour == Stc.StartHour && DateTime.Now.Minute == 0 && DateTime.Now.Second < 10)
                {
                    Business.Image.ImageBusiness imageBusiness = new Business.Image.ImageBusiness();
                    ResponseBo<List<ImageListCompressBo>> responseListBo = imageBusiness.GetListCompress();

                    if (responseListBo.IsSuccess && responseListBo.Bo != null && responseListBo.Bo.Count > 0)
                    {
                        imageProcessList = new List<ImageProcess>();
                        foreach (var item in responseListBo.Bo)
                        {
                            if (!item.IsCompressed)
                                imageProcessList.Add(getFromBo(item, StoreType.Normal));

                            if (!item.IsCompressedThumbnail)
                                imageProcessList.Add(getFromBo(item, StoreType.Thumbnail));
                        }

                        foreach (var item in imageProcessList)
                        {
                            process(item);
                        }
                    }
                    else
                    {
                        imageProcessList = null;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static ImageProcess getFromBo(ImageListCompressBo bo, StoreType storeTypeId)
        {
            ImageProcess imageProcess = new ImageProcess();
            imageProcess.ImageTypeId = bo.ImageTypeId;
            imageProcess.FileTypeId = bo.FileTypeId;
            imageProcess.ProductImageId = bo.Id;
            imageProcess.UniqueId = bo.UniqueId;
            imageProcess.StoreTypeId = storeTypeId;
            imageProcess.Done = false;

            return imageProcess;
        }
    }
}