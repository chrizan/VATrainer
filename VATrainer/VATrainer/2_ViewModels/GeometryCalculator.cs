using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class GeometryCalculator : IGeometryCalculator
    {
        private const double CardWidth = 25;
        private const double CardSpacing = 2;

        public GeometryGroup GetDeckGeometry(int numberOfCards)
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for (int i = 0; i < numberOfCards; i++)
            {
                double y = CardSpacing + i * CardSpacing;
                LineGeometry lineGeometry = new LineGeometry() 
                { 
                    StartPoint = new Point(0, y), 
                    EndPoint = new Point(CardWidth, y) 
                };
                geometryGroup.Children.Add(lineGeometry);
            }

            return geometryGroup;
        }
    }
}
