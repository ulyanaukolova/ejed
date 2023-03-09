using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using WpfApp_3;

namespace EveryDayNick
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int subIndex;
        string name;
        string description;
        DateTime? dateTime = DateTime.Now;
        static List<Note> notes = new List<Note>();
        static List<Note> notesOnDays = new List<Note>();

        public MainWindow()
        {
            InitializeComponent();
            notes = Note.Deserialization<Note>();
            Calendar.SelectedDate = DateTime.Today;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DateTime? date = Calendar.SelectedDate;
            int a = 0;
            foreach (var delNote in notes)
            {
                if (delNote.date == notesOnDays[subIndex].date & delNote.description == notesOnDays[subIndex].description & delNote.name == notesOnDays[subIndex].name)
                {
                    a = notes.IndexOf(delNote); break;
                }
            }
            notes.RemoveAt(a);
            notesOnDays.RemoveAt(subIndex);
            notesDataView.Items.RemoveAt(subIndex);
            notes.Add(new Note(Name_note.Text, Description.Text, date));
            notesOnDays.Add(new Note(Name_note.Text, Description.Text, date));
            notesDataView.Items.Add(Name_note.Text);
            Note.Serialize(notes);
            notes = Note.Deserialization<Note>();
            Empty();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            foreach (var delNote in notes)
            {
                if (delNote.date == notesOnDays[subIndex].date & delNote.description == notesOnDays[subIndex].description & delNote.name == notesOnDays[subIndex].name)
                {
                    a = notes.IndexOf(delNote); break;
                }
            }
            notes.RemoveAt(a);
            notesOnDays.RemoveAt(subIndex);
            notesDataView.Items.RemoveAt(subIndex);
            Note.Serialize(notes);
            notes = Note.Deserialization<Note>();
            subIndex = 0;
            Empty();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            name = Name_note.Text;
            description = Description.Text;
            dateTime = Calendar.SelectedDate;
            Note note = new Note(name, description, dateTime);
            notes.Add(note);
            Note.Serialize(notes);
            Note.Deserialization<Note>();
            notesDataView.Items.Add(note.name);
            notesOnDays.Add(note);
            Empty();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Calendar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Save.IsEnabled = false;
            Delete.IsEnabled = false;
            subIndex = 0;
            notesDataView.Items.Clear();
            notesOnDays.Clear();
            Empty();
            foreach (var note in notes)
            {
                if (note.date == Calendar.SelectedDate)
                {
                    notesOnDays.Add(note);
                }
            }
            CheckDate();
            foreach (var note in notesOnDays)
            {
                notesDataView.Items.Add(note.name);
            }
        }
        private void CheckDate()
        {
            if (Calendar.SelectedDate != null)
            {
                isEnabled(true);
            }
            else
            {
                isEnabled(false);
            }
        }
        private void isEnabled(bool a)
        {
            Name_note.IsEnabled = a;
            Description.IsEnabled = a;
        }


        private void Empty()
        {
            Name_note.Text = string.Empty;
            Description.Text = string.Empty;
        }

        private void notesDataView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isEnabled(true);
            subIndex = notesDataView.Items.IndexOf(notesDataView.SelectedItem);
            if (subIndex != -1)
            {
                Save.IsEnabled = true;
                Delete.IsEnabled = true;
                Name_note.Text = notesOnDays[subIndex].name;
                Description.Text = notesOnDays[subIndex].description;
            }
            else if (subIndex == -1 || notesDataView.SelectedItem == null)
            {
                Save.IsEnabled = false;
                Delete.IsEnabled = false;
            }
        }
    }
}