using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace IRIS.Presentation.Helpers
{
    public static class FontManager
    {
        public static PrivateFontCollection PoppinsCollection = new PrivateFontCollection();

        public static void LoadFonts()
        {

            string fontPath = Path.Combine(Application.StartupPath, "Fonts");
            string regularFont = Path.Combine(fontPath, "Poppins-Regular.ttf");
            string boldFont = Path.Combine(fontPath, "Poppins-Bold.ttf");

            if (File.Exists(regularFont))
                PoppinsCollection.AddFontFile(regularFont);

            if (File.Exists(boldFont))
                PoppinsCollection.AddFontFile(boldFont);
        }

        public static void ApplyFont(Control parentControl)
        {
            if (PoppinsCollection.Families.Length == 0) return; 

            parentControl.Font = new Font(PoppinsCollection.Families[0], parentControl.Font.Size, parentControl.Font.Style);
            foreach (Control child in parentControl.Controls)
            {
                ApplyFont(child);
            }
        }
    }
}