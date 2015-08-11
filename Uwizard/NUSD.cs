using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;


namespace Uwizard {
    class NUSD {

    }
    /* libWiiSharp is distributed in the hope that it will be
    * useful, but WITHOUT ANY WARRANTY; without even the implied warranty
    * of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    * GNU General Public License for more details.
    *
    * You should have received a copy of the GNU General Public License
    * along with this program.  If not, see <http://www.gnu.org/licenses/>.
    */

    namespace libWiiSharp {

        internal struct ContentIndices:IComparable {
            private int index;
            private int contentIndex;

            public int Index { get { return index; } }
            public int ContentIndex { get { return contentIndex; } }

            public ContentIndices(int index, int contentIndex) {
                this.index = index;
                this.contentIndex = contentIndex;
            }

            public int CompareTo(object obj) {
                if (obj is ContentIndices) return contentIndex.CompareTo(((ContentIndices) obj).contentIndex);
                else throw new ArgumentException();
            }
        }

        public static class Shared {
        /// <summary>
        /// Merges two string arrays into one without double entries.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string[] MergeStringArrays(string[] a, string[] b)
        {
            List<string> sList = new List<string>(a);

            foreach (string currentString in b)
                if (!sList.Contains(currentString)) sList.Add(currentString);

            sList.Sort();
            return sList.ToArray();
        }

        /// <summary>
        /// Compares two byte arrays.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="firstIndex"></param>
        /// <param name="second"></param>
        /// <param name="secondIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool CompareByteArrays(byte[] first, int firstIndex, byte[] second, int secondIndex, int length)
        {
            if (first.Length < length || second.Length < length) return false;

            for (int i = 0; i < length; i++)
                if (first[firstIndex + i] != second[secondIndex + i]) return false;

            return true;
        }

        /// <summary>
        /// Compares two byte arrays.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool CompareByteArrays(byte[] first, byte[] second)
        {
            if (first.Length != second.Length) return false;
            else
                for (int i = 0; i < first.Length; i++)
                    if (first[i] != second[i]) return false;

            return true;
        }

        /// <summary>
        /// Turns a byte array into a string, default separator is a space.
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] byteArray, char separator)
        {
            string res = string.Empty;

            foreach (byte b in byteArray)
                res += b.ToString("x2").ToUpper() + separator;

            return res.Remove(res.Length - 1);
        }

