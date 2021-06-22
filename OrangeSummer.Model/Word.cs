namespace OrangeSummer.Model
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// Word이벤트 Model
    /// </summary>
    public class Word
    {
        public int Total { get; set; }
        public int Row { get; set; }
        public string Id { get; set; }
        public int Sort { get; set; }
        public string FkMember { get; set; }
        public string Vote { get; set; }
        public string RegistDate { get; set; }
        public Member Member { get; set; }
        public Branch Branch { get; set; }
    }
}
