using System;
using System.Collections.Concurrent;
using HtmlAgilityPack;
using static System.Windows.Forms.LinkLabel;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Windows.Forms.Design.AxImporter;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using System.Xml;

namespace Parserr
{
    public partial class Form1 : Form
    {
        private static ChromeOptions StartFile(string selectedFolderPath)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            selectedFolderPath = folderBrowserDialog.SelectedPath;
            options.AddUserProfilePreference("download.default_directory", result);
            return options;
        }
        public static ChromeOptions options = new ChromeOptions();
        private IWebDriver driver = new ChromeDriver(StartFile(selectedFolderPath));
        private FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        private static string selectedFolderPath;

        private async Task ParseLinks(string key, string link)
        {

            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
            driver.Navigate().GoToUrl(link);
            string htmlCode = driver.PageSource;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlCode);
            var sectionNodes = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'section course-section main')]");
            for (int i = 0; i < sectionNodes.Count; i++)
            {
                var nameSection = sectionNodes[i].SelectSingleNode(".//h3[contains(@class, 'sectionname course-content-item d-flex align-self-stretch align-items-center mb-0')]").InnerHtml.Trim().Replace("\r\n", "").Trim();
                userControl2.AddCourseSection(key, nameSection);
            }
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
        }

        private async void ParseMyCourses()
        {
            string htmlCode = driver.PageSource;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlCode);

            var linkNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='w-100 text-truncate']/a");
            string courseName;
            for (int i = 0; i < linkNodes.Count; i++)
            {
                string link = linkNodes[i].GetAttributeValue("href", ""); // посилання видобуває з відсортованого вище
                string nameCourse = linkNodes[i].SelectSingleNode(".//span[contains(@class, 'multiline')]").InnerHtml.Trim().Replace("\r\n", "").Trim();
                //nameCourse = linkNodes[i].InnerHtml.Trim().Replace("\r\n", "").Trim();
                userControl2.AddCourse2(nameCourse);
                ParseLinks(nameCourse, link);
            }
        }
        private void ReadCheckedNodes(TreeNodeCollection nodes, string path)
        {
            foreach (TreeNode node in nodes)
            {
                bool hasCheckedChildNodes = node.Nodes.Cast<TreeNode>().Any(childNode => childNode.Checked);
                if (node.Checked || hasCheckedChildNodes)
                {
                    string htmlCode = driver.PageSource;
                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlCode);
                    var linkNodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='w-100 text-truncate']/a");
                    string courseName;
                    for (int i = 0; i < linkNodes.Count; i++)
                    {
                        string link = linkNodes[i].GetAttributeValue("href", ""); // посилання видобуває з відсортованого вище
                        string nameCourse = linkNodes[i].SelectSingleNode(".//span[contains(@class, 'multiline')]").InnerHtml.Trim().Replace("\r\n", "").Trim();

                        if (!Directory.Exists(path + "\\" + nameCourse))
                        {
                            Directory.CreateDirectory(path + "\\" + nameCourse);
                        }
                        else if (node.Name == nameCourse || !Directory.Exists(path + "\\" + nameCourse)) 
                            { 
                            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                            driver.Navigate().GoToUrl(link);
                            string htmlCodeCourse = driver.PageSource;
                            var htmlDocCourse = new HtmlDocument();
                            htmlDocCourse.LoadHtml(htmlCodeCourse);
                            var sectionNodes = htmlDocCourse.DocumentNode.SelectNodes("//li[contains(@class, 'section course-section main')]");
                            for (int j = 0; j < sectionNodes.Count; j++)
                            {
                                var nameSection = sectionNodes[j].SelectSingleNode(".//h3[contains(@class, 'sectionname course-content-item d-flex align-self-stretch align-items-center mb-0')]").InnerHtml.Trim().Replace("\r\n", "").Trim();
                                var parNameSection = sectionNodes[j].SelectNodes(".//span[contains(@class, 'instancename')]");
                                for (int k = 0; k < parNameSection.Count ; k++)
                                {
                                    string particularNameSection = parNameSection[k].InnerText.Trim().Replace("\r\n", "").Trim();
                                    if (node.Nodes[j].Name == nameSection && node.Nodes[j].Checked)
                                    {
                                        if (!Directory.Exists(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}"))
                                        {
                                            Directory.CreateDirectory(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}");
                                        }
                                        try
                                        {
                                            var linkDownload = htmlDoc.DocumentNode.SelectSingleNode(".//div[@class='activitytitle media  modtype_resource position-relative align-self-start']/img");
                                            string linkD = linkNodes[j].GetAttributeValue("href", ""); // посилання видобуває з відсортованого вище
                                            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                            driver.Navigate().GoToUrl(linkD);
                                        }
                                        catch (Exception)
                                        {
                                            IWebElement tableElement = driver.FindElement(By.CssSelector("div.description-inner table"));
                                            IList<IWebElement> rows = tableElement.FindElements(By.CssSelector("tbody tr"));
                                            List<string> csvData = new List<string>();
                                            foreach (IWebElement row in rows)
                                            {
                                                IList<IWebElement> cells = row.FindElements(By.CssSelector("td"));
                                                string rowData = string.Join(",", cells);
                                                csvData.Add(rowData);
                                            }
                                            File.WriteAllLines(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}.csv", csvData);
                                        }
                                        htmlCodeCourse = driver.PageSource;
                                        htmlDocCourse.LoadHtml(htmlCodeCourse);
                                        if (driver.Url.StartsWith("https://exam.nuwm.edu.ua/mod/forum/view.php"))
                                        {
                                            File.WriteAllText(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}", "Форум" + driver.Url);
                                            driver.Close();
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                        }
                                        else if (driver.Url.StartsWith("https://exam.nuwm.edu.ua/pluginfile.php"))
                                        {
                                            using (WebClient webClient = new WebClient())
                                            {
                                                webClient.DownloadFile(link, path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}" + j);
                                            }
                                            driver.Close();
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                        }
                                        else if (driver.Url.StartsWith("https://exam.nuwm.edu.ua/mod/folder/view.php"))
                                        {
                                            IWebElement downloadButton = driver.FindElement(By.CssSelector("button[type='submit']"));
                                            IWebElement headerElement = driver.FindElement(By.CssSelector("div.page-header-headings h1"));
                                            downloadButton.Click();
                                            string headerText = headerElement.Text;
                                            string sourceFilePath = selectedFolderPath + "\\" + headerText;
                                            string destinationFilePath = path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}";
                                            if (File.Exists(sourceFilePath))
                                            {
                                                File.Move(sourceFilePath, destinationFilePath);
                                            }
                                            else
                                            {
                                                File.WriteAllText(path + "\\" + nameCourse + "\\" + nameSection + $"\\Сталася помилка {particularNameSection}.txt", "Початковий файл не знайдено, перевірте папку Завантаження або завантажте файл самомтійно " + driver.Url);
                                            }
                                            driver.Close();
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                        }
                                        else if (driver.Url.StartsWith("https://ep3.nuwm.edu.ua/"))
                                        {
                                            try
                                            {
                                                IWebElement linkElement = driver.FindElement(By.CssSelector("a.ep_document_link"));
                                                string downloadLink = linkElement.GetAttribute("href");
                                                IWebElement quizElement = driver.FindElement(By.CssSelector(".//div[@class='ep_tm_page_content'"));
                                                quizElement = driver.FindElement(By.CssSelector(".//h1[@class='ep_tm_pagetitle']"));
                                                string nameElement = quizElement.GetAttribute("h1");
                                                using (WebClient webClient = new WebClient())
                                                {
                                                    webClient.DownloadFile(downloadLink, path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}");
                                                }
                                            }
                                            catch
                                            {
                                                File.WriteAllText(path + "\\" + nameCourse + "\\" + nameSection + $"\\Сталася помилка {particularNameSection}.txt", "Перейдіть за посиланням і перевірте чи файл все ще доступний " + driver.Url);
                                            }
                                            driver.Close();
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                        }
                                        else if (driver.Url.StartsWith("https://ep3.nuwm.edu.ua/mod/quiz/view.php"))
                                        {
                                            IWebElement quizElement = driver.FindElement(By.CssSelector(".//div[@class='main-inner'"));
                                            string elementText = quizElement.Text;
                                            quizElement = driver.FindElement(By.CssSelector(".//h1[@class='h2']"));
                                            string nameElement = quizElement.GetAttribute("h1");
                                            File.WriteAllText(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}.txt", elementText);
                                            driver.Close();
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                        }
                                        else if (driver.Url.StartsWith("https://exam.nuwm.edu.ua/mod/assign/view.php"))
                                        {
                                            IWebElement quizElement = driver.FindElement(By.CssSelector(".//div[@class='main-inner'"));
                                            string elementText = quizElement.Text;
                                            quizElement = driver.FindElement(By.CssSelector(".//h1[@class='h2']"));
                                            string nameElement = quizElement.GetAttribute("h1");
                                            File.WriteAllText(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}", elementText);
                                            driver.Close();
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                        }
                                        else if (driver.Url.StartsWith("https://exam.nuwm.edu.ua/mod/url/view.php"))
                                        {
                                            IWebElement urlElement = driver.FindElement(By.CssSelector(".//div[@class='urlworkaround'"));
                                            string url = urlElement.GetAttribute("href");
                                            IWebElement quizElement = driver.FindElement(By.CssSelector(".//div[@class='main-inner'"));
                                            string elementText = quizElement.Text;
                                            quizElement = driver.FindElement(By.CssSelector(".//h1[@class='h2']"));
                                            string nameElement = quizElement.GetAttribute("h1");
                                            driver.Navigate().GoToUrl(url);
                                            try
                                            {
                                                IWebElement linkElement = driver.FindElement(By.CssSelector("a.ep_document_link"));
                                                string downloadLink = linkElement.GetAttribute("href");
                                                using (WebClient webClient = new WebClient())
                                                {
                                                    webClient.DownloadFile(downloadLink, path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}");
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                File.WriteAllText(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}.txt", "Невідомий ресурс для розміщення файлів" + url);
                                            }
                                            driver.Close();
                                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                        }
                                        else
                                        {
                                            try
                                            {

                                                IWebElement textElement = driver.FindElement(By.CssSelector("div.activityname span.instancename"));
                                                string headerText = textElement.Text;
                                                string sourceFilePath = selectedFolderPath + "\\" + headerText;
                                                string destinationFilePath = path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}" + "\\" + headerText;
                                                if (File.Exists(sourceFilePath))
                                                {
                                                    File.Move(sourceFilePath, destinationFilePath);
                                                }
                                                else
                                                {
                                                    File.WriteAllText(path + "\\" + nameCourse + "\\" + nameSection + $"\\{particularNameSection}.txt" + "\\Сталася помилка", "Початковий файл не знайдено, перевірте папку Завантаження або завантажте файл самомтійно " + driver.Url);
                                                }
                                                driver.Close();
                                                driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                                            }
                                            catch (Exception)
                                            {

                                                throw;
                                            }
                                        }
                                    }
                                }
                            }

                            driver.Close();
                            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                        }
                    }
                    driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            userControl2.Hide();
            userControl1.Show();
            userControl1.BringToFront();
        }

        private async void btnDownloadFiles_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string selectedFolderPath = folderBrowserDialog.SelectedPath;
                if (selectedFolderPath != null)
                {
                    ReadCheckedNodes(userControl2.nodeCollection(), folderBrowserDialog.SelectedPath);
                }
            }
        }


        private void btnDownload_Click_1(object sender, EventArgs e)
        {

                //ChromeOptions options = new ChromeOptions();
                //options.AddArgument("--headless");
                driver.Navigate().GoToUrl("https://exam.nuwm.edu.ua/");
                IWebElement login = driver.FindElement(By.LinkText("Увійти"));
                login.Click();
                login = driver.FindElement(By.LinkText("Google"));
                login.Click();
                IWebElement emailInput = driver.FindElement(By.CssSelector("input[type='email']"));
                emailInput.SendKeys("melnychuk.o.s_ak21@nuwm.edu.ua");
                login = driver.FindElement(By.CssSelector(".F9NWFb #identifierNext button"));
                //Thread.Sleep(1000);
                login.Click();
                Thread.Sleep(5000);
                IWebElement passwordInput = driver.FindElement(By.CssSelector("input[type='password']"));
                Thread.Sleep(5000);
                passwordInput.SendKeys("66848870");
                login = driver.FindElement(By.CssSelector(".F9NWFb #passwordNext button"));
                Thread.Sleep(2000);
                login.Click();
                Thread.Sleep(2000);
                //string htmlCode = driver.PageSource;
                //var htmlDoc = new HtmlDocument();
                //htmlDoc.LoadHtml(driver.Url);
                //HtmlNode myElement = htmlDoc.GetElementbyId("menuitem");

                login = driver.FindElement(By.CssSelector("li[data-key='mycourses'] a"));
                login.Click();
                Thread.Sleep(1000);
                string htmlCode = driver.PageSource;
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlCode);

                var coursesNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'w-100 text-truncate')]");
                //List<string> linkList = new List<string>();
                string courseName;
                for (int i = 0; i < coursesNodes.Count; i++)
                {
                    //linkList[i] = coursesNodes[i].GetAttributeValue("href", ""); // посилання видобуває з відсортованого вище
                    var nameCourseNode = coursesNodes[i].SelectSingleNode(".//span[contains(@class, 'multiline')]"); // текст в тегу p<>
                                                                                                                     //var nameCourse = nameCourseNode.SelectSingleNode(".//span").InnerHtml;
                    courseName = nameCourseNode.InnerHtml.Trim().Replace("\r\n", "").Trim();
                    userControl1.AddCourse(courseName);
                }
                //driver.Quit();
        }

        private async void btnNextBack_Click(object sender, EventArgs e)
        {
            if (userControl1.Visible == true)
            {
                userControl2.Visible = true;
                userControl1.Visible = false;
                userControl2.BringToFront();
                btnNextBack.Text = "Назад";
                btnDownload.Text = "Завантажити курси";
                btnDownload.Visible = false;
                btnDownloadFiles.Visible = true;
                btnDownloadFiles.BringToFront();

                ParseMyCourses();
            }
            else
            {
                userControl1.Visible = true;
                userControl2.Visible = false;
                userControl1.BringToFront();
                btnNextBack.Text = "Продовжити далі";
                btnDownload.Text = "Завантажити список курсів";
                btnDownload.Visible = true;
                btnDownloadFiles.Visible = false;
                btnDownload.BringToFront();
            }
        }
    }
}