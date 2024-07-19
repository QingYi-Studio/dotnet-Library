using System;
using System.IO;

namespace QingYi.Core.Folder.SavedGames
{
    public class CreateFolder
    {
        public static string Create(string folderName)
        {
            // 在 Saved Games 文件夹内创建一个名为 "MyGameFolder" 的子文件夹
            string newFolderPath = Path.Combine(GetSavedGamesFolder.Get(), folderName);

            try
            {
                Directory.CreateDirectory(newFolderPath);

                return newFolderPath;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
