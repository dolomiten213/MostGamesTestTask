using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MostGames1
{

    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();
        
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Method is called by calc_button
        /// </summary>

        private async void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                calc_button.IsEnabled = false;
                Int32 offset = 0;
                var invalidSubstrings = await viewModel.Calculate();
                if (invalidSubstrings.Count > 0)
                {
                    foreach (var a in invalidSubstrings)
                    {
                        HighlightText(Ids, a.Item1, a.Item1 + a.Item2, Colors.MediumVioletRed, offset++);
                        offset++;
                    }
                    throw new Exception("Некорректная строка с идентификаторами!\n" +
                        "Убедитесть, что в строке содеражатся только идентификаторы (числа от 1 до 19), разделенные запятыми или точками с запятой");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                calc_button.IsEnabled = true;
            }
            
        }
        /// <summary>
        /// Highlights text in RichTextBox
        /// </summary>
        public static void HighlightText(RichTextBox richTextBox, int startPoint, int endPoint, Color color, int offSet)
        {
            //Trying to highlight charactars here
            TextPointer pointer = richTextBox.Document.ContentStart;
            TextPointer start = null, end = null;
            int count = 0;
            while (pointer != null)
            {
                if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    if (count == startPoint - offSet) start = pointer.GetInsertionPosition(LogicalDirection.Forward);
                    if (count == endPoint - offSet) end = pointer.GetInsertionPosition(LogicalDirection.Forward);
                    count++;
                }
                pointer = pointer.GetNextInsertionPosition(LogicalDirection.Forward);
            }
            if (start == null) start = richTextBox.Document.ContentEnd;
            if (end == null) end = richTextBox.Document.ContentEnd;

            TextRange range = new TextRange(start, end);
            range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(color));
        }
        private void DropSelection(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(Ids.Document.ContentStart, Ids.Document.ContentEnd);
            range.ApplyPropertyValue(TextElement.BackgroundProperty, new SolidColorBrush(Colors.White));
        }
    }
}
