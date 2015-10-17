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
using System.Runtime.InteropServices; 


namespace VHD_HELPER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MyDataGridColumns> data = new List<MyDataGridColumns>();
        BrushConverter bc = new BrushConverter();

        public MainWindow(string file="")
        {
            InitializeComponent();
            status_text.Text = "Ready";    
       
            if (file != "")
            {
                bool attached = mount.OpenAndAttachVHD(file.ToString());
                if (attached)
                {
                    statusbar.Background = (Brush)bc.ConvertFrom("#2E8DEF");
                    status_text.Text = "Selected File: " + file.ToString();
                    data.Add(new MyDataGridColumns()
                    {
                        Filename = file.ToString(),
                        Disksignature = "1234"
                    });
                    VHDDataGrid.ItemsSource = data;
                    VHDDataGrid.Items.Refresh();
                }

                else
                {
                    statusbar.Background = (Brush)bc.ConvertFrom("#DC572E");
                    status_text.Text = "Attaching VHD Failed " +file.ToString();
                }                    
            }

            VHDDataGrid.ItemsSource = data;
        }
        
        private void AttachButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();
            //var folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                    //var obj = new MyDataGridColumns();                    
                    var file = fileDialog.FileName;
                    
                    //filename_lbl.Content = "Selected File:" +file.ToString();
                    bool attached=mount.OpenAndAttachVHD(file.ToString());
                    if (attached)
                    {                       
                        statusbar.Background = (Brush)bc.ConvertFrom("#2E8DEF"); 
                        status_text.Text = "Selected File: " + file.ToString();                        
                        data.Add(new MyDataGridColumns()
                        {
                            Filename = file.ToString(),
                            Disksignature = "1234"
                        });
                        VHDDataGrid.ItemsSource = data;
                        VHDDataGrid.Items.Refresh();
                    }

                    else
                    {
                        statusbar.Background = (Brush)bc.ConvertFrom("#DC572E"); 
                        status_text.Text = "Attaching VHD Failed ";
                    }                    
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;
            }
        }
        private void DetachButton_Click(object sender, RoutedEventArgs e)
        {
            int items_count = VHDDataGrid.Items.Count;
            for (int i = 0; i <items_count ; i++ )
            {
                CheckBox chkbx = VHDDataGrid.Columns[0].GetCellContent(VHDDataGrid.Items[i]) as CheckBox;
             
                if (chkbx.IsChecked == true)
                {
                    TextBlock vhdname_column = VHDDataGrid.Columns[1].GetCellContent(VHDDataGrid.Items[i]) as TextBlock;
                    string vhd = vhdname_column.Text;
                    bool detached = mount.OpenAndDetachVHD(vhd);
                    if (detached)
                    {
                        BrushConverter bc = new BrushConverter();
                        statusbar.Background = (Brush)bc.ConvertFrom("#2E8DEF");
                        status_text.Text = "Detached File: " + vhd;
                        data.RemoveAt(i);                        
                    }
                    else
                    {
                        statusbar.Background = (Brush)bc.ConvertFrom("#DC572E");
                        status_text.Text = "Detaching VHD Failed " + vhd;
                    }                    
                }
            }
            // Update Data Grid Table
            VHDDataGrid.ItemsSource = data;
            VHDDataGrid.Items.Refresh();
        }
    }

    public class MyDataGridColumns
    {
       public string Filename { get; set; }
       public string Disksignature { get; set; }
    }
}
