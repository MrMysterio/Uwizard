namespace Uwizard {
    public struct SARC {
        public static string lerror = ""; // Gets the last error that occurred in this struct. Similar to the C perror().

        private struct sarcnode {
            public uint hash;
            public byte unk;
            public uint off, srt, end;

            public sarcnode(uint fhash, byte unknown, uint foffset, uint fstart, uint fend) {
                hash = fhash;
                unk = unknown;
                off = foffset;
                srt = fstart;
                end = fend;
            }
        }

        private struct filehash {
            public uint hash;
            public int index;

            public filehash(uint fhash, int findex) {
                hash = fhash;
                index = findex;
            }
        }

        private struct filedata {
            public string filename, realname;
            public int filesize, namesize, filenum;

            public filedata(string _filename, string _realname, int _filesize, int _namesize, int _filenum) {
                filename = _filename;
                realname = _realname;
                filesize = _filesize;
                namesize = _namesize;
                filenum  = _filenum ;
            }
        }

        private static ushort makeu16(byte b1, byte b2) {
            return (ushort)(((ushort) b1 << 8) | (ushort) b2);
        }

        private static uint makeu32(byte b1, byte b2, byte b3, byte b4) {
            return ((uint) b1 << 24) | ((uint) b2 << 16) | ((uint) b3 << 8) | (uint) b4;
        }

        private static byte[] breaku16(ushort u16) {
            return new byte[] {(byte)(u16>>8), (byte)(u16&0xFF)};
        }

        private static byte[] breaku32(uint u32) {
            return new byte[] {(byte) (u32>>24), (byte) ((u32>>16)&0xFF), (byte) ((u32>>8)&0xFF), (byte) (u32&0xFF)};
        }

        private static void makedirexist(string dir) {
            string dpath = System.IO.Path.GetFullPath(dir);
            int numdirs = 0;
            for (int c = 0; c < dpath.Length; c++)
                if (dpath[c] == '\\') numdirs++;

            for (int c = numdirs; c >= 0; c--) {
                string tmp = dpath;
                for (int cc = 0; cc < c; cc++)
                    tmp = System.IO.Path.GetDirectoryName(tmp);
                if (!System.IO.Directory.Exists(tmp))
                    System.IO.Directory.CreateDirectory(tmp);
            }
        }

        public static bool extract(string infile, string outdir) {
            return extract(System.IO.File.ReadAllBytes(infile), outdir);
        }

        public static bool extract(byte[] infile, string outdir) {
            if (infile[0] != 'S' || infile[1] != 'A' || infile[2] != 'R' || infile[3] != 'C') {
                lerror = "Not a SARC archive!";
                return false;
            }

            int pos=4;
            ushort hdr=makeu16(infile[pos], infile[pos+1]);
            pos += 2;
            ushort order=makeu16(infile[pos], infile[pos+1]);
            pos += 2;

            if (order != 65279) {
                lerror = "Little endian is not supported!";
                return false;
            }

            uint size = makeu32(infile[pos], infile[pos+1], infile[pos+2], infile[pos+3]);
            pos += 4;
            uint doff = makeu32(infile[pos], infile[pos+1], infile[pos+2], infile[pos+3]);
            pos += 4;
            uint unknown = makeu32(infile[pos], infile[pos+1], infile[pos+2], infile[pos+3]);
            pos += 4;

            if (infile[pos] != 'S' || infile[pos+1] != 'F' || infile[pos+2] != 'A' || infile[pos+3] != 'T') {
                lerror = "Unknown file section!";
                return false;
            }
            pos+=4;

            ushort hdr2=makeu16(infile[pos], infile[pos+1]);
            pos += 2;
            ushort nodec=makeu16(infile[pos], infile[pos+1]);
            pos += 2;
            uint hashr = makeu32(infile[pos], infile[pos+1], infile[pos+2], infile[pos+3]);
            pos += 4;

            sarcnode[] nodes = new sarcnode[nodec];
            sarcnode tmpnode = new sarcnode();

            for (int c = 0; c < nodec; c++) {
                tmpnode.hash = makeu32(infile[pos], infile[pos+1], infile[pos+2], infile[pos+3]);
                pos += 4;
                tmpnode.unk = infile[pos];
                pos += 1;
                tmpnode.off = makeu32(0, infile[pos], infile[pos+1], infile[pos+2]);
                pos += 3;
                tmpnode.srt = makeu32(infile[pos], infile[pos+1], infile[pos+2], infile[pos+3]);
                pos += 4;
                tmpnode.end = makeu32(infile[pos], infile[pos+1], infile[pos+2], infile[pos+3]);
                pos += 4;
                nodes[c] = tmpnode;
            }

            if (infile[pos] != 'S' || infile[pos+1] != 'F' || infile[pos+2] != 'N' || infile[pos+3] != 'T') {
                lerror = "Unknown file section!";
                return false;
            }


            pos+=4;

            ushort hdr3=makeu16(infile[pos], infile[pos+1]);
            pos += 2;
            ushort unk2=makeu16(infile[pos], infile[pos+1]);
            pos += 2;

            string[] fnames = new string[nodec];
            string tmpstr;

            for (int c = 0; c < nodec; c++) {
                tmpstr = "";
                while (infile[pos] != 0) {
                    tmpstr = tmpstr + ((char)infile[pos]).ToString();
                    pos += 1;
                }
                while (infile[pos] == 0)
                    pos += 1;

                fnames[c] = tmpstr;
            }

            if (!System.IO.Directory.Exists(outdir)) System.IO.Directory.CreateDirectory(outdir);

            System.IO.StreamWriter sw;

            for (int c = 0; c < nodec; c++) {
                makedirexist(System.IO.Path.GetDirectoryName(outdir + "/" + fnames[c]));
                sw = new System.IO.StreamWriter(outdir + "/" + fnames[c]);
                sw.BaseStream.Write(infile, (int) (nodes[c].srt + doff), (int) (nodes[c].end - nodes[c].srt));
                sw.Close();
                sw.Dispose();
            }

            return true;
        }

        public static bool pack(string indir, string outfile) {
            return pack(indir, outfile, 0x100);
        }

        private static uint calchash(string name) {
            ulong result = 0;
            for (int c = 0; c < name.Length; c++) {
                result = (((byte)name[c]) + (result * 0x65)) & 0xFFFFFFFF;
            }
            return (uint)(result & 0xFFFFFFFF);
        }

        private static string[] getfiles(string dir) {
            if (dir == "") dir = System.Environment.CurrentDirectory;
            return System.IO.Directory.GetFiles(dir);
        }

        private static string[] getdirs(string dir) {
            if (dir == "") dir = System.Environment.CurrentDirectory;
            return System.IO.Directory.GetDirectories(dir);
        }

        private static uint getfilesize(string fpath) {
            System.IO.StreamReader sr = new System.IO.StreamReader(fpath);
            uint fs = (uint) sr.BaseStream.Length;
            sr.Close();
            sr.Dispose();
            return fs;
        }

        public static bool pack(string indir, string outfile, uint padding) {
            if (!System.IO.Directory.Exists(indir)) return false;

            string[] indir_files = System.IO.Directory.GetFiles(indir=="" ? System.Environment.CurrentDirectory : indir, "*.*", System.IO.SearchOption.AllDirectories);

            filedata[] filedatalist = new filedata[indir_files.Length];
            int lenfiles=0, numfiles=indir_files.Length, lennames=0;
            uint filesize;
            for (int c = 0; c < indir_files.Length; c++) {
                string realname = indir_files[c];
                string filename = indir_files[c].Replace(indir + System.IO.Path.DirectorySeparatorChar.ToString(), "");

                filesize = getfilesize(realname);
                if (filesize % padding > 0) filesize += (padding - (filesize % padding));
                int namesize = filename.Length;
                namesize += (4 - (namesize % 4));
                lennames += namesize;
                filedatalist[c] = new filedata(filename, realname, (int)filesize, namesize, numfiles);
            }

            filehash[] hashes_unsorted = new filehash[numfiles];

            for (int c = 0; c < numfiles; c++) {
                hashes_unsorted[c] = new filehash(calchash(filedatalist[c].filename), c);
            }

            uint lhash;
            bool[] hashes_done = new bool[hashes_unsorted.Length];
            filehash[] hashes = new filehash[hashes_unsorted.Length];
            int dhi=0;

            for (int c=0; c < hashes.Length; c++) {
                lhash = uint.MaxValue;
                for (int cc=0; cc < hashes_unsorted.Length; cc++) {
                    if (hashes_done[cc]) continue;
                    if (hashes_unsorted[cc].hash < lhash) {
                        dhi=cc;
                        lhash = hashes_unsorted[cc].hash;
                    }
                }
                hashes_done[dhi] = true;
                hashes[c] = hashes_unsorted[dhi];
            }

            for (int c = 0; c < numfiles; c++) {
                lenfiles += (int)filedatalist[hashes[c].index].filesize;
            }
            uint lastfile = getfilesize(filedatalist[hashes[hashes.Length-1].index].realname);
            lenfiles += (int)lastfile;
            filesize = (uint) (32 + (16 * numfiles) + 8 + lennames);
            uint padSFAT = (padding - (filesize % padding));
            uint datastart = padSFAT + filesize;
            filesize += (uint) (padSFAT + lenfiles);

            System.IO.StreamWriter sw = new System.IO.StreamWriter(outfile);
            sw.BaseStream.Write(new byte[] {83, 65, 82, 67, 0x00, 0x14, 0xFE, 0xFF}, 0, 8);
            sw.BaseStream.Write(breaku32(filesize), 0, 4);
            sw.BaseStream.Write(breaku32(datastart), 0, 4);
            sw.BaseStream.Write(new byte[] {0x01, 0x00, 0x00, 0x00, 83, 70, 65, 84, 0x00, 0x0C}, 0, 10);
            sw.BaseStream.Write(breaku16((ushort)numfiles), 0, 2);
            sw.BaseStream.Write(breaku32(0x65), 0, 4);
            int strpos=0, filepos=0;
            for (int c=0; c < numfiles; c++) {
                sw.BaseStream.Write(breaku32(hashes[c].hash), 0, 4);
                sw.BaseStream.WriteByte(0x01); // Unknown, see http://mk8.tockdom.com/wiki/SARC_%28File_Format%29
                sw.BaseStream.Write(breaku32((uint)(strpos >> 2)), 1, 3);
                strpos += filedatalist[hashes[c].index].namesize;
                sw.BaseStream.Write(breaku32((uint)filepos), 0, 4);
                filesize = getfilesize(filedatalist[hashes[c].index].realname);
                sw.BaseStream.Write(breaku32((uint) filepos+(uint) filesize), 0, 4);
                filepos += filedatalist[hashes[c].index].filesize;
            }
            sw.BaseStream.Write(new byte[] {83, 70, 78, 84, 0x00, 0x08, 0x00, 0x00}, 0, 8);
            for (int c=0; c < numfiles; c++) {
                string tn = filedatalist[hashes[c].index].filename;
                for (int cc = 0; cc < tn.Length; cc++) {
                    sw.BaseStream.WriteByte((byte) tn[cc]);
                }
                int numpad0 = filedatalist[hashes[c].index].namesize - filedatalist[hashes[c].index].filename.Length;
                for (int cc = 0; cc < numpad0; cc++)
                    sw.BaseStream.WriteByte(0);
            }
            for (int cc = 0; cc < padSFAT; cc++)
                sw.BaseStream.WriteByte(0);

            byte[] tmp;
            for (int c=0; c < numfiles; c++) {
                tmp = System.IO.File.ReadAllBytes(filedatalist[hashes[c].index].realname);
                sw.BaseStream.Write(tmp, 0, tmp.Length);
                filesize = (uint)tmp.Length;
                if (c < numfiles-1) {
                    int numpad0 = (int)(filedatalist[hashes[c].index].filesize - filesize);
                    for (int cc=0; cc < numpad0; cc++)
                        sw.BaseStream.WriteByte(0);
                }
            }

            sw.Close();
            sw.Dispose();

            return true;
        }
    }
}