using AssessmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssessmentControlLib
{
    public partial class TaskControl : UserControl
    {
        public Task Task { get; set; }
        public int Val
        {
            get { return (int)pbPoints.Value; }
            set { pbPoints.Value = value; }
        }

        public TaskControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            lblName.Content = Task.ToString();
            pbPoints.Maximum = Task.TaskPoints;
            pbPoints.Value = Task.PointsToDistribute;
        }
    }
}
