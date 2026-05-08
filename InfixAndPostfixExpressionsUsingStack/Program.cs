using System.Text;

namespace InfixAndPostfixExpressionsUsingStack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder infix = new StringBuilder();
            StringBuilder postfix = new StringBuilder();

            InfixPostFixExpression expression = new InfixPostFixExpression();


            // -- Convert Infix to Postfix Expression --

            // read the expression from the user
            Console.Write("Enter an infix expression: ");
            infix.Append(Console.ReadLine());

            // convert infix to postfix
            postfix = expression.ConvertToPostfix(infix);

            // display the postfix expression
            Console.WriteLine("\nPostfix expression: " + postfix);



            // -- Evaluate Postfix Expression --

            int result = expression.EvaluatePostfixExpression(postfix);

            // display the result
            Console.WriteLine("\nResult: " + result);
        }
    }
}
