using System;
using System.IO;
using System.Windows.Forms;

namespace WiiSoFree
{
    public partial class WiiDiscPartitionsForm : Form
    {
        public WiiDiscPartitionsForm()
        {
            InitializeComponent();
        }
        
        //bring filename/path from first form

        public string FilenameF2TextBoxValue
        {
            get
            {
                return filenameF2TextBox.Text;
            }
            set
            {
                if (filenameF2TextBox != null)
                {
                    filenameF2TextBox.Text = value;
                }
            }
        }


        private void WiiDiscPartitionsFormLoad(object sender, EventArgs e)
        {
            
            string path = filenameF2TextBox.Text;
            int x;

            //get the image size
            FileInfo fileInfo = new FileInfo(path);
            long wiiIsoSize = fileInfo.Length;
            imageSizeTextBox.Text = wiiIsoSize.ToString();

            //partition numbers
            partNum1TextBox.Text = "0x1";
            partNum2TextBox.Text = "0x2";
            partNum3TextBox.Text = "0x3";
            partNum4TextBox.Text = "0x4";
            partNum5TextBox.Text = "0x5";
            partNum6TextBox.Text = "0x6";


            //1. Get Number of Partitions
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(262144, SeekOrigin.Begin);   // Seek to 262144th (0x40000) byte in the file
            x = 0;
            //read four bytes to get the number of partitions
            while (x <= 3)
            {
                string numberPartitions = fileStream.ReadByte().ToString("X");
                if (numberPartitions.Length == 1)
                {
                    numberPartitions = "0" + numberPartitions;
                }
                numOfPartTextBox.Text += numberPartitions;
                x++;
            }
            fileStream.Close();


            //2. Get partition table offset
            /*get the info at location 40004 & 40006, offset262148 read 4 bytes
             * 0x40004		00 01 00 08  -> 0001 0008 -> 10008 ->binary is 1 00000000 00001000
             * we need to shift left 2, so now we have
             * 100 00000000 00100000 ->  40020h -> 262176d <-offset of partition info
             * 
             * Is the partition an update partition or no?
             * 40020h + 4h = 40024h, read data
             * 0x40024  00 00 00 01  ->00000001-> 1 indicates an update partition / 0 if not
             */

            fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(262148, SeekOrigin.Begin);   // Seek to 262148 (0x40004) byte in the file

            string partitionTypeOffset1 = "";

            x = 0;
            //this loop lets us grab bytes 262148 to 262151 (0x40004 - 0x40007) - Partition info table offset, Address is (value << 2)
            while (x <= 3)
            {

                string pof = fileStream.ReadByte().ToString("X");
                if (pof.Length == 1)
                {
                    pof = "0" + pof;
                }

                //this stores the hex value of the offset 262148 + 4bytes (0x40004 to 0x40007)
                partitionTypeOffset1 += pof; //hex string
                
                x++;

            }
            fileStream.Close();

            //this is how we left shift 2 
            string partitionInfoTableOffset1Binary = Convert.ToString(Convert.ToInt32(partitionTypeOffset1, 16), 2);
            partitionInfoTableOffset1Binary = partitionInfoTableOffset1Binary + "00";

            //convert from binary back to hex to get the offset 
            string pito1Hex = Convert.ToString(Convert.ToInt32(partitionInfoTableOffset1Binary, 2), 16);

            partTableOffTextBox.Text = "0x" + pito1Hex;
            //put the offset to decimal form for seeking 1st partition info
            int partitionTypeOffset1Dec = int.Parse(pito1Hex, System.Globalization.NumberStyles.HexNumber);

            //now we need to get the type of partition, so move up 4 bytes and read that data
            int partitionTypeOffset1DecType = partitionTypeOffset1Dec + 0x4;

            fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(partitionTypeOffset1DecType, SeekOrigin.Begin);   // Seek to 262148 byte in the file
            string typeOfPartHexAll = "";
            x = 0;
            //this loop lets us grab 4 bytes to get the type of partition
            while (x <= 3)
            {
                string typeOfPartHex = fileStream.ReadByte().ToString("X");
                if (typeOfPartHex.Length == 1)
                {
                    typeOfPartHex = "0" + typeOfPartHex;
                }
                //this stores the hex value of the offset 262148 + 4bytes
                typeOfPartHexAll += typeOfPartHex; //hex string
                //partType1TextBox.Text = typeOfPartHexAll;
                x++;
            }
            fileStream.Close();

            switch (typeOfPartHexAll)
            {
                //define the partition type
                case "00000000":
                    partType1TextBox.Text = "0 (Data)";
                    break;
                case "00000001":
                    partType1TextBox.Text = "1 (Update)";
                    break;
                case "00000002":
                    partType1TextBox.Text = "2 (Channel installer)";
                    break;
                default:
                    partType1TextBox.Text = "? Unknown -> HEX Value:" + typeOfPartHexAll;
                    break;
            }

            //3. Get 1st partition offset
            //0x40020		00 01 40 00  ->00014000->10100000000000000->(<<2)
            //->1010000000000000000->(327680d, 0x00050000h)
            FileStream fs103 = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fs103.Seek(partitionTypeOffset1Dec, SeekOrigin.Begin);   // Seek to pito1Dec byte in the file

            string partition1OffsetHex = "";

            x = 0;
            //this loop lets us grab bytes 327680 (4 bytes)
            while (x <= 3)
            {

                string p1Offset = fs103.ReadByte().ToString("X");
                if (p1Offset.Length == 1)
                {
                    p1Offset = "0" + p1Offset;
                }

                //output the hex value
                partition1OffsetHex += p1Offset;

                x++;

            }
            fs103.Close();


            //this is how we left shift 2 
            string partition1OffsetBinary = Convert.ToString(Convert.ToInt32(partition1OffsetHex, 16), 2);
            partition1OffsetBinary = partition1OffsetBinary + "00";

            //convert from binary back to hex to get the offset 
            string partition1OffsetFoundHex = Convert.ToString(Convert.ToInt32(partition1OffsetBinary, 2), 16);

            po1TextBox.Text = "0x" + partition1OffsetFoundHex;
            //put the offset to decimal form for seeking ??
            int.Parse(partition1OffsetFoundHex, System.Globalization.NumberStyles.HexNumber);

            //in this example 50000 is the 1st partition offset,



            //3. Get 2st partition offset
            //0x40028		03 E0 00 00  ->03E00000->1111100000000000000000->(<<2)->111110000000000000000000->(260046848d, 0F800000) <-2nd partition location
            //0x4002C		00 00 00 00  ->00000000-> 0 indicates a data partition
            int pito2Dec = partitionTypeOffset1Dec + 8; //40020 + 8 = 40028


            fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(pito2Dec, SeekOrigin.Begin);   // Seek to pito2Dec byte in the file

            string partition2OffsetHex = "";

            x = 0;
            //this loop lets us grab bytes 327680 (4 bytes)
            while (x <= 3)
            {

                string partition2Offset = fileStream.ReadByte().ToString("X");
                if (partition2Offset.Length == 1)
                {
                    partition2Offset = "0" + partition2Offset;
                }

                //output the hex value
                partition2OffsetHex += partition2Offset;

                x++;

            }
            fileStream.Close();


            //this is how we left shift 2 
            string partition2OffsetBinary = Convert.ToString(Convert.ToInt32(partition2OffsetHex, 16), 2);
            partition2OffsetBinary = partition2OffsetBinary + "00";

            //convert from binary back to hex to get the offset 
            string partition2OffsetFoundHex = Convert.ToString(Convert.ToInt32(partition2OffsetBinary, 2), 16);

            po2TextBox.Text = "0x" + partition2OffsetFoundHex;
            //put the offset to decimal form for seeking ??
            int.Parse(partition1OffsetFoundHex, System.Globalization.NumberStyles.HexNumber);

            //in this example 0xF80000 is the 2nd partition offset,


            //now we need to get the type of partition, so move up 4 bytes and read that data
            int pito2DecType = pito2Dec + 4; //this should put us at 0x4002C


            fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(pito2DecType, SeekOrigin.Begin);   // Seek to 262148 byte in the file
            string typeOfPartHex2All = "";
            x = 0;
            //this loop lets us grab 4 bytes to get the type of partition
            while (x <= 3)
            {
                string typeOfPart2Hex = fileStream.ReadByte().ToString("X");
                if (typeOfPart2Hex.Length == 1)
                {
                    typeOfPart2Hex = "0" + typeOfPart2Hex;
                }
                //this stores the hex value of the offset 262148 + 4bytes
                typeOfPartHex2All += typeOfPart2Hex; //hex string
                //partType1TextBox.Text = typeOfPartHexAll;
                x++;
            }
            fileStream.Close();

            switch (typeOfPartHex2All)
            {
                //define the partition type
                case "00000001":
                    partType2TextBox.Text = "1 (Update)";
                    break;
                case "00000000":
                    partType2TextBox.Text = "0 (Data)";
                    break;
                default:
                    partType2TextBox.Text = "? Unknown -> HEX Value:" + typeOfPartHex2All;
                    break;
            }

        }
    }
}
