namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        public string Secret { get; set; }
        public string Guess { get; set; }
        public string Result { get; set; }
        public bool IsCorrect { get; set; }

        // 猜測次數的累計
        public int GuessCount { get; set; }
        // 猜對後的遊戲結束提示信息
        public string GameOverMessage { get; set; }

        // 用字符串來保存所有的猜測記錄
        public string GuessHistoryString { get; set; }

        //自定義秘密數字
        public XAXBEngine()
        {
            string randomSecret = "1234";
            Secret = randomSecret;
            Guess = null;
            Result = null;
            GuessCount = 0;
            GameOverMessage = null;
            IsCorrect = false;
        }
        
        //隨機產生秘密數字
        public XAXBEngine(string secretNumber)
        {
            Secret = secretNumber;
            Guess = null;
            Result = null;
            GuessCount = 0;
            GameOverMessage = null;
            IsCorrect = false;
        }
        
        //
        public int numOfA(string guessNumber)
        {
            int aCount = 0;
            for (int i = 0; i < Secret.Length; i++)
            {
                if (Secret[i] == guessNumber[i])
                {
                    aCount++;
                }
            }
            return aCount;
        }
        //
        public int numOfB(string guessNumber)
        {
            int bCount = 0;
            for (int i = 0; i < Secret.Length; i++)
            {
                if (Secret[i] != guessNumber[i] && Secret.Contains(guessNumber[i]))
                {
                    bCount++;
                }
            }
            return bCount;
        }
        //
        public bool IsGameOver(string guessNumber)
        {
            // TODO 3
            if (Secret.Equals(guessNumber))
            {
                GameOverMessage = "恭喜你猜對了！是否重新開始遊戲？";
                return true;
            }
            return false;
        }
    }
}