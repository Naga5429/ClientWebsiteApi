using System.IO;
using System;
using Microsoft.Extensions.Configuration;

namespace ClientWebsiteAPI.HelperClass
{
    public class Logger
    {
        private string logFolder = string.Empty;

        /// <summary>
        ///
        /// </summary>
        private IConfiguration? Configuration;
        public Logger(IConfiguration? configuration)
        {
            Configuration = configuration;
        }
        public Logger()
        {
            //logFolder = Environment.GetEnvironmentVariable("AppSettings:LogPath").Replace("\\", "\\\\");
            // logFolder = Configuration["LogPath"].ToString();
            //if (!string.IsNullOrEmpty(logFolder))
            //{
            //    logFolder = logFolder.Replace("\\", "\\\\");
            //}
            //else
            //{
            //    // Handle the case where the environment variable is missing
            //    logFolder = "DefaultPath"; // or handle it appropriately
            //}
            logFolder= "C:\\\\DTApplicationLog\\\\ClientWebsiteAPI\\\\";


            CreateLogFolder();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="logFolder"></param>
        public Logger(string logFolder)
        {
            logFolder = logFolder.Replace("\\", "\\\\");
            CreateLogFolder();
        }

        /// <summary>
        ///
        /// </summary>
        public void CreateLogFolder()
        {
            if (!Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
        }
        public void LogData(string from, string data)
        {
            try
            {
                FileStream file = new FileStream(logFolder + "Data.Log", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(DateTime.Now + ": " + from + ": " + data);
                if (file.Length > 1024567)
                {
                    sw.Flush();
                    sw.Close();
                    file.Close();
                    DirectoryInfo dir = new System.IO.DirectoryInfo(logFolder);
                    int count = dir.GetFiles("Data*.log").Length;
                    File.Move(logFolder + "Data.Log",
                        logFolder + "Data" + DateTime.Now.ToString("yyMMddHHmmssfff") + ".Log");
                    File.Delete(logFolder + "Data.log");
                }
                else
                {
                    sw.Flush();
                    sw.Close();
                    file.Close();
                }
            }
            catch (Exception)
            {
                // throw;
            }
        }

        public void LogException(string from, string data)
        {
            try
            {
                FileStream file = new FileStream(logFolder + "Exception.Log", FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(DateTime.Now + " : " + from + " : " + data);
                if (file.Length > 1024567)
                {
                    sw.Flush();
                    sw.Close();
                    file.Close();
                    DirectoryInfo dir = new System.IO.DirectoryInfo(logFolder);
                    int count = dir.GetFiles("Exception*.log").Length;
                    File.Move(logFolder + "Exception.Log",
                        logFolder + "Ex" + DateTime.Now.ToString("yyMMddHHmmssfff") + ".Log");
                    File.Delete(logFolder + "Exception.log");
                }
                else
                {
                    sw.Flush();
                    sw.Close();
                    file.Close();
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }
}
