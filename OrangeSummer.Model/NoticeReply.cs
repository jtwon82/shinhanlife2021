namespace OrangeSummer.Model
{
    /// <summary>
    /// 전윤기 - 2020.06.20
    /// 공지사항 이벤트 Model
    /// </summary>
    public class NoticeReply
    {
        public int Total { get; set; }
        public string Id { get; set; }
        public int Sort { get; set; }
        public string FkNotice { get; set; }
        public string FkMember { get; set; }
        public int DepthGid { get; set; }
        public int DepthSeq { get; set; }
        public int Depth { get; set; }
        public string Contents { get; set; }
        public int LikeCount { get; set; }
        public int Like { get; set; }
        public int ReplyCount { get; set; }
        public string DelYn { get; set; }
        public string RegistDate { get; set; }
        public Member Member { get; set; }
        public Branch Branch { get; set; }
    }
}
