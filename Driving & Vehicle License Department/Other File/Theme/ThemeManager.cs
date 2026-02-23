 
using System.Drawing;
using System.Windows.Forms;

namespace DrivingVehicleLicenseDepartment.Ather_File
{

    public enum AppTheme { Light, Dark }

    public static class ThemeManager
    {
        public static AppTheme CurrentTheme { get; private set; } = AppTheme.Light;

        // Dark palette (softer + better contrast)
        private static readonly Color DarkBack = Color.FromArgb(18, 18, 18);   // app background
        private static readonly Color DarkSurface = Color.FromArgb(28, 28, 28);   // panels/cards
        private static readonly Color DarkInput = Color.FromArgb(35, 35, 35);   // textbox/combobox
        private static readonly Color DarkBorder = Color.FromArgb(55, 55, 55);   // borders/grid
        private static readonly Color DarkText = Color.FromArgb(230, 230, 230);// main text
        private static readonly Color DarkMuted = Color.FromArgb(160, 160, 160);// secondary text
        private static readonly Color Accent = Color.FromArgb(64, 156, 255); // nice blue accent

        private static readonly Color LightBack = SystemColors.Control;
        private static readonly Color LightSurface = Color.White;
        private static readonly Color LightText = Color.Black;

        public static void SetTheme(AppTheme theme)
        {
            CurrentTheme = theme;

            
            foreach (Form f in Application.OpenForms)
                ApplyTheme(f);
        }

        public static void ApplyTheme(Control root)
        {
            if (root == null) return;

            ApplyToControl(root);

            
            foreach (Control child in root.Controls)
                ApplyTheme(child);
        }
        private static void ApplyToolStripItemsTheme(ToolStripItemCollection items, bool dark)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = dark ? Color.FromArgb(230, 230, 230) : Color.Black;
                item.BackColor = dark ? Color.FromArgb(28, 28, 28) : SystemColors.Control;

                if (item is ToolStripMenuItem menuItem && menuItem.HasDropDownItems)
                {
                    menuItem.DropDown.Renderer = new ThemedToolStripRenderer(dark);
                    menuItem.DropDown.BackColor = item.BackColor;
                    menuItem.DropDown.ForeColor = item.ForeColor;

                    ApplyToolStripItemsTheme(menuItem.DropDownItems, dark);
                }
            }
        }

        private static void ApplyToControl(Control c)
        {
            bool dark = CurrentTheme == AppTheme.Dark;

            // Base colors
            var back = dark ? DarkBack : LightBack;
            var surface = dark ? DarkSurface : LightBack;
            var text = dark ? DarkText : LightText;

            // Default
            c.ForeColor = text;

            // Background by role/type
            if (c is Form || c is UserControl)
                c.BackColor = back;
            else if (c is Panel || c is GroupBox || c is TabPage)
                c.BackColor = surface;
            else
                c.BackColor = back;

            // Labels can be slightly muted if you want (optional)
            if (dark && c is Label lbl)
                lbl.ForeColor = text; // or DarkMuted for secondary labels

            // Inputs
            if (c is TextBox tb)
            {
                tb.BackColor = dark ? DarkInput : LightSurface;
                tb.ForeColor = text;
                tb.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (c is ComboBox cb)
            {
                cb.BackColor = dark ? DarkInput : LightSurface;
                cb.ForeColor = text;
                cb.FlatStyle = FlatStyle.Flat;
            }

            // Buttons: accent look
            else if (c is Button btn)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;

                if (dark)
                {
                    btn.BackColor = DarkSurface;
                    btn.ForeColor = DarkText;
                    btn.FlatAppearance.BorderColor = DarkBorder;

                    // Optional: make primary buttons more visible
                    // If you have a naming convention like btnSave/btnAdd:
                    if (btn.Name.ToLower().Contains("save") || btn.Name.ToLower().Contains("add"))
                    {
                        btn.BackColor = Accent;
                        btn.ForeColor = Color.White;
                        btn.FlatAppearance.BorderColor = Accent;
                    }
                }
                else
                {
                    btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
                }
            }

            // DataGridView styling (big improvement)
            else if (c is DataGridView dgv)
            {
                dgv.EnableHeadersVisualStyles = false;
                dgv.BorderStyle = BorderStyle.FixedSingle;
                dgv.BackgroundColor = dark ? back : LightSurface;
                dgv.GridColor = dark ? DarkBorder : SystemColors.ControlDark;

                dgv.DefaultCellStyle.BackColor = dark ? DarkSurface : LightSurface;
                dgv.DefaultCellStyle.ForeColor = text;
                dgv.DefaultCellStyle.SelectionBackColor = dark ? Color.FromArgb(45, 90, 140) : SystemColors.Highlight;
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;

                dgv.ColumnHeadersDefaultCellStyle.BackColor = dark ? DarkInput : SystemColors.Control;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = text;
                dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

                dgv.RowHeadersDefaultCellStyle.BackColor = dark ? DarkInput : SystemColors.Control;
                dgv.RowHeadersDefaultCellStyle.ForeColor = text;

                dgv.AlternatingRowsDefaultCellStyle.BackColor = dark ? Color.FromArgb(24, 24, 24) : Color.FromArgb(245, 245, 245);
            }
            else if (c is ContextMenuStrip cms)
            {
         

                cms.Renderer = new ThemedToolStripRenderer(dark);
                cms.BackColor = dark ? Color.FromArgb(28, 28, 28) : SystemColors.Control;
                cms.ForeColor = dark ? Color.FromArgb(230, 230, 230) : Color.Black;

                // طبّق على كل العناصر داخل القائمة
                ApplyToolStripItemsTheme(cms.Items, dark);
            }
            else if (c is MenuStrip ms)
            {
               
                ms.Renderer = new ThemedToolStripRenderer(dark);
                ms.BackColor = dark ? Color.FromArgb(28, 28, 28) : SystemColors.Control;
                ms.ForeColor = dark ? Color.FromArgb(230, 230, 230) : Color.Black;
                ApplyToolStripItemsTheme(ms.Items, dark);
            }
            else if (c is ToolStrip ts)
            {
               
                ts.Renderer = new ThemedToolStripRenderer(dark);
                ts.BackColor = dark ? Color.FromArgb(28, 28, 28) : SystemColors.Control;
                ts.ForeColor = dark ? Color.FromArgb(230, 230, 230) : Color.Black;
            }
        }
    }
}
