using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static Mercuryfire_BackendExam_APIServer.Models.Mercuryfire_BackendExam;
using static Mercuryfire_BackendExam_APIServer.Models.Dto;

namespace Mercuryfire_BackendExam_APIServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(Mercuryfire_BackendExamDbContext db) : ControllerBase
    {

        /// <summary>
        /// API 測試
        /// </summary>
        [HttpGet("ApiCheck")]
        public IActionResult ApiCheck() => Ok(new { message = "Success!" });

        /// <summary>
        /// 資料庫連線檢查
        /// </summary>
        [HttpGet("DBCheck")]
        public IActionResult DBCheck()
        {
            try
            {
                db.Database.CanConnect();
                return Ok(new { message = "資料庫連線成功" });
            }
            catch
            {
                return StatusCode(500, new { message = "資料庫連線失敗" });
            }
        }

        /// <summary>
        /// 取得所有帳號資料
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try 
            {
                var accounts = await db.MyOffice_ACPD.ToListAsync();
                await LogOperation("GetAll", "查詢所有帳號");
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                await AddErrorLog("GetAll", "AccountController", $"查詢帳號失敗: {ex.Message}");
                return StatusCode(500, new { message = "查詢帳號時發生錯誤" });
            }
        }

        /// <summary>
        /// 新增帳號
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ACPDCreateDto dto)
        {
            try 
            {
                var newSid = await GenerateNewSID();
                var account = new MyOffice_ACPD
                {
                    ACPD_SID = newSid,
                    ACPD_Cname = dto.ACPD_Cname,
                    ACPD_Ename = dto.ACPD_Ename,
                    ACPD_Sname = dto.ACPD_Sname,
                    ACPD_Email = dto.ACPD_Email,
                    ACPD_Status = dto.ACPD_Status,
                    ACPD_Stop = dto.ACPD_Stop,
                    ACPD_StopMemo = dto.ACPD_StopMemo,
                    ACPD_LoginID = dto.ACPD_LoginID,
                    ACPD_LoginPWD = dto.ACPD_LoginPWD,
                    ACPD_Memo = dto.ACPD_Memo,
                    ACPD_UPDDateTime = DateTime.Now,
                    ACPD_UPDID = "System",
                    ACPD_NowID = newSid,
                    ACPD_NowDateTime = DateTime.Now
                };

                db.MyOffice_ACPD.Add(account);
                await db.SaveChangesAsync();
                
                await LogOperation("Create", $"新增帳號: {JsonConvert.SerializeObject(dto)}");
                return Ok(account);
            }
            catch (Exception ex)
            {
                await AddErrorLog("Create", "AccountController", $"新增帳號失敗: {ex.Message}");
                return StatusCode(500, new { message = "新增帳號時發生錯誤" });
            }
        }

        /// <summary>
        /// 更新帳號資料
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ACPDUpdateDto dto)
        {
            try 
            {
                var account = await db.MyOffice_ACPD.FindAsync(dto.ACPD_SID);
                if (account == null)
                    return NotFound();

                account.ACPD_Cname = dto.ACPD_Cname;
                account.ACPD_Ename = dto.ACPD_Ename;
                account.ACPD_Sname = dto.ACPD_Sname;
                account.ACPD_Email = dto.ACPD_Email;
                account.ACPD_Status = dto.ACPD_Status;
                account.ACPD_Stop = dto.ACPD_Stop;
                account.ACPD_StopMemo = dto.ACPD_StopMemo;
                account.ACPD_LoginID = dto.ACPD_LoginID;
                account.ACPD_LoginPWD = dto.ACPD_LoginPWD;
                account.ACPD_Memo = dto.ACPD_Memo;
                account.ACPD_UPDDateTime = DateTime.Now;
                account.ACPD_UPDID = "System";
                account.ACPD_NowID = dto.ACPD_SID;
                account.ACPD_NowDateTime = DateTime.Now;


                await db.SaveChangesAsync();
                
                await LogOperation("Update", $"更新帳號: {JsonConvert.SerializeObject(dto)}");
                return Ok(account);
            }
            catch (Exception ex)
            {
                await AddErrorLog("Update", "AccountController", $"更新帳號失敗: {ex.Message}");
                return StatusCode(500, new { message = "更新帳號時發生錯誤" });
            }
        }

        /// <summary>
        /// 刪除帳號
        /// </summary>
        [HttpDelete("{sid}")]
        public async Task<IActionResult> Delete(string sid)
        {
            try 
            {
                var account = await db.MyOffice_ACPD.FindAsync(sid);
                if (account == null)
                    return NotFound();

                db.MyOffice_ACPD.Remove(account);
                await db.SaveChangesAsync();
                
                await LogOperation("Delete", $"刪除帳號: {sid}");
                return Ok(account);
            }
            catch (Exception ex)
            {
                await AddErrorLog("Delete", "AccountController", $"刪除帳號失敗: {ex.Message}");
                return StatusCode(500, new { message = "刪除帳號時發生錯誤" });
            }
        }

        private async Task LogOperation(string operation, string info)
        {
            var log = new MyOffice_ExecutionLog
            {
                DeLog_ExecutionProgram = operation,
                DeLog_ExecutionInfo = info,
                DeLog_ExDateTime = DateTime.Now.Date.Add(DateTime.Now.TimeOfDay)
            };

            db.MyOffice_ExecutionLog.Add(log);
            await db.SaveChangesAsync();
        }

        /// <summary>
        /// 產生新的 SID
        /// </summary>
        private async Task<string> GenerateNewSID()
        {
            while (true)
            {
                var currentYear = DateTime.Now.Year - 2000;
                if (currentYear > 1295) currentYear = 1295;
                
                var dayOfYear = DateTime.Now.DayOfYear;
                var secondOfDay = DateTime.Now.Second + (60 * DateTime.Now.Minute) + (3600 * DateTime.Now.Hour);
                
                const string alphabets = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var firstDigit = alphabets[currentYear / 36 % 36];
                var secondDigit = alphabets[currentYear % 36];
                var prefix = $"{firstDigit}{secondDigit}";
                
                var dayCode = dayOfYear.ToString("D3");
                var secondCode = secondOfDay.ToString("D5");
                var randomValue = GenerateRandomString(10);
                
                var newSID = $"{prefix}{dayCode}{secondCode}{randomValue}";
                
                // 檢查 SID 是否已存在
                var exists = await db.MyOffice_ACPD.AnyAsync(x => x.ACPD_SID == newSID);
                if (!exists)
                    return newSID;
            }
        }

        /// <summary>
        /// 產生指定長度的隨機數字字串
        /// </summary>
        private static string GenerateRandomString(int length)
        {
            var random = new Random();
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = (char)('0' + random.Next(0, 10));
            }
            return new string(result);
        }

        /// <summary>
        /// 記錄執行錯誤
        /// </summary>
        private async Task AddErrorLog(string storedProcedure, string executionProgram, string actionInfo)
        {
            var groupId = Guid.NewGuid();
            var log = new MyOffice_ExecutionLog
            {
                DeLog_StoredPrograms = storedProcedure,
                DeLog_GroupID = groupId,
                DeLog_ExecutionProgram = executionProgram,
                DeLog_ExecutionInfo = actionInfo,
                DeLog_ExDateTime = DateTime.Now.Date.Add(DateTime.Now.TimeOfDay)
            };

            db.MyOffice_ExecutionLog.Add(log);
            await db.SaveChangesAsync();
        }
    }
}
