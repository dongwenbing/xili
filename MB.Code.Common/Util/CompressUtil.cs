using ICSharpCode.SharpZipLib.Zip.Compression;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
namespace MB.Core.Common.Util
{
    public static class CompressUtil
    {
        public static byte[] CompressBinary(byte[] input)
        {
            Deflater compressor = new Deflater(-1);
            compressor.SetInput(input);
            compressor.Finish();
            byte[] result;
            using (MemoryStream ms = new MemoryStream(input.Length))
            {
                byte[] buffer = new byte[1024];
                while (!compressor.IsFinished)
                {
                    int count = compressor.Deflate(buffer);
                    ms.Write(buffer, 0, count);
                }
                result = ms.ToArray();
            }
            return result;
        }
        public static byte[] DecompressBinary(byte[] input)
        {
            Inflater decompressor = new Inflater();
            decompressor.SetInput(input);
            byte[] result;
            using (MemoryStream ms = new MemoryStream(input.Length))
            {
                int bufferLength = 1024;
                if (input.Length < bufferLength)
                {
                    bufferLength = input.Length;
                }
                byte[] buffer = new byte[bufferLength];
                while (!decompressor.IsFinished)
                {
                    int count = decompressor.Inflate(buffer);
                    ms.Write(buffer, 0, count);
                }
                result = ms.ToArray();
            }
            return result;
        }
        public static string ZipCompressString(string str)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
            zip.Write(buffer, 0, buffer.Length);
            zip.Close();
            ms.Position = 0L;
            byte[] zipBuffer = new byte[ms.Length];
            ms.Read(zipBuffer, 0, zipBuffer.Length);
            ms.Close();
            return Convert.ToBase64String(zipBuffer);
        }
        public static string ZipDeCompressString(string str)
        {
            byte[] buffer = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream();
            ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0L;
            GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
            byte[] zipBuffer = new byte[1024];
            MemoryStream ms2 = new MemoryStream();
            while (true)
            {
                int bytesRead = zip.Read(zipBuffer, 0, zipBuffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }
                ms2.Write(zipBuffer, 0, bytesRead);
            }
            zip.Close();
            return Encoding.UTF8.GetString(ms2.ToArray(), 0, (int)ms2.Length);
        }
        public static byte[] DecompressByGzip(byte[] data)
        {
            byte[] result;
            using (MemoryStream streamOutput = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(new MemoryStream(data), CompressionMode.Decompress))
                {
                    byte[] readBuffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = gZipStream.Read(readBuffer, 0, readBuffer.Length)) != 0)
                    {
                        streamOutput.Write(readBuffer, 0, bytesRead);
                    }
                    gZipStream.Close();
                }
                result = streamOutput.ToArray();
            }
            return result;
        }
        public static byte[] CompressByGzip(byte[] data)
        {
            byte[] result;
            using (MemoryStream stream = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(stream, CompressionMode.Compress, true))
                {
                    gZipStream.Write(data, 0, data.Length);
                    gZipStream.Close();
                    stream.Position = 0L;
                    byte[] compressedData = stream.ToArray();
                    stream.Close();
                    result = compressedData;
                }
            }
            return result;
        }
        public static byte[] DecompressByDeflate(byte[] data)
        {
            byte[] result;
            using (new MemoryStream())
            {
                byte[] decompressBuffer = null;
                using (DeflateStream streamOutput = new DeflateStream(new MemoryStream(data), CompressionMode.Decompress, true))
                {
                    streamOutput.Flush();
                    int nSize = 1024;
                    int offset = 0;
                    decompressBuffer = new byte[data.Length * 10];
                    while (true)
                    {
                        int bytesRead = streamOutput.Read(decompressBuffer, offset, nSize);
                        if (bytesRead == 0)
                        {
                            break;
                        }
                        offset += bytesRead;
                    }
                    streamOutput.Close();
                }
                result = decompressBuffer;
            }
            return result;
        }
        public static byte[] CompressByDeflate(byte[] data)
        {
            byte[] result;
            using (MemoryStream stream = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Compress, true))
                {
                    deflateStream.Write(data, 0, data.Length);
                    deflateStream.Close();
                    stream.Position = 0L;
                    byte[] compressedData = stream.ToArray();
                    stream.Close();
                    result = compressedData;
                }
            }
            return result;
        }
    }
}
