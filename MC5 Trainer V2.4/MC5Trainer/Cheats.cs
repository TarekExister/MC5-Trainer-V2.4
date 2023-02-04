using System;
using System.Drawing;
using System.Windows.Forms;

namespace MC5Trainer
{
    public static class Cheats
    {
        
        public static bool jump,ammo1,ammo2,hp/*,wallhack*/,teleport,grenades,autoshoot,aimfocus;
        public static IntPtr ModuleBase = IntPtr.Zero;

        private static void AccuracyEnable()
        {
            try
            {
                byte[] buffer = { 0xF3, 0x0F, 0x5C, 0xC0 };
                Memory.WriteBytes(ModuleBase + 0x2C23D0, buffer, 4);
            }
            catch { }
        }
        private static void AccuracyDisable()
        {
            try { 
            byte[] buffer = { 0xF3, 0x0F, 0x5C, 0xC2 };
            Memory.WriteBytes(ModuleBase + 0x2C23D0, buffer, 4);
            }
            catch { }
        }





        private static void WallBangDisable()
        {
            try
            {
                byte[] buffer = { 0x83, 0xf8, 0x08 };
                Memory.WriteBytes(ModuleBase + 0xACB756, buffer, 3);
            }
            catch { }
        }
        private static void WallBangEnable()
        {
            try
            {
                byte[] buffer = { 0x90, 0x90, 0x90 };
                Memory.WriteBytes(ModuleBase + 0xACB756, buffer, 3);
            }
            catch { }
        }
        private static void EPatchDisable()
        {
            try
            {
                byte[] buffer = { 0x41, 0x89, 0x5C, 0x08, 0x18 };
                Memory.WriteBytes(ModuleBase + 0x283BC2, buffer, 5);
                byte[] buffe2 = { 0x41 ,0x89 ,0x49 ,0x18 };
                Memory.WriteBytes(ModuleBase + 0x288A2D, buffe2, 4);
            }
            catch { }
        }
        private static void EPatchEnable()
        {
            try
            {
                byte[] buffer = { 0x90, 0x90, 0x90, 0x90, 0x90 };
                Memory.WriteBytes(ModuleBase + 0x283BC2, buffer, 5);
                byte[] buffe2 = { 0x90, 0x90, 0x90, 0x90 };
                Memory.WriteBytes(ModuleBase + 0x288A2D, buffe2, 4);
            }
            catch { }
        }

        private enum WeaponsId : uint
        {
            LavaGrinder = 0xC1B22220,
            GoldenEER15 = 0xBB101010,
            LavaERG10 = 0xBF804030,
            FireFly = 0x9DB01000,
            TONI = 0x86411000,
            goldenimp5 = 0xB9F05A30,
            candycaneimp5 = 0xCE505A30,
            goldenbosk3 = 0xB6E08939,
            goldenred34 = 0xB5A08939,
            platinumred34 = 0xC4208B38,
            jadestonekogv = 0xC2608B38,
            goldenseverance = 0xBBE01020,
            goldenjglt313 = 0xBBD01000,
            goldendbs4 = 0xBED01000,
            lavapr39 = 0xBFD01000,
            goldenshred4 = 0xBAA01000,
            goldenfs80 = 0xBA101000,
            MORTx = 0xB1101000,
            seringplag9model = 0xB1501000,
            sklrcc45 = 0xB1A01000,
            hawk1z = 0xB1C01000,
            l1n4588 = 0xB1D01000,
            thezapper = 0xB2301000,
            zombifiedred34 = 0xB3501000,
            zombifiedbosk3 = 0xB3F01000,
            goldenmesn = 0xB4F01000,
            goldensixmg = 0xB5501000,
            goldenlgr35 = 0xBD201000,
            jadestoneimp5 = 0xC2701000,
            icedbuckshot = 0xCE601000,
            icedspec38a = 0xCEC01000,
            icedbramson = 0xCFA01000,
            icedblomr = 0xCF101000,
            icedshred4 = 0xCFE01000,
            lavared34 = 0xCAA01000,
            jadestonebramstone = 0xCAF01000,
            overchargedlak = 0xCBA01000,
            poisonouskogv = 0xCCA01000,
            gingerbreader10 = 0xCDA01000,
            gingerbreadred34 = 0xCDB01000,
            lavadbs4 = 0xCC501000,
            lavabosk3 = 0xCA501000,
            overchargedjglt313 = 0xCA201000,
            pacificbsw77 = 0xC9A01000,
            bloodyetherl = 0xC9201000,
            bloodywhisperer = 0xC8A01000,
            pacifickogv = 0xC8201000,
            forestgrinder = 0xC7201000,
            forestvice = 0xC7A01000,
            jadestonebsw77 = 0xC6201000,
            jadestonewhisperer = 0xC6501000,
            zombifiedmicrocov = 0xC6A01000,
            goldenbasu = 0xB7A00000,
            goldenslak7h = 0xBE500000,
            kv4l = 0xB3000000,
            goldenkogv = 0xB9001110,
            theroaster = 0xB2700000,
            lavawes = 0xC0001110,
            jadestoneerg10 = 0xC3100000,
            lavabsw77 = 0xC0800000,
            goldengrinder = 0xC0E01110,
            platinumkogv = 0xC4301110,
            platinumimp5 = 0xC4E01110,
            lavabramson = 0xC5001110,
            lavaimp5 = 0xC5601110,
            jadestonecaspol = 0xC7000000,
            bloodydbs4 = 0xC9000000,
            gingerbreadrokk = 0xCD000000,
            icedseverance = 0xD0100000,
            flamedaestx84 = 0xD0D00000,
            flamedfirecracker = 0xD1500000,
            flamedlgr35 = 0xD1A00000,
            flamedtwig = 0xD2200000,
            flamedratog = 0xD2500000,
            flamedmrager = 0xD2A00000,
            goldensering9 = 0x8AF00000,
            goldenrokk = 0x8C000000

        }

