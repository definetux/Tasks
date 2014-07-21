using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;

namespace KataTask
{
    public class KataMethods
    {
        public static string DictinaryReplacer(string message, Dictionary<string, string> dictionary)
        {
            const string pattern = @"\$(\w+)\$";

            var words = message.Split(' ');

            var sb = new StringBuilder();

            foreach (var word in words)
            {
                var t = word.Split('$');
                if( t.Length == 3)
                    sb.Append(Regex.Replace(word, pattern, dictionary[t[1]] + ' '));
                else
                {
                    sb.Append(word + ' ');
                }
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static List<string> Minesweeper(List<string> input)
        {
            var output = new List<string>();
            int fields = 0;

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i][0] == '0' && input[i][2] == '0')
                    return output;

                fields++;
                output.Add(string.Format("Field#{0}:", fields));
                var rows = int.Parse(input[i][0].ToString()) + i;
                var column = int.Parse(input[i][2].ToString());
                var currentLine = input[i + 1];
                int mines = 0;

                // Изменяем первый элемент первой строки текущего поля
                if (input[i + 1][1] == '*')
                        mines++;
                if (input[i + 2][0] == '*')
                    mines++;
                if (input[i + 2][1] == '*')
                    mines++;

                if (currentLine[0] != '*')
                    currentLine = currentLine.Remove(0, 1).Insert(0, mines.ToString());
                mines = 0;

                // Изменяем первую строку текущего поля
                for (int col = 1; col < column - 1; col++)
                {
                    if (input[i + 1][col - 1] == '*')
                        mines++;
                    if (input[i + 1][col + 1] == '*')
                        mines++;
                    if (input[i + 2][col] == '*')
                        mines++;
                    if (input[i + 2][col - 1] == '*')
                        mines++;
                    if (input[i + 2][col + 1] == '*')
                        mines++;

                    if (currentLine[col] != '*')
                        currentLine = currentLine.Remove(col, 1).Insert(col, mines.ToString());
                    mines = 0;
                }

                // Изменяем последний элемент первой строки текущего поля
                if (input[i + 1][column - 1] == '*')
                        mines++;
                if (input[i + 2][column - 1] == '*')
                    mines++;
                if (input[i + 2][column - 2] == '*')
                    mines++;

                if (currentLine[column - 1] != '*')
                    currentLine = currentLine.Remove(column - 1, 1).Insert(column - 1, mines.ToString());

                // Помещаем текущую строку в результат
                output.Add(currentLine);

                i++;

                // Изменяем остальные строки ( кроме последней ) текущего поля
                for (int j = i + 1; j < rows; j++, i++)
                {
                    currentLine = input[j];
                    mines = 0;

                    // Изменяем первый элемент текущей строки текущего поля
                    if (input[j][1] == '*')
                        mines++;
                    if (input[j + 1][0] == '*')
                        mines++;
                    if (input[j + 1][1] == '*')
                        mines++;
                    if (input[j - 1][0] == '*')
                        mines++;
                    if (input[j - 1][1] == '*')
                        mines++;

                    if (currentLine[0] != '*')
                        currentLine = currentLine.Remove(0, 1).Insert(0, mines.ToString());

                    // Изменяем остальные элементы (кроме последнего) текущей строки текущего поля
                    for (int k = 1; k < column - 1; k++)
                    {
                        if (currentLine[k] == '*')
                            continue;
                        else
                        {
                            int mine = 0;

                            if (input[j][k - 1] == '*')
                                mine++;
                            if (input[j][k + 1] == '*')
                                mine++;
                            if (input[j + 1][k] == '*')
                                mine++;
                            if (input[j - 1][k] == '*')
                                mine++;
                            if (input[j - 1][k + 1] == '*')
                                mine++;
                            if (input[j - 1][k - 1] == '*')
                                mine++;
                            if (input[j + 1][k + 1] == '*')
                                mine++;
                            if (input[j + 1][k - 1] == '*')
                                mine++;

                            if (currentLine[k] != '*')
                                currentLine = currentLine.Remove(k, 1).Insert(k, mine.ToString());
                        }
                    }

                    mines = 0;

                    // Изменяем последний элемент текущей строки текущего поля
                    if (input[j][column - 2] == '*')
                        mines++;
                    if (input[j + 1][column - 1] == '*')
                        mines++;
                    if (input[j + 1][column - 2] == '*')
                        mines++;
                    if (input[j - 1][column - 1] == '*')
                        mines++;
                    if (input[j - 1][column - 2] == '*')
                        mines++;

                    if (currentLine[column - 1] != '*')
                        currentLine = currentLine.Remove(column - 1, 1).Insert(column - 1, mines.ToString());

                    // Помещаем текущую строку в результат
                    output.Add( currentLine );
                }

                i++;
                
                currentLine = input[i];
                mines = 0;

                // Изменяем первый элемент последней строки текущего поля
                if (input[i][1] == '*')
                    mines++;
                if (input[i - 1][0] == '*')
                    mines++;
                if (input[i - 1][1] == '*')
                    mines++;

                if (currentLine[0] != '*')
                    currentLine = currentLine.Remove(0, 1).Insert(0, mines.ToString());

                // Изменяем остальные элементы (кроме последнего) последней строки текущего поля
                for (int col = 1; col < column - 1; col++)
                {
                    mines = 0;

                    if (input[i][col - 1] == '*')
                        mines++;
                    if (input[i][col + 1] == '*')
                        mines++;
                    if (input[i - 1][col] == '*')
                        mines++;
                    if (input[i - 1][col - 1] == '*')
                        mines++;
                    if (input[i - 1][col + 1] == '*')
                        mines++;

                    if (currentLine[col] != '*')
                        currentLine = currentLine.Remove(col, 1).Insert(col, mines.ToString());
                }
                mines = 0;

                // Изменяем последний элемент последней строки текущего поля
                if (input[i][column - 1] == '*')
                    mines++;
                if (input[i - 1][column - 1] == '*')
                    mines++;
                if (input[i - 1][column - 2] == '*')
                    mines++;

                if (currentLine[column - 1] != '*')
                    currentLine = currentLine.Remove(column - 1, 1).Insert(column - 1, mines.ToString());

                // Помещаем текущую строку в результат
                output.Add(currentLine);
            }

            return output;
        }
    }
}
