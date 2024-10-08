using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreMVC002.Models;
using Microsoft.AspNetCore.Mvc;


namespace CoreMVC002.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            // 創建猜測模型: 初始化猜測數字+比對結果+比對邏輯
            var model = new XAXBEngine();

            // 將遊戲引擎保存到 TempData 中以便後續使用
            TempData["GameEngine"] = model;

            // 使用 TempData.Keep() 來確保它不會在下一個請求後消失
            TempData.Keep("GameEngine");

            // 使用強型別
            return View(model);
        }

        [HttpPost]
        public ActionResult Guess(string guess)
        {
            // 從 TempData 中取出遊戲引擎
            var model = TempData.Peek("GameEngine") as XAXBEngine;
            if (model == null)
            {
                // 如果 TempData 中找不到遊戲引擎，重新創建一個
                model = new XAXBEngine();
            }

            // 檢查猜測輸入是否合法
            if (string.IsNullOrEmpty(guess) || guess.Length != 4)
            {
                ModelState.AddModelError("", "請輸入四位數字作為猜測。");
                TempData["GameEngine"] = model;
                TempData.Keep("GameEngine");
                return View("Index", model);
            }

            // 執行猜測邏輯
            model.MakeGuess(guess);

            // 判斷遊戲是否結束並顯示提示信息
            if (model.GameOver)
            {
                ViewBag.Message = "恭喜你猜對了！是否要重新開始遊戲？請選擇 T (重新開始) 或 F (結束遊戲)。";
                TempData["GameEngine"] = model;
                TempData.Keep("GameEngine");
                return View("GameOver", model);
            }

            // 保持 TempData 的狀態以便後續請求使用
            TempData["GameEngine"] = model;
            TempData.Keep("GameEngine");

            // 返回更新後的模型
            return View("Index", model);
        }

        [HttpPost]
        public ActionResult ResetGame(string choice)
        {
            // 從 TempData 中取出遊戲引擎
            var model = TempData.Peek("GameEngine") as XAXBEngine;
            if (model == null)
            {
                model = new XAXBEngine();
            }

            // 根據玩家的選擇重置遊戲或結束遊戲
            bool restart = choice.Equals("T", StringComparison.OrdinalIgnoreCase);
            model.ResetGame(restart);

            TempData["GameEngine"] = model;
            TempData.Keep("GameEngine");

            if (restart)
            {
                return View("Index", model); // 重新開始遊戲
            }
            else
            {
                ViewBag.Message = "遊戲已結束，感謝您的參與！";
                return View("GameOver"); // 結束遊戲，顯示簡單的結束訊息
            }
        }

    }
}

