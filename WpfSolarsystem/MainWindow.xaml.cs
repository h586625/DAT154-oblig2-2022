using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SpaceSim;

namespace WpfSolarSystem
{
    public partial class MainWindow : Window
    {

        List<SpaceObject> solarSystem = new();
        List<SpaceObject> solarSystemReal = new();

        double days = 0;
        readonly double SunScale = 500;
        double z = 1.0;
        double speed = .5;

        public MainWindow()
        {
            InitializeComponent();
            SolarSystem tempSol = new();
            solarSystem = tempSol.getSolar();
            solarSystemReal = tempSol.getRealSolar();

            makeLabelButton();
            makeZoomButton();
            makeSpeedButton();

            DispatcherTimer timer = new();
            timer.Tick += t_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            timer.Start();
        }

        private void makeSpeedButton()
        {
            Button fasterButton = new()
            {
                Content = "faster"
            };
            Button slowerButton = new()
            {
                Content = "slower"
            };
            Canvas.SetLeft(fasterButton, 0);
            Canvas.SetTop(fasterButton, 50);
            canvasMain.Children.Add(fasterButton);
            Canvas.SetLeft(slowerButton, 50);
            Canvas.SetTop(slowerButton, 50);
            canvasMain.Children.Add(slowerButton);
            fasterButton.Click += new RoutedEventHandler(faster);
            slowerButton.Click += new RoutedEventHandler(slower);
        }

        private void faster(object sender, RoutedEventArgs e)
        {
            speed += 2;
        }

        private void slower(object sender, RoutedEventArgs e)
        {
            speed -= 2;
        }

        private void makeZoomButton()
        {
            Button Plusbutton = new()
            {
                Content = " zoom in "
            };
            Button minusButton = new()
            {
                Content = " zoom out "
            };
            Canvas.SetLeft(Plusbutton, 0);
            Canvas.SetTop(Plusbutton, 30);
            Canvas.SetLeft(minusButton, 50);
            Canvas.SetTop(minusButton, 30);
            Plusbutton.Click += new RoutedEventHandler(zoomInn);
            minusButton.Click += new RoutedEventHandler(zoomOut);
            canvasMain.Children.Add(Plusbutton);
            canvasMain.Children.Add(minusButton);
        }


