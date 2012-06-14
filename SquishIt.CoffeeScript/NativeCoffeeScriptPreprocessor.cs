using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SquishIt.CoffeeScript.Coffee;
using SquishIt.Framework;

namespace SquishIt.CoffeeScript
{
    /// <summary>
    /// Uses the native coffee command
    /// </summary>
    public class NativeCoffeeScriptPreprocessor : IPreprocessor
    {
        const string validExtension = ".coffee";

        public bool ValidFor(string extension)
        {
            var upperExtension = extension.ToUpper();
            return Extensions.Contains(upperExtension.StartsWith(".") ? upperExtension : ("." + upperExtension));
        }

        public string Process(string filePath, string content)
        {
            var compiler = new NativeCoffeeScriptCompiler();
            return compiler.Compile(filePath);
        }

        public string[] Extensions
        {
            get { return new[]{ validExtension.ToUpper() }; }
        }
    }
}