        /// <summary>
        /// Turns a hex string into a byte array.
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hexString)
        {
            byte[] ba = new byte[hexString.Length / 2];

            for (int i = 0; i < hexString.Length / 2; i++)
                ba[i] = byte.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            
            return ba;
        }

        /// <summary>
        /// Counts how often the given char exists in the given string.
        /// </summary>
        /// <param name="theString"></param>
        /// <param name="theChar"></param>
        /// <returns></returns>
        public static int CountCharsInString(string theString, char theChar)
        {
            int count = 0;

            foreach (char thisChar in theString)
                if (thisChar == theChar)
                    count++;

            return count;
        }

        /// <summary>
        /// Pads the given value to a multiple of the given padding value, default padding value is 64.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long AddPadding(long value)
        {
            return AddPadding(value, 64);
        }

        /// <summary>
        /// Pads the given value to a multiple of the given padding value, default padding value is 64.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static long AddPadding(long value, int padding)
        {
            if (value % padding != 0)
            {
                value = value + (padding - (value % padding));
            }

            return value;
        }

        /// <summary>
        /// Pads the given value to a multiple of the given padding value, default padding value is 64.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int AddPadding(int value)
        {
            return AddPadding(value, 64);
        }

        /// <summary>
        /// Pads the given value to a multiple of the given padding value, default padding value is 64.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static int AddPadding(int value, int padding)
        {
            if (value % padding != 0)
            {
                value = value + (padding - (value % padding));
            }

            return value;
        }

        /// <summary>
        /// Swaps endianness.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort Swap(ushort value)
        {
            return (ushort)IPAddress.HostToNetworkOrder((short)value);
        }

        /// <summary>
        /// Swaps endianness.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static uint Swap(uint value)
        {
            return (uint)IPAddress.HostToNetworkOrder((int)value);
        }

        /// <summary>
        /// Swaps endianness
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ulong Swap(ulong value)
        {
            return (ulong)IPAddress.HostToNetworkOrder((long)value);
        }

        /// <summary>
        /// Turns a ushort array into a byte array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] UShortArrayToByteArray(ushort[] array)
        {
            List<byte> results = new List<byte>();
            foreach (ushort value in array)
            {
                byte[] converted = BitConverter.GetBytes(value);
                results.AddRange(converted);
            }
            return results.ToArray();
        }

        /// <summary>
        /// Turns a uint array into a byte array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] UIntArrayToByteArray(uint[] array)
        {
            List<byte> results = new List<byte>();
            foreach (uint value in array)
            {
                byte[] converted = BitConverter.GetBytes(value);
                results.AddRange(converted);
            }
            return results.ToArray();
        }

        /// <summary>
        /// Turns a byte array into a uint array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static uint[] ByteArrayToUIntArray(byte[] array)
        {
            UInt32[] converted = new UInt32[array.Length / 4];
            int j = 0;

            for (int i = 0; i < array.Length; i += 4)
                converted[j++] = BitConverter.ToUInt32(array, i);

            return converted;
        }

        /// <summary>
        /// Turns a byte array into a ushort array.
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static ushort[] ByteArrayToUShortArray(byte[] array)
        {
            ushort[] converted = new ushort[array.Length / 2];
            int j = 0;

            for (int i = 0; i < array.Length; i += 2)
                converted[j++] = BitConverter.ToUInt16(array, i);

            return converted;
        }
        }
        public enum StoreType {
            EncryptedContent=0,
            DecryptedContent=1,
            WAD=2,
            All=3,
            Empty=4
        }

        public class NusClient:IDisposable {
            private const string WII_NUS_URL = "http://nus.cdn.shop.wii.com/ccs/download/";
            private const string DSI_NUS_URL = "http://nus.cdn.t.shop.nintendowifi.net/ccs/download/";

            private const string WII_USER_AGENT = "wii libnup/1.0";
            private const string DSI_USER_AGENT = "Opera/9.50 (Nintendo; Opera/154; U; Nintendo DS; en)";

            private string nusUrl = WII_NUS_URL;
            private WebClient wcNus = new WebClient();
            private bool useLocalFiles = false;
            private bool continueWithoutTicket = false;

            private int titleversion;

            public int TitleVersion { get { return titleversion; } }

            /// <summary>
            /// If true, existing local files will be used.
            /// </summary>
            public bool UseLocalFiles { get { return useLocalFiles; } set { useLocalFiles = value; } }
            /// <summary>
            /// If true, the download will be continued even if no ticket for the title is avaiable (WAD packaging and decryption are disabled).
            /// </summary>
            public bool ContinueWithoutTicket { get { return continueWithoutTicket; } set { continueWithoutTicket = value; } }

            #region IDisposable Members
            private bool isDisposed = false;

            ~NusClient() {
                Dispose(false);
            }

            public void Dispose() {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing) {
                if (disposing && !isDisposed) {
                    wcNus.Dispose();
                }

                isDisposed = true;
            }
            #endregion

            #region Public Functions

            public void ConfigureNusClient(WebClient wcReady) {
                wcNus = wcReady;
            }

            public void SetToWiiServer() {
                nusUrl = WII_NUS_URL;
                wcNus.Headers.Add("User-Agent", WII_USER_AGENT);
            }

            public void SetToDSiServer() {
                nusUrl = DSI_NUS_URL;
                wcNus.Headers.Add("User-Agent", DSI_USER_AGENT);
            }

            /// <summary>
            /// Grabs a title from NUS, you can define several store types.
            /// Leave the title version empty for the latest.
            /// </summary>
            /// <param name="titleId"></param>
            /// <param name="titleVersion"></param>
            /// <param name="outputDir"></param>
            /// <param name="storeTypes"></param>
            public void DownloadTitle(string titleId, string titleVersion, string outputDir, string wadName, params StoreType[] storeTypes) {
                if (titleId.Length != 16) throw new Exception("Title ID must be 16 characters long!");
                downloadTitle(titleId, titleVersion, outputDir, wadName, storeTypes);
            }

            /// <summary>
            /// Grabs a TMD from NUS.
            /// Leave the title version empty for the latest.
            /// </summary>
            /// <param name="titleId"></param>
            /// <param name="titleVersion"></param>
            /// <returns></returns>
            public TMD DownloadTMD(string titleId, string titleVersion) {
                if (titleId.Length != 16) throw new Exception("Title ID must be 16 characters long!");
                return downloadTmd(titleId, titleVersion);
            }

            /// <summary>
            /// Grabs a Ticket from NUS.
            /// </summary>
            /// <param name="titleId"></param>
            /// <returns></returns>
            public Ticket DownloadTicket(string titleId) {
                if (titleId.Length != 16) throw new Exception("Title ID must be 16 characters long!");
                return downloadTicket(titleId);
            }

            /// <summary>
            /// Grabs a single content file and decrypts it.        
            /// Leave the title version empty for the latest. 
            /// </summary>
            /// <param name="titleId"></param>
            /// <param name="titleVersion"></param>
            /// <param name="contentId"></param>
            /// <returns></returns>
            public byte[] DownloadSingleContent(string titleId, string titleVersion, string contentId) {
                if (titleId.Length != 16) throw new Exception("Title ID must be 16 characters long!");
                return downloadSingleContent(titleId, titleVersion, contentId);
            }

            /// <summary>
            /// Grabs a single content file and decrypts it.        
            /// Leave the title version empty for the latest. 
            /// </summary>
            /// <param name="titleId"></param>
            /// <param name="titleVersion"></param>
            /// <param name="contentId"></param>
            /// <param name="savePath"></param>
            public void DownloadSingleContent(string titleId, string titleVersion, string contentId, string savePath) {
                if (titleId.Length != 16) throw new Exception("Title ID must be 16 characters long!");
                if (!Directory.Exists(Path.GetDirectoryName(savePath))) Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                if (File.Exists(savePath)) File.Delete(savePath);

                byte[] content = downloadSingleContent(titleId, titleVersion, contentId);
                File.WriteAllBytes(savePath, content);
            }
            #endregion

            #region Private Functions
            private byte[] downloadSingleContent(string titleId, string titleVersion, string contentId) {
                uint cId = uint.Parse(contentId, System.Globalization.NumberStyles.HexNumber);
                contentId = cId.ToString("x8");

                fireDebug("Downloading Content (Content ID: {0}) of Title {1} v{2}...", contentId, titleId, (string.IsNullOrEmpty(titleVersion)) ? "[Latest]" : titleVersion);

                fireDebug("   Checking for Internet connection...");
                if (!CheckInet()) { fireDebug("   Connection not found..."); throw new Exception("You're not connected to the internet!"); }

                fireProgress(0);

                string tmdFile = "tmd" + (string.IsNullOrEmpty(titleVersion) ? string.Empty : string.Format(".{0}", titleVersion));
                string titleUrl = string.Format("{0}{1}/", nusUrl, titleId);
                string contentIdString = string.Empty;
                int cIndex = 0;

                //Download TMD
                fireDebug("   Downloading TMD...");
                byte[] tmdArray = wcNus.DownloadData(titleUrl + tmdFile);
                fireDebug("   Parsing TMD...");
                TMD tmd = TMD.Load(tmdArray);

                fireProgress(20);

                //Search for Content ID in TMD
                fireDebug("   Looking for Content ID {0} in TMD...", contentId);
                bool foundContentId = false;
                for (int i = 0; i < tmd.Contents.Length; i++)
                    if (tmd.Contents[i].ContentID == cId) {
                        fireDebug("   Content ID {0} found in TMD...", contentId);
                        foundContentId = true;
                        contentIdString = tmd.Contents[i].ContentID.ToString("x8");
                        cIndex = i;
                        break;
                    }

                if (!foundContentId) { fireDebug("   Content ID {0} wasn't found in TMD...", contentId); throw new Exception("Content ID wasn't found in the TMD!"); }

                //Download Ticket
                fireDebug("   Downloading Ticket...");
                byte[] tikArray = wcNus.DownloadData(titleUrl + "cetk");
                fireDebug("   Parsing Ticket...");
                Ticket tik = Ticket.Load(tikArray);

                fireProgress(40);

                //Download and Decrypt Content
                fireDebug("   Downloading Content... ({0} bytes)", tmd.Contents[cIndex].Size);
                byte[] encryptedContent = wcNus.DownloadData(titleUrl + contentIdString);

                fireProgress(80);

                fireDebug("   Decrypting Content...");
                byte[] decryptedContent = decryptContent(encryptedContent, cIndex, tik, tmd);
                Array.Resize(ref decryptedContent, (int) tmd.Contents[cIndex].Size);

                //Check SHA1
                SHA1 s = SHA1.Create();
                byte[] newSha = s.ComputeHash(decryptedContent);

                if (!Shared.CompareByteArrays(newSha, tmd.Contents[cIndex].Hash)) { fireDebug(@"/!\ /!\ /!\ Hashes do not match /!\ /!\ /!\"); throw new Exception("Hashes do not match!"); }

                fireProgress(100);

                fireDebug("Downloading Content (Content ID: {0}) of Title {1} v{2} Finished...", contentId, titleId, (string.IsNullOrEmpty(titleVersion)) ? "[Latest]" : titleVersion);
                return decryptedContent;
            }

            private Ticket downloadTicket(string titleId) {
                if (!CheckInet())
                    throw new Exception("You're not connected to the internet!");

                string titleUrl = string.Format("{0}{1}/", nusUrl, titleId);
                byte[] tikArray = wcNus.DownloadData(titleUrl + "cetk");

                return Ticket.Load(tikArray);
            }

            private TMD downloadTmd(string titleId, string titleVersion) {
                if (!CheckInet())
                    throw new Exception("You're not connected to the internet!");

                string titleUrl = string.Format("{0}{1}/", nusUrl, titleId);
                string tmdFile = "tmd" + (string.IsNullOrEmpty(titleVersion) ? string.Empty : string.Format(".{0}", titleVersion));

                byte[] tmdArray = wcNus.DownloadData(titleUrl + tmdFile);

                return TMD.Load(tmdArray);
            }

            private void downloadTitle(string titleId, string titleVersion, string outputDir, string wadName, StoreType[] storeTypes) {
                fireDebug("Downloading Title {0} v{1}...", titleId, (string.IsNullOrEmpty(titleVersion)) ? "[Latest]" : titleVersion);

                if (storeTypes.Length < 1) { fireDebug("  No store types were defined..."); throw new Exception("You must at least define one store type!"); }

                string titleUrl = string.Format("{0}{1}/", nusUrl, titleId);
                bool storeEncrypted = true;
                bool storeDecrypted = false;
                bool storeWad = false;

                fireProgress(0);

                fireDebug("    [=] Storing Encrypted Content...");

                if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);
                if (!Directory.Exists(Path.Combine(outputDir, titleId))) Directory.CreateDirectory(Path.Combine(outputDir, titleId));
                outputDir = Path.Combine(outputDir, titleId);

                string tmdFile = "tmd" + (string.IsNullOrEmpty(titleVersion) ? string.Empty : string.Format(".{0}", titleVersion));

                //Download TMD
                fireDebug("  - Downloading TMD...");

                wcNus.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wcNus_DownloadProgressChanged);
                wcNus.DownloadFileCompleted += new AsyncCompletedEventHandler(wcNus_DownloadFileCompleted);

                TMD tmd;
                try {
                    wcNus.DownloadFile(titleUrl + tmdFile, Path.Combine(outputDir, tmdFile));
                    tmd = TMD.Load(File.ReadAllBytes(Path.Combine(outputDir, tmdFile)));
                } catch (Exception ex) { fireDebug("   + Downloading TMD Failed..."); throw new Exception("Downloading TMD Failed:\n" + ex.Message); }

                //Parse TMD
                fireDebug("  - Parsing TMD...");

                if (string.IsNullOrEmpty(titleVersion)) { fireDebug("    + Title Version: {0}", tmd.TitleVersion); }
                fireDebug("    + {0} Contents", tmd.NumOfContents);

                if (!Directory.Exists(Path.Combine(outputDir, tmd.TitleVersion.ToString()))) Directory.CreateDirectory(Path.Combine(outputDir, tmd.TitleVersion.ToString()));
                outputDir = Path.Combine(outputDir, tmd.TitleVersion.ToString());

                if (File.Exists(Path.Combine(outputDir, tmdFile))) File.Delete(Path.Combine(outputDir, tmdFile));
                File.Move(Path.Combine(Path.GetDirectoryName(outputDir), tmdFile), Path.Combine(outputDir, tmdFile));

                this.titleversion = tmd.TitleVersion;

                bytestotal = 0;
                for (int i = 0; i < tmd.NumOfContents; i++) {
                    bytestotal += tmd.Contents[i].Size;
                }
                fireDebug("  - " + tmd.NumOfContents + " files, " + bytestotal + " bytes.");

                /*TMD tmd;
                byte[] tmdFileWithCerts;
                try {
                    tmdFileWithCerts = wcNus.DownloadData(titleUrl + tmdFile);
                    tmd = TMD.Load(tmdFileWithCerts);
                } catch (Exception ex) { fireDebug("   + Downloading TMD Failed..."); throw new Exception("Downloading TMD Failed:\n" + ex.Message); }

                //Parse TMD
                fireDebug("  - Parsing TMD...");

                if (string.IsNullOrEmpty(titleVersion)) { fireDebug("    + Title Version: {0}", tmd.TitleVersion); }
                fireDebug("    + {0} Contents", tmd.NumOfContents);

                if (!Directory.Exists(Path.Combine(outputDir, tmd.TitleVersion.ToString()))) Directory.CreateDirectory(Path.Combine(outputDir, tmd.TitleVersion.ToString()));
                outputDir = Path.Combine(outputDir, tmd.TitleVersion.ToString());

                this.titleversion = tmd.TitleVersion;

                File.WriteAllBytes(Path.Combine(outputDir, tmdFile), tmd.ToByteArray());//*/

                fireProgress(5);

                if (System.IO.File.Exists(Path.Combine(outputDir, "cetk"))) System.IO.File.Move(Path.Combine(outputDir, "cetk"), Path.Combine(outputDir, "cetk_old"));

                //Download cetk
                fireDebug("  - Downloading Ticket...");
                try {
                    wcNus.DownloadFile(Path.Combine(titleUrl, "cetk"), Path.Combine(outputDir, "cetk"));
                } catch (Exception ex) {
                    if (!continueWithoutTicket || !storeEncrypted) {
                        fireDebug("   + Downloading Ticket Failed...");
                        throw new Exception("Downloading Ticket Failed:\n" + ex.Message);
                    }

                    if (!(File.Exists(Path.Combine(outputDir, "cetk")))) {
                        storeDecrypted = false;
                        storeWad = false;
                    }
                }

                fireProgress(10);

                // Parse Ticket
                Ticket tik = new Ticket();

                if (File.Exists(Path.Combine(outputDir, "cetk"))) {
                    fireDebug("   + Parsing Ticket...");
                    tik = Ticket.Load(Path.Combine(outputDir, "cetk"));

                    // DSi ticket? Must make sure to use DSi Key :D
                    if (nusUrl == DSI_NUS_URL)
                        tik.DSiTicket = true;
                } else {
                    fireDebug("   + Ticket Unavailable...");
                }

                string[] encryptedContents = new string[tmd.NumOfContents];

                bytesdone_total = 0;

                //return;

                //Download Content
                for (int i = 0; i < tmd.NumOfContents; i++) {
                    fireDebug("  - Downloading Content #{0} of {1}... ({2} bytes) to {3}", i + 1, tmd.NumOfContents, tmd.Contents[i].Size, tmd.Contents[i].ContentID.ToString("x8"));
                    pbs.Value = 0;

                    if (useLocalFiles && File.Exists(Path.Combine(outputDir, tmd.Contents[i].ContentID.ToString("x8")))) {
                        fireDebug("   + Using Local File, Skipping...");
                        bytesdone_total += tmd.Contents[i].Size;
                        pbt.Value = (int)(bytesdone_total*(ulong)pbt.Maximum/bytestotal);
                        pbs.Value = pbs.Maximum;
                        continue;
                    }

                    try {
                        isdownloading = true;
                        wcNus.DownloadFileAsync(new Uri(titleUrl + tmd.Contents[i].ContentID.ToString("x8")), Path.Combine(outputDir, tmd.Contents[i].ContentID.ToString("x8")));

                        while (isdownloading) {
                            if (cancelnusd) {
                                wcNus.CancelAsync();
                                pbs.Value = 0;
                                pbt.Value = 0;
                                while (isdownloading) { }
                                System.IO.File.Delete(Path.Combine(outputDir, tmd.Contents[i].ContentID.ToString("x8")));
                                throw new Exception("The download operation has been canceled.");
                            }
                        }
                        bytesdone_total += tmd.Contents[i].Size;
                        pbt.Value = (int) (bytesdone_total*(ulong)pbt.Maximum/bytestotal);

                        encryptedContents[i] = tmd.Contents[i].ContentID.ToString("x8");
                    } catch (Exception ex) { fireDebug("  - Downloading Content #{0} of {1} failed...", i + 1, tmd.NumOfContents); throw new Exception("Downloading Content Failed:\n" + ex.Message); }
                }
            }

            public System.Windows.Forms.ProgressBar pbs, pbt;
            public bool isdownloading;
            public bool cancelnusd;
            private ulong bytesdone_current = 0;
            private ulong bytesdone_total = 0;
            private ulong bytestotal = 0;

            private void wcNus_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
                ulong pv = (((ulong) e.BytesReceived + bytesdone_total)*(ulong) pbt.Maximum/bytestotal);
                if ((int)pv > pbt.Maximum) return;
                pbs.Value = (int) (e.BytesReceived*pbs.Maximum / e.TotalBytesToReceive);
                pbt.Value = (int)pv;
            }

            private void wcNus_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
                isdownloading = false;
            }

            private byte[] decryptContent(byte[] content, int contentIndex, Ticket tik, TMD tmd) {
                Array.Resize(ref content, Shared.AddPadding(content.Length, 16));
                byte[] titleKey = tik.TitleKey;
                byte[] iv = new byte[16];

                byte[] tmp = BitConverter.GetBytes(tmd.Contents[contentIndex].Index);
                iv[0] = tmp[1];
                iv[1] = tmp[0];

                RijndaelManaged rm = new RijndaelManaged();
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.None;
                rm.KeySize = 128;
                rm.BlockSize = 128;
                rm.Key = titleKey;
                rm.IV = iv;

                ICryptoTransform decryptor = rm.CreateDecryptor();

                MemoryStream ms = new MemoryStream(content);
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

                byte[] decCont = new byte[content.Length];
                cs.Read(decCont, 0, decCont.Length);

                cs.Dispose();
                ms.Dispose();

                return decCont;
            }

            private bool CheckInet() {
                try {
                    System.Net.IPHostEntry ipHost = System.Net.Dns.GetHostEntry("www.google.com");
                    return true;
                } catch { return false; }
            }
            #endregion

            #region Events
            /// <summary>
            /// Fires the Progress of various operations
            /// </summary>
            public event EventHandler<ProgressChangedEventArgs> Progress;
            /// <summary>
            /// Fires debugging messages. You may write them into a log file or log textbox.
            /// </summary>
            public event EventHandler<MessageEventArgs> Debug;

            private void fireDebug(string debugMessage, params object[] args) {
                EventHandler<MessageEventArgs> debug = Debug;
                if (debug != null)
                    debug(new object(), new MessageEventArgs(string.Format(debugMessage, args)));
            }

            private void fireProgress(int progressPercentage) {
                EventHandler<ProgressChangedEventArgs> progress = Progress;
                if (progress != null)
                    progress(new object(), new ProgressChangedEventArgs(progressPercentage, string.Empty));
            }
            #endregion
        }

        public class MessageEventArgs:EventArgs {
            private string message;
            public string Message { get { return message; } }

            public MessageEventArgs(string message) { this.message = message; }
        }


        public enum ContentType:ushort {
            Normal=0x0001,
            DLC=0x4001, //Seen this in a DLC wad...
            Shared=0x8001,
        }

        public enum Region:ushort {
            Japan=0,
            USA=1,
            Europe=2,
            Free=3,
        }

        public class TMD:IDisposable {
            private bool fakeSign = false;

            private uint signatureExponent = 0x00010001;
            private byte[] signature = new byte[256];
            private byte[] padding = new byte[60];
            private byte[] issuer = new byte[64];
            private byte version;
            private byte caCrlVersion;
            private byte signerCrlVersion;
            private byte paddingByte;
            private ulong startupIos;
            private ulong titleId;
            private uint titleType;
            private ushort groupId;
            private ushort padding2;
            private ushort region;
            private byte[] reserved = new byte[58];
            private uint accessRights;
            private ushort titleVersion;
            private ushort numOfContents;
            private ushort bootIndex;
            private ushort padding3;
            private List<TMD_Content> contents;

            /// <summary>
            /// The region of the title.
            /// </summary>
            public Region Region { get { return (Region) region; } set { region = (ushort) value; } }
            /// <summary>
            /// The IOS the title is launched with.
            /// </summary>
            public ulong StartupIOS { get { return startupIos; } set { startupIos = value; } }
            /// <summary>
            /// The Title ID.
            /// </summary>
            public ulong TitleID { get { return titleId; } set { titleId = value; } }
            /// <summary>
            /// The Title Version.
            /// </summary>
            public ushort TitleVersion { get { return titleVersion; } set { titleVersion = value; } }
            /// <summary>
            /// The Number of Contents.
            /// </summary>
            public ushort NumOfContents { get { return numOfContents; } }
            /// <summary>
            /// The boot index. Represents the index of the nand loader.
            /// </summary>
            public ushort BootIndex { get { return bootIndex; } set { if (value <= numOfContents) bootIndex = value; } }
            /// <summary>
            /// The content descriptions in the TMD.
            /// </summary>
            public TMD_Content[] Contents { get { return contents.ToArray(); } set { contents = new List<TMD_Content>(value); numOfContents = (ushort) value.Length; } }
            /// <summary>
            /// If true, the TMD will be fakesigned while saving.
            /// </summary>
            public bool FakeSign { get { return fakeSign; } set { fakeSign = value; } }

            #region IDisposable Members
            private bool isDisposed = false;

            ~TMD() {
                Dispose(false);
            }

            public void Dispose() {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing) {
                if (disposing && !isDisposed) {
                    signature = null;
                    padding = null;
                    issuer = null;
                    reserved = null;

                    contents.Clear();
                    contents = null;
                }

                isDisposed = true;
            }
            #endregion

            #region Public Functions
            /// <summary>
            /// Loads a tmd file.
            /// </summary>
            /// <param name="pathToTmd"></param>
            /// <returns></returns>
            public static TMD Load(string pathToTmd) {
                return Load(File.ReadAllBytes(pathToTmd));
            }

            /// <summary>
            /// Loads a tmd file.
            /// </summary>
            /// <param name="tmdFile"></param>
            /// <returns></returns>
            public static TMD Load(byte[] tmdFile) {
                TMD t = new TMD();
                MemoryStream ms = new MemoryStream(tmdFile);

                try { t.parseTmd(ms); } catch { ms.Dispose(); throw; }

                ms.Dispose();
                return t;
            }

            /// <summary>
            /// Loads a tmd file.
            /// </summary>
            /// <param name="tmd"></param>
            /// <returns></returns>
            public static TMD Load(Stream tmd) {
                TMD t = new TMD();
                t.parseTmd(tmd);
                return t;
            }



            /// <summary>
            /// Loads a tmd file.
            /// </summary>
            /// <param name="pathToTmd"></param>
            public void LoadFile(string pathToTmd) {
                LoadFile(File.ReadAllBytes(pathToTmd));
            }

            /// <summary>
            /// Loads a tmd file.
            /// </summary>
            /// <param name="tmdFile"></param>
            public void LoadFile(byte[] tmdFile) {
                MemoryStream ms = new MemoryStream(tmdFile);

                try { parseTmd(ms); } catch { ms.Dispose(); throw; }

                ms.Dispose();
            }

            /// <summary>
            /// Loads a tmd file.
            /// </summary>
            /// <param name="tmd"></param>
            public void LoadFile(Stream tmd) {
                parseTmd(tmd);
            }



            /// <summary>
            /// Saves the TMD.
            /// </summary>
            /// <param name="savePath"></param>
            public void Save(string savePath) {
                Save(savePath, false);
            }

            /// <summary>
            /// Saves the TMD. If fakeSign is true, the Ticket will be fakesigned.
            /// </summary>
            /// <param name="savePath"></param>
            /// <param name="fakeSign"></param>
            public void Save(string savePath, bool fakeSign) {
                if (fakeSign) this.fakeSign = true;
                if (File.Exists(savePath)) File.Delete(savePath);

                using (FileStream fs = new FileStream(savePath, FileMode.Create))
                    writeToStream(fs);
            }

            /// <summary>
            /// Returns the TMD as a memory stream.
            /// </summary>
            /// <returns></returns>
            public MemoryStream ToMemoryStream() {
                return ToMemoryStream(false);
            }

            /// <summary>
            /// Returns the TMD as a memory stream. If fakeSign is true, the Ticket will be fakesigned.
            /// </summary>
            /// <param name="fakeSign"></param>
            /// <returns></returns>
            public MemoryStream ToMemoryStream(bool fakeSign) {
                if (fakeSign) this.fakeSign = true;
                MemoryStream ms = new MemoryStream();

                try { writeToStream(ms); } catch { ms.Dispose(); throw; }

                return ms;
            }

            /// <summary>
            /// Returns the TMD as a byte array.
            /// </summary>
            /// <returns></returns>
            public byte[] ToByteArray() {
                return ToByteArray(false);
            }

            /// <summary>
            /// Returns the TMD as a byte array. If fakeSign is true, the Ticket will be fakesigned.
            /// </summary>
            /// <param name="fakeSign"></param>
            /// <returns></returns>
            public byte[] ToByteArray(bool fakeSign) {
                if (fakeSign) this.fakeSign = true;
                MemoryStream ms = new MemoryStream();

                try { writeToStream(ms); } catch { ms.Dispose(); throw; }

                byte[] res = ms.ToArray();
                ms.Dispose();
                return res;
            }

            /// <summary>
            /// Updates the content entries.
            /// </summary>
            /// <param name="contentDir"></param>
            /// <param name="namedContentId">True if you use the content ID as name (e.g. 0000008a.app).
            /// False if you use the index as name (e.g. 00000000.app)</param>
            public void UpdateContents(string contentDir) {
                bool namedContentId = true;
                for (int i = 0; i < contents.Count; i++)
                    if (!File.Exists(contentDir + Path.DirectorySeparatorChar + contents[i].ContentID.ToString("x8") + ".app")) { namedContentId = false; break; }

                if (!namedContentId)
                    for (int i = 0; i < contents.Count; i++)
                        if (!File.Exists(contentDir + Path.DirectorySeparatorChar + contents[i].ContentID.ToString("x8") + ".app"))
                            throw new Exception("Couldn't find all content files!");

                byte[][] conts = new byte[contents.Count][];

                for (int i = 0; i < contents.Count; i++) {
                    string file = contentDir + Path.DirectorySeparatorChar + ((namedContentId) ? contents[i].ContentID.ToString("x8") : contents[i].Index.ToString("x8")) + ".app";
                    conts[i] = File.ReadAllBytes(file);
                }

                updateContents(conts);
            }

            /// <summary>
            /// Updates the content entries.
            /// </summary>
            /// <param name="contentDir"></param>
            /// <param name="namedContentId">True if you use the content ID as name (e.g. 0000008a.app).
            /// False if you use the index as name (e.g. 00000000.app)</param>
            public void UpdateContents(byte[][] contents) {
                updateContents(contents);
            }

            /// <summary>
            /// Returns the Upper Title ID as a string.
            /// </summary>
            /// <returns></returns>
            public string GetUpperTitleID() {
                byte[] titleBytes = BitConverter.GetBytes(Shared.Swap((uint) titleId));
                return new string(new char[] { (char) titleBytes[0], (char) titleBytes[1], (char) titleBytes[2], (char) titleBytes[3] });
            }

            /// <summary>
            /// The Number of memory blocks the content will take.
            /// </summary>
            /// <returns></returns>
            public string GetNandBlocks() {
                return calculateNandBlocks();
            }

            /// <summary>
            /// Adds a TMD content.
            /// </summary>
            /// <param name="content"></param>
            public void AddContent(TMD_Content content) {
                contents.Add(content);

                numOfContents = (ushort) contents.Count;
            }

            /// <summary>
            /// Removes the content with the given index.
            /// </summary>
            /// <param name="contentIndex"></param>
            public void RemoveContent(int contentIndex) {
                for (int i = 0; i < numOfContents; i++)
                    if (contents[i].Index == contentIndex) { contents.RemoveAt(i); break; }

                numOfContents = (ushort) contents.Count;
            }

            /// <summary>
            /// Removes the content with the given ID.
            /// </summary>
            /// <param name="contentId"></param>
            public void RemoveContentByID(int contentId) {
                for (int i = 0; i < numOfContents; i++)
                    if (contents[i].ContentID == contentId) { contents.RemoveAt(i); break; }

                numOfContents = (ushort) contents.Count;
            }
            #endregion

            #region Private Functions
            private void writeToStream(Stream writeStream) {
                fireDebug("Writing TMD...");

                if (fakeSign) { fireDebug("   Clearing Signature..."); signature = new byte[256]; } //Clear Signature if we fake Sign

                MemoryStream ms = new MemoryStream();
                ms.Seek(0, SeekOrigin.Begin);

                fireDebug("   Writing Signature Exponent... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(signatureExponent)), 0, 4);

                fireDebug("   Writing Signature... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(signature, 0, signature.Length);

                fireDebug("   Writing Padding... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(padding, 0, padding.Length);

                fireDebug("   Writing Issuer... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(issuer, 0, issuer.Length);

                fireDebug("   Writing Version... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.WriteByte(version);

                fireDebug("   Writing CA Crl Version... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.WriteByte(caCrlVersion);

                fireDebug("   Writing Signer Crl Version... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.WriteByte(signerCrlVersion);

                fireDebug("   Writing Padding Byte... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.WriteByte(paddingByte);

                fireDebug("   Writing Startup IOS... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(startupIos)), 0, 8);

                fireDebug("   Writing Title ID... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(titleId)), 0, 8);

                fireDebug("   Writing Title Type... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(titleType)), 0, 4);

                fireDebug("   Writing Group ID... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(groupId)), 0, 2);

                fireDebug("   Writing Padding2... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(padding2)), 0, 2);

                fireDebug("   Writing Region... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(region)), 0, 2);

                fireDebug("   Writing Reserved... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(reserved, 0, reserved.Length);

                fireDebug("   Writing Access Rights... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(accessRights)), 0, 4);

                fireDebug("   Writing Title Version... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(titleVersion)), 0, 2);

                fireDebug("   Writing NumOfContents... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(numOfContents)), 0, 2);

                fireDebug("   Writing Boot Index... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(bootIndex)), 0, 2);

                fireDebug("   Writing Padding3... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(padding3)), 0, 2);

                //Write Contents
                List<ContentIndices> contentList = new List<ContentIndices>();
                for (int i = 0; i < contents.Count; i++)
                    contentList.Add(new ContentIndices(i, contents[i].Index));

                contentList.Sort();

                for (int i = 0; i < contentList.Count; i++) {
                    fireDebug("   Writing Content #{1} of {2}... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper().ToUpper(), i + 1, numOfContents);

                    ms.Write(BitConverter.GetBytes(Shared.Swap(contents[contentList[i].Index].ContentID)), 0, 4);
                    ms.Write(BitConverter.GetBytes(Shared.Swap(contents[contentList[i].Index].Index)), 0, 2);
                    ms.Write(BitConverter.GetBytes(Shared.Swap((ushort) contents[contentList[i].Index].Type)), 0, 2);
                    ms.Write(BitConverter.GetBytes(Shared.Swap(contents[contentList[i].Index].Size)), 0, 8);

                    ms.Write(contents[contentList[i].Index].Hash, 0, contents[contentList[i].Index].Hash.Length);
                }

                //fake Sign
                byte[] tmd = ms.ToArray();
                ms.Dispose();

                if (fakeSign) {
                    fireDebug("   Fakesigning TMD...");

                    byte[] hash = new byte[20];
                    SHA1 s = SHA1.Create();

                    for (ushort i = 0; i < 0xFFFF; i++) {
                        byte[] bytes = BitConverter.GetBytes(i);
                        tmd[482] = bytes[1]; tmd[483] = bytes[0];

                        hash = s.ComputeHash(tmd);
                        if (hash[0] == 0x00) { fireDebug("   -> Signed ({0})", i); break; } //Win! It's signed...

                        if (i == 0xFFFF - 1) { fireDebug("    -> Signing Failed..."); throw new Exception("Fakesigning failed..."); }
                    }

                    s.Clear();
                }

                writeStream.Seek(0, SeekOrigin.Begin);
                writeStream.Write(tmd, 0, tmd.Length);

                fireDebug("Writing TMD Finished...");
            }

            private void updateContents(byte[][] conts) {
                SHA1 s = SHA1.Create();

                for (int i = 0; i < contents.Count; i++) {
                    contents[i].Size = (ulong) conts[i].Length;
                    contents[i].Hash = s.ComputeHash(conts[i]);
                }

                s.Clear();
            }

            private void parseTmd(Stream tmdFile) {
                fireDebug("Pasing TMD...");

                tmdFile.Seek(0, SeekOrigin.Begin);
                byte[] temp = new byte[8];

                fireDebug("   Reading Signature Exponent... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 4);
                signatureExponent = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                fireDebug("   Reading Signature... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(signature, 0, signature.Length);

                fireDebug("   Reading Padding... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(padding, 0, padding.Length);

                fireDebug("   Reading Issuer... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(issuer, 0, issuer.Length);

                fireDebug("   Reading Version... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading CA Crl Version... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading Signer Crl Version... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading Padding Byte... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 4);
                version = temp[0];
                caCrlVersion = temp[1];
                signerCrlVersion = temp[2];
                paddingByte = temp[3];

                fireDebug("   Reading Startup IOS... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 8);
                startupIos = Shared.Swap(BitConverter.ToUInt64(temp, 0));

                fireDebug("   Reading Title ID... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 8);
                titleId = Shared.Swap(BitConverter.ToUInt64(temp, 0));

                fireDebug("   Reading Title Type... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 4);
                titleType = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                fireDebug("   Reading Group ID... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 2);
                groupId = Shared.Swap(BitConverter.ToUInt16(temp, 0));

                fireDebug("   Reading Padding2... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 2);
                padding2 = Shared.Swap(BitConverter.ToUInt16(temp, 0));

                fireDebug("   Reading Region... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 2);
                region = Shared.Swap(BitConverter.ToUInt16(temp, 0));

                fireDebug("   Reading Reserved... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(reserved, 0, reserved.Length);

                fireDebug("   Reading Access Rights... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 4);
                accessRights = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                fireDebug("   Reading Title Version... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading NumOfContents... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading Boot Index... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading Padding3... (Offset: 0x{0})", tmdFile.Position.ToString("x8").ToUpper());
                tmdFile.Read(temp, 0, 8);
                titleVersion = Shared.Swap(BitConverter.ToUInt16(temp, 0));
                numOfContents = Shared.Swap(BitConverter.ToUInt16(temp, 2));
                bootIndex = Shared.Swap(BitConverter.ToUInt16(temp, 4));
                padding3 = Shared.Swap(BitConverter.ToUInt16(temp, 6));
                tmdFile.Position = 0xb04;

                contents = new List<TMD_Content>();

                //Read Contents
                for (int i = 0; i < numOfContents; i++) {
                    fireDebug("   Reading Content #{0} of {1}... (Offset: 0x{2})", i + 1, numOfContents, tmdFile.Position.ToString("x8").ToUpper().ToUpper());

                    TMD_Content tempContent = new TMD_Content();
                    tempContent.Hash = new byte[20];

                    tmdFile.Read(temp, 0, 8);
                    tempContent.ContentID = Shared.Swap(BitConverter.ToUInt32(temp, 0));
                    tempContent.Index = Shared.Swap(BitConverter.ToUInt16(temp, 4));
                    tempContent.Type = (ContentType) Shared.Swap(BitConverter.ToUInt16(temp, 6));

                    tmdFile.Read(temp, 0, 8);
                    tempContent.Size = Shared.Swap(BitConverter.ToUInt64(temp, 0));

                    tmdFile.Read(tempContent.Hash, 0, tempContent.Hash.Length);

                    contents.Add(tempContent);
                    byte[] paddingcontent = new byte[12];
                    tmdFile.Read(paddingcontent, 0, 12);
                }

                fireDebug("Pasing TMD Finished...");
            }

            private string calculateNandBlocks() {
                int nandSizeMin = 0;
                int nandSizeMax = 0;

                for (int i = 0; i < numOfContents; i++) {
                    nandSizeMax += (int) contents[i].Size;
                    if (contents[i].Type == ContentType.Normal) nandSizeMin += (int) contents[i].Size;
                }

                int blocksMin = (int) Math.Ceiling((double) ((double) nandSizeMin / (128 * 1024)));
                int blocksMax = (int) Math.Ceiling((double) ((double) nandSizeMax / (128 * 1024)));

                if (blocksMin == blocksMax) return blocksMax.ToString();
                else return string.Format("{0} - {1}", blocksMin, blocksMax);
            }
            #endregion

            #region Events
            /// <summary>
            /// Fires debugging messages. You may write them into a log file or log textbox.
            /// </summary>
            public event EventHandler<MessageEventArgs> Debug;

            private void fireDebug(string debugMessage, params object[] args) {
                EventHandler<MessageEventArgs> debug = Debug;
                if (debug != null)
                    debug(new object(), new MessageEventArgs(string.Format(debugMessage, args)));
            }
            #endregion
        }

        public class TMD_Content {
            private uint contentId;
            private ushort index;
            private ushort type;
            private ulong size;
            private byte[] hash = new byte[20];

            public uint ContentID { get { return contentId; } set { contentId = value; } }
            public ushort Index { get { return index; } set { index = value; } }
            public ContentType Type { get { return (ContentType) type; } set { type = (ushort) value; } }
            public ulong Size { get { return size; } set { size = value; } }
            public byte[] Hash { get { return hash; } set { hash = value; } }
        }
        public enum CommonKeyType:byte {
            Standard=0x00,
            Korean=0x01,
        }

        public class Ticket:IDisposable {
            private byte newKeyIndex = (byte) CommonKeyType.Standard;
            private byte[] decryptedTitleKey = new byte[16];
            private bool fakeSign = false;
            private bool titleKeyChanged = false;
            private byte[] newEncryptedTitleKey = new byte[0];
            private bool reDecrypt = false;

            private uint signatureExponent = 0x00010001;
            private byte[] signature = new byte[256];
            private byte[] padding = new byte[60];
            private byte[] issuer = new byte[64];
            private byte[] unknown = new byte[63];
            private byte[] encryptedTitleKey = new byte[16];
            private byte unknown2;
            private ulong ticketId;
            private uint consoleId;
            private ulong titleId;
            private ushort unknown3 = 0xFFFF;
            private ushort numOfDlc;
            private ulong unknown4;
            private byte padding2;
            private byte commonKeyIndex = (byte) CommonKeyType.Standard;
            private byte[] unknown5 = new byte[48];
            private byte[] unknown6 = new byte[32]; //0xFF
            private ushort padding3;
            private uint enableTimeLimit;
            private uint timeLimit;
            private byte[] padding4 = new byte[88];

            private bool dsitik = false;

            /// <summary>
            /// The Title Key the WADs content is encrypted with.
            /// </summary>
            public byte[] TitleKey { get { return decryptedTitleKey; } set { decryptedTitleKey = value; titleKeyChanged = true; reDecrypt = false; } }
            /// <summary>
            /// Defines which Common Key is used (Standard / Korean).
            /// </summary>
            public CommonKeyType CommonKeyIndex { get { return (CommonKeyType) newKeyIndex; } set { newKeyIndex = (byte) value; } }
            /// <summary>
            /// The Ticket ID.
            /// </summary>
            public ulong TicketID { get { return ticketId; } set { ticketId = value; } }
            /// <summary>
            /// The Console ID.
            /// </summary>
            public uint ConsoleID { get { return consoleId; } set { consoleId = value; } }
            /// <summary>
            /// The Title ID.
            /// </summary>
            public ulong TitleID { get { return titleId; } set { titleId = value; if (reDecrypt) reDecryptTitleKey(); } }
            /// <summary>
            /// Number of DLC.
            /// </summary>
            public ushort NumOfDLC { get { return numOfDlc; } set { numOfDlc = value; } }
            /// <summary>
            /// If true, the Ticket will be fakesigned while saving.
            /// </summary>
            public bool FakeSign { get { return fakeSign; } set { fakeSign = value; } }
            /// <summary>
            /// True if the Title Key was changed.
            /// </summary>
            public bool TitleKeyChanged { get { return titleKeyChanged; } }

            /// <summary>
            /// If true, the Ticket will utilize the DSi CommonKey.
            /// </summary>
            public bool DSiTicket { get { return dsitik; } set { dsitik = value; decryptTitleKey(); } }

            #region IDisposable Members
            private bool isDisposed = false;

            ~Ticket() {
                Dispose(false);
            }

            public void Dispose() {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing) {
                if (disposing && !isDisposed) {
                    decryptedTitleKey = null;
                    newEncryptedTitleKey = null;
                    signature = null;
                    padding = null;
                    issuer = null;
                    unknown = null;
                    encryptedTitleKey = null;
                    unknown5 = null;
                    unknown6 = null;
                    padding4 = null;
                }

                isDisposed = true;
            }
            #endregion

            #region Public Functions
            /// <summary>
            /// Loads a tik file.
            /// </summary>
            /// <param name="pathToTicket"></param>
            /// <returns></returns>
            public static Ticket Load(string pathToTicket) {
                return Load(File.ReadAllBytes(pathToTicket));
            }

            /// <summary>
            /// Loads a tik file.
            /// </summary>
            /// <param name="ticket"></param>
            /// <returns></returns>
            public static Ticket Load(byte[] ticket) {
                Ticket tik = new Ticket();
                MemoryStream ms = new MemoryStream(ticket);

                try { tik.parseTicket(ms); } catch { ms.Dispose(); throw; }

                ms.Dispose();
                return tik;
            }

            /// <summary>
            /// Loads a tik file.
            /// </summary>
            /// <param name="ticket"></param>
            /// <returns></returns>
            public static Ticket Load(Stream ticket) {
                Ticket tik = new Ticket();
                tik.parseTicket(ticket);
                return tik;
            }



            /// <summary>
            /// Loads a tik file.
            /// </summary>
            /// <param name="pathToTicket"></param>
            public void LoadFile(string pathToTicket) {
                LoadFile(File.ReadAllBytes(pathToTicket));
            }

            /// <summary>
            /// Loads a tik file.
            /// </summary>
            /// <param name="ticket"></param>
            public void LoadFile(byte[] ticket) {
                MemoryStream ms = new MemoryStream(ticket);

                try { parseTicket(ms); } catch { ms.Dispose(); throw; }

                ms.Dispose();
            }

            /// <summary>
            /// Loads a tik file.
            /// </summary>
            /// <param name="ticket"></param>
            public void LoadFile(Stream ticket) {
                parseTicket(ticket);
            }



            /// <summary>
            /// Saves the Ticket.
            /// </summary>
            /// <param name="savePath"></param>
            public void Save(string savePath) {
                Save(savePath, false);
            }

            /// <summary>
            /// Saves the Ticket. If fakeSign is true, the Ticket will be fakesigned.
            /// </summary>
            /// <param name="savePath"></param>
            /// <param name="fakeSign"></param>
            public void Save(string savePath, bool fakeSign) {
                if (fakeSign) this.fakeSign = true;
                if (File.Exists(savePath)) File.Delete(savePath);

                using (FileStream fs = new FileStream(savePath, FileMode.Create))
                    writeToStream(fs);
            }

            /// <summary>
            /// Returns the Ticket as a memory stream.
            /// </summary>
            /// <returns></returns>
            public MemoryStream ToMemoryStream() {
                return ToMemoryStream(false);
            }

            /// <summary>
            /// Returns the Ticket as a memory stream. If fakeSign is true, the Ticket will be fakesigned.
            /// </summary>
            /// <param name="fakeSign"></param>
            /// <returns></returns>
            public MemoryStream ToMemoryStream(bool fakeSign) {
                if (fakeSign) this.fakeSign = true;
                MemoryStream ms = new MemoryStream();

                try { writeToStream(ms); } catch { ms.Dispose(); throw; }

                return ms;
            }

            /// <summary>
            /// Returns the Ticket as a byte array.
            /// </summary>
            /// <returns></returns>
            public byte[] ToByteArray() {
                return ToByteArray(false);
            }

            /// <summary>
            /// Returns the Ticket as a byte array. If fakeSign is true, the Ticket will be fakesigned.
            /// </summary>
            /// <param name="fakeSign"></param>
            /// <returns></returns>
            public byte[] ToByteArray(bool fakeSign) {
                if (fakeSign) this.fakeSign = true;
                MemoryStream ms = new MemoryStream();

                try { writeToStream(ms); } catch { ms.Dispose(); throw; }

                byte[] res = ms.ToArray();
                ms.Dispose();
                return res;
            }

            /// <summary>
            /// This will set a new encrypted Title Key (i.e. the one that you can "read" in the Ticket).
            /// </summary>
            /// <param name="newTitleKey"></param>
            public void SetTitleKey(string newTitleKey) {
                SetTitleKey(newTitleKey.ToCharArray());
            }

            /// <summary>
            /// This will set a new encrypted Title Key (i.e. the one that you can "read" in the Ticket).
            /// </summary>
            /// <param name="newTitleKey"></param>
            public void SetTitleKey(char[] newTitleKey) {
                if (newTitleKey.Length != 16)
                    throw new Exception("The title key must be 16 characters long!");

                for (int i = 0; i < 16; i++)
                    encryptedTitleKey[i] = (byte) newTitleKey[i];

                decryptTitleKey();
                titleKeyChanged = true;

                reDecrypt = true;
                newEncryptedTitleKey = encryptedTitleKey;
            }

            /// <summary>
            /// This will set a new encrypted Title Key (i.e. the one that you can "read" in the Ticket).
            /// </summary>
            /// <param name="newTitleKey"></param>
            public void SetTitleKey(byte[] newTitleKey) {
                if (newTitleKey.Length != 16)
                    throw new Exception("The title key must be 16 characters long!");

                encryptedTitleKey = newTitleKey;
                decryptTitleKey();
                titleKeyChanged = true;

                reDecrypt = true;
                newEncryptedTitleKey = newTitleKey;
            }

            /// <summary>
            /// Returns the Upper Title ID as a string.
            /// </summary>
            /// <returns></returns>
            public string GetUpperTitleID() {
                byte[] titleBytes = BitConverter.GetBytes(Shared.Swap((uint) titleId));
                return new string(new char[] { (char) titleBytes[0], (char) titleBytes[1], (char) titleBytes[2], (char) titleBytes[3] });
            }
            #endregion

            #region Private Functions
            private void writeToStream(Stream writeStream) {
                fireDebug("Writing Ticket...");

                fireDebug("   Encrypting Title Key...");
                encryptTitleKey();
                fireDebug("    -> Decrypted Title Key: {0}", Shared.ByteArrayToString(decryptedTitleKey,' '));
                fireDebug("    -> Encrypted Title Key: {0}", Shared.ByteArrayToString(encryptedTitleKey,' '));

                if (fakeSign) { fireDebug("   Clearing Signature..."); signature = new byte[256]; } //Clear Signature if we fake Sign

                MemoryStream ms = new MemoryStream();
                ms.Seek(0, SeekOrigin.Begin);

                fireDebug("   Writing Signature Exponent... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(signatureExponent)), 0, 4);

                fireDebug("   Writing Signature... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(signature, 0, signature.Length);

                fireDebug("   Writing Padding... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(padding, 0, padding.Length);

                fireDebug("   Writing Issuer... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(issuer, 0, issuer.Length);

                fireDebug("   Writing Unknown... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(unknown, 0, unknown.Length);

                fireDebug("   Writing Title Key... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(encryptedTitleKey, 0, encryptedTitleKey.Length);

                fireDebug("   Writing Unknown2... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.WriteByte(unknown2);

                fireDebug("   Writing Ticket ID... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(ticketId)), 0, 8);

                fireDebug("   Writing Console ID... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(consoleId)), 0, 4);

                fireDebug("   Writing Title ID... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(titleId)), 0, 8);

                fireDebug("   Writing Unknwon3... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(unknown3)), 0, 2);

                fireDebug("   Writing NumOfDLC... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(numOfDlc)), 0, 2);

                fireDebug("   Writing Unknwon4... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(unknown4)), 0, 8);

                fireDebug("   Writing Padding2... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.WriteByte(padding2);

                fireDebug("   Writing Common Key Index... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.WriteByte(commonKeyIndex);

                fireDebug("   Writing Unknown5... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(unknown5, 0, unknown5.Length);

                fireDebug("   Writing Unknown6... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(unknown6, 0, unknown6.Length);

                fireDebug("   Writing Padding3... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(padding3)), 0, 2);

                fireDebug("   Writing Enable Time Limit... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(enableTimeLimit)), 0, 4);

                fireDebug("   Writing Time Limit... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(BitConverter.GetBytes(Shared.Swap(timeLimit)), 0, 4);

                fireDebug("   Writing Padding4... (Offset: 0x{0})", ms.Position.ToString("x8").ToUpper());
                ms.Write(padding4, 0, padding4.Length);

                byte[] tik = ms.ToArray();
                ms.Dispose();

                //fake Sign
                if (fakeSign) {
                    fireDebug("   Fakesigning Ticket...");

                    byte[] hash = new byte[20];
                    SHA1 s = SHA1.Create();

                    for (ushort i = 0; i < 0xFFFF; i++) {
                        byte[] bytes = BitConverter.GetBytes(i);
                        tik[498] = bytes[1]; tik[499] = bytes[0];

                        hash = s.ComputeHash(tik);
                        if (hash[0] == 0x00) { fireDebug("   -> Signed ({0})", i); break; } //Win! It's signed...

                        if (i == 0xFFFF - 1) { fireDebug("    -> Signing Failed..."); throw new Exception("Fakesigning failed..."); }
                    }

                    s.Clear();
                }

                writeStream.Seek(0, SeekOrigin.Begin);
                writeStream.Write(tik, 0, tik.Length);

                fireDebug("Writing Ticket Finished...");
            }

            private void parseTicket(Stream ticketFile) {
                fireDebug("Parsing Ticket...");

                ticketFile.Seek(0, SeekOrigin.Begin);
                byte[] temp = new byte[8];

                fireDebug("   Reading Signature Exponent... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 4);
                signatureExponent = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                fireDebug("   Reading Signature... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(signature, 0, signature.Length);

                fireDebug("   Reading Padding... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(padding, 0, padding.Length);

                fireDebug("   Reading Issuer... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(issuer, 0, issuer.Length);

                fireDebug("   Reading Unknown... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(unknown, 0, unknown.Length);

                fireDebug("   Reading Title Key... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(encryptedTitleKey, 0, encryptedTitleKey.Length);

                fireDebug("   Reading Unknown2... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                unknown2 = (byte) ticketFile.ReadByte();

                fireDebug("   Reading Ticket ID.. (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 8);
                ticketId = Shared.Swap(BitConverter.ToUInt64(temp, 0));

                fireDebug("   Reading Console ID... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 4);
                consoleId = Shared.Swap(BitConverter.ToUInt32(temp, 0));

                fireDebug("   Reading Title ID... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 8);
                titleId = Shared.Swap(BitConverter.ToUInt64(temp, 0));

                fireDebug("   Reading Unknown3... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading NumOfDLC... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 4);
                unknown3 = Shared.Swap(BitConverter.ToUInt16(temp, 0));
                numOfDlc = Shared.Swap(BitConverter.ToUInt16(temp, 2));

                fireDebug("   Reading Unknown4... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 8);
                unknown4 = Shared.Swap(BitConverter.ToUInt64(temp, 0));

                fireDebug("   Reading Padding2... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                padding2 = (byte) ticketFile.ReadByte();

                fireDebug("   Reading Common Key Index... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                commonKeyIndex = (byte) ticketFile.ReadByte();

                newKeyIndex = commonKeyIndex;

                fireDebug("   Reading Unknown5... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(unknown5, 0, unknown5.Length);

                fireDebug("   Reading Unknown6... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(unknown6, 0, unknown6.Length);

                fireDebug("   Reading Padding3... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 2);
                padding3 = Shared.Swap(BitConverter.ToUInt16(temp, 0));

                fireDebug("   Reading Enable Time Limit... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                fireDebug("   Reading Time Limit... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(temp, 0, 8);
                enableTimeLimit = Shared.Swap(BitConverter.ToUInt32(temp, 0));
                timeLimit = Shared.Swap(BitConverter.ToUInt32(temp, 4));

                fireDebug("   Reading Padding4... (Offset: 0x{0})", ticketFile.Position.ToString("x8").ToUpper());
                ticketFile.Read(padding4, 0, padding4.Length);

                fireDebug("   Decrypting Title Key...");
                decryptTitleKey();
                fireDebug("    -> Encrypted Title Key: {0}", Shared.ByteArrayToString(encryptedTitleKey,' '));
                fireDebug("    -> Decrypted Title Key: {0}", Shared.ByteArrayToString(decryptedTitleKey,' '));

                fireDebug("Parsing Ticket Finished...");
            }

            private void decryptTitleKey() {
                byte[] ckey = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] iv = BitConverter.GetBytes(Shared.Swap(titleId));
                Array.Resize(ref iv, 16);

                RijndaelManaged rm = new RijndaelManaged();
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.None;
                rm.KeySize = 128;
                rm.BlockSize = 128;
                rm.Key = ckey;
                rm.IV = iv;

                ICryptoTransform decryptor = rm.CreateDecryptor();

                MemoryStream ms = new MemoryStream(encryptedTitleKey);
                CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);

                cs.Read(decryptedTitleKey, 0, decryptedTitleKey.Length);

                cs.Dispose();
                ms.Dispose();
                decryptor.Dispose();
                rm.Clear();
            }

            private void encryptTitleKey() {
                commonKeyIndex = newKeyIndex;
                byte[] ckey = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                byte[] iv = BitConverter.GetBytes(Shared.Swap(titleId));
                Array.Resize(ref iv, 16);

                RijndaelManaged rm = new RijndaelManaged();
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.None;
                rm.KeySize = 128;
                rm.BlockSize = 128;
                rm.Key = ckey;
                rm.IV = iv;

                ICryptoTransform encryptor = rm.CreateEncryptor();

                MemoryStream ms = new MemoryStream(decryptedTitleKey);
                CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Read);

                cs.Read(encryptedTitleKey, 0, encryptedTitleKey.Length);

                cs.Dispose();
                ms.Dispose();
                encryptor.Dispose();
                rm.Clear();
            }

            private void reDecryptTitleKey() {
                encryptedTitleKey = newEncryptedTitleKey;
                decryptTitleKey();
            }
            #endregion

            #region Events
            /// <summary>
            /// Fires debugging messages. You may write them into a log file or log textbox.
            /// </summary>
            public event EventHandler<MessageEventArgs> Debug;

            private void fireDebug(string debugMessage, params object[] args) {
                EventHandler<MessageEventArgs> debug = Debug;
                if (debug != null)
                    debug(new object(), new MessageEventArgs(string.Format(debugMessage, args)));
            }
            #endregion
        }
    }
}
