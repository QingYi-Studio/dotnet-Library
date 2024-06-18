using System;
using System.IO;

namespace QingYi.Core.SH
{
    public class Folder
    {
        // 显示文件夹的函数
        public static void ShowFolder(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    // 获取文件夹的当前属性
                    FileAttributes attributes = System.IO.File.GetAttributes(folderPath);

                    // 如果文件夹是隐藏的，则显示它
                    if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        System.IO.File.SetAttributes(folderPath, attributes & ~FileAttributes.Hidden);
                    }
                    else
                    {
                        throw new Exception("The folder is already displayed.");
                    }
                }
                else
                {
                    throw new Exception("Folder not found.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error displaying folder: {e.Message}");
            }
        }

        // 隐藏文件夹的函数
        public static void HideFolder(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    // 获取文件夹的当前属性
                    FileAttributes attributes = System.IO.File.GetAttributes(folderPath);

                    // 如果文件夹是隐藏的，则无需更改
                    if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        throw new Exception("The folder is already hidden.");
                    }
                    else
                    {
                        // 否则，将文件夹设置为隐藏
                        System.IO.File.SetAttributes(folderPath, attributes | FileAttributes.Hidden);
                    }
                }
                else
                {
                    throw new Exception("Folder does not exist.");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error hiding folder: {e.Message}");
            }
        }
    }
}
