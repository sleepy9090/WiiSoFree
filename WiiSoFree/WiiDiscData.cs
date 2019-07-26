using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WiiSoFree
{
    class WiiDiscData
    {
        /*
        string filename;
        public string filenameF2TextBoxValue
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        public string GetDataAtOffset(string dataAtOffset)
        {
            string path = filename;
            string dataAtOffsetEnter;
            dataAtOffsetEnter = dataAtOffset;
            FileStream fs777 = new FileStream(@path, FileMode.Open, FileAccess.Read);
            long offset105 = fs777.Seek(dataAtOffsetEnter, SeekOrigin.Begin);   

            //x = 0;

        }*/

        public string GetMakerByMakerCode(string makerCode)
        {
            string mcString = "";
            try
            {
                mcString = WSFConstants.GameMakerCodesDictionary[makerCode];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("makerCode: " + makerCode);
                //throw;
            }
             
            return mcString;
        }
    }
}
