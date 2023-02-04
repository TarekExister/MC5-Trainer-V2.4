using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MC5Trainer
{
    public static class Memory
    {
        public static IntPtr handle = IntPtr.Zero;
        private static int pid = 0;

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, Int32 nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);


        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys _key);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        public static bool AttachProcess(string processName)
        {

            Process[] processList = Process.GetProcesses();
            foreach (Process p in processList)
            {
                if (p.ProcessName.Equals(processName))
                {
                    pid = p.Id;
                    handle = OpenProcess(ProcessAccessFlags.All, false, pid);
                    return true;
                }
            }


            return false;
        }

        public static int GetProcessID(string ProcessName)
        {
            Process[] ProcessList = Process.GetProcesses();
            int ProcessID = 0;
            foreach (Process _this_process in ProcessList)
            {
                if (ProcessName == _this_process.ProcessName) { ProcessID = _this_process.Id; }
            }
            return ProcessID;
        }

        public static IntPtr GetModuleAddress(string processName, string moduleName)
        {
            IntPtr modAddress = IntPtr.Zero;
            Process[] procList = Process.GetProcessesByName(processName);
            Process pModule = procList[0];
            foreach (ProcessModule module in pModule.Modules)
            {
                if (module.ModuleName.Equals(moduleName.Insert(moduleName.Length, ".exe")))
                {
                    modAddress = module.BaseAddress;
                }
            }
            return modAddress;
        }

        public static IntPtr GetUWPWindowHandle()
        {
            Process[] procs = Process.GetProcessesByName("ApplicationFrameHost");
            return procs[0].MainWindowHandle;
        }

        public static byte[] ReadBytes(IntPtr address, int size)
        {
            byte[] bts = new byte[size];
            IntPtr readbts = IntPtr.Zero;
            ReadProcessMemory(handle, address, bts, size, out readbts);
            return bts;
        }

        public static void WriteBytes(IntPtr address, byte[] buffer, int size)
        {
            IntPtr readbts = IntPtr.Zero;
            WriteProcessMemory(handle, address, buffer, size, out readbts);
        }

        unsafe public static IntPtr GetPointerAddress(IntPtr BaseAddress, int[] offsets, int PointerLevel)
        {
            IntPtr address = BaseAddress;
            IntPtr tmp = IntPtr.Zero;

            for (int x = 0; x < PointerLevel; x++)
            {
                byte[] buffer = ReadBytes(address, sizeof(IntPtr));
                tmp = (IntPtr)(BitConverter.ToInt32(buffer, 0));
                address = tmp + offsets[x];
            }
            return address;
        }

        unsafe public static IntPtr GetPointerAddress64Bit(IntPtr BaseAddress, int[] offsets, int PointerLevel)
        {
            IntPtr address = BaseAddress;
            IntPtr tmp = IntPtr.Zero;

            for (int x = 0; x < PointerLevel; x++)
            {
                byte[] buffer = ReadBytes(address, sizeof(IntPtr));
                tmp = (IntPtr)(BitConverter.ToInt64(buffer, 0));
                address = tmp + offsets[x];
            }
            return address;
        }

        unsafe public static T Read<T>(IntPtr address)
        {
            object val = 0;

            if (typeof(T) == typeof(int))
            {
                val = BitConverter.ToInt32(ReadBytes(address, sizeof(int)), 0);
            }
            else if (typeof(T) == typeof(uint))
            {
                val = BitConverter.ToUInt32(ReadBytes(address, sizeof(uint)), 0);
            }
            else if (typeof(T) == typeof(long))
            {
                val = BitConverter.ToInt64(ReadBytes(address, sizeof(long)), 0);
            }
            else if (typeof(T) == typeof(byte))
            {
                val = ReadBytes(address, sizeof(byte))[0];
            }
            else if (typeof(T) == typeof(IntPtr))
            {
                val = BitConverter.ToUInt32(ReadBytes(address, sizeof(IntPtr)), 0);
            }
            else if (typeof(T) == typeof(UIntPtr))
            {
                val = BitConverter.ToUInt64(ReadBytes(address, sizeof(UIntPtr)), 0);
            }
            else if (typeof(T) == typeof(float))
            {
                val = BitConverter.ToSingle(ReadBytes(address, sizeof(float)), 0);
            }
            else if (typeof(T) == typeof(double))
            {
                val = BitConverter.ToDouble(ReadBytes(address, sizeof(double)), 0);
            }

            return (T)Convert.ChangeType(val, typeof(T));
        }


        unsafe public static void Write<T>(IntPtr address, T value)
        {

            if (typeof(T) == typeof(int))
            {
                int val = (int)Convert.ChangeType(value, typeof(int));
                byte[] buffer = BitConverter.GetBytes(val);
                WriteBytes(address, buffer, sizeof(int));

            }
            else if (typeof(T) == typeof(uint))
            {
                uint val = (uint)Convert.ChangeType(value, typeof(uint));
                byte[] buffer = BitConverter.GetBytes(val);
                WriteBytes(address, buffer, sizeof(uint));
            }
            else if (typeof(T) == typeof(long))
            {
                long val = (long)Convert.ChangeType(value, typeof(long));
                byte[] buffer = BitConverter.GetBytes(val);
                WriteBytes(address, buffer, sizeof(long));
            }
            else if (typeof(T) == typeof(float))
            {
                float val = (float)Convert.ChangeType(value, typeof(float));
                byte[] buffer = BitConverter.GetBytes(val);
                WriteBytes(address, buffer, sizeof(float));
            }
            else if (typeof(T) == typeof(double))
            {
                double val = (double)Convert.ChangeType(value, typeof(double));
                byte[] buffer = BitConverter.GetBytes(val);
                WriteBytes(address, buffer, sizeof(double));
            }
            else if (typeof(T) == typeof(byte))
            {
                byte val = (byte)Convert.ChangeType(value, typeof(byte));
                byte[] buffer = BitConverter.GetBytes(val);
                WriteBytes(address, buffer, sizeof(byte));
            }
            else if (typeof(T) == typeof(IntPtr))
            {
                IntPtr val = (IntPtr)Convert.ChangeType(value, typeof(IntPtr));
                byte[] buffer = BitConverter.GetBytes((uint)val);
                WriteBytes(address, buffer, sizeof(IntPtr));
            }
            else if (typeof(T) == typeof(UIntPtr))
            {
                UIntPtr val = (UIntPtr)Convert.ChangeType(value, typeof(UIntPtr));
                byte[] buffer = BitConverter.GetBytes((uint)val);
                WriteBytes(address, buffer, sizeof(UIntPtr));
            }
        }
    }
}
