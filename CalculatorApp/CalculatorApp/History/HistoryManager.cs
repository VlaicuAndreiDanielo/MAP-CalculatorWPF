using System.Collections.ObjectModel;

namespace CalculatorApp
{
    public static class HistoryManager
    {

        public static ObservableCollection<string> HistoryItems { get; } = new ObservableCollection<string>();

        public static void AddHistory(string entry)
        {
            HistoryItems.Add(entry);
        }

        public static void ClearHistory()
        {
            HistoryItems.Clear();
        }
    }
}
