using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiscaleModelingApp.Model;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;

namespace MultiscaleModelingApp
{
    public class FileManager
    {
        public static void Save2TextFile(Grain[,] grainTable, int xNumOfCells, int yNumOfCells)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Txt file|*.txt";
            saveFileDialog1.Title = "Save a File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                using (StreamWriter file = new StreamWriter(saveFileDialog1.FileName))
                {
                    file.WriteLine(xNumOfCells + " " + yNumOfCells);
                    for (int i = 0; i < xNumOfCells; i++)
                    {
                        for (int j = 0; j < yNumOfCells; j++)
                        {
                            file.WriteLine(i + " " + j + " " + grainTable[i, j].State + " " + grainTable[i, j].Color);
                        }
                    }
                }
            }
        }
        public static Grain[,] ReadFromTextFile(Canvas mainCanvas, ref int xNumOfCells, ref int yNumOfCells)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt file|*.txt";
            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    var tab = sr.ReadLine().Split();
                    xNumOfCells = int.Parse(tab[0]);
                    yNumOfCells = int.Parse(tab[1]);
                    MainWindow.GrainTable = new Grain[xNumOfCells, yNumOfCells];
                    MainWindow.TempGrainTable = new Grain[xNumOfCells, yNumOfCells];
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parsedline = line.Split();
                        int x= int.Parse(parsedline[0]);
                        int y = int.Parse(parsedline[1]);
                        int state = int.Parse(parsedline[2]);
                        Color color= (Color)ColorConverter.ConvertFromString(parsedline[3]);
                        MainWindow.GrainTable[x,y]=new Grain((int)mainCanvas.Width / xNumOfCells, (int)mainCanvas.Height / yNumOfCells,color);
                        MainWindow.GrainTable[x, y].X = x;
                        MainWindow.GrainTable[x, y].Y = y;
                        MainWindow.GrainTable[x, y].State = state;
                        MainWindow.GrainTable[x, y].Color = color;
                        MainWindow.TempGrainTable[x, y] = new Grain((int)mainCanvas.Width / xNumOfCells, (int)mainCanvas.Height / yNumOfCells,color);
                    }
                    Growth.Replace(MainWindow.TempGrainTable, MainWindow.GrainTable, xNumOfCells, yNumOfCells);
                }
            }
            return null;
        }
        public static void Save2Bitmap(Canvas mainCanvas)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Png file|*.png";
            saveFileDialog1.Title = "Save a bitmap";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)mainCanvas.Width, (int)mainCanvas.Height,96d, 96d, PixelFormats.Pbgra32);
                // needed otherwise the image output is black
                mainCanvas.Measure(new Size((int)mainCanvas.Width, (int)mainCanvas.Height));
                mainCanvas.Arrange(new Rect(new Size((int)mainCanvas.Width, (int)mainCanvas.Height)));

                renderBitmap.Render(mainCanvas);

                BitmapEncoder bmpencoder = new PngBitmapEncoder();
                bmpencoder.Frames.Add(BitmapFrame.Create(renderBitmap));

                using (var fs = System.IO.File.OpenWrite(saveFileDialog1.FileName))
                {
                    bmpencoder.Save(fs);
                }
            }
        }
        public static void LoadFromBitmap(Canvas mainCanvas)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Png file|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(openFileDialog.FileName));
                mainCanvas.Background = brush;
            }
        }

    }
}
