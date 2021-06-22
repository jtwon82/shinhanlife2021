
namespace OrangeSummer.Model
{
    /// <summary>
    /// 전윤기 - 2020.06.18
    /// 지점관리 Model
    /// </summary>
    public class Branch
    {
        public int Total { get; set; }
        public string Id { get; set; }
        public int Sort { get; set; }
        public string FkAdmin { get; set; }
        public string FkTravel { get; set; }
        public string Name { get; set; }
        public string DelYn { get; set; }
        public string RegistDate { get; set; }

        public Admin Admin { get; set; }
        public Travel Travel { get; set; }
    }
}
