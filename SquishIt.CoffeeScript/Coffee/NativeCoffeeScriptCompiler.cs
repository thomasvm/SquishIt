using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Jurassic;
using SquishIt.Framework;

namespace SquishIt.CoffeeScript.Coffee
{
    public class NativeCoffeeScriptCompiler
    {
        public string Compile(string file)
        {
            StringBuilder outputBuilder;
            ProcessStartInfo processStartInfo;
            Process process;

            outputBuilder = new StringBuilder();

            processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.Arguments = string.Format("-c {0}", file);
            processStartInfo.FileName = "coffee";

            process = new Process();
            process.StartInfo = processStartInfo;
            // enable raising events because Process does not raise events by default
            process.EnableRaisingEvents = true;
            // attach the event handler for OutputDataReceived before starting the process
            process.OutputDataReceived += (sender, e) => outputBuilder.Append(e.Data);
            // start the process
            // then begin asynchronously reading the output
            // then wait for the process to exit
            // then cancel asynchronously reading the output
            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
            process.CancelOutputRead();

            // use the output
            string output = outputBuilder.ToString();

            return output;
        }
    }
}
