using ClientWebsiteAPI.HelperClass;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System;
using Newtonsoft.Json;

namespace ClientWebsiteAPI.GeneralClasses
{
    public class GConvert
    {
        public static short ToInt16(object objValue)
        {
            short Value;
            try
            {
                Value = Convert.ToInt16(objValue);
            }
            catch (Exception ex)
            {
                Value = 0;
            }
            return Value;
        }

        public static int ToInt32(object objValue)
        {
            int Value;
            try
            {
                Value = Convert.ToInt32(objValue);
            }
            catch (Exception ex)
            {
                Value = 0;
            }
            return Value;
        }

        public static int? ToNullableInt32(object objValue)
        {
            int? Value = null;
            try
            {
                if (objValue != null)
                    Value = Convert.ToInt32(objValue);
            }
            catch (Exception ex)
            {
                Value = null;
            }
            return Value;
        }

        public static long ToInt64(object objValue)
        {
            long Value;
            try
            {
                Value = Convert.ToInt64(objValue);
            }
            catch (Exception ex)
            {
                Value = 0;
            }
            return Value;
        }

        public static long? ToNullableInt64(object objValue)
        {
            long? Value = null;
            try
            {
                if (objValue != null)
                    Value = Convert.ToInt64(objValue);
            }
            catch (Exception ex)
            {
                Value = null;
            }
            return Value;
        }

        public static double ToDouble(object objValue)
        {
            double Value;
            try
            {
                Value = Convert.ToDouble(objValue);
            }
            catch (Exception ex)
            {
                Value = 0;
            }
            return Value;
        }

        public static double? ToNullableDouble(object objValue)
        {
            double? Value = null;
            try
            {
                if (objValue != null)
                    Value = Convert.ToDouble(objValue);
            }
            catch (Exception ex)
            {
                Value = null;
            }
            return Value;
        }

        public static decimal ToDecimal(object objValue)
        {
            decimal Value;
            try
            {
                Value = Convert.ToDecimal(objValue);
            }
            catch (Exception ex)
            {
                Value = 0;
            }
            return Value;
        }

        public static decimal? ToNullableDecimal(object objValue)
        {
            decimal? Value = null;
            try
            {
                if (objValue != null)
                    Value = Convert.ToDecimal(objValue);
            }
            catch (Exception ex)
            {
                Value = null;
            }
            return Value;
        }

        public static bool ToBoolean(object objValue)
        {
            bool Value;
            try
            {
                Value = Convert.ToBoolean(objValue);
            }
            catch (Exception ex)
            {
                Value = false;
            }
            return Value;
        }

        public static bool? ToNullableBoolean(object objValue)
        {
            bool? Value = null;
            try
            {
                if (objValue != null)
                    Value = Convert.ToBoolean(objValue);
            }
            catch (Exception ex)
            {
                Value = null;
            }
            return Value;
        }

        public static DateTime ToDateTime(object objValue)
        {
            DateTime Value;
            try
            {
                if (objValue == null)
                {
                    Value = Convert.ToDateTime("1/1/1900");
                }
                else
                {
                    Value = Convert.ToDateTime(objValue);
                }
            }
            catch (Exception ex)
            {
                Value = Convert.ToDateTime("1/1/1900");
            }
            return Value;
        }

        public static DateTime? ToNullableDateTime(object objValue)
        {
            DateTime? Value = null;
            try
            {
                if (objValue != null)
                {
                    Value = Convert.ToDateTime(objValue);
                }
            }
            catch (Exception ex)
            {
                Value = null;
            }
            return Value;
        }

        public static string ToDateTimeString(object objValue)
        {
            string Value;
            try
            {
                if (objValue == null)
                {
                    Value = "";
                }
                else if (Convert.ToDateTime(objValue).Year.Equals(1900))
                {
                    Value = "";
                }
                else
                {
                    Value = Convert.ToDateTime(objValue).ToString("MM/dd/yyyy HH:mm");
                }
            }
            catch (Exception ex)
            {
                Value = "";
            }
            return Value;
        }

        public static string DisplayDateTime(object objValue)
        {
            string Value;
            try
            {
                if (objValue == null)
                {
                    Value = "";
                }
                else if (Convert.ToDateTime(objValue).Year.Equals(1900))
                {
                    Value = "";
                }
                else
                {
                    Value = Convert.ToDateTime(objValue).ToString("dd-MMM-yyyy HH:mm");
                }
            }
            catch (Exception ex)
            {
                Value = "";
            }
            return Value;
        }

