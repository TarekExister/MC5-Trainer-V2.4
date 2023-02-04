using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MC5Trainer
{

    public partial class MainForm : Form
    {
        
        #region Control Settings

        private bool mouseDown;
        private Point lastLocation;
        bool GameState = false;
        private float core_sixth_sense = 0.0f;
        private float core_evil_eye = 0.0f;
        private float core_impetum = 0.0f;
        private float core_infiltrator = 0.0f;
        private float core_high_powered = 0.0f;
        private float core_toxic_area = 0.0f;
        private float core_murder_blitz = 0.0f;
        private float core_yokai = 0.0f;
        private float core_armor_piercer = 0.0f;
        private float core_snowflake = 0.0f;
        bool swap = false;
        bool canfocus = false;
        IntPtr EntityAtCursor;


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public MainForm()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 12, 12));
            foreach (Button b in pnlControls.Controls) 
            {
                b.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, b.Width, b.Height, 12, 12));
            }
            
            btnInject.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnInject.Width, btnInject.Height, 12, 12));
            tbxDllPath.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tbxDllPath.Width, tbxDllPath.Height, 12, 12));
            btnTeleport.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnTeleport.Width, btnTeleport.Height, 12, 12));
            btnEPatch.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnEPatch.Width, btnEPatch.Height, 12, 12));
            btnAdd.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAdd.Width, btnAdd.Height, 12, 12));

            pnlCoresRow2.Location = pnlCoresRow1.Location;

            
        }

        private void swapPannels(bool s)
        {
            if (swap)
            {
                pnlCoresRow1.Visible = true;
                pnlCoresRow2.Visible = false;

            }
            else
            {
                pnlCoresRow1.Visible = false;
                pnlCoresRow2.Visible = true;
            }
            swap = !swap;
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRadar_MouseEnter(object sender, EventArgs e)
        {
            if ((sender as Button).BackColor != Color.DarkGreen) 
            {
                (sender as Button).BackColor = Color.DarkRed;
            }
        }

        private void btnRadar_MouseLeave(object sender, EventArgs e)
        {
            if ((sender as Button).BackColor != Color.DarkGreen)
            {
                (sender as Button).BackColor = Color.FromArgb(70, 70, 70);
            }
        }

        private void btnRadar_Click(object sender, EventArgs e)
        {
          
            if ( ((sender as Button).BackColor == Color.FromArgb(70, 70, 70)) || ((sender as Button).BackColor == Color.DarkRed))
            {
                (sender as Button).BackColor = Color.DarkGreen;
                Cheats.Enable((sender as Button).Text, GameState,ref btnAirWalk);
            }
            else 
            {
                (sender as Button).BackColor = Color.FromArgb(70, 70, 70);
                Cheats.Disable((sender as Button).Text, GameState, ref btnAirWalk);
            }
        }

        private void btnRadar_MouseUp(object sender, MouseEventArgs e)
        {
           
            if ((sender as Button).BackColor == Color.FromArgb(70, 70, 70)) 
            {
                (sender as Button).BackColor = Color.DarkRed;
                Cheats.Disable((sender as Button).Text, GameState, ref btnAirWalk);
            }
        }

        private void pnlTitle_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void pnlTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void pnlTitle_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void TimerOpacity_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0f)
            {
                this.Opacity += 0.1f;
                if (this.Opacity >= 1.0f)
                {
                    TimerOpacity.Enabled = false;
                    pnlCoresRow2.Visible = false;
                }
            }
        }

        private void btnTeleport_Click(object sender, EventArgs e)
        {
            Cheats.Teleport(tbxX.Text, tbxY.Text, tbxZ.Text, GameState);
            tbxX.Text = string.Empty;
            tbxY.Text = string.Empty;
            tbxZ.Text = string.Empty;
        }

        private void lblX_Click(object sender, EventArgs e)
        {
            if ((sender as Label).Name == lblX.Name)
            {
                tbxX.Text = lblX.Text.Replace('X', ' ').Replace(':', ' ').Trim();
            }
            else if ((sender as Label).Name == lblY.Name)
            {
                tbxY.Text = lblY.Text.Replace('Y', ' ').Replace(':', ' ').Trim();
            }
            else
            {
                tbxZ.Text = lblZ.Text.Replace('Z', ' ').Replace(':', ' ').Trim();
            }
        }

        private void tbxX_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, "[^0-9-.]"))
            {
                (sender as TextBox).Text = (sender as TextBox).Text.Remove((sender as TextBox).Text.Length - 1);
            }
        }
        bool rpTogle = true;
        private void TmStat_Tick(object sender, EventArgs e)
        {
            try
            {
                GameState = Memory.AttachProcess("WindowsEntryPoint.Windows_W10");
                if (GameState)
                {
                    if (Cheats.ModuleBase == IntPtr.Zero)
                        Cheats.ModuleBase = Memory.GetModuleAddress("WindowsEntryPoint.Windows_W10", "WindowsEntryPoint.Windows_W10");
                    IntPtr CoordsX = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x58 }, 5);
                    lblX.Text = "X:" + Memory.Read<float>(CoordsX).ToString("0.000");
                    lblY.Text = "Y:" + Memory.Read<float>(CoordsX + 4).ToString("0.000");
                    lblZ.Text = "Z:" + Memory.Read<float>(CoordsX + 8).ToString("0.000");
                }
                else
                {
                    TmCPointers.Enabled = false;
                    TmStat.Enabled = false;
                    MessageBox.Show(null, "Game Not Found", "Error", MessageBoxButtons.OK);
                    this.Close();
                }
                if (Memory.GetAsyncKeyState(Keys.F2)!=0) 
                {
                    while ((Memory.GetAsyncKeyState(Keys.F2) != 0)) ;
                    if (rpTogle)
                    {
                        Cheats.RapidFire1Enable();
                        Cheats.RapidFire2Enable();
                        btnRapidFire.BackColor = Color.DarkGreen;
                        rpTogle = !rpTogle;
                    }
                    else 
                    {
                        Cheats.RapidFire1Disable();
                        Cheats.RapidFire2Disable();
                        Memory.Write<int>(Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x90, 0x160, 0x38, 0x4e0, 0x8, 0x48 }, 7), 3);
                        btnRapidFire.BackColor = Color.FromArgb(70,70,70);
                        rpTogle = !rpTogle;
                    }
                }
            }
            catch { }
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Dll Files(.dll)| *.dll";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbxDllPath.Text = ofd.FileName;
            }
            System.Threading.Thread.Sleep(500);
            if (tbxDllPath.Text == string.Empty) return;
            btnInject.Enabled = false;

            int pid = Memory.GetProcessID("WindowsEntryPoint.Windows_W10");
            string dllPath = tbxDllPath.Text;

            DllPermission.SetDllPermission(dllPath);
            InjectDll.Injectlibrary(pid, dllPath);


        }

 
        private void lblInfo_Click(object sender, EventArgs e)
        {
            string info = "Jump: 'V'\n" +
                          "Tp To Aim Coords: 'B' (Cursor T.P.)\n" +
                          "Unlimited Grenades supported type : 'frag'\n";
            MessageBox.Show(null, info, "info", MessageBoxButtons.OK);
        }

        private void pnlSixthSense_MouseClick(object sender, MouseEventArgs e)
        {
            if ((sender as Panel).Name == pnlSixthSense.Name)
            {
                if (cbxSixthSense.Checked) cbxSixthSense.Checked = false;
                else cbxSixthSense.Checked = true;
            }
            else if ((sender as Panel).Name == pnlEvilEye.Name)
            {
                if (cbxEvilEye.Checked) cbxEvilEye.Checked = false;
                else cbxEvilEye.Checked = true;
            }
            else if ((sender as Panel).Name == pnlImpetum.Name)
            {
                if (cbxImpetum.Checked) cbxImpetum.Checked = false;
                else cbxImpetum.Checked = true;
            }
            else if ((sender as Panel).Name == pnlInfiltrator.Name)
            {
                if (cbxInfiltrator.Checked) cbxInfiltrator.Checked = false;
                else cbxInfiltrator.Checked = true;
            }
            else if ((sender as Panel).Name == pnlHPowered.Name)
            {
                if (cbxHPowered.Checked) cbxHPowered.Checked = false;
                else cbxHPowered.Checked = true;
            }
            else if ((sender as Panel).Name == pnlToxicArea.Name)
            {
                if (cbxToxicArea.Checked) cbxToxicArea.Checked = false;
                else cbxToxicArea.Checked = true;
            }
            else if ((sender as Panel).Name == pnlMurderBlitz.Name)
            {
                if (cbxMBlitz.Checked) cbxMBlitz.Checked = false;
                else cbxMBlitz.Checked = true;
            }
            else if ((sender as Panel).Name == pnlYokai.Name)
            {
                if (cbxYokai.Checked) cbxYokai.Checked = false;
                else cbxYokai.Checked = true;
            }
            else if ((sender as Panel).Name == pnlArmorPiercer.Name)
            {
                if (cbxAPiercer.Checked) cbxAPiercer.Checked = false;
                else cbxAPiercer.Checked = true;
            }
            else if ((sender as Panel).Name == pnlSnowflake.Name)
            {
                if (cbxSnowflake.Checked) cbxSnowflake.Checked = false;
                else cbxSnowflake.Checked = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            swapPannels(swap);
        }

        #endregion

        #region Cores

        private void cbxHPowered_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr HightPowered = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0xf30 }, 2);
                    if (cbxHPowered.Checked)
                    {
                        core_high_powered = Memory.Read<float>(HightPowered);
                        Memory.Write<float>(HightPowered, 999.0f);
                    }
                    else
                    {
                        if (core_high_powered > 0.0f)
                            Memory.Write<float>(HightPowered, core_high_powered);
                    }
                }
            }
            catch { }
        }

        private void cbxToxicArea_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr ToxicArea = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0x228 }, 2);
                    if (cbxToxicArea.Checked)
                    {
                        core_toxic_area = Memory.Read<float>(ToxicArea);
                        Memory.Write<float>(ToxicArea, 999.0f);
                    }
                    else
                    {
                        if (core_toxic_area > 0.0f)
                            Memory.Write<float>(ToxicArea, core_toxic_area);
                    }
                }
            }
            catch { }
        }

        private void cbxMBlitz_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr MurderBlitz = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0x3d8 }, 2);
                    if (cbxMBlitz.Checked)
                    {
                        core_murder_blitz = Memory.Read<float>(MurderBlitz);
                        Memory.Write<float>(MurderBlitz, 999.0f);
                    }
                    else
                    {
                        if (core_murder_blitz > 0.0f)
                            Memory.Write<float>(MurderBlitz, core_murder_blitz);
                    }
                }
            }
            catch { }
        }

        private void cbxYokai_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr Yokai = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0xa08 }, 2);
                    if (cbxYokai.Checked)
                    {
                        core_yokai = Memory.Read<float>(Yokai);
                        Memory.Write<float>(Yokai, 999.0f);
                    }
                    else
                    {
                        if (core_yokai > 0.0f)
                            Memory.Write<float>(Yokai, core_yokai);
                    }
                }
            }
            catch { }
        }

        private void cbxAPiercer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr ArmorPiercer = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0xdf8 }, 2);
                    if (cbxAPiercer.Checked)
                    {
                        core_armor_piercer = Memory.Read<float>(ArmorPiercer);
                        Memory.Write<float>(ArmorPiercer, 999.0f);
                    }
                    else
                    {
                        if (core_armor_piercer > 0.0f)
                            Memory.Write<float>(ArmorPiercer, core_armor_piercer);
                    }
                }
            }
            catch { }
        }

        private void cbxSnowflake_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr SnowFlake = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0xc30 }, 2);
                    if (cbxSnowflake.Checked)
                    {
                        core_snowflake = Memory.Read<float>(SnowFlake);
                        Memory.Write<float>(SnowFlake, 999.0f);
                    }
                    else
                    {
                        if (core_snowflake > 0.0f)
                            Memory.Write<float>(SnowFlake, core_snowflake);
                    }
                }
            }
            catch { }
        }
        private void cbxSixthSense_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr Sixth = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0x2e8 }, 2);
                    if (cbxSixthSense.Checked)
                    {
                        core_sixth_sense = Memory.Read<float>(Sixth);
                        Memory.Write<float>(Sixth, 999.0f);
                    }
                    else
                    {
                        if (core_sixth_sense > 0.0f)
                            Memory.Write<float>(Sixth, core_sixth_sense);
                    }
                }
            }
            catch { }
        }

        private void cbxEvilEye_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr Evileye = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0x600 }, 2);
                    if (cbxEvilEye.Checked)
                    {
                        core_evil_eye = Memory.Read<float>(Evileye);
                        Memory.Write<float>(Evileye, 999.0f);
                    }
                    else
                    {
                        if (core_evil_eye > 0.0f)
                            Memory.Write<float>(Evileye, core_evil_eye);
                    }
                }
            }
            catch { }
        }

        private void cbxImpetum_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr Impetum = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0xbd0 }, 2);
                    if (cbxImpetum.Checked)
                    {
                        core_impetum = Memory.Read<float>(Impetum);
                        Memory.Write<float>(Impetum, 999.0f);
                    }
                    else
                    {
                        if (core_impetum > 0.0f)
                            Memory.Write<float>(Impetum, core_impetum);
                    }
                }
            }
            catch { }
        }

        private void cbxInfiltrator_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GameState)
                {
                    IntPtr Infiltrator = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x01CB90A8, new int[] { 0x380, 0x4e0 }, 2);
                    if (cbxInfiltrator.Checked)
                    {
                        core_infiltrator = Memory.Read<float>(Infiltrator);
                        Memory.Write<float>(Infiltrator, 999.0f);
                    }
                    else
                    {
                        if (core_infiltrator > 0.0f)
                            Memory.Write<float>(Infiltrator, core_infiltrator);
                    }
                }
            }
            catch { }
        }
        #endregion

        private float GetDistance(float ecX, float ecY, float ecZ, float lpX, float lpY, float lpZ)
        {
            return (float)Math.Sqrt(Math.Pow((double)(ecX - lpX), 2.0) + Math.Pow((double)(ecY - lpY), 2.0) + Math.Pow((double)(ecZ - lpZ), 2.0));
        }

        private void TmCPointers_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Cheats.jump)
                {
                    if (Memory.GetAsyncKeyState(Keys.V) != 0)
                    {
                        IntPtr CoordsZ = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x60 }, 5);
                        Memory.Write<float>(CoordsZ, Memory.Read<float>(CoordsZ) + 1.5f);
                        System.Threading.Thread.Sleep(100);
                    }
                }
                if (Cheats.ammo1)
                {
                    IntPtr Ammo1 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x8, 0x70, 0x0 }, 5);
                    IntPtr Ammo2 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x8, 0x68 }, 4);
                    Memory.Write<uint>(Ammo1, 3460853931);
                    Memory.Write<uint>(Ammo2, 1999598699);
                }
                if (Cheats.ammo2)
                {
                    IntPtr Ammo1 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x28, 0x70, 0x0 }, 5);
                    IntPtr Ammo2 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x140, 0x28, 0x68 }, 4);
                    Memory.Write<uint>(Ammo1, 3460853931);
                    Memory.Write<uint>(Ammo2, 1999598699);
                }
                if (Cheats.hp)
                {
                    IntPtr Hp = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x1a8, 0x28 }, 6);
                    Memory.Write<float>(Hp, 200.0f);
                }
                if (Cheats.teleport)
                {
                    if (Memory.GetAsyncKeyState(Keys.B) != 0)
                    {
                        while (Memory.GetAsyncKeyState(Keys.B) != 0) ;
                        IntPtr CoordsX = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x58 }, 5);
                        IntPtr LookAtPos = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x14 }, 2);
                        byte[] buffer = Memory.ReadBytes(LookAtPos, 12);
                        Memory.WriteBytes(CoordsX, buffer, 12);
                    }
                }
                //if (Cheats.wallhack)
                //{
                //    if (Memory.GetAsyncKeyState(Keys.Space) != 0)
                //    {
                //        IntPtr Pitch = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x6c }, 5);
                //        IntPtr X = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x58 }, 5);
                //        float pitchval = Memory.Read<float>(Pitch);
                //        if ((pitchval < 30.0f) || (pitchval > 320.0f))
                //        {
                //            Memory.Write<float>(X + 4, Memory.Read<float>(X + 4) + 1.0f);
                //        }
                //        else if ((pitchval < 320.0f) && (pitchval > 230.0f))
                //        {
                //            Memory.Write<float>(X, Memory.Read<float>(X) + 1.0f);
                //        }
                //        else if ((pitchval < 230.0f) && (pitchval > 130.0f))
                //        {
                //            Memory.Write<float>(X + 4, Memory.Read<float>(X + 4) - 1.0f);
                //        }
                //        else
                //        {
                //            Memory.Write<float>(X, Memory.Read<float>(X) - 1.0f);
                //        }
                //        System.Threading.Thread.Sleep(100);
                //    }
                //}
                if (Cheats.grenades) 
                {
                    IntPtr G2 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x1b8, 0x0 }, 3);
                    IntPtr G1 = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x1b8, 0x8, 0x0 }, 4);
                    Memory.Write<uint>(G1, 3460853931);
                    Memory.Write<uint>(G2, 1999598699);
                }
                if (Cheats.autoshoot) 
                {
                    IntPtr Flag = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBC9B8, new int[] { 0x718, 0x190, 0x70, 0x190, 0x40, 0x218,0x0 }, 7);
                    IntPtr Shoot = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CB9250, new int[] { 0x8, 0x50, 0x58, 0x20, 0x20, 0x460 }, 6);
                    if (Memory.Read<byte>(Flag) == 7) 
                    {
                        Memory.Write<byte>(Shoot, 1);
                    }

                }

                if (Cheats.aimfocus)
                {

                    if (Memory.GetAsyncKeyState(Keys.RButton) != 0)
                    {

                        if (!canfocus)
                        {
                            EntityAtCursor = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x8, 0 }, 3);
                            if (EntityAtCursor != IntPtr.Zero)
                                canfocus = true;
                            //MessageBox.Show(Memory.GetPointerAddress(EntityAtCursor, new int[] { 0 }, 1).ToString());
                            //if (Memory.GetPointerAddress(EntityAtCursor, new int[] { 0 }, 1) == IntPtr.Zero) return;
                        }
                        //IntPtr Pitch = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x6c }, 5);
                        //IntPtr Yaw = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x610 }, 3);
                        //IntPtr WeaponLocation = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x68 }, 2);
                        //IntPtr EnemyHp = Memory.GetPointerAddress64Bit(EntityAtCursor, new int[] { 0x1a8, 0x28 }, 2);
                        //IntPtr EnemyHeadPosX = Memory.GetPointerAddress64Bit(EntityAtCursor, new int[] { 0x30, 0x38, 0x58, 0x0, 0x38, 0x40 }, 6);
                        //IntPtr TeamId = Memory.GetPointerAddress64Bit(EntityAtCursor, new int[] { 0xa0 }, 1);
                        //float NPitch, NYaw, EX, EY, EZ, PX, PY, PZ, deltax, deltay, deltaz, x, y, dist;
                        //EX = Memory.Read<float>(EnemyHeadPosX);
                        //EY = Memory.Read<float>(EnemyHeadPosX + 0x4);
                        //EZ = Memory.Read<float>(EnemyHeadPosX + 0x8);

                        //PX = Memory.Read<float>(WeaponLocation);
                        //PY = Memory.Read<float>(WeaponLocation + 0x4);
                        //PZ = Memory.Read<float>(WeaponLocation + 0x8);

                        //NYaw = (float)(Math.Atan2((EZ - PZ), GetDistance(EX, EY, EZ, PX, PY, PZ) * 180.0f / (float)Math.PI)); ;
                        //NPitch = -(float)Math.Atan2(EX - PX, EY - PY) / (float)Math.PI * 180.0f;
                        //Memory.Write<float>(Pitch, NPitch);
                        //Memory.Write<float>(Yaw, NYaw);



                        IntPtr Pitch = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x6c }, 5);
                        IntPtr Yaw = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x610 }, 3);
                        IntPtr HeadLocation = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x308, 0x68 }, 2);
                        IntPtr xPos = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0x58}, 5);
                        IntPtr MyTeamIdPtr = Memory.GetPointerAddress64Bit(Cheats.ModuleBase + 0x1CBBA78, new int[] { 0x700, 0x0, 0x858, 0x8, 0xa0 }, 5);
                        IntPtr EnemyHp = Memory.GetPointerAddress64Bit(EntityAtCursor+ 0x1a8, new int[] { 0x28 }, 1);
                        IntPtr EnemyHeadPos = Memory.GetPointerAddress64Bit(EntityAtCursor+0x160, new int[] { 0x0, 0x78, 0x0, 0x38, 0x40 }, 5);
                        IntPtr EnemyX = EntityAtCursor + 0x58;
                        int MyTeamID = Memory.Read<int>(MyTeamIdPtr);
                        int TeamId = Memory.Read<int>(EntityAtCursor + 0xa0);
                        float NPitch, NYaw, EX, EY, EZ, PX, PY, PZ, EHP;

                        EX = Memory.Read<float>(EnemyHeadPos);
                        EY = Memory.Read<float>(EnemyHeadPos+4);
                        EZ = Memory.Read<float>(EnemyHeadPos+8);
                        EHP = Memory.Read<float>(EnemyHp);

                        PX = Memory.Read<float>(HeadLocation);
                        PY = Memory.Read<float>(HeadLocation+ 4);
                        PZ = Memory.Read<float>(HeadLocation + 8);

                        NYaw = (float)(Math.Asin((EZ - PZ)/ GetDistance(EX, EY, EZ, PX, PY, PZ)) * 180.0f / Math.PI) ;
                        NPitch = -(float)(Math.Atan2(EX - PX, EY - PY) / Math.PI * 180.0f);
                        if ((MyTeamID != TeamId) && (EHP > 34.0f))
                        {
                            Memory.Write<float>(Pitch, NPitch);
                            Memory.Write<float>(Yaw, NYaw);
                        }

                    }
                    else { canfocus = false; }
                }

            }
            catch { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Cheats.InventoryManager(cbxSlots.Text, cbxPW.Text, cbxSW.Text, GameState);
        }
    }
}
