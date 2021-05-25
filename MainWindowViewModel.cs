using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Input;
using EmbeddedRecordCreator.Model;
using Microsoft.Win32;

namespace EmbeddedRecordCreator
{
    public class MainWindowViewModel
    {
        private ICommand? _openFileCommand;
        public ICommand OpenFileCommand => _openFileCommand ??= new RelayCommand(_ => OpenFile());
        private ICommand? _saveFileCommand;
        public ICommand SaveFileCommand => _saveFileCommand ??= new RelayCommand(_ => SaveFile());

        private const string FileFilter = "Json files (*.json)|*.json";
        public ObservableCollection<Record> Records { get; set; } = new();

        public MainWindowViewModel()
        {
            Records.Add(new Record() {Event = new Event() {Broad = false, Payl = 4, Evnt = EventType.EVNT_ACK}});
            Records.Add(new Record() {Event = new Event() {Broad = false, Payl = 43, Evnt = EventType.EVNT_WRN}});
        }

        private string Serialize()
        {
            return JsonSerializer.Serialize(Records, new JsonSerializerOptions()
                    {WriteIndented = true, Converters = {new JsonStringEnumConverter()}})
                .ToLower();
        }

        private void Deserialize(string json)
        {
            //TODO make this work
            var records = JsonSerializer.Deserialize<IEnumerable<Record>>(json);
            if (records is null)
                return;

            foreach (Record record in records)
            {
                Records.Add(record);
            }
        }


        private void OpenFile()
        {
            var ofd = new OpenFileDialog() {Filter = FileFilter};
            if (ofd.ShowDialog() == true)
            {
                string fileName = ofd.FileName;
                string json = File.ReadAllText(fileName);
                Deserialize(json);
            }
        }

        private void SaveFile()
        {
            var sfd = new SaveFileDialog() {Filter = FileFilter};
            if (sfd.ShowDialog() == true)
            {
                string fileName = sfd.FileName;
                File.WriteAllText(fileName, Serialize());
            }
        }
    }
}