namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        public string Secret { get; set; }
        public string Guess { get; set; }
        public string Result { get; set; }
        // 猜測的歷史記錄，每次猜測的內容與結果
     //   public List<string> GuessHistory { get; set; } = new List<string>();
        // 猜測次數的累計
        public int GuessCount { get; set; }
        // 猜對後的遊戲結束提示信息
        public string GameOverMessage { get; set; }

        // 用字符串來保存所有的猜測記錄
        public string GuessHistoryString { get; set; }

        public XAXBEngine()
        {
            // TODO 0 - randomly 
            string randomSecret = "1234";
            //
            Secret = randomSecret;
            Guess = null;
            Result = null;
            GuessCount = 0;
            GameOverMessage = null;
        }

        public XAXBEngine(string secretNumber)
        {
            Secret = secretNumber;
            Guess = null;
            Result = null;
            GuessCount = 0;
            GameOverMessage = null;
        }
        //
        public int numOfA(string guessNumber)
        {
            // TODO 1
            return 0;
        }
        //
        public int numOfB(string guessNumber)
        {
            // TODO 2
            return 0;
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
        /*
        public void AddGuessHistory(string guess, string result)
        {
            // 將本次猜測內容和結果添加到歷史記錄中
            GuessHistory.Add($"猜測: {guess}, 結果: {result}");
            // 增加猜測次數
            GuessCount++;
        }
        */
    }

}