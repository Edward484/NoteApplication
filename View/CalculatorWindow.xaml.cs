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
using System.Windows.Shapes;

namespace NoteApplication.View
{
    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Division,
        Multiplication
    }



    public partial class CalculatorWindow : Window
    {
        double lastNumber;
        double result;
        SelectedOperator SelectedOperator;
        public CalculatorWindow()
        {
            InitializeComponent();

            ACButton.Click += AcButton_Click;
            plusPerOneButton.Click += PlusPerOneButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
            pointButton.Click += PointButton_Click;
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Content.ToString().Contains("."))
            {
            }
            resultLabel.Content = $"{resultLabel.Content}.";

        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                miniDisplay.Content = miniDisplay.Content.ToString() + $" {newNumber.ToString()} =";
                switch (SelectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = lastNumber + newNumber;
                        break;
                    case SelectedOperator.Substraction:
                        result = lastNumber - newNumber;
                        break;
                    case SelectedOperator.Multiplication:
                        result = lastNumber * newNumber;
                        break;
                    case SelectedOperator.Division:
                        result = lastNumber / newNumber;
                        break;
                }
            }
            resultLabel.Content = result.ToString();

        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;
                if(lastNumber != 0)
                {
                    tempNumber = lastNumber;
                }
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void PlusPerOneButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * (-1);
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
            miniDisplay.Content = "";
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = 0;
                miniDisplay.Content = lastNumber.ToString();
            }

            if (sender == multiplyButton)
            {
                SelectedOperator = SelectedOperator.Multiplication;
                miniDisplay.Content = miniDisplay.Content.ToString()+ " *";
            }
            if(sender == divideButton)
            {
                SelectedOperator = SelectedOperator.Division;
                miniDisplay.Content = miniDisplay.Content.ToString() + " /";
            }
            if(sender == addButton)
            {
                SelectedOperator = SelectedOperator.Addition;
                miniDisplay.Content = miniDisplay.Content.ToString() + " +";
            }
            if(sender == substractButton)
            {
                SelectedOperator = SelectedOperator.Substraction;
                miniDisplay.Content = miniDisplay.Content.ToString() + " -";
            }
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }
}
