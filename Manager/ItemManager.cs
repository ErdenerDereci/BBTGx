using Manager.Entity;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Threading;

namespace Business
{
    public class ItemManager : IItemService
    {

        public List<Item> getItemList(string itemName, int _unitPrice, int _amount, bool amountCond, string email)
        {
            ChromeDriverService service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            service.HideCommandPromptWindow = true;
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            using (var browser = new ChromeDriver(service,chromeOptions))
            {
                browser.Navigate().GoToUrl("https://eu.tamrieltradecentre.com/pc/Trade/SearchResult?ItemNamePattern=" + splitItemName(itemName));
                Thread.Sleep(5000);
                var table = browser.FindElement(By.CssSelector(".trade-list-table"));
                var tbody = table.FindElement(By.TagName("tbody"));
                var trs = tbody.FindElements(By.TagName("tr"));
                int rowId = 0;
                List<Item> items = new List<Item>();
                int amount;
                double unitPrice;
                string location;
                string date;
                foreach (var tr in trs)
                {
                    if (tr.GetAttribute("class") == "cursor-pointer")
                    {
                        rowId++;
                        var tds = tr.FindElements(By.TagName("td"));
                        amount = Convert.ToInt32(tds[3].FindElements(By.TagName("span"))[1].Text);
                        unitPrice = Convert.ToDouble(tds[3].FindElements(By.TagName("span"))[0].Text);
                        location = tds[2].Text;
                        date = tds[4].Text;
                        if (unitPrice <= _unitPrice)
                        {
                            if (amountCond)
                            {
                                if (amount <= _amount)
                                {
                                    items.Add(
                                       new Item(rowId, amount, location, unitPrice, date));
                                }
                            }
                            else
                            {
                                if (amount >= _amount)
                                {
                                    items.Add(
                                       new Item(rowId, amount, location, unitPrice, date));
                                }
                            }
                        }
                    }
                }
                browser.Close();
                //sendEmail(items, email, itemName);
                return items;
            }     
        }

        public List<Item> getItemList()
        {
            Console.WriteLine();
            return new List<Item>()
            {
                new Item()
            };
        }
        public void getItemListx()
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync("https://eu.tamrieltradecentre.com/pc/Trade/SearchResult?ItemNamePattern=Chromium+Plating").Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        string result = content.ReadAsStringAsync().Result;
                        int x=1;
                    }
                }
            }
        
}
        private void sendEmail(List<Item> items, string email, string itemName)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("erdrci@gmail.com");
            mail.To.Add(email);
            mail.Subject = "Test Mail - 1";
            mail.IsBodyHtml = true;
            string htmlBody;
            htmlBody = parseItemDatas(items, itemName);
            mail.Body = htmlBody;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("erdrci@gmail.com", "bilibili1");
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }

        private string splitItemName(string itemName)
        {
            itemName=itemName.Trim();
            itemName.Replace(" ","+");
            return itemName;
        }
        private string parseItemDatas(List<Item> items, string itemName)
        {
            string result = "";
            foreach (var item in items)
            {
                result += "itemName: " + itemName + " || Location: " + item.Location + " || UnitPrice: " + item.UnitPrice+"\n" +
                    "_________________________________________________________________________________________________________"+ "\n";
            }
            return result;
        }
    }
}
