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

                // 創建猜測模型: 猜測數字+比對結果+比對邏輯
                var model = new XAXBEngine();

                // 使用強型別
                return View(model);
            }

            [HttpPost]
            public ActionResult Guess(XAXBEngine model)
            {
                // 取得秘密數字
                string secretNumber = TempData["secretNumber"] as string;

                // 讀取猜測次數
                int guessCount = TempData["GuessCount"] != null ? (int)TempData["GuessCount"] : 0;

                // 增加猜測次數
                guessCount++;
                TempData["GuessCount"] = guessCount;

                // 更新模型中的 GuessCount
                model.GuessCount = guessCount;

                // 檢查猜測結果
                model.Result = GetGuessResult(model.Guess);
                
                // 保留 TempData 中的秘密數字和猜測次數，避免它們在這次請求後被清除
                TempData.Keep("secretNumber");
                TempData.Keep("GuessCount");

                return View("Index", model);
            }

            // ------ 遊戲相關之邏輯 ------
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

            private string GetGuessResult(string guess)
            {
                string secretNumber = TempData["secretNumber"] as string;
                TempData.Keep("secretNumber");

                int aCount = 0; // 數字和位置都正確的數量
                int bCount = 0; // 數字正確但位置錯誤的數量

                for (int i = 0; i < secretNumber.Length; i++)
                {
                    if (guess[i] == secretNumber[i])
                    {
                        aCount++;
                    }
                    else if (secretNumber.Contains(guess[i]))
                    {
                        bCount++;
                    }
                }

                return $"{aCount}A{bCount}B";
            }
        }
    }