        private void zoomInn(object sender, RoutedEventArgs e)
        {
            var scaler = Grid.LayoutTransform as ScaleTransform;

            if (scaler == null)
            {
                scaler = new ScaleTransform(z, z);
                Grid.LayoutTransform = scaler;
            }
            DoubleAnimation animator = new()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(600)),
            };
            if (scaler.ScaleX == z)
            {
                z += 0.5;
                animator.To = z;
            }
            else if (scaler.ScaleX > 1.0)
            {
                z += 0.5;
                animator.To = z;
            }
            scaler.BeginAnimation(ScaleTransform.ScaleXProperty, animator);
            scaler.BeginAnimation(ScaleTransform.ScaleYProperty, animator);
        }
        private void zoomOut(object sender, RoutedEventArgs e)
        {
            var scaler = Grid.LayoutTransform as ScaleTransform;
            if (scaler == null)
            {
                scaler = new ScaleTransform(1.0, 1.0);
                Grid.LayoutTransform = scaler;
            }
            DoubleAnimation animator = new()
            {
                Duration = new Duration(TimeSpan.FromMilliseconds(600)),
            };
            if (scaler.ScaleX > 1.0)
            {
                z = scaler.ScaleX - 0.5;
                animator.To = z;
            }

            scaler.BeginAnimation(ScaleTransform.ScaleXProperty, animator);
            scaler.BeginAnimation(ScaleTransform.ScaleYProperty, animator);
        }

        private void spaceObjectInfo(object sender, MouseButtonEventArgs e)
        {
            ClearCanvasInfo();
            Ellipse ellipse = (Ellipse)sender;
            int i = 0;
            SpaceObject? temp;
            foreach (SpaceObject space in solarSystem)
            {
                if (ellipse.Height == space.ObjectRadius / SunScale)
                {
                    i = solarSystem.IndexOf(space);
                }
            }

            temp = solarSystemReal[i];

            if (temp.Parent == null)
            {
                TextBox textbox = new()
                {
                    Text = temp.Name + "\n" + "Object Radius: " + temp.ObjectRadius
                };
                Canvas.SetLeft(textbox, 0);
                Canvas.SetTop(textbox, 200);
               canvasInfo.Children.Add(textbox);
            }
            else
            {
                TextBox textbox = new();

                textbox.Text = temp.Name + "\nOrbital Radius: " + temp.OrbitalRadius + "*10^6 km " +
                "around the " + temp.Parent.Name + "\n" +
                "Orbital Period: " + temp.OrbitalPeriod + " days \n" +
                "Rotation Period: " + temp.RotationalPeriod + " days \n" +
                "Object Radius: " + temp.ObjectRadius + " km";

                if (temp.Children.Count > 0)
                {
                    textbox.Text += "\n \nMoons:\n";
                    foreach (SpaceObject child in temp.Children)
                    {
                        textbox.Text += child.Name + "\nOrbital Radius: " + child.OrbitalRadius + "*10^6 km " +
                        "around the " + child.Parent.Name + "\n" +
                        "Orbital Period: " + child.OrbitalPeriod + " days \n" +
                        "Rotation Period: " + child.RotationalPeriod + " days \n" +
                        "Object Radius: " + child.ObjectRadius + " km\n";
                    }
                }
                Canvas.SetLeft(textbox, 0);
                Canvas.SetTop(textbox, 200);
                canvasInfo.Children.Add(textbox);
            }
        }
        private void makeLabelButton()
        {
            Button button = new()
            {
                Content = "toggle labels"
            };
            Canvas.SetLeft(button, 0);
            Canvas.SetTop(button, 0);
            button.Click += new RoutedEventHandler(toggleLabel);
            canvasMain.Children.Add(button);
        }

        private void toggleLabel(object sender, RoutedEventArgs e)
        {
            if (canvasLabels.Visibility == Visibility.Collapsed)
            {
                canvasLabels.Visibility = Visibility.Visible;
            }
            else
            {
                canvasLabels.Visibility = Visibility.Collapsed;
            }
        }

        private void makeLabeldays()
        {
            Label label = new()
            {
                Content = "Days: " + (int)days + ".\nYears: " + (int)days / 365,
                Foreground = Brushes.White
            };
            Canvas.SetLeft(label, ActualWidth / 2);
            Canvas.SetTop(label, 50);
            canvasMain.Children.Add(label);
        }

        private Label makeLabel(SpaceObject obj)
        {
            Label label = new()
            {
                Content = obj.Name,
                Foreground = Brushes.White
            };
            return label;
        }

        private void ClearCanvasSpaceObj()
        {
            for (int i = canvasMain.Children.Count - 1; i >= 0; i--)
            {
                UIElement Child = canvasMain.Children[i];
                if (Child is Ellipse || Child is Label)
                    canvasMain.Children.Remove(Child);
            }
            for (int i = canvasLabels.Children.Count - 1; i >= 0; i--)
            {
                UIElement Child = canvasLabels.Children[i];
                if (Child is Label)
                    canvasLabels.Children.Remove(Child);
            }
        }
        private void ClearCanvasInfo()
        {
            for (int i = canvasInfo.Children.Count - 1; i >= 0; i--)
            {
                UIElement Child = canvasInfo.Children[i];
                if (Child is TextBox)
                    canvasInfo.Children.Remove(Child);
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            ClearCanvasSpaceObj();
            days += speed;
            makeSolarSystem();
            makeLabeldays();
        }

        public void makeSolarSystem()
        {
            foreach (SpaceObject obj in solarSystem)
            {
                double x = pos(obj, (int)days).Item1 + ((canvasMain.ActualWidth - ((obj.ObjectRadius) / SunScale)) / 2);
                double y = pos(obj, (int)days).Item2 + ((canvasMain.ActualHeight - ((obj.ObjectRadius) / SunScale)) / 2);

                Ellipse ellipse = makeSpaceObject(obj.ObjectRadius, obj.ObjectColor);

                ellipse.MouseLeftButtonDown += spaceObjectInfo;
                Label label = makeLabel(obj);

                Canvas.SetLeft(ellipse, x);
                Canvas.SetTop(ellipse, y);

                Canvas.SetLeft(label, x + 20);
                Canvas.SetTop(label, y);

                canvasLabels.Children.Add(label);
                canvasMain.Children.Add(ellipse);
            }
        }

        public Ellipse makeSpaceObject(double objectRadius, String color)
        {
            Ellipse ellipse = new()
            {
                Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color)),
                StrokeThickness = 2,
                Stroke = Brushes.Black,
                Width = objectRadius / SunScale,
                Height = objectRadius / SunScale
            };

            return ellipse;
        }

        public (double, double) pos(SpaceObject planet, int tid)
        {
            return planet.CalculatePosition(tid);
        }
    }
}
