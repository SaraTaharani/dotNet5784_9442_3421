namespace DalTest;
//namespace Dal:
using DalApi;
using DO;
public static class Initialization
{
    public const int MIN_ID = 200000000;
    public const int MAX_ID = 400000000;
    private static IDependency? s_dalDependency;
    private static IEngineer? s_dalEngineer;
    private static ITask? s_dalTask;
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
            do
                _id = s_rand.Next(MIN_ID, MAX_ID + 1);
            while (s_dalEngineer!.Read(_id) != null);
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
            Engineer newEng = new(_id, _name, _email, _cost, _level, true);
            s_dalEngineer!.Create(newEng);
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
        List<Engineer> EngineersList = s_dalEngineer!.ReadAll();
        for (int i = 0; i < 10; i++)
        {
            Task newTask = new(0, taskDescriptions[i], null, false, null, null, null, null, null, "", "", EngineersList[i].Id, EngineersList[i].Level, true);
            s_dalTask!.Create(newTask);
        }
    }

    private static void createDependency()
    {
        for (int i = 0; i < 10; i++)
        {
            Dependency newDependency = new(0, i + 1, i);
            s_dalDependency!.Create(newDependency);
        }
    }

    public static void DO(IEngineer? s_dalEngineer, ITask? s_dalTask, IDependency? s_dalDependency)
    {
        s_dalEngineer = s_dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = s_dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = s_dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        createEngineer();
        createTask();
        createDependency();
    }
}