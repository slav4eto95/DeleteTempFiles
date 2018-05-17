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

        public static void DeleteTempFiles(string pathToTempFiles)
        {
            try
            {
                Directory.Delete(pathToTempFiles, true);

                bool directoryExists = Directory.Exists(pathToTempFiles);
                WriteErrorLog("Temp directory exists? - " + directoryExists);
            }
            catch (Exception e)
            {
                WriteErrorLog(e);
            }
        }
    }
}
