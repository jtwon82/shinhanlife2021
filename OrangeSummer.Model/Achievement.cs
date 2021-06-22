namespace OrangeSummer.Model
{
    /// <summary>
    /// 전윤기 - 2020.06.19
    /// 업적관리 Model
    /// </summary>
    public class Achievement
    {
        public int Total { get; set; }
        public string Id { get; set; }
        public int Sort { get; set; }
        public string Date { get; set; }
        public string Part { get; set; }
        public string FkBranch { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string PersonSum { get; set; }
        public string PersonCmip { get; set; }
        public string PersonCamp { get; set; }
        public string PersonRank { get; set; }
        public string PersonRank2 { get; set; }
        public string LeaderCmip { get; set; }
        public string LeaderRank { get; set; }
        public string BranchRank { get; set; }
        public string BranchCmip { get; set; }
        public string SlRank { get; set; }
        public string SlCmip { get; set; }

        public Branch Branch { get; set; }
    }
}