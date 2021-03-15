using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public interface IGeometryCalculator
    {
        GeometryGroup GetDeckGeometry(int numberOfCards);
    }
}