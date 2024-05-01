using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Scripts.Manager
{
    public class SkillDataManager
    {
        private static SkillDataManager instance;
        [JsonProperty]
        public Dictionary<int, SkillData> SkillDictionary { get; private set; }

        private SkillDataManager()
        {
            SkillDictionary = new Dictionary<int, SkillData>();
            // Json 파일 받아오기
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\SaveData\Skills.json");
            string jsonText = File.ReadAllText(jsonFilePath);

            Console.WriteLine();

            SkillDictionary = JsonConvert.DeserializeObject<Dictionary<int, SkillData>>(jsonText);
        }

        public static SkillDataManager GetInstance()
        {
            if(instance == null) instance = new SkillDataManager();

            return instance;
        }
    }
}
