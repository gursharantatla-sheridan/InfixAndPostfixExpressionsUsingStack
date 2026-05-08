using System.Text;

namespace InfixAndPostfixExpressionsUsingStack
{
    public class InfixPostFixExpression
    {
        // converts infix expression to postfix expression, and returns the postfix expression
        public StringBuilder ConvertToPostfix(StringBuilder infix)
        {
            Stack<char> stack = new Stack<char>();
            StringBuilder postfix = new StringBuilder();

            stack.Push('(');
            infix.Append(')');

            // while stack is not empty
            while (stack.Count > 0)
            {
                // read infix from left to right
                for (int i = 0; i < infix.Length; i++)
                {
                    // read the current character in infix
                    char ch = infix[i];

                    // if it is a digit, append it to postfix
                    if (char.IsDigit(ch))
                    {
                        postfix.Append(ch);
                    }
                    // if it is a left parenthesis, push it onto the stack
                    else if (ch == '(')
                    {
                        stack.Push(ch);
                    }
                    // if it is an operator
                    else if (IsOperator(ch))
                    {
                        // read from top of stack
                        char topOfStack = stack.Peek();

                        // check precedence of operators
                        // if topOfStack have equal or higher precedence
                        // pop from stack and append to postfix
                        if (Precedence(ch, topOfStack))
                        {
                            postfix.Append(stack.Pop());
                        }

                        // push the current char onto the stack
                        stack.Push(ch);
                    }
                    // if it is a right parenthesis
                    else if (ch == ')')
                    {
                        // pop operators from the top of the stack and 
                        // append them to postfix until a left parenthesis 
                        // is at the top of the stack
                        while (stack.Peek() != '(')
                        {
                            postfix.Append(stack.Pop());
                        }

                        // pop (and discard) the left parenthesis 
                        // from the stack
                        stack.Pop();
                    }
                }
            }

            // return the formatted postfix expression
            return FormatPostfix(postfix);
        }


        // evaluates the postfix expression
        public int EvaluatePostfixExpression(StringBuilder postfix)
        {
            Stack<int> stack = new Stack<int>();

            // append a right parenthesis ')' to the end of the expression
            postfix.Append(')');

            int i = 0;

            // read from left to right until the ')' is encountered
            while (postfix[i] != ')')
            {
                char ch = postfix[i];

                // skip spaces if they were added during formatting
                if (ch == ' ')
                {
                    i++;
                    continue;
                }

                // if the character is a digit, push its integer value on the stack
                if (char.IsDigit(ch))
                {
                    // convert char to int (subtracting '0' gets the numeric value)
                    // ASCII char values -> '0' = 48, '1' = 49, '2' = 50, ..., '9' = 57
                    // so, '2' - '0' = 50 - 48 = 2
                    stack.Push(ch - '0');
                }
                // otherwise, if the character is an operator
                else if (IsOperator(ch))
                {
                    // pop the two top elements 
                    // first pop goes to x, second pop goes to y
                    int x = stack.Pop();
                    int y = stack.Pop();

                    // calculate y operator x and push the result back
                    int result = Calculate(y, x, ch);
                    stack.Push(result);
                }

                i++;
            }

            // when ')' is encountered, pop the top value; this is the final result
            return stack.Pop();
        }


        // Helper Method - returns true if the argument is one of these operator: +, -, *, /, %
        private bool IsOperator(char op)
        {
            if (op == '+' || op == '-' || op == '*' || op == '/' || op == '%')
                return true;
            else
                return false;
        }


        // Helper Method - returns true if precedence of operator2 is equal to greater than operator1, false otherwise
        private bool Precedence(char operator1, char operator2)
        {
            if (operator2 == '*' || operator2 == '/' || operator2 == '%')
            {
                if (operator1 == '+' || operator1 == '-')
                    return true;
                else
                    return false;
            }
            else if ((operator2 == '+' || operator2 == '-') && (operator1 == '+' || operator1 == '-'))
                return true;
            else if ((operator2 == '*' || operator2 == '/' || operator2 == '%') && (operator1 == '*' || operator1 == '/' || operator1 == '%'))
                return true;
            else
                return false;
        }


        // Helper Method - adds a space after each character in the postfix expression
        private StringBuilder FormatPostfix(StringBuilder postfix)
        {
            for (int i = 1; i < postfix.Length; i += 2)
            {
                postfix.Insert(i, ' ');
            }

            return postfix;
        }


        // Helper Method - performs the actual arithmetic calculation
        private int Calculate(int op1, int op2, char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+': return op1 + op2;
                case '-': return op1 - op2;
                case '*': return op1 * op2;
                case '/': return op1 / op2;
                case '%': return op1 % op2;
                default: return 0;
            }
        }
    }
}
