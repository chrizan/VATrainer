using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class DeckGeometryCalc : IDeckGeometryCalc
    {
        private const int XStart = 0;
        private const int DefaultWidth = 20;
        private const double DefaultSpacing = 2;

        public PathGeometry GetDeckGeometry(int numberOfCards)
        {
            PathGeometry pathGeometry = new PathGeometry();

            for (int i = 0; i < numberOfCards; i++)
            {
                PathFigure pathFigure = new PathFigure
                {
                    StartPoint = new Point(XStart, i * DefaultSpacing)
                };
                PathSegment pathSegment = new LineSegment(new Point(DefaultWidth, i * DefaultSpacing));
                pathFigure.Segments.Add(pathSegment);
                pathGeometry.Figures.Add(pathFigure);
            }

            return pathGeometry;
        }
    }
}