        private static void AddWeapon(string PW,string SW,byte SlotId) 
        {
            IntPtr SlotsBase = ModuleBase + 0x1CB90E0;
            IntPtr PWAddress = IntPtr.Zero;
            IntPtr SWAddress = IntPtr.Zero;
            switch (SlotId) 
            {
                case 1:
                    PWAddress = Memory.GetPointerAddress64Bit(SlotsBase, new int[] {0x568,0x18 }, 2);
                    SWAddress = Memory.GetPointerAddress64Bit(SlotsBase, new int[] { 0x568, 0x40 }, 2);
                    break;
                case 2:
                    PWAddress = Memory.GetPointerAddress64Bit(SlotsBase, new int[] { 0x568, 0xf0 }, 2);
                    SWAddress = Memory.GetPointerAddress64Bit(SlotsBase, new int[] { 0x568, 0x118 }, 2);
                    break;
                case 3:
                    PWAddress = Memory.GetPointerAddress64Bit(SlotsBase, new int[] { 0x568, 0x1c8 }, 2);
                    SWAddress = Memory.GetPointerAddress64Bit(SlotsBase, new int[] { 0x568, 0x1f0 }, 2);
                    break;
            }
            switch (PW)
            {
                case "lava grinder":
                    Memory.Write<uint>(PWAddress,(uint) WeaponsId.LavaGrinder);
                    break;
                case "golden eer 15":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.GoldenEER15);
                    break;
                case "lava erg 10":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.LavaERG10);
                    break;

                case "firefly":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.FireFly);
                    break;

