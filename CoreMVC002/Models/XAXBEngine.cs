namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        public string Secret { get; set; }
        public string Guess { get; set; }
        public string Result { get; set; }
        public int GuessCount { get; set; } // 猜測次數
        public List<string> GuessHistory { get; set; } // 猜測歷史紀錄
        public bool GameOver { get; set; } // 遊戲是否結束

        //隨機產生數字
        public XAXBEngine()
        {
            Secret = GenerateRandomSecret();
            Guess = string.Empty; ;
            Result = string.Empty; ;
            GuessCount = 0;
            GuessHistory = new List<string>();
            GameOver = false;
        }

        //設定秘密數字
        public XAXBEngine(string secretNumber)
        {
            Secret = secretNumber;
            Guess = null;
            Result = null;
            GuessCount = 0;
            GuessHistory = new List<string>();
            GameOver = false;
        }

        public void MakeGuess(string guessNumber)
        {
            Guess = guessNumber;
            GuessCount++;
            int aCount = numOfA(guessNumber); // 計算 A 的數量
            int bCount = numOfB(guessNumber); // 計算 B 的數量
            Result = $"{aCount}A{bCount}B"; // 設定結果為 xAyB 格式
            GuessHistory.Add($"Guess {GuessCount}: {guessNumber} => {Result}"); // 添加猜測歷史紀錄

            // 判斷是否猜對
            if (IsGameOver())
            {
                GameOver = true;
            }
        }

        //
        public int numOfA(string guessNumber)
        {
            int count = 0;
            for (int i = 0; i < Secret.Length; i++)
            {
                if (Secret[i] == guessNumber[i])
                {
                    count++;
                }
            }
            return count;
        }
        //
        public int numOfB(string guessNumber)
        {
            int count = 0;
            for (int i = 0; i < Secret.Length; i++)
            {
                if (Secret.Contains(guessNumber[i]) && Secret[i] != guessNumber[i])
                {
                    count++;
                }
            }
            return count;
        }
        //
        public bool IsGameOver()
        {
            return Guess == Secret;
        }

        // 重置遊戲以重新開始，取決於玩家的選擇
        public void ResetGame(bool restart)
        {
            if (restart)
            {
                Secret = GenerateRandomSecret(); // 重新生成隨機秘密數字
                Guess = null;
                Result = null;
                GuessCount = 0;
                GuessHistory.Clear(); // 清空猜測歷史紀錄
                GameOver = false;
            }
            else
            {
                GameOver = true; // 遊戲結束
            }
        }

        // 生成一個隨機且不重複的四位數秘密數字
        private string GenerateRandomSecret()
        {
            Random random = new Random();
            HashSet<int> digits = new HashSet<int>();

            while (digits.Count < 4)
            {
                int digit = random.Next(0, 10);
                digits.Add(digit);
            }

            return string.Join("", digits);
        }

    }

}
