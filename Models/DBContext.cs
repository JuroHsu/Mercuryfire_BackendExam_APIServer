using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Mercuryfire_BackendExam_APIServer.Models
{
    public class Mercuryfire_BackendExam
    {
        public class MyOffice_ACPD
        {
            [Key]
            public string ACPD_SID { get; set; } = string.Empty;
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
            public DateTime ACPD_NowDateTime { get; set; } = DateTime.Now;
            public string ACPD_NowID { get; set; } = string.Empty;
            public DateTime ACPD_UPDDateTime { get; set; } = DateTime.Now;
            public string ACPD_UPDID { get; set; } = string.Empty;
        }

        public class MyOffice_ExecutionLog
        {
            [Key]
            public long DeLog_AutoID { get; set; }
            public string DeLog_StoredPrograms { get; set; } = string.Empty;
            public Guid DeLog_GroupID { get; set; }
            public bool DeLog_isCustomDebug { get; set; }
            public string DeLog_ExecutionProgram { get; set; } = string.Empty;
            public string? DeLog_ExecutionInfo { get; set; } // Nullable to match "Checked" column
            public bool? DeLog_verifyNeeded { get; set; } // Nullable to match "Checked" column
            public DateTime DeLog_ExDateTime { get; set; } = DateTime.Now;
        }


        public class Mercuryfire_BackendExamDbContext(DbContextOptions<Mercuryfire_BackendExamDbContext> options) : DbContext(options)
        {
            public required DbSet<MyOffice_ACPD> MyOffice_ACPD { get; set; }
            public required DbSet<MyOffice_ExecutionLog> MyOffice_ExecutionLog { get; set; }
        }

    }
}
