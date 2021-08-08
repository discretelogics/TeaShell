using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Setup.Actions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CustomActions.UnRegisterPS(null);
        }
    }
}
