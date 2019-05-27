using AssessmentControlLib;
using AssessmentModel;
using MVVM.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace AssessmentViewModel
{
    public class AssessmentToolViewModel : ObservableObject
    {
        private AssessmentContext db;

        public AssessmentToolViewModel(AssessmentContext db)
        {
            this.db = db;
            InitView(db);
        }

        public AssessmentToolViewModel()
        {

        }

        private void InitView(AssessmentContext db)
        {
            TaskControls = db.Tasks.Select(x => new TaskControl
            {
                Task = x
            })
                        .AsObservableCollection();
            People = db.Persons.AsObservableCollection();
            MaxPoints = db.Tasks.Sum(x => x.TaskPoints);
            MaxPointsPerPerson = $"/{maxPoints / db.Persons.Count()}";
            CurrentProjectPoints = db.Persons.Sum(x => x.Points);
        }

        private ObservableCollection<TaskControl> taskControls;
        public ObservableCollection<TaskControl> TaskControls
        {
            get { return taskControls; }
            set
            {
                taskControls = value;
                RaisePropertyChangedEvent(nameof(TaskControls));
            }
        }

        private TaskControl selectedTaskControl;
        public TaskControl SelectedTaskControl
        {
            get { return selectedTaskControl; }
            set
            {
                selectedTaskControl = value;
                if (selectedTaskControl == null) return;
                SelectedTaskName = selectedTaskControl.Task.Name;
                RaisePropertyChangedEvent(nameof(SelectedTaskControl));
            }
        }

        private string selectedTaskName;
        public string SelectedTaskName
        {
            get { return selectedTaskName; }
            set
            {
                selectedTaskName = value;
                RaisePropertyChangedEvent(nameof(SelectedTaskName));
            }
        }

        private ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                RaisePropertyChangedEvent(nameof(People));
            }
        }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                SelectedPersonName = selectedPerson.Name;
                SelectedPersonPoints = selectedPerson.Points;
                PersonTasks = db.PersonTasks.Where(x => x.Person.Name == selectedPerson.Name)
                .AsObservableCollection();
                RaisePropertyChangedEvent(nameof(SelectedPerson));
            }
        }

        private string selectedPersonName;
        public string SelectedPersonName
        {
            get { return selectedPersonName; }
            set
            {
                selectedPersonName = value;
                RaisePropertyChangedEvent(nameof(SelectedPersonName));
            }
        }

        private int selectedPersonPoints;
        public int SelectedPersonPoints
        {
            get { return selectedPersonPoints; }
            set
            {
                selectedPersonPoints = value;
                RaisePropertyChangedEvent(nameof(SelectedPersonPoints));
            }
        }

        private PersonTask selectedPersonTask;
        public PersonTask SelectedPersonTask
        {
            get { return selectedPersonTask; }
            set
            {
                selectedPersonTask = value;
                RaisePropertyChangedEvent(nameof(SelectedPersonTask));
            }
        }

        public ICommand DeleteTaskFromPerson => new RelayCommand<string>(
                DoDeleteTaskFromPerson
            );
        private void DoDeleteTaskFromPerson(string obj)
        {
            if (selectedPersonTask != null)
            {

                db.Tasks.SingleOrDefault(x => x.Name == selectedPersonTask.Task.Name)
                    .PointsToDistribute += selectedPersonTask.PointsInvested;
                db.Persons.SingleOrDefault(x => x.Name == selectedPersonTask.Person.Name)
                    .Points -= selectedPersonTask.PointsInvested;
                CurrentProjectPoints -= selectedPersonTask.PointsInvested;
                
                db.PersonTasks.Remove(selectedPersonTask);
                db.SaveChanges();

                PersonTasks = db.PersonTasks.Where(x => x.Person.Name == selectedPerson.Name)
                .AsObservableCollection();
                SelectedPersonPoints = selectedPerson.Points;
                
                TaskControls = db.Tasks.Select(x => new TaskControl
                {
                    Task = x
                })
                .AsObservableCollection();
                People = db.Persons.AsObservableCollection();
            }
        }

        public ICommand AddTaskToPerson => new RelayCommand<string>(
                DoAddTaskToPerson
            );
        private void DoAddTaskToPerson(string obj)
        {
            if (selectedTaskName != null &&
                selectedPersonName != null &&
                selectedTaskControl.Val > 0)
            {
                GivePoint();
                AdjustView();
                AdjustTaskInDb();
                People = db.Persons.AsObservableCollection();
            }
        }

        private void AdjustTaskInDb()
        {
            db.Tasks.SingleOrDefault(x => x.Name == selectedTaskControl.Task.Name)
                            .PointsToDistribute--;
            selectedTaskControl.Val--;
            CurrentProjectPoints++;
            db.SaveChanges();
        }

        private void AdjustView()
        {
            selectedPerson.Points = db.PersonTasks.Where(x => x.Person.Name == selectedPerson.Name)
                            .Sum(x => x.PointsInvested);
            SelectedPersonPoints = selectedPerson.Points;
            PersonTasks = db.PersonTasks.Where(x => x.Person.Name == selectedPerson.Name)
                .AsObservableCollection();
            db.SaveChanges();
        }

        private void GivePoint()
        {
            try
            {
                db.PersonTasks.SingleOrDefault(x => x.Person.Name == selectedPersonName && x.Task.Name == selectedTaskName)
                .PointsInvested++;
            }
            catch (NullReferenceException exc)
            {
                db.PersonTasks.Add(new PersonTask
                {
                    Person = selectedPerson,
                    Task = selectedTaskControl.Task,
                    PointsInvested = 1
                });
            }
            db.SaveChanges();
        }

        private ObservableCollection<PersonTask> personTasks;
        public ObservableCollection<PersonTask> PersonTasks
        {
            get { return personTasks; }
            set
            {
                personTasks = value;
                RaisePropertyChangedEvent(nameof(personTasks));
            }
        }

        private int maxPoints;
        public int MaxPoints
        {
            get { return maxPoints; }
            set
            {
                maxPoints = value;
                RaisePropertyChangedEvent(nameof(MaxPoints));
            }
        }

        private string maxPointsPerPerson;
        public string MaxPointsPerPerson
        {
            get { return maxPointsPerPerson; }
            set
            {
                maxPointsPerPerson = value;
                RaisePropertyChangedEvent(nameof(MaxPointsPerPerson));
            }
        }

        private int currentProjectPoints;
        public int CurrentProjectPoints
        {
            get { return currentProjectPoints; }
            set
            {
                currentProjectPoints = value;
                RaisePropertyChangedEvent(nameof(CurrentProjectPoints));
            }
        }
    }
}
