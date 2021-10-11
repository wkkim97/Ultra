using Bayer.Ultra.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework
{
    public class EncryptionUtils
    {

        #region 상수 : 암호화에 필요한 key & 벡터 값 정의

        // DES 알고리즘용
        //private const string _encryptionKey = "8D08C2FA";
        //private const string _encryptionIV = "50E9A4CD";

        //TripleDES 알고리즘용
        /// <summary>
        /// key : 16자리 문자열(한글제외)
        /// </summary>
        private const string _encryptionKey = "DotnetSoft123456";
        /// <summary>
        /// IV : 8자리 문자열(한글제외)
        /// </summary>
        private const string _encryptionIV = "Dotnet35";

        #endregion

        #region 쿠키 암복호화
        /// <summary>
        /// 쿠키정보 암호화<br/>
        /// </summary>
        /// <remarks>암호화 할때 Key와 IV(Initialization Vector)를 하드코딩하여 간단한 암호화를 수행
        ///    하는 함수, 알고리즘은 DES알고리즘을 사용한다.<br/>
        ///   리턴되는 값은 Base64인코딩된 값이다.<br/>
        ///   암호화 해제할때는 SimpleDecrypt()를 사용한다.<br/>
        /// </remarks>
        /// <param name="stringSource">소스스트링</param>
        /// <returns>인크립션스트링</returns>
        public static string eWEncrypt(string stringSource)
        {
            EncryptionAlgorithm algorithm = EncryptionAlgorithm.TripleDes;
            byte[] cipherText = null;
            byte[] key = Encoding.UTF8.GetBytes(_encryptionKey);
            byte[] IV = Encoding.UTF8.GetBytes(_encryptionIV);
            try
            {

                Encryptor enc = new Encryptor(algorithm);
                enc.IV = IV;
                cipherText = enc.Encrypt(Encoding.UTF8.GetBytes(stringSource), key);
                return Convert.ToBase64String(cipherText);
            }
            catch (Exception ex)
            {
                throw new Exception("Error eWEncrypt()-" + ex.Message);
            }
        }

        /// <summary>
        /// 암호화된 쿠키정보를 TripleDes로 복호화한다.<br/>
        /// </summary>
        /// <param name="stringSource">소스스트링</param>
        /// <returns>디크립션스트링</returns>
        public static string eWDecrypt(string stringSource)
        {
            EncryptionAlgorithm algorithm = EncryptionAlgorithm.TripleDes;
            byte[] key = Encoding.UTF8.GetBytes(_encryptionKey);
            byte[] IV = Encoding.UTF8.GetBytes(_encryptionIV);
            try
            {
                Decryptor dec = new Decryptor(algorithm);
                dec.IV = IV;
                byte[] encryptText = Convert.FromBase64String(stringSource);
                byte[] plainText = dec.Decrypt(encryptText, key);
                return Encoding.UTF8.GetString(plainText);
            }
            catch (Exception ex)
            {
                throw new Exception("Error eWDecrypt()-" + ex.Message);
            }
        }



        /// <summary>
        ///	사용자 Cookie 정보를 암호화 한다.
        /// </summary>
        /// <param name="stringSource">소스스트링</param>
        /// <returns>인크립션스트링</returns>
        public static string UserInfoEncrypt(string stringSource)
        {
            string strReturn = string.Empty;
            string strLicense = string.Empty;
            string SessionKey = string.Empty;

            try
            {
                strReturn = eWEncrypt(stringSource);
            }
            catch (Exception ex)
            {
                throw new Exception("Error UserInfoEncrypt()-" + ex.Message);
            }
            return strReturn;
        }

        /// <summary>
        /// 사용자 Cookie 정보를 복호화 한다.
        /// </summary>
        /// <param name="stringSource">소스스트링</param>
        /// <returns>디크립션스트링</returns>
        public static string UserInfoDecrypt(string stringSource)
        {
            string strReturn = string.Empty;
            string strLicense = string.Empty;
            string SessionKey = string.Empty;

            try
            {
                strReturn = eWDecrypt(stringSource);
            }
            catch (Exception ex)
            {
                throw new Exception("Error UserInfoDecrypt()-" + ex.Message);
            }
            return strReturn;
        }

        #endregion
    }

    #region Encryptor 암호화객체
    /// <summary>
    /// <b>대칭형 암호 객체를 인스턴스하여, 데이터를 암호화 한다.</b><br/>
    /// </summary>
    public class Encryptor
    {
        private EncryptTransformer transformer;
        private byte[] initVec;
        private byte[] encKey;

        /// <summary>
        /// 암호화 객체 생성<br/>
        /// </summary>
        /// <param name="algId">암호화알고리즘ID</param>
        public Encryptor(EncryptionAlgorithm algId)
        {
            transformer = new EncryptTransformer(algId);
        }

        /// <summary>
        /// 대칭형 암호화알고리즘에 사용하는 백터값<br/>
        /// </summary>
		public byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }

        /// <summary>
        /// 대칭형 암호화알고리즘에 사용하는 키값<br/>
        /// </summary>
		public byte[] Key
        {
            get { return encKey; }
        }

        /// <summary>
        /// 데이터를 암호화한다.
        /// </summary>
        /// <param name="bytesData">암호할 데이터</param>
        /// <param name="bytesKey">암호화키</param>
        /// <returns>암호화한 데이터</returns>
        /// <example>
        /// 암호 키와 백터, 암호할 데이터를 바이트로 변환하여 암호화 객체를 생성한다.
        /// <code>
        ///     byte[] key = Encoding.UTF8.GetBytes(암호키값);
        ///     byte[] IV = Encoding.UTF8.GetBytes(암호백터값);
        ///     byte[] cipherText = null;
        ///     Encryptor enc = new Encryptor(algorithm);    
		///     enc.IV = IV;
		///     cipherText = enc.Encrypt(Encoding.UTF8.GetBytes(암호할데이터), key);    
        /// </code>
        /// </example>
		public byte[] Encrypt(byte[] bytesData, byte[] bytesKey)
        {
            //Set up the stream that will hold the encrypted data.
            MemoryStream memStreamEncryptedData = new MemoryStream();
            transformer.IV = initVec;
            ICryptoTransform transform = transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream encStream = new CryptoStream(memStreamEncryptedData,
                transform,
                CryptoStreamMode.Write);
            try
            {
                //Encrypt the data, write it to the memory stream.
                encStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while writing encrypted data to the stream: \n"
                    + ex.Message);
            }
            //Set the IV and key for the client to retrieve
            encKey = transformer.Key;
            initVec = transformer.IV;
            encStream.FlushFinalBlock();
            encStream.Close();
            //Send the data back.
            return memStreamEncryptedData.ToArray();
        }//end Encrypt

    }
    #endregion 

    #region Decryptor 복호화 객체
    /// <summary>
    /// <b>대칭형 암호 객체를 인스턴스하여, 데이터를 복호화 한다.</b><br/>
    /// </summary>
	public class Decryptor
    {
        private DecryptTransformer transformer;
        private byte[] initVec;

        /// <summary>
        /// 복호화 객체<br/>
        /// </summary>
        /// <param name="algId">암호화알고리즘ID</param>
		public Decryptor(EncryptionAlgorithm algId)
        {
            transformer = new DecryptTransformer(algId);
        }

        /// <summary>
        /// 대칭형 암호화알고리즘에 사용하는 백터값<br/>
        /// </summary>
		public byte[] IV
        {
            set { initVec = value; }
        }

        /// <summary>
        /// 데이터를 복호화한다.</br>
        /// </summary>
        /// <param name="bytesData">복호화할 데이터</param>
        /// <param name="bytesKey">암호화키</param>
        /// <returns>복호화된 데이터</returns>
        /// <example>
        /// 암호 키와 백터, 복호할 데이터를 바이트로 변환하여 복호화 객체를 생성한다.
        /// <code>
        ///     byte[] key = Encoding.UTF8.GetBytes(암호키값);
        ///     byte[] IV = Encoding.UTF8.GetBytes(암호백터값);
        ///     Decryptor dec = new Decryptor(algorithm);   
        ///     dec.IV = IV;
        ///     byte[] encryptText = Convert.FromBase64String(복호할데이터);
        ///     byte[] plainText = dec.Decrypt(encryptText, key);  
        /// </code>
        /// </example>
		public byte[] Decrypt(byte[] bytesData, byte[] bytesKey)
        {
            //Set up the memory stream for the decrypted data.
            MemoryStream memStreamDecryptedData = new MemoryStream();
            //Pass in the initialization vector.
            transformer.IV = initVec;
            ICryptoTransform transform = transformer.GetCryptoServiceProvider(bytesKey);
            CryptoStream decStream = new CryptoStream(memStreamDecryptedData,
                transform,
                CryptoStreamMode.Write);
            try
            {
                decStream.Write(bytesData, 0, bytesData.Length);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while writing encrypted data to the stream: \n"
                    + ex.Message);
            }
            decStream.FlushFinalBlock();
            decStream.Close();
            // Send the data back.
            return memStreamDecryptedData.ToArray();
        } //end Decrypt

    }
    #endregion 

    #region EncryptionAlgorithm - 대칭형암호화 알고리즘 타입 : 열거형
    /// <summary>
    /// 대칭형 암호화알고리즘 타입<br/>
    /// </summary>
	public enum EncryptionAlgorithm { Des = 1, Rc2, Rijndael, TripleDes };
    #endregion 

    #region EncryptTransformer
    /// <summary>
    ///   암호화 객체를 인스턴스화 한다<br/>
    /// </summary>
	internal class EncryptTransformer
    {
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;
        private byte[] encKey;

        /// <summary>
        /// 암호화 알고리즘 객체 인스턴스 생성자<br/>
        /// </summary>
        /// <param name="algId">암호화알고리즘ID</param>
		internal EncryptTransformer(EncryptionAlgorithm algId)
        {
            //Save the algorithm being used.
            algorithmID = algId;
        }

        /// <summary>
        /// 대칭형 암호화알고리즘에 사용하는 백터값<br/>
        /// </summary>
		internal byte[] IV
        {
            get { return initVec; }
            set { initVec = value; }
        }

        /// <summary>
        /// 대칭형 암호화알고리즘에 사용하는 키값<br/>
        /// </summary>
		internal byte[] Key
        {
            get { return encKey; }
        }

        /// <summary>
        /// 암호화 객체를 생성한다.
        /// </summary>
        /// <param name="bytesKey">암호키</param>
        /// <returns>생성된 객체</returns>
		internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            try
            {
                // Pick the provider.
                switch (algorithmID)
                {
                    case EncryptionAlgorithm.Des:
                        {
                            DES des = new DESCryptoServiceProvider();
                            des.Mode = CipherMode.CBC;
                            // See if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = des.Key;
                            }
                            else
                            {
                                des.Key = bytesKey;
                                encKey = des.Key;
                            }
                            // See if the client provided an initialization vector
                            if (null == initVec)
                            { // Have the algorithm create one
                                initVec = des.IV;
                            }
                            else
                            { //No, give it to the algorithm
                                des.IV = initVec;
                            }
                            return des.CreateEncryptor();
                        }
                    case EncryptionAlgorithm.TripleDes:
                        {
                            TripleDES des3 = new TripleDESCryptoServiceProvider();
                            des3.Mode = CipherMode.CBC;
                            // See if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = des3.Key;
                            }
                            else
                            {
                                des3.Key = bytesKey;
                                encKey = des3.Key;
                            }
                            // See if the client provided an IV
                            if (null == initVec)
                            { //Yes, have the alg create one
                                initVec = des3.IV;
                            }
                            else
                            { //No, give it to the alg.
                                des3.IV = initVec;
                            }
                            return des3.CreateEncryptor();
                        }
                    case EncryptionAlgorithm.Rc2:
                        {
                            RC2 rc2 = new RC2CryptoServiceProvider();
                            rc2.Mode = CipherMode.CBC;
                            // Test to see if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = rc2.Key;
                            }
                            else
                            {
                                rc2.Key = bytesKey;
                                encKey = rc2.Key;
                            }
                            // See if the client provided an IV
                            if (null == initVec)
                            { //Yes, have the alg create one
                                initVec = rc2.IV;
                            }
                            else
                            { //No, give it to the alg.
                                rc2.IV = initVec;
                            }
                            return rc2.CreateEncryptor();
                        }
                    case EncryptionAlgorithm.Rijndael:
                        {
                            Rijndael rijndael = new RijndaelManaged();
                            rijndael.Mode = CipherMode.CBC;
                            // Test to see if a key was provided
                            if (null == bytesKey)
                            {
                                encKey = rijndael.Key;
                            }
                            else
                            {
                                rijndael.Key = bytesKey;
                                encKey = rijndael.Key;
                            }
                            // See if the client provided an IV
                            if (null == initVec)
                            { //Yes, have the alg create one
                                initVec = rijndael.IV;
                            }
                            else
                            { //No, give it to the alg.
                                rijndael.IV = initVec;
                            }
                            return rijndael.CreateEncryptor();
                        }
                    default:
                        {
                            throw new CryptographicException("Algorithm ID '" + algorithmID +
                                "' not supported.");
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    #endregion

    #region DecryptTransformer
    /// <summary>
    /// 복호화 객체를 인스턴스화 한다<br/>
    /// </summary>
    internal class DecryptTransformer
    {
        private EncryptionAlgorithm algorithmID;
        private byte[] initVec;


        /// <summary>
        /// 암호화 알고리즘 객체 인스턴스 생성자<br/>
        /// </summary>
        /// <param name="algId">암호화알고리즘ID</param>
		internal DecryptTransformer(EncryptionAlgorithm deCryptId)
        {
            algorithmID = deCryptId;
        }

        /// <summary>
        /// 대칭형 암호화알고리즘에 사용하는 백터값<br/>
        /// </summary>
		internal byte[] IV
        {
            set { initVec = value; }
        }

        /// <summary>
        /// 복호화 객체를 생성한다.
        /// </summary>
        /// <param name="bytesKey">암호키</param>
        /// <returns>생성된 객체</returns>
		internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
        {
            try
            {
                // Pick the provider.
                switch (algorithmID)
                {
                    case EncryptionAlgorithm.Des:
                        {
                            DES des = new DESCryptoServiceProvider();
                            des.Mode = CipherMode.CBC;
                            des.Key = bytesKey;
                            des.IV = initVec;
                            return des.CreateDecryptor();
                        }
                    case EncryptionAlgorithm.TripleDes:
                        {
                            TripleDES des3 = new TripleDESCryptoServiceProvider();
                            des3.Mode = CipherMode.CBC;
                            return des3.CreateDecryptor(bytesKey, initVec);
                        }
                    case EncryptionAlgorithm.Rc2:
                        {
                            RC2 rc2 = new RC2CryptoServiceProvider();
                            rc2.Mode = CipherMode.CBC;
                            return rc2.CreateDecryptor(bytesKey, initVec);
                        }
                    case EncryptionAlgorithm.Rijndael:
                        {
                            Rijndael rijndael = new RijndaelManaged();
                            rijndael.Mode = CipherMode.CBC;
                            return rijndael.CreateDecryptor(bytesKey, initVec);
                        }
                    default:
                        {
                            throw new CryptographicException("Algorithm ID '" + algorithmID +
                                "' not supported.");
                        }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        } //end GetCryptoServiceProvider

    }
    #endregion
}
