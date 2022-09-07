using Devinno.Extensions;
using Devinno.Forms.Containers;
using Devinno.Forms.Icons;
using Devinno.Forms.Utils;
using Devinno.Tools;
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
    #region class : DvTheme
    public abstract class DvTheme : Component
    {
        public static DvTheme DefaultTheme = new BlackTheme();
        public const float GP = 6F;
        public const float GP2 = 10F;

        public abstract string ThemeName { get; }
        public abstract ThemeBrightness Brightness { get; }
        public abstract float DownBrightness { get; set; }
        public abstract float BorderBrightness { get; set; }
        public abstract float GradientLight { get; set; }
        public abstract float GradientDark { get; set; }
        public abstract int Corner { get; set; }
        public abstract float KeySpecialButtonBrightness { get; }
        
        public abstract byte OutShadowAlpha { get; set; }
        public abstract byte OutBevelAlpha { get; set; }
        public abstract byte InShadowAlpha { get; set; }
     
        public abstract byte GradientLightAlpha { get; }
        public abstract byte GradientDarkAlpha { get; }
        public abstract float DataGridInputBright { get; }
        public abstract float DataGridCheckBoxBright { get; }
        public abstract float DataGridColumnBevelBright { get; }
        public abstract float DataGridRowBevelBright { get; }
        public abstract int DisableAlpha { get; } 
        public abstract int TextOffsetY { get; }
        public abstract int TextOffsetX { get; }

        public abstract Color ForeColor { get; }
        public abstract Color BackColor { get; }
        public abstract Color ButtonColor { get; }

      
        public abstract Color LabelColor { get; }
        public abstract Color InputColor { get; }
        public abstract Color CheckBoxColor { get; }
        public abstract Color LampOnColor { get; }
        public abstract Color LampOffColor { get; }
        public abstract Color StepOnColor { get; }
        public abstract Color StepOffColor { get; }
        public abstract Color KnobColor { get; }
        public abstract Color KnobCursorColor { get; }
        public abstract Color NeedleColor { get; }
        public abstract Color NeedlePointColor { get; }
        public abstract Color ConcaveBoxColor { get; }
        public abstract Color GridColor { get; }
        public abstract Color PanelColor { get; }
        public abstract Color BorderPanelColor { get; }
        public abstract Color GroupBoxColor { get; }
        public abstract Color TabBackColor { get; }
        public abstract Color TabPageColor { get; }
        public abstract Color RowColor { get; }
        public abstract Color ColumnColor { get; }
        public abstract Color ListBackColor { get; }
        public abstract Color WindowTitleColor { get; }
        public abstract Color CalendarDaysColor { get; }
        public abstract Color CalendarWeeksColor { get; }
        public abstract Color CalendarMonthColor { get; }
        public abstract Color CalendarSelectColor { get; }
        public abstract Color PointColor { get; }
        public abstract Color ScrollBarColor { get; }
        public abstract Color ScrollCursorOffColor { get; }
        public abstract Color ScrollCursorOnColor { get; }
        public abstract Color MenuNormalColor { get; }
        public abstract Color MenuSelectedColor { get; }
        public abstract ThemeMenuColorTable MenuColorTable { get; }

        public abstract void DrawBox(Graphics g, RectangleF rect, Color boxColor, Color borderColor, RoundType round, BoxStyle style, int? Corner = null);
        public abstract void DrawText(Graphics g, string text, Font font, Color color, RectangleF rect, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true);
        public abstract void DrawTextIcon(Graphics g, DvIcon icon, Color colorIco, string text, Font font, Color colorText, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true);
        public abstract void DrawTextIcon(Graphics g, TextIcon texticon, Color colorIco, Color colorText, Font font, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true);
        public abstract void DrawIcon(Graphics g, DvIcon icon, Color color, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true);
        public void DrawTextIcon(Graphics g, TextIcon texticon, Font font, Color color, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true) => DrawTextIcon(g, texticon, color, color, font, bounds, align, shadow);
        public void DrawTextIcon(Graphics g, DvIcon icon, string text, Font font, Color color, RectangleF bounds, DvContentAlignment align = DvContentAlignment.MiddleCenter, bool shadow = true) => DrawTextIcon(g, icon, color, text, font, color, bounds, align, shadow);
        public abstract void DrawLamp(Graphics g, RectangleF bounds, Color BackColor, Color OnLampColor, Color OffLampColor, bool OnOff, bool Animation, Animation ani);
        public void DrawLamp(Graphics g, RectangleF bounds, Color BackColor, Color OnLampColor, Color OffLampColor, bool OnOff) => DrawLamp(g, bounds, BackColor, OnLampColor, OffLampColor, OnOff, false, null);

        public abstract Color GetBorderColor(Color fillColor, Color backColor);
        public abstract Color GetInBevelColor(Color BaseColor);
        public abstract void GetSwitchColors(Color Color, out Color c1, out Color c2, out Color c3, out Color c4);
        public abstract void GetLampColors(Color BackColor, Color OnLampColor, Color OffLampColor, bool OnOff,
                                           bool Animation, Animation ani,
                                           out Color BackLightColor, out Color BackDarkColor, 
                                           out Color LampLightColor, out Color LampDarkColor, out Color LampColor);

        #region Static Method
        internal static void SetTheme(Control control, DvTheme theme)
        {
            control.ForeColor = theme.ForeColor;
            control.BackColor = theme.BackColor;

            if (control is DvTabControl)
            {
                var c = control as DvTabControl;
                foreach (var v in c.TabPages.Cast<TabPage>()) v.BackColor = theme.BackColor;
            }
            else if (control is DvTablessControl)
            {
                var c = control as DvTabControl;
                foreach (var v in c.TabPages.Cast<TabPage>()) v.BackColor = theme.BackColor;
            }

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
    #endregion
    #region class : ThemeMenuColorTable
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
    #endregion

    #region enum : BoxStyle
    public enum BoxStyle : int
    {
        None = 0,
        Fill = 1,
        GradientV = 2, GradientV_R = 4, GradientH = 8, GradientH_R = 16, GradientLT = 32, GradientLT_R = 64, GradientRT = 128, GradientRT_R = 256,
        Border = 512,
        OutShadow = 1024, OutBevel = 2048,
        InShadow = 4096, InBevel = 8192,
    }
    #endregion
    #region enum : RoundType
    public enum RoundType
    {
        Rect,
        All,
        L, R, T, B,
        LT, RT, LB, RB,
        Ellipse,
    }
    #endregion
    #region enum : ThemeBrightness
    public enum ThemeBrightness { Light, Dark }
    #endregion
}
