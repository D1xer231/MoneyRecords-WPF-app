using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoneyRecords.ViewModels
{
    public static class ColourRandomiser
    {
        static List<TextBlock> Colours = new List<TextBlock>() 
        {
            new TextBlock { Foreground = new SolidColorBrush(Colors.Red) },
            new TextBlock { Foreground = new SolidColorBrush(Colors.Blue) },
            new TextBlock { Foreground = new SolidColorBrush(Colors.Green) },
            new TextBlock { Foreground = new SolidColorBrush(Colors.Black) },
            new TextBlock { Foreground = new SolidColorBrush(Colors.Gray) },
            new TextBlock { Foreground = new SolidColorBrush(Colors.Purple) },
            new TextBlock { Foreground = new SolidColorBrush(Colors.BlueViolet) },
        };
        private static readonly Random rand = new Random();
        public static TextBlock GetRandomColor()
        {
            int index = rand.Next(Colours.Count);
            return Colours[index];
        }
    }
}
