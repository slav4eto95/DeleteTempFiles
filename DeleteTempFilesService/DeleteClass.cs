using System;
using System.IO;

namespace DeleteTempFilesService
{
    public static class DeleteClass
    {
        public static void WriteErrorLog(string message)
        {
            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                streamWriter.WriteLine(DateTime.Now.ToString() + ": " + message);
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch
            {

            }
        }

        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }

        /*public static void DeleteTempFiles(string pathToTempFiles)
        {
            try
            {
                Directory.Delete(pathToTempFiles, true);

                bool directoryExists = Directory.Exists(pathToTempFiles);
                WriteErrorLog("Temp directory exists? - " + directoryExists);
            }
            catch
            {
                //WriteErrorLog(e);
            }
        }*/
        // https://social.msdn.microsoft.com/Forums/en-US/12a84d42-c2db-4908-8161-9383c5b91003/how-to-delete-recent-prefetch-and-temp-items-in-system-using-c?forum=csharplanguage
        public static void DeleteTempFiles(string folderName)
        {
            foreach (var folder in Directory.GetDirectories(folderName))
            {
                try
                {
                    Directory.Delete(folder, true);
                }
                catch
                {
                }
            }
            WriteErrorLog("Deleting folders - done!!");
            foreach (var file in Directory.GetFiles(folderName))
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch
                {
                }
            }
            WriteErrorLog("Deleting files - done!!");
        }
    }
}
