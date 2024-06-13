using System;
using System.Drawing.Imaging;
using System.Drawing;

namespace QingYi.ImageProcess.PngIco
{
    public class IcoToPng
    {
        private long _quality = 100; // 默认质量为100

        public long Quality
        {
            get { return _quality; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Quality must be between 0 and 100.");
                }
                _quality = value;
            }
        }

        public void Convert(string icoFilePath, string pngFilePath)
        {
            // Load ICO image
            using (Icon icoImage = new Icon(icoFilePath))
            {
                // Extract first frame (icon) from the ICO
                Bitmap pngImage = icoImage.ToBitmap();

                // Save as PNG with specified quality
                SavePngWithQuality(pngImage, pngFilePath, (byte)_quality); // 将 long 类型的质量值转换为 byte 类型
            }
        }

        private void SavePngWithQuality(Bitmap image, string outputPath, byte quality) // 将参数类型从 long 改为 byte
        {
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality); // 显式指定 Encoder 类型的来源

            ImageCodecInfo pngCodecInfo = GetEncoderInfo(ImageFormat.Png);

            image.Save(outputPath, pngCodecInfo, encoderParameters);
        }

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
