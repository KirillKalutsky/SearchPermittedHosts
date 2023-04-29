using test;

var result = Solution.GetSolution(new[]
{
    "unlock.microvirus.md",
    "visitwar.com",
    "visitwar.de",
    "fruonline.co.uk",
    "australia.open.com",
    "credit.card.us"
}, new[] 
{
    //"com",
    "microvirus.md",
    "visitwar.de",
    "piratebay.co.uk",
    "list.stolen.credit.card.us"
});

Console.WriteLine(string.Join(", ", result));
