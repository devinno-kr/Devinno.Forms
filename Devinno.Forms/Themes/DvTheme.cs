using Devinno.Forms.Icons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms.Themes
{
    public abstract class DvTheme : Component
    {
        #region Properties
        public static DvTheme DefaultTheme { get; set; } = new BlackTheme();

        public abstract string ThemeName { get; }

        public abstract Color ForeColor { get; set; }
        public abstract Color BackColor { get; set; }
        public abstract Color Color0 { get; set; }
        public abstract Color Color1 { get; set; }
        public abstract Color Color2 { get; set; }
        public abstract Color Color3 { get; set; }
        public abstract Color Color4 { get; set; }
        public abstract Color Color5 { get; set; }
        public abstract Color PointColor { get; set; }
        public abstract Color FrameColor { get; set; }
        public abstract Color ScrollBarColor { get; set; }
        public abstract Color ScrollCursorColor { get; set; }
        public abstract Color ColumnColor { get; set; }

        public abstract int Corner { get; set; }
        public abstract int TextOffsetX { get; set; }
        public abstract int TextOffsetY { get; set; }
        public abstract int ShadowGap { get; set; }

        public abstract double DownBright { get; set; }
        public abstract double BorderBright { get; set; }
        public abstract double GradientLightBright { get; set; }
        public abstract double GradientDarkBright { get; set; }
        public abstract double OutShadowBright { get; set; }
        public abstract double InShadowBright { get; set; }
        public abstract double OutBevelBright { get; set; }
        public abstract double InBevelBright { get; set; }
        
        public abstract int DisableAlpha { get; set; }
        public abstract int BevelAlpha { get; set; }
        public abstract int ShadowAlpha { get; set; }

        public abstract ThemeMenuColorTable MenuColorTable { get; }
        #endregion
        #region Method
        public abstract void DrawBox(Graphics g, Color c, Color bg, Rectangle bounds, RoundType round = RoundType.ALL, BoxDrawOption option = BoxDrawOption.NONE);
        public abstract void DrawBorder(Graphics g, Color borderColor, Color bg, int borderWidth, Rectangle bounds, RoundType round = RoundType.ALL, BoxDrawOption option = BoxDrawOption.NONE);

        public abstract void DrawText(Graphics g, DvIcon icon, string Text, Font ft, Color c, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter);
        public abstract void DrawTextShadow(Graphics g, DvIcon icon, string Text, Font ft, Color c, Color bg, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter);

        public abstract void DrawText(Graphics g, DvIcon icon, string Text, Font ft, Color c, Color cico, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter);
        public abstract void DrawTextShadow(Graphics g, DvIcon icon, string Text, Font ft, Color c, Color cico, Color bg, Rectangle bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter);
        #endregion
        #region Static Method
        internal static void SetTheme(Control control, DvTheme theme)
        {
            control.ForeColor = theme.ForeColor;
            control.BackColor = theme.BackColor;

            foreach (var c in control.Controls)
                if (c is Control)
                    SetTheme((Control)c, theme);
        }

        public static void LoopControl(Control control, Action<Control> Func)
        {
            Func(control);

            foreach (var c in control.Controls)
                if (c is Control)
                    LoopControl((Control)c, Func);
        }
        #endregion
    }

    public abstract class ThemeMenuColorTable : ProfessionalColorTable
    {
        #region Abstract
        public abstract Color MenuStripColor { get; set; }
        public abstract Color MenuItemSelectedColor { get; set; }
        public abstract Color MenuItemBorderColor { get; set; }
        public abstract Color MenuBorderColor { get; set; }
        public abstract Color MenuItemPressedColor { get; set; }
        public abstract Color ToolStripDropDownBackgroundColor { get; set; }
        public abstract Color ImageMarginColor { get; set; }
        public abstract Color SeparatorColor { get; set; }
        public abstract Color TextColor { get; set; }
        #endregion
        #region Override
        //메뉴바 색상
        public override Color MenuStripGradientBegin { get => MenuStripColor; }
        public override Color MenuStripGradientEnd { get => MenuStripColor; }

        //메뉴선택
        public override Color MenuItemSelectedGradientBegin { get { return MenuItemSelectedColor; } }
        public override Color MenuItemSelectedGradientEnd { get { return MenuItemSelectedColor; } }
        public override Color MenuItemSelected { get { return MenuItemSelectedColor; } }

        //메뉴아이템 보더
        public override Color MenuItemBorder { get { return MenuItemBorderColor; } }

        //메뉴전체 보더
        public override Color MenuBorder { get { return MenuBorderColor; } }

        //메뉴선택시 아이템색상
        public override Color MenuItemPressedGradientBegin { get { return MenuItemPressedColor; } }
        public override Color MenuItemPressedGradientMiddle { get { return MenuItemPressedColor; } }
        public override Color MenuItemPressedGradientEnd { get { return MenuItemPressedColor; } }

        //메뉴펼쳐질시 배경색
        public override Color ToolStripDropDownBackground { get { return ToolStripDropDownBackgroundColor; } }

        //이미지 아이콘 영역 배경색
        public override Color ImageMarginGradientBegin { get { return ImageMarginColor; } }
        public override Color ImageMarginGradientEnd { get { return ImageMarginColor; } }
        public override Color ImageMarginGradientMiddle { get { return ImageMarginColor; } }

        public override Color SeparatorDark { get { return SeparatorColor; } }
        public override Color SeparatorLight { get { return SeparatorColor; } }

        //화살표
        /*
        public override Color ToolStripBorder { get { return Color.Black; } }
        public override Color ToolStripContentPanelGradientBegin { get { return Color.FromArgb(30, 30, 30); } }
        public override Color ToolStripContentPanelGradientEnd { get { return Color.FromArgb(30, 30, 30); } }
        public override Color ToolStripGradientBegin { get { return Color.Black; } }
        public override Color ToolStripGradientMiddle { get { return Color.Black; } }
        public override Color ToolStripGradientEnd { get { return Color.Black; } }
        public override Color ToolStripPanelGradientBegin { get { return Color.DarkSlateGray; } }
        public override Color ToolStripPanelGradientEnd { get { return Color.DarkSlateGray; } }
        public override Color RaftingContainerGradientBegin { get { return Color.DarkSlateGray; } }
        public override Color RaftingContainerGradientEnd { get { return Color.DarkSlateGray; } }

        //public override Color ImageMarginGradientBegin { get { return Color.FromArgb(88, 104, 120); } }
        //public override Color ImageMarginGradientEnd { get { return Color.FromArgb(88, 104, 120); } }
        //public override Color ImageMarginGradientMiddle { get { return Color.FromArgb(88, 104, 120); } }

        public override Color CheckSelectedBackground { get { return Color.Orange; } }
        public override Color CheckBackground { get { return Color.Orange; } }
        public override Color CheckPressedBackground { get { return Color.SlateGray; } }
        public override Color ButtonCheckedHighlightBorder { get { return Color.OrangeRed; } }
        */
        #endregion
    }
}
