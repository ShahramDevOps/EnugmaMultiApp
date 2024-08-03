using DataCheckerApp.ApiService;
using DataCheckerApp.ViewModel;
using System.Diagnostics;

Stopwatch stopwatch = new();

Console.WriteLine("application Start...");
Console.WriteLine("Application Number [0,1,2,3] : ");
int ApplicationNumber = Convert.ToInt32(Console.ReadLine());
Console.Clear();

var oldEnigma = string.Empty;
Console.Clear();

List<string> stringList = new();
bool condition = false;

Console.WriteLine("Add Token");
var token1 = Console.ReadLine();
Console.Clear();

var enigmaCheck = await Services.CheckStartAsync(token1);
if (enigmaCheck.EnigmaId != null)
    oldEnigma = enigmaCheck.EnigmaId;
else
    Console.WriteLine("Token Is Expire....");

EnigmaViewModel enigmaViewModel = new();
Console.WriteLine("Please Enter To Start");
Console.ReadLine();
Console.Clear();

while (!condition)
{
    enigmaViewModel = await Services.CheckStartAsync(token1);
    if (enigmaViewModel.EnigmaId != oldEnigma)
        break;

    Thread.Sleep(1000);
}

stopwatch.Start();

List<List<string>> resultLists = OperationServices.GenerateRotatedLists(enigmaViewModel.PassPhrase);
List<List<List<string>>> groupedLists = OperationServices.DivideIntoGroups(resultLists, 4);

var tasks = new List<Task>
{
    Services.DelayedSendRequestAsync(token1, groupedLists[ApplicationNumber][0], enigmaViewModel, 0),
    Services.DelayedSendRequestAsync(token1, groupedLists[ApplicationNumber][1], enigmaViewModel, 10000),
    Services.DelayedSendRequestAsync(token1, groupedLists[ApplicationNumber][2], enigmaViewModel, 20000)
};

await Task.WhenAll(tasks);