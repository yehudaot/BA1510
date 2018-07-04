using System;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

namespace atpLib.Infra
{
    public static class Infra
    {
        
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            return GetMd5Hash(md5Hash, Encoding.UTF8.GetBytes(input));
        }

        public static string GetMd5Hash(MD5 md5Hash, byte[] input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(input);

            return ByteArrToString(data);
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ByteArrToString(Byte[] arr) 
        {
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < arr.Length; i++)
            {
                sBuilder.Append(arr[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public class FixedSizedQueue<T> : ConcurrentQueue<T>
        {
            private readonly object syncObject = new object();

            public int Size { get; private set; }

            public FixedSizedQueue(int size)
            {
                Size = size;
            }

            public new void Enqueue(T obj)
            {
                base.Enqueue(obj);
                lock (syncObject)
                {
                    while (base.Count > Size)
                    {
                        T outObj;
                        base.TryDequeue(out outObj);
                    }
                }
            }
        }

    }
}
