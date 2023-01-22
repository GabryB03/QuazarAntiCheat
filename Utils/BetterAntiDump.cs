using System;
using System.Runtime.InteropServices;

class BetterAntiDump
{
    [DllImport("kernel32.dll")]
    private static extern unsafe bool VirtualProtect(byte* lpAddress, int dwSize, uint flNewProtect, out uint lpflOldProtect);

    public static unsafe void Initialize()
    {
        uint AnY;
        var module = typeof(AntiDumpRun).Module;
        var bas = (byte*)Marshal.GetHINSTANCE(module);
        var ptr = bas + 0x3c;
        byte* ptr2;
        ptr = ptr2 = bas + *(uint*)ptr;
        ptr += 0x6;
        var sectNum = *(ushort*)ptr;
        ptr += 14;
        var optSize = *(ushort*)ptr;
        ptr = ptr2 = ptr + 0x4 + optSize;
        byte* @new = stackalloc byte[11];
        VirtualProtect(ptr - 16, 8, 0x40, out AnY);
        *(uint*)(ptr - 12) = 0;
        var mdDir = bas + *(uint*)(ptr - 16);
        *(uint*)(ptr - 16) = 0;
        VirtualProtect(mdDir, 0x48, 0x40, out AnY);
        var mdHdr = bas + *(uint*)(mdDir + 8);
        *(uint*)mdDir = 0;
        *((uint*)mdDir + 1) = 0;
        *((uint*)mdDir + 2) = 0;
        *((uint*)mdDir + 3) = 0;
        VirtualProtect(mdHdr, 4, 0x40, out AnY);
        *(uint*)mdHdr = 0;
        for (int i = 0; i < sectNum; i++)
        {
            VirtualProtect(ptr, 8, 0x40, out AnY);
            Marshal.Copy(new byte[8], 0, (IntPtr)ptr, 8);
            ptr += 0x28;
        }
    }
}