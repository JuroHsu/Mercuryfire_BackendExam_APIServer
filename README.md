# 後端考試 API 伺服器實作

這是一個使用 .NET Core Web API 實作的後端考試專案，主要實現了帳號管理相關的 API 功能。

## 專案說明

本專案採用 C# 實作原本需要使用 T-SQL Stored Procedure 的功能，主要考量如下：

1. 基於對 C# 的熟悉度較高，選擇使用 C# 來實現相關功能，可以更好地掌控程式邏輯和維護性。

2. 資料表命名規範已進行調整，以符合更好的實務做法。

3. 關於欄位命名的建議：
   - 建立日期建議使用 `CreatedAt`
   - 更新日期建議使用 `UpdatedAt`
   - 這樣的命名方式更符合業界慣例，提高程式碼可讀性

4. 關於使用者身份識別：
   - 目前系統中的 `ACPD_UPDID` 和 `ACPD_NowID` 欄位應該是用於記錄建立/更新資料的使用者 ID
   - 關於欄位命名的建議：
     - ACPD_UPDID 建議使用 `ACPD_UpdateID`
     - ACPD_NowID 建議使用 `ACPD_CreateID`
   - 建議實作 JWT 驗證機制：
     - 可以在使用者登入時將 JWT 存入 session
     - 用於 API 的身份驗證
     - 方便取得當前操作者的 ID

5. 併發處理考量：
   - 由於主要處理使用者帳號資料，預期不會有高併發的情況
   - 如果未來有其他高併發的需求，可以考慮加入鎖機制來處理

6. 開發測試相關：
   - 目前 CORS 政策設定為全部開放，方便開發測試
   - 實際部署時應該要根據需求設定適當的 CORS 規則

7. 資料庫連線：
   - 目前使用個人測試用資料庫
   - 實際部署時需要更換為正式環境的資料庫連線字串

## API 端點

專案實作了以下 API 端點：

- `GET /api/Account/ApiCheck` - API 可用性測試
- `GET /api/Account/DBCheck` - 資料庫連線測試
- `GET /api/Account` - 取得所有帳號資料
- `POST /api/Account` - 新增帳號
- `PUT /api/Account` - 更新帳號資料
- `DELETE /api/Account/{sid}` - 刪除帳號

## 系統功能

1. 完整的帳號 CRUD 操作
2. 自動生成唯一的 SID
3. 操作日誌記錄
4. 錯誤處理機制

## 未來優化建議

1. 實作完整的身份驗證機制
2. 加強資料驗證
3. 優化錯誤處理機制
4. 調整 CORS 政策以符合安全需求
5. 加入單元測試
6. 規劃正式環境的部署流程

## 技術堆疊

- .NET Core Web API
- Entity Framework Core
- Microsoft SQL Server
- C#