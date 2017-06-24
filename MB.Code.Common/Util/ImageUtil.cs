using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
namespace MB.Core.Common.Util
{
    public class ImageUtil
    {
        public MemoryStream GetCodeImage(string code)
        {
            Bitmap image = new Bitmap((int)((double)code.Length * 21.5), 22);
            Graphics g = Graphics.FromImage(image);
            MemoryStream ms = null;
            try
            {
                Random random = new Random();
                g.Clear(Color.White);
                for (int i = 0; i < 10; i++)
                {
                    int x = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x, y, x2, y2);
                }
                Font font = new Font("Arial", 14f, FontStyle.Bold);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(code, font, brush, 2f, 2f);
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                ms = new MemoryStream();
                image.Save(ms, ImageFormat.Gif);
            }
            catch
            {
                g.Dispose();
                image.Dispose();
            }
            return ms;
        }
        public static bool CheckImageFormat(Image image, ImageFormat imageformat)
        {
            bool flag = false;
            if (image != null && image.RawFormat.Equals(imageformat))
            {
                flag = true;
            }
            return flag;
        }
        public static bool CheckImageFormat(Stream stream, ImageFormat imageformat)
        {
            return ImageUtil.CheckImageFormat(Image.FromStream(stream), imageformat);
        }
        public static bool CheckImageFormat(string fileName, ImageFormat imageformat)
        {
            return ImageUtil.CheckImageFormat(Image.FromFile(fileName), imageformat);
        }
        public static bool CheckImagePixel(Image image, int pixel)
        {
            bool flag = false;
            if (image != null && Image.GetPixelFormatSize(image.PixelFormat) == pixel)
            {
                flag = true;
            }
            return flag;
        }
        public static bool CheckImagePixel(Stream stream, int pixel)
        {
            return ImageUtil.CheckImagePixel(Image.FromStream(stream), pixel);
        }
        public static bool CheckImagePixel(string fileName, int pixel)
        {
            return ImageUtil.CheckImagePixel(Image.FromFile(fileName), pixel);
        }
        public static int GetImagePixel(Image image)
        {
            return Image.GetPixelFormatSize(image.PixelFormat);
        }
        public static int GetImagePixel(Stream stream)
        {
            return ImageUtil.GetImagePixel(Image.FromStream(stream));
        }
        public static int GetImagePixel(string fileName)
        {
            return ImageUtil.GetImagePixel(Image.FromFile(fileName));
        }
        private static void SaveImage(Image image, string savePath)
        {
            if (!string.IsNullOrEmpty(savePath) && savePath.Contains("\\"))
            {
                string str = Path.GetExtension(savePath).ToLower();
                ImageFormat jpeg = ImageFormat.Jpeg;
                string key;
                switch (key = str)
                {
                    case ".bmp":
                        jpeg = ImageFormat.Bmp;
                        goto IL_12A;
                    case ".emf":
                        jpeg = ImageFormat.Emf;
                        goto IL_12A;
                    case ".exif":
                        jpeg = ImageFormat.Exif;
                        goto IL_12A;
                    case ".gif":
                        jpeg = ImageFormat.Gif;
                        goto IL_12A;
                    case ".icon":
                        jpeg = ImageFormat.Icon;
                        goto IL_12A;
                    case ".png":
                        jpeg = ImageFormat.Png;
                        goto IL_12A;
                    case ".tiff":
                        jpeg = ImageFormat.Tiff;
                        goto IL_12A;
                    case ".wmf":
                        jpeg = ImageFormat.Wmf;
                        goto IL_12A;
                }
                jpeg = ImageFormat.Jpeg;
                IL_12A:  
                image.Save(savePath, jpeg);
            }
        }
        public static Image Thumbnail(Image image, int width, int height)
        {
            return ImageUtil.Thumbnail(image, string.Empty, width, height);
        }
        public static Image Thumbnail(Image image, string thumbnailPath, int width, int height)
        {
            if (image == null)
            {
                throw new ArgumentNullException("原图片不能为null");
            }
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("缩略图宽度或高度不能小于或等于零");
            }
            image = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            ImageUtil.SaveImage(image, thumbnailPath);
            return image;
        }
        public static Image Thumbnail(string sourcePath, string thumbnailPath, int width, int height)
        {
            if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
            {
                throw new ArgumentException("原图片的地址不能为空或文件不存在");
            }
            return ImageUtil.Thumbnail(Image.FromFile(sourcePath), thumbnailPath, width, height);
        }
        public static Image Watermark(Image image, string mark, WatermarkPosition position)
        {
            return ImageUtil.Watermark(image, string.Empty, mark, position);
        }
        public static Image Watermark(Image image, string watermarkPath, string mark, WatermarkPosition position)
        {
            if (image == null)
            {
                throw new ArgumentNullException("原图片不能为null");
            }
            if (string.IsNullOrEmpty(mark))
            {
                throw new ArgumentException("水印文字不能为空");
            }
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                using (Font font = new Font("Arial", 10f, FontStyle.Bold | FontStyle.Italic))
                {
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        SizeF ef = graphics.MeasureString(mark, font);
                        float x;
                        float y;
                        switch (position)
                        {
                            case WatermarkPosition.Bottom_left:
                                x = 10f;
                                y = (float)image.Height - ef.Height - 10f;
                                goto IL_16F;
                            case WatermarkPosition.Top_left:
                                x = 10f;
                                y = 10f;
                                goto IL_16F;
                            case WatermarkPosition.Top_right:
                                x = (float)image.Width - ef.Width - 10f;
                                y = 10f;
                                goto IL_16F;
                            case WatermarkPosition.Center:
                                x = (float)image.Width / 2f - ef.Width / 2f;
                                y = (float)image.Height / 2f - ef.Height / 2f;
                                goto IL_16F;
                        }
                        x = (float)image.Width - ef.Width - 10f;
                        y = (float)image.Height - ef.Height - 10f;
                        IL_16F:
                        graphics.DrawString(mark, font, brush, x, y);
                        ImageUtil.SaveImage(image, watermarkPath);
                    }
                }
            }
            return image;
        }
        public static Image Watermark(string sourcePath, string watermarkPath, string mark, WatermarkPosition position)
        {
            if (string.IsNullOrEmpty(sourcePath) || !File.Exists(sourcePath))
            {
                throw new ArgumentException("原图片的地址不能为空或文件不存在");
            }
            return ImageUtil.Watermark(Image.FromFile(sourcePath), watermarkPath, mark, position);
        }
    }
}