        public static string DisplayDateTimeWithSeconds(object objValue)
        {
            string Value;
            try
            {
                if (objValue == null)
                {
                    Value = "";
                }
                else if (Convert.ToDateTime(objValue).Year.Equals(1900))
                {
                    Value = "";
                }
                else
                {
                    Value = Convert.ToDateTime(objValue).ToString("dd-MMM-yyyy HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                Value = "";
            }
            return Value;
        }

        public static string DisplayDate(object objValue)
        {
            string Value;
            try
            {
                if (objValue == null)
                {
                    Value = "";
                }
                else if (Convert.ToDateTime(objValue).Year.Equals(1900))
                {
                    Value = "";
                }
                else
                {
                    Value = Convert.ToDateTime(objValue).ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception ex)
            {
                Value = "";
            }
            return Value;
        }

        public static string DisplayTime(object objValue)
        {
            string Value;
            try
            {
                if (objValue == null)
                {
                    Value = "";
                }
                else if (Convert.ToDateTime(objValue).Year.Equals(1900))
                {
                    Value = "";
                }
                else
                {
                    Value = Convert.ToDateTime(objValue).ToString("HH:mm");
                }
            }
            catch (Exception ex)
            {
                Value = "";
            }
            return Value;
        }

        //No Need to convert the date from one timezone to other, bec here we are processing all as UTC to epoch(UnixTime) vice versa.
        public static long? ToEpochTime(object objValue)
        {
            long? epochTime = null;

            try
            {
                DateTime dateObject = Convert.ToDateTime(objValue);
                dateObject = DateTime.SpecifyKind(dateObject, DateTimeKind.Utc);

                if (dateObject.Year == 1900 && dateObject.Month == 1 && dateObject.Day == 1)
                {
                    epochTime = defaultDatetimeEpochTime();
                }
                else
                {
                    DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    //int seconds = dateObject.Second;
                    var ticks = dateObject.Ticks - epoch.Ticks;
                    epochTime = (ticks / TimeSpan.TicksPerSecond); //- seconds;
                }

            }
            catch (Exception) { epochTime = defaultDatetimeEpochTime(); }

            return epochTime;
        }
        public static long defaultDatetimeEpochTime()
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime defaultepoch = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var ticks = defaultepoch.Ticks - epoch.Ticks;
            return (ticks / TimeSpan.TicksPerSecond);
        }


        public static string ToBase64String(object objValue)
        {
            string result = string.Empty;

            try
            {
                if (objValue != null)
                    result = Convert.ToBase64String((byte[])objValue);
            }
            catch (Exception) { }
            return result;
        }

        public static DynamicData ToConvertDynamicDataFromJSON(string JsonString)
        {
            DynamicData dynamicData = new DynamicData();
            try
            {
                dynamicData = JsonConvert.DeserializeObject<DynamicData>(JsonString);
            }
            catch (Exception ex)
            {

            }

            return dynamicData;
        }

        public static X509Certificate2 GetRSAPublicKey(string certificatePath)
        {
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

            X509Certificate2 cert2 = new X509Certificate2(certificatePath);

            return cert2;
        }

        public static string ToRSAEncryptedPassword(string stringToConvert, X509Certificate2 certificate)
        {
            ASCIIEncoding ByteConverter = new ASCIIEncoding();
            byte[] data = ByteConverter.GetBytes(stringToConvert);

            RSACryptoServiceProvider rsa = certificate.PublicKey.Key as RSACryptoServiceProvider;

            byte[] cryptedData = rsa.Encrypt(data, true);

            return Convert.ToBase64String(cryptedData);
        }

        public static bool IsPropertyExists(DynamicData dynamicObj, string property)
        {
            try
            {
                return dynamicObj.isMemberExists(property);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsPropertyExistsWithValue(DynamicData dynamicObj, string property)
        {
            try
            {
                return dynamicObj.isMemberExists(property) && GConvert.ToDouble(dynamicObj.GetMember(property)) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static string ToString(dynamic _requestData, string propertyName)
        {
            string Value;
            Object objValue = null;
            DynamicData requestData = (DynamicData)Convert.ChangeType(_requestData, typeof(DynamicData));
            try
            {
                if (requestData.isMemberExists(propertyName))
                    objValue = requestData.GetMember(propertyName);

                Value = objValue == null ? String.Empty : Convert.ToString(objValue);
            }
            catch (Exception ex)
            {
                
                Value = "";
            }
            return Value;
        }
        public static long ToInt64(dynamic _requestData, string propertyName)
        {
            long Value;
            Object objValue = null;
            DynamicData requestData = (DynamicData)Convert.ChangeType(_requestData, typeof(DynamicData));
            try
            {
                if (requestData.isMemberExists(propertyName))
                    objValue = requestData.GetMember(propertyName);

                Value = objValue == null ? default(Int64) : Convert.ToInt64(objValue);
            }
            catch (Exception ex)
            {
               
                Value = 0;
            }
            return Value;
        }

        public static int ToInt32(dynamic _requestData, string propertyName)
        {
            int Value;
            Object objValue = null;
            DynamicData requestData = (DynamicData)Convert.ChangeType(_requestData, typeof(DynamicData));
            try
            {
                if (requestData.isMemberExists(propertyName))
                    objValue = requestData.GetMember(propertyName);

                Value = objValue == null ? default(int) : Convert.ToInt32(objValue);
            }
            catch (Exception ex)
            {
                
                Value = 0;
            }
            return Value;
        }

        public static DateTime ToDateTimeFromEpochTimeMili(object epochTime, int dateTimeSecondsResetFor = 3)
        {
            DateTime datetime;

            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(epochTime)))
                {
                    return Convert.ToDateTime("1/1/1900");
                }

                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                datetime = epoch.AddMilliseconds(GConvert.ToDouble(epochTime));

                TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, DateTime.Now.Millisecond);
                if (dateTimeSecondsResetFor == 1)
                    timeSpan = new TimeSpan(0, 0, 0, 0 - datetime.Second);
                else if (dateTimeSecondsResetFor == 2)
                    timeSpan = new TimeSpan(0, 0, 0, 59 - datetime.Second);

                datetime = datetime.Add(timeSpan);

            }
            catch (Exception) { datetime = Convert.ToDateTime("1/1/1900"); }

            return datetime;
        }
        public static byte[] GetBinaryDataFromBase64String(string hexString)
        {
            byte[] fileData = null;
            byte[] imageBytes = new byte[hexString.Length / 2];


            try
            {
                if (hexString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                {
                    hexString = hexString.Substring(2);
                }

                // Convert hex string to byte array

                for (int i = 0; i < imageBytes.Length; i++)
                {
                    imageBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
                }
            }
            catch (Exception ex)
            {
                fileData = null;
            }

            return imageBytes;
        }

    }
}

