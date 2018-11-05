using MultiscaleModelingApp.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public static Grain[,] GrainTable { get; set; }
        public static Grain[,] TempGrainTable { get; set; }
        private bool RunningSimulation=false;
        private DispatcherTimer timer = new DispatcherTimer();
        private int XNumOfCells=0;
        private int YNumOfCells = 0;
        private Random rand = new Random();
        private Bitmap bitmap;
        private List<Grain> grains2Regrow;
        public static int Probability = 10;
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Update;
            //CompositionTarget.Rendering += Update;

        }

        private void PaintPane()
        {
            bitmap = new Bitmap(XNumOfCells, YNumOfCells);
            for(int i=0;i<XNumOfCells;i++)
            {
                for(int j=0;j<YNumOfCells;j++)
                {
                    bitmap.SetPixel(i, j, System.Drawing.Color.FromArgb(GrainTable[i, j].Color.A, GrainTable[i, j].Color.R, GrainTable[i, j].Color.G, GrainTable[i, j].Color.B));
                }
            }
            BitmapImage bitmapImage = Convert(bitmap);
            MainImage.Source = bitmapImage;
            //MainImage.Source = imagesource;
            /*inCanvas.Children.Clear();
            int width = (int)MainCanvas.Width / XNumOfCells;
            int height = (int)MainCanvas.Height / YNumOfCells;
            /*for (int i = 0; i < MainCanvas.Width; i += width)
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
            }*/
        }
        public BitmapImage Convert(Bitmap src)
        {
            MemoryStream ms = new MemoryStream();
            ((System.Drawing.Bitmap)src).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
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
                    GrainTable[i, j] = new Grain(i,j,(int)MainImage.Width/XNumOfCells, (int)MainImage.Height/YNumOfCells);
                    TempGrainTable[i,j] = new Grain(i,j,(int)MainImage.Width / XNumOfCells, (int)MainImage.Height / YNumOfCells);
                }
            }
            PaintPane();
        }

        private void SeedBtn_Click(object sender, RoutedEventArgs e)
        {
            int numofSeeds = int.Parse(SeedCountTxtBox.Text);
            Grain g;
            List<Grain> freeGrains = Grain.NumberOfFeeeCells();
            if (numofSeeds < freeGrains.Count)
            {
                for (int i = 0; i < numofSeeds; i++)
                {
                    do
                    {
                        g = freeGrains.ElementAt(rand.Next(freeGrains.Count));
                    } while (g.State != 0 && g.Inclusion==true);
                    g.Seed(rand);
                }
            }
         
            PaintPane();

        }
        private void Update(object sender, EventArgs e)
        {
            for (int i = 0; i < XNumOfCells; i++)
            {
                for (int j = 0; j < YNumOfCells; j++)
                {
                    if (!Growth.Moore(i, j,XNumOfCells,YNumOfCells))
                    {
                        if(!Growth.NearestMoore(i, j, XNumOfCells, YNumOfCells))
                        {
                            if (!Growth.FutherMoore(i, j, XNumOfCells, YNumOfCells))
                            {
                                Growth.RuleFour(i, j, XNumOfCells, YNumOfCells);
                            }
                        }
                    }
                }
            }

            Growth.Replace(GrainTable, TempGrainTable, XNumOfCells, YNumOfCells);
            PaintPane();
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

        private void SaveTextMenuItem(object sender, RoutedEventArgs e)
        {
            FileManager.Save2TextFile(GrainTable, XNumOfCells, YNumOfCells);
        }

        private void LoadFromFileMenuItem(object sender, RoutedEventArgs e)
        {
            FileManager.ReadFromTextFile(MainImage,ref XNumOfCells,ref YNumOfCells);
            PaintPane();
        }

        private void SaveBitmapMenuItem(object sender, RoutedEventArgs e)
        {
            FileManager.Save2Bitmap(bitmap);
        }

        private void LoadBitmap(object sender, RoutedEventArgs e)
        {
            FileManager.LoadFromBitmap(MainImage, ref XNumOfCells, ref YNumOfCells);
            PaintPane();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = int.Parse(AmountOfInclusionsTxtBox.Text);
                int size = int.Parse(SizeOfInclusionsTxtBox.Text);
                int type = TypeOfInclusionComboBox.SelectedIndex;
                switch (type)
                {
                    case 0:
                        Inclusions.Circular(amount,size,XNumOfCells, YNumOfCells);
                        break;
                    case 1:
                        Inclusions.Diagonal(amount, size, XNumOfCells, YNumOfCells);
                        break;
                    default:
                        Inclusions.Circular(amount, size, XNumOfCells, YNumOfCells);
                        break;
                }
                PaintPane();

            }
            catch (Exception) { }
            
        }



        private void MainImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Windows.Point p = e.GetPosition(this);
                double xpos = p.X-20;
                double ypos = p.Y-30;
                int x = (int)(xpos / MainImage.Width * XNumOfCells);
                int y = (int)(ypos / MainImage.Height * YNumOfCells);
                grains2Regrow.Add(GrainTable[x, y]);
            }
            catch (Exception) { }
            
        }

        private void RegrowBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int type = TypeOfStructureCmbBox.SelectedIndex;

                switch (type)
                {
                    case 0:
                        Regrowth.Substructure(grains2Regrow);
                        SeedBtn_Click(null, null);
                        Growth.Replace(TempGrainTable, GrainTable, XNumOfCells, YNumOfCells);
                        break;
                    case 1:
                        Regrowth.DualPhase(grains2Regrow);
                        SeedBtn_Click(null, null);
                        Growth.Replace(TempGrainTable, GrainTable, XNumOfCells, YNumOfCells);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception) { }
            
        }

        private void TypeOfStructureCmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            grains2Regrow = new List<Grain>();
        }
    }
}
