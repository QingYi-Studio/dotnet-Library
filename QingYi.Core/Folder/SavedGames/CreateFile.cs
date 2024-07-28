using System;
using System.IO;

namespace QingYi.Core.Folder.SavedGames
{
    public class CreateFile
    {
        public static string Create(string fileName)
        {
            // 在 Saved Games 文件夹内创建一个名为 "MyGameFolder" 的子文件夹
            string newFilePath = Path.Combine(GetSavedGamesFolder.Get(), fileName);

            try
            {
                // 创建空文件
                using (File.Create(newFilePath)) { } ;
                return newFilePath;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
