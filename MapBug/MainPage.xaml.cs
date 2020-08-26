using Windows.UI.Xaml.Controls;
using MapBug.ViewModels;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MapBug
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            myMap.Center = new Geopoint(new BasicGeoposition() { Latitude = 17.387140, Longitude = 78.491684 });
            myMap.ZoomLevel = 9;

            ViewModel = new MainViewModel();
        }

        public MainViewModel ViewModel { get; set; }

      
    }

    public class MapControl : DependencyObject
    {

        #region Dependency properties

        public static readonly DependencyProperty ZIndexProperty = DependencyProperty.RegisterAttached("ZIndex", typeof(int), typeof(MapControl), new PropertyMetadata(0, OnZIndexChanged));

        #endregion


        #region Methods

        public static int GetZIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(ZIndexProperty);
        }

        private static void OnZIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContentControl us = d as ContentControl;

            if (us != null)
            {
                // returns null if it is not loaded yet
                // returns 'DependencyObject' of type 'MapOverlayPresenter'
                var parent = VisualTreeHelper.GetParent(us);

                if (parent != null)
                {
                    parent.SetValue(Canvas.ZIndexProperty, e.NewValue);
                }
            }
        }

        public static void SetZIndex(DependencyObject obj, int value)
        {
            obj.SetValue(ZIndexProperty, value);
        }

        #endregion

    }
}
