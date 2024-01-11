using System;
using System.Windows.Forms;
using ModelLayer;
using Ninject;

namespace DecanatPRO_MVP
{
    internal static class Program
    {
        private static IKernel Kernel { get; } = new StandardKernel(new SimpleConfigModule());

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FromMain(Kernel));
        }
    }
}