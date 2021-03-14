using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public interface IGeometryCalculator
    {
        PathGeometry GetDeckGeometry(int numberOfCards);
    }
}