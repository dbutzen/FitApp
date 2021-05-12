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
using System.Windows.Shapes;
using TCT.FitApp.BL;
using TCT.FitApp.BL.Models;

namespace TCT.FitApp.WPF
{
    /// <summary>
    /// Interaction logic for MaintainItemTypes.xaml
    /// </summary>
    public partial class MaintainItemTypes : Window
    {
        ItemType itemType = new ItemType();
        bool isNew = false;

        //Add activity
        public MaintainItemTypes()
        {
            InitializeComponent();
            txtItemType.Text = String.Empty;
            isNew = true;
        }

        //Edit activity
        public MaintainItemTypes(ItemType itemType)
        {
            this.itemType = itemType;
            InitializeComponent();
            txtItemType.Text = txtItemType.Name;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            itemType.Name = txtItemType.Text;

            if (isNew == false)
            {
                await ItemTypeManager.Update(itemType);
                MessageBox.Show("Item Type has been updated");
                this.Close();
            }
            else
            {
                await ItemTypeManager.Insert(itemType);
                MessageBox.Show("Item Type has been added");
                this.Close();
            }
        }
    }
}
