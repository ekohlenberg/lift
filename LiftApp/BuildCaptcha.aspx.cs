using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace liftprayer
{
    public partial class BuildCaptcha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap objBMP = new Bitmap(60, 20);
            Graphics objGraphics = Graphics.FromImage(objBMP);

            objGraphics.Clear(Color.Wheat);
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

            //-- configure font to use for text in image
            Font objFont = new Font("Arial", 8, FontStyle.Italic);
            string captchaValue = "";
            char[] myArray = new char[5];
            int x;

            //-- generate a random character and add it to our string
            Random autoRand = new Random();

            for (x = 0; x < 5; x++)
            {
                myArray[x] = System.Convert.ToChar(autoRand.Next(65, 90));
                captchaValue += (myArray[x].ToString());
            }

            //-- add the CAPTCHA string value to the session (to be compared later)
            Session.Add("captchaValue", captchaValue);

            //-- write out the text as an image
            objGraphics.DrawString(captchaValue, objFont, Brushes.Red, 3, 3);

            //-- set the content type and return the image
            Response.ContentType = "image/GIF";
            objBMP.Save(Response.OutputStream, ImageFormat.Gif);
            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();
        }
    }
}
