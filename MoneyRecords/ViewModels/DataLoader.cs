using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Windows.Controls;

namespace MoneyRecords.ViewModels
{
    public partial class DataLoader
    {
        public static void Load(ItemsControl control, IEnumerable data)
        {
            control.ItemsSource = data.Cast<object>().ToList();
        }
    }
}
