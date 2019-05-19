using AssessmentModel;
using AssessmentViewModel;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Assessment_Tool
{
    public partial class MainWindow : Window
    {
        private AssessmentContext db = new AssessmentContext();
        private static readonly string INIT_DATA_FILENAME = "assessment.csv";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HandleDbActions();
            DataContext = new AssessmentToolViewModel(db);
        }

        private void HandleDbActions()
        {
            SetDbConnectionString();
            TryDbConnection();
            FillDbIfEmpty();
        }
        
        private void SetDbConnectionString()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine($"*****Path: {path}");
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private void TryDbConnection()
        {
            try
            {
                int nr = db.Persons.Count();
                Console.WriteLine($"*****No. Persons: {nr}");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void FillDbIfEmpty()
        {
            if (db.Persons.Count() == 0 && db.Tasks.Count() == 0)
            {
                var reader = new StreamReader(INIT_DATA_FILENAME);
                InsertPeople(reader);
                while (!reader.EndOfStream) InsertTasks(reader);
                db.SaveChanges();
            }
        }
        
        /*
         * 2 is the specific column number for this kind of CSV where the list of names starts.
         * The last column 'Gesamt' is cut off by using 'people.Length-1'.
         */
        private void InsertPeople(StreamReader reader)
        {
            var personLine = reader.ReadLine();
            var people = personLine.Split(';');
            for (int i = 2; i < people.Length-1; i++)
            {
                db.Persons.Add(new Person
                {
                    Name = people[i]
                });
            }
        }

        private void InsertTasks(StreamReader reader)
        {
            var taskLine = reader.ReadLine();
            var task = taskLine.Split(';');
            db.Tasks.Add(new Task
            {
                Name = task[0],
                PointsToDistribute = int.Parse(task[1]),
                TaskPoints = int.Parse(task[1])
            });
        }
    }
}
