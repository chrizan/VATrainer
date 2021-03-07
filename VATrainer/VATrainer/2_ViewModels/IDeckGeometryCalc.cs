using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public interface IDeckGeometryCalc
    {
        PathGeometry GetDeckGeometry(int numberOfCards);
    }
}