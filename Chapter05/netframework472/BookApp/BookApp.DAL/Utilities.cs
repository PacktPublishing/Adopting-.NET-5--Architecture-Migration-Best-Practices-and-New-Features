using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.DAL
{
    public static class Utilities
    {

        public static byte[] ExtractImage(String fileName)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream resFilestream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".Resources." + fileName);
            if (resFilestream != null)
            {
                BinaryReader br = new BinaryReader(resFilestream);
                byte[] ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                br.Close();
                return ba;
            }
            else
            {
                Log.Warning("Embedded Resource with name {fileName} not found", fileName);
                return null;
            }
        }
    }
}
