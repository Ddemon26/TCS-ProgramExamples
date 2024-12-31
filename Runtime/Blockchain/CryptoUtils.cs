using System.Security.Cryptography;
using System.Text;
namespace TCS.ProgramExamples.Blockchain {
    public static class CryptoUtils {
        public static string ComputeSHA256(string input) {
            using var sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static (byte[] privateKey, byte[] publicKey) GenerateRsaKeyPair(int keySize = 2048) {
            using var rsa = new RSACryptoServiceProvider(keySize);
            byte[] priv = rsa.ExportCspBlob(true);
            byte[] pub = rsa.ExportCspBlob(false);
            return (priv, pub);
        }

        public static byte[] SignDataRsa(byte[] data, byte[] privateKey) {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(privateKey);
            return rsa.SignData(data, CryptoConfig.MapNameToOID("SHA256"));
        }

        public static bool VerifyDataRsa(byte[] data, byte[] signature, byte[] publicKey) {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportCspBlob(publicKey);
            return rsa.VerifyData(data, CryptoConfig.MapNameToOID("SHA256"), signature);
        }
    }
}