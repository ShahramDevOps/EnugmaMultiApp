namespace GetEnigmaCorrect.ApiService
{
    public static class OperationServices
    {
        public static List<List<string>> GenerateRotatedLists(List<string> keys)
        {
            int count = keys.Count;
            var resultLists = new List<List<string>>();

            for (int i = 0; i < count; i++)
            {
                var newList = new List<string>();

                for (int j = 0; j < count; j++)
                    newList.Add(keys[(j + i) % count]);
                resultLists.Add(newList);
            }

            return resultLists;
        }

        public static List<List<List<string>>> DivideIntoGroups(List<List<string>> lists, int numberOfGroups)
        {
            int groupSize = lists.Count / numberOfGroups;
            var groupedLists = new List<List<List<string>>>();

            for (int i = 0; i < numberOfGroups; i++)
            {
                var group = new List<List<string>>();
                for (int j = 0; j < groupSize; j++)
                {
                    group.Add(lists[i * groupSize + j]);
                }
                groupedLists.Add(group);
            }

            return groupedLists;
        }
    }
}
