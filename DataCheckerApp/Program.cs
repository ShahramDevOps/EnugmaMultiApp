using DataCheckerApp.ApiService;
using DataCheckerApp.ViewModel;
using System.Diagnostics;

Stopwatch stopwatch = new();

Console.WriteLine("application Start...");
Console.WriteLine("Application Number [0,1,2,3] : ");
int ApplicationNumber = Convert.ToInt32(Console.ReadLine());
var oldEnigma = string.Empty;

List<string> stringList = new();
bool condition = false;

Console.WriteLine("Add authentication Token :");
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

//List<string> data = ["erherh", "heh", "erherh", "rhrehe", "drherrt5h", "therh", "ergegh3", "yjdf", "erge5", "rthrthk", "rthrthrh", "dvfwf"];
//bool[] values = [true, true, true, true, true, true, true, true, true, true, true, true,];
//await Services.InitializeDataRequestAsync(data, values);

while (!condition)
{
    enigmaViewModel = await Services.CheckStartAsync(token1);
    if (enigmaViewModel.EnigmaId != oldEnigma)
        break;

    Thread.Sleep(1000);
}

List<List<string>> resultLists = OperationServices.GenerateRotatedLists(enigmaViewModel.PassPhrase);
List<List<List<string>>> groupedLists = OperationServices.DivideIntoGroups(resultLists, 4);
var tasks = new List<Task>
{
    Services.DelayedSendRequestAsync(token1, groupedLists[ApplicationNumber][0], enigmaViewModel, 0),
    Services.DelayedSendRequestAsync(token1, groupedLists[ApplicationNumber][1], enigmaViewModel, 10000),
    Services.DelayedSendRequestAsync(token1, groupedLists[ApplicationNumber][2], enigmaViewModel, 20000)
};
await Task.WhenAll(tasks);