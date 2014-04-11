﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS_DLR_Int
{
    public class CallCompiler
    {
        private static string comptype;
        public CallCompiler(string name, string type){

            comptype = type;
            string directory = Path.GetDirectoryName(name);
                string[] files = Directory.GetFiles(directory);

                foreach (string s in files)
                {
                    if (s.Contains(".plot") && !s.Contains(".tmp"))
                    {
                        string Filename = Path.GetFileNameWithoutExtension(s);
                        RunDesugar(s, s + ".tmp");
                        Runcompiler(s + ".tmp", Filename);
                    }
                }

                if (File.Exists(directory + @"\Compiler_lib.dll"))
                {
                    File.Delete(directory + @"\Compiler_Lib.dll");
                }
                System.IO.File.Copy(@"C:\Users\Scott\Documents\Compiler\IronPlot\Compiler\Compiler_Lib\bin\Debug\Compiler_Lib.dll", directory + @"\Compiler_Lib.dll");


            }
         

       

        private static void Runcompiler(string inputfile, string output_name)
        {
            ProcessStartInfo comp = new ProcessStartInfo(@"C:\Users\Scott\Documents\Compiler\IronPlot\Compiler\DLR_Compiler\bin\Debug\DLR_Compiler.exe", String.Format("\"{0}\" {1} {2}", inputfile, comptype, output_name));
            comp.UseShellExecute = true;
            comp.CreateNoWindow = false;
            try
            {
                Process Compile = Process.Start(comp);
                Compile.WaitForExit();
                Compile.Close();
            }
            catch (Exception e)
            {

            }
           }

        private void RunDesugar(string inputfile,string tempfile)
        {
            //Start the exe and forward standard output/error so we can parse etc.
            ProcessStartInfo Desugar = new ProcessStartInfo(@"C:\Users\Scott\Documents\Compiler\IronPlot\desugar.exe", String.Format("\"{0}\" \"{1}\"", inputfile, tempfile));
            Desugar.UseShellExecute = false;
            Desugar.CreateNoWindow = false;
            Desugar.RedirectStandardError = true;
            Desugar.RedirectStandardOutput = true;
            
          
            try
            {
                Process DesugarEXE = Process.Start(Desugar);
                StreamReader sr = DesugarEXE.StandardOutput;
                StreamReader sr2 = DesugarEXE.StandardError;
                string output=sr.ReadToEnd();
                string output2 = sr2.ReadToEnd();
                DesugarEXE.WaitForExit();
                DesugarEXE.Close();
            }
            catch (Exception e)
            {
               
            }

        }
    }
}
