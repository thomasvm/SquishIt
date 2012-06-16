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
            StringBuilder outputBuilder = new StringBuilder();
            StringBuilder errorBuilder = new StringBuilder();

            ProcessStartInfo coffee = new ProcessStartInfo();
            coffee.CreateNoWindow = true;
            coffee.RedirectStandardOutput = true;
            coffee.RedirectStandardInput = true;
            coffee.RedirectStandardError = true;
            coffee.UseShellExecute = false;

            if(!FileSystem.Unix)
            {
                coffee.Arguments = string.Format("/c coffee -cp {0} ", file);
                coffee.FileName = "cmd";
            }
            else
            {
                coffee.Arguments = string.Format("-cp {0}", file);
                coffee.FileName = "coffee";
            }
            
            Process process = new Process();
            process.StartInfo = coffee;
            // enable raising events because Process does not raise events by default
            process.EnableRaisingEvents = true;
            // attach the event handler for OutputDataReceived before starting the process
            process.OutputDataReceived += (sender, e) => outputBuilder.Append(e.Data);
            process.ErrorDataReceived += (sender, e) => errorBuilder.AppendLine(e.Data);
            
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if(errorBuilder.ToString().Trim().Length > 0)
                throw new ArgumentException(errorBuilder.ToString());
            
            // use the output
            string output = outputBuilder.ToString().Trim();
            return output;
        }
    }
}
