namespace ClientWebsiteAPI.HelperClass
{
   
        public static class Util
        {
            //private IConfiguration? Configuration;
            //public Util(IConfiguration? configuration)
            //{
            //    Configuration = configuration;
            //}

            public static string GetUniqueKeyErrorMessage(string errorMsg)
            {
                string result = string.Empty;

                try
                {
                    string constraintCSV = string.Empty;
                    string contraintFilePath = string.Empty;

                    if (Environment.GetEnvironmentVariable("AppSettings:ContraintFilePath") != null)
                        contraintFilePath = Convert.ToString(Environment.GetEnvironmentVariable("AppSettings:ContraintFilePath"));

                    if (File.Exists(contraintFilePath))
                    {
                        constraintCSV = File.ReadAllText(contraintFilePath);

                        foreach (string constraint in constraintCSV.Split(','))
                        {
                            if (errorMsg.Contains(constraint) && !string.IsNullOrEmpty(constraint))
                            {
                                int inderscoreIndex = constraint.LastIndexOf('_');
                                if (inderscoreIndex >= 0 && constraint.Split('_').Length > 2)
                                {
                                    result = constraint.Substring(inderscoreIndex + 1, (constraint.Length - (inderscoreIndex + 1)));

                                    if (result.ToLower() == "code")
                                        result = "msgCodeAlreadyExists";
                                    else if (result.ToLower() == "name")
                                        result = "msgNameAlreadyExists";
                                    else if (result.ToLower() == "id")
                                        result = "msgIDAlready";
                                    else if (result.ToLower() == "serialnumber")
                                        result = "msgSerialNumberExist";

                                    break;
                                }
                                else
                                {
                                    result = "msgAlreadyExist";
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            }
        }
    }


