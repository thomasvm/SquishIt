using System.Linq;
using SquishIt.CoffeeScript.Coffee;
using SquishIt.Framework;

namespace SquishIt.CoffeeScript 
{
    public class CoffeeScriptPreprocessor : IPreprocessor
    {
        public bool ValidFor(string extension)
        {
            return CoffeeUtils.ValidFor(extension);
        }

        public string Process(string filePath, string content) 
        {
            var compiler = new EmbeddedCoffeeScriptCompiler();
            return compiler.Compile(content);
        }

        public string[] Extensions
        {
            get { return CoffeeUtils.Extensions; }
        }
    }
}
