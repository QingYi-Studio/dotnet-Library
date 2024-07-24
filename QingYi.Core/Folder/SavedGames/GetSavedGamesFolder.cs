using System;
using System.IO;

namespace QingYi.Core.Folder.SavedGames
{
    public class GetSavedGamesFolder
    {
        public static string Get()
        {
            // 获取当前用户的个人文件夹路径
            string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // 构建 Saved Games 文件夹路径
            string savedGamesFolder = Path.Combine(userProfile, "Saved Games");

            return savedGamesFolder;
        }
    }
}
