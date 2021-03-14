using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class GeometryCalculator : IGeometryCalculator
    {
        private const double DeckWidth = 20;
        private const double DeckSpacing = 2;

        public PathGeometry GetDeckGeometry(int numberOfCards)
        {
            PathGeometry pathGeometry = new PathGeometry();

            for (int i = 0; i < numberOfCards; i++)
            {
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = new Point(0, i * DeckSpacing)
                };
                PathSegment pathSegment = new LineSegment(new Point(DeckWidth, i * DeckSpacing));
                pathFigure.Segments.Add(pathSegment);
                pathGeometry.Figures.Add(pathFigure);
            }

            return pathGeometry;
        }
    }
}
