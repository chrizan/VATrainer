using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace VATrainer.ViewModels
{
    public class TrainingPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        public TrainingPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            NavigateCommand = new DelegateCommand<string>(NavigateCommandExecuted);
        }

        public PathGeometry Path
        {
            get
            {
                return GetPathGeometry();
            }

        }

        private PathGeometry GetPathGeometry()
        {
            PathGeometry pathGeometry = new PathGeometry();

            PathFigure pathFigure = new PathFigure
            {
                StartPoint = new Point(0, 0)
            };
            PathSegment pathSegment = new LineSegment(new Point(10,0));
            pathFigure.Segments.Add(pathSegment);
            pathGeometry.Figures.Add(pathFigure);

            PathFigure pathFigure1 = new PathFigure
            {
                StartPoint = new Point(0, 5)
            };
            PathSegment pathSegment1 = new LineSegment(new Point(10, 5));
            pathFigure1.Segments.Add(pathSegment1);
            pathGeometry.Figures.Add(pathFigure1);

            PathFigure pathFigure2 = new PathFigure
            {
                StartPoint = new Point(0, 10)
            };
            PathSegment pathSegment2 = new LineSegment(new Point(10, 10));
            pathFigure2.Segments.Add(pathSegment2);
            pathGeometry.Figures.Add(pathFigure2);

            PathFigure pathFigure3 = new PathFigure
            {
                StartPoint = new Point(0, 15)
            };
            PathSegment pathSegment3 = new LineSegment(new Point(10, 15));
            pathFigure3.Segments.Add(pathSegment3);
            pathGeometry.Figures.Add(pathFigure3);

            PathFigure pathFigure4 = new PathFigure
            {
                StartPoint = new Point(0, 20)
            };
            PathSegment pathSegment4 = new LineSegment(new Point(10, 20));
            pathFigure4.Segments.Add(pathSegment4);
            pathGeometry.Figures.Add(pathFigure4);

            return pathGeometry;
        }

        public DelegateCommand<string> NavigateCommand { get; }

        private async void NavigateCommandExecuted(string view)
        {
            await _navigationService.NavigateAsync(view);
        }
    }
}
