using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Runtime;
public partial class MainForm : Form
{
    [DllImport("psapi.dll")]
    static extern int EmptyWorkingSet(IntPtr hwProc);

    [DllImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    static extern int OutputDebugString(string str);

    [DllImport("ntdll.dll")]
    internal static extern NtStatus NtSetInformationThread(IntPtr ThreadHandle, ThreadInformationClass ThreadInformationClass, IntPtr ThreadInformation, int ThreadInformationLength);

    [DllImport("kernel32.dll")]
    static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

    [DllImport("kernel32.dll")]
    static extern uint SuspendThread(IntPtr hThread);

    [DllImport("kernel32.dll")]
    static extern int ResumeThread(IntPtr hThread);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern bool CloseHandle(IntPtr handle);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtQueryInformationProcess([In] IntPtr ProcessHandle, [In] PROCESSINFOCLASS ProcessInformationClass, out IntPtr ProcessInformation, [In] int ProcessInformationLength, [Optional] out int ReturnLength);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtClose([In] IntPtr Handle);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtRemoveProcessDebug(IntPtr ProcessHandle, IntPtr DebugObjectHandle);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtSetInformationDebugObject([In] IntPtr DebugObjectHandle, [In] DebugObjectInformationClass DebugObjectInformationClass, [In] IntPtr DebugObjectInformation, [In] int DebugObjectInformationLength, [Out][Optional] out int ReturnLength);

    [DllImport("ntdll.dll", SetLastError = true, ExactSpelling = true)]
    internal static extern NtStatus NtQuerySystemInformation([In] SYSTEM_INFORMATION_CLASS SystemInformationClass, IntPtr SystemInformation, [In] int SystemInformationLength, [Out][Optional] out int ReturnLength);

    static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] ref bool isDebuggerPresent);

    [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsDebuggerPresent();

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    public static ProtectedInteger integerValue = new ProtectedInteger(53729);
    public static ProtectedString stringValue = new ProtectedString("Hello!");
    public static ProtectedInteger moduleNumber = new ProtectedInteger(0);
    public static ProtectedFloat floatValue = new ProtectedFloat(8753.273F);
    public static ProtectedDecimal decimalValue = new ProtectedDecimal(5733.22227M);
    public static ProtectedDouble doubleValue = new ProtectedDouble(7555.89311D);
    public static ProtectedBoolean booleanValue = new ProtectedBoolean(true);
    public static ProtectedByte byteValue = new ProtectedByte(55);
    public static ProtectedChar charValue = new ProtectedChar('z');
    public static ProtectedShort shortValue = new ProtectedShort(short.Parse("12333"));
    public static ProtectedLong longValue = new ProtectedLong(746351);
    public static ProtectedUShort UShortValue = new ProtectedUShort(ushort.Parse("31771"));
    public static ProtectedUInt UIntValue = new ProtectedUInt(437214);
    public static ProtectedULong ULongValue = new ProtectedULong(581922);
    public static ProtectedSByte SByteValue = new ProtectedSByte(111);

    private static string[] GetArray()
    {
        return new string[] { "procmon64", "codecracker", "x96dbg", "pizza", "OLLYDBG", "idaq", "idaq64",  "pepper", "reverse", "reversal", "de4dot", "pc-ret", "crack", "ILSpy", "x32dbg", "sharpod", "x64dbg", "x32_dbg", "x64_dbg", "debug", "dbg", "strongod", "PhantOm", "titanHide", "scyllaHide", "ilspy", "graywolf", "simpleassemblyexplorer", "MegaDumper", "megadumper", "X64NetDumper", "x64netdumper", "HxD", "hxd", "PETools", "petools", "Protection_ID", "protection_id", "die", "process hacker 2", "process", "hacker", "ollydbg", "x32dbg", "x64dbg", "ida -", "charles", "dnspy", "simpleassembly", "peek", "httpanalyzer", "httpdebug", "fiddler", "wireshark", "proxifier", "mitmproxy", "process hacker", "process monitor", "process hacker 2", "system explorer", "systemexplorer", "systemexplorerservice", "WPE PRO", "ghidra", "folderchangesview", "pc-ret", "folder", "dump", "proxy", "de4dotmodded", "StringDecryptor", "Centos", "SAE", "monitor", "brute", "checker", "zed", "sniffer", "http", "debugger", "james", "exeinfope", "codecracker", "x32dbg", "x64dbg", "ollydbg", "ida -", "charles", "dnspy", "simpleassembly", "peek", "httpanalyzer", "httpdebug", "fiddler", "wireshark", "dbx", "mdbg", "gdb", "windbg", "dbgclr", "kdb", "kgdb", "mdb", "sandboxierpcss", "e", "ProcessHacker", "FSociety" };
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr ZeroMemory(IntPtr addr, IntPtr size);

    [DllImport("kernel32.dll")]
    private static extern IntPtr VirtualProtect(IntPtr lpAddress, IntPtr dwSize, IntPtr flNewProtect, ref IntPtr lpflOldProtect);

    public static void RemovePEHeader()
    {
        IntPtr baseAddress = GetModuleHandle("speedhack-i386.dll");
        VirtualProtect(baseAddress, (IntPtr)4096, (IntPtr)0x04, ref baseAddress);
        ZeroMemory(baseAddress, (IntPtr)4096);
    }

    public MainForm()
    {
        InitializeComponent();
        CheckForIllegalCrossThreadCalls = false;
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

        new Thread(new ThreadStart(clearRam)).Start();
        new Thread(new ThreadStart(antiDebug)).Start();
        new Thread(new ThreadStart(hideFromDebugger)).Start();
        new Thread(new ThreadStart(antiSandbox)).Start();
        new Thread(new ThreadStart(badModule)).Start();
        new Thread(new ThreadStart(badProcess)).Start();
        new Thread(new ThreadStart(badHooks)).Start();
        new Thread(new ThreadStart(moduleDefiner)).Start();
        new Thread(new ThreadStart(memoryModify)).Start();
        new Thread(new ThreadStart(timeModify)).Start();
    }

    public void timeModify()
    {
        while (true)
        {
            long tickCount = Environment.TickCount;
            Thread.Sleep(500);
            long tickCount2 = Environment.TickCount;

            if (((tickCount2 - tickCount) > 600L))
            {
                label18.Text = "DETECTED";
                label18.ForeColor = Color.Green;
            }
            else
            {
                label18.Text = "NOT DETECTED";
                label18.ForeColor = Color.Red;
            }
        }
    }

    public void memoryModify()
    {
        while (true)
        {
            Thread.Sleep(250);

            if (integerValue.IsViolated() || stringValue.IsViolated() || moduleNumber.IsViolated() || floatValue.IsViolated() || decimalValue.IsViolated() || doubleValue.IsViolated() || booleanValue.isViolated() || byteValue.IsViolated() || charValue.isViolated() || shortValue.IsViolated() || longValue.IsViolated() || UShortValue.IsViolated() || UIntValue.IsViolated() || SByteValue.IsViolated() || ULongValue.IsViolated())
            {
                label16.Text = "DETECTED";
                label16.ForeColor = Color.Green;
            }
            else
            {
                label16.Text = "NOT DETECTED";
                label16.ForeColor = Color.Red;
            }
        }
    }

    public void moduleDefiner()
    {
        Thread.Sleep(1250);
        moduleNumber.SetValue(Process.GetCurrentProcess().Modules.Count);
        moduleChecker();
    }

    public void moduleChecker()
    {
        while (true)
        {
            Thread.Sleep(250);

            if (Process.GetCurrentProcess().Modules.Count != moduleNumber.GetValue())
            {
                MessageBox.Show(Process.GetCurrentProcess().Modules[Process.GetCurrentProcess().Modules.Count].FileName);
                label14.Text = "DETECTED";
                label14.ForeColor = Color.Green;
            }
            else
            {
                label14.Text = "NOT DETECTED";
                label14.ForeColor = Color.Red;
            }
        }
    }

    public void clearRam()
    {
        while (true)
        {
            Thread.Sleep(100);
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            GCSettings.LargeObjectHeapCompactionMode = GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect(GC.MaxGeneration);
            GC.WaitForPendingFinalizers();
            SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, (UIntPtr)0xFFFFFFFF, (UIntPtr)0xFFFFFFFF);

            label11.Text = "Integer value: " + integerValue.GetValue().ToString();
            label12.Text = "String value: " + stringValue.GetValue().ToString();
            label19.Text = "Float value: " + floatValue.GetValue().ToString();
            label20.Text = "Decimal value: " + decimalValue.GetValue().ToString();
            label21.Text = "Double value: " + doubleValue.GetValue().ToString();
            label22.Text = "Boolean value: " + booleanValue.GetValue().ToString();
            label23.Text = "Byte value: " + byteValue.GetValue().ToString();
            label24.Text = "Char value: " + charValue.GetValue().ToString();
            label25.Text = "Short value: " + shortValue.GetValue().ToString();
            label26.Text = "Long value: " + longValue.GetValue().ToString();
            label27.Text = "Unsigned short: " + UShortValue.GetValue().ToString();
            label28.Text = "Unsigned int: " + UIntValue.GetValue().ToString();
            label29.Text = "Unsigned long: " + ULongValue.GetValue().ToString();
            label30.Text = "Signed byte: " + SByteValue.GetValue().ToString();
        }
    }

    public void badHooks()
    {
        while (true)
        {
            Thread.Sleep(250);
            bool found = false;
            IntPtr kernel32 = LoadLibrary("kernel32.dll");
            IntPtr GetProcessId = GetProcAddress(kernel32, "IsDebuggerPresent");
            byte[] data = new byte[1];
            Marshal.Copy(GetProcessId, data, 0, 1);

            if (data[0] == 0xE9)
            {
                found = true;
            }

            GetProcessId = GetProcAddress(kernel32, "CheckRemoteDebuggerPresent");
            data = new byte[1];
            Marshal.Copy(GetProcessId, data, 0, 1);

            if (data[0] == 0xE9)
            {
                found = true;
            }

            GetProcessId = GetProcAddress(kernel32, "WriteProcessMemory");
            data = new byte[1];
            Marshal.Copy(GetProcessId, data, 0, 1);

            if (data[0] == 0xE9)
            {
                found = true;
            }

            GetProcessId = GetProcAddress(kernel32, "ReadProcessMemory");
            data = new byte[1];
            Marshal.Copy(GetProcessId, data, 0, 1);

            if (data[0] == 0xE9)
            {
                found = true;
            }

            if (found)
            {
                label10.Text = "DETECTED";
                label10.ForeColor = Color.Green;
            }
            else
            {
                label10.Text = "NOT DETECTED";
                label10.ForeColor = Color.Red;
            }
        }
    }

    public void badProcess()
    {
        while (true)
        {
            Thread.Sleep(250);
            bool found = false;

            foreach (Process process in Process.GetProcesses())
            {
                try
                {
                    foreach (string proc in GetArray())
                    {
                        if ((process.ProcessName == proc || process.ProcessName.ToLower().Contains("cheat") || process.ProcessName.ToLower().Contains("exploit") || process.ProcessName.ToLower().Contains("clicker")) && process.Id != Process.GetCurrentProcess().Id)
                        {
                            found = true;
                        }
                    }
                }
                catch
                {

                }
            }

            if (found)
            {
                label8.Text = "DETECTED";
                label8.ForeColor = Color.Green;
            }
            else
            {
                label8.Text = "NOT DETECTED";
                label8.ForeColor = Color.Red;
            }
        }
    }

    public void hideFromDebugger()
    {
        Thread.Sleep(1250);
        AntiDumpRun.AntiDump();
        HideOSThreads();
        RemovePEHeader();
    }

    public void badModule()
    {
        while (true)
        {
            Thread.Sleep(250);
            bool found = false;

            if (GetModuleHandle("speedhack-i386.dll").ToInt32() != 0)
            {
                found = true;
            }
            else if (GetModuleHandle("speedhack-i386.dll").ToInt32() != 0)
            {
                found = true;
            }
            else if (GetModuleHandle("ffffffffffffff.dll").ToInt32() != 0)
            {
                found = true;
            }
            else if (GetModuleHandle("gggggggggggggggg.dll").ToInt32() != 0)
            {
                found = true;
            }

            foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
            {
                string moduleName = module.FileName.ToLower();

                if (moduleName.Contains("els.bin") || moduleName.Contains("cheat") || moduleName.Contains("dump") || moduleName.Contains("debug") || moduleName.Contains("trainer") || moduleName.Contains("glitch") || moduleName.Contains("hack") || moduleName.Contains("speed") || moduleName.Contains("exploit") || moduleName.Contains("bot") || moduleName.Contains("bypass") || moduleName.Contains("mod") || moduleName.Contains("inject"))
                {
                    if (moduleName != Application.ExecutablePath.ToLower())
                    {
                        found = true;
                        break;
                    }              
                }
            }

            if (found)
            {
                label6.Text = "DETECTED";
                label6.ForeColor = Color.Green;
            }
            else
            {
                label6.Text = "NOT DETECTED";
                label6.ForeColor = Color.Red;
            }
        }
    }

    public void antiSandbox()
    {
        while (true)
        {
            Thread.Sleep(250);
            bool found = false;

            if (GetModuleHandle("SbdieDll.dll").ToInt32() != 0)
            {
                found = true;
            }

            if (Process.GetCurrentProcess().MainWindowTitle.StartsWith("[#]") && Process.GetCurrentProcess().MainWindowTitle.EndsWith("[#]"))
            {
                found = true;
            }

            if (found)
            {
                label4.Text = "DETECTED";
                label4.ForeColor = Color.Green;
            }
            else
            {
                label4.Text = "NOT DETECTED";
                label4.ForeColor = Color.Red;
            }
        }
    }

    public void antiDebug()
    {
        while (true)
        {
            Thread.Sleep(250);
            bool found = false;

            if (CheckDebuggerManagedPresent())
            {
                found = true;
            }
            else if (CheckDebuggerUnmanagedPresent())
            {
                found = true;
            }
            else if (CheckRemoteDebugger())
            {
                found = true;
            }
            else if (CheckDebugPort())
            {
                found = true;
            }
            else if (DetachFromDebuggerProcess())
            {
                found = true;
            }
            else if (CheckKernelDebugInformation())
            {
                found = true;
            }
            else if (Debugger.IsLogging())
            {
                found = true;
            }
            else if (string.Compare(Environment.GetEnvironmentVariable("COR_ENABLE_PROFILING"), "1", StringComparison.Ordinal) == 0)
            {
                found = true;
            }
            else
            {
                if (Process.GetCurrentProcess().Handle == IntPtr.Zero)
                {
                    found = true;
                }
                if (OutputDebugString("") > IntPtr.Size)
                {
                    found = true;
                }
                try
                {
                    CloseHandle(IntPtr.Zero);
                }
                catch
                {
                    found = true;
                }
                if (GetModuleHandle("vehdebug-i386.dll").ToInt32() != 0)
                {
                    found = true;
                }
                else if (GetModuleHandle("vehdebug-x86_64.dll").ToInt32() != 0)
                {
                    found = true;
                }
            }

            if (found)
            {
                label2.Text = "DETECTED";
                label2.ForeColor = Color.Green;
            }
            else
            {
                label2.Text = "NOT DETECTED";
                label2.ForeColor = Color.Red;
            }
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        Process.GetCurrentProcess().Kill();
    }

    public static bool CheckDebuggerManagedPresent()
    {
        return Debugger.IsAttached;
    }

    public static bool CheckDebuggerUnmanagedPresent()
    {
        return IsDebuggerPresent();
    }

    public static bool CheckRemoteDebugger()
    {
        var isDebuggerPresent = false;
        var bApiRet = CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);

        return bApiRet && isDebuggerPresent;
    }

    public static bool CheckDebugPort()
    {
        NtStatus status;
        IntPtr DebugPort = new IntPtr(0);
        int ReturnLength;

        unsafe
        {
            status = NtQueryInformationProcess(Process.GetCurrentProcess().Handle, PROCESSINFOCLASS.ProcessDebugPort, out DebugPort, Marshal.SizeOf(DebugPort), out ReturnLength);

            if (status == NtStatus.Success)
            {
                return DebugPort == new IntPtr(-1);
            }
        }

        return false;
    }

    public static bool DetachFromDebuggerProcess()
    {
        IntPtr hDebugObject = INVALID_HANDLE_VALUE;
        var dwFlags = 0U;
        NtStatus ntStatus;
        int retLength_1;
        int retLength_2;

        unsafe
        {
            ntStatus = NtQueryInformationProcess(Process.GetCurrentProcess().Handle, PROCESSINFOCLASS.ProcessDebugObjectHandle, out hDebugObject, IntPtr.Size, out retLength_1);

            if (ntStatus != NtStatus.Success)
            {
                return false;
            }

            ntStatus = NtSetInformationDebugObject(hDebugObject, DebugObjectInformationClass.DebugObjectFlags, new IntPtr(&dwFlags), Marshal.SizeOf(dwFlags), out retLength_2);

            if (ntStatus != NtStatus.Success)
            {
                return false;
            }

            ntStatus = NtRemoveProcessDebug(Process.GetCurrentProcess().Handle, hDebugObject);

            if (ntStatus != NtStatus.Success)
            {
                return false;
            }

            ntStatus = NtClose(hDebugObject);

            if (ntStatus != NtStatus.Success)
            {
                return false;
            }
        }

        return true;
    }
    public static bool CheckKernelDebugInformation()
    {
        SYSTEM_KERNEL_DEBUGGER_INFORMATION pSKDI;
        int retLength;
        NtStatus ntStatus;

        unsafe
        {
            ntStatus = NtQuerySystemInformation(SYSTEM_INFORMATION_CLASS.SystemKernelDebuggerInformation, new IntPtr(&pSKDI), Marshal.SizeOf(pSKDI), out retLength);

            if (ntStatus == NtStatus.Success)
            {
                return pSKDI.KernelDebuggerEnabled && !pSKDI.KernelDebuggerNotPresent;
            }
        }

        return false;
    }

    public static void HideOSThreads()
    {
        ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;

        foreach (ProcessThread thread in currentThreads)
        {
            IntPtr pOpenThread = OpenThread(ThreadAccess.SET_INFORMATION, false, (uint)thread.Id);

            if (pOpenThread == IntPtr.Zero)
            {
                continue;
            }

            HideFromDebugger(pOpenThread);
            CloseHandle(pOpenThread);
        }
    }

    public static bool HideFromDebugger(IntPtr Handle)
    {
        NtStatus nStatus = NtSetInformationThread(Handle, ThreadInformationClass.ThreadHideFromDebugger, IntPtr.Zero, 0);

        return nStatus == NtStatus.Success;
    }
}