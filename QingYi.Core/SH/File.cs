using System;
using System.IO;

namespace QingYi.Core.SH
{
    public class File
    {
        public static void Show(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    FileAttributes attributes = System.IO.File.GetAttributes(filePath);

                    if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        System.IO.File.SetAttributes(filePath, attributes & ~FileAttributes.Hidden);
                    }
                    else
                    {
                        throw new Exception("The file is already show");
                    }
                }
                else
                {
                    throw new Exception("File not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Show file error: {e.Message}");
            }
        }

        public static void HideFile(string filePath)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    FileAttributes attributes = System.IO.File.GetAttributes(filePath);

                    if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        throw new Exception("File is already hide.");
                    }
                    else
                    {
                        System.IO.File.SetAttributes(filePath, attributes | FileAttributes.Hidden);
                    }
                }
                else
                {
                    throw new Exception("File not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Catch error when hide the file: {e.Message}");
            }
        }
    }
}
