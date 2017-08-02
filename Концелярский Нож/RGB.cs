namespace Концелярский_Нож
{
    using System.Drawing;
    public class Rgb
    {
        private byte r, g, b;

        public byte Red
        {
            get => this.r;
            set => this.r = value;
        }

        public byte Green
        {
            get => this.g;
            set => this.g = value;
        }

        public byte Blue
        {
            get => this.b;
            set => this.b = value;
        }

        public Color Color
        {
            get => Color.FromArgb(this.Red, this.Green, this.Blue);
            set
            {
                this.r = value.R;
                this.g = value.G;
                this.b = value.B;
            }
        }
    }
}