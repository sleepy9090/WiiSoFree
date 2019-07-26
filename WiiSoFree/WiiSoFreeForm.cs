/**
 *  @file           WiiSoFreeForm.cs
 *  @brief          This file contains the methods for reading and writing Wii disc data.
 *  @copyright      2019 Shawn M. Crawford [sleepy]
 *  @date           07/25/2019
 *
 *  @remark Author  Shawn M. Crawford
 *
 *  @note           N/A
 *
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace WiiSoFree
{
    public partial class WiiSoFreeForm : Form
    {
        private const string _ERROR_UNABLE_TO_RETRIEVE_VALUE = "Error: Unable to retrieve value.";

        public WiiSoFreeForm()
        {
            InitializeComponent();
        }
        
        //string openFlagString;
        string filename;
        string path;

        private void SelectNewRegionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            string gameSelected = selectNewRegionComboBox.Text;
            try
            {
                newRegionStringTextBox.Text = WSFConstants.GameRegionDictionary[gameSelected];
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
            

            if (gameSelected.Contains(WSFConstants.Usa))
            {
                newRegionCodeTextBox.Text = WSFConstants.Region1;
            }
            else if (gameSelected.Contains(WSFConstants.Jap))
            {
                newRegionCodeTextBox.Text = WSFConstants.Region0;
            }
            else if (gameSelected.Contains(WSFConstants.Pal))
            {
                newRegionCodeTextBox.Text = WSFConstants.Region2;
            }
        }

        private void WiiSoFreeFormLoad(object sender, EventArgs e)
        {

            changeBootSettingComboBox.Items.Add("R: Revolution (Wii) disc, Manual boot");
            changeBootSettingComboBox.Items.Add("S: Wii disc, Manual boot");
            changeBootSettingComboBox.Items.Add("G: GameCube, Manual boot");
            changeBootSettingComboBox.Items.Add("U: Utility-disc / GameCube (GBA-Player) disc, Manual boot");
            changeBootSettingComboBox.Items.Add("D: GameCube Demo disc, Manual boot");
            changeBootSettingComboBox.Items.Add("P: GameCube promotional disc, Manual boot");
            changeBootSettingComboBox.Items.Add("0: Diagnostic disc, Auto boot");
            changeBootSettingComboBox.Items.Add("1: Diagnostic disc, Manual boot");
            changeBootSettingComboBox.Items.Add("4: Wii backup disc, Unknown");
            changeBootSettingComboBox.Items.Add("_: WiiFit Channel installer, Unknown");
            changeBootSettingComboBox.Items.Add("H: Wii Channel, Unknown");
            changeBootSettingComboBox.Items.Add("F: Virtual Console: NES, Unknown");
            changeBootSettingComboBox.Items.Add("J: Virtual Console: SNES, Unknown");
            changeBootSettingComboBox.Items.Add("M: Virtual Console: MegaDrive/Genesis, Unknown");
            changeBootSettingComboBox.Items.Add("T: Virtual Console: TurboGrafx-16, Unknown");
            changeBootSettingComboBox.Items.Add("N: Virtual Console: Nintendo 64, Unknown");
            changeBootSettingComboBox.SelectedIndex = 0;

            // Offsets 0x04E010 to 0x4E01F need to contain the 16 bytes from any other game in the region.
            // Offset 0x04E003 contains the region code...

            // 0x04E003: 00 = NTSC JAP
            // 0x04E003: 01 = NTSC USA
            // 0x04E003: 02 = PAL

            foreach (KeyValuePair<string, string> game in WSFConstants.GameRegionDictionary)
            {
                // populate selectNewRegionComboBox
                selectNewRegionComboBox.Items.Add(game.Key);
            }

            selectNewRegionComboBox.SelectedIndex = 0;
            discInfoButton.Enabled = false;
            newRegionStringTextBox.Enabled = false;
            newRegionCodeTextBox.Enabled = false;
        }

        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();

            ab.ShowDialog();
        }

        private void OpenIsoToolStripMenuItemClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = @"Open file...";
            ofd.Filter = @"ISO files (*.iso)|*.iso|All files (*.*)|*.*";
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //clear/reset stuff
                filenameTextBox.Clear();
                gameTitleTextBox.Clear();
                gameCodeTextBox.Clear();
                consoleIDAsciiTextBox.Clear();
                consoleIDTextBox.Clear();
                gameIDAsciiTextBox.Clear();
                gameIDTextBox.Clear();
                makerIDAsciiTextBox.Clear();
                makerIDTextBox.Clear();
                regionIDAsciiTextBox.Clear();
                regionIDTextBox.Clear();
                regionCodeHexTextBox.Clear();
                regionCodeTextBox.Clear();
                regionStringTextBox.Clear();
                offsetMagicWordTextBox.Clear();
                totalDiscPartitionsTextBox.Clear();
                mismatchTextBox.Clear();
                manualRegionCheckBox.Checked = false;
                selectNewRegionComboBox.SelectedIndex = 0;
                discInfoButton.Enabled = true;
                newRegionCodeTextBox.Text = "01";

                filenameTextBox.Text = ofd.FileName;
            }

            if (filenameTextBox.Text != "")
            {
                path = filenameTextBox.Text;
                filename = filenameTextBox.Text.Substring(filenameTextBox.Text.LastIndexOf('\\') + 1);
                patchISOToolStripMenuItem.Enabled = true;
                patchIsoButton.Enabled = true;

                LoadFile();
            }
        }

        private void CopyPartitionInfoToolStripMenuItemClick(object sender, EventArgs e)
        {

            string iiString = "WiiSoFree Image Output" + "\r\n" + "Coded by: Shawn M. Crawford [sLeEpY]" + "\r\n"
                + "------------------------------------" + "\r\n" + "Filename: " + filenameTextBox.Text +
                "\r\n" + "Game Title: " + gameTitleTextBox.Text + "\r\n" + "Game Code: " + gameCodeTextBox.Text
                + " " + mismatchTextBox.Text + "\r\n" + "Console ID: " + consoleIDAsciiTextBox.Text + " "
                + consoleIDTextBox.Text + "\r\n" + "Game ID: " + gameIDAsciiTextBox.Text + " " +
                gameIDTextBox.Text + "\r\n" + "Maker ID: " + makerIDAsciiTextBox.Text + " " + makerIDTextBox.Text
                + "\r\n" + "Region ID: " + regionIDAsciiTextBox.Text + " " + regionIDTextBox.Text + "\r\n" +
                "Region Code: " + regionCodeHexTextBox.Text + " " + regionCodeTextBox.Text + "\r\n" + "Region String: " +
                regionStringTextBox.Text + "\r\n" + "Offset / Magic Word: " + offsetMagicWordTextBox.Text +
                "\r\n" + "Boot Settings: " + bootSettingByteTextBox.Text + " " + bootSettingTextBox.Text +
                "\r\n" + "Total Disc Partitions: " + totalDiscPartitionsTextBox.Text;
            Clipboard.SetText(iiString);
        }

        private void CopyNewRegionStringToolStripMenuItemClick(object sender, EventArgs e)
        {

            if (newRegionStringTextBox.SelectionLength == 0)
            {
                newRegionStringTextBox.SelectAll();
            }

            newRegionStringTextBox.Copy();
        }

        private void LoadFile()
        {
            PopulateGameIdTextBox();
            PopulateRegionCode();
            PopulateMagicWord();
            PopulateGameTitle();
            PopulateMakerId();
            PopulateRegion();
            PopulateConsoleIdAndBootSettings();
            PopulateGameId();
            PopulateNumberOfPartitions();
            PopulateGameCode();
        }

        private void PopulateGameId()
        {
            // Game ID (Game Code)
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(1, SeekOrigin.Begin);   // Seek to 1 (2nd) byte in the file

            int x = 0;

            while (x < 2)
            {
                string gameId = fileStream.ReadByte().ToString("X");
                
                string gameIdChar = "";
                while (gameId.Length > 0)
                {
                    // Use ToChar() to convert each hex byte to the actual character
                    gameIdChar += System.Convert.ToChar(System.Convert.ToUInt32(gameId.Substring(0, 2), 16)).ToString();
                    // Remove from the hex object the converted value
                    gameId = gameId.Substring(2, gameId.Length - 2);
                }
                gameIDAsciiTextBox.Text += gameIdChar;//.ToString();
                x++;
            }
            fileStream.Close();
        }

        private void PopulateConsoleIdAndBootSettings()
        {
            // Console ID (Game Code) and auto/manual boot settings
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);   // Seek to 0 (1st)byte in the file

            string consoleId = fileStream.ReadByte().ToString("X");

            // convert the hex to char
            string consoleIdChar = "";

            if (consoleId != "0")
            {
                while (consoleId.Length > 0)
                {
                    // Use ToChar() to convert each ASCII value (two hex digits) to the actual character
                    consoleIdChar += System.Convert.ToChar(System.Convert.ToUInt32(consoleId.Substring(0, 2), 16)).ToString();
                    // Remove from the hex object the converted value
                    consoleId = consoleId.Substring(2, consoleId.Length - 2);
                }
            }
            else
            {
                consoleIdChar = consoleId;
            }
            consoleIDAsciiTextBox.Text = consoleIdChar;
            bootSettingByteTextBox.Text = consoleIdChar;

            fileStream.Close();

            switch (consoleIdChar)
            {
                case "R":
                    consoleIDTextBox.Text = WSFConstants.Rev;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "S":
                    consoleIDTextBox.Text = WSFConstants.Wii;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "G":
                    consoleIDTextBox.Text = WSFConstants.Gc;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "H":
                    consoleIDTextBox.Text = WSFConstants.WiiChan;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "F":
                    consoleIDTextBox.Text = WSFConstants.VcNes;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "J":
                    consoleIDTextBox.Text = WSFConstants.VcSnes;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "M":
                    consoleIDTextBox.Text = WSFConstants.VcGen;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "T":
                    consoleIDTextBox.Text = WSFConstants.VcT16;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "N":
                    consoleIDTextBox.Text = WSFConstants.VcN64;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "U":
                    consoleIDTextBox.Text = WSFConstants.Util;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "D":
                    consoleIDTextBox.Text = WSFConstants.GcDemo;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "P":
                    consoleIDTextBox.Text = WSFConstants.GcPromo;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "0":
                    consoleIDTextBox.Text = WSFConstants.Diag;
                    bootSettingTextBox.Text = WSFConstants.Auto;
                    break;
                case "1":
                    consoleIDTextBox.Text = WSFConstants.Diag;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "4":
                    consoleIDTextBox.Text = WSFConstants.WiiBackup;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                case "_":
                    consoleIDTextBox.Text = WSFConstants.WiiFit;
                    bootSettingTextBox.Text = WSFConstants.Manual;
                    break;
                default:
                    consoleIDTextBox.Text = WSFConstants.Unknown;
                    bootSettingTextBox.Text = WSFConstants.Unknown;
                    break;
            }
        }

        private void PopulateRegion()
        {
            // 0x4E010 (319504d) - 0x4E01F (319519d)
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(319504, SeekOrigin.Begin);   // Seek to 319504th byte in the file

            int x = 0;
            while (x <= 15)
            {
                string regionString = fileStream.ReadByte().ToString("X");
                // if length is single digit add a 0 (1 is now 01)
                if (regionString.Length == 1)
                {
                    regionString = "0" + regionString;
                }

                if (x > 0)
                {
                    regionStringTextBox.Text += " " + regionString;
                }
                else
                {
                    regionStringTextBox.Text = regionString;
                }

                x++;
            }

            fileStream.Close();
        }

        private void PopulateMakerId()
        {
            // Maker Code
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(4, SeekOrigin.Begin);   // Seek to 4 (5th) byte in the file

            string makerCode = "";

            int x = 0;

            while (x < 2)
            {
                string makerId = fileStream.ReadByte().ToString("X");
                string makerIdChar = "";

                while (makerId.Length > 0)
                {
                    // Use ToChar() to convert each hex byte to char
                    makerIdChar += System.Convert.ToChar(System.Convert.ToUInt32(makerId.Substring(0, 2), 16)).ToString();
                    // Remove from the hex object the converted value
                    makerId = makerId.Substring(2, makerId.Length - 2);
                }
                makerIDAsciiTextBox.Text += makerIdChar;

                makerCode += makerIdChar;
                x++;

            }
            fileStream.Close();
            
            WiiDiscData wiiDiscData = new WiiDiscData();

            wiiDiscData.GetMakerByMakerCode(makerCode);
            makerIDTextBox.Text = wiiDiscData.GetMakerByMakerCode(makerCode);
        }

        private void PopulateGameCode()
        {
            gameCodeTextBox.Text = consoleIDAsciiTextBox.Text + gameIDAsciiTextBox.Text + regionIDAsciiTextBox.Text + makerIDAsciiTextBox.Text;
        }

        private void PopulateNumberOfPartitions()
        {
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(262144, SeekOrigin.Begin);   // Seek to 262144th byte in the file

            int x = 0;

            while (x <= 3)
            {
                string numberOfPartitions = fileStream.ReadByte().ToString("X");
                if (numberOfPartitions.Length == 1)
                {
                    numberOfPartitions = "0" + numberOfPartitions;
                }
                totalDiscPartitionsTextBox.Text += numberOfPartitions; // e.g. 00 00 00 02
                x++;
            }
            fileStream.Close();
        }

        private void PopulateGameTitle()
        {
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(32, SeekOrigin.Begin);   // Seek to 32 (0x20) byte in the file
            
            string gameTitleToAsciiGameTitle = "";

            int x = 0;
            while (x <= 64) // Read 0x40 bytes
            {
                // the following try / catch statements catch the error of reading the title, since
                // this title is variable depending on game it would crash or not read enough
                try
                {
                    string gameTitle = fileStream.ReadByte().ToString("X");
                    gameTitleToAsciiGameTitle += System.Convert.ToChar(System.Convert.ToUInt32(gameTitle.Substring(0, 2), 16)).ToString();
                }
                catch (Exception)
                {
                    // Swallow exception
                }

                x++;
            }
            gameTitleTextBox.Text = gameTitleToAsciiGameTitle;
            fileStream.Close();
        }

        private void PopulateMagicWord()
        {
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            // Wii 0x5D1C9EA3
            fileStream.Seek(24, SeekOrigin.Begin);   // Seek to 24 (0x18) byte in the file

            // GameCube 0xC2339F3D
            //fileStream.Seek(28, SeekOrigin.Begin);   // Seek to 28 (0x1C) byte in the file

            int x = 0;

            while (x <= 3)
            {
                try
                {
                    offsetMagicWordTextBox.Text += fileStream.ReadByte().ToString("X");
                }
                catch (Exception)
                {
                    offsetMagicWordTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
                }
                
                x++;
            }
            
            fileStream.Close();
        }

        private void PopulateRegionCode()
        {
            try
            {
                // Region ID (Game Code)
                FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
                fileStream.Seek(3, SeekOrigin.Begin);   // Seek to 3 (4th) byte in the file

                string regionId = fileStream.ReadByte().ToString("X");

                // Use ToChar() to convert hex byte to char
                string charRegionId = System.Convert.ToChar(System.Convert.ToUInt32(regionId.Substring(0, 2), 16)).ToString();

                regionIDAsciiTextBox.Text = charRegionId;

                fileStream.Close();

                switch (charRegionId)
                {
                    case "D":
                        regionIDTextBox.Text = WSFConstants.Ger;
                        break;
                    case "E":
                        regionIDTextBox.Text = WSFConstants.Usa;
                        break;
                    case "F":
                        regionIDTextBox.Text = WSFConstants.Fra;
                        break;
                    case "I":
                        regionIDTextBox.Text = WSFConstants.Ita;
                        break;
                    case "J":
                        regionIDTextBox.Text = WSFConstants.Jap;
                        break;
                    case "K":
                        regionIDTextBox.Text = WSFConstants.Kor;
                        break;
                    case "P":
                    case "Y":
                        regionIDTextBox.Text = WSFConstants.Pal;
                        break;
                    case "R":
                        regionIDTextBox.Text = WSFConstants.Rus;
                        break;
                    case "S":
                        regionIDTextBox.Text = WSFConstants.Spa;
                        break;
                    case "T":
                        regionIDTextBox.Text = WSFConstants.Tai;
                        break;
                    case "U":
                        regionIDTextBox.Text = WSFConstants.Aus;
                        break;
                    default:
                        regionIDTextBox.Text = WSFConstants.Unknown;
                        break;
                }
            
                fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
                fileStream.Seek(319491, SeekOrigin.Begin);   // Seek to 319491th byte in the file
                string regionCode = fileStream.ReadByte().ToString("X");

                // if length is single digit add a 0 (1 is now 01)
                if (regionCode.Length == 1)
                {
                    regionCode = "0" + regionCode;
                }
                regionCodeHexTextBox.Text = regionCode;
                fileStream.Close();

                int regionCodeInt = int.Parse(regionCode);

                switch (regionCodeInt)
                {
                    case 00:
                        regionCodeTextBox.Text = WSFConstants.JapNtsc;
                        break;
                    case 01:
                        regionCodeTextBox.Text = WSFConstants.UsaNtsc;
                        break;
                    case 02:
                        regionCodeTextBox.Text = WSFConstants.EurPal;
                        break;
                    default:
                        regionCodeTextBox.Text = WSFConstants.Unknown;
                        break;
                }

                // check if game code matches
                string regionCodeIntString = regionCodeInt.ToString();

                switch (charRegionId)
                {
                    case "E" when regionCodeIntString == "1":
                    case "P" when regionCodeIntString == "2":
                    case "Y" when regionCodeIntString == "2":
                    case "J" when regionCodeIntString == "0":
                    case "K" when regionCodeIntString == "4":
                        mismatchTextBox.Text = @"Region ID & Region Code Match";
                        break;
                    default:
                        mismatchTextBox.Text = @"Unknown, Unsupported, or Region ID & Region Code Mismatch";
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                regionIDTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
                regionIDAsciiTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
                regionCodeTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
                regionCodeHexTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
                mismatchTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
            }
        }

        private void PopulateGameIdTextBox()
        {
            FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);   // Seek to 0 byte in the file
            
            string discIdString = "";
            int x = 0;
            
            while (x < 6)
            {
                try
                {
                    string discIdHexBytes = fileStream.ReadByte().ToString("X");
                    discIdString += System.Convert.ToChar(System.Convert.ToUInt32(discIdHexBytes.Substring(0, 2), 16)).ToString();
                }
                catch (Exception)
                {
                    gameIDTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
                }
                
                x++;

            }
            fileStream.Close();

            try
            {
                gameIDTextBox.Text = WSFConstants.WiiGamesDictionary[discIdString];
            }
            catch (Exception e)
            {
                gameIDTextBox.Text = _ERROR_UNABLE_TO_RETRIEVE_VALUE;
                // Console.WriteLine(e);
            }
            
        }

        private void ManualRegionCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (manualRegionCheckBox.Checked)
            {
                newRegionStringTextBox.Enabled = true;
                newRegionCodeTextBox.Enabled = true;
            }
            else
            {
                newRegionStringTextBox.Enabled = false;
                newRegionCodeTextBox.Enabled = false;
            }
        }

        private void PatchIsoButtonClick(object sender, EventArgs e)
        {

            UpdateRegionString();
            
            UpdateRegionCode();

            if (changeBootSettingCheckBox.Checked)
            {
                ChangeBootSettings();
            }
            
            if (disableUpdatePartCheckBox.Checked)
            {
                DisableUpdatePartition();
            }
            
            MessageBox.Show(@"Patching completed.", @"WiiSoFree", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void UpdateRegionString()
        {
            // Write the new region string
            // 0x4E010 (319504d) - 0x4E01F (319519d)

            try
            {
                string newRegionString = newRegionStringTextBox.Text;
                string newRegionString2 = newRegionString.Replace(" ", "");
            
                string[] newRegionStringArray = new string[16];
                byte[] newRegionByteArray = new byte[16];
            
                int x = 0;
                int y = 0;
                while (x < 16)
                {
                    // Convert string to string array
                    newRegionStringArray[x] = newRegionString2[y].ToString() + newRegionString2[y + 1];
                    x++;
                    y += 2;
                }

                for (x = 0; x < newRegionStringArray.Length; x++)
                {
                    // neccessary for converting, e.g. 80h = 128
                    newRegionByteArray[x] = byte.Parse(newRegionStringArray[x], System.Globalization.NumberStyles.HexNumber);
                }
            
                FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Write);
                fileStream.Seek(319504, SeekOrigin.Begin);   // Seek to 319491th byte in the file
                x = 0;
                while (x < 16)
                {
                    // writebyte writes the hex value
                    fileStream.WriteByte(newRegionByteArray[x]);
                    x++;
                }

                fileStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateRegionCode()
        {
            try
            {
                string newRegionCodeString = WSFConstants.Region1;
                switch (newRegionCodeTextBox.Text)
                {
                    case WSFConstants.Region0:
                        newRegionCodeString = WSFConstants.Region0; //jap
                        break;
                    case WSFConstants.Region1:
                        newRegionCodeString = WSFConstants.Region1; //usa
                        break;
                    case WSFConstants.Region2:
                        newRegionCodeString = WSFConstants.Region2; //pal
                        break;
                    default:
                        newRegionCodeString = WSFConstants.Region1; //usa
                        break;
                }

                byte newRegionCodeByteArray = Convert.ToByte(newRegionCodeString);

                // Write the new region code
                FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Write);
                fileStream.Seek(319491, SeekOrigin.Begin);   // Seek to 319491th byte in the file
                fileStream.WriteByte(newRegionCodeByteArray);

                fileStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangeBootSettings()
        {

            try
            {
                string bootSetting = "";

                switch (changeBootSettingComboBox.SelectedIndex)
                {
                    case 0:
                        bootSetting = "82"; // ASCII code 82 = R
                        break;
                    case 1:
                        bootSetting = "83"; // ASCII code 83 = S
                        break;
                    case 2:
                        bootSetting = "71"; // ASCII code 71 = G
                        break;
                    case 3:
                        bootSetting = "85"; // ASCII code 85 = U
                        break;
                    case 4:
                        bootSetting = "68"; // ASCII code 68 = D
                        break;
                    case 5:
                        bootSetting = "80"; // ASCII code 80 = P
                        break;
                    case 6:
                        bootSetting = "48"; // ASCII code 48 = 0 
                        break;
                    case 7:
                        bootSetting = "49"; // ASCII code 49 = 1
                        break;
                    case 8:
                        bootSetting = "52"; // ASCII code 52 = 4
                        break;
                    case 9:
                        bootSetting = "95"; // ASCII code 95 = _
                        break;
                    case 10:
                        bootSetting = "72"; // ASCII code 95 = H
                        break;
                    case 11:
                        bootSetting = "70"; // ASCII code 70 = F
                        break;
                    case 12:
                        bootSetting = "74"; // ASCII code 74 = J
                        break;
                    case 13:
                        bootSetting = "77"; // ASCII code 77 = M
                        break;
                    case 14:
                        bootSetting = "84"; // ASCII code 84 = T
                        break;
                    case 15:
                        bootSetting = "78"; // ASCII code 78 = N
                        break;
                    default:
                        bootSetting = "82"; // ASCII code 82 = R
                        break;
                }

                // necessary for converting, e.g. 82h = 130
                byte bootSettingByte = byte.Parse(bootSetting);

                FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Write);
                fileStream.Seek(0, SeekOrigin.Begin);   // Seek to 1st byte in the file

                // writebyte writes the hex value, 130 becomes 82
                fileStream.WriteByte(bootSettingByte);

                fileStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableUpdatePartition()
        {
            // This is currently a blind patch, it just modifies the data at 
            // original
            // 000040020 00 01 40 00 00 00 00 01
            // modifies to
            // 000040020 00 00 00 00 00 00 00 00

            try
            {
                int x = 0;
                byte[] disableUpdatePartitionByteArray = new byte[8];

                while (x < 8)
                {
                    disableUpdatePartitionByteArray[x] = Convert.ToByte(null);
                    x++;
                }

                FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Write);
                fileStream.Seek(262176, SeekOrigin.Begin);   // Seek to 262176th byte in the file

                x = 0;
                while (x < 8)
                {
                    fileStream.WriteByte(disableUpdatePartitionByteArray[x]);
                    x++;
                }

                fileStream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Console.WriteLine(e);
            }
        }

        private void DiscInfoButtonClick(object sender, EventArgs e)
        {
            WiiDiscPartitionsForm wiiDiscPartitionsForm = new WiiDiscPartitionsForm
            {
                FilenameF2TextBoxValue = filenameTextBox.Text
            };

            wiiDiscPartitionsForm.ShowDialog();
            
        }

        private void OpenWadToolStripMenuItemClick(object sender, EventArgs e)
        {
                WadForm wadForm = new WadForm();
                wadForm.ShowDialog();  
        }

    }
}
