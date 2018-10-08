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

namespace MultiscaleModelingApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Grain[,] GrainTable { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void PaintPane()
        {
           
            int numOfCells = int.Parse(XTextBox.Text);
            int width = (int)MainCanvas.Width / numOfCells;
            for (int i = 0; i < MainCanvas.Width; i += width)
            {
                Line line = new Line();
                line.Stroke = Brushes.DarkBlue;

                line.X1 = i;
                line.X2 = i;
                line.Y1 = 0;
                line.Y2 = MainCanvas.Height;

                line.StrokeThickness = 0.5f;
                MainCanvas.Children.Add(line);
            }
            numOfCells = int.Parse(YTextBox.Text);
            int height = (int)MainCanvas.Height / numOfCells;
            for (int i = 0; i < MainCanvas.Height; i += height)
            {
                Line line = new Line();
                line.Stroke = Brushes.DarkBlue;

                line.X1 = 0;
                line.X2 = MainCanvas.Width;
                line.Y1 = i;
                line.Y2 = i;

                line.StrokeThickness = 0.5f;
                MainCanvas.Children.Add(line);
            }
            GrainTable = new Grain[int.Parse(XTextBox.Text), int.Parse(YTextBox.Text)];
            for (int i = 0; i < int.Parse(XTextBox.Text); i++)
            {
                for (int j = 0; j < int.Parse(YTextBox.Text); j++)
                {
                    GrainTable[i, j] = new Grain(0, int.Parse(XTextBox.Text) / (int)MainCanvas.Width, int.Parse(YTextBox.Text) / (int)MainCanvas.Height);
                    Canvas.SetLeft(GrainTable[i, j].Rect, i*width);
                    Canvas.SetTop(GrainTable[i, j].Rect, i*height);
                    MainCanvas.Children.Add(GrainTable[i, j].Rect);
                }
            }
        }

        private void DrawPane(object sender, RoutedEventArgs e)
        {

            PaintPane();
        }

        private void SeedBtn_Click(object sender, RoutedEventArgs e)
        {
            int numofSeeds = int.Parse(SeedCountTxtBox.Text);
            Random r = new Random();
            for (int i = 0; i < numofSeeds; i++)
            {
                GrainTable[r.Next(int.Parse(XTextBox.Text)), r.Next(int.Parse(YTextBox.Text))].Seed();
            }
            Thread t = new Thread(UpdateCanvas);
            t.Start();
        }
        private void UpdateCanvas()
        {
            foreach(Grain g in GrainTable)
            {
                if (g!=null&&g.State == 1)
                {
                    int a = 0;
                }
            }
        }
    }
}
