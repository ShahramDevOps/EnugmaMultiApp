using GetEnigmaCorrect.ApiService;
using GetEnigmaCorrect.ViewModel;

bool condition = false;

Console.WriteLine("Last Application Find Wallet...");
var oldEnigma = string.Empty;

Console.WriteLine("Add authentication Token :");
var token = Console.ReadLine();
Console.Clear();

var enigmaCheck = await Services.CheckStartAsync(token);
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
    enigmaViewModel = await Services.CheckStartAsync(token);
    if (enigmaViewModel.EnigmaId != oldEnigma)
        break;

    Thread.Sleep(1000);
}

enigmaViewModel = await Services.CheckStartAsync(token);

List<string> stringList = await Services.GetCorrectDataRequestAsync();
await Services.SendCorrectRequestAsync(token, stringList, enigmaViewModel);
Console.ReadLine();