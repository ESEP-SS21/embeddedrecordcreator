using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using EmbeddedRecordCreator.Model;

namespace EmbeddedRecordCreator
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Record> Records { get; set; } = new();

        public MainWindowViewModel()
        {
            Records.Add(new Record() {Event = new Event() {Broad = false, Payl = 4, Evnt = EventType.EVNT_ACK}});
            Records.Add(new Record() {Event = new Event() {Broad = false, Payl = 43, Evnt = EventType.EVNT_WRN}});

            string s = JsonSerializer.Serialize(Records, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Converters =
                {
                    new JsonStringEnumConverter(),
                }
            });

            s = s.ToLower();
            int i = 1;
        }
    }
}