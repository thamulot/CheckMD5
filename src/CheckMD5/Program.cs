using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;

namespace CheckMD5
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] firstHash = null, secondHash = null;

            var fileSize0 = new FileInfo(args[0]).Length;
            var fileSize1 = new FileInfo(args[1]).Length;
            if (fileSize0 == fileSize1)
            {
                using (var md5 = MD5.Create())
                {
                    // Technically, the "source" hash should be kept all time and be calculated only once.
                    using (var stream = File.OpenRead(args[0]))
                    {
                        firstHash = md5.ComputeHash(stream);
                    }

                    using (var stream = File.OpenRead(args[1]))
                    {
                        secondHash = md5.ComputeHash(stream);
                    }
                }
            }

            Console.WriteLine("Are equals? {0}", (fileSize0 == fileSize1) && firstHash.SequenceEqual(secondHash));

            // NOTE: If source differ from result, then use git compare for human readability?
        }
    }
}
