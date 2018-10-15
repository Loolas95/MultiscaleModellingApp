using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MultiscaleModelingApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Grain[,] GrainTable { get; set; }
        public Grain[,] TempGrainTable { get; set; }
        private bool RunningSimulation=false;
        private DispatcherTimer timer = new DispatcherTimer();
        private int XNumOfCells=0;
        private int YNumOfCells = 0;
        private Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Update;
            //CompositionTarget.Rendering += Update;

        }

        private void PaintPane()
        {
            MainCanvas.Children.Clear();
            int width = (int)MainCanvas.Width / XNumOfCells;
            for (int i = 0; i < MainCanvas.Width; i += width)
            {
                Line line = new Line();
                line.Stroke = Brushes.DarkBlue;

                line.X1 = i;
                line.X2 = i;
                line.Y1 = 0;
                line.Y2 = MainCanvas.Height;

                line.StrokeThickness = 5f/XNumOfCells;
                MainCanvas.Children.Add(line);
            }

            int height = (int)MainCanvas.Height / YNumOfCells;
            for (int i = 0; i < MainCanvas.Height; i += height)
            {
                Line line = new Line();
                line.Stroke = Brushes.DarkBlue;

                line.X1 = 0;
                line.X2 = MainCanvas.Width;
                line.Y1 = i;
                line.Y2 = i;

                line.StrokeThickness = 5f/ XNumOfCells;
                MainCanvas.Children.Add(line);
            }
            
            for (int i = 0; i < XNumOfCells; i++)
            {
                for (int j = 0; j < YNumOfCells; j++)
                {
                    MainCanvas.Children.Add(GrainTable[i, j].Rect);
                    Canvas.SetLeft(GrainTable[i, j].Rect, i*width);
                    Canvas.SetTop(GrainTable[i, j].Rect, j*height);
                    
                }
            }
        }

        private void DrawPane(object sender, RoutedEventArgs e)
        {
            XNumOfCells= int.Parse(XTextBox.Text);
            YNumOfCells = int.Parse(YTextBox.Text);
            GrainTable = new Grain[XNumOfCells, YNumOfCells];
            TempGrainTable=new Grain[XNumOfCells, YNumOfCells];
            for (int i = 0; i < XNumOfCells ; i++)
            {
                for (int j = 0; j < YNumOfCells; j++)
                {
                    GrainTable[i, j] = new Grain((int)MainCanvas.Width/XNumOfCells, (int)MainCanvas.Height/YNumOfCells);
                    TempGrainTable[i,j] = new Grain((int)MainCanvas.Width / XNumOfCells, (int)MainCanvas.Height / YNumOfCells);
                }
            }
            PaintPane();
        }

        private void SeedBtn_Click(object sender, RoutedEventArgs e)
        {
            int numofSeeds = int.Parse(SeedCountTxtBox.Text);
            for (int i = 0; i < numofSeeds; i++)
            {
                GrainTable[rand.Next(int.Parse(XTextBox.Text)), rand.Next(int.Parse(YTextBox.Text))].Seed(rand);
            }

        }
        private void Update(object sender, EventArgs e)
        {
            for (int i = 0; i < XNumOfCells; i++)
            {
                for (int j = 0; j < YNumOfCells; j++)
                {
                    Growth.Moore(i, j, GrainTable, TempGrainTable,XNumOfCells,YNumOfCells);
                }
            }
            Growth.Replace(GrainTable, TempGrainTable, XNumOfCells, YNumOfCells);
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RunningSimulation)
            {
                
                RunningSimulation = false;
                StartBtn.Content = "START";
                timer.Stop();
            }
            else
            {
                RunningSimulation = true;
                StartBtn.Content = "STOP";
                timer.Start();
                
                
            }
            
        }
    }
}
