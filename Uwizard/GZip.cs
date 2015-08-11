namespace Uwizard {
    public struct gzip {
        public static string lerror = ""; // Gets the last error that occurred in this struct. Similar to the C perror().

        public static bool decompress(byte[] indata, string outfile) {
            try {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(indata);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(outfile);
                System.IO.Compression.GZipStream gzs = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress);
                int lbyte = gzs.ReadByte();
                while (lbyte != -1) {
                    sw.BaseStream.WriteByte((byte) lbyte);
                    lbyte = gzs.ReadByte();
                }
                gzs.Close();
                gzs.Dispose();
                sw.Close();
                sw.Dispose();
            } catch (System.Exception ex) {
                lerror = ex.Message;
                return false;
            }
            return true;
        }

        public static bool compress(string infile, string outfile) {
            try {
                byte[] ifdata = System.IO.File.ReadAllBytes(infile);
                System.IO.StreamWriter sw = new System.IO.StreamWriter(outfile);
                System.IO.Compression.GZipStream gzs = new System.IO.Compression.GZipStream(sw.BaseStream, System.IO.Compression.CompressionMode.Compress);
                gzs.Write(ifdata, 0, ifdata.Length);
                gzs.Close();
                gzs.Dispose();
            } catch (System.Exception ex) {
                lerror = ex.Message;
                return false;
            }
            return true;
        }

        public static bool decompress(string infile, string outfile) {
            try {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(outfile);
                System.IO.StreamReader sr = new System.IO.StreamReader(infile);
                System.IO.Compression.GZipStream gzs = new System.IO.Compression.GZipStream(sr.BaseStream, System.IO.Compression.CompressionMode.Decompress);
                int lbyte = gzs.ReadByte();
                while (lbyte != -1) {
                    sw.BaseStream.WriteByte((byte) lbyte);
                    lbyte = gzs.ReadByte();
                }
                gzs.Close();
                gzs.Dispose();
                sw.Close();
                sw.Dispose();
            } catch (System.Exception ex) {
                lerror = ex.Message;
                return false;
            }
            return true;
        }
    }
}
