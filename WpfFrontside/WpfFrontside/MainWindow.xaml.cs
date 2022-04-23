using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WpfFrontside
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("DLLAsm.dll")]
        private static extern unsafe void Dodaj(DataToAssembly* structurePtr);

        [DllImport("DLLAsm.dll")]
        private static extern unsafe void Odejmij(DataToAssembly* structurePtr);

        [DllImport("DLLAsm.dll")]
        private static extern unsafe void Podziel(DataToAssembly* structurePtr);

        [DllImport("DLLAsm.dll")]
        private static extern unsafe void Pomnoz(DataToAssembly* structurePtr);

        private static DataToAssembly _dataToAssembly = new DataToAssembly();

        unsafe delegate void AssemblyAction(DataToAssembly* ptr);

        public MainWindow()
        {
            InitializeComponent();
        }

        private float[] GetInputArrayA()
        {
            return new[]
            {
                Input1A.GetValueOfInput(),
                Input2A.GetValueOfInput(),
                Input3A.GetValueOfInput(),
                Input4A.GetValueOfInput()
            };
        }

        private float[] GetInputArrayB()
        {
            return new[]
            {
                Input1B.GetValueOfInput(),
                Input2B.GetValueOfInput(),
                Input3B.GetValueOfInput(),
                Input4B.GetValueOfInput()
            };
        }

        private void AppendOutput(float[] output)
        {
            Output1.SetValueOfOutput(output[0]);
            Output2.SetValueOfOutput(output[1]);
            Output3.SetValueOfOutput(output[2]);
            Output4.SetValueOfOutput(output[3]);
        }

        private void InvokeAssemblerFunction(AssemblyAction assemblyAction)
        {
            var result = new float[4];

            unsafe
            {
                fixed (DataToAssembly* aAddress = &_dataToAssembly)
                {
                    fixed (float* ptr1 = GetInputArrayA(), ptr2 = GetInputArrayB(), ptr3 = result)
                    {
                        _dataToAssembly.InputA = ptr1;
                        _dataToAssembly.InputB = ptr2;
                        _dataToAssembly.Output = ptr3;

                        assemblyAction(aAddress);
                    }
                }
            }

            AppendOutput(result);
        }

        private unsafe void AddButton_Click(object sender, RoutedEventArgs e)
        {
            InvokeAssemblerFunction(Dodaj);
        }

        private unsafe void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            InvokeAssemblerFunction(Odejmij);
        }

        private unsafe void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            InvokeAssemblerFunction(Podziel);
        }

        private unsafe void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            InvokeAssemblerFunction(Pomnoz);
        }
    }



    public static class WpfUtils
    {
        public static float GetValueOfInput(this TextBox input)
        {
            return float.Parse(input.Text);
        }

        public static void SetValueOfOutput(this TextBox input, float value)
        {
            input.Text = value.ToString(CultureInfo.CurrentCulture);
        }
    }
}
