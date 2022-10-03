using System.Diagnostics;

Random rnd = new();

//filmek címeinek betöltése egy vektorba
string[] filmek = new string[100];
using StreamReader sr = new(@"..\..\..\src\filmek.txt");
int index = 0;
while (!sr.EndOfStream)
{
    filmek[index] = sr.ReadLine();
    index++;
}

#region teszt 01
// foreach (var film in filmek) Console.WriteLine(film);
#endregion

//feladvány kiválasztása (a filmek vektor egy 'véletlenedik' indexű eleme)
string feladvany = filmek[rnd.Next(filmek.Length)];

#region teszt 02
//Console.WriteLine($"feladvány: {feladvany}");
#endregion

string megoldas = null;
List<char> tippek = new();
int hibalehetosegek = 10;
Stopwatch sw = new();


while (hibalehetosegek > 0 && feladvany != megoldas && sw.Elapsed.TotalSeconds < 90)
{
    Console.Clear();
    megoldas = "";
    //feladvány megjelenítése:
    for (int i = 0; i < feladvany.Length; i++)
    {
        if (feladvany[i] == ' ')
            megoldas += ' ';
        else if (tippek.Contains(feladvany[i]) || tippek.Contains(char.Parse(feladvany[i].ToString().ToLower())))
            megoldas += feladvany[i];
        else
            megoldas += '*';
    }

    Console.WriteLine($"{megoldas}\t\thibalehetőségek száma: {hibalehetosegek}");
    Console.Write("Eddigi tippek: ");
    foreach (var b in tippek)
    {
        if (feladvany.Contains(b) || feladvany.Contains(b.ToString().ToUpper()))
            Console.ForegroundColor = ConsoleColor.Green;
        else Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"{b} ");
    }
    Console.Write('\n');
    Console.ResetColor();

    if (feladvany != megoldas)
    {
        Console.Write("Új tipp: ");
        string ujTippString = Console.ReadLine().ToLower();
        if (ujTippString.Length == 1)
        {
            char ujTipp = char.Parse(ujTippString);

            if (!sw.IsRunning) sw.Start();
            if (!tippek.Contains(ujTipp))
            {
                tippek.Add(ujTipp);
                if (!feladvany.ToLower().Contains(ujTipp))
                {
                    hibalehetosegek--;
                }
            }
        }
        else if (ujTippString.ToLower() == feladvany.ToLower())
        {
            megoldas = feladvany;
        }
    }  
}
sw.Stop();

if (sw.Elapsed.TotalSeconds >= 90)
{
    Console.WriteLine("lejárt az idő :(");
}
else if (hibalehetosegek > 0)
{
    
    Console.WriteLine($"A megoldás: {feladvany}, nyertél!");
    Console.WriteLine($"A megoldással {sw.Elapsed.TotalSeconds:00.00} másodpercet pazaroltál el az életedből");
}
else
{
    Console.WriteLine("fel lettél akasztva, te csíra!");
    Console.WriteLine("vesztettél!");
    Console.WriteLine($"a megoldás '{feladvany}' lett volna");
}
