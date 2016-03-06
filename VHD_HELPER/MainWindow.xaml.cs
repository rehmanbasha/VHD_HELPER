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
using System.Collections.ObjectModel;


namespace VHD_HELPER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<MyDataGridColumns> data = new ObservableCollection<MyDataGridColumns>();        
       
        public MainWindow(string file="")
        {
            InitializeComponent();
            VHDDataGrid.ItemsSource = data;

            #region Right Click
            StatusUpdate("Ready");           

            if (file != "")
            {
                bool attached = mount.OpenAndAttachVHD(file.ToString());
                if (attached)
                {                 
                    StatusUpdate("Selected File: " + file.ToString());
                    data.Add(new MyDataGridColumns()
                    {
                        Filename = file.ToString(),
                        Disksignature = "1234"
                    });                    
                }
                else
                {                    
                    StatusUpdate("Attaching VHD Failed " + file.ToString(),status.failure);
                }
            }

            #endregion            
        }

        #region Attaching

        private void AttachButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new System.Windows.Forms.OpenFileDialog();        
            var result = fileDialog.ShowDialog();
            switch (result)
            {
                case System.Windows.Forms.DialogResult.OK:
                                      
                    var file = fileDialog.FileName;                                       
                    bool attached=mount.OpenAndAttachVHD(file.ToString());
                    if (attached)
                    {
                        vhdlib vhd_info = new vhdlib();                        
                        StatusUpdate("Selected File: " + file.ToString());
                        data.Add(new MyDataGridColumns()
                        {
                            Filename = file.ToString(),
                            Disksignature = "1234",
                            DriveLetter= vhdlib.GetMountPoints(file.ToString())
                        });                        
                    }

                    else
                    {                        
                        StatusUpdate("Attaching VHD Failed ", status.failure);
                    }
                    
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                default:
                    break;

            }
        }

        #endregion 

        #region Detaching
        private void DetachButton_Click(object sender, RoutedEventArgs e)
        {
            int items_count = VHDDataGrid.Items.Count;
            int row = 0;
            for (int i = 0; i <items_count ; i++ )
            {         
                CheckBox chkbx = VHDDataGrid.Columns[0].GetCellContent(VHDDataGrid.Items[row]) as CheckBox;

                if (chkbx.IsChecked == true)
                {
                    TextBlock vhdname_column = VHDDataGrid.Columns[1].GetCellContent(VHDDataGrid.Items[row]) as TextBlock;
                    string vhd = vhdname_column.Text;
                    bool detached = mount.OpenAndDetachVHD(vhd);
                    if (detached)
                    {                        
                        StatusUpdate("Detached File: " + vhd);
                        data.RemoveAt(row);
                    }
                    else
                    {                        
                        StatusUpdate("Detaching VHD Failed " + vhd, status.failure);
                    }
                }
                else
                {
                    // Move to next row if this vhd's row is not selected
                    row++;
                }
            }           
        }
        #endregion


        #region Status Update

        private void StatusUpdate(string message,status sc=status.progress)
        {
            BrushConverter bc = new BrushConverter();

            switch(sc)
            {
                case status.progress:
                    statusbar.Background = (Brush)bc.ConvertFrom("#2E8DEF");
                    break;;
                case status.success:
                    statusbar.Background = (Brush)bc.ConvertFrom("#2E8DEF");
                    break;;
                case status.failure:
                    statusbar.Background = (Brush)bc.ConvertFrom("#DC572E");
                    break;;
                case status.warning:
                    statusbar.Background = (Brush)bc.ConvertFrom("#DC572E");
                    break; ;    
            }
            
            status_text.Text = message;     
        }
        
    }

    public enum status
    {
        success,
        failure,
        warning,
        progress
    }
        #endregion

    #region DataGridColumn Details

    public class MyDataGridColumns
    {
       public string Filename { get; set; }
       public string Disksignature { get; set; }
       public string DriveLetter { get; set; }
    }

    #endregion
}
