using System.Drawing;

namespace OpenCvSharp
{
    public static class Extensions
    {
        public static Bitmap ToBitmap(this Mat mat)
        {
            using (var ms = mat.ToMemoryStream())
            {
                return (Bitmap)Image.FromStream(ms);
            }
        }

    }
}