                case "golden imp-5":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenimp5);
                    break;
                case "candycane imp-5":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.candycaneimp5);
                    break;
                case "golden bosk 3":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenbosk3);
                    break;
                case "golden red-34":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenred34);
                    break;
                case "platinum red-34":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.platinumred34);
                    break;
                case "jadestone kog v":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.jadestonekogv);
                    break;
                case "golden severance":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenseverance);
                    break;
                case "golden jglt-313":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenjglt313);
                    break;
                case "golden dbs 4":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldendbs4);
                    break;
                case "lava pr39":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavapr39);
                    break;
                case "golden shred-4":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenshred4);
                    break;
                case "golden fs80":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenfs80);
                    break;
                case "M.O.R.T.-x":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.MORTx);
                    break;
                case "sering plag-9 model":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.seringplag9model);
                    break;
                case "skl-rcc-45":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.sklrcc45);
                    break;
                case "hawk-1z":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.hawk1z);
                    break;
                case "l1n4-588":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.l1n4588);
                    break;
                case "the zapper":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.thezapper);
                    break;
                case "zombified red-34":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.zombifiedred34);
                    break;
                case "zombified bosk 3":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.zombifiedbosk3);
                    break;
                case "golden mes-n":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenmesn);
                    break;
                case "golden six-mg":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldensixmg);
                    break;
                case "golden lgr 35":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenlgr35);
                    break;
                case "jadestone imp-5":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.jadestoneimp5);
                    break;
                case "iced buckshot":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.icedbuckshot);
                    break;
                case "iced spec-38a":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.icedspec38a);
                    break;
                case "iced bramson":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.icedbramson);
                    break;
                case "iced blom-r":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.icedblomr);
                    break;
                case "iced shred-4":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.icedshred4);
                    break;
                case "lava red-34":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavared34);
                    break;
                case "jadestone bramstone":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.jadestonebramstone);
                    break;
                case "overcharged l.a.k":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.overchargedlak);
                    break;
                case "poisonous kog v":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.poisonouskogv);
                    break;
                case "gingerbread er 10":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.gingerbreader10);
                    break;
                case "gingerbread red-34":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.gingerbreadred34);
                    break;
                case "lava dbs 4":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavadbs4);
                    break;
                case "lava bosk 3":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavabosk3);
                    break;
                case "overcharged jglt-313":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.overchargedjglt313);
                    break;
                case "pacific bsw 77":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.pacificbsw77);
                    break;
                case "bloody ether-l":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.bloodyetherl);
                    break;
                case "bloody whisperer":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.bloodywhisperer);
                    break;
                case "pacific kog v":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.pacifickogv);
                    break;
                case "forest grinder":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.forestgrinder);
                    break;
                case "forest vice":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.forestvice);
                    break;
                case "jadestone bsw 77":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.jadestonebsw77);
                    break;
                case "jadestone whisperer":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.jadestonewhisperer);
                    break;
                case "zombified micro cov":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.zombifiedmicrocov);
                    break;

                case "golden b.a.s.u.":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenbasu);
                    break;
                case "golden slak 7h":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenslak7h);
                    break;
                case "k-v4l":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.kv4l);
                    break;
                case "golden kog v":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenkogv);
                    break;
                case "the roaster":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.theroaster);
                    break;
                case "lava wes":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavawes);
                    break;
                case "jadestone erg 10":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.jadestoneerg10);
                    break;
                case "lava bsw 77":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavabsw77);
                    break;
                case "golden grinder":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldengrinder);
                    break;
                case "platinum kog v":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.platinumkogv);
                    break;
                case "platinum imp-5":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.platinumimp5);
                    break;
                case "lava bramson":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavabramson);
                    break;
                case "lava imp-5":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.lavaimp5);
                    break;
                case "jadestone cas&pol":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.jadestonecaspol);
                    break;
                case "bloody dbs 4":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.bloodydbs4);
                    break;
                case "gingerbread rokk":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.gingerbreadrokk);
                    break;

                case "iced severance":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.icedseverance);
                    break;
                case "flamed a.e.s.t.-x84":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.flamedaestx84);
                    break;
                case "flamed firecracker":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.flamedfirecracker);
                    break;
                case "flamed lgr 35":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.flamedlgr35);
                    break;
                case "flamed twig":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.flamedtwig);
                    break;
                case "flamed ratog":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.flamedratog);
                    break;
                case "flamed mrager":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.flamedmrager);
                    break;
                case "golden sering 9":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldensering9);
                    break;
                case "golden rokk":
                    Memory.Write<uint>(PWAddress, (uint)WeaponsId.goldenrokk);
                    break;


























            }
            switch (SW)
            {
               
                case "t.o.n.i.":
                    Memory.Write<uint>(SWAddress, (uint)WeaponsId.TONI);
                    break;
                
            }
        }

        public static void InventoryManager(string Slot,string PrimaryWeapon,string SecondaryWeapon,bool GameState)
        {
            try
            {
                if (GameState) 
                {
                    switch (Slot) 
                    {
                        case "Slot1":
                            AddWeapon(PrimaryWeapon, SecondaryWeapon,1);
                            break;
                        case "Slot2":
                            AddWeapon(PrimaryWeapon, SecondaryWeapon, 2);
                            break;
                        case "Slot3":
                            AddWeapon(PrimaryWeapon, SecondaryWeapon, 3);
                            break;
                    }
                }
            }
            catch { }
        }


        private static void RadarEnable()
        {
            try { 
            byte[] buffer = { 0xb8, 0xd0, 0x07, 0x00, 0x00, 0x90 };
            Memory.WriteBytes(ModuleBase + 0x771b38, buffer, 6);
            }
            catch { }
        }
        private static void RadarDisable()
        {
            try { 
            byte[] buffer = { 0x8b, 0x83, 0xe4, 0x03, 0x00, 0x00 };
            Memory.WriteBytes(ModuleBase + 0x771b38, buffer, 6);
            }
            catch { }
        }

        private static void VPowerEnable()
        {
            try { 
            byte[] buffer = { 0xf3, 0x0f, 0x5c, 0xf6, 0x90 };
            Memory.WriteBytes(ModuleBase + 0x201f9e, buffer, 5);
            }
            catch { }
        }
        private static void VPowerDisable()
        {
            try { 
            byte[] buffer = { 0xf3, 0x0f, 0x58, 0x73, 0x28 };
            Memory.WriteBytes(ModuleBase + 0x201f9e, buffer, 5);
            }
            catch { }
        }

        private static void NoClipEnable()
        {
            try
            {
                byte[] buffer = { 0x90, 0x90, 0x90, 0x90, 0x90 };
                Memory.WriteBytes(ModuleBase + 0xADE5B4, buffer, 5);
            }
            catch { }
        }
        private static void NoClipDisable()
        {
            try
            {
                byte[] buffer = { 0xE8, 0xC7, 0x13, 0x01, 0x00 };
                Memory.WriteBytes(ModuleBase + 0xADE5B4, buffer, 5);
            }
            catch { }
        }

        private static void AirWalkEnable()
        {
            try
            {
                byte[] buffer = { 0xc6, 0x83, 0xd4, 0x00, 0x00, 0x00, 0x00 };
                Memory.WriteBytes(ModuleBase + 0x80fed4, buffer, 7);
            }
            catch { }
        }
        private static void AirWalkDisable()
        {
            try
            {
                byte[] buffer = { 0xc6, 0x83, 0xd4, 0x00, 0x00, 0x00, 0x01 };
                Memory.WriteBytes(ModuleBase + 0x80fed4, buffer, 7);
            }
            catch { }
        }

        private static void NoRecoilEnable()
        {
            try
            {
                byte[] buffer = { 0xf3, 0x0f, 0x10, 0xb3, 0x30, 0x06, 0x00, 0x00 };
                Memory.WriteBytes(ModuleBase + 0x464469, buffer, 7);
            }
            catch { }
        }
        private static void NoRecoilDisable()
        {
            try
            {
                byte[] buffer = { 0xf3, 0x0f, 0x58, 0xb3, 0x30, 0x06, 0x00, 0x00 };
                Memory.WriteBytes(ModuleBase + 0x464469, buffer, 7);
            }
            catch { }
        }
        /// <summary>
        /// WallHack functions
        /// </summary>

        private static byte[] JmpBack(IntPtr Dest)
        {
            byte[] jmp_syntax = { 0xff, 0x25, 0x00, 0x00, 0x00, 0x00 };
            byte[] jmp_back = new byte[14];
            byte[] dest_bts = BitConverter.GetBytes((ulong)Dest);
            for (int x = 0, i = 0; x < 14; x++)
            {
                if (x < 6)
                {
                    jmp_back[x] = jmp_syntax[x];
                }
                else if (x < 14 && (x > 6 || x == 6))
                {
                    jmp_back[x] = dest_bts[i];
                    i++;
                }
            }
            return jmp_back;

        }

        private static void HookAddress(IntPtr Origin, IntPtr Dest, int LengthOfBytesToHook)
        {
            byte[] jmp_syntax = { 0xff, 0x25, 0x00, 0x00, 0x00, 0x00 };
            byte[] jmp = new byte[LengthOfBytesToHook];
            byte[] dest_bts = BitConverter.GetBytes((ulong)Dest);
            for (int x = 0, i = 0; x < LengthOfBytesToHook; x++)
            {
                if (x < 6)
                {
                    jmp[x] = jmp_syntax[x];
                }
                else if (x < 14 && (x > 6 || x == 6))
                {
                    jmp[x] = dest_bts[i];
                    i++;
                }
                else
                {
                    jmp[x] = 0x90;
                }
            }
            Memory.WriteBytes(Origin, jmp, jmp.Length);
        }

            private static void X64CodeCave(IntPtr Origin, IntPtr Dest, int LengthOfBytesToHook, byte[] BytesToWrite)
        {
            byte[] jmp_syntax = { 0xff, 0x25, 0x00, 0x00, 0x00, 0x00 };
            byte[] jmp = new byte[LengthOfBytesToHook];
            //byte[] Hooked_bts = mem.ReadBytes(Origin, LengthOfBytesToHook);
            byte[] dest_bts = BitConverter.GetBytes((ulong)Dest);
            for (int x = 0, i = 0; x < LengthOfBytesToHook; x++)
            {
                if (x < 6)
                {
                    jmp[x] = jmp_syntax[x];
                }
                else if (x < 14 && (x > 6 || x == 6))
                {
                    jmp[x] = dest_bts[i];
                    i++;
                }
                else
                {
                    jmp[x] = 0x90;
                }
            }

            byte[] jmp_back = JmpBack(Origin + LengthOfBytesToHook);

            Memory.WriteBytes(Dest, BytesToWrite, BytesToWrite.Length);
            Memory.WriteBytes(Dest + BytesToWrite.Length, jmp_back, 14);
            Memory.WriteBytes(Origin, jmp, jmp.Length);
        }

        private static IntPtr WallHackAllocatedMemory = IntPtr.Zero;
        private static IntPtr RapidFireAllocatedMemory = IntPtr.Zero;
        private static IntPtr RapidFire2AllocatedMemory = IntPtr.Zero;
        private static IntPtr SWAllocatedMemory = IntPtr.Zero;

        private static void WallHackEnable()
        {
            try
            {
                if (WallHackAllocatedMemory == IntPtr.Zero)
                    WallHackAllocatedMemory = Memory.VirtualAllocEx(Memory.handle, IntPtr.Zero, 100, (uint)Memory.AllocationType.Commit | (uint)Memory.AllocationType.Reserve,(uint)Memory.MemoryProtection.ExecuteReadWrite);
                byte[] BytesToWrite = { 0x74, 0x1f, 0x41, 0x83, 0x7D, 0x0C, 0x00, 0x0F, 0x85
                        ,0x08 ,0x00 ,0x00 ,0x00 ,0x41 ,0xC7 ,0x45 ,0x0C ,0x00 ,0x02 ,0x00 ,0x00
                        ,0x41 ,0xF6,0x45 ,0x0C ,0x04 ,0x74 ,0x05 ,0x41 ,0xB0 ,0x01 ,0xEB ,0x0A
                        ,0x45 ,0x32 ,0xC0 ,0x44 ,0x8B ,0x8F ,0x54 ,0x08 ,0x00 ,0x00 };
                X64CodeCave(ModuleBase + 0x21a321, WallHackAllocatedMemory, 24,BytesToWrite);
            }
            catch { }
        }
        private static void WallHackDisable()
        {
            byte[] buffer = { 0x74, 0x0C, 0x41, 0xF6, 0x45, 0x0C, 0x04, 0x74, 0x05, 0x41, 0xB0, 0x01, 0xEB, 0x03, 0x45, 0x32, 0xC0, 0x44, 0x8B, 0x8F, 0x54, 0x08, 0x00, 0x00 };
            try
            {
                Memory.WriteBytes(ModuleBase + 0x21a321, buffer, 24);
            }
            catch { }
        }











        


        public static void RapidFire1Enable()
        {
            try
            {
                if (RapidFireAllocatedMemory == IntPtr.Zero)
                    RapidFireAllocatedMemory = Memory.VirtualAllocEx(Memory.handle, ModuleBase-1000, 100, (uint)Memory.AllocationType.Commit | (uint)Memory.AllocationType.Reserve, (uint)Memory.MemoryProtection.ExecuteReadWrite);
                byte[] buffer1 = { 0x83, 0xF9, 0x0F, 0x75, 0x3d, 0x52, 0x48, 0x8B, 0x15 };
                Memory.WriteBytes(RapidFireAllocatedMemory, buffer1, buffer1.Length);
                byte[] buffer2 = BitConverter.GetBytes((int)(((ulong)(ModuleBase + 0x1CBBA78) - (ulong)(RapidFireAllocatedMemory + buffer1.Length)) - 4));
                Memory.WriteBytes(RapidFireAllocatedMemory + buffer1.Length, buffer2, buffer2.Length);
                byte[] buffer3 = { 0x48 ,0x8B ,0x92 ,0x08 ,0x03 ,0x00 ,0x00
                        ,0x48 ,0x8B ,0x92 ,0x90 ,0x00
                        ,0x00 ,0x00 ,0x48 ,0x8B ,0x92 ,0x60 ,0x01 ,0x00 ,0x00 ,0x48 ,0x8B ,0x52
                        ,0x38 ,0x48 ,0x8B ,0x92 ,0xE0 ,0x04 ,0x00 ,0x00 ,0x48 ,0x8B
                        ,0x52 ,0x08 ,0xC7 ,0x42 ,0x48 ,0x0B
                        ,0x00 ,0x00 ,0x00 ,0x5A ,0xC7 ,0x43 ,0x0C ,0xFF ,0xFF ,0xFF ,0xFF
                        ,0xEB ,0x0A ,0x89
                        ,0x4B ,0x08 ,0xC7 ,0x43 ,0x0C ,0xFF ,0xFF ,0xFF ,0xFF 
                };
                Memory.WriteBytes(RapidFireAllocatedMemory + buffer1.Length + buffer2.Length, buffer3, buffer3.Length);
                byte[] jmpBack = {0xe9,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x488315) - (ulong)(RapidFireAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length) - 5))[0]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x488315) - (ulong)(RapidFireAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length) - 5))[1]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x488315) - (ulong)(RapidFireAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length) - 5))[2]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x488315) - (ulong)(RapidFireAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length) - 5))[3]};

                Memory.WriteBytes(RapidFireAllocatedMemory + buffer1.Length + buffer2.Length + buffer3.Length, jmpBack, jmpBack.Length);
                byte[] jmpto = {0xe9,BitConverter.GetBytes( (ulong)((ulong)(RapidFireAllocatedMemory) - (ulong)(ModuleBase+0x48830b) - 5))[0]
                ,BitConverter.GetBytes( (ulong)((ulong)(RapidFireAllocatedMemory) - (ulong)(ModuleBase+0x48830b)  - 5))[1]
                ,BitConverter.GetBytes( (ulong)((ulong)(RapidFireAllocatedMemory) - (ulong)(ModuleBase+0x48830b) - 5))[2]
                ,BitConverter.GetBytes( (ulong)((ulong)(RapidFireAllocatedMemory) - (ulong)(ModuleBase+0x48830b) - 5))[3],0x90,0x90,0x90,0x90,0x90};

                Memory.WriteBytes(ModuleBase + 0x48830b, jmpto, jmpto.Length);

               // MessageBox.Show(RapidFireAllocatedMemory.ToString("x"));


            }
            catch { }
        }
        public static void RapidFire1Disable()
        {
            byte[] buffer1 = { 0x89, 0x4B, 0x08, 0xC7, 0x43, 0x0C, 0xFF, 0xFF, 0xFF, 0xFF };
            Memory.WriteBytes(ModuleBase + 0x48830b, buffer1, buffer1.Length);
        }


        public static void RapidFire2Enable()
        {
            try
            {
                if (RapidFire2AllocatedMemory == IntPtr.Zero)
                    RapidFire2AllocatedMemory = Memory.VirtualAllocEx(Memory.handle, ModuleBase - 0x20000, 100, (uint)Memory.AllocationType.Commit | (uint)Memory.AllocationType.Reserve, (uint)Memory.MemoryProtection.ExecuteReadWrite);
                byte[] buffer1 = { 0x80, 0x3d};
                Memory.WriteBytes(RapidFire2AllocatedMemory, buffer1, buffer1.Length);
                byte[] buffer2 = BitConverter.GetBytes((int)(((ulong)(ModuleBase + 0x1CBC9F8) - (ulong)(RapidFire2AllocatedMemory + buffer1.Length)) - 4));
                Memory.WriteBytes(RapidFire2AllocatedMemory + buffer1.Length, buffer2, buffer2.Length);
                byte[] buffer3 = { 0x1, 0x75, 0x39, 0x52, 0x48, 0x8b, 0x15 };
                Memory.WriteBytes(RapidFire2AllocatedMemory + buffer1.Length + buffer2.Length, buffer3, 7);
                byte[] buffer4 = BitConverter.GetBytes((int)(((ulong)(ModuleBase + 0x1CBBA78) - (ulong)(RapidFire2AllocatedMemory + buffer1.Length + buffer2.Length + buffer3.Length)) - 4));
                Memory.WriteBytes(RapidFire2AllocatedMemory + buffer1.Length + buffer2.Length+ buffer3.Length, buffer4, buffer4.Length);
                byte[] buffer5 = { 0x48 ,0x8B ,0x92 ,0x08 ,0x03 ,0x00 ,0x00
                        ,0x48 ,0x8B ,0x92 ,0x90 ,0x00
                        ,0x00 ,0x00 ,0x48 ,0x8B ,0x92 ,0x60 ,0x01 ,0x00 ,0x00 ,0x48 ,0x8B ,0x52
                        ,0x38 ,0x48 ,0x8B ,0x92 ,0xE0 ,0x04 ,0x00 ,0x00 ,0x48 ,0x8B
                        ,0x52 ,0x08 ,0xC7 ,0x42 ,0x48 ,0x0B
                        ,0x00 ,0x00 ,0x00 ,0x5A ,0x41 ,0xb1 ,0x01 
                        ,0xEB ,0x0A ,0xC7 ,0x43 ,0x08 ,0x03 ,0x00 ,0x00 ,0x00 ,0x41 ,0xB1 ,0x01
                };
                Memory.WriteBytes(RapidFire2AllocatedMemory + buffer1.Length + buffer2.Length + buffer3.Length + buffer4.Length, buffer5, buffer5.Length);
                byte[] jmpBack = {0xe9,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x4883AC) - (ulong)(RapidFire2AllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length+buffer5.Length) - 5))[0]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x4883AC) - (ulong)(RapidFire2AllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length+buffer5.Length) - 5))[1]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x4883AC) - (ulong)(RapidFire2AllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length+buffer5.Length) - 5))[2]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x4883AC) - (ulong)(RapidFire2AllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length+buffer5.Length) - 5))[3]};

                Memory.WriteBytes(RapidFire2AllocatedMemory + buffer1.Length + buffer2.Length + buffer3.Length + buffer4.Length + buffer5.Length, jmpBack, jmpBack.Length);
                byte[] jmpto = {0xe9,BitConverter.GetBytes( (ulong)((ulong)(RapidFire2AllocatedMemory) - (ulong)(ModuleBase+0x4883A6) - 5))[0]
                ,BitConverter.GetBytes( (ulong)((ulong)(RapidFire2AllocatedMemory) - (ulong)(ModuleBase+0x4883A6)  - 5))[1]
                ,BitConverter.GetBytes( (ulong)((ulong)(RapidFire2AllocatedMemory) - (ulong)(ModuleBase+0x4883A6) - 5))[2]
                ,BitConverter.GetBytes( (ulong)((ulong)(RapidFire2AllocatedMemory) - (ulong)(ModuleBase+0x4883A6) - 5))[3],0x90};

                Memory.WriteBytes(ModuleBase + 0x4883A6, jmpto, jmpto.Length);

                //MessageBox.Show(RapidFire2AllocatedMemory.ToString("x"));


            }
            catch { }
        }



        public static void RapidFire2Disable()
        {
            byte[] buffer1 = { 0x89, 0x43, 0x08, 0x41, 0xB1, 0x01 };
            Memory.WriteBytes(ModuleBase + 0x4883A6, buffer1, buffer1.Length);
        }




        private static void SafeWalkEnable()
        {
            try
            {
                if (SWAllocatedMemory == IntPtr.Zero)
                    SWAllocatedMemory = Memory.VirtualAllocEx(Memory.handle, ModuleBase - 0x30000, 100, (uint)Memory.AllocationType.Commit | (uint)Memory.AllocationType.Reserve, (uint)Memory.MemoryProtection.ExecuteReadWrite);
                byte[] buffer1 = { 0x53, 0x48, 0x8b, 0x1d };
                Memory.WriteBytes(SWAllocatedMemory, buffer1, buffer1.Length);
                byte[] buffer2 = BitConverter.GetBytes((int)(((ulong)(ModuleBase + 0x1CBBA78) - (ulong)(SWAllocatedMemory + buffer1.Length)) - 4));
                Memory.WriteBytes(SWAllocatedMemory + buffer1.Length, buffer2, buffer2.Length);
                byte[] buffer3 = { 0x48, 0x8B, 0x9B, 0x00, 0x07, 0x00, 0x00, 0x48,
                    0x8B, 0x1B, 0x48, 0x8B, 0x9B, 0x58, 0x08, 0x00, 0x00, 0x48,
                    0x8B, 0x9B, 0xD8, 0x00, 0x00, 0x00, 0x48, 0x8B, 0x9B, 0x20, 0x04,
                    0x00, 0x00, 0x48, 0x83, 0xC3, 0x38, 0x48, 0x39, 0xD9, 0x5B, 0x75, 0x03, 0xc3, 0xeb, 0x09 ,
                    0x48, 0x39, 0xd1, 0x0f, 0x84};
                Memory.WriteBytes(SWAllocatedMemory + buffer1.Length + buffer2.Length, buffer3, buffer3.Length);
                byte[] buffer4 = BitConverter.GetBytes((int)(((ulong)(ModuleBase + 0x6F641D) - (ulong)(SWAllocatedMemory +
                    buffer1.Length + buffer2.Length + buffer3.Length)) - 4));
                Memory.WriteBytes(SWAllocatedMemory + buffer1.Length + buffer2.Length + buffer3.Length, buffer4, buffer4.Length);

                byte[] jmpBack = {0xe9,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x6F6399) - (ulong)(SWAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length) - 5))[0]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x6F6399) - (ulong)(SWAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length) - 5))[1]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x6F6399) - (ulong)(SWAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length) - 5))[2]
                ,BitConverter.GetBytes( (int)((ulong)(ModuleBase+0x6F6399) - (ulong)(SWAllocatedMemory+buffer1.Length+buffer2.Length+buffer3.Length+buffer4.Length) - 5))[3]};

                Memory.WriteBytes(SWAllocatedMemory + buffer1.Length + buffer2.Length + buffer3.Length + buffer4.Length, jmpBack, jmpBack.Length);
                byte[] jmpto = {0xe9,BitConverter.GetBytes( (ulong)((ulong)(SWAllocatedMemory) - (ulong)(ModuleBase+0x6F6390) - 5))[0]
                ,BitConverter.GetBytes( (ulong)((ulong)(SWAllocatedMemory) - (ulong)(ModuleBase+0x6F6390)  - 5))[1]
                ,BitConverter.GetBytes( (ulong)((ulong)(SWAllocatedMemory) - (ulong)(ModuleBase+0x6F6390) - 5))[2]
                ,BitConverter.GetBytes( (ulong)((ulong)(SWAllocatedMemory) - (ulong)(ModuleBase+0x6F6390) - 5))[3],0x90,0x90,0x90,0x90};

                Memory.WriteBytes(ModuleBase + 0x6F6390, jmpto, jmpto.Length);
            }
            catch { }
        }
        private static void SafeWalkDisable()
        {
            try
            {
                byte[] buffer = { 0x48, 0x3B, 0xCA, 0x0F, 0x84, 0x84, 0x00, 0x00, 0x00 };
                Memory.WriteBytes(ModuleBase + 0x6F6390, buffer, 9);
            }
            catch { }
        }

        private static void SFixesEnable()
        {
            byte[] buffer = { 0xC3, 0x90, 0x90 };
            try
            {
                Memory.WriteBytes(ModuleBase + 0x6BF5F0, buffer, 3);
            }
            catch { }
        }
        private static void SFixesDisable()
        {
            byte[] buffer = { 0x48, 0x8B, 0xC4 };
            try
            {
                Memory.WriteBytes(ModuleBase + 0x6BF5F0, buffer, 3);
            }
            catch { }
        }


        /// <summary>
        /// /////////////////////////////////////////////
        /// </summary>
        private static void checkString(ref string x, ref string y, ref string z) 
        {
            if (x.Trim() == string.Empty)
            {
                IntPtr virtual_x = Memory.GetPointerAddress64Bit(ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x58 }, 5);
                x = Memory.Read<float>(virtual_x).ToString("0.00000");
            }
            if (y.Trim() == string.Empty)
            {
                IntPtr virtual_y = Memory.GetPointerAddress64Bit(ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x5c }, 5);
                y = Memory.Read<float>(virtual_y).ToString("0.00000");
            }
            if (z.Trim() == string.Empty)
            {
                IntPtr virtual_z = Memory.GetPointerAddress64Bit(ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x60 }, 5);
                z = Memory.Read<float>(virtual_z).ToString("0.00000");
            }
        }

        public static void Teleport(string x, string y, string z,bool GameStat)
        {
            if (!GameStat) return;
            checkString(ref x, ref y, ref z);
            try
            {
                IntPtr XAddr = Memory.GetPointerAddress64Bit(ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x58 }, 5);
                float _x = Convert.ToSingle(x);
                float _y = Convert.ToSingle(y);
                float _z = Convert.ToSingle(z);
                byte[] buffer = new byte[12];

                Array.Copy(BitConverter.GetBytes(_x), 0, buffer, 0, 4);
                Array.Copy(BitConverter.GetBytes(_y), 0, buffer, 4, 4);
                Array.Copy(BitConverter.GetBytes(_z), 0, buffer, 8, 4);

                Memory.WriteBytes(XAddr, buffer, 12);


            }
            catch { }
        }

        public static void DisableAmmo1() 
        {
            ammo1 = false;
            try
            {
                IntPtr Ammo1 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x8, 0x70, 0x0 }, 5);
                IntPtr Ammo2 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x8, 0x68 }, 4);
                Memory.Write<uint>(Ammo1, 0);
                Memory.Write<uint>(Ammo2, 0);
            }
            catch { }
        }
        public static void DisableAmmo2()
        {
            ammo2 = false;
            try
            {
                IntPtr Ammo1 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x28, 0x70, 0x0 }, 5);
                IntPtr Ammo2 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x28, 0x68 }, 4);
                Memory.Write<uint>(Ammo1, 0);
                Memory.Write<uint>(Ammo2, 0);
            }
            catch { }
        }

        public static void DisableGrenades()
        {
            grenades = false;
            try
            {
                IntPtr G2 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x1b8, 0x0 }, 3);
                IntPtr G1 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x1b8, 0x8, 0x0 }, 4);
                Memory.Write<uint>(G1, 0);
                Memory.Write<uint>(G2, 0);
            }
            catch { }
        }

        private static void DisableAllPointers()
        {
            jump = false;
            ammo1 = false;
            ammo2 = false;
            hp = false; 
            //wallhack = false; 
            teleport = false; 
            grenades = false;
            autoshoot = false;
            aimfocus = false;
        }

        public static void Enable(string Text,bool GameStat,ref Button btn_airwalk) 
        {
            if (!GameStat) { DisableAllPointers(); return; }
            try {
                switch (Text)
                {
                    case "Radar":
                        RadarEnable();
                        break;
                    case "Jump":
                        jump = true;
                        break;
                    case "Ammo1":
                        ammo1 = true;
                        break;
                    case "Air Walk":
                        AirWalkEnable();
                        break;
                    case "Health":
                        hp = true;
                        break;
                    case "Wall Hack":
                        //wallhack = true;
                        WallHackEnable();
                        break;
                    case "Verr Power":
                        VPowerEnable();
                        break;
                    case "Cursor T.P.":
                        teleport = true;
                        break;
                    case "Accuracy":
                        AccuracyEnable();
                        break;
                    case "Auto Shoot":
                        autoshoot = true;
                        break;
                    case "Focus Aim":
                        aimfocus = true;
                        break;
                    case "Frag":
                        grenades = true;
                        break;
                    case "No Clip":
                        AirWalkEnable();
                        NoClipEnable();
                        btn_airwalk.BackColor = Color.DarkGreen;
                        btn_airwalk.Enabled = false;
                        break;
                    case "No Recoil":
                        NoRecoilEnable();
                        break;
                    case "Ammo2":
                        ammo2 = true;
                        break;
                    case "E.Patch":
                        EPatchEnable();
                        break;
                    case "Wall Bang":
                        WallBangEnable();
                        break;
                    case "Rapid Fire":
                        RapidFire1Enable();
                        RapidFire2Enable();
                        break;
                    case "Safe Walk":
                        SafeWalkEnable();
                        SFixesEnable();
                        break;
                }
            }
            catch { }
        }

        public static void Disable(string Text, bool GameStat,ref Button btn_airwalk)
        {
            if (!GameStat) { DisableAllPointers(); return; }
            try {
                switch (Text)
                {
                    case "Radar":
                        RadarDisable();
                        break;
                    case "Jump":
                        jump = false;
                        break;
                    case "Ammo1":
                        DisableAmmo1();
                        break;
                    case "Air Walk":
                        AirWalkDisable();
                        break;
                    case "Health":
                        hp = false;
                        break;
                    case "Wall Hack":
                        //wallhack = false;
                        WallHackDisable();
                        break;
                    case "Verr Power":
                        VPowerDisable();
                        break;
                    case "Cursor T.P.":
                        teleport = false;
                        break;
                    case "Accuracy":
                        AccuracyDisable();
                        break;
                    case "Auto Shoot":
                        autoshoot = false;
                        break;
                    case "Focus Aim":
                        aimfocus = false;
                        break;
                    case "Frag":
                        DisableGrenades();
                        break;
                    case "No Clip":
                        NoClipDisable();
                        AirWalkDisable();
                        btn_airwalk.BackColor = Color.FromArgb(70, 70, 70);
                        btn_airwalk.Enabled = true;
                        break;
                    case "No Recoil":
                        NoRecoilDisable();
                        break;
                    case "Ammo2":
                        DisableAmmo2();
                        break;
                    case "E.Patch":
                        EPatchDisable();
                        break;
                    case "Wall Bang":
                        WallBangDisable();
                        break;
                    case "Rapid Fire":
                        RapidFire1Disable();
                        RapidFire2Disable();
                        Memory.Write<int>(Memory.GetPointerAddress64Bit(ModuleBase + 0x1CBBA78, new int[] {0x308, 0x90, 0x160, 0x38, 0x4e0, 0x8, 0x48 }, 7), 3);
                        break;
                    case "Safe Walk":
                        SafeWalkDisable();
                        SFixesDisable();
                        break;
                }
            }
            catch { }
        }

    }
}
