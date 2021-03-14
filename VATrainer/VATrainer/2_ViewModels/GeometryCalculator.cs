using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class GeometryCalculator : IGeometryCalculator
    {
        private const double DeckWidth = 20;
        private const double DeckSpacing = 2;

        private const double ArrowLenght = 100;
        private const double ArrowWidth = 20;
        private const double ArrowTipLenght = 40;
        private const double ArrowTipWidth = 40;

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

        public PointCollection GetArrowPoints()
        {
            PointCollection pointCollection = new PointCollection
            {
                new Point(0, 0),
                new Point(ArrowTipLenght, ArrowTipWidth / 2),
                new Point(ArrowTipLenght, ArrowWidth / 2),
                new Point(ArrowLenght, ArrowWidth / 2),
                new Point(ArrowLenght, -ArrowWidth / 2),
                new Point(ArrowTipLenght, -ArrowWidth / 2),
                new Point(ArrowTipLenght, -ArrowTipWidth / 2)
            };
            return pointCollection;
        }
    }
}
