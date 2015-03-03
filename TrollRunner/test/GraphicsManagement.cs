using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrollRunner
{
    public class GraphicsManagement
    {
        private const string FolderName = @"../../graphics/"; 

        private static Dictionary<string, char[,]> graphicsContainer = new Dictionary<string, char[,]>();

        private static string GetFilePath(string fileName)
        {
            try
            {
                string filePathAndName = System.IO.Path.GetFullPath(FolderName + fileName + ".txt");
                if (!File.Exists(filePathAndName))
                    throw new System.Exception("File not found!");
                return filePathAndName;
            }
            catch (ArgumentNullException)
            {
                System.Console.WriteLine("There is no folder containing image files or the path is not correct.");
                return ""; 
            }
            catch (ArgumentException)
            {
                System.Console.WriteLine("There is no folder containing image files or the path is not correct.");
                return ""; 
            }
        }

        public static char[,] GetGraphic(string imageName)
        {
            if (graphicsContainer.ContainsKey(imageName))
            {
                return graphicsContainer[imageName];
            }
            else
            {
                return null;
            }
        }
        public static void InitializeGraphics()
        {
            string[] fileEntries = Directory.GetFiles(FolderName);
            int imageWidth, imageHeight;
            string[] fileData; 
            string imageName="";
            foreach (string fullFileName in fileEntries)
            {
                fileData = File.ReadAllLines(fullFileName);
                imageHeight = fileData.GetLength(0);
                imageWidth = fileData[0].Length;
                char[,] image = new char[imageHeight, imageWidth];
                for (int i = 0; i < imageHeight; i++)
                {
                    for (int j = 0; j < imageWidth; j++)
                    {
                        image[i, j] = fileData[i][j];
                    }
                }
                imageName = System.IO.Path.GetFileName(fullFileName).Replace(".txt", "");
                graphicsContainer.Add(imageName, image);
            }
        }
    }
}