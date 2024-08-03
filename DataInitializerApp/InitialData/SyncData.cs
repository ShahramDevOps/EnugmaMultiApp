using DataInitializerApp.ViewModels;

namespace DataInitializerApp.InitialData
{
    public static class SyncData
    {
        private static readonly object _lock = new();
        private static List<CorrectListViewModel> CorrectPassPhrase = new();

        public static void InitializeCorrect(List<string> values, bool[] result)
        {
            List<CorrectListViewModel> itemsToAdd = new();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i])
                    if (!CorrectPassPhrase.Any(current => current.RowIndex == i && current.Keyword == values[i]))
                        itemsToAdd.Add(new CorrectListViewModel { RowIndex = i, Keyword = values[i] });
            }

            lock (_lock)
            {
                CorrectPassPhrase.AddRange(itemsToAdd);
            }
        }

        public static List<CorrectListViewModel> GetCorrectPassPhrase()
        {
            lock (_lock)
            {
                return new List<CorrectListViewModel>(CorrectPassPhrase);
            }
        }

        public static List<CorrectListViewModel> ResetList() => CorrectPassPhrase = [];
    }
}