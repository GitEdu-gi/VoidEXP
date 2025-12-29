using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Vanara.PInvoke.User32;
using static Vanara.PInvoke.Kernel32;
using static Vanara.PInvoke.Gdi32;
using Vanara.PInvoke;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VoidEXP
{
    class RGBImport
    {
        [DllImport("gdi32.dll")]
        public static extern int SetDIBitsToDevice(IntPtr hdc, int XDest, int YDest, uint
dwWidth, uint dwHeight, int XSrc, int YSrc, uint uStartScan, uint cScanLines,
byte[] lpvBits, [In] ref BITMAPINFO lpbmi, uint fuColorUse);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        public static int w = GetSystemMetrics(0);
        public static int h = GetSystemMetrics((SystemMetric)1);

        public struct rgb
        {
            public byte r;
            public byte g;
            public byte b;
            public rgb(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
            public rgb(RGBQUAD rgbq)
            {
                r = rgbq.rgbRed;
                g = rgbq.rgbGreen;
                b = rgbq.rgbBlue;
            }
            public rgb(Color rgbcol)
            {
                r = rgbcol.R;
                g = rgbcol.G;
                b = rgbcol.B;
            }
            public rgb(int rgbwin32)
            {
                r = (byte)(rgbwin32 & 255);
                g = (byte)(rgbwin32 >> 8 & 255);
                b = (byte)(rgbwin32 >> 16 & 255);
            }
            public rgb(hsv hsv)
            {
                if (((hsv.h >= 0) && (hsv.h < 360)) && ((hsv.s >= 0) && (hsv.s <= 1)) && ((hsv.v >= 0) && (hsv.v <= 1)))
                {
                    double C = hsv.v * hsv.s;
                    double X = C * (1 - Math.Abs(((hsv.h / 60d) % 2) - 1));
                    double m = hsv.v - C;
                    double Rp = 0;
                    double Gp = 0;
                    double Bp = 0;
                    if ((hsv.h >= 0) && (hsv.h < 60))
                    {
                        Rp = C;
                        Gp = X;
                        Bp = 0;
                    }
                    else if ((hsv.h >= 60) && (hsv.h < 120))
                    {
                        Rp = X;
                        Gp = C;
                        Bp = 0;
                    }
                    else if ((hsv.h >= 120) && (hsv.h < 180))
                    {
                        Rp = 0;
                        Gp = C;
                        Bp = X;
                    }
                    else if ((hsv.h >= 180) && (hsv.h < 240))
                    {
                        Rp = 0;
                        Gp = X;
                        Bp = C;
                    }
                    else if ((hsv.h >= 240) && (hsv.h < 300))
                    {
                        Rp = X;
                        Gp = 0;
                        Bp = C;
                    }
                    else if ((hsv.h >= 300) && (hsv.h < 360))
                    {
                        Rp = C;
                        Gp = 0;
                        Bp = X;
                    }
                    r = (byte)((Rp + m) * 255);
                    g = (byte)((Gp + m) * 255);
                    b = (byte)((Bp + m) * 255);
                }
                else
                {
                    r = 0;
                    g = 0;
                    b = 0;
                }
            }
            public struct hsv
            {
                public double h;
                public double s;
                public double v;
                public hsv(double h, double s, double l)
                {
                    this.h = h;
                    this.s = s;
                    v = l;
                }
                public hsv(hsv k)
                {
                    h = k.h;
                    s = k.s;
                    v = k.v;
                }
                public hsv(rgb rgb)
                {
                    double R = rgb.r / 255d;
                    double G = rgb.g / 255d;
                    double B = rgb.b / 255d;
                    double CM = Math.Max(Math.Max(R, G), B);
                    double Cm = Math.Min(Math.Min(R, G), B);
                    double DELTA = CM - Cm;
                    double HUE = 0;
                    double SAT = 0;
                    double VAL = 0;
                    if (DELTA == 0)
                    {
                        HUE = 0;
                    }
                    else
                    {
                        if (CM == R)
                        {
                            HUE = 60 * ((G - B) / DELTA % 6);
                        }
                        else if (CM == G)
                        {
                            HUE = 60 * ((B - R) / DELTA + 2);
                        }
                        else if (CM == B)
                        {
                            HUE = 60 * ((R - G) / DELTA + 4);
                        }
                    }
                    SAT = CM == 0 ? 0 : (DELTA / CM);
                    VAL = CM;
                    h = HUE;
                    s = SAT;
                    v = VAL;
                }
                public hsv addhue(double hue)
                {
                    h += hue;
                    h %= 360;
                    h = Math.Abs(h);
                    return this;
                }
                public hsv changehue(double hue)
                {
                    h = hue;
                    h %= 360;
                    h = Math.Abs(h);
                    return this;
                }
                public hsv changebright(double hue)
                {
                    v = Math.Abs(fmod(hue, 1d));

                    return this;
                }
                public hsv changeSAT(double sat)
                {
                    s = Math.Abs(fmod(sat, 1d));
                    return this;
                }

            }
            public static double fmod(double xx, double yy)
            {
                long xxx = (long)(xx * 10000000);
                long yyy = (long)(yy * 10000000);
                xxx %= yyy;
                return xxx / 10000000d;
            }
            public static Bitmap getScreen()
            {
                try
                {
                    Bitmap bmp = new Bitmap(w, h); ;
                    Graphics.FromImage(bmp).CopyFromScreen(0, 0, 0, 0, bmp.Size);
                    return bmp;
                }
                catch { }
                return null;
            }
        }
    }
}
