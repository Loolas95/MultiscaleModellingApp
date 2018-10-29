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
                        MainWindow.GrainTable[x,y]=new Grain(x,y,(int)mainCanvas.Width / xNumOfCells, (int)mainCanvas.Height / yNumOfCells,color);
                        MainWindow.GrainTable[x, y].State = state;
                        MainWindow.TempGrainTable[x, y] = new Grain(x,y,(int)mainCanvas.Width / xNumOfCells, (int)mainCanvas.Height / yNumOfCells,color);
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
        public static void LoadFromBitmap(Canvas mainCanvas ,ref int xNumOfCells, ref int yNumOfCells)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Png file|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(openFileDialog.FileName))
                {
                   
                    int width = bmp.Width *3/ (int)(bmp.HorizontalResolution+0.5);
                    int height = bmp.Height*3 / (int)(bmp.VerticalResolution + 0.5);
                    xNumOfCells = width;
                    yNumOfCells = height;
                    Color[,] colortab = new Color[width, height];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            var clr = bmp.GetPixel(i* (int)(bmp.HorizontalResolution / 3 + 0.5) + (int)(bmp.HorizontalResolution/6), j * (int)(bmp.VerticalResolution / 3 + 0.5) + (int)(bmp.VerticalResolution /6));
                            colortab[i,j] = Color.FromRgb(clr.R, clr.G, clr.B);
                        }
                    }
                    MainWindow.GrainTable = new Grain[width, height];
                    MainWindow.TempGrainTable = new Grain[width, height];
                    
                    
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            MainWindow.GrainTable[i, j] = new Grain(i, j, (int)mainCanvas.Width / width, (int)mainCanvas.Height / height, colortab[i, j]);
                            MainWindow.TempGrainTable[i, j] = new Grain(i, j, (int)mainCanvas.Width / width, (int)mainCanvas.Height / height, colortab[i, j]);
                            if (colortab[i,j]!=Color.FromRgb(255,255,255))
                            {
                                MainWindow.GrainTable[i, j].State = 1;
                            }
                        }
                    }
                    Growth.Replace(MainWindow.TempGrainTable, MainWindow.GrainTable, width, height);

                }
         
            }
        }

    }
}
