using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ExampleSaveFaleDate.Resources;
using System.IO.IsolatedStorage;
using System.Windows.Media;
using System.IO;
using System.Text;
using System.ComponentModel;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Threading.Tasks;
using System.Windows.Shapes;
using Microsoft.Phone.BackgroundAudio;
using System.Windows.Resources;
using System.Threading;

namespace ExampleSaveFaleDate
{
    public partial class MainPage : PhoneApplicationPage
    {
        readonly IsolatedStorageSettings _settings =
         IsolatedStorageSettings.ApplicationSettings; //Хранилище

        Zagadki[] array = new Zagadki[122];
        public static int q = 0;
        public string fullstringToByte;

        Golovolomki[] myArrGolovolomki = new Golovolomki[120];
        public int numberGolovolomka;

        ColorSet[] ColorAr = new ColorSet[1];

        TFScope[] ScopeAr = new TFScope[1];

        public List<string> Items;
        //private int counter;


//Массивы для раздела TrueFalse список категорий
       // int pointsTF;
        int schetchikTF;
        int userTFtrueAn;
        int liveUser;
        TFLiteratyra[] LiteratyraArr = new TFLiteratyra[120];
        int[] generatedNumbers = new int[15];
        int nRr = 0;
        string answerUserTF = "";
        public int CategoryNumber;

//**********************************************

        public int sounds = 0;
        int Nom ;                                                  
        int win;
        int help;
       

        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            InitializeSettings();
            ReadTextZagadki();
            ReadTextGolovolomki();
            ReadColorConfig();
            SetBackround();
            SetForegroundColorText();
            VisibilityConfiguration();
            TotalColorChangeButtons();
           // ClearBackEntries();



           // LoadSound("Assets/audio/black_m_sur_ma_route.wav", out sweet);
           
            //sweetInstence = sweet.CreateInstance();
            //sweetInstence.IsLooped = true;
            //sweetInstence.Volume = 0.5f;
            //sweetInstence.Play();
            

//---------------------------------------------------------------------------------------
//       ВЫВОД НА ЭКРАН ЗНАЧЕНИЙ ПЕРЕМЕННЫХ (int) - Для показа очков и кол-ва подсказок  * int win, help;
//---------------------------------------------------------------------------------------    

            if (IsolatedStorageSettings.ApplicationSettings.Contains("emailFlag1"))//--------------------  Int help;
            {
                tbok.Text = Convert.ToString(help); //"Ответы:" + Convert.ToString(help);
                tbok.Text += IsolatedStorageSettings.ApplicationSettings["emailFlag1"];
                
            }
            else
            {
                tbok.Text = "No Key Found !!";
            }                           
            if (IsolatedStorageSettings.ApplicationSettings.Contains("emailFlag"))//---------------------   Int win
            {
                tbpoints.Text = "Очки:" + Convert.ToString(win) + "/3";
                tbpoints.Text += IsolatedStorageSettings.ApplicationSettings["emailFlag"];
            }
            else
            {
                tbpoints.Text = "No Key Found !!";
            }

            
     
//------------------------------------------------------------------------------------------------------------------
//              Категория загадки проверка, подсчет очков, смена цвета кнопки
//------------------------------------------------------------------------------------------------------------------


            Nom = 1;



            
            tbnom.Text = "Загадка №" + "1";                          // вывод текста в текстблок            
            tbtext.Text = array[Nom].Question;                              // вывод первого текста загадки в текст блок "tblockZ"
            tbpoints.Text = "Очки:" + Convert.ToString(win) + "/3";  // Первый вывод Очки: 0/3
            tbok.Text =  Convert.ToString(help);          // Первый вывод Ответы: 0// "Ответы:" + Convert.ToString(help); 
        }

        void LoadSound(string p, out SoundEffect sweet)
        {

            StreamResourceInfo SoundFileInfo = App.GetResourceStream(new Uri((p), UriKind.Relative));
            sweet = SoundEffect.FromStream(SoundFileInfo.Stream);
        }

        
        private void SetForegroundColorText()
        {
            //if (ColorAr[0].foregroundTextColor == 1)
            //{
            //    //tbColor.Foreground = Application.Current.Resources["PhoneForegroundBrush"] as SolidColorBrush;
            //}
        }

        private void ReadColorConfig()
        {
            FileStream fs3 = new FileStream("TextFiles/ColorConfig.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr3 = new StreamReader(fs3, Encoding.Unicode);
            //for (int i = 0; i < 1; i++)
            //{
            ColorAr[0] = new ColorSet(sr3.ReadLine().Split('|'));
                // }
            fs3.Close();
            sr3.Close();
        }

        private void SetBackround()
        {
            
            ContentPanel.Background = new SolidColorBrush(Color.FromArgb(ColorAr[0].alfaColor, (byte)(ColorAr[0].redColor), (byte)(ColorAr[0].greenColor), (byte)(ColorAr[0].blueColor)));

        }

        private void ReadTextGolovolomki()
        {
            FileStream fs2 = new FileStream("TextFiles/TextGolovolomki.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr2 = new StreamReader(fs2, Encoding.Unicode);
            for (int i = 0; i < 99; i++)
            {
                myArrGolovolomki[i] = new Golovolomki(sr2.ReadLine().Split('|'));
            }
            fs2.Close();
            sr2.Close();
        }

        private void ClearBackEntries()
        {
            while (NavigationService.BackStack != null
        && NavigationService.BackStack.Any())
                NavigationService.RemoveBackEntry();
        }

        private void ReadTextZagadki()
        {
            FileStream fs = new FileStream("TextFiles/TextZagadki.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr = new StreamReader(fs, Encoding.Unicode);

            while (sr.Peek() != -1)
            {
                sr.ReadLine();
                q++;
            }
            sr.Close();
            fs.Close();

            FileStream fs1 = new FileStream("TextFiles/TextZagadki.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr1 = new StreamReader(fs1, Encoding.Unicode);
            for (int i = 0; i < 121; i++)
            {
                array[i] = new Zagadki(sr1.ReadLine().Split('|'));
            }               
            fs1.Close();
            sr1.Close();
        }

        private void InitializeSettings()
        {

            if (_settings.Contains("emailFlag1"))//-----Вывод из Хранилища значения переменной int help; " Ответы: (help) "
            {
                help = (int)_settings["emailFlag1"];
            }
            else                                        
            {                                           
                _settings.Add("emailFlag1", 0);        
                _settings.Save();                     
            }

            if (_settings.Contains("emailFlag"))//--- Вывод из Хранилища значения переменной int win; " Очки: (win)/3 "
            {
                win = (int)_settings["emailFlag"];
            }
            else
            {
                _settings.Add("emailFlag", 0);
                _settings.Save();
            }
                 
        }

        
//----------------------------------------------------------------------------------------------------------
//           ПРИ НАЖАТИИ НА КЛАВИШИ "НАЗАД"   "ВПЕРЕД"  МЕНЯЕТЬСЯ ТЕКСТ ЗАГАДОК СОГЛАСНО НОМЕРУ ИЗ МАССИВА
//----------------------------------------------------------------------------------------------------------
        private void Button_Click(object sender, RoutedEventArgs e)//-------------  ВПЕРЕД
        {
         
            Nom++;
            if (Nom > 120) Nom = 120;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);      // Так же происходит замена номера загадки в textblock
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//-------------  НАЗАД
        {
           
            Nom--;
            if (Nom < 1) Nom = 1;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);     // Так же происходит замена номера загадки в textblock
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
           
        }
//--------------------------------------------------------------------------------------------------------------
//         ПРОВЕРКА ВВЕДЕННОГО В textbox ОТВЕТА НА КОРРЕКТНОСТЬ И ПОВТОР             (по нажатию Enter на экранной кл-ре)
//--------------------------------------------------------------------------------------------------------------
        private /*async*/ void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key.Equals(System.Windows.Input.Key.Enter))       //  При нажатии Enter 
            {
                string tb;
                tb = tboxZ.Text;//---------------------------  Введенное значение в textbox присваеваем переменной
                if (array[Nom].AntiRepeat == "true")//---------------  Если значение соответствует true оно не повторяеться>
                {
                    if (tb == array[Nom].Answer)//-------------  Проверка в соответствии с столбцом с ответами
                    {
                        //BtnColor[Nom, 0] = 2;//----------------  Изменяем индекс на 2 для изминения цвета кнопки

                        array[Nom].AntiRepeat = "false"; //-------------  Изменяем на false что бы избежать повторного ввода ответа.
                       
                        //***********Перезапись данных в файл***************
                        WriteDataInFile();


                        
                        TotalColorChangeButtons();
                        switch (win)//--------------------------- Проверки прошли корректно, изменяються очки 
                        {
                            case 0:
                                win = +1;
                                break;
                            case 1:
                                win++;
                                break;
                            case 2://--------------------------- Если 2 , если+1  то будет '3/3' > обнуляем до 0/3
                                win = 0;
                                help++; //---------------------- Прибовляем +1 ответ. 
                                break;
                            default:
                                MessageBox.Show("ohh My  Goood");
                                break;

                        }

                        tbok.Text = Convert.ToString(help);//---------------- Выводим изминения очков на экран "Ответы:" + 
                        tbpoints.Text = "Очки:" + Convert.ToString(win) + "/3";//-------- Выводим изминения очков на экран
                        tipText.Text = array[Nom].Answer;

                        //-------------------------------------------------------------------------------------------------------
                        //               ПРИ КЛИКЕ ENTER АВТАМОТИЧЕСКИ СОХРАНЯЮТЬСЯ ЗНАЧЕНИЯ ПЕРЕМЕННЫХ 'win'   'help'   (ОЧКИ)
                        //-------------------------------------------------------------------------------------------------------


                        IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                        if (!settings.Contains("emailFlag"))//------------------------  Сохранение int win;
                        {
                            settings.Add("emailFlag", win);
                        }
                        else
                        {
                            settings["emailFlag"] = win;
                        }
                        settings.Save();

                        if (!settings.Contains("emailFlag1"))//------------------------  Сохранение int help;
                        {
                            settings.Add("emailFlag1", help);
                        }
                        else
                        {
                            settings["emailFlag1"] = help;
                        }

                        //-------------------------------------------------------------------------------------------------------------------
                        //      ПОСЛЕ ВСЕХ ПРОВЕРОК ПОКАЗ УВЕДОМЛЕНИЯ О ПРАВЕЛЬНОМ ОТВЕТЕ
                        //-------------------------------------------------------------------------------------------------------------------      
                       
                        gridWin.Visibility = Visibility.Visible;// При правильном ответе показ gridWin
                        
  
                    }

//----****----****----***     ЕСЛИ ПРОВЕРКИ НЕ ПРОШЛИ ТО ДИОЛОГ О НЕПРАВИЛЬНОМ ОТВЕТЕ           ***---***---***---
                    else
                    {
                       
                        gridFalse.Visibility = Visibility.Visible;
                        

                    }
                }
                //}
                else
                {
                    MessageBox.Show("Уже ответили!");
                }
            }
          }

        

       /* private async Task PlaySoundWin()
        {
            if (sounds == 0)
            {
                switch (MediaPlayer.State)
                {
                    case MediaState.Stopped:
                    case MediaState.Paused:
                        MediaElement me = new MediaElement();
                        this.LayoutRoot.Children.Add(me);
                        me.Source = new Uri("Assets/audio/applause_y.wav", UriKind.RelativeOrAbsolute);
                      me.Play(); 
                    
                        break;
                }
            }
        }*/

        private void WriteDataInFile()
        {
            /*for (int i = 0; i < q; i++)
            {
                string line = array[i].Question + "|" + array[i].Answer + "|" + array[i].AntiRepeat;
                SetAFullString(line);

            }*/
              fullstringToByte = "";
            for (int i = 0; i < 121; i++)
            {
                string line = array[i].Question + "|" + array[i].Answer + "|" + array[i].AntiRepeat;
                SetAFullString(line);

            }

            FileStream fs = new FileStream("TextFiles/TextZagadki.txt", FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.WriteLine(fullstringToByte);
            sw.Close();
        }

        private void SetAFullString(string line)
        {
            fullstringToByte += line + "\r\n";
        }
//-------------------    При нажатии Enter пропадает фокус, при потере фокуса, появляеться текст в текстбоксе,
//                                             и при фокусе пропадает.
//----------------------------------------------------------------------------------------------------------------

        private void tboxZ_GotFocus(object sender, RoutedEventArgs e)
        {
            tboxZ.Text = "";
        }

        private void tboxZ_LostFocus(object sender, RoutedEventArgs e)
        {
            tboxZ.Text = "Введите вариант ответа:";
        }

        private void tboxZ_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                this.Focus();
            }
        }

