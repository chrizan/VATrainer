using Xamarin.Forms;

namespace VATrainer.Views
{
    public struct FontSizeCalculator
    {
        public FontSizeCalculator(Label label, double fontSize, double containerWidth)
        {
            FontSize = fontSize;

            label.FontSize = fontSize;
            SizeRequest sizeRequest = label.Measure(containerWidth, double.PositiveInfinity);

            TextHeight = sizeRequest.Request.Height;
        }

        public double FontSize { get; private set; }

        public double TextHeight { get; private set; }
    }
}
