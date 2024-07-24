using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace QingYi.ImageProcess.PngIco
{
    public class PngToIco
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

        public void Convert(string pngFilePath, string icoFilePath)
        {
            // Load PNG image
            using (Bitmap pngImage = new Bitmap(pngFilePath))
            {
                // Save as ICO with specified quality
                SaveIcoWithQuality(pngImage, icoFilePath, _quality);
            }
        }

        private void SaveIcoWithQuality(Bitmap image, string outputPath, long quality)
        {
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality); // 设置指定的质量

            ImageCodecInfo icoCodecInfo = GetEncoderInfo(ImageFormat.Icon);

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Icon);
                ms.Position = 0;
                Icon icon = new Icon(ms);
                using (FileStream stream = new FileStream(outputPath, FileMode.Create))
                {
                    icon.Save(stream);
                }
            }
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

        public static implicit operator PngToIco(string v)
        {
            var converter = new PngToIco();
            // Implement logic to convert string to PngToIco instance if needed
            return converter;
        }
    }
}
