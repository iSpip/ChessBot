using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessBot
{
    public static class Images
    {
        private static readonly Dictionary<int, ImageSource> imageSources = new()
        {
            { 1, LoadImage("Assets/pieceImages/1.png")},
            { 2, LoadImage("Assets/pieceImages/2.png")},
            { 3, LoadImage("Assets/pieceImages/3.png")},
            { 4, LoadImage("Assets/pieceImages/4.png")},
            { 5, LoadImage("Assets/pieceImages/5.png")},
            { 6, LoadImage("Assets/pieceImages/6.png")},
            { 9, LoadImage("Assets/pieceImages/9.png")},
            { 10, LoadImage("Assets/pieceImages/10.png")},
            { 11, LoadImage("Assets/pieceImages/11.png")},
            { 12, LoadImage("Assets/pieceImages/12.png")},
            { 13, LoadImage("Assets/pieceImages/13.png")},
            { 14, LoadImage("Assets/pieceImages/14.png")}
        };

        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(int piece)
        {
            if (piece == 0)
            {
                return null;
            }

            return imageSources[piece];
        }
    }
}
