using GetEnigmaCorrect.ApiService;
using GetEnigmaCorrect.ViewModel;
using System.Diagnostics;

Stopwatch stopwatch = new();

Console.WriteLine("application Start...");
Console.WriteLine("Application Number [0,1,2,3] : ");
int ApplicationNumber = Convert.ToInt32(Console.ReadLine());
Console.Clear();

var oldEnigma = string.Empty;
Console.Clear();


Console.WriteLine("Add Token");
var token = Console.ReadLine();
Console.Clear();

var enigmaCheck = await Services.CheckStartAsync(token);
if (enigmaCheck.EnigmaId == null)
{
    Console.WriteLine("Token Is Expire....");
    Console.ReadLine();
}
Console.WriteLine("Please Enter To Start");
Console.ReadLine();
Console.Clear();

EnigmaViewModel enigmaViewModel = await Services.CheckStartAsync(token);

stopwatch.Start();

List<string> stringList = await Services.GetCorrectDataRequestAsync();

await Services.SendCorrectRequestAsync(token, stringList, enigmaViewModel);

Console.ReadLine();