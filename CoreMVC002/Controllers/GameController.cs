    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CoreMVC002.Models;
    using Microsoft.AspNetCore.Mvc;

    // For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

    namespace CoreMVC002.Controllers
    {
        public class GameController : Controller
        {
            [HttpGet]
            public ActionResult Index()
            {
                // 初始化秘密數字
                string secretNumber = GenerateSecretNumber();

                // 傳遞到 View 內部後再回到 Controller
                TempData["secretNumber"] = secretNumber;
                
                // 初始化猜測次數
                if (TempData["GuessCount"] == null)
                {
                    TempData["GuessCount"] = 0;
                }

                 // 初始化猜測記錄（如果不存在）
                if (TempData["GuessHistory"] == null)
                {
                    TempData["GuessHistory"] = new List<string>();
                }


                // 創建猜測模型: 猜測數字+比對結果+比對邏輯
                var model = new XAXBEngine(secretNumber);

                // 使用強型別
                return View(model);
            }

            [HttpPost]
            public ActionResult Guess(XAXBEngine model)
            {
                // 取得秘密數字
                string secretNumber = TempData["secretNumber"] as string;

                model.Secret = secretNumber;

                // 讀取猜測次數
                int guessCount = TempData["GuessCount"] != null ? (int)TempData["GuessCount"] : 0;

                // 增加猜測次數
                guessCount++;
                TempData["GuessCount"] = guessCount;

                // 更新模型中的 GuessCount
                model.GuessCount = guessCount;

                // 檢查猜測結果
                //model.Result = GetGuessResult(model.Guess);
                int aCount = model.numOfA(model.Guess);
                int bCount = model.numOfB(model.Guess);
                model.Result = $"{aCount}A{bCount}B";

                    
                // 記錄每次猜測的數字和結果
                string guessHistory = TempData["GuessHistory"] as string ?? ""; // 如果為 null，則初始化為空字符串
                string guessRecord = $"猜測 {model.Guess}: {model.Result}";
                guessHistory += guessRecord + "\n";
                TempData["GuessHistory"] = guessHistory; // 更新 TempData

                 // 將猜測記錄加入模型中
                model.GuessHistoryString = guessHistory;  
                

                
                // 判斷是否猜對
                 model.IsCorrect = model.IsGameOver(model.Guess);


                // 保留 TempData 中的秘密數字和猜測次數，避免它們在這次請求後被清除
                TempData.Keep("secretNumber");
                TempData.Keep("GuessCount");
                TempData.Keep("GuessHistory");
                return View("Index", model);
            }

            // ------ 遊戲相關之邏輯 ------

            [HttpPost]
            public ActionResult Restart()
            {
                // 清空 TempData 的數據，重新初始化遊戲
                TempData.Remove("secretNumber");
                TempData.Remove("GuessCount");
                TempData.Remove("GuessHistory");

                // 重定向回 Index，重新開始遊戲
                return RedirectToAction("Index");
            }


            private string GenerateSecretNumber()
            {
                // 創建一個隨機數生成器
                Random random = new Random();
                // 使用 HashSet 來確保生成的數字不重複
                HashSet<int> digits = new HashSet<int>();

                // 生成 4 個不重複的隨機數字
                while (digits.Count < 4)
                {
                    int digit = random.Next(0, 10); // 在 0 到 9 之間生成一個隨機數字
                    digits.Add(digit); // 將生成的數字添加到 HashSet 中，如果重複則不會添加
                }

                // 將 HashSet 中的數字轉換為字符串並返回
                return string.Join("", digits);
            }
        }
    }