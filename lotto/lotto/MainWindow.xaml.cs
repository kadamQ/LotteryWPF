using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace lotto
{
    public partial class MainWindow : Window
    {
        static Random lotteryNumber = new Random();
        static int matches = 0;
        List<int> userNumbers = new List<int>();
        List<int> lotteryNumbers = new List<int>();
        FontWeight Bold = FontWeights.Bold;
        double prize = 0;
        double money = 0;
        const int maxHighScoreListEntryCount = 10;
        string playerName = String.Empty;
        bool noError = true;

        public MainWindow()
        {
            InitializeComponent();
            HideOpen(false);
            HideChoose();
            HideGame();
            HideScoreboard();
            LoadHighscoreList();

        }
        #region Set name then start
        private void SetName()
        {
            if (tbplayerName.Text.Length <= 13 && tbplayerName.Text != String.Empty && !tbplayerName.Text.StartsWith(" "))
            {
                playerName = tbplayerName.Text;
                HideOpen();
                HideChoose(false);
            }
        }
        async private void BtnDraw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(1 <= (Convert.ToInt32(txtUserNum1.Text)) && (Convert.ToInt32(txtUserNum1.Text)) <= 90
                        && 1 <= (Convert.ToInt32(txtUserNum2.Text)) && (Convert.ToInt32(txtUserNum2.Text)) <= 90
                        && 1 <= (Convert.ToInt32(txtUserNum3.Text)) && (Convert.ToInt32(txtUserNum3.Text)) <= 90
                        && 1 <= (Convert.ToInt32(txtUserNum4.Text)) && (Convert.ToInt32(txtUserNum4.Text)) <= 90
                        && 1 <= (Convert.ToInt32(txtUserNum5.Text)) && (Convert.ToInt32(txtUserNum5.Text)) <= 90))
                {
                    throw new Exception("A számnak 1 és 90 között kell lennie!");
                }
                if (!((Convert.ToInt32(txtUserNum1.Text)) != (Convert.ToInt32(txtUserNum2.Text))
                        && (Convert.ToInt32(txtUserNum1.Text)) != (Convert.ToInt32(txtUserNum3.Text))
                        && (Convert.ToInt32(txtUserNum1.Text)) != (Convert.ToInt32(txtUserNum4.Text))
                        && (Convert.ToInt32(txtUserNum1.Text)) != (Convert.ToInt32(txtUserNum5.Text))
                        && (Convert.ToInt32(txtUserNum2.Text)) != (Convert.ToInt32(txtUserNum3.Text))
                        && (Convert.ToInt32(txtUserNum2.Text)) != (Convert.ToInt32(txtUserNum4.Text))
                        && (Convert.ToInt32(txtUserNum2.Text)) != (Convert.ToInt32(txtUserNum5.Text))
                        && (Convert.ToInt32(txtUserNum3.Text)) != (Convert.ToInt32(txtUserNum4.Text))
                        && (Convert.ToInt32(txtUserNum3.Text)) != (Convert.ToInt32(txtUserNum5.Text))
                        && (Convert.ToInt32(txtUserNum4.Text)) != (Convert.ToInt32(txtUserNum5.Text))))
                {
                    throw new Exception("Nem lehet két azonos számjegy megadva!");
                }
                userNumbers.Clear();
                foreach (var TextBox in userNumPanel.Children)
                {
                    userNumbers.Add(Convert.ToInt32((TextBox as TextBox).Text));
                }
                noError = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hiba történt: " + ex.Message, "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                noError = false;
            }

            #endregion
            #region Prepare to run
            if (noError)
            {
                btnDraw.IsEnabled = false;
                btnBack.IsEnabled = false;
                btnExit2.IsEnabled = false;
                lotteryNumbers.Clear();
                lblUserNumbers.Content = String.Empty;
                userNumbers.Sort();
                lblUserNumbers.Content = lblUserNumbers.Content + string.Join(", ", userNumbers);
                lotteryNumbers.Clear();
                lblLotteryNumbers.Content = String.Empty;
                lblResult.Content = String.Empty;
                lblMoney.Content = String.Empty;

                foreach (var TextBox in userNumPanel.Children)
                {
                    (TextBox as TextBox).IsEnabled = false;
                }
                foreach (var TextBlock in resultPanel.Children)
                {
                    (TextBlock as TextBlock).Text = lotteryNumber.Next(1, 91).ToString();
                    (TextBlock as TextBlock).Foreground = Brushes.Black;
                    (TextBlock as TextBlock).FontWeight = FontWeights.Normal;
                }

                #endregion
                #region Draw the numbers
                for (int i = 0; i < 15; i++)
                {
                    do
                    {
                        txtResult1.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(50);
                        txtResult2.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(50);
                        txtResult3.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(50);
                        txtResult4.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(50);
                        txtResult5.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(50);
                    }
                    while (lotteryNumbers.Contains(Convert.ToInt32(txtResult1.Text)));
                }
                lotteryNumbers.Add(Convert.ToInt32(txtResult1.Text));
                txtResult1.Foreground = Brushes.Blue;
                txtResult1.FontWeight = Bold;
                lotteryNumbers.Sort();
                lblLotteryNumbers.Content = string.Join(", ", lotteryNumbers);
                for (int i = 0; i < 10; i++)
                {
                    do
                    {
                        txtResult2.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(75);
                        txtResult3.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(75);
                        txtResult4.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(75);
                        txtResult5.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(75);
                    }
                    while (lotteryNumbers.Contains(Convert.ToInt32(txtResult2.Text)));
                }
                lotteryNumbers.Add(Convert.ToInt32(txtResult2.Text));
                txtResult2.Foreground = Brushes.DarkGoldenrod;
                txtResult2.FontWeight = Bold;
                lotteryNumbers.Sort();
                lblLotteryNumbers.Content = string.Join(", ", lotteryNumbers);

                for (int i = 0; i < 9; i++)
                {
                    do
                    {
                        txtResult3.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(110);
                        txtResult4.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(110);
                        txtResult5.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(110);
                    }
                    while (lotteryNumbers.Contains(Convert.ToInt32(txtResult3.Text)));
                }
                lotteryNumbers.Add(Convert.ToInt32(txtResult3.Text));
                txtResult3.Foreground = Brushes.DarkGreen;
                txtResult3.FontWeight = Bold;
                lotteryNumbers.Sort();
                lblLotteryNumbers.Content = string.Join(", ", lotteryNumbers);
                for (int i = 0; i < 8; i++)
                {
                    do
                    {
                        txtResult4.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(150);
                        txtResult5.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(150);
                    }
                    while (lotteryNumbers.Contains(Convert.ToInt32(txtResult4.Text)));
                }
                lotteryNumbers.Add(Convert.ToInt32(txtResult4.Text));
                txtResult4.Foreground = Brushes.DarkRed;
                txtResult4.FontWeight = Bold;
                lotteryNumbers.Sort();
                lblLotteryNumbers.Content = string.Join(", ", lotteryNumbers);
                for (int i = 0; i < 7; i++)
                {
                    do
                    {
                        txtResult5.Text = lotteryNumber.Next(1, 91).ToString();
                        await Task.Delay(250);
                    }
                    while (lotteryNumbers.Contains(Convert.ToInt32(txtResult5.Text)));
                }
                lotteryNumbers.Add(Convert.ToInt32(txtResult5.Text));
                txtResult5.Foreground = Brushes.DarkViolet;
                txtResult5.FontWeight = Bold;
                lotteryNumbers.Sort();
                lblLotteryNumbers.Content = string.Join(", ", lotteryNumbers);
                #endregion
                #region Result
                foreach (var userNum in userNumbers)
                    matches += lotteryNumbers.Contains(userNum) ? 1 : 0;

                if (matches == 0)
                {
                    prize = 0;
                    money = 0;
                    lblResult.Content = "Nincs találat!";
                    lblMoney.Content = "Az Ön nyereménye nulla forint.";
                }
                else if (matches == 1)
                {
                    prize = 0;
                    money = 1;
                    lblResult.Content = "Egyes találat!";
                    lblMoney.Content = "Az ön nyereménye nulla forint.";
                }
                else if (matches == 2)
                {
                    prize = lotteryNumber.Next(1140, 1710);
                    money = (int)prize;
                    lblResult.Content = "Kettes találat!";
                    lblMoney.Content = "Az ön nyereménye " + prize + " forint!";
                }
                else if (matches == 3)
                {
                    prize = lotteryNumber.Next(16950, 25430);
                    money = (int)prize;
                    lblResult.Content = "Hármas találat!";
                    lblMoney.Content = "Az ön nyereménye " + prize + " forint!";
                }
                else if (matches == 4)
                {
                    prize = Math.Round(lotteryNumber.NextDouble() * (1.987445 - 1.324965) + 1.324965, 2);
                    money = (int)prize * 1000000;
                    lblResult.Content = "Négyes találat!";
                    lblMoney.Content = "Az ön nyereménye " + prize + " millió forint!";
                }
                else if (matches == 5)
                {
                    prize = Math.Round(lotteryNumber.NextDouble() * (2.682 - 1.788) + 1.788, 2);
                    money = (double)prize * 1000000000;
                    lblResult.Content = "Telitalálat!";
                    lblMoney.Content = "Az ön nyereménye " + prize + " milliárd forint!";
                }
                DoHighScore();
                #endregion
                #region Reset
                matches = 0;
                btnDraw.IsEnabled = true;
                btnBack.IsEnabled = true;
                btnExit2.IsEnabled = true;

                foreach (var TextBox in userNumPanel.Children)
                {
                    (TextBox as TextBox).IsEnabled = true;
                }

            }
        }
        #endregion
        #region Save and load scores
        public class LotteryHighScores
        {
            public string PlayerName { get; set; }

            public int Score { get; set; }

            public string UserNumbers { get; set; }

            public string LotteryNumber { get; set; }

            public string Prize { get; set; }

            public double Money { get; set; }
        }

        public ObservableCollection<LotteryHighScores> HighscoreList
        {
            get; set;
        } = new ObservableCollection<LotteryHighScores>();

        private void LoadHighscoreList()
        {
            if (File.Exists("lottery_highscorelist.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<LotteryHighScores>));
                using (Stream reader = new FileStream("lottery_highscorelist.xml", FileMode.Open))
                {
                    List<LotteryHighScores> tempList = (List<LotteryHighScores>)serializer.Deserialize(reader);
                    this.HighscoreList.Clear();
                    foreach (var item in tempList.OrderByDescending(x => x.Score))
                        this.HighscoreList.Add(item);
                }
            }
        }
        private void SaveHighscoreList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<LotteryHighScores>));
            using (Stream writer = new FileStream("lottery_highscorelist.xml", FileMode.Create))
            {
                serializer.Serialize(writer, this.HighscoreList);
            }
        }
        private void DoHighScore()
        {
            int newIndex = 0;
            if ((this.HighscoreList.Count > 0) && (money < this.HighscoreList.Max(x => x.Money)))
            {
                LotteryHighScores justAbove = this.HighscoreList.OrderByDescending(x => x.Money).First(x => x.Money >= money);
                if (justAbove != null)
                    newIndex = this.HighscoreList.IndexOf(justAbove) + 1;
            }
            string temp;
            if (matches == 4)
            {
               temp = prize + " millió forint";
            }
            else if (matches == 5)
            {
                temp = prize + " milliárd forint";
            }
            else
            {
                temp = prize + " forint";
            }
            this.HighscoreList.Insert(newIndex, new LotteryHighScores()
            {
                PlayerName = tbplayerName.Text,
                UserNumbers = String.Join(", ", userNumbers.ToArray()),
                LotteryNumber = String.Join(", ", lotteryNumbers.ToArray()),
                Score = matches,
                Prize = temp,
                Money = money
            });
            while (this.HighscoreList.Count > maxHighScoreListEntryCount)
                this.HighscoreList.RemoveAt(maxHighScoreListEntryCount);

            SaveHighscoreList();
        }
        #endregion

        #region Visibility
        private void HideOpen(bool hide = true)
        {
            Open.Visibility = hide ? Visibility.Hidden : Visibility.Visible;
        }
        private void HideGame(bool hide = true)
        {
            Game.Visibility = hide ? Visibility.Hidden : Visibility.Visible;
        }
        private void HideScoreboard(bool hide = true)
        {
            Scoreboard.Visibility = hide ? Visibility.Hidden : Visibility.Visible;
        }
        private void HideChoose(bool hide = true)
        {
            Choose.Visibility = hide ? Visibility.Hidden : Visibility.Visible;
        }
        #endregion

        #region Navigate
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void TbplayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SetName();
        }
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            SetName();
        }
        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            HideOpen();
            HideChoose();
            HideScoreboard();
            HideGame(false);
        }
        private void BtnPrize_Click(object sender, RoutedEventArgs e)
        {
            HideOpen();
            HideChoose();
            HideScoreboard(false);
            HideGame();
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            HideOpen();
            HideChoose(false);
            HideScoreboard();
            HideGame();
            lblResult.Content = String.Empty;
            lblUserNumbers.Content = String.Empty;
            lblLotteryNumbers.Content = String.Empty;
            lblMoney.Content = string.Empty;
        }
       
    }
}
#endregion
