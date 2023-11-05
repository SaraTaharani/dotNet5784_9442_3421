namespace DalTest;
//namespace Dal:
using DalApi;
using DO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

public static class Initialization
{
    public const int MIN_ID = 200000000;
    public const int MAX_ID = 400000000;
    private static IDependency? d_dalDependency;
    private static IEngineer? e_dalEngineer;
    private static ITask? t_dalTask;
    private static readonly Random s_rand = new();


    private static void createEngineer()
    {
        string[] engineerNames =
        {
        "Dani Levi",
        "Eli Amar",
        "Yair Cohen",
        "Ariela Levin",
        "Dina Klein",
        "Shira Israelof",
        "Sarale taharani",
        "Hodaya Shteinhouse",
        "Hadassa Bradpiece",
        "Noa Marciano"
        };


        foreach (var _name in engineerNames)
        {
            int _id;
            do
                _id = s_rand.Next(MIN_ID, MAX_ID);
            while (e_dalEngineer!.Read(_id) != null);
            string _email = _name + _id + "@gmail.com";
            Engineer newEng = new(_id, _name,_email, s_rand.Next(50, 400), , true);//enum איך ליבא את
            e_dalEngineer!.Create(newEng);
        }
    }
  
}
