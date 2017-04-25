using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LearningPJ.Main
{
    class Program
    {


        private static List<EncryptModel> Data;

        private static void Main(string[] args)
        {
            /*
             1、提供一个加密的方法
             2、开始需要用户输入密码
             3、根据account生成编号，输入编号 返回密码
             */

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.CursorVisible = false;
            Console.Title = "search password tool by northwolf1";


            //登录查询
            //try
            //{
            //    Login();
            //}
            //catch (Exception ex)
            //{
            //    Console.Clear();
            //    Console.Write("发错错误：" + ex.Message);

            //    Login();
            //}



            //密码生成
            try
            {
                CreatePassword();
            }
            catch (Exception ex)
            {
                Console.WriteLine("发错错误：" + ex.Message+";现在重新执行！");
                CreatePassword();
            }


            Console.ReadKey();
        }

        private static void Login()
        {
            Console.WriteLine("请输入密码：");

            var loginPassword = Console.ReadLine();

            if (EncryptHelper.CheckPassword(loginPassword))
            {
                Console.Clear();

                Data = Data ?? EncryptHelper.GetAllAppSetting();

                foreach (var d in Data)
                {
                    Console.WriteLine(d.ID + "、" + d.Account + "\r\n");
                }

                Console.WriteLine("请输入要查询的ID");
                var searchIDStr = Console.ReadLine();
                int searchID;
                if (int.TryParse(searchIDStr, out searchID))
                {
                    var searchResult = Data.FirstOrDefault(k => k.ID == searchID);
                    Console.WriteLine(EncryptHelper.DecryptDES(searchResult.Password, searchResult.Key));
                }
            }
            else
            {
                Console.WriteLine("密码错误！");
                Login();
            }
        }

        private static void CreatePassword()
        {
            Console.Clear();

            Console.WriteLine("输入要生成的密码和解密key，格式为：账户,密码,key(注意key要8位)");

            var userInput = Console.ReadLine().Split(',');

            var password = EncryptHelper.EncryptDES(userInput[1], userInput[2]);

            var showStr = string.Format("{0}|{1}|{2}", userInput[0], password, userInput[2]);
            Console.WriteLine(showStr);

            Console.WriteLine("是否继续生成？");
            var isCarryOn = Console.ReadLine().ToLower();
            if (isCarryOn.Equals("true"))
            {
                CreatePassword();
            }
        }






    }
}
