namespace OrangeSummer.Model
{
    /// <summary>
    /// 전윤기 - 2020.06.18
    /// 회원관리 Model
    /// </summary>
    public class Member
    {
        public int Total { get; set; }
        public string Id { get; set; }
        public int Sort { get; set; }
        public string FkBranch { get; set; }
        public string FkTravel { get; set; }
        public string Code { get; set; }
        public string Pwd { get; set; }
        public string Reset { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string AdvertYn { get; set; }
        public string DelYn { get; set; }
        public string RegistDate { get; set; }
        public string ProfileImg { get; set; }
        public string BackgroundImg { get; set; }


        public Branch Branch { get; set; }
        public Travel Travel { get; set; }
    }
}
