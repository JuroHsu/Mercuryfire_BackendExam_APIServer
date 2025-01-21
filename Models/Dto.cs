namespace Mercuryfire_BackendExam_APIServer.Models
{
    public class Dto
    {
        public class ACPDCreateDto
        {
            public string ACPD_Cname { get; set; } = string.Empty;
            public string ACPD_Ename { get; set; } = string.Empty;
            public string ACPD_Sname { get; set; } = string.Empty;
            public string ACPD_Email { get; set; } = string.Empty;
            public byte ACPD_Status { get; set; }
            public bool ACPD_Stop { get; set; }
            public string ACPD_StopMemo { get; set; } = string.Empty;
            public string ACPD_LoginID { get; set; } = string.Empty;
            public string ACPD_LoginPWD { get; set; } = string.Empty;
            public string ACPD_Memo { get; set; } = string.Empty;
        }

        public class ACPDUpdateDto : ACPDCreateDto
        {
            public string ACPD_SID { get; set; } = string.Empty;
        }
    }
}
