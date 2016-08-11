using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TestWork.Models;

namespace TestWork.Code
{
    public class WorkWithDirectory
    {
        private void FileSort(int fileSize, Result tree)
        {
            //FileInfo file = new FileInfo(path);            
            if (fileSize <= 10485760)
            {
                tree.Less10Mb += 1;
            }
            if (fileSize > 10485760 && fileSize <= 52428800)
            {
                tree.Between10Mb_50Mb = tree.Between10Mb_50Mb + 1;
            }
            if (fileSize >= 104857600)
            {
                tree.More100Mb = tree.More100Mb + 1;
            }
        }
        private void GetAllFiles(string folder, Action<int, Result> fileAction, Result tree)
        {

            DirectoryInfo dir = new DirectoryInfo(folder);
            foreach (int fileSize in dir.EnumerateFiles().Select(m => m.Length))
            {
                fileAction(fileSize, tree);
            }
            foreach (string subDir in Directory.GetDirectories(folder))
            {
                try
                {
                    GetAllFiles(subDir, fileAction, tree);
                }
                catch
                { }
            }
        }
        public Result GetDirAndFiles(string directory)
        {
            Result tree = new Result();
            if (String.IsNullOrEmpty(directory))
            {

                string[] disks = DriveInfo.GetDrives().Where(m => m.DriveType == DriveType.Fixed).Select(m => m.Name).ToArray();
                tree.Trees = disks;
                return tree;
            }
            else
            {
                try
                {
                    string[] DirectoryName = Directory.GetDirectories(directory);
                    string[] FilesName = Directory.GetFiles(directory);
                    tree.Trees = DirectoryName.Concat(FilesName).ToArray();
                    if (directory.Length > 3)
                    {
                        GetAllFiles(directory, FileSort, tree);
                        tree.ParentDir = Directory.GetParent(directory).ToString();
                    }
                }
                catch
                {
                    string[] disks = DriveInfo.GetDrives().Where(m => m.DriveType == DriveType.Fixed).Select(m => m.Name).ToArray();
                    tree.Trees = disks;
                    tree.Error = "Вы выбрали файл или пытаетесь получить директиву к которой нет доступа";
                }
                return tree;
            }

        }

    }
}