using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using EmbeddedRecordCreator.Model;
using Microsoft.Win32;

namespace EmbeddedRecordCreator
{
    public class MainWindowViewModel
    {
        private const string FileFilter = "Json files (*.json)|*.json";

        private readonly JsonSerializerOptions _jsonSerializerOptions = new() {WriteIndented = true};
        private ICommand? _exportCommand;
        private ICommand? _importCommand;


        public MainWindowViewModel()
        {
            Records.Add(new Record {Evnt = new Evnt {Broad = false, Payl = 4, Type = EventType.EVNT_ACK}});
            Records.Add(new Record {Evnt = new Evnt {Broad = false, Payl = 43, Type = EventType.EVNT_WRN}});
        }

        public ICommand ImportCommand => _importCommand ??= new RelayCommand(_ => ImportRecordsFromFile());
        public ICommand ExportCommand => _exportCommand ??= new RelayCommand(_ => ExportRecordsToFile());

        public IEnumerable<EventType> EventTypes => Enum.GetValues(typeof(EventType)).Cast<EventType>();

        public bool ImportOverwrite { get; set; } = true;
        public ObservableCollection<Record> Records { get; set; } = new();

        private string Serialize()
        {
            return JsonSerializer.Serialize(Records, _jsonSerializerOptions)
                .ToLower();
        }

        private IEnumerable<Record> Deserialize(string json)
        {
            return JsonSerializer.Deserialize<IEnumerable<Record>>(json, _jsonSerializerOptions) ?? Enumerable.Empty<Record>();
        }


        private void ImportRecordsFromFile()
        {
            var ofd = new OpenFileDialog {Filter = FileFilter};
            if (ofd.ShowDialog() == false)
                return;

            if (!File.Exists(ofd.FileName))
            {
                MessageBox.Show("File does not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            IEnumerable<Record> records;
            try
            {
                string json = File.ReadAllText(ofd.FileName);
                records = Deserialize(json);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ImportOverwrite)
                Records.Clear();

            foreach (Record record in records)
                Records.Add(record);
        }

        private void ExportRecordsToFile()
        {
            var sfd = new SaveFileDialog {Filter = FileFilter};
            if (sfd.ShowDialog() == true)
                File.WriteAllText(sfd.FileName, Serialize());
        }
    }
}