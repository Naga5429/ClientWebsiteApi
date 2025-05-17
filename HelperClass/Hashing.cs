using System.Security.Cryptography;
using System.Text;

namespace ClientWebsiteAPI.HelperClass
{
    public class Hashing
    {
        internal static byte[] GetHashKey(string hashKey)
        {
            // Initialise
            UTF8Encoding encoder = new UTF8Encoding();

            // Get the salt
            string salt = "@@-Dubai~Technologies~CRM-$$";
            byte[] saltBytes = encoder.GetBytes(salt);

            // Setup the hasher
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(hashKey, saltBytes);

            // Return the key
            return rfc.GetBytes(16);
        }
        internal static string Encrypt(string dataToEncrypt)
        {
            // Initialise
            AesManaged encryptor = new AesManaged();
            byte[] key = Hashing.GetHashKey("DubaiTechnologies-CabMan");

            // Set the key
            encryptor.Key = key;
            encryptor.IV = key;

            // create a memory stream
            using (MemoryStream encryptionStream = new MemoryStream())
            {
                // Create the crypto stream
                using (CryptoStream encrypt = new CryptoStream(encryptionStream, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    // Encrypt
                    byte[] utfD1 = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
                    encrypt.Write(utfD1, 0, utfD1.Length);
                    encrypt.FlushFinalBlock();
                    encrypt.Close();

                    // Return the encrypted data
                    return Convert.ToBase64String(encryptionStream.ToArray());
                }
            }
        }
        internal static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
