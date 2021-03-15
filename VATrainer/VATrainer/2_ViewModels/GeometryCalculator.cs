using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class GeometryCalculator : IGeometryCalculator
    {
        private const double DeckWidth = 20;
        private const double DeckSpacing = 2;
        //If set to 0, single line (numberOfCards = 1) does not render
        private const double Spacer = 1;

        public GeometryGroup GetDeckGeometry(int numberOfCards)
        {
            GeometryGroup geometryGroup = new GeometryGroup();

            for (int i = 0; i < numberOfCards; i++)
            {
                LineGeometry lineGeometry = new LineGeometry() 
                { 
                    StartPoint = new Point(0, Spacer + i * DeckSpacing), 
                    EndPoint = new Point(DeckWidth, Spacer + i * DeckSpacing) 
                };
                geometryGroup.Children.Add(lineGeometry);
            }

            return geometryGroup;
        }
    }
}