        struct Zagadki
        {
            public string Question;
            public string Answer;
            public string AntiRepeat;
            //public int Numb;

            public Zagadki(string[] args)
            {
                Question = args[0];
                Answer = args[1];
                AntiRepeat = args[2];
                //Numb = int.Parse(args[3]);
            }

            /*static public bool CheckFac(string repit, Zagadki[] array)
            {
                bool temp = false;
                foreach (Zagadki s in array)
                    if (s.AntiRepeat == repit)
                        temp = true;
                return temp;
            }*/
        }

        public void VisibilityConfiguration()
        {
           // bool Vision1 = true;
           // bool Vision2 = true;
            //bool Vision3 = false;
            //if (Vis == 1) Vision2 = false;
            gridGolovolom.Visibility = Visibility.Collapsed;
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Collapsed;
            gridFalse.Visibility = Visibility.Collapsed;
            gridWin.Visibility = Visibility.Collapsed;
            gridGolovolomInput.Visibility = Visibility.Collapsed;
            gridGlmkuHelp.Visibility = Visibility.Collapsed;          
            gridTF.Visibility = Visibility.Collapsed;          
            gridSetColor.Visibility = Visibility.Collapsed;
            gridTFCategory.Visibility = Visibility.Collapsed;
            gridTFWinAndAswer.Visibility = Visibility.Collapsed;
            gridTFErorrAnswer.Visibility = Visibility.Collapsed;
            gridTotalTFLose.Visibility = Visibility.Collapsed; 
            gridTotalTFWin.Visibility = Visibility.Collapsed;
            gridAbout.Visibility = Visibility.Collapsed;
        }

        
        public void TotalColorChangeButtons()
        {
            /*if (array[1].AntiRepeat == "false") { btn1z_Copy1.Background = new SolidColorBrush(Colors.Blue); }
            if (array[2].AntiRepeat == "false") { btn1z_Copy2.Background = new SolidColorBrush(Colors.Blue); }
            if (array[3].AntiRepeat == "false") { btn1z_Copy3.Background = new SolidColorBrush(Colors.Blue); }
            if (array[4].AntiRepeat == "false") { btn1z_Copy4.Background = new SolidColorBrush(Colors.Blue); }*/
            //for (int i = 0; i < q; i++)
            //{
                if (array[1].AntiRepeat == "false") { btn1z_Copy1.Background = new SolidColorBrush(Colors.Blue); } 
                if (array[2].AntiRepeat == "false") { btn1z_Copy2.Background = new SolidColorBrush(Colors.Blue); }
                if (array[3].AntiRepeat == "false") { btn1z_Copy3.Background = new SolidColorBrush(Colors.Blue); }
                if (array[4].AntiRepeat == "false") { btn1z_Copy4.Background = new SolidColorBrush(Colors.Blue); }
                if (array[5].AntiRepeat == "false") { btn1z_Copy5.Background = new SolidColorBrush(Colors.Blue); }
                if (array[6].AntiRepeat == "false") { btn1z_Copy6.Background = new SolidColorBrush(Colors.Blue); }
                if (array[7].AntiRepeat == "false") { btn1z_Copy7.Background = new SolidColorBrush(Colors.Blue); }
                if (array[8].AntiRepeat == "false") { btn1z_Copy8.Background = new SolidColorBrush(Colors.Blue); }
                if (array[9].AntiRepeat == "false") { btn1z_Copy9.Background = new SolidColorBrush(Colors.Blue); }
                if (array[10].AntiRepeat == "false") { btn1z_Copy10.Background = new SolidColorBrush(Colors.Blue); }
                if (array[11].AntiRepeat == "false") { btn1z_Copy11.Background = new SolidColorBrush(Colors.Blue); }
                if (array[12].AntiRepeat == "false") { btn1z_Copy12.Background = new SolidColorBrush(Colors.Blue); }
                if (array[13].AntiRepeat == "false") { btn1z_Copy13.Background = new SolidColorBrush(Colors.Blue); }
                if (array[14].AntiRepeat == "false") { btn1z_Copy14.Background = new SolidColorBrush(Colors.Blue); }
                if (array[15].AntiRepeat == "false") { btn1z_Copy15.Background = new SolidColorBrush(Colors.Blue); }
                if (array[16].AntiRepeat == "false") { btn1z_Copy16.Background = new SolidColorBrush(Colors.Blue); }
                if (array[17].AntiRepeat == "false") { btn1z_Copy17.Background = new SolidColorBrush(Colors.Blue); }
                if (array[18].AntiRepeat == "false") { btn1z_Copy18.Background = new SolidColorBrush(Colors.Blue); }
                if (array[19].AntiRepeat == "false") { btn1z_Copy19.Background = new SolidColorBrush(Colors.Blue); }
                if (array[20].AntiRepeat == "false") { btn1z_Copy20.Background = new SolidColorBrush(Colors.Blue); }
                if (array[21].AntiRepeat == "false") { btn1z_Copy21.Background = new SolidColorBrush(Colors.Blue); }
                if (array[22].AntiRepeat == "false") { btn1z_Copy22.Background = new SolidColorBrush(Colors.Blue); }
                if (array[23].AntiRepeat == "false") { btn1z_Copy23.Background = new SolidColorBrush(Colors.Blue); }
                if (array[24].AntiRepeat == "false") { btn1z_Copy24.Background = new SolidColorBrush(Colors.Blue); }
                if (array[25].AntiRepeat == "false") { btn1z_Copy25.Background = new SolidColorBrush(Colors.Blue); }
                if (array[26].AntiRepeat == "false") { btn1z_Copy26.Background = new SolidColorBrush(Colors.Blue); }
                if (array[27].AntiRepeat == "false") { btn1z_Copy27.Background = new SolidColorBrush(Colors.Blue); }
                if (array[28].AntiRepeat == "false") { btn1z_Copy28.Background = new SolidColorBrush(Colors.Blue); }
                if (array[29].AntiRepeat == "false") { btn1z_Copy29.Background = new SolidColorBrush(Colors.Blue); }
                if (array[30].AntiRepeat == "false") { btn1z_Copy30.Background = new SolidColorBrush(Colors.Blue); }
                if (array[31].AntiRepeat == "false") { btn1z_Copy31.Background = new SolidColorBrush(Colors.Blue); }
                if (array[32].AntiRepeat == "false") { btn1z_Copy32.Background = new SolidColorBrush(Colors.Blue); }
                if (array[33].AntiRepeat == "false") { btn1z_Copy33.Background = new SolidColorBrush(Colors.Blue); }
                if (array[34].AntiRepeat == "false") { btn1z_Copy34.Background = new SolidColorBrush(Colors.Blue); }
                if (array[35].AntiRepeat == "false") { btn1z_Copy35.Background = new SolidColorBrush(Colors.Blue); }
                if (array[36].AntiRepeat == "false") { btn1z_Copy36.Background = new SolidColorBrush(Colors.Blue); }
                if (array[37].AntiRepeat == "false") { btn1z_Copy37.Background = new SolidColorBrush(Colors.Blue); }
                if (array[38].AntiRepeat == "false") { btn1z_Copy38.Background = new SolidColorBrush(Colors.Blue); }
                if (array[39].AntiRepeat == "false") { btn1z_Copy39.Background = new SolidColorBrush(Colors.Blue); }
                if (array[40].AntiRepeat == "false") { btn1z_Copy40.Background = new SolidColorBrush(Colors.Blue); }
                if (array[41].AntiRepeat == "false") { btn1z_Copy41.Background = new SolidColorBrush(Colors.Blue); }
                if (array[42].AntiRepeat == "false") { btn1z_Copy42.Background = new SolidColorBrush(Colors.Blue); }
                if (array[43].AntiRepeat == "false") { btn1z_Copy43.Background = new SolidColorBrush(Colors.Blue); }
                if (array[44].AntiRepeat == "false") { btn1z_Copy44.Background = new SolidColorBrush(Colors.Blue); }
                if (array[45].AntiRepeat == "false") { btn1z_Copy45.Background = new SolidColorBrush(Colors.Blue); }
                if (array[46].AntiRepeat == "false") { btn1z_Copy46.Background = new SolidColorBrush(Colors.Blue); }
                if (array[47].AntiRepeat == "false") { btn1z_Copy47.Background = new SolidColorBrush(Colors.Blue); }
                if (array[48].AntiRepeat == "false") { btn1z_Copy48.Background = new SolidColorBrush(Colors.Blue); }
                if (array[49].AntiRepeat == "false") { btn1z_Copy49.Background = new SolidColorBrush(Colors.Blue); }
                if (array[50].AntiRepeat == "false") { btn1z_Copy50.Background = new SolidColorBrush(Colors.Blue); }
                if (array[51].AntiRepeat == "false") { btn1z_Copy51.Background = new SolidColorBrush(Colors.Blue); }
                if (array[52].AntiRepeat == "false") { btn1z_Copy52.Background = new SolidColorBrush(Colors.Blue); }
                if (array[53].AntiRepeat == "false") { btn1z_Copy53.Background = new SolidColorBrush(Colors.Blue); }
                if (array[54].AntiRepeat == "false") { btn1z_Copy54.Background = new SolidColorBrush(Colors.Blue); }
                if (array[55].AntiRepeat == "false") { btn1z_Copy55.Background = new SolidColorBrush(Colors.Blue); }
                if (array[56].AntiRepeat == "false") { btn1z_Copy56.Background = new SolidColorBrush(Colors.Blue); }
                if (array[57].AntiRepeat == "false") { btn1z_Copy57.Background = new SolidColorBrush(Colors.Blue); }
                if (array[58].AntiRepeat == "false") { btn1z_Copy58.Background = new SolidColorBrush(Colors.Blue); }
                if (array[59].AntiRepeat == "false") { btn1z_Copy59.Background = new SolidColorBrush(Colors.Blue); }
                if (array[60].AntiRepeat == "false") { btn1z_Copy60.Background = new SolidColorBrush(Colors.Blue); }
                if (array[61].AntiRepeat == "false") { btn1z_Copy61.Background = new SolidColorBrush(Colors.Blue); }
                if (array[62].AntiRepeat == "false") { btn1z_Copy62.Background = new SolidColorBrush(Colors.Blue); }
                if (array[63].AntiRepeat == "false") { btn1z_Copy63.Background = new SolidColorBrush(Colors.Blue); }
                if (array[64].AntiRepeat == "false") { btn1z_Copy64.Background = new SolidColorBrush(Colors.Blue); }
                if (array[65].AntiRepeat == "false") { btn1z_Copy65.Background = new SolidColorBrush(Colors.Blue); }
                if (array[66].AntiRepeat == "false") { btn1z_Copy66.Background = new SolidColorBrush(Colors.Blue); }
                if (array[67].AntiRepeat == "false") { btn1z_Copy67.Background = new SolidColorBrush(Colors.Blue); }
                if (array[68].AntiRepeat == "false") { btn1z_Copy68.Background = new SolidColorBrush(Colors.Blue); }
                if (array[69].AntiRepeat == "false") { btn1z_Copy69.Background = new SolidColorBrush(Colors.Blue); }
                if (array[70].AntiRepeat == "false") { btn1z_Copy70.Background = new SolidColorBrush(Colors.Blue); }
                if (array[71].AntiRepeat == "false") { btn1z_Copy71.Background = new SolidColorBrush(Colors.Blue); }
                if (array[72].AntiRepeat == "false") { btn1z_Copy72.Background = new SolidColorBrush(Colors.Blue); }
                if (array[73].AntiRepeat == "false") { btn1z_Copy73.Background = new SolidColorBrush(Colors.Blue); }
                if (array[74].AntiRepeat == "false") { btn1z_Copy74.Background = new SolidColorBrush(Colors.Blue); }
                if (array[75].AntiRepeat == "false") { btn1z_Copy75.Background = new SolidColorBrush(Colors.Blue); }
                if (array[76].AntiRepeat == "false") { btn1z_Copy76.Background = new SolidColorBrush(Colors.Blue); }
                if (array[77].AntiRepeat == "false") { btn1z_Copy77.Background = new SolidColorBrush(Colors.Blue); }
                if (array[78].AntiRepeat == "false") { btn1z_Copy78.Background = new SolidColorBrush(Colors.Blue); }
                if (array[79].AntiRepeat == "false") { btn1z_Copy79.Background = new SolidColorBrush(Colors.Blue); }
                if (array[80].AntiRepeat == "false") { btn1z_Copy80.Background = new SolidColorBrush(Colors.Blue); }
                if (array[81].AntiRepeat == "false") { btn1z_Copy81.Background = new SolidColorBrush(Colors.Blue); }
                if (array[82].AntiRepeat == "false") { btn1z_Copy82.Background = new SolidColorBrush(Colors.Blue); }
                if (array[83].AntiRepeat == "false") { btn1z_Copy83.Background = new SolidColorBrush(Colors.Blue); }
                if (array[84].AntiRepeat == "false") { btn1z_Copy84.Background = new SolidColorBrush(Colors.Blue); }
                if (array[85].AntiRepeat == "false") { btn1z_Copy85.Background = new SolidColorBrush(Colors.Blue); }
                if (array[86].AntiRepeat == "false") { btn1z_Copy86.Background = new SolidColorBrush(Colors.Blue); }
                if (array[87].AntiRepeat == "false") { btn1z_Copy87.Background = new SolidColorBrush(Colors.Blue); }
                if (array[88].AntiRepeat == "false") { btn1z_Copy88.Background = new SolidColorBrush(Colors.Blue); }
                if (array[89].AntiRepeat == "false") { btn1z_Copy89.Background = new SolidColorBrush(Colors.Blue); }
                if (array[90].AntiRepeat == "false") { btn1z_Copy90.Background = new SolidColorBrush(Colors.Blue); }
                if (array[91].AntiRepeat == "false") { btn1z_Copy91.Background = new SolidColorBrush(Colors.Blue); }
                if (array[92].AntiRepeat == "false") { btn1z_Copy92.Background = new SolidColorBrush(Colors.Blue); }
                if (array[93].AntiRepeat == "false") { btn1z_Copy93.Background = new SolidColorBrush(Colors.Blue); }
                if (array[94].AntiRepeat == "false") { btn1z_Copy94.Background = new SolidColorBrush(Colors.Blue); }
                if (array[95].AntiRepeat == "false") { btn1z_Copy95.Background = new SolidColorBrush(Colors.Blue); }
                if (array[96].AntiRepeat == "false") { btn1z_Copy96.Background = new SolidColorBrush(Colors.Blue); }
                if (array[97].AntiRepeat == "false") { btn1z_Copy97.Background = new SolidColorBrush(Colors.Blue); }
                if (array[98].AntiRepeat == "false") { btn1z_Copy98.Background = new SolidColorBrush(Colors.Blue); }
                if (array[99].AntiRepeat == "false") { btn1z_Copy99.Background = new SolidColorBrush(Colors.Blue); }
                if (array[100].AntiRepeat == "false") { btn1z_Copy100.Background = new SolidColorBrush(Colors.Blue); }
                if (array[101].AntiRepeat == "false") { btn1z_Copy101.Background = new SolidColorBrush(Colors.Blue); }
                if (array[102].AntiRepeat == "false") { btn1z_Copy102.Background = new SolidColorBrush(Colors.Blue); }
                if (array[103].AntiRepeat == "false") { btn1z_Copy103.Background = new SolidColorBrush(Colors.Blue); }
                if (array[104].AntiRepeat == "false") { btn1z_Copy104.Background = new SolidColorBrush(Colors.Blue); }
                if (array[105].AntiRepeat == "false") { btn1z_Copy105.Background = new SolidColorBrush(Colors.Blue); }
                if (array[106].AntiRepeat == "false") { btn1z_Copy106.Background = new SolidColorBrush(Colors.Blue); }
                if (array[107].AntiRepeat == "false") { btn1z_Copy107.Background = new SolidColorBrush(Colors.Blue); }
                if (array[108].AntiRepeat == "false") { btn1z_Copy108.Background = new SolidColorBrush(Colors.Blue); }
                if (array[109].AntiRepeat == "false") { btn1z_Copy109.Background = new SolidColorBrush(Colors.Blue); }
                if (array[110].AntiRepeat == "false") { btn1z_Copy110.Background = new SolidColorBrush(Colors.Blue); }
                if (array[111].AntiRepeat == "false") { btn1z_Copy111.Background = new SolidColorBrush(Colors.Blue); }
                if (array[112].AntiRepeat == "false") { btn1z_Copy112.Background = new SolidColorBrush(Colors.Blue); }
                if (array[113].AntiRepeat == "false") { btn1z_Copy113.Background = new SolidColorBrush(Colors.Blue); }
                if (array[114].AntiRepeat == "false") { btn1z_Copy114.Background = new SolidColorBrush(Colors.Blue); }
                if (array[115].AntiRepeat == "false") { btn1z_Copy115.Background = new SolidColorBrush(Colors.Blue); }
                if (array[116].AntiRepeat == "false") { btn1z_Copy116.Background = new SolidColorBrush(Colors.Blue); }
                if (array[117].AntiRepeat == "false") { btn1z_Copy117.Background = new SolidColorBrush(Colors.Blue); }
                if (array[118].AntiRepeat == "false") { btn1z_Copy118.Background = new SolidColorBrush(Colors.Blue); }
                if (array[119].AntiRepeat == "false") { btn1z_Copy119.Background = new SolidColorBrush(Colors.Blue); }
                if (array[120].AntiRepeat == "false") { btn1z_Copy120.Background = new SolidColorBrush(Colors.Blue); }

        }
        private void btnZall_Click(object sender, RoutedEventArgs e)
        {
            gridZ.Visibility = Visibility.Collapsed;
            gridZall.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Nom = 1;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Nom = 2;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy3_Click(object sender, RoutedEventArgs e)
        {
            Nom = 3;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {

            base.OnBackKeyPress(e);
                MessageBoxResult result = MessageBox.Show("Выйти из приложения?",
                                 "Exit?", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    ClearBackEntries();
                }
                else
                {
                    e.Cancel = true;
                }          
        }

        private void btn1z_Copy4_Click(object sender, RoutedEventArgs e)
        {
            Nom = 4;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy5_Click(object sender, RoutedEventArgs e)
        {
            Nom = 5;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy6_Click(object sender, RoutedEventArgs e)
        {
            Nom = 6;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy7_Click(object sender, RoutedEventArgs e)
        {
            Nom = 7;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy8_Click(object sender, RoutedEventArgs e)
        {
            Nom = 8;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy9_Click(object sender, RoutedEventArgs e)
        {
            Nom = 9;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy10_Click(object sender, RoutedEventArgs e)
        {
            Nom = 10;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy11_Click(object sender, RoutedEventArgs e)
        {
            Nom = 11;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy12_Click(object sender, RoutedEventArgs e)
        {
            Nom = 12;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy13_Click(object sender, RoutedEventArgs e)
        {
            Nom = 13;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy14_Click(object sender, RoutedEventArgs e)
        {
            Nom = 14;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy15_Click(object sender, RoutedEventArgs e)
        {
            Nom = 15;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy16_Click(object sender, RoutedEventArgs e)
        {
            Nom = 16;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy17_Click(object sender, RoutedEventArgs e)
        {
            Nom = 17;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy18_Click(object sender, RoutedEventArgs e)
        {
            Nom = 18;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy19_Click(object sender, RoutedEventArgs e)
        {
            Nom = 19;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy20_Click(object sender, RoutedEventArgs e)
        {
            Nom = 20;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy21_Click(object sender, RoutedEventArgs e)
        {
            Nom = 21;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy22_Click(object sender, RoutedEventArgs e)
        {
            Nom = 22;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy23_Click(object sender, RoutedEventArgs e)
        {
            Nom = 23;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy24_Click(object sender, RoutedEventArgs e)
        {
            Nom = 24;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy25_Click(object sender, RoutedEventArgs e)
        {
            Nom = 25;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy26_Click(object sender, RoutedEventArgs e)
        {
            Nom = 26;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy27_Click(object sender, RoutedEventArgs e)
        {
            Nom = 27;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy28_Click(object sender, RoutedEventArgs e)
        {
            Nom = 28;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy29_Click(object sender, RoutedEventArgs e)
        {
            Nom = 29;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy30_Click(object sender, RoutedEventArgs e)
        {
            Nom = 30;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy31_Click(object sender, RoutedEventArgs e)
        {
            Nom = 31;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy32_Click(object sender, RoutedEventArgs e)
        {
            Nom = 32;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy33_Click(object sender, RoutedEventArgs e)
        {
            Nom = 33;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy34_Click(object sender, RoutedEventArgs e)
        {
            Nom = 34;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy35_Click(object sender, RoutedEventArgs e)
        {
            Nom = 35;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy36_Click(object sender, RoutedEventArgs e)
        {
            Nom = 36;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy37_Click(object sender, RoutedEventArgs e)
        {
            Nom = 37;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy38_Click(object sender, RoutedEventArgs e)
        {
            Nom = 38;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy39_Click(object sender, RoutedEventArgs e)
        {
            Nom = 39;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy40_Click(object sender, RoutedEventArgs e)
        {
            Nom = 40;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy41_Click(object sender, RoutedEventArgs e)
        {
            Nom = 41;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy42_Click(object sender, RoutedEventArgs e)
        {
            Nom = 42;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy43_Click(object sender, RoutedEventArgs e)
        {
            Nom = 43;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy44_Click(object sender, RoutedEventArgs e)
        {
            Nom = 44;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy45_Click(object sender, RoutedEventArgs e)
        {
            Nom = 45;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy46_Click(object sender, RoutedEventArgs e)
        {
            Nom = 46;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy47_Click(object sender, RoutedEventArgs e)
        {
            Nom = 47;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy48_Click(object sender, RoutedEventArgs e)
        {
            Nom = 48;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy49_Click(object sender, RoutedEventArgs e)
        {
            Nom = 49;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy50_Click(object sender, RoutedEventArgs e)
        {
            Nom = 50;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy51_Click(object sender, RoutedEventArgs e)
        {
            Nom = 51;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy52_Click(object sender, RoutedEventArgs e)
        {
            Nom = 52;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy53_Click(object sender, RoutedEventArgs e)
        {
            Nom = 53;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy54_Click(object sender, RoutedEventArgs e)
        {
            Nom = 54;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy55_Click(object sender, RoutedEventArgs e)
        {
            Nom = 55;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy56_Click(object sender, RoutedEventArgs e)
        {
            Nom = 56;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy57_Click(object sender, RoutedEventArgs e)
        {
            Nom = 57;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy58_Click(object sender, RoutedEventArgs e)
        {
            Nom = 58;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy59_Click(object sender, RoutedEventArgs e)
        {
            Nom = 59;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy60_Click(object sender, RoutedEventArgs e)
        {
            Nom = 60;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy61_Click(object sender, RoutedEventArgs e)
        {
            Nom = 61;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy62_Click(object sender, RoutedEventArgs e)
        {
            Nom = 62;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy63_Click(object sender, RoutedEventArgs e)
        {
            Nom = 63;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy64_Click(object sender, RoutedEventArgs e)
        {
            Nom = 64;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy65_Click(object sender, RoutedEventArgs e)
        {
            Nom = 65;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy66_Click(object sender, RoutedEventArgs e)
        {
            Nom = 66;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy67_Click(object sender, RoutedEventArgs e)
        {
            Nom = 67;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy68_Click(object sender, RoutedEventArgs e)
        {
            Nom = 68;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy69_Click(object sender, RoutedEventArgs e)
        {
            Nom = 69;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy70_Click(object sender, RoutedEventArgs e)
        {
            Nom = 70;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy71_Click(object sender, RoutedEventArgs e)
        {
            Nom = 71;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy72_Click(object sender, RoutedEventArgs e)
        {
            Nom = 72;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy73_Click(object sender, RoutedEventArgs e)
        {
            Nom = 73;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy74_Click(object sender, RoutedEventArgs e)
        {
            Nom = 74;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy75_Click(object sender, RoutedEventArgs e)
        {
            Nom = 75;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy76_Click(object sender, RoutedEventArgs e)
        {
            Nom = 76;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy77_Click(object sender, RoutedEventArgs e)
        {
            Nom = 77;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy78_Click(object sender, RoutedEventArgs e)
        {
            Nom = 78;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy79_Click(object sender, RoutedEventArgs e)
        {
            Nom = 79;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy80_Click(object sender, RoutedEventArgs e)
        {
            Nom = 80;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy81_Click(object sender, RoutedEventArgs e)
        {
            Nom = 81;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy82_Click(object sender, RoutedEventArgs e)
        {
            Nom = 82;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy83_Click(object sender, RoutedEventArgs e)
        {
            Nom = 83;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy84_Click(object sender, RoutedEventArgs e)
        {
            Nom = 84;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy85_Click(object sender, RoutedEventArgs e)
        {
            Nom = 85;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy86_Click(object sender, RoutedEventArgs e)
        {
            Nom = 86;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy87_Click(object sender, RoutedEventArgs e)
        {
            Nom = 87;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy88_Click(object sender, RoutedEventArgs e)
        {
            Nom = 88;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy89_Click(object sender, RoutedEventArgs e)
        {
            Nom = 89;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy90_Click(object sender, RoutedEventArgs e)
        {
            Nom = 90;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy91_Click(object sender, RoutedEventArgs e)
        {
            Nom = 91;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy92_Click(object sender, RoutedEventArgs e)
        {
            Nom = 92;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy93_Click(object sender, RoutedEventArgs e)
        {
            Nom = 93;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy94_Click(object sender, RoutedEventArgs e)
        {
            Nom = 94;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy95_Click(object sender, RoutedEventArgs e)
        {
            Nom = 95;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy96_Click(object sender, RoutedEventArgs e)
        {
            Nom = 96;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy97_Click(object sender, RoutedEventArgs e)
        {
            Nom = 97;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy98_Click(object sender, RoutedEventArgs e)
        {
            Nom = 98;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy99_Click(object sender, RoutedEventArgs e)
        {
            Nom = 99;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy100_Click(object sender, RoutedEventArgs e)
        {
            Nom = 100;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy101_Click(object sender, RoutedEventArgs e)
        {
            Nom = 101;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy102_Click(object sender, RoutedEventArgs e)
        {
            Nom = 102;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy103_Click(object sender, RoutedEventArgs e)
        {
            Nom = 103;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy104_Click(object sender, RoutedEventArgs e)
        {
            Nom = 104;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy105_Click(object sender, RoutedEventArgs e)
        {
            Nom = 105;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy106_Click(object sender, RoutedEventArgs e)
        {
            Nom = 106;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy107_Click(object sender, RoutedEventArgs e)
        {
            Nom = 107;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy108_Click(object sender, RoutedEventArgs e)
        {
            Nom = 108;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy109_Click(object sender, RoutedEventArgs e)
        {
            Nom = 109;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy110_Click(object sender, RoutedEventArgs e)
        {
            Nom = 110;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy111_Click(object sender, RoutedEventArgs e)
        {
            Nom = 111;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy112_Click(object sender, RoutedEventArgs e)
        {
            Nom = 112;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy113_Click(object sender, RoutedEventArgs e)
        {
            Nom = 113;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy114_Click(object sender, RoutedEventArgs e)
        {
            Nom = 114;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy115_Click(object sender, RoutedEventArgs e)
        {
            Nom = 115;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy116_Click(object sender, RoutedEventArgs e)
        {
            Nom = 116;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy117_Click(object sender, RoutedEventArgs e)
        {
            Nom = 117;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy118_Click(object sender, RoutedEventArgs e)
        {
            Nom = 118;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy119_Click(object sender, RoutedEventArgs e)
        {
            Nom = 119;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }

        private void btn1z_Copy120_Click(object sender, RoutedEventArgs e)
        {
            Nom = 120;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = ""; }
            gridZall.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }


        private void btGoBackToMenu_Click(object sender, RoutedEventArgs e)
        {  
            gridZ.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
        }

        private void btBeginTips_Click(object sender, RoutedEventArgs e)
        {
            string encryption = "";
            string stars = "";
          
            if (array[Nom].AntiRepeat == "true")
            {
                if (help > 0)
                {

                    if (tipText.Text == encryption)
                    {
                        var result = MessageBox.Show("Показать подсказку? За подсказку вы отдадите звездочку.",
                             "Помощь", MessageBoxButton.OKCancel);
                        if (result == MessageBoxResult.OK)
                        {
                            string tips = array[Nom].Answer;
                            string firstUnit = new string(tips.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x[0]).ToArray());
                            int qtUnit = 0;
                            qtUnit = tips.Length - 1;                          
                            for (int i = 0; i < qtUnit; i++)
                            {
                                stars += "∎";

                                   
                            }
                            encryption = firstUnit + stars;
                            tipText.Text = encryption;
                            help += -1;
                            tbok.Text = Convert.ToString(help);// tbok.Text = "Ответы:" + Convert.ToString(help);

                            IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                            if (!settings.Contains("emailFlag1"))//------------------------  Сохранение int help;
                            {
                                settings.Add("emailFlag1", help);
                            }
                            else
                            {
                                settings["emailFlag1"] = help;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Вы уже взяли подсказку!",
                            "Выполнено", MessageBoxButton.OK);
                    }
                }
                else
                {
                    MessageBox.Show("Для подсказки требуется звездочка!",
                    "Жаль", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Вы уже ответили!",
                    "Выполнено", MessageBoxButton.OK);

            }


        }

       

        private void btBackandNext_Click(object sender, RoutedEventArgs e)
        { 
            Nom++;
            if (Nom > 120) Nom = 120;
            tbnom.Text = "Загадка №" + Convert.ToString(Nom);      // Так же происходит замена номера загадки в textblock
            tbtext.Text = array[Nom].Question;
            if (array[Nom].AntiRepeat == "false") { tipText.Text = array[Nom].Answer; } else { tipText.Text = "";  }
            gridWin.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;

        }

        private void btnFalseandback_Click(object sender, RoutedEventArgs e)
        {
            gridFalse.Visibility = Visibility.Collapsed;
            gridZ.Visibility = Visibility.Visible;
        }


        private void btGreen1_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 0;
            ColorAr[0].greenColor = 255;
            ColorAr[0].blueColor = 0;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btBlue1_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 0;
            ColorAr[0].greenColor = 0;
            ColorAr[0].blueColor = 220;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btMauve_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 170;
            ColorAr[0].greenColor = 50;
            ColorAr[0].blueColor = 0;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btCrimson_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 255;
            ColorAr[0].greenColor = 0;
            ColorAr[0].blueColor = 0;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btPink1_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 250;
            ColorAr[0].greenColor = 0;
            ColorAr[0].blueColor = 173;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btYelow1_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 255;
            ColorAr[0].greenColor = 255;
            ColorAr[0].blueColor = 0;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btBlue2_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 97;
            ColorAr[0].greenColor = 89;
            ColorAr[0].blueColor = 9;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btGreen2_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 0;
            ColorAr[0].greenColor = 100;
            ColorAr[0].blueColor = 0;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btOrage_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 125;
            ColorAr[0].greenColor = 0;
            ColorAr[0].blueColor = 125;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btBrown_Click(object sender
             , RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 140;
            ColorAr[0].greenColor = 140;
            ColorAr[0].blueColor = 140;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void btWhite_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 0;
            ColorAr[0].greenColor = 255;
            ColorAr[0].blueColor = 255;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;         
            SaveColorConfig();
        }

        private void btViolet_Click(object sender, RoutedEventArgs e)
        {
            ColorAr[0].alfaColor = 255;
            ColorAr[0].redColor = 80;
            ColorAr[0].greenColor = 130;
            ColorAr[0].blueColor = 150;
            SetBackround();
            gridSetColor.Visibility = Visibility.Collapsed;
            gridGeneralMenu.Visibility = Visibility.Visible;
            SaveColorConfig();
        }

        private void SaveColorConfig()
        {

            fullstringToByte = "";
           
            string lineColor = ColorAr[0].alfaColor + "|" + ColorAr[0].redColor + "|" + ColorAr[0].greenColor + "|" + ColorAr[0].blueColor + "|" + ColorAr[0].foregroundTextColor;
             
                FileStream fs = new FileStream("TextFiles/ColorConfig.txt", FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.WriteLine(lineColor);
            sw.Close();

        }

        private void btGoToAllZag_Click(object sender, RoutedEventArgs e)
        {
            gridGeneralMenu.Visibility = Visibility.Collapsed;
            gridZall.Visibility = Visibility.Visible;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            gridGeneralMenu.Visibility = Visibility.Collapsed;
            gridSetColor.Visibility = Visibility.Visible;
        }

        

        private void TextInputInTextBlock(int categoryIndex)
        {
            
                    if (nRr < 15)// если nRr>14 все вопросы кончились нужно переходить к гриду с подводом итогов
                    {
                        tbTFTextQuest.Text = LiteratyraArr[generatedNumbers[nRr]].QuestionTFLiteratyra;
                        tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);

                    }
                  

           
        }

        

        private void RandomNumbers()
        {
            Random randNodes = new Random();

            //int[] generatedNumbers = new int[14];
            // Generate six random integers from 0 to int.MaxValue.
            int i = 0;
            while (i < 15)
            {
                generatedNumbers[i] = randNodes.Next(15);
                for (int j = 0; j < i; j++)
                    if (generatedNumbers[j] == generatedNumbers[i])
                    {
                        i--;
                        break;
                    }
                i++;
            }
        }


        private void btGoToAllTF_Click(object sender, RoutedEventArgs e)
        {
            ReadTFScope();
            WriteAlltblockInTF();

            gridTFCategory.Visibility = Visibility.Visible;
            gridGeneralMenu.Visibility = Visibility.Collapsed;
            
        }

        private void WriteAlltblockInTF()
        {

         tbPointLitra.Text = ScopeAr[0].Literatyra.ToString() + "/15";
         tbPointAuto.Text = ScopeAr[0].Automabili.ToString() + "/15";
         tbPointHistory.Text = ScopeAr[0].History.ToString() + "/15";
         tbPointTehnology.Text = ScopeAr[0].ITtehnology.ToString() + "/15";
         tbPointEarth.Text = ScopeAr[0].EarthPlanet.ToString() + "/15";
         tbPointSports.Text = ScopeAr[0].Sports.ToString() + "/15";
            
        }

        private void ReadTFScope()
        {
            FileStream fs3 = new FileStream("TextFiles/TextTF_Scope.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr3 = new StreamReader(fs3, Encoding.Unicode);
            //for (int i = 0; i < 1; i++)
            //{
            ScopeAr[0] = new TFScope(sr3.ReadLine().Split('|'));
             

            // }
            fs3.Close();
            sr3.Close();

        }

        private void btnTFTrue_Click(object sender, RoutedEventArgs e)
        {
            answerUserTF = "true";
            CheckingTF();

            nRr++;
            schetchikTF++;
            //if (nRr > 14) nRr = 14;
            TextInputInTextBlock(0);
        }

        private void btnTFFalse_Click(object sender, RoutedEventArgs e)
        {
            answerUserTF = "false";
            CheckingTF();

            nRr++;
            schetchikTF++;
            //if (nRr > 14) nRr = 14;
            TextInputInTextBlock(0);

        }

        private void CheckingTF()
        {
            if (LiteratyraArr[generatedNumbers[nRr]].CheckValidityTFLiteratyra == "true")
            {
                if (LiteratyraArr[generatedNumbers[nRr]].CheckValidityTFLiteratyra == answerUserTF)
                {
                    
                    userTFtrueAn += 1;
                    tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);
                    
                    gridTFWinAndAswer.Visibility = Visibility.Visible;
                    gridTF.Visibility = Visibility.Collapsed;
                }
                else
                {
                    
                    tbInputTrueAns1.Text = "Утверждение было правдивым.";
                    liveUser -= 1;
                    
                    tbTFErrorAppot.Text = "Жизней: " + Convert.ToString(liveUser);

                    gridTFErorrAnswer.Visibility = Visibility.Visible;
                    gridTF.Visibility = Visibility.Collapsed;
                    //tbInputTrueAns1.Text = "";

                }
            }
            

            if (LiteratyraArr[generatedNumbers[nRr]].CheckValidityTFLiteratyra == "false")
            {
                if (LiteratyraArr[generatedNumbers[nRr]].CheckValidityTFLiteratyra == answerUserTF)
                {
                    
                    tbTans.Text = "Правильный ответ:";
                    tbInputTrueAns.Text = LiteratyraArr[generatedNumbers[nRr]].AnswerTFLiteratyra.ToString();
                    userTFtrueAn += 1;
                    tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);
                    
                    gridTFWinAndAswer.Visibility = Visibility.Visible;
                    gridTF.Visibility = Visibility.Collapsed;
                    //tbTans.Text = "";
                    //tbInputTrueAns.Text = "";
                    
                }
                else
                {
                    
                    tb2Tans.Text = "Правильный ответ:";
                    tbInputTrueAns1.Text = LiteratyraArr[generatedNumbers[nRr]].AnswerTFLiteratyra.ToString();
                    liveUser -= 1;

                    tbTFErrorAppot.Text = "Жизней: " + Convert.ToString(liveUser);
                    
                    gridTFErorrAnswer.Visibility = Visibility.Visible;
                    gridTF.Visibility = Visibility.Collapsed;
                   // tb2Tans.Text = "";
                   // tbInputTrueAns1.Text = "";

                }
            }
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            tbTans.Text = "";
            tbInputTrueAns.Text = "";
            if (nRr<=14)
            {
                gridTF.Visibility = Visibility.Visible;
                gridTFWinAndAswer.Visibility = Visibility.Collapsed;
                
            }
            if(nRr > 14)
            {
                ChangeScopeTF();

                WriteScopeTF();

                tbEnterKolichestvoStar.Text = "Вы получили " + userTFtrueAn + " звездочек за каждый правильный ответ.";
                gridTotalTFWin.Visibility = Visibility.Visible;
                gridTFWinAndAswer.Visibility = Visibility.Collapsed;
                //switch в зависимости от номера категории меняем значение массива и производим запись в текстовый файл
                //загрузка осуществляеться из текстового файла при загрузке приложения или входа в грид категории
                
            }
        }

        private void ChangeScopeTF()
        {
            switch (CategoryNumber)
            {

                case 0:
                    ScopeAr[0].Literatyra = userTFtrueAn;
                    tbPointLitra.Text = ScopeAr[0].Literatyra.ToString() + "/15";                    
                    break;

                case 1:
                    ScopeAr[0].Automabili = userTFtrueAn;
                    tbPointAuto.Text = ScopeAr[0].Automabili.ToString() + "/15";
                    
                    break;
                case 2:

                    ScopeAr[0].History = userTFtrueAn;
                    tbPointHistory.Text = ScopeAr[0].History.ToString() + "/15";
                    break;
                case 3:

                    ScopeAr[0].ITtehnology = userTFtrueAn;
                    tbPointTehnology.Text = ScopeAr[0].ITtehnology.ToString() + "/15";
                    break;
                case 4:

                    ScopeAr[0].EarthPlanet = userTFtrueAn;
                    tbPointEarth.Text = ScopeAr[0].EarthPlanet.ToString() + "/15";
                    break;
                case 5:

                    ScopeAr[0].Sports = userTFtrueAn;
                    tbPointSports.Text = ScopeAr[0].Sports.ToString() + "/15";
                    break;

                default:
                    MessageBox.Show("ohh My  Goood");
                    break;


            }



           
        }

        private void WriteScopeTF()
        {
            fullstringToByte = "";
            
            string lineColor = ScopeAr[0].Literatyra + "|" + ScopeAr[0].Automabili + "|" + ScopeAr[0].History + "|" + ScopeAr[0].ITtehnology + "|" + ScopeAr[0].EarthPlanet + "|" + ScopeAr[0].Sports;
            

            FileStream fs = new FileStream("TextFiles/TextTF_Scope.txt", FileMode.Open, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.WriteLine(lineColor);
            sw.Close();

        }

        

        

        

        

        private void Button_Click_9(object sender, RoutedEventArgs e)//кнопка переиграть категорию
        {


            nRr = 0;
            schetchikTF = 1;
            userTFtrueAn = 0;
            liveUser = 3;
            tbTFErrorAppot.Text = "Жизней: 3";
            tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);



            tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);

            ChangeScopeTF();

            WriteScopeTF();
            
            ReadTextFileTFCatecory();
            RandomNumbers();
            TextInputInTextBlock(0);
            
            gridTF.Visibility = Visibility.Visible;
            gridTotalTFLose.Visibility = Visibility.Collapsed;
        }

        private void ReadTextFileTFCatecory()
        {
            switch (CategoryNumber)
            {
                case 0:
                    FileStream fs1 = new FileStream("TextFiles/TextTFLiteratyra.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr1 = new StreamReader(fs1, Encoding.Unicode);
            for (int i = 0; i < 15; i++)
            {
                LiteratyraArr[i] = new TFLiteratyra(sr1.ReadLine().Split('|'));
            }
            fs1.Close();
            sr1.Close();
                    break;
                case 1:
                    FileStream fs2 = new FileStream("TextFiles/TextAuto.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr2 = new StreamReader(fs2, Encoding.Unicode);
            for (int i = 0; i < 15; i++)
            {
                LiteratyraArr[i] = new TFLiteratyra(sr2.ReadLine().Split('|'));
            }
            fs2.Close();
            sr2.Close();
                    break;
                case 2:
                    FileStream fs3 = new FileStream("TextFiles/TextHistory.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr3 = new StreamReader(fs3, Encoding.Unicode);
            for (int i = 0; i < 15; i++)
            {
                LiteratyraArr[i] = new TFLiteratyra(sr3.ReadLine().Split('|'));
            }
            fs3.Close();
            sr3.Close();
                    break;

                case 3:
                    FileStream fs4 = new FileStream("TextFiles/TextTehnologyIT.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr4 = new StreamReader(fs4, Encoding.Unicode);
            for (int i = 0; i < 15; i++)
            {
                LiteratyraArr[i] = new TFLiteratyra(sr4.ReadLine().Split('|'));
            }
            fs4.Close();
            sr4.Close();
                    break;
                case 4:
                    FileStream fs5 = new FileStream("TextFiles/TextTF_Georaphi.txt", FileMode.Open, FileAccess.ReadWrite);
            StreamReader sr5 = new StreamReader(fs5, Encoding.Unicode);
            for (int i = 0; i < 15; i++)
            {
                LiteratyraArr[i] = new TFLiteratyra(sr5.ReadLine().Split('|'));
            }
            fs5.Close();
            sr5.Close();
                    break;
                case 5:
                    FileStream fs6 = new FileStream("TextFiles/TextTF_Sport.txt", FileMode.Open, FileAccess.ReadWrite);
                    StreamReader sr6 = new StreamReader(fs6, Encoding.Unicode);
                    for (int i = 0; i < 15; i++)
                    {
                        LiteratyraArr[i] = new TFLiteratyra(sr6.ReadLine().Split('|'));
                    }
                    fs6.Close();
                    sr6.Close();
                    break;

            }

        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            ChangeScopeTF();

            WriteScopeTF();
            gridTotalTFLose.Visibility = Visibility.Collapsed;
            gridTFCategory.Visibility = Visibility.Visible;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {

            gridTotalTFWin.Visibility = Visibility.Collapsed;
            gridTFCategory.Visibility = Visibility.Visible;
        }

        private void btnLiteratyra_Click(object sender, RoutedEventArgs e)
        {
            CategoryNumber = 0;
            nRr = 0;
            schetchikTF = 1;
            userTFtrueAn = 0;
            liveUser = 3;
            tbTFErrorAppot.Text = "Жизней: 3";
            tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);
            tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);



            //ReadTextFileTFLiteratyra();
            ReadTextFileTFCatecory();
            RandomNumbers();
            TextInputInTextBlock(0);


            gridTFCategory.Visibility = Visibility.Collapsed;
            gridTF.Visibility = Visibility.Visible;
        }

        private void btnAutoCategory_Click(object sender, RoutedEventArgs e)
        {
            CategoryNumber = 1;
            nRr = 0;
            schetchikTF = 1;
            userTFtrueAn = 0;
            liveUser = 3;
            tbTFErrorAppot.Text = "Жизней: 3";
            tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);
            tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);



            //ReadTextFileTFAuto();
            ReadTextFileTFCatecory();

            RandomNumbers();
            TextInputInTextBlock(1);


            gridTFCategory.Visibility = Visibility.Collapsed;
            gridTF.Visibility = Visibility.Visible;
        }

        private void btnHistory_Click(object sender, RoutedEventArgs e)
        {
            CategoryNumber = 2;
            nRr = 0;
            schetchikTF = 1;
            userTFtrueAn = 0;
            liveUser = 3;
            tbTFErrorAppot.Text = "Жизней: 3";
            tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);
            tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);



            ReadTextFileTFCatecory();

            RandomNumbers();
            TextInputInTextBlock(1);


            gridTFCategory.Visibility = Visibility.Collapsed;
            gridTF.Visibility = Visibility.Visible;



        }

        private void TFbtnGrSad_Click(object sender, RoutedEventArgs e)
        {
            tb2Tans.Text = "";
            tbInputTrueAns1.Text = "";


            if (liveUser == 0)
            {
                gridTotalTFLose.Visibility = Visibility.Visible;
                gridTFErorrAnswer.Visibility = Visibility.Collapsed;

            }
            if(liveUser>0)
            {
                gridTF.Visibility = Visibility.Visible;
                gridTFErorrAnswer.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_IT_Tehnolog_Click(object sender, RoutedEventArgs e)//3 - ЭВМ
        {
            CategoryNumber = 3;
            nRr = 0;
            schetchikTF = 1;
            userTFtrueAn = 0;
            liveUser = 3;
            tbTFErrorAppot.Text = "Жизней: 3";
            tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);
            tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);



            ReadTextFileTFCatecory();

            RandomNumbers();
            TextInputInTextBlock(1);


            gridTFCategory.Visibility = Visibility.Collapsed;
            gridTF.Visibility = Visibility.Visible;

        }

        private void btn_Earth_Click(object sender, RoutedEventArgs e)
        {
            CategoryNumber = 4;
            nRr = 0;
            schetchikTF = 1;
            userTFtrueAn = 0;
            liveUser = 3;
            tbTFErrorAppot.Text = "Жизней: 3";
            tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);
            tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);



            ReadTextFileTFCatecory();

            RandomNumbers();
            TextInputInTextBlock(1);


            gridTFCategory.Visibility = Visibility.Collapsed;
            gridTF.Visibility = Visibility.Visible;
        }

        private void btn_Sport_Click(object sender, RoutedEventArgs e)
        {
            CategoryNumber = 5;
            nRr = 0;
            schetchikTF = 1;
            userTFtrueAn = 0;
            liveUser = 3;
            tbTFErrorAppot.Text = "Жизней: 3";
            tbNumberAlligetion.Text = "Утверждение: " + Convert.ToString(schetchikTF);
            tbTFnumberStar.Text = "Правильные ответы: " + Convert.ToString(userTFtrueAn);



            ReadTextFileTFCatecory();

            RandomNumbers();
            TextInputInTextBlock(1);


            gridTFCategory.Visibility = Visibility.Collapsed;
            gridTF.Visibility = Visibility.Visible;
        }

       

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            gridGeneralMenu.Visibility = Visibility.Visible;
            gridTFCategory.Visibility = Visibility.Collapsed;
        }

        private void btGoToAllGolov_Click(object sender, RoutedEventArgs e)
        {
            LoadTextListGolovolomli();
            gridGeneralMenu.Visibility = Visibility.Collapsed;
            gridGolovolom.Visibility = Visibility.Visible;


        }

        private void LoadTextListGolovolomli()
        {

            this.Items = new List<string>();
            //TextBlock txblc  = new TextBlock();
            
            for (int i = 1; i < 99; i++)
            {

                Items.Add(myArrGolovolomki[i].Zagolovok);
                
                //txblc.Text = myArrGolovolomki[i].Zagolovok;
               
            }

            this.MyLongListG.ItemsSource = Items;
            //gridGolovolom.Children.Add(txblc);
            
            
            

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            gridGeneralMenu.Visibility = Visibility.Visible;
            gridAbout.Visibility = Visibility.Collapsed;
            
        }

        private void MyLongListG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int i = 0;
            object selectUser;
            string resultselectUser;
            selectUser = MyLongListG.SelectedItem;

            resultselectUser = selectUser.ToString();
            for(int i = 0; i < 99; i++)
            {
                if (resultselectUser == myArrGolovolomki[i].Zagolovok)
                {
                    tbGolovolomka.Text = myArrGolovolomki[i].QuestionGolovolom;
                    tbGolovolomTitul.Text = resultselectUser;
                    numberGolovolomka = i;
                    gridGolovolomInput.Visibility = Visibility.Visible;
                    gridGolovolom.Visibility = Visibility.Collapsed;
                    break;
                }
                
            }
           
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            gridGolovolom.Visibility = Visibility.Visible;
            gridGolovolomInput.Visibility = Visibility.Collapsed;
        }

        private void btGolnext_Click(object sender, RoutedEventArgs e)
        {
            numberGolovolomka++;

            if (numberGolovolomka > 98) numberGolovolomka = 98;
            tbGolovolomTitul.Text = myArrGolovolomki[numberGolovolomka].Zagolovok;
            tbGolovolomka.Text = myArrGolovolomki[numberGolovolomka].QuestionGolovolom;
          


        }

        private void btGolback_Click(object sender, RoutedEventArgs e)
        {
            numberGolovolomka--;

            if (numberGolovolomka < 1) numberGolovolomka = 1;
            tbGolovolomTitul.Text = myArrGolovolomki[numberGolovolomka].Zagolovok;
            tbGolovolomka.Text = myArrGolovolomki[numberGolovolomka].QuestionGolovolom;
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)//кнопка подсказки
        {

             if (help > 0)
             {
                 if (myArrGolovolomki[numberGolovolomka].AntiRepeatGolovolom == "true")
                 {
                     var result = MessageBox.Show("Показать подсказку? За подсказку вы отдадите звездочку.",
                             "Помощь", MessageBoxButton.OKCancel);
                     if (result == MessageBoxResult.OK)
                     {
                         help += -1;
                            
                 
                     IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
                                if (!settings.Contains("emailFlag1"))//------------------------  Сохранение int help;
                                {
                                    settings.Add("emailFlag1", help);
                                }
                                else
                                {
                                    settings["emailFlag1"] = help;
                               
                                }
                         tbGmkuTrueAnswer.Text = myArrGolovolomki[numberGolovolomka].AnswerGolovolom;
                         tbStarsNumber.Visibility = Visibility.Visible;
                         tbStarsNumber.Text = "Звездочек осталось: " + Convert.ToString(help);
                         gridGlmkuHelp.Visibility = Visibility.Visible;
                         gridGolovolomInput.Visibility = Visibility.Collapsed;

                     }

                 }
                 else
                    {
                        tbGmkuTrueAnswer.Text = myArrGolovolomki[numberGolovolomka].AnswerGolovolom;
                        tbStarsNumber.Visibility = Visibility.Collapsed;
                     gridGlmkuHelp.Visibility = Visibility.Visible;
                         gridGolovolomInput.Visibility = Visibility.Collapsed;

                    }
             }
            else
                {
                    MessageBox.Show("Для подсказки требуется звездочка!",
                    "Жаль", MessageBoxButton.OK);
                }

        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {

            numberGolovolomka++;

            if (numberGolovolomka > 98) numberGolovolomka = 98;
            tbGolovolomTitul.Text = myArrGolovolomki[numberGolovolomka].Zagolovok;
            tbGolovolomka.Text = myArrGolovolomki[numberGolovolomka].QuestionGolovolom;

            gridGolovolomInput.Visibility = Visibility.Visible;
            gridGlmkuHelp.Visibility = Visibility.Collapsed;
        }

        private void btn_about_Click(object sender, RoutedEventArgs e)
        {
            gridAbout.Visibility = Visibility.Visible;
            gridGeneralMenu.Visibility = Visibility.Collapsed;
        }

        private void btnbackmenufrgol_Click(object sender, RoutedEventArgs e)
        {
            gridGeneralMenu.Visibility = Visibility.Visible;
            gridGolovolom.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            
            gridGeneralMenu.Visibility = Visibility.Visible;
            gridSetColor.Visibility = Visibility.Collapsed;
        }


      }


    public struct Golovolomki
    {
            public string Zagolovok;
            public string QuestionGolovolom;
            public string AnswerGolovolom;
            public string AntiRepeatGolovolom;
            
            //public int Numb;

            public Golovolomki(string[] args)
            {
                Zagolovok = args[0];
                QuestionGolovolom = args[1];
                AnswerGolovolom = args[2];
                AntiRepeatGolovolom = args[3];
                //Numb = int.Parse(args[3]);
            }

            /*static public bool CheckFac(string repit, Zagadki[] array)
            {
                bool temp = false;
                foreach (Zagadki s in array)
                    if (s.AntiRepeat == repit)
                        temp = true;
                return temp;
            }*/

    }

    public struct ColorSet
    {
        public byte alfaColor;
        public byte redColor;
        public byte greenColor;
        public byte blueColor;
        public byte foregroundTextColor;
        
            //public int Numb;

            public ColorSet(string[] args)
            {
                alfaColor = byte.Parse(args[0]);
                redColor = byte.Parse(args[1]);
                greenColor = byte.Parse(args[2]);
                blueColor = byte.Parse(args[3]);
                foregroundTextColor = byte.Parse(args[4]);

                //Numb = int.Parse(args[3]);
            }

            

    }

    public struct TFLiteratyra
    {
        public string QuestionTFLiteratyra;
        public string AnswerTFLiteratyra;
        public string CheckValidityTFLiteratyra;
       // public string CheckReplay;

            

            public TFLiteratyra(string[] args)
            {
                QuestionTFLiteratyra = args[0];
                AnswerTFLiteratyra = args[1];
                CheckValidityTFLiteratyra = args[2];
               // CheckReplay = args[3];
                //Numb = int.Parse(args[3]);
            }

            
    }

    public struct TFScope
    {
        public int Literatyra;
        public int Automabili;
        public int History;
        public int ITtehnology;
        public int EarthPlanet;
        public int Sports;

        public TFScope(string[] args)
            {
                Literatyra = int.Parse(args[0]);
                Automabili = int.Parse(args[1]);
                History = int.Parse(args[2]);
                ITtehnology = int.Parse(args[3]);
                EarthPlanet = int.Parse(args[4]);
                Sports = int.Parse(args[5]);

                
            }


    }
}
        
    
