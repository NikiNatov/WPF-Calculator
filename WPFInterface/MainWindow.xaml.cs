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

namespace WPFInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Member Variables
        private enum Operation { NO_OPERATION, PLUS, MINUS, MULTIPLY, DIVIDE, POWER };

        private const double PI = 3.1415926535897932384626433832795;
        private const double E  = 2.7182818284590452353602874713527;

        private double leftOperand = 0.0;
        private double rightOperand = 0.0;
        private double result = 0.0;
        private bool isOperationFinished = false;
        private Operation operation = Operation.NO_OPERATION;
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Number Buttons
        /// <summary>
        /// Function for all number buttons including the ","
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string input = (string)((Button)sender).Content;

            // If we are finished with the last operation we reset the text fields
            if(isOperationFinished)
            {
                TxtOutput.Text = "";
                TxtInput.Text = "0";
                isOperationFinished = false;
            }

            if (TxtInput.Text == "0")
            {
                if (input == ",")
                    TxtInput.Text += input;
                else
                    TxtInput.Text = input;
            }
            else
            {
                if (TxtInput.Text.Contains(",") && input == ",")
                    return;
         
                TxtInput.Text += input;
            }
        }

        /// <summary>
        /// Function for "+/-" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNegate_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInput.Text != "0")
            {
                if (TxtInput.Text.Contains("-"))
                {
                    string processedString = TxtInput.Text.Remove(0, 1);
                    TxtInput.Text = processedString;
                }
                else
                {
                    string processedString = TxtInput.Text.Insert(0, "-");
                    TxtInput.Text = processedString;
                }
            }
        }
        #endregion

        #region Operation Buttons

        /// <summary>
        /// Fuction for button "="
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            // If the current operation is valid and not finished we handle the input and display the result
            if (!isOperationFinished && operation != Operation.NO_OPERATION)
            {
                if (double.TryParse(TxtInput.Text, out rightOperand))
                {
                    TxtOutput.Text += rightOperand + " = ";

                    switch (operation)
                    {
                        case Operation.PLUS: result = leftOperand + rightOperand; break;
                        case Operation.MINUS: result = leftOperand - rightOperand; break;
                        case Operation.MULTIPLY: result = leftOperand * rightOperand; break;
                        case Operation.DIVIDE:
                        {
                            // Division by zero check
                            if (rightOperand == 0.0)
                            {
                                TxtInput.Text = "<ERROR>";
                                MessageBox.Show("Division by zero not allowed!");
                                return;
                            }
                            else
                                result = leftOperand / rightOperand;
                            break;
                        }
                        case Operation.POWER: result = Math.Pow(leftOperand, rightOperand); break;
                    }
                    TxtInput.Text = result.ToString();
                    operation = Operation.NO_OPERATION;
                    isOperationFinished = true;
                }
                else
                {
                    MessageBox.Show($"{TxtInput.Text} is invalid number!");
                }
            }
        }

        /// <summary>
        /// Function for all operation buttons including "exp"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOperation_Click(object sender, RoutedEventArgs e)
        {
            // This allows us to continue making calculations by using the result from the last operation as left operand
            if (isOperationFinished == true)
                isOperationFinished = false;

            string operationString = (string)((Button)sender).Content;

            if (double.TryParse(TxtInput.Text, out leftOperand))
            {
                TxtInput.Text = "0";

                // We only update the operation if it is different than the last
                switch (operationString)
                {
                    case "+":
                    {
                        if (operation != Operation.PLUS)
                        {                               
                            TxtOutput.Text = leftOperand.ToString() + " " + operationString + " ";
                            operation = Operation.PLUS;                                
                        }
                        break;
                    }
                    case "-":
                    {
                        if (operation != Operation.MINUS)
                        {
                            TxtOutput.Text = leftOperand.ToString() + " " + operationString + " ";
                            operation = Operation.MINUS; 
                        }
                        break;
                    }
                    case "*":
                    {
                        if (operation != Operation.MULTIPLY)
                        {
                            TxtOutput.Text = leftOperand.ToString() + " " + operationString + " ";
                            operation = Operation.MULTIPLY;                            
                        }
                        break;
                    }
                    case "/":
                    {
                        if (operation != Operation.DIVIDE)
                        {
                            TxtOutput.Text = leftOperand.ToString() + " " + operationString + " ";
                            operation = Operation.DIVIDE;
                            
                        }
                        break;
                    }
                    case "exp":
                    {
                        if(operation != Operation.POWER)
                        {
                            TxtOutput.Text = leftOperand.ToString() + " ^ ";
                            operation = Operation.POWER;
                        }
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show($"{TxtInput.Text} is invalid number!");
            }
           
        }
        #endregion

        #region Constants Buttons
        /// <summary>
        /// Function for the PI constant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPI_Click(object sender, RoutedEventArgs e)
        {
            if (isOperationFinished)
            {
                TxtOutput.Text = "";
                TxtInput.Text = "0";
                isOperationFinished = false;
            }

            TxtInput.Text = PI.ToString();
        }

        /// <summary>
        /// Function for the Euler constant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnE_Click(object sender, RoutedEventArgs e)
        {
            if (isOperationFinished)
            {
                TxtOutput.Text = "";
                TxtInput.Text = "0";
                isOperationFinished = false;
            }

            TxtInput.Text = E.ToString();
        }
        #endregion

        #region Mathematical Functions Buttons

        /// <summary>
        /// Natural Logarithm Function: ln(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLn_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if(double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Log(number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Base 10 Logarithm Function: log(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Log10(number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Cosecant Function: cosec(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCosec_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = (1.0 / Math.Sin(number)).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Secant Function: sec(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSec_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = (1.0 / Math.Cos(number)).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Cotangent Function: cotg(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCotg_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = (Math.Cos(number) / Math.Sin(number)).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }


        /// <summary>
        /// Tangent Function: tg(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTg_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = (Math.Sin(number) / Math.Cos(number)).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Cosine Function: cos(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCos_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Cos(number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }


        /// <summary>
        /// Sine Function: sin(x)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSin_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Sin(number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Ten to the Power of X Function: 10^x
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTenToX_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Pow(10, number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Square Root Function: x^(1/2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSqrt_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Sqrt(number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// X Squared Function: x^2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSquare_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Pow(number, 2).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// One Over X Function: 1/x
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOneOverX_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = (1.0 / number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }

        /// <summary>
        /// Absolute Value Function: |x|
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAbs_Click(object sender, RoutedEventArgs e)
        {
            double number = 0.0;

            if (double.TryParse(TxtInput.Text, out number))
                TxtInput.Text = Math.Abs(number).ToString();
            else
                MessageBox.Show($"{number} is invalid number!");
        }
        #endregion

        #region Clear Buttons
        /// <summary>
        /// Clear Current Entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            TxtInput.Text = "0";
        }

        /// <summary>
        /// Clear All Entries
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            TxtOutput.Text = "";
            TxtInput.Text = "0";
            operation = Operation.NO_OPERATION;
            isOperationFinished = false;
        }

        /// <summary>
        /// Clear Last Digit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInput.Text == "0")
                return;

            string processedString = TxtInput.Text.Remove(TxtInput.Text.Length - 1);
            TxtInput.Text = processedString == "" ? "0" : processedString;
        }
        #endregion
    }
}
