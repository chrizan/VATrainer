using System;
using Xamarin.Forms;

namespace VATrainer.Views
{
    public class SelfAdaptiveLabel : ContentView, IDisposable
    {
        private const int MinimumFontSize = 10;
        private const int MaximumFontSize = 100;

        public SelfAdaptiveLabel()
        {
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, EventArgs args)
        {
            if(!(Content is Label))
            {
                string message = $"The content of {nameof(SelfAdaptiveLabel)} is of type {Content.GetType()} but has to be of type {nameof(Label)}";
                throw new NotSupportedException(message);
            }

            View view = (View)sender;
            if (view.Width <= 0 || view.Height <= 0) return;

            FontSizeCalculator lowerFontCalc = new FontSizeCalculator((Label)Content, MinimumFontSize, view.Width);
            FontSizeCalculator upperFontCalc = new FontSizeCalculator((Label)Content, MaximumFontSize, view.Width);

            while (upperFontCalc.FontSize - lowerFontCalc.FontSize > 1)
            {
                double fontSize = (lowerFontCalc.FontSize + upperFontCalc.FontSize) / 2;

                FontSizeCalculator newFontCalc = new FontSizeCalculator((Label)Content, fontSize, view.Width);

                if (newFontCalc.TextHeight > view.Height)
                {
                    upperFontCalc = newFontCalc;
                }
                else
                {
                    lowerFontCalc = newFontCalc;
                }
            }

            ((Label)Content).FontSize = lowerFontCalc.FontSize;
        }

        ~SelfAdaptiveLabel()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                SizeChanged -= OnSizeChanged;
            }
        }
    }
}
