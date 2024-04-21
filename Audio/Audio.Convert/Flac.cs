using NAudio.Wave;

namespace Audio.Convert
{
    public class Flac
    {
        public class Flac2Wav
        {
            public static void Convert(string flacFilePath, string wavFilePath)
            {
                Console.WriteLine("Undo");
            }
        }

        public class Flac2Mp3
        {
            public static void Convert(string inputFilePath, string outputFilePath)
            {
                using var reader = new FlacReader(inputFilePath);
                var outFormat = new WaveFormat(44100, 16, 2); // MP3 format
                using var writer = new LameMP3FileWriter(outputFilePath, outFormat);
                byte[] buffer = new byte[reader.Length];
                int read = reader.Read(buffer, 0, buffer.Length);
                writer.Write(buffer, 0, read);
            }
        }
    }
}
