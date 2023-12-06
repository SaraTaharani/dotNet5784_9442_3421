using Dal;

namespace DalTest;

//namespace Dal:
using DalApi;
using DO;
using System.Data.Common;
using System.Xml.Linq;

public static class Initialization
{
    public const int MIN_ID = 200000000;
    public const int MAX_ID = 400000000;
    private static IDal? s_dal;
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
        "Noa Marciano",
        "Yossi Cohen",
        "Shifra Weiss",
        "Dina Bradpiece",
        "Tamar Levi"
        };

        foreach (var _name in engineerNames)
        {
            int _id;
            
                _id = s_rand.Next(MIN_ID, MAX_ID + 1);
         //   while (s_dalEngineer!.Read(_id) != null);
            string _email = _name + _id + "@gmail.com";
            int _cost = 0;
            EngineerExperience _level = (EngineerExperience)s_rand.Next(0, Enum.GetValues<EngineerExperience>().Count());
            switch (_level)
            {
                case EngineerExperience.expert: _cost = 400;
                    break;
                case EngineerExperience.Jr:_cost = 300;
                    break;
                case EngineerExperience.rookie:_cost = 200;
                    break;
                default:
                    break;
            }

            s_dal!.Engineer!.Create(new Engineer(_id, _name, _email, _cost, _level, true));
        }
    }

    private static void createTask()
    {
        string[] taskDescriptions =
        {
            "building permits",//היתרי בניה
            "organization",//התארגנות
            "measurement",//מדידה
            "Fencing the site",//גידור האתר
            "digging",//חפירה
            "grounding",//ביסוס
            "plumbing",//אינסטלציה
            "electrical power",//חשמל
            "construction",//בניה
            "sealing",//איטום
            "frames",//מסגרות
            "Elevators",//מעליות                    
            "plaster",//גבס
            "color"//צבע
        };
        //
        EngineerExperience _complexityLevel = (EngineerExperience)(s_rand.Next(0, 3));
        for (int i = 0; i < 10; i++)
        {
            Task newTask = new(i, taskDescriptions[i], null, false, null, null, null, null, null, "", "",null, _complexityLevel, true);
            s_dal!.Task!.Create(newTask);
        }
    }

    private static void createDependency()
    {
        for (int i = 0; i < 10; i++)
        {
            Dependency newDependency = new(0, i + 1, i);
            s_dal!.Dependency!.Create(newDependency);
        }
    }

    public static void Do(IDal dal)
    {
        s_dal= dal ?? throw new NullReferenceException("DAL can not be null!");
        createEngineer();
        createTask();
        createDependency();
    }
}