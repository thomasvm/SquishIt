using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquishIt.CoffeeScript
{
    public class CoffeeUtils
    {
        const string validExtension = ".coffee";

        public static bool ValidFor(string extension)
        {
            var upperExtension = extension.ToUpper();
            return Extensions.Contains(upperExtension.StartsWith(".") ? upperExtension : ("." + upperExtension));
        }

        public static string[] Extensions
        {
            get { return new[] { validExtension.ToUpper() }; }
        }
    }
}
