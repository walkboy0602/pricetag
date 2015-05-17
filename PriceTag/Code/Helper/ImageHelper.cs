using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PriceTag.Code.Helper
{
    public class ImageHelper
    {
        public static byte[] CreateImage(byte[] PassedImage, string filename, int Width = 640, int Height = 400, int quality = 85)
        {
            byte[] ReturnedThumbnail;

            using (MemoryStream StartMemoryStream = new MemoryStream(),
                                NewMemoryStream = new MemoryStream())
            {
                // write the string to the stream  
                StartMemoryStream.Write(PassedImage, 0, PassedImage.Length);

                // create the start Bitmap from the MemoryStream that contains the image  
                Bitmap startBitmap = new Bitmap(StartMemoryStream);

                // set thumbnail height and width proportional to the original image.  
                int newHeight;
                int newWidth;
                double HW_ratio;
                HW_ratio = 1.8823;

                if (startBitmap.Height > Height || (startBitmap.Width < Width && startBitmap.Height < Height))
                {
                    // newHeight = LargestSide;
                    // HW_ratio = (double)((double)LargestSide / (double)startBitmap.Height);
                    // newWidth = (int)(HW_ratio * (double)startBitmap.Width);
                    newHeight = Height;
                    HW_ratio = (double)((double)Height / (double)startBitmap.Height);
                    newWidth = (int)(HW_ratio * (double)startBitmap.Width);
                }
                else
                {
                    //  newWidth = LargestSide;
                    // HW_ratio = (double)((double)LargestSide / (double)startBitmap.Width);
                    //  newHeight = (int)(HW_ratio * (double)startBitmap.Height);
                    newWidth = Width;
                    HW_ratio = (double)((double)Width / (double)startBitmap.Width);
                    newHeight = (int)(HW_ratio * (double)startBitmap.Height);
                }

                ImageFormat imageExtension = GetImageFormat(filename);
                // create a new Bitmap with dimensions for the thumbnail.  
                Bitmap newBitmap = new Bitmap(startBitmap.Width, startBitmap.Height);

                // Copy the image from the START Bitmap into the NEW Bitmap.  
                // This will create a thumnail size of the same image.i
                newBitmap = ResizeImage(startBitmap, newWidth, newHeight);

                //Reduce quality of image to reduce file upload/download size
                newBitmap.SetResolution(quality, quality); // OR 70,70

                // Save this image to the specified stream in the specified format.  
                //newBitmap.Save(NewMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                newBitmap.Save(NewMemoryStream, imageExtension);

                // Fill the byte[] for the thumbnail from the new MemoryStream.  
                ReturnedThumbnail = NewMemoryStream.ToArray();
            }

            // return the resized image as a string of bytes.  
            return ReturnedThumbnail;
        }

        // Resize a Bitmap  
        private static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            return resizedImage;
        }

        private static ImageFormat GetImageFormat(string fileName)
        {
            string extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentException(
                    string.Format("Unable to determine file extension for fileName: {0}", fileName));

            switch (extension.ToLower())
            {
                case @".bmp":
                    return ImageFormat.Bmp;

                case @".gif":
                    return ImageFormat.Gif;

                case @".ico":
                    return ImageFormat.Icon;

                case @".jpg":
                case @".jpeg":
                    return ImageFormat.Jpeg;

                case @".png":
                    return ImageFormat.Png;

                case @".tif":
                case @".tiff":
                    return ImageFormat.Tiff;

                case @".wmf":
                    return ImageFormat.Wmf;

                default:
                    throw new NotImplementedException();
            }
        }

    }
}