namespace OrangeSummer.Model
{
    /// <summary>
    /// 전윤기 - 2020.06.19
    /// 배너관리 Model
    /// </summary>
    public class Banner
    {
        public int Total { get; set; }
        public string Id { get; set; }
        public int Sort { get; set; }
        public string FkAdmin { get; set; }
        public string Type { get; set; }
        public int Section { get; set; }
        public string Title { get; set; }
        public string AttPc { get; set; }
        public string AttMobile { get; set; }
        public string Link { get; set; }
        public string Sdate { get; set; }
        public string Edate { get; set; }
        public string UseYn { get; set; }
        public string DelYn { get; set; }
        public string RegistDate { get; set; }
        public Admin Admin { get; set; }
    }
}
