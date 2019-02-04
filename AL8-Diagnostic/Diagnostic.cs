using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using mscoree;

namespace Advanced_Lesson_6_Diagnostic
{
    public static class Diagnostic
    {
        public static void ListAllRunningProcesses()
        {
            // Получение списка всех системных процессов, которые выполняются на текущей машине. 
            Process[] runningProcs = Process.GetProcesses(".");
            // Отображение идентификатора и имени каждого из процессов. 
            foreach (Process p in runningProcs)
            {
                if (p.Id == Process.GetCurrentProcess().Id)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }

                string info = string.Format("-> PID: {0}\tName: {1}", p.Id, p.ProcessName);
                Console.WriteLine(info);
                Console.ResetColor();
            }
            Console.WriteLine("************************************\n");
        }

        public static void ListAllProcessThreads()
        {
            // Потоки операционной системы доступные в рамках текущего процесса.
            foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
            {
                string info = string.Format("-> Thread ID: {0}\tState: {1}", thread.Id, thread.ThreadState);
                Console.WriteLine(info);
            }
            Console.WriteLine("************************************\n");
        }

        public static void ListAllProcessCodeModules()
        {
            ProcessModuleCollection theMods = Process.GetCurrentProcess().Modules;
            foreach (ProcessModule pm in theMods)
            {
                string info = string.Format("-> Mod Name: {0}", pm.ModuleName);
                Console.WriteLine(info);
            }
            Console.WriteLine("************************************\n");
        }

        public static void ListAllAppDomains()
        {
            var currentDomain = AppDomain.CurrentDomain;

            IList<AppDomain> list = new List<AppDomain>();
            IntPtr enumHandle = IntPtr.Zero;
            ICorRuntimeHost host = new CorRuntimeHost();
            try
            {
                host.EnumDomains(out enumHandle);
                object domain = null;
                while (true)
                {
                    host.NextDomain(enumHandle, out domain);
                    if (domain == null) break;
                    AppDomain appDomain = (AppDomain)domain;
                    list.Add(appDomain);
                }

                foreach (var item in list)
                {
                    if (currentDomain.Id == item.Id)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                    }

                    Console.WriteLine($"-> Domain friendly name: {item.FriendlyName}");
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                host.CloseEnum(enumHandle);
                Marshal.ReleaseComObject(host);
            }
        }

        
    }
}

