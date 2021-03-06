﻿#region Usings
using System;
using System.IO;
using System.Windows.Forms;
#endregion

#region Icon Loader

public class IconLoader
{
    private OpenFileDialog openFileDialog1;
    private string installOutcome = "";

    public IconLoader()
    {
        openFileDialog1 = new OpenFileDialog()
        {
            FileName = "Select an icon .png file",
            Filter = "Portable Network Graphics image (*.png)|*.png",
            Title = "Open icon .png"
        };
    }

    public void setInstallOutcome(string text)
    {
        installOutcome = text;
    }

    public string getInstallOutcome()
    {
        return installOutcome;
    }

    public void LoadImage()
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                using (Stream str = openFileDialog1.OpenFile())
                {
                    using (Stream output = new FileStream(GlobalPaths.extradir + "\\icons\\" + GlobalVars.UserConfiguration.PlayerName + ".png", FileMode.Create))
                    {
                        byte[] buffer = new byte[32 * 1024];
                        int read;

                        while ((read = str.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, read);
                        }
                    }

                    str.Close();
                }

                installOutcome = "Icon " + openFileDialog1.SafeFileName + " installed!";
            }
            catch (Exception ex)
            {
                installOutcome = "Error when installing icon: " + ex.Message;
            }
        }
    }
}
#endregion