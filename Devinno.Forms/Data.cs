using Devinno.Forms.Controls;
using Devinno.Forms.Dialogs;
using Devinno.Forms.Icons;
using Devinno.Forms.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devinno.Forms
{
    #region Enums
    public enum DvContentAlignment
    {
        TopLeft, TopCenter, TopRight,
        MiddleLeft, MiddleCenter, MiddleRight,
        BottomLeft, BottomCenter, BottomRight
    }
    
    public enum DvTextIconAlignment { LeftRight, TopBottom }
    public enum DvPosition { Left, Top, Right, Bottom }
    public enum DvDirection { Left, Right, Up, Down }
    public enum DvDirectionHV { Horizon, Vertical }
    public enum DvSizeMode { Pixel, Percent }
    public enum DvNumberBoxStyle { UpDown, LeftRight, RightUpDown, Right };
    public enum DvPictureScaleMode { Real, CenterImage, Strech, Zoom }
    public enum DvTextBoxType { Number, Floating, Text, Hex }
    public enum DvStepButtonStyle { PlusMinus, LeftRight }
    public enum DvBarGraphMode { Stack, List }
    public enum DvDockSide { Left, Right }
    public enum DvDropState { Closed, Closing, Dropping, Dropped }
 
    public enum DateTimePickerType { Date, Time, DateTime }
    public enum ColorCodeType { ARGB, RGB, CodeRGB, CodeARGB }
    public enum ItemSelectionMode { None, Single, Multi }
    public enum BorderThickness { Thin, Normal, Bold }
    public enum Embossing { Flat, FlatConcave, FlatConvex, Concave, Convex }
    public enum Fill
    {
        None,
        Fill,
        GradientV, GradientVR, 
        GradientH, GradientHR,
        GradientLT, GradientRT,
        GradientLB, GradientRB,
    }
    #endregion

    #region class : SizeInfo
    public class SizeInfo
    {
        public DvSizeMode Mode { get; set; }
        public float Size { get; set; }
        
        public SizeInfo(DvSizeMode mode, float size)
        {
            this.Mode = mode;
            this.Size = size;
        }
    }
    #endregion
    #region class : TextIcon
    public class TextIcon
    {
        #region Member Variable
        private DvIcon ico = new DvIcon();
        #endregion

        #region Properteis
        public DvIcon Icon => ico;
        public Bitmap IconImage
        {
            get => ico.IconImage;
            set => ico.IconImage = value;
        }
        public string IconString
        {
            get => ico.IconString;
            set => ico.IconString = value;
        }
        public float IconSize
        {
            get => ico.IconSize;
            set => ico.IconSize = value;
        }
        public int IconGap
        {
            get => ico.Gap;
            set => ico.Gap = value;
        }
        public DvTextIconAlignment IconAlignment
        {
            get => ico.Alignment;
            set => ico.Alignment = value;
        }

        public string Text { get; set; }
        public Padding TextPadding { get; set; } = new Padding(0);

        public object Tag { get; set; }
        public object Value { get; set; }
        #endregion
    }
    #endregion
 
    #region classes : EventArgs
    #region class : ThemeDrawEventArgs
    public class ThemeDrawEventArgs
    {
        public Graphics Graphics { get; private set; }
        public Rectangle ClipRectangle { get; private set; }
        public DvTheme Theme { get; private set; }

        public ThemeDrawEventArgs(Graphics g, Rectangle clip, DvTheme theme)
        {
            this.Graphics = g;
            this.ClipRectangle = clip;
            this.Theme = theme;
        }
    }
    #endregion
    #region class : ButtonsClickventArgs
    public class ButtonsClickventArgs : EventArgs
    {
        public ButtonInfo Button { get; private set; }

        public ButtonsClickventArgs(ButtonInfo Button)
        {
            this.Button = Button;
        }
    }
    #endregion
    #region class : ButtonsSelectedventArgs
    public class ButtonsSelectedventArgs : EventArgs
    {
        public ButtonInfo Button { get; private set; }

        public ButtonsSelectedventArgs(ButtonInfo Button)
        {
            this.Button = Button;
        }
    }
    #endregion
    #region class : MenuSelectedEventArgs
    public class MenuSelectedEventArgs : EventArgs
    {
        public DvFormMenuSelector Menu { get; private set; }

        public MenuSelectedEventArgs(DvFormMenuSelector Menu)
        {
            this.Menu = Menu;
        }
    }
    #endregion
    #region class : ItemClickedEventArgs
    public class ItemClickedEventArgs : EventArgs
    {
        public TextIcon Item { get; private set; }

        public ItemClickedEventArgs(TextIcon Item) { this.Item = Item; }
    }
    #endregion
    #endregion
    #region classes : Graph
    #region class : GraphSeries
    public class GraphSeries
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public Color SeriesColor { get; set; }
    }
    #endregion
    #region class : GraphSeries2
    public class GraphSeries2 : GraphSeries
    {
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public bool Visible { get; set; } = true;
    }
    #endregion

    #region abstract class : GraphData
    public abstract class GraphData
    {
        public abstract string Name { get; set; }
        public Color Color { get; set; }
    }
    #endregion
    #region abstract class : TimeGraphData
    public abstract class TimeGraphData
    {
        public abstract DateTime Time { get; set; }
    }
    #endregion

    #region class : GV
    class GV
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Dictionary<string, double> Values
        {
            get
            {
                var ret = new Dictionary<string, double>();
                foreach (var vk in Props.Keys) ret.Add(vk, Convert.ToDouble(Props[vk].GetValue(Data)));
                return ret;
            }
        }

        internal Dictionary<string, PropertyInfo> Props { get; set; }
        internal GraphData Data { get; set; }
    }
    #endregion
    #region class : TGV
    class TGV
    {
        public DateTime Time { get; set; }
        public Dictionary<string, double> Values { get; } = new Dictionary<string, double>();
    }
    #endregion
    #region class : LGV
    class LGV
    {
        public PointF Position { get; set; }
        public double Value { get; set; }
    }
    #endregion
    #region class : CGV
    class CGV
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public Color Color { get; set; }
    }
    #endregion
    #endregion
}
