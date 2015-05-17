using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace PriceTag.Code.Helper
{
    public class ImageResizer
    {
        private int allowedFileSizeInByte;
        private string sourcePath;
        private string destinationPath;
        private Stream fileStream;
        private string fileExtention;

        public ImageResizer(int allowedSize, string sourcePath, string destinationPath)
        {
            allowedFileSizeInByte = allowedSize;
            this.sourcePath = sourcePath;
            this.destinationPath = destinationPath;
        }

        public ImageResizer(int allowedSize, Stream fs, string extention)
        {
            allowedFileSizeInByte = allowedSize;
            this.fileStream = fs;
            this.fileExtention = extention;
        }

        public void ScaleImage()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(sourcePath, FileMode.Open))
                {
                    Bitmap bmp = (Bitmap)Image.FromStream(fs);
                    SaveTemporary(bmp, ms, 100);

                    while (ms.Length < 0.9 * allowedFileSizeInByte || ms.Length > allowedFileSizeInByte)
                    {
                        double scale = Math.Sqrt((double)allowedFileSizeInByte / (double)ms.Length);
                        ms.SetLength(0);
                        bmp = ScaleImage(bmp, scale);
                        SaveTemporary(bmp, ms, 100);
                    }

                    if (bmp != null)
                        bmp.Dispose();
                    SaveImageToFile(ms,destinationPath);
                }
            }
        }

        public byte[] ScaleImageFromStream(out byte[] imageThumbnailBytes, int width, int height)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (this.fileStream)
                {
                    Bitmap bmp = (Bitmap)Image.FromStream(this.fileStream);

                    // Create cropped Thumbnail
                    int x = bmp.Width / 2 - width / 2;
                    int y = bmp.Height / 2 - height / 2;
                    Bitmap imageThumbnail = (Bitmap)CreateThumbnail(bmp, new Size(200, 200));
                    imageThumbnailBytes = ImageToByte(imageThumbnail);
                    // End thumnail

                    SaveTemporary(bmp, ms, 100, this.fileExtention);

                    while (ms.Length < 0.9 * allowedFileSizeInByte || ms.Length > allowedFileSizeInByte)
                    {
                        double scale = Math.Sqrt((double)allowedFileSizeInByte / (double)ms.Length);
                        ms.SetLength(0);
                        bmp = ScaleImage(bmp, scale);
                        SaveTemporary(bmp, ms, 100, this.fileExtention);
                    }

                    if (bmp != null)
                        bmp.Dispose();
                    //SaveImageToFile(ms);
                    return ms.ToArray();
                }
            }
        }

        private void SaveImageToFile(MemoryStream ms, string dest)
        {
            byte[] data = ms.ToArray();

            using (FileStream fs = new FileStream(dest, FileMode.Create))
            {
                fs.Write(data, 0, data.Length);
            }
        }

        private void SaveTemporary(Bitmap bmp, MemoryStream ms, int quality)
        {
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            var codec = GetImageCodecInfo();
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            if (codec != null)
                bmp.Save(ms, codec, encoderParams);
            else
                bmp.Save(ms, GetImageFormat());
        }

        private void SaveTemporary(Bitmap bmp, MemoryStream ms, int quality, string extention)
        {
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            var codec = GetImageCodecInfo(extention);
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            if (codec != null)
                bmp.Save(ms, codec, encoderParams);
            else
                bmp.Save(ms, GetImageFormat());
        }

        public Bitmap ScaleImage(Bitmap image, double scale)
        {
            int newWidth = (int)(image.Width * scale);
            int newHeight = (int)(image.Height * scale);

            Bitmap result = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                g.DrawImage(image, 0, 0, result.Width, result.Height);
            }
            return result;
        }

        private ImageCodecInfo GetImageCodecInfo()
        {
            FileInfo fi = new FileInfo(sourcePath);

            switch (fi.Extension)
            {
                case ".bmp": return ImageCodecInfo.GetImageEncoders()[0];
                case ".jpg":
                case ".jpeg": return ImageCodecInfo.GetImageEncoders()[1];
                case ".gif": return ImageCodecInfo.GetImageEncoders()[2];
                case ".tiff": return ImageCodecInfo.GetImageEncoders()[3];
                case ".png": return ImageCodecInfo.GetImageEncoders()[4];
                default: return null;
            }
        }

        private ImageCodecInfo GetImageCodecInfo(string extention)
        {
            switch (extention)
            {
                case ".bmp": return ImageCodecInfo.GetImageEncoders()[0];
                case ".jpg":
                case ".jpeg": return ImageCodecInfo.GetImageEncoders()[1];
                case ".gif": return ImageCodecInfo.GetImageEncoders()[2];
                case ".tiff": return ImageCodecInfo.GetImageEncoders()[3];
                case ".png": return ImageCodecInfo.GetImageEncoders()[4];
                default: return null;
            }
        }

        private ImageFormat GetImageFormat()
        {
            FileInfo fi = new FileInfo(sourcePath);

            switch (fi.Extension)
            {
                case ".jpg": return ImageFormat.Jpeg;
                case ".bmp": return ImageFormat.Bmp;
                case ".gif": return ImageFormat.Gif;
                case ".png": return ImageFormat.Png;
                case ".tiff": return ImageFormat.Tiff;
                default: return ImageFormat.Png;
            }
        }

        public Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);
            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            return cropped;
        }

        public byte[] ImageToByte(Image img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public System.Drawing.Image CreateThumbnail(System.Drawing.Image image, Size thumbnailSize)
        {
            float scalingRatio = CalculateScalingRatio(image.Size, thumbnailSize);

            int scaledWidth = (int)Math.Round((float)image.Size.Width * scalingRatio);
            int scaledHeight = (int)Math.Round((float)image.Size.Height * scalingRatio);
            int scaledLeft = (thumbnailSize.Width - scaledWidth) / 2;
            int scaledTop = (thumbnailSize.Height - scaledHeight) / 2;

            // For portrait mode, adjust the vertical top of the crop area so that we get more of the top area
            if (scaledWidth < scaledHeight && scaledHeight > thumbnailSize.Height)
            {
                scaledTop = (thumbnailSize.Height - scaledHeight) / 4;
            }

            Rectangle cropArea = new Rectangle(scaledLeft, scaledTop, scaledWidth, scaledHeight);

            System.Drawing.Image thumbnail = new Bitmap(thumbnailSize.Width, thumbnailSize.Height);
            using (Graphics thumbnailGraphics = Graphics.FromImage(thumbnail))
            {
                thumbnailGraphics.CompositingQuality = CompositingQuality.HighQuality;
                thumbnailGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                thumbnailGraphics.SmoothingMode = SmoothingMode.HighQuality;
                thumbnailGraphics.DrawImage(image, cropArea);
            }
            return thumbnail;
        }

        private float CalculateScalingRatio(Size originalSize, Size targetSize)
        {
            float originalAspectRatio = (float)originalSize.Width / (float)originalSize.Height;
            float targetAspectRatio = (float)targetSize.Width / (float)targetSize.Height;

            float scalingRatio = 0;

            if (targetAspectRatio >= originalAspectRatio)
            {
                scalingRatio = (float)targetSize.Width / (float)originalSize.Width;
            }
            else
            {
                scalingRatio = (float)targetSize.Height / (float)originalSize.Height;
            }

            return scalingRatio;
        }
    }
}