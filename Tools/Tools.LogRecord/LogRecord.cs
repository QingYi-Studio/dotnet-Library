using System.Security.Cryptography;
using System.Text;
using System.IO.Compression;

namespace Tools.LogRecord
{
    public class LogRecord
    {
        private string _folderPath;

        public LogRecord(string folderPath)
        {
            _folderPath = folderPath;
        }

        /// <summary>
        /// 写入单个日志文件
        /// </summary>
        /// <param name="folderPath">日志文件所在文件夹路径（相对或绝对路径）</param>
        /// <param name="encryptionType">日志文件加密方式(无加密则写none，支持md5、sha1、sha256三种)</param>
        /// <param name="content">要写入的文件内容</param>
        public void WriteSingleLogFile(string folderPath, string encryptionType = "none", params string[] content)
        {
            string filePath = GetAbsolutePath(folderPath, "debug.log");

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var line in content)
                {
                    writer.WriteLine(line);
                }
            }

            if (encryptionType != "none")
            {
                EncryptFile(filePath, encryptionType);
            }
        }

        /// <summary>
        /// 写入自定义日志文件
        /// </summary>
        /// <param name="folderPath">日志文件所在文件夹路径（相对或绝对路径）</param>
        /// <param name="fileName">日志文件名称(包含后缀名)</param>
        /// <param name="encryptionType">日志文件加密方式(无加密则写none，支持md5、sha1、sha256三种)</param>
        /// <param name="content">要写入的文件内容</param>
        public void WriteCustomLogFile(string folderPath, string fileName, string encryptionType = "none", params string[] content)
        {
            string filePath = GetAbsolutePath(folderPath, fileName);

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var line in content)
                {
                    writer.WriteLine(line);
                }
            }

            if (encryptionType != "none")
            {
                EncryptFile(filePath, encryptionType);
            }
        }

        private void EncryptFile(string filePath, string encryptionType)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            byte[] encryptedBytes;

            using (var algorithm = GetEncryptionAlgorithm(encryptionType))
            {
                encryptedBytes = algorithm.ComputeHash(fileBytes);
            }

            File.WriteAllBytes(filePath, encryptedBytes);
        }

        private HashAlgorithm GetEncryptionAlgorithm(string encryptionType)
        {
            return encryptionType switch
            {
                "md5" => MD5.Create(),
                "sha1" => SHA1.Create(),
                "sha256" => SHA256.Create(),
                _ => throw new NotSupportedException("Encryption type not supported."),
            };
        }

        private string GetAbsolutePath(string path, string fileName = "")
        {
            if (Path.IsPathRooted(path))
            {
                return Path.Combine(path, fileName);
            }
            else
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                return Path.Combine(baseDirectory, path, fileName);
            }
        }
    }

    public static class Output
    {
        public static void WriteToFile(string fileName, string content)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(content);
            }
        }
    }

    class Example
    {
        // 用法示例
        void LogRecordExampleUsage()
        {
            // 创建日志记录器实例
            LogRecord logRecord = new LogRecord("logs");

            // 单个日志文件写入示例
            logRecord.WriteSingleLogFile("logs", "md5", "This is a debug message.", "Another debug message.");

            // 自定义日志文件写入示例
            logRecord.WriteCustomLogFile("CustomLogs", "custom.log", "none", "Custom log entry 1", "Custom log entry 2");
        }

        void OutputExample()
        {
            Output.WriteToFile("output.txt", "This is a single line of output.");

            string multiLineContent = @"This is a
multi-line
output.";
            Output.WriteToFile("output.txt", multiLineContent);
        }
    }
}
