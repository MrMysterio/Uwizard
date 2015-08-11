using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Uwizard {
    static class Program {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool AllocConsole();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            string[] cla = Environment.GetCommandLineArgs();

            if (cla.Length > 1) {
                AllocConsole();
                AttachConsole(-1);
                Console.WriteLine("Uwizard " + Form1.getVerText(Form1.myversion));
                Console.WriteLine();

                string opath = "";
                bool sepchans = false;
                byte sarcmode = 0;
                int sarcpad = 0x100;
                for (int c = 2; c < cla.Length; c++) {
                    if (cla[c] == "-s")
                        sepchans = true;
                    if (cla[c] == "-o" && c+1 < cla.Length)
                        opath = cla[c+1];
                    if (cla[c] == "-c")
                        sarcmode = 1;
                    if (cla[c] == "-e")
                        sarcmode = 2;
                    if (cla[c] == "-pad" && c+1 < cla.Length)
                        sarcpad = int.Parse(cla[c+1]);
                }

                if (System.IO.File.Exists(cla[1])) {

                    Console.WriteLine("Reading \"{0}\".", System.IO.Path.GetFileName(cla[1]));

                    System.IO.StreamReader sr = new System.IO.StreamReader(cla[1]);
                    string magic = ((char) sr.BaseStream.ReadByte()).ToString() + ((char) sr.BaseStream.ReadByte()).ToString() + ((char) sr.BaseStream.ReadByte()).ToString() + ((char) sr.BaseStream.ReadByte()).ToString();

                    switch (magic) {
                        case "SARC":
                            Console.WriteLine("File is a SARC archive.");
                            if (sarcmode == 0) {
                                Console.WriteLine("Do you want to extract the contents of this archive or compress this archive into a Yaz0 SZS? (C/E)");
                                if (Console.ReadKey().Key == ConsoleKey.C)
                                    sarcmode = 1;
                                else
                                    sarcmode = 2;
                            }
                            if (sarcmode == 1) {
                                if (opath == "") opath = cla[1] + ".szs";
                                Console.WriteLine("Compressing to \"" + opath + "\".");
                                if (Form1.packszs(cla[1], opath))
                                    Console.WriteLine("Finished!");
                                else
                                    Console.WriteLine("Error!");
                            } else {
                                if (opath == "") opath = cla[1] + "_extracted";
                                Console.WriteLine("Extracting to \"" + opath + "\".");
                                if (SARC.extract(cla[1], opath)) {
                                    Console.WriteLine("Finished! Open the new directory? (Y/N)");
                                    if (Console.ReadKey().Key == ConsoleKey.Y)
                                        System.Diagnostics.Process.Start(opath);
                                } else
                                    Console.WriteLine("Error!\n" + SARC.lerror);
                            }
                            break;
                        case "Yaz0":
                            Console.WriteLine("File is Yaz0 compressed.");
                            if (opath == "") opath = cla[1] + ".bin";
                            Console.WriteLine("Decompressing to \"" + opath + "\".");
                            if (Form1.extractszs(cla[1], opath))
                                Console.WriteLine("Finished!");
                            else
                                Console.WriteLine("Error!");
                            break;
                        case "FRES":
                            Console.WriteLine("File is a BFRES model.");
                            Console.WriteLine("Support for BFRES models is coming soon!");
                            break;
                        case "FSTM":
                            Console.WriteLine("File is a BFSTM sound stream.");
                            if (opath == "") opath = cla[1] + ".wav";
                            Console.Write("Extracting...");
                            if (Form1.convertbfstm(cla[1], opath, sepchans))
                                Console.WriteLine("Done!");
                            else
                                Console.WriteLine("Error!");
                            break;
                        case "FWAV":
                            Console.WriteLine("File is a BFWAV sound stream.");
                            if (opath == "") opath = cla[1] + ".wav";
                            Console.Write("Extracting...");
                            if (Form1.convertbfstm(cla[1], opath, sepchans))
                                Console.WriteLine("Done!");
                            else
                                Console.WriteLine("Error!");
                            break;
                        default:
                            Console.WriteLine("Unknown file type!");
                            break;
                    }
                } else
                    if (System.IO.Directory.Exists(cla[1])) {
                        if (opath == "") opath = cla[1] + ".sarc";
                        Console.WriteLine("Packing directory into a SARC archive at " + opath);
                        if (SARC.pack(cla[1], opath))
                            Console.WriteLine("Finished!");
                        else
                            Console.WriteLine("Error!");
                    } else {
                        Console.WriteLine("Uwizard can run in command line mode in addition to GUI mode. You may specify a file and Uwizard will take the correct action for that file type. For example, if the first argument is the path to an SZS file, Uwizard will try to decompress it. You may also specify an output path with the \"-o <outputfile>\" parameter. If the input file is a BFSTM sound stream, you may also specify the \"-s\" switch to export all sound channels as seperate WAV files. If the input file is a SARC archive, you may add \"-c\" to compress the SARC into a Yaz0 SZS, or \"-e\" to extract it to a directory. You may also specify the SARC padding with the \"-pad <decimalvalue>\".");
                    }
                Console.WriteLine();
                Console.WriteLine("Press any key to close this window. The 'A' key works too. :P");
                Console.ReadKey();
                return;
            }//*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}