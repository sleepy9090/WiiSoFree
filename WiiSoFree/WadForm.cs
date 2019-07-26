using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace WiiSoFree
{
    public partial class WadForm : Form
    {
        private string path2 = "";
        private string filename2 = "";

        public WadForm()
        {
            InitializeComponent();
        }


        private void OpenWadVCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd2 = new OpenFileDialog
            {
                Title = @"Open file...",
                Filter = @"WAD files (*.wad)|*.wad|All files (*.*)|*.*",
                Multiselect = false
            };

            if (ofd2.ShowDialog() == DialogResult.OK)
            {
                //clear all the stuff

                //load the wad filename
                wadFilenameTextBox.Text = ofd2.FileName;
            }

            if (wadFilenameTextBox.Text != "")
            {
                path2 = wadFilenameTextBox.Text;
                filename2 = wadFilenameTextBox.Text.Substring(wadFilenameTextBox.Text.LastIndexOf('\\') + 1);
                patchWadVCToolStripMenuItem.Enabled = true;
                patchWadButton.Enabled = true;

                LoadWadfile();
            }
        }

        private void WadFormLoad(object sender, EventArgs e)
        {
            patchWadVCToolStripMenuItem.Enabled = false;
            patchWadButton.Enabled = false;
            newRegionCodeTextBox.Enabled = false;
            newRegionStringTextBox.Enabled = false;

            newRegionStringComboBox.Items.Add("USA - GENERIC");
            newRegionStringComboBox.Items.Add("JAP - GENERIC");
            newRegionStringComboBox.Items.Add("PAL - GENERIC");
            newRegionStringComboBox.Items.Add("Region Free");

            newRegionCodeTextBox.Text = "0001";
            newRegionStringComboBox.SelectedIndex = 0;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewRegionStringComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (newRegionStringComboBox.SelectedIndex)
            {
                case 0:
                    //USA
                    newRegionCodeTextBox.Text = "0001";
                    newRegionStringTextBox.Text = WSFConstants.UsaRegionString;
                    break;

                case 1:
                    //JAP
                    newRegionCodeTextBox.Text = "0000";
                    newRegionStringTextBox.Text = WSFConstants.JapRegionString;
                    break;

                case 2:
                    //PAL
                    newRegionCodeTextBox.Text = "0002";
                    newRegionStringTextBox.Text = WSFConstants.PalRegionString;
                    break;

                case 3:
                    //Region Free
                    newRegionCodeTextBox.Text = "0003";
                    newRegionStringTextBox.Text = WSFConstants.UsaRegionString; // TODO: Verify
                    break;

                default:
                    // Should not happen
                    newRegionCodeTextBox.Text = "0001";
                    newRegionStringTextBox.Text = WSFConstants.UsaRegionString;
                    break;

            }
        }

        private void PatchWadButton_Click(object sender, EventArgs e)
        {
            int errorCounterInt = 0;
            //1. write the new region string
            //0x4E010 (319504d) - 0x4E01F (319519d)

            string regionPatchValueString = newRegionStringTextBox.Text;
            //get rid of spaces
            string regionPatchValueString2 = regionPatchValueString.Replace(" ", "");


            //setup arrays
            string[] rpvs = new string[32];
            byte[] rpvsb = new byte[32];
            int[] rpvsd = new int[32];
            string[] rpvsFinal = new string[32];

            //split it up into twos
            rpvs[0] = regionPatchValueString2[0].ToString() + regionPatchValueString2[1].ToString();
            rpvs[1] = regionPatchValueString2[2].ToString() + regionPatchValueString2[3].ToString();
            rpvs[2] = regionPatchValueString2[4].ToString() + regionPatchValueString2[5].ToString();
            rpvs[3] = regionPatchValueString2[6].ToString() + regionPatchValueString2[7].ToString();
            rpvs[4] = regionPatchValueString2[8].ToString() + regionPatchValueString2[9].ToString();
            rpvs[5] = regionPatchValueString2[10].ToString() + regionPatchValueString2[11].ToString();
            rpvs[6] = regionPatchValueString2[12].ToString() + regionPatchValueString2[13].ToString();
            rpvs[7] = regionPatchValueString2[14].ToString() + regionPatchValueString2[15].ToString();
            rpvs[8] = regionPatchValueString2[16].ToString() + regionPatchValueString2[17].ToString();
            rpvs[9] = regionPatchValueString2[18].ToString() + regionPatchValueString2[19].ToString();
            rpvs[10] = regionPatchValueString2[20].ToString() + regionPatchValueString2[21].ToString();
            rpvs[11] = regionPatchValueString2[22].ToString() + regionPatchValueString2[23].ToString();
            rpvs[12] = regionPatchValueString2[24].ToString() + regionPatchValueString2[25].ToString();
            rpvs[13] = regionPatchValueString2[26].ToString() + regionPatchValueString2[27].ToString();
            rpvs[14] = regionPatchValueString2[28].ToString() + regionPatchValueString2[29].ToString();
            rpvs[15] = regionPatchValueString2[30].ToString() + regionPatchValueString2[31].ToString();

            //convert the chars to int32, then to string, then byte again
            int q = 0;
            while (q <= 15)
            {
                rpvsd[q] = int.Parse(rpvs[q], System.Globalization.NumberStyles.HexNumber);
                rpvsFinal[q] = rpvsd[q].ToString();
                rpvsb[q] = byte.Parse(rpvsFinal[q]);
                q++;
            }

            FileStream fileStream = new FileStream(@path2, FileMode.Open, FileAccess.Write);
            fileStream.Seek(3742, SeekOrigin.Begin);   // Seek to 3742th byte in the file
            //this is converting to hex before it writes
            q = 0;
            while (q <= 15)
            {
                fileStream.WriteByte(rpvsb[q]);
                q++;
            }

            fileStream.Close();





            string newRegionCodeString = "01"; //default usa

            switch (newRegionCodeTextBox.Text)
            {
                //2.Write the new region code
                case "0000":
                    newRegionCodeString = "00"; //jap
                    break;
                case "0001":
                    newRegionCodeString = "01"; //usa
                    break;
                case "0002":
                    newRegionCodeString = "02"; //pal
                    break;
                case "0003":
                    newRegionCodeString = "03"; //rf
                    break;
                default:
                    MessageBox.Show("Invalid region code!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorCounterInt = 1;
                    break;
            }

            string[] nrcs = new string[2];
            byte[] nrcsb = new byte[2];
            int[] nrcsd = new int[2];
            string[] nrcss_final = new string[2];

            nrcs[0] = newRegionCodeString;

            nrcsd[0] = int.Parse(nrcs[0], System.Globalization.NumberStyles.HexNumber);
            nrcss_final[0] = nrcsd[0].ToString();
            nrcsb[0] = byte.Parse(nrcss_final[0]);
            
            fileStream = new FileStream(@path2, FileMode.Open, FileAccess.Write);
            fileStream.Seek(3805, SeekOrigin.Begin);   // Seek to 3805th byte in the file
            fileStream.WriteByte(nrcsb[0]);

            fileStream.Close();


            if (errorCounterInt == 0)
            {
                MessageBox.Show("WAD files successfully patched! Reload WAD file to see changes.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                patchWadButton.Enabled = false;
            }

        }

        private void LoadWadfile()
        {
            //1.get wad game code (offset 3728, 4 bytes)
            string gcComplete = "";
            string gcFirstThree = "";
            string gcLastOne = "";
            int x = 0;
            //gonna need a try/catch here in case the file is in use
            FileStream fileStream = new FileStream(@path2, FileMode.Open, FileAccess.Read);
            fileStream.Seek(3728, SeekOrigin.Begin);   // Seek to 3728th byte in the file

            while (x < 4)
            {
                string gc = fileStream.ReadByte().ToString("X");
                //if length is single digit add a 0 ( 1 now is 01)
                if (gc.Length == 1)
                {
                    gc = "0" + gc;
                }
                
                //convert the hex to ascii
                char gcAscii = (char)Int32.Parse(gc, NumberStyles.AllowHexSpecifier);

                gcComplete += gcAscii;
                if (x < 3)
                {
                    //so we can just get the first 3 for identification below
                    gcFirstThree += gcAscii;
                }
                if (x > 2)
                {
                    gcLastOne += gcAscii;
                }
                x += 1;
            }
            
            fileStream.Close();

            gamecodeTextBox.Text = gcComplete;
            try
            {
                gameNameTextBox.Text = WSFConstants.WadGamesDictionary[gcFirstThree];
                platformTextBox.Text = WSFConstants.WadMakerCodesDictionary[gcFirstThree];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
            

            //3.get maker code (offset 3736, 2 bytes, ascii)
            x = 0;
            string mcComplete = "";

            fileStream = new FileStream(@path2, FileMode.Open, FileAccess.Read);
            fileStream.Seek(3736, SeekOrigin.Begin);   // Seek to 3736th byte in the file

            while (x < 2)
            {
                string mc = fileStream.ReadByte().ToString("X");
                //if length is single digit add a 0 ( 1 now is 01)
                if (mc.Length == 1)
                {
                    mc = "0" + mc;
                }

                //convert the hex to ascii
                char mcAscii = (char)Int32.Parse(mc, NumberStyles.AllowHexSpecifier);

                mcComplete += mcAscii;
                
                x += 1;
            }
            fileStream.Close();


            makerCodeTextBox.Text = mcComplete;

            WiiDiscData wiiDiscData = new WiiDiscData();

            wiiDiscData.GetMakerByMakerCode(mcComplete);
            makerCodeCoTextBox.Text = wiiDiscData.GetMakerByMakerCode(mcComplete);


            //4.get region string (offset 3741, 16 bytes)
            x = 0;
            string rsComplete = "";

            fileStream = new FileStream(@path2, FileMode.Open, FileAccess.Read);
            fileStream.Seek(3742, SeekOrigin.Begin);   // Seek to 3742th byte in the file

            while (x < 16)
            {
                string rs = fileStream.ReadByte().ToString("X");
                //if length is single digit add a 0 ( 1 now is 01)
                if (rs.Length == 1)
                {
                    rs = "0" + rs;
                }

                //convert the hex to ascii
                //char mc_ascii = (char)Int32.Parse(mc, NumberStyles.AllowHexSpecifier);

                rsComplete += rs + " ";

                x += 1;
            }
            fileStream.Close();


            regionStringTextBox.Text = rsComplete;

            //5.get region code (offset 3804, 2 bytes)
            x = 0;
            string rcComplete = "";

            fileStream = new FileStream(@path2, FileMode.Open, FileAccess.Read);
            fileStream.Seek(3804, SeekOrigin.Begin);   // Seek to 3804th byte in the file

            while (x < 2)
            {
                string rc = fileStream.ReadByte().ToString("X");
                //if length is single digit add a 0 ( 1 now is 01)
                if (rc.Length == 1)
                {
                    rc = "0" + rc;
                }


                rcComplete += rc;

                x += 1;
            }
            fileStream.Close();

            switch (rcComplete)
            {
                case "0000":
                    rcComplete = "0000 [JAP]";
                    break;
                case "0001":
                    rcComplete = "0001 [USA]";
                    break;
                case "0002":
                    rcComplete = "0002 [PAL]";
                    break;
                case "0003":
                    rcComplete = "0003 [Region Free]";
                    break;
                default:
                    rcComplete = "???? [Unknown Region]";
                    break;
            }

            regionCodeTextBox.Text = rcComplete;

        }
    }
}
