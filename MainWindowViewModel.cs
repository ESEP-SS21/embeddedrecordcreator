﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using EmbeddedRecordCreator.Model;
using Microsoft.Win32;

namespace EmbeddedRecordCreator
{
    public class MainWindowViewModel
    {
        private ICommand? _importCommand;
        public ICommand ImportCommand => _importCommand ??= new RelayCommand(_ => ImportRecordsFromFile());
        private ICommand? _exportCommand;
        public ICommand ExportCommand => _exportCommand ??= new RelayCommand(_ => ExportRecordsToFile());

        public bool ImportOverwrite { get; set; } = true;
        public ObservableCollection<Record> Records { get; set; } = new();

        private const string FileFilter = "Json files (*.json)|*.json";

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
            {WriteIndented = true, Converters = {new JsonStringEnumConverter()}, PropertyNameCaseInsensitive = true};


        public MainWindowViewModel()
        {
            Records.Add(new Record() {Event = new Event() {Broad = false, Payl = 4, Evnt = EventType.EVNT_ACK}});
            Records.Add(new Record() {Event = new Event() {Broad = false, Payl = 43, Evnt = EventType.EVNT_WRN}});
        }

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
            var ofd = new OpenFileDialog() {Filter = FileFilter};
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
            {
                Records.Add(record);
            }
        }

        private void ExportRecordsToFile()
        {
            var sfd = new SaveFileDialog() {Filter = FileFilter};
            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, Serialize());
            }
        }
    }
}